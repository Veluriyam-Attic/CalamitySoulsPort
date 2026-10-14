// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchExtermination（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchExterminationV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Extermination";
            ChineseName = "绝灭";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「人无一物以报天。」");
                QuickModiLine(tooltips, "给予你周围的生物和弹幕以更强的重力", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「All things die.」");
            QuickModiLine(tooltips, "Inflict stronger gravity on creatures and projectiles around you", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchExtermination = CSToggleV104.EnchExtermination;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.RealmRavager>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.CraniumSmasher>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
