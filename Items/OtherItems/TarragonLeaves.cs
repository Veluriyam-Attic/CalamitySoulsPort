using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Projectiles.Typeless;
using static Terraria.ModLoader.ModContent;
using static CalamityMod.CalamityUtils;

namespace CalamitySouls.Items.OtherItems
{
    public abstract class TarragonLeaves : ModItem
    {
        // 1.4 移植：这 8 种龙蒿叶原版（1.3）也没有中文名，这里补上
        public override LocalizedText DisplayName => CSCompat.Zh("TarragonLeaves." + GetType().Name, GetType().Name switch
        {
            "RegenLeaf" => "再生之叶",
            "ShieldLeaf" => "护盾之叶",
            "EnergyLeaf" => "能量之叶",
            "ExplosionLeaf" => "爆炸之叶",
            "BurstLeaf" => "迸发之叶",
            "HealLeaf" => "治疗之叶",
            "DamageLeaf" => "伤害之叶",
            "ImmuLeaf" => "免疫之叶",
            _ => GetType().Name
        });

        public static int GetRandomTarragonLeaves()
        {
            switch (Main.rand.Next(8))
            {
                case 0:return ItemType<BurstLeaf>();
                case 1:return ItemType<DamageLeaf>();
                case 2:return ItemType<EnergyLeaf>();
                case 3:return ItemType<ExplosionLeaf>();
                case 4:return ItemType<HealLeaf>();
                case 5:return ItemType<ImmuLeaf>();
                case 6:return ItemType<RegenLeaf>();
                case 7:return ItemType<ShieldLeaf>();
            }
            return 0;
        }
        public static Projectile SpawnProj(int type, Vector2 position, Vector2 velocity, int damage)
        {
            Item item2 = Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem];
            Projectile proj = Projectile.NewProjectileDirect(null, position, velocity,
               type, damage, 0f, Main.LocalPlayer.whoAmI);
            if (!item2.IsAir) proj.GetItemDamageType(item2);
            return proj;
        }
        public override string Texture => "CalamitySouls/Items/OtherItems/TarragonLeaves";
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 22;
            Item.rare = ItemRarityID.Expert;
        }
        public virtual int BuffType => 0;
        public virtual int BuffTime => 10 * 60;
        public override bool OnPickup(Player player)
        {
            player.AddBuff(BuffType, BuffTime);
            return false;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            gravity = 0f;
            maxFallSpeed = 1000f;
            float Dist = 16 * 100f;
            int me = -1;
            if (Main.netMode == NetmodeID.SinglePlayer) me = Main.myPlayer;
            else for (int i = 0; i < Main.player.Length; i++)
                {
                    Player player = Main.player[i];
                    if (player.active && !player.dead && player.Distance(Item.Center) < Dist)
                    {
                        Dist = player.Distance(Item.Center);
                        me = i;
                    }
                }
            if (me != -1)
            {
                Item.velocity = Vector2.Lerp(Item.velocity, Item.DirectionTo(Main.player[me].Center) * 50f, 0.5f);
                Item.beingGrabbed = true;
                Item.noGrabDelay = 0;
            }
        }
    }
    public class RegenLeaf : TarragonLeaves
    {
        public override int BuffType => BuffType<TarraLifeRegen>();
    }
    public class ShieldLeaf : TarragonLeaves
    {
        public override int BuffType => BuffType<Buffs.TarragonCloak>();
    }
    public class EnergyLeaf : TarragonLeaves
    {
        public override bool OnPickup(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                SpawnProj(ProjectileType<TarraEnergy>(), player.Center, player.DirectionTo(Main.MouseWorld) * 8.5f, 20000);
            }
            return false;
        }
    }
    public class ExplosionLeaf : TarragonLeaves
    {
        public override bool OnPickup(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 15; i++)
                {
                    Projectile explosion = SpawnProj(ProjectileID.CrystalLeafShot,
                        player.Center + player.DirectionTo(Main.MouseWorld) * 64f*i, Vector2.Zero, 8000);
                    explosion.timeLeft = 1;
                }
            }
            return false;
        }
    }
    public class BurstLeaf : TarragonLeaves
    {
        public override bool OnPickup(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 10; i++)
                {
                    SpawnProj(ProjectileID.Leaf, player.Center, player.DirectionTo(Main.MouseWorld).RotatedByRandom(0.1f) * 12f, 500);
                }
            }
            return false;
        }
    }
    public class HealLeaf : TarragonLeaves
    {
        public override bool OnPickup(Player player)
        {
            player.lifeSteal -= 228;
            player.HealLife(38);
            return false;
        }
    }
    public class DamageLeaf : TarragonLeaves
    {
        public override bool OnPickup(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC npc2 = Main.npc[k];
                    if (npc2.active && !npc2.friendly && !npc2.dontTakeDamage && Vector2.Distance(player.Center, npc2.Center) <= 300f)
                    {
                        Projectile.NewProjectileDirect(null, npc2.Center, Vector2.Zero, ProjectileType<DirectStrike>(),
                            3000, 0f, player.whoAmI, k, 0f);
                    }
                }
            }
            for(int i = 0; i < 160; i++)
            {
                Dust dust = Dust.NewDustPerfect(player.Center + MathHelper.ToRadians(i * 360 / 160).ToRotationVector2() * 300f, 107);
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
                dust.scale = 1.2f;
            }
            return false;
        }
    }
    public class ImmuLeaf : TarragonLeaves
    {
        public override int BuffType => BuffType<Buffs.TarragonImmunity>();
        public override int BuffTime => 90;
    }
}