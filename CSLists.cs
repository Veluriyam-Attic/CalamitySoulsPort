using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
// 1.4：CalamityLists 已从灾厄移除
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Melee.Spears;
using CalamityMod.Projectiles.Melee.Yoyos;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Rogue;
// 1.4：CalamityMod.Projectiles.Hybrid 命名空间已移除
using CalamityMod.Projectiles.Typeless;
using CalamitySouls.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls
{
    public class CSLists
    {
        public static void Load()
        {
            WeakIceProjectiles = new List<int>
            {
                ModContent.ProjectileType<DarkIceZero>(),
                ModContent.ProjectileType<ArcticBearPawProj>(),
                ModContent.ProjectileType<IceBombFriendly>(),
                ProjectileID.FrostShard,
                ModContent.ProjectileType<CosmicDischargeFlail>(),
                ModContent.ProjectileType<CosmicIceBurst>(),
                ModContent.ProjectileType<CryoBlast>(),
                ModContent.ProjectileType<CrystalPiercerProjectile>(),
                ModContent.ProjectileType<CrystalPiercerShard>(),
                ProjectileID.BallofFrost,
                ProjectileID.SnowBallFriendly,
                ModContent.ProjectileType<FlurrystormCannonShooting>(),
                ModContent.ProjectileType<FlurrystormIceChunk>(),
                ModContent.ProjectileType<FlurrystormIceShard>(),
                ModContent.ProjectileType<FrostBlossom>(),
                ModContent.ProjectileType<FrostBeam>(),
                ModContent.ProjectileType<FlareBoltProjectile>(),
                ProjectileID.FrostBoltStaff,
                ModContent.ProjectileType<ValariBoomerang>(),
                ModContent.ProjectileType<Valaricicle>(),
                ModContent.ProjectileType<IceBarrageMain>(),
                ModContent.ProjectileType<IceBlock>(),
                ModContent.ProjectileType<IceBlockIcicle>(),
                ProjectileID.IceBoomerang,
                ProjectileID.FrostArrow,
                ProjectileID.IceBlock,
                ProjectileID.IceSickle,
                ModContent.ProjectileType<IceStarProjectile>(),
                ModContent.ProjectileType<IcebreakerHammer>(),
                ModContent.ProjectileType<IcicleStaffProj>(),
                ProjectileID.NorthPoleWeapon,
                ProjectileID.NorthPoleSpear,
                ProjectileID.NorthPoleSnowflake,
                ModContent.ProjectileType<Snowflake>(),
                ProjectileID.FrostHydra,
                ProjectileID.FrostBlastFriendly,
                ModContent.ProjectileType<StarnightLanceProjectile>(),
                ModContent.ProjectileType<StarnightBeam>(),
                125,123,121,
            };
            NormalIceProjectils = new List<int>
            {
                ProjectileID.Amarok,
                ModContent.ProjectileType<IceClasperMinion>(),
                ModContent.ProjectileType<EndoHydraHead>(),
                ModContent.ProjectileType<FrostyFlareProj>(),
                ModContent.ProjectileType<FrostyFlareStealth>(),
                ModContent.ProjectileType<FrostShardFriendly>(),
                ModContent.ProjectileType<HypothermiaChunk>(),
                ModContent.ProjectileType<HypothermiaShard>(),
                ModContent.ProjectileType<TridentIcicle>(),
                ModContent.ProjectileType<KelvinCatalystBoomerang>(),
                ModContent.ProjectileType<KelvinCatalystStar>(),
                ModContent.ProjectileType<ShadecrystalProjectile>(),
                ModContent.ProjectileType<ShimmersparkYoyo>(),
                92,
                ModContent.ProjectileType<Icicle>(),
                ModContent.ProjectileType<Snowball>(),
            };
            StrongIceProjectiles = new List<int>
            {
                ProjectileID.Blizzard,
                ModContent.ProjectileType<IceSentry>(),
                ModContent.ProjectileType<IceSentryFrostBolt>(),
                ModContent.ProjectileType<IceSentryShard>(),
                ModContent.ProjectileType<EndoBeam>(),
                ModContent.ProjectileType<EndoCooperBody>(),
                ModContent.ProjectileType<EndoCooperLimbs>(),
                ModContent.ProjectileType<EndoFire>(),
                ModContent.ProjectileType<EndoIceShard>(),
                ModContent.ProjectileType<IcicleArrowProj>(),
                ProjectileID.RocketSnowmanI,
                ProjectileID.RocketSnowmanII,
                ProjectileID.RocketSnowmanIII,
                ProjectileID.RocketSnowmanIV,
            };
            ExtraProjectileList = new List<int>
            {
                ProjectileType<UmbraphileBoom>(),
                // 1.4 修复：1.3 这里放的是 ReaverBlast / XerocStar / XerocOrb / XerocFire / XerocBlast /
                // XerocBubble / GodSlayerPhantom（灾厄 2.x 已删除）。移植时被机械替换成了字面量 0，
                // 于是 ExtraProjectileList 里塞进了 11 个 0 —— 任意 type==0 的弹幕都会被 IsAnExtraProj()
                // 判成“附加弹幕”。这里改为放本模组的占位弹幕 CSPlaceholder（移植后就是它取代了上述弹幕），
                // 恢复 1.3 “本模组自己生成的附加弹幕不再触发命中特效”的语义。
                ProjectileType<CSPlaceholder>(),
                ProjectileID.SporeGas,
                ProjectileID.SporeGas2,
                ProjectileID.SporeGas3,
            };
        }
        public static void Unload()
        {
            WeakIceProjectiles = null;
            NormalIceProjectils = null;
            StrongIceProjectiles = null;
            ExtraProjectileList = null;
        }
        public static List<int> WeakIceProjectiles;
        public static List<int> NormalIceProjectils;
        public static List<int> StrongIceProjectiles;
        public static List<int> ExtraProjectileList;
    }
}
