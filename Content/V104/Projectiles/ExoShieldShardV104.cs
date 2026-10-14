using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「星流魔石（EnchExo）」护盾破碎时飞出的 8 片碎片 —— 对应新版 1.3 的 CalamitySouls.Projectiles.ExoShieldShard
	public class ExoShieldShardV104 : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.timeLeft = 180;
		}
		public override void AI()
		{
			Projectile.velocity.Y += 0.2f;
			Projectile.velocity *= 0.98f;
			float rSpeed = Math.Abs(Projectile.velocity.X) / 40f;
			if (rSpeed > 0.4f) rSpeed = 0.4f;
			Projectile.rotation += rSpeed * Math.Sign(Projectile.velocity.X);
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X * 0.7f;
			if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y * 0.4f;
			if (Projectile.timeLeft > 60) Projectile.timeLeft -= 20;
			return false;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Projectiles/ExoShieldShardV104", AssetRequestMode.ImmediateLoad).Value;
			Color color = new Color(255, 255, 255, 100);
			if (Projectile.ai[0] == 0f) color = new Color(194, 255, 245, 100);
			else if (Projectile.ai[0] == 1f) color = new Color(141, 194, 126, 100);
			else if (Projectile.ai[0] == 2f) color = new Color(211, 235, 108, 100);
			else if (Projectile.ai[0] == 3f) color = new Color(242, 182, 41, 100);
			else if (Projectile.ai[0] == 4f) color = new Color(250, 255, 112, 100);
			else if (Projectile.ai[0] == 5f) color = new Color(242, 112, 73, 100);
			else if (Projectile.ai[0] == 6f) color = new Color(237, 192, 168, 100);
			else if (Projectile.ai[0] == 7f) color = new Color(255, 255, 199, 100);
			if (Projectile.timeLeft < 60) color *= Projectile.timeLeft / 60f;
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, color, Projectile.rotation,
				texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
			return false;
		}
	}
}
