using msg;
using System;
using System.Collections.Generic;

public class CalcMoneyHandler
{
	private static Action<bool, int> func;

	public static void CSCalcMoney(int type, int cdType, int index, long id, int itemID, int configureType, Action<bool, int> callBack)
	{
		CSCalcMoney cSCalcMoney = new CSCalcMoney();
		cSCalcMoney.type = type;
		cSCalcMoney.cdType = cdType;
		cSCalcMoney.index = index;
		cSCalcMoney.id = id;
		cSCalcMoney.itemId = itemID;
		cSCalcMoney.configureType = configureType;
		CalcMoneyHandler.func = callBack;
		ClientMgr.GetNet().SendHttp(9012, cSCalcMoney, new DataHandler.OpcodeHandler(CalcMoneyHandler.CalcMoneyCallBack), null);
	}

	public static void CSCalcMoney(int type, int cdType, int index, long id, int itemID, int configureType, int confNum, Action<bool, int> callBack)
	{
		CSCalcMoney cSCalcMoney = new CSCalcMoney();
		cSCalcMoney.type = type;
		cSCalcMoney.cdType = cdType;
		cSCalcMoney.index = index;
		cSCalcMoney.id = id;
		cSCalcMoney.itemId = itemID;
		cSCalcMoney.configureType = configureType;
		cSCalcMoney.confNum = confNum;
		CalcMoneyHandler.func = callBack;
		ClientMgr.GetNet().SendHttp(9012, cSCalcMoney, new DataHandler.OpcodeHandler(CalcMoneyHandler.CalcMoneyCallBack), null);
	}

	public static void CSCalcMoney_Walls(int type, List<long> cityWallIds, Action<bool, int> callBack)
	{
		CSCalcMoney cSCalcMoney = new CSCalcMoney();
		cSCalcMoney.type = type;
		cSCalcMoney.cityWallIds.AddRange(cityWallIds);
		CalcMoneyHandler.func = callBack;
		ClientMgr.GetNet().SendHttp(9012, cSCalcMoney, new DataHandler.OpcodeHandler(CalcMoneyHandler.CalcMoneyCallBack), null);
	}

	private static void CalcMoneyCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			List<SCCalcMoney> list = code.Get<SCCalcMoney>(10088);
			ExpensePanelManage.ClearCache();
			if (list.Count == 0)
			{
				LogManage.LogError("CalcMoneyCallBack is null");
				return;
			}
			int money = list[0].money;
			for (int i = 0; i < list[0].res.Count; i++)
			{
				switch ((int)list[0].res[i].key)
				{
				case 1:
				{
					int num = (int)list[0].res[i].value;
					ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("获取金币", "ResIsland") + "： " + num);
					break;
				}
				case 2:
				{
					int num2 = (int)list[0].res[i].value;
					ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("获取石油", "ResIsland") + ":" + num2);
					break;
				}
				case 3:
				{
					int num3 = (int)list[0].res[i].value;
					ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("获取钢铁", "ResIsland") + ":" + num3);
					break;
				}
				case 4:
				{
					int num4 = (int)list[0].res[i].value;
					ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("获取稀矿", "ResIsland") + ":" + num4);
					break;
				}
				}
			}
			if (list[0].timeDiff > 0L)
			{
				ExpensePanelManage.strings.Add(string.Format("{0}:{1}", LanguageManage.GetTextByKey("立即结束冷却", "ResIsland"), TimeTools.ConvertFloatToTimeByMilliseconds((double)list[0].timeDiff)));
			}
			if (money == 0 && list[0].timeDiff == 0L)
			{
				if (CalcMoneyHandler.func != null)
				{
					CalcMoneyHandler.func(true, 0);
				}
			}
			else
			{
				MessageBox.GetExpensePanel().Show_NewByServer(money, TimeTools.GetNowTimeSyncServerToDateTime().AddMilliseconds((double)list[0].timeDiff), CalcMoneyHandler.func);
			}
		}
		else if (CalcMoneyHandler.func != null)
		{
			CalcMoneyHandler.func(false, 0);
		}
	}
}
