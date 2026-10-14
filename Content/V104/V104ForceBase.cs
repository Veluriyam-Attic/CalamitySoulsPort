using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 「CalamitySouls v1.0.4.0（新版）」的「力」基类。
	// 对应新版 1.3 源码 CalamitySouls.Force.BasicForce。
	// 旧版那一套是 CalamitySouls.Force.BasicForce，两者互不影响。
	public abstract class V104ForceBase : ModItem
	{
		public override void SetStaticDefaults()
		{
			SetName(out string NM, out string CNM);
			CSCompat.SeedDisplayName(this, CNM + "之力（新版）");
			CSCompat.SeedTooltip(this, "[c/36A442:---力（新版）---]");
		}

		public override void SetDefaults()
		{
			Item.width = Item.height = 86;
			Item.accessory = true;
			Item.value = Item.sellPrice(1, 50);
			SetForceDefaults();
		}

		public virtual void SetName(out string DisplayName, out string ChineseName)
		{
			DisplayName = "Basic";
			ChineseName = "基础";
		}

		public virtual void SetForceDefaults()
		{
		}

		public static void QuickModiDescription(List<TooltipLine> tooltips, string text)
		{
			QuickModiLine(tooltips, "「" + text + "」", -1);
		}

		public static void QuickModiLine(List<TooltipLine> tooltips, string text, int ItemID = -1)
		{
			int index = tooltips.FindLastIndex(x => x.Name != null && x.Name.StartsWith("V104ForceLine") && x.Mod == "CalamitySouls");
			if (index == -1) index = tooltips.FindIndex(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
			if (index == -1) return;
			string rgb = Main.DiscoR.Trans10to16under256() + Main.DiscoG.Trans10to16under256() + Main.DiscoB.Trans10to16under256();
			if (ItemID != -1) text = "[i:" + ItemID + "][c/" + rgb + "::]" + text;
			index++;
			tooltips.Insert(index, new TooltipLine(CalamitySouls.Instance, "V104ForceLine" + index, text));
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			maxFallSpeed = 0f;
		}
	}
}
