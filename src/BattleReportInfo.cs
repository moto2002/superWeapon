using System;
using System.Linq;

public class BattleReportInfo
{
	public static void ProcessData()
	{
		HeroInfo.GetInstance().DefReport = (from p in HeroInfo.GetInstance().DefReport
		orderby p.time descending
		select p).ToList<ReportData>();
		HeroInfo.GetInstance().AttrackReport = (from p in HeroInfo.GetInstance().AttrackReport
		orderby p.time descending
		select p).ToList<ReportData>();
		HeroInfo.GetInstance().attrackTitle = (from p in HeroInfo.GetInstance().attrackTitle
		orderby p.time descending
		select p).ToList<ReportData>();
		HeroInfo.GetInstance().defTitle = (from p in HeroInfo.GetInstance().defTitle
		orderby p.time descending
		select p).ToList<ReportData>();
	}

	public static void AddOrReplaceOneRecord(ReportData data)
	{
		if (data.type == BattleReportType.LiberateIsland)
		{
			int key = (int)data.targetId;
			if (UnitConst.GetInstance().AllNpc.ContainsKey(key) && (UnitConst.GetInstance().AllNpc[key].type == NpcType.石油资源点npc || UnitConst.GetInstance().AllNpc[key].type == NpcType.钢铁npc || UnitConst.GetInstance().AllNpc[key].type == NpcType.稀金npc))
			{
				data.type = BattleReportType.ResIsland;
			}
		}
		BattleReportInfo.AddTitleAndTipType(data);
		if ((data.type == BattleReportType.Base && data.targetId == HeroInfo.GetInstance().userId) || data.type == BattleReportType.IslandLost || (data.type == BattleReportType.ResIsland && data.targetId == HeroInfo.GetInstance().userId))
		{
			bool flag = false;
			for (int i = 0; i < HeroInfo.GetInstance().DefReport.Count; i++)
			{
				if (HeroInfo.GetInstance().DefReport[i].id == data.id)
				{
					HeroInfo.GetInstance().DefReport[i] = data;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				for (int j = HeroInfo.GetInstance().DefReport.Count - 1; j >= 0; j--)
				{
					if (HeroInfo.GetInstance().DefReport[j].id == data.id)
					{
						HeroInfo.GetInstance().DefReport.Remove(HeroInfo.GetInstance().AttrackReport[j]);
					}
				}
				HeroInfo.GetInstance().DefReport.Add(data);
				HeroInfo.GetInstance().DefReport = (from p in HeroInfo.GetInstance().DefReport
				orderby p.time descending
				select p).ToList<ReportData>();
			}
		}
		else
		{
			for (int k = HeroInfo.GetInstance().AttrackReport.Count - 1; k >= 0; k--)
			{
				if (HeroInfo.GetInstance().AttrackReport[k].id == data.id)
				{
					HeroInfo.GetInstance().AttrackReport.Remove(HeroInfo.GetInstance().AttrackReport[k]);
				}
			}
			HeroInfo.GetInstance().AttrackReport.Add(data);
			HeroInfo.GetInstance().AttrackReport = (from p in HeroInfo.GetInstance().AttrackReport
			orderby p.time descending
			select p).ToList<ReportData>();
		}
	}

	public static void AddTitleAndTipType(ReportData data)
	{
		string title = string.Empty;
		BattleReportUIType uiType = BattleReportUIType.BaseAttrackedByEnemy;
		BattleReportType type = data.type;
		switch (type)
		{
		case BattleReportType.Base:
			if (data.targetId == HeroInfo.GetInstance().userId)
			{
				if (data.fighterWin)
				{
					title = LanguageManage.GetTextByKey("主基地被攻陷", "Battle");
					uiType = BattleReportUIType.BaseAttrackedByEnemy;
				}
				else
				{
					title = LanguageManage.GetTextByKey("主基地防御成功", "Battle");
					uiType = BattleReportUIType.BaseDefByMe;
				}
			}
			if (data.fighterId == HeroInfo.GetInstance().userId)
			{
				if (!data.fighterWin)
				{
					title = LanguageManage.GetTextByKey("进攻", "Battle") + data.targetName + LanguageManage.GetTextByKey("基地失败", "Battle");
					uiType = BattleReportUIType.BaseDefByEnemy;
				}
				else
				{
					title = ((data.relation != RelationType.Enemy) ? string.Format(LanguageManage.GetTextByKey("攻陷", "Battle") + "{0}" + LanguageManage.GetTextByKey("基地", "Battle"), data.targetName) : string.Format(LanguageManage.GetTextByKey("对", "Battle") + "{0}" + LanguageManage.GetTextByKey("复仇成功", "Battle"), data.targetName));
					uiType = BattleReportUIType.BaseAttrackedByMe;
				}
			}
			break;
		case BattleReportType.IslandLost:
			title = LanguageManage.GetTextByKey("海岛丢失", "Battle");
			uiType = BattleReportUIType.IslandBreak;
			break;
		case BattleReportType.ResIsland:
			if (data.targetId == HeroInfo.GetInstance().userId)
			{
				if (data.fighterWin)
				{
					title = LanguageManage.GetTextByKey("资源岛丢失", "Battle");
					uiType = BattleReportUIType.ResAttrackedByEnemy;
				}
				else
				{
					title = LanguageManage.GetTextByKey("资源岛已防御", "Battle");
					uiType = BattleReportUIType.ResDefByMe;
				}
			}
			if (data.fighterId == HeroInfo.GetInstance().userId)
			{
				if (!data.fighterWin)
				{
					title = string.Format(LanguageManage.GetTextByKey("进攻", "Battle") + "{0}" + LanguageManage.GetTextByKey("的资源岛失败", "Battle"), data.targetName);
					uiType = BattleReportUIType.ResDefByEnemy;
				}
				else
				{
					title = ((data.relation != RelationType.Enemy) ? string.Format(LanguageManage.GetTextByKey("成功占领", "Battle") + "{0}" + LanguageManage.GetTextByKey("的资源岛", "Battle"), data.targetName) : string.Format("{0}", LanguageManage.GetTextByKey("成功收复资源岛", "Battle")));
					uiType = BattleReportUIType.ResAttrackedByMe;
				}
			}
			break;
		case BattleReportType.LiberateIsland:
		case BattleReportType.LiberateCommandIsland:
			if (data.fighterWin)
			{
				title = LanguageManage.GetTextByKey("解放海岛成功", "Battle");
				uiType = BattleReportUIType.IslandAttrackSuccess;
			}
			else
			{
				title = LanguageManage.GetTextByKey("解放海岛失败", "Battle");
				uiType = BattleReportUIType.IslandAttrackFaild;
			}
			break;
		case BattleReportType.IslandAppear:
			title = string.Format("{0}" + LanguageManage.GetTextByKey("已出现", "Battle"), LanguageManage.GetTextByKey("什么岛屿呢", "Battle"));
			uiType = BattleReportUIType.NpcAppear;
			break;
		case BattleReportType.NewOpenMap:
			title = LanguageManage.GetTextByKey("发现新目标", "others");
			uiType = BattleReportUIType.NpcAppear;
			break;
		case BattleReportType.LiberateOtherPlayer:
			title = ((data.relation != RelationType.Enemy) ? string.Format(LanguageManage.GetTextByKey("攻陷", "Battle") + "{0}" + LanguageManage.GetTextByKey("基地", "Battle"), data.targetName) : string.Format(LanguageManage.GetTextByKey("对", "Battle") + "{0}" + LanguageManage.GetTextByKey("复仇成功", "Battle"), data.targetName));
			uiType = BattleReportUIType.BaseAttrackedByMe;
			break;
		case BattleReportType.PVP:
			break;
		default:
			if (type != BattleReportType.dailyReport)
			{
			}
			break;
		}
		data.title = title;
		data.uiType = uiType;
	}

	public static ReportData GetReportDataByIndex(string index)
	{
		ReportData reportData = HeroInfo.GetInstance().AttrackReport.FirstOrDefault((ReportData p) => p.Index == index);
		if (reportData == null)
		{
			reportData = HeroInfo.GetInstance().DefReport.FirstOrDefault((ReportData p) => p.Index == index);
		}
		return reportData;
	}

	public static ReportData GetTitleByIndex(string index)
	{
		ReportData reportData = HeroInfo.GetInstance().attrackTitle.FirstOrDefault((ReportData p) => p.Index == index);
		if (reportData == null)
		{
			reportData = HeroInfo.GetInstance().defTitle.FirstOrDefault((ReportData p) => p.Index == index);
		}
		return reportData;
	}
}
