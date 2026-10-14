// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchHydrothermic（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchHydrothermicV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Hydrothermic";
            ChineseName = "渊泉";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「让火焰净化一切。」");
                QuickModiLine(tooltips, "你周围环绕着高伤害日炎环", ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicArmor>());
                QuickModiLine(tooltips, "让你的所有武器都附带火焰属性", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Now the fire cleans the place.」");
            QuickModiLine(tooltips, "You are arounded by high-damage solar area", ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicArmor>());
            QuickModiLine(tooltips, "Make all your weapon imbued with fire", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchHydrothermic = CSToggleV104.EnchHydrothermic;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Hydrothermic");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicSubligar>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.HellfireFlamberge>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Helstorm>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
