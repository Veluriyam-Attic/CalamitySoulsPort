using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 「新版掠夺者魔石（EnchReaver）」近战吸血用的隐形治疗球。
	//
	// 来源：1.3 灾厄 CalamityMod.Projectiles.Healing.TransfusionTrail（已反编译核对）。
	// 说明：新版 v1.0.4.0 源码里写的是 ProjectileType<ReaverHealOrb>()，但该类型在 1.3 灾厄的
	// 18 个治疗球家族里并不存在（两版 CalamitySouls 源码里也只有引用、没有定义），
	// 而 TransfusionTrail 的 ai[0]/ai[1] 语义与新版调用点完全对得上，故以它为等价实现。
	//
	// ⚠️ TransfusionTrail 在 1.3 里把追踪与治疗委托给灾厄的 `HealingProjectile` 扩展；
	//    该扩展在 2.x 仍存在（CalamityMod.Projectiles.CommonProjectileAI.HealingProjectile），
	//    但**是 internal，跨模组无法调用**（编译器 CS0122）。因此这里把它的**完整实现逐行内联**，
	//    行为与灾厄原实现一致（见报告差异表）。
	public class ReaverHealOrbV104 : ModProjectile
	{
		// 1.3 原文是 CalamityMod/Projectiles/InvisibleProj（全透明）。
		// 为避免"跨模组引用灾厄贴图路径"（灾厄若改路径会在加载期直接报找不到贴图），
		// 改用本模组自带的全透明图 CSUtils.BlankTexture —— 视觉完全一致，仅来源不同（见报告差异表）。
		public override string Texture => CSUtils.BlankTexture;

		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "治疗");
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 10;
		}

		public override void AI()
		{
			// ---- 以下 = 灾厄 HealingProjectile(healing, playerToHeal, homingVelocity, inertia) 的完整实现 ----
			int playerToHeal = (int)Projectile.ai[0];
			int healing = (int)Projectile.ai[1];
			float homingVelocity = 6.5f;
			float inertia = 15f;

			if (playerToHeal < 0 || playerToHeal >= Main.maxPlayers) { Projectile.Kill(); return; }
			Player target = Main.player[playerToHeal];
			if (!target.active || target.dead) { Projectile.Kill(); return; }

			float speed = homingVelocity;
			if (target.lifeMagnet) speed *= 1.5f;

			Vector2 toPlayer = target.Center - Projectile.Center;
			float dist = toPlayer.Length();
			if (dist < 50f && Projectile.Hitbox.Intersects(target.Hitbox))
			{
				if (Projectile.owner == Main.myPlayer && !Main.LocalPlayer.moonLeech)
				{
					target.HealLife(healing);
					NetMessage.SendData(66, -1, -1, null, playerToHeal, healing, 0f, 0f, 0, 0, 0);
				}
				Projectile.Kill();
				return;
			}
			if (dist > 0f)
			{
				toPlayer *= speed / dist;
				Projectile.velocity.X = (Projectile.velocity.X * inertia + toPlayer.X) / (inertia + 1f);
				Projectile.velocity.Y = (Projectile.velocity.Y * inertia + toPlayer.Y) / (inertia + 1f);
			}

			// ---- 以下 = 1.3 TransfusionTrail 的拖尾（1 + 2 粒 DustID 183）----
			float dx = Projectile.velocity.X * 0.334f;
			float dy = -Projectile.velocity.Y * 0.334f;
			Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 183, 0f, 0f, 100, default, 0.7f);
			dust.noGravity = true;
			dust.position.X -= dx;
			dust.position.Y -= dy;
			for (int i = 0; i < 2; i++)
			{
				float dx2 = Projectile.velocity.X * 0.2f * i;
				float dy2 = -Projectile.velocity.Y * 0.2f * i;
				Dust d2 = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 183, 0f, 0f, 100, default, 0.9f);
				d2.noGravity = true;
				d2.position.X -= dx2;
				d2.position.Y -= dy2;
			}
		}
	}
}
