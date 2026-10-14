using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「胜潮魔石（EnchVictide）」召唤的追击海星 —— 对应新版 1.3 的 CalamitySouls.Projectiles.VictideSeastar
	public class VictideSeastarV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "胜潮海星（新版）");
			// 1.3: projectile.SetupProjectileTrailling(2, 4) —— 拖尾模式 2、长度 4
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 4;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 22;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.scale = 0.8f;
			Projectile.extraUpdates = 2;
		}
		public override bool? CanDamage() => Projectile.frameCounter > 45;
		public override void AI()
		{
			if (Projectile.localAI[0] == 0f)
			{
				for (int i = 0; i < 20; i++)
				{
					Vector2 dir = CSUtils.RandomRotate;
					Dust shard = Dust.NewDustPerfect(Projectile.Center + dir * 2f, 120);
					shard.noGravity = true;
					shard.scale *= 0.9f;
					shard.velocity = dir * 4f;
				}
				Projectile.localAI[0] = 1f;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter < 45)
			{
				Projectile.velocity *= 0.97f;
				Projectile.rotation += Math.Sign(Projectile.velocity.X) * 0.1f;
			}
			else
			{
				if (Projectile.frameCounter < 90)
				{
					bool validTarget = false;
					float vdir = 0f;
					if (Projectile.ai[0] > 0 && Projectile.ai[0] <= Main.maxNPCs)
					{
						NPC npc = Main.npc[(int)Projectile.ai[0]];
						if (npc.active && !npc.friendly)
						{
							validTarget = true;
							vdir = Projectile.AngleTo(npc.Center);
						}
					}
					// 1.3 用 AngleToSafe（带零向量保护），1.4 直接用 AngleTo
					if (!validTarget && Projectile.owner == Main.myPlayer)
					{
						vdir = Projectile.AngleTo(Main.MouseWorld);
						Projectile.netUpdate = true;
					}
					Projectile.ai[1] = vdir;
				}
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.ai[1].ToRotationVector2() * 12f, 0.05f);
			}
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.FlatBonusDamage += target.defense / 2f;
			modifiers.DisableCrit();
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			hurtInfo.Damage += (int)(target.statDefense / 2f);
		}
		// 1.3 在 frameCounter > 45 时用 CalamityUtils.DrawAfterimagesCentered 画残影并 return false。
		// 2.x 该方法签名改过，这里改用等价的 1.4 原生残影写法（TrailCacheLength=4 / TrailingMode=2）。
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.frameCounter <= 45) return true;
			Texture2D texture = TextureAssets.Projectile[Type].Value;
			for (int i = 0; i < Projectile.oldPos.Length; i++)
			{
				if (Projectile.oldPos[i] == Vector2.Zero) continue;
				float alpha = (1f - i / (float)Projectile.oldPos.Length) * 0.5f;
				Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, drawPos, null, lightColor * alpha, Projectile.oldRot[i],
					texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
			}
			return false;
		}
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
			Vector2 vel = Projectile.velocity.SafeNormalize(Vector2.UnitX);
			for (int i = 0; i < 5; i++)
			{
				Vector2 dir = (MathHelper.ToRadians(i * 72) + Projectile.rotation).ToRotationVector2();
				Dust shard = Dust.NewDustPerfect(Projectile.Center + dir * 5f, DustID.Coralstone);
				shard.noGravity = false;
				shard.scale *= 1.2f;
				shard.velocity = dir - vel * 2f;
			}
			for (int i = 0; i < 10; i++)
			{
				Vector2 dir = CSUtils.RandomRotate;
				Dust shard = Dust.NewDustPerfect(Projectile.Center + dir * 2f, 120);
				shard.noGravity = true;
				shard.scale *= 1f;
				shard.velocity = dir * 2f + vel / 2f;
			}
		}
	}
}
