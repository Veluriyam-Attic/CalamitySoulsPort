using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.Items.Materials;
using Terraria.Localization;

namespace CalamitySouls.Items.GrabBags
{
	public class EctoFish : ModItem
	{
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 34;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Yellow;
		}
		public override bool CanRightClick() => true;
		public override void RightClick(Player player)
		{
		}
	}
}