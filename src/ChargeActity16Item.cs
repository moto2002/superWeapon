using System;

public class ChargeActity16Item : ChargeActityItem
{
	public UIScrollView scrow;

	public UIGrid grid;

	public UILabel text;

	public ButtonClick btnClick;

	public UILabel btnUIlabel;

	public UISprite btnUisprite;

	public override void InitData()
	{
		this.scrow.ResetPosition();
		this.grid.ClearChild();
		ChargeActityPanel.ins.CreateRes(this.grid.gameObject, this.curActity.curActivityResReward);
		ChargeActityPanel.ins.CreateItem(this.grid.gameObject, this.curActity.curActivityItemReward);
		ChargeActityPanel.ins.CreateSkill(this.grid.gameObject, this.curActity.curActivitySkillReward);
		base.StartCoroutine(this.grid.RepositionAfterFrame());
		this.text.text = LanguageManage.GetTextByKey(this.curActity.conditionName, "Activities");
		ChargeActityPanel.ins.SetBtnState(this.curActity, this.btnClick, this.btnUisprite, this.btnUIlabel, true, EventManager.EventType.ChargeActityPnael_Get16, "未达成", EventManager.EventType.none);
	}

	public override void InitDataBySevenDay()
	{
	}
}
