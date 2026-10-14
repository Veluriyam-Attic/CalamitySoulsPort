// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchEmpyrean（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchEmpyreanV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Empyrean";
            ChineseName = "皇天";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「裁断之权现在在你手中。」");
                QuickModiLine(tooltips, "日影魔石的效果", ModContent.ItemType<EnchUmbraphileV104>());
                QuickModiLine(tooltips, "皇天幻影为你而战", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「The justice is within your hands.」");
            QuickModiLine(tooltips, "Effects of Umbraphile Enchantment", ModContent.ItemType<EnchUmbraphileV104>());
            QuickModiLine(tooltips, "The empyrean phantasm fight for you", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchEmpyrean = CSToggleV104.EnchEmpyrean;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Empyrean.EmpyreanMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Empyrean.EmpyreanCloak>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Empyrean.EmpyreanCuisses>());
            recipe.AddIngredient(ModContent.ItemType<EnchUmbraphileV104>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.StarofDestruction>());
            // [方案甲 2026-10-03] 照 1.3 补回漏掉的 2 件，用 2.x 原版：TheEmpyrean→DeadSunsWind、XerocPitchfork→ShardofAntumbra
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.DeadSunsWind>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.ShardofAntumbra>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
