using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace CalamitySouls.Projectiles
{
    public class ImpurityLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 22;
            Projectile.DamageType=DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.soundDelay = 2;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 2;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 120;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 20) Projectile.tileCollide = true;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 86);
                dust.noGravity = true;
                dust.noLight = false;
                dust.velocity = Vector2.Normalize(Projectile.velocity) * 3f;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item30, Projectile.Center);
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 86);
                dust.noGravity = true;
                dust.noLight = false;
                dust.velocity = Vector2.Normalize(Projectile.velocity) * Main.rand.Next(7) + CSUtils.RandomRotate;
                dust.scale *= 2f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int dmg = damageDone;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;
            target.AddBuff(BuffID.ShadowFlame, 5 * 60);
        }
    }
}