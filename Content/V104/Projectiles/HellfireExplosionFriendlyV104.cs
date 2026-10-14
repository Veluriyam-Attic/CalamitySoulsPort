using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Buffs.DamageOverTime;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 灾厄 1.3 的 HellfireExplosionFriendly（CalamityMod.Projectiles.Magic）—— 2.x 已移除，故在 V104 自建等价实现。
	// 来源：ilspycmd 反编译 D:\ai\port\_tm_verify_CalamityMod.XNA.dll（1.3 CalamityMod v1.4.5.7），逐行照搬。
	// 2.x 差异仅两处：① 贴图改用本模组自带全透明图（1.3 原文是全透明的 Calamity InvisibleProj，视觉一致）
	//                ② projectile.magic = true → 1.4 的 Projectile.DamageType = DamageClass.Magic
	public class HellfireExplosionFriendlyV104 : ModProjectile
	{
		public override string Texture => CSUtils.BlankTexture;

		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "爆炸");
		}

		public override void SetDefaults()
		{
			Projectile.width = 130;
			Projectile.height = 130;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			Projectile.DamageType = DamageClass.Magic;   // 1.3: projectile.magic = true
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, 0.9f, 0f, 0f);
			if (Projectile.localAI[0] == 0f)
			{
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
				Projectile.localAI[0] += 1f;
			}
			float num = 25f;
			if (Projectile.ai[0] > 180f)
			{
				num -= (Projectile.ai[0] - 180f) / 2f;
			}
			if (num <= 0f)
			{
				num = 0f;
				Projectile.Kill();
			}
			num *= 0.7f;
			Projectile.ai[0] += 4f;
			for (int i = 0; (float)i < num; i++)
			{
				float num2 = Main.rand.Next(-10, 11);
				float num3 = Main.rand.Next(-10, 11);
				float num4 = Main.rand.Next(3, 9);
				float num5 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
				num5 = num4 / num5;
				num2 *= num5;
				num3 *= num5;
				Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 235, 0f, 0f, 100, default, 1.5f);
				dust.noGravity = true;
				dust.position.X = Projectile.Center.X;
				dust.position.Y = Projectile.Center.Y;
				dust.position.X += Main.rand.Next(-10, 11);
				dust.position.Y += Main.rand.Next(-10, 11);
				dust.velocity.X = num2;
				dust.velocity.Y = num3;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 600);
		}
	}
}
