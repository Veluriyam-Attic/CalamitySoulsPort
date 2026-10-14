// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchFearmonger（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchFearmongerV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Fearmonger";
            ChineseName = "神惧者";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string tfkey = CalamitySouls.FearmongerTimeFreezeKey.TooltipHotkeyString();
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「即使最强大的神明也对你感到恐惧。」");
                QuickModiLine(tooltips, "取消手持非召唤武器时的召唤伤害衰减，极大增加生命回复", ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerPlateMail>());
                QuickModiLine(tooltips, "按" + tfkey + "时让时间冻结，再次按下以取消", Item.type);
                QuickModiLine(tooltips, "时间冻结会使得你的生命值快速降低", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Even the most magnificent Gods treat you with fear.」");
            QuickModiLine(tooltips, "The minion damage nerf while wielding weaponry is cancelled, massively increase life regen", ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerPlateMail>());
            QuickModiLine(tooltips, "Press " + tfkey + " to freeze the time, press again to cancel", Item.type);
            QuickModiLine(tooltips, "Frozen time decreases your health quickly", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchFearmonger = CSToggleV104.EnchFearmonger;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreathelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Ammo.BloodRune>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CorvidHarbringerStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Hypothermia>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
