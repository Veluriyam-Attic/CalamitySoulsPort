// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchDemonshade（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;                       // player.Calamity() 扩展方法所在命名空间
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchDemonshadeV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Demonshade";
            ChineseName = "魔影";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = "";
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「在你统治者的沮丧要求下，你将踏平敌人的一切。」");
                QuickModiLine(tooltips, "按" + hotkey + "使用黑魔咒激怒所有生物10秒", ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeBreastplate>());
                return;
            }
            QuickModiLine(tooltips, "「Now you are sent to demolish the foe upon your ruler's despondont demand.」");
            QuickModiLine(tooltips, "Press " + hotkey + " to enrage nearby enemies with a dark magic spell for 10 seconds", ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeBreastplate>());
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchDemonshade = CSToggleV104.EnchDemonshade;
            // 魔影：置真以驱动灾厄侧的 4 处读取（1.3 CSPlayerEffects.cs:149 逐字；字段在 2.x 已改名 demonshadeSet）
            // 位置说明：此处随"佩戴"每帧执行 ⇒ 与 1.3 的激活块时机一致（力/捆绑链路径不能替代 ✗）
            player.Calamity().demonshadeSet = CSToggleV104.EnchDemonshade;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeHelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Demonshade.DemonshadeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Tools.BrimstoneLocus>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Pets.BrimstoneJewel>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CindersOfLament>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
