using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellGlobe : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Globe";
            ChineseName = "寰球";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "99C8C1";
            string color2 = "E06C1D";
            string color3 = "C59A77";
            int Material1 = ItemType<EnchAerospec>();
            int Material2 = ItemType<EnchHydrothermic>();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "天上天下");
                QuickModiLine(tooltips, "让你下落更快，免疫坠落伤害", Material1, color1);
                QuickModiLine(tooltips, "增加25%伤害，但是减少25%暴击率", Material1, color1);
                QuickModiLine(tooltips, "受伤时造成爆炸，你的攻击有几率吸取微不足道的血量", Material2, color2);
                QuickModiLine(tooltips, "增加25%移速和加速度，受伤时回复100血量，此效果冷却为25秒", Material2, color2);
                QuickModiLine(tooltips, "召唤一个寰宇之刃协助你", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Asgard and the world");
            QuickModiLine(tooltips, "Allows you to fall more quickly and disables fall damage", Material1, color1);
            QuickModiLine(tooltips, "Grant 25% more damage but 25% less crit chance", Material1, color1);
            QuickModiLine(tooltips, "You emit a blazing explosion when you are hit, and your attack may drain a little life", Material2, color2);
            QuickModiLine(tooltips, "+25% movement speed and acceleration, you regen 100 hp when hurt with a 25s cooldown", Material2, color2);
            QuickModiLine(tooltips, "Summon a globe blade to protect you", Item.type, color3);
        }
        public override void SetSpellDefaults()
        {
            Item.defense = 8;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellGlobe = CSToggle.SpellGlobe;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchAerospec>());
            recipe.AddIngredient(ModContent.ItemType<EnchHydrothermic>());
            recipe.AddIngredient(ItemID.GravityGlobe);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.TheMicrowave>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.StarSputter>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.RegulusRiot>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}