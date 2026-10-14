using System.IO;
using Terraria;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;

namespace CalamitySouls.Content.V104
{
	// 新版「神惧者时间冻结」的世界标志 —— 对应新版 1.3 CalamitySouls.CSWorld（1:1，含网络同步与存档）
	// 1.4：ModWorld → ModSystem；NetSend/NetReceive/SaveWorldData/LoadWorldData 钩子同名
	// ⚠️ 屏幕滤镜与天空（1.3 的 Filters.Scene["CalamitySouls:FearmongerTimeFreeze"] + FearmongerTimeFreezeSky）
	//    按裁决省略：【已订正】着色器资源在 1.3 发布包中确实存在（Effects/BlindEffect.fx 1429B + .xnb 4376B + BlindScreenShader.cs 808B）⇒ 原判断有误，已列入补做；1.4 侧等价挂法另按 FilterLoader/ModSceneEffect 评估
	public class CSWorldV104 : ModSystem
	{
		public static bool FearmongerTimeStop
		{
			get => fearmongerTimeStop;
			set => fearmongerTimeStop = value;
		}
		private static bool fearmongerTimeStop;

		public override void OnWorldLoad() => fearmongerTimeStop = false;
		public override void OnWorldUnload() => fearmongerTimeStop = false;

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(fearmongerTimeStop);
		}
		public override void NetReceive(BinaryReader reader)
		{
			fearmongerTimeStop = reader.ReadBoolean();
		}
		public override void SaveWorldData(TagCompound tag)
		{
			tag["fearmongerTimeStop"] = fearmongerTimeStop;
		}
		public override void LoadWorldData(TagCompound tag)
		{
			fearmongerTimeStop = tag.GetBool("fearmongerTimeStop");
		}
	}
}
