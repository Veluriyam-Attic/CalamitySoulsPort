using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using CalamitySouls.Spell;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Force
{
    public class ForceTyranny : BasicForce
    {
        public override void SetName(out string DisplayName, out string ChineseName)
        {
            DisplayName = "Tyranny";
            ChineseName = "暴政";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string hotkey = "";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "你即君主。");
                QuickModiLine(tooltips, ItemType<EnchGodslayerAncient>(), "+21%伤害，+50%加速度，你获得异界增益");
                QuickModiLine(tooltips, ItemType<EnchGodslayer>(), "你获得弑神者复活效果，并缩短其CD至30秒");
                QuickModiLine(tooltips, ItemType<EnchGodslayer>(), "你的攻击会生成弑神者幻影，你每隔一段时间获得不同的增益");
                QuickModiLine(tooltips, ItemType<EnchAuric>(), "受击时生成一个能够清除弹幕的冲击波，此效果有15秒冷却");
                QuickModiLine(tooltips, ItemType<EnchAuric>(), "常驻强化暴君之怒效果，大大减弱孱弱巫咒的效果");
                QuickModiLine(tooltips, ItemType<EnchDemonshade>(), "暗影回血，暗影速度，暗影减益和受击时从天而降的一些暗影弹幕");
                QuickModiLine(tooltips, ItemType<EnchDemonshade>(), "按" + hotkey + "使用黑魔咒激怒所有生物10秒");
                return;
            }
            QuickModiDescription(tooltips, "You are the monarch.");
            QuickModiLine(tooltips, ItemType<EnchGodslayerAncient>(), "+21% damage, +50% acceleration, you grant otherworldly buff");
            QuickModiLine(tooltips, ItemType<EnchGodslayer>(), "your attack will summon god slayer phantoms and you gain different bonus over time");
            QuickModiLine(tooltips, ItemType<EnchAuric>(), "Spawn a flash that clears projectiles when taken damage, the effect has a 15s cooldown");
            QuickModiLine(tooltips, ItemType<EnchAuric>(), "Grant buffed tyrant's fury buff, greatly reduce the effect of vulnerable hex");
            QuickModiLine(tooltips, ItemType<EnchDemonshade>(), "Shadow regen, shadow speed, shadow debuff, and some shadow projectiles will fire down when you are hit");
            QuickModiLine(tooltips, ItemType<EnchDemonshade>(), "Press " + hotkey + " to enrage nearby enemies with a dark magic spell for 10 seconds");
        }
        public override void SetForceDefaults()
        {
            Item.defense += 50;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().ForceTyranny = CSToggle.ForceTyranny;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchGodslayerAncient>());
            recipe.AddIngredient(ModContent.ItemType<EnchGodslayer>());
            recipe.AddIngredient(ModContent.ItemType<EnchAuric>());
            recipe.AddIngredient(ModContent.ItemType<EnchDemonshade>());
            recipe.AddTile(ModContent.TileType<Tiles.DemonshadeWorkbenchTile>());
            recipe.Register();
        }
    }
}