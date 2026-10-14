// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulMysterious.cs ｜ 对应旧版类：SoulMysterious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulMysteriousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulMysteriousV104";

        public override List<int> MaterialList => new List<int>
        {
            ItemID.CelestialCuffs,
            ModContent.ItemType<CalamityMod.Items.Accessories.PermafrostsConcoction>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.ChaosStone>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.EtherealTalisman>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Mysterious";
            ChineseName = "神秘";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            V104SoulBase.QuickAddSoulTip(tooltips, "_____");
        }
    
    }
}
