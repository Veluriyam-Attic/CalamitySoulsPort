// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」独立配置页（与旧版 CSConfig 是两个页面）。
//   · 每颗新版魔石一个开关（34）
//   · 每个新版之力一个开关（5）
//   · 每个新版之魂一个开关（16）
//   · 第三栏：是否允许新旧魂同时装备 —— 默认 false（不允许）；只限制最顶层的"魂"，
//     魔石 / 饰品 / 之力 可以随意混搭。
// ============================================================================
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace CalamitySouls.Content.V104
{
    [BackgroundColor(32, 40, 52, 216)]
    public class CSConfigV104 : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => Sync();
        public override void OnChanged() => Sync();

        private void Sync()
        {
            CSToggleV104.EnchAcidivine = EnchAcidivine;
            CSToggleV104.EnchAerospec = EnchAerospec;
            CSToggleV104.EnchAstral = EnchAstral;
            CSToggleV104.EnchAuric = EnchAuric;
            CSToggleV104.EnchBloodFlare = EnchBloodFlare;
            CSToggleV104.EnchBrimFlame = EnchBrimFlame;
            CSToggleV104.EnchDaedalus = EnchDaedalus;
            CSToggleV104.EnchDemonshade = EnchDemonshade;
            CSToggleV104.EnchDesert = EnchDesert;
            CSToggleV104.EnchEmpyrean = EnchEmpyrean;
            CSToggleV104.EnchExo = EnchExo;
            CSToggleV104.EnchExtermination = EnchExtermination;
            CSToggleV104.EnchFathom = EnchFathom;
            CSToggleV104.EnchFearmonger = EnchFearmonger;
            CSToggleV104.EnchForbidden = EnchForbidden;
            CSToggleV104.EnchGodslayerAncient = EnchGodslayerAncient;
            CSToggleV104.EnchGodslayer = EnchGodslayer;
            CSToggleV104.EnchHydrothermic = EnchHydrothermic;
            CSToggleV104.EnchLuxAstri = EnchLuxAstri;
            CSToggleV104.EnchMollusk = EnchMollusk;
            CSToggleV104.EnchOmega = EnchOmega;
            CSToggleV104.EnchPlagueReaper = EnchPlagueReaper;
            CSToggleV104.EnchPlagueBringer = EnchPlagueBringer;
            CSToggleV104.EnchPrismatic = EnchPrismatic;
            CSToggleV104.EnchReaver = EnchReaver;
            CSToggleV104.EnchSilva = EnchSilva;
            CSToggleV104.EnchSnowRuffian = EnchSnowRuffian;
            CSToggleV104.EnchStatigel = EnchStatigel;
            CSToggleV104.EnchSulphurous = EnchSulphurous;
            CSToggleV104.EnchTarragon = EnchTarragon;
            CSToggleV104.EnchTitan = EnchTitan;
            CSToggleV104.EnchUmbraphile = EnchUmbraphile;
            CSToggleV104.EnchVictide = EnchVictide;
            CSToggleV104.EnchWulfrum = EnchWulfrum;
            CSToggleV104.ForceAnnihilation = ForceAnnihilation;
            CSToggleV104.ForceCreature = ForceCreature;
            CSToggleV104.ForceRevolution = ForceRevolution;
            CSToggleV104.ForceTechnical = ForceTechnical;
            CSToggleV104.ForceTyranny = ForceTyranny;
            CSToggleV104.SoulCalamityV104 = SoulCalamityV104;
            CSToggleV104.SoulCommunitiousV104 = SoulCommunitiousV104;
            CSToggleV104.SoulDautlessiousV104 = SoulDautlessiousV104;
            CSToggleV104.SoulDevotiousV104 = SoulDevotiousV104;
            CSToggleV104.SoulExpeditiousV104 = SoulExpeditiousV104;
            CSToggleV104.SoulHornoriousV104 = SoulHornoriousV104;
            CSToggleV104.SoulInfinityV104 = SoulInfinityV104;
            CSToggleV104.SoulLeaderiousV104 = SoulLeaderiousV104;
            CSToggleV104.SoulMenaciousV104 = SoulMenaciousV104;
            CSToggleV104.SoulMysteriousV104 = SoulMysteriousV104;
            CSToggleV104.SoulPreciousV104 = SoulPreciousV104;
            CSToggleV104.SoulReclusiousV104 = SoulReclusiousV104;
            CSToggleV104.SoulSparkliousV104 = SoulSparkliousV104;
            CSToggleV104.SoulUnitiousV104 = SoulUnitiousV104;
            CSToggleV104.SoulUnlimityV104 = SoulUnlimityV104;
            CSToggleV104.SoulVelocitiousV104 = SoulVelocitiousV104;
        }
        // ================= 新版魔石 =================
        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.NewEnch")]
        [Label("圣蚀魔石（新版）")]
        [Tooltip("是否启用「圣蚀魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchAcidivine { get; set; } = true;

        [Label("天蓝魔石（新版）")]
        [Tooltip("是否启用「天蓝魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchAerospec { get; set; } = true;

        [Label("幻星魔石（新版）")]
        [Tooltip("是否启用「幻星魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchAstral { get; set; } = true;

        [Label("金源魔石（新版）")]
        [Tooltip("是否启用「金源魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchAuric { get; set; } = true;

        [Label("血炎魔石（新版）")]
        [Tooltip("是否启用「血炎魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchBloodFlare { get; set; } = true;

        [Label("硫火魔石（新版）")]
        [Tooltip("是否启用「硫火魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchBrimFlame { get; set; } = true;

        [Label("代达罗斯魔石（新版）")]
        [Tooltip("是否启用「代达罗斯魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchDaedalus { get; set; } = true;

        [Label("魔影魔石（新版）")]
        [Tooltip("是否启用「魔影魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchDemonshade { get; set; } = true;

        [Label("沙漠游侠魔石（新版）")]
        [Tooltip("是否启用「沙漠游侠魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchDesert { get; set; } = true;

        [Label("皇天魔石（新版）")]
        [Tooltip("是否启用「皇天魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchEmpyrean { get; set; } = true;

        [Label("星流魔石（新版）")]
        [Tooltip("是否启用「星流魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchExo { get; set; } = true;

        [Label("绝灭魔石（新版）")]
        [Tooltip("是否启用「绝灭魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchExtermination { get; set; } = true;

        [Label("幻渊鱼群魔石（新版）")]
        [Tooltip("是否启用「幻渊鱼群魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchFathom { get; set; } = true;

        [Label("神惧者魔石（新版）")]
        [Tooltip("是否启用「神惧者魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchFearmonger { get; set; } = true;

        [Label("禁戒魔石（新版）")]
        [Tooltip("是否启用「禁戒魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchForbidden { get; set; } = true;

        [Label("远古弑神者魔石（新版）")]
        [Tooltip("是否启用「远古弑神者魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchGodslayerAncient { get; set; } = true;

        [Label("弑神者魔石（新版）")]
        [Tooltip("是否启用「弑神者魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchGodslayer { get; set; } = true;

        [Label("渊泉魔石（新版）")]
        [Tooltip("是否启用「渊泉魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchHydrothermic { get; set; } = true;

        [Label("星光魔石（新版）")]
        [Tooltip("是否启用「星光魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchLuxAstri { get; set; } = true;

        [Label("软壳魔石（新版）")]
        [Tooltip("是否启用「软壳魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchMollusk { get; set; } = true;

        [Label("欧米伽蓝魔石（新版）")]
        [Tooltip("是否启用「欧米伽蓝魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchOmega { get; set; } = true;

        [Label("瘟疫收割者魔石（新版）")]
        [Tooltip("是否启用「瘟疫收割者魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchPlagueReaper { get; set; } = true;

        [Label("瘟疫使者魔石（新版）")]
        [Tooltip("是否启用「瘟疫使者魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchPlagueBringer { get; set; } = true;

        [Label("光棱魔石（新版）")]
        [Tooltip("是否启用「光棱魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchPrismatic { get; set; } = true;

        [Label("掠夺者魔石（新版）")]
        [Tooltip("是否启用「掠夺者魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchReaver { get; set; } = true;

        [Label("始源林海魔石（新版）")]
        [Tooltip("是否启用「始源林海魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchSilva { get; set; } = true;

        [Label("雪境暴徒魔石（新版）")]
        [Tooltip("是否启用「雪境暴徒魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchSnowRuffian { get; set; } = true;

        [Label("斯塔提斯魔石（新版）")]
        [Tooltip("是否启用「斯塔提斯魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchStatigel { get; set; } = true;

        [Label("硫磺魔石（新版）")]
        [Tooltip("是否启用「硫磺魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchSulphurous { get; set; } = true;

        [Label("龙蒿魔石（新版）")]
        [Tooltip("是否启用「龙蒿魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchTarragon { get; set; } = true;

        [Label("泰坦之心魔石（新版）")]
        [Tooltip("是否启用「泰坦之心魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchTitan { get; set; } = true;

        [Label("日影魔石（新版）")]
        [Tooltip("是否启用「日影魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchUmbraphile { get; set; } = true;

        [Label("胜潮魔石（新版）")]
        [Tooltip("是否启用「胜潮魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchVictide { get; set; } = true;

        [Label("钨钢魔石（新版）")]
        [Tooltip("是否启用「钨钢魔石（新版）」的效果")]
        [DefaultValue(true)]
        public bool EnchWulfrum { get; set; } = true;

        // ================= 新版之力 =================
        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.NewForces")]
        [Label("湮灭之力（新版）")]
        [Tooltip("是否启用「湮灭之力（新版）」的效果")]
        [DefaultValue(true)]
        public bool ForceAnnihilation { get; set; } = true;

        [Label("生灵之力（新版）")]
        [Tooltip("是否启用「生灵之力（新版）」的效果")]
        [DefaultValue(true)]
        public bool ForceCreature { get; set; } = true;

        [Label("起义之力（新版）")]
        [Tooltip("是否启用「起义之力（新版）」的效果")]
        [DefaultValue(true)]
        public bool ForceRevolution { get; set; } = true;

        [Label("科技之力（新版）")]
        [Tooltip("是否启用「科技之力（新版）」的效果")]
        [DefaultValue(true)]
        public bool ForceTechnical { get; set; } = true;

        [Label("暴政之力（新版）")]
        [Tooltip("是否启用「暴政之力（新版）」的效果")]
        [DefaultValue(true)]
        public bool ForceTyranny { get; set; } = true;

        // ============================================================================
        // 二级开关（魂的子项效果）—— 第 1 批：全能 / 无畏 / 荣光 / 精准（共 19 项）
        // 机制：魂会把 MaterialList 里每件饰品的 UpdateAccessory 挨个跑一遍
        //       （V104SoulBase:63 → CSUtils.UpdateAccessory → ItemLoader.UpdateAccessory）
        //       ⇒ 每个开关 = "跳过该件饰品的那一次调用"（按魂分别作用；不影响力/魔石/玩家真装备）
        // ⚠️ 译名：部分为【本移植自译】（非官方译名），差异表已标注
        // ============================================================================
        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Communitious")]
        [Label("压力药丸")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_StressPills { get; set; } = true;

        [Label("沙尘披风")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_SandCloak { get; set; } = true;

        [Label("转换器")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_TheTransformer { get; set; } = true;

        [Label("蜜蜂护符")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_TheBee { get; set; } = true;

        [Label("深潜者")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_DeepDiver { get; set; } = true;

        [Label("绽花石")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_BloomStone { get; set; } = true;

        [Label("归一")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_TheCommunity { get; set; } = true;

        [Label("灾劫之尖啸")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_Affliction { get; set; } = true;

        [Label("寻神者之礼")]
        [Tooltip("是否启用「全能之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitious_YharimsGift { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Dautlessious")]
        [Label("暗影之心")]
        [Tooltip("是否启用「无畏之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDautlessious_HeartofDarkness { get; set; } = true;

        [Label("老死")]
        [Tooltip("是否启用「无畏之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDautlessious_OldDie { get; set; } = true;

        [Label("收割者牙链")]
        [Tooltip("是否启用「无畏之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDautlessious_ReaperToothNecklace { get; set; } = true;

        [Label("老公爵鳞片")]
        [Tooltip("是否启用「无畏之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDautlessious_OldDukeScales { get; set; } = true;

        [Label("融合体")]
        [Tooltip("是否启用「无畏之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDautlessious_TheAmalgam { get; set; } = true;

        [Label("破碎共同体")]
        [Tooltip("是否启用「无畏之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDautlessious_ShatteredCommunity { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Hornorious")]
        [Label("真菌共生体")]
        [Tooltip("是否启用「荣光之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulHornorious_FungalSymbiote { get; set; } = true;

        [Label("元素护手")]
        [Tooltip("是否启用「荣光之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulHornorious_ElementalGauntlet { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Precious")]
        [Label("动力干细胞")]
        [Tooltip("是否启用「精准之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulPrecious_DynamoStemCells { get; set; } = true;

        [Label("星辰灵源")]
        [Tooltip("是否启用「精准之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulPrecious_DaawnlightSpiritOrigin { get; set; } = true;

        // ================= 二级开关 · 第 2 批：探寻 / 先驱 / 险恶（21 项）=================
        // 译名说明：部分为【本移植自译】（非官方译名），差异表已标注
        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Expeditious")]
        [Label("诱饵")]
        [Tooltip("是否启用「探寻之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulExpeditious_AlluringBait { get; set; } = true;

        [Label("海洋冠冕")]
        [Tooltip("是否启用「探寻之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulExpeditious_OceanCrest { get; set; } = true;

        [Label("附魔珍珠")]
        [Tooltip("是否启用「探寻之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulExpeditious_EnchantedPearl { get; set; } = true;

        [Label("盗贼硬币")]
        [Tooltip("是否启用「探寻之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulExpeditious_ThiefsDime { get; set; } = true;

        [Label("至尊钓具箱")]
        [Tooltip("是否启用「探寻之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulExpeditious_SupremeBaitTackleBoxFishingStation { get; set; } = true;

        [Label("深渊潜水服")]
        [Tooltip("是否启用「探寻之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulExpeditious_AbyssalDivingSuit { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Leaderious")]
        [Label("角斗士吊坠")]
        [Tooltip("是否启用「先驱之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderious_GladiatorsLocket { get; set; } = true;

        [Label("真菌团")]
        [Tooltip("是否启用「先驱之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderious_FungalClump { get; set; } = true;

        [Label("嚎叫之心")]
        [Tooltip("是否启用「先驱之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderious_HowlsHeart { get; set; } = true;

        [Label("冰晶石")]
        [Tooltip("是否启用「先驱之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderious_CryoStone { get; set; } = true;

        [Label("元素之心")]
        [Tooltip("是否启用「先驱之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderious_HeartoftheElements { get; set; } = true;

        [Label("变异松露")]
        [Tooltip("是否启用「先驱之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderious_MutatedTruffle { get; set; } = true;

        [Label("灾厄")]
        [Tooltip("是否启用「先驱之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderious_Calamity { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Menacious")]
        [Label("气之饰品")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_TrinketofChi { get; set; } = true;

        [Label("霜焰")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_FrostFlare { get; set; } = true;

        [Label("利维坦龙涎香")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_LeviathanAmbergris { get; set; } = true;

        [Label("星神之皮")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_HideofAstrumDeus { get; set; } = true;

        [Label("爆弹推进器")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_BlunderBooster { get; set; } = true;

        [Label("星云核心")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_NebulousCore { get; set; } = true;

        [Label("血神之杯")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_ChaliceOfTheBloodGod { get; set; } = true;

        [Label("暗日戒指")]
        [Tooltip("是否启用「险恶之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMenacious_DarkSunRing { get; set; } = true;

        // ================= 二级开关 · 第 4 批：奉献 / 速度（17 项）=================
        // 译名说明：部分为【本移植自译】（非官方译名），差异表已标注
        // ★ TheCamper（露营者）：「不移动时少造成 90% 伤害」——它的减伤就写在该饰品自己的
        //   UpdateAccessory 里；V104SoulBase:63 的 continue 跳过这次调用即可真正关掉它。
        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Devotious")]
        [Label("鸦片酊")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_Laudanum { get; set; } = true;

        [Label("巨型珍珠")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_GiantPearl { get; set; } = true;

        [Label("水生徽章")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_AquaticEmblem { get; set; } = true;

        [Label("巨龟壳")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_GiantTortoiseShell { get; set; } = true;

        [Label("仙馔针剂")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_AmbrosialAmpoule { get; set; } = true;

        [Label("再生器")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_Regenerator { get; set; } = true;

        [Label("露营者")]
        [Tooltip("★ 是否启用「奉献之魂」中该饰品的效果（该饰品会让你「不移动时少造成 90% 伤害」，关掉它即可取消这一惩罚）")]
        [DefaultValue(true)]
        public bool SoulDevotious_TheCamper { get; set; } = true;

        [Label("血焰核心")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_BloodflareCore { get; set; } = true;

        [Label("进化")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_TheEvolution { get; set; } = true;

        [Label("众神壁垒")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_RampartofDeities { get; set; } = true;

        [Label("海绵")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_TheSponge { get; set; } = true;

        [Label("德雷顿之心")]
        [Tooltip("是否启用「奉献之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulDevotious_DraedonsHeart { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Velocitious")]
        [Label("风石")]
        [Tooltip("是否启用「速度之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulVelocitious_AeroStone { get; set; } = true;

        [Label("闪避围巾")]
        [Tooltip("是否启用「速度之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulVelocitious_EvasionScarf { get; set; } = true;

        [Label("幽灵面纱")]
        [Tooltip("是否启用「速度之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulVelocitious_SpectralVeil { get; set; } = true;

        [Label("斯塔提斯的虚空饰带")]
        [Tooltip("是否启用「速度之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulVelocitious_StatisVoidSash { get; set; } = true;

        [Label("阿斯加德神盾")]
        [Tooltip("是否启用「速度之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulVelocitious_AsgardianAegis { get; set; } = true;

        // ================= 二级开关 · 第 3 批：神秘 / 隐者 / 闪烁 / 团结（19 项）=================
        // 译名说明：部分为【本移植自译】（非官方译名），差异表已标注
        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Mysterious")]
        [Label("永冻药剂")]
        [Tooltip("是否启用「神秘之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMysterious_PermafrostsConcoction { get; set; } = true;

        [Label("混沌石")]
        [Tooltip("是否启用「神秘之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMysterious_ChaosStone { get; set; } = true;

        [Label("空灵护符")]
        [Tooltip("是否启用「神秘之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulMysterious_EtherealTalisman { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Reclusious")]
        [Label("腐坏犬牙")]
        [Tooltip("是否启用「隐者之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulReclusious_RottenDogtooth { get; set; } = true;

        [Label("精准手套")]
        [Tooltip("是否启用「隐者之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulReclusious_GloveOfPrecision { get; set; } = true;

        [Label("莽撞手套")]
        [Tooltip("是否启用「隐者之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulReclusious_GloveOfRecklessness { get; set; } = true;

        [Label("吸血护符")]
        [Tooltip("是否启用「隐者之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulReclusious_VampiricTalisman { get; set; } = true;

        [Label("纳米科技")]
        [Tooltip("是否启用「隐者之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulReclusious_Nanotech { get; set; } = true;

        [Label("日蚀之镜")]
        [Tooltip("是否启用「隐者之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulReclusious_EclipseMirror { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Sparklious")]
        [Label("浮游者宝石")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_ScuttlersJewel { get; set; } = true;

        [Label("毁灭勋章")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_RuinMedallion { get; set; } = true;

        [Label("阿米迪亚斯吊坠")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_AmidiasPendant { get; set; } = true;

        [Label("腐蚀之脊")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_CorrosiveSpine { get; set; } = true;

        [Label("动量电容")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_MomentumCapacitor { get; set; } = true;

        [Label("空灵敲诈者")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_EtherealExtorter { get; set; } = true;

        [Label("崇敬吊坠")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_VeneratedLocket { get; set; } = true;

        [Label("龙鳞")]
        [Tooltip("是否启用「闪烁之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulSparklious_DragonScales { get; set; } = true;

        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.Soul_Unitious")]
        [Label("幻影神器")]
        [Tooltip("是否启用「团结之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulUnitious_PhantomicArtifact { get; set; } = true;

        [Label("核起源")]
        [Tooltip("是否启用「团结之魂」中该饰品的效果")]
        [DefaultValue(true)]
        public bool SoulUnitious_Nucleogenesis { get; set; } = true;

        // ================= 二级开关 · 补遗：6 项【原版饰品】（运行时真值 82 = 76 模组 + 6 原版）=================
        // 由来：MaterialList 里除 ItemType<> 外还混有 ItemID.<原版饰品>，此前的清点只认 ItemType<> ⇒ 漏掉这 6 项
        //       （运行期自检报 76/82、missing 6 才发现；独立重清点得 82，两者互证）
        // 译名：原版物品名照游戏内官方中文（本移植自取，未逐字比对官方 zh-Hans 文件）
        [Label("悠悠球袋")]
        [Tooltip("是否启用「荣光之魂」中该饰品的效果（原版饰品）")]
        [DefaultValue(true)]
        public bool SoulHornorious_YoyoBag { get; set; } = true;

        [Label("天界壳")]
        [Tooltip("是否启用「荣光之魂」中该饰品的效果（原版饰品）")]
        [DefaultValue(true)]
        public bool SoulHornorious_CelestialShell { get; set; } = true;

        [Label("天界手铐")]
        [Tooltip("是否启用「神秘之魂」中该饰品的效果（原版饰品）")]
        [DefaultValue(true)]
        public bool SoulMysterious_CelestialCuffs { get; set; } = true;

        [Label("悬浮板")]
        [Tooltip("是否启用「精准之魂」中该饰品的效果（原版饰品）")]
        [DefaultValue(true)]
        public bool SoulPrecious_Hoverboard { get; set; } = true;

        [Label("狙击镜")]
        [Tooltip("是否启用「精准之魂」中该饰品的效果（原版饰品）")]
        [DefaultValue(true)]
        public bool SoulPrecious_SniperScope { get; set; } = true;

        [Label("女猎人圆盾")]
        [Tooltip("是否启用「团结之魂」中该饰品的效果（原版饰品）")]
        [DefaultValue(true)]
        public bool SoulUnitious_HuntressBuckler { get; set; } = true;

        // ================= 新版之魂 =================
        [Header("Mods.CalamitySouls.Configs.CSConfigV104.Headers.NewSouls")]
        [Label("灾厄之魂（新版）")]
        [Tooltip("是否启用「灾厄之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulCalamityV104 { get; set; } = true;

        [Label("全能之魂（新版）")]
        [Tooltip("是否启用「全能之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulCommunitiousV104 { get; set; } = true;

        [Label("无畏之魂（新版）")]
        [Tooltip("是否启用「无畏之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulDautlessiousV104 { get; set; } = true;

        [Label("奉献之魂（新版）")]
        [Tooltip("是否启用「奉献之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulDevotiousV104 { get; set; } = true;

        [Label("探寻之魂（新版）")]
        [Tooltip("是否启用「探寻之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulExpeditiousV104 { get; set; } = true;

        [Label("荣光之魂（新版）")]
        [Tooltip("是否启用「荣光之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulHornoriousV104 { get; set; } = true;

        [Label("无限之魂（新版）")]
        [Tooltip("是否启用「无限之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulInfinityV104 { get; set; } = true;

        [Label("先驱之魂（新版）")]
        [Tooltip("是否启用「先驱之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulLeaderiousV104 { get; set; } = true;

        [Label("险恶之魂（新版）")]
        [Tooltip("是否启用「险恶之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulMenaciousV104 { get; set; } = true;

        [Label("神秘之魂（新版）")]
        [Tooltip("是否启用「神秘之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulMysteriousV104 { get; set; } = true;

        [Label("精准之魂（新版）")]
        [Tooltip("是否启用「精准之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulPreciousV104 { get; set; } = true;

        [Label("隐者之魂（新版）")]
        [Tooltip("是否启用「隐者之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulReclusiousV104 { get; set; } = true;

        [Label("闪烁之魂（新版）")]
        [Tooltip("是否启用「闪烁之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulSparkliousV104 { get; set; } = true;

        [Label("团结之魂（新版）")]
        [Tooltip("是否启用「团结之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulUnitiousV104 { get; set; } = true;

        [Label("超限之魂（新版）")]
        [Tooltip("是否启用「超限之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulUnlimityV104 { get; set; } = true;

        [Label("速度之魂（新版）")]
        [Tooltip("是否启用「速度之魂（新版）」的效果")]
        [DefaultValue(true)]
        public bool SoulVelocitiousV104 { get; set; } = true;

    }
}
