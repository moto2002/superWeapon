using msg;
using System;

public class BattleReportHander
{
	public static void CSBattleReport(int type, long reportId, bool video)
	{
		CSBattleReport cSBattleReport = new CSBattleReport();
		cSBattleReport.reportId = reportId;
		cSBattleReport.type = type;
		cSBattleReport.video = video;
		ClientMgr.GetNet().SendHttp(5008, cSBattleReport, null, null);
	}

	public static void CSReceiveReportMoney(long id)
	{
		CSReceiveReportMoney cSReceiveReportMoney = new CSReceiveReportMoney();
		cSReceiveReportMoney.id = id;
		ClientMgr.GetNet().SendHttp(5010, cSReceiveReportMoney, null, null);
	}
}
