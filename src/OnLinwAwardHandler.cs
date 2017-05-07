using msg;
using System;

public class OnLinwAwardHandler
{
	private static Action<bool> online;

	public static void CG_CSOnLine(int OnlineId, Action<bool> func = null)
	{
		OnLinwAwardHandler.online = func;
		CSOnlineRewards cSOnlineRewards = new CSOnlineRewards();
		cSOnlineRewards.id = OnlineId;
		ClientMgr.GetNet().SendHttp(2208, cSOnlineRewards, new DataHandler.OpcodeHandler(OnLinwAwardHandler.OnHttpShow), null);
	}

	private static void OnHttpShow(bool isError, Opcode opcode)
	{
		if (OnLinwAwardHandler.online != null)
		{
			OnLinwAwardHandler.online(isError);
		}
	}
}
