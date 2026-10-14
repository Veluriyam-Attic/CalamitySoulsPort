using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using CalamityMod;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Projectiles
{
    public class PeerlessWind : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;            
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;            
        }
        public override void SetDefaults()
        {
            Projectile.width = 112;
            Projectile.height = 14;
            Projectile.DamageType=DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.timeLeft = 60;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Projectile.alpha = 0;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0f);
            if (Main.rand.Next(30) == 0)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    int amt = Main.rand.Next(2, 5);
                    for (int i = 0; i < amt; i++)
                        Projectile.NewProjectile(null, Projectile.Center + CSUtils.RandomRotate * Main.rand.Next(0, 32), CSUtils.RandomRotate * 5f,
                            ModContent.ProjectileType<PeerlessBullet>(), Projectile.damage / 2, 0f, Projectile.owner);
                }
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = -3; i < 5; i++) if (targetHitbox.Distance(Projectile.Center + Vector2.Normalize(Projectile.velocity) * 14 * i) < 7f) return true;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            var csTex = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(csTex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, csTex.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}