// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Force/ForceTyranny.cs ｜ 对应旧版类：ForceTyranny（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Force
{
    public class ForceTyrannyV104 : V104ForceBase
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Tyranny";
            ChineseName = "暴政";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = CSUtils.TooltipHotkeyString(CalamitySouls.GodslayerDashKey);
            string hotkey2 = "";
            if (V104Utils.UsingChi)
            {
                V104ForceBase.QuickModiDescription(tooltips, "你即君主。");
                V104ForceBase.QuickModiLine(tooltips, "让你下落更快并免疫坠落伤害", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAerospecV104>());
                V104ForceBase.QuickModiLine(tooltips, "不使用物品10秒后获得遁于暗影增益", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchUmbraphileV104>());
                V104ForceBase.QuickModiLine(tooltips, "皇天幻影为你而战", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchEmpyreanV104>());
                V104ForceBase.QuickModiLine(tooltips, "受到伤害时随机传送到周围位置", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchGodslayerAncientV104>());
                V104ForceBase.QuickModiLine(tooltips, "按" + hotkey + "伴随强大的吞噬者幻影向鼠标方向冲刺", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchGodslayerV104>());
                V104ForceBase.QuickModiLine(tooltips, "每隔一段时间释放金源冲击波，摧毁途经的一切", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAuricV104>());
                V104ForceBase.QuickModiLine(tooltips, "按" + hotkey2 + "使用黑魔咒激怒所有生物", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDemonshadeV104>());
                return;
            }
            V104ForceBase.QuickModiDescription(tooltips, "You are the monarch.");
            V104ForceBase.QuickModiLine(tooltips, "Allows you to fall more quickly and immune to fall damage", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAerospecV104>());
            V104ForceBase.QuickModiLine(tooltips, "After 10s of not using items you grant Shadecovered buff", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchUmbraphileV104>());
            V104ForceBase.QuickModiLine(tooltips, "The empyrean phantasm fight for you", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchEmpyreanV104>());
            V104ForceBase.QuickModiLine(tooltips, "Teleport to a random position when hit", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchGodslayerAncientV104>());
            V104ForceBase.QuickModiLine(tooltips, "Press " + hotkey + " to dash towards your mouse with devourer phantom", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchGodslayerV104>());
            V104ForceBase.QuickModiLine(tooltips, "Periodly release auric flash destroying everything it passes by", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAuricV104>());
            V104ForceBase.QuickModiLine(tooltips, "Press " + hotkey2 + " to enrage nearby enemies with a dark magic spell", ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDemonshadeV104>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().ForceTyranny = CSToggleV104.ForceTyranny;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchEmpyreanV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchGodslayerAncientV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchGodslayerV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchAuricV104>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Content.V104.Ench.EnchDemonshadeV104>());
            recipe.AddTile(ModContent.TileType<global::CalamitySouls.Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}
