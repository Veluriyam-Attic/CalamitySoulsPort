using Terraria.Localization;

namespace CalamitySouls
{
	// 1.4 移植新增：把中文名字直接注册进语言缓存。
	// 背景：本机的 hjson 本地化未生效（机器翻译类模组 hook 了本地化加载流程），
	// 因此不能只依赖 Localization/*.hjson。这里在加载后用代码补一份，
	// Language.GetOrRegister 对已存在的键不会覆盖，所以 hjson 生效时仍以 hjson 为准。
	public static class CSLocalization
	{
		public static void Seed()
		{
			// 第三重保险：即使两个语言文件都没被加载，这里也把中文作为默认值注册进去。
			void R(string key, string text) => Language.GetOrRegister("Mods.CalamitySouls." + key, () => text);

			// ---- Items ----
			R("Items.AquaticfrogFin.DisplayName", "渊洋蛙鳍");
			R("Items.AquaticfrogFin.Tooltip", "+48%跳跃速度，获得连跳和一个海啸二段跳\n可以在液体中自由移动\n+5%近战速度，+5%远程暴击，-7.5%蓝耗，+1召唤栏和7.5%盗贼伤害");
			R("Items.BasicEnch.DisplayName", "基础魔石");
			R("Items.BasicSpell.DisplayName", "基础之咒");
			R("Items.Changelog.DisplayName", "改动日志");
			R("Items.Changelog.Tooltip", "使用来观看改动日志\n全英文，建议自行翻译（太屑被揍死）");
			R("Items.CoreofSlimeGod.DisplayName", "史莱姆之神核心");
			R("Items.CoreofSlimeGod.Tooltip", "把你的史莱姆坐骑转化为史莱姆神坐骑\n它的水平移速被大大增加，而且可以飞行一段距离\n脑瘫模式专属");
			R("Items.CovidVaccines.DisplayName", "中国新冠疫苗");
			R("Items.CovidVaccines.Tooltip", "医生：“中国新冠疫苗提供74.8%的保护。”\n观众：“才75%？这么少的吗？”\n医生：“这还少？80%不感染，感染99%不发病，发病100%不转重症。”\n医生：“换句话说，80%闪避几率，加99%免伤还免疫暴击。”\n医生：“这妥妥神器啊！”\n所以就有了这个物品\n80%几率闪避攻击，减少99%受到伤害并且100%不会被暴击");
			R("Items.CrystalTreads.DisplayName", "水晶靴");
			R("Items.CrystalTreads.Tooltip", "超凡的速度！\n提供一个冰霜冲刺\n增加20%跳跃速度\n但是，你的防御减少10");
			R("Items.DemonshadeWorkbench.DisplayName", "魔影坩埚");
			R("Items.EctoFish.DisplayName", "灵气鱼");
			R("Items.EctoFish.Tooltip", "右键获取一些灵气");
			R("Items.EnchAerospec.DisplayName", "天蓝魔石");
			R("Items.EnchAstral.DisplayName", "幻星魔石");
			R("Items.EnchAuric.DisplayName", "金源魔石");
			R("Items.EnchBloodflare.DisplayName", "血炎魔石");
			R("Items.EnchBrimflame.DisplayName", "硫火魔石");
			R("Items.EnchDaedalus.DisplayName", "代达罗斯魔石");
			R("Items.EnchDemonshade.DisplayName", "魔影魔石");
			R("Items.EnchDesertprowler.DisplayName", "沙漠游侠魔石");
			R("Items.EnchEmpyrean.DisplayName", "皇天魔石");
			R("Items.EnchFathom.DisplayName", "幻渊鱼群魔石");
			R("Items.EnchFearmonger.DisplayName", "神惧者魔石");
			R("Items.EnchGodslayer.DisplayName", "弑神者魔石");
			R("Items.EnchGodslayerAncient.DisplayName", "远古弑神者魔石");
			R("Items.EnchHydrothermic.DisplayName", "渊泉魔石");
			R("Items.EnchMollusk.DisplayName", "软壳魔石");
			R("Items.EnchOmega.DisplayName", "欧米伽蓝魔石");
			R("Items.EnchPlagueReaper.DisplayName", "瘟疫收割者魔石");
			R("Items.EnchPlaguebringer.DisplayName", "瘟疫使者魔石");
			R("Items.EnchPrismatic.DisplayName", "光棱魔石");
			R("Items.EnchReaver.DisplayName", "掠夺者魔石");
			R("Items.EnchSilva.DisplayName", "始源林海魔石");
			R("Items.EnchSnowruffian.DisplayName", "雪境暴徒魔石");
			R("Items.EnchStatigel.DisplayName", "斯塔提斯魔石");
			R("Items.EnchSulphurous.DisplayName", "硫磺海魔石");
			R("Items.EnchTarragon.DisplayName", "龙蒿魔石");
			R("Items.EnchTitan.DisplayName", "泰坦之心魔石");
			R("Items.EnchUmbraphile.DisplayName", "日影魔石");
			R("Items.EnchVictide.DisplayName", "胜潮魔石");
			R("Items.EnchWulfrum.DisplayName", "钨钢魔石");
			R("Items.EvilTreads.DisplayName", "邪恶之靴");
			R("Items.EvilTreads.Tooltip", "超凡的速度！\n你处于慌乱状态\n+20魔力上限");
			R("Items.FishofFright.DisplayName", "恐惧之鱼");
			R("Items.FishofFright.Tooltip", "右键获取一些魂");
			R("Items.FishofMight.DisplayName", "力量之鱼");
			R("Items.FishofMight.Tooltip", "右键获取一些魂");
			R("Items.FishofSight.DisplayName", "视域之鱼");
			R("Items.FishofSight.Tooltip", "右键获取一些魂");
			R("Items.ForceAnnihilation.DisplayName", "湮灭之力");
			R("Items.ForceCreature.DisplayName", "生灵之力");
			R("Items.ForceRevolution.DisplayName", "起义之力");
			R("Items.ForceTechnical.DisplayName", "科技之力");
			R("Items.ForceTyranny.DisplayName", "暴政之力");
			R("Items.FrostPaintgun.DisplayName", "雪花彩弹枪");
			R("Items.FrostPaintgun.Tooltip", "射出造成霜火的冰雪染料球");
			R("Items.GodlySpikeNecklace.DisplayName", "神明之刺项链");
			R("Items.GodlySpikeNecklace.Tooltip", "+150穿甲和+15%伤害");
			R("Items.ImpurityWithinOnesBody.DisplayName", "内在邪秽");
			R("Items.ImpurityWithinOnesBody.Tooltip", "地球之怒");
			R("Items.InfestedfrogClaw.DisplayName", "感染蛙爪");
			R("Items.InfestedfrogClaw.Tooltip", "+48%跳跃速度，获得连跳和一个海啸二段跳\n获得攀爬墙壁的能力\n+5%免伤");
			R("Items.JawoftheDesert.DisplayName", "荒漠之颚");
			R("Items.JawoftheDesert.Tooltip", "+500%挖掘速度\n你的镐子多造成100%伤害\n脑瘫模式专属");
			R("Items.LethesGift.DisplayName", "忘川的礼物");
			R("Items.LethesGift.Tooltip", "你的武器根据伤害类型射出不同种类的弹幕\n部分武器无法收到此加成\n你的攻击造成着火");
			R("Items.ManaMedallion.DisplayName", "魔力徽章");
			R("Items.ManaMedallion.Tooltip", "放在背包中，给予自动喝魔力药效果");
			R("Items.ManicTalisman.DisplayName", "欠抽护符");
			R("Items.ManicTalisman.Tooltip", "激活脑瘫模式，让NPC的速度和ai速度乘150%，减少30%NPC受到伤害，你多受到50%伤害\n你的肾上腺素伤害增加50%，你的防御阻挡0.5伤害而非0.75\n击败任意boss前且没有boss存在时，NPC的AI速度和免伤增幅被取消\n只能在复仇模式下使用");
			R("Items.MoltenNecklace.DisplayName", "熔岩项链");
			R("Items.MoltenNecklace.Tooltip", "+20穿甲");
			R("Items.MonolithRay.DisplayName", "神话之书");
			R("Items.MonolithRay.Tooltip", "射出强大的幻星射线");
			R("Items.MonolithStick.DisplayName", "感染木棍");
			R("Items.MonolithStick.Tooltip", "看上去这种木头硬到难以加工...不过它已经可以作为一个好武器了");
			R("Items.PeerlessWindGod.DisplayName", "无瑕之风");
			R("Items.PeerlessWindGod.Tooltip", "‘我会放水的’");
			R("Items.PhantoFish.DisplayName", "幻魂鱼");
			R("Items.PhantoFish.Tooltip", "右键获取一些幻魂");
			R("Items.PortableFart.DisplayName", "便携屁");
			R("Items.PortableFart.Tooltip", "在背包中收藏，可以像绿马蹄气球一样工作");
			R("Items.PrismaticPrism.DisplayName", "光棱镜");
			R("Items.PrismaticPrism.Tooltip", "召唤一个在你周围环绕，改变敌对弹幕方向的棱镜\n当穿着全套光棱套时，取消光棱套的非魔法伤害减少效果");
			R("Items.PrismaticTrailToggler.DisplayName", "光棱预判线开关");
			R("Items.PrismaticTrailToggler.Tooltip", "在背包中收藏，关闭光棱魔石的预判线效果");
			R("Items.ReleaseoftheId.DisplayName", "情绪释放");
			R("Items.ReleaseoftheId.Tooltip", "514");
			R("Items.SandsharkNecklace.DisplayName", "沙鲨项链");
			R("Items.SandsharkNecklace.Tooltip", "+50穿甲和+8%伤害");
			R("Items.SeafrogLeg.DisplayName", "海蛙腿");
			R("Items.SeafrogLeg.Tooltip", "免疫摔伤，+48%跳跃速度，获得连跳和一个海啸二段跳");
			R("Items.SoulCalamity.DisplayName", "灾厄之魂");
			R("Items.SpellAquatic.DisplayName", "渊洋之咒");
			R("Items.SpellBetrayal.DisplayName", "背叛之咒");
			R("Items.SpellDespair.DisplayName", "绝望之咒");
			R("Items.SpellEleum.DisplayName", "冰川之咒");
			R("Items.SpellGlobe.DisplayName", "寰球之咒");
			R("Items.SpellHeavenshade.DisplayName", "天影之咒");
			R("Items.SpellOasis.DisplayName", "绿洲之咒");
			R("Items.SpellPlague.DisplayName", "瘟疫之咒");
			R("Items.SpellStarlight.DisplayName", "星光之咒");
			R("Items.StinkCushion.DisplayName", "恶臭坐垫");
			R("Items.StinkCushion.Tooltip", "TMD臭死了\n提供一个臭屁二段跳");
			R("Items.SuspiciousLookingBox.DisplayName", "可疑的盒子");
			R("Items.SuspiciousLookingBox.Tooltip", "召唤一个强大的渔具盒\n它在击败血肉之墙，机械boss，世纪之花和月球领主后都会增强\n染料商会售卖这个，他从哪里搞到的资源？");
			R("Items.SuspiciousLookingBoxInfinite.DisplayName", "可疑的盒子");
			R("Items.SuspiciousLookingBoxInfinite.Tooltip", "召唤一个强大的渔具盒\n可以无限使用，可以同时召唤不止一个盒子");
			R("Items.TheSorber.DisplayName", "阴阳化功石");
			R("Items.TheSorber.Tooltip", "阴阳吸星石的简化版\n+12%移速，+24%跳跃速度\n+20最大生命和最大魔力，+5%免伤\n");
			R("Items.UnholyFish.DisplayName", "不洁鱼");
			R("Items.UnholyFish.Tooltip", "右键获取一些精华");
			// ---- Projectiles ----
			R("Projectiles.AeroBlade.DisplayName", "天蓝之刃");
			R("Projectiles.AnnihilationOption.DisplayName", "子机");
			R("Projectiles.AuricFlash.DisplayName", "金源冲击");
			R("Projectiles.BloodOption.DisplayName", "子机");
			R("Projectiles.EleumCrystalShard.DisplayName", "冰川水晶碎片");
			R("Projectiles.EmpyreanSpectre.DisplayName", "皇天幻影");
			R("Projectiles.FathomFish.DisplayName", "幻渊小鱼");
			R("Projectiles.FriendlyCalamitas.DisplayName", "友好的终灾");
			R("Projectiles.FrigidArea.DisplayName", "霜冻区域");
			R("Projectiles.IdentityCard.DisplayName", "本我符卡");
			R("Projectiles.IdentityHeart.DisplayName", "本我之心");
			R("Projectiles.ImpurityLaser.DisplayName", "邪秽激光");
			R("Projectiles.ImpurityOrb.DisplayName", "邪秽球");
			R("Projectiles.PeerlessBullet.DisplayName", "无双弹");
			R("Projectiles.PeerlessWind.DisplayName", "无双之风");
			R("Projectiles.SnowflakeDecoration.DisplayName", "雪花");
			// ---- NPCs ----
			R("NPCs.MagnificentFishbox.DisplayName", "无敌的\n勇敢的\n性感的\n神秘的\n迷人的\n神气的\n勤勉的\n强势的\n华丽的\n激情的\n可怕的\n漂亮的\n强大的\n鱼盒子");
			R("NPCs.MagnificentFishes.DisplayName", "鱼");
			R("NPCs.WulfrumBase.DisplayName", "钨钢基地");
			R("NPCs.WulfrumProbe.DisplayName", "钨钢探测器");
			// ---- Buffs ----
			R("Buffs.AeroBladeBuff.DisplayName", "天蓝之刃");
			R("Buffs.AstralDivision.Description", "你被一分为二");
			R("Buffs.AstralDivision.DisplayName", "幻星分裂");
			R("Buffs.AstralImmunity.Description", "你不会受到伤害");
			R("Buffs.AstralImmunity.DisplayName", "幻星无敌");
			R("Buffs.AuricReviveCooldown.Description", "你的金源冲击正在充能");
			R("Buffs.AuricReviveCooldown.DisplayName", "金源冲击冷却");
			R("Buffs.BrimFrenzy.Description", "你使用了硫火的力量。\n你的攻击多造成25%伤害");
			R("Buffs.BrimFrenzy.DisplayName", "硫火暴乱");
			R("Buffs.BrimFrenzyCooldown.Description", "你不能使用硫火的力量。");
			R("Buffs.BrimFrenzyCooldown.DisplayName", "硫火冷却");
			R("Buffs.DaedalusEvadeCooldown.Description", "你的代达罗斯闪避正在冷却");
			R("Buffs.DaedalusEvadeCooldown.DisplayName", "代达罗斯闪避冷却");
			R("Buffs.DesertDodgeBuff.Description", "你掌握着沙尘暴的威力\n闪避一次任意伤害");
			R("Buffs.DesertDodgeBuff.DisplayName", "沙暴闪避");
			R("Buffs.DesertDodgeCD.Description", "你被暂时禁止使用沙暴的能力");
			R("Buffs.DesertDodgeCD.DisplayName", "沙暴闪避冷却");
			R("Buffs.FearmongerArea.DisplayName", "霜冻区域");
			R("Buffs.FearmongerAreaCooldown.DisplayName", "霜冻区域冷却");
			R("Buffs.FearmongerMarkCooldown.DisplayName", "目标切换冷却");
			R("Buffs.Frozen.Description", "你被冰冻了\n+12防御");
			R("Buffs.Frozen.DisplayName", "冰冻");
			R("Buffs.GodslayerAncientOtherworld.Description", "你的移速翻倍，高速移动时你处于无敌状态且能反弹弹幕");
			R("Buffs.GodslayerAncientOtherworld.DisplayName", "异界");
			R("Buffs.GodslayerCrit.Description", "你的攻击有50%几率暴击");
			R("Buffs.GodslayerCrit.DisplayName", "弑神者暴击");
			R("Buffs.GodslayerDefense.Description", "+80防御");
			R("Buffs.GodslayerDefense.DisplayName", "弑神者防御");
			R("Buffs.GodslayerResource.Description", "快速回复生命和魔力");
			R("Buffs.GodslayerResource.DisplayName", "弑神者资源");
			R("Buffs.GodslayerSpeed.Description", "+50%武器使用速度");
			R("Buffs.GodslayerSpeed.DisplayName", "弑神者速度");
			R("Buffs.GodslayerSplit.Description", "你的暴击多造成50%伤害");
			R("Buffs.GodslayerSplit.DisplayName", "弑神者暴击");
			R("Buffs.HydrothermicReviveCooldown.Description", "你的渊泉回复正在充能");
			R("Buffs.HydrothermicReviveCooldown.DisplayName", "渊泉回复充能");
			R("Buffs.IdentityMinionBuff.Description", "本我的释放");
			R("Buffs.IdentityMinionBuff.DisplayName", "本我");
			R("Buffs.SilvaInvulnerable.DisplayName", "林海无敌");
			R("Buffs.SilvaRecharge.DisplayName", "林海充能");
			R("Buffs.TarragonCloak.Description", "敌人伤害减少");
			R("Buffs.TarragonCloak.DisplayName", "龙蒿保护");
			R("Buffs.TarragonImmunity.Description", "你无敌了");
			R("Buffs.TarragonImmunity.DisplayName", "龙蒿无敌");
			R("Buffs.TerminalMark.Description", "取消所有生物的无敌");
			R("Buffs.TerminalMark.DisplayName", "终末标记");
			R("Buffs.UmbraphileUndetected.Description", "所有生物对你友好");
			R("Buffs.UmbraphileUndetected.DisplayName", "不可检测");
			// ---- Tiles ----
			R("Tiles.DemonshadeWorkbenchTile.MapEntry.0", "魔");
			R("Tiles.DemonshadeWorkbenchTile.MapEntry.1", "影");
			R("Tiles.DemonshadeWorkbenchTile.MapEntry.2", "坩");
			R("Tiles.DemonshadeWorkbenchTile.MapEntry.3", "埚");
		}

