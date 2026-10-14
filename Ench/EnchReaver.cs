using Terraria;
using CalamityMod.Items.Armor.Reaver;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchReaver : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Reaver";
            ChineseName = "掠夺者";
            lineAmount = 5;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "91CB66";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「*悠久的蒸汽机关*」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "击中敌人产生大量孢子云，攻击时每过一段时间射出追踪火箭");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "受到伤害获得近战加成并炸开大片孢子");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<CalamityMod.Items.Accessories.NecklaceofVexation>(), color) +
                    "半血以下增加20%伤害，攻击造成脓血、咒火和剧毒");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "苍华之庭速度加倍");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「*Long-standing stream engine*」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Enermy hit spawn plenty of spore clouds, your attack release chasing rocket sometimes");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Grant melee bonus and burst out many spores on hurt");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<CalamityMod.Items.Accessories.NecklaceofVexation>(), color) +
                "Extra 20% damage under half life, attacks inflict ichor, cursed inferno and venom");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Plantation Staff minion has doubled speed");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchReaver = CSToggle.EnchReaver;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Reaver");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverCuisses>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.NecklaceofVexation>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.FeralthornClaymore>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.HellionFlowerSpear>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Quagmire>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.EvergladeSpray>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.MangroveChakram>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}