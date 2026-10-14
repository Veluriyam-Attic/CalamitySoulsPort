using Terraria;
using CalamityMod.Items.Armor.OmegaBlue;
using CalamityMod.Items.Weapons.Typeless;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchOmega : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Omega Blue";
            ChineseName = "欧米伽蓝";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "4887CD";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「他们应当为自己的所作所为付出代价。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "触手会汲取敌人生命治疗你");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "你常驻深渊狂乱状态");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「They'd pay for what they've done.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Tentacles heal you by sucking enemy life");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "You always have abyssal madness activated");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchOmega = CSToggle.EnchOmega;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueTentacles>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.CrescentMoon>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Typeless.StratusSphere>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CalamarisLament>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Valediction>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.DeepSeaDumbbell>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.JawsOfOblivion>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
