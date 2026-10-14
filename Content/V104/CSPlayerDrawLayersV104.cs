using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;
using ReLogic.Content;

namespace CalamitySouls.Content.V104
{
	// 批次 8 —— 绘制层（1.4 用 PlayerDrawLayer 子类，自动加载；旧版那套的 CSPlayerDrawEffect.cs 已是同款写法）

	// 弑神者/弑神者·古 的暗影残影（对应 1.3 CSPlayerDrawEffect.cs:22-51；⚠️ 旧注释写的 "199-233" 是错的，
	//   该文件总共只有 197 行 —— 已订正，避免以后照错位置比对）
	// ✅ [2026-10-11 重做] 1.3 的「End 批次 ⇒ Begin(Additive, …, CosmicEffect) ⇒ Main.instance.DrawPlayer ×7」
	//    在 1.4 不成立（1.4 的 PlayerRenderer 自己管理批次 ⇒ 着色器泄漏 + 递归栈溢出），已改为
	//    「复制 drawInfo.DrawDataCache + 位移 + 染紫 + 插到最前」，完全不碰 spriteBatch。详见类内注释。
	public class GsaShadowDrawLayerV104 : PlayerDrawLayer
	{
		// ⭐ [2026-10-11 重做] 「紫色残影」改用 1.4 的正确做法：复制 drawInfo.DrawDataCache。
		//    历史：原先照 1.3 在【批次里】重画玩家（Main.PlayerRenderer.DrawPlayer ×6 + Additive + CosmicEffect），
		//    这套在 1.4 不成立 —— 玩家实测「装备远古弑神者 / 弑神者冲刺时全屏变紫」：
		//      · 1.3（CSPlayerDrawEffect.cs:37-50）在 ModPlayer.ModifyDrawInfo 里调 Main.instance.DrawPlayer，
		//        那是画进【调用者当前批次】的 ⇒ 外层 Begin(Additive, …, CosmicEffect) 能生效 ✓
		//      · 1.4 的 Main.PlayerRenderer.DrawPlayer → LegacyPlayerRenderer.DrawPlayerInternal
		//        → PlayerDrawLayers.DrawPlayer_RenderAllLayers 会【自己成对地 End/Begin 若干次】
		//        （tModLoader\Terraria\Graphics\Renderers\LegacyPlayerRenderer.cs:151-201）⇒ 作废外层批次
		//      · 而 CosmicEffect.fx 会把批次内所有非黑像素刷成同一种紫：
		//        if (any(color)) return float4(0.37, 0.12, 0.63, 1.0);  ⇒ 整个世界变紫且保留轮廓 ✗
		//      · 另外那套写法还会【无限递归】（LegacyPlayerRenderer.cs:174 会重跑全部图层含本层）
		//        ⇒ StackOverflowException ⇒ 进程立即终止 ⇒ 日志零异常（玩家实测的「直接闪退」）
		//    现在：完全不碰 spriteBatch，改为复制本帧玩家已生成的 DrawData，加偏移与紫色后插到列表最前 ⇒
		//    残影以普通 AlphaBlend 画在本体【之下】，物理上不可能再泄漏任何批次/着色器状态 ✓
		//    因此本层必须排在【最后一个本体图层之后】才能拿到完整 DrawData ⇒ 用 EyebrellaCloud 作锚点。
		//    （7 个描边/阴影标志已移到 CSPlayerV104.ModifyDrawInfo —— 它们必须在绘制【之前】生效，1.3 亦然。）
		//    ⚠️ 与 1.3 唯一无法在不碰批次前提下复现的差异：1.3 是「Additive + 着色器刷成纯不透明紫」，
		//       本实现是「DrawData 颜色乘紫、保留原 alpha」⇒ 观感为紫色残影光晕而非纯紫剪影。
		//       若要 1.3 的精确观感，唯一忠实的办法是把残影画进 RenderTarget 再用着色器合成（另立任务）。
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.EyebrellaCloud);

