using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「天蓝魔石（EnchAerospec）」的疲乏 —— 对应新版 1.3 CalamitySouls.Buffs.AeroFatigue（逐值 1:1）
	// 冲得越多，Buff 时间越长，伤害减免越多（每 600 帧 = 10%，上限 100%，按 10% 取整）
	public class AeroFatigueV104 : ModBuff
	{
		private const float MaxDamageReduced = 1f;
		private const int ExhaustedTime = 180;

		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "疲乏");
			CSCompat.SeedTooltip(this, "你跳的太多了，现在很累");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
		}
		public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
		{
			int index = -1;
			for (int i = 0; i < Main.LocalPlayer.buffTime.Length; i++)
			{
				if (Main.LocalPlayer.buffType[i] == Type && Main.LocalPlayer.buffTime[i] > 0)
				{
					index = i;
					break;
				}
			}
			if (index == -1) return;
			int time = Main.LocalPlayer.buffTime[index];
			float amt = time / 600f;
			if (amt > MaxDamageReduced) amt = MaxDamageReduced;
			amt = (int)(amt * 100f);
			amt = (int)(amt / 10f) * 10f;
			if (V104Utils.UsingChi)
			{
				if (time < ExhaustedTime) tip = "你刚刚使用了天蓝冲刺\n小心不要累到自己";
				else tip = "你冲的太多了，现在精疲力竭\n你的伤害减少" + amt + "%";
				return;
			}
			if (time < ExhaustedTime) tip = "You just used aero dash\nTake care and dont get tired";
			else tip = "You dashed too much and get exhausted\nDamage decreased by " + amt + "%";
		}
		public override void Update(Player player, ref int buffIndex)
		{
			int time = Main.LocalPlayer.buffTime[buffIndex];
			if (time < ExhaustedTime) return;
			float amt = time / 600f;
			if (amt > MaxDamageReduced) amt = MaxDamageReduced;
			amt = (int)(amt * 100f);
			amt = (int)(amt / 10f) / 10f;
			player.V104().AllDamageByMult *= 1f - amt;
		}
	}
}
