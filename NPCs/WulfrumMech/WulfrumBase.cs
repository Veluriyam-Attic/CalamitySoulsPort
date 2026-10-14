using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod;
using CalamityMod.World;
using System.Collections.Generic;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Buffs.StatDebuffs;

namespace CalamitySouls.NPCs.WulfrumMech
{
    public class WulfrumBase : ModNPC
    {
        public override void SetStaticDefaults()
        {
        }
        public override void AI()
        {
            NPC boat = CSUtils.GetBoss<WulfrumFloatingBoat>();
            if (boat == null)
            {
                //balabala
            }
            //balabala
        }
    }
}