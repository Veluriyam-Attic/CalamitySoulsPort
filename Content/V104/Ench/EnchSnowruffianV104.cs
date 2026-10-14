// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchSnowruffian（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchSnowruffianV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Snow Ruffian";
            ChineseName = "雪境暴徒";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「他们从我们手中夺取了一切，现在是时候夺回来了。」");
                QuickModiLine(tooltips, "大大增加在空中时的水平移速", ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianChestplate>());
                QuickModiLine(tooltips, "冰冻周围敌人使其难以移动", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「They took everything from us, now let's get these back.」");
            QuickModiLine(tooltips, "Greatly increases your horizonal speed when in the air", ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianChestplate>());
            QuickModiLine(tooltips, "Freezes nearby enermies making them hard to move", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchSnowRuffian = CSToggleV104.EnchSnowRuffian;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianGreaves>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Weapons.FrostPaintgun>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Weapons.IcicleDagger>());
            recipe.AddIngredient(ItemID.IceBoomerang);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
