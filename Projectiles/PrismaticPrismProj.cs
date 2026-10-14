using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Projectiles
{
    public class PrismaticPrismProj : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("PrismaticPrismProj.DisplayName", "光棱棱镜");
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 900;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.CS().pPrism)
            {
                Projectile.Kill();
                return;
            }
            Projectile.rotation += Main.rand.Next(3) / 10f;
            Projectile.timeLeft = 900;
            Projectile.Center = player.Center + MathHelper.ToRadians(Projectile.ai[1]*6f).ToRotationVector2() * 320f;
            Projectile.ai[1] += 1f;
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 86);
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 89);
            dust.velocity *= 0f;
            dust.noGravity = true;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile hosProj = Main.projectile[i];
                if (hosProj.active && hosProj.hostile && !hosProj.friendly && hosProj.Hitbox.Distance(Projectile.Center) < 30f)
                {
                    hosProj.velocity = hosProj.DirectionFrom(player.Center) * hosProj.velocity.Length();
                }
            }
        }
        public override bool? CanDamage() => false;
    }
}