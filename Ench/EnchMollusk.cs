using Terraria;
using CalamityMod.Items.Armor.Mollusk;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchMollusk : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Mollusk";
            ChineseName = "软壳";
            lineAmount = 5;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "7FFFFD";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「Clamity」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "你无视水的阻力，且蛤对你友好");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "当拥有任意套装效果时，大大增强最大等级的饰品前缀");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "险恶+2%易伤，幸运+2%暴伤，敏捷+4%最大移速，暴力+4%武器使用速度");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "护佑+1%额外免伤，奥秘-5%蓝耗，寂静+8%盗贼弹速");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchMollusk>(),
                    ModContent.ItemType<EnchVictide>()
                }, "获得一个强大的冲刺");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Clamity」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Grant better mobility in water, clams become friendly");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "When you have any kind of set bonus, greatly buff your accessories prefixes if they are at maxinum level");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Menacing make enermies take 2% more damage, Lucky increase crit damage by 2%, Quick +4% max speed, Violent +4% weapon fire rate");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Warding +1% extra damage reduction, Arcane -5% mana cost, Silent +8% rogue velocity");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchMollusk>(),
                ModContent.ItemType<EnchVictide>()
            }, "Grant a powerful dash");
        }
        public override void SetEnchDefaults()
        {
            Item.defense = 10;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchMollusk = CSToggle.EnchMollusk;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Mollusk.MolluskShellmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Mollusk.MolluskShellplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Mollusk.MolluskShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.ClamCrusher>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.ClamorRifle>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.Poseidon>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.Serpentine>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.ShellfishStaff>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}