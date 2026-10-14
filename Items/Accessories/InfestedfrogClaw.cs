using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CalamitySouls.Items.Accessories
{
    public class InfestedfrogClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 44;
            Item.defense = 5;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 7);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetJumpState(ExtraJump.TsunamiInABottle).Enable();
            player.jumpBoost = true;
            player.jumpSpeedBoost += 2.4f;
            player.autoJump = true;
            player.spikedBoots = 2;
            player.endurance += 0.05f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SeafrogLeg>());
            recipe.AddIngredient(ItemID.TigerClimbingGear);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}