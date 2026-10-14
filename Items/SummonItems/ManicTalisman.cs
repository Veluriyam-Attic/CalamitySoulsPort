using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CalamitySouls.Items.SummonItems
{
    public class ManicTalisman : ModItem
    {
        public override void SetStaticDefaults()
        {
            CSCompat.SeedTooltip(this, "激活脑瘫模式，让NPC的速度和ai速度乘150%，减少30%NPC受到伤害，你多受到50%伤害\n你的肾上腺素伤害增加50%，你的防御阻挡0.5伤害而非0.75\n击败任意boss前且没有boss存在时，NPC的AI速度和免伤增幅被取消\n只能在复仇模式下使用");
        }

        public override LocalizedText DisplayName => CSCompat.Zh("ManicTalisman.DisplayName", "欠抽护符");
        public override LocalizedText Tooltip => CSCompat.Zh("ManicTalisman.Tooltip",
            "激活脑瘫模式，让NPC的速度和ai速度乘150%，减少30%NPC受到伤害，你多受到50%伤害\n" +
            "你的肾上腺素伤害增加50%，你的防御阻挡0.5伤害而非0.75\n" +
            "击败任意boss前且没有boss存在时，NPC的AI速度和免伤增幅被取消\n" +
            "只能在复仇模式下使用");
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.useAnimation = Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item120;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    line.OverrideColor = Color.SkyBlue;
                }
            }
        }
        public override bool CanUseItem(Player player) => CalamityMod.World.CalamityWorld.revenge;
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                string text = CSUtils.GameCultureChinese ? CSUtils.ManicModeNameChi + "关闭" : CSUtils.ManicModeName + " deactivated.";
                if (!CSWorld.HyperMode) text = CSUtils.GameCultureChinese ? CSUtils.ManicModeNameChi + "开启" : CSUtils.ManicModeName + " activated.";
                if (!CSUtils.AnyBossAlive) CSWorld.HyperMode = !CSWorld.HyperMode;
                else text = CSUtils.GameCultureChinese ? "Boss战中不能改变模式" : "You cannot switch the mode during a boss fight";
                Main.NewText(text, Color.SkyBlue);
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}