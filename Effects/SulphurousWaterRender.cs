using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.GameContent;
using Terraria.ModLoader;
using CalamitySouls.Projectiles;
using ReLogic.Graphics;
using ReLogic.Content;
using System;

namespace CalamitySouls
{
    public static class SulphurousWaterRender
    {
        public static RenderTarget2D projRender;
        public static Effect SulphurousWaterEffect;
        public static Effect ScaleEffect;
        public static Texture2D SulphurousTexture;
        public static Texture2D circleTexture;
        public static void Load(Mod mod)
        {
            // 1.4：Mod.Load 由线程池线程执行（日志里显示为 .NET TP Worker），而 RenderTarget2D /
            // Texture2D 的创建必须在主线程进行，否则 FNA 会抛
            //   "most FNA3D audio/graphics functions must be called on the main thread"
            // 这条渲染路径当前并未启用（CalamitySoulsSystem 里的调用是注释掉的），
            // 因此这里不做任何 GPU 操作，改为在主线程首次真正渲染时惰性创建。
            //
            // 注意：不要在这里加 new RenderTarget2D / new Texture2D / SetData，会直接导致模组加载失败。
        }

        // 在主线程里惰性创建 GPU 资源；没有图形设备（服务端）或屏幕尺寸未就绪时返回 false
        private static bool EnsureResources()
        {
            if (projRender != null) return true;
            if (Main.dedServ || Main.instance == null || Main.instance.GraphicsDevice == null) return false;
            if (Main.screenWidth <= 0 || Main.screenHeight <= 0) return false;
            try
            {
                projRender = new RenderTarget2D(Main.instance.GraphicsDevice, Main.screenWidth, Main.screenHeight);
                SulphurousWaterEffect = ModContent.Request<Effect>("CalamitySouls/Effects/SulphurousWaterEffect", AssetRequestMode.ImmediateLoad).Value;
                ScaleEffect = ModContent.Request<Effect>("CalamitySouls/Effects/ScaleEffect", AssetRequestMode.ImmediateLoad).Value;
                SulphurousTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/SulphurousTexture").Value;
                circleTexture = CreateACircle(Main.instance.GraphicsDevice, 100);
                return true;
            }
            catch (System.Exception e)
            {
                // ⭐ [2026-10-11] 资源创建失败时：只报一次明确日志并【彻底停用】这条渲染路径，
                //    绝不每帧抛异常（实测会连锁毁掉 UI）。projRender 置空 ⇒ 下次不再重试到底。
                projRender = null;
                if (!_resourceFailedLogged)
                {
                    _resourceFailedLogged = true;
                    Terraria.ModLoader.ModLoader.GetMod("CalamitySouls").Logger.Error(
                        "[硫海剧毒区域] 渲染资源创建失败，已停用该渲染路径（不再重试）：" + e);
                }
                return false;
            }
        }
        private static bool _resourceFailedLogged;
        public static void Unload()
        {
            projRender = null;
            SulphurousWaterEffect = null;
            ScaleEffect = null;
            SulphurousTexture = null;
            circleTexture = null;
        }
        public static bool CanUseRender
        {
            get
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (!proj.active) continue;
                    // ⭐ [2026-10-11] 同时认【新版】的剧毒区域弹幕（原实现只认旧版 ⇒ 新版装生灵之力时
                    //    这条渲染路径永远不触发 ⇒ 玩家实测「剧毒区域的外部显示没有，1.3 是有的」✓）
                    if (proj.type == ModContent.ProjectileType<SulphurousArea>()) return true;
                    if (proj.type == ModContent.ProjectileType<global::CalamitySouls.Content.V104.Projectiles.SulphurousAreaV104>()) return true;
                }
                for (int i = 0; i < Main.dust.Length; i++)
                {
                    Dust dust = Main.dust[i];
                    if (dust.active && dust.type == ModContent.DustType<SulphurousDust>()) return true;
                }
                return false;
            }
        }
        // 1.4：FilterManager.EndCapture 的签名变了，MonoMod detour 不再适用；
        // 该渲染路径本身已被 CalamitySoulsSystem 注释掉，这里保留原委托定义以避免牵连其它代码。
        private delegate void orig_EndCapture(FilterManager self);
        public static void DoRender(Action org)
        {
            // DoRender 只在主线程（渲染阶段）被调用，这里才创建 GPU 资源
            if (!CanUseRender || !EnsureResources())
            {
                org();
                return;
            }
            GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
            graphicsDevice.SetRenderTarget(projRender);
            try
            {
                graphicsDevice.Clear(Color.Transparent);

                // ⭐ [2026-10-11 加固] 批次进出全部走 SafeBegin/SafeEnd：实测日志里有
                //    `InvalidOperationException: End was called, but Begin has not yet been called`（01:04 会话）
                //    从本方法抛出 ⇒ 任何一次 Begin/End 失败都会连锁冒到框架（UI 全灭）。包装后最坏只是这一帧不画 ✓
                SafeBegin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone,
                    ScaleEffect, Main.GameViewMatrix.TransformationMatrix);
                ScaleEffect.CurrentTechnique.Passes["Scale"].Apply();
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (!proj.active) continue;
                    if (proj.type == ModContent.ProjectileType<SulphurousArea>()) DrawProj(Main.spriteBatch, proj);
                    // ⭐ [2026-10-11] 新版（V104）的剧毒区域也画进来 —— 1.3 只有旧版这一种，
                    //    而新版魔石生成的是 SulphurousAreaV104 ⇒ 原实现永远画不到 ⇒ 外部显示缺失 ✓
                    else if (proj.type == ModContent.ProjectileType<global::CalamitySouls.Content.V104.Projectiles.SulphurousAreaV104>())
                        DrawAreaByRange(Main.spriteBatch, proj.Center, proj.ai[1]);   // 新版 Range = ai[1]，与 1.3 同义
                }
                for (int i = 0; i < Main.dust.Length; i++)
                {
                    Dust dust = Main.dust[i];
                    if (dust.active && dust.type == ModContent.DustType<SulphurousDust>()) DrawDust(Main.spriteBatch, dust);
                }
                SafeEnd();
            }
            finally
            {
                // ⭐⭐ [2026-10-11 修 · 关键] 无论如何都必须把渲染目标解绑回屏幕（= null）！
                //    1.3 这一步是 `org()` 干的 —— 它就是 FilterManager.EndCapture 的原函数，职责正是
                //    "结束截获、把渲染目标切回屏幕"。我最初给 org 传了空委托 ⇒ 这一步被跳过 ⇒
                //    projRender 一直处于绑定状态 ⇒ 之后【世界后续绘制与全部 UI】都被画进了这张离屏 RT
                //    而不是屏幕 ⇒ 玩家实测「ui 直接消失了」✗
                //    放在 finally 里：即使上面的绘制抛异常，也绝不会再吞掉 UI ✓
                //    （End 用 try 包住：批次若已被结束，End() 会抛 InvalidOperationException，忽略即可）
                SafeEnd();
                graphicsDevice.SetRenderTarget(null);
            }
            org();
            SafeBegin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            try
            {
                SulphurousWaterEffect.CurrentTechnique.Passes["SulWater"].Apply();
                SulphurousWaterEffect.Parameters["uImageSize"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
                SulphurousWaterEffect.Parameters["uAlpha"].SetValue(0.25f);
                // ⭐ [2026-10-11 恢复] uColor —— 这是「绿色光环」的关键，也是本文件历史的一个转折点：
                //    · 1.3 原件 Effects/SulphurousWaterEffect.fx 声明了 `float3 uColor`，
                //      填充 `float4(uColor, r)`、描边 `float4(uColor, 1.0)` 全取自它 ⇒ 装圣蚀之环时是绿 ✓
                //    · V104 移植时把着色器【改写】成"用贴图颜色 + 硬编码黄描边"，颜色切换因此丢失 ✗
                //    · 我一度想在 C# 里补回 uColor，但当时游戏里的 .xnb 仍是被改写的版本（无此参数）
                //      ⇒ EffectParameter 为 null ⇒ .SetValue 抛 NullReferenceException ⇒ 批次失衡 ⇒ UI 全灭 ✗
                //    · 现在已把 .fx/.xnb 换成 1.3 的原件（946B/3836B，参数表里有 uColor）⇒ 必须在这里传值 ✓
                //    取值照 1.3 SulphurousWaterRender.cs:87 逐字：圣蚀之环 → 绿，否则 → 黄。
                //    ⚠️ 仍保留 null 判断：万一 .xnb 与 .fx 不匹配，也只是"不传色"，绝不抛异常 ✓
                EffectParameter uColor = SulphurousWaterEffect.Parameters["uColor"];
                if (uColor != null)
                {
                    bool acidivineCircle = Main.LocalPlayer
                        .GetModPlayer<global::CalamitySouls.Content.V104.CSPlayerV104>().AcidivineCircle;
                    uColor.SetValue(acidivineCircle ? new Vector3(0.52f, 0.85f, 0.05f) : new Vector3(0.97f, 1f, 0.43f));
                }
                Main.graphics.GraphicsDevice.Textures[1] = SulphurousTexture;
                Main.spriteBatch.Draw(projRender, Vector2.Zero, Color.White);
            }
            finally
            {
                // ⭐ 无论成败都要关掉这里开的批次 —— 否则异常会带着"批次仍开着"的状态冒出去，
                //    调用方再 Begin 就会抛 "Begin has been called before calling End" ⇒ UI 连环失效 ✗
                SafeEnd();
            }
        }

        // ---- 批次安全包装：任何一次 Begin/End 失败都不会把异常抛给框架（否则会连锁毁掉 UI）----
        private static void SafeEnd()
        {
            try { Main.spriteBatch.End(); } catch { }
        }
        private static bool SafeBegin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState,
            DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix matrix)
        {
            try
            {
                Main.spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
                return true;
            }
            catch { return false; }
        }

        // ⭐ [2026-10-11] 新版剧毒区域的圆绘制（与 DrawProj 同款参数，只是不做类型转换）
        private static void DrawAreaByRange(SpriteBatch sb, Vector2 position, float range)
        {
            ScaleEffect.Parameters["uImageSize"].SetValue(new Vector2(range * 2f));
            float scale = range / 100f;
            sb.Draw(circleTexture, position - Main.screenPosition, null, Color.White, 0f,
                new Vector2(100), scale, SpriteEffects.None, 0);
        }
        public static void DrawProj(SpriteBatch sb, Projectile projectile)
        {
            SulphurousArea area = projectile.ModProjectile as SulphurousArea;
            ScaleEffect.Parameters["uImageSize"].SetValue(new Vector2(area.Range * 2));
            float scale = area.Range / 100;
            sb.Draw(circleTexture, projectile.position - Main.screenPosition, null, Color.White, 0f,
                            new Vector2(100), scale, SpriteEffects.None, 0);
        }
        public static void DrawDust(SpriteBatch sb, Dust dust)
        {
            int range = (int)(dust.scale * 25);
            if (range <= 0) range = 1;
            ScaleEffect.Parameters["uImageSize"].SetValue(new Vector2(range * 2));
            float scale = range / 100f;
            sb.Draw(circleTexture, dust.position - Main.screenPosition, null, Color.White, 0f,
                       new Vector2(100), scale, SpriteEffects.None, 0);
        }
        private static Texture2D CreateACircle(GraphicsDevice gd, int r)
        {
            int d = 2 * r;
            Texture2D circle = new Texture2D(gd, d, d);
            Color[] colors = new Color[d * d];
            for (int x = 0; x < d; x++)
            {
                for (int y = 0; y < d; y++)
                {
                    int index = x * d + y;
                    Vector2 point = new Vector2(x - r, y - r);
                    if (point.LengthSquared() < r * r) colors[index] = Color.White;
                    else colors[index] = Color.Transparent;
                }
            }
            circle.SetData(colors);
            return circle;
        }
        private static void DrawCombatText()
        {
            float num81 = CombatText.TargetScale;
            for (int n = 0; n < 100; n++)
            {
                if (Main.combatText[n].active)
                {
                    int num82 = 0;
                    if (Main.combatText[n].crit)
                    {
                        num82 = 1;
                    }
                    Vector2 vector6 = FontAssets.CombatText[num82].Value.MeasureString(Main.combatText[n].text);
                    Vector2 vector7 = new Vector2(vector6.X * 0.5f, vector6.Y * 0.5f);
                    float num83 = Main.combatText[n].scale / num81;
                    float num84 = Main.combatText[n].color.R;
                    float num85 = Main.combatText[n].color.G;
                    float num86 = Main.combatText[n].color.B;
                    float num87 = Main.combatText[n].color.A;
                    num84 *= num83 * Main.combatText[n].alpha * 0.3f;
                    num86 *= num83 * Main.combatText[n].alpha * 0.3f;
                    num85 *= num83 * Main.combatText[n].alpha * 0.3f;
                    num87 *= num83 * Main.combatText[n].alpha;
                    Color color9 = new Color((int)num84, (int)num85, (int)num86, (int)num87);
                    for (int num88 = 0; num88 < 5; num88++)
                    {
                        float num89 = 0f;
                        float num90 = 0f;
                        if (num88 == 0)
                        {
                            num89 -= num81;
                        }
                        else if (num88 == 1)
                        {
                            num89 += num81;
                        }
                        else if (num88 == 2)
                        {
                            num90 -= num81;
                        }
                        else if (num88 == 3)
                        {
                            num90 += num81;
                        }
                        else
                        {
                            num84 = Main.combatText[n].color.R * num83 * Main.combatText[n].alpha;
                            num86 = Main.combatText[n].color.B * num83 * Main.combatText[n].alpha;
                            num85 = Main.combatText[n].color.G * num83 * Main.combatText[n].alpha;
                            num87 = Main.combatText[n].color.A * num83 * Main.combatText[n].alpha;
                            color9 = new Color((int)num84, (int)num85, (int)num86, (int)num87);
                        }
                        if (Main.player[Main.myPlayer].gravDir == -1f)
                        {
                            float num91 = Main.combatText[n].position.Y - Main.screenPosition.Y;
                            num91 = Main.screenHeight - num91;
                            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.CombatText[num82].Value, Main.combatText[n].text, new Vector2(Main.combatText[n].position.X - Main.screenPosition.X + num89 + vector7.X, num91 + num90 + vector7.Y), color9, Main.combatText[n].rotation, vector7, Main.combatText[n].scale, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.CombatText[num82].Value, Main.combatText[n].text, new Vector2(Main.combatText[n].position.X - Main.screenPosition.X + num89 + vector7.X, Main.combatText[n].position.Y - Main.screenPosition.Y + num90 + vector7.Y), color9, Main.combatText[n].rotation, vector7, Main.combatText[n].scale, SpriteEffects.None, 0f);
                        }

                    }
                }

            }
        }
    }
}