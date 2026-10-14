using Terraria;
using Terraria.GameContent;
using Terraria.Audio;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using CalamitySouls.Buffs;
using CalamityMod;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Ranged;

namespace CalamitySouls.Content.V104.Projectiles
{
    public class CalPetV104 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            CSCompat.SeedDisplayName(this, "迷你灾厄");
        }
        // 1.3 CSUtils.HoldingAClassedWeapon 的 1.4 等价实现（逐条照搬；1.3 的 item.Calamity().trueMelee 在 1.4 已由 DamageClass.Melee 覆盖）
        private static bool HoldingAClassedWeaponV104(Player player)
        {
            Item item = player.HeldItem;
            if (item.IsAir || item.damage <= 0) return false;
            if (item.pick > 0 || item.axe > 0 || item.hammer > 0) return false;
            if (item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged)
                || item.CountsAsClass(DamageClass.Magic) || item.CountsAsClass(DamageClass.Summon)
                || item.CountsAsClass(DamageClass.Throwing)
                || item.CountsAsClass(ModContent.GetInstance<CalamityMod.RogueDamageClass>())) return true;
            return false;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 20;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.frameCounter = 0;
            Projectile.frame = 0;
        }
        public override bool? CanDamage() => false;
        public void SelectFrame()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
            if (Projectile.frame > 3) Projectile.frame = 0;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (CheckDead(player)) return;
            else Projectile.timeLeft = 2;
            if (HoldingAClassedWeaponV104(player)) Projectile.GetPlayerDamageType(player);
            int targetIndex = FindTarget(player);
            if (targetIndex == -1) WanderingAI(player);
            else AttackAI(player, Main.npc[targetIndex]);
            SelectFrame();
        }
        public float AiTimer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public int AttackTimer
        {
            get => Projectile.soundDelay;
            set => Projectile.soundDelay = value;
        }
        public float ai1
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public bool CheckDead(Player player)
        {
            if (!player.active || player.dead || !player.V104().BrimCalPet)
            {
                Projectile.active = false;
                return true;
            }
            return false;
        }
        public int FindTarget(Player player)
        {
            if (player.HasMinionAttackTargetNPC)
                if (Main.npc[player.MinionAttackTargetNPC].Hitbox.Distance(player.Center) < 1000f) return player.MinionAttackTargetNPC;
            int index = player.V104().closetNPCindex;
            if (index != -1 && Main.npc[index].Distance(player.Center) > 1000f) index = -1;
            return index;
        }
        public void WanderingAI(Player player)
        {
            length = MathHelper.Lerp(length, 1f, 0.1f);
            width = MathHelper.Lerp(width, 1f, 0.1f);
            Projectile.FollowPlayer(player.Center - Vector2.UnitY * 80, 1200f, 64f, player.velocity, 30f, false, 0.02f);
            if (Projectile.velocity.Length() != 0) Projectile.rotation = Utils.AngleLerp(Projectile.rotation, Projectile.velocity.ToRotation(), 0.2f);
        }
        public void AttackAI(Player player, NPC target)
        {
            AiTimer++;
            if (AiTimer < 300)
            {
                if (AiTimer == 25 || AiTimer == 65) AttackTimer = 30;
                if (AiTimer > 120 && AiTimer % 25 == 0) AttackTimer = 25;
                Shoot(player, target);
                if (AiTimer < 120) Projectile.FollowPlayer(target.Center + CSPlayerV104Effects.DirectionFromSafe(player, target.Center) * 200f, -1, 0f, Vector2.Zero, 20f);
                else Projectile.FollowPlayer(target.Center + (CSPlayerV104Effects.DirectionFromSafe(player, target.Center).RotatedBy(MathHelper.PiOver2) * 200f), -1, 0f, Vector2.Zero, 20f);
            }
            else if (AiTimer - 300 < 120)
            {
                if (AiTimer % 60 == 1) AttackTimer = 60;
                Spam(player, target);
            }
            else AiTimer = 0;
        }
        public void Shoot(Player player, NPC target)
        {
            bool bloodflare = player.V104().BloodflareCalPetBuff;
            bool anniihilation = player.V104().AnnihilationEffect;
            bool bloodflareVisual = bloodflare && !anniihilation;
            Projectile.rotation = Utils.AngleLerp(Projectile.rotation, CSPlayerV104Effects.AngleToSafe(Projectile, target.Center), 0.2f);
            if (AttackTimer > 15)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 dir = CSUtils.RandomRotate;
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain);
                    dust.noGravity = true;
                    dust.noLight = false;
                    dust.position = Projectile.Center + dir * Main.rand.Next(24, 49);
                    dust.velocity = -dir * 5f;
                    dust.scale *= 1.1f;
                }
                length = MathHelper.Lerp(1f, 0.75f, (30 - AttackTimer) / 15f);
                width = MathHelper.Lerp(1f, 1.2f, (30 - AttackTimer) / 15f);
            }
            else if (AttackTimer > 10)
            {
                length = MathHelper.Lerp(0.75f, 1.2f, (15 - AttackTimer) / 5f);
                width = MathHelper.Lerp(1.2f, 0.75f, (15 - AttackTimer) / 5f);
            }
            else
            {
                length = MathHelper.Lerp(1.2f, 1f, (10 - AttackTimer) / 10f);
                width = MathHelper.Lerp(0.75f, 1f, (10 - AttackTimer) / 10f);
                if (AttackTimer == 10)
                {
                    Vector2 dir = CSPlayerV104Effects.DirectionToSafe(Projectile, target.Center);
                    for (int i = 0; i < 12; i++)
                    {
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain);
                        dust.noGravity = true;
                        dust.noLight = false;
                        dust.position = Projectile.Center + dir * 20;
                        dust.velocity = dir * Main.rand.NextFloat() * 10f + CSUtils.RandomRotate;
                        dust.scale *= 1f + Main.rand.NextFloat();
                    }
                    if (Main.myPlayer == Projectile.owner)
                    {
                        if (AiTimer < 120)
                        {
                            int type = bloodflareVisual ? ProjectileType<GhostlyBolt>() : ProjectileType<SeethingDischargeBrimstoneHellblast>();
                            int damage = CSPlayerV104Effects.HeldDamage(player, anniihilation ? 2000f : bloodflare ? 1000f : 150f);
                            SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneHellblastSound") { Volume = 1.2f }, Projectile.Center);
                            Projectile hellBlast = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, dir * 10f,
                                     type, damage, 0f, player.whoAmI);
                            hellBlast.CopyProjDamageType(Projectile);
                            hellBlast.extraUpdates = 2;
                            hellBlast.tileCollide = false;
                            hellBlast.penetrate = 5;
                            hellBlast.usesLocalNPCImmunity = true;
                            hellBlast.localNPCHitCooldown = 10;
                        }
                        else
                        {
                            int type = bloodflareVisual ? ProjectileType<BloodfireArrowProj>() : ProjectileType<SeethingDischargeBrimstoneBarrage>();
                            int damage = CSPlayerV104Effects.HeldDamage(player, anniihilation?800f: bloodflare ? 375f : 50f);
                            SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneShoot") { Volume = 0.6f }, Projectile.Center);
                            for (int i = 0; i < 3; i++)
                            {
                                Vector2 dir2 = dir.RotatedBy(MathHelper.ToRadians((i - 1) * 5f)) * (2 + i) * 2f;
                                Projectile barrage = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, dir2,
                                     type, damage, 0f, player.whoAmI);
                                barrage.CopyProjDamageType(Projectile);
                                barrage.extraUpdates = 3;
                                barrage.tileCollide = false;
                                barrage.penetrate = 1;
                                if (bloodflare) barrage.ai[1] = 80f;
                            }
                        }
                    }
                }
            }
        }
        public void Spam(Player player, NPC target)
        {
            bool bloodflare = player.V104().BloodflareCalPetBuff;
            bool anniihilation = player.V104().AnnihilationEffect;
            bool bloodflareVisual = bloodflare && !anniihilation;
            length = MathHelper.Lerp(length, 1f, 0.1f);
            width = MathHelper.Lerp(width, 1f, 0.1f);
            if (AttackTimer > 30)
            {
                Projectile.rotation = Utils.AngleLerp(Projectile.rotation, CSPlayerV104Effects.AngleToSafe(Projectile, target.Center + target.velocity * 10f), 0.2f);
                Projectile.FollowPlayer(target.Center + CSPlayerV104Effects.DirectionFromSafe(player, target.Center) * 500f, -1, 0f, Vector2.Zero, 20f);
            }
            else if (AttackTimer == 30)
            {
                Projectile.rotation = CSPlayerV104Effects.AngleToSafe(Projectile, target.Center + target.velocity * 10f);
                Projectile.velocity = Projectile.rotation.ToRotationVector2() * 40f;
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/SCalDash") { Volume = 0.6f }, Projectile.Center);
                for (int i = 0; i < 32; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain);
                    dust.noGravity = true;
                    dust.noLight = false;
                    dust.velocity = CSUtils.RandomRotate * 10f;
                    dust.scale *= 1.3f;
                }
            }
            else if (AttackTimer > 0)
            {
                Projectile.velocity = Projectile.rotation.ToRotationVector2() * 25f;
                int type = bloodflareVisual ? ProjectileType<BloodBombExplosion>() : ProjectileType<HellfireExplosionFriendlyV104>();
                int damage = CSPlayerV104Effects.HeldDamage(player, anniihilation ? 1000f : bloodflare ? 400f : 150f);
                if (AttackTimer % 4 == 0 && player.whoAmI == Main.myPlayer)
                {
                    Projectile explosion = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                                     type, damage, 0f, player.whoAmI);
                    explosion.CopyProjDamageType(Projectile);
                    explosion.penetrate = 10;
                    explosion.usesLocalNPCImmunity = true;
                    explosion.localNPCHitCooldown = 8;
                    explosion.timeLeft = 20;
                }
            }
            else Projectile.FollowPlayer(target.Center + CSPlayerV104Effects.DirectionFromSafe(player, target.Center) * 500f, -1, 0f, Vector2.Zero, 20f);
        }
        public float length
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public float width
        {
            get => Projectile.localAI[1];
            set => Projectile.localAI[1] = value;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            bool bloodflare = player.V104().BloodflareCalPetBuff;
            bool anniihilation = player.V104().AnnihilationEffect;
            bool bloodflareVisual = bloodflare && !anniihilation;
            Texture2D texture = anniihilation ? ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/SCalPetV104", AssetRequestMode.ImmediateLoad).Value : TextureAssets.Projectile[Type].Value;
            Vector2 origin = new Vector2(32, 21);
            Vector2 scale = new Vector2(length, width) * Projectile.scale;
            Rectangle sRect = new Rectangle(0, Projectile.frame * 42, 64, 42);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            for (int i = 0; i < 6; i++)
            {
                Vector2 pos = Projectile.Center - Main.screenPosition;
                if (AiTimer > 300 && AiTimer < 420 && AttackTimer < 30 && AttackTimer > 0) pos += -Projectile.rotation.ToRotationVector2() * 15f * (i + 1);
                else pos += MathHelper.ToRadians(i * 60).ToRotationVector2() * 6f;
                Main.spriteBatch.Draw(texture, pos, sRect, Color.DarkRed, Projectile.rotation, origin, scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            Color color = Color.White;
            if (AiTimer < 300 && AttackTimer > 15) color = Color.Lerp(Color.DarkRed, Color.White, (AttackTimer - 15f) / 15f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sRect, color, Projectile.rotation, origin, scale, SpriteEffects.None, 0f);
            if (bloodflareVisual) Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/CalPet_bfHelmV104", AssetRequestMode.ImmediateLoad).Value, Projectile.Center - Main.screenPosition, 
                null, Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}