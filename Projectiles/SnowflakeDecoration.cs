using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamitySouls.Projectiles
{
    public class SnowflakeDecoration : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 22;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.94f;
            Projectile.alpha += 2;
            Projectile.scale += 0.01f;
        }
    }
}