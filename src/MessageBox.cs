using System;
using UnityEngine;

public class MessageBox
{
	public static MessagePanelManage GetMessagePanel()
	{
		if (MessagePanelManage.inst == null)
		{
			return (UnityEngine.Object.Instantiate(Resources.Load("UI/MessageBox")) as GameObject).GetComponentInChildren<MessagePanelManage>();
		}
		MessagePanelManage.inst.gameObject.SetActive(true);
		return MessagePanelManage.inst;
	}

	public static NetMessagePanel GetNetMessagePanel()
	{
		if (NetMessagePanel.inst == null)
		{
			return (UnityEngine.Object.Instantiate(Resources.Load("UI/Net_MessageBox")) as GameObject).GetComponentInChildren<NetMessagePanel>();
		}
		NetMessagePanel.inst.gameObject.SetActive(true);
		return NetMessagePanel.inst;
	}

	public static ExpensePanelManage GetExpensePanel()
	{
		if (ExpensePanelManage.inst != null)
		{
			ExpensePanelManage.inst.gameObject.SetActive(true);
			return ExpensePanelManage.inst;
		}
		return (UnityEngine.Object.Instantiate(Resources.Load("UI/ExpensePanel")) as GameObject).GetComponentInChildren<ExpensePanelManage>();
	}

	public static BuyGoldPanelManager GetBuyGoldPanel()
	{
		if (!BuyGoldManager.GetIns().CheckTimes())
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金币购买次数不足", "ResIsland"), HUDTextTool.TextUITypeEnum.Num5);
			return null;
		}
		if (BuyGoldPanelManager.ins == null)
		{
			return (UnityEngine.Object.Instantiate(Resources.Load("UI/BuyGoldPanel")) as GameObject).GetComponentInChildren<BuyGoldPanelManager>();
		}
		BuyGoldPanelManager.ins.gameObject.SetActive(true);
		return BuyGoldPanelManager.ins;
	}

	public static TalkManager GetTalkPanel()
	{
		return (UnityEngine.Object.Instantiate(Resources.Load("UI/TalkPanel")) as GameObject).GetComponentInChildren<TalkManager>();
	}
}
