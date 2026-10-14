using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using CalamitySouls.Spell;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Force
{
    public class ForceTechnical : BasicForce
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Technical";
            ChineseName = "科技";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "科技革命。");
                QuickModiLine(tooltips, ItemType<SpellStarlight>(), "增加300%击退，半血以下加15防御，召唤一个阻挡弹幕的盾");
                QuickModiLine(tooltips, ItemType<SpellStarlight>(), "攻击时在鼠标位置产生幻星爆破和幻星增幅器");
                QuickModiLine(tooltips, ItemType<SpellPlague>(), "无限飞行时间，增强肾上腺素的效果而且受击不再清空肾上腺素");
                QuickModiLine(tooltips, ItemType<SpellPlague>(), "攻击造成瘟疫和幻星感染，对敌人多造成10%伤害和额外20真实伤害");
                QuickModiLine(tooltips, ItemType<SpellPlague>(), "免疫瘟疫，瘟疫蜂巢的效果，快速移动生成蜜蜂");
                QuickModiLine(tooltips, ItemType<SpellPlague>(), "获得强大的瘟疫冲刺，大大增强友方瘟疫蜜蜂");
                QuickModiLine(tooltips, ItemType<EnchAstral>(), "获得勘探、狩猎和危险感药水的效果，大幅度增加你的全部属性");
                QuickModiLine(tooltips, ItemType<EnchAstral>(), "你获得幻星分裂效果");
                QuickModiLine(tooltips, ItemType<EnchPrismatic>(), "召唤一个在你周围环绕，改变敌对弹幕方向的棱镜");
                QuickModiLine(tooltips, ItemType<EnchPrismatic>(), "你的攻击会伴随着强大的光束，敌人的攻击将会拥有预警线");
                return;
            }
            QuickModiDescription(tooltips, "Technological Revolution.");
            QuickModiLine(tooltips, ItemType<SpellStarlight>(), "+300% knockback, +15 defense when below 50% life, spawn a shield that blocks projectiles");
            QuickModiLine(tooltips, ItemType<SpellStarlight>(), "Create astral explosion and an astral booster at mouse position when attacking");
            QuickModiLine(tooltips, ItemType<SpellPlague>(), "Infinite flight time, your adrenaline deal more damage and won't be cleared on full charge");
            QuickModiLine(tooltips, ItemType<SpellPlague>(), "Attacks inflict plague and astral infection, you deal 10% more damage and extra 20 true damage");
            QuickModiLine(tooltips, ItemType<SpellPlague>(), "Immunity to plague, plague hive effect and you spawn bees while moving quickly");
            QuickModiLine(tooltips, ItemType<SpellPlague>(), "Grant a powerful plague dash, and greatly buff friendly plague bees");
            QuickModiLine(tooltips, ItemType<EnchAstral>(), "Spenkular, Hunter and Dangersense effect, major increase to your all data");
            QuickModiLine(tooltips, ItemType<EnchAstral>(), "You are granted astral division effect");
            QuickModiLine(tooltips, ItemType<EnchPrismatic>(), "Spawn a prism that circle around you and change hostile projectile's direction");
            QuickModiLine(tooltips, ItemType<EnchPrismatic>(), "Your attack will be followed by powerful lasers, enermies' attack will have their trail shown");
        }
        public override void SetForceDefaults()
        {
            Item.defense += 10;
            Item.lifeRegen += 10;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().ForceTechnical = CSToggle.ForceTechnical;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SpellStarlight>());
            recipe.AddIngredient(ModContent.ItemType<SpellPlague>());
            recipe.AddIngredient(ModContent.ItemType<EnchAstral>());
            recipe.AddIngredient(ModContent.ItemType<EnchPrismatic>());
            recipe.AddTile(ModContent.TileType<Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}