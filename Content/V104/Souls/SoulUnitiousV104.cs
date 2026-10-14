// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulUnitious.cs ｜ 对应旧版类：SoulUnitious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulUnitiousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulUnitiousV104";

        public override List<int> MaterialList => new List<int>
        {
            ItemID.HuntressBuckler,
            ModContent.ItemType<CalamityMod.Items.Accessories.PhantomicArtifact>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.Nucleogenesis>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Unitious";
            ChineseName = "团结";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "把特殊的人团结到一起，去做一般人做不到的事");
            else V104SoulBase.QuickAddSoulTip(tooltips, "Gather specific ones together and do something we can't do.");
        }
    
    }
}
