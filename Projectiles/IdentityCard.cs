using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using CalamityMod;
using CalamitySouls.Buffs;

namespace CalamitySouls.Projectiles
{
    public class IdentityCard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 18;
            Projectile.minion = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.minionSlots = 0.5f;
        }
        public override void AI()
        {
            Projectile.alpha = 0;
            Lighting.AddLight(Projectile.Center, 0.3f, 0f, 0f);
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                player.CS().RotI = false;
                Projectile.Kill();
                return;
            }
            bool flag = Projectile.type == ModContent.ProjectileType<IdentityCard>();
            player.AddBuff(ModContent.BuffType<IdentityMinionBuff>(), 3600, true);
            if (flag && player.CS().RotI) Projectile.timeLeft = 2;
            Projectile.ai[0] += MathHelper.ToRadians(4);
            Projectile.Center = player.Center + Projectile.ai[0].ToRotationVector2() * 80f;
            Projectile.velocity = player.DirectionTo(Projectile.Center).RotatedBy(MathHelper.PiOver2);
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.ai[1]++;
            //if (projectile.ai[1] % 60 < 15 && projectile.ai[1] % 5 < 1) 
            if (Projectile.ai[1] % 60 < 1) 
            {
                //SoundEngine.PlaySound(SoundID.Item26, projectile.Center);
                for (int i = -1; i <= 1; i += 2)
                {
                    Projectile.NewProjectile(null, Projectile.Center, player.DirectionTo(Projectile.Center) * 12f,
                        ModContent.ProjectileType<IdentityHeart>(), (int)(60f * player.MinionDamage()), Projectile.knockBack, player.whoAmI, i);
                }
            }
        }
        public override bool? CanDamage() => false;
    }
}