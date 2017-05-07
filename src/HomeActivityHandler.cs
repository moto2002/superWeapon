using msg;
using System;
using UnityEngine;

public class HomeActivityHandler : MonoBehaviour
{
	private static Action<bool> ReceiveActivity;

	private static Action<bool> RequestChargeTop;

	private static Action<bool> BuySecialGift;

	public static void CG_CSgetActivityPrize(int activityId, Action<bool> func = null)
	{
		HomeActivityHandler.ReceiveActivity = func;
		CSgetActivityPrize cSgetActivityPrize = new CSgetActivityPrize();
		cSgetActivityPrize.activityId = activityId;
		ClientMgr.GetNet().SendHttp(2112, cSgetActivityPrize, new DataHandler.OpcodeHandler(HomeActivityHandler.OnHttpShow), null);
	}

	private static void OnHttpShow(bool isError, Opcode opcode)
	{
		if (HomeActivityHandler.ReceiveActivity != null)
		{
			HomeActivityHandler.ReceiveActivity(isError);
		}
	}

	public static void CG_CSGetCSPayRanking(int type, Action<bool> func = null)
	{
		HomeActivityHandler.RequestChargeTop = func;
		CSGetRanking cSGetRanking = new CSGetRanking();
		cSGetRanking.type = type;
		ClientMgr.GetNet().SendHttp(2114, cSGetRanking, new DataHandler.OpcodeHandler(HomeActivityHandler.OnRechargeTop), null);
	}

	private static void OnRechargeTop(bool isError, Opcode opcode)
	{
		if (HomeActivityHandler.RequestChargeTop != null)
		{
			HomeActivityHandler.RequestChargeTop(isError);
		}
	}

	public static void CG_CSBuySpecialGift(int id, Action<bool> func = null)
	{
	}

	private static void OnBuySpecialGift(bool isError, Opcode opcode)
	{
		if (HomeActivityHandler.BuySecialGift != null)
		{
			HomeActivityHandler.BuySecialGift(isError);
		}
	}
}
