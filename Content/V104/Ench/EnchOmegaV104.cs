// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchOmega（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchOmegaV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Omega Blue";
            ChineseName = "欧米伽蓝";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「他们应当为自己的所作所为付出代价。」");
                QuickModiLine(tooltips, "召唤所向披靡的触手护你战斗", ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueChestplate>());
                QuickModiLine(tooltips, "敌人的护甲越高，你对其造成的伤害越高", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「They'd pay for what they've done.」");
            QuickModiLine(tooltips, "Summon unstoppable tentacles protect you in combat", ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueChestplate>());
            QuickModiLine(tooltips, "You deal more damage when enermies' defense increases", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchOmega = CSToggleV104.EnchOmega;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.OmegaBlue.OmegaBlueTentacles>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.CrescentMoon>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.DeepSeaDumbbell>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
