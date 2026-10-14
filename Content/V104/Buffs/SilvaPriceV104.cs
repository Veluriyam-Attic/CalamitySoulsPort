using System;
using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「始源林海魔石」的代价 —— 对应新版 1.3 CalamitySouls.Buffs.SilvaPrice（1:1）
	// 1.3 的 ModifyBuffTip 在 1.4 改名为 ModifyBuffText（逻辑不变）
	public class SilvaPriceV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "心灵破碎");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.persistentBuff[Type] = true;
		}
		public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
		{
			int reviveCount = Main.LocalPlayer.V104().SilvaReviveCount;
			float damageReduction = 100 - (int)(Math.Pow(0.7f, reviveCount) * 1000) / 10f;
			float drReduction = (int)(0.4f * reviveCount * 100f);
			tip = V104Utils.UsingChi ? ("死亡与重生磨损了你的信念，耗费了你的精力\n" +
				"伤害降低" + damageReduction + "%，伤害减免降低" + drReduction + "%\n" +
				"取下林海魔石会导致你立即死亡")
				: ("Death and revive abraded your morale and fatigued your strength\n" +
				"Damage is reduced by " + damageReduction + "% and damage reduction is reduced by " + drReduction + "%\n" +
				"Unequipping the silva ench results in instant death");
		}
	}
}
