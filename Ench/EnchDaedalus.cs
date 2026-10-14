using Terraria;
using CalamityMod.Items.Armor.Daedalus;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamitySouls;
using System.Collections.Generic;

namespace CalamitySouls.Ench
{
    public class EnchDaedalus : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Daedalus";
            ChineseName = "代达罗斯";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "DA69E9";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「从太阳处逃离者建立了一个冻结的世界」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "增加20生命上限，受伤时闪避并生成一圈追踪水晶，冷却45秒");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "免疫伊卡洛斯之愚蠢，一个真代达罗斯水晶协助你，增加10%移动速度和加速度");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchDaedalus>(),
                    ModContent.ItemType<EnchSnowruffian>()
                }, "你的攻击会产生冰晶碎片，不再召唤代达罗斯水晶");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「The one who escaped from the sun build up a frozen world.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "+20 max health, evade damage and burst out chasing crystal shard with a 45s cooldown");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Immunity to Icarus' Folly, a true daedalus crystal assist you, +10% movement speed and acceleration");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchDaedalus>(),
                ModContent.ItemType<EnchSnowruffian>()
            }, "Your attacks would spawn ice crystal shards but no longer summons true daedalus crystal");
        }
        public override void SetEnchDefaults()
        {
            Item.defense = 4;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchDaedalus = CSToggle.EnchDaedalus;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Daedalus");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.OrnateShield>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.StarnightLance>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Shimmerspark>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.DarkechoGreatbow>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.CrystalPiercer>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}