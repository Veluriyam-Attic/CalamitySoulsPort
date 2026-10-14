using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 「新版（v1.0.4.0）」专用小工具集。
	// 旧版的对应物是 CalamitySouls.CSUtils，这里只做薄封装，避免复制两份语义。
	public static class V104Utils
	{
		// 新版源码里的 CSUtils.UsingChi
		public static bool UsingChi => CSUtils.GameCultureChinese;

		// 新版源码里的 CSUtils.TooltipHotkeyString
		public static string TooltipHotkeyString(ModKeybind mhk) => CSUtils.TooltipHotkeyString(mhk);

		// 取新版玩家的 ModPlayer
		public static CSPlayerV104 V104(this Player player) => player.GetModPlayer<CSPlayerV104>();

		// 取新版的逐 NPC 全局类（旧版 NPC 走 CSUtils.CS(this NPC npc)）
		public static CSNPCV104 CSV104(this NPC npc) => npc.GetGlobalNPC<CSNPCV104>();
	}
}
