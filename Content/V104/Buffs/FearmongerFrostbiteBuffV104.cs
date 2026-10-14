using Terraria;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104.Buffs
{
	// 新版「神惧者」冻结时间的代价 —— 对应新版 1.3 CalamitySouls.Buffs.FearmongerFrostbiteBuff（1:1）
	public class FearmongerFrostbiteBuffV104 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			CSCompat.SeedDisplayName(this, "时光之殇");
			CSCompat.SeedTooltip(this, "时间冻结的代价\n温度的本质，是分子热运动的剧烈程度\n你冻结了除你之外物体时间，分子便慢了下来\n因此，温度大大降低，你的生命值持续下降\n继续下去的话恐怕会对你的身体造成不可逆转的损伤");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			// 1.3：不在冻结状态时快速衰减（-19/帧），冻结中则维持（+2/帧）
			if (!CSWorldV104.FearmongerTimeStop)
			{
				player.buffTime[buffIndex] -= 19;
				return;
			}
			player.buffTime[buffIndex] += 2;
		}
	}
}
