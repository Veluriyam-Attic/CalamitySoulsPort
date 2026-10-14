using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using CalamitySouls.Buffs;
using CalamitySouls.Projectiles;
using Terraria.ModLoader.IO;
using CalamityMod;

namespace CalamitySouls
{
    public class CSPlayer : ModPlayer
    {
        public override void Initialize()
        {
            StealthEssenseMax = 100f;
        }
        public override void UpdateDead()
        {
            AuricFlashCooldown = 0;

            StealthEssense = 0f;

            AstralTimer = 0;
            BrimFrenzy = 0;
            BrimFrenzyVisualEffect = 0;
            BrimFrenzyCooldown = 0;
            DaedalusEvadeCooldown = 0;
            GodslayerBuffTimer = 0;
            GodslayerAncientOtherworldTimer = 0;
            HydrothermicReviveCooldown = 0;
            SilvaRechargeCounter = 0;
            VictideBlessingCooldown = 0;
            WulfrumShieldCooldown = 0;

            EffectsThatNeedtoReset();
        }
        public override void ResetEffects()
        {
            EffectsThatNeedtoReset();
        }
        public void EffectsThatNeedtoReset()
        {
            WeaponFireRateBoost = 1f;
            Frozen = false;
            WeaponKnockback = 1f;
            AllDamageByMult = 1f;
            AdrenalineDamageBoost = 0f;
            if (CSWorld.HyperMode) AdrenalineDamageBoost += 0.5f;
            MyAccelerationMult = 1f;
            MyMoveSpeedMult = 1f;
            FearmongerFrozen = false;
            CritDamageMultipier = 1f;

            StealthEssenseMax2 = StealthEssenseMax;
            StealthEssenseRegen = 0.5f;
            if (StealthEssenseTransformCooldown > 0) StealthEssenseTransformCooldown--;
            if (StealthEssenseRegenCooldownCounter > 0) StealthEssenseRegenCooldownCounter--;
            if (StealthEssenseRegenCooldownCounter > StealthEssenseRegenCooldown) StealthEssenseRegenCooldownCounter = StealthEssenseRegenCooldown;
            StealthEssenseRegenCooldown = 120;

            cCVaccines = false;
            pPrism = false;
            NoTrailPleaseEvenIfYouHaventDoneConfigPleaseDoSomethingToCloseTheTrail = false;

            CreatureSpore = false;
            AnnihilationOption = false;
            #region SpellEffects
            SpellAquatic = false;
            SpellBetrayal = false;
            SpellDespair = false;
            SpellEleum = false;
            SpellGlobe = false;
            SpellHeavenshade = false;
            SpellOasis = false;
            SpellPlague = false;
            SpellStarlight = false;

            AquaticArea = false;
            DespairSoul = false;
            EleumCrystal = false;
            GlobeBlade = false;
            HeavenshadeStar = false;
            PlagueHive = false;
            StarlightBooster = false;
            #endregion
            #region EnchEffects
            HasAeroBlade = false;
            if (AstralTimer > 0) AstralTimer--;
            AuricFlash = false;
            AuricHexNerf = false;
            AuricWeaponBoost = false;
            BloodCanOption = false;
            if (BrimFrenzy == 1)
            {
                BrimFrenzyCooldown = 25 * 60;
                SoundEngine.PlaySound(new SoundStyle("CalamitySouls/Sounds/Custom/BrimflameRecharge"), Player.Center);
            }
            if (BrimFrenzy > 0) BrimFrenzy--;
            if (BrimFrenzyVisualEffect > 0) BrimFrenzyVisualEffect--;
            if (BrimFrenzyCooldown > 0) BrimFrenzyCooldown--;
            CanBrimFrenzy = false;
            DaedalusEvade = false;
            if (DaedalusEvadeCooldown > 0) DaedalusEvadeCooldown--;
            DaedalusCrystal = false;
            DesertProwler = false;
            DesertOverKillSandShark = false;
            DesertDodge = false;
            DesertDodgeBuff = false;
            DesertDodgeCD = false;
            EmpyreanProj = false;
            EmpyreanSpectre = false;
            FathomFish = false;
            FearmongerNerfCancel = false;
            FearmongerCanArea = false;
            FearmongerCanMark = false;
            GodslayerPhantom = false;
            if (!GodslayerBuff) GodslayerBuffTimer = 0;
            GodslayerBuff = false;
            GodslayerCrit = false;
            if (!GodslayerAncientCanOtherworld) GodslayerAncientOtherworldTimer = 0;
            GodslayerAncientCanOtherworld = false;
            GodslayerAncientOtherworld = false;
            CanHydrothermicRevive = false;
            if (HydrothermicReviveCooldown > 0) HydrothermicReviveCooldown--;
            HydrothermicEffect = false;
            PlagueBringerPlague = false;
            PlagueReaperPlague = false;
            PlagueReaperFlight= false;
            PlagueReaperBlind = false;
            PlagueReaperAdrenaline = false;
            PrismaticLaser = false;
            PrismaticTrail = false;
            ReaverEffect = false;
            ReaverPlantera = false;
            SilvaBetterRevive = false;
            if (SilvaRechargeCounter > 0) SilvaRechargeCounter--;
            SnowFreeze = false;
            StatigelSgod = false;
            SulphurousCircle = false;
            TarragonLeaves = false;
            if (Player.miscCounter % 30 == 0) TarragonLeavesCoolDown = true;
            TitanExplosion = false;
            if (TitanExplosionCounter > 0) TitanExplosionCounter--;
            TitanInfection = false;
            if (MolluskPrefix > 0) MolluskPrefix--;
            if (eleumShardCooldown > 0) eleumShardCooldown--;
            if (reaverSporeCooldown > 0) reaverSporeCooldown--;
            UmbraphileExplosion = false;
            CanUmbraphileShealth = false;
            if (VictideBlessingCooldown > 0) VictideBlessingCooldown--;
            HasWulfrumShield = false;
            if (WulfrumShieldCooldown > 0) WulfrumShieldCooldown--;
            #endregion
            #region Force&Ench
            ForceAnnihilation = false;
            ForceCreature = false;
            ForceRevolution = false;
            ForceTechnical = false;
            ForceTyranny = false;
            EnchAero = false;
            EnchAstral = false;
            EnchAuric = false;
            EnchBlood = false;
            EnchBrim = false;
            EnchDaedalus = false;
            EnchDemonshade = false;
            EnchDesert = false;
            EnchEmpyrean = false;
            EnchFathom = false;
            EnchFearmonger = false;
            EnchGodslayer = false;
            EnchGodslayerAncient = false;
            EnchHydrothermic = false;
            EnchMollusk = false;
            EnchOmega = false;
            EnchPlagueBringer = false;
            EnchPlagueReaper = false;
            EnchPrismatic = false;
            EnchReaver = false;
            EnchSilva = false;
            EnchSnowRuffian = false;
            EnchStatigel = false;
            EnchSulphurous = false;
            EnchTarragon = false;
            EnchTitan = false;
            EnchUmbraphile = false;
            EnchVictide = false;
            EnchWulfrum = false;
            #endregion
            if (!sgCore)
            {
                sgFlightTime = sgFlightTimeMax;
                sgSliding = false;
            }

            dsJaw = false;
            sgCore = false;
            lGift = false;

            RotI = false;
        }
        public override void OnEnterWorld()
        {
            // 1.4 移植：按用户要求移除改动日志物品，不再自动发放
        }
        #region TONs of effects
        public override void PostUpdateEquips()
        {
            CSPlayerEffects.PostUpdateEquips(Player);
        }
        public override void UpdateLifeRegen()
        {
            CSPlayerEffects.UpdateLifeRegen(Player);
        }
        public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
        {
            knockback *= WeaponKnockback;
        }
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return CSPlayerEffects.CanBeHitByNPC(Player, npc, ref cooldownSlot);
        }
        public override void PostUpdateBuffs()
        {
            CSPlayerEffects.PostUpdateBuffs(Player);
        }
        // ==== 1.4 适配层：以 100 为基准跑旧的 ref 风格逻辑，再折算成修饰符倍率 ====
        private void ApplyHurtModifiers(ref Player.HurtModifiers modifiers, NPC npc, Projectile proj)
        {
            int damage = 100; bool crit = false;
            CSPlayerEffects.ModifyHitByAnthing(Player, ref damage, ref crit, npc, proj);
            modifiers.FinalDamage *= damage / 100f;
        }
        private void ApplyNpcHitModifiers(ref NPC.HitModifiers modifiers, NPC target, Item item, Projectile proj)
        {
            int damage = 100; float knockback = 0f; bool crit = false;
            CSPlayerEffects.ModifyHitNPCWithAnything(Player, target, ref damage, ref knockback, ref crit, item, proj);
            modifiers.FinalDamage *= damage / 100f;
            if (crit) modifiers.SetCrit();
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            float speedX = velocity.X, speedY = velocity.Y;
            int type_ = type, damage_ = damage; float knockBack_ = knockBack;
            bool ret = CSPlayerEffects.Shoot(Player, item, ref position, ref speedX, ref speedY, ref type_, ref damage_, ref knockBack_);
            return ret;
        }
        public override void PostUpdateMiscEffects()
        {
            CSPlayerEffects.PostUpdateMiscEffects(Player);
            CSPlayerEffects.UpdateAccMinions(Player);
        }
        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            ApplyHurtModifiers(ref modifiers, npc, null);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            ApplyHurtModifiers(ref modifiers, null, proj);
        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            CSPlayerEffects.OnHitByAnything(Player, npc, null, hurtInfo.Damage, false);
        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            CSPlayerEffects.OnHitByAnything(Player, null, proj, hurtInfo.Damage, false);
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            int damage = 100, hitDirection = 0;
            bool crit = false, customDamage = false, playSound = true, genGore = true;
            PlayerDeathReason src = modifiers.DamageSource;
            bool notDodged = CSPlayerEffects.PreHurt(Player, modifiers.PvP, false, ref damage, ref hitDirection,
                ref crit, ref customDamage, ref playSound, ref genGore, ref src);
            if (CSWorld.HyperMode)
            {
                // 原 PreHurt 里 HyperMode 的增伤逻辑，用修饰符近似
                modifiers.FinalDamage *= 1.5f;
            }
            modifiers.FinalDamage *= damage / 100f;
            if (!notDodged) modifiers.FinalDamage *= 0f;   // 闪避：伤害归零
        }
        public override void PostHurt(Player.HurtInfo info)
        {
            CSPlayerEffects.PostHurt(Player, info.PvP, false, info.Damage, info.HitDirection, false);
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            CSPlayerEffects.PostHurt(Player, pvp, false, damage, hitDirection, false);
            return CSPlayerEffects.PreKill(Player, damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            ApplyNpcHitModifiers(ref modifiers, target, item, null);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            ApplyNpcHitModifiers(ref modifiers, target, null, proj);
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            CSPlayerEffects.OnHitNPCWithAnything(Player, target, damageDone, hit.Knockback, hit.Crit, null, item);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            CSPlayerEffects.OnHitNPCWithAnything(Player, target, damageDone, hit.Knockback, hit.Crit, proj);
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            float add = 0f, mult = 1f, flat = 0f;
            CSPlayerEffects.ModifyWeaponDamage(Player, item, ref add, ref mult, ref flat);
            damage.Base += add;
            damage *= mult;
            damage.Flat += flat;
        }
        public override void PostUpdate()
        {
            CSPlayerEffects.PostUpdate(Player);
        }
        public override void PostUpdateRunSpeeds()
        {
            CSPlayerEffects.PostUpdateRunSpeeds(Player);
        }
        public override void PreUpdateMovement()
        {
            CSPlayerEffects.PreUpdateMovement(Player);
        }
        #endregion
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (CalamitySouls.FearmongerArea.JustPressed && FearmongerCanArea && FearmongerAreaTimer == 0)
            {
                SoundEngine.PlaySound(SoundID.Item46, Player.Center);
                FearmongerAreaTimer = 30 * 60;
                if (Player.whoAmI == Main.myPlayer)
                {
                    Projectile area = Projectile.NewProjectileDirect(null, Player.Center, Vector2.Zero,
                           ProjectileType<FearmongerAreaProj>(), 0, 0f, Player.whoAmI);
                    area.Center = Player.Center;
                }
            }
            if (CalamitySouls.FearmongerMark.JustPressed && FearmongerCanMark)
            {
                if (FearmongerMark != -1) FearmongerMark = -1;
                else if (FearmongerMarkCooldown == 0 && Player.whoAmI == Main.myPlayer)
                {
                    int index = -1;
                    float dist = -1;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.friendly && npc.CanBeChasedBy() && (dist == -1 || npc.Distance(Main.MouseWorld) < dist))
                        {
                            index = i;
                            dist = npc.Distance(Main.MouseWorld);
                        }
                    }
                    if (index != -1)
                    {
                        FearmongerMark = index;
                        FearmongerMarkCooldown = 15 * 60;
                    }
                }
            }
            if (CanBrimFrenzy && BrimFrenzy == 0 && BrimFrenzyCooldown == 0 && CalamitySouls.BrimflameKey.JustPressed)
            {
                BrimFrenzyVisualEffect = 30;
                BrimFrenzy = 10 * 60;
                SoundEngine.PlaySound(new SoundStyle("CalamitySouls/Sounds/Custom/BrimflameAbility"), Player.Center);
                if (DespairSoul)
                {
                    SoundEngine.PlaySound(new SoundStyle("CalamitySouls/Sounds/Custom/BloodflareRangerActivation"), Player.Center);
                    for (int d = 0; d < 64; d++)
                    {
                        int dust2 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + 16f), Player.width, Player.height - 16, 60);
                        Main.dust[dust2].velocity *= 3f;
                        Main.dust[dust2].scale *= 1.15f;
                    }
                    int dustAmt = 36;
                    for (int d2 = 0; d2 < dustAmt; d2++)
                    {
                        Vector2 value2 = (Vector2.Normalize(Player.velocity) * new Vector2(Player.width / 2f, Player.height))
                            .RotatedBy(MathHelper.ToRadians((d2+ 1 - dustAmt / 2 )  / dustAmt)) * 0.75f + Player.Center;
                        Vector2 dustVel = value2 - Player.Center;
                        int phanto = Dust.NewDust(value2 + dustVel, 0, 0, 60, dustVel.X * 1.5f, dustVel.Y * 1.5f, 100, default, 1.4f);
                        Main.dust[phanto].noGravity = true;
                        Main.dust[phanto].noLight = true;
                        Main.dust[phanto].velocity = dustVel;
                    }
                    float spread = 0.783f;
                    float startAngle = Player.velocity.ToRotation() - (spread / 2f);
                    float deltaAngle = (spread / 8f);
                    int damage = (int)(1000f * Player.AverageDamage());
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            float ai = Utils.NextFloat(Main.rand) + 0.5f;
                            Vector2 randomSpeed = Vector2.UnitY * Main.rand.Next(1, 7);
                            Vector2 randomSpeed2 = Vector2.UnitY * Main.rand.Next(1, 7);
                            float offsetAngle = startAngle + deltaAngle * (k + k * k) / 2f + (32f * k);
                            Projectile.NewProjectile(null, Player.Center, offsetAngle.ToRotationVector2() * 5f + randomSpeed
                                , ProjectileType<CalamityMod.Projectiles.Ranged.BloodflareSoul>(), damage, 0f, Player.whoAmI, 0f, ai);
                            Projectile.NewProjectile(null, Player.Center, -offsetAngle.ToRotationVector2() * 5f + randomSpeed2,
                                ProjectileType<CalamityMod.Projectiles.Ranged.BloodflareSoul>(), damage, 0f, Player.whoAmI, 0f, ai);
                        }
                    }
                }
            }
        }
        public float WeaponFireRateBoost;
        public bool Frozen;
        public float WeaponKnockback;
        public float AllDamageByMult;
        // 1.4：灾厄 2.x 移除了 CalamityPlayer.canFireReaverRangedProjectile，自持一份
        public bool canFireReaverRangedProjectile;
        // 1.4：灾厄 2.x 移除了 CalamityPlayer.godSlayerDmg，这里自持一份
        public float godSlayerDmg;
        // 1.4 修复：以下两个效果在 1.3 里就是「每次命中 100% 触发」，而 1.4 里高命中率武器
        // （如原版波涌之刃 InfluxWaver，其剑光束会连续穿透/连击）会把它们线性放大成屏级弹幕。
        // 这里给每个效果加回一个独立 cooldown，只做频率上限，不改变效果本身。
        public int eleumShardCooldown;   // 冰川水晶碎片：每次命中最多 1 发，最短间隔 12 tick
        public int reaverSporeCooldown;  // 掠夺者孢子：每次命中最多 1 发，最短间隔 12 tick
        public float AdrenalineDamageBoost;
        public float MyAccelerationMult;
        public float MyMoveSpeedMult;
        public bool FearmongerFrozen;
        public float CritDamageMultipier;

        public float StealthEssense = 0;
        public float StealthEssenseMax = 100f;
        public float StealthEssenseMax2;
        public float StealthEssenseRegen;
        public int StealthEssenseTransformCooldown = 0;
        public int StealthEssenseRegenCooldown;
        public int StealthEssenseRegenCooldownCounter;
        //public float StealthEssensePickRange;
        //public float StealthEssenseAmountBonus;
        //public float StealthEssenseChance;

        public bool cCVaccines;
        public bool pPrism;
        public bool NoTrailPleaseEvenIfYouHaventDoneConfigPleaseDoSomethingToCloseTheTrail;
        public bool lGift;

        public bool CreatureSpore;
        public bool AnnihilationOption;
        #region Spell Effect
        public bool AquaticArea;
        public bool DespairSoul;
        public bool EleumCrystal;
        public bool GlobeBlade;
        public bool HeavenshadeStar;
        public bool PlagueHive;
        public bool StarlightBooster;
        #endregion
        #region Ench Effect
        public bool HasAeroBlade;
        public int AstralTimer = 0;
        public bool AuricFlash;
        public int AuricFlashCooldown = 0;
        public bool AuricHexNerf;
        public bool AuricWeaponBoost;
        public bool BloodCanOption;
        public bool CanBrimFrenzy;
        public int BrimFrenzy = 0;
        public int BrimFrenzyCooldown = 0;
        public int BrimFrenzyVisualEffect = 0;
        public bool DaedalusEvade;
        public int DaedalusEvadeCooldown = 0;
        public bool DaedalusCrystal;
        public bool DesertProwler;
        public bool DesertOverKillSandShark;
        public bool DesertDodge;
        public int DesertDodgeTimer = 0;
        public bool DesertDodgeBuff;
        public bool DesertDodgeCD;
        public bool EmpyreanProj;
        public bool EmpyreanSpectre;
        public bool FathomFish;
        public bool FearmongerNerfCancel;
        public bool FearmongerCanArea;
        public int FearmongerAreaTimer = 0;
        public bool FearmongerCanMark;
        public int FearmongerMark = -1;
        public int FearmongerMarkCooldown = 0;
        public bool GodslayerPhantom;
        public bool GodslayerBuff;
        public int GodslayerBuffTimer = 0;
        public bool GodslayerCrit;
        public bool GodslayerAncientCanOtherworld;
        public int GodslayerAncientOtherworldTimer = 0;
        public bool GodslayerAncientOtherworld;
        public bool CanHydrothermicRevive;
        public int HydrothermicReviveCooldown = 0;
        public bool HydrothermicEffect;
        public int MolluskPrefix = 0;
        public bool PlagueBringerPlague;
        public bool PlagueReaperPlague;
        public bool PlagueReaperFlight;
        public bool PlagueReaperBlind;
        public bool PlagueReaperAdrenaline;
        public bool PlagueReaperHasAdrenalineCurrently;
        public bool PrismaticLaser;
        public bool PrismaticTrail;
        public bool ReaverEffect;
        public bool ReaverPlantera;
        public bool SilvaBetterRevive;
        public int SilvaRechargeCounter = 0;
        public bool SnowFreeze;
        public bool StatigelSgod;
        public bool SulphurousCircle;
        public bool TarragonLeaves;
        public bool TarragonLeavesCoolDown;
        public bool TitanExplosion;
        public int TitanExplosionCounter = 0;
        public bool TitanInfection;
        public bool UmbraphileExplosion;
        public bool CanUmbraphileShealth;
        public int UmbraphileShealth = 0;
        public int VictideBlessingCooldown = 0;
        public bool HasWulfrumShield;
        public int WulfrumShieldCooldown = 0;
        #endregion
        #region Force
        public bool ForceAnnihilation;
        public bool ForceCreature;
        public bool ForceRevolution;
        public bool ForceTechnical;
        public bool ForceTyranny;
        #endregion
        #region Spell
        public bool SpellAquatic;
        public bool SpellBetrayal;
        public bool SpellDespair;
        public bool SpellEleum;
        public bool SpellGlobe;
        public bool SpellHeavenshade;
        public bool SpellOasis;
        public bool SpellPlague;
        public bool SpellStarlight;
        #endregion
        #region Ench
        public bool EnchAero;
        public bool EnchAstral;
        public bool EnchAuric;
        public bool EnchBlood;
        public bool EnchBrim;
        public bool EnchDaedalus;
        public bool EnchDemonshade;
        public bool EnchDesert;
        public bool EnchEmpyrean;
        public bool EnchFathom;
        public bool EnchFearmonger;
        public bool EnchGodslayer;
        public bool EnchGodslayerAncient;
        public bool EnchHydrothermic;
        public bool EnchMollusk;
        public bool EnchOmega;
        public bool EnchPlagueBringer;
        public bool EnchPlagueReaper;
        public bool EnchPrismatic;
        public bool EnchReaver;
        public bool EnchSilva;
        public bool EnchSnowRuffian;
        public bool EnchStatigel;
        public bool EnchSulphurous;
        public bool EnchTarragon;
        public bool EnchTitan;
        public bool EnchUmbraphile;
        public bool EnchVictide;
        public bool EnchWulfrum;
        #endregion
        #region Manic Only Accessories Effect
        public int sgFlightTime = 0;
        public static int sgFlightTimeMax = 30;
        public bool sgSliding;
        #endregion
        #region Manic Only Accessories
        public bool dsJaw;
        public bool sgCore;
        #endregion
        public bool RotI;
    }
}