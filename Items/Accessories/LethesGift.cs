using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items;
using CalamityMod;
using CalamityMod.Tiles.DraedonStructures;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Items.Accessories
{
    public class LethesGift : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 48;
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().lGift = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.LuxorsGift>());
            recipe.AddIngredient(ItemID.MagmaStone);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}