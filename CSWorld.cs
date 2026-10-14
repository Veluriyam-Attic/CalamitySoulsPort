using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamitySouls.Buffs;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace CalamitySouls
{
    public class CSWorld : ModSystem
    {
        public override void OnWorldLoad()
        {
            HyperMode = false;
        }
        public override void PreUpdateWorld()
        {
            if (!CalamityMod.World.CalamityWorld.revenge && HyperMode) 
            {
                HyperMode = false;
                string text = CSUtils.GameCultureChinese ? CSUtils.ManicModeNameChi + "关闭" : CSUtils.ManicModeName + " deactivated.";
                Main.NewText(text, Color.SkyBlue);
            }
        }
        // 1.4：ModSystem 的存档钩子改为 SaveWorldData / LoadWorldData
        public override void SaveWorldData(TagCompound tag)
        {
            tag["HyperMode"] = HyperMode;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            HyperMode = tag.GetBool("HyperMode");
        }
        public static bool HyperMode;
    }
}