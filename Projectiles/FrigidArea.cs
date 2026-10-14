using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamitySouls.Projectiles
{
    public class FrigidArea : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 30;
            Projectile.scale = 0f;
        }
        public override void AI()
        {
            if (Projectile.timeLeft > 20)
            {
                float lerpAmt = (30f - Projectile.timeLeft) / 10f;
                Projectile.scale = MathHelper.Lerp(0f, 1f, lerpAmt);
                Projectile.alpha = (int)MathHelper.Lerp(100, 50, lerpAmt);
            }
            else
            {
                float lerpAmt = (20f - Projectile.timeLeft) / 20f;
                Projectile.scale = MathHelper.Lerp(1f, 1.1f, lerpAmt);
                Projectile.alpha = (int)MathHelper.Lerp(50, 255, lerpAmt);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor),
                0f, texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            return false;
        }
    }
}