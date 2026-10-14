using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using CalamitySouls.Buffs;
using CalamitySouls.Projectiles;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod;
using static Terraria.ModLoader.ModContent;
using static System.Math;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Projectiles
{
    public class SlimeGodMinion : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("SlimeGodMinion.DisplayName", "史莱姆之神仆从");
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public float CircleCounter
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public float Range
        {
            get => Projectile.ai[1] + 80f;
            set => Projectile.ai[1] = value - 80f;
        }
        public float HealAmount
        {
            get => Projectile.ai[1] / 4f;
            set => Projectile.ai[1] = value * 4f;
        }
        public int HealCooldown
        {
            get => Projectile.soundDelay;
            set => Projectile.soundDelay = value;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.CS().StatigelSgod)
            {
                Projectile.active = false;
                return;
            }
            if (HealCooldown <= 0)
            {
                int amt = player.statLifeMax2 - player.statLife;
                if (amt > 0) 
                {
                    if (amt > HealAmount) amt = (int)HealAmount;
                    HealAmount -= amt;
                    player.HealLife(amt);
                    HealCooldown = 60;
                    for(int i = 0; i < 16; i++)
                    {
                        Dust dust = Dust.NewDustPerfect(Projectile.Center, 21);
                        dust.noGravity = true;
                        dust.velocity = CSUtils.RandomRotate * 5f;
                        dust.scale *= 2f;
                        dust = Dust.NewDustPerfect(GetCrimPos(Projectile.Center), 231);
                        dust.noGravity = true;
                        dust.velocity = CSUtils.RandomRotate * 5f;
                        dust.scale *= 2f;
                    }
                }
            }
            if (HealAmount < 100)
            {
                Range += 0.1f;
            }
            Projectile.timeLeft = 2;
            CircleCounter -= MathHelper.ToRadians(3);
            Projectile.Center = player.Center + CircleCounter.ToRotationVector2() * Range;
            if (Projectile.Distance(player.Center) > 1600f) Projectile.Center = player.Center;
            Projectile.rotation += MathHelper.Clamp(player.velocity.X, -0.1f, 0.1f);
        }
        public override bool? CanDamage() => false;
        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/SGCorr").Value;
            Texture2D AiTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/SGCorr_Ai").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/SGCrim").Value;
            Texture2D AiTex2 = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/SGCrim_Ai").Value;
            Color color = lightColor;
            float scale = Projectile.scale;
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                color *= 0.6f;
                scale *= 0.9f;
                Main.spriteBatch.Draw(AiTex, Projectile.oldPos[i] + AiTex.Size() / 2f - Main.screenPosition, null, color, Projectile.rotation,
                  AiTex.Size() / 2f, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(AiTex2, GetCrimPos(Projectile.oldPos[i] + AiTex2.Size() / 2f) - Main.screenPosition, null, color, Projectile.rotation,
                  AiTex2.Size() / 2f, scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation,
                texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2, GetCrimPos(Projectile.Center) - Main.screenPosition, null, lightColor, Projectile.rotation,
                texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        private Vector2 GetCrimPos(Vector2 CorrPos)
        {
            Player player = Main.player[Projectile.owner];
            return player.Center + (player.Center - CorrPos);
        }
    }
}