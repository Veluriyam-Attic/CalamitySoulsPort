using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「瘟疫使者魔石（EnchPlaguebringer）」的追踪瘟疫核弹 —— 对应新版 1.3 CalamitySouls.Projectiles.PlaguebringerNuke（1:1）
	// 2.x 差异：① 音效走 SoundEngine + CalamityMod 自有资源（已核实 2.x tmod 内含 PlagueSounds/*）
	//          ② PreDraw 改用 1.4 的 ref Color 签名
	public class PlaguebringerNukeV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 64;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 180;
		}
		public override bool? CanDamage() => Projectile.active;
		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation();
			int index = Projectile.ClosetNPC(1500f);
			if (index == -1)
			{
				Projectile.velocity *= 0.95f;
				Projectile.ai[0] = Projectile.velocity.Length();
			}
			else
			{
				Projectile.ai[0] += 0.5f;
				float angleLerpAmt = MathHelper.Clamp((Projectile.ai[0] - 40f) / 10f, 0f, 0.95f) + 0.05f;
				Projectile.velocity = Utils.AngleLerp(
					Projectile.velocity.ToRotation(),
					CSPlayerV104Effects.DirectionToSafe(Projectile, Main.npc[index].Center).ToRotation(),
					angleLerpAmt).ToRotationVector2() * Projectile.ai[0];
				Projectile.timeLeft++;
				if ((int)(Projectile.ai[0] * 2f) % 3 == 0) SoundEngine.PlaySound(SoundID.Item24, Projectile.Center);
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
			Vector2 origin = texture.Size() / 2f;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
			float scale = Projectile.scale * 0.8f;
			for (int i = 0; i < 6; i++)
			{
				scale *= 0.9f;
				Vector2 pos = Projectile.oldPos[i] + new Vector2(32) - Main.screenPosition;
				spriteBatch.Draw(texture, pos, null, Color.Lime, Projectile.oldRot[i], origin, scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PlagueBoom" + (Main.rand.Next(4) + 1).ToString()));
			for (int i = 0; i < 100; i++)
			{
				Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemEmerald);
				dust.noGravity = true;
				dust.velocity = CSPlayerV104Effects.ToDegreesVector2(i * 3.6f) * 30f;
				float rot = dust.velocity.ToRotation();
				float d60 = MathHelper.ToRadians(60);
				rot = CSPlayerV104Effects.Wrap(rot, 0f, 2 * d60);
				if (rot > d60)
				{
					dust.velocity = dust.velocity.RotatedBy(d60) / 3f;
				}
				dust.scale *= 4f;
			}
			for (int x = 0; x < 6; x++)
			{
				for (int y = 1; y <= 20; y++)
				{
					Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemEmerald);
					dust.noGravity = true;
					dust.velocity = CSPlayerV104Effects.ToDegreesVector2(x * 60) * (y + 10f);
					dust.scale *= 4f;
				}
			}
			foreach (NPC npc in Main.npc)
			{
				if (npc.active && !npc.dontTakeDamage && !npc.friendly && npc.Hitbox.Distance(Projectile.Center) < 320f)
				{
					int amt = 1250;
					if (npc.realLife >= 0) amt = 250;
					if (Main.player[Projectile.owner].V104().TechnicalEffect) amt *= 10;
					amt = (int)(amt * (1 - npc.Calamity().DR));
					if (amt < 1) amt = 1;
					npc.StrikeNPCExtra(amt, new Color(128, 188, 67), Main.player[Projectile.owner]);
					npc.ForceThrowABuffOnYourFuckingFace(ModContent.BuffType<Plague>(), 60);
				}
			}
			if (Projectile.owner == Main.myPlayer)
				foreach (Player player in Main.player)
					if (player.active && !player.dead && !player.immune && player.Hitbox.Distance(Projectile.Center) < 320f)
						player.AddBuff(ModContent.BuffType<Plague>(), 60);
		}
	}
}
