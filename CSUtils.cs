using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod;
using CalamityMod.CalPlayer;
using System.Collections.Generic;
using System.Text;
using CalamitySouls.NPCs;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls
{
    public static class CSUtils
    {
        public static void UpdateAccessory(int ItemID, Player player, bool hideVisual=false)
        {
            Item item = new Item();
            item.SetDefaults(ItemID);
            ItemLoader.UpdateAccessory(item, player, hideVisual);
        }
        public static void Strike(this NPC npc, int damage, Player player = null)
        {
            if (player == null && Main.netMode != NetmodeID.MultiplayerClient) player = Main.LocalPlayer;
            if (player.whoAmI == Main.myPlayer)
            {
                Projectile strike = Projectile.NewProjectileDirect(null, npc.Center, Vector2.Zero,
                   ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.DirectStrike>(), damage, 0f, player.whoAmI);
                strike.GetPlayerDamageType(player);
            }
        }
        public static Mod Cal = ModLoader.GetMod("CalamityMod");
        // 1.4：灾厄移除了 Player.AverageDamage 扩展，这里按 DamageClass 重新实现（1.3 语义：各系伤害倍率均值）
        public static float AverageDamage(Player player) => CSCompat.AverageDamage(player);
        public static bool HoldingANotSummonWeapon(this Player player)
        {
            if (player.GetModPlayer<CalamityPlayer>().fearmongerSet || player.setForbidden || player.GetModPlayer<CalamityPlayer>().forbiddenCirclet) return false;
            Item item = player.inventory[player.selectedItem];
            if (item.IsAir) return false;
            if (item.pick > 0 || item.axe > 0 || item.hammer > 0) return false;
            if (item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged) || item.CountsAsClass(DamageClass.Magic) || item.CountsAsClass(DamageClass.Throwing) || item.CountsAsClass<RogueDamageClass>()) return true;
            return false;
        }
        public static void HealLife(this Player player, int amount, bool visible = true)
        {
            player.statLife += amount;
            if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;
            if (visible) player.HealEffect(amount, true);
        }
        public static void HealMana(this Player player, int amount, bool visible = true)
        {
            player.statMana += amount;
            if (player.statMana > player.statManaMax2) player.statMana = player.statManaMax2;
            if (visible) player.ManaEffect(amount);
        }
        // 1.4：Projectile.melee/ranged/magic/thrown 布尔字段被 DamageType 取代
        public static void Melee(this Projectile projectile)
        {
            projectile.DamageType = DamageClass.Melee;
        }
        public static void Ranged(this Projectile projectile)
        {
            projectile.DamageType = DamageClass.Ranged;
        }
        public static void Magic(this Projectile projectile)
        {
            projectile.DamageType = DamageClass.Magic;
        }
        public static void Minion(this Projectile projectile)
        {
            projectile.DamageType = DamageClass.Summon;
        }
        public static void Rogue(this Projectile projectile)
        {
            projectile.DamageType = ModContent.GetInstance<RogueDamageClass>();
        }
        public static bool Classless(this Projectile projectile)
        {
            bool CanDo = projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.Ranged) || projectile.CountsAsClass(DamageClass.Magic) || projectile.minion || projectile.CountsAsClass(DamageClass.Throwing) || projectile.CountsAsClass<RogueDamageClass>();
            return !CanDo;
        }
        public static bool ForceClassless(this Projectile projectile)
        {
            bool CanDo = projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.Ranged) || projectile.CountsAsClass(DamageClass.Magic) || projectile.minion || projectile.CountsAsClass(DamageClass.Throwing) || projectile.CountsAsClass<RogueDamageClass>();
            projectile.DamageType = DamageClass.Default;
            return CanDo;
        }
        public static void AllClass(this Projectile projectile)
        {
            projectile.DamageType = DamageClass.Generic;
        }
        public static bool CanHitNPC(this Projectile projectile, NPC npc) => Collision.CanHit(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
        public static void FollowPlayer(this Projectile projectile, Vector2 place, float maxDist, float minDist, Vector2 velo, float speed = 0, bool positionLerp = false, float lerpAmtFar = 0.05f, float lerpAmtClose = 0.07f, float lerpAmtPos = 0.1f)
        {
            if (projectile.Distance(place) > maxDist)
            {
                projectile.Center = place - projectile.DirectionTo(place) * maxDist;
                projectile.velocity *= 0f;
            }
            else if (projectile.Distance(place) < minDist) projectile.velocity = Vector2.Lerp(projectile.velocity, velo, lerpAmtClose);
            else projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(place) * speed, lerpAmtFar);
            if (positionLerp) projectile.Center = Vector2.Lerp(projectile.Center, place, lerpAmtPos);
        }
        public static int ClosetNPC(this Projectile projectile, float maxDist) => projectile.Center.ClosetNPC(maxDist, !projectile.tileCollide);
        public static int ClosetNPC(this Vector2 pos, float maxDist, bool ignoreTile = true)
        {
            int index = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC n = Main.npc[i];
                if (n.active && !n.friendly && n.CanBeChasedBy() && (maxDist == -1 || n.Distance(pos) < maxDist))
                {
                    if (ignoreTile || Collision.CanHit(pos, 1, 1, n.position, n.width, n.height))
                    {
                        index = i;
                        maxDist = n.Distance(pos);
                    }
                }
            }
            return index;
        }
        public static float LerpRotation(this float Angle1, float Angle2, float lerpAmt = 0.1f)
        {
            Angle1 = MathHelper.WrapAngle(Angle1);
            Angle2 = MathHelper.WrapAngle(Angle2);
            if (Angle1 > Angle2)
            {
                if (Angle1 - Angle2 > MathHelper.Pi) Angle1 -= MathHelper.PiOver2;
                return MathHelper.Lerp(Angle1, Angle2, lerpAmt);
            }
            else
            {
                if (Angle2 - Angle1 > MathHelper.Pi) Angle2 -= MathHelper.PiOver2;
                return MathHelper.Lerp(Angle1, Angle2, lerpAmt);
            }
        }
        public static void ModifyAllCrit(this Player player, int add = 0, bool set = false, int settle = 0)
        {
            if (set)
            {
            }
        }
        public static bool AnyBossAlive
        {
            get
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.boss) return true;
                    if (npc.active && npc.type == NPCID.EaterofWorldsHead) return true;
                }
                return false;
            }
        }
        public static bool GameCultureChinese
        {
            get
            {
                // 1.4：GameCulture.CultureName.Chinese 只对应繁体（zh-Hant），简体（zh-Hans）不在该枚举里，
                // 直接用枚举判断会导致简体环境下恒为 false、所有中英文分支都显示英文。
                // 这里改为按文化名判断，简繁都算中文。
                var culture = Language.ActiveCulture;
                if (culture == null) return false;
                string name = culture.Name;
                return name != null && name.StartsWith("zh");
            }
        }
        public static GameCulture GcChinese => GameCulture.FromCultureName(GameCulture.CultureName.Chinese);
        public static string GetModItemText(int ItemID, string color = "", string itemname = "")
        {
            Item item = new Item(); item.SetDefaults(ItemID);
            string name = item.Name;
            if (itemname != "") name = itemname;
            if (color == "") return "[i:" + ItemID + "]「" + name + "」";
            return "[i:" + ItemID + "]「[c/" + color + ":" + name + "]」";
        }
        public static bool GoodNetMode => Main.netMode != NetmodeID.MultiplayerClient;
        public static CSPlayer CS(this Player player) => player.GetModPlayer<CSPlayer>();
        public static CSNPC CS(this NPC npc) => npc.GetGlobalNPC<CSNPC>();
        public static CSProjectile CS(this Projectile projectile) => projectile.GetGlobalProjectile<CSProjectile>();
        public static void GetItemDamageType(this Projectile projectile, Item item)
        {
            // 1.4：布尔伤害标志已由 DamageType 取代
            projectile.DamageType = item.DamageType;
            projectile.minion = item.CountsAsClass(DamageClass.Summon);
        }
        public static void GetPlayerDamageType(this Projectile projectile, Player player)
        {
            Item item = player.inventory[player.selectedItem];
            if (item.IsAir)
            {
                projectile.DamageType = DamageClass.Default;
                projectile.minion = false;
                return;
            }
            // 1.4：布尔伤害标志已由 DamageType 取代
            projectile.DamageType = item.DamageType;
            projectile.minion = item.CountsAsClass(DamageClass.Summon);
        }
        public static void GetItemDamageType(int ProjWhoAmI, Item item)
        {
            Projectile projectile = Main.projectile[ProjWhoAmI];
            projectile.DamageType = item.DamageType;
            projectile.minion = item.CountsAsClass(DamageClass.Summon);
        }
        public static void CopyProjDamageType(this Projectile projectile, Projectile Source)
        {
            projectile.DamageType = Source.DamageType;
            projectile.minion = Source.minion;
            projectile.friendly = Source.friendly;
            projectile.hostile = Source.hostile;
        }
        public static string TooltipHotkeyString(this ModKeybind mhk)
        {
            if (Main.dedServ || mhk == null)
            {
                return "";
            }
            List<string> keys = mhk.GetAssignedKeys(0);
            if (keys.Count == 0)
            {
                return GameCultureChinese ? "[未设置，请在控件中设置该快捷键]" : "[None yet, please set the key up in Control]";
            }
            StringBuilder sb = new StringBuilder(16);
            sb.Append(keys[0]);
            for (int i = 1; i < keys.Count; i++)
            {
                sb.Append(" / ").Append(keys[i]);
            }
            return sb.ToString();
        }
        public static void DoAVisualBuff(this Player player, int type, int time = 0)
        {
            // 1.4：buff 图标带渐显动画，若每帧 ClearBuff + AddBuff 会不断重建图标，
            // 表现为 buff 栏闪烁/抽搐（弑神者魔石每帧调用本方法，尤其明显）。
            // 这里改为：已存在时只更新剩余时间，只有缺失时才真正添加。
            int idx = player.FindBuffIndex(type);
            if (idx >= 0)
            {
                if (player.buffTime[idx] < time + 2) player.buffTime[idx] = time + 2;
            }
            else
            {
                player.AddBuff(type, time + 2);
            }
        }
        public static Vector2 RandomRotate => MathHelper.ToRadians(Main.rand.Next(360)).ToRotationVector2();
        public static int ownedProjectileCounts(this Player player, int type)
        {
            int amt = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.type == type && projectile.owner == player.whoAmI) amt++;
            }
            return amt;
        }
        public static bool IsAnExtraProj(this Projectile projectile)
        {
            return projectile.Classless() || CSLists.ExtraProjectileList.Contains(projectile.type);
        }
        public static int TransFloatToInt(float num)
        {
            int low = (int)num;
            int chance = (int)((num - low) * 100);
            if (Main.rand.Next(100) < chance) low++;
            return low;
        }
        public static void ForceThrowABuffOnYourFuckingFace(this NPC npc, int type, int time = 2)
        {
            npc.buffImmune[type] = false;
            npc.AddBuff(type, time);
        }
        public static string Trans10to16under256(this int num)
        {
            int numSmall = num % 16;
            num /= 16;
            return num.Trans10to16under16() + numSmall.Trans10to16under16();
        }
        public static string Trans10to16under16(this int num)
        {
            if (num == 10) return "A";
            if (num == 11) return "B";
            if (num == 12) return "C";
            if (num == 13) return "D";
            if (num == 14) return "E";
            if (num == 15) return "F";
            return num.ToString();
        }
        public static Vector2 Length(this Vector2 vector2, float amount) => Vector2.Normalize(vector2) * amount;
        public static int GetOneOrNegativeOne => Main.rand.Next(2) * 2 - 1;
        public static string ManicModeName => "Manic Mode";
        public static string ManicModeNameChi => "脑瘫模式";
        public static bool ManicModeNerf => !AnyBossAlive && true;
        public static string BlankTexture => "CalamitySouls/Extras/BlankTexture";
        public static void AddImmunityTime(this Player player, int amount)
        {
            player.immune = true;
            player.immuneTime += amount;
            for (int i = 0; i < player.hurtCooldowns.Length; i++)
            {
                player.hurtCooldowns[i] += amount;
            }
        }
        public static void SetImmunityTime(this Player player, int amount)
        {
            if (player.immuneTime < amount)
            {
                player.immune = true;
                player.immuneTime = amount;
                for (int i = 0; i < player.hurtCooldowns.Length; i++)
                {
                    player.hurtCooldowns[i] = amount;
                }
            }
        }
        public static bool IsBossExist<T>() where T : ModNPC
        {
            int type = NPCType<T>();
            if (!CSNPCStatic.bIndexDict.ContainsKey(type)) return false;
            int index = CSNPCStatic.bIndexDict[type];
            if (index < 0 || index >= 255) return false;
            return true;
        }
        public static NPC GetBoss<T>() where T : ModNPC
        {
            int type = NPCType<T>();
            if (!IsBossExist<T>()) return null;
            return Main.npc[CSNPCStatic.bIndexDict[type]];
        }
        public static void StrikeNPCExtra(this NPC target, int damage, Color Color, Player player = null)
        {
            if (!target.active || target.life <= 1) return;
            if (player != null) player.addDPS(damage);
            if ((target.takenDamageMultiplier > 1f || damage != 9999) && target.lifeMax > 1)
                CombatText.NewText(target.Hitbox, Color, damage);
            if (target.HitSound != null) SoundEngine.PlaySound(target.HitSound, target.position);
            if (!target.immortal)
                if (target.realLife >= 0)
                {
                    NPC realLife = Main.npc[target.realLife];
                    realLife.life -= damage;
                    target.life = realLife.life;
                    target.lifeMax = realLife.lifeMax;
                }
                else target.life -= damage;
            if (target.life > 0) return;
            if (target.realLife >= 0)
            {
                Main.npc[target.realLife].life = 1;
                return;
            }
            target.life = 1;
        }
        public static int GetHostileProjDamage(float damage)
        {
            damage /= 2f;
            if (Main.expertMode) damage /= 2f;
            return (int)damage;
        }
        public static int ProjDamage(this NPC npc, float mult = 1f) => GetHostileProjDamage(npc.damage * mult);
        public static bool HoldingATrueMeleeWeapon(int whoAmI)
        {
            if (whoAmI < 0 || whoAmI >= 255) return false;
            Player player = Main.player[whoAmI];
            if (!player.active) return false;
            Item item = player.inventory[player.selectedItem];
            if (item == null || !item.CountsAsClass(DamageClass.Melee)) return false;
            if (item.shoot == ProjectileID.None || false) return true;
            return false;
        }
        public static bool HoldingATrueMeleeWeapon(this Player player)
        {
            if (!player.active) return false;
            Item item = player.inventory[player.selectedItem];
            if (item == null || !item.CountsAsClass(DamageClass.Melee)) return false;
            if (item.shoot == ProjectileID.None || false) return true;
            return false;
        }
    }
}