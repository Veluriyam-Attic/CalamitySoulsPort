using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using CalamitySouls.Spell;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Force
{
    public class ForceAnnihilation : BasicForce
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Annihilation";
            ChineseName = "湮灭";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = CSUtils.TooltipHotkeyString(CalamitySouls.BrimflameKey);
            string key1 = CalamitySouls.FearmongerArea.TooltipHotkeyString();
            string key2 = CalamitySouls.FearmongerMark.TooltipHotkeyString();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "这超越自然的力量将抹除一切存在。");
                QuickModiLine(tooltips, ItemType<SpellGlobe>(), "让你下落更快，免疫坠落伤害，受伤时回复100血量，此效果冷却为25秒");
                QuickModiLine(tooltips, ItemType<SpellGlobe>(), "增加25%伤害，但是减少25%暴击率，受伤时造成爆炸，你的攻击有几率吸血");
                QuickModiLine(tooltips, ItemType<SpellDespair>(), "按" + hotkey + "开启硫火暴乱效果，同时释放硫火激光和血炎爆破");
                QuickModiLine(tooltips, ItemType<SpellDespair>(), "半血的敌人被攻击时几率掉落红心，血月时掉落更多血珠，+100生命上限");
                QuickModiLine(tooltips, ItemType<SpellDespair>(), "免疫大多数火系列减益，获得血炎核心的效果");
                QuickModiLine(tooltips, ItemType<SpellHeavenshade>(), "日影药水始终有最大化效果，吸血鬼护符的吸血效果");
                QuickModiLine(tooltips, ItemType<SpellHeavenshade>(), "+15%武器使用速度，你的攻击有几率生成天堂之影碎片");
                QuickModiLine(tooltips, ItemType<SpellHeavenshade>(), "常驻宇宙之怒和宇宙狂怒增益，不使用物品一段时间后获得不可检测增益");
                QuickModiLine(tooltips, ItemType<EnchFearmonger>(), "取消手持非召唤武器时的召唤伤害衰减，极大增加生命回复");
                QuickModiLine(tooltips, ItemType<EnchFearmonger>(), "受击或者按" + key1 + "时生成一个霜冻区域，按" + key2 + "锁定距离鼠标最近的目标");
                QuickModiLine(tooltips, Item.type, "+65%移速和加速度，召唤能够吸血并消除弹幕的湮灭子机为你而战");
                return;
            }
            QuickModiDescription(tooltips, "The unnatural power may demolish all existance.");
            QuickModiLine(tooltips, ItemType<SpellGlobe>(), "Allows you to fall more quickly and disables fall damage, you regen 100 hp when hurt with a 25s cooldown");
            QuickModiLine(tooltips, ItemType<SpellGlobe>(), "+25% damage but -25% crit chance, you emit a blazing explosion when hurt, and your attack deal lifesteal");
            QuickModiLine(tooltips, ItemType<SpellDespair>(), "Press " + hotkey + " to trigger brimflame frenzy effect and release brimstone rays and blood eruption");
            QuickModiLine(tooltips, ItemType<SpellDespair>(), "Enemies below 50% life may drop hearts when struck and drop more blood orbs during a blood moon, +100 Max Hp");
            QuickModiLine(tooltips, ItemType<SpellDespair>(), "Immnue to most fire-relative debuffs, you graant bloodflare core effect");
            QuickModiLine(tooltips, ItemType<SpellHeavenshade>(), "Penumbra potions always build stealth at max effectiveness, lifesteal effect of vampiric talisman");
            QuickModiLine(tooltips, ItemType<SpellHeavenshade>(), "+15% weapon fire rate, Your attack may create heavenshade shards");
            QuickModiLine(tooltips, ItemType<SpellHeavenshade>(), "You are imbued with cosmic wrath and rage and after a period of not using items you grant Undetected buff");
            QuickModiLine(tooltips, ItemType<EnchFearmonger>(), "The minion damage nerf while wielding weaponry is cancelled, massively increase life regen");
            QuickModiLine(tooltips, ItemType<EnchFearmonger>(), "Being attacked or press " + key1 + " spawn a frigid area, press " + key2 + " to mark closet npc to mouse");
            QuickModiLine(tooltips, Item.type, "+65% movement speed and acceleration, summon lifestealing and proj-blocking annihilation options to fight for you");
        }
        public override void SetForceDefaults()
        {
            Item.defense += 25;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().ForceAnnihilation = CSToggle.ForceAnnihilation;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SpellGlobe>());
            recipe.AddIngredient(ModContent.ItemType<SpellDespair>());
            recipe.AddIngredient(ModContent.ItemType<SpellHeavenshade>());
            recipe.AddIngredient(ModContent.ItemType<EnchFearmonger>());
            recipe.AddTile(ModContent.TileType<Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}