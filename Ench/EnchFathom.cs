using Terraria;
using CalamityMod.Items.Armor.FathomSwarmer;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchFathom : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Fathom Swarmer";
            ChineseName = "幻渊鱼群";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "63A0A4";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「合则存，分必亡。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "+10%伤害，+2召唤栏，在深渊提供大量光照和呼吸");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "召唤一个围着你转圈，摧毁敌对弹幕的幻渊小鱼");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchFathom>(),
                    ModContent.ItemType<EnchSulphurous>(),
                }, "你的攻击直接造成多种debuff而不再生成酸海剧毒区域");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「United we stand, divided we fall.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "+10% damage, +2 max minion and provides a good amount of light and greatly reduces breath loss in the abyss");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Spawn a fathom fish that circle around you destroying hostile projectiles");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchFathom>(),
                ModContent.ItemType<EnchSulphurous>(),
            }, "Your attacks inflicts tons of debuffs and no longer need the sulphurous aura");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchFathom = CSToggle.EnchFathom;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerVisage>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBoots>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.TyphonsGreed>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.FlakKraken>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.DreadmineStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Apoctolith>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
