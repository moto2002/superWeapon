using msg;
using System;

public class PVPHandler
{
	public static void CS_PVP()
	{
		CSPVP cSPVP = new CSPVP();
		cSPVP.id = 1;
		ClientMgr.GetNet().SendHttp(5028, cSPVP, new DataHandler.OpcodeHandler(PVPHandler.SCPVPCallBack), null);
	}

	public static void CS_PVPMes()
	{
		CSReportList cSReportList = new CSReportList();
		cSReportList.listType = 1;
		ClientMgr.GetNet().SendHttp(5030, cSReportList, new DataHandler.OpcodeHandler(PVPHandler.SCPVPMesCallBack), null);
	}

	public static void CS_PVPMesRead()
	{
		CSReportReaded cSReportReaded = new CSReportReaded();
		cSReportReaded.id = 123L;
		cSReportReaded.type = 1;
		ClientMgr.GetNet().SendHttp(4008, cSReportReaded, null, null);
	}

	public static void CS_PVPFightBack(long fightback_id, long id)
	{
		SenceManager.inst.DelaySendHttp();
		CSSpyIsland cSSpyIsland = new CSSpyIsland();
		cSSpyIsland.id = fightback_id;
		cSSpyIsland.from = 7;
		cSSpyIsland.npcId = 0;
		LogManage.Log("复仇！  fightback_id:" + fightback_id);
		PVPMessage.inst.PVPFightBackID = id;
		ClientMgr.GetNet().SendHttp(4006, cSSpyIsland, new DataHandler.OpcodeHandler(PVPHandler.SCPVPFightBackCallBack), null);
	}

	public static void CS_PVPReport(long report_id)
	{
		BattleReportHander.CSBattleReport(8, report_id, true);
	}

	public static void SCPVPCallBack(bool Error, Opcode func)
	{
		if (!Error)
		{
			UIManager.curState = SenceState.Spy;
			if (Loading.senceType == SenceType.WorldMap)
			{
				SenceInfo.battleResource = SenceInfo.BattleResource.PVPFightBack_WorldMap;
			}
			else
			{
				SenceInfo.battleResource = SenceInfo.BattleResource.PVPFightBack_Home;
			}
			Loading.IsRefreshSence = true;
			PlayerHandle.EnterSence("island");
		}
	}

	public static void SCPVPFightBackCallBack(bool Error, Opcode func)
	{
		if (!Error)
		{
			UIManager.curState = SenceState.Spy;
			if (Loading.senceType == SenceType.WorldMap)
			{
				SenceInfo.battleResource = SenceInfo.BattleResource.PVPFightBack_WorldMap;
			}
			else
			{
				SenceInfo.battleResource = SenceInfo.BattleResource.PVPFightBack_Home;
			}
			Loading.IsRefreshSence = true;
			PlayerHandle.EnterSence("island");
		}
	}

	public static void SCPVPMesCallBack(bool Error, Opcode func)
	{
	}
}
