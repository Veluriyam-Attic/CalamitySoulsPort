using Terraria;
using CalamityMod.Items.Armor.DesertProwler;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamitySouls;
using System.Collections.Generic;

namespace CalamitySouls.Ench
{
    public class EnchDesertprowler : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Desert Prowler";
            ChineseName = "沙漠游侠";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "C29151";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「面对沙暴吧。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "攻击额外造成5真实伤害，击中敌人有很小几率召唤强大的尘罩");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "免疫击退和强劲的风，你对沙尘精、沙鲨和大沙鲨造成双倍伤害");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "站立而且不使用物品超过5s后，你获得一次会将你吹上天的沙暴闪避，此效果有60s冷却");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Now face the sandstorm」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Attacks deal an extra 5 true damage and has a chance to create a powerful dust veil on enermy hits");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Immune to knockback and Mighty Wind, your attack deals doubled damage to Sand Elemental, sandsharks and the Great Sand Shark");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "After standing still and not using item for 5 seconds you gain a sandstorm dodge which lifts you up, which have a 60s Cooldown");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchDesert = CSToggle.EnchDesert;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerHat>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerShirt>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerPants>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.SandCloak>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.BurntSienna>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.StormjawStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.NastyCholla>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
