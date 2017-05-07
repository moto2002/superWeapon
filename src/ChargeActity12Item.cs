using System;
using System.Collections.Generic;

public class ChargeActity12Item : ChargeActityItem
{
	public UIScrollView scrow;

	public UIGrid grid;

	public UILabel text;

	public ButtonClick btnClick;

	public UILabel btnUIlabel;

	public UISprite btnUisprite;

	public UILabel NumLabel;

	public override void InitData()
	{
		this.scrow.ResetPosition();
		this.grid.ClearChild();
		ChargeActityPanel.ins.CreateRes(this.grid.gameObject, this.curActity.curActivityResReward);
		ChargeActityPanel.ins.CreateItem(this.grid.gameObject, this.curActity.curActivityItemReward);
		ChargeActityPanel.ins.CreateSkill(this.grid.gameObject, this.curActity.curActivitySkillReward);
		base.StartCoroutine(this.grid.RepositionAfterFrame());
		this.text.text = LanguageManage.GetTextByKey(this.curActity.conditionName, "Activities");
		ChargeActityPanel.ins.SetBtnState(this.curActity, this.btnClick, this.btnUisprite, this.btnUIlabel, true, EventManager.EventType.ChargeActityPnael_Get12, "充值", EventManager.EventType.ChargeActityPnael_Charge);
	}

	public override void InitDataBySevenDay()
	{
		this.scrow.ResetPosition();
		this.grid.ClearChild();
		if (UnitConst.GetInstance().SevenDay[this.curDay.id].goldBox == 1)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			dictionary.Add(107, 1);
			ChargeActityPanel.ins.CreateItem(this.grid.gameObject, dictionary);
		}
		else
		{
			ChargeActityPanel.ins.CreateRes(this.grid.gameObject, this.curDay.res);
			ChargeActityPanel.ins.CreateItem(this.grid.gameObject, this.curDay.items);
			ChargeActityPanel.ins.CreateSkill(this.grid.gameObject, this.curDay.skill);
			ChargeActityPanel.ins.CreateMoney(this.grid.gameObject, this.curDay.money);
		}
		base.StartCoroutine(this.grid.RepositionAfterFrame());
		this.text.text = LanguageManage.GetTextByKey(this.curDay.name, "Activities");
		this.btnClick.gameObject.name = this.curDay.id.ToString();
		for (int i = 0; i < SevenDayMgr.state.Length; i++)
		{
			if (i + 1 == this.curDay.id)
			{
				if (SevenDayMgr.state[i] == 0)
				{
					this.btnUisprite.spriteName = "blue";
					this.btnClick.eventType = EventManager.EventType.ActivityPanelManager_GetAward;
					this.btnUIlabel.text = LanguageManage.GetTextByKey("领取", "Activities");
					return;
				}
				if (SevenDayMgr.state[i] == 1)
				{
					this.btnUisprite.spriteName = "hui";
					this.btnClick.eventType = EventManager.EventType.none;
					this.btnUIlabel.text = LanguageManage.GetTextByKey("已领取", "Activities");
					return;
				}
				this.btnUisprite.spriteName = "hui";
				this.btnClick.eventType = EventManager.EventType.none;
				this.btnUIlabel.text = LanguageManage.GetTextByKey("未达成", "Activities");
			}
		}
	}
}
