// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchReaver（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchReaverV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Reaver";
            ChineseName = "掠夺者";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            byte effect = Main.LocalPlayer.V104().ReaverEffectSelect;
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「草」");
                QuickModiLine(tooltips, string.Format("按{0}以切换此魔石的三种效果", V104Utils.TooltipHotkeyString(CalamitySouls.ReaverKey)), ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>());
                QuickModiLine(tooltips, GetEffectString(effect), GetEffectItem(effect));
                QuickModiLine(tooltips, GetEffectString(effect, true), GetEffectItem(effect));
                return;
            }
            QuickModiLine(tooltips, "「草」");
            QuickModiLine(tooltips, string.Format("Press {0} to switch between the 3 effects of the ench", V104Utils.TooltipHotkeyString(CalamitySouls.ReaverKey)), ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>());
            QuickModiLine(tooltips, GetEffectString(effect), GetEffectItem(effect));
            QuickModiLine(tooltips, GetEffectString(effect, true), GetEffectItem(effect));
        }
        public static string GetEffectString(byte effect, bool line2 = false)
        {
            string text = "You already enjoys all effects";
            if (!line2)
            {
                if (V104Utils.UsingChi)
                {
                    text = "你已经拥有了全部效果";
                    switch (effect)
                    {
                        case 0:
                            text = "防御效果：抵消负面生命回复，你的攻击有几率吸取生命";
                            break;
                        case 1:
                            text = "速度效果：增加20%钩爪的掷出和拉回速度";
                            break;
                        case 2:
                            text = "探索效果：大大增加挖掘放置速度";
                            break;
                    }
                    return text;
                }
                switch (effect)
                {
                    case 0:
                        text = "Defense effect: Cancel negative life regen, your attack may lifesteal";
                        break;
                    case 1:
                        text = "Speed effect: Hooks fly out and retract 20% faster";
                        break;
                    case 2:
                        text = "Explore effect: Greatly increase your digging and placing speed";
                        break;
                }
            }
            else
            {
                if (V104Utils.UsingChi)
                {
                    text = "包括防御、速度和探索效果";
                    switch (effect)
                    {
                        case 0:
                            text = "酒精中毒会让抵消负面生命回复失效";
                            break;
                        case 1:
                            text = "略微减少冲刺冷却";
                            break;
                        case 2:
                            text = "大大增加挖掘和物品拾取距离";
                            break;
                    }
                    return text;
                }
                text = "Including defense, speed and explore effect";
                switch (effect)
                {
                    case 0:
                        text = "Alcohol poison would disable the cancel effect";
                        break;
                    case 1:
                        text = "Slightly reduce dash cooldown";
                        break;
                    case 2:
                        text = "Greatly increase your digging and grab range";
                        break;
                }
            }
            return text;
        }
        public static int GetEffectItem(byte effect)
        {
            // ⭐ [2026-10-11 修复] 1.3 EnchReaver.cs:109-114 是【三个头盔 + 一个胸甲】：
            //   effect 0 防御 → ReaverHelm ／ 1 速度 → ReaverVisage ／ 2 探索 → ReaverHeadgear ／ 其余 → ReaverScaleMail
            //   2.x 把这三个头盔【改名】为 ReaverHeadTank / ReaverHeadMobility / ReaverHeadExplore
            //   （本文件配方注释 :122-127 已记载该改名，只是当时没顺手改到这里 ✗）⇒ 按效果名一一对应：
            //     防御↔Tank、速度↔Mobility、探索↔Explore
            //   原 V104 把四个分支全写成 ReaverScaleMail（胸甲）⇒ tooltip 行首显示成「三个胸甲」
            //   （玩家实测：1.3 是「胸甲和两个头盔」✓ —— 因为第 1 行是胸甲，第 2/3 行都用 GetEffectItem(effect)，
            //     effect=0 时两者取到同一个头盔 ✓）
            if (effect == 0) return ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadTank>();
            else if (effect == 1) return ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadMobility>();
            else if (effect == 2) return ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadExplore>();
            return ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchReaver = CSToggleV104.EnchReaver;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            // NOTE (2026-10-03): 1.3 v1.0.4.0 requires THREE different Reaver helmets:
            //   ReaverHelm / ReaverVisage / ReaverHeadgear. In Calamity 2.x these were RENAMED to
            //   ReaverHead{Explore,Mobility,Tank} (renamed, not removed). The naming is backed by this
            //   mod's own recipe group "CS:Reaver" (see CSRecipe.cs). Because 1.3 requires ALL THREE,
            //   listing the three 2.x helmets is exactly 1:1 with 1.3 and does not depend on knowing
            //   which 1.3 name maps to which 2.x name.
            // History: this slot used to hold FOUR copies of ReaverScaleMail (the chestplate) as a
            //   stand-in for the three helmets; per user decision that substitute was removed.
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadExplore>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadMobility>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadTank>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverCuisses>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.ViralSprout>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
