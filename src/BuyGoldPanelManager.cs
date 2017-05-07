using System;
using UnityEngine;

public class BuyGoldPanelManager : FuncUIPanel
{
	public GameObject Obj;

	public UILabel lblZuanShi;

	public UILabel lblJinBi;

	public UILabel lblTimes;

	public UILabel lblBtnDesc;

	public static BuyGoldPanelManager ins;

	private float oldTime;

	public void OnDestroy()
	{
		BuyGoldPanelManager.ins = null;
	}

	public override void Awake()
	{
		BuyGoldPanelManager.ins = this;
	}

	public void Refresh()
	{
		BuyGoldManager.GetIns().CheckReset();
		this.lblZuanShi.text = BuyGoldManager.GetIns().GetZuanShiNum() + "钻石";
		this.lblJinBi.text = BuyGoldManager.GetIns().GetJinBiNum() + "金币";
		this.lblTimes.text = string.Concat(new object[]
		{
			BuyGoldManager.GetIns().GetLeftTimes(),
			"/",
			BuyGoldManager.GetIns().GetTotalTimes(),
			"次"
		});
		BuyGoldManager.GetIns().GetButtonStr(this.lblBtnDesc);
	}

	public void Click(BuyGoldBtnType type)
	{
		if (type == BuyGoldBtnType.Close)
		{
			this.Obj.SetActive(false);
			BuyGoldPanelManager.ins = null;
			UnityEngine.Object.Destroy(this.Obj);
		}
		else if (type == BuyGoldBtnType.Buy && BuyGoldManager.GetIns().IsCanCheck())
		{
			if (!BuyGoldManager.GetIns().CheckTimes())
			{
				MessageBox.GetMessagePanel().ShowBtn("次数不足", "次数不足", "次数不足", null);
				return;
			}
			if (!BuyGoldManager.GetIns().CheckZuanShi())
			{
				MessageBox.GetMessagePanel().ShowBtn("钻石不足", "钻石不足", "钻石不足", null);
				return;
			}
			if (!BuyGoldManager.GetIns().CheckFull())
			{
				MessageBox.GetMessagePanel().ShowBtn("温馨提示", "本次兑换金币可能会爆仓，是否还要继续兑换", "是", delegate
				{
					BuyGoldManager.GetIns().Buy();
				}, "否", null);
				return;
			}
			if (Time.time - this.oldTime > 0.5f)
			{
				BuyGoldManager.GetIns().Buy();
				this.oldTime = Time.time;
			}
			else
			{
				LogManage.Log("太快了");
			}
		}
	}
}
