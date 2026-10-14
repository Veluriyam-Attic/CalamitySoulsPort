using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod;
using CalamityMod.World;
using System.Collections.Generic;
using Terraria.ID;
using CalamitySouls.NPCs.WulfrumMech;
using static Terraria.ModLoader.ModContent;
using System;
using System.Linq;

namespace CalamitySouls.NPCs
{
    public class CSNPCStatic : GlobalNPC
    {
        public static Dictionary<int, int> bIndexDict = new Dictionary<int, int>();
        public static void RegisterBossIndexes()
        {
            RegisterBossIndex<WulfrumBase>();
            RegisterBossIndex<WulfrumFloatingBoat>();
        }
        public static void RegisterBossIndex<T>() where T : ModNPC
        {
            int type = NPCType<T>();
            if (bIndexDict.ContainsKey(type)) return;
            bIndexDict[type] = -1;
        }
        public static void ResetIndex()
        {
            for (int i = 0; i < bIndexDict.Count; i++)
            {
                int type = bIndexDict.ElementAt(i).Key;
                int BossIndex = bIndexDict.ElementAt(i).Value;
                if (BossIndex >= 0 && BossIndex < 255)
                {
                    if (!Main.npc[BossIndex].active || Main.npc[BossIndex].type != type) BossIndex = -1;
                }
                else BossIndex = -1;
                bIndexDict[type] = BossIndex;
            }
        }
    }
}