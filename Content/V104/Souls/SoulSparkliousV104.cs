// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulSparklious.cs ｜ 对应旧版类：SoulSparklious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulSparkliousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulSparkliousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.ScuttlersJewel>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.RuinMedallion>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.AmidiasPendant>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.CorrosiveSpine>(),
            ModContent.ItemType<CalamityMod.Items.Tools.MomentumCapacitor>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.EtherealExtorter>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.VeneratedLocket>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.DragonScales>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Sparklious";
            ChineseName = "闪烁";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "帧数解离者.");
            else V104SoulBase.QuickAddSoulTip(tooltips, "Fps dissociator.");
        }
    
    }
}
