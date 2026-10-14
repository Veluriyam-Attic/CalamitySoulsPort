using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod;
using CalamitySouls.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Projectiles
{
    public class FrostPaintball : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("FrostPaintball.DisplayName", "霜冻漆弹");
        public override string Texture => CSUtils.BlankTexture;
        public override void SetDefaults()
        {
            Projectile.DamageType=DamageClass.Ranged;
            Projectile.width = Projectile.height = 8;
            Projectile.extraUpdates = 3;
            Projectile.soundDelay = 2;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            if (Projectile.soundDelay > 0)
            {
                Projectile.soundDelay = 0;
                Projectile.ai[0] = Main.rand.Next(2);
                Projectile.penetrate = (int)Projectile.ai[0] + 1;
                for (int i = 0; i < 10; i++)
                {
                    Dust dust1 = Dust.NewDustPerfect(Projectile.Center, Projectile.ai[0] == 0 ? 76 : 92);
                    dust1.noGravity = true;
                    dust1.noLight = true;
                    dust1.velocity = Vector2.Normalize(Projectile.velocity) * 2.5f + CSUtils.RandomRotate * 2f;
                }
            }
            if (Projectile.velocity.Y < 20f) Projectile.velocity.Y += 0.05f;
            Dust dust = Dust.NewDustPerfect(Projectile.Center, Projectile.ai[0] == 0 ? 76 : 92);
            dust.noGravity = true;
            dust.noLight = true;
            dust.velocity = Vector2.Normalize(Projectile.velocity) * 2f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int dmgDone = damageDone;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;
            target.AddBuff(BuffID.Frostburn, Projectile.ai[0] == 0 ? 120 : 300);
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            for(int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, Projectile.ai[0] == 0 ? 76 : 92);
                dust.noGravity = true;
                dust.noLight = true;
                dust.velocity = CSUtils.RandomRotate * Main.rand.Next(30, 51) / 10f;
            }
        }
    }
    public class IcicleDaggerProj : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("IcicleDaggerProj.DisplayName", "冰刺投刀");
        public override string Texture => "CalamitySouls/Items/Weapons/IcicleDagger";
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 22;
            Projectile.DamageType=ModContent.GetInstance<RogueDamageClass>();
            Projectile.penetrate = 2;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            if (Projectile.velocity.Y < 20f) Projectile.velocity.Y += 0.15f;
            if (Projectile.timeLeft < 100) Projectile.rotation += 0.1f;
            else Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 92);
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int dmgDone = damageDone;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;
            target.AddBuff(BuffID.Frostburn,  120);
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
            for(int i = 0; i < 12; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 92);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(Projectile.velocity) * 5f + CSUtils.RandomRotate * 2f;
            }
        }
    }
}
