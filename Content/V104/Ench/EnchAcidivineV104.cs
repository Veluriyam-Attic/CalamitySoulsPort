// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchAcidivine（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchAcidivineV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Acidivine";
            ChineseName = "圣蚀";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「千里之堤，溃于蚁穴。」");
                QuickModiLine(tooltips, "永久酸蚀雕像效果，你的攻击造成辐射和深海水压", ModContent.ItemType<EnchSulphurousV104>());
                QuickModiLine(tooltips, "你的周围环绕着硫海剧毒区域", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「A small leak will sink a great ship.」");
            QuickModiLine(tooltips, "Permanent effigy of decay effect, your attack inflicts Irradiated and Crush Depth", ModContent.ItemType<EnchSulphurousV104>());
            QuickModiLine(tooltips, "Sulphurous area are around you", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchAcidivine = CSToggleV104.EnchAcidivine;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            // 1.3 EnchAcidivine.cs:53-55 逐值照搬：三件圣蚀装备
            // （当年移植时圣蚀套尚未存在 ⇒ 被漏掉；2026-10-03 补回，用户实机发现）
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Armor.AcidivineHelmet>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Armor.AcidivineBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Armor.AcidivineLeggings>());
            recipe.AddIngredient(ModContent.ItemType<EnchSulphurousV104>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.SubmarineShocker>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.BelchingSaxophone>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.OrthoceraShell>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
