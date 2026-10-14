using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「星流魔石（EnchExo）」的护盾 Buff —— 对应新版 1.3 的 CalamitySouls.Buffs.ExoShieldBuff
	public class ExoShieldBuffV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "力场（新版）");
			CSCompat.SeedTooltip(this, "你受到伤害不会大于50");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.V104().ExoShieldActive = true;
		}
	}
}
