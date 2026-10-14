// 龙蒿魔石（EnchTarragon）配套物品 · 龙蒿增益叶片 TarragonLeafV104
// 照搬：ext1040/Items/OtherItems/TarragonLeaf.cs（本模组自己的 ModItem 拾取物 —— 注意：它不是弹幕 ✗）
// 1.4 处置：DisplayName→CSCompat.SeedDisplayName ✓ item.→Item. ✓ Kill/OnPickup/ItemSpace/GrabRange/Update 同名 ✓
//           Main.RegisterItemAnimation(item.type,…) → Item.type ✓ · PlaySound(ClamImpact).Volume/4 → SoundEngine（音量按甲省略）
// 贴图：v1.0.4.0 包 Items/OtherItems/TarragonLeaf.rawimg → Content/V104/Items/TarragonLeafV104.png
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Items
{
	public class TarragonLeafV104 : ModItem
	{
		public bool CheckedSpawn = false;

		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "龙蒿增益叶片");
			// 1.3 只有英文 tooltip（"Why its max stack must be 1? Figure it out yourself"）⇒ 此处给出对应中文
			CSCompat.SeedTooltip(this, "为什么它的堆叠上限必须是 1？自己想去吧");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 28;
			Item.rare = ItemRarityID.Expert;
		}

		public override bool OnPickup(Player player)
		{
			// 1.3: player.CS().TarragonLeafEffects[Main.rand.Next(8)] = 5 * 60;
			player.V104().TarragonLeafEffects[Main.rand.Next(8)] = 5 * 60;
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/ClamImpact"), player.Center);   // 1.3 音量 ÷4 按（甲）省略
			return false;
		}

		public override bool ItemSpace(Player player) => true;

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			gravity = 0f;
			Item.velocity *= 0.8f;
			if (!CheckedSpawn)
			{
				foreach (Item item2 in Main.item)
				{
					if (!item2.active || item2.IsAir) continue;
					if (item2.Distance(Item.Center) > 10f) continue;
					Item.TurnToAir();
					Item.active = false;
					break;
				}
				CheckedSpawn = true;
			}
		}

		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange = 360;
		}
	}
}
