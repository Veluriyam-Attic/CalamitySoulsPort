using Terraria;
using CalamityMod.Items.Armor.Victide;
using CalamityMod.Items.Weapons.Summon;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchVictide : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Victide";
            ChineseName = "胜潮";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "CC4235";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「胜者书写历史。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "提供水下呼吸，消除水的阻力");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "每半分钟获得77秒阿米迪亚斯的祝福");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchVictide>(),
                    ModContent.ItemType<EnchMollusk>()
                }, "获得一个强大的冲刺");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Only victory leads the history.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Gills effect, provides increased underwater mobility");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Grants 77s Amidias' Blessing every half minute");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchVictide>(),
                ModContent.ItemType<EnchMollusk>()
            }, "Grant a powerful dash");
        }
        public override void SetEnchDefaults()
        {
            Item.defense = 7;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchVictide = CSToggle.EnchVictide;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Victide");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.ShieldoftheOcean>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.Cnidarian>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.CoralSpout>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}