// ============================================================================
// 「杂项」独立配置页：存放从其它两页搬来的通用开关（新旧版本通用）。
//   · 禁用灾厄之魂的移速加成  —— 从 CSConfig（旧版页）搬来
//   · 允许新旧魂同时装备      —— 从 CSConfigV104（新版页）搬来
// 注意：搬动配置项会改变它的序列化键，玩家这两项会回到默认值（均为默认关闭）。
// ============================================================================
using System.ComponentModel;
using CalamitySouls.Common;
using CalamitySouls.Content.V104;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace CalamitySouls
{
	[BackgroundColor(40, 36, 52, 216)]
	public class CSConfigMisc : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public override void OnLoaded() => Sync();
		public override void OnChanged() => Sync();

		private void Sync()
		{
			CSToggle.DisableSoulSpeed = DisableSoulSpeed;
			CSToggleV104.AllowMixedSouls = AllowMixedSouls;
		}
		[Label("禁用灾厄之魂的移速加成")]
		[Tooltip("开启后，本模组（魔石 / 之力 / 咒 / 魂）提供的移动速度加成全部失效。\n用于最终形态移速过快、难以控制的情况。其它效果不受影响。")]
		[DefaultValue(false)]
		public bool DisableSoulSpeed { get; set; } = false;
		[Label("允许新旧魂同时装备")]
		[Tooltip("默认关闭。关闭时：只限制最顶层的「魂」——旧版之魂与新版的魂不能同时佩戴；\n魔石、之力与饰品可以随意混搭，不受影响。")]
		[DefaultValue(false)]
		public bool AllowMixedSouls { get; set; } = false;
	}
}