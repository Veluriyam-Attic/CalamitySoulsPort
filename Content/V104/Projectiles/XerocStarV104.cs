using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using CalamityMod.Projectiles;
using CalamityMod;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles;

public class XerocStarV104 : ModProjectile
{
	public override void SetStaticDefaults()
	{
			CSCompat.SeedDisplayName(this, "XerocStar");
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
		Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		Projectile.rotation += 0.25f;
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] > 4f)
		{
			for (int i = 0; i < 1; i++)
			{
				int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num].noGravity = true;
				Dust obj = Main.dust[num];
				obj.velocity *= 0f;
			}
		}
		CalamityUtils.HomeInOnNPC(Projectile, ignoreTiles: false, 400f, 10f, 20f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D = TextureAssets.Projectile[Type].Value;
		Main.spriteBatch.Draw(texture2D, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, Utils.Size(texture2D) / 2f, Projectile.scale, SpriteEffects.None, 0f);
		return false;
	}
}
