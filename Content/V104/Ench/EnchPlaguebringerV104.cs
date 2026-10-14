// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchPlaguebringer（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchPlaguebringerV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Plague Bringer";
            ChineseName = "瘟疫使者";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「从内部将他们消灭殆尽。」");
                QuickModiLine(tooltips, "获得能够碾压敌人的瘟疫冲刺", ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerCarapace>());
                QuickModiLine(tooltips, "每隔一段时间发射追踪的瘟疫核弹，对周围目标造成一次巨大伤害", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Destroy them from inside.」");
            QuickModiLine(tooltips, "Grants a plague dash to ram enermies", ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerCarapace>());
            QuickModiLine(tooltips, "You launch a plague nuke periodly which deals great damage to nearby enermies", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchPlagueBringer = CSToggleV104.EnchPlagueBringer;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerVisor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerCarapace>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerPistons>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.PestilentDefiler>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.PlagueStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.TheSyringe>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
