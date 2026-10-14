// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Force/ForceCreature.cs ｜ 对应旧版类：ForceCreature（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Force
{
    public class ForceCreatureV104 : V104ForceBase
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Creature";
            ChineseName = "生灵";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                V104ForceBase.QuickModiDescription(tooltips, "生灵为寰宇之主。");
                V104ForceBase.QuickModiLine(tooltips, "获得永久渊洋馈赠增益", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchVictideV104>());
                V104ForceBase.QuickModiLine(tooltips, "永久酸蚀雕像效果，你的攻击造成大量减益", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSulphurousV104>());
                V104ForceBase.QuickModiLine(tooltips, "向周围敌人射出珍珠", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchMolluskV104>());
                V104ForceBase.QuickModiLine(tooltips, "你的周围环绕着硫海剧毒区域", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAcidivineV104>());
                V104ForceBase.QuickModiLine(tooltips, "召唤摧毁敌人的幻渊小鱼", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchFathomV104>());
                V104ForceBase.QuickModiLine(tooltips, "掠夺者魔石的防御、速度和探索效果", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchReaverV104>());
                V104ForceBase.QuickModiLine(tooltips, "无视敌人护甲，召唤所向披靡的触手护你战斗", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchOmegaV104>());
                return;
            }
            V104ForceBase.QuickModiDescription(tooltips, "Creature is the owner of cosmos.");
                V104ForceBase.QuickModiLine(tooltips, "Permanent Aquatic Blessing buff", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchVictideV104>());
                V104ForceBase.QuickModiLine(tooltips, "Permanent effigy of decay effect, your attack inflicts many debuffs", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSulphurousV104>());
                V104ForceBase.QuickModiLine(tooltips, "Shoot pearls towards nearby enermies", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchMolluskV104>());
                V104ForceBase.QuickModiLine(tooltips, "Sulphurous area are around you", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAcidivineV104>());
                V104ForceBase.QuickModiLine(tooltips, "Spawn a fathom fish that destroy enermies", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchFathomV104>());
                V104ForceBase.QuickModiLine(tooltips, "Defense, speed and explore effect of Reaver Enchantment", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchReaverV104>());
                V104ForceBase.QuickModiLine(tooltips, "Ignore enermy defense, summon unstoppable tentacles protect you in combat", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchOmegaV104>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().ForceCreature = CSToggleV104.ForceCreature;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchMolluskV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAcidivineV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchFathomV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchReaverV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchOmegaV104>());
            recipe.AddTile(ModContent.TileType<global::CalamitySouls.Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}
