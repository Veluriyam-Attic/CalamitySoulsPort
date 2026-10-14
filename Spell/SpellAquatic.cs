using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellAquatic : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Aquatic";
            ChineseName = "渊洋";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "A89254";
            string color2 = "63A0A4";
            string color3 = "86A184";
            int Material1 = ItemType<EnchSulphurous>();
            int Material2 = ItemType<EnchFathom>();
            int Material3 = 0;
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "废土");
                QuickModiLine(tooltips, "无视水阻力，免疫酸海剧毒", Material1, color1);
                QuickModiLine(tooltips, "+10%伤害，+2召唤栏，在深渊提供大量呼吸", Material2, color2);
                QuickModiLine(tooltips, "召唤一个围着你转圈，摧毁敌对弹幕的幻渊小鱼", Material2, color2);
                QuickModiLine(tooltips, "你的攻击造成多种debuff", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Wasteland");
            QuickModiLine(tooltips, "Ignore water and grant immunity to sulphurous poisoning", Material1, color1);
            QuickModiLine(tooltips, "+10% damage, +2 max minion and greatly reduces breath loss in the abyss", Material2, color2);
            QuickModiLine(tooltips, "Spawn a fathom fish that circle around you destroying hostile projectiles", Material2, color2);
            QuickModiLine(tooltips, "Your attacks inflicts tons of debuffs", Item.type, color3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellAquatic = CSToggle.SpellAquatic;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchSulphurous>());
            recipe.AddIngredient(ModContent.ItemType<EnchFathom>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Greentide>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Leviatitan>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.GastricBelcherStaff>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
