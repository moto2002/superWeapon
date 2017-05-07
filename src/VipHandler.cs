using msg;
using System;

public class VipHandler
{
	public static void ReceieveVIP()
	{
		CSReceiveVipDailyMoney cSReceiveVipDailyMoney = new CSReceiveVipDailyMoney();
		cSReceiveVipDailyMoney.id = 1;
		ClientMgr.GetNet().SendHttp(1010, cSReceiveVipDailyMoney, new DataHandler.OpcodeHandler(VipHandler.ReceieveVipCallBack), null);
	}

	private static void ReceieveVipCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
		}
	}

	public static void ReceieveMonthlyCard(long buildingID)
	{
		CSTakeResource cSTakeResource = new CSTakeResource();
		cSTakeResource.buildingId = buildingID;
		ClientMgr.GetNet().SendHttp(2006, cSTakeResource, new DataHandler.OpcodeHandler(VipHandler.ReceieveVipCallBack), null);
	}
}
