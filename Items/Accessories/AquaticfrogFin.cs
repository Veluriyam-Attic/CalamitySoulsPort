using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using CalamityMod;

namespace CalamitySouls.Items.Accessories
{
    public class AquaticfrogFin : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 42;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 7);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetJumpState(ExtraJump.TsunamiInABottle).Enable();
            player.jumpBoost = true;
            player.jumpSpeedBoost += 2.4f;
            player.autoJump = true;
            player.accFlipper = true;
            player.ignoreWater = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
            player.manaCost -= 0.075f;
            player.maxMinions += 1;
            player.GetDamage<RogueDamageClass>() += 0.075f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SeafrogLeg>());
            recipe.AddIngredient(ItemID.Flipper);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.LeviathanMask>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}