using msg;
using System;

public class SevenDayHandler
{
	private static Action<bool> sevenAct;

	public static void CS_SevenDay(int sevenId, Action<bool> func = null)
	{
		SevenDayHandler.sevenAct = func;
		CSSevenDaysPrize cSSevenDaysPrize = new CSSevenDaysPrize();
		cSSevenDaysPrize.id = sevenId;
		ClientMgr.GetNet().SendHttp(2210, cSSevenDaysPrize, new DataHandler.OpcodeHandler(SevenDayHandler.OnSevenDay), null);
	}

	private static void OnSevenDay(bool isError, Opcode opcode)
	{
		if (SevenDayHandler.sevenAct != null)
		{
			SevenDayHandler.sevenAct(isError);
		}
	}
}
