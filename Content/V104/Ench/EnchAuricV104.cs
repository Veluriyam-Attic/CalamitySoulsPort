// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchAuric（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchAuricV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Auric";
            ChineseName = "金源";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「臣服。」");
                QuickModiLine(tooltips, "每隔一段时间释放金源冲击波，摧毁途经的一切", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Surrender.」");
            QuickModiLine(tooltips, "Periodly release auric flash destroying everything it passes by", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchAuric = CSToggleV104.EnchAuric;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Auric");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaCuisses>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Ataraxia>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.TheWand>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurnitureAuric.AuricToilet>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
