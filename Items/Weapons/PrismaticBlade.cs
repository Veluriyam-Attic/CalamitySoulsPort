using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;
using CalamitySouls.Projectiles;

namespace CalamitySouls.Items.Weapons
{
	// 1.3 CalamitySouls.Items.Weapons.PrismaticBlade 逐值照搬（光棱剑）
	// 1.4/2.x 适配（均有工程内先例）：
	//   ModifyHitNPC(…ref int damage) ⇒ ModifyHitNPC(…ref NPC.HitModifiers) → FinalDamage *= 5
	//   Rarity12BuyPrice ⇒ Item.sellPrice(gold: 12)（先例：Rarity8BuyPrice ⇒ sellPrice(gold: 8)）
	//   CalamityGlobalItem.customRarity = 12 ⇒ 2.x 已移除该成员，省略（仅影响稀有度边框配色，记差异表）
	//   obj.Melee() ⇒ obj.DamageType = DamageClass.Melee
	//   player.DirectionToSafe(v) ⇒ CSPlayerV104Effects.DirectionToSafe(player, v)
	public class PrismaticBlade : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 78;
			Item.height = 78;
			Item.DamageType = DamageClass.Melee;
			Item.useStyle = ItemUseStyleID.Swing;      // 1.3: useStyle = 1
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
			Item.damage = 200;
			Item.knockBack = 20f;
			Item.shoot = 156;                          // 1.3 原值；Shoot 被完全覆写并返回 false，仅占位
			Item.shootSpeed = 16f;
			Item.rare = ItemRarityID.Red;              // 1.3: rare = 10
			Item.value = Item.sellPrice(gold: 12);
			Item.useAnimation = 30;
			Item.useTime = 15;
		}

		// 1.3: damage *= 5
		public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.FinalDamage *= 5f;
		}

		// 1.3: 击中敌人时释放额外的光波（DeathhailBeam + 4~9 个 GodSlayerOrb）
		// ⚠️ 2.x 差异：CalamityMod 的 GodSlayerOrb 在 1.3/2.x 均不存在（calref 标 [BOTH_MISSING]）
		//    ⇒ 按本工程既有做法替换为 CSPlaceholder（见 CSPlayerEffects.cs:590 同款处理），已记差异表
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
			int type, int damage, float knockBack)
		{
			Vector2 dir = CSPlayerV104Effects.DirectionToSafe(player, Main.MouseWorld);
			Projectile beam = Projectile.NewProjectileDirect(source, player.Center, dir,
				ModContent.ProjectileType<CalamityMod.Projectiles.Magic.DeathhailBeam>(), damage, knockBack, player.whoAmI);
			beam.DamageType = DamageClass.Melee;
			beam.localNPCHitCooldown = 10;
			for (int i = 0; i < Main.rand.Next(4, 10); i++)
			{
				Projectile orb = Projectile.NewProjectileDirect(source, player.Center,
					dir * 8f + CSUtils.RandomRotate * 12f,
					ModContent.ProjectileType<CSPlaceholder>(),
					(int)(damage / 5f), knockBack / 5f, player.whoAmI);
				orb.DamageType = DamageClass.Melee;
				orb.timeLeft = 100;
				orb.extraUpdates = 100;
			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(2880, 1)                       // 1.3 数字 ID 原样保留
				.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.ArmoredShell>(), 2)
				.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.Ores.ExodiumCluster>(), 9)
				.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.DivineGeode>(), 5)
				.AddIngredient(1346, 100)                     // 1.3 数字 ID 原样保留
				.AddTile(412)                                 // 1.3: AddTile(412)
				.Register();
		}
	}
}
