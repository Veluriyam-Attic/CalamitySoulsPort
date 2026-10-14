using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using CalamitySouls;

namespace CalamitySouls.Buffs
{
    public class TarragonCloak : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.5f;
        }
    }
    public class TarragonImmunity : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.SetImmunityTime(player.buffTime[buffIndex]);
        }
    }
}