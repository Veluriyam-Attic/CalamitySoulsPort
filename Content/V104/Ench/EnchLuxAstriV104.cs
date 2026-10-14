// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchLuxAstri（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchLuxAstriV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Lux Astri";
            ChineseName = "星光";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「幻影的荣光」");
                QuickModiLine(tooltips, "非穿透弹幕会在击中物体时释放幻星爆破", ModContent.ItemType<EnchTitanV104>());
                QuickModiLine(tooltips, "攻击时对前方的敌人重拳出击", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Phantasmal glory」");
            QuickModiLine(tooltips, "Non-penetrate projectiles explodes on death", ModContent.ItemType<EnchTitanV104>());
            QuickModiLine(tooltips, "Punch enermies in front of you when you're attacking", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchLuxAstri = CSToggleV104.EnchLuxAstri;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchTitanV104>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.TitanArm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.AstralScythe>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.StellarCannon>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
