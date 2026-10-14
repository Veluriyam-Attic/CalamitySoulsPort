using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「始源林海魔石（EnchSilva）」的无敌 —— 对应新版 1.3 CalamitySouls.Buffs.SilvaInvincibility（1:1）
	public class SilvaInvincibilityV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "无敌");
			CSCompat.SeedTooltip(this, "你不会受到任何伤害");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
			Main.persistentBuff[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.V104().SilvaTrueInvincibility = true;
		}
	}
}
