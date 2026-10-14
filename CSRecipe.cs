using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls
{
    public static class CSRecipe
    {
        public static void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.BurntSienna>());
            recipe.AddIngredient(ItemID.Sandstone, 30);
            recipe.AddIngredient(ItemID.HardenedSand, 30);
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.StormjawStaff>());
            recipe.AddIngredient(ItemType<CalamityMod.Items.Materials.StormlionMandible>(), 3);
            recipe.AddIngredient(ItemID.HardenedSand, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        // 1.4：灾厄 2.x 把各套装头盔拆成了 近战/远程/魔法/盗贼/召唤 五系，
        // 1.3 时代的 Helmet/Mask/Visage 等“任意头盔”变体大多被删除。
        // 这里改为列出 2.x 实际存在的头盔，RecipeGroup 语义（"Any X Helmet"）保持不变。
        public static void AddRecipeGroup()
        {
            RecipeGroup.RegisterGroup("CS:Aerospec", new RecipeGroup(() => "Any Aerospec Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Auric", new RecipeGroup(() => "Any Auric Tesla Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Bloodflare", new RecipeGroup(() => "Any Bloodflare Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Daedalus", new RecipeGroup(() => "Any Daedalus Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Godslayer", new RecipeGroup(() => "Any Godslayer Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Vanity.GodSlayerHornedHelm>(),
                ItemType<CalamityMod.Items.Armor.Vanity.GodSlayerVisage>()
            }));
            RecipeGroup.RegisterGroup("CS:Hydrothermic", new RecipeGroup(() => "Any Hydrothermic Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Reaver", new RecipeGroup(() => "Any Reaver Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadTank>(),
                ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadMobility>(),
                ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadExplore>()
            }));
            RecipeGroup.RegisterGroup("CS:Silva", new RecipeGroup(() => "Any Silva Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Silva.SilvaHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Silva.SilvaHeadSummon>(),
                ItemType<CalamityMod.Items.Armor.Vanity.SilvaHelm>(),
                ItemType<CalamityMod.Items.Armor.Vanity.SilvaHornedHelm>(),
                ItemType<CalamityMod.Items.Armor.Vanity.SilvaMask>()
            }));
            RecipeGroup.RegisterGroup("CS:Statigel", new RecipeGroup(() => "Any Statigel Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Statigel.StatigelHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Statigel.StatigelHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Statigel.StatigelHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Statigel.StatigelHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Statigel.StatigelHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Tarragon", new RecipeGroup(() => "Any Tarragon Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Tarragon.TarragonHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Tarragon.TarragonHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Tarragon.TarragonHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Tarragon.TarragonHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Tarragon.TarragonHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Victide", new RecipeGroup(() => "Any Victide Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Victide.VictideHeadMelee>(),
                ItemType<CalamityMod.Items.Armor.Victide.VictideHeadRanged>(),
                ItemType<CalamityMod.Items.Armor.Victide.VictideHeadMagic>(),
                ItemType<CalamityMod.Items.Armor.Victide.VictideHeadRogue>(),
                ItemType<CalamityMod.Items.Armor.Victide.VictideHeadSummon>()
            }));
            RecipeGroup.RegisterGroup("CS:Wulfrum", new RecipeGroup(() => "Any Wulfrum Helmet", new int[]
            {
                ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumHat>()
            }));
        }
    }
}
