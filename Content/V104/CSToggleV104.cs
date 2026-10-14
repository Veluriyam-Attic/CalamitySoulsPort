using Terraria.ModLoader;
using Terraria.ID;

// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」运行期开关快照（调用点统一读这里，避免每帧访问 ModConfig）。
// 覆盖：34 颗新版魔石 + 5 个新版之力 + 16 个新版之魂 + 新旧魂互斥总开关。
// ============================================================================
namespace CalamitySouls.Content.V104
{
    public static class CSToggleV104
    {
        // =====================================================================================
        // 运行期开关快照：与 CSConfigV104 的属性一一对应（由 fix-cstoggle.js 依据配置重建）
        // 二级开关（魂的子项效果）机制：见下方 SoulSubToggles / GetSoulSubToggle
        //   缺键 => 默认开启（true）=> 行为与未启用二级开关时完全一致
        // =====================================================================================
        public static bool AllowMixedSouls = false;
        public static bool EnchAcidivine = true;
        public static bool EnchAerospec = true;
        public static bool EnchAstral = true;
        public static bool EnchAuric = true;
        public static bool EnchBloodFlare = true;
        public static bool EnchBrimFlame = true;
        public static bool EnchDaedalus = true;
        public static bool EnchDemonshade = true;
        public static bool EnchDesert = true;
        public static bool EnchEmpyrean = true;
        public static bool EnchExo = true;
        public static bool EnchExtermination = true;
        public static bool EnchFathom = true;
        public static bool EnchFearmonger = true;
        public static bool EnchForbidden = true;
        public static bool EnchGodslayerAncient = true;
        public static bool EnchGodslayer = true;
        public static bool EnchHydrothermic = true;
        public static bool EnchLuxAstri = true;
        public static bool EnchMollusk = true;
        public static bool EnchOmega = true;
        public static bool EnchPlagueReaper = true;
        public static bool EnchPlagueBringer = true;
        public static bool EnchPrismatic = true;
        public static bool EnchReaver = true;
        public static bool EnchSilva = true;
        public static bool EnchSnowRuffian = true;
        public static bool EnchStatigel = true;
        public static bool EnchSulphurous = true;
        public static bool EnchTarragon = true;
        public static bool EnchTitan = true;
        public static bool EnchUmbraphile = true;
        public static bool EnchVictide = true;
        public static bool EnchWulfrum = true;
        public static bool ForceAnnihilation = true;
        public static bool ForceCreature = true;
        public static bool ForceRevolution = true;
        public static bool ForceTechnical = true;
        public static bool ForceTyranny = true;
        public static bool SoulCalamityV104 = true;
        public static bool SoulCommunitiousV104 = true;
        public static bool SoulDautlessiousV104 = true;
        public static bool SoulDevotiousV104 = true;
        public static bool SoulExpeditiousV104 = true;
        public static bool SoulHornoriousV104 = true;
        public static bool SoulInfinityV104 = true;
        public static bool SoulLeaderiousV104 = true;
        public static bool SoulMenaciousV104 = true;
        public static bool SoulMysteriousV104 = true;
        public static bool SoulPreciousV104 = true;
        public static bool SoulReclusiousV104 = true;
        public static bool SoulSparkliousV104 = true;
        public static bool SoulUnitiousV104 = true;
        public static bool SoulUnlimityV104 = true;
        public static bool SoulVelocitiousV104 = true;

        private static readonly System.Collections.Generic.Dictionary<string, bool> SoulSubToggles
            = new System.Collections.Generic.Dictionary<string, bool>();

        // level-2 soul sub-toggle: per-soul methods (design B) are added batch by batch.
        // For now only the mechanism exists => every pair defaults to enabled.
        // 魂名 -> 静态开关（由 fix-cstoggle.js 依据配置/魂表重建）
        public static bool GetSoulToggle(string toggleName)
        {
            switch (toggleName)
            {
                case "SoulCalamityV104": return SoulCalamityV104;
                case "SoulCommunitiousV104": return SoulCommunitiousV104;
                case "SoulDautlessiousV104": return SoulDautlessiousV104;
                case "SoulDevotiousV104": return SoulDevotiousV104;
                case "SoulExpeditiousV104": return SoulExpeditiousV104;
                case "SoulHornoriousV104": return SoulHornoriousV104;
                case "SoulInfinityV104": return SoulInfinityV104;
                case "SoulLeaderiousV104": return SoulLeaderiousV104;
                case "SoulMenaciousV104": return SoulMenaciousV104;
                case "SoulMysteriousV104": return SoulMysteriousV104;
                case "SoulPreciousV104": return SoulPreciousV104;
                case "SoulReclusiousV104": return SoulReclusiousV104;
                case "SoulSparkliousV104": return SoulSparkliousV104;
                case "SoulUnitiousV104": return SoulUnitiousV104;
                case "SoulUnlimityV104": return SoulUnlimityV104;
                case "SoulVelocitiousV104": return SoulVelocitiousV104;
                default: return true;   // 未登记的魂 => 默认开启
            }
        }

