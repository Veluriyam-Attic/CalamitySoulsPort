using Terraria.ModLoader;
using Terraria;
using CalamityMod.Items.Placeables.FurnitureSilva;
using CalamityMod.Items.Placeables.FurnitureBotanic;
using CalamityMod.Items.Placeables.FurnitureAncient;
using CalamityMod.Items.Placeables.FurnitureStatigel;
using Terraria.Localization;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using CalamitySouls.Tiles;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Items.Placeables
{
   public class DemonshadeWorkbench:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<ShadowspecBar>());
            Item.createTile = TileType<DemonshadeWorkbenchTile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.Furniture.CraftingStations.DraedonsForge>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurnitureSilva.SilvaBasin>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.Furniture.CraftingStations.VoidCondenser>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurnitureBotanic.BotanicPlanter>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.Furniture.CraftingStations.PlagueInfuser>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurnitureAncient.AncientAltar>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.Furniture.CraftingStations.AshenAltar>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurnitureStatigel.StaticRefiner>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.ShadowspecBar>(), 20);
            recipe.Register();
        }
    }
}
