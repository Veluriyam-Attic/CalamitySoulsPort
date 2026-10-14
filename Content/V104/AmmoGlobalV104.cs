// 皇天层 1-b · 神之风箱的"70% 不消耗凝胶" —— 1.4 的等价实现
// 1.3：ModItem 上有 `ConsumeAmmo(Player)` 钩子；1.4 已移除该 ModItem 钩子 ✗
//      ⇒ 必须改用 GlobalItem.CanConsumeAmmo（语义一致：false = 不消耗 ✓）
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	public class AmmoGlobalV104 : GlobalItem
	{
		// tML 加载期校验：GlobalNPC/GlobalItem/GlobalProjectile 等若含实例字段，必须声明 InstancePerEntity ✓
		// （本类含每实体状态 ⇒ 返回 true；不可改为 static ✗ 那会让所有实体共享状态）
		public override bool InstancePerEntity => true;

		public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
		{
			// 1.3 TheEmpyrean.ConsumeAmmo: if (Main.rand.Next(0, 100) < 70) return false; return true;
			if (weapon.type == ModContent.ItemType<Items.TheEmpyreanV104>())
			{
				if (Main.rand.Next(0, 100) < 70)
				{
					return false;
				}
			}
			return true;   // 其它物品：保持默认（消耗）
		}
	}
}
