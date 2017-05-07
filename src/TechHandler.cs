using msg;
using System;
using System.Collections.Generic;

public class TechHandler
{
	private static Action updateFun;

	private static Action endUpdateFun;

	public static bool IsChangeTechLevel;

	public static void CG_TechUpdate(int techId, int money, Action _Fun)
	{
		CSTechUp cSTechUp = new CSTechUp();
		TechHandler.updateFun = _Fun;
		cSTechUp.techId = techId;
		cSTechUp.money = money;
		LogManage.Log(money);
		ClientMgr.GetNet().SendHttp(6002, cSTechUp, new DataHandler.OpcodeHandler(TechHandler.GC_TechUpdate), null);
	}

	public static void GC_TechUpdate(bool isError, Opcode opcode)
	{
		if (TechHandler.updateFun != null)
		{
			TechHandler.updateFun();
		}
	}

	public static void CG_CSTechUpEnd(int techID, int money, Action _Fun)
	{
		CSTechUpEnd cSTechUpEnd = new CSTechUpEnd();
		TechHandler.endUpdateFun = _Fun;
		cSTechUpEnd.techId = techID;
		cSTechUpEnd.money = money;
		LogManage.Log(money);
		ClientMgr.GetNet().SendHttp(6004, cSTechUpEnd, new DataHandler.OpcodeHandler(TechHandler.GC_CSTechUpEnd), null);
	}

	public static void CG_CSTechDiamondPrize()
	{
		CSTechDiamondPrize cSTechDiamondPrize = new CSTechDiamondPrize();
		cSTechDiamondPrize.id = 111;
		ClientMgr.GetNet().SendHttp(6006, cSTechDiamondPrize, new DataHandler.OpcodeHandler(TechHandler.ReceievePrizeCallBack), null);
	}

	private static void ReceievePrizeCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
		}
	}

	private static void GC_CSTechUpEnd(bool iserror, Opcode code)
	{
		if (TechHandler.endUpdateFun != null)
		{
			TechHandler.endUpdateFun();
		}
	}

	public static void GetPlayerTechDataByServer(List<SCTechData> st)
	{
		TechHandler.IsChangeTechLevel = false;
		for (int i = 0; i < st.Count; i++)
		{
			if (st[i].cdTime > 0L)
			{
				HeroInfo.GetInstance().PlayerTechnologyUpdatingTime = st[i].cdTime;
				HeroInfo.GetInstance().PlayerTechnologyUpdatingItemID = (int)st[i].id;
			}
			if ((long)HeroInfo.GetInstance().PlayerTechnologyUpdatingItemID == st[i].id)
			{
				HeroInfo.GetInstance().PlayerTechnologyUpdatingTime = st[i].cdTime;
			}
			if (HeroInfo.GetInstance().PlayerTechnologyInfo.ContainsKey((int)st[i].id))
			{
				if (HeroInfo.GetInstance().PlayerTechnologyInfo[(int)st[i].id] != st[i].level)
				{
					TechHandler.IsChangeTechLevel = true;
				}
				HeroInfo.GetInstance().PlayerTechnologyInfo[(int)st[i].id] = st[i].level;
			}
			else
			{
				HeroInfo.GetInstance().PlayerTechnologyInfo.Add((int)st[i].id, st[i].level);
			}
		}
	}
}
