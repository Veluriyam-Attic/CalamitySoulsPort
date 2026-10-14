using System;
using Microsoft.Xna.Framework;
using Terraria;
using CalamityMod.Projectiles;
using CalamityMod;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles;

public class XerocBlastV104 : ModProjectile
{
	public override string Texture => CSUtils.BlankTexture;

	public override void SetStaticDefaults()
	{
			CSCompat.SeedDisplayName(this, "XerocBlast");
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 20;
		
	}

	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 5;
	}

	public override void AI()
	{
		Lighting.AddLight(Projectile.Center, (float)(255 - Projectile.alpha) * 0.35f / 255f, (float)(255 - Projectile.alpha) * 0.05f / 255f, (float)(255 - Projectile.alpha) * 0.25f / 255f);
		bool flag = false;
		bool flag2 = false;
		if (Projectile.velocity.X < 0f && Projectile.position.X < Projectile.ai[0])
		{
			flag = true;
		}
		if (Projectile.velocity.X > 0f && Projectile.position.X > Projectile.ai[0])
		{
			flag = true;
		}
		if (Projectile.velocity.Y < 0f && Projectile.position.Y < Projectile.ai[1])
		{
			flag2 = true;
		}
		if (Projectile.velocity.Y > 0f && Projectile.position.Y > Projectile.ai[1])
		{
			flag2 = true;
		}
		if (flag && flag2)
		{
			Projectile.Kill();
		}
		float num = 25f;
		if (Projectile.ai[0] > 180f)
		{
			num -= (Projectile.ai[0] - 180f) / 2f;
		}
		if (num <= 0f)
		{
			num = 0f;
			Projectile.Kill();
		}
		num *= 0.7f;
		Projectile.ai[0] += 4f;
		for (int i = 0; (float)i < num; i++)
		{
			float num2 = Main.rand.Next(-10, 11);
			float num3 = Main.rand.Next(-10, 11);
			float num4 = Main.rand.Next(3, 9);
			float num5 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
			num5 = num4 / num5;
			num2 *= num5;
			num3 *= num5;
			int num6 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 1f);
			Dust obj = Main.dust[num6];
			obj.noGravity = true;
			obj.position.X = Projectile.Center.X;
			obj.position.Y = Projectile.Center.Y;
			obj.position.X += Main.rand.Next(-10, 11);
			obj.position.Y += Main.rand.Next(-10, 11);
			obj.velocity.X = num2;
			obj.velocity.Y = num3;
		}
	}
}
