using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 「新版」专属的 GlobalItem（旧版 CSItem 完全不动）。
	// 只做一件事：实现需求 9 —— 默认不允许「旧版的魂」与「新版的魂」同时装备。
	// 判定范围刻意只覆盖最顶层的魂：魔石 / 之力 / 任何饰品都不受影响。
	public class CSItemV104 : GlobalItem
	{
		// tML 加载期校验：GlobalNPC/GlobalItem/GlobalProjectile 等若含实例字段，必须声明 InstancePerEntity ✓
		// （本类含每实体状态 ⇒ 返回 true；不可改为 static ✗ 那会让所有实体共享状态）
		public override bool InstancePerEntity => true;

		// 掠夺者·探索档：物品拾取距离 +640（1.3 逐值）
		public override void GrabRange(Item item, Player player, ref int grabRange)
		{
			if (player.V104().ReaverExplore) grabRange += 640;
		}

		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if (CSToggleV104.AllowMixedSouls) return true;
			bool equippedOld = SoulExclusion.IsOldSoul(equippedItem);
			bool equippedNew = SoulExclusion.IsNewSoul(equippedItem);
			bool incomingOld = SoulExclusion.IsOldSoul(incomingItem);
			bool incomingNew = SoulExclusion.IsNewSoul(incomingItem);
			if ((equippedOld && incomingNew) || (equippedNew && incomingOld)) return false;
			return true;
		}
	}
}
