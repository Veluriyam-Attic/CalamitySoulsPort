using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Souls
{
	// 「CalamitySouls v1.0.4.0（新版）」的「魂」基类 —— 对应新版 1.3 的 CalamitySouls.Souls.BasicSoul。
	// 新版魂的机制：MaterialList 里全是灾厄饰品，佩戴时自动把这些饰品的 UpdateAccessory 逐个跑一遍。
	// 旧版那一套是 CalamitySouls.Souls.BasicSoul（只由 5 个「力」合成），两者互不影响。
	public abstract class V104SoulBase : ModItem
	{
		public override void SetStaticDefaults()
		{
			SetName(out string NM, out string CNM);
			CSCompat.SeedDisplayName(this, CNM + "之魂（新版）");
			// 需求：新版「魂」加「（新版）」标记
			CSCompat.SeedTooltip(this, needRock ? "[c/0072FF:---组合魂（新版）---]" : "[c/0072FF:---魂（新版）---]");
			SetSoulStaticDefaults();
		}

		public virtual void SetName(out string DisplayName, out string ChineseName)
		{
			DisplayName = "Basic";
			ChineseName = "基础";
		}

		public virtual void SetSoulStaticDefaults() { }

		public override void SetDefaults()
		{
			Item.accessory = true;
			Item.value = Item.sellPrice(5);
			SetSoulDefaults();
		}

		public virtual void SetSoulDefaults() { }

		public abstract List<int> MaterialList { get; }

		public virtual bool NeedAutoUpdate => true;
		public virtual bool needRock => false;
		public virtual void UpdateSoul(Player player, bool hideVisual) { }
		public virtual void PreModifyBaseTooltips(List<TooltipLine> tooltips) { }
		public virtual void PostModifyBaseTooltips(List<TooltipLine> tooltips) { }

		// 该魂在配置页里的开关名（子类覆写）
		public virtual string ToggleName => null;
		// 该魂的物品类型（用于新旧魂互斥判定）
		public virtual int SoulType => Type;

		private bool ToggleOn => ToggleName == null || CSToggleV104.GetSoulToggle(ToggleName);

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// 需求 3：默认不允许新旧魂同时装备。
			// 判定范围只限"最顶层的魂"——魔石与饰品可以随意混搭。
			if (!CSToggleV104.AllowMixedSouls && SoulExclusion.HasOldSoulEquipped(player, Item.type)) return;
			if (!ToggleOn) return;
			if (NeedAutoUpdate)
			{
				for (int i = 0; i < MaterialList.Count; i++)
				{
					if (!CSToggleV104.GetSoulSubToggle(ToggleName, MaterialList[i])) continue;   // level-2 toggle (per-soul)
					CSUtils.UpdateAccessory(MaterialList[i], player, hideVisual);
				}
			}
			UpdateSoul(player, hideVisual);
		}

		public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			PreModifyBaseTooltips(tooltips);
			if (!CSToggleV104.AllowMixedSouls && SoulExclusion.HasOldSoulEquipped(Main.LocalPlayer, Item.type))
			{
				QuickAddSoulLine(tooltips, "「你已佩戴旧版之魂，新版之魂不会生效（可在配置页第三栏放开）」", -1);
			}
			if (NeedAutoUpdate)
			{
				for (int i = 0; i < MaterialList.Count; i++)
				{
					if (V104Utils.UsingChi) QuickAddSoulLine(tooltips, GetItemName(MaterialList[i]) + "的效果", MaterialList[i]);
					else QuickAddSoulLine(tooltips, "Effect of " + GetItemName(MaterialList[i]), MaterialList[i]);
				}
			}
			PostModifyBaseTooltips(tooltips);
		}

		public static void QuickAddSoulTip(List<TooltipLine> tooltips, string text)
		{
			QuickAddSoulLine(tooltips, "「" + text + "」", -1);
		}

		public static string GetItemName(int ItemID)
		{
			Item item = new Item();
			item.SetDefaults(ItemID);
			return item.Name;
		}

		public static void QuickAddSoulLine(List<TooltipLine> tooltips, string text, int ItemID = -1)
		{
			int index = tooltips.FindLastIndex(x => x.Name != null && x.Name.StartsWith("V104SoulLine") && x.Mod == "CalamitySouls");
			if (index == -1) index = tooltips.FindIndex(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
			if (index == -1) return;
			string rgb = Main.DiscoR.Trans10to16under256() + Main.DiscoG.Trans10to16under256() + Main.DiscoB.Trans10to16under256();
			if (ItemID != -1) text = "[i:" + ItemID + "][c/" + rgb + ":" + text + "]";
			index++;
			tooltips.Insert(index, new TooltipLine(CalamitySouls.Instance, "V104SoulLine" + index, text));
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			maxFallSpeed = 0f;
		}

		// 新版基类：配方 = MaterialList 里的每一样 + 恶魔祭坛（组合魂额外需要 PlacedRock）
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			for (int i = 0; i < MaterialList.Count; i++)
			{
				recipe.AddIngredient(MaterialList[i]);
			}
			// [2026-10-04 用户要求] 组合魂不再需要「放置岩石(PlacedRock)」制作台，统一只留魔影坩埚
			// 原代码：if (needRock) recipe.AddTile(ModContent.TileType<CalamityMod.Tiles.PlacedRock>());
			recipe.AddTile(ModContent.TileType<global::CalamitySouls.Tiles.DemonshadeWorkbenchTile>());
			recipe.Register();
		}
	}
}
