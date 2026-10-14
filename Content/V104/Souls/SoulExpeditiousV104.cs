// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulExpeditious.cs ｜ 对应旧版类：SoulExpeditious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulExpeditiousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulExpeditiousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.AlluringBait>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.OceanCrest>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.EnchantedPearl>(),
            ModContent.ItemType<CalamityMod.Items.Pets.ThiefsDime>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.SupremeBaitTackleBoxFishingStation>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.AbyssalDivingSuit>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Expedious";
            ChineseName = "探寻";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "历史将永远保持螺旋式上升。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "History spirals upwards forever.");
        }
    
    }
}
