using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace CalamitySouls
{
    public static class CSTexture
    {
        public static void Load(Mod mod)
        {
            寒元 = ModContent.Request<Texture2D>("CalamitySouls/Extras/HanYuan").Value;
            Frozen = ModContent.Request<Texture2D>("CalamitySouls/Extras/Frozen").Value;
            FearmongerMark = ModContent.Request<Texture2D>("CalamitySouls/Extras/FearmongerMark").Value;
            MolluskSign = ModContent.Request<Texture2D>("CalamitySouls/Extras/MolluskSign").Value;
        }
        public static void Unload()
        {
            寒元 = null;
            Frozen = null;
            FearmongerMark = null;
            MolluskSign = null;
        }
        public static Texture2D 寒元;
        public static Texture2D Frozen;
        public static Texture2D FearmongerMark;
        public static Texture2D MolluskSign;
    }
}