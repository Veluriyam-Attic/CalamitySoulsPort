using Terraria;
using CalamityMod.Projectiles.Typeless;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Projectiles
{
    public class DaedalusTrueCrystal : ModProjectile
    {
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("DaedalusTrueCrystal.DisplayName", "代达罗斯真晶");
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.soundDelay = 2;
            Projectile.penetrate = -1;
            Projectile.coldDamage = true;
        }
        public override bool? CanDamage() => false;
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D gTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/DaedalusTrueCrystalGlow").Value;
            Texture2D aTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/DaedalusTrueCrystalAfterImage").Value;
            Texture2D texHam = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/DaedalusHammer").Value;
            Texture2D texWing = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/DaedalusWings").Value;
            Texture2D texLight = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/DaedalusLight").Value;
            Vector2 origin = texture.Size() / 2f;
            Vector2 origin2 = new Vector2(Projectile.width, Projectile.height) / 2f;
            if (Projectile.ai[0] > 0)
            {
                texture = CSTexture.寒元;
                origin = texture.Size() / 2f;
            }
            Color color = Color.White;
            float scale = Projectile.scale;
            for (int i = 0; i < 4; i++)
            {
                color *= 0.8f;
                scale *= 0.9f;
                Main.spriteBatch.Draw(aTex, Projectile.oldPos[i] + origin2 - Main.screenPosition, null, color, Projectile.oldRot[i],
                    origin, scale, SpriteEffects.None, 0f);
            }
            if (Projectile.CountsAsClass(DamageClass.Magic))
            {
                float radius = ai1;
                while (radius >= 30) radius -= 30;
                if (radius > 15) radius = 30 - radius;
                radius = 0.95f + radius / 150f;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                Main.spriteBatch.Draw(texLight, Projectile.Center - Main.screenPosition, null, Color.White,
                Projectile.rotation, texLight.Size() / 2f, Projectile.scale * radius, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
            }
            if (Projectile.CountsAsClass(DamageClass.Ranged)) Main.spriteBatch.Draw(texWing, Projectile.Center - Main.screenPosition, null, Color.White,
                Projectile.rotation, texWing.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            if (Projectile.CountsAsClass(DamageClass.Melee)) Main.spriteBatch.Draw(texHam, Projectile.Center - Main.screenPosition, null, Color.White,
                MathHelper.ToRadians(ai1 * 6), texHam.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            if (Projectile.ai[0] == 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                Main.spriteBatch.Draw(gTex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, gTex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
            }
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        private void Transform()
        {
            SoundEngine.PlaySound(SoundID.Item25, Projectile.Center);
            for (int i = 0; i < 48; i++)
            {
                int seleDust = Utils.SelectRandom(Main.rand, new int[]
                {
                    DustID.BlueCrystalShard,
                    DustID.PinkCrystalShard,
                    DustID.PurpleCrystalShard,
                    88,
                    173,
                });
                Dust dust = Dust.NewDustPerfect(Projectile.Center, seleDust);
                dust.noGravity = true;
                dust.velocity = MathHelper.ToRadians(Main.rand.Next(360)).ToRotationVector2() * Main.rand.Next(30, 41) / 10f;
                dust.scale *= 2f;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
        }
        public override void AI()
        {
            if (Projectile.soundDelay > 0) SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
            if (ai0 == 1)
            {
                Transform();
            }
            if (ai0 > 0) ai0--;
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.CS().DaedalusCrystal)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;
            ai1++;
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
                int i = CSUtils.ClosetNPC(player.Center, SearchDist);
                if (i != -1) target = Main.npc[i];
            }
            bool attack = true;
            Item item = player.inventory[player.selectedItem];
            if (!item.IsAir)
            {
                if (item.type == 0 && item.stack == 1) 寒元AI(target, player);
                else if (target != null)
                {
                    if (item.CountsAsClass(DamageClass.Melee) && item.pick <= 0 && item.axe <= 0 && item.hammer <= 0) MeleeAI(target, player);
                    else if (item.CountsAsClass(DamageClass.Ranged)) RangedAI(target, player);
                    else if (item.CountsAsClass(DamageClass.Magic)) MagicAI(target, player);
                    else if (item.CountsAsClass(DamageClass.Summon)) SummonAI(target, player);
                    else if (item.CountsAsClass(DamageClass.Throwing) || item.CountsAsClass<RogueDamageClass>()) RogueAI(target, player);
                    else attack = false;
                }
                else attack = false;
            }
            else attack = false;
            if (!attack)
            {
                Projectile.FollowPlayer(player.Center, 1600f, 1f, player.velocity, 1f, true, 0.05f, 0.07f, 0.3f);
            }
            if (Projectile.velocity.Length() > 30f) Projectile.velocity.Length(30f);
            float rot = System.Math.Sign((int)Projectile.velocity.X) * 0.5f;
            Projectile.rotation = MathHelper.Lerp(Projectile.rotation, rot, 0.1f);
        }
        private float ai0
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        private float ai1
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        private void MeleeAI(NPC target, Player player)
        {
            Projectile.Melee();
            Projectile.FollowPlayer(target.Center, 1600f, 64f, target.velocity, 50f);
            if (ai1 > 5)
            {
                if (target.Distance(Projectile.Center) < 100f && player.whoAmI == Main.myPlayer)
                {
                    Projectile beam = Projectile.NewProjectileDirect(null, target.Center, Vector2.Zero, ProjectileID.FrostBoltSword,
                        (int)(74f * player.MeleeDamage()), 12f, player.whoAmI);
                    beam.penetrate = 1; beam.timeLeft = 20; beam.tileCollide = false;
                    ai1 = 0;
                }
            }
        }
        private void RangedAI(NPC target, Player player)
        {
            Projectile.Ranged();
            Projectile.FollowPlayer(player.Center + player.DirectionTo(target.Center) * 120f, 800f, 20f, player.velocity * 4, 32f);
            if (ai1 > 23)
            {
                ai1 = 0;
                SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
                Projectile.velocity += Vector2.UnitY * 10f;
                for (int j = 18; j <= 36; j++)
                {
                    Vector2 rotate = MathHelper.ToRadians(j * 10f).ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, 88);
                    dust.noGravity = true;
                    dust.scale *= 2f;
                    dust.velocity = rotate * 4f;
                    dust.velocity.Y /= 1.3f;
                }
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 pos = (Projectile.Center + target.Center * 3) / 4 + new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101) - 800f);
                        Vector2 vel = target.Center + target.velocity * 20f - pos; vel.SafeNormalize(Vector2.UnitY);
                        Projectile arrow = Projectile.NewProjectileDirect(null, pos, vel * 20f,
                                 ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.IcicleArrowProj>(),
                                 (int)(55f * player.RangedDamage()), 12f, player.whoAmI);
                        arrow.extraUpdates = 3;
                    }
                }
            }
        }
        private void MagicAI(NPC target, Player player)
        {
            Projectile.Magic();
            Projectile.FollowPlayer(player.Center - Vector2.UnitY * 160f, 800f, 5f, player.velocity, 20f, false, 0.05f, 0.07f);
            if (ai1 >= 30)
            {
                ai1 = 0;
                SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);
                Vector2 iCenter = Projectile.Center + new Vector2(Main.rand.Next(-80, 81), Main.rand.Next(-48, 49));
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile lunic = Projectile.NewProjectileDirect(null, iCenter, target.DirectionFrom(iCenter) * 16f,
                        ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.MagnusBeam>(),
                        (int)(90f * player.MagicDamage()), 12f, player.whoAmI);
                    lunic.tileCollide = false; lunic.Magic();
                }
            }
        }
        private void SummonAI(NPC target, Player player)
        {
            Projectile.Minion();
            Projectile.FollowPlayer(player.Center - Vector2.UnitY * 80f, 800f, 1f, player.velocity, 3f, true, 0.05f, 0.07f, 0.3f);
            if (ai1 >= 17)
            {
                ai1 = 0;
                for (int i = 0; i < 18; i++)
                {
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, 173);
                    dust.noGravity = true;
                    dust.velocity = MathHelper.ToRadians(Main.rand.Next(360)).ToRotationVector2() * 3f;
                    dust.scale *= 1.4f;
                }
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(null, Projectile.Center, Projectile.DirectionTo(target.Center + target.velocity * 10f) * 24f,
                        ModContent.ProjectileType<CalamityMod.Projectiles.Summon.DaedalusCrystalShot>(),
                       (int)(162 * player.MinionDamage()), 12f, player.whoAmI);
                }
            }
        }
        private void RogueAI(NPC target, Player player)
        {
            Projectile.Rogue();
            Projectile.FollowPlayer(target.Center + target.DirectionTo(player.Center) * 180f, 800f, 20f, target.velocity, 32f);
            if (ai1 > 100)
            {
                ai1 = 0;
                SoundEngine.PlaySound(SoundID.Item28, Projectile.Center);
                Vector2 dir = Projectile.DirectionTo(target.Center);
                for (int i = 0; i < 31; i++)
                {
                    Vector2 rotate = (MathHelper.ToRadians(360 * (i + 3) / 36f) + dir.ToRotation()).ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, 88);
                    dust.noGravity = true; dust.scale *= 2f; dust.velocity = rotate * 4f;
                }
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + dir * i * 8f, 88);
                    dust.noGravity = true; dust.scale *= 2f; dust.velocity = dir * 4f;
                }
                Projectile.velocity -= dir * 25f;
                if (player.whoAmI == Main.myPlayer)
                    for (int i = 0; i < 12; i++)
                    {
                        Projectile.NewProjectile(null, Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaedalusIcicle>(),
                                  (int)(55f * player.RogueDamage()), 12f, player.whoAmI, Projectile.whoAmI, i * 30);
                    }
            }
        }
        private void 寒元AI(NPC target, Player player)
        {
            if (ai0 == 0)
            {
                Transform();
            }
            ai0 = 2;
            Projectile.AllClass();
            if (target != null)
            {
                Projectile.FollowPlayer(player.Center + player.DirectionTo(target.Center) * 320f, 800f, 20f, player.velocity * 4, 32f);
                if (player.ownedProjectileCounts(ModContent.ProjectileType<DaedalusJavelin>()) < 1)
                {
                    SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
                    if (player.whoAmI == Main.myPlayer)
                    {
                        Projectile.NewProjectileDirect(null, Projectile.Center, Vector2.Zero,
                            ModContent.ProjectileType<DaedalusJavelin>(), (int)(200f * player.AverageDamage()), 12f, player.whoAmI, Projectile.whoAmI);
                    }
                }
            }
            else Projectile.FollowPlayer(player.Center, 800f, 160f, player.velocity, 20f);
        }
    }
}