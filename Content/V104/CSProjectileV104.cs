using CalamityMod;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104.Items;
using CalamitySouls.Content.V104.Projectiles;

namespace CalamitySouls.Content.V104
{
	// 「新版」专属的弹幕全局钩子（旧版 CSProjectile 完全不动）。
	// 目前承载两颗新魔石的命中效果：
	//   · 禁戒 EnchForbidden  —— 手持武器时抵消召唤伤害衰减
	//   · 圣蚀 EnchAcidivine  —— 攻击附加辐射（对应新版 SulphurousPoison 的 target.Calamity().irradiated += 60）
	public class CSProjectileV104 : GlobalProjectile
	{
		// tML 加载期校验：GlobalNPC/GlobalItem/GlobalProjectile 等若含实例字段，必须声明 InstancePerEntity ✓
		// （本类含每实体状态 ⇒ 返回 true；不可改为 static ✗ 那会让所有实体共享状态）
		public override bool InstancePerEntity => true;

		// 1.3 CSProjectile.PostAI 用的上一帧位置
		private readonly System.Collections.Generic.Dictionary<int, Vector2> fearmongerOldProjPosition = new();
		// 掠夺者·速度档：钩爪掷出与拉回速度 +20%（1.3 逐值）
		public override void GrappleRetreatSpeed(Projectile projectile, Player player, ref float speed)
		{
			if (player.V104().ReaverSpeed) speed *= 1.2f;
		}
		public override void GrapplePullSpeed(Projectile projectile, Player player, ref float speed)
		{
			if (player.V104().ReaverSpeed) speed *= 1.2f;
		}

		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (projectile.owner < 0 || projectile.owner >= Main.player.Length) return;
			Player player = Main.player[projectile.owner];
			if (player == null || !player.active) return;
			CSPlayerV104 mplayer = player.V104();

			if (projectile.owner < 0 || projectile.owner >= Main.player.Length) return;

			// ---- 禁戒 / 神惧者魔石：抵消"手持武器时的召唤伤害衰减" ----
			// （新版：FearmongerNerfCancel || ForbiddenNerfCancel，两者走同一段逻辑）
			if ((mplayer.ForbiddenNerfCancel || mplayer.FearmongerNerfCancel) && projectile.CountsAsClass(DamageClass.Summon))
			{
				CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
				Item heldItem = player.HeldItem;
				bool forbiddenArmor = player.head == 200 && player.body == 198 && player.legs == 142;
				float summonNerfMult = (cplayer.fearmongerSet || (forbiddenArmor && heldItem.CountsAsClass(DamageClass.Magic))) ? 0.75f : 0.5f;
				// 1.4 已用 DamageType 取代 melee/ranged/magic/summon 布尔标志
				bool heldIsWeapon = !heldItem.IsAir && !heldItem.CountsAsClass(DamageClass.Summon)
					&& (heldItem.CountsAsClass(DamageClass.Melee) || heldItem.CountsAsClass(DamageClass.Ranged)
						|| heldItem.CountsAsClass(DamageClass.Magic) || heldItem.CountsAsClass(ModContent.GetInstance<RogueDamageClass>()))
					&& heldItem.hammer == 0 && heldItem.pick == 0 && heldItem.axe == 0 && heldItem.useStyle != 0
					&& !heldItem.accessory && heldItem.ammo == AmmoID.None;
				if (heldIsWeapon) modifiers.FinalDamage *= 1f / summonNerfMult;
			}

			// ---- 圣蚀魔石：攻击造成辐射 ----
			// 1.3 灾厄的 CalamityGlobalNPC.irradiated 是计时数值（+= 60）；2.x 改成了 bool 标志
			if (mplayer.EnchAcidivine || mplayer.SulphurousPoison) target.Calamity().irradiated = true;
			// ---- 欧米伽蓝魔石：敌人护甲越高你伤害越高（近战分支在 CSPlayerV104.ModifyHitNPCWithItem）----
			if (mplayer.OmegaDefenseBreak) modifiers.FlatBonusDamage += target.defDamage * 0.5f;
		}

		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player player = Main.player[projectile.owner];
			if (player == null || !player.active) return;
			CSPlayerV104 mplayer = player.V104();
			if (mplayer.EnchAcidivine) target.Calamity().irradiated = true;

