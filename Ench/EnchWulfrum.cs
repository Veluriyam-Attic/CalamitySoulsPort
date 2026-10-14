using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamitySouls;
using System.Collections.Generic;

namespace CalamitySouls.Ench
{
    public class EnchWulfrum : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Wulfrum";
            ChineseName = "钨钢";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "9BAB75";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「钨钢就是最好的。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "半血以下加15防御");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "召唤一个阻挡一次不穿墙弹幕的盾");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "这个盾存在15秒，破碎后冷却15秒");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchWulfrum>(),
                    ModContent.ItemType<EnchTitan>()
                }, "使用物品时在鼠标位置召唤幻星增幅器，增加附近弹幕的伤害");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Wulfrum Makes Great.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "+15 defense when below 50% life");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Spawn a shield that blocks projectiles that cannot pass through walls once");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "The shield exists for 15s with a 15s Cooldown after broken");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                    ModContent.ItemType<EnchWulfrum>(),
                    ModContent.ItemType<EnchTitan>()
            }, "Create an astral booster at mouse position when attacking, boosting nearby projectiles' damage");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchWulfrum = CSToggle.EnchWulfrum;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Wulfrum");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumJacket>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumOveralls>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.WulfrumController>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.WulfrumKnife>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
