using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using CalamitySouls.Content.V104;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Items.Armor
{
	// 1.3 CalamitySouls.Items.Armor.AcidivineHelmet 逐值照搬（圣蚀头盔）
	[AutoloadEquip(EquipType.Head)]
	public class AcidivineHelmet : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
			Item.defense = 16;
			Item.rare = ItemRarityID.Pink;   // 1.3: rare = 5
			Item.value = Item.sellPrice(gold: 5);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<AcidivineBreastplate>()
				&& legs.type == ModContent.ItemType<AcidivineLeggings>();
		}

		// 1.3：击中敌人时生成大量硫磺泡泡 / +10%暴击 / -20%蓝耗 / +2召唤栏
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "击中敌人时生成大量硫磺泡泡\n+10%暴击率，-20%蓝耗，+2召唤栏";
			player.V104().AcidivineSet = true;
			player.ModifyAllCrit(10);
			player.manaCost *= 0.8f;
			player.maxMinions += 2;
		}

		// 1.3：+8%伤害，+2%暴击
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.08f;
			player.ModifyAllCrit(2);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Acidwood>(10)
				.AddIngredient<CorrodedFossil>(10)
				.AddTile(TileID.Anvils)      // 1.3: AddTile(16)
				.Register();
		}
	}
}
