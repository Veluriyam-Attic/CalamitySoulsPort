// 皇天层 1-b · 熵阔剑（照搬 1.3 CalamityMod.Items.Weapons.Melee.XerocsGreatsword；该物品已从灾厄 2.x 移除）
// 它按档位切换 3 个 MeldGreatsword{Small,Medium,Big}Projectile —— 全是我方层 0 自建 ✓（层 0 产物第 3 次兑现）
// 贴图：1.3 灾厄包 Items/Weapons/Melee/XerocsGreatsword.rawimg → Content/V104/Items/XerocsGreatswordV104.png
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
	public class XerocsGreatswordV104 : ModItem
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "熵阔剑");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "V104Tip1", "发射数个追踪离子球"));
		}

		public override void SetDefaults()
		{
			Item.width = 130;
			Item.height = 106;
			Item.damage = 90;
			Item.DamageType = DamageClass.Melee;   // 1.3: item.melee = true
			Item.useAnimation = 26;
			Item.useTime = 26;
			Item.useStyle = ItemUseStyleID.Swing;   // 1.3: item.useStyle = 1
			Item.useTurn = true;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = Item.buyPrice(0, 95, 0, 0);
			Item.rare = 9;
			Item.shoot = ModContent.ProjectileType<MeldGreatswordSmallProjectileV104>();   // 我方层 0 自建 ✓
			Item.shootSpeed = 12f;
		}

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
			hitbox = CalamityUtils.FixSwingHitbox(118f, 118f);   // 1.3 逐值
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int num = Main.rand.Next(4, 6);
			for (int i = 0; i < num; i++)
			{
				float num2 = velocity.X + (float)Main.rand.Next(-20, 21) * 0.05f;
				float num3 = velocity.Y + (float)Main.rand.Next(-20, 21) * 0.05f;
				float num4 = 0.5f;
				switch (i)
				{
				case 0:
					type = ModContent.ProjectileType<MeldGreatswordSmallProjectileV104>();
					break;
				case 1:
					type = ModContent.ProjectileType<MeldGreatswordMediumProjectileV104>();
					num4 = 0.65f;
					break;
				case 2:
					type = ModContent.ProjectileType<MeldGreatswordBigProjectileV104>();
					num4 = 0.8f;
					break;
				}
				Projectile.NewProjectile(source, position, new Vector2(num2, num3), type, (int)((float)damage * num4), knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Utils.NextBool(Main.rand, 3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 27, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
