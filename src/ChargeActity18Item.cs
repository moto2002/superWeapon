using System;

public class ChargeActity18Item : ChargeActityItem
{
	public UIScrollView scrow;

	public UIGrid grid;

	public UILabel jindu;

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
		this.jindu.text = string.Format("[{2}]{0}[-]/{1}", HeroInfo.GetInstance().playerlevel, this.curActity.conditionID, (HeroInfo.GetInstance().playerlevel >= this.curActity.conditionID) ? "ffffff" : "ff0000");
		string btn_ActyNoEndText;
		if (!HeroInfo.GetInstance().IsBuyChengZhangJiJin)
		{
			btn_ActyNoEndText = "未购买";
		}
		else
		{
			btn_ActyNoEndText = "未达成";
		}
		ChargeActityPanel.ins.SetBtnState(this.curActity, this.btnClick, this.btnUisprite, this.btnUIlabel, true, EventManager.EventType.ChargeActityPnael_Get18, btn_ActyNoEndText, EventManager.EventType.none);
	}

	public override void InitDataBySevenDay()
	{
	}
}
