using Terraria;
using CalamityMod.Items.Armor.Demonshade;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchDemonshade : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Demonshade";
            ChineseName = "魔影";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "FF1910";
            string hotkey = "";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「在你统治者的沮丧要求下，你将踏平敌人的一切。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "暗影回血，暗影速度，暗影减益和受击时从天而降的一些暗影弹幕");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "按" + hotkey + "使用黑魔咒激怒所有生物10秒");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "一个可爱的终灾宠物跟着你");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Now you are sent to demolish the foe upon your ruler's despondont demand.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Shadow regen, shadow speed, shadow debuff, and some shadow projectiles will fire down when you are hit");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Press " + hotkey + " to enrage nearby enemies with a dark magic spell for 10 seconds");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "A lovely calamitas pet follow you");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchDemonshade = CSToggle.EnchDemonshade;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeHelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Pets.BrimstoneJewel>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Pets.Levi>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Critters.PiggyItem>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}