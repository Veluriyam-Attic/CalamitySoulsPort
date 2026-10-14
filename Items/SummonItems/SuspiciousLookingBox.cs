using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using CalamityMod.Items.TreasureBags;
using CalamitySouls.NPCs.MagnificentFishboxFolder;

namespace CalamitySouls.Items.SummonItems
{
    public class SuspiciousLookingBox : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 42;
            Item.useAnimation = Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item120;
            Item.maxStack = 64;
            Item.consumable = true;
        }
        public override bool CanUseItem(Player player) => !NPC.AnyNPCs(ModContent.NPCType<MagnificentFishbox>()) && !CSUtils.AnyBossAlive;
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                NPC.NewNPC(null, (int)player.Center.X, (int)player.Center.Y - 160, ModContent.NPCType<MagnificentFishbox>());
            }
            return true;
        }
        public override void AddRecipes()
        {
            // 1.4：Recipe.SetResult 已移除，产物直接在 Recipe.Create 里指定
            Recipe recipe = Recipe.Create(ModContent.ItemType<SuspiciousLookingBoxInfinite>());
            recipe.AddIngredient(this);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
    public class SuspiciousLookingBoxInfinite : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 42;
            Item.useAnimation = Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item120;
            Item.rare = ItemRarityID.Red;
        }
        public override bool CanUseItem(Player player) => !CSUtils.AnyBossAlive;
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                NPC.NewNPC(null, (int)player.Center.X, (int)player.Center.Y - 160, ModContent.NPCType<MagnificentFishbox>());
            }
            return true;
        }
    }
}