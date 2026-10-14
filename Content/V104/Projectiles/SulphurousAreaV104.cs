using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using CalamityMod.Buffs.DamageOverTime;

namespace CalamitySouls.Content.V104.Projectiles
{
	// 「新版硫磺魔石（EnchSulphurous）」的周围酸海剧毒区域。
	// 与我方旧版 Projectiles\SulphurousArea 是同一个机制，唯一区别是读新版玩家的标记
	// （旧版读 player.CS().SulphurousCircle，这里读 player.V104().SulphurousCircle），
	// 因此两套魔石同时佩戴时互不干扰。
	public class SulphurousAreaV104 : ModProjectile
	{
		public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("SulphurousAreaV104.DisplayName", "硫海剧毒区域（新版）");
		public float Type
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public float Range
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		public override string Texture => CSUtils.BlankTexture;
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.hide = true;
		}
		public override bool ShouldUpdatePosition()
		{
			Player player = Main.player[Projectile.owner];
			Vector2 iCenter = player.Center;
			if (Type != 1 || player.whoAmI == Main.myPlayer)
			{
				Projectile.velocity = iCenter - Projectile.Center;
			}
			if (Type == 1 && player.whoAmI == Main.myPlayer)
			{
				iCenter = Main.MouseWorld;
				Projectile.netImportant = true;
				Projectile.velocity = iCenter - Projectile.Center;
			}
			return true;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (!player.active || player.dead || !player.V104().SulphurousCircle)
			{
				Projectile.active = false;
				return;
			}
			Projectile.timeLeft++;
			Range = 36;
			if (Type == 1) Range = 16;
			if (Projectile.soundDelay == 0)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position - new Vector2(Range / 2f), (int)Range, (int)Range, ModContent.DustType<global::CalamitySouls.Projectiles.SulphurousDust>());
				if (Type == 1) dust.scale *= 0.8f;
				Projectile.soundDelay = Main.rand.Next(10, 26);
			}
			if (Main.rand.Next(3) == 0)
			{
				Dust sparkle = Dust.NewDustPerfect(Projectile.position + CSUtils.RandomRotate * Range, 75);
				sparkle.noGravity = true;
				sparkle.velocity = Vector2.Zero;
				sparkle.scale = 0.8f;
			}
			int bufftime = 2 * 60;
			if (Type == 1) bufftime = 2;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && !npc.friendly && npc.Hitbox.Distance(Projectile.Center) <= Range)
				{
					npc.ForceThrowABuffOnYourFuckingFace(ModContent.BuffType<SulphuricPoisoning>(), bufftime);
				}
			}
		}
	}
}
