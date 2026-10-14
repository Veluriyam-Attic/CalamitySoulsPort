using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.GameContent;
using CalamitySouls.Ench;
using System.Collections.Generic;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CalamitySouls.Projectiles;
using Terraria.ID;
using CalamityMod.UI;
using CalamityMod.CalPlayer;
using CalamityMod;
using System;
using CalamitySouls.NPCs;

namespace CalamitySouls
{
	public class CalamitySouls : Mod
	{
		public override void Load()
		{
			Instance = this;
			RegisterHotKeys();
			LoadTextures();
			LoadShader();
			SulphurousWaterRender.Load(this);
			CSLists.Load();
			CSNPCStatic.RegisterBossIndexes();
			// 新版配置页在界面上的中文页名（否则会显示类名 CSConfigV104）。
			// 只注册这一个键，旧版 CSConfig 不动。
			Terraria.Localization.Language.GetOrRegister("Mods.CalamitySouls.Configs.CSConfigV104.DisplayName", () => "灾厄之魂 新版（v1.0.4.0）");
		}
		public override void Unload()
		{
			Instance = null;
			UnloadHotKeys();
			UnloadTextures();
			UnloadShader();
			SulphurousWaterRender.Unload();
			CSLists.Unload();
		}
		public override void PostSetupContent()
		{
			// 1.4 移植：本机 hjson 本地化未生效，这里用代码补一份中文名并自检
			CSLocalization.Seed();
			CSLocalization.Verify();
		}
		public override void AddRecipes()
		{
			CSRecipe.AddRecipes();
		}
		public override void AddRecipeGroups()
		{
			CSRecipe.AddRecipeGroup();
		}
		//      public void AddConfigTranslation(string Source, string name, string item, string color)
		//{
		//	ModTranslation text = CreateTranslation("Config" + Source);
		//	text.SetDefault(string.Concat(new object[]
		//	{
		//		"[i:",
		//		Instance.ItemType(item),
		//		"][c/",
		//		color,
		//		": ",
		//		name,
		//		"]"
		//	}));
		//	AddTranslation(text);
		//}
		//public void AddConfigTranslation(string Source, string name, int item, string color)
		//{
		//	ModTranslation text = CreateTranslation(Source);
		//	text.SetDefault(string.Concat(new object[]
		//	{
		//		"[i:",
		//		item,
		//		"][c/",
		//		color,
		//		": ",
		//		name,
		//		"]"
		//	}));
		//	AddTranslation(text);
		//}
		public static Mod Instance;
		public void RegisterHotKeys()
		{
			BrimflameKey = KeybindLoader.RegisterKeybind(this, "Brimflame enchant hotkey", "G");
			FearmongerArea = KeybindLoader.RegisterKeybind(this, "Fearmonger Frigid Area hotkey", "K");
			FearmongerMark = KeybindLoader.RegisterKeybind(this, "Fearmonger Mark hotkey", "J");
			// 新版（v1.0.4.0）新增的快捷键 —— 键位严格照新版 1.3 源码 CalamitySouls.RegisterHotKeys()：
			//   AeroDashKey="Z" / FearmongerTimeFreezeKey="K" / GodslayerDashKey="H" / ReaverKey="L" / AfflictionFearKey="U"
			// （新版原本还有 BrimflameKey="G"，旧版已经注册过同名同键的键位，这里不重复注册。）
			// 与旧版 G/K/J 的键位重叠按用户要求"照旧重叠"，只新增注册，旧版键位与逻辑一个字不动。
			AeroDashKey = KeybindLoader.RegisterKeybind(this, "Aerospec dash hotkey (V104)", "Z");
			ReaverKey = KeybindLoader.RegisterKeybind(this, "Reaver effect switch key (V104)", "L");
			FearmongerTimeFreezeKey = KeybindLoader.RegisterKeybind(this, "Fearmonger time freeze hotkey (V104)", "K");
			GodslayerDashKey = KeybindLoader.RegisterKeybind(this, "Godslayer dash hotkey (V104)", "H");
			AfflictionFearKey = KeybindLoader.RegisterKeybind(this, "Affliction fear hotkey (V104)", "U");
			// 1.3 的魔影激怒原为灾厄 CalamityMod.TarraHotKey（默认键 "Y"）；2.x 已移除该热键，
			// 故本 mod 自建同名同键的键位，让用户按熟悉的键。
			DemonshadeEnrageKey = KeybindLoader.RegisterKeybind(this, "Demonshade enrage hotkey (V104)", "Y");
		}
		public void UnloadHotKeys()
		{
			BrimflameKey = null;
			FearmongerArea = null;
			FearmongerMark = null;
			AeroDashKey = null;
			ReaverKey = null;
			FearmongerTimeFreezeKey = null;
			GodslayerDashKey = null;
			AfflictionFearKey = null;
			DemonshadeEnrageKey = null;
		}
		public static ModKeybind BrimflameKey;
		public static ModKeybind FearmongerArea;
		public static ModKeybind FearmongerMark;
		public static ModKeybind AeroDashKey;
		public static ModKeybind ReaverKey;
		public static ModKeybind FearmongerTimeFreezeKey;
		public static ModKeybind GodslayerDashKey;
		public static ModKeybind AfflictionFearKey;
		public static ModKeybind DemonshadeEnrageKey;
		public void LoadTextures()
		{
			CSTexture.Load(this);
			slimeMountOriginal = TextureAssets.SlimeMount;
			slimeMountItemOriginal = TextureAssets.Item[ItemID.SlimySaddle];
		}
		public void UnloadTextures()
		{
			CSTexture.Unload();
			TextureAssets.SlimeMount = slimeMountOriginal;
			TextureAssets.Item[ItemID.SlimySaddle] = slimeMountItemOriginal;
		}
		public static Asset<Texture2D> slimeMountOriginal;
		public static Asset<Texture2D> slimeMountItemOriginal;
		public void LoadShader()
		{
			FrozenEffect = ModContent.Request<Effect>("CalamitySouls/Effects/FrozenEffect", AssetRequestMode.ImmediateLoad).Value;
			TrailEffect = ModContent.Request<Effect>("CalamitySouls/Effects/TrailEffect", AssetRequestMode.ImmediateLoad).Value;
			FearmongerEffect = ModContent.Request<Effect>("CalamitySouls/Effects/FearmongerEffect", AssetRequestMode.ImmediateLoad).Value;
			BlindEffect = ModContent.Request<Effect>("CalamitySouls/Effects/BlindEffect", AssetRequestMode.ImmediateLoad).Value;
			// 1.3 CalamitySouls.cs:62-63 逐值照搬：注册"神惧者时间冻结"屏幕滤镜与天空（实现见 Effects/FearmongerTimeFreezeRegister.cs）
			global::CalamitySouls.Effects.FearmongerTimeFreezeRegister.Register(FearmongerEffect);
			// 1.3 CalamitySouls.cs:60-61 逐值照搬：注册"瘟疫收割者致盲"屏幕滤镜（实现见 Effects/BlindRegister.cs）
			global::CalamitySouls.Effects.BlindRegister.Register(BlindEffect);
		}
		public void UnloadShader()
		{
			FrozenEffect = null;
			TrailEffect = null;
			FearmongerEffect = null;
			BlindEffect = null;
		}
		public static Effect FrozenEffect;
		public static Effect TrailEffect;
		public static Effect FearmongerEffect;
		public static Effect BlindEffect;
	}
}