using System;
using UnityEngine;

public class MilitaryOrders : FuncUIPanel
{
	public static MilitaryOrders inst;

	public UILabel m_MilitaryNum;

	public UISprite Exp;

	private int needMoney;

	public void OnDestroy()
	{
		MilitaryOrders.inst = null;
	}

	public override void Awake()
	{
		MilitaryOrders.inst = this;
		this.Initialize();
		this.UpdateMilitary();
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_BuyJunlingBtn, new EventManager.VoidDelegate(this.OnClickAddBtn));
	}

	private void Initialize()
	{
		this.m_MilitaryNum = base.transform.FindChild("Num").GetComponent<UILabel>();
		this.Exp = base.transform.FindChild("Num/Sprite").GetComponent<UISprite>();
	}

	public override void OnEnable()
	{
		base.OnEnable();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.ShowMilitary);
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.ShowMilitary);
		base.OnDisable();
	}

	public void ShowMilitary(int opcodeCMD)
	{
		this.UpdateMilitary();
	}

	public void UpdateMilitary()
	{
		this.m_MilitaryNum.text = string.Format("{0}{1}/{2}", (int.Parse(UnitConst.GetInstance().DesighConfigDic[23].value) <= HeroInfo.GetInstance().playerRes.junLing) ? string.Empty : "[ff0000]", HeroInfo.GetInstance().playerRes.junLing, UnitConst.GetInstance().DesighConfigDic[23].value);
		this.Exp.fillAmount = 0f + (float)HeroInfo.GetInstance().playerRes.junLing / float.Parse(UnitConst.GetInstance().DesighConfigDic[23].value);
	}

	public void OnClickAddBtn(GameObject go)
	{
		if (HeroInfo.GetInstance().playerRes.junLing >= int.Parse(UnitConst.GetInstance().DesighConfigDic[23].value))
		{
			HUDTextTool.inst.SetText("军令已满", HUDTextTool.TextUITypeEnum.Num5);
		}
		else
		{
			for (int i = 0; i < UnitConst.GetInstance().moneyToToken.Count; i++)
			{
				MoneyToToken moneyToToken = UnitConst.GetInstance().moneyToToken[i];
				if (moneyToToken.type == 12 && moneyToToken.times == 1)
				{
					this.needMoney = moneyToToken.money;
				}
			}
			NTollgateManage.inst.m_Notarize.SetActive(true);
			string des = string.Concat(new object[]
			{
				"花费",
				this.needMoney,
				"钻可购买",
				UnitConst.GetInstance().DesighConfigDic[23].value,
				"个军令"
			});
			NTollgateManage.inst.m_Notarize.transform.GetComponent<NotarizeWindowManage>().ShowUI(des, this.needMoney.ToString(), 1, 3);
			if (HeroInfo.GetInstance().playerRes.RMBCoin < int.Parse(NTollgateManage.inst.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.text))
			{
				NTollgateManage.inst.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.transform.GetComponent<UILabel>().color = new Color(0.7647059f, 0.07058824f, 0f);
			}
			else
			{
				NTollgateManage.inst.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.transform.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
			}
		}
	}
}
