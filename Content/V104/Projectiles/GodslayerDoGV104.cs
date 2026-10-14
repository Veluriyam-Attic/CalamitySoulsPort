using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;
using CalamityMod;
using CalamityMod.Systems.Collections;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 新版「弑神者魔石（EnchGodslayer）」的冲刺神明吞噬者 —— 对应新版 1.3 CalamitySouls.Projectiles.GodslayerDoG（逐行 1:1）
	// 2.x 差异（全部已记差异表）：
	//  ① SetupProjectileTrailling(-1, 45) 2.x 已移除 → TrailingMode = 2 + TrailCacheLength = 45（长度逐值）
	//  ② CirculateOldpos() 2.x 已移除 → 1.4 设定 TrailingMode 后由引擎自动滚动（等价）
	//  ③ 1.3 CalamityLists.projectileDestroyExceptionList → 2.x 等价物 CalamityProjectileSets.ShouldNotBeReflected
	//     （已核实为 public static bool[]，可写 ✓）；本弹幕按 1.3 做法把自己登记进去（1:1）
	//  ④ 音效 Cal.GetLegacySoundSlot(...DevourerAttack) → SoundStyle("CalamityMod/Sounds/Custom/DevourerAttack")（已核实 2.x 存在）
	//  ⑤ Utils.InverseLerp → 1.4 的 Utils.GetLerpValue；Main.GlobalTime → Main.GlobalTimeWrappedHourly（该处外层有 % 30f，语义不变）
	public class GodslayerDoGV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "神明吞噬者");
			ProjectileID.Sets.TrailingMode[Type] = 2;
			ProjectileID.Sets.TrailCacheLength[Type] = 45;
			// 1.3：CSLists.projectileDestroyExceptionList 登记了本弹幕 → 2.x 等价集合
			CalamityProjectileSets.ShouldNotBeReflected[Type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 2;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 0;
			Projectile.soundDelay = 2;
			Projectile.timeLeft = 180;
		}
		public override void AI()
		{
			if (Projectile.soundDelay == 1)
				SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/DevourerAttack"), Projectile.Center);
			Player player = Main.player[Projectile.owner];
			float desAngle = CSPlayerV104Effects.AngleToSafe(Projectile, player.Center);
			if (!player.V104().GodslayerDashing)
			{
				int index = player.V104().closetNPCindex;
				if (index != -1) desAngle = CSPlayerV104Effects.AngleToSafe(Projectile, Main.npc[index].Center);
			}
			Projectile.ai[0] = (Projectile.ai[0] + 1) % 60;
			float angle = Utils.AngleLerp(Projectile.velocity.ToRotation(), desAngle, Projectile.ai[0] / 300f);
			Projectile.velocity = angle.ToRotationVector2() * 55f;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Projectile.localAI[0]++;
			if (Projectile.timeLeft < 30) Projectile.Opacity = MathHelper.Lerp(0f, 1f, Projectile.timeLeft / 30f);
			ClearProj();
		}
		public void ClearProj()
		{
			foreach (Projectile hosP in Main.projectile)
			{
				if (!hosP.active || !hosP.hostile || CalamityProjectileSets.ShouldNotBeReflected[hosP.type]) continue;
				bool flag = false;
				if (CSPlayerV104Effects.DistFromRectan(Projectile.Center, hosP.Hitbox) < 50f) flag = true;
				if (flag) hosP.active = false;
			}
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (CanDamage() != true) return false;
			if (CSPlayerV104Effects.DistFromRectan(Projectile.Center, targetHitbox) < 50f) return true;
			foreach (Vector2 oldp in Projectile.oldPos)
			{
				if (oldp != Vector2.Zero && CSPlayerV104Effects.DistFromRectan(oldp, targetHitbox) < 30f) return true;
			}
			return false;
		}
		public override bool? CanDamage() => Projectile.Opacity > 0.3f;
		public int TrailingCacheLength => Projectile.oldPos.Length;
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D head = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/DevourerofGodsHeadV104", AssetRequestMode.ImmediateLoad).Value;
			Texture2D body = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/DevourerofGodsBodyV104", AssetRequestMode.ImmediateLoad).Value;
			Texture2D tail = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/DevourerofGodsTailV104", AssetRequestMode.ImmediateLoad).Value;
			float opacity = Projectile.Opacity;
			Color color = Color.Violet;
			color.A /= 2;
			float scale = Projectile.scale;
			SpriteEffects effects = SpriteEffects.None;
			if (Projectile.localAI[0] >= TrailingCacheLength)
			{
				spriteBatch.Draw(tail, Projectile.oldPos[TrailingCacheLength - 1]
					- (Projectile.oldRot[TrailingCacheLength - 1] - MathHelper.PiOver2).ToRotationVector2() * 68f - Main.screenPosition, null,
					color * opacity, Projectile.oldRot[TrailingCacheLength - 1], tail.Size() / 2f, scale, effects, 0f);
			}
			for (int i = TrailingCacheLength - 1; i >= 0; i--)
			{
				if (Projectile.localAI[0] < i) { i = (int)Projectile.localAI[0]; continue; }
				spriteBatch.Draw(body, Projectile.oldPos[i] - Main.screenPosition, null,
					color * opacity, Projectile.oldRot[i], body.Size() / 2f, scale, effects, 0f);
			}
			spriteBatch.Draw(head, Projectile.Center - Main.screenPosition, null,
				color * opacity, Projectile.rotation, head.Size() / 2f, scale, effects, 0f);
			if (Projectile.localAI[0] < TrailingCacheLength) drawRift(spriteBatch);
			return false;
		}
		public void drawRift(SpriteBatch spriteBatch)
		{
			Projectile.localAI[1] += 0.10471976f;
			float lerpMult = Utils.GetLerpValue(15f, 30f, (TrailingCacheLength - 1 - Projectile.localAI[0]) * 200 / TrailingCacheLength, true)
				* Utils.GetLerpValue(240f, 200f, (TrailingCacheLength - 1 - Projectile.localAI[0]) * 200 / TrailingCacheLength, true)
				* (1f + (0.2f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 30f / 0.5f * 6.2831855f * 3f))) * 0.8f;
			Texture2D texture = ModContent.Request<Texture2D>("CalamitySouls/Content/V104/Extras/StarProjV104", AssetRequestMode.ImmediateLoad).Value;
			Vector2 drawPos = Projectile.oldPos[(int)Projectile.localAI[0] - 1] - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
			Color baseColor = new Color(150, 100, 255, 255) * Projectile.Opacity;
			baseColor *= 0.5f;
			baseColor.A = 0;
			Color colorA = baseColor;
			Color colorB = baseColor * 0.5f;
			colorA *= lerpMult;
			colorB *= lerpMult;
			Vector2 origin = texture.Size() / 2f;
			Vector2 scale = new Vector2(3f, 9f) * Projectile.Opacity * lerpMult;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1) spriteEffects = SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, drawPos, null, colorA, 1.5707964f, origin, scale, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorA, 0f, origin, scale, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, 1.5707964f, origin, scale * 0.8f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, 0f, origin, scale * 0.8f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorA, 1.5707964f + Projectile.localAI[1] * 0.25f, origin, scale, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorA, Projectile.localAI[1] * 0.25f, origin, scale, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, 1.5707964f + Projectile.localAI[1] * 0.5f, origin, scale * 0.8f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, Projectile.localAI[1] * 0.5f, origin, scale * 0.8f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorA, 0.7853982f, origin, scale * 0.6f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorA, 2.3561945f, origin, scale * 0.6f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, 0.7853982f, origin, scale * 0.4f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, 2.3561945f, origin, scale * 0.4f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorA, 0.7853982f + Projectile.localAI[1] * 0.75f, origin, scale * 0.6f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorA, 2.3561945f + Projectile.localAI[1] * 0.75f, origin, scale * 0.6f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, 0.7853982f + Projectile.localAI[1], origin, scale * 0.4f, spriteEffects, 0f);
			spriteBatch.Draw(texture, drawPos, null, colorB, 2.3561945f + Projectile.localAI[1], origin, scale * 0.4f, spriteEffects, 0f);
		}
	}
}
