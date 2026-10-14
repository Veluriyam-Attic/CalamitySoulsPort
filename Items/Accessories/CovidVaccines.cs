using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace CalamitySouls.Items.Accessories
{
    public class CovidVaccines : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand) => new bool?(false);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.CS().cCVaccines = true;
        }
    }
}