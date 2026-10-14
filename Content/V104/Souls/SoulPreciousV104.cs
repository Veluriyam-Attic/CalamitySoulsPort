// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulPrecious.cs ｜ 对应旧版类：SoulPrecious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulPreciousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulPreciousV104";

        public override List<int> MaterialList => new List<int>
        {
            ItemID.Hoverboard,
            ItemID.SniperScope,
            ModContent.ItemType<CalamityMod.Items.Accessories.DynamoStemCells>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.DaawnlightSpiritOrigin>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Precious";
            ChineseName = "精准";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "百步穿杨。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "Pierce a willow leaf with your arrow from a hundred paces.");
        }
    
    }
}
