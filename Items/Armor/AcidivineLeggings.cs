using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Items.Armor
{
	// 1.3 CalamitySouls.Items.Armor.AcidivineLeggings 逐值照搬（圣蚀护腿）
	[AutoloadEquip(EquipType.Legs)]
	public class AcidivineLeggings : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.defense = 14;
			Item.rare = ItemRarityID.Pink;   // 1.3: rare = 5
			Item.value = Item.sellPrice(gold: 5);
		}

		// 1.3：+30%移动速度（并把最大跑速下限抬到 4）
		public override void UpdateEquip(Player player)
		{
			if (player.maxRunSpeed < 4f) player.maxRunSpeed = 4f;
			player.CS().MyMoveSpeedMult *= 1.3f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Acidwood>(15)
				.AddIngredient<CorrodedFossil>(15)
				.AddTile(TileID.MythrilAnvil)   // 1.3: AddTile(134)
				.Register();
		}
	}
}
