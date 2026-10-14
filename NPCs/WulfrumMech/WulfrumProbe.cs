using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod;
using CalamityMod.World;
using System.Collections.Generic;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Magic;

namespace CalamitySouls.NPCs.WulfrumMech
{
    public class WulfrumProbe : ModNPC
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Spazmatism);
            NPC.aiStyle = -1;
            AIType = -1;
            NPC.width = NPC.height = 68;
            NPC.damage = CalamityWorld.death ? 90 : CalamityWorld.revenge ? 70 : Main.expertMode ? 60 : 30;
            NPC.damage /= 2;
            NPC.defDamage = NPC.damage;
            NPC.defense = Main.expertMode ? 10 : 5;
            NPC.defDefense = NPC.defense;
            NPC.npcSlots = 5f;
            NPC.Calamity().DR = 0.05f;
            NPC.lifeMax = Main.expertMode ? 5000 : 3000;
            double HPBoost = CalamityMod.CalamityServerConfig.Instance.BossHealthBoost * 0.01;
            NPC.lifeMax += (int)(NPC.lifeMax * HPBoost);
            NPC.knockBackResist = 0f;
            NPC.buffImmune[0] = true;
            NPC.buffImmune[BuffType<TemporalSadness>()] = true;
            NPC.buffImmune[0] = true;
            NPC.buffImmune[0] = true;
            NPC.buffImmune[BuffType<Eutrophication>()] = true;
            NPC.buffImmune[BuffType<PearlAura>()] = true;
            NPC.buffImmune[149] = true;
            NPC.buffImmune[32] = true;
            NPC.buffImmune[0] = false;
            NPC.buffImmune[BuffType<Enraged>()] = false;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.value = 10000f;
        }
        public override void AI()
        {
            NPC wBoat = CSUtils.GetBoss<WulfrumFloatingBoat>();
            NPC wBase = CSUtils.GetBoss<WulfrumBase>();
            Player player = SelectingTarget();
            if (player == null) 
            {
                AIRunAway();
                return;
            }
            switch (AiState)
            {
                case StateNull:
                    AINull(player, false);
                    break; 
                case StateCircle:
                    AICircle(player, false);
                    break; 
                case StateRocket:
                    AIRocket(player, false);
                    break; 
                case StateSwitch:
                    AISwitch(player);
                    break; 
                case StateNullL:
                    AINull(player, true);
                    break; 
                case StateCircleL:
                    AICircle(player, true);
                    break; 
                case StateRocketL:
                    AIRocket(player, true);
                    break; 
                case StateBarrage:
                    AIBarrage(player);
                    break; 
                case StateFinalAttack:
                    AIFinalAttack(player);
                    break; 
            }
        }
        private const int StateNull= 0;
        private const int StateCircle = 1;
        private const int StateRocket = 2;
        private const int StateSwitch = 3;
        private const int StateNullL = 4;
        private const int StateCircleL = 5;
        private const int StateRocketL = 6;
        private const int StateBarrage = 7;
        private const int StateFinalAttack = 8;

        private const float RunAwayDistance = 16000f;
        public float AiState
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public float AiCounter
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public float AiSaver
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private Player SelectingTarget()
        {
            Player player = Main.player[NPC.target];
            if (!player.active || player.dead||player.Distance(NPC.Center)>3600f)
            {
                NPC wBase = CSUtils.GetBoss<WulfrumBase>();
                NPC.TargetClosestUpgraded(false, wBase == null ? null : (Vector2?)wBase.Center);
                player = Main.player[NPC.target];
                if (!player.active || player.dead || player.Distance(NPC.Center) > RunAwayDistance) return null;                
            }
            return player;
        }
        private void AIRunAway()
        {
            NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.UnitY * 100f, 0.07f);
            Vector2 iCenter;
            NPC wBase = CSUtils.GetBoss<WulfrumBase>();
            if (wBase == null)
            {
                NPC.TargetClosest(false);
                iCenter = Main.player[NPC.target].Center;
            }
            else iCenter = wBase.Center;
            if (NPC.Distance(iCenter) > RunAwayDistance) NPC.active = false;
        }
        private void AINull(Player player, bool L)
        {
            AiCounter++;
            NPC.velocity *= 0.95f;
            if (L)
            {
                if (AiCounter > 20)
                {
                    AiState = Utils.SelectRandom(Main.rand, new int[]
                    {
                        StateCircleL,
                        StateRocketL,
                        StateBarrage,
                    });
                    AiCounter = 0;
                }
                return;
            }
            if (AiCounter > 40)
            {
                AiCounter = 0;
                if (NPC.life < NPC.lifeMax / 2f)
                {
                    AiState = StateCircleL;
                    return;
                }
                AiState = Utils.SelectRandom(Main.rand, new int[]
                {
                    StateCircle,
                    StateRocket,
                });
            }
        }
        private void AICircle(Player player, bool L)
        {
            float dist = 480f;
            float velo = 30f;
            int Time = 240;
            AiCounter++;
            if (L)
            {
                dist = 640f;
                velo = 40f;
                Time = 300;
                if (AiCounter % 300 >= 240)
                {
                    NPC.velocity *= 0.95f;
                    float radius = AiCounter % 300 - 240;
                    if (radius < 40)
                    {
                        Vector2 dRot = CSUtils.RandomRotate;
                        Dust dust = Dust.NewDustPerfect(NPC.Center + dRot * 64, 61);
                        dust.noGravity = true;
                        dust.velocity = -dRot * 5f;
                        dust.scale = Main.rand.Next(12, 25) / 10f;
                        AiSaver = NPC.AngleTo(player.Center);
                        NPC.rotation = AiSaver;
                    }
                    else if (radius < 50 && radius % 3 <= 1 && CSUtils.GoodNetMode)
                    {
                        Vector2 dir = AiSaver.ToRotationVector2();
                        if (dir.HasNaNs()) dir = Vector2.UnitY;
                        Projectile bolt = Projectile.NewProjectileDirect(null, NPC.Center + dir * 32, dir * (5f + radius / 2f), ProjectileType<WulfrumBolt>(), NPC.ProjDamage(2f), 2f);
                        bolt.ForceClassless();
                        bolt.friendly = false;
                        bolt.hostile = true;
                        bolt.extraUpdates = 1;
                        bolt.tileCollide = false;
                    }
                    return;
                }
                else
                {
                    if (AiCounter % 101 >= 100)
                    {
                        Vector2 dir = NPC.DirectionTo(player.Center);
                        if (dir.HasNaNs()) dir = Vector2.UnitY;
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 dRot = MathHelper.ToRadians(i * 36f).ToRotationVector2();
                            dRot.X /= 2;
                            dRot = dRot.RotatedBy(dir.ToRotation());
                            Dust dust = Dust.NewDustPerfect(NPC.Center + dir * 32, DustID.Electric);
                            dust.noGravity = true;
                            dust.velocity = dRot * Main.rand.Next(30, 61) / 10f;
                            dust.scale = Main.rand.Next(8, 25) / 10f;
                        }
                        if (CSUtils.GoodNetMode)
                        {
                            Projectile bolt = Projectile.NewProjectileDirect(null, NPC.Center + dir * 32, dir * 10f, ProjectileID.MartianTurretBolt, NPC.ProjDamage(), 2f);
                            bolt.ForceClassless();
                            bolt.friendly = false;
                            bolt.hostile = true;
                            bolt.extraUpdates = 1;
                        }
                    }
                }
            }
            else
            {
                float radius = AiCounter % 120;
                if (radius >= 80)
                {
                    NPC.velocity *= 0.95f;
                    radius -= 80;
                    if (radius >= 39)
                    {
                        Vector2 dir = AiSaver.ToRotationVector2();
                        if (dir.HasNaNs()) dir = Vector2.UnitY;
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 dRot = MathHelper.ToRadians(i * 36f).ToRotationVector2();
                            dRot.X /= 2;
                            dRot = dRot.RotatedBy(dir.ToRotation());
                            Dust dust = Dust.NewDustPerfect(NPC.Center + dir * 32, 61);
                            dust.noGravity = true;
                            dust.velocity = dRot * Main.rand.Next(30, 61) / 10f;
                            dust.scale = Main.rand.Next(8, 25) / 10f;
                        }
                        if (CSUtils.GoodNetMode)
                        {
                            Projectile bolt = Projectile.NewProjectileDirect(null, NPC.Center + dir * 32, dir * 8f, ProjectileType<WulfrumBolt>(), NPC.ProjDamage(), 2f);
                            bolt.ForceClassless();
                            bolt.friendly = false;
                            bolt.hostile = true;
                            bolt.extraUpdates = 3;
                        }
                    }
                    else
                    {
                        Vector2 dRot = CSUtils.RandomRotate;
                        Dust dust = Dust.NewDustPerfect(NPC.Center + dRot * 64, 61);
                        dust.noGravity = true;
                        dust.velocity = -dRot * 5f;
                        dust.scale = Main.rand.Next(12, 25) / 10f;
                        AiSaver = NPC.AngleTo(player.Center);
                        NPC.rotation = AiSaver;
                    }
                    return;
                }
            }
            NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionTo(
                player.Center + NPC.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(0.3f)) * dist) * velo, 0.06f);
            NPC.rotation = Utils.AngleLerp(NPC.rotation, NPC.DirectionTo(player.Center).ToRotation(), 0.5f);
            if (AiCounter >= Time) BackAIToNull(L);       
        }
        private void AIRocket(Player player, bool L)
        {
            AiCounter++;
            if (L)
            {
                if (AiCounter < 60)
                {
                    Vector2 pos = player.Center + Vector2.UnitY * 400f;
                    NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionTo(pos) * 20f, 0.07f);
                    if (NPC.Distance(pos) > 200f) NPC.Center = Vector2.Lerp(NPC.Center, pos, 0.1f);
                }
                else if (AiCounter == 60)
                {
                }
                else if (AiCounter < 90)
                {
                    AiSaver++;
                    float vAmt = 30f - 250f / (AiSaver + 10f);
                    NPC.velocity = Vector2.Lerp(NPC.velocity, -Vector2.UnitY * vAmt, 0.09f);
                }
                else if (AiCounter < 140)
                {
                    NPC.velocity *= 0.98f;
                    AiSaver = 3.5f;
                }
                else if (AiCounter < 180)
                {
                    NPC.velocity *= 0.97f;
                    if (AiSaver > 0) AiSaver -= 0.1f;
                    NPC.velocity += NPC.DirectionTo(player.Center) * AiSaver;
                }
                else BackAIToNull(L);
                return;
            }
            if (AiCounter < 80)
            {
                NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionTo(player.Center + Vector2.UnitY * 270f) * 40f, 0.1f);
            }
            else if (AiCounter < 100)
            {
                NPC.velocity *= 0.9f;
            }
            else if (AiCounter < 140)
            {
                NPC.velocity -= Vector2.UnitY * 0.4f;
            }
            else if (AiCounter < 180)
            {
                NPC.velocity *= 0.975f;
                AiSaver = 2f;
            }
            else if (AiCounter < 240)
            {
                NPC.velocity *= 0.98f;
                if (AiSaver > 0) AiSaver -= 0.05f;
                NPC.velocity += NPC.DirectionTo(player.Center) * AiSaver;
            }
            else BackAIToNull(L);
        }
        private void AIBarrage(Player player)
        {

        }
        private void AISwitch(Player player)
        {

        }
        private void AIFinalAttack(Player player)
        {

        }
        private void BackAIToNull(bool L)
        {
            AiState = L ? StateNullL : StateNull;
            AiCounter = 0;
            AiSaver = 0;
        }
    }
}