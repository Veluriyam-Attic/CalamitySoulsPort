using System;
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

public class MeldGreatswordSmallProjectileV104 : ModProjectile
{
	public override void SetStaticDefaults()
	{
			CSCompat.SeedDisplayName(this, "MeldGreatswordSmallProjectile");
		
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
	}

	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.DamageType = DamageClass.Melee;   // 1.3: Projectile.melee
		Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		Projectile.rotation = Utils.ToRotation(Projectile.velocity);
		Vector2 vector = new Vector2(6f, 12f);
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] == 48f)
		{
			Projectile.localAI[0] = 0f;
		}
		else
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 vector2 = Vector2.UnitX * -15f;
				vector2 = -Utils.RotatedBy(Vector2.UnitY, (double)(Projectile.localAI[0] * ((float)Math.PI / 24f) + (float)i * (float)Math.PI), default(Vector2)) * vector;
				int num = Dust.NewDust(Projectile.Center, 0, 0, 27, 0f, 0f, 160, default(Color), 1f);
				Main.dust[num].scale = 1f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = Projectile.Center + vector2;
				Main.dust[num].velocity = Projectile.velocity;
			}
		}
		int num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 27, 0f, 0f, 100, default(Color), 0.8f);
		Main.dust[num2].noGravity = true;
		Dust obj = Main.dust[num2];
		obj.velocity *= 0f;
		CalamityUtils.HomeInOnNPC(Projectile, ignoreTiles: false, 400f, 12f, 20f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		// ⚠️ 1.3: CalamityGlobalProjectile.DrawCenteredAndAfterimage(...) 手绘残影并 return false（不画本体）
		//    2.x: 该 helper 已迁到 CalamityUtils.DrawAfterimagesCentered，但签名不同（Arg3 类型不符）
		//    本移植按（甲）省略该残影、改为正常绘制本体（与 VictideSeastarV104 的既有处置一致）— 差异表已记
		return true;
		return false;
	}
}
