using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CalamitySouls.Items.Accessories
{
    public class SeafrogLeg : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 38;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 2);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noFallDmg = true;
            player.GetJumpState(ExtraJump.TsunamiInABottle).Enable();
            player.jumpBoost = true;
            player.jumpSpeedBoost += 2.4f;
            player.autoJump = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrogLeg);
            recipe.AddIngredient(ItemID.BalloonHorseshoeSharkron);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}