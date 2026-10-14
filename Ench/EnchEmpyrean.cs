using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchEmpyrean : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Empyrean";
            ChineseName = "皇天";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "9CFFEE";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「裁断之权现在在你手中。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "击中敌人会释放各种各样的额外弹幕，常驻宇宙之怒和宇宙狂怒增益");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "增加40%移速和加速度，一个皇天幻影保护你，并根据手持物品切换攻击方式");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchEmpyrean>(),
                    ModContent.ItemType<EnchUmbraphile>(),
                }, "你的攻击会生成天堂之影碎片，取代皇天弹幕和幻影");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「The justice is within your hands.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Hitting a creature release tons of projectiles, you are imbued with cosmic wrath and rage");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "+40% acceleration and speed, a empyrean phantasm assist you and switch attack pattern according to your holding item");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                    ModContent.ItemType<EnchEmpyrean>(),
                    ModContent.ItemType<EnchUmbraphile>(),
            }, "Your attack would create heavenshade shards instead of empyrean projectiles and spectre");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        { 
            player.CS().EnchEmpyrean = CSToggle.EnchEmpyrean;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Empyrean.EmpyreanMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Empyrean.EmpyreanCloak>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Empyrean.EmpyreanCuisses>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
