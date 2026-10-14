using Microsoft.Xna.Framework;
using Terraria;
using CalamityMod.Projectiles;
using CalamityMod;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles;

public class XerocFireV104 : ModProjectile
{
	public override void SetStaticDefaults()
	{
			CSCompat.SeedDisplayName(this, "XerocFire");
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 20;
		
	}

	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 12;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		if (Projectile.velocity.X != Projectile.velocity.X)
		{
			Projectile.velocity.X = Projectile.velocity.X * -0.1f;
		}
		if (Projectile.velocity.X != Projectile.velocity.X)
		{
			Projectile.velocity.X = Projectile.velocity.X * -0.5f;
		}
		if (Projectile.velocity.Y != Projectile.velocity.Y && Projectile.velocity.Y > 1f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y * -0.5f;
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 5f)
		{
			Projectile.ai[0] = 5f;
			if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
			{
				Projectile.velocity.X *= 0.97f;
				if (Projectile.velocity.X > -0.01f && Projectile.velocity.X < 0.01f)
				{
					Projectile.velocity.X = 0f;
					Projectile.netUpdate = true;
				}
			}
			Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
		}
		Projectile.rotation += Projectile.velocity.X * 0.1f;
		if (Utils.NextBool(Main.rand, 3))
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 0.75f);
			Dust obj = Main.dust[num];
			obj.position.X -= 2f;
			obj.position.Y += 2f;
			obj.scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			obj.noGravity = true;
			obj.velocity *= 0.1f;
		}
		else
		{
			int num2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 0.75f);
			Dust obj2 = Main.dust[num2];
			obj2.position.X -= 2f;
			obj2.position.Y += 2f;
			obj2.scale += (float)Main.rand.Next(50) * 0.01f;
			obj2.noGravity = true;
			obj2.velocity.Y -= 2f;
		}
		if (Projectile.velocity.Y < 0.25f && Projectile.velocity.Y > 0.15f)
		{
			Projectile.velocity.X *= 0.8f;
		}
		Projectile.rotation = (0f - Projectile.velocity.X) * 0.05f;
		if (Projectile.velocity.Y > 16f)
		{
			Projectile.velocity.Y = 16f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.penetrate == 0)
		{
			Projectile.Kill();
		}
		return false;
	}
}
