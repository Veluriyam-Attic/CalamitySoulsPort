using Terraria;
using CalamityMod.Items.Armor.PlagueReaper;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamitySouls.Ench
{
    public class EnchPlagueReaper : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Plague Reaper";
            ChineseName = "瘟疫收割者";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "3A514B";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「无知而浅薄的生命只能等待收割。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "被瘟疫感染的生物多受到15%伤害，你获得无限飞行时间但是被致盲");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "增强肾上腺素的效果，且满肾上腺素后受击不会清空肾上腺素");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchPlagueReaper>(),
                    ModContent.ItemType<EnchPlaguebringer>(),
                }, "你不再被致盲，且友方瘟疫蜜蜂造成更多伤害");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Those ignorance only wait for their reaper.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Plagued enemies receive 10% more damage, you gain infinite flight time but get blinded");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Boost the effect of adrenaline and your adrenaline won't be cleared anymore once get fully charged");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchPlagueReaper>(),
                ModContent.ItemType<EnchPlaguebringer>(),
            }, "You are no longer blinded and your plague bees deal more damage");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchPlagueReaper = CSToggle.EnchPlagueReaper;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperVest>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperStriders>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.SoulHarvester>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.Plaguenade>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.EpidemicShredder>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
