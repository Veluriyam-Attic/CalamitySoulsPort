using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「硫火魔石（EnchBrimflame）」的暴乱/冷却 —— 对应新版 1.3 CalamitySouls.Buffs.BrimFrenzy / BrimFrenzyCooldown（1:1）
	// 备注：1.3 用 Cal.GetLegacySoundSlot(...BrimflameRecharge)；音效资源已补入本模组 Sounds/Custom/，用自有路径播放（1:1）
	public class BrimFrenzyV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "硫火暴乱");
			CSCompat.SeedTooltip(this, "你使用了硫火的力量。\n你的攻击多造成35%伤害");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.V104().AllDamageByMult *= 1.35f;
			player.V104().BrimFrenzy = true;
			if (player.buffTime[buffIndex] == 1)
			{
				player.AddBuff(ModContent.BuffType<BrimFrenzyCooldownV104>(), 25 * 60);
				// 1.3: Cal ...BrimflameRecharge（已核实 2.x 无该资源）→ 退回默认音，差异表已记录
				SoundEngine.PlaySound(new SoundStyle("CalamitySouls/Sounds/Custom/BrimflameRecharge"), player.Center);
			}
		}
	}
	public class BrimFrenzyCooldownV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "硫火冷却");
			CSCompat.SeedTooltip(this, "你不能使用硫火的力量。");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.V104().BrimFrenzyCooldown = true;
		}
	}
}
