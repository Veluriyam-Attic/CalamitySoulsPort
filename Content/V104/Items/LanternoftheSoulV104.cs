// 皇天层 1-b · 武器族试金石 —— 遗境魂灯
// 照搬 1.3: CalamityMod.Items.Weapons.Summon.LanternoftheSoul（该物品已从灾厄 2.x 移除 ✗ 故自建）
// 它发射的弹幕 LanternSoul 在 2.x 仍存在 ✓ ⇒ 直接引用灾厄自己的（无需自建 ✓）
// 贴图：1.3 灾厄包 Items/Weapons/Summon/LanternoftheSoul.rawimg → Content/V104/Items/LanternoftheSoulV104.png
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Items
{
	public class LanternoftheSoulV104 : ModItem
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "遗境魂灯");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "V104Tip1", "无尽火焰的暗影之梦，火焰吞噬，余烬落下"));
			tooltips.Add(new TooltipLine(Mod, "V104Tip2", "召唤失落之灯为你而战"));
		}

		public override void SetDefaults()
		{
			Item.damage = 250;
			Item.DamageType = DamageClass.Summon;   // 1.3: item.summon = true
			Item.sentry = true;
			Item.mana = 10;
			Item.width = 42;
			Item.height = 60;
			Item.useTime = Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;   // 1.3: item.useStyle = 1
			Item.noMelee = true;
			Item.knockBack = 5f;
			Item.value = Item.buyPrice(1, 40, 0, 0);
			Item.rare = 10;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<LanternSoul>();   // 2.x 仍存在 ✓
			// ⚠️ 1.3: Item.Calamity().customRarity = CalamityRarity.RareVariant（专属稀有度配色）
			//    2.x: CalamityGlobalItem 已无 customRarity、且 CalamityRarity 类型名已变 ⇒ 本移植按（甲）省略该配色（Item.rare = 10 仍在，仅配色差异 ✓ 差异表已记）
			Item.UseSound = SoundID.Item44;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// 1.3: Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f);
			player.UpdateMaxTurrets();
			return false;
		}
	}
}
