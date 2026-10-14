using Terraria;
using CalamityMod.Items.Armor.Statigel;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchStatigel : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Statigel";
            ChineseName = "斯塔提斯";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "DFCCFF";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「传说忍者之魂。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "增加10跳跃高度，增加48%跳跃速度，无敌时间延长");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "召唤迷你史莱姆神，在你受伤之时治疗你");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchStatigel>(),
                    ModContent.ItemType<EnchTarragon>()
                }, "减半冲刺冷却");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Spirit of the legendary ninja.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "+10 jump height, +48% jump speed and increased immunity frame");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Summon a mini slime god to heal you when you're hurt");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchStatigel>(),
                ModContent.ItemType<EnchTarragon>()
            }, "Halves your dash cooldown");
        }
        public override void SetEnchDefaults()
        {
            Item.defense = 2;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchStatigel = CSToggle.EnchStatigel;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Statigel");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Statigel.StatigelArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Statigel.StatigelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.GeliticBlade>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.TheGodsGambit>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Goobow>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.GunkShot>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.OverloadedBlaster>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.GelDart>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}