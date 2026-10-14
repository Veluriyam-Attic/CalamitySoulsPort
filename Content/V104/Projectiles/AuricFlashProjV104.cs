using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Systems.Collections;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「金源魔石（EnchAuric）」的金源冲击波 —— 对应新版 1.3 CalamitySouls.Projectiles.AuricFlashProj（1:1）
	// 2.x 差异只有两处：① 豁免集合改用 CalamityProjectileSets.ShouldNotBeReflected（1.3 的
	// CalamityLists.projectileDestroyExceptionList 在 2.x 已移除）② 音效走 SoundEngine 默认变体。
	public class AuricFlashProjV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "金源冲击");
		}
		public override string Texture => CSUtils.BlankTexture;
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
			Projectile.soundDelay = 2;
		}
		public float RangeCounter
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public float Range
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		public const float HitBoxRange = 16f;
		public override void AI()
		{
			if (Projectile.soundDelay == 1)
			{
				RangeCounter = 1f;
				Range = 64f;
			}
			if (RangeCounter < HitBoxRange) RangeCounter *= 1.07f;
			if (Range < 640f)
			{
				Projectile.timeLeft = 60;
				Range += RangeCounter;
			}
			else Range += 2f;
			Projectile.localAI[0]++;
			if (Projectile.localAI[0] % 3 == 0)
			{
				int rand = Main.rand.Next(360);
				int dAmt = (int)(360 * Range / 640);
				for (int i = 0; i < dAmt; i++)
				{
					if (Main.rand.Next(8) == 0) continue;
					Dust dust = Dust.NewDustPerfect(Projectile.position + MathHelper.ToRadians(i * 360f / dAmt + rand).ToRotationVector2() * Range, DustID.GoldFlame);
					dust.noGravity = true;
					dust.scale = 1.6f;
					dust.velocity = Vector2.Zero;
				}
			}
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active && proj.hostile && !proj.friendly
					&& !CalamityProjectileSets.ShouldNotBeReflected[proj.type]
					&& proj.Hitbox.Distance(Projectile.position) < Range)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), proj.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0f, 255);
					proj.Kill();
				}
			}
		}
		public override void OnKill(int timeLeft)
		{
			// 1.3: Main.PlaySound(SoundID.Zombie, projectile.Center, 103) —— legacy style 103
			// 1.4 的 SoundID 已无 Zombie 成员 → 改用已验证可用的 SoundID.Item14（差异表已记录）
			SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
		}
	}
}
