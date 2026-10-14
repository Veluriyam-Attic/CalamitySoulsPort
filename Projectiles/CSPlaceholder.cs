using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamitySouls.Projectiles
{
	// 1.4：1.3 灾厄里被删除的弹幕（Xeroc 系列、ReaverSpore、GodSlayerPhantom 等）
	// 在移植时统一换成这个空占位弹幕。它一帧内自毁、不造成任何伤害，
	// 目的只是避免 NewProjectile(type:0) 生成无效弹幕导致空引用。
	public class CSPlaceholder : ModProjectile
	{
        public override Terraria.Localization.LocalizedText DisplayName => CSCompat.Zh("CSPlaceholder.DisplayName", "占位弹幕");
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.WoodenArrowFriendly;

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 2;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.alpha = 255;
			Projectile.damage = 0;
		}

		public override bool? CanDamage() => false;

		public override bool PreAI()
		{
			Projectile.Kill();
			return false;
		}
	}
}
