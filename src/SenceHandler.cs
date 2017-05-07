using msg;
using System;

public class SenceHandler
{
	private static bool isGoInSence;

	private static Action<bool> FuncCallBack;

	public static void CG_GetPalyerWMapData()
	{
		CSPlayerWorldMap cSPlayerWorldMap = new CSPlayerWorldMap();
		cSPlayerWorldMap.worldMapId = 1L;
		ClientMgr.GetNet().SendHttp(4000, cSPlayerWorldMap, new DataHandler.OpcodeHandler(SenceHandler.GC_GetPalyerWMapData), null);
	}

	public static void GC_GetPalyerWMapData(bool isError, Opcode opcode)
	{
		PlayerHandle.EnterSence("WorldMap");
	}

	public static void CG_GetMapData(int idx, int from, int npcId = 0, Action<bool> funcCallBack = null)
	{
		LogManage.LogError("curIsland.mapIdx   " + idx);
		CSSpyIsland cSSpyIsland = new CSSpyIsland();
		cSSpyIsland.id = (long)idx;
		cSSpyIsland.from = from;
		cSSpyIsland.npcId = npcId;
		SenceHandler.FuncCallBack = funcCallBack;
		ClientMgr.GetNet().SendHttp(4006, cSSpyIsland, new DataHandler.OpcodeHandler(SenceHandler.GC_GetMapData), null);
	}

	public static void GC_GetMapData(bool isError, Opcode opcode)
	{
		if (SenceHandler.FuncCallBack != null)
		{
			SenceHandler.FuncCallBack(isError);
		}
		if (!isError)
		{
			Loading.IsRefreshSence = true;
			PlayerHandle.EnterSence("island");
		}
	}

	public static void GC_WatchNote2()
	{
		UIManager.curState = SenceState.WatchVideo;
		if (Loading.senceType == SenceType.WorldMap)
		{
			SenceInfo.battleResource = SenceInfo.BattleResource.ReplayVideo_WorldMap;
		}
		else
		{
			SenceInfo.battleResource = SenceInfo.BattleResource.ReplayVideo_Home;
		}
		Loading.IsRefreshSence = true;
		PlayerHandle.EnterSence("island");
	}
}
