using Terraria;
using CalamityMod.Items.Armor.Astral;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchAstral : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Astral";
            ChineseName = "幻星";
            lineAmount = 5;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "63A0A4";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「愿群星指引你的前路。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "获得勘探、狩猎和危险感药水的效果，增加10%伤害，暴击，生命回复，免伤，武器使用速度");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "增加10生命和魔力上限，2召唤栏和2炮台栏，减少10%魔力消耗");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "半血的时候获得四秒无敌，之后聚集能量，攻速和生命上限都加倍");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "该效果持续10秒，结束时再获得四秒无敌，并以60分钟冷却收尾");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「May the stars guide your way.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Spenkular, Hunter and Dangersense effect, +10% damage, crit, life regen, damage reduction and weapon fire rate");
            QuickModiLine(tooltips, ref nextLine, "" +
                "+10 max life and mana, +2 minion and sentry slots, reduce 10% mana cost");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Grant 4s inmmunity under half life, then grant doubled fire rate and max hp");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "The effect lasts for 10s and grant another 4s immunity in the end, all these have a 60min cooldown");
        }
        public override void SetEnchDefaults()
        {
            Item.defense += 10;
            Item.lifeRegen += 10;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchAstral = CSToggle.EnchAstral;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralHelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Astral.AstralLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.AstralPike>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.AstralBlaster>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.AstralStaff>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}