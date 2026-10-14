using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Projectiles
{
    public class FriendlyCalamitas : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 50;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 1800;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead || !player.CS().EnchDemonshade)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft++;
            ref Vector2 velocity = ref Projectile.velocity;
            Vector2 playerCenterWithY = player.Center - Vector2.UnitY * 80f;
            if (Projectile.Distance(playerCenterWithY) < 1) velocity = Vector2.Lerp(velocity, player.velocity, 1f);
            else if (Projectile.Distance(playerCenterWithY) > 160) Projectile.Center = playerCenterWithY + Projectile.DirectionFrom(playerCenterWithY) * 160f;
            else velocity = Vector2.Lerp(velocity, Projectile.DirectionTo(playerCenterWithY) * 10f, 0.1f);
            Projectile.Center = Vector2.Lerp(Projectile.Center, playerCenterWithY, 0.15f);
            int NPCWhoAmI = -1;
            if (player.HasMinionAttackTargetNPC && Main.npc[player.MinionAttackTargetNPC].Distance(Projectile.Center) < 16 * 1000 * 10) 
            {
                NPCWhoAmI = player.MinionAttackTargetNPC ;
            }
            else
            {
                float MinDistance = 16 * 1000 * 10;
                foreach (NPC See in Main.npc)
                {
                    if (See.active && !See.friendly && !See.dontTakeDamage && Projectile.Distance(See.Center) < MinDistance)
                    {
                        MinDistance = Projectile.Distance(See.Center);
                        NPCWhoAmI = See.whoAmI ;
                    }
                }
            }
            if (NPCWhoAmI == -1)
            {
                if (Projectile.velocity != Vector2.Zero) Projectile.rotation = Projectile.velocity.ToRotation();
                return;
            }
            NPC npc = Main.npc[NPCWhoAmI];
            if (!npc.active || npc.friendly || npc.dontTakeDamage || npc.Distance(Projectile.Center) >= 16 * 1000 * 10
                || (player.HasMinionAttackTargetNPC && npc.whoAmI != player.MinionAttackTargetNPC))
            {
                Projectile.ai[1] = 0;
                return;
            }
            Projectile.rotation = MathHelper.Lerp(Projectile.rotation, (npc.Center - Projectile.Center).ToRotation(), 0.1f);
            Projectile.ai[1]++;
            int Damage = (int)(1000 * CSUtils.AverageDamage(player));
            Vector2 Direction = npc.Center - Projectile.Center;
            Direction.Normalize();
            if (Projectile.ai[1] % 15 == 0)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    int PWAI = Projectile.NewProjectile(null, Projectile.Center, Direction * 22f,
                        ModContent.ProjectileType<SeethingDischargeBrimstoneHellblast>(), Damage, 0f, player.whoAmI);
                    CSUtils.GetItemDamageType(PWAI, player.HeldItem);
                    Main.projectile[PWAI].penetrate = 2;
                    Main.projectile[PWAI].tileCollide = false;
                    Main.projectile[PWAI].usesLocalNPCImmunity = true;
                    Main.projectile[PWAI].localNPCHitCooldown = 0;
                }
                for (int j = -5; j <= 5; j++)
                {
                    Vector2 Rotation = (MathHelper.TwoPi * j / 30f + Projectile.rotation).ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + Rotation * 16f, 235);
                    dust.velocity = Rotation * (7f - System.Math.Abs(j));
                    dust.noGravity = true;
                    dust.noLight = false;
                    dust.scale *= 2f;
                }
            }
            if (Projectile.ai[1] >= 60)
            {
                Projectile.ai[1] = 0;
                if (player.whoAmI == Main.myPlayer)
                    for (int i = 0; i < 5; i++)
                    {
                        float radians = MathHelper.ToRadians(5);
                        Vector2 Dir2 = Vector2.Lerp(Direction.RotatedBy(-radians), Direction.RotatedBy(radians), i / 4f);
                        int PWAI = Projectile.NewProjectile(null, Projectile.Center, Dir2 * 27f,
                            ModContent.ProjectileType<SeethingDischargeBrimstoneBarrage>(), (int)(Damage / 40f), 0f, player.whoAmI);
                        CSUtils.GetItemDamageType(PWAI, player.HeldItem);
                        Main.projectile[PWAI].penetrate = -1;
                        Main.projectile[PWAI].usesLocalNPCImmunity = true;
                        Main.projectile[PWAI].localNPCHitCooldown = 0;
                    }
            }
        }
        public override bool? CanCutTiles() => false;
        public override bool? CanDamage() => false;
        //public override bool PreDraw(ref Color lightColor)
        //{
        //    Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
        //    Rectangle sourceRectangle = new Rectangle(0, 0, 66, 50);
        //    Vector2 origin = new Vector2(33, 25);
        //    Color color = Color.White;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        color *= 0.6f; color.R = (byte)(color.R / 0.7f);
        //        Main.spriteBatch.Draw(texture, projectile.oldPos[i] + origin - Main.screenPosition, sourceRectangle, color, projectile.oldRot[i],
        //            origin, projectile.scale, SpriteEffects.None, 0f);
        //    }
        //    Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, sourceRectangle, Color.White, projectile.rotation,
        //        origin, projectile.scale, SpriteEffects.None, 0f);
        //    return false;
        //}
    }
}