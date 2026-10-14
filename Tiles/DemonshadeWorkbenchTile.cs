using System;
using CalamityMod.Items.Placeables.FurnitureSilva;
using CalamityMod.Items.Placeables.FurnitureBotanic;
using CalamityMod.Items.Placeables.FurnitureAncient;
using CalamityMod.Items.Placeables.FurnitureStatigel;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ObjectData;
using CalamitySouls.Items.Placeables;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Tiles
{
	public class DemonshadeWorkbenchTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.DrawFlipHorizontal = false;
			TileObjectData.newTile.DrawFlipVertical = false;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
			};
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(255, 0, 0), name);
			// 1.4：ModTile.disableSmartCursor 已移除
			AdjTiles = new int[]
			{
				TileID.WorkBenches,
				TileID.Furnaces,
				TileID.Hellforge,
				TileID.AdamantiteForge,
				TileID.Anvils,
				TileID.MythrilAnvil,
				TileID.DemonAltar,
				TileID.LunarCraftingStation,
				TileType<DraedonsForge>(),
				TileType<CalamityMod.Tiles.FurnitureSilva.SilvaBasin>(),
				TileType<VoidCondenser>(),
				TileType<CalamityMod.Tiles.FurnitureBotanic.BotanicPlanter>(),
				0,
				TileType<PlagueInfuser>(),
				TileType<AshenAltar>(),
				TileType<CalamityMod.Tiles.FurnitureAncient.AncientAltar>(),
				0,
				TileType<CalamityMod.Tiles.FurnitureStatigel.StaticRefiner>(),
				0,
			};
			DustType = 183;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(null, i * 16, j * 16, 32, 16, ItemType<DemonshadeWorkbench>());
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = Main.DiscoR / 255f;
			g = Main.DiscoG / 255f;
			b = Main.DiscoB / 255f;
		}
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
			Tile tile = Main.tile[i, j];
			if ((tile.TileFrameX == 0 || tile.TileFrameX == 72) && tile.TileFrameY == 0) 
            {
                Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Tiles/DemonshadeWorkbenchTileFlame").Value;
                Rectangle dRect2 = new Rectangle((int)(i * 16 + 192 - Main.screenPosition.X), (int)(j * 16 + 176 - Main.screenPosition.Y), texture.Width, texture.Height);
                spriteBatch.Draw(texture, dRect2, Color.White);
            }
            return true;
        }
    }
}