using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod.World;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Buffs.Potions;
using CalamityMod.Items.TreasureBags;

namespace CalamitySouls.NPCs.MagnificentFishboxFolder
{
    public class MagnificentFishbox : ModNPC
    {
        public int AttackCounter
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public int HitPlayerTimes
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public int AttackPower
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public int DeathCounter
        {
            get => (int)NPC.ai[3] - 1;
            set => NPC.ai[3] = value + 1;
        }
        public float RestrictCircleRange => 1200f - AttackCounter / (float)ChargeCooldown * 20f;
        public const int ChargeCooldown = 180;
        public static int AttackCooldown
        {
            get
            {
                int value = 90;
                if (Main.expertMode) value = 60;
                if (CalamityWorld.revenge) value = 40;
                if (CalamityWorld.death) value = 30;
                return value;
            }
        }
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = AIType = -1;
            NPC.width = 54;
            NPC.height = 62;
            NPC.friendly = false;
            NPC.dontTakeDamage = false;
            NPC.chaseable = true;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.knockBackResist = 0F;
            NPC.lifeMax = 666666666;
            for (int i = 0; i < NPC.buffImmune.Length; i++) NPC.buffImmune[i] = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }
        public override void AI()
        {
            NPC.life = NPC.lifeMax;
            if (DeathCounter > 0)
            {
                string[] text = new string[]
                {
                    "Ok, That's it.",
                    "Let's stop baby-caring games.",
                    "I doubt if your ability on dodging is enough to keep on.",
                    "Try to dodge on the border next time."
                };
                if (CSUtils.GameCultureChinese) text = new string[]
                 {
                    "就这样了。",
                    "小打小闹到此为止。",
                    "我怀疑你的闪躲能力不能继续走下去。",
                    "下次可以试着在边框处躲避。"
                  };
                Color ComColor = Color.Orange;
                if (NPC.dontTakeDamage)
                {
                    text = new string[]
                    {
                        "Nice~",
                        "Your talent of looking for how to dodge makes me satisfied.",
                        "By the way, there's some true challenges hidden.",
                        "I'm expecting that."
                    };
                    if (CSUtils.GameCultureChinese) text = new string[]
                    {
                        "很好~",
                        "你寻找走位方式的才能让我满意。",
                        "顺带一提，有个真正的挑战隐藏着。",
                        "我很期待看到完成的那一刻。"
                    };
                    ComColor = Color.Green;
                    if (NPC.chaseable)
                    {
                        text = new string[]
                        {
                            "Great!",
                            "You are more powerful than everyone I saw.",
                            "I think you're worthy of more treasures...",
                            "Ouch, I have nothing good left, just take this."
                        };
                        if (CSUtils.GameCultureChinese) text = new string[]
                        {
                            "牛逼！",
                            "你是我迄今为止见过最牛逼的人！",
                            "你值得获得更多奖励...",
                            "哦草，我没什么好东西了，就拿着这个吧。"
                        };
                        ComColor = Color.Pink;
                    }
                }
                if (DeathCounter == 360) Main.NewText(text[0], ComColor);
                if (DeathCounter == 240) Main.NewText(text[1], ComColor);
                if (DeathCounter == 120) Main.NewText(text[2], ComColor);
                if (DeathCounter == 1) Main.NewText(text[3], ComColor);
                DeathCounter--;
                if (DeathCounter == 0)
                {
                    if (NPC.dontTakeDamage) Main.NewText(CSUtils.GameCultureChinese ? "干得漂亮，拿走这些奖励。" : "Good Job. Take your reward.", Color.Green);
                    else Main.NewText(CSUtils.GameCultureChinese ? "你受伤太多了。不去和利维坦学学走位吗？"
                        : "You've received too much hurts. Haven't you learned how to dodge from Leviathan?", Color.Orange);
                    KillMe();
                }
                for (int i = 0; i < Main.item.Length; i++)
                {
                    Item item = Main.item[i];
                    if (item.active && item.Distance(NPC.Center) < RestrictCircleRange + 36f)
                    {
                        item.beingGrabbed = true;
                        item.velocity = item.DirectionTo(NPC.Center) * 8f;
                    }
                }
                return;
            }
            if (AttackPower > 20)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC fish = Main.npc[i];
                    if (fish.active && fish.type == NPCType<MagnificentFishes>())
                    {
                        fish.life = 0;
                        fish.checkDead();
                    }
                }
                NPC.dontTakeDamage = true;
                KillMe();
                return;
            }
            if (HitPlayerTimes >= 5)
            {
                KillMe();
                return;
            }
            NPC.TargetClosest();
            if (Main.player[NPC.target].Distance(NPC.Center) > RestrictCircleRange)
            {
                Main.player[NPC.target].Center = NPC.Center + NPC.DirectionTo(Main.player[NPC.target].Center) * RestrictCircleRange;
                NPC.chaseable = false;
            }
            NPC.velocity = Vector2.Zero;
            if (NPC.oldPosition!=Vector2.Zero) NPC.position = NPC.oldPosition;
            AttackCounter++;
            if (AttackCounter >= 120)
            {
                if (AttackCounter % ChargeCooldown == 0) AttackPower++;
                if (AttackCounter % AttackCooldown == 0)
                {
                    if (CSUtils.GoodNetMode)
                    {
                        for (int i = 0; i < AttackPower; i++)
                        {
                            NPC spawnedfishes = Main.npc[NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y,
                                NPCType<MagnificentFishes>(), 0, NPC.whoAmI, MagnificentFishes.RandomFish, Main.rand.Next(4))];
                            spawnedfishes.velocity = CSUtils.RandomRotate * 7f;
                        }
                    }
                }
            }
            float rand = Main.rand.Next(3600) / 10f;
            for (int i = 0; i < 90; i++)
            {
                Dust dust = Dust.NewDustPerfect(NPC.Center + MathHelper.ToRadians(i * 4 + rand).ToRotationVector2() * RestrictCircleRange, 89);
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
                dust.noLight = true;
                dust.scale = 0.8f;
            }
            for (int i = 0; i < Main.item.Length; i++)
            {
                Item item = Main.item[i];
                if (item.active && item.Distance(NPC.Center) < RestrictCircleRange + 36f)
                {
                    item.beingGrabbed = true;
                    item.velocity = item.DirectionTo(NPC.Center) * 2f;
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
        }
        public void KillMe()
        {
            NPC.life = 0;
            NPC.checkDead();
        }
        public override bool CheckDead()
        {
            if (DeathCounter == -1) DeathCounter = 360;
            NPC.life = 1;
            if (DeathCounter != 0) return false;
            if (NPC.dontTakeDamage && NPC.chaseable && HitPlayerTimes < 1) 
                Main.player[NPC.target].QuickSpawnItem(null, 0, 50);
            return true;
        }
        public override bool CheckActive() => false;
        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;
        public override bool CanHitNPC(NPC target) => false;
        // 1.4：StrikeNPC 已移除，改为按来源的命中修饰钩子
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (DeathCounter == -1)
            {
                modifiers.FinalDamage *= 0f;
                NPC.life = NPC.lifeMax;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (DeathCounter == -1)
            {
                modifiers.FinalDamage *= 0f;
                NPC.life = NPC.lifeMax;
            }
        }
    }
}