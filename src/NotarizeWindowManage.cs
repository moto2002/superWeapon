using msg;
using System;
using UnityEngine;

public class NotarizeWindowManage : MonoBehaviour
{
	public UILabel m_Des;

	public UILabel m_NeedNum;

	public UILabel m_AstrictNum;

	public GameObject addNun;

	public GameObject ClosePanel;

	public int m_type;

	public int num;

	private void Awake()
	{
		if (base.gameObject.GetComponent<UITexture>())
		{
			base.gameObject.GetComponent<UITexture>().mainTexture = (Resources.Load("购买军令面板") as Texture);
		}
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_CloseNotarize, new EventManager.VoidDelegate(this.CloseThis));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_addNum, new EventManager.VoidDelegate(this.OnClickShopBtn));
		this.Initialize();
		base.gameObject.SetActive(false);
	}

	private void Start()
	{
		if (HeroInfo.GetInstance().playerRes.RMBCoin < int.Parse(this.m_NeedNum.text))
		{
			this.m_NeedNum.transform.GetComponent<UILabel>().color = new Color(0.7647059f, 0.07058824f, 0f);
		}
	}

	private void Initialize()
	{
		this.m_Des = base.transform.FindChild("Des").GetComponent<UILabel>();
		this.m_NeedNum = base.transform.FindChild("AddBtn/NeedNum").GetComponent<UILabel>();
		this.m_AstrictNum = base.transform.FindChild("AstrictNum").GetComponent<UILabel>();
		this.addNun = base.transform.FindChild("AddBtn").gameObject;
		this.addNun.AddComponent<ButtonClick>();
		ButtonClick component = this.addNun.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.BattlePanel_addNum;
		this.ClosePanel = base.transform.FindChild("CloseBtn").gameObject;
		this.ClosePanel.AddComponent<ButtonClick>();
		ButtonClick component2 = this.ClosePanel.GetComponent<ButtonClick>();
		component2.eventType = EventManager.EventType.BattlePanel_CloseNotarize;
	}

	private void CloseThis(GameObject ga)
	{
		base.gameObject.SetActive(false);
	}

	public void ShowUI(string des, string needNum, int type, int num)
	{
		this.num = num;
		this.m_Des.text = des;
		this.m_NeedNum.text = needNum;
		this.m_type = type;
		this.m_AstrictNum.text = string.Format("今日还可购买{0}次", num);
	}

	public void OnClickShopBtn(GameObject go)
	{
		int type = this.m_type;
		if (type != 1)
		{
			if (type == 2)
			{
				if (HeroInfo.GetInstance().playerRes.tanSuoLing < 30)
				{
					if (HeroInfo.GetInstance().playerRes.RMBCoin < int.Parse(this.m_NeedNum.text))
					{
						HUDTextTool.inst.ShowBuyMoney();
						return;
					}
					if (this.num > 0)
					{
						CSBuySearchToken cSBuySearchToken = new CSBuySearchToken();
						cSBuySearchToken.id = 1;
						ClientMgr.GetNet().SendHttp(1008, cSBuySearchToken, null, null);
					}
					else
					{
						HUDTextTool.inst.SetText("购买次数已达到上限", HUDTextTool.TextUITypeEnum.Num1);
					}
				}
				else
				{
					HUDTextTool.inst.SetText("探索令已满", HUDTextTool.TextUITypeEnum.Num1);
				}
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			if (HeroInfo.GetInstance().playerRes.junLing < 20)
			{
				if (HeroInfo.GetInstance().playerRes.RMBCoin < int.Parse(this.m_NeedNum.text))
				{
					HUDTextTool.inst.ShowBuyMoney();
					base.gameObject.SetActive(false);
					return;
				}
				if (this.num > 0)
				{
					CSBuyArmyToken cSBuyArmyToken = new CSBuyArmyToken();
					cSBuyArmyToken.id = 1;
					ClientMgr.GetNet().SendHttp(1006, cSBuyArmyToken, null, null);
				}
				else
				{
					HUDTextTool.inst.SetText("购买次数已达到上限", HUDTextTool.TextUITypeEnum.Num1);
				}
			}
			else
			{
				HUDTextTool.inst.SetText("军令已满", HUDTextTool.TextUITypeEnum.Num1);
			}
			base.gameObject.SetActive(false);
		}
	}
}
