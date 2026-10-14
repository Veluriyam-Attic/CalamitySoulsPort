using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellEleum : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Eleum";
            ChineseName = "冰川";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "63DDE0";
            string color2 = "DA69E9";
            string color3 = "A7ABE5";
            int Material1 = ItemType<EnchSnowruffian>();
            int Material2 = ItemType<EnchDaedalus>();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "绝对零度");
                QuickModiLine(tooltips, "免疫摔伤和冰肺，增加20%盗贼弹速", Material1, color1);
                QuickModiLine(tooltips, "提供死亡模式寒冷抗性，受击时冰冻周围敌人和你自己", Material1, color1);
                QuickModiLine(tooltips, "增加20生命上限，受伤时闪避并生成一圈追踪水晶，冷却45秒", Material2, color2);
                QuickModiLine(tooltips, "免疫伊卡洛斯之愚蠢，增加10%移动速度和加速度", Material2, color2);
                QuickModiLine(tooltips, "你的攻击会产生冰晶碎片", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Absolute zero");
            QuickModiLine(tooltips, "Grant fall damage and frozen lung immunity, +20% rogue velocity", Material1, color1);
            QuickModiLine(tooltips, "Provide death mode cold resistance, being attacked freezes nearby enermies and yourself", Material1, color1);
            QuickModiLine(tooltips, "+20 max health, evade damage and burst out chasing crystal shard with a 45s cooldown", Material2, color2);
            QuickModiLine(tooltips, "Immunity to Icarus' Folly, +10% movement speed and acceleration", Material2, color2);
            QuickModiLine(tooltips, "Your attacks would spawn ice crystal shards", Item.type, color3);
        }
        public override void SetSpellDefaults()
        {
            Item.defense = 4;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellEleum = CSToggle.SpellEleum;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchSnowruffian>());
            recipe.AddIngredient(ModContent.ItemType<EnchDaedalus>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.AbsoluteZero>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.EternalBlizzard>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.WintersFury>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CryogenicStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.FrostcrushValari>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}