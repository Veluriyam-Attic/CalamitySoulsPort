using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellDespair : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Despair";
            ChineseName = "绝望";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "E34F4F";
            string color2 = "CC2A3C";
            string color3 = "D83D4E";
            int Material1 = ItemType<EnchBrimflame>();
            int Material2 = ItemType<EnchBloodflare>();
            int Material3 = ItemType<BloodflareCore>();
            string hotkey = CSUtils.TooltipHotkeyString(CalamitySouls.BrimflameKey);
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "虚无");
                QuickModiLine(tooltips, "免疫大多数火系列减益，按" + hotkey + "开启硫火暴乱效果", Material1, color1);
                QuickModiLine(tooltips, "释放硫火暴乱的同时释放随机消除弹幕的硫火激光", Material1, color1);
                QuickModiLine(tooltips, "半血的敌人被攻击时几率掉落红心，血月时掉落更多血珠，+100生命上限", Material2, color2);
                QuickModiLine(tooltips, "生成3个血炎子机，子机会为你吸血和消弹", Material2, color2);
                QuickModiLine(tooltips, "血炎核心的效果", Material3, color3);
                QuickModiLine(tooltips, "释放硫火暴乱时，会进行一次强大的血炎爆破", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Nihilism");
            QuickModiLine(tooltips, "Immnue to most fire-relative debuffs, press " + hotkey + " to trigger brimflame frenzy effect", Material1, color1);
            QuickModiLine(tooltips, "Releasing a frenzy causes you to release brimstone rays which randomly clear projectiles", Material1, color1);
            QuickModiLine(tooltips, "Enemies below 50% life may drop hearts when struck and drop more Blood Orbs during a Blood Moon, +100 Max Hp", Material2, color2);
            QuickModiLine(tooltips, "Spawn 3 bloodflare option, options do lifesteal and can block projectiles for you", Material2, color2);
            QuickModiLine(tooltips, "Bloodflare Core Effect", Material3, color3);
            QuickModiLine(tooltips, "A great blood eruption is released when a brimflame fenzy is triggered", Item.type, color3);
        }
        public override void SetSpellDefaults()
        {
            Item.defense = 10;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellDespair = CSToggle.SpellDespair;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchBrimflame>());
            recipe.AddIngredient(ModContent.ItemType<EnchBloodflare>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.BloodflareCore>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ApoctosisArray>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.AuricBar>(), 4);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.DormantBrimseeker>());
            recipe.AddTile(ModContent.TileType<CalamityMod.Tiles.Furniture.CraftingStations.DraedonsForge>());
            recipe.Register();
        }
    }
}