using Terraria;
using CalamityMod.Waters;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using CalamitySouls.Buffs;
using CalamitySouls.Items.OtherItems;
using CalamitySouls.Projectiles;
using CalamityMod.CalPlayer;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Pets;
using CalamityMod;
using CalamityMod.Systems.Collections;
using CalamityMod.Items.Accessories;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using static System.Math;

namespace CalamitySouls
{
    public class CSPlayerEffects
    {
        public static void PostUpdateEquips(Player player)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            UpdateForces(player, mplayer, cplayer);
            UpdateSpells(player, mplayer, cplayer);
            UpdateEnchantments(player, mplayer, cplayer);
            UpdateBossLoots(player, mplayer, cplayer);
            SetMinionOverride(player, mplayer, cplayer);
            if (mplayer.NoTrailPleaseEvenIfYouHaventDoneConfigPleaseDoSomethingToCloseTheTrail) mplayer.PrismaticTrail = false;
        }
        public static void UpdateForces(Player player, CSPlayer mplayer, CalamityPlayer cplayer)
        {
            if (mplayer.ForceTechnical)
            {
                mplayer.SpellStarlight = CSToggle.SpellStarlight;
                mplayer.SpellPlague = CSToggle.SpellPlague;
                mplayer.EnchAstral = CSToggle.EnchAstral;
                mplayer.EnchPrismatic = CSToggle.EnchPrismatic;
            }
            if (mplayer.ForceCreature)
            {
                mplayer.CreatureSpore = true;

                mplayer.SpellOasis = CSToggle.SpellOasis;
                mplayer.SpellAquatic = CSToggle.SpellAquatic;
                mplayer.EnchReaver = CSToggle.EnchReaver;
                mplayer.EnchOmega = CSToggle.EnchOmega;
            }
            if (mplayer.ForceAnnihilation)
            {
                mplayer.AnnihilationOption = true;
                mplayer.SpellGlobe = CSToggle.SpellGlobe;
                mplayer.SpellDespair = CSToggle.SpellDespair;
                mplayer.SpellHeavenshade = CSToggle.SpellHeavenshade;

                mplayer.EnchFearmonger = CSToggle.EnchFearmonger;
            }
            if (mplayer.ForceRevolution)
            {
                mplayer.SpellEleum = CSToggle.SpellEleum;
                mplayer.EnchDesert = CSToggle.EnchDesert;
                mplayer.SpellBetrayal = CSToggle.SpellBetrayal;
                mplayer.EnchSilva = CSToggle.EnchSilva;
            }
            if (mplayer.ForceTyranny)
            {
                mplayer.EnchGodslayerAncient = CSToggle.EnchGodslayerAncient;
                mplayer.GodslayerAncientOtherworld = true;
                mplayer.EnchGodslayer = CSToggle.EnchGodslayer;
                mplayer.EnchAuric = CSToggle.EnchAuric;
                mplayer.EnchDemonshade = CSToggle.EnchDemonshade;
            }
        }
        public static void UpdateSpells(Player player, CSPlayer mplayer, CalamityPlayer cplayer)
        {
            if (CSToggle.SpellAquatic && (mplayer.SpellAquatic || (mplayer.EnchSulphurous && mplayer.EnchFathom)))
            {
                mplayer.AquaticArea = true;
                CSUtils.UpdateAccessory(0, player);
                mplayer.SpellAquatic = CSToggle.SpellAquatic;
                mplayer.EnchSulphurous = CSToggle.EnchSulphurous;
                mplayer.EnchFathom = CSToggle.EnchFathom;
            }
            if (CSToggle.SpellBetrayal && (mplayer.SpellBetrayal || (mplayer.EnchStatigel && mplayer.EnchTarragon)))
            {
                if (player.dashDelay > 1) player.dashDelay = 1;
                mplayer.SpellBetrayal = CSToggle.SpellBetrayal;
                mplayer.EnchStatigel = CSToggle.EnchStatigel;
                mplayer.EnchTarragon = CSToggle.EnchTarragon;
            }
            if (CSToggle.SpellDespair && (mplayer.SpellDespair || (mplayer.EnchBrim && mplayer.EnchBlood)))
            {
                mplayer.DespairSoul = true;
                CSUtils.UpdateAccessory(ItemType<BloodflareCore>(), player);
                mplayer.SpellDespair = CSToggle.SpellDespair;
                mplayer.EnchBrim = CSToggle.EnchBrim;
                mplayer.EnchBlood = CSToggle.EnchBlood;
            }
            if (CSToggle.SpellEleum && (mplayer.SpellEleum || (mplayer.EnchSnowRuffian && mplayer.EnchDaedalus)))
            {
                mplayer.EleumCrystal = true;
                mplayer.SpellEleum = CSToggle.SpellEleum;
                mplayer.EnchSnowRuffian = CSToggle.EnchSnowRuffian;
                mplayer.EnchDaedalus = CSToggle.EnchDaedalus;
            }
            if (CSToggle.SpellGlobe && (mplayer.SpellGlobe || (mplayer.EnchAero && mplayer.EnchHydrothermic)))
            {
                mplayer.GlobeBlade = true;
                mplayer.SpellGlobe = CSToggle.SpellGlobe;
                mplayer.EnchAero = CSToggle.EnchAero;
                mplayer.EnchHydrothermic = CSToggle.EnchHydrothermic;
            }
            if (CSToggle.SpellHeavenshade && (mplayer.SpellHeavenshade || (mplayer.EnchUmbraphile && mplayer.EnchEmpyrean)))
            {
                mplayer.HeavenshadeStar = true;
                CSUtils.UpdateAccessory(ItemType<VampiricTalisman>(), player);
                mplayer.SpellHeavenshade = CSToggle.SpellHeavenshade;
                mplayer.EnchUmbraphile = CSToggle.EnchUmbraphile;
                mplayer.EnchEmpyrean = CSToggle.EnchEmpyrean;
            }
            if (CSToggle.SpellOasis && (mplayer.SpellOasis || (mplayer.EnchVictide && mplayer.EnchMollusk)))
            {
                mplayer.SpellOasis = CSToggle.SpellOasis;
                mplayer.EnchVictide = CSToggle.EnchVictide;
                mplayer.EnchMollusk = CSToggle.EnchMollusk;
            }
            if (CSToggle.SpellPlague && (mplayer.SpellPlague || (mplayer.EnchPlagueReaper && mplayer.EnchPlagueBringer)))
            {
                mplayer.PlagueHive = true;
                CSUtils.UpdateAccessory(0, player);
                mplayer.SpellPlague = CSToggle.SpellPlague;
                mplayer.EnchPlagueReaper = CSToggle.EnchPlagueReaper;
                mplayer.EnchPlagueBringer = CSToggle.EnchPlagueBringer;
            }
            if (CSToggle.SpellStarlight && (mplayer.SpellStarlight || (mplayer.EnchWulfrum && mplayer.EnchTitan)))
            {
                mplayer.StarlightBooster = true;
                mplayer.SpellStarlight = CSToggle.SpellStarlight;
                mplayer.EnchWulfrum = CSToggle.EnchWulfrum;
                mplayer.EnchTitan = CSToggle.EnchTitan;
            }
        }
        public static void UpdateEnchantments(Player player, CSPlayer mplayer, CalamityPlayer cplayer)
        {
            float liferadius = player.statLife / (float)player.statLifeMax2;
            if (mplayer.EnchAero)
            {
                player.noFallDmg = true;
                if (!player.wet && !player.mount.Active) player.maxFallSpeed = 15f;
                mplayer.HasAeroBlade = true;
                player.GetDamage(DamageClass.Generic) += 0.25f;
                player.ModifyAllCrit(-25);
            }
            if (mplayer.EnchAstral)
            {
                player.findTreasure = true;
                player.detectCreature = true;
                player.dangerSense = true;
                player.GetDamage(DamageClass.Generic) += 0.1f;
                player.ModifyAllCrit(10);
                player.manaCost -= 0.1f;
                player.endurance += 0.1f;
                mplayer.WeaponFireRateBoost += 0.1f;
                player.maxMinions += 2;
                player.maxTurrets += 2;
                player.statLifeMax2 += 10;
                player.statManaMax2 += 10;
                if (liferadius < 0.5f && mplayer.AstralTimer <= 0) mplayer.AstralTimer = (4 + 10 + 4 + 60 * 60) * 60;
            }
            if (mplayer.EnchAuric)
            {
                mplayer.AuricFlash = true;
                player.AddBuff(0, 2);
                mplayer.AuricHexNerf = true;
                mplayer.AuricWeaponBoost = true;
            }
            if (mplayer.EnchBlood)
            {
                cplayer.bloodflareSet = true;
                player.statLifeMax2 += 100;
                player.lifeMagnet = true;
                player.manaMagnet = true;
                mplayer.BloodCanOption = true;
            }
            if (mplayer.EnchBrim)
            {
                mplayer.CanBrimFrenzy = true;
                player.buffImmune[BuffID.OnFire] = true;
                player.buffImmune[BuffID.Frostburn] = true;
                player.buffImmune[BuffID.CursedInferno] = true;
                player.buffImmune[BuffID.ShadowFlame] = true;
                player.buffImmune[BuffID.Burning] = true;
                player.buffImmune[BuffID.Daybreak] = true;
                player.buffImmune[0] = true;
                player.buffImmune[BuffType<CalamityMod.Buffs.DamageOverTime.BrimstoneFlames>()] = true;
                player.buffImmune[0] = true;
                player.buffImmune[0] = true;
                player.buffImmune[BuffType<CalamityMod.Buffs.DamageOverTime.GodSlayerInferno>()] = true;
                player.buffImmune[BuffType<CalamityMod.Buffs.DamageOverTime.HolyFlames>()] = true;
                player.buffImmune[0] = true;
                player.buffImmune[BuffType<CalamityMod.Buffs.DamageOverTime.Shadowflame>()] = true;
                player.buffImmune[0] = true;
                player.buffImmune[BuffType<CalamityMod.Buffs.StatBuffs.BrimflameFrenzyBuff>()] = true;
                player.buffImmune[0] = true;
                player.lavaRose = true;
                player.fireWalk = true;
                player.lavaImmune = true;
            }
            if (mplayer.EnchDaedalus)
            {
                player.statLifeMax2 += 20;
                mplayer.DaedalusEvade = true;
                player.buffImmune[0] = true;
                mplayer.DaedalusCrystal = true;
                mplayer.MyMoveSpeedMult += 0.1f;
                mplayer.MyMoveSpeedMult += 0.1f;
            }
            if (mplayer.EnchDemonshade)
            {
                cplayer.shadeRegen = true;
                cplayer.shadowSpeed = true;
                cplayer.scalPet = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts(ProjectileType<SCalPet>()) < 1)
                        Projectile.NewProjectile(null, player.Center.X, player.Center.Y, 0f, -1f, ProjectileType<SCalPet>(), 0, 0f, Main.myPlayer);
                }
            }
            if (mplayer.EnchDesert)
            {
                mplayer.DesertProwler = true;
                player.noKnockback = true;
                player.buffImmune[BuffID.WindPushed] = true;
                mplayer.DesertOverKillSandShark = true;
                mplayer.DesertDodge = true;
            }
            if (mplayer.EnchEmpyrean)
            {
                mplayer.EmpyreanProj = true;
                player.AddBuff(0, 2);
                player.AddBuff(0, 2);
                player.GetDamage<RogueDamageClass>() -= 0.1f;
                player.GetCritChance<RogueDamageClass>() -= 5;
                player.GetDamage(DamageClass.Generic) += 0.1f;
                player.ModifyAllCrit(10);
                mplayer.EmpyreanSpectre = true;
                mplayer.MyMoveSpeedMult += 0.4f;
                mplayer.MyMoveSpeedMult += 0.4f;
            }
            if (mplayer.EnchFathom)
            {
                player.maxMinions += 2;
                player.GetDamage(DamageClass.Generic) += 0.1f;
                mplayer.FathomFish = true;
            }
            if (mplayer.EnchFearmonger)
            {
                mplayer.FearmongerNerfCancel = true;
                player.lifeRegen += 14;
                player.lifeRegenTime += 7;
                if (player.lifeRegenTime < 1800) player.lifeRegenTime = 1800;
                mplayer.FearmongerCanArea = true;
                mplayer.FearmongerCanMark = true;
            }
            if (mplayer.EnchGodslayer)
            {
                cplayer.godSlayer = true;
                mplayer.GodslayerPhantom = true;
                int grcd = player.FindBuffIndex(0);
                if (grcd != -1 && player.buffTime[grcd] > 30 * 60) player.buffTime[grcd] = 30 * 60;
                mplayer.GodslayerBuff = true;
            }
            if (mplayer.EnchGodslayerAncient)
            {
                player.GetDamage(DamageClass.Generic) += 0.21f;
                mplayer.MyAccelerationMult += 0.5f;
                mplayer.GodslayerAncientCanOtherworld = true;
            }
            if (mplayer.EnchHydrothermic)
            {
                mplayer.HydrothermicEffect = true;
                mplayer.CanHydrothermicRevive = true;
                mplayer.MyMoveSpeedMult += 0.25f;
                mplayer.MyMoveSpeedMult += 0.25f;
            }
            if (mplayer.EnchMollusk)
            {
                player.ignoreWater = true;
                player.npcTypeNoAggro[NPCType<CalamityMod.NPCs.SunkenSea.Clam>()] = true;
                player.AddBuff(BuffType<CalamityMod.Buffs.StatDebuffs.Clamity>(), 2);
                if (player.setBonus != "") mplayer.MolluskPrefix = 2;
            }
            if (mplayer.EnchOmega)
            {
                cplayer.omegaBlueSet = true;
                if (player.ownedProjectileCounts(ProjectileType<CalamityMod.Projectiles.Typeless.OmegaBlueTentacle>()) >= 6) cplayer.omegaBlueSet = true;
            }
            if (mplayer.EnchPlagueBringer)
            {
                player.buffImmune[BuffType<CalamityMod.Buffs.DamageOverTime.Plague>()] = true;
                Lighting.AddLight(player.Center, 0f, 0.39f, 0.24f);
                mplayer.PlagueBringerPlague = true;
            }
            if (mplayer.EnchPlagueReaper)
            {
                mplayer.PlagueReaperPlague = true;
                mplayer.PlagueReaperFlight = true;
                mplayer.PlagueReaperBlind = true;
                mplayer.PlagueReaperAdrenaline = true;
                mplayer.AdrenalineDamageBoost += 0.15f;
            }
            if (mplayer.EnchPrismatic)
            {
                mplayer.PrismaticLaser = true;
                mplayer.pPrism = true;
                mplayer.PrismaticTrail = true;
            }
            if (mplayer.EnchReaver)
            {
                mplayer.ReaverEffect = true;
                if (liferadius < 0.5f) player.GetDamage(DamageClass.Generic) += 0.2f;
                mplayer.ReaverPlantera = true;
            }
            if (mplayer.EnchSilva)
            {
                cplayer.silvaSet = true;
                player.statLifeMax2 += 80;
                mplayer.SilvaBetterRevive = true;
            }
            if (mplayer.EnchSnowRuffian)
            {
                player.noFallDmg = true;
                cplayer.rogueVelocity += 0.2f;
                player.buffImmune[BuffType<CalamityMod.Buffs.StatDebuffs.FrozenLungs>()] = true;
                player.resistCold = true;
                mplayer.SnowFreeze = true;
            }
            if (mplayer.EnchStatigel)
            {
                Player.jumpHeight += 10;
                player.jumpSpeedBoost += 2.4f;
                player.longInvince = true;
                mplayer.StatigelSgod = true;
            }
            if (mplayer.EnchSulphurous)
            {
                player.ignoreWater = true;
                player.buffImmune[BuffType<CalamityMod.Buffs.DamageOverTime.SulphuricPoisoning>()] = true;
                mplayer.SulphurousCircle = true;
            }
            if (mplayer.EnchTarragon)
            {
                cplayer.tarraSet = true;
                mplayer.WeaponFireRateBoost += 0.25f;
                mplayer.TarragonLeaves = true;
            }
            if (mplayer.EnchTitan)
            {
                mplayer.WeaponKnockback += 3f;
                mplayer.TitanExplosion = true;
                mplayer.TitanInfection = true;
            }
            if (mplayer.EnchUmbraphile)
            {
                if (!cplayer.umbraphileSet)
                {
                    if (!Main.eclipse)
                    {
                        cplayer.stealthGenStandstill += 0.2f;
                        cplayer.stealthGenMoving += 0.2f;
                        if (!Main.dayTime)
                        {
                            cplayer.stealthGenStandstill -= 0.15f;
                            cplayer.stealthGenMoving -= 0.15f;
                        }
                        else cplayer.stealthGenMoving -= 0.15f;
                    }
                }
                mplayer.UmbraphileExplosion = true;
                mplayer.WeaponFireRateBoost += 0.15f;
                mplayer.CanUmbraphileShealth = true;
            }
            if (mplayer.EnchVictide)
            {
                player.gills = true;
                player.ignoreWater = true;
                if (mplayer.VictideBlessingCooldown <= 0)
                {
                    player.AddBuff(BuffType<CalamityMod.Buffs.StatBuffs.AmidiasBlessing>(), 77 * 60);
                    mplayer.VictideBlessingCooldown = 60 * 60;
                }
            }
            if (mplayer.EnchWulfrum)
            {
                if (liferadius < 0.5f)
                {
                    player.statDefense += 15;
                }
                mplayer.HasWulfrumShield = true;
            }
        }
        public static void SetMinionOverride(Player player, CSPlayer mplayer, CalamityPlayer cplayer)
        {
            if (mplayer.AnnihilationOption)
            {
                mplayer.HasAeroBlade = false;
                mplayer.BloodCanOption = false;
                mplayer.EmpyreanSpectre = false;
            }
            if (mplayer.CreatureSpore) mplayer.ReaverEffect = false;

            if (mplayer.AquaticArea)
            {
                mplayer.SulphurousCircle = false;
            }
            if (mplayer.EleumCrystal)
            {
                mplayer.DaedalusCrystal = false;
            }
            if (mplayer.HeavenshadeStar)
            {
                mplayer.UmbraphileExplosion = false;
                mplayer.EmpyreanProj = false;
                mplayer.EmpyreanSpectre = false;
            }
            if (mplayer.PlagueHive)
            {
                mplayer.PlagueReaperBlind = false;
            }
        }
        public static void UpdateAccMinions(Player player)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            if (mplayer.AnnihilationOption)
            {
                int option = ProjectileType<AnnihilationOption>();
                if (player.ownedProjectileCounts(option) < 4)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile killoption = Main.projectile[i];
                        if (killoption.active && killoption.type == option && killoption.owner == player.whoAmI) killoption.Kill();
                    }
                    for (int i = 0; i < 4; i++) Projectile.NewProjectile(null, player.Center, Vector2.Zero, option, 0, 0f, player.whoAmI, 0, i * 60);
                }
            }
            if (mplayer.HasAeroBlade)
            {
                if (player.FindBuffIndex(BuffType<AeroBladeBuff>()) == -1)
                    player.AddBuff(BuffType<AeroBladeBuff>(), 3600, true);
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts(ProjectileType<AeroBlade>()) < 1)
                        Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<AeroBlade>(),
                            (int)(60f * CSUtils.AverageDamage(player)), 0f, Main.myPlayer, 0f, 0f);
                }
            }
            if (mplayer.BloodCanOption)
            {
                int option = ProjectileType<BloodOption>();
                if (player.ownedProjectileCounts(option) < 3)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        Projectile killoption = Main.projectile[i];
                        if (killoption.active && killoption.type == option && killoption.owner == player.whoAmI) killoption.Kill();
                    }
                    for (int i = 0; i < 3; i++) Projectile.NewProjectile(null, player.Center, Vector2.Zero, option, 0, 0f, player.whoAmI, 0, i * 120);
                }
            }
            if (mplayer.DaedalusCrystal)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts(ProjectileType<DaedalusTrueCrystal>()) < 1)
                        Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<DaedalusTrueCrystal>(), (int)(200f * player.AverageDamage()), 0f, Main.myPlayer);
                }
            }
            if (mplayer.EmpyreanSpectre)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts(ProjectileType<EmpyreanSpectre>()) < 1)
                        Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<EmpyreanSpectre>(), 0, 0f, player.whoAmI);
                }
            }
            if (mplayer.FathomFish)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts(ProjectileType<FathomFish>()) < 1)
                        Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<FathomFish>(), 0, 0f, player.whoAmI);
                }
            }
            if (mplayer.PlagueReaperFlight)
            {
                if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts(ProjectileType<PlagueReaperWings>()) < 1)
                    Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<PlagueReaperWings>(),
                        0, 0f, player.whoAmI);
            }
            if (mplayer.StatigelSgod)
            {
                if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts(ProjectileType<SlimeGodMinion>()) < 1)
                    Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<SlimeGodMinion>(),
                        0, 0f, player.whoAmI);
            }
            if (mplayer.SulphurousCircle)
            {
                if (player.ownedProjectileCounts(ProjectileType<SulphurousArea>()) < 1)
                {
                    Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<SulphurousArea>(), 0, 0f, player.whoAmI);
                    Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<SulphurousArea>(), 0, 0f, player.whoAmI, 1);
                }
            }
            if (mplayer.HasWulfrumShield)
            {
                int shield = ProjectileType<WulfrumShield>();
                if (player.ownedProjectileCounts(shield) < 1)
                {
                    if (mplayer.WulfrumShieldCooldown == 0 && player.whoAmI == Main.myPlayer)
                        Projectile.NewProjectile(null, player.Center, Vector2.Zero, shield, 0, 0f, player.whoAmI);
                }
            }
        }
        public static void UpdateBossLoots(Player player, CSPlayer mplayer, CalamityPlayer cplayer)
        {
            if (mplayer.dsJaw)
            {
                player.pickSpeed -= 5f;
            }
        }
        public static void UpdateLifeRegen(Player player)
        {
            CSPlayer mplayer = player.CS();
        }
        public static void GetWeaponKnockback(Player player, Item item, ref float knockback)
        {
        }
        public static void ModifyHitByAnthing(Player player, ref int damage, ref bool crit, NPC npc = null, Projectile proj = null)
        {
            CSPlayer mplayer = player.CS();
        }
        public static void OnHitByAnything(Player player, NPC npc, Projectile proj, int damage, bool crit)
        {
            CSPlayer mplayer = player.CS();
            // 1.4 修复：1.3 此处有 !CalamityLists.projectileDestroyExceptionList.Contains(proj.type) 过滤，
            // 移植时该列表被灾厄移除、判断被填成 true，导致灾厄明确标记“不可被反射”的 Boss 弹幕
            // （BrimstoneMonster / OldDukeVortex / Ares 激光 / Thanatos 光束 等）都会被强制 friendly=true、
            // owner=玩家、damage*=40，而这些弹幕的 AI 会持续生成子弹幕，进而刷屏。这里改用 1.4 的等价集合。
            if (mplayer.GodslayerAncientOtherworld && proj != null && !CalamityProjectileSets.ShouldNotBeReflected[proj.type])
            {
                proj.friendly = true;
                proj.hostile = false;
                proj.owner = player.whoAmI;
                proj.velocity = -proj.velocity;
                proj.damage *= 40;
            }
        }
        public static bool CanBeHitByNPC(Player player, NPC npc, ref int cooldownSlot)
        {
            CSPlayer mplayer = player.CS();
            return true;
        }
        public static bool Shoot(Player player, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            Vector2 velocity = new Vector2(speedX, speedY);
            if (mplayer.StarlightBooster)
            {
                if (player.ownedProjectileCounts(ProjectileType<TitanBooster>()) < 1)
                    Projectile.NewProjectile(null, Main.MouseWorld, Vector2.Zero, ProjectileType<TitanBooster>(), 0, 0f, player.whoAmI);
            }
            if (mplayer.PrismaticLaser)
            {
                int laserID = 0;
                if (player.ownedProjectileCounts(laserID) < 1)
                {
                    Projectile laser = Projectile.NewProjectileDirect(null, player.Center, velocity.Length(1f), laserID, 500, 0f, player.whoAmI);
                    laser.GetItemDamageType(item);
                    for (int i = 0; i < Main.rand.Next(4, 10); i++)
                    {
                        laser = Projectile.NewProjectileDirect(null, player.Center, CSUtils.RandomRotate * 20f, ProjectileType<CSPlaceholder>() , 100, 0f, player.whoAmI);
                        laser.GetItemDamageType(item);
                    }
                }
            }
            if (mplayer.ReaverEffect && mplayer.canFireReaverRangedProjectile)
            {
                mplayer.canFireReaverRangedProjectile = false;
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile rocket = Projectile.NewProjectileDirect(null, position, velocity * 1.25f, ProjectileType<CSPlaceholder>() , 150, 2f, player.whoAmI);
                    rocket.GetItemDamageType(item);
                }
            }
            if (mplayer.TitanExplosion && mplayer.TitanExplosionCounter == 0)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC youAreDeadLmao = Main.npc[i];
                    if (youAreDeadLmao.active && !youAreDeadLmao.friendly && !youAreDeadLmao.dontTakeDamage && youAreDeadLmao.Distance(Main.MouseWorld) < 320f)
                    {
                        Projectile.NewProjectile(null, youAreDeadLmao.Center, Vector2.Zero, ProjectileType<CalamityMod.Projectiles.Typeless.DirectStrike>(),
                            (int)(2500f * player.AverageDamage()), 0f, player.whoAmI, i);
                    }
                }
                SoundEngine.PlaySound(SoundID.Item14, Main.MouseWorld);
                for (int dustexplode = 0; dustexplode < 360; dustexplode++)
                {
                    Vector2 dustd = Utils.RotatedBy(new Vector2(17f), MathHelper.ToRadians(dustexplode), default);
                    Dust eDust = Dust.NewDustDirect(Main.MouseWorld, 0, 0,
                        Utils.NextBool(Main.rand, 2) ? DustType<CalamityMod.Dusts.AstralBlue>() : DustType<CalamityMod.Dusts.AstralOrange>(),
                        dustd.X, dustd.Y, 100, default, 3f);
                    eDust.noGravity = true;
                    eDust.position = Main.MouseWorld;
                }
                mplayer.TitanExplosionCounter = 600;
            }
            if (mplayer.lGift)
            {
                Projectile cloneafuuckingproj = new Projectile();
                cloneafuuckingproj.SetDefaults(type);
                Projectile lProj = null;
                if (item.CountsAsClass(DamageClass.Melee)) lProj = Projectile.NewProjectileDirect(null, position, velocity, ProjectileType<CalamityMod.Projectiles.Melee.LuxorsGiftMelee>(),
                     CalamityUtils.DamageSoftCap(damage * 0.6, 120), 0f, player.whoAmI);
                else if (item.CountsAsClass(DamageClass.Ranged)) lProj = Projectile.NewProjectileDirect(null, position, velocity, ProjectileType<CalamityMod.Projectiles.Ranged.LuxorsGiftRanged>(),
                    CalamityUtils.DamageSoftCap(damage * 0.4, 80), 0f, player.whoAmI);
                else if (item.CountsAsClass(DamageClass.Magic)) lProj = Projectile.NewProjectileDirect(null, position, velocity, ProjectileType<CalamityMod.Projectiles.Magic.LuxorsGiftMagic>(),
                    CalamityUtils.DamageSoftCap(damage * 0.8, 160), 0f, player.whoAmI);
                else if (item.CountsAsClass<RogueDamageClass>()) lProj = Projectile.NewProjectileDirect(null, position, velocity, ProjectileType<CalamityMod.Projectiles.Rogue.LuxorsGiftRogue>(),
                    CalamityUtils.DamageSoftCap(damage * 0.5, 100), 0f, player.whoAmI);
                if (lProj != null) lProj.extraUpdates = cloneafuuckingproj.extraUpdates;
            }
            return true;
        }
        // ★ 1.3 → 1.4 关键修复：这里原样保留了 1.3 的 `return false;`。
        // tML 1.3 里 GlobalItem.UseItem 返回 false 只影响「是否执行默认使用效果」；
        // 但 1.4 把「设置 itemTime（使用冷却）」搬进了 Player.ApplyItemTime，其判断是：
        //     if (!(callUseItem ?? ItemTimeIsZero) || ItemLoader.UseItem(sItem, this) != false)
        //         SetItemTime(CombinedHooks.TotalUseTime(...));
        // 于是返回 false ⇒ SetItemTime 被跳过 ⇒ itemTime 永远是 0 ⇒ Player.ItemCheck 里
        //     bool flag2 = itemAnimation > 0 && ItemTimeIsZero && flag;
        //     if (sItem.shoot > 0 && flag2) ItemCheck_Shoot(...);
        // 在整个挥击动画期间（itemAnimation > 0）**每一帧都会开火一次**。
        // 波涌之刃 useAnimation = 25 ⇒ 一次挥击约 25 个剑光束，连成一整条横贯屏幕的弹幕流。
        // 修法：返回 null（不表态），让 vanilla 正常设置 itemTime；副作用保留。
        public static bool? UseItem(Item item, Player player)
        {
            CSPlayer mplayer = player.CS();
            if (mplayer.UmbraphileShealth > 0) mplayer.UmbraphileShealth = 0;
            return null;
        }
        public static bool PreHurt(Player player, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            if (mplayer.cCVaccines)
            {
                damage = (int)(damage * 0.01f);
                crit = false;
                if (Main.rand.Next(5) != 0)
                {
                    player.AddImmunityTime(4 * 60);
                    return false;
                }
            }
            if (mplayer.DaedalusEvade && mplayer.DaedalusEvadeCooldown == 0)
            {
                if (player.whoAmI == Main.myPlayer)
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 rotate = MathHelper.ToRadians(i * 30f).ToRotationVector2();
                        Projectile shard = Projectile.NewProjectileDirect(null, player.Center + rotate * 16f, rotate * 10f,
                            0,
                            (int)(player.AverageDamage() * 200f), 12f, player.whoAmI);
                        shard.GetPlayerDamageType(player); shard.extraUpdates = 1;
                    }
                player.AddImmunityTime(player.longInvince ? 4 * 60 : 2 * 60);
                mplayer.DaedalusEvadeCooldown = 45 * 60;
                return false;
            }
            if (mplayer.DesertDodgeBuff)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile sStorm = Projectile.NewProjectileDirect(null, player.Center, Vector2.Zero, ProjectileType<CSPlaceholder>() ,
                        50, 4f, player.whoAmI);
