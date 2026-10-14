using Terraria;
using CalamityMod.Items.Armor.Aerospec;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace CalamitySouls.Ench
{
    public class EnchAerospec : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Aerospec";
            ChineseName = "天蓝";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "99C8C1";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「蓝天之音于你耳畔萦绕。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "让你下落更快，免疫坠落伤害，召唤一个天蓝刃协助你");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "增加25%伤害，但是减少25%暴击率");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchAerospec>(),
                    ModContent.ItemType<EnchHydrothermic>()
                }, "将天蓝刃升级为寰宇之刃，伤害和速度大大增加");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「The voice of the sky lingers.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Allows you to fall more quickly and disables fall damage, summons a aerospec blade to protect you");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Grant 25% more damage but 25% less crit chance");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchAerospec>(),
                ModContent.ItemType<EnchHydrothermic>()
            }, "Upgrade the blade to globe blade, massively increase its damage and speed");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchAero = CSToggle.EnchAero;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Aerospec");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.WindBlade>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.GoldplumeSpear>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.AirSpinner>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Galeforce>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.StormSurge>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.Tradewinds>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Turbulance>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.FeatherKnife>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}