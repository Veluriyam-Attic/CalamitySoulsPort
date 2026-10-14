// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 新版专用 ModPlayer。
// 旧版那一套是 CalamitySouls.CSPlayer，本类与它并存、互不影响。
// 字段照搬新版 1.3 源码 CSPlayer.cs，供新版内容使用。
// ============================================================================
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
    public class CSPlayerV104 : ModPlayer
    {
        public int closetNPCindex;
        public float WeaponFireRateBoost;
        public float WeaponKnockback;
        public float AllDamageByMult;
        public float AdrenalineDamageBoost;
        public float MyAccelerationMult;
        public float MyMoveSpeedMult;
        public bool FearmongerFrozen;
        public float CritDamageMultipier;
        public int SummonCrit = 0;
        public bool cCVaccines;
        public bool pTrail;
        public bool gsaShadow;
        public bool AcidivineSet;
        public bool AnnihilationEffect;
        public bool CreatureEffect;
        public bool RevolutionEffect;
        public bool TechnicalEffect;
        public bool TyrannyEffect;
        public bool AcidivineCircle;
        public bool AeroJump;
        public int AeroJumpEffect = 0;
        public float AeroJumpDirection;
        public int AstralFsTimer = 0;
        public bool AstralFallingStar;
        public bool AstralSatellite;
        public bool AstralBan;
        public bool AuricFlash;
        public bool AuricFlashCooldown;
        public bool BloodflareLostSouls;
        public bool BloodflareCalPetBuff;
        public bool CanBrimFrenzy;
        public bool BrimFrenzy;
        public bool BrimFrenzyCooldown;
        public bool BrimCalPet;
        public bool DaedalusEvade;
        public bool DaedalusEvadeCooldown;
        public bool DaedalusCrystal;
        public bool DesertProwler;
        public bool EmpyreanSpectre;
        public bool ExoShield;
        public bool ExoShieldActive;
        public bool ExterminationGravity;
        public bool FathomFish;
        public bool FearmongerNerfCancel;
        public bool FearmongerCanStopTime;
        public bool ForbiddenNerfCancel;
        public bool GodslayerDash;
        public int GodslayerDashingTimer = 0;
        public bool GodslayerDashing;
        public float GodslayerDashDirection = 0f;
        public bool GodslayerAncientTeleport;
        public bool HydrothermicFireWeapon;
        public bool HydrothermicSolarCircle;
        public bool LuxAstriPunch;
        public bool MolluskDrown;
        public bool MolluskDrown2;
        public bool OmegaTouch;
        public bool OmegaDefenseBreak;
        public bool PlagueBringerNuke;
        public int PlagueBringerNukeCD = 0;
        public bool PlagueReaperAdrenaline;
        public float PlagueReaperHasAdrenalineCurrently;
        public int PlagueReaperBlind = 0;
        public bool PrismaticPrism;
        public bool ReaverEffect;
        public byte ReaverEffectSelect = 0;
        public bool ReaverDefense;
        public bool ReaverSpeed;
        public bool ReaverExplore;
        public bool SilvaRevive;
        public int SilvaReviveCount = 0;
        public bool SilvaTrueInvincibility;
        public bool SnowFreeze;
        public bool StatigelArea;
        public bool SulphurousCircle;
        public bool SulphurousPoison;
        public bool TarragonLeaf;
        public bool TarragonLeafCanSpawn;
        public int[] TarragonLeafEffects = new int[8];
        public bool TarragonImmunity;
        public bool TitanExplosion;
        public int TitanHasteCooldown = 0;
        public bool CanUmbraphileShealth;
        public int UmbraphileShealth = 0;
        public bool VictideStarfish;
        public int VictideCooldown = 0;
        public bool WulfrumField;
        public int WulfrumFieldCooldown = 0;
        public int WulfrumFieldAnimationTimer = 0;
        public bool ForceAnnihilation;
        public bool ForceCreature;
        public bool ForceRevolution;
        public bool ForceTechnical;
        public bool ForceTyranny;
        public bool EnchAcidivine;
        public bool EnchAerospec;
        public bool EnchAstral;
        public bool EnchAuric;
        public bool EnchBloodFlare;
        public bool EnchBrimFlame;
        public bool EnchDaedalus;
        public bool EnchDemonshade;
        public bool EnchDesert;
        public bool EnchEmpyrean;
        public bool EnchExo;
        public bool EnchExtermination;
        public bool EnchFathom;
        public bool EnchFearmonger;
        public bool EnchForbidden;
        public bool EnchGodslayer;
        public bool EnchGodslayerAncient;
        public bool EnchHydrothermic;
        public bool EnchLuxAstri;
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
        public int sgFlightTime = 0;
        public bool sgSliding;
        public bool dsJaw;
        public bool sgCore;
        public bool RotI;

        // 每帧先把新版魔石的开关清空：装备中的魔石会在 UpdateAccessory 里重新点亮，
        // 与旧版 CSPlayer.EffectsThatNeedtoReset 的做法一致。
        // 1.3 CalamityLists.debuffList 的 29 项 —— 经名字解析后【全部为原版减益】（名字跨版本稳定，故按名字而非数字 ID 实现）
        // 顺序与 1.3 完全一致：20,22,23,24,30,31,32,33,35,36,39,44,46,47,67,68,69,70,80,144,148,149,156,160,164,195,196,197,203
        private static readonly int[] ClearDebuffList = new int[]
        {
            BuffID.Poisoned, BuffID.Darkness, BuffID.Cursed, BuffID.OnFire, BuffID.Bleeding, BuffID.Confused,
            BuffID.Slow, BuffID.Weak, BuffID.Silenced, BuffID.BrokenArmor, BuffID.CursedInferno, BuffID.Frostburn,
            BuffID.Chilled, BuffID.Frozen, BuffID.Burning, BuffID.Suffocation, BuffID.Ichor, BuffID.Venom,
            BuffID.Blackout, BuffID.Electrified, BuffID.Rabies, BuffID.Webbed, BuffID.Stoned, BuffID.Dazed,
            BuffID.VortexDebuff, BuffID.WitheredArmor, BuffID.WitheredWeapon, BuffID.OgreSpit, BuffID.BetsysCurse
        };
        // 弑神者·古 的受击传送闪避（1.4 的"完全闪避"钩子；逻辑见 CSPlayerV104Effects.GodslayerAncientFreeDodge）
        public override bool FreeDodge(Player.HurtInfo info) => CSPlayerV104Effects.GodslayerAncientFreeDodge(Player, info);

        // 始源林海：致命伤时复活（1.3 由 DoSilvaRevive 承担；1.4 的入口是 PreKill，返回值 false = 取消死亡）
        // 1.3 CSPlayer.PreUpdateBuffs:261-266 —— 复活的代价（心灵破碎 Buff + 伤害倍率 + 伤害减免惩罚）
        public override void PreUpdateBuffs()
        {
            // 1.3 CSPlayer.cs:260 —— 冻结中每隔帧维持"时光之殇"（2 帧）
            if (CSWorldV104.FearmongerTimeStop) Player.AddBuff(ModContent.BuffType<Buffs.FearmongerFrostbiteBuffV104>(), 2);
            // 1.3 CSPlayer.cs:700 逐值照搬：把"神惧者时间冻结"滤镜/天空的激活状态交给玩家视觉系统
            Player.ManageSpecialBiomeVisuals("CalamitySouls:FearmongerTimeFreeze", CSWorldV104.FearmongerTimeStop);
            // 1.3 CSPlayer.cs:401-412 逐值照搬：瘟疫收割者致盲时激活屏幕滤镜（本工程该标记为 bool，无 1.3 的倒计时）
            global::CalamitySouls.Effects.BlindRegister.SetActive(Player.CS().PlagueReaperBlind);
            if (SilvaReviveCount > 0)
            {
                Player.AddBuff(ModContent.BuffType<Buffs.SilvaPriceV104>(), 2);
                AllDamageByMult *= (float)System.Math.Pow(RevolutionEffect ? 0.85f : 0.7f, SilvaReviveCount);
                Player.endurance -= (RevolutionEffect ? 0.2f : 0.4f) * SilvaReviveCount;
            }
        }
        // 1.3 CSPlayerEffects:558 / :910 —— 无敌期间完全不被命中
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot) => !SilvaTrueInvincibility;
        public override bool CanBeHitByProjectile(Projectile proj) => !SilvaTrueInvincibility;

        // 龙蒿 8 叶效果 [0]：开火时额外撒叶片（1.3 CSPlayerEffects.cs:520-542 MyShoot 逐字）
		// 1.3 的 damage = player.GetWeaponDamage(item) ⇒ 1.4 Shoot 的 damage 参数 ✓；1.3 的 shootDir = 方向 × item.shootSpeed ⇒ 1.4 的 velocity 参数 ✓
		public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			CSPlayerV104 mplayer = Player.V104();
			if (mplayer.TarragonLeafEffects[0] > 0 && Main.rand.Next(5) == 0 && Player.whoAmI == Main.myPlayer)
			{
				int leafAmt = 8 + Main.rand.Next(3);
				int leafDamage = (int)(damage * 0.2f);
				for (int i = 0; i < leafAmt; i++)
				{
					float spreadMult = 0.025f * i;
					float xDiff = velocity.X + Main.rand.Next(-25, 26) * spreadMult;
					float yDiff = velocity.Y + Main.rand.Next(-25, 26) * spreadMult;
					int leaf = Projectile.NewProjectile(Player.GetSource_ItemUse(item), Player.Center, new Vector2(xDiff, yDiff), ProjectileID.Leaf, leafDamage, 5f, Player.whoAmI);
					if (leaf >= 0 && leaf < Main.projectile.Length) Main.projectile[leaf].Classless();   // 1.3: if (leaf.WithinBounds(1000)) Main.projectile[leaf].Classless();
				}
			}
			return true;
		}

		// 龙蒿 8 叶效果 [6]：受到敌人伤害减半（1.3 CSPlayerEffects.cs:477 ModifyHitByAnything 逐字）
		public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
		{
			if (Player.V104().TarragonLeafEffects[6] > 0) modifiers.FinalDamage *= 0.5f;   // 1.3: if (npc != null) damage /= 2
		}
		// 1.3 的同一个方法也覆盖"被弹幕打"的情形 ⇒ 两条钩子都要接（弹幕路径按 npcProj 判定，对应 1.3 的 npc != null）
		public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
		{
			if (Player.V104().TarragonLeafEffects[6] > 0 && proj.npcProj) modifiers.FinalDamage *= 0.5f;
		}

		// 龙蒿 8 叶效果 ⑬：8 片增益全部存在时免死（1.3 CSPlayerEffects.cs:900-909 与 :1188-1196 两处逐字）
		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (Player.V104().TarragonImmunity)
			{
				bool immu = true;
				foreach (int i in Player.V104().TarragonLeafEffects)
					if (i <= 0) { immu = false; break; }
				if (immu) return false;
			}
			return true;
		}
		public override void UpdateLifeRegen()
		{
			if (Player.V104().TarragonLeafEffects[5] > 0) Player.lifeRegen += 3;
		}

		public override void ResetEffects()
        {
            bool clearDebuff = false;   // 1.3 CSPlayer.cs:49（方法内局部变量）
            // ⭐ [修复 2026-10-11] 1.3 CSPlayer.cs:51 —— V104 整行漏移植 ✗
            //   后果：closetNPCindex 恒为默认值 0 ⇒ 依赖它的 4 个弹幕（皇天幻影 / 灾厄宠物 /
            //   弑神者幻影 / 欧米伽触手）会把 Main.npc[0] 当成"最近的敌人" ⇒ 没有敌人时也持续
            //   朝那个位置发动攻击 ⇒ 皇天幻影原地空转攻击（玩家实测：站着不动就不断冒紫色粉尘 ✓）
            closetNPCindex = CSUtils.ClosetNPC(Player.Center, -1);
            if (PlagueReaperBlind > 0) PlagueReaperBlind--;   // 1.3 CSPlayer.cs:118-120
            FearmongerFrozen = false;
            cCVaccines = false;
            pTrail = false;
            gsaShadow = false;
            AcidivineSet = false;
            AnnihilationEffect = false;
            CreatureEffect = false;
            RevolutionEffect = false;
            TechnicalEffect = false;
            TyrannyEffect = false;
            AcidivineCircle = false;
            AeroJump = false;
            if (AeroJumpEffect > 0) AeroJumpEffect--;   // 1.3 CSPlayer.cs:76（V104 原先整行缺失 ✗ ⇒ 天蓝冲刺用一次后永远回不来）
            if (AstralFsTimer > 0) AstralFsTimer--;     // 1.3 CSPlayer.cs:77（V104 原先整行缺失 ✗ ⇒ 群星天降只掉第一颗）
            AstralFallingStar = false;
            AstralSatellite = false;
            AstralBan = false;
            AuricFlash = false;
            AuricFlashCooldown = false;
            BloodflareLostSouls = false;
            BloodflareCalPetBuff = false;
            CanBrimFrenzy = false;
            BrimFrenzy = false;
            BrimFrenzyCooldown = false;
            BrimCalPet = false;
            DaedalusEvade = false;
            DaedalusEvadeCooldown = false;
            DaedalusCrystal = false;
            DesertProwler = false;
            EmpyreanSpectre = false;
            ExoShield = false;
            ExoShieldActive = false;
            ExterminationGravity = false;
            FathomFish = false;
            FearmongerNerfCancel = false;
            FearmongerCanStopTime = false;
            ForbiddenNerfCancel = false;
            GodslayerDash = false;
            // 1.3 每帧重置基准值（缺失会导致 runAcceleration/accRunSpeed/maxRunSpeed 被乘 0 ⇒ 无法移动 ✗）
            MyAccelerationMult = 1f;
            MyMoveSpeedMult = 1f;
            // 1.3 CSPlayer.cs:55-56 —— 同类"每帧基准值"（生成器只输出 bool/int，漏了 float 乘算基准 ✗）
            AllDamageByMult = 1f;          // 1.3 为 1f ✓（旧版也写 1f ✓）·不可用 0f ✗ 否则 *= 1.35f 之类永远为 0
            AdrenalineDamageBoost = 1f;    // 1.3 CSPlayer.cs:56 为 1f ✓（⚠️ 旧版写 0f ✗）⇒ 以 1.3 为准
            // 1.3 CSPlayer.cs:102-108 —— 冲刺计时（注意：GodslayerDashing 不在"每帧重置"里，而是在此按计时结束）
            if (GodslayerDashingTimer > 0) GodslayerDashingTimer--;
            else if (GodslayerDashing)
            {
                Player.velocity = GodslayerDashDirection.ToRotationVector2() * 15f;
                GodslayerDashing = false;
            }
            // 1.3 CSPlayer.cs:108 —— 冲刺期间清减益（另注：:130 的 SilvaTrueInvincibility 是始源林海那颗的同类触发点，届时同样接这里）
            if (GodslayerDashing) clearDebuff = true;
            if (SilvaTrueInvincibility) clearDebuff = true;   // 1.3 CSPlayer.cs:130（与弑神者共用同一套 29 项清减益机制）
            // 1.3 CSPlayer.cs:203-208 —— 对列表内每个减益授予免疫并清除（1.3 无任何撤销逻辑 ⇒ 永久免疫，属原版行为，差异表已注明）
            if (clearDebuff)
                foreach (int debuff in ClearDebuffList)
                {
                    Player.buffImmune[debuff] = true;
                    Player.ClearBuff(debuff);
                }
            // 1.3: if (GodslayerDashing) clearDebuff = true;（clearDebuff 是 1.3 方法内的局部变量，
            //   我方 V104 尚无该机制 → 待查清其消费点后补，见报告差异表）
            GodslayerAncientTeleport = false;
            HydrothermicFireWeapon = false;
            HydrothermicSolarCircle = false;
            LuxAstriPunch = false;
            // 1.3 CSPlayer.cs:142-143 —— 保留上一帧的"浸水"状态供 WetCollision 钩子判定
            MolluskDrown2 = MolluskDrown;
            MolluskDrown = false;
            OmegaTouch = false;
            OmegaDefenseBreak = false;
            PlagueBringerNuke = false;
            if (PlagueBringerNukeCD > 0) PlagueBringerNukeCD--;   // 1.3 CSPlayer.cs:116（V104 原先整行缺失 ✗ ⇒ 瘟疫核弹只发一次）
            PlagueReaperAdrenaline = false;
            PrismaticPrism = false;
            ReaverEffect = false;
            ReaverDefense = false;
            ReaverSpeed = false;
            ReaverExplore = false;
            SilvaRevive = false;
            SilvaTrueInvincibility = false;
            SnowFreeze = false;
            StatigelArea = false;
            SulphurousCircle = false;
            SulphurousPoison = false;
            if (Player.miscCounter % 30 == 0) TarragonLeafCanSpawn = true;   // 1.3 CSPlayer.cs:135
            for (int i = 0; i < TarragonLeafEffects.Length; i++)   // 1.3 :136-139
            	if (TarragonLeafEffects[i] > 0)
            		if (TarragonLeaf) TarragonLeafEffects[i]--;
            		else TarragonLeafEffects[i] = 0;
            TarragonLeaf = false;
            TarragonImmunity = false;
            TitanExplosion = false;
            CanUmbraphileShealth = false;
            VictideStarfish = false;
            WulfrumField = false;
            if (WulfrumFieldCooldown > 0) WulfrumFieldCooldown--;   // 1.3 CSPlayer.cs:147
            WulfrumFieldAnimationTimer++;
            if (WulfrumFieldAnimationTimer > 12 * 8) WulfrumFieldAnimationTimer = 0;   // 1.3 CSPlayer.cs:148-149
            ForceAnnihilation = false;
            ForceCreature = false;
            ForceRevolution = false;
            ForceTechnical = false;
            ForceTyranny = false;
            EnchAcidivine = false;
            EnchAerospec = false;
            EnchAstral = false;
            EnchAuric = false;
            EnchBloodFlare = false;
            EnchBrimFlame = false;
            EnchDaedalus = false;
            EnchDemonshade = false;
            EnchDesert = false;
            EnchEmpyrean = false;
            EnchExo = false;
            EnchExtermination = false;
            EnchFathom = false;
            EnchFearmonger = false;
            EnchForbidden = false;
            EnchGodslayer = false;
            EnchGodslayerAncient = false;
            EnchHydrothermic = false;
            EnchLuxAstri = false;
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
            sgSliding = false;
            dsJaw = false;
            sgCore = false;
            RotI = false;
        }

        public override void UpdateDead()
        {
            ResetEffects();
        }

        // ================= 「新版」效果层接线 =================
        // 只驱动 Content/V104 下自己的效果实现，绝不触碰旧版 CSPlayer / CSPlayerEffects。
        public override void PostUpdateEquips()
        {
            CSPlayerV104Effects.PostUpdateEquips(Player);
        }

        public override void PostUpdateMiscEffects()
        {
            CSPlayerV104Effects.PostUpdateMiscEffects(Player);
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            CSPlayerV104Effects.PostHurt(Player);
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            CSPlayerV104Effects.ModifyHurt(Player, ref modifiers);
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            CSPlayerV104Effects.ModifyWeaponDamage(Player, item, ref damage);
        }

        // ⭐ [2026-10-11] 1.3 CSPlayerDrawEffect.cs:22-36 的钩子位置 —— 关掉 7 个原版描边/阴影标志。
        //    1.3 是在 ModPlayer.ModifyDrawInfo 里做的（必须在玩家本体绘制【之前】生效）；tML 1.4 的
        //    ModPlayer.ModifyDrawInfo(ref PlayerDrawSet) 就是同一个钩子（仅签名类型从 PlayerDrawInfo 变成 PlayerDrawSet）。
        //    ⚠️ 这一段必须留在【绘制前】的钩子里：若挪到绘制层里（尤其挪到最后一层）就变成下一帧才生效 ⇒ 描边会多画一帧。
        public override void ModifyDrawInfo(ref Terraria.DataStructures.PlayerDrawSet drawInfo)
        {
            if (!gsaShadow) return;
            Player.armorEffectDrawOutlines = false;
            Player.armorEffectDrawOutlinesForbidden = false;
            Player.armorEffectDrawShadow = false;
            Player.armorEffectDrawShadowBasilisk = false;
            Player.armorEffectDrawShadowEOCShield = false;
            Player.armorEffectDrawShadowLokis = false;
            Player.armorEffectDrawShadowSubtle = false;
        }

        public override void PostUpdateRunSpeeds()
        {
            CSPlayerV104Effects.PostUpdateRunSpeeds(Player);
        }

        public override void PostUpdate()
        {
            CSPlayerV104Effects.PostUpdate(Player);
            // 1.3 CSPlayer.cs:222 —— 始源林海：已死亡或重生计时中则复活（1.4 沿用同一触发条件）
            if (SilvaRevive && (Player.dead || Player.respawnTimer > 0))
                CSPlayerV104Effects.DoSilvaReviveV104(Player, this);
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            CSPlayerV104Effects.ModifyHitNPCWithItem(Player, target, ref modifiers);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            CSPlayerV104Effects.OnHitNPCWithItem(Player, target, damageDone);
        }

        // 1.4 里 ModKeybind.JustPressed 只在 ProcessTriggers 内可靠
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            // 1.3 CSPlayer.cs:428-432 —— 神惧者时间冻结热键 K
            if (CalamitySouls.FearmongerTimeFreezeKey != null && CalamitySouls.FearmongerTimeFreezeKey.JustPressed && FearmongerCanStopTime)
            {
                SoundEngine.PlaySound(SoundID.NPCHit5, Player.Center);
                CSWorldV104.FearmongerTimeStop = !CSWorldV104.FearmongerTimeStop;
            }
            if (CalamitySouls.GodslayerDashKey != null && CalamitySouls.GodslayerDashKey.JustPressed)
            {
                CSPlayerV104Effects.GodslayerDashTrigger(Player);
            }
            if (CalamitySouls.BrimflameKey != null && CalamitySouls.BrimflameKey.JustPressed)
            {
                CSPlayerV104Effects.BrimFrenzyKeyTrigger(Player);
            }
            if (CalamitySouls.AeroDashKey != null && CalamitySouls.AeroDashKey.JustPressed)
            {
                CSPlayerV104Effects.AeroDashTrigger(Player);
            }
            if (CalamitySouls.ReaverKey != null && CalamitySouls.ReaverKey.JustPressed)
            {
                CSPlayerV104Effects.ReaverSwitchKey(Player);
            }
            if (CalamitySouls.DemonshadeEnrageKey != null && CalamitySouls.DemonshadeEnrageKey.JustPressed)
            {
                CSPlayerV104Effects.DemonshadeEnrage(Player);
            }
        }
    }
}