sStorm.timeLeft = 60;
                }
                for (int i = 0; i < 36; i++)
                {
                    Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Sandnado);
                    dust.scale *= 3f;
                }
                player.ClearBuff(BuffType<DesertDodgeBuff>());
                player.AddBuff(BuffType<DesertDodgeCD>(), 60 * 60);
              player.AddImmunityTime( 2 * 60);
                player.velocity = -Vector2.UnitY * 15f;
                damage = 0; playSound = false; customDamage = false; crit = false; hitDirection = 0;
                return false;
            }
            if (mplayer.FearmongerCanArea && mplayer.FearmongerAreaTimer == 0)
            {
                SoundEngine.PlaySound(SoundID.Item46, player.Center);
                mplayer.FearmongerAreaTimer = 60 * 60;
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile area = Projectile.NewProjectileDirect(null, player.Center, Vector2.Zero,
                           ProjectileType<FearmongerAreaProj>(), 0, 0f, player.whoAmI);
                    area.Center = player.Center;
                }
              player.AddImmunityTime( player.longInvince ? 4 * 60 : 2 * 60);
                return false;
            }
            return true;
        }
        public static void PostHurt(Player player, bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            if (mplayer.AuricFlash && mplayer.AuricFlashCooldown <= 0 && player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Zombie67, player.Center);
                Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<AuricFlash>(), 0, 0f, player.whoAmI);
                mplayer.AuricFlashCooldown = 15 * 60;
            }
            if (mplayer.CanHydrothermicRevive && mplayer.HydrothermicReviveCooldown <= 0)
            {
                player.HealLife(100);
                mplayer.HydrothermicReviveCooldown = 25 * 60;
            }
            if (mplayer.HydrothermicEffect)
            {
                SoundEngine.PlaySound(SoundID.Item74, player.Center);
                if (player.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<CSPlaceholder>() , (int)damage, 1f, player.whoAmI);
                }
            }
            if (mplayer.ReaverEffect)
            {
                SoundEngine.PlaySound(SoundID.Item1, player.Center);
                float startAngle4 = player.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 / 2;
                float deltaAngle4 = MathHelper.PiOver4 / 8f;
                int rDamage = (int)(58f * player.AverageDamage());
                if (player.whoAmI == Main.myPlayer && damage > 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        float xPos2 = Utils.NextBool(Main.rand, 2) ? (player.Center.X + 100f) : (player.Center.X - 100f);
                        Vector2 spawnPos2 = new Vector2(xPos2, player.Center.Y + Main.rand.Next(-100, 101));
                        Vector2 offsetAngle4 = (startAngle4 + deltaAngle4 * (i + i * i) / 2f + (32f * i)).ToRotationVector2();
                        Projectile rspore = Projectile.NewProjectileDirect(null, spawnPos2, offsetAngle4 * 5f, ProjectileType<CSPlaceholder>() , rDamage, 2f, player.whoAmI);
                        rspore.usesLocalNPCImmunity = true;
                        rspore.localNPCHitCooldown = 60;
                        rspore.GetPlayerDamageType(player);
                        rspore = Projectile.NewProjectileDirect(null, spawnPos2, -offsetAngle4 * 5f, ProjectileType<CSPlaceholder>() , rDamage, 2f, player.whoAmI, 1f);
                        rspore.usesLocalNPCImmunity = true;
                        rspore.localNPCHitCooldown = 60;
                        rspore.GetPlayerDamageType(player);
                    }
                }
                player.AddBuff(BuffType<CalamityMod.Buffs.StatBuffs.ReaverRage>(), 180, true);
            }
            if (mplayer.SnowFreeze)
            {
                Projectile.NewProjectile(null, player.Center, Vector2.Zero, ProjectileType<FrigidArea>(), 0, 0f);
                Projectile.NewProjectile(null, player.Center, -Vector2.UnitY * 16f, ProjectileType<SnowflakeDecoration>(), 0, 0f);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    float maxdist = 320f;
                    if (npc.active && npc.Distance(player.Center) < maxdist)
                    {
                        for (int j = 1; j <= 32; j++)
                        {
                            if (j * 10f >= npc.Distance(player.Center)) break;
                            Vector2 dir = player.DirectionTo(npc.Center);
                            Dust dust = Dust.NewDustPerfect(player.Center + dir * j * 10f, DustID.BlueCrystalShard);
                            dust.noGravity = true;
                            dust.scale = 2f;
                            dust.velocity = Vector2.Zero;
                        }
                        npc.CS().Frozen = 6 * 60;
                        npc.AddBuff(BuffID.Frostburn, 6 * 60);
                    }
                }
                player.AddBuff(BuffType<Frozen>(), 1 * 60);
            }
        }
        public static void OnHitNPCWithAnything(Player player, NPC target, int damage, float knockback, bool crit, Projectile proj = null, Item item = null)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            if (mplayer.CreatureSpore)
            {
                target.AddBuff(BuffID.Ichor, 10 * 60);
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                target.AddBuff(BuffID.Venom, 10 * 60);
                if ((proj == null || !proj.IsAnExtraProj()) && player.whoAmI == Main.myPlayer && Main.rand.Next(10) == 0)
                {
                    Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f, 0.1f);
                    Projectile spore = Projectile.NewProjectileDirect(null, target.Center, velocity, ProjectileID.TerrarianBeam,
                      (int)(damage * 0.5f), 0f, player.whoAmI);
                    spore.penetrate = -1;
                    spore.timeLeft = 10;
                    spore.usesLocalNPCImmunity = true;
                    spore.localNPCHitCooldown = 30;
                    spore.ForceClassless();
                }
            }

            if (mplayer.AquaticArea)
            {
                target.ForceThrowABuffOnYourFuckingFace(BuffType<CalamityMod.Buffs.DamageOverTime.SulphuricPoisoning>(), 600);
                target.ForceThrowABuffOnYourFuckingFace(BuffType<CalamityMod.Buffs.DamageOverTime.CrushDepth>(), 600);
                target.ForceThrowABuffOnYourFuckingFace(BuffType<CalamityMod.Buffs.DamageOverTime.Nightwither>(), 600);
            }
            if (mplayer.EleumCrystal)
            {
                // 1.4 修复：1.3 原版此处无冷却，每次命中都生成 1 发冰川水晶碎片。高命中率武器
                // （波涌之刃等会连续穿透/连击的剑光束）会让每一段命中都生成一发，几十上百发一起刷屏。
                // 加回独立冷却：最短 12 tick 一次（≈5 次/秒），效果本身不变。
                if (player.whoAmI == Main.myPlayer && mplayer.eleumShardCooldown <= 0 && (proj == null || !proj.IsAnExtraProj()))
                {
                    Vector2 velocity = CalamityUtils.RandomVelocity(50f, 30f, 60f, 0.1f);
                    Projectile.NewProjectile(null, target.Center, velocity, ProjectileType<EleumCrystalShard>(), (int)(damage * 0.15f), 0f, player.whoAmI, 0f, Main.rand.Next(2));
                    mplayer.eleumShardCooldown = 12;
                }
            }
            if (mplayer.HeavenshadeStar)
            {
                if (Main.rand.Next(10) == 0 && player.whoAmI == Main.myPlayer && (proj == null || (!proj.IsAnExtraProj() && proj.type != ProjectileType<HeavenshadeStar>())))
                {
                    Projectile star = Projectile.NewProjectileDirect(null, target.Center, CSUtils.RandomRotate * Main.rand.Next(15, 30),
                             ProjectileType<HeavenshadeStar>(), damage * 3, 0f, player.whoAmI);
                    star.GetPlayerDamageType(player);
                }
            }

            if (mplayer.DesertProwler)
            {
                target.StrikeNPCExtra(5, new Color(238,209,34), player);
                if (Main.rand.Next(25) == 0)
                {
                    int DustVeil = ProjectileType<CalamityMod.Projectiles.Typeless.SandCloakVeil>();
                    if (player.ownedProjectileCounts(DustVeil) < 1 && player.ownedProjectileCounts(DustVeil) < 1 && player.whoAmI == Main.myPlayer)
                    {
                        Projectile veil = Projectile.NewProjectileDirect(null, target.Center, Vector2.Zero, DustVeil, damage, 25f, player.whoAmI);
                        veil.GetPlayerDamageType(player);
                        veil.usesLocalNPCImmunity = true;
                        veil.localNPCHitCooldown = 60;
                    }
                }
            }
            if (mplayer.EmpyreanProj)
            {
                Vector2 velocity = CalamityUtils.RandomVelocity(100f, 15f, 15f, 1f);
                if (proj != null && !proj.IsAnExtraProj() && Main.rand.Next(3) == 0)
                {
                    Vector2 Center = target.Center;
                    int damage2 = damage;
                    if (proj.CountsAsClass(DamageClass.Melee))
                    {
                        damage2 = (int)(damage2 * 0.16f);
                        if (damage2 > 0) Projectile.NewProjectileDirect(null, Center, velocity * (Main.rand.Next(15, 30) / 15), ProjectileType<CSPlaceholder>() , damage2, 0f, player.whoAmI, target.whoAmI, 0f);
                    }
                    else if (proj.CountsAsClass(DamageClass.Ranged))
                    {
                        damage2 = (int)(damage2 * 0.03);
                        if (damage2 > 0) Projectile.NewProjectile(null, Center, Vector2.Zero, ProjectileType<CSPlaceholder>() , damage2, 0f, player.whoAmI, 0f, 0f);
                    }
                    else if (proj.CountsAsClass(DamageClass.Magic))
                    {
                        damage2 = (int)(damage2 * 0.125f); if (damage2 > 0) Projectile.NewProjectileDirect(null, Center, velocity * 2f, ProjectileType<CSPlaceholder>() , damage2, 0f, player.whoAmI, target.whoAmI, 0f);
                        if (target.canGhostHeal)
                        {
                            float heal10 = damage * (0.06f - proj.numHits * 0.015f);
                            if (heal10 >= 1)
                            {
                            }
                        }
                    }
                    else if (proj.minion)
                    {
                        damage2 = (int)(damage2 * 0.04); if (damage2 > 0) Projectile.NewProjectile(null, Center, Vector2.Zero, ProjectileType<CSPlaceholder>() , damage2, 0f, player.whoAmI, 0f, 0f);
                    }
                    else if (proj.CountsAsClass<RogueDamageClass>())
                    {
                        damage2 = (int)(damage2 * 0.12); if (damage2 > 0) Projectile.NewProjectileDirect(null, Center, velocity, ProjectileType<CSPlaceholder>() , damage2, 0f, player.whoAmI, target.whoAmI, 0f);
                    }
                }
            }
            if (mplayer.GodslayerPhantom)
            {
                if (player.GodSlayerDmg() <= 0f && proj != null && !proj.IsAnExtraProj())
                {
                    player.GodSlayerDmg() += damage * 0.5f;
                }
            }
            if (mplayer.HydrothermicEffect)
            {
                if (Main.rand.Next(20) == 0 && player.whoAmI == Main.myPlayer)
                {
                    Vector2 iCenter = target.position + new Vector2(Main.rand.Next(target.width), Main.rand.Next(target.height));
                    Projectile.NewProjectile(null, iCenter, Vector2.Zero, ProjectileType<CSPlaceholder>() , 0, 0f, player.whoAmI, player.whoAmI, 1f);
                }
            }
            if (mplayer.PlagueBringerPlague)
            {
                target.StrikeNPCExtra(20, new Color(128, 188, 67), player);
                target.ForceThrowABuffOnYourFuckingFace(BuffType<CalamityMod.Buffs.DamageOverTime.Plague>(), 600);
            }
            if (mplayer.ReaverEffect)
            {
                target.AddBuff(BuffID.Ichor, 10 * 60);
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                target.AddBuff(BuffID.Venom, 10 * 60);
                // 1.4 修复：1.3 原版此处是「每次命中 100% 生成 1 发孢子」，高命中率武器会把它线性放大。
                // 加回独立冷却：最短 12 tick 一次（≈5 次/秒）。
                // 同时补上 proj != null：近战（item）命中时 proj 为 null，而下面要用 proj.damage / proj.owner，
                // 1.3/1.4 原样都会在近战命中时抛 NullReferenceException。
                if (proj != null && !proj.IsAnExtraProj() && player.whoAmI == Main.myPlayer && mplayer.reaverSporeCooldown <= 0)
                {
                    Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f, 0.1f);
                    Projectile spore = Projectile.NewProjectileDirect(null, target.Center, velocity, ProjectileID.SporeGas + Main.rand.Next(3),
                        CalamityUtils.DamageSoftCap(proj.damage * 0.08f, 150), 0f, proj.owner);
                    spore.usesLocalNPCImmunity = true;
                    spore.localNPCHitCooldown = 30;
                    spore.GetPlayerDamageType(player);
                    mplayer.reaverSporeCooldown = 12;
                }
            }
            if (mplayer.TarragonLeaves && mplayer.TarragonLeavesCoolDown)
            {
                Item.NewItem(null, target.Center, TarragonLeaves.GetRandomTarragonLeaves(), 1, true);
                mplayer.TarragonLeavesCoolDown = false;
            }
            if (mplayer.TitanInfection) target.ForceThrowABuffOnYourFuckingFace(BuffType<CalamityMod.Buffs.DamageOverTime.AstralInfectionDebuff>(), 600);
            if (mplayer.UmbraphileExplosion && proj != null)
            {
                if (Utils.NextBool(Main.rand, 4) && !proj.IsAnExtraProj())
                {
                    Projectile explosion = Projectile.NewProjectileDirect(null, proj.Center, Vector2.Zero,
                        ProjectileType<CalamityMod.Projectiles.Rogue.UmbraphileBoom>(), (int)(proj.damage * 0.25f), 0f, player.whoAmI);
                    explosion.ForceClassless();
                }
            }
            if (mplayer.lGift) target.AddBuff(BuffID.OnFire, 10 * 60);
        }
        public static void ModifyHitNPCWithAnything(Player player, NPC target, ref int damage, ref float knockback, ref bool crit, Item item = null, Projectile proj = null)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            float mult = 0f;
            float flat = 0f;
            if (mplayer.AuricWeaponBoost)
            {
                if (proj != null && proj.type == ProjectileType<CalamityMod.Projectiles.Magic.DarkSparkBeam>()) damage *= 15;
            }
            if (mplayer.DesertOverKillSandShark)
                if (target.type == NPCID.SandElemental || target.type == NPCID.SandShark || target.type == NPCType<CalamityMod.NPCs.GreatSandShark.GreatSandShark>())
                {
                    mult += 1f;
                }
            if (mplayer.FearmongerNerfCancel && proj != null)
            {
                bool forbidden = player.head == 200 && player.body == 198 && player.legs == 142;
                Item heldItem = player.HeldItem;
                float summonNerfMult = (cplayer.fearmongerSet || (forbidden && heldItem.CountsAsClass(DamageClass.Magic))) ? 0.75f : 0.5f;
                if (proj.CountsAsClass(DamageClass.Summon) && !cplayer.profanedCrystalBuffs && heldItem.type > ItemID.None && !heldItem.CountsAsClass(DamageClass.Summon)
                    && (heldItem.CountsAsClass(DamageClass.Melee) || heldItem.CountsAsClass(DamageClass.Ranged) || heldItem.CountsAsClass(DamageClass.Magic) || heldItem.CountsAsClass<RogueDamageClass>())
                    && heldItem.hammer == 0 && heldItem.pick == 0 && heldItem.axe == 0 && heldItem.useStyle != 0 && !heldItem.accessory && heldItem.ammo == AmmoID.None)
                {
                    damage = (int)(damage / summonNerfMult);
                }
            }
            if (mplayer.FearmongerCanMark && mplayer.FearmongerMark != -1)
            {
                if (target.whoAmI == mplayer.FearmongerMark) mult += 0.25f;
                else mult -= 0.5f;
            }
            if (mplayer.GodslayerCrit)
            {
                if (Main.rand.Next(2) == 0) crit = true;
            }
            if (mplayer.PlagueHive && proj != null && (proj.type == ProjectileType<CalamityMod.Projectiles.Rogue.PlaguenadeBee>()
                || proj.type == ProjectileID.Bee || proj.type == ProjectileID.BeeHive || proj.type == ProjectileID.GiantBee || proj.type == ProjectileID.Wasp))
                mult += 2f;
            if (mplayer.PlagueReaperPlague && true) mult += 0.15f;
            if (mplayer.ReaverPlantera)
            {
                if (proj != null)
                {
                    if (proj.type == 0 || proj.type == 0) mult += 1f;
                }
            }
            if (mplayer.UmbraphileShealth == -1)
            {
                mult += 1f;
                mplayer.UmbraphileShealth = 0;
            }

            if (mplayer.dsJaw && item != null && item.pick > 0) mult += 1f;

            damage = (int)(damage * (mult + mplayer.AllDamageByMult + (crit ? mplayer.CritDamageMultipier : 1f) - 1f) + flat);
            float adrenalineDamageBoost = 2f + (cplayer.adrenalineBoostOne ? 0.15f : 0f)
                + (cplayer.adrenalineBoostTwo ? 0.15f : 0) + (cplayer.adrenalineBoostThree ? 0.15f : 0);
            if (cplayer.adrenalineModeActive) damage = (int)(damage * (adrenalineDamageBoost + mplayer.AdrenalineDamageBoost) / adrenalineDamageBoost);
        }
        public static bool PreKill(Player player, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            return true;
        }
        public static void PostUpdateBuffs(Player player)
        {
            CSPlayer mplayer = player.CS();
        }
        public static void PostUpdateMiscEffects(Player player)
        {
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            CSPlayer mplayer = player.CS();
            if (mplayer.AstralTimer >= (10 + 4 + 60 * 60) * 60)
            {
                player.DoAVisualBuff(BuffType<AstralImmunity>(), mplayer.AstralTimer - (10 + 4 + 60 * 60) * 60);
                player.SetImmunityTime(2);
            }
            else if (mplayer.AstralTimer >= (4 + 60 * 60) * 60)
            {
                player.DoAVisualBuff(BuffType<AstralDivision>(), mplayer.AstralTimer - (4 + 60 * 60) * 60);
                mplayer.WeaponFireRateBoost *= 2f;
                player.statLifeMax2 *= 2;
            }
            else if (mplayer.AstralTimer >= 60 * 60 * 60)
            {
                player.DoAVisualBuff(BuffType<AstralImmunity>(), mplayer.AstralTimer - 60 * 60 * 60);
                player.SetImmunityTime(2);
            }
            if (mplayer.AuricFlash)
            {
                if (mplayer.AuricFlashCooldown > 0)
                {
                    mplayer.AuricFlashCooldown--;
                    player.DoAVisualBuff(BuffType<AuricReviveCooldown>(), mplayer.AuricFlashCooldown);
                }
            }
            if (mplayer.AuricHexNerf && cplayer.vHex)
            {
                cplayer.contactDamageReduction += 0.2;
                cplayer.projectileDamageReduction += 0.2;
                player.blind = false;
                player.statDefense += 20;
                player.moveSpeed += 0.1f;
                player.wingTimeMax *= 2;
                player.endurance += 0.3f;
            }
            if (mplayer.BrimFrenzy > 0)
            {
                player.DoAVisualBuff(BuffType<BrimFrenzy>(), mplayer.BrimFrenzy);
                mplayer.AllDamageByMult += 0.25f;
            }
            if (mplayer.BrimFrenzyCooldown > 0)
            {
                player.DoAVisualBuff(BuffType<BrimFrenzyCooldown>(), mplayer.BrimFrenzyCooldown);
            }
            if (mplayer.BrimFrenzyVisualEffect > 0)
            {
                Lighting.AddLight(player.Center, 2.55f, 2f, 2f);
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.active && !projectile.friendly && projectile.hostile && projectile.Distance(player.Center) <= 960f
                        && !CalamityProjectileSets.ShouldNotBeReflected[projectile.type])
                    {
                        if (Main.rand.Next((projectile.tileCollide ? 1 : 2) * 30) == 0)
                        {
                            Projectile.NewProjectile(null, projectile.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f, 255);
                            projectile.Kill();
                        }
                    }
                }
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && npc.Distance(player.Center) < 960f && !npc.dontTakeDamage)
                    {
                        npc.Strike(50, player);
                    }
                }
            }
            if (mplayer.DaedalusEvadeCooldown > 0)
            {
                player.ClearBuff(BuffType<DaedalusEvadeCooldown>());
                player.AddBuff(BuffType<DaedalusEvadeCooldown>(), mplayer.DaedalusEvadeCooldown + 2);
            }
            if (mplayer.DesertDodge)
            {
                if (!mplayer.DesertDodgeCD)
                {
                    bool flag = (player.velocity.Length() <= 0.1f) && (player.itemAnimation <= 0);
                    if (flag) mplayer.DesertDodgeTimer++;
                    else mplayer.DesertDodgeTimer = 0;
                    if (mplayer.DesertDodgeTimer > 5 * 60) player.AddBuff(BuffType<DesertDodgeBuff>(), 5 * 60);
                }
            }
            else
            {
                mplayer.DesertDodgeTimer = 0;
                if (mplayer.DesertDodgeBuff) player.ClearBuff(BuffType<DesertDodgeBuff>());
            }
            if (mplayer.FearmongerCanArea)
            {
                if (mplayer.FearmongerAreaTimer > 0)
                {
                    player.DoAVisualBuff(BuffType<FearmongerAreaCooldown>(), mplayer.FearmongerAreaTimer);
                    mplayer.FearmongerAreaTimer--;
                }
            }
            if (mplayer.FearmongerCanMark)
            {
                if (mplayer.FearmongerMarkCooldown > 0)
                {
                    player.DoAVisualBuff(BuffType<FearmongerMarkCooldown>(), mplayer.FearmongerMarkCooldown);
                    mplayer.FearmongerMarkCooldown--;
                }
                if (mplayer.FearmongerMark != -1 && !Main.npc[mplayer.FearmongerMark].active)
                {
                    mplayer.FearmongerMark = -1;
                }
            }
            if (mplayer.Frozen)
            {
                player.noKnockback = true;
                player.statDefense += 12;
            }
            if (mplayer.GodslayerBuff)
            {
                int oneBuffTime = 5 * 60;
                if (mplayer.GodslayerBuffTimer == 0) mplayer.GodslayerBuffTimer = Main.rand.Next(5 * oneBuffTime);
                int bufftimer = mplayer.GodslayerBuffTimer % (5 * oneBuffTime);
                int bufftier = bufftimer / oneBuffTime;
                bufftimer -= bufftier * oneBuffTime;
                bufftimer = oneBuffTime - bufftimer;
                mplayer.GodslayerBuffTimer++;
                if (bufftier == 0)
                {
                    player.statDefense += 80;
                    player.DoAVisualBuff(BuffType<GodslayerDefense>(), bufftimer);
                }
                else if (bufftier == 1)
                {
                    mplayer.CritDamageMultipier += 0.5f;
                    player.DoAVisualBuff(BuffType<GodslayerSplit>(), bufftimer);
                }
                else if (bufftier == 2)
                {
                    if (player.miscCounter % 10 == 0) player.HealMana(25);
                    if (player.miscCounter % 10 == 5) player.HealLife(5);
                    player.DoAVisualBuff(BuffType<GodslayerResource>(), bufftimer);
                }
                else if (bufftier == 3)
                {
                    mplayer.GodslayerCrit = true;
                    player.DoAVisualBuff(BuffType<GodslayerCrit>(), bufftimer);
                }
                else if (bufftier == 4)
                {
                    mplayer.WeaponFireRateBoost += 0.5f;
                    player.DoAVisualBuff(BuffType<GodslayerSpeed>(), bufftimer);
                }
            }
            if (mplayer.GodslayerAncientCanOtherworld)
            {
                mplayer.GodslayerAncientOtherworldTimer++;
                if (player.itemAnimation > 0) mplayer.GodslayerAncientOtherworldTimer = 0;
                if (mplayer.GodslayerAncientOtherworldTimer > 120)
                {
                    player.AddBuff(BuffType<GodslayerAncientOtherworld>(), 2);
                    if (player.velocity.Length() > 14f) mplayer.GodslayerAncientOtherworld = true;
                }
            }
            if (mplayer.CanHydrothermicRevive && mplayer.HydrothermicReviveCooldown > 0)
            {
                mplayer.HydrothermicReviveCooldown--;
                player.DoAVisualBuff(BuffType<HydrothermicReviveCooldown>(), mplayer.HydrothermicReviveCooldown);
            }
            if (mplayer.PlagueReaperFlight)
            {
                player.wingTime = player.wingTimeMax = 99999;
                player.rocketTime = 99999;
            }
            if (mplayer.PlagueReaperBlind)
            {
                player.blind = true;
            }
            if (mplayer.pPrism)
            {
                int prism = ProjectileType<PrismaticPrismProj>();
                if (player.ownedProjectileCounts(prism) < 1 && player.whoAmI == Main.myPlayer)
                    Projectile.NewProjectile(null, player.Center, Vector2.Zero, prism, 0, 0f, player.whoAmI);
            }
            if (mplayer.SilvaBetterRevive)
            {
                if (cplayer.hasSilvaEffect && mplayer.SilvaRechargeCounter == 0)
                {
                    mplayer.SilvaRechargeCounter = 180 * 60;
                    player.DoAVisualBuff(BuffType<SilvaRecharge>(), mplayer.SilvaRechargeCounter);
                    if (cplayer.silvaCountdown > 0)
                    {
                        cplayer.silvaCountdown = (cplayer.auricSet ? 2 : 1) * 20 * 60;
                        player.AddBuff(BuffType<CalamityMod.Buffs.StatBuffs.SilvaRevival>(), 20 * 60);
                    }
                }
                if (mplayer.SilvaRechargeCounter > 175 * 60 && cplayer.silvaCountdown > 0)
                {
                    player.DoAVisualBuff(BuffType<SilvaInvulnerable>(), mplayer.SilvaRechargeCounter - 175 * 60);
                }
                if (mplayer.SilvaRechargeCounter == 1)
                {
                    cplayer.hasSilvaEffect = false;
                    cplayer.silvaCountdown = 600;
                    SoundEngine.PlaySound(new SoundStyle("CalamitySouls/Sounds/Custom/SilvaDispel"), player.Center);
                }
            }
            else
            {
                mplayer.SilvaRechargeCounter = 0;
                player.ClearBuff(BuffType<SilvaRecharge>());
                player.ClearBuff(BuffType<SilvaInvulnerable>());
            }
            if (mplayer.CanUmbraphileShealth)
            {
                if (mplayer.UmbraphileShealth >= 0) mplayer.UmbraphileShealth++;
                if (mplayer.UmbraphileShealth >= 5 * 60) mplayer.UmbraphileShealth = -1;
            }
            else
            {
                mplayer.UmbraphileShealth = 0;
            }
            if (mplayer.UmbraphileShealth == -1)
            {
                player.AddBuff(BuffType<UmbraphileUndetected>(), 2);
                if (!CSUtils.AnyBossAlive)
                    for (int i = 0; i < player.npcTypeNoAggro.Length; i++)
                    {
                        player.npcTypeNoAggro[i] = true;
                    }
            }

            if (mplayer.sgCore)
            {
                player.maxFallSpeed += 5f;
                if (CalamityUtils.CountHookProj() > 0 || player.sliding && !mplayer.sgSliding)
                {
                    mplayer.sgFlightTime = CSPlayer.sgFlightTimeMax;
                }
                else
                {
                    if (player.position.Y == player.oldPosition.Y && !mplayer.sgSliding && player.releaseJump)
                    {
                        mplayer.sgFlightTime = CSPlayer.sgFlightTimeMax;
                    }
                }
            }
        }
        public static void PostUpdateRunSpeeds(Player player)
        {
            CSPlayer mplayer = player.CS();
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            if (CSUtils.AnyBossAlive && mplayer.MyAccelerationMult > 2f) mplayer.MyAccelerationMult = 2f;
            if (CSUtils.AnyBossAlive && mplayer.MyMoveSpeedMult > 1.5f) mplayer.MyMoveSpeedMult = 1.5f;
            if (mplayer.GodslayerAncientOtherworld && !CSUtils.AnyBossAlive)
            {
                mplayer.MyMoveSpeedMult += 0.5f;
            }
            if (mplayer.sgCore && player.mount.Active && player.mount.Type == MountID.Slime)
            {
                mplayer.MyAccelerationMult += 2f;
                mplayer.MyMoveSpeedMult += 0.5f;
            }
            if (mplayer.sgCore && mplayer.sgSliding) mplayer.MyMoveSpeedMult += 1f;
            player.runAcceleration *= mplayer.MyAccelerationMult;
            // [设置] 玩家可关闭移速加成（最终形态移速过快时使用）
            if (!CSToggle.DisableSoulSpeed)
            {
            	player.accRunSpeed *= mplayer.MyMoveSpeedMult;
            	player.maxRunSpeed *= mplayer.MyMoveSpeedMult;
            }
        }
        public static void PreUpdateMovement(Player player)
        {
            CSPlayer mplayer = player.CS();
            player.CS().sgSliding = false;
            if (mplayer.sgCore && player.mount.Active && player.mount.Type == MountID.Slime)
            {
                if (player.controlJump)
                {
                    if ((!player.GetJumpState(ExtraJump.CloudInABottle).Active || !player.GetJumpState(ExtraJump.CloudInABottle).Available) && (!player.GetJumpState(ExtraJump.SandstormInABottle).Active || !player.GetJumpState(ExtraJump.SandstormInABottle).Available))
                    {
                        if (mplayer.sgFlightTime > 0)
                        {
                            mplayer.sgFlightTime--;
                            if (player.velocity.Y > -15f) player.velocity.Y -= 1f;
                        }
                        else
                        {
                            player.velocity.Y -= player.gravity;
                            player.CS().sgSliding = true;
                        }
                    }
                }
            }
        }
        public static void PostUpdate(Player player)
        {
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            CSPlayer mplayer = player.CS();
            if (mplayer.PlagueReaperAdrenaline && CalamityMod.World.CalamityWorld.revenge)
            {
                if (cplayer.adrenaline == cplayer.adrenalineMax) mplayer.PlagueReaperHasAdrenalineCurrently = true;
                if (cplayer.adrenalineModeActive && mplayer.PlagueReaperHasAdrenalineCurrently) mplayer.PlagueReaperHasAdrenalineCurrently = false;
                if (mplayer.PlagueReaperHasAdrenalineCurrently) cplayer.adrenaline = cplayer.adrenalineMax;
            }
            else
            {
                mplayer.PlagueReaperHasAdrenalineCurrently = false;
            }
            if (cplayer.rogueStealthMax > 0f && cplayer.wearingRogueArmor)
            {
                if (cplayer.stealthStrikeThisFrame)
                {
                    mplayer.StealthEssenseTransformCooldown = 30;
                }
                if (mplayer.StealthEssenseRegenCooldownCounter <= 0)
                {
                    mplayer.StealthEssense += mplayer.StealthEssenseRegen * (1 + cplayer.accStealthGenBoost);
                    if (mplayer.StealthEssense >= mplayer.StealthEssenseMax2 && cplayer.rogueStealth < cplayer.rogueStealthMax
                        && mplayer.StealthEssenseTransformCooldown <= 0 && !cplayer.stealthStrikeThisFrame)
                    {
                        mplayer.StealthEssense = mplayer.StealthEssenseMax2 * cplayer.rogueStealth / cplayer.rogueStealthMax;
                        cplayer.rogueStealth = cplayer.rogueStealthMax;
                        mplayer.StealthEssenseRegenCooldownCounter = mplayer.StealthEssenseRegenCooldown;
                    }
                }
                if (mplayer.StealthEssense >= mplayer.StealthEssenseMax2) mplayer.StealthEssense = mplayer.StealthEssenseMax2;
            }
            else
            {
                mplayer.StealthEssense = 0f;
            }
        }
        public static void ModifyWeaponDamage(Player player, Item item, ref float add, ref float mult, ref float flat)
        {
            CSPlayer mplayer = player.CS();
        }
    }
}