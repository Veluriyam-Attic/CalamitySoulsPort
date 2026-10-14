// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchFathom（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchFathomV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Fathom Swarmer";
            ChineseName = "幻渊鱼群";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「合则存，分必亡。」");
                QuickModiLine(tooltips, "在深渊时自动回复呼吸槽", ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBreastplate>());
                QuickModiLine(tooltips, "召唤七个摧毁敌人的幻渊小鱼", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「United we stand, divided we fall.」");
            QuickModiLine(tooltips, "You are capable of recovering breath constantly in the abyss", ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBreastplate>());
            QuickModiLine(tooltips, "Spawn seven fathom fishes that destroy enermies", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchFathom = CSToggleV104.EnchFathom;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerVisage>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBoots>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.DreadmineStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Apoctolith>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
