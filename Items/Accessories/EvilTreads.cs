using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace CalamitySouls.Items.Accessories
{
	[AutoloadEquip(EquipType.Shoes)]
	public class EvilTreads : ModItem
	{
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 28;
			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.accRunSpeed = 7.50f;
			player.rocketBoots = 3;
			player.AddBuff(BuffID.Panic, 2);
			player.statManaMax2 += 20;
		}
		public override void AddRecipes()
		{
			Recipe modRecipe = CreateRecipe();
			modRecipe.AddIngredient(ItemID.SpectreBoots);
			modRecipe.AddIngredient(ItemID.PanicNecklace);
			modRecipe.AddIngredient(ItemID.BandofStarpower);
			modRecipe.AddIngredient(ItemID.ShadowScale, 25);
			modRecipe.AddTile(TileID.Anvils);
			modRecipe.Register();
			modRecipe = CreateRecipe();
			modRecipe.AddIngredient(ItemID.SpectreBoots);
			modRecipe.AddIngredient(ItemID.PanicNecklace);
			modRecipe.AddIngredient(ItemID.BandofStarpower);
			modRecipe.AddIngredient(ItemID.TissueSample, 25);
			modRecipe.AddTile(TileID.Anvils);
			modRecipe.Register();
		}
	}
}
	