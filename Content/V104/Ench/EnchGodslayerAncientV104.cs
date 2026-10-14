// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchGodslayerAncient（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchGodslayerAncientV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Ancient Godslayer";
            ChineseName = "远古弑神者";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「追随神谕。」");
                QuickModiLine(tooltips, "你留下帅气的残影", ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerChestplate>());
                QuickModiLine(tooltips, "将你随机传送到周围来躲开伤害", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Follow the Oracle.」");
            QuickModiLine(tooltips, "You leave cool afterimage", ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerChestplate>());
            QuickModiLine(tooltips, "Teleport you to a random position to avoid getting hit", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchGodslayerAncient = CSToggleV104.EnchGodslayerAncient;
        }
        public override void UpdateEquip(Player player)
        {
            player.V104().gsaShadow = true;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerHelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.MirrorBlade>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.TheStorm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.Cosmilamp>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