		// [self-check] Declared batch: souls whose level-2 sub-toggles are DONE (regenerated).
		internal static readonly string[] DeclaredSubToggleSouls = new string[]
		{
			"SoulCommunitiousV104",
			"SoulDautlessiousV104",
			"SoulDevotiousV104",
			"SoulExpeditiousV104",
			"SoulHornoriousV104",
			"SoulLeaderiousV104",
			"SoulMenaciousV104",
			"SoulMysteriousV104",
			"SoulPreciousV104",
			"SoulReclusiousV104",
			"SoulSparkliousV104",
			"SoulUnitiousV104",
			"SoulVelocitiousV104",
		};

		private static bool Sub_SoulCommunitious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.StressPills>()) return c.SoulCommunitious_StressPills;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.SandCloak>()) return c.SoulCommunitious_SandCloak;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TheTransformer>()) return c.SoulCommunitious_TheTransformer;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TheBee>()) return c.SoulCommunitious_TheBee;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.DeepDiver>()) return c.SoulCommunitious_DeepDiver;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.BloomStone>()) return c.SoulCommunitious_BloomStone;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TheCommunity>()) return c.SoulCommunitious_TheCommunity;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.Affliction>()) return c.SoulCommunitious_Affliction;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.YharimsGift>()) return c.SoulCommunitious_YharimsGift;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulDautlessious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.HeartofDarkness>()) return c.SoulDautlessious_HeartofDarkness;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.OldDie>()) return c.SoulDautlessious_OldDie;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.ReaperToothNecklace>()) return c.SoulDautlessious_ReaperToothNecklace;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.OldDukeScales>()) return c.SoulDautlessious_OldDukeScales;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TheAmalgam>()) return c.SoulDautlessious_TheAmalgam;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.ShatteredCommunity>()) return c.SoulDautlessious_ShatteredCommunity;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulDevotious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.Laudanum>()) return c.SoulDevotious_Laudanum;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.GiantPearl>()) return c.SoulDevotious_GiantPearl;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.AquaticEmblem>()) return c.SoulDevotious_AquaticEmblem;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.GiantTortoiseShell>()) return c.SoulDevotious_GiantTortoiseShell;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.AmbrosialAmpoule>()) return c.SoulDevotious_AmbrosialAmpoule;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.Regenerator>()) return c.SoulDevotious_Regenerator;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TheCamper>()) return c.SoulDevotious_TheCamper;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.BloodflareCore>()) return c.SoulDevotious_BloodflareCore;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TheEvolution>()) return c.SoulDevotious_TheEvolution;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.RampartofDeities>()) return c.SoulDevotious_RampartofDeities;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TheSponge>()) return c.SoulDevotious_TheSponge;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.DraedonsHeart>()) return c.SoulDevotious_DraedonsHeart;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulExpeditious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.AlluringBait>()) return c.SoulExpeditious_AlluringBait;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.OceanCrest>()) return c.SoulExpeditious_OceanCrest;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.EnchantedPearl>()) return c.SoulExpeditious_EnchantedPearl;
			if (type == ModContent.ItemType<CalamityMod.Items.Pets.ThiefsDime>()) return c.SoulExpeditious_ThiefsDime;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.SupremeBaitTackleBoxFishingStation>()) return c.SoulExpeditious_SupremeBaitTackleBoxFishingStation;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.AbyssalDivingSuit>()) return c.SoulExpeditious_AbyssalDivingSuit;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulHornorious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.FungalSymbiote>()) return c.SoulHornorious_FungalSymbiote;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.ElementalGauntlet>()) return c.SoulHornorious_ElementalGauntlet;
			if (type == ItemID.YoyoBag) return c.SoulHornorious_YoyoBag;
			if (type == ItemID.CelestialShell) return c.SoulHornorious_CelestialShell;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulLeaderious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.GladiatorsLocket>()) return c.SoulLeaderious_GladiatorsLocket;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.FungalClump>()) return c.SoulLeaderious_FungalClump;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.HowlsHeart>()) return c.SoulLeaderious_HowlsHeart;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.CryoStone>()) return c.SoulLeaderious_CryoStone;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.HeartoftheElements>()) return c.SoulLeaderious_HeartoftheElements;
			if (type == ModContent.ItemType<CalamityMod.Items.Weapons.Summon.MutatedTruffle>()) return c.SoulLeaderious_MutatedTruffle;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.Calamity>()) return c.SoulLeaderious_Calamity;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulMenacious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.TrinketofChi>()) return c.SoulMenacious_TrinketofChi;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.FrostFlare>()) return c.SoulMenacious_FrostFlare;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.LeviathanAmbergris>()) return c.SoulMenacious_LeviathanAmbergris;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.HideofAstrumDeus>()) return c.SoulMenacious_HideofAstrumDeus;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.BlunderBooster>()) return c.SoulMenacious_BlunderBooster;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.NebulousCore>()) return c.SoulMenacious_NebulousCore;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.ChaliceOfTheBloodGod>()) return c.SoulMenacious_ChaliceOfTheBloodGod;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.DarkSunRing>()) return c.SoulMenacious_DarkSunRing;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulMysterious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.PermafrostsConcoction>()) return c.SoulMysterious_PermafrostsConcoction;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.ChaosStone>()) return c.SoulMysterious_ChaosStone;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.EtherealTalisman>()) return c.SoulMysterious_EtherealTalisman;
			if (type == ItemID.CelestialCuffs) return c.SoulMysterious_CelestialCuffs;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulPrecious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.DynamoStemCells>()) return c.SoulPrecious_DynamoStemCells;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.DaawnlightSpiritOrigin>()) return c.SoulPrecious_DaawnlightSpiritOrigin;
			if (type == ItemID.Hoverboard) return c.SoulPrecious_Hoverboard;
			if (type == ItemID.SniperScope) return c.SoulPrecious_SniperScope;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulReclusious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.RottenDogtooth>()) return c.SoulReclusious_RottenDogtooth;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.GloveOfPrecision>()) return c.SoulReclusious_GloveOfPrecision;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.GloveOfRecklessness>()) return c.SoulReclusious_GloveOfRecklessness;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.VampiricTalisman>()) return c.SoulReclusious_VampiricTalisman;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.Nanotech>()) return c.SoulReclusious_Nanotech;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.EclipseMirror>()) return c.SoulReclusious_EclipseMirror;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulSparklious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.ScuttlersJewel>()) return c.SoulSparklious_ScuttlersJewel;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.RuinMedallion>()) return c.SoulSparklious_RuinMedallion;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.AmidiasPendant>()) return c.SoulSparklious_AmidiasPendant;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.CorrosiveSpine>()) return c.SoulSparklious_CorrosiveSpine;
			if (type == ModContent.ItemType<CalamityMod.Items.Tools.MomentumCapacitor>()) return c.SoulSparklious_MomentumCapacitor;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.EtherealExtorter>()) return c.SoulSparklious_EtherealExtorter;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.VeneratedLocket>()) return c.SoulSparklious_VeneratedLocket;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.DragonScales>()) return c.SoulSparklious_DragonScales;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulUnitious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.PhantomicArtifact>()) return c.SoulUnitious_PhantomicArtifact;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.Nucleogenesis>()) return c.SoulUnitious_Nucleogenesis;
			if (type == ItemID.HuntressBuckler) return c.SoulUnitious_HuntressBuckler;
			return true;   // 未绑定 => 默认开启
		}

		private static bool Sub_SoulVelocitious(int type, CSConfigV104 c)
		{
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.AeroStone>()) return c.SoulVelocitious_AeroStone;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.EvasionScarf>()) return c.SoulVelocitious_EvasionScarf;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.SpectralVeil>()) return c.SoulVelocitious_SpectralVeil;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.StatisVoidSash>()) return c.SoulVelocitious_StatisVoidSash;
			if (type == ModContent.ItemType<CalamityMod.Items.Accessories.AsgardianAegis>()) return c.SoulVelocitious_AsgardianAegis;
			return true;   // 未绑定 => 默认开启
		}

		private static CSConfigV104 cfgCache;
		private static CSConfigV104 GetCfg()
		{
			if (cfgCache == null) cfgCache = ModContent.GetInstance<CSConfigV104>();
			return cfgCache;
		}

		internal static bool DispatchSubToggle(string soul, int type)
		{
			CSConfigV104 c = GetCfg();
			switch (soul)
			{
				case "SoulCommunitiousV104": return Sub_SoulCommunitious(type, c);
				case "SoulDautlessiousV104": return Sub_SoulDautlessious(type, c);
				case "SoulDevotiousV104": return Sub_SoulDevotious(type, c);
				case "SoulExpeditiousV104": return Sub_SoulExpeditious(type, c);
				case "SoulHornoriousV104": return Sub_SoulHornorious(type, c);
				case "SoulLeaderiousV104": return Sub_SoulLeaderious(type, c);
				case "SoulMenaciousV104": return Sub_SoulMenacious(type, c);
				case "SoulMysteriousV104": return Sub_SoulMysterious(type, c);
				case "SoulPreciousV104": return Sub_SoulPrecious(type, c);
				case "SoulReclusiousV104": return Sub_SoulReclusious(type, c);
				case "SoulSparkliousV104": return Sub_SoulSparklious(type, c);
				case "SoulUnitiousV104": return Sub_SoulUnitious(type, c);
				case "SoulVelocitiousV104": return Sub_SoulVelocitious(type, c);
				default: return true;   // 未纳入二级开关 => 全开
			}
		}






        public static bool GetSoulSubToggle(string soulName, int itemType)
        {
            string key = soulName + "|" + itemType;
            bool v;
			return DispatchSubToggle(soulName, itemType);   // 第1批绑定（配置已有属性为准）
        }

        public static void SetSoulSubToggle(string soulName, int itemType, bool enabled)
        {
            SoulSubToggles[soulName + "|" + itemType] = enabled;
        }
    }
}
