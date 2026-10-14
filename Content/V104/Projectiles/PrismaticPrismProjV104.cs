using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「光棱魔石（EnchPrismatic）」的环绕棱镜 —— 对应新版 1.3 CalamitySouls.Projectiles.PrismaticPrismProj（逐值 1:1）
	// 2.x 差异（全部已记差异表）：
	//  ① SetupProjectileTrailling(-1, 8) 在 2.x 已移除 → 用 1.4 的 TrailingMode = 2 + TrailCacheLength = 8（长度逐值 1:1）
	//  ② CirculateOldpos() 在 2.x 已移除 → 1.4 设置 TrailingMode 后由引擎自动滚动 oldPos/oldRot，故省略（等价）
	//  ③ DustID.AmethystBolt / EmeraldBolt → 1.4 的 GemAmethyst / GemEmerald（同族）
	//  ④ Main.PlaySound(...).Volume /= 2 → SoundID.Item56 with { Volume = 0.5f }（逐值等价）
	public class PrismaticPrismProjV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 8;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D texture = TextureAssets.Projectile[Type].Value;
			Vector2 origin = texture.Size() / 2f;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
			float scale = Projectile.scale;
			for (int i = 0; i < Projectile.oldPos.Length; i++)
			{
				Vector2 pos = Projectile.oldPos[i] - Main.screenPosition;
				spriteBatch.Draw(texture, pos, null, Color.Lerp(Color.HotPink, Color.LimeGreen, (float)i / Projectile.oldPos.Length) * 0.5f,
					Projectile.oldRot[i], origin, scale, SpriteEffects.None, 0f);
				scale *= 0.9f;
			}
			spriteBatch.End();
			spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 26;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 900;
			Projectile.extraUpdates = 1;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (!player.active || player.dead || !player.V104().PrismaticPrism)
			{
				Projectile.Kill();
				return;
			}
			Projectile.rotation += Main.rand.Next(3) / 10f;
			Projectile.timeLeft = 2;
			float minDist = 1000f;
			int index = -1;
			if (Projectile.soundDelay <= 0)
				foreach (Projectile hosProj in Main.projectile)
				{
					if (hosProj.active && hosProj.hostile && !hosProj.friendly && hosProj.damage > 0
						&& Math.Cos(hosProj.velocity.ToRotation() - CSPlayerV104Effects.AngleToSafe(hosProj, player.Center)) >= 0)
					{
						float dist = hosProj.Hitbox.Distance(player.Center);
						if (dist < minDist)
						{
							index = hosProj.whoAmI;
							minDist = dist;
						}
					}
				}
			Vector2 tarPos = player.Center + CSPlayerV104Effects.DirectionFromSafe(Projectile, player.Center).RotatedBy(0.5f) * 180f;
			if (index != -1) tarPos = Main.projectile[index].Center;
			float dir = Utils.AngleLerp(Projectile.velocity.ToRotation(), CSPlayerV104Effects.AngleToSafe(Projectile, tarPos), 0.15f);
			Projectile.velocity = dir.ToRotationVector2() * 15f;
			if (!Projectile.FinalExtraUpdate()) return;
			if (index != -1)
			{
				Projectile hosP = Main.projectile[index];
				if (hosP.Hitbox.Distance(Projectile.Center) < 40f)
				{
					hosP.velocity = CSPlayerV104Effects.DirectionFromSafe(hosP, player.Center) * hosP.velocity.Length() * 1.2f;
					Projectile.velocity = CSPlayerV104Effects.DirectionToSafe(Projectile, player.Center) * 25f;
					SoundEngine.PlaySound(SoundID.Item56 with { Volume = 0.5f }, player.Center);
					Projectile.soundDelay += player.V104().TechnicalEffect ? 20 : 60;
					for (int i = 0; i < 6; i++)
					{
						Dust dust1 = Dust.NewDustPerfect(Projectile.Center + CSPlayerV104Effects.DirectionToSafe(Projectile, hosP.Center) * 20f, DustID.GemAmethyst);
						dust1.noGravity = true;
						dust1.scale *= Main.rand.NextFloat() + 1f;
						dust1 = Dust.NewDustPerfect(Projectile.Center + CSPlayerV104Effects.DirectionToSafe(Projectile, hosP.Center) * 20f, DustID.GemEmerald);
						dust1.noGravity = true;
						dust1.scale *= Main.rand.NextFloat() + 1f;
					}
				}
			}
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmethyst);
			dust.velocity = Projectile.velocity / 10f;
			dust.scale *= 0.8f;
			dust.noGravity = true;
			dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald);
			dust.velocity = Projectile.velocity / 10f;
			dust.scale *= 0.8f;
			dust.noGravity = true;
		}
		public override bool? CanDamage() => false;
	}
}
