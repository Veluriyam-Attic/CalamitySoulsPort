// 皇天层 1-b · 半影裂片 XerocPitchforkV104
// 照搬 1.3: CalamityMod.Items.Weapons.Rogue.XerocPitchfork（该物品已从灾厄 2.x 移除 ✗ 故自建）
//   ⚠️ 1.3 用 `: RogueWeapon` + `SafeSetDefaults()`；2.x 已无 RogueWeapon 基类 ✗
//      该基类唯一作用是设盗贼伤害类型 ⇒ 本移植改用 ModItem + SetDefaults + 显式 RogueDamageClass（行为等价；先例 CSCompat.cs:23）
// 它发射的弹幕 AntumbraShardProjectile 在 2.x 仍存在 ✓ ⇒ 直接引用灾厄自己的 ✓
// [2026-10-03] 配方已移除：本物品改为不可合成（前置材料 MeldiateBar 已删除）
// 贴图：1.3 灾厄包 Items/Weapons/Rogue/XerocPitchfork.rawimg → Content/V104/Items/XerocPitchforkV104.png
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Items
{
	public class XerocPitchforkV104 : ModItem
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "半影裂片");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "V104Tip1", "潜伏攻击会使此武器在飞行时留下追踪星星"));
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.damage = 280;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 19;
			Item.useTime = 19;
			Item.useStyle = ItemUseStyleID.Swing;   // 1.3: item.useStyle = 1
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.maxStack = 999;
			Item.value = 10000;
			Item.rare = 9;
			Item.shoot = ModContent.ProjectileType<AntumbraShardProjectile>();   // 2.x 仍存在 ✓
			Item.shootSpeed = 16f;
			Item.DamageType = ModContent.GetInstance<RogueDamageClass>();   // 1.3: item.Calamity().rogue = true
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// 1.3: if (player.Calamity().StealthStrikeAvailable()) { int num = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0f, 0f); Main.projectile[num].Calamity().stealthStrike = true; }
			if (player.Calamity().StealthStrikeAvailable())
			{
				int num = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 0f);
				Main.projectile[num].Calamity().stealthStrike = true;
			}
			return !player.Calamity().StealthStrikeAvailable();
		}
	}
}
