using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 「新版」专属的 NPC 全局类（旧版 CSNPC 完全不动）。
	// GlobalNPC 在 1.4 里默认是"每个 NPC 一份实例"，所以这里的字段天然是逐 NPC 的。
	public class CSNPCV104 : GlobalNPC
	{
		// tML 加载期校验：GlobalNPC/GlobalItem/GlobalProjectile 等若含实例字段，必须声明 InstancePerEntity ✓
		// （本类含每实体状态 ⇒ 返回 true；不可改为 static ✗ 那会让所有实体共享状态）
		public override bool InstancePerEntity => true;

		// 1.3 CSNPC.UpdateExtraPosition 用的上一帧位置（冻结时把位置拉回一半 = 软冻结）
		private readonly System.Collections.Generic.Dictionary<int, Vector2> fearmongerOldPosition = new();
		// 新版渊泉魔石"日炎环"的逐 NPC 引爆冷却（对应新版 CSNPC.HydrothermicBurstCooldown）
		public int HydrothermicBurstCooldown;

		public override void ResetEffects(NPC npc)
		{
			if (HydrothermicBurstCooldown > 0) HydrothermicBurstCooldown--;
		}
		// 1.3 CSNPC.UpdateExtraPosition:27-42 —— 时间冻结时把 NPC 位置往上一帧拉（软冻结）
		public override void PostAI(NPC npc)
		{
			if (CSWorldV104.FearmongerTimeStop)
			{
				Vector2 oldPosition;
				if (fearmongerOldPosition.TryGetValue(npc.whoAmI, out oldPosition) && oldPosition.Length() != 0f)
					npc.position = (npc.position + oldPosition) / 2f;
			}
			fearmongerOldPosition[npc.whoAmI] = npc.position;
		}
	}
}
