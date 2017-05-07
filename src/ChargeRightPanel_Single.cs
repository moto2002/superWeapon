using System;

public class ChargeRightPanel_Single : ChargeRightPanel
{
	public UIScrollView resScroView;

	public UIGrid resGrid;

	public ButtonClick btnClick;

	public UISprite btnUiSprite;

	public UILabel actityLbabel;

	public UILabel btnUIlabel;

	protected virtual ActivityClass curActitvty
	{
		get;
		set;
	}

	public override void OnEnable()
	{
		this.resGrid.ClearChild();
		this.actityLbabel.text = LanguageManage.GetTextByKey(this.curActitvty.conditionName, "Activities");
		ChargeActityPanel.ins.CreateRes(this.resGrid.gameObject, this.curActitvty.curActivityResReward);
		ChargeActityPanel.ins.CreateItem(this.resGrid.gameObject, this.curActitvty.curActivityItemReward);
		ChargeActityPanel.ins.CreateSkill(this.resGrid.gameObject, this.curActitvty.curActivitySkillReward);
		base.StartCoroutine(this.resGrid.RepositionAfterFrame());
		this.resScroView.ResetPosition();
		ChargeActityPanel.ins.SetBtnState(this.curActitvty, this.btnClick, this.btnUiSprite, this.btnUIlabel, true, EventManager.EventType.ChargeActityPnael_RecieveActity_SingleType, "充值", EventManager.EventType.ChargeActityPnael_Charge);
	}
}
