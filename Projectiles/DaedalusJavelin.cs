using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Summon;

namespace CalamitySouls.Projectiles
{
    public class DaedalusJavelin : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("DaedalusJavelin.DisplayName", "代达罗斯标枪");
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 50;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override bool? CanDamage() => false;
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle sourceRectangle = new Rectangle(0, 0, 152, 152);
            Vector2 origin = new Vector2(57, 95);
            Vector2 origin2 = new Vector2(25, 25);
            Color color = lightColor;
            for (int i = 0; i < 4; i++)
            {
                color *= 0.6f;
                Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + origin2 - Main.screenPosition, sourceRectangle, color, Projectile.oldRot[i],
                    origin, Projectile.scale / 1.25f, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, lightColor, Projectile.rotation,
                origin, Projectile.scale / 1.25f, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            Projectile crystal = Main.projectile[(int)Projectile.ai[0]];
            if (!crystal.active || crystal.owner != Projectile.owner || crystal.type != ModContent.ProjectileType<DaedalusTrueCrystal>() || crystal.ai[0] <= 0)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;
            Player player = Main.player[Projectile.owner];
            float SearchDist = 1200f;
            NPC target = null;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (npc.active && npc.CanBeChasedBy(Projectile) && npc.Distance(Projectile.Center) < SearchDist)
                {
                    if (CSUtils.CanHitNPC(Projectile, npc)) target = npc;
                }
            }
            if (target == null)
            {
                float MinDist = SearchDist;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && npc.CanBeChasedBy(Projectile) && npc.Distance(Projectile.Center) < MinDist)
                    {
                        if (CSUtils.CanHitNPC(Projectile, npc))
                        {
                            target = npc;
                            MinDist = npc.Distance(Projectile.Center);
                        }
                    }
                }
            }
            ref float ai1 = ref Projectile.ai[1];
            if (ai1 <= 30 && !Projectile.friendly)
            {
                ai1++;
                Projectile.rotation += 0.2f;
                Projectile.scale = MathHelper.Lerp(0, 1f, ai1 / 30f);
                Projectile.Center = crystal.Center + Vector2.Lerp(Vector2.Zero, -Vector2.UnitY * 64f, ai1 / 30f);
                if (ai1 == 30)
                {
                    Projectile.friendly = true;
                    ai1 = 0;
                }
            }
            else
            {
                Projectile.FollowPlayer(crystal.Center, 76f, 38f, crystal.velocity, 20f, true);
                ai1++;
                if (target != null && Projectile.owner == Main.myPlayer)
                {
                    Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.AngleTo(target.Center) + MathHelper.PiOver4, 0.1f);
                    if (ai1 % 20 == 0 && ai1 <= 60)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Vector2 pos = new Vector2(Main.rand.Next(-50, 51), Main.rand.Next(-50, 51)) + crystal.Center;
                            Vector2 dir = target.Center + target.velocity * 10f - pos; dir.SafeNormalize(Vector2.Zero);
                            Projectile YellowRocket = Projectile.NewProjectileDirect(null, pos, dir * 16f,
                                ModContent.ProjectileType<GalacticaComet>(), (int)(20f * player.AverageDamage()), 12f, Projectile.owner);
                            YellowRocket.AllClass(); YellowRocket.extraUpdates = 2;
                        }
                        for (int i = -9; i <= 9; i++)
                        {
                            Vector2 pos = new Vector2(Main.rand.Next(-50, 51), Main.rand.Next(-50, 51)) + crystal.Center;
                            Vector2 dir = target.Center + target.velocity * 10f - pos; dir.SafeNormalize(Vector2.Zero);
                            Vector2 rot = (MathHelper.ToRadians(i * 45 / 18f) + dir.ToRotation()).ToRotationVector2();
                            if (i == -6 || i == 2) i += 3;
                            Projectile BlueRocket = Projectile.NewProjectileDirect(null, crystal.Center + rot * 8f, rot * 16f,
                                ModContent.ProjectileType<CosmicViperConcussionMissile > (), (int)(20f * player.AverageDamage()), 12f, Projectile.owner);
                            BlueRocket.AllClass(); BlueRocket.extraUpdates = 2;
                        }
                    }
                    if (ai1 % 40 == 0 && ai1 > 60)
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            Vector2 pos = new Vector2(Main.rand.Next(-50, 51), Main.rand.Next(-50, 51)) + crystal.Center;
                            Vector2 dir = target.Center + target.velocity * 10f - pos; dir.SafeNormalize(Vector2.Zero);
                            Vector2 rot = (MathHelper.ToRadians(i * 5 / 5f) + dir.ToRotation()).ToRotationVector2();
                            if (i == 0) i += 1;
                            Projectile BlueRocket = Projectile.NewProjectileDirect(null, crystal.Center + rot * 8f, rot * 8f, ProjectileType<CSPlaceholder>() , (int)(50f * player.AverageDamage()), 12f, Projectile.owner);
                            BlueRocket.AllClass(); BlueRocket.extraUpdates = 1;
                        }
                        if (ai1 >= 120) ai1 = 0;
                    }
                }
                else
                {
                    float rotdir = System.Math.Sign(Projectile.velocity.X);
                    Projectile.rotation = MathHelper.Lerp(Projectile.rotation, 0.3f * rotdir - MathHelper.PiOver4, 0.1f);
                }
            }
        }
    }
}