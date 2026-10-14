using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 新版「软壳魔石（EnchMollusk）」的浸水判定 —— 对应新版 1.3 CodeEdit/OnEditControl.cs 的
	//   On.Terraria.Collision.WetCollision 钩子（1:1 逻辑；1.4 用 MonoModHooks.Add 代替 1.3 的 On. 语法）
	// 约束：① 用 MonoModHooks.Add ② 挂钩全程 try/catch、失败只记日志（绝不让加载崩）③ 逻辑严格照 1.3
	//       ④ 签名不匹配/挂不上 → 不硬修，记日志 + 差异表 ⑤ 报告见钩子清单
	// ⚠️ 水的【外观覆盖】（1.3 用 WaterEffect 着色器 + WaterOver/WaterUnder 贴图）按裁决省略：
	//    【已订正】资源确实在 1.3 发布包中：Effects/WaterEffect.fx 1132B + .xnb 3948B、Extras/WaterOver.rawimg 1200012B、WaterUnder.rawimg 2280112B ⇒ 原判断（作者未随包发布）有误，已列入补做
	public class CSManualHooksV104 : ModSystem
	{
		private static bool wetCollisionHooked;
		internal static string HookStatus = "未挂钩";

		public override void Load()
		{
			try
			{
				MethodInfo mi = typeof(Collision).GetMethod("WetCollision", BindingFlags.Public | BindingFlags.Static);
				if (mi == null) { HookStatus = "未找到 Collision.WetCollision"; Mod.Logger.Warn("[CS V104] " + HookStatus); return; }
				ParameterInfo[] ps = mi.GetParameters();
				if (ps.Length != 3 || mi.ReturnType != typeof(bool))
				{
					HookStatus = "签名不匹配（参数数=" + ps.Length + "，返回=" + mi.ReturnType.Name + "）→ 按约束④不硬修";
					Mod.Logger.Warn("[CS V104] " + HookStatus);
					return;
				}
				MonoModHooks.Add(mi, (Func<Func<Vector2, int, int, bool>, Vector2, int, int, bool>)((orig, Position, Width, Height) =>
				{
					// 1.3 逻辑逐行：先判 flag，再判距离 < 640f，命中才 return true；否则一律 orig
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						if (Main.LocalPlayer.active && Main.LocalPlayer.V104().MolluskDrown2)
							if (Vector2.Distance(Main.LocalPlayer.Center, new Vector2(Position.X - Width / 2f, Position.Y - Height / 2f)) < 640f)
								return true;
					}
					else
					{
						foreach (Player player in Main.player)
						{
							if (player.active && player.V104().MolluskDrown2)
								if (Vector2.Distance(player.Center, new Vector2(Position.X - Width / 2f, Position.Y - Height / 2f)) < 640f)
									return true;
						}
					}
					return orig(Position, Width, Height);
				}));
				wetCollisionHooked = true;
				HookStatus = "已挂钩（Collision.WetCollision）";
				Mod.Logger.Info("[CS V104] " + HookStatus);
			}
			catch (Exception e)
			{
				HookStatus = "挂钩失败（" + e.GetType().Name + "）→ 按约束④不硬修";
				Mod.Logger.Warn("[CS V104] " + HookStatus + " : " + e.Message);
			}
		}

		public override void Unload()
		{
			// 1.4：MonoModHooks 由 tML 在卸载时统一撤销；本系统只复位自己的状态
			wetCollisionHooked = false;
			HookStatus = "已卸载";
		}

	}
}
