using msg;
using System;

public class ArmsDealerHandler
{
	public static void CG_CSRefreshArmsDealer()
	{
		CSRefreshArmsDealer cSRefreshArmsDealer = new CSRefreshArmsDealer();
		cSRefreshArmsDealer.id = 1;
		ClientMgr.GetNet().SendHttp(1904, cSRefreshArmsDealer, null, null);
	}

	public static void CG_CSRefreshArmsDealerUseMoney()
	{
		CSRefreshArmsDealerUseMoney cSRefreshArmsDealerUseMoney = new CSRefreshArmsDealerUseMoney();
		cSRefreshArmsDealerUseMoney.id = 1;
		ClientMgr.GetNet().SendHttp(1902, cSRefreshArmsDealerUseMoney, null, null);
	}

	public static void CG_CSBuyItemFromArmsDealer(int armsId)
	{
		CSBuyItem cSBuyItem = new CSBuyItem();
		cSBuyItem.id = armsId;
		ClientMgr.GetNet().SendHttp(1906, cSBuyItem, null, null);
	}
}
