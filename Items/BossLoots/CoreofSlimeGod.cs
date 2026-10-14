using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Tiles.Furniture.CraftingStations;

namespace CalamitySouls.Items.BossLoots
{
    public class CoreofSlimeGod : BasicLoots
    {
        public override void SetStaticDefaults()
        {
            CSCompat.SeedDisplayName(this, "史莱姆之神核心");
            CSCompat.SeedTooltip(this, "把你的史莱姆坐骑转化为史莱姆神坐骑\n它的水平移速被大大增加，而且可以飞行一段距离\n脑瘫模式专属");
        }

        public override LocalizedText DisplayName => CSCompat.Zh("CoreofSlimeGod.DisplayName", "史莱姆之神核心");
        public override LocalizedText Tooltip => CSCompat.Zh("CoreofSlimeGod.Tooltip",
            "把你的史莱姆坐骑转化为史莱姆神坐骑\n它的水平移速被大大增加，而且可以飞行一段距离\n脑瘫模式专属");
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 32;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().sgCore = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<AbyssalTome>(), 2);
            recipe.AddIngredient(ItemType<EldritchTome>(), 2);
            recipe.AddIngredient(ItemType<SlimeGodMask>());
            recipe.AddIngredient(ItemType<SlimeGodMask2>());
            recipe.AddIngredient(ItemID.Gel, 100);
            recipe.AddIngredient(ItemType<PurifiedGel>(), 25);
            recipe.AddTile(TileID.DemonAltar);
            recipe.AddCondition(new Condition("HyperMode", () => CSWorld.HyperMode));
            recipe.Register();
        }
    }
}