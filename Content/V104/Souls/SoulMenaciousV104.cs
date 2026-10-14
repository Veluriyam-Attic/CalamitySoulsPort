// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulMenacious.cs ｜ 对应旧版类：SoulMenacious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulMenaciousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulMenaciousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.TrinketofChi>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.FrostFlare>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.LeviathanAmbergris>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.HideofAstrumDeus>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.BlunderBooster>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.NebulousCore>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.ChaliceOfTheBloodGod>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.DarkSunRing>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Menacious";
            ChineseName = "险恶";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "让来敌识得何为恐惧。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "Now let them feel the horror.");
        }
    
    }
}
