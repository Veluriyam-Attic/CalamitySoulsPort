using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod.Dusts;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Projectiles
{
    public class AstralRay : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("AstralRay.DisplayName", "幻星射线");
        public override string Texture => CSUtils.BlankTexture;
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 600;
            Projectile.DamageType=DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
            Projectile.soundDelay = 2;
        }
        public override void AI()
        {
            if (Projectile.soundDelay > 0)
            {
                int rand = Main.rand.Next(360);
                for (int i = 0; i < 12; i++)
                {
                    Vector2 rot = MathHelper.ToRadians(360 * i / 12 + rand).ToRotationVector2();
                    int randomDust = Utils.SelectRandom(Main.rand, new int[]
                    {
                        DustType<AstralOrange>(),
                        DustType<AstralBlue>()
                    });
                    Dust astral = Dust.NewDustPerfect(Projectile.Center, randomDust);
                    astral.velocity = rot;
                    astral.scale *=2f;
                    astral.noGravity = true;
                    astral = Dust.NewDustPerfect(Projectile.Center, randomDust);
                    astral.velocity = rot /2f;
                    astral.scale *= 3f;
                    astral.noGravity = true;
                }
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] % 40 == 39)
            {
                for (int i = 0; i < 24; i++)
                {
                    Vector2 rot = MathHelper.ToRadians(360 * i / 24).ToRotationVector2();
                    rot.X /= 2;
                   rot= rot.RotatedBy(Projectile.velocity.ToRotation());
                    int randomDust = Utils.SelectRandom(Main.rand, new int[]
                    {
                        DustType<AstralOrange>(),
                        DustType<AstralBlue>()
                    });
                    Dust astral = Dust.NewDustPerfect(Projectile.Center, randomDust);
                    astral.velocity = rot;
                    astral.scale *= 1.5f;
                    astral.noGravity = true;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                Dust astral = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustType<AstralOrange>());
                astral.noGravity = true;
                astral.noLight = true;
                astral.velocity *= 0.05f;
                astral = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustType<AstralBlue>());
                astral.noGravity = true;
                astral.noLight = true;
                astral.velocity *= 0.05f;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit43, Projectile.Center);
            int rand = Main.rand.Next(360);
            for (int i = 0; i < 12; i++)
            {
                Vector2 rot = MathHelper.ToRadians(360 * i / 12 + rand).ToRotationVector2();
                int randomDust = Utils.SelectRandom(Main.rand, new int[]
                {
                        DustType<AstralOrange>(),
                        DustType<AstralBlue>()
                });
                Dust astral = Dust.NewDustPerfect(Projectile.Center, randomDust);
                astral.velocity = rot;
                astral.scale *= 2f;
                astral.noGravity = true;
                astral = Dust.NewDustPerfect(Projectile.Center, randomDust);
                astral.velocity = rot / 2f;
                astral.scale *= 3f;
                astral.noGravity = true;
            }
        }
    }
}