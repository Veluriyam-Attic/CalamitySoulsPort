// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulInfinity.cs ｜ 对应旧版类：SoulInfinity（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulInfinityV104 : V104SoulBase
    {
        public override string ToggleName => "SoulInfinityV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<SoulCommunitiousV104>(),
            ModContent.ItemType<SoulDevotiousV104>(),
            ModContent.ItemType<SoulExpeditiousV104>(),
            ModContent.ItemType<SoulMenaciousV104>(),
            ModContent.ItemType<SoulVelocitiousV104>(),        };
        public override bool needRock => true;
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Infinity";
            ChineseName = "无限";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "永无止境。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "It never stops.");
        }
    
    }
}
