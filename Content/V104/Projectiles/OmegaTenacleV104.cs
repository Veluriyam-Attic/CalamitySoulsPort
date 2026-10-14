using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.Systems.Collections;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「欧米伽蓝魔石（EnchOmega）」的触手 —— 对应新版 1.3 CalamitySouls.Projectiles.OmegaTenacle（239 行逐行照搬）
	// 2.x 差异（已记差异表）：
	//  ① SetupProjectileTrailling(-1, 20) → TrailingMode = 2 + TrailCacheLength = 20；
	//     1.3 在 PreDraw 里又调了一次 (-1, 15)，1.4 的这两个是静态集合、运行时重设无意义 → 按等价省略
	//  ② CirculateOldpos() → 1.4 引擎自动滚动 oldPos（省略）
	//  ③ 1.3 的 4 个 CSUtils helper（AngleFromSafe / DirectionToSafe(V2,V2) / AngleToSafe(V2,V2) / WrapAngleBetweenPi）
	//     我方 V104 没有 → 以等价实现内联（见下方私有 helper）
	//  ④ 两个音效（OmegaBlueAbility / OmegaBlueRecharge）在灾厄 2.x 已移除 → 已从 1.3 灾厄包解包补入本模组 Sounds/Custom/
	//  ⑤ 豁免登记：1.3 把本弹幕登记进 CSLists.projectileDestroyExceptionList → 2.x 等价 CalamityProjectileSets.ShouldNotBeReflected
	public class OmegaTenacleV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "欧米伽之触");
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 20;
			CalamityProjectileSets.ShouldNotBeReflected[Type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 20;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.friendly = true;
			Projectile.frameCounter = 0;
			Projectile.frame = 0;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 35;
		}

		// ---- 我方 V104 缺失的 4 个 helper 的等价实现 ----
		private static Vector2 DirSafe(Vector2 from, Vector2 to) => (to - from).SafeNormalize(Vector2.UnitY);
		private static float AngleOf(Vector2 from, Vector2 to) => (to - from).ToRotation();
		private static bool HoldingAClassedWeaponV104(Player player)
		{
			Item item = player.HeldItem;
			if (item.IsAir || item.damage <= 0) return false;
			if (item.pick > 0 || item.axe > 0 || item.hammer > 0) return false;
			return item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Ranged)
				|| item.CountsAsClass(DamageClass.Magic) || item.CountsAsClass(DamageClass.Summon)
				|| item.CountsAsClass(DamageClass.Throwing)
				|| item.CountsAsClass(ModContent.GetInstance<CalamityMod.RogueDamageClass>());
		}

		public Vector2 centerPos => (Projectile.oldPos[2] + player.Center) / 2f;
		public Player player => Main.player[Projectile.owner];
		public override void AI()
		{
			if (Projectile.localAI[0] == 0)
			{
				AiTimer = MathHelper.ToDegrees(Projectile.ai[1]) / 4f;
				attackRange = 480f;
				Projectile.localAI[0] = 1;
			}
			Player player = Main.player[Projectile.owner];
			if (CheckDead(player)) return;
			else Projectile.timeLeft = 2;
			if (HoldingAClassedWeaponV104(player))
			{
				Projectile.GetPlayerDamageType(player);
				Projectile.damage = CSPlayerV104Effects.HeldDamage(player, 1000f);
			}
			int targetIndex = FindTarget(player);
			if (targetIndex == -1) WanderingAI(player);
			else AttackAI(player, Main.npc[targetIndex]);
			if (Projectile.velocity.Length() > 45f) Projectile.velocity.Length(45f);
			if (player.Distance(Projectile.Center + Projectile.velocity) > attackRange)
				Projectile.Center = player.Center + player.DirectionTo(Projectile.Center + Projectile.velocity) * attackRange;
		}
		public float AiTimer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public Vector2 TenaclePositon
		{
			get => Projectile.ai[1].ToRotationVector2();
			set => Projectile.ai[1] = value.ToRotation();
		}
		public static float attackRange = 480f;
		public bool CheckDead(Player player)
		{
			if (!player.active || player.dead || !player.V104().OmegaTouch)
			{
				Projectile.active = false;
				return true;
			}
			return false;
		}
		public int FindTarget(Player player)
		{
			int target = player.V104().closetNPCindex;
			if (target != -1 && Main.npc[target].Distance(player.Center) <= attackRange) return target;

			if (player.HasMinionAttackTargetNPC)
				if (Main.npc[player.MinionAttackTargetNPC].Distance(player.Center) < attackRange) return player.MinionAttackTargetNPC;

			if (Projectile.soundDelay >= 600) return -1;
			foreach (NPC npc in Main.npc)
				if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(player.Center) < attackRange)
					return npc.whoAmI;

			return -1;
		}
		public void WanderingAI(Player player)
		{
			if (Projectile.soundDelay >= 600)
			{
				Projectile.soundDelay++;
				Projectile.velocity *= 0.85f;
				Vector2 desPos = player.Center + TenaclePositon * 5f;
				if (Projectile.Distance(desPos) <= 30f)
				{
					Projectile.Center = desPos;
					for (int i = 0; i < 2; i++) Projectile.oldPos[i] = player.Center;
					Projectile.rotation = Projectile.ai[1];
				}
				else
				{
					Projectile.Center = Vector2.Lerp(Projectile.Center, desPos, 0.4f);
					CommonRotation(player);
				}
				return;
			}
			else if (player.itemAnimation <= 0)
			{
				Projectile.soundDelay += 2;
				if (MathHelper.ToDegrees(Projectile.ai[1]) % 360 < 45)
					if (Projectile.soundDelay >= 600)
						SoundEngine.PlaySound(new SoundStyle("CalamitySouls/Sounds/Custom/OmegaBlueRecharge"), player.Center);
			}
			else Projectile.soundDelay = 0;
			Projectile.velocity = Vector2.Lerp(Projectile.velocity,
				CSPlayerV104Effects.DirectionToSafe(Projectile,
				player.Center + TenaclePositon.RotatedByRandom(MathHelper.ToRadians(20f)) * 80f) * 30f,
				0.05f);
			CommonRotation(player);
		}
		public void AttackAI(Player player, NPC target)
		{
			if (Projectile.soundDelay != 0)
			{
				if (Projectile.soundDelay >= 600)
					if (MathHelper.ToDegrees(Projectile.ai[1]) % 360 < 45)
						if (Projectile.soundDelay >= 300)
							SoundEngine.PlaySound(new SoundStyle("CalamitySouls/Sounds/Custom/OmegaBlueAbility"), player.Center);
				Projectile.soundDelay = 0;
			}
			AiTimer = (AiTimer + 1) % 90;
			if (AiTimer < 30 && Projectile.localAI[1] == 0)
			{
				Projectile.velocity = Vector2.Lerp(Projectile.velocity,
				DirSafe(Projectile.Center, target.Center - Projectile.rotation.ToRotationVector2() * 36 * Projectile.scale) * 35f,
				0.1f);
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, CSPlayerV104Effects.AngleToSafe(Projectile, target.Center), 0.2f);
			}
			else
			{
				if (AiTimer >= 30) Projectile.localAI[1] = 0;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity,
				CSPlayerV104Effects.DirectionToSafe(Projectile, player.Center + TenaclePositon * 80f) * 30f,
				0.07f);
				CommonRotation(player);
			}
		}
		public void CommonRotation(Player player)
		{
			float desAngle = MathHelper.WrapAngle((CSPlayerV104Effects.DirectionFromSafe(Projectile, centerPos).ToRotation()
				- CSPlayerV104Effects.DirectionFromSafe(Projectile, player.Center).ToRotation()) * 3f);
			desAngle = CSPlayerV104Effects.DirectionFromSafe(Projectile, player.Center).ToRotation() + desAngle;
			if (Projectile.velocity.Length() != 0)
				Projectile.rotation = Utils.AngleLerp(Projectile.rotation, desAngle, 0.2f);
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.localAI[1] = 1;
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Vector2 truePos = Projectile.Center + (Projectile.rotation.ToRotationVector2() * 36 * Projectile.scale);
			Vector2 dir = Projectile.rotation.ToRotationVector2() * 35f;
			float point = 0;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(),
				   truePos - dir, truePos + dir, 20f, ref point);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Player player = Main.player[Projectile.owner];
			Texture2D texture = TextureAssets.Projectile[Type].Value;

			Rectangle topRect = new Rectangle(2, 2, 70, 20);
			Rectangle armRect = new Rectangle(2, 24, 64, 18);
			Rectangle bodyRect = new Rectangle(2, 44, 30, 22);
			Rectangle smallBallRect = new Rectangle(4, 68, 18, 18);
			Rectangle bigBallRect = new Rectangle(30, 66, 24, 24);
			Vector2 topBallPos = Projectile.Center - Main.screenPosition;
			Vector2 armBallPos = centerPos - Main.screenPosition;
			Vector2 bodyBallPos = player.Center + CSPlayerV104Effects.DirectionToSafe(player, centerPos) * 16f * Projectile.scale - Main.screenPosition;
			Vector2 topPos = topBallPos + Projectile.rotation.ToRotationVector2() * 36 * Projectile.scale;
			Vector2 armPos = (topBallPos + armBallPos) / 2f;
			Vector2 bodyPos = (armBallPos + bodyBallPos) / 2f;
			Vector2 bodyArmorPos = bodyBallPos + DirSafe(bodyBallPos, armBallPos) * 15f * Projectile.scale;
			Vector2 armScale = new Vector2(Vector2.Distance(topBallPos, armBallPos) / 64f, 1f) * Projectile.scale;
			Vector2 bodyScale = new Vector2(Vector2.Distance(armBallPos, bodyBallPos) / 64f, 1f) * Projectile.scale;

			SpriteEffects effects = SpriteEffects.None;
			if (MathHelper.WrapAngle(Projectile.rotation + MathHelper.PiOver2) < 0) effects = SpriteEffects.FlipVertically;

			Color color = Color.White;
			float scale = Projectile.scale;
			spriteBatch.Draw(texture, bodyPos, armRect, color, AngleOf(bodyBallPos, armBallPos), armRect.Size() / 2f, bodyScale, effects, 0f);
			spriteBatch.Draw(texture, armPos, armRect, color, AngleOf(armBallPos, topBallPos), armRect.Size() / 2f, armScale, effects, 0f);
			spriteBatch.Draw(texture, bodyBallPos, bigBallRect, color, Projectile.ai[1], bigBallRect.Size() / 2f, scale, effects, 0f);
			spriteBatch.Draw(texture, armBallPos, smallBallRect, color, AngleOf(armBallPos, topBallPos), smallBallRect.Size() / 2f, scale, effects, 0f);
			spriteBatch.Draw(texture, bodyArmorPos, bodyRect, color, AngleOf(bodyBallPos, armBallPos), bodyRect.Size() / 2f, scale, effects, 0f);
			spriteBatch.Draw(texture, topBallPos, smallBallRect, color, Projectile.rotation, smallBallRect.Size() / 2f, scale, effects, 0f);
			spriteBatch.Draw(texture, topPos, topRect, color, Projectile.rotation, topRect.Size() / 2f, scale, effects, 0f);
			return false;
		}
	}
}
