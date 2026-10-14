using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod.Systems.Collections;

namespace CalamitySouls.Projectiles
{
    public class WulfrumShield : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("WulfrumShield.DisplayName", "钨钢护盾");
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 118;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 15 * 60;
            Projectile.soundDelay = 2;
            Projectile.rotation = -MathHelper.PiOver2;
            Projectile.frame = 9;
        }
        public override bool ShouldUpdatePosition()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            return false;
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 8 == 0)
            {
                Projectile.rotation += MathHelper.ToRadians(30f);
                Projectile.frame++;
            }
            if (Projectile.frame > 11) Projectile.frame = 0;
            if (Projectile.soundDelay > 0) SoundEngine.PlaySound(SoundID.Item15, Projectile.Center);
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.CS().HasWulfrumShield) 
            {
                Projectile.Kill();
                return;
            }
            player.CS().WulfrumShieldCooldown = 15 * 60;
            Projectile.Center = player.Center;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile bullet = Main.projectile[i];
                if (bullet.active && bullet.hostile && !bullet.friendly && bullet.tileCollide
                    && !CalamityProjectileSets.ShouldNotBeReflected[bullet.type])
                {
                    if (bullet.Hitbox.Distance(TruePosition) < 50f)
                    {
                        if (ProjectileLoader.OnTileCollide(bullet, bullet.oldVelocity)) bullet.Kill();
                        Projectile.Kill();
                        SoundEngine.PlaySound(SoundID.NPCDeath44, Projectile.Center);
                        player.SetImmunityTime(30);
                        return;
                    }
                }
            }
            if (Main.rand.Next(30) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard);
                dust.noGravity = true;
                dust.scale *= 2f;
                dust.velocity = Main.rand.Next(10, 41) / 10f * MathHelper.ToRadians(Main.rand.Next(360)).ToRotationVector2();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = -10; i < 11; i++)
            {
                Vector2 dir = Projectile.rotation.ToRotationVector2().RotatedBy(MathHelper.ToRadians(i * 3));
                Dust dust = Dust.NewDustPerfect(Projectile.Center + dir * 59f, DustID.Electric);
                dust.noLight = false;
                dust.scale *= 3f;
                dust.velocity.Length(1f);
            }
        }
        public override bool PreDraw(ref Color lightColor) => false;
        public override bool? CanDamage() => false;
        private Vector2 TruePosition => Projectile.Center + Projectile.rotation.ToRotationVector2() * 36f;
    }
}