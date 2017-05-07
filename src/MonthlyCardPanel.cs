using System;
using UnityEngine;

public class MonthlyCardPanel : FuncUIPanel
{
	public UILabel puTongBuyHuoDeRMB;

	public UILabel zhizunBuyHongDeRMB;

	public UILabel puTongRMB_Daily;

	public UILabel zhizunRMB_Daily;

	public GameObject putongBuy;

	public GameObject putongBuyed;

	public GameObject zhizunBuy;

	public GameObject zhizunBuyed;

	public UILabel putongTime;

	public override void OnEnable()
	{
		base.OnEnable();
		this.InitInfo();
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10054)
		{
			this.InitInfo();
		}
	}

	public override void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		base.OnDisable();
	}

	public override void Awake()
	{
		this.InitEvent();
	}

	private void InitInfo()
	{
		this.puTongRMB_Daily.text = UnitConst.GetInstance().DesighConfigDic[25].value;
		this.zhizunRMB_Daily.text = UnitConst.GetInstance().DesighConfigDic[97].value;
		this.zhizunBuy.SetActive(HeroInfo.GetInstance().vipData.superCard != 1);
		this.zhizunBuyed.SetActive(HeroInfo.GetInstance().vipData.superCard == 1);
		this.putongBuy.SetActive(HeroInfo.GetInstance().vipData.cardEndTime == 0L);
		this.putongBuyed.SetActive(HeroInfo.GetInstance().vipData.cardEndTime > 0L);
		if (HeroInfo.GetInstance().vipData.cardEndTime > 0L)
		{
			if (TimeTools.IsSmallOrEquByDay(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.cardEndTime)))
			{
				this.putongTime.color = new Color(0.992156863f, 0.9843137f, 0.784313738f);
			}
			else
			{
				this.putongTime.color = Color.red;
			}
			this.putongTime.text = string.Format("{0}:{1}", LanguageManage.GetTextByKey("有效期至", "Vip"), TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().vipData.cardEndTime).ToString("yyyy-MM-dd"));
		}
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.MonthlyCard_PutongBuy, new EventManager.VoidDelegate(this.MonthlyCard_PutongBuy));
		EventManager.Instance.AddEvent(EventManager.EventType.MonthlyCard_ZhiZunBuy, new EventManager.VoidDelegate(this.MonthlyCard_ZhiZunBuy));
		EventManager.Instance.AddEvent(EventManager.EventType.MonthlyCard_ClosePanel, new EventManager.VoidDelegate(this.MonthlyCard_ClosePanel));
	}

	private void MonthlyCard_ZhiZunBuy(GameObject ga)
	{
		ShopHandler.CS_ShopBuyRMB(8, 0, null);
	}

	private void MonthlyCard_PutongBuy(GameObject ga)
	{
		ShopHandler.CS_ShopBuyRMB(7, 0, null);
	}

	private void MonthlyCard_ClosePanel(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("MonthlyCardPanel");
	}
}
