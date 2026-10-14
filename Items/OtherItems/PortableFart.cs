using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CalamitySouls.Items.OtherItems
{
    public class PortableFart : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.rare = ItemRarityID.Expert;
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
        public override void UpdateInventory(Player player)
        {
            if (Item.favorited)
            {
                player.GetJumpState(ExtraJump.FartInAJar).Enable();
                player.jumpBoost = true;
                player.noFallDmg = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BalloonHorseshoeFart);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}