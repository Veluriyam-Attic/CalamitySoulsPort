using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using static System.Math;
using CalamityMod;
using CalamityMod.Projectiles.Magic;

namespace CalamitySouls.Projectiles
{
    public class AnnihilationOption : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 900;
            Projectile.penetrate = 1;
            Projectile.soundDelay = 32;
        }
        public override void AI()
        {
            if (Projectile.soundDelay == 31)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                SpawnDust();
            }
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.CS().AnnihilationOption)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;
            bool flag = player.velocity.Length() > 7.5f;
            float pDist = flag ? 80f : 36f;
            Vector2 pos = player.Center + MathHelper.ToRadians(Projectile.ai[1]).ToRotationVector2() * pDist;
            Projectile.FollowPlayer(pos, 800f, 1f, player.velocity, 3f, true, 0f, 0f, 0.5f);
            if (Projectile.ai[0] != 0)
            {
                Projectile.ai[0]--;
                Projectile.alpha = 200;
                return;
            }
            Lighting.AddLight(Projectile.Center, 1f, 0f, 0f);
            Projectile.rotation += MathHelper.ToRadians(12);
            float dist = 5000f;
            int index = Projectile.ClosetNPC(dist);
            if (index != -1 && Projectile.soundDelay == 0)
            {
                NPC target = Main.npc[index];
                if (player.whoAmI == Main.myPlayer)
                {
                    int drainID = ProjectileType<VisceraBeam>();
                    Projectile drain = Projectile.NewProjectileDirect(null, pos, target.DirectionFrom(pos) * 16f, drainID, 10000, 0f, player.whoAmI);
                    drain.usesLocalNPCImmunity = true;
                    drain.localNPCHitCooldown = 60;
                    drain.penetrate = -1;
                    drain.extraUpdates = 20;
                    drain.GetPlayerDamageType(player);
                }
                Projectile.soundDelay = 20;
            }
            Projectile.alpha = 0;
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 183);
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = false;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile hosProj = Main.projectile[i];
                if (hosProj.active && hosProj.hostile && !hosProj.friendly && hosProj.Hitbox.Distance(Projectile.Center) < 15f)
                {
                    hosProj.Kill();
                    SpawnDust();
                    Projectile.ai[0] = 10 * 60;
                    break;
                }
            }
        }
        private void SpawnDust()
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 183);
                dust.velocity *= 3f;
                dust.scale *= 2f;
                dust.noLight = false;
                if (Utils.NextBool(Main.rand, 2))
                {
                    dust.scale = 0.5f;
                    dust.fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 183);
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust.scale *= 3f;
                dust.noLight = false;
                dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 183);
                dust.velocity *= 2f;
                dust.scale *= 2f;
                dust.noLight = false;
            }
        }
        public override bool? CanDamage() => false;
        public override void Kill(int timeLeft)
        {
            SpawnDust();
        }
    }
}