using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using CalamitySouls.Spell;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Force
{
    public class ForceRevolution : BasicForce
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Revolution";
            ChineseName = "起义";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "世界属于人民。");
                QuickModiLine(tooltips, ItemType<SpellEleum>(), "免疫摔伤、击退、死亡模式寒冷、冰肺、伊卡洛斯之愚蠢和强劲的风");
                QuickModiLine(tooltips, ItemType<SpellEleum>(), "受击时冰冻周围敌人和你自己，你获得代达罗斯闪避效果");
                QuickModiLine(tooltips, ItemType<SpellEleum>(), "+15%移动速度和加速度，+100生命上限，你的攻击会产生冰晶碎片");
                QuickModiLine(tooltips, ItemType<EnchDesertprowler>(), "攻击额外造成5真实伤害，击中敌人有很小几率召唤强大的尘罩");
                QuickModiLine(tooltips, ItemType<EnchDesertprowler>(), "你对沙尘精、沙鲨和大沙鲨造成双倍伤害，沙漠游侠闪避效果");
                QuickModiLine(tooltips, ItemType<SpellBetrayal>(), "增加10跳跃高度，48%跳跃速度和25%武器使用速度，无敌时间延长");
                QuickModiLine(tooltips, ItemType<SpellBetrayal>(), "击中敌人生成不同的增益叶片，击杀敌人有几率掉落额外的红心");
                QuickModiLine(tooltips, ItemType<SpellBetrayal>(), "获得拾心和镇静效果，召唤迷你史莱姆神，大大减少冲刺冷却");
                QuickModiLine(tooltips, ItemType<EnchSilva>(), "所有攻击生成治疗叶珠，你获得大大增强的林海复活效果");
                return;
            }
            QuickModiDescription(tooltips, "The world belong to the civilization.");
            QuickModiLine(tooltips, ItemType<SpellEleum>(), "Immunity to fall damage, knockback, deathmode coldness, frozen lung, icarus' folly and mighty wind");
            QuickModiLine(tooltips, ItemType<SpellEleum>(), "Freezes nearby enermies and yourself on hurt and you are granted daedalus evasion effect");
            QuickModiLine(tooltips, ItemType<SpellEleum>(), "+10% movement speed and acceleration, +100 max health, your attacks spawn ice crystal shards");
            QuickModiLine(tooltips, ItemType<EnchDesertprowler>(), "Attacks deal an extra 5 true damage and has a chance to create a powerful dust veil on enermy hits");
            QuickModiLine(tooltips, ItemType<EnchDesertprowler>(), "You deal doubled damage to sandstorm minibosses, desert prowler dodge effect");
            QuickModiLine(tooltips, ItemType<SpellBetrayal>(), "+10 jump height, 48% jump speed and 25% weapon fire rate, you have increased immunity frame");
            QuickModiLine(tooltips, ItemType<SpellBetrayal>(), "Hitting enermies spawn different bonus leaves, killing enemies have a chance to drop extra hearts");
            QuickModiLine(tooltips, ItemType<SpellBetrayal>(), "Heartreach and calm effect, summon a mini slime god to heal you, greatly decreases your dash cooldown");
            QuickModiLine(tooltips, ItemType<EnchSilva>(), "All attack spawn healing leaf orbs, you are granted magnificently buffed silva revive effect");
        }
        public override void SetForceDefaults()
        {
            Item.defense = 6;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().ForceRevolution = CSToggle.ForceRevolution;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SpellEleum>());
            recipe.AddIngredient(ModContent.ItemType<EnchDesertprowler>());
            recipe.AddIngredient(ModContent.ItemType<SpellBetrayal>());
            recipe.AddIngredient(ModContent.ItemType<EnchSilva>());
            recipe.AddTile(ModContent.TileType<Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}