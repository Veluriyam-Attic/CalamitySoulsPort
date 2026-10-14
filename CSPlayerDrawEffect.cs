using Terraria;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamitySouls.Buffs;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls
{
	// 1.4：绘制层由 PlayerLayer 改为 PlayerDrawLayer 子类（自动加载），
	// 原先在 ModPlayer 上重写 ModifyDrawLayers/ModifyDrawInfo 的写法已不存在
	public class CSPlayerDrawEffect : ModPlayer
	{
	}

	public class WulfrumShieldDrawLayer : PlayerDrawLayer
	{
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (!drawPlayer.active || drawPlayer.dead || drawPlayer.outOfRange) return false;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile p = Main.projectile[i];
				if (p.active && p.owner == drawPlayer.whoAmI && p.type == ProjectileType<Projectiles.WulfrumShield>())
					return true;
			}
			return false;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			int index = -1;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile p = Main.projectile[i];
				if (p.active && p.owner == drawPlayer.whoAmI && p.type == ProjectileType<Projectiles.WulfrumShield>())
				{
					index = i;
					break;
				}
			}
			if (index == -1) return;
			Projectile projectile = Main.projectile[index];
			Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
			Rectangle sRect = new Rectangle(0, 120 * projectile.frame, 118, 118);
			DrawData data = new DrawData(texture, projectile.Center - Main.screenPosition, sRect, new Color(new Vector4(255)),
				0f, new Vector2(59, 59), 1f, SpriteEffects.None, 0);
			drawInfo.DrawDataCache.Add(data);
		}
	}

	public class FrozenOverDrawLayer : PlayerDrawLayer
	{
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			return drawPlayer.active && !drawPlayer.dead && drawPlayer.CS().Frozen;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Texture2D texture = CSTexture.Frozen;
			Vector2 position = drawPlayer.Center - Main.screenPosition;
			SpriteEffects spriteEffects = (drawPlayer.direction != -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			DrawData data = new DrawData(texture, position, null, new Color(new Vector4(100)), 0f,
				texture.Size() / 2, 1f, spriteEffects, 0);
			drawInfo.DrawDataCache.Add(data);
		}
	}
}
