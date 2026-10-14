using Terraria;
using CalamityMod.Items.Armor.Umbraphile;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Items.Weapons;

namespace CalamitySouls.Ench
{
    public class EnchUmbraphile : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Umbraphile";
            ChineseName = "日影";
            lineAmount = 4;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "ED7112";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「黑蚀之日正在等待。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "攻击有几率产生爆炸，日影药水始终有最大化效果");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "+15%武器使用速度，不使用物品一段时间后获得不可检测增益，在没有boss存活时使所有生物变得友好");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "这个增益在你击中敌人时失效，但会大大增加那次攻击的伤害");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchUmbraphile>(),
                    ModContent.ItemType<EnchEmpyrean>(),
                }, "你的攻击会生成天堂之影碎片，取代爆炸");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「The dark sun awaits.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Attacks have a chance to create explosions on hit, penumbra potions always build stealth at max effectiveness");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "+15% weapon fire rate, after a period of not using items you grant Undetected buff, which makes all creatures friendly while no boss is alive");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "The buff breaks when you hit any creature, but greatly boosts the damage that hit deals");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                    ModContent.ItemType<EnchUmbraphile>(),
                    ModContent.ItemType<EnchEmpyrean>(),
            }, "Your attack would create heavenshade shards instead of explosions");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchUmbraphile = CSToggle.EnchUmbraphile;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileHood>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileRegalia>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileBoots>());
            recipe.AddIngredient(ModContent.ItemType<PeerlessWindGod>());
            recipe.AddIngredient(ModContent.ItemType<ImpurityWithinOnesBody>());
            recipe.AddIngredient(ModContent.ItemType<ReleaseoftheId>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.DefectiveSphere>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.FantasyTalisman>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}