			// 龙蒿 8 叶效果 [2]：命中时放出追踪能量（1.3 CSPlayerEffects.cs:817-829 逐字）
			// 注：本块位于 OnHitNPC ⇒ damageDone 即 1.3 的 damage（最终伤害 ✓ 1:1）
			if (player.Calamity().tarraRangedCooldown <= 0 && mplayer.TarragonLeafEffects[2] > 0)
			{
				player.Calamity().tarraRangedCooldown = 60;
				if (player.ownedProjectileCounts[ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.TarraEnergy>()] < 2)
				{
					for (int projCount = 0; projCount < 2; projCount++)
					{
						Vector2 velo = CalamityUtils.RandomVelocity(100f, 70f, 100f, 0.1f);
						int energyDamage = (int)(0.33f * damageDone);
						Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, velo,
							ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.TarraEnergy>(), energyDamage, 0f, player.whoAmI);
					}
				}
			}
			// 龙蒿 8 叶效果 [3]：命中时再撒两片原版叶（1.3 CSPlayerEffects.cs:830-844 逐字）
			if (player.Calamity().tarraRangedCooldown <= 0 && mplayer.TarragonLeafEffects[3] > 0 && player.whoAmI == Main.myPlayer)
			{
				player.Calamity().tarraRangedCooldown = 60;
				for (int i = 0; i < 2; i++)
				{
					Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f, 0.1f);
					int leafDamage = (int)(0.25f * projectile.damage);   // 1.3 原文用的是 proj.damage（弹幕自身伤害）⇒ 不是 damageDone
					int leaf = Projectile.NewProjectile(projectile.GetSource_FromAI(), target.Center, velocity, ProjectileID.CrystalLeafShot, leafDamage, 0f, player.whoAmI, 0f, 0f);
					if (leaf >= 0 && leaf < Main.projectile.Length)
					{
						Main.projectile[leaf].ForceClassless();   // 1.3: Calamity().forceTypeless = true
						Main.projectile[leaf].netUpdate = true;
					}
				}
			}
			// 龙蒿：命中掉落"龙蒿增益叶片"（1.3 CSPlayerEffects.cs:809-814 逐字）
			if (mplayer.EnchTarragon && mplayer.TarragonLeaf && mplayer.TarragonLeafCanSpawn)
			{
				Vector2 randPos = target.position + new Vector2(Main.rand.Next(target.width), Main.rand.NextFloat(target.height));
				Item.NewItem(player.GetSource_FromThis(), randPos, ModContent.ItemType<TarragonLeafV104>(), 1);
				mplayer.TarragonLeafCanSpawn = false;
			}

// ---- 幻星魔石：命中敌人使群星从天而降（1.3 CSPlayerEffects.cs:743-752 逐字）----
			// 1.3 条件：AstralFallingStar 且（无来源弹幕 或 来源不是落星本身）⇒ 避免落星连锁自生
			Player astralPlayer = Main.player[projectile.owner];   // 1.3 的 player 参数 ⇒ 用弹幕拥有者（忠实）
			CSPlayerV104 astralMp = astralPlayer.V104();
			if (astralMp.AstralFallingStar && astralMp.AstralFsTimer <= 0 && astralPlayer.whoAmI == Main.myPlayer)
			{
				Vector2 starPos = target.Center - Vector2.UnitY * 800f + Vector2.UnitX * Main.rand.Next(-200, 201);
				Projectile.NewProjectile(astralPlayer.GetSource_FromThis(), starPos,
					CSPlayerV104Effects.DirectionToSafe(projectile, target.Center) * 1f,
					ModContent.ProjectileType<FallenAstralStarV104>(),
					CSPlayerV104Effects.HeldDamage(astralPlayer, astralMp.AnnihilationEffect ? 300f : 80f), 10f, astralPlayer.whoAmI);
				astralMp.AstralFsTimer = 5;
			}
			// ---- 胜潮魔石：击中敌人时召唤两只小海星追击（对应新版 OnHitNPCWithAnything 里的 VictideStarfish 段）----
			if (mplayer.VictideStarfish)
			{
				if (mplayer.VictideCooldown > 0) mplayer.VictideCooldown--;
				if (player.whoAmI == Main.myPlayer && mplayer.VictideCooldown == 0 && Main.rand.Next(5) == 0)
				{
					for (int i = 0; i < 2; i++)
					{
						Vector2 dir = CSUtils.RandomRotate;
						Projectile star = Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center + dir * 40f,
							dir * 4f, ModContent.ProjectileType<Projectiles.VictideSeastarV104>(),
							CSPlayerV104Effects.HeldDamage(player, 25f), 0f, player.whoAmI, target.whoAmI);
						star.GetPlayerDamageType(player);
					}
					mplayer.VictideCooldown = 10;
				}
			}
		}

		// ---- 泰坦魔石：非穿透的主要弹幕消失时，在原地炸出 180×180 的震荡（对应新版 CSProjectile.Kill 里的 TitanExplosion 段）----
		public override void OnKill(Projectile projectile, int timeLeft)
		{
			if (!projectile.friendly || projectile.owner < 0 || projectile.owner >= Main.player.Length) return;
			if (!(projectile.penetrate == 1 || projectile.penetrate == 0)) return;
			if (projectile.Classless()) return;
			Player player = Main.player[projectile.owner];
			if (player == null || !player.active || !player.V104().TitanExplosion) return;

			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.Opacity = 0f;
			projectile.position = projectile.Center;
			projectile.width = projectile.height = 180;
			projectile.Center = projectile.position;
			projectile.damage = (int)(projectile.damage / 4f);
			projectile.ForceClassless();
			if (projectile.knockBack < 8f) projectile.knockBack = 8f;
			projectile.Damage();
			for (int i = 0; i < 24; i++)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, ModContent.DustType<CalamityMod.Dusts.AstralBlue>());
				dust.noGravity = true;
				dust.noLight = true;
			}
			for (int i = 0; i < 24; i++)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, ModContent.DustType<CalamityMod.Dusts.AstralOrange>());
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
		// 1.3 CSProjectile.PostAI:71-78 —— 时间冻结时把弹幕位置往上一帧拉（软冻结）
		public override void PostAI(Projectile projectile)
		{
			if (CSWorldV104.FearmongerTimeStop)
			{
				Vector2 oldPosition;
				if (fearmongerOldProjPosition.TryGetValue(projectile.whoAmI, out oldPosition) && oldPosition.Length() != 0f)
					projectile.position = (oldPosition + projectile.position) / 2f;
			}
			fearmongerOldProjPosition[projectile.whoAmI] = projectile.position;
		}
	}
}
