// 幻星魔石（EnchAstral）配套弹幕 · 星界卫星 AstralSatelliteMinionV104
// 照搬：ext1040/Projectiles/AstralSatelliteMinion.cs（本模组自己的弹幕）
// 1.4 处置：SetupProjectileTrailling(-1,100)→静态集合 ✓ CirculateOldpos 省略 ✓ Kill→OnKill ✓ CanDamage→bool? ✓
//           PreDraw(ref Color) ✓ · spriteBatch.Reset(Immediate, Additive, null) → End()/Begin(...) 成对 ✓
//           Main.projectileTexture→TextureAssets ✓ DirectionToSafe→CSPlayerV104Effects ✓ RandomRotate→Main.rand.NextVector2Unit ✓
//           音效 ScornHurt：已核实 2.x 存在（Sounds/NPCHit/ScornHurt.ogg 8943B ✓）⇒ 用灾厄路径 ✓（音量 ×0.8 按甲省略）
// 差异表：① 1.3 用 CSTexture.GlowTexture 画辉光 ✗ 本模组 V104 无该内置图 ⇒ 暂用卫星自身贴图代替（视觉近似）
//         ② 1.3 PreDraw 里 PrimitiveTrail + OverpoweredTouhouSpearShader 整段被注释掉 ⇒ 不移植 ✓
//         ③ 文件尾的 VertexInfo2 结构只服务那段注释代码 ⇒ 不移植 ✓
// 贴图：v1.0.4.0 包 Projectiles/AstralSatelliteMinion.rawimg → Content/V104/Projectiles/AstralSatelliteMinionV104.png
using System;
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
	public class AstralSatelliteMinionV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[Type] = -1;
			ProjectileID.Sets.TrailCacheLength[Type] = 100;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 68;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 180;
		}
		public override bool? CanDamage() => false;
		public int TargetIndex
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (!player.active || player.dead || !player.V104().AstralSatellite)
			{
				Projectile.Kill();
				return;
			}
			Projectile.timeLeft = 2;

			Projectile.velocity = Vector2.Lerp(Projectile.velocity, CSPlayerV104Effects.DirectionToSafe(Projectile, player.Center) * 15f,
				0.03f + 0.02f * Math.Sign(Vector2.Dot(Projectile.velocity, CSPlayerV104Effects.DirectionToSafe(Projectile, player.Center))));
			Projectile.velocity = Projectile.velocity.RotatedByRandom(0.05f);
			Projectile.rotation += MathHelper.Clamp(Projectile.velocity.X / 60f, -0.2f, 0.2f);

			Projectile.ai[0]--;
			int healAmount = (int)(50 - Projectile.ai[0] / 2f);
			if (healAmount > 50) healAmount = 50;
			if (healAmount > 0 && player.statLifeMax2 - player.statLife > healAmount / 2)
			{
				if (player.Hitbox.Distance(Projectile.Center) < 50f)
				{
					player.HealLife(healAmount);
					for (int i = 0; i < 36; i++)
					{
						Vector2 dir = MathHelper.ToRadians(i * 10f).ToRotationVector2();
						Dust dust = Dust.NewDustPerfect(Projectile.Center + dir * 34f, ModContent.DustType<CalamityMod.Dusts.AstralBasic>());
						dust.noGravity = true;
						dust.velocity = -dir;
						dust.scale *= 1.5f;
					}
					for (int j = 0; j < 25; j++)
					{
						Dust dust = Dust.NewDustPerfect(player.Center + Vector2.UnitY * (player.height / 2f + 8f), DustID.AncientLight);
						dust.noGravity = true;
						dust.scale *= 2f;
						dust.velocity = Main.rand.NextVector2Unit() * 4f;
						dust.velocity.Y /= 2;
						dust.color = Color.Cyan;
					}
					for (int i = 0; i < 10; i++)
					{
						for (int j = -1; j <= 1; j += 2)
						{
							Dust dust = Dust.NewDustPerfect(player.Center + new Vector2(player.width / 2f * j, player.height / 2f + 8f), DustID.AncientLight);
							dust.noGravity = true;
							dust.scale *= 1.2f + i / 7f;
							dust.velocity = new Vector2(0.3f * j, -1) * i;
							dust.color = Color.OrangeRed;
						}
					}
					// 1.3: ...ScornHurt").Volume *= 0.8f（灾厄音 ⇒ 音量按（甲）省略，见差异表）
					SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/NPCHit/ScornHurt"), Projectile.Center);
					Projectile.ai[0] = 500f;
				}
			}
		}
		private Color AstralColor
		{
			get
			{
				float la0 = Projectile.localAI[0];
				la0 /= 50f;
				if (la0 > 1) la0 = 2 - la0;
				return Color.Lerp(new Color(255, 110, 0, 200), new Color(0, 255, 165, 200), la0);
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.localAI[0] = (Projectile.localAI[0] + 1) % 100;
			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D texture = TextureAssets.Projectile[Type].Value;
			// 1.3: Texture2D glow = CSTexture.GlowTexture（本模组内置辉光图）⇒ V104 无该图，暂用自身贴图代替（差异表已记）
			Texture2D glow = texture;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(glow, Projectile.oldPos[4] - Main.screenPosition, null, AstralColor * 0.9f, Projectile.rotation,
				new Vector2(glow.Width, glow.Height) / 2f, Projectile.scale * 0.8f, SpriteEffects.None, 0f);
			spriteBatch.Draw(glow, Projectile.Center - Main.screenPosition, null, AstralColor, Projectile.rotation,
				new Vector2(glow.Width, glow.Height) / 2f, Projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation,
				new Vector2(texture.Width, texture.Height) / 2f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/NPCHit/ScornHurt"), Projectile.Center);
		}
	}
}
