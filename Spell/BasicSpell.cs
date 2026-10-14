using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using CalamityMod;

namespace CalamitySouls.Spell
{
    public abstract class BasicSpell : ModItem
    {
        public sealed override void SetStaticDefaults()
        {
            SetName(out string Name, out string ChineseName);
            CSCompat.SeedDisplayName(this, ChineseName + "之咒");
            string tt = "0";
            CSCompat.SeedTooltip(this, tt);
        }
        public sealed override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 5);
            SetSpellDefaults();
        }
        public virtual void SetName(out string Name, out string ChineseName)
        {
            Name = "Basic";
            ChineseName = "基础";
        }
        public virtual void SetSpellDefaults()
        {
        }
        public static void QuickModiDescription(List<TooltipLine> tooltips, string text)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "Tooltip0" && line.Mod == "Terraria")
                {
                    line.Text = "「" + text + "」";
                    return;
                }
            }
        }
        public static void QuickModiLine(List<TooltipLine> tooltips, string text, int ItemID, string color)
        {
            int index = tooltips.FindLastIndex((TooltipLine x) => x.Name.StartsWith("SpellLine") && x.Mod == "CalamitySouls");
            if (index == -1) index = tooltips.FindIndex((TooltipLine x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            if (index == -1) return;

            Item item = new Item(); item.SetDefaults(ItemID);
            string iText = "「[c/" + color + ":" + item.Name + "]」";
            text = "[i:+" + ItemID + "]" + iText + text;

            index++;
            tooltips.Insert(index, new TooltipLine(CalamitySouls.Instance, "SpellLine " +  index.ToString(), text));
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            maxFallSpeed = 0;
        }
    }
}