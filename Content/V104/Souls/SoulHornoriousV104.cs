// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulHornorious.cs ｜ 对应旧版类：SoulHornorious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulHornoriousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulHornoriousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.FungalSymbiote>(),
            ItemID.YoyoBag,
            ItemID.CelestialShell,
            ModContent.ItemType<CalamityMod.Items.Accessories.ElementalGauntlet>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Honorious";
            ChineseName = "荣光";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "为荣耀而战！");
            else V104SoulBase.QuickAddSoulTip(tooltips, "For honor and glory!");
        }
    
    }
}
