using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「金源魔石」的金源冲击冷却 —— 对应新版 1.3 CalamitySouls.Buffs.AuricFlashCooldownBuff（1:1）
	public class AuricFlashCooldownBuffV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "金源冲击冷却");
			CSCompat.SeedTooltip(this, "你的金源冲击正在充能");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.V104().AuricFlashCooldown = true;
		}
	}
}
