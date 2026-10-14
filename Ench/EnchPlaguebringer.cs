using Terraria;
using CalamityMod.Items.Armor.Plaguebringer;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchPlaguebringer : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Plague Bringer";
            ChineseName = "瘟疫使者";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "E54242";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「从内部将他们消灭殆尽吧。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "获得一个瘟疫冲刺，快速移动时生成蜜蜂，你的攻击多造成20伤害");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "免疫瘟疫，攻击造成瘟疫，移除敌人的瘟疫免疫");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchPlaguebringer>(),
                    ModContent.ItemType<EnchPlagueReaper>(),
                }, "你不再被瘟疫收割者魔石致盲，且友方瘟疫蜜蜂造成更多伤害");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Destroy them from inside.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Grants a plague dash and you spawn bees while sprinting or dashing, your attack deal 20 extra damage");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Immunity to Plague, your attack force plague on enermies");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchPlaguebringer>(),
                ModContent.ItemType<EnchPlagueReaper>(),
            }, "You are no longer blinded by plague reaper enchant and your plague bees deal more damage");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchPlagueBringer = CSToggle.EnchPlagueBringer;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerVisor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerCarapace>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerPistons>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Malevolence>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.PestilentDefiler>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.TheHive>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.PlagueStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.FuelCellBundle>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.InfectedRemote>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.TheSyringe>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}