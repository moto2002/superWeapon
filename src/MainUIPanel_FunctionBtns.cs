using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainUIPanel_FunctionBtns : MonoBehaviour
{
	public GameObject OnlineGa;

	public GameObject ShouChongGa;

	public GameObject OneYuanGouGa;

	public GameObject FuliGa;

	public GameObject HuoDongGa;

	public GameObject GongGaoGa;

	public GameObject DuiHuanMaGa;

	public GameObject BaseGiftGa;

	public GameObject onlineNotice;

	public GameObject fuliNotice;

	public GameObject shouchongNotice;

	public GameObject oneYuanGouNotice;

	public GameObject huodongNotice;

	public GameObject gonggaoNotice;

	public GameObject basegiftNotice;

	private void Start()
	{
	}

	private bool IsDisPlayBaseGift()
	{
		return HeroInfo.GetInstance().BaseGiftClass.Count != 0;
	}

	private void OnEnable()
	{
		GameObject arg_4A_0 = this.ShouChongGa;
		bool arg_4A_1;
		if (HeroInfo.GetInstance().ShouChongChargeClass.Count > 0)
		{
			arg_4A_1 = HeroInfo.GetInstance().ShouChongChargeClass.Any((KeyValuePair<int, List<ActivityClass>> a) => a.Value.Any((ActivityClass b) => !b.isReceived));
		}
		else
		{
			arg_4A_1 = false;
		}
		arg_4A_0.SetActive(arg_4A_1);
		GameObject arg_99_0 = this.OneYuanGouGa;
		bool arg_99_1;
		if (HeroInfo.GetInstance().OneYuanGouChargeClass.Count > 0)
		{
			arg_99_1 = HeroInfo.GetInstance().OneYuanGouChargeClass.Any((KeyValuePair<int, List<ActivityClass>> a) => a.Value.Any((ActivityClass b) => !b.isReceived));
		}
		else
		{
			arg_99_1 = false;
		}
		arg_99_0.SetActive(arg_99_1);
		this.HuoDongGa.SetActive(HeroInfo.GetInstance().activityClass.Count > 0);
		this.FuliGa.SetActive(HeroInfo.GetInstance().reChargeClass.Count > 0);
		this.OnlineGa.SetActive(UnitConst.GetInstance().loadReward.ContainsKey(OnLineAward.laod.step));
		this.BaseGiftGa.SetActive(this.IsDisPlayBaseGift());
		base.GetComponent<UIGrid>().Reposition();
		this.SetActivesNotice();
		this.SetGongGaoNotice();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void SetActivesNotice()
	{
		if (HeroInfo.GetInstance().reChargeClass.ContainsKey(19) && HeroInfo.GetInstance().LotteryDataFreeTimes > 0)
		{
			this.fuliNotice.SetActive(true);
		}
		else
		{
			this.fuliNotice.SetActive(HeroInfo.GetInstance().reChargeClass.Any((KeyValuePair<int, List<ActivityClass>> a) => a.Value.Any((ActivityClass b) => b.isCanGetAward)));
		}
		if (HeroInfo.GetInstance().activityClass.ContainsKey(1) && GetawardPanelShow.getId.Count > 0)
		{
			this.huodongNotice.SetActive(true);
		}
		else
		{
			this.huodongNotice.SetActive(HeroInfo.GetInstance().activityClass.Any((KeyValuePair<int, List<ActivityClass>> a) => a.Value.Any((ActivityClass b) => b.isCanGetAward)));
		}
		this.shouchongNotice.SetActive(HeroInfo.GetInstance().ShouChongChargeClass.Any((KeyValuePair<int, List<ActivityClass>> a) => a.Value.Any((ActivityClass b) => b.isCanGetAward)));
		this.oneYuanGouNotice.SetActive(HeroInfo.GetInstance().OneYuanGouChargeClass.Any((KeyValuePair<int, List<ActivityClass>> a) => a.Value.Any((ActivityClass b) => b.isCanGetAward)));
	}

	private void SetGongGaoNotice()
	{
		this.gonggaoNotice.SetActive(HeroInfo.GetInstance().gameAnnouncementData.isHaveNewAnounce);
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10097)
		{
			this.SetActivesNotice();
		}
		if (opcodeCMD == 10064)
		{
			this.SetGongGaoNotice();
		}
	}
}
