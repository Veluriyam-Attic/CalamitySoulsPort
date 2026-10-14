using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using System.Linq;
using CalamityMod;

namespace CalamitySouls.Ench
{
    public abstract class BasicEnch : ModItem
    {
        public override void SetStaticDefaults()
        {
            SetName(out string Name, out string ChineseName, out int lineAmount);
            // 需求：两套魔石都要加标记 —— 这是旧版那一套
            CSCompat.SeedDisplayName(this, ChineseName + "魔石（旧版）");
            string tt = "0";
            for (int i = 1; i < lineAmount; i++)
            {
                tt += "\ni";
            }
            CSCompat.SeedTooltip(this, tt);
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 5);
            SetEnchDefaults();
        }
        public virtual void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Basic";
            ChineseName = "基础";
            lineAmount = 1;
        }
        public virtual void SetEnchDefaults()
        {
        }
        public static void QuickModiLine(List<TooltipLine> tooltips, ref int nextLine, string text)
        {
            foreach (TooltipLine ttline in tooltips)
            {
                if (ttline.Mod == "Terraria" && ttline.Name == "Tooltip" + nextLine.ToString())
                {
                    ttline.Text = text;
                    break;
                }
            }
            nextLine++;
        }
        public static void WriteASpellEffect(List<TooltipLine> tooltips, int nextLine, int[] enchCombainations, string text)
        {
            int index = tooltips.FindIndex((TooltipLine x) => x.Name == ("Tooltip" + (nextLine - 1).ToString()) && x.Mod == "Terraria");
            if (index == -1) return;
            string tText = "";
            for (int i = 0; i < enchCombainations.Length; i++) tText += "[i:+" + enchCombainations[i] + "]";
            Item item = new Item(); item.SetDefaults(enchCombainations[1]); string name = item.Name;
            string iText = "With " + name;
            if (CSUtils.GameCultureChinese) iText = "与" + name + "连携效果";
            iText = "「[c/" + Main.DiscoR.Trans10to16under256() + Main.DiscoG.Trans10to16under256() + Main.DiscoB.Trans10to16under256() + ":" + iText + "]」";
            text = tText + iText + text;
            tooltips.Insert(index + 1, new TooltipLine(CalamitySouls.Instance, "SpellLine", text));
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            maxFallSpeed = 0;
        }
    }
}