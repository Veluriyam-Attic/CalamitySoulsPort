// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchSulphurous（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchSulphurousV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Sulphurous";
            ChineseName = "硫磺";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「侵蚀使得存在更有力。」");
                QuickModiLine(tooltips, "你的攻击造成辐射", ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousBreastplate>());
                QuickModiLine(tooltips, "你的周围环绕着超酷的酸海剧毒区域", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「Corrosion leads to better existance.」");
            QuickModiLine(tooltips, "Your attack inflicts Irradiated", ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousBreastplate>());
            QuickModiLine(tooltips, "Cool acid area are around you", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchSulphurous = CSToggleV104.EnchSulphurous;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Basher>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ParasiticSceptor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.RustyBeaconPrototype>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
