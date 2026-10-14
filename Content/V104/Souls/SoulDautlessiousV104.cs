// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulDautlessious.cs ｜ 对应旧版类：SoulDautlessious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulDautlessiousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulDautlessiousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.HeartofDarkness>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.OldDie>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.ReaperToothNecklace>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.OldDukeScales>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.TheAmalgam>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.ShatteredCommunity>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Dautlessious";
            ChineseName = "无畏";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "已经……没有什么好怕的了。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "There's nothing to be scared of......Nothing.");
        }
    
    }
}
