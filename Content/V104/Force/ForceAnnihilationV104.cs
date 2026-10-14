// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Force/ForceAnnihilation.cs ｜ 对应旧版类：ForceAnnihilation（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Force
{
    public class ForceAnnihilationV104 : V104ForceBase
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Annihilation";
            ChineseName = "湮灭";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = CSUtils.TooltipHotkeyString(CalamitySouls.BrimflameKey);
            string tfkey = CalamitySouls.FearmongerTimeFreezeKey.TooltipHotkeyString();
            if (V104Utils.UsingChi)
            {
                V104ForceBase.QuickModiDescription(tooltips, "这超越自然的力量将抹除一切存在。");
                V104ForceBase.QuickModiLine(tooltips, "非穿透弹幕会在击中物体时释放幻星爆破", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchTitanV104>());
                V104ForceBase.QuickModiLine(tooltips, "按" + hotkey + "开启硫火暴乱效果", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchBrimflameV104>());
                V104ForceBase.QuickModiLine(tooltips, "攻击时对前方的敌人重拳出击", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchLuxAstriV104>());
                V104ForceBase.QuickModiLine(tooltips, "日炎环环绕着你，你的所有武器都附带火焰属性", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchHydrothermicV104>());
                V104ForceBase.QuickModiLine(tooltips, "给予你周围的生物和弹幕以更强的重力", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchExterminationV104>());
                V104ForceBase.QuickModiLine(tooltips, "击中敌人使得群星从天而降，一个小卫星为你回复生命值", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAstralV104>());
                V104ForceBase.QuickModiLine(tooltips, "触发硫火暴乱时释放迷失灵魂摧毁敌人", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchBloodflareV104>());
                V104ForceBase.QuickModiLine(tooltips, "极大增加生命回复，按" + tfkey + "时让时间冻结", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchFearmongerV104>());
                V104ForceBase.QuickModiLine(tooltips, "迷你至尊灾厄为你而战", ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceAnnihilationV104>());
                return;
            }
            V104ForceBase.QuickModiDescription(tooltips, "The unnatural power may demolish all existance.");
            V104ForceBase.QuickModiLine(tooltips, "Non-penetrate projectiles explodes on death", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchTitanV104>());
            V104ForceBase.QuickModiLine(tooltips, "Press " + hotkey + " to trigger a brimflame frenzy effect", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchBrimflameV104>());
            V104ForceBase.QuickModiLine(tooltips, "Punch enermies in front of you when you're attacking", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchLuxAstriV104>());
            V104ForceBase.QuickModiLine(tooltips, "Solar area around you, all your weapon are imbued with fire", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchHydrothermicV104>());
            V104ForceBase.QuickModiLine(tooltips, "Inflict stronger gravity on creatures and projectiles around you", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchExterminationV104>());
            V104ForceBase.QuickModiLine(tooltips, "Stars falls from heaven whenever you hit an enery, a mini satellite spawn healing stars for you", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAstralV104>());
            V104ForceBase.QuickModiLine(tooltips, "Unleash lost souls to destroy your enemies when triggering brimfrenzy", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchBloodflareV104>());
            V104ForceBase.QuickModiLine(tooltips, "Massively increase life regen, press " + tfkey + " to freeze the time", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchFearmongerV104>());
            V104ForceBase.QuickModiLine(tooltips, "Mini supreme calamitas fight for you", ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceAnnihilationV104>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().ForceAnnihilation = CSToggleV104.ForceAnnihilation;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchLuxAstriV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchHydrothermicV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchExterminationV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAstralV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchBloodflareV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchFearmongerV104>());
            recipe.AddTile(ModContent.TileType<global::CalamitySouls.Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}
