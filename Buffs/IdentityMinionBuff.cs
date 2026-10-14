using CalamityMod.CalPlayer;
using CalamitySouls.Projectiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamitySouls.Buffs
{
	public class IdentityMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			CSPlayer modPlayer = player.CS();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<IdentityCard>()] > 0) modPlayer.RotI = true;
			if (!modPlayer.RotI)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}