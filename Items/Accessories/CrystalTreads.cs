using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using CalamityMod;

namespace CalamitySouls.Items.Accessories
{
	[AutoloadEquip(EquipType.Shoes)]
	public class CrystalTreads : ModItem
	{
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 38;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(gold: 7);
			Item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.accRunSpeed = 7.75f;
			player.rocketBoots = 3;
			player.jumpSpeedBoost += 1f;
		}
		public override void AddRecipes()
		{
			Recipe modRecipe = CreateRecipe();
			modRecipe.AddIngredient(ModContent.ItemType<EvilTreads>());
			modRecipe.AddIngredient(ItemID.FrostCore);
			modRecipe.AddIngredient(ItemID.FallenStar,5);
			modRecipe.AddIngredient(ItemID.CrystalShard,66);
			modRecipe.AddTile(TileID.MythrilAnvil);
			modRecipe.Register();
		}
	}
}
