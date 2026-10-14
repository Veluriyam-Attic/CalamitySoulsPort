using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls
{
	/// <summary>
	/// 重排【合成表里我们 mod 自己的配方】的显示顺序，目标：
	///     ① 新版（V104）配方            —— 保持原顺序，排最前
	///     ② 旧版 魔石(Ench)             —— 30 条
	///     ③ 旧版 咒(Spell)              —— 10 条
	///     ④ 旧版 力(Force)              —— 6 条
	///     ⑤ 旧版 灾厄之魂(SoulCalamity) —— 1 条
	///     ⑥ 其余旧版配方                —— 保持原顺序，排在后面
	/// 另外：圣蚀套内部排成 头盔 → 胸甲 → 护腿。
	///
	/// 实现要点：只处理【属于 CalamitySouls 的配方】，并且只写回它们原先占据的那些位置
	///          ⇒ 其它 mod 的配方一格都不动；配方内容（材料/数量/制作站/产物）一律不改。
	///          OrderBy 是稳定排序 ⇒ 同一组内部维持原有先后。
	/// </summary>
	public class CSRecipeOrderV104 : ModSystem
	{
		private const string Root = "CalamitySouls";

		private static int GroupKey(Recipe r)
		{
			var t = r?.createItem?.ModItem?.GetType();
			if (t == null) return -1;
			string ns = t.Namespace ?? string.Empty;
			if (!ns.StartsWith(Root)) return -1;                  // 不是我们 mod 的 ⇒ 不参与

			if (ns.Contains("V104")) return 0;                    // 新版
			if (ns == Root + ".Ench") return 1;                   // 魔石
			if (ns == Root + ".Spell") return 2;                  // 咒
			if (ns == Root + ".Force") return 3;                  // 力
			if (t.Name == "SoulCalamity") return 4;               // 灾厄之魂
			return 5;                                             // 其余旧版
		}

		public override void PostAddRecipes()
		{
			var slots = new List<int>();
			for (int i = 0; i < Main.recipe.Length; i++)
			{
				if (Main.recipe[i] != null && GroupKey(Main.recipe[i]) >= 0) slots.Add(i);
			}
			if (slots.Count < 2) return;

			Recipe[] ordered = slots.Select(i => Main.recipe[i])
				.OrderBy(GroupKey)
				.ToArray();
			for (int k = 0; k < slots.Count; k++) Main.recipe[slots[k]] = ordered[k];

			Mod.Logger.Info("[CS V104] 本 mod 配方显示顺序已重排：新版 → 魔石 → 咒 → 力 → 灾厄之魂 → 其余旧版（共 " + slots.Count + " 条）");

			// 圣蚀套：头盔 → 胸甲 → 护腿
			ReorderByName(new[] { "AcidivineHelmet", "AcidivineBreastplate", "AcidivineLeggings" }, "圣蚀套：头盔 → 胸甲 → 护腿");
		}

		private void ReorderByName(string[] order, string label)
		{
			var slots = new List<int>();
			for (int i = 0; i < Main.recipe.Length; i++)
			{
				var t = Main.recipe[i]?.createItem?.ModItem?.GetType();
				if (t != null && System.Array.IndexOf(order, t.Name) >= 0) slots.Add(i);
			}
			if (slots.Count < 2) return;

			Recipe[] ordered = slots.Select(i => Main.recipe[i])
				.OrderBy(r => System.Array.IndexOf(order, r.createItem.ModItem.GetType().Name))
				.ToArray();
			for (int k = 0; k < slots.Count; k++) Main.recipe[slots[k]] = ordered[k];

			Mod.Logger.Info("[CS V104] " + label + " 显示顺序已重排（共 " + slots.Count + " 条）");
		}
	}
}