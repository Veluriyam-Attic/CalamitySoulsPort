using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CalamitySouls.Items.Accessories
{
    public class StinkCushion : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 24;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 3);
            Item.useAnimation = Item.useTime = 5;
            Item.holdStyle = ItemHoldStyleID.HoldFront;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item16;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    line.OverrideColor = Color.LimeGreen;
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetJumpState(ExtraJump.FartInAJar).Enable();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Stinkfish);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
            recipe = Recipe.Create(ItemID.FartInABalloon);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ItemID.ShinyRedBalloon);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}