// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchTitan（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchTitanV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Titan Heart";
            ChineseName = "泰坦之心";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「抛瓦！...但是泰拉人，代价是什么呢？」");
                QuickModiLine(tooltips, "非穿透主要弹幕击中物体时会对周围造成震荡", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「POWER!...And what, Terrarian, must we give in return?」");
            QuickModiLine(tooltips, "Non-penetrate projectiles explodes on death", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchTitan = CSToggleV104.EnchTitan;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartMantle>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartBoots>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Weapons.MonolithStick>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Weapons.MonolithRay>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Fishing.AstralCatches.AstrophageItem>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
