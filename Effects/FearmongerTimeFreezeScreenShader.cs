using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamitySouls.Effects
{
	public class FearmongerTimeFreezeScreenShader : ScreenShaderData
	{
		public FearmongerTimeFreezeScreenShader(Ref<Effect> shader, string passName) : base(shader, passName) { }
		public override void Apply()
		{
			UseTargetPosition(Main.LocalPlayer.Center);
			base.Apply();
		}
		public override void Update(GameTime gameTime)
		{
			UseIntensity(FearmongerTimeFreezeSky.Intensity / 3f);
			UseProgress(1f / (FearmongerTimeFreezeSky.Intensity + 0.01f));
		}
	}
}
