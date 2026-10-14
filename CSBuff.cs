using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod;
using CalamityMod.CalPlayer;
using System.Collections.Generic;
using System.Text;
using CalamitySouls.NPCs;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls
{
    public class CSBuff : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            CalamityPlayer cplayer = player.GetModPlayer<CalamityPlayer>();
            if (type == 43 && Main.hardMode)
            {
                player.endurance -= 0.25f;
                cplayer.contactDamageReduction += 0.25f;
                cplayer.projectileDamageReduction += 0.25f;
            }
        }
    }
}