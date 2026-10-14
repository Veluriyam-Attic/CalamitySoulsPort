// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Force/ForceTechnical.cs ｜ 对应旧版类：ForceTechnical（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Force
{
    public class ForceTechnicalV104 : V104ForceBase
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Technical";
            ChineseName = "科技";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                V104ForceBase.QuickModiDescription(tooltips, "科技革命。");
                V104ForceBase.QuickModiLine(tooltips, "召唤一个环绕你的力场，提供照明并阻挡敌人和弹幕", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchWulfrumV104>());
                V104ForceBase.QuickModiLine(tooltips, "被击中时以视野变黑为代价释放当前拥有的肾上腺素", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPlagueReaperV104>());
                V104ForceBase.QuickModiLine(tooltips, "获得瘟疫冲刺，召唤小瘟疫使者，发射瘟疫核弹", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPlaguebringerV104>());
                V104ForceBase.QuickModiLine(tooltips, "召唤一个在你周围旋转，改变敌对弹幕方向的棱镜", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPrismaticV104>());
                V104ForceBase.QuickModiLine(tooltips, "受击生成阻挡高于50的伤害的护盾", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchExoV104>());
                return;
            }
            V104ForceBase.QuickModiDescription(tooltips, "Technological Revolution.");
            V104ForceBase.QuickModiLine(tooltips, "Cast a field that provides light and stops enermies and projectiles", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchWulfrumV104>());
            V104ForceBase.QuickModiLine(tooltips, "Being hit release Adrenaline you have at the price of darker vision", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPlagueReaperV104>());
            V104ForceBase.QuickModiLine(tooltips, "You grant plague dash, spawn lil' plague bringer and launch plague nuke", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPlaguebringerV104>());
            V104ForceBase.QuickModiLine(tooltips, "Spawn a prism that circle around you and change hostile projectile's direction", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPrismaticV104>());
            V104ForceBase.QuickModiLine(tooltips, "Cast a shield when taking damage, blocking any damage higher than 50", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchExoV104>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().ForceTechnical = CSToggleV104.ForceTechnical;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPlagueReaperV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPlaguebringerV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchPrismaticV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchExoV104>());
            recipe.AddTile(ModContent.TileType<global::CalamitySouls.Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}
