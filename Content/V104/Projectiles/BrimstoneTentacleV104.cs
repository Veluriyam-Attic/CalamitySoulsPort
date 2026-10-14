using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using CalamityMod;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles;

public class BrimstoneTentacleV104 : ModProjectile
{
	public override string Texture => CSUtils.BlankTexture;

	public override void SetStaticDefaults()
	{
			CSCompat.SeedDisplayName(this, "BrimstoneTentacle");
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 20;   // 1.3 SetupProjectileTrailling 等价
		
	}

	public override void SetDefaults()
	{
		Projectile.width = 40;
		Projectile.height = 40;
		Projectile.friendly = true;
		Projectile.penetrate = 5;
		Projectile.MaxUpdates = 3;
		Projectile.DamageType = DamageClass.Magic;   // 1.3: projectile.magic = true
	}

	public override void AI()
	{
		Projectile.localAI[1] += 1f;
		if (Projectile.velocity.X != Projectile.velocity.X)
		{
			if (Math.Abs(Projectile.velocity.X) < 1f)
			{
				Projectile.velocity.X = 0f - Projectile.velocity.X;
			}
			else
			{
				Projectile.Kill();
			}
		}
		if (Projectile.velocity.Y != Projectile.velocity.Y)
		{
			if (Math.Abs(Projectile.velocity.Y) < 1f)
			{
				Projectile.velocity.Y = 0f - Projectile.velocity.Y;
			}
			else
			{
				Projectile.Kill();
			}
		}
		Vector2 center = Projectile.Center;
		Projectile.scale = 1f - Projectile.localAI[0];
		Projectile.width = (int)(20f * Projectile.scale);
		Projectile.height = Projectile.width;
		Projectile.position.X = center.X - (float)(Projectile.width / 2);
		Projectile.position.Y = center.Y - (float)(Projectile.height / 2);
		if (Projectile.localAI[0] < 0.1f)
		{
			Projectile.localAI[0] += 0.01f;
		}
		else
		{
			Projectile.localAI[0] += 0.025f;
		}
		if (Projectile.localAI[0] >= 0.95f)
		{
			Projectile.Kill();
		}
		Projectile.velocity.X = Projectile.velocity.X + Projectile.ai[0] * 1.5f;
		Projectile.velocity.Y = Projectile.velocity.Y + Projectile.ai[1] * 1.5f;
		if (Projectile.velocity.Length() > 16f)
		{
			Projectile.velocity.Normalize();
			((Entity)Projectile).velocity = ((Entity)Projectile).velocity * 16f;
		}
		Projectile.ai[0] *= 1.05f;
		Projectile.ai[1] *= 1.05f;
		if (Projectile.scale < 1f && Projectile.localAI[1] > 5f)
		{
			for (int i = 0; (float)i < Projectile.scale * 10f; i++)
			{
				int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 235, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.5f);
				Main.dust[num].position = (Main.dust[num].position + Projectile.Center) / 2f;
				Main.dust[num].noGravity = true;
				Dust obj = Main.dust[num];
				obj.velocity *= 0.1f;
				Dust obj2 = Main.dust[num];
				obj2.velocity -= Projectile.velocity * (1.3f - Projectile.scale);
				Main.dust[num].fadeIn = 100 + Projectile.owner;
				Dust obj3 = Main.dust[num];
				obj3.scale += Projectile.scale * 0.75f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 5;
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240, false);
	}
}
