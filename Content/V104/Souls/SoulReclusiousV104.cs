// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulReclusious.cs ｜ 对应旧版类：SoulReclusious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulReclusiousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulReclusiousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.RottenDogtooth>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.GloveOfPrecision>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.GloveOfRecklessness>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.VampiricTalisman>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.Nanotech>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.EclipseMirror>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Reclusious";
            ChineseName = "隐者";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "背刺！！！");
            else V104SoulBase.QuickAddSoulTip(tooltips, "BACKSTAB!!!");
        }
    
    }
}
