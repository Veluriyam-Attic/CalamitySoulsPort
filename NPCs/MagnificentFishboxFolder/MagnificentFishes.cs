using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using CalamitySouls.Items.GrabBags;
using CalamityMod.Items.Fishing;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;

namespace CalamitySouls.NPCs.MagnificentFishboxFolder
{
    public class MagnificentFishes : ModNPC
    {
        public override string Texture => CSUtils.BlankTexture;
        private NPC OwnerBox
        {
            get => Main.npc[(int)NPC.ai[0]];
            set => NPC.ai[0] = value.whoAmI;
        }
        private int FishType
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private int AiCounter
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float AiCounter2
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailingMode[NPC.type] = 3;
            NPCID.Sets.TrailCacheLength[NPC.type] = 9;
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = AIType = -1;
            NPC.width = 20;
            NPC.height = 20;
            NPC.friendly = false;
            NPC.chaseable = true;
            NPC.damage = 5;
            NPC.defense = 100;
            NPC.knockBackResist = 0F;
            NPC.lifeMax = 1000;
            for (int i = 0; i < NPC.buffImmune.Length; i++) NPC.buffImmune[i] = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath19;
        }
        public override void AI()
        {
            if (!OwnerBox.active || OwnerBox.type != NPCType<MagnificentFishbox>() || ((MagnificentFishbox)OwnerBox.ModNPC).DeathCounter >= 0
                || NPC.Distance(OwnerBox.Center) > ((MagnificentFishbox)OwnerBox.ModNPC).RestrictCircleRange)
            {
                NPC.active = false;
                SoundEngine.PlaySound(NPC.DeathSound, NPC.Center);
                Item.NewItem(null, NPC.Center, ItemType);
                return;
            }
            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver4;
            Player player = Main.player[OwnerBox.target];
            if (FishType == ItemType<StuffedFish>())
            {
                AiCounter++;
                if (AiCounter < 120)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC otherFishes = Main.npc[i];
                        if (otherFishes.active && otherFishes.type == NPC.type && otherFishes.whoAmI != NPC.whoAmI && otherFishes.ai[1] == FishType)
                        {
                            if (otherFishes.Distance(NPC.Center) > 100f) NPC.velocity += NPC.DirectionTo(otherFishes.Center);
                            if (otherFishes.Distance(NPC.Center) < 40f) NPC.velocity += NPC.DirectionFrom(otherFishes.Center) * 12f;
                        }
                    }
                    NPC.velocity += NPC.DirectionTo(player.Center) * 3f;
                    NPC.velocity = Vector2.Normalize(NPC.velocity) * 6f;
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 61);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
            }
            else if (FishType == ItemType<GlimmeringGemfish>())
            {
                NPC.velocity.Y += 0.01f;
            }
            else if (FishType == 0)
            {
                if (AiCounter == 0)
                {
                    AiCounter = 1;
                    NPC.velocity = CSUtils.RandomRotate * 0.1f;
                }
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 90);
                dust.noGravity = true;
                dust.velocity = NPC.velocity / 2f;
                NPC.velocity = Vector2.Normalize(NPC.velocity) * (NPC.velocity.Length() + 0.2f);
            }
            else if (FishType == 0)
            {
                AiCounter++;
                if (AiCounter < 120) NPC.velocity = NPC.velocity.RotatedBy(0.01f);
                else if (AiCounter == 120)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Dust dust = Dust.NewDustPerfect(NPC.Center, 242);
                        dust.noGravity = true;
                        dust.scale *= Main.rand.Next(20, 31) / 10f;
                        dust.velocity = CSUtils.RandomRotate * Main.rand.Next(20, 31) / 10f;
                    }
                }
                else
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 242);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
            }
            else if (FishType == 0)
            {
                AiCounter++;
                if (AiCounter < 120) NPC.velocity = NPC.velocity.RotatedBy(-0.01f);
                else if (AiCounter == 120)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Dust dust = Dust.NewDustPerfect(NPC.Center, 62);
                        dust.noGravity = true;
                        dust.scale *= Main.rand.Next(20, 31) / 10f;
                        dust.velocity = CSUtils.RandomRotate * Main.rand.Next(20, 31) / 10f;
                    }
                }
                else
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 62);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
            }
            else if (FishType == ItemType<FishofFlight>())
            {
                AiCounter++;
                if (AiCounter > 60)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 88);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
                if (AiCounter == 60)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Dust dust = Dust.NewDustPerfect(NPC.Center, 62);
                        dust.noGravity = true;
                        dust.scale *= Main.rand.Next(20, 31) / 10f;
                        dust.velocity = CSUtils.RandomRotate * Main.rand.Next(20, 31) / 10f;
                    }
                    NPC.velocity = NPC.DirectionTo(player.Center) * NPC.velocity.Length();
                }
            }
            else if (FishType == ItemType<FishofEleum>())
            {
                AiCounter++;
                if (AiCounter % 60 == 0)
                {
                    NPC.velocity = NPC.velocity.RotatedBy(MathHelper.Pi / 6f * (Main.rand.Next(0, 2) * 2 - 1));
                }
            }
            else if (FishType == ItemType<SunbeamFish>())
            {
                AiCounter++;
                if (AiCounter > 120)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.AmberBolt);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
                if (AiCounter == 120) NPC.velocity = -NPC.velocity * 2F;
            }
            else if (FishType == 0)
            {
                AiCounter++;
                if (AiCounter % 30 == 0)
                {
                    NPC.velocity = NPC.velocity.RotatedByRandom(MathHelper.Pi) * 0.98f;
                    for (int i = 0; i < 16; i++)
                    {
                        Dust dust = Dust.NewDustPerfect(NPC.Center, 90);
                        dust.noGravity = true;
                        dust.scale *= Main.rand.Next(20, 31) / 10f;
                        dust.velocity = CSUtils.RandomRotate * Main.rand.Next(20, 31) / 10f;
                    }
                }
                if (NPC.velocity.Length() < 4f) NPC.velocity = Vector2.Normalize(NPC.velocity) * 4f;
            }
            else if (FishType == ItemType<FishofSight>())
            {
                AiCounter++;
                if (AiCounter % 120 > 10)
                {
                    if (AiCounter % 120 < 110)
                    {
                        if (NPC.Distance(player.Center) > 180f) NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionTo(player.Center) * 15f, 0.07f);
                        else NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionFrom(player.Center) * 15f, 0.07f);
                    }
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC otherFishes = Main.npc[i];
                        if (otherFishes.active && otherFishes.type == NPC.type && otherFishes.whoAmI != NPC.whoAmI && otherFishes.ai[1] == FishType)
                        {
                            if (otherFishes.Distance(NPC.Center) < 40f) NPC.velocity += NPC.DirectionFrom(otherFishes.Center) * 12f;
                        }
                    }
                }
                else
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 61);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                    if (AiCounter % 120 == 0)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            Dust dust2 = Dust.NewDustPerfect(NPC.Center, 61);
                            dust2.noGravity = true;
                            dust2.scale *= Main.rand.Next(20, 31) / 10f;
                            dust2.velocity = CSUtils.RandomRotate * Main.rand.Next(20, 31) / 10f;
                        }
                        NPC.velocity = NPC.DirectionTo(player.Center) * 30f;
                    }
                }
            }
            else if (FishType == ItemType<FishofMight>())
            {
                AiCounter++;
                if (AiCounter % 60 == 0) AiCounter2++;
                Vector2 amount = Vector2.UnitY * (360f + AiCounter2 * 40f);
                if (AiCounter % 120 < 60) NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionTo(player.Center + amount) * 24f, 0.04f);
                else NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionTo(player.Center - amount) * 24f, 0.04f);
            }
            else if (FishType == ItemType<FishofFright>())
            {
                AiCounter++;
                if (AiCounter < 300)
                {
                    if (AiCounter % 75 == 0) AiCounter2 = MathHelper.ToRadians(Main.rand.Next(360));
                    NPC.velocity = Vector2.Lerp(NPC.velocity, NPC.DirectionTo(player.Center + AiCounter2.ToRotationVector2() * 600f) * 14f, 0.07f);
                }
                else
                {
                    NPC.velocity = NPC.velocity.Length(12f);
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 158);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
            }
            else if (FishType == ItemType<EctoFish>())
            {
                AiCounter++;
                if (AiCounter % 90 < 40)
                {
                    NPC.velocity = NPC.DirectionTo(player.Center) * 10f;
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 175);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
            }
            else if (FishType == ItemType<PhantoFish>())
            {
                AiCounter++;
                if (AiCounter % 60 < 20)
                {
                    NPC.velocity = NPC.DirectionTo(player.Center) * 14f;
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 175);
                    dust.noGravity = true;
                    dust.velocity = NPC.velocity / 2f;
                }
            }
            else if (FishType == ItemType<UnholyFish>())
            {
                AiCounter++;
                NPC.velocity *= 0.96f;
                if (AiCounter % 45 == 0)
                {
                    NPC.velocity = NPC.DirectionTo(player.Center) * (24f + AiCounter2 * 3f);
                    AiCounter2++;
                }
            }
            else
            {
            }
        }
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, FishType);
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            // 1.3 里这里把伤害归零；1.4 用修饰符表达
            modifiers.FinalDamage *= 0f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.HealLife(hurtInfo.Damage);
            ((MagnificentFishbox)OwnerBox.ModNPC).HitPlayerTimes++;
            NPC.active = false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            drawColor = Color.White;
            Texture2D texture = TextureAssets.Item[FishType].Value;
            if (FishType == ItemType<StuffedFish>())
            {
                if (AiCounter < 120) drawColor = Color.Green;
            }
            else if (FishType == ItemType<GlimmeringGemfish>())
            {
            }
            else if (FishType == 0)
            {
                drawColor = Color.Red;
            }
            else if (FishType == 0)
            {
                if (AiCounter > 120) drawColor = Color.Pink;
            }
            else if (FishType == 0)
            {
                if (AiCounter > 120) drawColor = Color.Purple;
            }
            else if (FishType == ItemType<FishofFlight>())
            {
                if (AiCounter > 60) drawColor = Color.SkyBlue;
            }
            else if (FishType == ItemType<FishofEleum>())
            {
            }
            else if (FishType == ItemType<SunbeamFish>())
            {
                if (AiCounter > 120) drawColor = Color.Yellow;
            }
            else if (FishType == 0)
            {
            }
            else if (FishType == ItemType<FishofSight>())
            {
                if (AiCounter % 120 <= 10) drawColor = Color.Teal;
            }
            else if (FishType == ItemType<FishofMight>())
            {
            }
            else if (FishType == ItemType<FishofFright>())
            {
                if (AiCounter >= 300) drawColor = Color.Orange;
            }
            else if (FishType == ItemType<EctoFish>())
            {
                if (AiCounter % 90 < 40) drawColor = Color.SkyBlue;
            }
            else if (FishType == ItemType<PhantoFish>())
            {
                if (AiCounter % 60 < 20) drawColor = Color.Red;
            }
            else if (FishType == ItemType<UnholyFish>())
            {
                if (AiCounter % 45 < 25) drawColor = Color.Gold;
            }
            Color drawColor2 = drawColor;
            float scale = NPC.scale;
            for (int i = 0; i < 10; i += 2)
            {
                drawColor2 *= 0.7f;
                //drawColor2.A = (byte)(drawColor2.A / 0.7f);
                scale *= 0.9f;
                spriteBatch.Draw(texture, NPC.oldPos[i] + NPC.Hitbox.Size() / 2 - Main.screenPosition, null, drawColor2,
                    NPC.oldRot[i], texture.Size() / 2f, scale, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, null, drawColor,
                NPC.rotation, texture.Size() / 2f, NPC.scale, SpriteEffects.None, 0f);
            return false;
        }
        public static int RandomFish
        {
            get
            {
                List<int> FishList = new List<int>
                {
                    ItemType<StuffedFish>(),
                    ItemType<GlimmeringGemfish>(),
                    0
                };
                if (Main.LocalPlayer.statManaMax < 200) FishList.Add(ItemType<EnchantedStarfish>());
                if (Main.hardMode)
                {
                    FishList.AddRange(new List<int>
                    {
                        0,
                        0,
                        ItemType<FishofFlight>(),
                        ItemType<FishofEleum>(),
                        ItemType<SunbeamFish>(),
                        0
                    });
                }
                if (NPC.downedMechBoss2) FishList.Add(ItemType<FishofSight>());
                if (NPC.downedMechBoss1) FishList.Add(ItemType<FishofMight>());
                if (NPC.downedMechBoss3) FishList.Add(ItemType<FishofFright>());
                if (NPC.downedPlantBoss) FishList.Add(ItemType<EctoFish>());
                if (NPC.downedMoonlord) FishList.Add(ItemType<PhantoFish>());
                if (NPC.downedMoonlord) FishList.Add(ItemType<UnholyFish>());
                return Utils.SelectRandom(Main.rand, FishList.ToArray());
            }
        }
        public int ItemType
        {
            get
            {
                if (FishType == ItemType<StuffedFish>()) return Utils.SelectRandom(Main.rand, new int[]
                {
                    ItemID.Daybloom,
                    ItemID.Shiverthorn,
                    ItemID.Deathweed,
                    ItemID.Moonglow,
                    ItemID.Blinkroot,
                    ItemID.Waterleaf,
                    ItemID.Fireblossom,
                });
                if (FishType == ItemType<GlimmeringGemfish>()) return Utils.SelectRandom(Main.rand, new int[]
                {
                    ItemID.Amethyst,
                    ItemID.Topaz,
                    ItemID.Sapphire,
                    ItemID.Emerald,
                    ItemID.Ruby,
                    ItemID.Diamond,
                    ItemID.Amber
                });
                if (FishType == 0) return Utils.SelectRandom(Main.rand, new int[]
                {
                    ItemType<BloodOrb>(),
                    ItemType<BloodOrb>(),
                    ItemType<BloodOrb>(),
                    0
                });
                if (FishType == 0) return ItemID.SoulofLight;
                if (FishType == 0) return ItemID.SoulofNight;
                if (FishType == ItemType<FishofFlight>()) return ItemID.SoulofFlight;
                if (FishType == ItemType<FishofEleum>()) return ItemType<EssenceofEleum>();
                if (FishType == ItemType<SunbeamFish>()) return 0;
                if (FishType == 0) return 0;
                if (FishType == ItemType<FishofSight>()) return Utils.SelectRandom(Main.rand, new int[]
                {
                    ItemID.SoulofSight,
                    ItemID.SoulofLight,
                    ItemID.SoulofLight
                });
                if (FishType == ItemType<FishofMight>()) return Utils.SelectRandom(Main.rand, new int[]
                {
                    ItemID.SoulofMight,
                    ItemID.SoulofNight,
                    ItemID.SoulofNight
                });
                if (FishType == ItemType<FishofFright>()) return Utils.SelectRandom(Main.rand, new int[]
                {
                    ItemID.SoulofFright,
                    ItemID.SoulofLight,
                    ItemID.SoulofNight
                });
                if (FishType == ItemType<EctoFish>()) return 0;
                if (FishType == ItemType<PhantoFish>()) return 0;
                if (FishType == ItemType<UnholyFish>()) return ItemType<UnholyEssence>();
                return ItemID.Star;
            }
        }
    }
}