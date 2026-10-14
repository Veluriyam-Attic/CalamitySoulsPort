using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 灾厄 1.3 的 SealedSingularityBlackhole（CalamityMod.Projectiles.Rogue）—— 2.x 已移除，故在 V104 自建等价实现。
	// 来源：ilspycmd 反编译 _tm_verify_CalamityMod.XNA.dll（1.3 CalamityMod v1.4.5.7），逐行照搬。
	// 2.x 差异：
	//  ① projectile.Calamity().rogue = true（2.x 无该字段）→ Projectile.DamageType = RogueDamageClass（本工程既有映射）
	//  ② OnHitPvp → 1.4 的 OnHitPlayer(Player, Player.HurtInfo)；AddBuff(80, 300, true) → AddBuff(BuffID.Blackout, 300)（名字驱动）
	//  ③ GetTexture 由引擎按类名解析（贴图 7 帧，从 1.3 灾厄包转）
	public class SealedSingularityBlackholeV104 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// 1.3 原文只有 DisplayName.SetDefault("Blackhole")、无中文；该名字不面向玩家显示，此处给中文名
			CSCompat.SeedDisplayName(this, "黑洞");
			Main.projFrames[Type] = 7;
		}
		public override void SetDefaults()
		{
			Projectile.width = 88;
			Projectile.height = 90;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = ModContent.GetInstance<CalamityMod.RogueDamageClass>();   // 1.3: Calamity().rogue = true
			Projectile.tileCollide = false;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 5;
		}
		public override void AI()
		{
			if (Projectile.timeLeft % 5 == 0)
			{
				Projectile.frame++;
			}
			if (Projectile.frame >= Main.projFrames[Type])
			{
				Projectile.frame = 0;
			}
			float x = Projectile.Center.X;
			float y = Projectile.Center.Y;
			float num = Projectile.Calamity().stealthStrike ? 1000f : 500f;
			float num2 = Projectile.Calamity().stealthStrike ? 0.25f : 0.1f;
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (!npc.CanBeChasedBy(Projectile, false) || !Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1)
					|| (!CalamityMod.NPCs.CalamityGlobalNPC.ShouldAffectNPC(npc) && npc.type != ModContent.NPCType<CalamityMod.NPCs.NormalNPCs.SuperDummyNPC>()))
				{
					continue;
				}
				float num3 = npc.width / 2 + npc.height / 2;
				if (Vector2.Distance(npc.Center, Projectile.Center) < num + num3)
				{
					if (npc.position.X < x) npc.velocity.X += num2;
					else npc.velocity.X -= num2;
					if (npc.position.Y < y) npc.velocity.Y += num2;
					else npc.velocity.Y -= num2;
				}
			}
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 300f)
			{
				Projectile.scale *= 0.95f;
				Projectile.Opacity *= 0.95f;
				Projectile.height = (int)(Projectile.height * Projectile.scale);
				Projectile.width = (int)(Projectile.width * Projectile.scale);
			}
			if (Projectile.scale <= 0.05f)
			{
				Projectile.Kill();
			}
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			// 1.3: target.AddBuff(80, 300, true)  —— 80 = BuffID.Blackout（已由名字解析确认）
			target.AddBuff(BuffID.Blackout, 300);
		}
	}
}
