// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulUnlimity.cs ｜ 对应旧版类：SoulUnlimity（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulUnlimityV104 : V104SoulBase
    {
        public override string ToggleName => "SoulUnlimityV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<SoulDautlessiousV104>(),
            ModContent.ItemType<SoulHornoriousV104>(),
            ModContent.ItemType<SoulLeaderiousV104>(),
            ModContent.ItemType<SoulMysteriousV104>(),
            ModContent.ItemType<SoulPreciousV104>(),
            ModContent.ItemType<SoulReclusiousV104>(),
            ModContent.ItemType<SoulSparkliousV104>(),
            ModContent.ItemType<SoulUnitiousV104>(),        };
        public override bool needRock => true;
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Unlimity";
            ChineseName = "超限";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "捕捉到运行异常：代号System.IndexOutOfRangeException：索引超出了数组的界限。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "Silently Caught Exception: System.IndexOutOfRangeException: Index was outside the bounds of the array.");
        }
    
    }
}
