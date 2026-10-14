using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Buffs.DamageOverTime;

namespace CalamitySouls.Projectiles
{
    public class SulphurousArea : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("SulphurousArea.DisplayName", "硫磺海域");
        public float Type
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public float Range
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public override string Texture => CSUtils.BlankTexture;
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
        }
        public override bool ShouldUpdatePosition()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 iCenter = player.Center;
            if (Type != 1 || player.whoAmI == Main.myPlayer)
            {
                Projectile.velocity = iCenter - Projectile.Center;
            }
            if (Type == 1 && player.whoAmI == Main.myPlayer)
            {
                iCenter = Main.MouseWorld;
                Projectile.netImportant = true;
                Projectile.velocity = iCenter - Projectile.Center;
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.CS().SulphurousCircle)
            {
                Projectile.active = false;
                return;
            }
            Projectile.timeLeft++;
            Range = 36;
            if (Type == 1) Range = 16;            
            if (Projectile.soundDelay == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position - new Vector2(Range / 2), (int)Range, (int)Range, DustType<SulphurousDust>());
                if (Type == 1) dust.scale *= 0.8f;                
                Projectile.soundDelay = Main.rand.Next(10, 26);
            }
            if (Main.rand.Next(3) == 0)
            {
                Dust sparkle = Dust.NewDustPerfect(Projectile.position + CSUtils.RandomRotate * Range, 75);
                sparkle.noGravity = true;
                sparkle.velocity = Vector2.Zero;
                sparkle.scale = 0.8f;
            }
            int bufftime = 2 * 60;
            if (Type == 1) bufftime = 2;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.Hitbox.Distance(Projectile.Center) <= Range)
                {
                    npc.ForceThrowABuffOnYourFuckingFace(BuffType<SulphuricPoisoning>(), bufftime);
                }
            }
        }
    }
    public class SulphurousDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = 1.2f;
        }
        public override bool Update(Dust dust)
        {
            dust.scale -= 0.05f;
            if (dust.scale <= 0) dust.active = false;
            return false;
        }
    }
}