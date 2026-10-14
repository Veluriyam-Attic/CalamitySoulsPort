using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Buffs;
using CalamityMod.World;

namespace CalamitySouls
{
    public class CSTile : GlobalTile
    {
        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            // 1.4：灾厄的 Boss 击破标志改由静态 DownedBossSystem 提供（1.3 是 CalamityWorld.downedSCal）
            if (CalamityMod.DownedBossSystem.downedCalamitas && type == TileType<CalamityMod.Tiles.PlacedRock>())
            {
                int terminalMark = BuffType<TerminalMark>();
                //Main.LocalPlayer.AddBuff(BuffType<TerminalMark>(), 60);
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.active)
                    {
                        npc.buffImmune[terminalMark] = false;
                        npc.AddBuff(terminalMark, 60);
                    }
                }
            }
        }
    }
}