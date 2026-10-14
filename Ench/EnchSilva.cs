using Terraria;
using CalamityMod.Items.Armor.Silva;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Items;

namespace CalamitySouls.Ench
{
    public class EnchSilva : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Silva";
            ChineseName = "始源林海";
            lineAmount = 5;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "E2BC4A";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「神话树妖闭上了她的双眼。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "所有攻击生成治疗叶珠，+5%最大移速和加速度，始源林海复活效果");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.SilvaWings>(), color) +
                    "始源林海复活直接治疗至半血，增加80最大生命");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "始源林海复活拥有180秒的再次充能");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "始源林海无敌的时间增加到20秒，且最开始获得5秒真正的无敌");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「The mythical Dryad closed her eyes.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "All attack spawn healing leaf orbs on enemy hits, +5% max run speed and acceleration, silva revival effect");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.SilvaWings>(), color) +
                "The Silva revive heals you to half health, +80 max health");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "Silva revive can recharge after 180s");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "Silva invincibility increased to 20s, and grant 5s true invulnerability at first");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchSilva = CSToggle.EnchSilva;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Silva");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Silva.SilvaArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Silva.SilvaLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.SilvaWings>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}