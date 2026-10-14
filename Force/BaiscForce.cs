using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using CalamityMod;

namespace CalamitySouls.Force
{
    public abstract class BasicForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            SetName(out string NM, out string CNM);
            CSCompat.SeedDisplayName(this, CNM + "之力");
        }
        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 50;
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
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "Tooltip0" && line.Mod == "Terraria")
                {
                    line.Text = "「" + text + "」";
                    return;
                }
            }
        }
        public static void QuickModiLine(List<TooltipLine> tooltips, int ItemID, string text)
        {
            int index = tooltips.FindLastIndex((TooltipLine x) => x.Name.StartsWith("ForceLine") && x.Mod == "CalamitySouls");
            if (index == -1) index = tooltips.FindIndex((TooltipLine x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            if (index == -1) return;

            Item item = new Item(); item.SetDefaults(ItemID);
            string iText = "「[c/" + Main.DiscoR.Trans10to16under256() + Main.DiscoG.Trans10to16under256() + Main.DiscoB.Trans10to16under256() + ":" + item.Name + "]」";
            text = "[i:+" + ItemID + "]" + iText + text;

            index++;
            tooltips.Insert(index, new TooltipLine(CalamitySouls.Instance, "ForceLine " + index.ToString(), text));
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            maxFallSpeed = 0;
        }
    }
}