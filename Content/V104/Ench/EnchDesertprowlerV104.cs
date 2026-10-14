// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchDesertprowler（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchDesertprowlerV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Desert Prowler";
            ChineseName = "沙漠游侠";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「面对沙暴吧。」");
                QuickModiLine(tooltips, "增加5点武器面板", ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerShirt>());
                return;
            }
            QuickModiLine(tooltips, "「Now face the sandstorm」");
            QuickModiLine(tooltips, "Your weapon grant 5 more damage", ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerShirt>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchDesert = CSToggleV104.EnchDesert;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerHat>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerShirt>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerPants>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.StormjawStaff>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
