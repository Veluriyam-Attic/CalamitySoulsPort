using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「代达罗斯魔石（EnchDaedalus）」的闪避冷却 —— 对应新版 1.3 CalamitySouls.Buffs.DaedalusEvadeCooldown
	public class DaedalusEvadeCooldownV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "代达罗斯闪避冷却");
			CSCompat.SeedTooltip(this, "你的代达罗斯闪避正在充能");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.V104().DaedalusEvadeCooldown = true;
		}
	}
}
