using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellPlague : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Plague";
            ChineseName = "瘟疫";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "3A514B";
            string color2 = "E54242";
            string color3 = "985247";
            int Material1 = ItemType<EnchPlagueReaper>();
            int Material2 = ItemType<EnchPlaguebringer>();
            int Material3 = 0;
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "空城");
                QuickModiLine(tooltips, "无限飞行时间，增强肾上腺素的效果而且受击不再清空肾上腺素",Material1,color1);
                QuickModiLine(tooltips, "攻击造成瘟疫，且对敌人多造成10%伤害", Material1, color1);
                QuickModiLine(tooltips, "获得强大的瘟疫冲刺，快速移动生成蜜蜂", Material2, color2);
                QuickModiLine(tooltips, "免疫瘟疫，你的攻击额外造成20真实伤害", Material2, color2);
                QuickModiLine(tooltips, "瘟疫蜂巢的效果", Material3, color3);
                QuickModiLine(tooltips, "大大增强友方瘟疫蜜蜂", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Empty city");
            QuickModiLine(tooltips, "Infinite flight time, your adrenaline deal more damage and won't be cleared on full charge", Material1, color1);
            QuickModiLine(tooltips, "Attacks inflict plague and deal 10% more damage", Material1, color1);
            QuickModiLine(tooltips, "Grant a powerful plague dash, and you spawn bees while moving quickly", Material2, color2);
            QuickModiLine(tooltips, "Immunity to plague, your attack deal an extra 20 true damage", Material2, color2);
            QuickModiLine(tooltips, "Plague Hive Effect", Material3, color3);
            QuickModiLine(tooltips, "Greatly buff friendly plague bees", Item.type, color3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellPlague = CSToggle.SpellPlague;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchPlagueReaper>());
            recipe.AddIngredient(ModContent.ItemType<EnchPlaguebringer>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.SepticSkewer>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.VitriolicViper>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.TacticalPlagueEngine>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}