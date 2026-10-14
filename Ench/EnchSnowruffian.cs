using Terraria;
using CalamityMod.Items.Armor.SnowRuffian;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Items.Weapons;

namespace CalamitySouls.Ench
{
   public class EnchSnowruffian:BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Snow Ruffian";
            ChineseName = "雪境暴徒";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "63DDE0";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「他们从我们手中夺取了一切，现在是时候夺回来了。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "免疫摔伤，增加20%盗贼弹速");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "免疫冰肺，提供寒冷抗性，受击时冰冻周围敌人和你自己");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "被冰冻的玩家获得12防御，被冰冻的敌人在非boss战期间无法移动");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchSnowruffian>(),
                    ModContent.ItemType<EnchDaedalus>()
                }, "你的攻击会产生冰晶碎片，不再召唤代达罗斯水晶");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「They took everything from us, now let's get these back.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Negate fall damage, +20% rogue velocity");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Immunity to Frozen Lung, provide cold resistance, being attacked freezes nearby enermies and yourself for a short time");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "The player frozen grant 12 defense, and enermies frozen cannot move when currently no boss is alive");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchSnowruffian>(),
                ModContent.ItemType<EnchDaedalus>()
            }, "Your attacks would spawn ice crystal shards but no longer summons true daedalus crystal");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchSnowRuffian = CSToggle.EnchSnowRuffian;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianGreaves>());
            recipe.AddIngredient(ModContent.ItemType<FrostPaintgun>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.IcicleStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.FrostBolt>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.FrostBlossomStaff>());
            recipe.AddIngredient(ModContent.ItemType<IcicleDagger>(), 1000);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
