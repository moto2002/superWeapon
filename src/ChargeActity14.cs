using msg;
using System;
using UnityEngine;

public class ChargeActity14 : ChargeRightPanel
{
	public UIScrollView resScroView;

	public UIGrid resGrid;

	public UILabel btnLabel;

	public ButtonClick btnClick;

	public UISprite btnUisprite;

	private ActivityClass curActitvty;

	public void Awake()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Get14, new EventManager.VoidDelegate(this.ChargeActityPnael_Get14));
	}

	private void ChargeActityPnael_Get14(GameObject ga)
	{
		if (ChargeActityPanel.ins.isCanRecieveActityRes(this.curActitvty))
		{
			CSgetActivityPrize cSgetActivityPrize = new CSgetActivityPrize();
			cSgetActivityPrize.activityId = this.curActitvty.activityId;
			ClientMgr.GetNet().SendHttp(2112, cSgetActivityPrize, delegate(bool isError, Opcode opcode)
			{
				ShowAwardPanelManger.showAwardList();
			}, null);
		}
	}

	public override void OnEnable()
	{
		this.curActitvty = ChargeActityPanel.GetRegCharges[14][0];
		this.resGrid.ClearChild();
		ChargeActityPanel.ins.CreateRes(this.resGrid.gameObject, this.curActitvty.curActivityResReward);
		ChargeActityPanel.ins.CreateItem(this.resGrid.gameObject, this.curActitvty.curActivityItemReward);
		ChargeActityPanel.ins.CreateSkill(this.resGrid.gameObject, this.curActitvty.curActivitySkillReward);
		base.StartCoroutine(this.resGrid.RepositionAfterFrame());
		this.resScroView.ResetPosition();
		ChargeActityPanel.ins.SetBtnState(this.curActitvty, this.btnClick, this.btnUisprite, this.btnLabel, true, EventManager.EventType.ChargeActityPnael_Get14, "充值", EventManager.EventType.ChargeActityPnael_Charge);
	}
}
