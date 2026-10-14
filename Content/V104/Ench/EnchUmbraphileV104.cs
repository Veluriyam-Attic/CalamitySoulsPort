// ============================================================================
// 「CalamitySouls v1.0.4.0（新版）」移植 —— 由 1.3 源码自动转换 + 人工校正（tML 1.4 / 灾厄 2.x）
// 对应旧版类：CalamitySouls.Ench.EnchUmbraphile（旧版那一套保持不动）
// 生成脚本：D:\ai\port\cs_analysis\gen-v104-ench2.js
// ============================================================================
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104;

namespace CalamitySouls.Content.V104.Ench
{
    public class EnchUmbraphileV104 : V104EnchBase
    {
        public override void SetName(out string Name, out string ChineseName)
        {
            Name = "Umbraphile";
            ChineseName = "日影";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (V104Utils.UsingChi)
            {
                QuickModiLine(tooltips, "「黑蚀之日正在等待。」");
                QuickModiLine(tooltips, "半影药水始终有最大化效果", ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileRegalia>());
                QuickModiLine(tooltips, "不使用物品10秒后获得遁于暗影增益，提升你的暴击（包含召唤）", Item.type);
                QuickModiLine(tooltips, "拥有该增益时玩家可以闪避一次攻击", Item.type);
                return;
            }
            QuickModiLine(tooltips, "「The dark sun awaits.」");
            QuickModiLine(tooltips, "Penumbra potions always build at max effectiveness", ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileRegalia>());
            QuickModiLine(tooltips, "After 10s of not using items you grant Shadecovered buff, increasing your damage (summon included)", Item.type);
            QuickModiLine(tooltips, "Having the buff grants you a disposable dodge", Item.type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.V104().EnchUmbraphile = CSToggleV104.EnchUmbraphile;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileHood>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileRegalia>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileBoots>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Weapons.PeerlessWindGod>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Weapons.ImpurityWithinOnesBody>());
            recipe.AddIngredient(ModContent.ItemType<global::CalamitySouls.Items.Weapons.ReleaseoftheId>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
