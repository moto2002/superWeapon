using System;
using UnityEngine;

public class BuyGoldManager
{
	public delegate void VoidDelegate();

	private static BuyGoldManager ins;

	public BuyGoldManager.VoidDelegate CallBackRefresh;

	public int vipLevel;

	public int freeBuyTimes;

	public int buyTimes;

	public long lastRefreshTime;

	public static BuyGoldManager GetIns()
	{
		if (BuyGoldManager.ins == null)
		{
			BuyGoldManager.ins = new BuyGoldManager();
		}
		return BuyGoldManager.ins;
	}

	public void CheckReset()
	{
		if (TimeTools.GetNowTimeSyncServerToDateTime().Date != TimeTools.ConvertLongDateTime(this.lastRefreshTime).Date)
		{
			this.freeBuyTimes = 0;
			this.buyTimes = 0;
		}
	}

	public long GetJinBiNum()
	{
		return long.Parse(UnitConst.GetInstance().DesighConfigDic[8].value);
	}

	public int GetZuanShiNum()
	{
		int result;
		if (this.freeBuyTimes < this.GetFreeTimes())
		{
			result = 0;
		}
		else
		{
			int num = (this.buyTimes >= this.GetTotalTimes()) ? this.GetTotalTimes() : (this.buyTimes + 1);
			result = Mathf.FloorToInt((float)(num * int.Parse(UnitConst.GetInstance().DesighConfigDic[9].value) * this.GetDiscount() / 100));
		}
		return result;
	}

	public int GetLeftTimes()
	{
		int result;
		if (this.freeBuyTimes < this.GetFreeTimes())
		{
			result = this.freeBuyTimes;
		}
		else
		{
			result = this.buyTimes;
		}
		return result;
	}

	public void GetButtonStr(UILabel textBtn)
	{
		if (this.IsCanCheck())
		{
			textBtn.color = Color.white;
			textBtn.text = ((this.freeBuyTimes >= this.GetFreeTimes()) ? "兑换" : "免费");
		}
		else
		{
			textBtn.color = Color.red;
			textBtn.text = "已满仓";
		}
	}

	public int GetTotalTimes()
	{
		int result;
		if (this.freeBuyTimes < this.GetFreeTimes())
		{
			result = this.GetFreeTimes();
		}
		else
		{
			int num = UnitConst.GetInstance().GoldPurchaseDic[HeroInfo.GetInstance().PlayerCommondLv].number[ResType.金币];
			int num2 = 0;
			result = num + num2;
		}
		return result;
	}

	public int GetFreeTimes()
	{
		return int.Parse(UnitConst.GetInstance().DesighConfigDic[7].value);
	}

	public int GetDiscount()
	{
		return 100;
	}

	public bool CheckTimes()
	{
		int num = UnitConst.GetInstance().GoldPurchaseDic[HeroInfo.GetInstance().PlayerCommondLv].number[ResType.金币];
		int num2 = 0;
		return this.buyTimes < num + num2;
	}

	public bool CheckZuanShi()
	{
		return this.GetZuanShiNum() <= HeroInfo.GetInstance().playerRes.RMBCoin;
	}

	public bool IsCanCheck()
	{
		return HeroInfo.GetInstance().playerRes.resCoin < HeroInfo.GetInstance().playerRes.maxCoin;
	}

	public bool CheckFull()
	{
		return this.GetJinBiNum() + (long)HeroInfo.GetInstance().playerRes.resCoin <= (long)HeroInfo.GetInstance().playerRes.maxCoin;
	}

	public void Buy()
	{
		BuyGoldHandler.CSUseMoneyBuyGold(this.freeBuyTimes < this.GetFreeTimes());
	}

	public void RefreshUI()
	{
		if (BuyGoldPanelManager.ins != null)
		{
			BuyGoldPanelManager.ins.Refresh();
		}
		if (FuncUIManager.inst != null && (UIManager.curState == SenceState.InBuild || UIManager.curState == SenceState.Home))
		{
			FuncUIManager.inst.MainUIPanelManage.SetResourceText(false);
		}
	}
}
