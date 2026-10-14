using Terraria;
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

namespace CalamitySouls.Items.OtherItems
{
    public class PrismaticTrailToggler : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 38;
        }
        public override void UpdateInventory(Player player)
        {
            if (Item.favorited) player.CS().NoTrailPleaseEvenIfYouHaventDoneConfigPleaseDoSomethingToCloseTheTrail = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.DarkPlasma>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.ArmoredShell>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.TwistingNether>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.DivineGeode>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}