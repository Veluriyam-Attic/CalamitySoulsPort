using Terraria;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamitySouls.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Items.Weapons
{
    public class MonolithRay : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 46;
            Item.DamageType=DamageClass.Magic;
            Item.damage = 80;
            Item.knockBack = 5f;
            Item.mana = 10;
            Item.useAnimation = Item.useTime = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item125;
            Item.shoot = ProjectileType<AstralRay>();
            Item.shootSpeed = 8f;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 5);
            Item.autoReuse = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // 1.4：速度改为 Vector2 velocity，这里映射回旧变量名
            float speedX = velocity.X, speedY = velocity.Y;
            position += new Vector2(speedX, speedY) * 4f;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurnitureMonolith.AstralMonolith>(), 20);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }
    }
    public class MonolithStick : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 102;
            Item.DamageType=DamageClass.Melee;
            Item.damage = 300;
            Item.knockBack = 10f;
            Item.useAnimation = Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 5);
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.scale = 1.2f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<AstralMonolith>(), 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}