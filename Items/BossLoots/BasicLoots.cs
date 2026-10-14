using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CalamitySouls.Items.BossLoots
{
    public abstract class BasicLoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(gold: 12);
            Item.expert = true;
            Item.expertOnly = true;
            SafeSetDefaults();
        }
        public virtual void SafeSetDefaults() { }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    line.OverrideColor = Color.Gold;
                }
            }
        }
    }
    // 1.4：Recipe 已 sealed，无法继承；可用性改用条件表达（见各配方的 AddCondition）
}