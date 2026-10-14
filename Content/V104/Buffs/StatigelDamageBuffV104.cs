using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「斯塔提斯魔石（EnchStatigel）」的擦弹增伤 —— 对应新版 1.3 CalamitySouls.Buffs.StatigelDamageBuff
	// ⚠️ 1.3 的 canBeCleared = false 在 tML 1.4 的 ModBuff 里没有对应成员（我方工程与 2.x API 都查不到），
	//    故未设置 → 玩家可右键清掉该 Buff（差异表已记录），其余逐值 1:1。
	public class StatigelDamageBuffV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "斯塔提斯伤害提升");
			CSCompat.SeedTooltip(this, "增加12%伤害");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = false;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.V104().AllDamageByMult *= 1.12f;
		}
	}
}
