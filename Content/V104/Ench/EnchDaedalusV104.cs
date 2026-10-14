// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchDaedalus（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchDaedalusV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Daedalus";
            ChineseName = "代达罗斯";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「从太阳处逃离者建立了一个冻结的世界」");
                QuickModiLine(tooltips, "受伤时闪避并生成一圈追踪水晶，冷却60秒", ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusBreastplate>());
                QuickModiLine(tooltips, "免疫伊卡洛斯之愚蠢，真代达罗斯水晶协助你", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「The one who escaped from the sun build up a frozen world.」");
            QuickModiLine(tooltips, "You can evade damage and burst out chasing crystal shard with a 60s cooldown", ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusBreastplate>());
            QuickModiLine(tooltips, "Immunity to Icarus' Folly, a true daedalus crystal assist you", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchDaedalus = CSToggleV104.EnchDaedalus;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Daedalus");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.StarnightLance>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.DarkechoGreatbow>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
