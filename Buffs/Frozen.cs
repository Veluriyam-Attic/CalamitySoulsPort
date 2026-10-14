using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamitySouls.Buffs
{
    public class Frozen : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.CS().Frozen = true;
        }
    }
}