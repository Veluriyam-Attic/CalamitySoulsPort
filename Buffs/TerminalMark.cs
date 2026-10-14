using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamitySouls.Buffs
{
    public class TerminalMark : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.active)
            {
                npc.dontTakeDamage = false;
                npc.dontTakeDamageFromHostiles = false;
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active) npc.AddBuff(Type, 2);
            }
        }
    }
}