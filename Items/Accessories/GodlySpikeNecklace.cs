using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using CalamityMod;

namespace CalamitySouls.Items.Accessories
{
    public class GodlySpikeNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 50;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 15);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 150;
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.longInvince = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SandsharkNecklace>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.ReaperToothNecklace>());
            recipe.AddIngredient(ItemID.CrossNecklace);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.AuricBar>(), 4);
            recipe.AddTile(ModContent.TileType<CalamityMod.Tiles.Furniture.CraftingStations.DraedonsForge>());
            recipe.Register();
        }
    }
}