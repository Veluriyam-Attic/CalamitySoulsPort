// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Force/ForceRevolution.cs ｜ 对应旧版类：ForceRevolution（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Force
{
    public class ForceRevolutionV104 : V104ForceBase
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Revolution";
            ChineseName = "起义";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                V104ForceBase.QuickModiDescription(tooltips, "世界属于人民。");
                V104ForceBase.QuickModiLine(tooltips, "大大增加空中移速，受击冰冻周围敌人", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSnowruffianV104>());
                V104ForceBase.QuickModiLine(tooltips, "免疫击退和沙暴狂风", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDesertprowlerV104>());
                V104ForceBase.QuickModiLine(tooltips, "擦弹给予你短暂的无敌帧和伤害提升", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchStatigelV104>());
                V104ForceBase.QuickModiLine(tooltips, "免疫击退，击中敌人时造成25点真实伤害", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchForbiddenV104>());
                V104ForceBase.QuickModiLine(tooltips, "受伤时闪避，冷却为60秒，免疫伊卡洛斯之愚蠢", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDaedalusV104>());
                V104ForceBase.QuickModiLine(tooltips, "击中敌人生成多种增益叶片", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchTarragonV104>());
                V104ForceBase.QuickModiLine(tooltips, "林海魔石的复活效果", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSilvaV104>());
                V104ForceBase.QuickModiLine(tooltips, "召唤一个林海水晶为你而战", ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceRevolutionV104>());
                return;
            }
            V104ForceBase.QuickModiDescription(tooltips, "The world belong to the civilization.");
            V104ForceBase.QuickModiLine(tooltips, "Greatly increases your aerial speed, freezes nearby enermies on hurt", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSnowruffianV104>());
            V104ForceBase.QuickModiLine(tooltips, "Immunity to knockback and Mighty Wind", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDesertprowlerV104>());
            V104ForceBase.QuickModiLine(tooltips, "You grant extra immunity frames and damage boost on grazing", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchStatigelV104>());
            V104ForceBase.QuickModiLine(tooltips, "Attacks deal an extra 25 true damage to enermies", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchForbiddenV104>());
            V104ForceBase.QuickModiLine(tooltips, "Evade damage with a 60s cooldown, immunity to Icarus' Folly", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDaedalusV104>());
            V104ForceBase.QuickModiLine(tooltips, "Hitting enermies spawn varies of bonus leaves", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchTarragonV104>());
            V104ForceBase.QuickModiLine(tooltips, "Revive effect of Silva Enchantment", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSilvaV104>());
            V104ForceBase.QuickModiLine(tooltips, "A silva crystal fight for you", ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceRevolutionV104>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().ForceRevolution = CSToggleV104.ForceRevolution;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSnowruffianV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchStatigelV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchForbiddenV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDaedalusV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchTarragonV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchSilvaV104>());
            recipe.AddTile(ModContent.TileType<global::CalamitySouls.Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}
