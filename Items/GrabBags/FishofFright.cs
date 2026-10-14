using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using CalamityMod;
using Terraria.Localization;

namespace CalamitySouls.Items.GrabBags
{
	public class FishofFright : ModItem
	{
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 34;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Pink;
		}
		public override bool CanRightClick() => true;
		public override void RightClick(Player player)
		{
			CSCompat.DropItem(player, ItemID.SoulofFright, 1, 2);
		}
	}
}