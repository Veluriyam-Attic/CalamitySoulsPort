using Terraria;
using CalamityMod.Items.Tools;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.Ench;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Items.Accessories;

namespace CalamitySouls.Spell
{
    public class SpellStarlight : BasicSpell
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Starlight";
            ChineseName = "星光";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string color1 = "9BAB75";
            string color2 = "7B6382";
            string color3 = "8B8784";
            int Material1 = ItemType<EnchWulfrum>();
            int Material2 = ItemType<EnchTitan>();
            if (CSUtils.GameCultureChinese)
            {
                QuickModiDescription(tooltips, "闪耀");
                QuickModiLine(tooltips, "半血以下加15防御", Material1, color1);
                QuickModiLine(tooltips, "召唤一个阻挡不穿墙弹幕的盾", Material1, color1);
                QuickModiLine(tooltips, "增加300%击退，攻击造成幻星感染", Material2, color2);
                QuickModiLine(tooltips, "攻击时在鼠标位置产生幻星爆破", Material2, color2);
                QuickModiLine(tooltips, "使用物品时在鼠标位置召唤幻星增幅器", Item.type, color3);
                return;
            }
            QuickModiDescription(tooltips, "Shining");
            QuickModiLine(tooltips, "+15 defense when below 50% life", Material1, color1);
            QuickModiLine(tooltips, "Spawn a shield that blocks projectiles that cannot pass through walls", Material1, color1);
            QuickModiLine(tooltips, "+300% knockback, your attack inflicts astral infection", Material2, color2);
            QuickModiLine(tooltips, "Create astral explosion at mouse position when attacking", Material2, color2);
            QuickModiLine(tooltips, "Create an astral booster at mouse position when attacking", Item.type, color3);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().SpellStarlight = CSToggle.SpellStarlight;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnchWulfrum>());
            recipe.AddIngredient(ModContent.ItemType<EnchTitan>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Tools.MomentumCapacitor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.AstralScythe>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.StellarCannon>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.AstralachneaStaff>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.HivePod>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.StellarKnife>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}