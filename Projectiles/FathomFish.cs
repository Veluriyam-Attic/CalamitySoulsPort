using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using CalamityMod.Systems.Collections;

namespace CalamitySouls.Projectiles
{
    public class FathomFish : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = texture.Size() / 2f;
            if (Projectile.alpha == 0)
            {
                Texture2D gTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/FathomFishGlow").Value;
                Color color = Projectile.GetAlpha(lightColor);
                float scale = Projectile.scale;
                for (int i = 0; i < 4; i++)
                {
                    scale *= 0.9f;
                    color *= 0.6f; color.B = (byte)(color.B / 0.6f); color.G = (byte)(color.G / 0.7f);
                    Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + origin - Main.screenPosition, null, color, Projectile.oldRot[i],
                        origin, scale, SpriteEffects.None, 0f);
                }
                Main.spriteBatch.Draw(gTex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation,
                    gTex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation,
                origin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 22;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead || !player.active || !player.CS().FathomFish)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;
            Lighting.AddLight(Projectile.Center, 0f, 0.15f, 0.4f);
            int randomDust = Utils.SelectRandom(Main.rand, new int[] { 89, 33, 33, 33 });
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, randomDust);
            if (randomDust == 89) dust.scale *= 0.5f;
            dust.noGravity = true; dust.velocity *= 0f;
            Projectile.ai[1]++;
            Vector2 dir = MathHelper.ToRadians(Projectile.ai[1]*3f).ToRotationVector2();
            Projectile.Center = player.Center + dir * 240f;
            Projectile.velocity = Vector2.Zero;
            Projectile.rotation = dir.ToRotation() + MathHelper.PiOver2;
            if (Projectile.ai[0] > 0)
            {
                Projectile.ai[0]--;
                Projectile.alpha = 200;
                return;
            }
            Projectile.alpha = 0;
            Lighting.AddLight(Projectile.Center, 0, 1f, 1f);
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile hostileProj = Main.projectile[i];
                if (hostileProj.active && hostileProj.hostile && hostileProj.Hitbox.Contains(Projectile.Center.ToPoint()) 
                    && !CalamityProjectileSets.ShouldNotBeReflected[hostileProj.type])
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath19, hostileProj.Center);
                    if (player.whoAmI == Main.myPlayer)
                    {
                        hostileProj.Kill();
                        Projectile.ai[0] = 60;
                        break;
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath19, Projectile.Center);
            for (int i = 0; i < 18; i++)
            {
                Vector2 rot = MathHelper.ToRadians(360 * i / 18f).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 89);
                dust.noGravity = true;
                dust.velocity = rot * 3f;
                dust.scale *= 2f;
            }
        }
        public override bool? CanDamage() => false;
    }
}