using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using CalamityMod.Projectiles;
using Terraria.ID;

namespace CalamitySouls.Projectiles
{
	public class HeavenshadeStar : ModProjectile
	{
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("HeavenshadeStar.DisplayName", "天影之星");
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 0;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
		}
		public override void AI()
		{
			Projectile.rotation += 0.25f;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 10f)
			{
				Projectile.penetrate = 1;
				for (int i = 0; i < 1; i++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 155);
					dust.noGravity = true;
					dust.velocity = Vector2.Zero;
					dust.alpha = 100;
					dust.scale = 1.2f;
				}
			}
			int index = Projectile.ClosetNPC(400f);
			if (index == -1) return;
			NPC target = Main.npc[index];
			Vector2 dir = Projectile.DirectionTo(target.Center);
			if (dir.HasNaNs()) dir = Vector2.Zero;
			Projectile.velocity = Vector2.Lerp(Projectile.velocity, dir * 10f, 0.04f);
		}
        public override bool? CanDamage() => Projectile.ai[0] > 10f;
        public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor),
				Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}