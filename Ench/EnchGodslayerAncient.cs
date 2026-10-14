using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchGodslayerAncient : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Ancient Godslayer";
            ChineseName = "远古弑神者";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "5F21A0";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「追随神谕。」");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "增加21%伤害，+50%加速度");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "不使用物品两秒会获得异界增益，使用物品会打破它");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "异界增益在非boss战期间使你的移速增加50%，高速移动时你能反弹弹幕");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Follow the Oracle.」");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "+21% damage, +50% acceleration");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Not using item for 2s grant you Otherworldly buff, and the use of any item breaks it");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "With the buff you grant 50% more speed while no boss is alive, and when moving quickly you can reflect projectiles");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchGodslayerAncient = CSToggle.EnchGodslayerAncient;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerHelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Vanity.AncientGodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.MirrorBlade>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.TheStorm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.Cosmilamp>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.CosmicKunai>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}