// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchBloodflare（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchBloodflareV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Bloodflare";
            ChineseName = "血炎";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = V104Utils.TooltipHotkeyString(CalamitySouls.BrimflameKey);
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「神弃的烈炎在黑暗中滋生。」");
                QuickModiLine(tooltips, "按" + hotkey + "释放迷失灵魂摧毁敌人", ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareBodyArmor>());
                QuickModiLine(tooltips, "硫火魔石的效果", ModContent.ItemType<EnchBrimflameV104>());
                QuickModiLine(tooltips, "召唤一只强化的灾厄宠物为你而战", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Forsaken Flare infestates in the darkness.」");
            QuickModiLine(tooltips, "Press " + hotkey + " to unleash the lost souls of polterghast to destroy your enemies", ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareBodyArmor>());
            QuickModiLine(tooltips, "Effect of brimflame enchantment", ModContent.ItemType<EnchBrimflameV104>());
            QuickModiLine(tooltips, "Summon a reinforced pet calamtas to fight for you", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchBloodFlare = CSToggleV104.EnchBloodFlare;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Bloodflare");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareCuisses>());
            recipe.AddIngredient(ModContent.ItemType<EnchBrimflameV104>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.TheMutilator>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.BloodBoiler>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.SanguineFlare>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
