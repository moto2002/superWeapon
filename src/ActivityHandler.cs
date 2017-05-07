using msg;
using System;

public class ActivityHandler
{
	public static void CSRefreshActivity(int type)
	{
		CSRefreshActivity cSRefreshActivity = new CSRefreshActivity();
		cSRefreshActivity.id = type;
		ClientMgr.GetNet().SendHttp(2100, cSRefreshActivity, null, null);
	}

	public static void SCRefreshActivity(bool isError, Opcode opcode)
	{
		if (ActivityDailyTip.ins != null && ActivityManager.GetIns().curActItem != null && ActivityManager.GetIns().curActItem.actType == ActType.day)
		{
			ActivityDailyTip.ins.RefreshUI();
		}
		if (ActivityWeekTip.ins != null && ActivityManager.GetIns().curActItem != null && ActivityManager.GetIns().curActItem.actType == ActType.week)
		{
			ActivityWeekTip.ins.RefreshUI();
		}
	}

	public static void CSRequestActivity()
	{
		CSRequestActivity cSRequestActivity = new CSRequestActivity();
		cSRequestActivity.worldMapId = 1;
		ClientMgr.GetNet().SendHttp(2102, cSRequestActivity, null, null);
	}

	public static void CSGetActivityList(int type, DataHandler.OpcodeHandler FuncCallBack)
	{
		CSGetActivityList cSGetActivityList = new CSGetActivityList();
		cSGetActivityList.type = type;
		ClientMgr.GetNet().SendHttp(2212, cSGetActivityList, FuncCallBack, null);
	}
}
