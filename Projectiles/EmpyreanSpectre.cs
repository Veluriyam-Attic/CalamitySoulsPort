using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using static System.Math;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Rogue;
using CalamityMod;

namespace CalamitySouls.Projectiles
{
    public class EmpyreanSpectre : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 52;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.frame = WalkingFrame1;
            Projectile.timeLeft = 60 * 60 * 24 * 30;
            Projectile.soundDelay = 0;
            Projectile.friendly = true;
            Role = 0;
            WeaponRotation = 0f;
            WeaponPosition = Vector2.Zero;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead || !player.CS().EmpyreanSpectre || !player.active)
            {
                Projectile.Kill();
                return;
            }
            if (Projectile.timeLeft < 1800) Projectile.timeLeft = 1800;
            Item item = player.inventory[player.selectedItem];
            if (!item.IsAir)
            {
                Role = None;
                if (item.CountsAsClass(DamageClass.Melee)) Role = Melee;
                else if (item.CountsAsClass(DamageClass.Ranged)) Role = Ranged;
                else if (item.CountsAsClass(DamageClass.Magic)) Role = Magic;
                else if (item.CountsAsClass(DamageClass.Summon)) Role = Summon;
                else if (item.CountsAsClass<RogueDamageClass>()) Role = Rogue;
            }
            ref float CurrentStatus = ref Projectile.ai[0];
            ref float ShootCD = ref Projectile.ai[1];
            ref Vector2 velocity = ref Projectile.velocity;
            if (CurrentStatus == 0) CurrentStatus = Returning;
            if (ShootCD > 0) ShootCD--;
            #region FindTarget
            int Target = -1;
            if (player.HasMinionAttackTargetNPC)
            {
                Target = player.MinionAttackTargetNPC;
                NPC See = Main.npc[Target];
                if (Projectile.Distance(See.Center) < 16 * 800) Target = See.whoAmI;
            }
            else
            {
                float MinDistance = 16 * 800;
                foreach (NPC See in Main.npc)
                {
                    if (See.active && !See.friendly && !See.dontTakeDamage && Projectile.Distance(See.Center) < MinDistance && See.CanBeChasedBy())
                    {
                        MinDistance = Projectile.Distance(See.Center);
                        Target = See.whoAmI;
                    }
                }
                if (Target == -1)
                {
                    foreach (NPC See in Main.npc)
                    {
                        if (See.active && !See.friendly && !See.dontTakeDamage && Projectile.Distance(See.Center) < MinDistance)
                        {
                            MinDistance = Projectile.Distance(See.Center);
                            Target = See.whoAmI;
                        }
                    }
                }
            }
            #endregion
            if (CurrentStatus > NoTargetFound||Role==None)
            {
                #region TurningBack
                if (CurrentStatus != Returned)
                {
                    CurrentStatus = Returning;
                    Vector2 BackPos = Vector2.UnitY * -64 + player.Center;
                    Vector2 BackVelo = BackPos - Projectile.Center;
                    BackVelo.Normalize();
                    velocity = BackVelo * 17f;
                    if (Projectile.Distance(BackPos) < 32f || Projectile.Distance(BackPos) > 16f * 480f)
                    {
                        CurrentStatus = Returned;
                        ClearWeapon();
                        SpawnDust(Projectile.Center, 6f);
                        SpawnDust(BackPos, 6f);
                    }
                }
                else
                {
                    Vector2 BackPos = Utils.ToRotationVector2(MathHelper.ToRadians(-90)) * 64;
                    BackPos = player.Center + BackPos;
                    velocity = player.velocity;
                    if (velocity != Vector2.Zero) velocity.Normalize();
                    Projectile.Center = BackPos;
                }
                if (Target != -1 && Role != None) CurrentStatus = ChasingEnermy;
                #endregion
            }
            else
            {
                if (Target == -1)
                {
                    CurrentStatus = Returning;
                    ClearWeapon();
                }
                else
                {
                    NPC npc = Main.npc[Target];
                    float Speed1 = 17f;
                    float Speed2 = 3f;
                    float dir = Projectile.AngleTo(npc.Center);
                    float range = 32f;
                    float rot = 114514f;
                    switch (Role)
                    {
                        case 1:
                            {
                                if (Projectile.Distance(npc.Center) < 160f)
                                {
                                    if (CurrentStatus == ChasingEnermy)
                                    {
                                        CurrentStatus = AlreadyChasedOn;
                                        SpawnWeapon();
                                    }
                                    velocity = Vector2.Lerp(velocity, npc.velocity, 0.1f);
                                    if (Projectile.Distance(npc.Center) < 80f)
                                    {
                                        velocity *= 0.985f;
                                    }
                                }
                                else velocity = Vector2.Lerp(velocity, Projectile.DirectionTo(npc.Center) * Speed1, 0.06f);
                                if (HasWeapon)
                                {
                                    dir = MathHelper.ToRadians(MathHelper.Lerp(30, -90, ShootCD / 13));
                                    range = 16f;
                                    if (Projectile.spriteDirection == -1) dir = MathHelper.Pi - dir;
                                    if (ShootCD == 0 && player.whoAmI == Main.myPlayer)
                                    {
                                        ShootCD = 13;
                                        Vector2 velocity2 = Projectile.DirectionTo(npc.Center) * 24f;
                                        int amt = Main.rand.Next(4, 6);
                                        for (int i = 0; i < amt; i++)
                                        {
                                            velocity2 += new Vector2(Main.rand.Next(-20, 21), Main.rand.Next(-20, 21)) * 0.05f;
                                            float damageMult = 0.5f;
                                            int type = 0;
                                            switch (i)
                                            {
                                                case 1:
                                                    type = 0;
                                                    damageMult = 0.65f;
                                                    break;
                                                case 2:
                                                    type = 0;
                                                    damageMult = 0.8f;
                                                    break;
                                            }
                                            Projectile.NewProjectile(null, Projectile.Center, velocity2, type, (int)(45 * player.MeleeDamage() * damageMult), 5.25f, player.whoAmI);
                                        }
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                if (CurrentStatus == ChasingEnermy)
                                {
                                    CurrentStatus = AlreadyChasedOn;
                                    SpawnWeapon();
                                }
                                Vector2 RangedGo = Projectile.DirectionTo((player.Center * 3 + npc.Center) / 4f);
                                velocity = Vector2.Lerp(velocity, RangedGo * 12f, 0.05f);
                                if (HasWeapon)
                                {
                                    range = 20f;
                                    if (ShootCD == 0 && player.whoAmI == Main.myPlayer)
                                    {
                                        Projectile flame = Projectile.NewProjectileDirect(null, Projectile.Center, Projectile.DirectionTo(npc.Center) * 9f, ProjectileType<ShadeFire>(),
                                               (int)(40 * player.RangedDamage()), 3.5f, player.whoAmI);
                                        flame.usesLocalNPCImmunity = true;
                                        flame.localNPCHitCooldown = 10;
                                        ShootCD = 6;
                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                Projectile.velocity = Vector2.Zero;
                                if (Projectile.Distance(npc.Center) < 240)
                                {
                                    if (CurrentStatus == ChasingEnermy)
                                    {
                                        CurrentStatus = AlreadyChasedOn;
                                        SpawnWeapon();
                                    }
                                }
                                else
                                {
                                    Vector2 RandPos = Vector2.UnitY * Main.rand.Next(32, 160);
                                    RandPos = npc.Center + RandPos.RotatedByRandom(MathHelper.TwoPi);
                                    SpawnDust(Projectile.Center, 6f);
                                    Projectile.Center = RandPos;
                                    SpawnDust(RandPos, 6f);
                                }
                                if (HasWeapon)
                                {
                                    if (ShootCD == 0 && player.whoAmI == Main.myPlayer)
                                    {
                                        Vector2 velo = Vector2.Normalize(Projectile.DirectionTo(npc.Center) * 4f + CSUtils.RandomRotate) * 17f;
                                        float rLerp = Main.rand.Next(10, 160) * 0.001f;
                                        if (Main.rand.Next(2) == 0) rLerp *= -1f;
                                        float rDir = Main.rand.Next(10, 160) * 0.001f;
                                        if (Main.rand.Next(2) == 0) rDir *= -1f;
                                        int type = ProjectileType<EldritchTentacle>();
                                        float damage2 = 55 * player.MagicDamage();
                                        if (Main.rand.Next(7) == 0)
                                        {
                                            type = 0;
                                            damage2 *= 1.5f;
                                        }
                                        Projectile tenacle = Projectile.NewProjectileDirect(null, Projectile.Center, velo, type, (int)damage2, 3.5f, player.whoAmI, rDir, rLerp);
                                        tenacle.usesLocalNPCImmunity = true;
                                        tenacle.localNPCHitCooldown = 10;
                                        ShootCD = 5;
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                if (Projectile.Distance(npc.Center) < 20 * 16)
                                {
                                    velocity = Vector2.Lerp(velocity, npc.velocity, 0.05f);
                                    if (CurrentStatus == ChasingEnermy)
                                    {
                                        CurrentStatus = AlreadyChasedOn;
                                        SpawnWeapon();
                                    }
                                }
                                else velocity = Vector2.Lerp(velocity, Projectile.DirectionTo(npc.Center) * Speed1, 0.05f);
                                if (HasWeapon)
                                {
                                    if (Projectile.spriteDirection != -1) dir = MathHelper.Pi;
                                    rot = 0f;
                                    if (ShootCD == 0 && player.whoAmI == Main.myPlayer && player.ownedProjectileCounts(ProjectileType<LanternFlame>()) < 35)
                                    {
                                        ShootCD = Main.rand.Next(5, 9);
                                        float startOffsetX = Main.rand.Next(15, 200) * (Utils.NextBool(Main.rand) ? -1f : 1f);
                                        float startOffsetY = Main.rand.Next(15, 200) * (Utils.NextBool(Main.rand) ? -1f : 1f);
                                        Vector2 Pos = new Vector2(Projectile.Center.X + startOffsetX, Projectile.Center.Y + startOffsetY);
                                        Projectile.NewProjectile(null, Pos, Vector2.Zero, ProjectileType<LanternFlame>(),
                                               (int)(32 * player.MinionDamage()), 5f, player.whoAmI, 0f, 0f);
                                    }
                                }
                                break;
                            }
                        case 5:
                            {
                                if (Projectile.Distance(npc.Center) < 320)
                                {
                                    velocity = Vector2.Lerp(velocity, npc.velocity, 0.06f);
                                    if (Projectile.Distance(npc.Center) > 160)
                                        velocity = Vector2.Lerp(velocity, Projectile.DirectionTo(npc.Center) * Speed2, 0.05f);
                                    if (CurrentStatus == ChasingEnermy)
                                    {
                                        CurrentStatus = AlreadyChasedOn;
                                        SpawnWeapon();
                                    }
                                }
                                else velocity = Vector2.Lerp(velocity, Projectile.DirectionTo(npc.Center) * Speed1, 0.05f);
                                if (HasWeapon)
                                {
                                    if (ShootCD == 0 && player.whoAmI == Main.myPlayer)
                                    {
                                        Projectile disk = Projectile.NewProjectileDirect(null, Projectile.Center, Projectile.DirectionTo(npc.Center) * 20F,
                                            ProjectileType<DestructionBolt>(), (int)(750 * player.RogueDamage()), 10f, player.whoAmI);
                                        disk.penetrate = 1;
                                        ShootCD = 30;
                                    }
                                }
                                break;
                            }
                    }
                    if (rot == 114514f) rot = dir;
                    WeaponPosition = dir.ToRotationVector2() * range;
                    WeaponRotation = rot;
                }
            }
            #region VisualEffect
            ref int Frame = ref Projectile.frame;
            ref int SideFrame = ref Projectile.frameCounter;
            ref float SideFrameCounter = ref Projectile.knockBack;
            ref int ShadowEffect = ref Projectile.soundDelay;
            ShadowEffect += 2;
            if (ShadowEffect > 40 || Projectile.soundDelay < 0) ShadowEffect = 0;
            if (CurrentStatus > NoTargetFound)
            {
                SideFrame = 0;
                if (Frame >= 2) Frame = WalkingFrame1;
                SideFrameCounter++;
                if (SideFrameCounter >= 7)
                {
                    SideFrameCounter = 0;
                    if (Frame == WalkingFrame1) Frame = WalkingFrame2;
                    else if (Frame == WalkingFrame2) Frame = WalkingFrame1;
                }
                Projectile.spriteDirection = player.direction;
            }
            else
            {
                if (velocity.Y > 0.1f)
                {
                    SideFrameCounter = 0;
                    SideFrame = 2;
                    Frame = FlyingFrameDown;
                }
                else
                {
                    SideFrameCounter++;
                    if (SideFrameCounter >= 6)
                    {
                        SideFrameCounter = 0;
                        SideFrame++;
                    }
                    if (SideFrame >= 4) SideFrame = 0;
                    Frame = FlyingFrameUp;
                }
                float TryToLerp = Math.Abs(velocity.X);
                TryToLerp = MathHelper.ToRadians(25 - 25 / (1 + TryToLerp / 5f));
                if (Projectile.Center.X > Main.npc[Target].Center.X) Projectile.spriteDirection = -1;
                else Projectile.spriteDirection = 1;
                if (velocity.X > 0) Projectile.rotation = TryToLerp;
                else Projectile.rotation = -TryToLerp;
            }
            #endregion
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Spectre = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/EmpyreanSpectre").Value;
            Texture2D Wing = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/EmpyreanSpectre_Wing").Value;
            Texture2D Weapon = TextureAssets.Item[GetWeaponType()].Value;
            Rectangle GetSpectreFrameRectan = new Rectangle(0, Projectile.frame * 54, 40, 52);
            Rectangle GetWingFrameRectan = new Rectangle(0, Projectile.frameCounter * 62, 86, 60);
            Vector2 origin = new Vector2(20, 26);
            Vector2 worigin = new Vector2(43, 30);
            Vector2 weaorigin = Weapon.Size() / 2f;
            Vector2 TruePos = origin - Main.screenPosition;
            Vector2 wTruePos = new Vector2(-23 + 32, 26) - Main.screenPosition;
            Vector2 weaTruePos =origin -Main.screenPosition + WeaponPosition;
            SpriteEffects effects = SpriteEffects.None;
            SpriteEffects weffects = SpriteEffects.None;
            float wRotation = WeaponRotation;
            if (Projectile.spriteDirection < 0)
            {
                effects = SpriteEffects.FlipHorizontally;
                weffects = SpriteEffects.FlipVertically;
                wTruePos = new Vector2(28, 26) - Main.screenPosition;
            }
            Color color = Color.Lerp(Main.DiscoColor, lightColor, 0.5f);
            color.R = 0;
            color.A = 0;
            for (int i = 0; i < 5; i++)
            {
                color *= 0.8f;
                Main.spriteBatch.Draw(Wing, Projectile.oldPos[i] + wTruePos, GetWingFrameRectan, color, Projectile.rotation, worigin, 1f, effects, 0f);
                Main.spriteBatch.Draw(Spectre, Projectile.oldPos[i] + TruePos, GetSpectreFrameRectan, color, Projectile.rotation, origin, 1f, effects, 0f);
                if (HasWeapon) Main.spriteBatch.Draw(Weapon, Projectile.oldPos[i] + weaTruePos, null, color, WeaponRotation, weaorigin, 1f, weffects, 0f);
            }
            Main.spriteBatch.Draw(Wing, Projectile.position + wTruePos, GetWingFrameRectan, lightColor, Projectile.rotation, worigin, 1f, effects, 0f);
            Main.spriteBatch.Draw(Spectre, Projectile.position + TruePos, GetSpectreFrameRectan, lightColor, Projectile.rotation, origin, 1f, effects, 0f);
            if (HasWeapon) Main.spriteBatch.Draw(Weapon, Projectile.position + weaTruePos, null, lightColor, wRotation, weaorigin, 1f, weffects, 0f);
            return false;
        }
        public override bool? CanDamage() => false;
        private void SpawnDust(Vector2 Center, float Speed)
        {
            for (int i = 0; i < 18; i++)
            {
                Vector2 DustRotate = Utils.ToRotationVector2(MathHelper.TwoPi * i / 18);
                Dust dust = Dust.NewDustPerfect(Center, 62, DustRotate * Speed / 2, 100, default, 2f);
                dust.noGravity = true;
                dust.noLight = false;
                dust.scale = 2f;
            }
        }
        private void ClearWeapon()
        {
            SpawnDust(WeaponPosition, 14f);
            HasWeapon = false;
            Role = None;
            WeaponRotation = 0f;
            WeaponPosition = Vector2.Zero;
        }
        private void SpawnWeapon()
        {
            HasWeapon = true;
            SpawnDust(Projectile.Center, 14f);
        }
        private int GetWeaponType()
        {
            switch (Role)
            {
                case 1: return 0;
                case 2: return 0;
                case 3: return 0;
                case 4: return 0;
            }
            return 0;
        }
        public bool HasWeapon;
        public int Role;
        public float WeaponRotation;
        public Vector2 WeaponPosition;
        protected override bool CloneNewInstances => true;

        private const int WalkingFrame1 = 0;
        private const int WalkingFrame2 = 1;
        private const int FlyingFrameDown = 2;
        private const int FlyingFrameUp = 3;
        private const int Returning = 23333;
        private const int Returned = 66666;
        private const int NoTargetFound = 10000;
        private const int ChasingEnermy = 1234;
        private const int AlreadyChasedOn = 4321;
        private const int None = 0;
        private const int Melee = 1;
        private const int Ranged = 2;
        private const int Magic = 3;
        private const int Summon = 4;
        private const int Rogue = 5;
    }
}