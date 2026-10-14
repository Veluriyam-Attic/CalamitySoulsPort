using Terraria;
using CalamityMod.Items.Armor.Prismatic;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Items.Accessories;
using CalamitySouls.Items.OtherItems;

namespace CalamitySouls.Ench
{
    public class EnchPrismatic : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Prismatic";
            ChineseName = "光棱";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "FF6AF6";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「棱镜模块运转正常，光能聚焦中。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "你的攻击会伴随着强大的光束");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<PrismaticPrism>(), color) +
                    "召唤一个在你周围环绕，改变敌对弹幕方向的棱镜");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "敌人的攻击将会拥有预警线");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Prism sensors stable, focusing light energy.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Your attack will be followed by powerful lasers");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<PrismaticPrism>(), color) +
                "Spawn a prism that circle around you and change hostile projectile's direction");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Enermies' attack will have their trail shown");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchPrismatic = CSToggle.EnchPrismatic;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Prismatic.PrismaticHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Prismatic.PrismaticRegalia>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Prismatic.PrismaticGreaves>());
            recipe.AddIngredient(ModContent.ItemType<PrismaticPrism>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}