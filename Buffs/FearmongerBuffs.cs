using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamitySouls.Buffs
{
    public class FearmongerArea : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.CS().FearmongerFrozen = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.position -= player.velocity / 2;
        }
    }
    public class FearmongerAreaCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
    public class FearmongerMarkCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}