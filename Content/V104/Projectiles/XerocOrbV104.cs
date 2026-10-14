using Microsoft.Xna.Framework;
using Terraria;
using CalamityMod.Projectiles;
using CalamityMod;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles;

public class XerocOrbV104 : ModProjectile
{
	public override string Texture => "CalamitySouls/Content/V104/Projectiles/XerocHealOrbV104";

	public override void SetStaticDefaults()
	{
			CSCompat.SeedDisplayName(this, "XerocOrb");
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 20;
		
	}

	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 200;
	}

	public override void AI()
	{
		int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 2f);
		Main.dust[num].noGravity = true;
		Dust obj = Main.dust[num];
		obj.velocity *= 0f;
		CalamityUtils.HomeInOnNPC(Projectile, ignoreTiles: false, 400f, 7f, 20f);
	}
}
