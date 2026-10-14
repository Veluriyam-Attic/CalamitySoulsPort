using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「天蓝魔石（EnchAerospec）」冲刺时每 4 帧洒出的羽毛 —— 对应新版 1.3 CalamitySouls.Projectiles.AeroFeather（1:1）
	// 1.3 用 CSUtils.GetOneOrNegativeOne；直接调用我方已有的 CSUtils.GetOneOrNegativeOne（1:1）
	public class AeroFeatherV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "羽毛");
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.BlueCrystalShard);
				dust.noGravity = true;
				dust.velocity *= 0.5f;
			}
			Projectile.velocity.Y += 0.1f;
			Projectile.velocity.Y *= 0.96f;
			Projectile.velocity.X += Projectile.ai[0];
			Projectile.rotation += Projectile.ai[0] * 0.2f;
			Projectile.velocity.X *= 0.9f;
			if (Main.rand.Next(5) == 0) Projectile.ai[0] = CSUtils.GetOneOrNegativeOne;
			Projectile.alpha += 2;
			if (Projectile.alpha >= 255) Projectile.Kill();
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.friendly = false;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) => false;
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.localAI[1] == 0f) Projectile.localAI[0] = Main.rand.Next(1, 4);
			Projectile.localAI[1] = (Projectile.localAI[1] + 1f) % 10f;
			Texture2D texture = TextureAssets.Projectile[Type].Value;
			Rectangle rectan = new Rectangle(0, (int)(Projectile.localAI[0] * 20f - 20f), 20, 20);
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, rectan, Projectile.GetAlpha(lightColor),
				Projectile.rotation, rectan.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
			return false;
		}
	}
}
