using System;
using System.Collections.Generic;
using CalamityMod.Skies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Effects
{
	public class FearmongerTimeFreezeSky : CustomSky
	{
		public override void Update(GameTime gameTime)
		{
			if (Intensity > 0f && Utils.NextBool(Main.rand, 8))
			{
				int lifetime = Main.rand.Next(285, 445)*2;
				float depth = Utils.NextFloat(Main.rand, 1.8f, 5f);
				Vector2 startingPosition = Main.screenPosition + new Vector2(Main.screenWidth * Utils.NextFloat(Main.rand, -0.1f, 1.1f), -Main.screenHeight * 1.05f);
				Vector2 startingVelocity = Utils.RotatedByRandom(Vector2.UnitY, 0.9100000262260437);
				Flakes.Add(new Flake(lifetime, Flakes.Count, depth, Color.White, startingPosition, startingVelocity,Main.rand.Next(3)));
			}
			if (Flakes.Count > 0) 
			for (int i = 0; i < Flakes.Count; i++)
			{
				Flake flake = Flakes[i];
				flake.Scale = Utils.GetLerpValue(flake.Lifetime, flake.Lifetime / 3, flake.Time, true);
				flake.Scale *= MathHelper.Lerp(0.6f, 0.9f, flake.IdentityIndex % 6f / 6f);
				Vector2 idealVelocity = Utils.RotatedBy(Vector2.UnitY, MathHelper.Lerp(-0.94f, 0.94f,
					((float)Math.Sin((flake.Time / 36f) + flake.IdentityIndex) * 0.5f) + 0.5f), default) * 4.5f;
				float movementInterpolant = MathHelper.Lerp(0.01f, 0.08f, Utils.GetLerpValue(45f, 145f, flake.Time, true));
				flake.Velocity = Vector2.Lerp(flake.Velocity, idealVelocity, movementInterpolant);
				flake.Velocity = Utils.SafeNormalize(flake.Velocity, Vector2.UnitY) * 4.5f;
				flake.Time++;
				flake.Center += flake.Velocity;
			}
			Flakes.RemoveAll((Flake c) => c.Time >= c.Lifetime);
			if (CurrentlyActive)
			{
				if (Intensity < 1f)
				{
					Intensity += 0.03f;
				}
				CurrentInterest = MathHelper.Clamp(CurrentInterest - 0.005f, CurrentInterestMin, 1f);
				IncrementalInterest = MathHelper.Lerp(IncrementalInterest, CurrentInterest, 0.085f);
				IdleTimer += MathHelper.Lerp(0.04f, 0.1f, IncrementalInterest);
				return;
			}
			if (!CurrentlyActive && Intensity > 0f)
			{
				Intensity -= 0.03f;
				CurrentInterest = 0f;
				IncrementalInterest = 0f;
				IdleTimer = 0f;
			}
		}
		public override Color OnTileColor(Color inColor)
		{
			return Color.Lerp(inColor, Color.DarkBlue, Intensity);
		}
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (Intensity == 0f)
			{
				return;
			}
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				Vector2 screenCenter2 = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
				float scale = MathHelper.Lerp(0.8f, 0.9f, IncrementalInterest) + (float)Math.Sin(IdleTimer) * 0.01f;
				Vector2 drawPosition = (new Vector2(Main.LocalPlayer.Center.X, 1120f) - screenCenter2) * 0.097f + screenCenter2 - Main.screenPosition - Vector2.UnitY * 100f;
				Texture2D eyeTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/FearmongerClock", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                Color baseColorDraw = Color.Lerp(Color.White, Color.Red, IncrementalInterest);
				baseColorDraw *= Intensity;
                spriteBatch.Draw(eyeTexture, drawPosition, null, baseColorDraw, 0f, Utils.Size(eyeTexture) * 0.5f, scale, SpriteEffects.None, 0f);
				Color fadedColor = Color.Lerp(baseColorDraw, Color.Coral, 0.3f) * MathHelper.Lerp(0.18f, 0.3f, IncrementalInterest);
				fadedColor.A = 0;
				fadedColor *= Intensity;
				float backEyeOutwardness = MathHelper.Lerp(8f, 4f, IncrementalInterest);
				int backInstances = (int)MathHelper.Lerp(6f, 24f, IncrementalInterest);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
				for (int i = 0; i < backInstances; i++)
				{
					Vector2 drawOffset = Utils.ToRotationVector2((12.566371f * i / backInstances) + (Main.GlobalTimeWrappedHourly * 2.1f)) * backEyeOutwardness;
					spriteBatch.Draw(eyeTexture, drawPosition + drawOffset, null, fadedColor, 0f, Utils.Size(eyeTexture) * 0.5f, scale, SpriteEffects.None, 0f);
				}
				Main.spriteBatch.End();
				Main.spriteBatch.Begin();
			}
			Texture2D flakeTexture = ModContent.Request<Texture2D>("CalamitySouls/Extras/FearmongerFlake", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			if (flakeTexture == null)
            {
                Main.NewText("fuck");
				return;
            }
            for (int i = 0; i < Flakes.Count; i++)
			{
				Vector2 drawPosition = Flakes[i].Center - Main.screenPosition;
                //Rectangle? sRect = null;
                Rectangle sRect = new Rectangle(0, 26 * Flakes[i].frame, 26, 26);
				//Vector2 origin = flakeTexture.Size() / 2f;
				Vector2 origin = sRect.Size() / 2f;
                //for (int j = 0; j < 3; j++)
                //{
                //	Vector2 offsetDrawPosition = drawPosition + Utils.ToRotationVector2(6.2831855f * j / 3f) * 1.4f;
                //	Color offsetDrawColor = Color.SkyBlue * 0.56f;
                //	offsetDrawColor.A = 0;
                //	spriteBatch.Draw(flakeTexture, offsetDrawPosition, sRect, offsetDrawColor, 0f, origin, Flakes[i].Scale * 1.5f/2f, SpriteEffects.None, 0f);
                //}
                spriteBatch.Draw(flakeTexture, drawPosition, sRect, Flakes[i].DrawColor, 0f, origin, Flakes[i].Scale/2f, SpriteEffects.None, 0f);
			}
		}
		public override float GetCloudAlpha()
		{
			return 1f - Intensity;
		}
		public override void Activate(Vector2 position, params object[] args)
		{
			CurrentlyActive = true;
		}
		public override void Deactivate(params object[] args)
		{
			CurrentlyActive = false;
		}
		public override void Reset()
		{
			CurrentlyActive = false;
		}
		public override bool IsActive()
		{
			return CurrentlyActive || Intensity > 0f;
		}
		public bool CurrentlyActive;
		public static float Intensity;
		public static float IdleTimer;
		public static float CurrentInterest;
		public static float IncrementalInterest;
		public static float CurrentInterestMin;

		public List<Flake> Flakes = new List<Flake>();
		public class Flake
		{
			public Flake(int lifetime, int identity, float depth, Color color, Vector2 startingPosition, Vector2 startingVelocity,int startingFrame)
			{
				Lifetime = lifetime;
				IdentityIndex = identity;
				Depth = depth;
				DrawColor = color;
				Center = startingPosition;
				Velocity = startingVelocity;
				frame = startingFrame;
			}
			public int Time;
			public int Lifetime;
			public int IdentityIndex;
			public float Scale;
			public float Depth;
			public Color DrawColor;
			public Vector2 Velocity;
			public Vector2 Center;
			public int frame;
		}
	}
}

