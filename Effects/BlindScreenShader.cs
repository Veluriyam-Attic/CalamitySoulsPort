using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamitySouls.Effects
{
    public class BlindScreenShader : ScreenShaderData
    {
        public BlindScreenShader(Ref<Effect> shader, string passName) : base(shader, passName) { }
        public override void Apply()
        {
            try
            {
            Player player = Main.LocalPlayer;
                // ⚠️ 1.3 差异：1.3 的 PlagueReaperBlind 是倒计时（int）⇒ 用 blind/120f 做渐隐进度；
                //    本工程（1.4 移植版）该标记是 bool ⇒ 无渐变源，故固定 1f。已记差异表（仅影响滤镜渐隐速度，非逻辑降级）。
                if (player != null && player.CS().PlagueReaperBlind)
                    UseProgress(1f);
            base.Apply();
            }
            catch
            {
                Main.NewText("A bug occured while using plague reaper blind effect");
            }
        }
    }
}