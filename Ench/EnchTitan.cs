using Terraria;
using CalamityMod.Items.Armor.TitanHeart;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Items.Weapons;

namespace CalamitySouls.Ench
{
    public class EnchTitan : BasicEnch
    {
        public override void SetName(out string Name, out string ChineseName, out int lineAmount)
        {
            Name = "Titan Heart";
            ChineseName = "泰坦之心";
            lineAmount = 3;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int nextLine = 0;
            string color = "7B6382";
            if (CSUtils.GameCultureChinese)
            {
                QuickModiLine(tooltips, ref nextLine, "「抛瓦！...但是泰拉人，代价是什么呢？」");
                QuickModiLine(tooltips, ref nextLine, "" +
                    "增加300%击退，攻击造成幻星感染");
                QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                    "攻击时在鼠标位置产生幻星爆破");
                WriteASpellEffect(tooltips, nextLine, new int[]
                {
                    ModContent.ItemType<EnchTitan>(),
                    ModContent.ItemType<EnchWulfrum>(),
                }, "使用物品时在鼠标位置召唤幻星增幅器，增加附近弹幕的伤害");
                return;
            }
            QuickModiLine(tooltips, ref nextLine, "「POWER!...And what, Terrarian, must we give in return?」");
            QuickModiLine(tooltips, ref nextLine, "" +
                "+300% knockback, your attack inflicts astral infection");
            QuickModiLine(tooltips, ref nextLine, CSUtils.GetModItemText(Item.type, color) +
                "Create astral explosions at mouse position when attacking");
            WriteASpellEffect(tooltips, nextLine, new int[]
            {
                    ModContent.ItemType<EnchTitan>(),
                    ModContent.ItemType<EnchWulfrum>(),
            }, "Create an astral booster at mouse position when attacking, boosting nearby projectiles' damage");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().EnchTitan = CSToggle.EnchTitan;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartMask>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartMantle>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartBoots>());
            recipe.AddIngredient(ModContent.ItemType<MonolithStick>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.PolarisParrotfish>());
            recipe.AddIngredient(ModContent.ItemType<MonolithRay>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Typeless.LunicEye>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.GacruxianMollusk>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.TitanHeart>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}