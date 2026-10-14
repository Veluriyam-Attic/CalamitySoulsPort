// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulLeaderious.cs ｜ 对应旧版类：SoulLeaderious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulLeaderiousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulLeaderiousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.GladiatorsLocket>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.FungalClump>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.HowlsHeart>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.CryoStone>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.HeartoftheElements>(),
            ModContent.ItemType<CalamityMod.Items.Weapons.Summon.MutatedTruffle>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.Calamity>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Leaderious";
            ChineseName = "先驱";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "现在，时代由你引领。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "The age is in your lead now.");
        }
    
    }
}
