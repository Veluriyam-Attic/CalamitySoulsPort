using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace CalamitySouls.Items.Accessories
{
    public class SandsharkNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 54;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 8);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 50;
            player.GetDamage(DamageClass.Generic) += 0.08f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MoltenNecklace>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.GrandScale>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}