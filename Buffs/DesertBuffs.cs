using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamitySouls;
using Microsoft.Xna.Framework;

namespace CalamitySouls.Buffs
{
    public class DesertDodgeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.CS().DesertDodgeBuff = true;
        }
    }
    public class DesertDodgeCD : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.CS().DesertDodgeCD = true;
            player.CS().DesertDodgeTimer = 0;
        }
    }
}
