using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using CalamityMod.Systems.Collections;

namespace CalamitySouls.Projectiles
{
    public class AuricFlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override string Texture => CSUtils.BlankTexture;
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
        }
        public float RangeCounter
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public float Range
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public const float HitBoxRange = 16f;
        public override void AI()
        {
            if (RangeCounter < HitBoxRange) RangeCounter += 0.5f;
            Range+= RangeCounter;
            int rand = Main.rand.Next(360);
            for (int i = 0; i < 360; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.position + MathHelper.ToRadians(i + rand).ToRotationVector2() * Range, DustID.GoldFlame);
                dust.noGravity = true;
                dust.scale = 0.8f;
                dust.velocity = Vector2.Zero;
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && ((proj.hostile && !proj.friendly) || proj.type == 0)
                    && proj.Hitbox.Distance(proj.position) < Range
                    //&& proj.Hitbox.Distance(proj.position) > Range - HitBoxRange
                    && !CalamityProjectileSets.ShouldNotBeReflected[proj.type])
                {
                    Projectile.NewProjectile(null, proj.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f, 255);
                    proj.Kill();
                }
            }
            if (Range > 1920f)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Zombie103, Projectile.Center);
        }
    }
}