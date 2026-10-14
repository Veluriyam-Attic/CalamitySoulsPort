// 皇天层 1-b · 神之风箱（照搬 1.3 CalamityMod.Items.Weapons.Ranged.TheEmpyrean；该物品已从灾厄 2.x 移除）
// 贴图：1.3 灾厄包 Items/Weapons/Ranged/TheEmpyrean.rawimg → Content/V104/Items/TheEmpyreanV104.png
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104.Projectiles;

namespace CalamitySouls.Content.V104.Items
{
	public class TheEmpyreanV104 : ModItem
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "神之风箱");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "V104Tip1", "70%几率不消耗凝胶"));
		}

		public override void SetDefaults()
		{
			Item.damage = 80;
			Item.DamageType = DamageClass.Ranged;   // 1.3: item.ranged = true
			Item.width = 70;
			Item.height = 24;
			Item.useTime = 6;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Shoot;   // 1.3: item.useStyle = 5
			Item.noMelee = true;
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item34;
			Item.value = Item.buyPrice(0, 95, 0, 0);
			Item.rare = 9;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<CosmicFire>();   // 2.x 仍存在 ✓
			Item.shootSpeed = 9f;
			Item.useAmmo = AmmoID.Gel;
		}

		// 注：1.3 的 ConsumeAmmo（70% 不消耗凝胶）在 1.4 已无 ModItem 钩子 ⇒ 已改由 AmmoGlobalV104.CanConsumeAmmo 实现（等价 ✓）
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10f, 0f);
		}
	}
}
