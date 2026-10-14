using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellBetrayal : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Betrayal";
            ChineseName = "背叛";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "DFCCFF";
            string color2 = "D3B258";
            string color3 = "D9C7AC";
            int Material1 = ItemType<EnchStatigel>();
            int Material2 = ItemType<EnchTarragon>();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "叛变");
                QuickModiLine(tooltips, "增加10跳跃高度，增加48%跳跃速度，无敌时间延长", Material1, color1);
                QuickModiLine(tooltips, "召唤迷你史莱姆神，在你受伤之时治疗你", Material1, color1);
                QuickModiLine(tooltips, "获得拾心和镇静效果，击杀敌人有几率掉落额外的红心", Material2, color2);
                QuickModiLine(tooltips, "增加25%武器使用速度，击中敌人生成不同的增益叶片", Material2, color2);
                QuickModiLine(tooltips, "大大减少冲刺冷却", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Insurancy");
            QuickModiLine(tooltips, "+10 jump height, +48% jump speed and increased immunity frame", Material1, color1);
            QuickModiLine(tooltips, "Summon a mini slime god to heal you when you're hurt", Material1, color1);
            QuickModiLine(tooltips, "Heartreach and calm potion effect, enemies have a chance to drop extra hearts on death", Material2, color2);
            QuickModiLine(tooltips, "+25% weapon fire rate, hitting enermies spawn different bonus leaves", Material2, color2);
            QuickModiLine(tooltips, "Greatly decreases your dash cooldown", Item.type, color3);
        }
        public override void SetSpellDefaults()
        {
            Item.defense = 2;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellBetrayal = CSToggle.SpellBetrayal;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchStatigel>());
            recipe.AddIngredient(ModContent.ItemType<EnchTarragon>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.EvasionScarf>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.BansheeHook>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.DaemonsFlame>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.FatesReveal>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.EtherealSubjugator>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.GhoulishGouger>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}