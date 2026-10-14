using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using CalamityMod;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Projectiles
{
    public class IdentityHeart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 18;
            Projectile.minion = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 120;
            Projectile.alpha = 255;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            Projectile.alpha = 0;
            Lighting.AddLight(Projectile.Center, 0.3f, 0f, 0f);
            if (Projectile.ai[1] < 60)
            {
                Projectile.ai[1]++;
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(Projectile.ai[0] * (60 - Projectile.ai[1]) / 12f));
                Projectile.velocity *= 0.97f;
            }
            else
            {
                int index = -1;
                float minDist = 800f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC targetSelect = Main.npc[i];
                    if (targetSelect.active && !targetSelect.friendly && !targetSelect.dontTakeDamage && targetSelect.Distance(Projectile.Center) < minDist)
                    {
                        index = i;
                        minDist = targetSelect.Distance(Projectile.Center);
                    }
                }
                if (index != -1)
                {
                    Vector2 iCenter = Main.npc[index].Center;
                    Projectile.FollowPlayer(iCenter, 800f, 0f, Vector2.Zero, 25f, false, 0.06f);
                    Projectile.timeLeft++;
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
            Dust dust = Dust.NewDustPerfect(Projectile.Center, 73);
            dust.velocity = Vector2.Zero;
            dust.noGravity = true;
            dust.scale *= 0.8f;
        }
        public override bool? CanDamage() => Projectile.ai[1] >= 60;
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item28, Projectile.Center);
            for (int k = 0; k < 22; k++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 73);
                dust.velocity = CSUtils.RandomRotate * 2f;
                dust.noGravity = true;
                dust.scale *= 1f;
            }
        }
    }
}