using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamitySouls.Projectiles;

namespace CalamitySouls.Items.Weapons
{
    public class PeerlessWindGod : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 66;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 8);
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.DamageType=DamageClass.Melee;
            Item.damage = 60;
            Item.knockBack = 3.5f;
            Item.useAnimation = Item.useTime = 30;
            Item.UseSound = SoundID.Item71;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<PeerlessWindGodProj>();
            Item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            // 1.4：速度改为 Vector2 velocity，这里映射回旧变量名
            float speedX = velocity.X, speedY = velocity.Y;
            Projectile.NewProjectile(null, position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts(Item.shoot) < 1;
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Arkhalis);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.SolarVeil>(), 10);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.AerialiteBar>(), 23);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}