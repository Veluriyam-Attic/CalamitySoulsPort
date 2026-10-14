using Microsoft.Xna.Framework;
using Terraria;
using CalamityMod.Projectiles;
using CalamityMod;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles;

public class XerocBubbleV104 : ModProjectile
{
	public override void SetStaticDefaults()
	{
			CSCompat.SeedDisplayName(this, "XerocBubble");
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 20;
		
	}

	public override void SetDefaults()
	{
		Projectile.width = 20;
		Projectile.height = 20;
		Projectile.alpha = 100;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 200;
	}

	public override void AI()
	{
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] > 4f)
		{
			for (int i = 0; i < 1; i++)
			{
				int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num].noGravity = true;
				Dust obj = Main.dust[num];
				obj.velocity *= 0f;
			}
		}
		CalamityUtils.HomeInOnNPC(Projectile, ignoreTiles: false, 400f, 8f, 20f);
	}
}
