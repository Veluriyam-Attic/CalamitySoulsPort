using Terraria;
using CalamityMod.Items.Armor.GodSlayer;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Items;

namespace CalamitySouls.Ench
{
    public class EnchGodslayer : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Godslayer";
            ChineseName = "弑神者";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "FC6DCA";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「还没完，小鬼！」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "你获得弑神者复活效果，你的攻击会生成弑神者幻影");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "缩短弑神者复活CD至30秒，你每隔一段时间获得不同的增益");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「It's not over yet, kid!」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "You are granted godslayer revive effect and your attack will summon god slayer phantoms");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Reduce Godslayer revive cooldown to 30s, you gain different bonus over time");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchGodslayer = CSToggle.EnchGodslayer;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Godslayer");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.EssenceFlayer>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.StreamGouge>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.CleansingBlaze>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.SoulPiercer>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.ExecutionersBlade>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
