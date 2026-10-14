using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellHeavenshade : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Heavenshade";
            ChineseName = "天影";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "ED7112";
            string color2 = "9CFFEE";
            string color3 = "CDB888";
            int Material1 = ItemType<EnchUmbraphile>();
            int Material2 = ItemType<EnchEmpyrean>();
            int Material3 = ItemType<VampiricTalisman>();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "毁灭");
                QuickModiLine(tooltips, "日影药水始终有最大化效果", Material1, color1);
                QuickModiLine(tooltips, "+15%武器使用速度，不使用物品一段时间后获得不可检测增益", Material1, color1);
                QuickModiLine(tooltips, "常驻宇宙之怒和宇宙狂怒增益，增加40%移速和加速度", Material2, color2);
                QuickModiLine(tooltips, "吸血鬼护符的吸血效果", Material3, color3);
                QuickModiLine(tooltips, "你的攻击会生成天堂之影碎片", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Destruction");
            QuickModiLine(tooltips, "Penumbra potions always build stealth at max effectiveness", Material1, color1);
            QuickModiLine(tooltips, "+15% weapon fire rate, after a period of not using items you grant Undetected buff", Material1, color1);
            QuickModiLine(tooltips, "You are imbued with cosmic wrath and rage, +40% acceleration and speed", Material2, color2);
            QuickModiLine(tooltips, "Lifesteal effect of Vampiric Talisman", Material3, color3);
            QuickModiLine(tooltips, "Your attack would create heavenshade shards", Item.type, color3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellHeavenshade = CSToggle.SpellHeavenshade;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchUmbraphile>());
            recipe.AddIngredient(ModContent.ItemType<EnchEmpyrean>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.VampiricTalisman>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.HolyCollider>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.BlissfulBombardier>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.PurgeGuzzler>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.DazzlingStabberStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.MoltenAmputator>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}