// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchBrimflame（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchBrimflameV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Brimflame";
            ChineseName = "硫火";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = V104Utils.TooltipHotkeyString(CalamitySouls.BrimflameKey);
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「被遗忘的女神蔑视着这些只知道寻求力量的可怜虫子。」");
                QuickModiLine(tooltips, "按" + hotkey + "开启硫火暴乱效果，增加35%伤害", ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameRobes>());
                QuickModiLine(tooltips, "硫火暴乱持续10秒，冷却时间25秒", ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameRobes>());
                QuickModiLine(tooltips, "当硫火暴乱不在冷却时，你的攻击会唤出硫火射线攻击敌人", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「The Forgotten Goddess looks with contempt at those poor insects seeking for power.」");
            QuickModiLine(tooltips, "Press " + hotkey + " to trigger a brimflame frenzy effect, +35% damage", ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameRobes>());
            QuickModiLine(tooltips, "The frenzy lasts for 10s with a 25s cooldown", ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameRobes>());
            QuickModiLine(tooltips, "Your attack summons brimflame laser to attack enermies while the frenzy isn't recharging", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchBrimFlame = CSToggleV104.EnchBrimFlame;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameCowl>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameRobes>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameBoots>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.BrimstoneFury>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.BrimroseStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Brimblade>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
