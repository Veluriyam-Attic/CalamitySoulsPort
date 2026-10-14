// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchPlagueReaper（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchPlagueReaperV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Plague Reaper";
            ChineseName = "瘟疫收割者";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「无知而浅薄的生命只能等待收割。」");
                QuickModiLine(tooltips, "被击中时会释放当前拥有的肾上腺素，而非清空", Item.type);
                QuickModiLine(tooltips, "但由于肾上腺素飙升，受击后你的视野会短暂地模糊变暗", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Those ignorance only wait for their reaper.」");
            QuickModiLine(tooltips, "Being hit release Adrenaline you have instead of clearing it", Item.type);
            QuickModiLine(tooltips, "But your vision becomes dark as your adrenaline soaring", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchPlagueReaper = CSToggleV104.EnchPlagueReaper;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperVest>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperStriders>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Plaguenade>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurniturePlagued.PlaguedPlateBed>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
