// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulDevotious.cs ｜ 对应旧版类：SoulDevotious（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulDevotiousV104 : V104SoulBase
    {
        public override string ToggleName => "SoulDevotiousV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<CalamityMod.Items.Accessories.Laudanum>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.GiantPearl>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.AquaticEmblem>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.GiantTortoiseShell>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.AmbrosialAmpoule>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.Regenerator>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.TheCamper>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.BloodflareCore>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.TheEvolution>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.RampartofDeities>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.TheSponge>(),
            ModContent.ItemType<CalamityMod.Items.Accessories.DraedonsHeart>(),
        };
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Devotious";
            ChineseName = "奉献";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "一夫当关，万夫莫开。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "One man can hold out against ten thousand.");
        }
    
    }
}
