using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using CalamityMod;

namespace CalamitySouls.Projectiles
{
    public class PeerlessBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.DamageType=DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.975f;
            Projectile.velocity.Y += 0.3f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0f);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.AmberBolt);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(Projectile.velocity) * 3f;
                dust.scale = Main.rand.Next(10, 31) / 10f;
            }
        }
    }
}