// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchStatigel（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchStatigelV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Statigel";
            ChineseName = "斯塔提斯";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「传说忍者之魂。」");
                QuickModiLine(tooltips, "在你周围生成擦弹区域，检测经过的敌对生物或弹幕", Item.type);
                QuickModiLine(tooltips, "如果检测成功，则会给予你短暂的无敌帧和伤害提升", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Spirit of the legendary ninja.」");
            QuickModiLine(tooltips, "Cast a graze area around you that detects nearby hostile creatures and projectiles", Item.type);
            QuickModiLine(tooltips, "The area give you extra immunity frames and damage boost on detecting", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchStatigel = CSToggleV104.EnchStatigel;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Statigel");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Statigel.StatigelArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Statigel.StatigelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.GeliticBlade>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Goobow>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.GelDart>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
