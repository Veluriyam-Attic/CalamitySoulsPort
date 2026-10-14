using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace CalamitySouls.Projectiles
{
    public class ImpurityOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 22;
            Projectile.DamageType=DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.soundDelay = 2;
            Projectile.timeLeft = 114514;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (Projectile.soundDelay > 0)
            {
                Projectile.soundDelay = 0;
                Projectile.timeLeft = 104 + Main.rand.Next(24);
                Projectile.rotation = MathHelper.ToRadians(Main.rand.Next(360));
            }
            Projectile.rotation += 0.157f;
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 86);
                dust.noGravity = true;
                dust.noLight = false;
                dust.velocity = Vector2.Normalize(Projectile.velocity) * 3f;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);
            if (Projectile.owner == Main.myPlayer)
            {
                int index = -1;
                float minDist = 800f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC targetSelect = Main.npc[i];
                    if (targetSelect.active && !targetSelect.friendly && !targetSelect.dontTakeDamage && targetSelect.Distance(Main.player[Projectile.owner].Center) < minDist)
                    {
                        index = i;
                        minDist = targetSelect.Distance(Main.player[Projectile.owner].Center);
                    }
                }
                Vector2 iCenter = Main.MouseWorld;
                if (index != -1) iCenter = Main.npc[index].Center;
                Projectile.NewProjectile(null, Projectile.Center, Projectile.DirectionTo(iCenter) * 15f,
                    ModContent.ProjectileType<ImpurityLaser>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 86);
                dust.noGravity = true;
                dust.noLight = false;
                dust.velocity = CSUtils.RandomRotate * 3f;
                dust.scale *= 2f;
            }
        }
        public override bool? CanDamage() => false;
    }
}