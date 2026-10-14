using Terraria;
using CalamityMod.Items.Placeables;
using Terraria.ModLoader;
using System.Collections.Generic;
using CalamitySouls.Force;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Souls
{
    public class SoulCalamity : BasicSoul
    {
        public override void SetSoulStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 10));
        }
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Calamity";
            ChineseName = "灾厄";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "你即灾厄。");
                QuickModiLine(tooltips, ItemType<ForceAnnihilation>(),"湮灭之力的效果");
                QuickModiLine(tooltips, ItemType<ForceCreature>(),"生灵之力的效果");
                QuickModiLine(tooltips, ItemType<ForceRevolution>(),"起义之力的效果");
                QuickModiLine(tooltips, ItemType<ForceTechnical>(),"科技之力的效果");
                QuickModiLine(tooltips, ItemType<ForceTyranny>(),"暴政之力的效果");
                QuickModiLine(tooltips, Item.type,"你的肾上腺素多造成300%伤害");
                return;
            }
            QuickModiDescription(tooltips, "You are the calamity.");
            QuickModiLine(tooltips, ItemType<ForceAnnihilation>(), "Effect of annihilation force");
            QuickModiLine(tooltips, ItemType<ForceCreature>(), "Effect of creature force");
            QuickModiLine(tooltips, ItemType<ForceRevolution>(), "Effect of revolution force");
            QuickModiLine(tooltips, ItemType<ForceTechnical>(), "Effect of technical force");
            QuickModiLine(tooltips, ItemType<ForceTyranny>(), "Effect of tyranny force");
            QuickModiLine(tooltips, Item.type, "Your adrenaline deals 300% more damage");
        }
        public override void SetSoulDefaults()
        {
            Item.width = 38;
            Item.height = 46;
            Item.defense += 108;
            Item.lifeRegen += 10;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().ForceAnnihilation = CSToggle.ForceAnnihilation;
            player.CS().ForceCreature = CSToggle.ForceCreature;
            player.CS().ForceRevolution = CSToggle.ForceRevolution;
            player.CS().ForceTechnical = CSToggle.ForceTechnical;
            player.CS().ForceTyranny = CSToggle.ForceTyranny;
            player.CS().AdrenalineDamageBoost += 3f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ForceAnnihilation>());
            recipe.AddIngredient(ModContent.ItemType<ForceCreature>());
            recipe.AddIngredient(ModContent.ItemType<ForceRevolution>());
            recipe.AddIngredient(ModContent.ItemType<ForceTechnical>());
            recipe.AddIngredient(ModContent.ItemType<ForceTyranny>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.Rock>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}