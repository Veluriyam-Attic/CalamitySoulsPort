using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
   public class EnchHydrothermic : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Hydrothermic";
            ChineseName = "渊泉";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "E06C1D";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「让火焰净化一切。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "受伤时造成爆炸，你的攻击有几率吸取微不足道的血量");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "增加25%移速和加速度，受伤时回复100血量，此效果冷却为25秒");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchHydrothermic>(),
                    ModContent.ItemType<EnchAerospec>(),
                }, "将天蓝刃升级为寰宇之刃，伤害和速度大大增加");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Now the fire cleans the place.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "You emit a blazing explosion when you are hit, and your attack may drain a little life");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "+25% movement speed and acceleration, you regen 100 hp when hurt with a 25s cooldown");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchHydrothermic>(),
                ModContent.ItemType<EnchAerospec>(),
            }, "Upgrade the aero blade to globe blade, massively increase its damage and speed");
        }
        public override void SetEnchDefaults()
        {
            Item.defense = 8;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchHydrothermic = CSToggle.EnchHydrothermic;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Hydrothermic");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicSubligar>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.HellfireFlamberge>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Helstorm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ForbiddenSun>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}