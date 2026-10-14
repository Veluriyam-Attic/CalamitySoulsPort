using System.Collections.Generic;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamitySouls.NPCs;

namespace CalamitySouls
{
	// 1.4：ModifyInterfaceLayers 与 NPC 更新钩子已从 Mod 迁移到 ModSystem
	public class CalamitySoulsSystem : ModSystem
	{
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			// ⭐ [2026-10-11] 把「硫海剧毒区域的外部显示」接回 1.4。
			//    1.3 走的是挂钩 FilterManager.EndCapture（其委托签名在 1.4 已变，无法照搬 —— 见
			//    Effects/SulphurousWaterRender.cs 顶部注释），移植时因此整条被注释掉 ⇒ 玩家实测
			//    「剧毒区域的外部显示没有 1.3 是有的」✓
			//    这里改用界面层：InterfaceScaleType.None 插在【最前】（世界画完之后、其它 UI 之前），
			//    与 1.3 EndCapture 的时机等价；坐标系恒等 ⇒ 与屏幕 1:1，RenderTarget 对得上 ✓
			//    批次契约（Terraria.UI.GameInterfaceLayer.Draw:53-62）：本委托被调用时框架已用
			//    (SpriteSortMode)0, null…, Matrix.Identity 开了批次，并在委托返回后 End() ⇒
			//    我们必须先 End、渲染完再按【同一状态】Begin 回来，否则框架收尾时会抛绘制异常 ✗
			layers.Insert(0, new LegacyGameInterfaceLayer("SulphurousWater", delegate ()
			{
				if (SulphurousWaterRender.CanUseRender)
				{
					SpriteBatch sb = Main.spriteBatch;
					sb.End();
					// ⭐ 双保险：DoRender 内部已用 try/finally 保证"它开的批次一定被关掉"，
					//    这里再兜一层 try/catch —— 万一将来又有人在里面抛异常，也绝不能把
					//    "批次仍开着"的状态留给框架（那会导致后续全部 UI 画不出来，实测过一次 ✗）
					try { SulphurousWaterRender.DoRender(() => { }); }
					finally { try { sb.Begin((SpriteSortMode)0, null, null, null, null, null, Matrix.Identity); } catch { } }
				}
				return true;
			}, InterfaceScaleType.None));
			// [已按玩家要求移除] 生命回复数值显示（"生命回复 x/秒 (神惧者 关 lifeRegen=…)"）——
			// 它已完成使命：证实回血一直在按 lifeRegen÷2 生效（16.0/秒 基础 vs 23.0/秒 装神惧者 ✓）
			Player player = Main.LocalPlayer;
			CSPlayer mplayer = player.CS();
			int projIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Smart Cursor Targets");
			if (projIndex != -1)
			{
				layers.Insert(projIndex, new LegacyGameInterfaceLayer("BrimflameRay", delegate ()
				{
					if (mplayer.BrimFrenzyVisualEffect > 0)
					{
						Texture2D laserTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/BrimflameLasers").Value;
						float rot = MathHelper.ToRadians(3 * mplayer.BrimFrenzyVisualEffect);
						float colorLerpRaius = 1f;
						if (mplayer.BrimFrenzyVisualEffect <= 10) colorLerpRaius = MathHelper.Lerp(0.5f, 0f, mplayer.BrimFrenzyVisualEffect / 10f);
						else if (mplayer.BrimFrenzyVisualEffect >= 20) colorLerpRaius = MathHelper.Lerp(0.5f, 0f, (mplayer.BrimFrenzyVisualEffect - 20f) / 10f);
						Color color = Color.White * colorLerpRaius * 0.5f;
						Main.spriteBatch.End();
						Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
						Main.spriteBatch.Draw(laserTexture, player.Center - Main.screenPosition, null, color, -rot, laserTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
						Main.spriteBatch.End();
						Main.spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
					}
					return true;
				}, InterfaceScaleType.Game));
			}
			int mouseIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Mouse Text");
			if (mouseIndex != -1)
			{
				layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Manic Mode UI", delegate ()
				{
					if (Main.playerInventory && CSWorld.HyperMode)
					{
						Texture2D outerAreaTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/ManicModeUI").Value;
						Vector2 drawCenter = new Vector2(Main.screenWidth - 400f, 72f) + Utils.Size(outerAreaTexture) * 0.5f;
						Main.spriteBatch.Draw(outerAreaTexture, drawCenter, null, Color.White, 0f, Utils.Size(outerAreaTexture) * 0.5f, 1f, SpriteEffects.None, 0f);
					}
					return true;
				}, InterfaceScaleType.UI));

				layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Stealth Essense UI", delegate ()
				{
					CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
					if (!CalamityMod.CalamityClientConfig.Instance.StealthMeter || cplayer.rogueStealthMax <= 0f || !cplayer.wearingRogueArmor) return true;
					cplayer.stealthUIAlpha = 1f;
					Texture2D edgeTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/StealthEssenseMeter").Value;
					Texture2D barTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/StealthEssenseMeterBar").Value;
					Texture2D essenseTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/StealthEssenseBar").Value;
					Main.spriteBatch.Draw(edgeTexture, CSCompat.StealthMeterPosition, null, Color.White * cplayer.stealthUIAlpha, 0f,
						Utils.Size(edgeTexture) * 0.5f, 1f, SpriteEffects.None, 0f);
					float completionRatio = cplayer.rogueStealth / cplayer.rogueStealthMax;
					Rectangle barRectangle = new Rectangle((int)(barTexture.Width * completionRatio), 0, (int)(barTexture.Width * (1f - completionRatio)), barTexture.Height);
					//Main.spriteBatch.Draw(barTexture, CSCompat.StealthMeterPosition, barRectangle, Color.White * cplayer.stealthUIAlpha, 0f, 
					//	Utils.Size(barTexture) * 0.5f, 1f, SpriteEffects.None, 0f);
					completionRatio = mplayer.StealthEssense / mplayer.StealthEssenseMax2;
					barRectangle = new Rectangle(0, 0, (int)(essenseTexture.Width * completionRatio), essenseTexture.Height);
					Main.spriteBatch.Draw(essenseTexture, CSCompat.StealthMeterPosition, barRectangle, Color.White * cplayer.stealthUIAlpha, 0f,
						Utils.Size(essenseTexture) * 0.5f, 1f, SpriteEffects.None, 0f);
					return true;
				}, InterfaceScaleType.UI));
			}
		}

		// ⭐ [2026-10-11] 幻渊鱼群·深渊呼吸恢复的"最后一道保险"：放在 PostUpdateEverything
		//    （所有玩家更新结束之后、渲染开始之前）⇒ 保证渲染阶段读到的呼吸值稳定在上限。
		//    灾厄的扣呼吸在 PostUpdateMiscEffects（CalamityPlayer.cs:15030 AbyssEffects()），
		//    我们在 PostUpdate 已顶过一次；这里再兜一次，避免 ModPlayer 注册顺序变化带来的抖动。
		// 🔍🔍 同时是【临时诊断】：记录 buff 列表的每一次变动，用于定位
		//    「最深层的深渊里 buff 栏全部在闪烁」（原假设"呼吸抖动"已被证伪：时序本来就是对的）。
		//    若整条栏都闪，通常是某个 debuff 被反复清除+重加 ⇒ 槽位重排 ⇒ 每个图标都重建。
		private static readonly int[] _lastBuffs = new int[Player.MaxBuffs];
		private static int _buffLogBudget = 400;
		private static int _lavaGuardLogged;
		private static int _lavaTimeFixLogged;
		public override void PostUpdateEverything()
		{
			Player p = Main.LocalPlayer;
			if (p == null || !p.active) return;
			bool inAbyss = p.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().ZoneAbyss;
			if (p.GetModPlayer<global::CalamitySouls.Content.V104.CSPlayerV104>().EnchFathom && inAbyss)
				p.breath = System.Math.Max(p.breath, p.breathMax);

			// ⭐⭐ [2026-10-11 修「深渊里整条 buff 栏一起闪」] 原版绘制 DrawInterface_Resources_Breath()
			//    （反编译 Main.cs:56610-56616）里有一个整数除法隐患：
			//        if (!(player.lavaTime < player.lavaMax && !ghost && flag)) return;
			//        int num7 = player.lavaMax / 10;
			//        _ = player.breathMax / num7;        // ← num7 == 0 ⇒ DivideByZeroException ✗
			//    该异常从 GUIBarsDraw 抛出 ⇒ 同一层里的【buff 栏也一起不画】⇒ 玩家实测「整条栏一起闪」✓
			//    本模组与灾厄都不写这些字段（grep 确认）⇒ 那个 0 值来自别处。
			//    两道防御（都在深渊里生效）：
			//      ① lavaMax ∈ 1..9 ⇒ 抬到 10（10/10=1 ✓）；日志实证 0 次触发 ⇒ 说明真正的 0 不是它 ✗
			//      ② 【本轮新增】直接让那个分支不进入：保持 lavaTime ≥ lavaMax。
			//         深渊没有岩浆 ⇒ 对玩法没有实际影响 ✓，但能让整条 UI 不再每帧崩 ✓
			//         若还闪 ⇒ 说明入口条件也与我反编译的那份不同，下一轮靠 10/秒的 [深渊状态] 日志定位
			if (p.lavaMax > 0 && p.lavaMax < 10)
			{
				if (_lavaGuardLogged < 5)
				{
					_lavaGuardLogged++;
					ModLoader.GetMod("CalamitySouls").Logger.Info(
						$"[呼吸槽防崩] 检测到 lavaMax={p.lavaMax}（会触发原版除零）⇒ 已抬到 10；" +
						$"lavaTime={p.lavaTime} breath={p.breath}/{p.breathMax} lavaImmune={p.lavaImmune} t={Main.GameUpdateCount}");
				}
				p.lavaMax = 10;
			}
			if (inAbyss && p.lavaTime < p.lavaMax)
			{
				if (_lavaTimeFixLogged < 5)
				{
					_lavaTimeFixLogged++;
					ModLoader.GetMod("CalamitySouls").Logger.Info(
						$"[呼吸槽防崩] 深渊中 lavaTime({p.lavaTime}) < lavaMax({p.lavaMax}) ⇒ 已拉平" +
						$"（避开原版除零分支）；breath={p.breath}/{p.breathMax} t={Main.GameUpdateCount}");
				}
				p.lavaTime = p.lavaMax;
			}
			// [已移除临时诊断 2026-10-11] 原「[buff变动] 记录 + [深渊状态] 每秒/每 10 秒快照」——
			// 它们已完成使命：buff 数据稳定（23 条变动全在进世界瞬间）⇒ 闪烁不是数据问题，
			// 而是原版呼吸槽绘制的除零异常；用下面的 lavaTime 拉平后玩家确认「buff 栏不闪了 修好了」✓
		}

		public override void PostUpdateNPCs()
		{
			CSItem.SwitchTexture();
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active)
					if (CSNPCStatic.bIndexDict.ContainsKey(npc.type))
						CSNPCStatic.bIndexDict[npc.type] = i;
			}
			CSNPCStatic.ResetIndex();
		}
	}
}
