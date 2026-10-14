using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Projectiles
{
	public class EleumCrystalShard : ModProjectile
	{
		public override string Texture => CSUtils.BlankTexture;
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 12;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 120;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}
		public override void AI()
		{
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 5f)
			{
				Projectile.ai[0] = 5f;
				if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.97f;
					if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
					{
						Projectile.velocity.X = 0f;
						Projectile.netUpdate = true;
					}
				}
				Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			}
			int CrystalDust = Projectile.ai[1] == 0 ? 206 : 255;
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, CrystalDust, 0f, 0f, 100, default, 0.5f);
			dust.position.X -= 2f;
			dust.position.Y += 2f;
			dust.scale += Main.rand.Next(50) * 0.01f;
			dust.noGravity = true;
			dust.velocity.Y -= 2f;
			if (Utils.NextBool(Main.rand, 2))
			{
				Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, CrystalDust, 0f, 0f, 100, default, 0.5f);
				dust2.position.X -= 2f;
				dust2.position.Y += 2f;
				dust2.scale += 0.3f + Main.rand.Next(50) * 0.01f;
				dust2.noGravity = true;
				dust2.velocity *= 0.1f;
			}
			if (Projectile.velocity.Y < 0.25 && Projectile.velocity.Y > 0.15) Projectile.velocity.X = Projectile.velocity.X * 0.8f;
			if (Projectile.velocity.Y > 16f) Projectile.velocity.Y = 16f;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int dmg = damageDone;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;
			target.AddBuff(Terraria.ID.BuffID.Frostburn, 60);
        }
        public override bool OnTileCollide(Vector2 oldVelocity) => false;
	}
}