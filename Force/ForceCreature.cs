using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using CalamitySouls.Spell;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Force
{
    public class ForceCreature : BasicForce
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Creature";
            ChineseName = "生灵";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "生灵为寰宇之主。");
                QuickModiLine(tooltips, ItemType<SpellOasis>(), "提供水下呼吸，消除水的阻力，每半分钟获得77秒阿米迪亚斯的祝福");
                QuickModiLine(tooltips, ItemType<SpellOasis>(), "当拥有任意套装效果时，大大增强最大等级的饰品前缀");
                QuickModiLine(tooltips, ItemType<SpellOasis>(), "蛤对你友好，获得一个强大的冲刺");
                QuickModiLine(tooltips, ItemType<SpellAquatic>(), "免疫酸海剧毒，在深渊提供大量呼吸");
                QuickModiLine(tooltips, ItemType<SpellAquatic>(), "召唤一个摧毁敌对弹幕的幻渊小鱼，你的攻击造成多种debuff");
                QuickModiLine(tooltips, ItemType<EnchReaver>(), "半血以下增加20%伤害，苍华之庭速度加倍");
                QuickModiLine(tooltips, ItemType<EnchOmega>(), "触手会汲取敌人生命治疗你，你常驻深渊狂乱状态");
                QuickModiLine(tooltips, Item.type, "击中敌人产生大量孢子球");
                return;
            }
            QuickModiDescription(tooltips, "Creature is the owner of cosmos.");
            QuickModiLine(tooltips, ItemType<SpellOasis>(), "Gills effect, provides increased underwater mobility, grants 77s Amidias' Blessing every half minute");
            QuickModiLine(tooltips, ItemType<SpellOasis>(), "When you have any kind of set bonus, greatly buff your accessories prefixes if they are at maxinum level");
            QuickModiLine(tooltips, ItemType<SpellOasis>(), "Clams become friendly, you grant a powerful dash");
            QuickModiLine(tooltips, ItemType<SpellAquatic>(), "Grant immunity to sulphurous poisoning and greatly reduces breath loss in the abyss");
            QuickModiLine(tooltips, ItemType<SpellAquatic>(), "Spawn a fathom fish that destroys hostile projectiles, your attacks inflicts tons of debuffs");
            QuickModiLine(tooltips, ItemType<EnchReaver>(), "Extra 20% damage under half life, plantation staff minion has doubled speed");
            QuickModiLine(tooltips, ItemType<EnchOmega>(), "Tentacles heal you by sucking enemy life and you always have abyssal madness activated");
            QuickModiLine(tooltips, Item.type, "Enermy hit spawn plenty of spore balls");
        }
        public override void SetForceDefaults()
        {
            Item.defense += 17;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().ForceCreature = CSToggle.ForceCreature;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SpellOasis>());
            recipe.AddIngredient(ModContent.ItemType<SpellAquatic>());
            recipe.AddIngredient(ModContent.ItemType<EnchReaver>());
            recipe.AddIngredient(ModContent.ItemType<EnchOmega>());
            recipe.AddTile(ModContent.TileType<Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}