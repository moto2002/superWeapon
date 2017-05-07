using msg;
using System;

public class TopTenHandler
{
	private static Action<bool> TopTenShow;

	private static void OnHttpShow(bool isError, Opcode opcode)
	{
		if (TopTenHandler.TopTenShow != null)
		{
			TopTenHandler.TopTenShow(isError);
		}
	}

	public static void CG_TopTenListStart(int typeId)
	{
		CSRankingList cSRankingList = new CSRankingList();
		cSRankingList.type = typeId;
		ClientMgr.GetNet().SendHttp(1200, cSRankingList, null, null);
	}

	public static void GC_TopTenListEnd(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			FuncUIManager.inst.OpenFuncUI("TopTenPanel", Loading.senceType);
		}
	}

	public static void CG_TopTenPrizeListStart()
	{
		CSRankingPrizeList cSRankingPrizeList = new CSRankingPrizeList();
		cSRankingPrizeList.type = 2;
		ClientMgr.GetNet().SendHttp(1204, cSRankingPrizeList, null, null);
	}

	public static void GC_TopTenPrizeListEnd(bool isError, Opcode opcode)
	{
	}

	public static void CG_TopTenPrizeStart(int prizeId, Action<bool> func = null)
	{
		TopTenHandler.TopTenShow = func;
		CSRankingPrize cSRankingPrize = new CSRankingPrize();
		cSRankingPrize.type = prizeId;
		ClientMgr.GetNet().SendHttp(1202, cSRankingPrize, new DataHandler.OpcodeHandler(TopTenHandler.OnHttpShow), null);
	}

	public static void GC_TopTenPrizeEnd(bool isError, Opcode opcode)
	{
		TopTenHandler.CG_TopTenPrizeListStart();
	}
}
