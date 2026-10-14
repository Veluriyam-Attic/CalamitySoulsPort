// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchGodslayer（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchGodslayerV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Godslayer";
            ChineseName = "弑神者";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = V104Utils.TooltipHotkeyString(CalamitySouls.GodslayerDashKey);
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「还没完，小鬼！」");
                QuickModiLine(tooltips, "按" + hotkey + "向鼠标方向冲刺，冷却时间15秒", ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerChestplate>());
                QuickModiLine(tooltips, "让你下落更快并免疫坠落伤害", ModContent.ItemType<EnchAerospecV104>());
                QuickModiLine(tooltips, "冲刺会伴随强大的神明吞噬者幻影，清除一切途径敌对弹幕", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「It's not over yet, kid!」");
            QuickModiLine(tooltips, "Press " + hotkey + " to dash for an immense distance towards your mouse with a 15s cooldown", ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerChestplate>());
            QuickModiLine(tooltips, "Allows you to fall more quickly and immune to fall damage", ModContent.ItemType<EnchAerospecV104>());
            QuickModiLine(tooltips, "A powerful devourer phantom follows you when dashing, clearing all hostile projectiles passed by", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchGodslayer = CSToggleV104.EnchGodslayer;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Godslayer");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<EnchAerospecV104>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.CleansingBlaze>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.SoulPiercer>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
