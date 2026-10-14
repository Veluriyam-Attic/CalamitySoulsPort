using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamitySouls.Projectiles;

namespace CalamitySouls.Items.Weapons
{
    public class ReleaseoftheId : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 36;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 8);
            Item.noMelee = true;

            Item.DamageType = DamageClass.Summon;
            Item.damage = 60;
            Item.knockBack = 5f;
            Item.useAnimation = Item.useTime = 20;
            Item.UseSound = SoundID.Item71;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<IdentityCard>();
            Item.shootSpeed = 0;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // 1.4：速度改为 Vector2 velocity，这里映射回旧变量名
            float speedX = velocity.X, speedY = velocity.Y;
            if (player.ownedProjectileCounts(Item.shoot) >= 6) return false;
            float totalMinionSlots = 0f;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].minion && Main.projectile[i].owner == player.whoAmI)
				{
					totalMinionSlots += Main.projectile[i].minionSlots;
				}
			}
            if (player.altFunctionUse != 2 && totalMinionSlots < player.maxMinions - 1) 
			{
				Projectile.NewProjectile(null, position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
				Projectile.NewProjectile(null, position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
				int swordCount = 0;
				for (int j = 0; j < Main.projectile.Length; j++)
				{
					if (Main.projectile[j].active && Main.projectile[j].type == type && Main.projectile[j].owner == player.whoAmI)
					{
						swordCount++;
					}
				}
				float angleVariance = MathHelper.TwoPi/ swordCount;
				float angle = 0f;
				for (int l = 0; l < Main.projectile.Length; l++)
				{
					if (Main.projectile[l].active && Main.projectile[l].type == type && Main.projectile[l].owner == player.whoAmI)
					{
						Main.projectile[l].ai[0] = angle;
                        Main.projectile[l].ai[1] = 15f;
						angle += angleVariance;
					}
				}
			}
			return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.SolarVeil>(), 10);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}