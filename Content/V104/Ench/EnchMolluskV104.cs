// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchMollusk（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchMolluskV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Mollusk";
            ChineseName = "软壳";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「Clamity」");
                QuickModiLine(tooltips, "将周围大片区域浸入水中", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Clamity」");
            QuickModiLine(tooltips, "Make a large area covered with water", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchMollusk = CSToggleV104.EnchMollusk;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Mollusk.MolluskShellmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Mollusk.MolluskShellplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Mollusk.MolluskShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.ClamorRifle>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.Serpentine>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
