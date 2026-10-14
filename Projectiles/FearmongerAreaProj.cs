using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Systems.Collections;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using CalamitySouls.Buffs;

namespace CalamitySouls.Projectiles
{
    public class FearmongerAreaProj : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("FearmongerAreaProj.DisplayName", "神惧者领域");
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 500;
            Projectile.timeLeft = 5 * 60;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 155;
        }
        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.scale = 2f;
            int rand = Main.rand.Next(360);
            for (int i = 0; i < 36; i++)
            {
                Vector2 rot = MathHelper.ToRadians(360 * i / 36f + rand).ToRotationVector2() * Main.rand.Next(20, 500);
                Dust dust = Dust.NewDustPerfect(Projectile.Center + rot, 92);
                dust.noGravity = true; dust.velocity = Vector2.Zero; dust.scale *= 0.4f;
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.Hitbox.Distance(Projectile.Center) < 500f) proj.CS().FearmongerFrozen = 2;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.Distance(Projectile.Center) < 500f) npc.CS().FearmongerFrozen = true;
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player1 = Main.player[i];
                if (player1.active && !player1.dead && player1.Distance(Projectile.Center) < 500) player1.AddBuff(BuffType<FearmongerArea>(), 2);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && ((projectile.hostile && !projectile.friendly) || projectile.type == 0)
                    && projectile.Hitbox.Distance(projectile.Center) < 560f
                    && !CalamityProjectileSets.ShouldNotBeReflected[projectile.type])
                {
                    Projectile.NewProjectile(null, projectile.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f, 255);
                    projectile.Kill();
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            return true;
        }
        public override void PostDraw(Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
        }
    }
}