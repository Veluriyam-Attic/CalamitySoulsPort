using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Buffs
{
	// 1.3 CalamitySouls.Buffs.nCoreCooldown 逐值照搬（星云核复活冷却）
	// 1.3: buffNoSave = true / debuff = true / canBeCleared = false / longerExpertDebuff = false
	// 1.4 适配：canBeCleared ⇒ ModBuff.RightClick 返回 false（等价）；longerExpertDebuff 默认即 false
	public class nCoreCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
		}

		public override bool RightClick(int buffIndex) => false;
	}
}
