using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using System.IO;

namespace CalamitySouls.Ench
{
   public  class EnchSulphurous:BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Sulphurous";
            ChineseName = "硫磺海";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "A89254";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「侵蚀使得存在更有力。」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "无视水阻力，免疫酸海剧毒");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "你和你的鼠标周围环绕着超酷的酸海剧毒区域");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchSulphurous>(),
                    ModContent.ItemType<EnchFathom>()
                }, "你的攻击直接造成多种debuff而不再生成酸海剧毒区域");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「Corrosion leads to better existance.」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "Ignore water and grant immunity to sulphurous poisoning");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Cool sulphurous areas are around you and your mouse");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                ModContent.ItemType<EnchSulphurous>(),
                ModContent.ItemType<EnchFathom>()
            }, "Your attacks inflicts tons of debuffs and no longer need the sulphurous aura");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchSulphurous = CSToggle.EnchSulphurous;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Sulphurous.SulphurousLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Basher>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Toxibow>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.AcidGun>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ParasiticSceptor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CausticCroakerStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.RustyBeaconPrototype>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.ContaminatedBile>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Pets.RadiatingCrystal>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}