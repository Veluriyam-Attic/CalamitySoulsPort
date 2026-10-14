using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「星光魔石（EnchLuxAstri）」的白金之拳 —— 对应新版 1.3 的 CalamitySouls.Projectiles.LuxAstriPunch
	public class LuxAstriPunchV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "白金之拳（新版）");
			Main.projFrames[Type] = 4;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 44;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.Opacity = 0f;
			Projectile.friendly = true;
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 0;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 20;
		}
		public override void AI()
		{
			Projectile.frameCounter++;
			Projectile.frame = Projectile.frameCounter / 5 % Main.projFrames[Projectile.type];
			// 1.4 的 Utils 没有 InverseLerp，等价物是 GetLerpValue
			Projectile.Opacity = Utils.GetLerpValue(0f, 12f, Projectile.timeLeft, true) * Utils.GetLerpValue(1200f, 1188f, Projectile.timeLeft, true);
			Projectile.rotation = Projectile.velocity.ToRotation();
			Projectile.spriteDirection = Utils.ToDirectionInt(Projectile.velocity.X > 0);
			if (Projectile.ai[1] == 0f) Lighting.AddLight(Projectile.Center, 0f, 0.5f * Projectile.Opacity, 0.5f * Projectile.Opacity);
			else Lighting.AddLight(Projectile.Center, 0.5f * Projectile.Opacity, 0.25f * Projectile.Opacity, 0f);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.ai[1] == 0f)
			{
				lightColor.G = (byte)(255f * Projectile.Opacity);
				lightColor.B = (byte)(255f * Projectile.Opacity);
			}
			else
			{
				lightColor.R = (byte)(255f * Projectile.Opacity);
				lightColor.G = (byte)(255f / 2f * Projectile.Opacity);
			}
			SpriteEffects direction = (Projectile.spriteDirection == -1) ? SpriteEffects.FlipVertically : SpriteEffects.None;
			Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Projectiles/LuxAstriPunchV104", AssetRequestMode.ImmediateLoad).Value;
			if (Projectile.ai[1] == 1f)
			{
				texture = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Projectiles/LuxAstriPunchAltV104", AssetRequestMode.ImmediateLoad).Value;
			}
			Vector2 drawPosition = Projectile.Center - Main.screenPosition + Vector2.UnitY * Projectile.gfxOffY;
			drawPosition.X -= Math.Sign(Projectile.velocity.X) * 40f;
			Rectangle frame = Utils.Frame(texture, 1, Main.projFrames[Projectile.type], 0, Projectile.frame);
			Main.EntitySpriteDraw(texture, drawPosition, frame, Projectile.GetAlpha(lightColor), Projectile.rotation,
				Utils.Size(frame) / 2f, Projectile.scale, direction, 0);
			return false;
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			// 1.3: damage += target.defense * 0.5f —— 1.4 用 FlatBonusDamage 表达同样的加算
			modifiers.FlatBonusDamage += target.defense * 0.5f;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
			SoundStyle sid = SoundID.NPCHit15;
			if (Main.rand.Next(3) == 0) sid = SoundID.NPCHit16;
			else if (Main.rand.Next(2) == 0) sid = SoundID.NPCHit17;
			SoundEngine.PlaySound(sid, Projectile.Center);
		}
		// 1.4：OnHitPvp 已被 OnHitPlayer 取代，玩家受伤信息类型是 Player.HurtInfo
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
			SoundStyle sid = SoundID.NPCHit15;
			if (Main.rand.Next(3) == 0) sid = SoundID.NPCHit16;
			else if (Main.rand.Next(2) == 0) sid = SoundID.NPCHit17;
			SoundEngine.PlaySound(sid, Projectile.Center);
		}
	}
}
