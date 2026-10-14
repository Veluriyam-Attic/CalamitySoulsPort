using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Accessories;
using CalamityMod.Tiles.Furniture.CraftingStations;

namespace CalamitySouls.Items.OtherItems
{
    public class ManaMedallion : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 46;
            Item.rare = ItemRarityID.Expert;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    line.OverrideColor = Color.SkyBlue;
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.manaFlower = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.NaturesGift);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.EssenceofEleum>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}