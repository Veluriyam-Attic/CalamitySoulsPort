using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using CalamitySouls.Common;

namespace CalamitySouls
{
	// 1.4 移植新增：把每颗魔石/每个之力/每个咒做成独立开关。
	// 原 1.3 版本只留了注释掉的配置骨架，这里补全成真正可用的配置。
	// 作用域为客户端：单人直接生效，多人服上每个玩家也能自己关，不需要房主操作。
	[BackgroundColor(49, 32, 36, 216)]
	public class CSConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		// 配置加载/修改后把值同步到运行期快照 CSToggle
		public override void OnLoaded() => Sync();
		public override void OnChanged() => Sync();

		private void Sync()
		{
			CSToggle.EnchAero = EnchAero;
			CSToggle.EnchAstral = EnchAstral;
			CSToggle.EnchAuric = EnchAuric;
			CSToggle.EnchBlood = EnchBlood;
			CSToggle.EnchBrim = EnchBrim;
			CSToggle.EnchDaedalus = EnchDaedalus;
			CSToggle.EnchDemonshade = EnchDemonshade;
			CSToggle.EnchDesert = EnchDesert;
			CSToggle.EnchEmpyrean = EnchEmpyrean;
			CSToggle.EnchFathom = EnchFathom;
			CSToggle.EnchFearmonger = EnchFearmonger;
			CSToggle.EnchGodslayer = EnchGodslayer;
			CSToggle.EnchGodslayerAncient = EnchGodslayerAncient;
			CSToggle.EnchHydrothermic = EnchHydrothermic;
			CSToggle.EnchMollusk = EnchMollusk;
			CSToggle.EnchOmega = EnchOmega;
			CSToggle.EnchPlagueBringer = EnchPlagueBringer;
			CSToggle.EnchPlagueReaper = EnchPlagueReaper;
			CSToggle.EnchPrismatic = EnchPrismatic;
			CSToggle.EnchReaver = EnchReaver;
			CSToggle.EnchSilva = EnchSilva;
			CSToggle.EnchSnowRuffian = EnchSnowRuffian;
			CSToggle.EnchStatigel = EnchStatigel;
			CSToggle.EnchSulphurous = EnchSulphurous;
			CSToggle.EnchTarragon = EnchTarragon;
			CSToggle.EnchTitan = EnchTitan;
			CSToggle.EnchUmbraphile = EnchUmbraphile;
			CSToggle.EnchVictide = EnchVictide;
			CSToggle.EnchWulfrum = EnchWulfrum;
			CSToggle.ForceAnnihilation = ForceAnnihilation;
			CSToggle.ForceCreature = ForceCreature;
			CSToggle.ForceRevolution = ForceRevolution;
			CSToggle.ForceTechnical = ForceTechnical;
			CSToggle.ForceTyranny = ForceTyranny;
			CSToggle.SpellAquatic = SpellAquatic;
			CSToggle.SpellBetrayal = SpellBetrayal;
			CSToggle.SpellDespair = SpellDespair;
			CSToggle.SpellEleum = SpellEleum;
			CSToggle.SpellGlobe = SpellGlobe;
			CSToggle.SpellHeavenshade = SpellHeavenshade;
			CSToggle.SpellOasis = SpellOasis;
			CSToggle.SpellPlague = SpellPlague;
			CSToggle.SpellStarlight = SpellStarlight;
		}
		[Header("魔石（Enchantment）")]
		// 每颗魔石的独立效果开关。关掉后该魔石不再提供任何效果；若它是某个「之力/咒」的组成部分，对应组合也会相应失效。
		[Label("天蓝魔石")]
		[Tooltip("是否启用「天蓝魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchAero { get; set; } = true;
		[Label("幻星魔石")]
		[Tooltip("是否启用「幻星魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchAstral { get; set; } = true;
		[Label("金源魔石")]
		[Tooltip("是否启用「金源魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchAuric { get; set; } = true;
		[Label("血炎魔石")]
		[Tooltip("是否启用「血炎魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchBlood { get; set; } = true;
		[Label("硫火魔石")]
		[Tooltip("是否启用「硫火魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchBrim { get; set; } = true;
		[Label("代达罗斯魔石")]
		[Tooltip("是否启用「代达罗斯魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchDaedalus { get; set; } = true;
		[Label("魔影魔石")]
		[Tooltip("是否启用「魔影魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchDemonshade { get; set; } = true;
		[Label("沙漠游侠魔石")]
		[Tooltip("是否启用「沙漠游侠魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchDesert { get; set; } = true;
		[Label("皇天魔石")]
		[Tooltip("是否启用「皇天魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchEmpyrean { get; set; } = true;
		[Label("幻渊鱼群魔石")]
		[Tooltip("是否启用「幻渊鱼群魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchFathom { get; set; } = true;
		[Label("神惧者魔石")]
		[Tooltip("是否启用「神惧者魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchFearmonger { get; set; } = true;
		[Label("弑神者魔石")]
		[Tooltip("是否启用「弑神者魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchGodslayer { get; set; } = true;
		[Label("远古弑神者魔石")]
		[Tooltip("是否启用「远古弑神者魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchGodslayerAncient { get; set; } = true;
		[Label("渊泉魔石")]
		[Tooltip("是否启用「渊泉魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchHydrothermic { get; set; } = true;
		[Label("软壳魔石")]
		[Tooltip("是否启用「软壳魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchMollusk { get; set; } = true;
		[Label("欧米伽蓝魔石")]
		[Tooltip("是否启用「欧米伽蓝魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchOmega { get; set; } = true;
		[Label("瘟疫使者魔石")]
		[Tooltip("是否启用「瘟疫使者魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchPlagueBringer { get; set; } = true;
		[Label("瘟疫收割者魔石")]
		[Tooltip("是否启用「瘟疫收割者魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchPlagueReaper { get; set; } = true;
		[Label("光棱魔石")]
		[Tooltip("是否启用「光棱魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchPrismatic { get; set; } = true;
		[Label("掠夺者魔石")]
		[Tooltip("是否启用「掠夺者魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchReaver { get; set; } = true;
		[Label("始源林海魔石")]
		[Tooltip("是否启用「始源林海魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchSilva { get; set; } = true;
		[Label("雪境暴徒魔石")]
		[Tooltip("是否启用「雪境暴徒魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchSnowRuffian { get; set; } = true;
		[Label("斯塔提斯魔石")]
		[Tooltip("是否启用「斯塔提斯魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchStatigel { get; set; } = true;
		[Label("硫磺海魔石")]
		[Tooltip("是否启用「硫磺海魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchSulphurous { get; set; } = true;
		[Label("龙蒿魔石")]
		[Tooltip("是否启用「龙蒿魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchTarragon { get; set; } = true;
		[Label("泰坦之心魔石")]
		[Tooltip("是否启用「泰坦之心魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchTitan { get; set; } = true;
		[Label("日影魔石")]
		[Tooltip("是否启用「日影魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchUmbraphile { get; set; } = true;
		[Label("胜潮魔石")]
		[Tooltip("是否启用「胜潮魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchVictide { get; set; } = true;
		[Label("钨钢魔石")]
		[Tooltip("是否启用「钨钢魔石」的效果")]
		[DefaultValue(true)]
		public bool EnchWulfrum { get; set; } = true;

		[Header("之力（Force）")]
		// 「之力」是魔石的上位组合。关掉后该之力不再点亮其所包含的魔石。
		[Label("湮灭之力")]
		[Tooltip("是否启用「湮灭之力」的效果")]
		[DefaultValue(true)]
		public bool ForceAnnihilation { get; set; } = true;
		[Label("生灵之力")]
		[Tooltip("是否启用「生灵之力」的效果")]
		[DefaultValue(true)]
		public bool ForceCreature { get; set; } = true;
		[Label("起义之力")]
		[Tooltip("是否启用「起义之力」的效果")]
		[DefaultValue(true)]
		public bool ForceRevolution { get; set; } = true;
		[Label("科技之力")]
		[Tooltip("是否启用「科技之力」的效果")]
		[DefaultValue(true)]
		public bool ForceTechnical { get; set; } = true;
		[Label("暴政之力")]
		[Tooltip("是否启用「暴政之力」的效果")]
		[DefaultValue(true)]
		public bool ForceTyranny { get; set; } = true;

		[Header("咒（Spell）")]
		// 「咒」由两颗魔石组合而成。关掉后不再触发该咒的专属效果。
		[Label("渊洋之咒")]
		[Tooltip("是否启用「渊洋之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellAquatic { get; set; } = true;
		[Label("背叛之咒")]
		[Tooltip("是否启用「背叛之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellBetrayal { get; set; } = true;
		[Label("绝望之咒")]
		[Tooltip("是否启用「绝望之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellDespair { get; set; } = true;
		[Label("冰川之咒")]
		[Tooltip("是否启用「冰川之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellEleum { get; set; } = true;
		[Label("寰球之咒")]
		[Tooltip("是否启用「寰球之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellGlobe { get; set; } = true;
		[Label("天影之咒")]
		[Tooltip("是否启用「天影之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellHeavenshade { get; set; } = true;
		[Label("绿洲之咒")]
		[Tooltip("是否启用「绿洲之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellOasis { get; set; } = true;
		[Label("瘟疫之咒")]
		[Tooltip("是否启用「瘟疫之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellPlague { get; set; } = true;
		[Label("星光之咒")]
		[Tooltip("是否启用「星光之咒」的效果")]
		[DefaultValue(true)]
		public bool SpellStarlight { get; set; } = true;
	}

	// 运行期快照：调用点统一读这里，避免每帧访问 ModConfig 实例。
	// 服务端没有客户端配置（GetInstance 返回 null），这份快照因此保留全 true 的默认值，
	// 保证服务器端逻辑行为与 1.3 原版一致，不会因为缺配置而崩。
	public static class CSToggle
	{
		public static bool DisableSoulSpeed = false;
		public static bool EnchAero = true;
		public static bool EnchAstral = true;
		public static bool EnchAuric = true;
		public static bool EnchBlood = true;
		public static bool EnchBrim = true;
		public static bool EnchDaedalus = true;
		public static bool EnchDemonshade = true;
		public static bool EnchDesert = true;
		public static bool EnchEmpyrean = true;
		public static bool EnchFathom = true;
		public static bool EnchFearmonger = true;
		public static bool EnchGodslayer = true;
		public static bool EnchGodslayerAncient = true;
		public static bool EnchHydrothermic = true;
		public static bool EnchMollusk = true;
		public static bool EnchOmega = true;
		public static bool EnchPlagueBringer = true;
		public static bool EnchPlagueReaper = true;
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
		public static bool SpellAquatic = true;
		public static bool SpellBetrayal = true;
		public static bool SpellDespair = true;
		public static bool SpellEleum = true;
		public static bool SpellGlobe = true;
		public static bool SpellHeavenshade = true;
		public static bool SpellOasis = true;
		public static bool SpellPlague = true;
		public static bool SpellStarlight = true;
	}
}