		// 注册后抽查几条，方便在日志里确认本地化是否生效
		public static void Verify()
		{
			var log = CalamitySouls.Instance?.Logger;
			if (log == null) return;
			const string P = "Mods.CalamitySouls.Items.";
			string Live(int type)
			{
				var mi = Terraria.ModLoader.ItemLoader.GetItem(type);
				if (mi == null) return "<非本模组物品>";
				return mi.DisplayName.Value + " || " + mi.Tooltip.Value.Replace("\n", " / ");
			}
			log.Info("[本地化自检] 魔石: " + Language.GetTextValue(P + "EnchAerospec.DisplayName"));
			log.Info("[本地化自检] 饰品: " + Language.GetTextValue(P + "AquaticfrogFin.DisplayName"));
			log.Info("[本地化自检] 咒  : " + Language.GetTextValue(P + "SpellGlobe.DisplayName"));
			log.Info("[本地化自检] 实况-欠抽护符: " + Live(Terraria.ModLoader.ModContent.ItemType<Items.SummonItems.ManicTalisman>()));
			log.Info("[本地化自检] 实况-史莱姆核心: " + Live(Terraria.ModLoader.ModContent.ItemType<Items.BossLoots.CoreofSlimeGod>()));
			log.Info("[本地化自检] 实况-荒漠之颚: " + Live(Terraria.ModLoader.ModContent.ItemType<Items.BossLoots.JawoftheDesert>()));
			log.Info("[本地化自检] 实况-冰刺投刀: " + Live(Terraria.ModLoader.ModContent.ItemType<Items.Weapons.IcicleDagger>()));
			log.Info("[本地化自检] 中文语言判定(应为 True): " + CSUtils.GameCultureChinese);
			log.Info("[本地化自检] 增益: " + Language.GetTextValue("Mods.CalamitySouls.Buffs.BrimFrenzy.DisplayName"));

			// 全量审计：把所有本模组物品里「名字不含任何汉字」的挑出来，避免再有漏网之鱼
			var sb = new System.Text.StringBuilder();
			int bad = 0, total = 0;
			for (int i = Terraria.ID.ItemID.Count; i < Terraria.ID.ItemID.Count + Terraria.ModLoader.ItemLoader.ItemCount; i++)
			{
				var mi = Terraria.ModLoader.ItemLoader.GetItem(i);
				if (mi == null || mi.Mod != CalamitySouls.Instance) continue;
				total++;
				string nm = mi.DisplayName.Value;
				bool zh = false;
				foreach (char c in nm) if (c >= 0x4e00 && c <= 0x9fff) { zh = true; break; }
				if (!zh) { bad++; sb.Append(nm + "(" + mi.Name + ") "); }
			}
			log.Info("[本地化自检] 物品总数: " + total + "，名字未汉化数: " + bad);
			log.Info("[本地化自检] 未汉化清单: " + (sb.Length == 0 ? "（无）" : sb.ToString()));

			// 审计二：物品描述里还有英文的
			var sb2 = new System.Text.StringBuilder();
			int bad2 = 0;
			for (int i = Terraria.ID.ItemID.Count; i < Terraria.ID.ItemID.Count + Terraria.ModLoader.ItemLoader.ItemCount; i++)
			{
				var mi = Terraria.ModLoader.ItemLoader.GetItem(i);
				if (mi == null || mi.Mod != CalamitySouls.Instance) continue;
				string tp = mi.Tooltip.Value;
				if (string.IsNullOrWhiteSpace(tp)) continue;
				bool zh = false;
				foreach (char c in tp) if (c >= 0x4e00 && c <= 0x9fff) { zh = true; break; }
				if (!zh) { bad2++; sb2.Append(mi.Name + " "); }
			}
			log.Info("[本地化自检] 描述未汉化的物品数: " + bad2 + " -> " + (sb2.Length == 0 ? "（无）" : sb2.ToString()));

			// 审计三：增益（buff）
			var sb3 = new System.Text.StringBuilder();
			int bad3 = 0, totalB = 0;
			for (int i = Terraria.ID.BuffID.Count; i < Terraria.ID.BuffID.Count + Terraria.ModLoader.BuffLoader.BuffCount; i++)
			{
				var mb = Terraria.ModLoader.BuffLoader.GetBuff(i);
				if (mb == null || mb.Mod != CalamitySouls.Instance) continue;
				totalB++;
				string nm = mb.DisplayName.Value + "|" + mb.Description.Value;
				bool zh = false;
				foreach (char c in nm) if (c >= 0x4e00 && c <= 0x9fff) { zh = true; break; }
				if (!zh) { bad3++; sb3.Append(mb.Name + " "); }
			}
			log.Info("[本地化自检] 增益总数: " + totalB + "，未汉化数: " + bad3 + " -> " + (sb3.Length == 0 ? "（无）" : sb3.ToString()));

			// 审计四：弹幕（只在死亡讯息里出现，先只报数量）
			int bad4 = 0, totalP = 0;
			for (int i = Terraria.ID.ProjectileID.Count; i < Terraria.ID.ProjectileID.Count + Terraria.ModLoader.ProjectileLoader.ProjectileCount; i++)
			{
				var mp = Terraria.ModLoader.ProjectileLoader.GetProjectile(i);
				if (mp == null || mp.Mod != CalamitySouls.Instance) continue;
				totalP++;
				string nm = mp.DisplayName.Value;
				bool zh = false;
				foreach (char c in nm) if (c >= 0x4e00 && c <= 0x9fff) { zh = true; break; }
				if (!zh) bad4++;
			}
			log.Info("[本地化自检] 弹幕总数: " + totalP + "，名字未汉化数: " + bad4);
		}
	}
}
