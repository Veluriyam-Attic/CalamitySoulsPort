using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria;
using Terraria.ModLoader;
using CalamityMod;
using Terraria.ID;
using CalamityMod.Dusts;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Projectiles
{
	public class TitanBooster : ModProjectile
	{
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("TitanBooster.DisplayName", "泰坦推进器");
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Projectile.width = 36;
			Projectile.height = 20;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 600;
		}
		public override void AI()
		{
			if (!Main.player[Projectile.owner].CS().StarlightBooster)
			{
				Projectile.Kill();
			}
			if (Utils.NextBool(Main.rand, 4))
			{
				int numDust = 200;
				int rand = Main.rand.Next(360);
				for (int i = 0; i < numDust; i++)
				{
					Vector2 dir = MathHelper.ToRadians(i * 360 / numDust + rand).ToRotationVector2() * 180f;
					bool flag = MathHelper.WrapAngle(dir.ToRotation() + MathHelper.PiOver2) > 0;
					Dust dust = Dust.NewDustPerfect(Projectile.Center + dir, flag ? 89 : 90);
					dust.noGravity = true;
					dust.fadeIn = 1f;
					dust.velocity *= 0f;
					dust.scale = 1.2f;
				}
			}
			for (int j = 0; j < Main.projectile.Length; j++)
			{
				Projectile proj = Main.projectile[j];
				if (proj.owner == Projectile.owner && proj.localAI[1] == 0f && proj.friendly && proj.Distance(Projectile.Center) < 180f)
				{
					proj.damage = (int)(proj.damage * 1.15f);
					proj.localAI[1] = 1f;
				}
			}
			if (ShieldDelay != 0) return;
			Projectile.frameCounter++;
			if (Projectile.frameCounter % 8 == 0)
			{
				ShieldFrame++;
			}
			if (ShieldFrame > 11) ShieldFrame = 0;
			if (Projectile.soundDelay > 0) SoundEngine.PlaySound(SoundID.Item15, Projectile.Center);
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile bullet = Main.projectile[i];
				if (bullet.active && bullet.hostile && !bullet.friendly && bullet.tileCollide
					&& bullet.Hitbox.Distance(TruePosition) < 50f)
				{
					bullet.Kill();
					for (int j = -10; j < 11; j++)
					{
						Vector2 dir = Projectile.rotation.ToRotationVector2().RotatedBy(MathHelper.ToRadians(j * 3));
						Dust dust = Dust.NewDustPerfect(Projectile.Center + dir * 59f, DustID.Electric);
						dust.noLight = false;
						dust.scale *= 3f;
						dust.velocity.Length(1f);
					}
					SoundEngine.PlaySound(SoundID.NPCDeath44, Projectile.Center);
					ShieldDelay = 5 * 60;
					return;
				}
			}
		}
		private int ShieldFrame
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		private int ShieldDelay
		{
			get => Projectile.soundDelay;
			set => Projectile.soundDelay = value;
		}
		public override void PostDraw(Color lightColor)
		{
			if (ShieldDelay != 0) return;
			Texture2D texture = TextureAssets.Projectile[ProjectileType<WulfrumShield>()].Value;
			Rectangle sRect = new Rectangle(0, 120 * ShieldFrame, 118, 118);
			Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, sRect, Color.White, 0f, new Vector2(59, 59), 1f, SpriteEffects.None, 0f);
		}
		private Vector2 TruePosition => Projectile.Center + MathHelper.ToRadians(ShieldFrame * 30f).ToRotationVector2() * 36f;
	}
}