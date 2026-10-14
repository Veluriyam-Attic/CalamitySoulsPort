using Terraria;
using CalamityMod.Items.Armor.Fearmonger;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchFearmonger : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Fearmonger";
            ChineseName = "神惧者";
            lineAmount = 6;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "CD4D23";
            string key1 = CalamitySouls.FearmongerArea.TooltipHotkeyString();
            string key2 = CalamitySouls.FearmongerMark.TooltipHotkeyString();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「即使最强大的神明也对你感到恐惧。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "取消手持非召唤武器时的召唤伤害衰减，极大增加生命回复");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "受击或者按" + key1 + "时在你周围生成一个霜冻区域，停止区域内的所有实体");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "霜冻区域存在5秒，冷却为60秒，消失时清除范围内弹幕");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "按" + key2 + "锁定距离鼠标最近的目标，你对锁定的目标多造成25%伤害，但对其他目标伤害减半");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "切换目标的冷却为15秒，可以随意清除锁定，如果目标死亡则自动清除锁定");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Even the most magnificent Gods treat you with fear.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "The minion damage nerf while wielding weaponry is cancelled, massively increase life regen");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Being attacked or press " + key1 + " spawn a frigid area stopping all entities");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "The area lasts 5s with a 60s cooldown and clear projectiles when dead");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Press " + key2 + " to mark closet npc to mouse, you deal 25% more damage to marked enermies but deal half damage to others");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "It takes 15s to mark another target, you can clear mark with no price, and the death of the marked clears the mark");
        }
        public override void SetEnchDefaults()
        {
            Item.defense += 7;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchFearmonger = CSToggle.EnchFearmonger;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreathelm>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.IceBarrage>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.EndoHydraStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CorvidHarbringerStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Hypothermia>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
