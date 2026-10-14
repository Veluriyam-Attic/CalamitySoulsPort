using Terraria;
using CalamityMod.Items.Armor.Prismatic;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items;
using CalamityMod.Items.Armor;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Items.Accessories
{
   public class PrismaticPrism:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 13);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().pPrism = true;
            if (player.armor[0].type == ItemType<PrismaticHelmet>() 
                && player.armor[1].type == ItemType<PrismaticRegalia>() 
                && player.armor[2].type == ItemType<PrismaticGreaves>())
            {
                player.GetDamage(DamageClass.Generic) += 0.6f;
                player.GetDamage(DamageClass.Magic) -= 0.6f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.DarkPlasma>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.ArmoredShell>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.TwistingNether>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.DivineGeode>(), 10);
            recipe.AddIngredient(ItemID.Nanites, 777);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
