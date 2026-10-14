using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Tiles.Furniture.CraftingStations;

namespace CalamitySouls.Items.BossLoots
{
    public class JawoftheDesert : BasicLoots
    {
        public override void SetStaticDefaults()
        {
            CSCompat.SeedDisplayName(this, "荒漠之颚");
            CSCompat.SeedTooltip(this, "+500%挖掘速度\n你的镐子多造成100%伤害\n脑瘫模式专属");
        }

        public override LocalizedText DisplayName => CSCompat.Zh("JawoftheDesert.DisplayName", "荒漠之颚");
        public override LocalizedText Tooltip => CSCompat.Zh("JawoftheDesert.Tooltip",
            "+500%挖掘速度\n你的镐子多造成100%伤害\n脑瘫模式专属");
        public override void SafeSetDefaults()
        {
            Item.width = Item.height = 40;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().dsJaw = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Barinade>(), 2);
            recipe.AddIngredient(ItemType<ScourgeoftheDesert>(), 2);
            recipe.AddIngredient(ItemType<OceanCrest>(), 2);
            recipe.AddIngredient(ItemType<SandCloak>());
            recipe.AddIngredient(ItemType<AeroStone>());
            recipe.AddIngredient(ItemType<DesertScourgeMask>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.AddCondition(new Condition("HyperMode", () => CSWorld.HyperMode));
            recipe.Register();
        }
    }
}