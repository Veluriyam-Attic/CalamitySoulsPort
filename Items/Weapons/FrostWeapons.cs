using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod;
using CalamitySouls.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Items.Weapons
{
    public class FrostPaintgun : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.DamageType=DamageClass.Ranged;
            Item.damage = 13;
            Item.useAnimation = 30;
            Item.useTime = 10;
            Item.knockBack = 2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileType<FrostPaintball>();
            Item.shootSpeed = 7f;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 2);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // 1.4：速度改为 Vector2 velocity，这里映射回旧变量名
            float speedX = velocity.X, speedY = velocity.Y;
            Vector2 velo = new Vector2(speedX, speedY);
            position += velo * 3f;
            speedX += Main.rand.Next(-10, 11) / 10f;
            speedY += Main.rand.Next(-10, 11) / 10f;
            SoundEngine.PlaySound(SoundID.Item5, position);
            return true;  
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.PainterPaintballGun);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class IcicleDagger : RogueWeapon
    {
        public override void SetStaticDefaults()
        {
            CSCompat.SeedDisplayName(this, "冰刺投刀");
        }

        public override LocalizedText DisplayName => CSCompat.Zh("IcicleDagger.DisplayName", "冰刺投刀");
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 36;
            Item.DamageType=ModContent.GetInstance<RogueDamageClass>();
            Item.damage = 14;
            Item.knockBack = 3f;
            Item.useAnimation = 11;
            Item.useTime = 11;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ProjectileType<IcicleDaggerProj>();
            Item.shootSpeed = 15f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 1);
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<IcicleDagger>(), 40);
            recipe.AddIngredient(ItemID.IceBlock, 10);
            recipe.AddIngredient(ItemID.SnowBlock, 10);
            recipe.AddIngredient(ItemID.BorealWood, 5);
            recipe.Register();
            recipe = Recipe.Create(ItemType<IcicleDagger>(), 100);
            recipe.AddIngredient(ItemID.FrostDaggerfish, 100);
            recipe.AddIngredient(ItemID.BorealWood, 10);
            recipe.Register();
        }
    }
}