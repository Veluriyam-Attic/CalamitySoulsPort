using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod;

namespace CalamitySouls.Projectiles
{
   public class PeerlessWindGodProj:ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("PeerlessWindGodProj.DisplayName", "无双风神");
        public override string Texture => CSUtils.BlankTexture;
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 2;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override bool? CanDamage() => false;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || !player.channel || player.dead || player.CCed)
            {
                Projectile.Kill();
                return;
            }
            if (player.whoAmI == Main.myPlayer)
            {
                Projectile.velocity = player.DirectionTo(Main.MouseWorld);
                Projectile.netUpdate = true;
            }
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            Projectile.timeLeft = 2;
            Projectile.Center = player.Center;
            Projectile.ai[0]++;
            Projectile.ai[1]++;
            int amt = 5;
            if (Projectile.ai[1] < 60) amt += 5;
            if (Projectile.ai[1] < 120) amt += 5;
            if (Projectile.ai[0] % amt < 1) 
            {
                SoundEngine.PlaySound(SoundID.Item71, Projectile.Center);
                if (Projectile.owner == Main.myPlayer) 
                {
                    Vector2 dir = Projectile.DirectionTo(Main.MouseWorld).RotatedByRandom(MathHelper.ToRadians(15));
                    Vector2 rand = new Vector2(Main.rand.Next(-250, 251), Main.rand.Next(-50, 51));
                    Projectile.NewProjectile(null, Projectile.Center + rand, dir * Main.rand.Next(200, 251) / 10f,
                        ModContent.ProjectileType<PeerlessWind>(), (int)(120f * player.MeleeDamage()), Projectile.knockBack, Projectile.owner);
                }
            }
        }
    }
}
