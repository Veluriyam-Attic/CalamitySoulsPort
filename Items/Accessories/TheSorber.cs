using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Items.Accessories
{
   public class TheSorber:ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.defense = 6;
            Item.width = 20;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            if (!cplayer.absorber)
            {
                player.moveSpeed += 0.12f;
                player.jumpSpeedBoost += 1.2f;
                player.statLifeMax2 += 20;
                player.statManaMax2 += 20;
            }
            if (true) player.endurance += 0.05f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GrandGelatin>());
            recipe.AddIngredient(ModContent.ItemType<CrawCarapace>());
            recipe.AddIngredient(ModContent.ItemType<GiantTortoiseShell>());
            recipe.AddIngredient(ModContent.ItemType<RoverDrive>());
            recipe.AddIngredient(ModContent.ItemType<DepthCells>(), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GrandGelatin>());
            recipe.AddIngredient(ModContent.ItemType<GiantShell>());
            recipe.AddIngredient(ModContent.ItemType<GiantTortoiseShell>());
            recipe.AddIngredient(ModContent.ItemType<RoverDrive>());
            recipe.AddIngredient(ModContent.ItemType<DepthCells>(), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = Recipe.Create(ModContent.ItemType<TheAbsorber>());
            recipe.AddIngredient(this);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
