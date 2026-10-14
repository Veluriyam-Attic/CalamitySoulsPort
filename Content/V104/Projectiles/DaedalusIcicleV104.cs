using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using Microsoft.Xna.Framework;

namespace CalamitySouls.Content.V104.Projectiles
{
    public class DaedalusIcicleV104 : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("DaedalusIcicleV104.DisplayName", "代达罗斯冰锥");
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 14;
            Projectile.DamageType=ModContent.GetInstance<RogueDamageClass>();
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 1;
            Projectile.coldDamage = true;
            Projectile.friendly = true;
        }
        private Projectile Crystal
        {
            get => Main.projectile[(int)Projectile.ai[0]];
        }
        private float ai1
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public override void AI()
        {
            if (!Crystal.active || Crystal.type != ModContent.ProjectileType<DaedalusTrueCrystalV104>() || Crystal.owner != Projectile.owner)
            {
                if (Projectile.timeLeft > 540) Projectile.timeLeft = 540;
            }
            ai1 += 6;
            if (Projectile.timeLeft > 540)
            {
                Projectile.FollowPlayer(Crystal.Center + MathHelper.ToRadians(ai1).ToRotationVector2() * 64f, 800f, 0f, Crystal.velocity, 30f, true);
                Projectile.rotation = Projectile.AngleFrom(Crystal.Center);
            }
            else if (Projectile.timeLeft == 540)
            {
                Projectile.velocity = MathHelper.ToRadians(ai1).ToRotationVector2() * 20f;
                SoundEngine.PlaySound(SoundID.Item28, Projectile.Center);
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            else if (Projectile.timeLeft < 530)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                int targetIndex = Projectile.ClosetNPC(800f);
                if (targetIndex == -1)
                {
                    Projectile.velocity *= 0.97f;
                    Projectile.timeLeft--;
                    return;
                }
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.npc[targetIndex].Center) * 40f, 0.1f);
                Projectile.timeLeft++;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
        }
    }
}