// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchAstral（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchAstralV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Astral";
            ChineseName = "幻星";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「愿群星指引你的前路。」");
                QuickModiLine(tooltips, "击中敌人使得群星从天而降", ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralBreastplate>());
                QuickModiLine(tooltips, "一个小卫星为你回复生命值", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「May the stars guide your way.」");
            QuickModiLine(tooltips, "Stars falls from heaven whenever you hit an enery", ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralBreastplate>());
            QuickModiLine(tooltips, "A mini satellite spawn healing stars for you", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchAstral = CSToggleV104.EnchAstral;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralHelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.AstralPike>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.AstralBlaster>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.AstralStaff>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
