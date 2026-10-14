using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「日影魔石（EnchUmbraphile）」的「遁于暗影」标记 Buff。
	// 对应新版 1.3 源码 CalamitySouls.Buffs.UmbraphileUndetected（1:1，含 1.3 把它标成 debuff 的写法）。
	public class UmbraphileUndetectedV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "遁于暗影");
			CSCompat.SeedTooltip(this, "+10%武器使用速度");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			// ⭐ [2026-10-11] 修「buff 栏一直闪烁」：
			//    本 buff 是"只要遁于暗影成立就每帧续期"的标记 buff，1.3 原文给的是 2 帧时长
			//    （_13check\CSPlayerEffects.cs:1136 `AddBuff(BuffType<UmbraphileUndetected>(), 2)`），
			//    而 tML 1.4 画图标时只要剩余时间不足就会画成"即将到期"的闪烁态
			//    （Main.cs:56346 的 `!buffNoTimeDisplay[num]` 判断）⇒ 2 帧时长 = 永久闪烁 ✗
			//    ⇒ 照原版宠物/召唤 buff 的做法标记为"不显示计时"，闪烁随之消失；
			//      【不改时长、不改效果】（遁于暗影的判定与持续时间完全照旧 ✓）
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}
