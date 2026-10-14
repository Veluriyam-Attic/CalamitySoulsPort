// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchTarragon（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchTarragonV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Tarragon";
            ChineseName = "龙蒿";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「丛林之心与你同在。」");
                QuickModiLine(tooltips, "击中敌人生成多种增益叶片", ModContent.ItemType<CalamityMod.Items.Armor.Tarragon.TarragonBreastplate>());
                QuickModiLine(tooltips, "同时拥有所有增益叶片的玩家不会受到伤害", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Heart of the jungle is with you.」");
            QuickModiLine(tooltips, "Hitting enermies spawn varies of bonus leaves", ModContent.ItemType<CalamityMod.Items.Armor.Tarragon.TarragonBreastplate>());
            QuickModiLine(tooltips, "Player who have all leaves at the same time wont take any damage", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchTarragon = CSToggleV104.EnchTarragon;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Tarragon");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Tarragon.TarragonBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Tarragon.TarragonLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.TarragonThrowingDart>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
