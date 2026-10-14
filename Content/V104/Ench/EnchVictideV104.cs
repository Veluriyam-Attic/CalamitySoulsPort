// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchVictide（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchVictideV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Victide";
            ChineseName = "胜潮";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「胜者书写历史。」");
                QuickModiLine(tooltips, "击中敌人时，召唤两只小海星追击", ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideBreastplate>());
                return;
            }
            QuickModiLine(tooltips, "「Only victory leads the history.」");
            QuickModiLine(tooltips, "Two mini starfish chase after your enermy when you hit them", ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideBreastplate>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchVictide = CSToggleV104.EnchVictide;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Victide");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.CoralSpout>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.PolypLauncher>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
