using Terraria;
using CalamityMod.Items.Armor.Auric;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchAuric : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Auric";
            ChineseName = "金源";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "7BCDED";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「臣服。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "受击时生成一个能够清除弹幕的冲击波，此效果有15秒冷却");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "常驻强化暴君之怒效果，大大减弱孱弱巫咒的效果");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "升级真·暴君巨剑、破灭魔王剑、寂虚之光、斥责法杖和幻魂归墟，使它们强度相当于金源武器");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Surrender.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Spawn a flash that clears projectiles when taken damage, the effect has a 15s cooldown");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Grant buffed Tyrant's Fury buff, greatly reduce the effect of Vulnerable Hex");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Upgrade True Tyrant's Ultisword, Devil's Devastation, Dark Spark, Keelhaul and Phantasmal Ruin to auric tier");
        }
        public override void SetEnchDefaults()
        {
            Item.defense += 50;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchAuric = CSToggle.EnchAuric;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Auric");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Auric.AuricTeslaCuisses>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Ataraxia>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.GaelsGreatsword>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.YharimsCrystal>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.TheWand>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}