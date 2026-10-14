using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod;

namespace CalamitySouls
{
	// 1.4 兼容层：灾厄 2.x 移除了 Player 上的一批扩展方法/属性，
	// 这里按 1.4 的 DamageClass API 重新实现，保持原有数值语义
	public static class CSCompat
	{
		public static float AverageDamage(this Player player)
		{
			float sum = 0f;
			int n = 0;
			void Acc(DamageClass c) { sum += player.GetDamage(c).ApplyTo(1f); n++; }
			Acc(DamageClass.Melee);
			Acc(DamageClass.Ranged);
			Acc(DamageClass.Magic);
			Acc(DamageClass.Summon);
			Acc(DamageClass.Throwing);
			Acc(ModContent.GetInstance<RogueDamageClass>());
			return n > 0 ? sum / n : 1f;
		}

		public static float MeleeDamage(this Player p) => p.GetDamage(DamageClass.Melee).ApplyTo(1f);
		public static float RangedDamage(this Player p) => p.GetDamage(DamageClass.Ranged).ApplyTo(1f);
		public static float MagicDamage(this Player p) => p.GetDamage(DamageClass.Magic).ApplyTo(1f);
		public static float MinionDamage(this Player p) => p.GetDamage(DamageClass.Summon).ApplyTo(1f);
		public static float RogueDamage(this Player p) => p.GetDamage(ModContent.GetInstance<RogueDamageClass>()).ApplyTo(1f);

		// 灾厄 2.x 移除的弹幕判定辅助
		public static bool IsSummon(this Projectile p) => p.CountsAsClass(DamageClass.Summon);

		// 灾厄 2.x 移除的玩家状态标志（原为 CalamityPlayer 字段），
		// 这里放到模组自己的 CSPlayer 上以免丢功能
		public static ref float GodSlayerDmg(this Player p) => ref p.CS().godSlayerDmg;

		// ===== 1.3 -> 1.4 本地化：DisplayName/Tooltip.SetDefault 已移除 =====
		// 1.4 里 ModItem/ModBuff 等的名字与提示走 "Mods.<模组>.<类别>.<内部名>.<后缀>" 键，
		// 这里在加载期把这套键补进语言缓存，等于 1.3 的 SetDefault。
		public static void SeedDisplayName(ILocalizedModType self, string text) => Seed(self, "DisplayName", text);
		public static void SeedTooltip(ILocalizedModType self, string text) => Seed(self, "Tooltip", text);

		// 1.4 移植：这几个物品的本地化键在加载期就被 PrettyPrintName 的英文默认值占用了，
		// GetOrRegister 不覆盖已有键，hjson 也救不回来。这里改用全新的键名，
		// 通过覆写 ModItem.DisplayName / Tooltip 直接把中文交给游戏渲染。
		public static LocalizedText Zh(string id, string text) =>
			Language.GetOrRegister("Mods.CalamitySouls.CSZh." + id, () => text);

		private static void Seed(ILocalizedModType self, string kind, string text)
		{
			if (!(self is ModType mt)) return;
			// 增益/减益的提示键名是 Description 而不是 Tooltip
			if (kind == "Tooltip" && mt is ModBuff) kind = "Description";
			string key = "Mods." + mt.Mod.Name + "." + self.LocalizationCategory + "." + mt.Name + "." + kind;
			Language.GetOrRegister(key, () => text);
		}

		// 1.3 的 DropHelper.DropItem：在玩家位置掉落随机数量物品
		public static void DropItem(Player player, int itemType, int min, int max)
			=> player.QuickSpawnItem(player.GetSource_FromThis(), itemType, Main.rand.Next(min, max + 1));

		// 1.3 的 StealthUI.DrawPosition：2.x 里潜行条位置改由客户端配置决定
		public static Vector2 StealthMeterPosition
			=> new Vector2(CalamityMod.CalamityClientConfig.Instance.StealthMeterPosX, CalamityMod.CalamityClientConfig.Instance.StealthMeterPosY);
	}
}
