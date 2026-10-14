using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;

namespace CalamitySouls.Content.V104
{
    // ============================================================================
    // 【第 1 批】软壳魔石（EnchMolluskV104）的"水淹视觉"—— 1.3 CodeEdit/OnEditControl.cs 的
    //   On.Terraria.Main.DrawPlayer 钩子的 1.4 等价实现（同文件 CSManualHooksV104 的 WetCollision 同款做法）。
    //
    // 1.3 原文（逐行对应）：
    //   orig(self, drawPlayer, Position, rotation, rotationOrigin, shadow);
    //   if (shadow != 0f) return;
    //   if (Main.gameMenu || !drawPlayer.CS().MolluskDrown) return;
    //   WaterEffect.CurrentTechnique.Passes[0].Apply();
    //   WaterEffect.Parameters["uTime"].SetValue(drawPlayer.miscCounter);
    //   WaterEffect.Parameters["uPosition"].SetValue(new Vector2(|Center.X % 1280|/1280, |Center.Y % 1280|/1280));
    //   WaterEffect.Parameters["uImageSize1"].SetValue(CSTexture.WaterOver.Size());
    //   WaterEffect.Parameters["uImageSize2"].SetValue(CSTexture.WaterUnder.Size());
    //   Main.graphics.GraphicsDevice.Textures[1] = CSTexture.WaterOver;
    //   Main.graphics.GraphicsDevice.Textures[2] = CSTexture.WaterUnder;
    //   Main.spriteBatch.Draw(BlankTexture, SpawnCenteredRectan(Center - screenPosition, 1280), Color.White);
    //
    // 资源（均已核实存在于 1.3 发布包 ✓ 且已搬入本工程 ✓）：
    //   Effects/WaterEffect.fx 1132B + .xnb 3948B · Extras/WaterOver.png 1000x300 · Extras/WaterUnder.png 755x755
    //   Extras/BlankTexture.png 68B
    //
    // ⚠️ 约束（按裁决）：
    //   ① 钩子全程 try/catch：签名不符/挂不上 ⇒ **只记日志** + 差异表，绝不硬修、绝不破坏可编译/可加载状态
    //   ② 这是**视觉层**，不影响 MolluskDrown/MolluskDrown2 的逻辑（那部分已在 CSManualHooksV104 里实现 ✓）
    // ============================================================================
    public class CSWaterDrawHookV104 : ModSystem
    {
        internal static string WaterDrawHookStatus = "未尝试";

        private static bool hooked;

        public override void Load()
        {
            try
            {
                MethodInfo mi = typeof(Main).GetMethod("DrawPlayer", BindingFlags.Public | BindingFlags.Static);
                if (mi == null)
                {
                    WaterDrawHookStatus = "未找到 Main.DrawPlayer（静态）⇒ 按约束不硬修，记差异表";
                    Mod.Logger.Warn("[CS V104] " + WaterDrawHookStatus);
                    return;
                }
                ParameterInfo[] ps = mi.GetParameters();
                // 1.4 标准签名：DrawPlayer(Player, Vector2, float, Vector2, float shadow, float alpha)
                if (ps.Length != 6 || ps[0].ParameterType != typeof(Player))
                {
                    WaterDrawHookStatus = "签名不符（参数数 " + ps.Length + "，首参 " + ps[0].ParameterType.Name + "）⇒ 按约束不硬修，记差异表";
                    Mod.Logger.Warn("[CS V104] " + WaterDrawHookStatus);
                    return;
                }
                MonoModHooks.Add(mi,
                    (Action<Action<Player, Vector2, float, Vector2, float, float>,
                            Player, Vector2, float, Vector2, float, float>)
                    ((orig, drawPlayer, position, rotation, rotationOrigin, shadow, alpha) =>
                    {
                        orig(drawPlayer, position, rotation, rotationOrigin, shadow, alpha);
                        try { DrawWaterOverlay(drawPlayer, shadow); }
                        catch (Exception e) { Mod.Logger.Warn("[CS V104] 水淹绘制异常（已忽略）: " + e.Message); }
                    }));
                hooked = true;
                WaterDrawHookStatus = "已挂钩（Main.DrawPlayer）";
                Mod.Logger.Info("[CS V104] " + WaterDrawHookStatus);
            }
            catch (Exception e)
            {
                WaterDrawHookStatus = "挂钩失败（" + e.GetType().Name + "）⇒ 按约束不硬修，记差异表";
                Mod.Logger.Warn("[CS V104] " + WaterDrawHookStatus + " : " + e.Message);
            }
        }

        public override void Unload()
        {
            // 1.4：MonoModHooks 由 tML 在卸载时统一撤销；本系统只复位自己的状态
            hooked = false;
            WaterDrawHookStatus = "已卸载";
        }

        // —— 逐行等价于 1.3 的 Main_DrawPlayer 主体 ——
        private static void DrawWaterOverlay(Player drawPlayer, float shadow)
        {
            if (shadow != 0f) return;                                        // 1.3: if (shadow != 0f) return;
            if (Main.gameMenu) return;                                       // 1.3: if (Main.gameMenu || ...) return;
            if (drawPlayer == null || !drawPlayer.active) return;
            if (!drawPlayer.V104().MolluskDrown) return;                     // 1.3: || !drawPlayer.CS().MolluskDrown

            Effect fx = ModContent.Request<Effect>("CalamitySouls/Effects/WaterEffect", AssetRequestMode.ImmediateLoad).Value;
            Texture2D over = ModContent.Request<Texture2D>("CalamitySouls/Extras/WaterOver", AssetRequestMode.ImmediateLoad).Value;
            Texture2D under = ModContent.Request<Texture2D>("CalamitySouls/Extras/WaterUnder", AssetRequestMode.ImmediateLoad).Value;
            Texture2D blank = ModContent.Request<Texture2D>("CalamitySouls/Extras/BlankTexture", AssetRequestMode.ImmediateLoad).Value;
            if (fx == null || over == null || under == null || blank == null) return;

            fx.CurrentTechnique.Passes[0].Apply();
            fx.Parameters["uTime"].SetValue(drawPlayer.miscCounter);
            fx.Parameters["uPosition"].SetValue(new Vector2(
                Math.Abs(drawPlayer.Center.X % 1280f) / 1280f,
                Math.Abs(drawPlayer.Center.Y % 1280f) / 1280f));
            fx.Parameters["uImageSize1"].SetValue(over.Size());
            fx.Parameters["uImageSize2"].SetValue(under.Size());
            Main.graphics.GraphicsDevice.Textures[1] = over;
            Main.graphics.GraphicsDevice.Textures[2] = under;

            // 1.3: SpawnCenteredRectan(drawPlayer.Center - Main.screenPosition, 1280)
            // 1.4：就地等价（以玩家屏幕坐标为中心、1280×1280 的方框）
            Vector2 screenCenter = drawPlayer.Center - Main.screenPosition;
            Rectangle rect = new Rectangle(
                (int)(screenCenter.X - 640f), (int)(screenCenter.Y - 640f), 1280, 1280);
            Main.spriteBatch.Draw(blank, rect, Color.White);
        }
    }
}
