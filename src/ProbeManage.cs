using System;
using UnityEngine;

public class ProbeManage : MonoBehaviour
{
	public UILabel m_ProdeNum;

	public GameObject m_Notarize;

	public UISprite Exp;

	private int needMoney;

	public void Awake()
	{
		base.gameObject.SetActive(true);
		this.UpdateMilitary();
	}

	public void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.ShowMilitary);
	}

	private void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.ShowMilitary);
	}

	public void ShowMilitary(int opcodeCMD)
	{
		if (opcodeCMD == 10003)
		{
			this.UpdateMilitary();
		}
	}

	public void UpdateMilitary()
	{
		this.m_ProdeNum.text = string.Format("{0}{1}/{2}", (int.Parse(UnitConst.GetInstance().DesighConfigDic[24].value) <= HeroInfo.GetInstance().playerRes.tanSuoLing) ? string.Empty : "[ff0000]", HeroInfo.GetInstance().playerRes.tanSuoLing, UnitConst.GetInstance().DesighConfigDic[24].value);
		this.Exp.fillAmount = 0f + (float)HeroInfo.GetInstance().playerRes.tanSuoLing / float.Parse(UnitConst.GetInstance().DesighConfigDic[24].value);
	}

	public void OnClickAddBtn()
	{
		for (int i = 0; i < UnitConst.GetInstance().moneyToToken.Count; i++)
		{
			MoneyToToken moneyToToken = UnitConst.GetInstance().moneyToToken[i];
			if (moneyToToken.type == 15 && moneyToToken.times == HeroInfo.GetInstance().buyArmyTokenTimes + 1)
			{
				this.needMoney = moneyToToken.money;
			}
			if (HeroInfo.GetInstance().buyArmyTokenTimes == 3 && moneyToToken.type == 15 && moneyToToken.times == HeroInfo.GetInstance().buyArmyTokenTimes)
			{
				this.needMoney = moneyToToken.money;
			}
		}
		this.m_Notarize.SetActive(true);
		string des = string.Concat(new object[]
		{
			"花费",
			this.needMoney,
			"钻可购买",
			UnitConst.GetInstance().DesighConfigDic[24].value,
			"个探索令"
		});
		this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().ShowUI(des, this.needMoney.ToString(), 2, 3);
		if (HeroInfo.GetInstance().playerRes.RMBCoin < int.Parse(this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.text))
		{
			this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.transform.GetComponent<UILabel>().color = new Color(0.7647059f, 0.07058824f, 0f);
		}
		else
		{
			this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.transform.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
		}
	}
}
