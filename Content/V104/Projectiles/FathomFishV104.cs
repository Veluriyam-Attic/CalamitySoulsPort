// 幻渊鱼群魔石（EnchFathom）配套弹幕 · 幻渊小鱼 FathomFishV104
// 照搬：ext1040/Projectiles/FathomFish.cs（本模组自己的弹幕）
// 1.4 处置：SetupProjectileTrailling→静态集合 ✓ CirculateOldpos 省略 ✓ Kill→OnKill ✓ CanDamage→bool? ✓
//           PreDraw(ref Color) + End()/Begin() 成对（与幻影/FallenAstralStar 同法 ✓）
//           Main.projectileTexture→TextureAssets ✓ AngleToSafe/DirectionToSafe→CSPlayerV104Effects ✓
//           DustID.AmethystBolt/EmeraldBolt ✗ → DustID.GemAmethyst/GemEmerald ✓（既有映射先例）
//           Main.PlaySound→SoundEngine（1.3 此处无音量修饰 ⇒ 1:1 ✓）
// ⭐ CreatureEffect 分支：player.V104().CreatureEffect（我方 ForceCreature 捆绑链已接通 ⇒ 本轮起真正生效）
//    生效点：① 光环时长 20 分钟 vs 5 分钟（cDepth/marked/sulphurPoison/irradiated/nightwither）
//            ② soundDelay 是否累加（CreatureEffect 时不累加）③（上游）鱼的数量 1 条 vs 7 条
// 贴图：v1.0.4.0 包 Projectiles/FathomFish.rawimg → Content/V104/Projectiles/FathomFishV104.png
using System;
using CalamityMod;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;

namespace CalamitySouls.Content.V104.Projectiles
{
	public class FathomFishV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "幻渊小鱼");
			ProjectileID.Sets.TrailingMode[Type] = -1;
			ProjectileID.Sets.TrailCacheLength[Type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 38;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.scale = 0.7f;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D texture = TextureAssets.Projectile[Type].Value;
			Vector2 origin = new Vector2(texture.Width, texture.Height) / 2f;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			float scale = Projectile.scale;
			for (int i = 0; i < 5; i++)
			{
				scale *= 0.85f;
				spriteBatch.Draw(texture, Projectile.oldPos[i] - Main.screenPosition, null, Color.ForestGreen, Projectile.oldRot[i],
					origin, scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation,
				origin, Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (player.dead || !player.active || !player.V104().FathomFish)
			{
				Projectile.Kill();
				return;
			}
			Projectile.timeLeft = 2;
			int randomDust = Utils.SelectRandom(Main.rand, new int[] { 89, 33, 33, 33 });
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, randomDust);
			if (randomDust == 89) dust.scale *= 0.5f;
			dust.noGravity = true;
			dust.velocity *= 0f;
			Lighting.AddLight(Projectile.Center, 0, 1f, 1f);

			float minDist = 1000f;
			int index = -1;
			if (Projectile.soundDelay <= 0)
				foreach (NPC enermy in Main.npc)
				{
					if (enermy.active && !enermy.dontTakeDamage && !enermy.friendly
						&& !enermy.HasBuff(ModContent.BuffType<CalamityMod.Buffs.DamageOverTime.CrushDepth>()))   // 1.3: Calamity().cDepth <= 0
					{
						float dist = enermy.Hitbox.Distance(Projectile.Center);
						if (dist < minDist)
						{
							index = enermy.whoAmI;
							minDist = dist;
						}
					}
				}
			Vector2 tarPos = player.Center + CSPlayerV104Effects.DirectionToSafe(Projectile, player.Center) * 180f;
			float angleRadius = 0.05f;
			float speed = 12f;
			if (index != -1)
			{
				tarPos = Main.npc[index].Center;
				angleRadius = 0.2f;
				speed = 25f;
			}
			float dir = Utils.AngleLerp(Projectile.velocity.ToRotation(), CSPlayerV104Effects.AngleToSafe(Projectile, tarPos), angleRadius) + Main.rand.NextFloat(-1, 1) * 0.1f;
			Projectile.velocity = dir.ToRotationVector2() * speed;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.Distance(player.Center) > 2000f) Projectile.Center = player.Center;

			if (index != -1)
			{
				NPC npc = Main.npc[index];
				if (npc.Hitbox.Distance(Projectile.Center) < 40f)
				{
					// ⭐ CreatureEffect：20 分钟 vs 5 分钟（1.3 FathomFish.cs:104）
					int bufftime = player.V104().CreatureEffect ? 20 * 60 : 5 * 60;
					// 1.3: CalamityGlobalNPC 的 5 个"计时数值"字段（cDepth/marked/sulphurPoison/irradiated/nightwither）+= bufftime
					// 2.x: 这批字段改为 bool 标志或改名（先例见 CSProjectileV104.cs:53-54）；但 bool 无法表达"20 分钟 vs 5 分钟"的时长
					//      ⇒ 本移植改用等价的"施加对应减益并携带 bufftime"（时长 1:1 保留 ✓）
					npc.AddBuff(ModContent.BuffType<CalamityMod.Buffs.DamageOverTime.CrushDepth>(), bufftime);
					npc.AddBuff(ModContent.BuffType<CalamityMod.Buffs.StatDebuffs.MarkedforDeath>(), bufftime);
					if (npc.realLife == -1)
					{
						// 1.3: cnpc.sulphurPoison += bufftime；2.x 无 SulphurPoison 减益（API 无匹配）⇒ 按（甲）省略该条（差异表已记）
						npc.AddBuff(ModContent.BuffType<CalamityMod.Buffs.StatDebuffs.Irradiated>(), bufftime);
						npc.AddBuff(ModContent.BuffType<CalamityMod.Buffs.DamageOverTime.Nightwither>(), bufftime);
					}
					SoundEngine.PlaySound(SoundID.NPCDeath19, Projectile.Center);
					for (int i = 0; i < 18; i++)
					{
						Vector2 rot = MathHelper.ToRadians(360 * i / 18f).ToRotationVector2();
						Dust dBurst = Dust.NewDustPerfect(Projectile.Center, 89);
						dBurst.noGravity = true;
						dBurst.velocity = rot * 3f + Projectile.velocity / 2f;
						dBurst.scale *= 2f;
					}
					// ⭐ CreatureEffect：非该效果时才累加 soundDelay（1.3 FathomFish.cs:123）
					if (!player.V104().CreatureEffect) Projectile.soundDelay += 20;
					for (int i = 0; i < 6; i++)
					{
						Dust dust1 = Dust.NewDustPerfect(Projectile.Center + CSPlayerV104Effects.DirectionToSafe(Projectile, npc.Center) * 20f, DustID.GemAmethyst);
						dust1.noGravity = true;
						dust1.scale *= Main.rand.NextFloat() + 1f;
						dust1 = Dust.NewDustPerfect(Projectile.Center + CSPlayerV104Effects.DirectionToSafe(Projectile, npc.Center) * 20f, DustID.GemEmerald);
						dust1.noGravity = true;
						dust1.scale *= Main.rand.NextFloat() + 1f;
					}
				}
			}
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.NPCDeath19, Projectile.Center);
			for (int i = 0; i < 18; i++)
			{
				Vector2 rot = MathHelper.ToRadians(360 * i / 18f).ToRotationVector2();
				Dust dust = Dust.NewDustPerfect(Projectile.Center, 89);
				dust.noGravity = true;
				dust.velocity = rot * 3f;
				dust.scale *= 2f;
			}
		}

		public override bool? CanDamage() => false;
	}
}
