using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 「新旧魂互斥」判定 —— 需求 9：只限制最顶层的「魂」，
	// 魔石 / 之力 / 饰品可以随意混搭。
	public static class SoulExclusion
	{
		// 旧版那一套的魂全部继承 CalamitySouls.Souls.BasicSoul
		public static bool IsOldSoul(Item item)
		{
			return item != null && !item.IsAir && item.ModItem is global::CalamitySouls.Souls.BasicSoul;
		}

		// 新版这一套的魂全部继承 CalamitySouls.Content.V104.Souls.V104SoulBase
		public static bool IsNewSoul(Item item)
		{
			return item != null && !item.IsAir && item.ModItem is Souls.V104SoulBase;
		}

		public static bool HasOldSoulEquipped(Player player)
		{
			if (player == null || player.armor == null) return false;
			for (int i = 0; i < player.armor.Length; i++)
			{
				if (IsOldSoul(player.armor[i])) return true;
			}
			return false;
		}

		// incomingType 仅为可读性保留（当前判定不依赖它）
		public static bool HasOldSoulEquipped(Player player, int incomingType) => HasOldSoulEquipped(player);
	}
}
