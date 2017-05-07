using msg;
using System;

public class BuyGoldHandler
{
	public static void CSUseMoneyBuyGold(bool freebuy)
	{
		CSUseMoneyBuyGold cSUseMoneyBuyGold = new CSUseMoneyBuyGold();
		cSUseMoneyBuyGold.id = 1;
		cSUseMoneyBuyGold.freeBuy = freebuy;
		ClientMgr.GetNet().SendHttp(1700, cSUseMoneyBuyGold, new DataHandler.OpcodeHandler(BuyGoldHandler.SCUseMoneyBuyGold), null);
	}

	public static void SCUseMoneyBuyGold(bool isError, Opcode opcode)
	{
		BuyGoldManager.GetIns().RefreshUI();
	}
}
