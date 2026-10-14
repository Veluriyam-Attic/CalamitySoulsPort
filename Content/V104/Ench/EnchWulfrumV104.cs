// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchWulfrum（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchWulfrumV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Wulfrum";
            ChineseName = "钨钢";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            CSPlayerV104 mplayer = Main.LocalPlayer.V104();
            int timeInSec = (int)(mplayer.WulfrumFieldCooldown / 60f);
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「钨钢就是最好的。」");
                QuickModiLine(tooltips, "召唤一个环绕你的力场，提供照明并阻挡敌人和弹幕", Item.type);
                if (mplayer.WulfrumFieldCooldown > 0) QuickModiLine(tooltips, "力场已经破碎，再次充能时间：" + timeInSec + "秒", Item.type);
                else QuickModiLine(tooltips, "力场阻挡实体后会破碎，破碎后需要15秒的充能", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Wulfrum Makes Best.」");
            QuickModiLine(tooltips, "Cast a field that provides light and stops enermies and projectiles", Item.type);
            if (mplayer.WulfrumFieldCooldown > 0) QuickModiLine(tooltips, "The field has broken, recharging in " + timeInSec + " second", Item.type);
            else QuickModiLine(tooltips, "The field breaks after blocking entities, and take 15s to recharge after broken", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchWulfrum = CSToggleV104.EnchWulfrum;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Wulfrum");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumJacket>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumOveralls>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.WulfrumKnife>());
            recipe.AddIngredient(ModContent.ItemType<CalamityPlantation.Items.SummonItems.WulfrumControl>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
