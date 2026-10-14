using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Items.Armor
{
	// 1.3 CalamitySouls.Items.Armor.AcidivineBreastplate 逐值照搬（圣蚀胸甲）
	[AutoloadEquip(EquipType.Body)]
	public class AcidivineBreastplate : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 20;
			Item.defense = 10;
			Item.rare = ItemRarityID.Pink;   // 1.3: rare = 5
			Item.value = Item.sellPrice(gold: 5);
		}

		// 1.3：+10%伤害，+5%武器使用速度
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.1f;
			player.CS().WeaponFireRateBoost *= 1.05f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Acidwood>(20)
				.AddIngredient<CorrodedFossil>(20)
				.AddTile(TileID.MythrilAnvil)   // 1.3: AddTile(134)
				.Register();
		}
	}
}
