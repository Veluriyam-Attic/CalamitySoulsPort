// 皇天层 1-b · 命运之手（照搬 1.3 CalamityMod.Items.Weapons.Magic.TomeofFates；该物品已从灾厄 2.x 移除）
// 它发射 CosmicTentacle（2.x 仍存在 ✓）与 BrimstoneTentacle（我方层 0 自建 ✓）—— 层 0 产物第 2 次兑现
// 贴图：1.3 灾厄包 Items/Weapons/Magic/TomeofFates.rawimg → Content/V104/Items/TomeofFatesV104.png
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
	public class TomeofFatesV104 : ModItem
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "命运之手");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "V104Tip1", "召唤宇宙之触助你撕裂敌人"));
			tooltips.Add(new TooltipLine(Mod, "V104Tip2", "随机产生硫磺火触手，造成更巨大的伤害"));
		}

		public override void SetDefaults()
		{
			Item.damage = 110;
			Item.DamageType = DamageClass.Magic;   // 1.3: item.magic = true
			Item.mana = 8;
			Item.width = 28;
			Item.height = 30;
			Item.crit = 3;
			Item.useTime = 5;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;   // 1.3: item.useStyle = 5
			Item.noMelee = true;
			Item.knockBack = 3.5f;
			Item.value = Item.buyPrice(0, 95, 0, 0);
			Item.rare = 9;
			Item.UseSound = SoundID.Item103;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<CosmicTentacle>();   // 2.x 仍存在 ✓
			Item.shootSpeed = 17f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int myPlayer = Main.myPlayer;
			int num = damage;
			float num2 = knockback;
			num2 = player.GetWeaponKnockback(Item, num2);
			player.itemTime = Item.useTime;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float x = (float)Main.mouseX + Main.screenPosition.X - vector.X;
			float y = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
			Vector2 vector2 = new Vector2(x, y);
			vector2.Normalize();
			Vector2 vector3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
			vector3.Normalize();
			vector2 = vector2 * 4f + vector3;
			vector2.Normalize();
			vector2 *= Item.shootSpeed;
			int num3 = Main.rand.Next(7);
			float num4 = (float)Main.rand.Next(10, 160) * 0.001f;
			if (Utils.NextBool(Main.rand, 2))
			{
				num4 *= -1f;
			}
			float num5 = (float)Main.rand.Next(10, 160) * 0.001f;
			if (Utils.NextBool(Main.rand, 2))
			{
				num5 *= -1f;
			}
			if (num3 == 0)
			{
				// 1.3: Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, ProjectileType<BrimstoneTentacle>(), (int)(num * 1.5), num2, myPlayer, num5, num4);
				Projectile.NewProjectile(source, vector, vector2, ModContent.ProjectileType<BrimstoneTentacleV104>(), (int)((double)num * 1.5), num2, myPlayer, num5, num4);
			}
			else
			{
				Projectile.NewProjectile(source, vector, vector2, ModContent.ProjectileType<CosmicTentacle>(), num, num2, myPlayer, num5, num4);
			}
			return false;
		}
	}
}
