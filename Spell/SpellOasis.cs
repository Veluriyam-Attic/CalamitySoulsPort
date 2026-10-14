
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;
namespace CalamitySouls.Spell
{
    public class SpellOasis : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Oasis";
            ChineseName = "绿洲";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "CC4235";
            string color2 = "7FFFFD";
            string color3 = "985247";
            int Material1 = ItemType<EnchVictide>();
            int Material2 = ItemType<EnchMollusk>();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "生命");
                QuickModiLine(tooltips, "提供水下呼吸，消除水的阻力", Material1, color1);
                QuickModiLine(tooltips, "每半分钟获得77秒阿米迪亚斯的祝福", Material1, color1);
                QuickModiLine(tooltips, "蛤对你友好", Material2, color2);
                QuickModiLine(tooltips, "当拥有任意套装效果时，大大增强最大等级的饰品前缀", Material2, color2);
                QuickModiLine(tooltips, "获得一个强大的冲刺", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Life");
            QuickModiLine(tooltips, "Gills effect, provides increased underwater mobility", Material1, color1);
            QuickModiLine(tooltips, "Grants 77s Amidias' Blessing every half minute", Material1, color1);
            QuickModiLine(tooltips, "Clams become friendly", Material2, color2);
            QuickModiLine(tooltips, "When you have any kind of set bonus, greatly buff your accessories prefixes if they are at maxinum level", Material2, color2);
            QuickModiLine(tooltips, "Grant a powerful dash", Item.type, color3);
        }
        public override void SetSpellDefaults()
        {
            Item.defense = 17;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellOasis = CSToggle.SpellOasis;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchVictide>());
            recipe.AddIngredient(ModContent.ItemType<EnchMollusk>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.DeepDiver>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Tumbleweed>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ShiftingSands>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.SandSharknadoStaff>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}