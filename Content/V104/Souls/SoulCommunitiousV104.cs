// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulCommunitious.cs ｜ 对应旧版类：SoulCommunitious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulCommunitiousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulCommunitiousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.StressPills>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.SandCloak>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.TheTransformer>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.TheBee>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.DeepDiver>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.BloomStone>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.TheCommunity>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.Affliction>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.YharimsGift>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Communitious";
            ChineseName = "全能";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "感谢各位支持者，使这个mod成为现实。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "Thank you all the supporters making the mod reality.");
        }
    
    }
}
