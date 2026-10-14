// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 来源：ext1040/Souls/SoulCalamity.cs ｜ 对应旧版类：SoulCalamity（旧版那一套保持不动）
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Souls
{
    public class SoulCalamityV104 : V104SoulBase
    {
        public override string ToggleName => "SoulCalamityV104";

        public override List<int> MaterialList => new List<int>
        {
            ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceAnnihilationV104>(),
            ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceCreatureV104>(),
            ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceRevolutionV104>(),
            ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceTechnicalV104>(),
            ModContent.ItemType<global::CalamitySouls.Content.V104.Force.ForceTyrannyV104>(),
        };
        public override bool needRock => true;
        public override void SetSoulStaticDefaults()
        {
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 10));
        }
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Calamity";
            ChineseName = "灾厄";
        }
        public override void PreModifyBaseTooltips(List<TooltipLine> tooltips) 
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulTip(tooltips, "你即灾厄。");
            else V104SoulBase.QuickAddSoulTip(tooltips, "You are the calamity.");
        }
        public override void PostModifyBaseTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi) V104SoulBase.QuickAddSoulLine(tooltips, "你的肾上腺素多造成300%伤害", Item.type);
            else V104SoulBase.QuickAddSoulLine(tooltips, "Your adrenaline deals 300% more damage", Item.type);
        }
        public override void UpdateSoul(Player player, bool hideVisual)
        {
            player.V104().AdrenalineDamageBoost *= 4f;
        }
    
    }
}
