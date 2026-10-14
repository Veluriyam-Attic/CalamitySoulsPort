// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchExo（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchExoV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Exo";
            ChineseName = "星流";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「……没这个必要。」");
                QuickModiLine(tooltips, "受击生成阻挡高于50的伤害的护盾", Item.type);
                QuickModiLine(tooltips, "护盾在10秒后或者阻挡一次伤害后破碎", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「...Quite unnecessary.」");
            QuickModiLine(tooltips, "Cast a shield when taking damage, blocking any damage higher than 50", Item.type);
            QuickModiLine(tooltips, "The shield cracks upon blocking damage or after 10s", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchExo = CSToggleV104.EnchExo;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.DraedonMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AresMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.ArtemisMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.ApolloMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.ThanatosMask>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
