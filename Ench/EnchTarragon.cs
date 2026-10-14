using Terraria;
using CalamityMod.Items.Armor.Tarragon;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchTarragon : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Tarragon";
            ChineseName = "龙蒿";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "D3B258";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「丛林之心与你同在。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "获得拾心和镇静效果，击杀敌人有几率掉落额外的红心");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<CalamityMod.Items.Accessories.BadgeofBravery>(), color) +
                    "增加25%武器使用速度");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "击中敌人生成不同的增益叶片，玩家吃到时会获得各种各样的加成");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchStatigel>(),
                    ModContent.ItemType<EnchTarragon>()
                }, "减半冲刺冷却");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Heart of the jungle is with you.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Heartreach and calm potion effect, enemies have a chance to drop extra hearts on death");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(ModContent.ItemType<CalamityMod.Items.Accessories.BadgeofBravery>(), color) +
                "+25% weapon fire rate");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Hitting enermies spawn different bonus leaves, player who pick them are granted all kinds of buffs");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchStatigel>(),
                ModContent.ItemType<EnchTarragon>()
            }, "Halves your dash cooldown");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchTarragon = CSToggle.EnchTarragon;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Tarragon");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Tarragon.TarragonBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Tarragon.TarragonLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.BadgeofBravery>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.TarragonThrowingDart>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}