// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchForbidden（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchForbiddenV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Forbidden";
            ChineseName = "禁戒";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「面对沙暴吧。」");
                QuickModiLine(tooltips, "抵消手持其他武器时的召唤伤害减免", ModContent.ItemType<CalamityMod.Items.Armor.ForbiddenCirclet>());
                return;
            }
            QuickModiLine(tooltips, "「Now face the sandstorm」");
            QuickModiLine(tooltips, "The minion damage nerf while wielding weaponry is cancelled", ModContent.ItemType<CalamityMod.Items.Armor.ForbiddenCirclet>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchForbidden = CSToggleV104.EnchForbidden;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.ForbiddenCirclet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.ForsakenSaber>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.MineralMortar>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.RelicofRuin>());
            recipe.AddIngredient(ItemID.AncientBattleArmorShirt);
            recipe.AddIngredient(ItemID.AncientBattleArmorPants);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
