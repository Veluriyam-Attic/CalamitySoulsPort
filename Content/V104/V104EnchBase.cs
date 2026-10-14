using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 「CalamitySouls v1.0.4.0（新版）」魔石基类。
	// 直接对应新版 1.3 源码 CalamitySouls.Ench.BasicEnch（2 参数版 SetName + 新式 QuickModiLine）。
	// 旧版那一套走的是 CalamitySouls.Ench.BasicEnch，两者互不影响。
	public abstract class V104EnchBase : ModItem
	{
		public override void SetStaticDefaults()
		{
			SetName(out string Name, out string ChineseName);
			// 统一加「（新版）」标记，和旧版魔石的「（旧版）」区分
			CSCompat.SeedDisplayName(this, ChineseName + "魔石（新版）");
			CSCompat.SeedTooltip(this, "[c/FFA3D3:---魔石（新版）---]");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 34;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 5);
			SetEnchDefaults();
		}

		public virtual void SetName(out string Name, out string ChineseName)
		{
			Name = "Basic";
			ChineseName = "基础";
		}

		public virtual void SetEnchDefaults()
		{
		}

		// 1.3 的 QuickModiLine(tooltips, text, ItemID) 语义：在最后一条 EnchLine 之后（或 Tooltip0 之后）插入一行。
		public static void QuickModiLine(List<TooltipLine> tooltips, string text, int ItemID = -1)
		{
			int num = tooltips.FindLastIndex(x => x.Name != null && x.Name.StartsWith("V104EnchLine") && x.Mod == "CalamitySouls");
			if (num == -1)
			{
				num = tooltips.FindIndex(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
			}
			if (num == -1) return;
			string color = Main.DiscoR.Trans10to16under256() + Main.DiscoG.Trans10to16under256() + Main.DiscoB.Trans10to16under256();
			if (ItemID != -1)
			{
				text = "[i:" + ItemID + "][c/" + color + "::]" + text;
			}
			num++;
			tooltips.Insert(num, new TooltipLine(CalamitySouls.Instance, "V104EnchLine" + num, text));
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			maxFallSpeed = 0f;
		}
	}
}