		// 复用同一个 List，避免每帧分配（量级 ≈ 6 × 本体 DrawData 条数）
		private static readonly System.Collections.Generic.List<DrawData> _ghosts = new System.Collections.Generic.List<DrawData>(512);
		// 1.3 CosmicEffect.fx 里那个紫：float4(0.37, 0.12, 0.63) ⇒ (94, 31, 161)
		private static readonly Color _cosmicPurple = new Color(94, 31, 161);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (!drawPlayer.active || drawPlayer.dead || drawPlayer.outOfRange) return false;
			// 1.3 CSPlayerDrawEffect.cs:39 —— chaosState（弑神者冲刺进行中）时不画残影
			if (drawPlayer.chaosState) return false;
			return drawPlayer.V104().gsaShadow;
		}

		// 1.3 CSPlayerDrawEffect.cs:39-41 的残影：6 份，偏移 = new Vector2(2f).RotatedBy(i * 60°)
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			System.Collections.Generic.List<DrawData> cache = drawInfo.DrawDataCache;
			int count = cache.Count;
			if (count == 0) return;

			_ghosts.Clear();
			for (int i = 0; i < 6; i++)
			{
				Vector2 offset = new Vector2(2f).RotatedBy(MathHelper.ToRadians(i * 60f));
				for (int k = 0; k < count; k++)
				{
					DrawData d = cache[k];
					d.position += offset;
					// 保留原始 alpha（半透明部件仍半透明），只把颜色换成宇宙紫 ⇒ 等价于 1.3 着色器的着色
					d.color = _cosmicPurple * (d.color.A / 255f);
					_ghosts.Add(d);
				}
			}
			// 插到最前 ⇒ 最先画 ⇒ 位于本体之下（等价于 1.3「残影先画、本体后覆盖」的层次）
			cache.InsertRange(0, _ghosts);
		}
	}

	// 天蓝冲刺助推器（1.3 CSPlayerDrawEffect.AeroBoosterLayer:78-100 逐值照搬）
	// 贴图：1.3 用灾厄的 Projectiles/Ranged/FallenStarAura ✗ 2.x 无该资源 ✓ 已从 1.3 灾厄包解包补入本模组 Content/V104/Extras/
	public class AeroBoosterDrawLayerV104 : PlayerDrawLayer
	{
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (!drawPlayer.active || drawPlayer.dead || drawPlayer.outOfRange) return false;
			return drawPlayer.V104().AeroJumpEffect > 0;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			CSPlayerV104 mplayer = drawPlayer.V104();
			Vector2 drawPos = drawInfo.Position + drawPlayer.Hitbox.Size() / 2f;
			if (mplayer.AeroJumpEffect > 0)
			{
				Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/FallenStarAuraV104", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
				SpriteEffects spriteEffects = SpriteEffects.None;
				if (drawPlayer.direction == -1) spriteEffects |= SpriteEffects.FlipHorizontally;
				if (drawPlayer.gravDir == -1) spriteEffects |= SpriteEffects.FlipVertically;
				Color color = Color.Yellow * (1 - drawInfo.shadow) * 0.8f;
				DrawData data = new DrawData(texture, drawPos - Main.screenPosition, null,
					color, mplayer.AeroJumpDirection + MathHelper.PiOver2, new Vector2(17, 20), 1.6f, spriteEffects, 0);
				drawInfo.DrawDataCache.Add(data);
				for (int i = 0; i < 3; i++)
				{
					data = new DrawData(texture, drawPos - Main.screenPosition + (mplayer.AeroJumpDirection
						+ MathHelper.ToRadians(i * 120 + drawPlayer.miscCounter * 12f)).ToRotationVector2() * 15f,
						null, color * 0.5f, mplayer.AeroJumpDirection + MathHelper.PiOver2, new Vector2(17, 20), 1.6f, spriteEffects, 0);
					drawInfo.DrawDataCache.Add(data);
				}
			}
		}
	}

	// 钨钢力场的外观（1.3 CSPlayerDrawEffect.WulfrumFieldLayer:180-195 逐值；贴图 12 帧）
	// 贴图：1.3 用本模组 Extras/WulfrumField ✓ 从 v1.0.4.0 包取（新版才有的资源）
	public class WulfrumFieldDrawLayerV104 : PlayerDrawLayer
	{
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (!drawPlayer.active || drawPlayer.dead || drawPlayer.outOfRange) return false;
			CSPlayerV104 mplayer = drawPlayer.V104();
			return mplayer.WulfrumField && mplayer.WulfrumFieldCooldown <= 0;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawInfo.shadow != 0f) return;
			CSPlayerV104 mplayer = drawPlayer.V104();
			if (!(mplayer.WulfrumField && mplayer.WulfrumFieldCooldown <= 0)) return;
			Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/WulfrumFieldV104", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			int frame = (int)(mplayer.WulfrumFieldAnimationTimer / 8f) % 12;
			Rectangle sRect = new Rectangle(0, 120 * frame, 118, 118);
			DrawData data = new DrawData(texture, drawPlayer.Center - Main.screenPosition, sRect, Color.White * 0.9f,
				0f, new Vector2(59, 59), 1f, SpriteEffects.None, 0);
			drawInfo.DrawDataCache.Add(data);
		}
	}

	// 龙蒿 8 叶效果 ⑭：玩家身周的增益叶片（1.3 CSPlayerDrawEffect.cs:160-179 逐字）
	// 贴图 v1.0.4.0 包 Extras/TarragonLeaves → Content/V104/Extras/TarragonLeavesV104.png（已核实 32×272 竖排 8 帧 ✓ 帧高 34 ✓）
	public class TarragonLeafDrawLayerV104 : PlayerDrawLayer
	{
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (!drawPlayer.active || drawPlayer.dead || drawPlayer.outOfRange) return false;
			return drawPlayer.V104().TarragonLeaf;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow != 0f) return;                                   // 1.3: if (drawInfo.shadow != 0f) return
			Player drawPlayer = drawInfo.drawPlayer;
			CSPlayerV104 mplayer = drawPlayer.V104();
			if (!mplayer.TarragonLeaf) return;
			Vector2 drawPos = drawInfo.Position + drawPlayer.Hitbox.Size() / 2f;
			Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/TarragonLeavesV104", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (drawPlayer.gravDir == -1) spriteEffects = SpriteEffects.FlipVertically;
			for (int i = 0; i < mplayer.TarragonLeafEffects.Length; i++)
			{
				if (mplayer.TarragonLeafEffects[i] <= 0) continue;
				// 1.3: CSUtils.ToDegreesVector2(i, 360f / Length, drawPlayer.miscCounter / 300f * 360f) * 64
				Vector2 dir = MathHelper.ToRadians(i * (360f / mplayer.TarragonLeafEffects.Length) + drawPlayer.miscCounter / 300f * 360f).ToRotationVector2() * 64;
				Rectangle sRect = new Rectangle(0, i * 34, 32, 32);
				Color color = Color.White * MathHelper.Clamp(mplayer.TarragonLeafEffects[i] * 0.005f + 1 / 3f, 0f, 1f) * 0.7f;
				DrawData data = new DrawData(texture, drawPos + dir - Main.screenPosition, sRect, color,
					drawPlayer.fullRotation, new Vector2(8), 1f, spriteEffects, 0);
				drawInfo.DrawDataCache.Add(data);
			}
		}
	}

	// ============================================================================
	// 斯塔提斯魔石的「擦弹区域」（1.3 CSPlayerDrawEffect.cs:136-159 的两个 PlayerLayer 逐值照搬）
	//   ⚠️ V104 原先只搬了 StatigelArea 的【逻辑】（CSPlayerV104Effects 里那个 125px 检测），
	//      这两个【绘制层】整个漏掉 ⇒ 玩家实测「没有擦弹区域」（效果在、画面不在）✗
	//   1.3 的层序（CSPlayerDrawEffect.cs:70-76）：StatisAreaInnerLayer 插在最前（最底层）、
	//      StatisAreaLayer 追加到最后（最上层）⇒ 这里分别对应 BeforeParent(MountBack) / AfterParent(EyebrellaCloud)
	//   贴图：1.3 Extras/StatigelGrazeArea(_Inner)（rawimg 头声明 250×250）→ 已复制为本工程
	//      Content/V104/Extras/StatigelGrazeAreaV104.png / StatigelGrazeAreaInnerV104.png（同为 250×250 ✓）
	// ============================================================================
	public class StatigelGrazeAreaInnerLayerV104 : PlayerDrawLayer
	{
		private static ReLogic.Content.Asset<Texture2D> _tex;
		public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.MountBack);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow > 0f) return false;   // 1.3: if (drawInfo.shadow > 0f) return;
			Player p = drawInfo.drawPlayer;
			if (!p.active || p.dead || p.outOfRange) return false;
			return p.V104().StatigelArea;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player p = drawInfo.drawPlayer;
			// [2026-10-11] 静态缓存（不在绘制路径里 ImmediateLoad —— 遵循本工程既有教训）
			_tex ??= ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/StatigelGrazeAreaInnerV104", AssetRequestMode.AsyncLoad);
			Texture2D texture = _tex.Value;
			if (texture == null) return;
			// 1.3：Color.White * 0.3f; color.A /= 2;（内层）
			Color color = Color.White * 0.3f;
			color.A /= 2;
			DrawData data = new DrawData(texture, p.Center - Main.screenPosition, null, color,
				MathHelper.ToRadians(p.miscCounter * 1.2f * 8f), texture.Size() / 2f, 1f, SpriteEffects.None, 0);
			drawInfo.DrawDataCache.Add(data);
		}
	}

	public class StatigelGrazeAreaLayerV104 : PlayerDrawLayer
	{
		private static ReLogic.Content.Asset<Texture2D> _tex;
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.EyebrellaCloud);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow > 0f) return false;
			Player p = drawInfo.drawPlayer;
			if (!p.active || p.dead || p.outOfRange) return false;
			return p.V104().StatigelArea;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player p = drawInfo.drawPlayer;
			_tex ??= ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/StatigelGrazeAreaV104", AssetRequestMode.AsyncLoad);
			Texture2D texture = _tex.Value;
			if (texture == null) return;
			// 1.3：Color.White * 0.7f; color.A /= 2;（外层）
			Color color = Color.White * 0.7f;
			color.A /= 2;
			DrawData data = new DrawData(texture, p.Center - Main.screenPosition, null, color,
				MathHelper.ToRadians(p.miscCounter * 1.2f * 8f), texture.Size() / 2f, 1f, SpriteEffects.None, 0);
			drawInfo.DrawDataCache.Add(data);
		}
	}

	// ============================================================================
	// 软壳魔石（EnchMolluskV104）的「水淹视觉」—— 1.3 CodeEdit/OnEditControl.cs 里
	//   On.Terraria.Main.DrawPlayer 钩子的 1.4 等价实现。
	//   ⚠️ 原 CSWaterDrawHookV104 试图挂 Main.DrawPlayer(Player,vector2,float,vector2,float,float)，
	//      但【1.4 里不存在这个静态方法】（启动日志："[CS V104] 未找到 Main.DrawPlayer（静态）⇒ 按约束不硬修"）
	//      ⇒ 水面背景从来不画 ⇒ 玩家实测「软壳生效了 但是背景没生效」✓
	//   本层 = 等价的 1.4 实现：在【该玩家本体画完之后】用 WaterEffect 画那张 1280×1280 的水面。
	//   1.3 原文（逐行对应，见 CSWaterDrawHookV104 顶部注释）：判 shadow != 0 / gameMenu / MolluskDrown，
	//   然后 Apply WaterEffect（uTime=miscCounter、uPosition=中心取模 /1280、uImageSize1/2=两张水图尺寸、
	//   GraphicsDevice.Textures[1]/[2]=WaterOver/WaterUnder），最后用 BlankTexture 画一个以玩家为中心
	//   的 1280×1280 方框。
	//   ⚠️ 这里会 End/Begin 一次批次（着色器必须如此），但【不会在里面重新跑玩家绘制管线】
	//      ⇒ 不存在 CosmicEffect 那种"嵌套 PlayerRenderer 打断批次"的泄漏风险 ✓
	// ============================================================================
	public class MolluskWaterOverlayLayerV104 : PlayerDrawLayer
	{
		private static ReLogic.Content.Asset<Effect> _fx;
		private static ReLogic.Content.Asset<Texture2D> _over;
		private static ReLogic.Content.Asset<Texture2D> _under;
		private static ReLogic.Content.Asset<Texture2D> _blank;

		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.EyebrellaCloud);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow != 0f) return false;   // 1.3: if (shadow != 0f) return;
			if (Main.gameMenu) return false;           // 1.3: || Main.gameMenu
			Player p = drawInfo.drawPlayer;
			return p.active && p.V104().MolluskDrown;  // 1.3: !drawPlayer.CS().MolluskDrown（本工程读新版标记 ✓）
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player p = drawInfo.drawPlayer;
			// [2026-10-11] 静态缓存：不在绘制路径里 ImmediateLoad
			_fx ??= ModContent.Request<Effect>("CalamitySouls/Effects/WaterEffect", AssetRequestMode.AsyncLoad);
			_over ??= ModContent.Request<Texture2D>("CalamitySouls/Extras/WaterOver", AssetRequestMode.AsyncLoad);
			_under ??= ModContent.Request<Texture2D>("CalamitySouls/Extras/WaterUnder", AssetRequestMode.AsyncLoad);
			_blank ??= ModContent.Request<Texture2D>("CalamitySouls/Extras/BlankTexture", AssetRequestMode.AsyncLoad);
			Effect fx = _fx.Value;
			Texture2D over = _over.Value, under = _under.Value, blank = _blank.Value;
			if (fx == null || over == null || under == null || blank == null) return;

			SpriteBatch sb = Main.spriteBatch;
			sb.End();
			try
			{
				fx.CurrentTechnique.Passes[0].Apply();
				fx.Parameters["uTime"].SetValue(p.miscCounter);
				fx.Parameters["uPosition"].SetValue(new Vector2(
					System.Math.Abs(p.Center.X % 1280f) / 1280f,
					System.Math.Abs(p.Center.Y % 1280f) / 1280f));
				fx.Parameters["uImageSize1"].SetValue(over.Size());
				fx.Parameters["uImageSize2"].SetValue(under.Size());
				Main.graphics.GraphicsDevice.Textures[1] = over;
				Main.graphics.GraphicsDevice.Textures[2] = under;

				sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState,
					DepthStencilState.None, Main.Rasterizer, fx, Main.GameViewMatrix.TransformationMatrix);
				Vector2 screenCenter = p.Center - Main.screenPosition;
				Rectangle rect = new Rectangle(
					(int)(screenCenter.X - 640f), (int)(screenCenter.Y - 640f), 1280, 1280);
				sb.Draw(blank, rect, Color.White);
			}
			finally
			{
				// 无论成败都把批次恢复成框架期望的状态（Deferred + AlphaBlend，不带着色器）
				sb.End();
				sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState,
					DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
			}
		}
	}
}
