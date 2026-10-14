using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace CalamitySouls.Effects
{
	// 1.3 CalamitySouls.cs:62-63 的注册，逐值照搬（1.4 形态）：
	//   Filters.Scene["CalamitySouls:FearmongerTimeFreeze"] = new Filter(new FearmongerTimeFreezeScreenShader(new Ref<Effect>(FearmongerEffect), "Fearmonger"), (EffectPriority)4);
	//   SkyManager.Instance["CalamitySouls:FearmongerTimeFreeze"] = new FearmongerTimeFreezeSky();
	// 放在本命名空间内注册，避免在 CalamitySouls 类内出现 CalamitySouls.Effects 的解析歧义。
	public static class FearmongerTimeFreezeRegister
	{
		public const string Key = "CalamitySouls:FearmongerTimeFreeze";

		public static void Register(Effect fearmongerEffect)
		{
			Filters.Scene[Key] = new Filter(
				new FearmongerTimeFreezeScreenShader(new Ref<Effect>(fearmongerEffect), "Fearmonger"),
				(EffectPriority)4);
			SkyManager.Instance[Key] = new FearmongerTimeFreezeSky();
		}
	}
}
