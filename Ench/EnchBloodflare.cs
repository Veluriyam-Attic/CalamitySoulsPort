using Terraria;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Weapons.Typeless;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace CalamitySouls.Ench
{
    public class EnchBloodflare : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Bloodflare";
            ChineseName = "血炎";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "CC2A3C";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「神弃的烈炎在黑暗中滋生。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "半血的敌人被攻击时几率掉落红心，血月时掉落更多血珠，增加100生命上限");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "生成3个血炎子机，子机会为你吸血和消弹，消弹有10秒的冷却");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchBloodflare>(),
                    ModContent.ItemType<EnchBrimflame>(),
                }, "释放硫火暴乱时，会进行一次强大的血炎爆破");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Forsaken Flare infestates in the darkness.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Enemies below 50% life may drop hearts when struck and drop more Blood Orbs during a Blood Moon, +100 Max Hp");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Spawn 3 bloodflare option, options do lifesteal and can block projectiles for you which have a 10s Cooldown");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchBloodflare>(),
                ModContent.ItemType<EnchBrimflame>(),
            }, "A great blood eruption is released when a brimflame fenzy is triggered");
        }
        public override void SetEnchDefaults()
        {
            Item.defense = 10;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchBlood = CSToggle.EnchBlood;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("CS:Bloodflare");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareCuisses>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.TheMutilator>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Lacerator>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.ArterialAssault>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Typeless.ClaretCannon>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.BloodBoiler>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.Viscera>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.SanguineFlare>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.DragonbloodDisgorger>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.BloodsoakedCrasher>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
