using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamitySouls.Projectiles;

namespace CalamitySouls.Items.Weapons
{
  public  class ImpurityWithinOnesBody:ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 54;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 8);

            Item.DamageType=DamageClass.Magic;
            Item.mana = 6;
            Item.damage = 66;
            Item.knockBack = 6f;
            Item.useAnimation = 30;
            Item.useTime = 10;
            Item.UseSound = SoundID.Item71;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ImpurityOrb>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // 1.4：速度改为 Vector2 velocity，这里映射回旧变量名
            float speedX = velocity.X, speedY = velocity.Y;
            speedX = Main.rand.Next(-10, 11) / 10f;
            speedY = -10f;
            Projectile.NewProjectile(null, position.X + Main.rand.Next(300, 401), position.Y + 900, speedX, speedY, type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(null, position.X - Main.rand.Next(300, 401), position.Y + 900, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.SolarVeil>(), 10);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
