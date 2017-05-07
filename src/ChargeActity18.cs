using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChargeActity18 : ChargeRightPanel
{
	public UILabel goumaiNum;

	public UIScrollView scrow;

	public UIGrid grid;

	public GameObject itemPrefab;

	public ButtonClick btnBuy;

	public void Awake()
	{
		this.grid.isRespositonOnStart = false;
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get18, new EventManager.VoidDelegate(this.ChargeActityPnael_Get18));
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Buy18, new EventManager.VoidDelegate(this.ChargeActityPnael_Buy18));
	}

	public override void OnEnable()
	{
		if (HeroInfo.GetInstance().IsBuyChengZhangJiJin)
		{
			this.btnBuy.eventType = EventManager.EventType.none;
		}
		else
		{
			this.btnBuy.eventType = EventManager.EventType.ChargeActityPnael_Buy18;
		}
		List<ActivityClass> list = ChargeActityPanel.GetRegCharges[18];
		this.scrow.ResetPosition();
		this.grid.ClearChild();
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.grid.gameObject, this.itemPrefab);
			ChargeActityItem component = gameObject.GetComponent<ChargeActityItem>();
			component.tr.localPosition = new Vector3(this.grid.cellWidth * (float)i, -360f, 0f);
			component.tr.DOLocalMoveY(0f, 0.16f, false).SetDelay(0.1f * (float)i);
			component.SetInfo(list[i]);
		}
		int num = 0;
		if (HeroInfo.GetInstance().ActivitiesData_RecieveCountServer.ContainsKey(9999))
		{
			num = HeroInfo.GetInstance().ActivitiesData_RecieveCountServer[9999].count;
		}
		this.goumaiNum.text = num.ToString("d4");
	}

	private void ChargeActityPnael_Get18(GameObject ga)
	{
		ActivityClass curActity = ga.GetComponentInParent<ChargeActityItem>().curActity;
		if (ChargeActityPanel.ins.isCanRecieveActityRes(curActity))
		{
			CSgetActivityPrize cSgetActivityPrize = new CSgetActivityPrize();
			cSgetActivityPrize.activityId = curActity.activityId;
			ClientMgr.GetNet().SendHttp(2112, cSgetActivityPrize, delegate(bool isError, Opcode opcode)
			{
				ShowAwardPanelManger.showAwardList();
			}, null);
		}
	}

	private void ChargeActityPnael_Buy18(GameObject ga)
	{
		int num = int.Parse(UnitConst.GetInstance().DesighConfigDic[94].value);
		if (HeroInfo.GetInstance().playerRes.RMBCoin < num)
		{
			ShopPanelManage.ShowHelp_NoRMB(null, null);
			return;
		}
		MessageBox.GetMessagePanel().ShowBtn_RMB(LanguageManage.GetTextByKey("成长基金", "Activities"), LanguageManage.GetTextByKey("是否确定购买成长基金", "Activities"), num.ToString(), delegate
		{
			CSBuyGrowthFund cSBuyGrowthFund = new CSBuyGrowthFund();
			cSBuyGrowthFund.id = 1;
			ClientMgr.GetNet().SendHttp(1912, cSBuyGrowthFund, delegate(bool isError, Opcode opcode)
			{
				if (!isError)
				{
					this.OnEnable();
				}
			}, null);
		}, LanguageManage.GetTextByKey("取消", "Battle"), null);
	}
}
