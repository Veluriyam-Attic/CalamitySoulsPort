// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchSilva（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchSilvaV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Silva";
            ChineseName = "始源林海";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「神话树妖闭上了她的双眼。」");
                QuickModiLine(tooltips, "受到致命伤害时，原地满血复活并获得一段时间的无敌", ModContent.ItemType<CalamityMod.Items.Armor.Silva.SilvaArmor>());
                QuickModiLine(tooltips, "每次复活都会降低你的伤害和免伤，并回复敌人一定量生命值", Item.type);
                QuickModiLine(tooltips, "如果复活过，取下此饰品会使你当场死亡", Item.type);
                if (Main.LocalPlayer.difficulty != 0)
                    QuickModiLine(tooltips, "温馨提示：本魔石的复活效果为死亡后再复活，请中核和硬核玩家谨慎使用",
                        ModContent.ItemType<CalamityMod.Items.Placeables.DraedonStructures.LaboratoryElectricPanelItem>());
                return;
            }
            QuickModiLine(tooltips, "「The mythical Dryad closed her eyes.」");
            QuickModiLine(tooltips, "Revive with full health and grant a period of immunity upon reciving fatal damage", ModContent.ItemType<CalamityMod.Items.Armor.Silva.SilvaArmor>());
            QuickModiLine(tooltips, "Your damage and damage reduction decreases and all enermy recovers healith every time you revived", Item.type);
            QuickModiLine(tooltips, "Unequipping the accessory leads to instant death if you have revived", Item.type);
            if (Main.LocalPlayer.difficulty != 0)
            {
                QuickModiLine(tooltips, "Tip: the revive effect of this enchantment is executed AFTER death",
                        ModContent.ItemType<CalamityMod.Items.Placeables.DraedonStructures.LaboratoryElectricPanelItem>());
                QuickModiLine(tooltips, "Mediumcore players and hardcore players please use with caution",
                        ModContent.ItemType<CalamityMod.Items.Placeables.DraedonStructures.LaboratoryElectricPanelItem>());
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchSilva = CSToggleV104.EnchSilva;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Silva");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Silva.SilvaArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Silva.SilvaLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.SilvaWings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Mounts.GazeOfCrysthamyr>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.FurnitureSilva.SilvaCandelabra>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
