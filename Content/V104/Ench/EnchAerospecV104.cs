// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchAerospec（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchAerospecV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Aerospec";
            ChineseName = "天蓝";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string key = CalamitySouls.AeroDashKey.TooltipHotkeyString();
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「蓝天之音于你耳畔萦绕。」");
                QuickModiLine(tooltips, "按" + key + "进行一次天蓝冲刺", Item.type);
                QuickModiLine(tooltips, "冲刺太频繁会降低你的伤害", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Let the voice of sky take you higher.」");
            QuickModiLine(tooltips, "Press " + key + " to execute an aero dash", Item.type);
            QuickModiLine(tooltips, "Dashing too frequently decreases your damage", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchAerospec = CSToggleV104.EnchAerospec;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Aerospec");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.GoldplumeSpear>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Galeforce>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
