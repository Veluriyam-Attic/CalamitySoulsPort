// 皇天幻影 EmpyreanSpectreV104 —— 层 2 第 2 轮：AI + 辅助 + 分派器 + Melee/Ranged 攻击
// 来源：ext1040/Projectiles/EmpyreanSpectre.cs（本模组自己的弹幕）
// 本轮实现：trueWeaponRotation / trueWeaponPosition / HasClass / FindTarget / FindClass / Attack / BackToPlayer
//           + AI（含帧动画与 rotation 三角式）+ MeleeAttack + RangedAttack
// 仍为空壳（第 3 轮）：MagicAttack / SummonAttack / RogueAttack / ClearWeapon / ClearWeaponNoDust / SpawnDust / PreDraw
// 差异表：两处音量修饰（Item14 ×4 · NPCDeath13 ÷2）未移植；FollowPlayer 用可证等价的 5 参形式
// 贴图：Projectiles/EmpyreanSpectre(_Wing)（v1.0.4.0 包）→ 第 3 轮接 PreDraw
using System;
using System.IO;
using CalamityMod;
using Microsoft.Xna.Framework;
using Terraria;
using ReLogic.Content;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104.Items;

namespace CalamitySouls.Content.V104.Projectiles
{
	public class EmpyreanSpectreV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "皇天幻影");
			Main.projFrames[Type] = 4;
			ProjectileID.Sets.TrailingMode[Type] = -1;
			ProjectileID.Sets.TrailCacheLength[Type] = 6;
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 52;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.friendly = true;
			player = Main.player[Projectile.owner];
			HasWeapon = false;
			WeaponRotation = 0;
			WeaponPosition = Vector2.Zero;
			WeaponScale = 1f;
			WeaponOldRot = new float[ProjectileID.Sets.TrailCacheLength[Type]];
			WeaponOldPos = new Vector2[ProjectileID.Sets.TrailCacheLength[Type]];
			OldSpriteDirection = new int[ProjectileID.Sets.TrailCacheLength[Type]];
			for (int i = 0; i < WeaponOldPos.Length; i++)
			{
				OldSpriteDirection[i] = 1;
				WeaponOldPos[i] = Vector2.Zero;
			}
			wingFrame = 0;
		}

		public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(targetPosition.X);
            writer.Write(targetPosition.Y);
            writer.Write(HasWeapon);
            writer.Write(WeaponRotation);
            writer.Write(WeaponPosition.X);
            writer.Write(WeaponPosition.Y);
            writer.Write(WeaponScale);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            targetPosition.X = reader.ReadSingle();
            targetPosition.Y = reader.ReadSingle();
            HasWeapon = reader.ReadBoolean();
            WeaponRotation = reader.ReadSingle();
            WeaponPosition.X = reader.ReadSingle();
            WeaponPosition.Y = reader.ReadSingle();
            WeaponScale = reader.ReadSingle();
        }
        public int AttackTimer
		{
			get => Projectile.soundDelay;
			set => Projectile.soundDelay = value;
		}
		public float ai0
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public float ai1
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		public Player player;
		public Vector2 targetPosition;
		public bool HasWeapon;
		// ⭐ [2026-10-11] 记录"上一帧是否已经贴在玩家身上"，用于只播一次真正的到达粉尘
		//    （原因见 BackToPlayer 内的注释：ai1 会被 AI() 周期性顶回 0）
		private bool arrivedLastFrame;
		public float WeaponRotation;
		public float WeaponScale;
		public Vector2 WeaponPosition;
		public float[] WeaponOldRot;
		public Vector2[] WeaponOldPos;
		public int[] OldSpriteDirection;
		public int wingFrame;

		// 1.3: projectile.melee/ranged/magic/minion/sentry + projectile.Calamity().rogue（2.x 按 DamageType 判定）
		public int GetAttackTime
		{
			get
			{
				if (Projectile.DamageType == DamageClass.Melee) return 120;
				if (Projectile.DamageType == DamageClass.Ranged) return 45;
				if (Projectile.DamageType == DamageClass.Magic) return 36;
				if (Projectile.DamageType == DamageClass.Summon) return 60;
				if (Projectile.DamageType == ModContent.GetInstance<RogueDamageClass>()) return 90;
				return 0;
			}
		}

		public float trueWeaponRotation
		{
			get
			{
				if (ai1 == 0) return Projectile.spriteDirection * WeaponRotation;
				if (ai1 == 1) return Projectile.spriteDirection == -1 ? MathHelper.Pi + WeaponRotation : WeaponRotation;
				return WeaponRotation;
			}
		}
		public Vector2 trueWeaponPosition
		{
			get
			{
				if (ai1 == 0) return new Vector2(WeaponPosition.X * Projectile.spriteDirection, WeaponPosition.Y);
				if (ai1 == 1) return WeaponPosition * Projectile.spriteDirection;
				return WeaponPosition;
			}
		}
		public bool HasClass => !Projectile.Classless();

		// 自包含私有副本（照 OmegaTenacleV104 同法；不改共享文件）
		private static bool HoldingAClassedWeaponV104(Player p)
		{
			Item item = p.HeldItem;
			if (item.IsAir || item.damage <= 0) return false;
			if (item.pick > 0 || item.axe > 0 || item.hammer > 0) return false;
			return item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged)
				|| item.CountsAsClass(DamageClass.Magic) || item.CountsAsClass(DamageClass.Summon)
				|| item.CountsAsClass(DamageClass.Throwing)
				|| item.CountsAsClass(ModContent.GetInstance<RogueDamageClass>());
		}

		// [修复 2026-10-10] 贴图缓存（原写法在 PreDraw 里每帧 ImmediateLoad ✗）
		private static ReLogic.Content.Asset<Texture2D> _spectreTex;
		private static ReLogic.Content.Asset<Texture2D> _spectreWingTex;

		public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.alpha == 255) return false;
            // [修复 2026-10-10] 原写法在 PreDraw 里每帧 ImmediateLoad（同步阻塞）✗
            // 改为静态缓存：只在第一次请求，之后直接复用 ⇒ 不再每帧阻塞主线程
            _spectreTex ??= ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Projectiles/EmpyreanSpectreV104", AssetRequestMode.AsyncLoad);
            _spectreWingTex ??= ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Projectiles/EmpyreanSpectreWingV104", AssetRequestMode.AsyncLoad);
            Texture2D texture = _spectreTex.Value;
            Texture2D Wing = _spectreWingTex.Value;
            if (texture == null || Wing == null) return false;   // 还没加载完就先不画（安全 ✓）
            Texture2D Weapon = TextureAssets.Item[GetWeaponType()].Value;
            Rectangle GetSpectreFrameRectan = new Rectangle(0, Projectile.frame * 54, 40, 52);
            Rectangle GetWingFrameRectan = new Rectangle(0, wingFrame * 62, 86, 60);
            Vector2 origin = new Vector2(20, 26);
            Vector2 worigin = new Vector2(43, 30);
            Vector2 weaorigin = new Vector2(Weapon.Width, Weapon.Height) / 2f;
            Vector2 wTruePos = new Vector2(-23 + 32, 26) - Main.screenPosition;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
                wTruePos = new Vector2(28, 26) - Main.screenPosition;
            }
            SpriteBatch spriteBatch = Main.spriteBatch;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            float scale = Projectile.scale * 1.35f;
            float wScale = WeaponScale * 1.2f;
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                Color color = Color.Lerp(Color.LimeGreen, Color.Violet, i / (Projectile.oldPos.Length - 2f));
                scale *= 0.9f;
                wScale *= 0.9f;
                SpriteEffects oldEffects = (OldSpriteDirection[i] == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(Wing, Projectile.oldPos[i] - origin + wTruePos, GetWingFrameRectan, color,
                    Projectile.oldRot[i], worigin, scale, oldEffects, 0f);
                spriteBatch.Draw(texture, Projectile.oldPos[i] - Main.screenPosition, GetSpectreFrameRectan, color,
                    Projectile.oldRot[i], origin, scale, oldEffects, 0f);
                if (HasWeapon)
                    spriteBatch.Draw(Weapon, WeaponOldPos[i] - Main.screenPosition,
                        null, color, WeaponOldRot[i], weaorigin, wScale, oldEffects, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            lightColor = Color.White;
            spriteBatch.Draw(Wing, Projectile.position + wTruePos, GetWingFrameRectan, lightColor, Projectile.rotation, worigin, Projectile.scale, effects, 0f);
            spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, GetSpectreFrameRectan, lightColor, Projectile.rotation, origin, Projectile.scale, effects, 0f);
            if (HasWeapon)
            {
                spriteBatch.Draw(Weapon, trueWeaponPosition + Projectile.Center - Main.screenPosition, null, lightColor, trueWeaponRotation, weaorigin, WeaponScale, effects, 0f);
            }
            return false;
        }
        public int GetWeaponType()
		{
			if (Projectile.DamageType == DamageClass.Melee) return ModContent.ItemType<XerocsGreatswordV104>();
			if (Projectile.DamageType == DamageClass.Ranged) return ModContent.ItemType<TheEmpyreanV104>();
			if (Projectile.DamageType == DamageClass.Magic) return ModContent.ItemType<TomeofFatesV104>();
			if (Projectile.DamageType == DamageClass.Summon) return ModContent.ItemType<LanternoftheSoulV104>();
			return ModContent.ItemType<XerocPitchforkV104>();
		}

		public override bool? CanDamage() => false;

		public override void AI()
		{
			player = Main.player[Projectile.owner];
			// 1.3 判 player.CS().EmpyreanSpectre；该标记在 1.3 仅由 EnchEmpyrean 置真（CSPlayerEffects:155-158）⇒ EnchEmpyrean 等价 ✓（已结案）
			if (!player.active || player.dead || !player.V104().EnchEmpyrean)
			{
				SpawnDust(Projectile.Center);
				Projectile.Kill();
				return;
			}
			Projectile.timeLeft = 2;
			if (AttackTimer == 1)
			{
				ai0 = 0;
				ai1 = 0;
			}
			// ⭐⭐ [2026-10-11 修「皇天幻影抽搐」] 必须连【速度】一起清理 NaN —— 原来的检查只管位置 ✗
			//    日志实证（01:4x 会话）：
			//      System.ArithmeticException: Function does not accept floating point Not-a-Number values.
			//        at System.Math.Sign(Single value)
			//        at CalamitySouls...EmpyreanSpectreV104.AI()  ← 本文件 AI() 的末行 Math.Sign(velocity.X)
			//    ⇒ 速度一旦是 NaN，Math.Sign 抛异常 ⇒ **AI 每帧在这里中断** ⇒ 后续的帧计数/翅膀/朝向都不更新、
			//      位置也停在异常状态 ⇒ 玩家看到的就是"抽搐" ✗
			//    ⇒ 先把 NaN 速度清零，再走原来的"NaN 位置/过远则拉回玩家身上" ✓
			if (float.IsNaN(Projectile.velocity.X) || float.IsNaN(Projectile.velocity.Y))
				Projectile.velocity = Vector2.Zero;
			if (Projectile.position.HasNaNs() || Projectile.Distance(player.Center) > 3600f) Projectile.Center = player.Center;

			for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
			{
				Projectile.oldPos[i] = Projectile.oldPos[i - 1];
				Projectile.oldRot[i] = Projectile.oldRot[i - 1];
				WeaponOldPos[i] = WeaponOldPos[i - 1];
				WeaponOldRot[i] = WeaponOldRot[i - 1];
				OldSpriteDirection[i] = OldSpriteDirection[i - 1];
			}
			Projectile.oldPos[0] = Projectile.Center;
			Projectile.oldRot[0] = Projectile.rotation;
			WeaponOldPos[0] = trueWeaponPosition + Projectile.Center;
			WeaponOldRot[0] = trueWeaponRotation;
			OldSpriteDirection[0] = Projectile.spriteDirection;

			int index = FindTarget();
			// 🔍 [临时诊断 2026-10-11] 记录幻影实际锁定的目标 —— 用于回答"周围明明没怪，为什么还在攻击/冒粉尘"：
			//    FindTarget() 的第二条来自 player.closetNPCindex（CSUtils.ClosetNPC(中心, -1) = 全图最近），
			//    再由下一行按 1800px 过滤 ⇒ 【屏幕外/地下的敌人也会被锁定】✓
			//    （已核对 1.3 与本工程的 ClosetNPC 筛选条件逐字相同：active && !friendly && CanBeChasedBy ✓）
			if (index != -1 && Main.GameUpdateCount % 30 == 0)
			{
				NPC tn = Main.npc[index];
				Terraria.ModLoader.ModLoader.GetMod("CalamitySouls").Logger.Info(
					$"[幻影目标] {tn.FullName} type={tn.type} dist={tn.Distance(player.Center):F0} " +
					$"life={tn.life}/{tn.lifeMax} friendly={tn.friendly} t={Main.GameUpdateCount}");
			}
			bool DoAttackingThisFrame;
			if (AttackTimer <= 0)
			{
				if (index != -1)
				{
					NPC npc = Main.npc[index];
					targetPosition = npc.Center;
					if (HoldingAClassedWeaponV104(player))
					{
						FindClass();
						DoAttackingThisFrame = true;
					}
					else DoAttackingThisFrame = HasClass;
				}
				else DoAttackingThisFrame = false;
			}
			else
			{
				// 1.3: if (index != -1) targetPosition = Main.npc[index].Center;  DoAttackingThisFrame = true;
				// ⭐⭐ [2026-10-11 修「无敌人时幻影一直冒粉尘/抽搐」· 第二次修正]
				//    1.3 的 `DoAttackingThisFrame = true` 本意是"让已经开始的这一击打完" ✓，
				//    但 2.x 里攻击函数内部有多处【按距离重新上膛】，会把这一击无限续下去：
				//      · RogueAttack: `if (dist < 120 && AttackTimer == GetAttackTime - 29 ...) AttackTimer = GetAttackTime;`
				//        ← 我把 targetPosition 收敛到自己身上后，dist≈0 < 120 ⇒ 反而每轮都命中它 ✗
				//      · 其余 `if (dist > 800) AttackTimer = ...` 已被 targetPosition 收敛挡掉 ✓
				//    ⇒ 结果：幻影永远处于攻击状态 ⇒ 每帧收招 ⇒ `ClearWeapon()` 用
				//      `SpawnDust(Projectile.Center + trueWeaponPosition)` 在【幻影自己身上】炸紫色粉尘 ✗
				//      （玩家截图：紫球在幻影位置 ✓ 而非玩家身上 ⇒ 正是 ClearWeapon 那条 ✗）
				//    ⇒ 完全无目标时直接收招（不再"打完这一击"）。这是【有意偏离 1.3】：1.3 不会有
				//      重新上膛的自续问题，而 2.x 会 ⇒ 不改就无法达到 1.3 的"只闪一下"表现 ✓
				if (index != -1)
				{
					targetPosition = Main.npc[index].Center;
					DoAttackingThisFrame = true;
				}
				else DoAttackingThisFrame = false;
			}
			Projectile.alpha = 0;
			// ⭐ [2026-10-11 修「抽搐」· 第三次修正] targetPosition 若是 NaN，会污染其后所有向量运算
			//    （`targetPosition - Projectile.Center` ⇒ NaN 速度 ⇒ AI 末行 Math.Sign(NaN) 抛异常 ⇒
			//      AI 每帧中断 ⇒ 帧计数/翅膀/朝向不更新 = 玩家看到的"抽搐" ✗）。
			//    来源可能是某个模组的 NPC 坐标为 NaN（FindTarget 直接取 npc.Center）⇒ 这里统一清洗 ✓
			if (targetPosition.HasNaNs()) targetPosition = player.Center;
			if (DoAttackingThisFrame) Attack();
			else
			{
				if (HasWeapon) ClearWeapon();
				BackToPlayer();
				if (ai1 == -1) Projectile.alpha = 255;
			}

			Projectile.frameCounter++;
			if (Projectile.frameCounter % 7 == 0)
			{
				Projectile.frame++;
				wingFrame++;
			}
			if (Projectile.frame >= 2) Projectile.frame = 0;
			if (wingFrame >= 4) wingFrame = 0;
			if (Projectile.velocity.Y > 0) wingFrame = 2;
			// ⭐ [2026-10-11 修「抽搐」· 第三次修正] 1.3 原样是 Math.Sign(Projectile.velocity.X)，
			//    但速度可能在本帧的计算中才变成 NaN（AI 开头的检查挡不住"本帧新产生"的 NaN ✗）
			//    ⇒ Math.Sign(NaN) 抛 ArithmeticException（日志实证）⇒ AI 每帧在此中断 ⇒ 抽搐 ✗
			//    ⇒ 在【使用点】兜住：NaN 当作 0 处理 ✓（角度算式本身对 0 是安全的 ✓）
			float rotVx = float.IsNaN(Projectile.velocity.X) ? 0f : Projectile.velocity.X;
			Projectile.rotation = MathHelper.ToRadians(25 - 25 / (1 + Math.Abs(rotVx) / 5f)) * Math.Sign(rotVx);
		}

		public int FindTarget()
		{
			if (player.HasMinionAttackTargetNPC)
				if (Main.npc[player.MinionAttackTargetNPC].Distance(player.Center) < 1800f) return player.MinionAttackTargetNPC;

			int target = player.V104().closetNPCindex;
			if (target != -1 && Main.npc[target].Distance(player.Center) <= 1800f) return target;

			foreach (NPC npc in Main.npc)
				// ⭐ [2026-10-11] 加两个小动物排除（本行是【应玩家要求主动偏离 1.3】）：
				//    1.3 原码这里只判 active && !friendly && !dontTakeDamage ⇒ 会把【小动物】当成敌人，
				//    而小动物不满足 CanBeChasedBy()（要求 lifeMax > 5，tml_all\Terraria\NPC.cs:107202）
				//    ⇒ ClosetNPC 排除它、这里却收下它 ✗
				//    ⚠️ 必须【同时】判两个来源，缺一不可 —— 这是第一次没修干净的原因：
				//      · npc.CountsAsACritter      = 原版属性（NPC.cs:1657）
				//      · NPCID.Sets.CountsAsCritter[type] = tML 集合，**灾厄自己的小动物只设这个**
				//        （BabyGhostBell / SeaMinnow / BabyCannonballJellyfish / Androomba / Twinkler … 共 15 种）
				//      灾厄本体的炮塔也是两个一起判（TEPlayerFireTurret.cs:127 等 7 处）✓ 照它的写法。
				//    结果：玩家实测「站着不动一直冒紫色粉尘、一移动就消失」—— 幻影在反复近战攻击
				//    附近的小动物（第 19 帧每轮炸 15×10 颗 PurpleTorch 粉尘）✓
				if (npc.active && !npc.friendly && !npc.dontTakeDamage
					&& !npc.CountsAsACritter && !NPCID.Sets.CountsAsCritter[npc.type]
					&& npc.Distance(player.Center) < 1800f)
					return npc.whoAmI;

			return -1;
		}
		public void FindClass()
		{
			if (!HoldingAClassedWeaponV104(player)) return;
			Projectile.GetPlayerDamageType(player);
		}
		public void BackToPlayer()
		{
			ai0 = -1;
			Vector2 pos = player.Center;
			if (ai1 == -1)
			{
				Projectile.Center = pos;
				Projectile.velocity = Vector2.UnitY * 0.01f * Math.Sign(player.velocity.Y);
				Projectile.spriteDirection = player.direction;
			}
			else
			{
				// 1.3: projectile.FollowPlayer(pos, 2000f, 0f, 0.1f)（4 参重载 = 转调 5 参、lerpAmtPos=0.1f ⇒ 可证等价）
				Projectile.FollowPlayer(pos, 2000f, 0f, Vector2.Zero, 0.1f);
				if (Projectile.Distance(pos) < 4f)
				{
					// ⭐ [2026-10-11 修] 只在【真正飞回来】的那一帧播粉尘；上一帧就已经贴在你身上时不再播。
					//    诊断日志（1:26-1:27 会话）钉死了现象：
					//      caller=BackToPlayer ai1=0 ai0=-1 HasWeapon=False AttackTimer=0 HasClass=False dist=0.0 target=-1
					//    ⇒ ai1 本应是 -1（"已回到玩家身上"的闩锁），却被 AI() 里
					//      `if (AttackTimer == 1) { ai0 = 0; ai1 = 0; }` 顶回 0 ⇒ 下一帧走 else 分支 ⇒
					//      dist 本来就是 0.0 < 4f ⇒ 每次都当成"刚到达"⇒ 每轮都炸一次紫色粉尘 ✗
					//    修法：用 arrivedLastFrame 记录"上一帧是否已在身上"，只播真正的到达那一帧 ✓
					//    （真正让玩家看到"只闪一次"的是 AI() 里"无目标不再攻击"那处修复 ⇒ 不再有收招 ⇒
					//      ai1 恒为 -1 ⇒ 这条分支之后不会再进入 ✓）
					if (!arrivedLastFrame) SpawnDust(pos);
					arrivedLastFrame = true;
					ai1 = -1;
				}
				else arrivedLastFrame = false;
				Projectile.spriteDirection = Math.Sign(player.Center.X - Projectile.Center.X);
			}
		}
		public void Attack()
		{
			if (AttackTimer == 0) AttackTimer = GetAttackTime;
			if (Projectile.DamageType == DamageClass.Melee) MeleeAttack();
			else if (Projectile.DamageType == DamageClass.Ranged) RangedAttack();
			else if (Projectile.DamageType == DamageClass.Magic) MagicAttack();
			else if (Projectile.DamageType == DamageClass.Summon) SummonAttack();
			else if (Projectile.DamageType == ModContent.GetInstance<RogueDamageClass>()) RogueAttack();
		}

		public void MeleeAttack()
		{
			ai1 = 0;
			Vector2 movePos = Vector2.UnitX * 168f * -Math.Sign(targetPosition.X - player.Center.X);
			Projectile.spriteDirection = Math.Sign(targetPosition.X - Projectile.Center.X);
			float baseRot = MathHelper.ToRadians(90 - 50.46f);
			float baseLengthOver2 = 84f;
			if (AttackTimer >= GetAttackTime - 60)
			{
				if (AttackTimer == GetAttackTime) HasWeapon = true;
				float radius = (GetAttackTime - AttackTimer) / 60f;
				WeaponRotation = baseRot - MathHelper.ToRadians(90 + MathHelper.Lerp(0, 20, radius));
				WeaponScale = MathHelper.Lerp(1f, 2f, radius);
				movePos *= (168f + MathHelper.Lerp(0, 48, radius)) / 168;
			}
			else if (AttackTimer > GetAttackTime - 80)
			{
				if (Projectile.Distance(targetPosition) > 168f * 2f) AttackTimer++;
				movePos -= Vector2.UnitY * 64;
			}
			else if (AttackTimer >= 20)
			{
				float radius = 1 - (AttackTimer - 20f) / (GetAttackTime - 80f - 20f);
				WeaponRotation = baseRot + MathHelper.ToRadians(MathHelper.Lerp(-110, 0, radius));
				movePos *= (168f - MathHelper.Lerp(0, 48, radius)) / 168;
				movePos -= Vector2.UnitY * MathHelper.Lerp(64, -32, radius);
			}
			else if (AttackTimer == 19)
			{
				for (int i = 0; i < 15; i++)
				{
					if (Projectile.owner == Main.myPlayer)
					{
						Vector2 pos = (WeaponRotation - baseRot).ToRotationVector2() * i * 24 + Main.rand.NextVector2Unit() * Main.rand.NextFloat(16f);
						pos.X *= Projectile.spriteDirection;
						pos += Projectile.Center;
						// 1.3: Main.PlaySound(SoundID.Item14, pos).Volume *= 4f（音量修饰未移植，见差异表）
						SoundEngine.PlaySound(SoundID.Item14, pos);
						for (int j = 0; j < 10; j++)
						{
							Dust explosion = Dust.NewDustDirect(pos - new Vector2(12), 24, 24, DustID.PurpleTorch);
							if (Main.rand.Next(2) == 0)
							{
								explosion.noGravity = false;
								explosion.scale = Main.rand.NextFloat(1f, 2f);
								explosion.velocity = new Vector2(Projectile.spriteDirection * Main.rand.NextFloat(17f - i), -Main.rand.NextFloat(7f));
							}
							else
							{
								explosion.noGravity = true;
								explosion.scale = Main.rand.NextFloat(2f, 3f);
								explosion.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 2f);
							}
						}
						Vector2 starDir = (targetPosition - pos).SafeNormalize(Vector2.UnitY) * 5f;   // 1.3: CSUtils.DirectionToSafe(pos, targetPosition) * 5f
						Projectile star = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), pos, starDir,
							ModContent.ProjectileType<XerocStarV104>(), (int)(200f * player.MeleeDamage()), 0f, Projectile.owner);
						star.timeLeft = 120;
						star.extraUpdates = 1;
						star.tileCollide = false;
					}
				}
				ClearWeaponNoDust();
			}
			if (HasWeapon) WeaponPosition = (WeaponRotation - baseRot).ToRotationVector2() * (baseLengthOver2 * WeaponScale);
			Projectile.velocity = Vector2.Lerp(Projectile.velocity,
				CSPlayerV104Effects.DirectionToSafe(Projectile, targetPosition + movePos) * 20f, 0.06f);
			if (Projectile.Distance(targetPosition + movePos) < 8f) Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Zero, 0.1f);
		}

		public void RangedAttack()
		{
			ai1 = 1;
			WeaponScale = 1f;
			Projectile.spriteDirection = Math.Sign(targetPosition.X - Projectile.Center.X);
			float movePosRad = 0f;
			float movePosAmt = 180f;
			if (AttackTimer > 25)
			{
				HasWeapon = true;
				float radius = (GetAttackTime - AttackTimer) / (GetAttackTime - 25f);
				WeaponRotation = CSPlayerV104Effects.AngleToSafe(Projectile, targetPosition);
				WeaponPosition = trueWeaponRotation.ToRotationVector2() * MathHelper.Lerp(0, 24, radius);
				Vector2 mouthPos = Projectile.Center + trueWeaponPosition + WeaponRotation.ToRotationVector2() * 40f;
				Dust dust = Dust.NewDustPerfect(mouthPos, DustID.PurpleTorch);
				dust.noGravity = true;
				dust.velocity = Main.rand.NextVector2Unit();
				dust.position = mouthPos - dust.velocity * Main.rand.NextFloat(12f, 20f);
				dust.velocity *= Main.rand.NextFloat(2f, 5f);
				dust.scale = 2f;
				if (AttackTimer == 30)
				{
					// 1.3: Main.PlaySound(SoundID.NPCDeath13, mouthPos).Volume /= 2（音量修饰未移植，见差异表）
					SoundEngine.PlaySound(SoundID.NPCDeath13, mouthPos);
				}
			}
			else
			{
				if (AttackTimer == 25)
				{
					WeaponPosition = trueWeaponRotation.ToRotationVector2() * -8;
					Vector2 mouthPos = Projectile.Center + trueWeaponPosition + WeaponRotation.ToRotationVector2() * 40f;
					for (int i = 0; i < 6; i++)
					{
						Vector2 dir = WeaponRotation.ToRotationVector2() * Main.rand.NextFloat(10, 15f) * 2f;
						dir.Y -= Main.rand.NextFloat(2);
						Projectile fire = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), mouthPos, dir,
							ModContent.ProjectileType<XerocFireV104>(), (int)(player.RangedDamage() * 250f), 0f, Projectile.owner);
						fire.tileCollide = false;
						fire.timeLeft = 120;
					}
				}
				float radius = 1 - ((AttackTimer - 5f) / 20f);
				WeaponRotation = CSPlayerV104Effects.AngleToSafe(Projectile, targetPosition);
				WeaponPosition = trueWeaponRotation.ToRotationVector2() * MathHelper.Lerp(-8, 0, radius);
			}
			Vector2 destination = targetPosition + CSPlayerV104Effects.DirectionFromSafe(player, targetPosition)
					* (player.Distance(targetPosition) * movePosRad + movePosAmt);
			Projectile.velocity = Vector2.Lerp(Projectile.velocity,
				CSPlayerV104Effects.DirectionToSafe(Projectile, destination) * 20f, 0.06f);
			if (Projectile.Distance(destination) < 8f) Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Zero, 0.1f);
		}

		// ==== 第 3 轮填实（当前为空壳，保证文件完整可编译）====
		public void MagicAttack()
		{
			WeaponScale = 1f;
			Projectile.velocity = Vector2.Zero;
			if (AttackTimer % 36 <= 6 && AttackTimer % 36 > 0)
			{
				ai1 = 1;
				HasWeapon = true;
				// 1.3: projectile.AngleFromSafe(targetPosition)（我方无该 helper ⇒ 等价 DirectionFromSafe(...).ToRotation()）
				Projectile.position = targetPosition + CSPlayerV104Effects.DirectionFromSafe(Projectile, targetPosition).ToRotation()
					.ToRotationVector2().RotatedBy(MathHelper.PiOver4) * 240f;
				Projectile.spriteDirection = Math.Sign(targetPosition.X - Projectile.Center.X);
				WeaponRotation = CSPlayerV104Effects.AngleToSafe(Projectile, targetPosition);
				WeaponPosition = trueWeaponRotation.ToRotationVector2() * 24f;
				SoundEngine.PlaySound(SoundID.Item103, Projectile.Center);   // 1.3 无音量修饰 ⇒ 1:1
				if (Projectile.owner == Main.myPlayer)
				{
					Projectile orb = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(),
						Projectile.Center + trueWeaponPosition,
						CSPlayerV104Effects.DirectionToSafe(Projectile, targetPosition) * 16f,
						ModContent.ProjectileType<XerocOrbV104>(), (int)(125f * player.MagicDamage()), 0f, Projectile.owner);
					orb.timeLeft = 120;
					orb.extraUpdates = 1;
					orb.tileCollide = false;
				}
			}
		}
		public void SummonAttack()
		{
			ai1 = 0;
			WeaponScale = 1f;
			HasWeapon = true;
			Projectile.velocity = Vector2.Lerp(Projectile.velocity,
				Projectile.DirectionTo(targetPosition + new Vector2(Main.rand.Next(-10, 11) * 40f, -250f)) * 30f, 0.04f);
			Projectile.spriteDirection = Math.Sign(targetPosition.X - Projectile.Center.X);
			float basRot = MathHelper.ToRadians(-17.92f);
			if (AttackTimer > GetAttackTime - 20)
			{
				float radius = (GetAttackTime - AttackTimer) / 20f;
				WeaponRotation = basRot + Utils.AngleLerp(MathHelper.ToRadians(-15), 0, radius);
				WeaponPosition = Vector2.Lerp(-Vector2.UnitX * 16, new Vector2(-4, 12), radius);
			}
			else if (AttackTimer > GetAttackTime - 27)
			{
				float radius = (GetAttackTime - 20 - AttackTimer) / 7f;
				WeaponPosition = new Vector2(-4, MathHelper.Lerp(12, -36, radius));
			}
			else if (AttackTimer > GetAttackTime - 35)
			{
				WeaponPosition = new Vector2(-4, -36);
				if (AttackTimer == GetAttackTime - 27)
				{
					Vector2 LanternPos = Projectile.Center + trueWeaponPosition - trueWeaponRotation.ToRotationVector2().RotatedBy(MathHelper.PiOver2) * 28f * WeaponScale;
					if (Projectile.owner == Main.myPlayer)
					{
						Projectile blast = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), targetPosition, Vector2.Zero,
							ModContent.ProjectileType<XerocBlastV104>(), (int)(1500f * player.MinionDamage()), 0f, Projectile.owner);
						blast.timeLeft = 30;
						blast.penetrate = 1;
						blast.usesLocalNPCImmunity = true;
						blast.localNPCHitCooldown = -1;
					}
					for (int i = -20; i < 21; i++)
					{
						Vector2 dir = WeaponRotation.ToRotationVector2();
						Dust dust = Dust.NewDustPerfect(LanternPos + 4 * i * dir, DustID.LifeDrain);
						dust.noGravity = true;
						dust.scale = 2.5f - Math.Abs(i) / 10f;
						dust.velocity = -Math.Sign(i) * dir * 1f;
						dir = WeaponRotation.ToRotationVector2().RotatedBy(MathHelper.PiOver2);
						dust = Dust.NewDustPerfect(LanternPos + 4 * i * dir, DustID.LifeDrain);
						dust.noGravity = true;
						dust.scale = 2.5f - Math.Abs(i) / 10f;
						dust.velocity = -Math.Sign(i) * dir * 1f;
					}
					for (int i = 0; i < 300; i++)
					{
						if (i * 10 > Vector2.Distance(LanternPos, targetPosition)) break;
						Vector2 dir = (targetPosition - LanternPos).SafeNormalize(Vector2.UnitY);   // 1.3: LanternPos.DirectionToSafe(targetPosition)
						Dust dust = Dust.NewDustPerfect(LanternPos + dir * i * 10f, DustID.LifeDrain);
						dust.noGravity = true;
						dust.scale = 1f;
						dust.velocity = dir * 2f;
					}
					// 1.3: Main.PlaySound(Cal.GetLegacySoundSlot(...ProvidenceHolyRay), LanternPos).Volume /= 3f
					//     灾厄音 ⇒ 音量修饰按（甲）省略（差异表已记）；资源已核实 2.x 存在 ✓
					SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyRay"), LanternPos);
				}
			}
			else if (AttackTimer > GetAttackTime - 50)
			{
				float radius = (GetAttackTime - 35 - AttackTimer) / 15f;
				WeaponRotation = basRot + Utils.AngleLerp(0, MathHelper.ToRadians(-15), radius);
				WeaponPosition = Vector2.Lerp(new Vector2(-4, -36), -Vector2.UnitX * 16, radius);
			}
		}
		public void RogueAttack()
        {
            HasWeapon = true;
            Vector2 movePos = -new Vector2(80 * Projectile.spriteDirection, 64);
            bool shouldUpdateSpriteDirection = true;
            float baseRot = MathHelper.PiOver4;
            float baseRot2 = MathHelper.PiOver4 * Projectile.spriteDirection;
            float dist = Projectile.Distance(targetPosition);
            WeaponScale = 1.4f;
            if (AttackTimer > GetAttackTime - 30)
            {
                ai1 = 0;
                float radius = (GetAttackTime - AttackTimer) / 30f;
                if (AttackTimer == GetAttackTime)
                {
                    WeaponRotation = baseRot + MathHelper.ToRadians(10);
                    WeaponPosition = (WeaponRotation - baseRot).ToRotationVector2() * 4f;
                }
                else
                {
                    WeaponRotation += MathHelper.ToRadians(24);
                    WeaponPosition = (WeaponRotation - baseRot).ToRotationVector2() * MathHelper.Clamp(MathHelper.Lerp(4, 100, radius), 4, 36);
                    if (Projectile.owner == Main.myPlayer && AttackTimer % 2 == 0)
                    {
                        Projectile bubble = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(),
                           Projectile.Center + trueWeaponPosition,
                           trueWeaponRotation.ToRotationVector2() * 2,
                           ModContent.ProjectileType<XerocBubbleV104>(), (int)(30f * player.RogueDamage()), 0f, Projectile.owner);
                        bubble.tileCollide = false;
                        bubble.timeLeft = 60;
                        bubble.extraUpdates = bubble.timeLeft - 1;
                        bubble.penetrate = -1;
                        bubble.usesLocalNPCImmunity = true;
                        bubble.localNPCHitCooldown = -1;
                    }
                    if (AttackTimer % 9 == 0) SoundEngine.PlaySound(SoundID.Item15, Projectile.Center + trueWeaponPosition);
                    // 1.3: inventory[selectedItem].Calamity().rogue ⇒ 2.x 等价 = HeldItem 的 DamageType 是盗贼
                    if (dist < 120 && AttackTimer == GetAttackTime - 29
                        && player.V104().closetNPCindex != -1 && Main.npc[player.V104().closetNPCindex].Distance(Projectile.Center) < 1800f
                        && (!HoldingAClassedWeaponV104(player) || player.HeldItem.DamageType == ModContent.GetInstance<RogueDamageClass>())) AttackTimer = GetAttackTime;
                    if (dist > 800) AttackTimer = GetAttackTime - 30;
                    shouldUpdateSpriteDirection = false;
                }
                movePos = Vector2.Zero;
            }
            else if (AttackTimer > GetAttackTime - 45)
            {
                float radius = (GetAttackTime - 30 - AttackTimer) / 15f;
                WeaponRotation += MathHelper.ToRadians((float)Math.Pow(1 - radius, 2f) * 24f);
                WeaponPosition = (WeaponRotation - baseRot).ToRotationVector2() * 36f;
            }
            else if (AttackTimer > 25)
            {
                float radius = 1 - (AttackTimer - 25) / (GetAttackTime - 45 - 25f);
                WeaponRotation = Utils.AngleLerp(WeaponRotation, baseRot + MathHelper.ToRadians(45), radius);
                WeaponPosition = Vector2.Lerp(WeaponPosition, new Vector2(-8, -8), 0.1f);
            }
            else if (AttackTimer > 20)
            {
                ai1 = 2;
                float radius = 1 - (AttackTimer - 20) / 4f;
                WeaponRotation = baseRot2 + CSPlayerV104Effects.AngleToSafe(Projectile, targetPosition) + (Projectile.spriteDirection == -1 ? MathHelper.Pi : 0);
                WeaponPosition = (WeaponRotation - baseRot2).ToRotationVector2() * Projectile.spriteDirection * MathHelper.Lerp(-8f, 160f, radius);
                SoundEngine.PlaySound(SoundID.Item71, Projectile.Center + WeaponPosition);
                shouldUpdateSpriteDirection = false;
            }
            else if (AttackTimer > 15)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    float angle = CSPlayerV104Effects.AngleToSafe(Projectile, targetPosition);
                    Projectile bubble = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(),
                       Projectile.Center + trueWeaponPosition,
                       angle.ToRotationVector2() * (AttackTimer - 10f) * 2f,
                       ModContent.ProjectileType<XerocBubbleV104>(), (int)(100f * player.RogueDamage()), 0f, Projectile.owner);
                    bubble.tileCollide = false;
                    bubble.timeLeft = 70;
                    bubble.extraUpdates = bubble.timeLeft - 1;
                }
                shouldUpdateSpriteDirection = false;
            }
            else
            {
                // 1.3 原式（运算符优先级下三元条件恒取 else 分支 —— 忠实保留并注明）：
                // float angle = baseRot + spriteDirection == -1 ? Pi - ToRadians(10f) : ToRadians(10f);
                float angle = ((baseRot + Projectile.spriteDirection) == -1f) ? MathHelper.Pi - MathHelper.ToRadians(10f) : MathHelper.ToRadians(10f);
                Vector2 wPos = (angle - baseRot2).ToRotationVector2() * Projectile.spriteDirection * 4f;
                WeaponRotation = Utils.AngleLerp(WeaponRotation, angle, 0.05f);
                WeaponPosition = Vector2.Lerp(WeaponPosition, wPos, 0.1f);
                if (AttackTimer == 1 && dist > 600
                        && player.V104().closetNPCindex != -1 && Main.npc[player.V104().closetNPCindex].Distance(Projectile.Center) < 1800f
                        && (!HoldingAClassedWeaponV104(player) || player.HeldItem.DamageType == ModContent.GetInstance<RogueDamageClass>())) AttackTimer = 25;
            }
            if (shouldUpdateSpriteDirection)
                Projectile.spriteDirection = Math.Sign(targetPosition.X - Projectile.Center.X);
            Projectile.velocity = Vector2.Lerp(Projectile.velocity,
                CSPlayerV104Effects.DirectionToSafe(Projectile, targetPosition + movePos) * 30f, 0.06f);
            if (Projectile.Distance(targetPosition + movePos) < 8f) Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Zero, 0.1f);
        }
		public void ClearWeapon()
        {
            SpawnDust(Projectile.Center + trueWeaponPosition);
            HasWeapon = false;
            WeaponRotation = 0;
            WeaponPosition = Vector2.Zero;
            WeaponScale = 1f;
        }
        public void ClearWeaponNoDust()
        {
            HasWeapon = false;
            WeaponRotation = 0;
            WeaponPosition = Vector2.Zero;
            WeaponScale = 1f;
        }
        // 🔍🔍 [临时诊断 2026-10-11] 定位「附近没敌人、站着不动也冒紫色粉尘」的调用来源。
        //    用 CallerMemberName 自动记录调用者，调用点无需改动。定位后整段删除。
        //    日志形如：[幻影粉尘] caller=BackToPlayer ai1=-1 HasWeapon=False AttackTimer=0 dist=2.3 target=-1
        private void SpawnDust(Vector2 Center, float Speed = 6f, float scale = 2f,
            [System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {
            if (Main.GameUpdateCount % 60 == 0)
            {
                Terraria.ModLoader.ModLoader.GetMod("CalamitySouls").Logger.Info(
                    $"[幻影粉尘] caller={caller} ai1={ai1} ai0={ai0} HasWeapon={HasWeapon} " +
                    $"AttackTimer={AttackTimer} HasClass={HasClass} dist={Projectile.Distance(player.Center):F1} " +
                    $"target={FindTarget()} posY={Center.Y:F0} playerY={player.Center.Y:F0}");
            }
            for (int i = 0; i < 18; i++)
            {
                Dust dust = Dust.NewDustPerfect(Center, 62);
                dust.noGravity = true;
                dust.noLight = false;
                dust.scale = scale;
                dust.velocity = Main.rand.NextVector2Unit() * Speed / 2;
            }
        }
	}
}
