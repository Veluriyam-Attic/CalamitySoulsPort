// 幻星魔石（EnchAstral）配套弹幕 · 落星 FallenAstralStarV104
// 照搬：ext1040/Projectiles/FallenAstralStar.cs（本模组自己的弹幕）
// 1.4 处置：SetupProjectileTrailling→静态集合 ✓ · CirculateOldpos 省略 ✓ · Kill→OnKill ✓ · PreDraw(ref Color) ✓
//           spriteBatch.Reset(...) → End()/Begin(...) 等价 ✓ · Main.projectileTexture→TextureAssets ✓
//           ForceThrowABuffOnYourFuckingFace → AddBuff（先例 LuxAstriPunchV104 ✓）· ClosetNPC(800f) → 内联等价 ✓
//           音量修饰（Item9 ÷3 · Item10 ×0.25）按（甲）省略 ✓ · CSUtils.RandomRotate → Main.rand.NextVector2Unit ✓
// 贴图：v1.0.4.0 包 Projectiles/FallenAstralStar(.rawimg) + FallenAstralStar_Glow(.rawimg) → 自有路径
using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;

namespace CalamitySouls.Content.V104.Projectiles
{
	public class FallenAstralStarV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[Type] = -1;
			ProjectileID.Sets.TrailCacheLength[Type] = 3;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = 2;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 600;
			Projectile.scale = 1f;
			Projectile.localAI[1] = Main.rand.Next(2);
		}
		public override void AI()
		{
			Projectile.rotation += 0.1f * Math.Sign(Projectile.velocity.X);
			if (Projectile.ai[1] == 1)
			{
				Projectile.scale *= 0.97f;
				Projectile.velocity *= 0.9f;
				Projectile.velocity = Projectile.velocity.RotatedBy(0.05f * Math.Sign(Projectile.velocity.X));
				if (Projectile.scale < 0.1f) Projectile.Kill();
				return;
			}
			if (Main.rand.Next(5) == 0)
			{
				// 1.3 原式三元两支相同（都是 AstralBlue）—— 忠实保留
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
					Main.rand.NextBool() ? DustID.GoldFlame : (Projectile.localAI[1] == 0 ? ModContent.DustType<CalamityMod.Dusts.AstralBlue>() : ModContent.DustType<CalamityMod.Dusts.AstralBlue>()));
				dust.velocity = Projectile.velocity / 3f;
				dust.noGravity = true;
			}
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 20 + Main.rand.Next(40);
				SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);   // 1.3 音量 ÷3 按（甲）省略
			}
			int index = ClosetNPCV104(800f);   // 1.3: projectile.ClosetNPC(800f)
			if (Projectile.velocity.Length() < 12f) Projectile.velocity *= 1.1f;
			if (index != -1)
			{
				if (Projectile.ai[0] < 40f) Projectile.ai[0] += 0.5f;
				float angleLerpAmt = MathHelper.Clamp((Projectile.ai[0] - 20f) / 20f, 0f, 0.95f) + 0.05f;
				Projectile.velocity =
					Utils.AngleLerp(Projectile.velocity.ToRotation(), CSPlayerV104Effects.DirectionToSafe(Projectile, Main.npc[index].Center).ToRotation(),
					angleLerpAmt)
					.ToRotationVector2() * Projectile.velocity.Length();
				Projectile.timeLeft++;
			}
		}

		// 1.3 的灾厄扩展 projectile.ClosetNPC(range) 的内联等价（最近的敌意 NPC）
		private int ClosetNPCV104(float range)
		{
			int best = -1;
			float min = range;
			foreach (NPC npc in Main.npc)
			{
				if (!npc.active || npc.friendly || npc.dontTakeDamage) continue;
				float d = npc.Distance(Projectile.Center);
				if (d < min) { min = d; best = npc.whoAmI; }
			}
			return best;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);   // 1.3: ForceThrowABuffOnYourFuckingFace
			SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);            // 1.3 音量 ×0.25 按（甲）省略
			int dustAmt = 15;
			for (int i = 0; i < dustAmt; i++)
			{
				Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.AncientLight);
				dust.noGravity = true;
				dust.scale *= 1.5f;
				dust.velocity = Main.rand.NextVector2Unit() * 3;
				dust.velocity.X /= 2;
				dust.velocity = dust.velocity.RotatedBy(Projectile.velocity.ToRotation());
				dust.color = Projectile.localAI[1] == 0 ? Color.OrangeRed : Color.Cyan;
			}
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.AncientLight);
				dust.noGravity = true;
				dust.scale *= 1.2f;
				dust.velocity = Main.rand.NextVector2Unit() * 20;
				dust.color = Projectile.localAI[1] == 0 ? Color.OrangeRed : Color.Cyan;
			}
			Projectile.friendly = false;
			Projectile.ai[1] = 1;
			Projectile.velocity = -Projectile.velocity.RotatedByRandom(MathHelper.PiOver2) * 0.9f;
			int amt = Main.rand.Next(3);
			for (int i = 0; i < amt; i++)
			{
				Gore gore = Gore.NewGoreDirect(Projectile.GetSource_FromAI(), Projectile.Center, -Projectile.velocity * 0.2f, Projectile.localAI[1] == 0 ? 16 : 17);
				gore.position = new Vector2(Main.rand.Next(Projectile.Hitbox.X, Projectile.Hitbox.Right), Main.rand.Next(Projectile.Hitbox.Y, Projectile.Hitbox.Bottom));
			}
		}
		public override void OnKill(int timeLeft)
		{
		}
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D glow = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Projectiles/FallenAstralStarGlowV104", AssetRequestMode.ImmediateLoad).Value;
			Vector2 origin = new Vector2(glow.Width, glow.Height) / 2f;
			spriteBatch.Draw(glow, Projectile.oldPos[0] - Main.screenPosition, null, Color.White, Projectile.oldRot[0], origin, Projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);

			Texture2D texture = TextureAssets.Projectile[Type].Value;
			origin = new Vector2(texture.Width, texture.Height) / 2f;
			Color color = Projectile.localAI[1] == 0 ? new Color(255, 110, 0, 150) : new Color(0, 255, 165, 150);
			for (int i = ProjectileID.Sets.TrailCacheLength[Type] - 1; i >= 0; i--)
			{
				spriteBatch.Draw(texture, Projectile.oldPos[i] - Main.screenPosition, null, color * (float)Math.Pow(0.6f, i),
					Projectile.oldRot[i], origin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
