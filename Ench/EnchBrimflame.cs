using Terraria;
using CalamityMod.Items.Armor.Brimflame;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchBrimflame : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Brimflame";
            ChineseName = "硫火";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "E34F4F";
            string hotkey = CSUtils.TooltipHotkeyString(CalamitySouls.BrimflameKey);
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「被遗忘的女神蔑视着这些只知道寻求力量的可怜虫子。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "免疫大多数火系列减益，按" + hotkey + "开启硫火暴乱效果，期间你多造成25%伤害");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "硫火暴乱持续10秒，冷却时间25秒");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "释放硫火暴乱的同时释放扫射的硫火激光，随机消除一段距离内的弹幕");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchBrimflame>(),
                    ModContent.ItemType<EnchBloodflare>()
                }, "释放硫火暴乱时，会进行一次强大的血炎爆破");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「The Forgotten Goddess looks with contempt at those poor insects seeking for power.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Immnue to most fire-relative debuffs, Press " + hotkey + " to trigger a brimflame frenzy effect, during which you deal 25% more damage");
            QuickModiLine(tooltips, ref nextLine, "" +
                "The frenzy lasts for 10s with a 25s cooldown");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Releasing a frenzy causes you to release brimstone rays which randomly clear projectiles within a certain range");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchBrimflame>(),
                ModContent.ItemType<EnchBloodflare>()
            }, "A great blood eruption is released when a brimflame fenzy is triggered");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchBrim = CSToggle.EnchBrim;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameCowl>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameRobes>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Brimflame.BrimflameBoots>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.BrimstoneFury>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.BrimroseStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.BurningSea>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.IgneousExaltation>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Brimblade>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}