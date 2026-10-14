// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchPrismatic（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchPrismaticV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Prismatic";
            ChineseName = "光棱";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「棱镜模块运转正常，光能聚焦中。」");
                QuickModiLine(tooltips, "召唤一个环绕你的力场，提供照明并阻挡敌人和弹幕", ModContent.ItemType<EnchWulfrumV104>());
                QuickModiLine(tooltips, "召唤一个在你周围旋转，改变敌对弹幕方向的棱镜", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Prism sensors stable, focusing light energy.」");
            QuickModiLine(tooltips, "Cast a field that provides light and stops enermies and projectiles", ModContent.ItemType<EnchWulfrumV104>());
            QuickModiLine(tooltips, "Spawn a prism that circle around you and change hostile projectile's direction", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchPrismatic = CSToggleV104.EnchPrismatic;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Prismatic.PrismaticHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Prismatic.PrismaticRegalia>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Prismatic.PrismaticGreaves>());
            recipe.AddIngredient(ModContent.ItemType<EnchWulfrumV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Accessories.PrismaticPrism>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
