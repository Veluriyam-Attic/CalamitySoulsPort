using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace CalamitySouls.Effects
{
	// 1.3 CalamitySouls.cs:60-61 的注册，逐值照搬（1.4 形态）：
	//   Filters.Scene["CalamitySouls:Blind"] = new Filter(new BlindScreenShader(new Ref<Effect>(BlindEffect), "Blind").UseProgress(0f), (EffectPriority)4);
	//   Filters.Scene["CalamitySouls:Blind"].Load();
	// 激活/关闭（1.3 CSPlayer.cs:401-412）：PlagueReaperBlind > 0 时 Activate，否则 Deactivate。
	public static class BlindRegister
	{
		public const string Key = "CalamitySouls:Blind";

		public static void Register(Effect blindEffect)
		{
			Filters.Scene[Key] = new Filter(
				new BlindScreenShader(new Ref<Effect>(blindEffect), "Blind").UseProgress(0f),
				(EffectPriority)4);
		}

		public static void SetActive(bool active)
		{
			if (Main.netMode == 2) return;
			Filter filter = Filters.Scene[Key];
			if (filter == null) return;
			if (active && !filter.IsActive()) Filters.Scene.Activate(Key, default(Vector2));
			else if (!active && filter.IsActive()) Filters.Scene.Deactivate(Key);
		}
	}
}
