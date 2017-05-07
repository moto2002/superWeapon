using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargeActityPanel : FuncUIPanel
{
	public static ChargeActityPanel ins;

	public GameObject leftPrefab;

	public GameObject itemPrefab;

	public GameObject skillPrefab;

	public GameObject resPrefab;

	public UIScrollView leftScrollView;

	public UIGrid leftTabel;

	public List<ChargeLeftPrefab> AllChareActypeLeftBtns = new List<ChargeLeftPrefab>();

	public static int curActityTypeIndex;

	public UILabel activetyTittleUIlabe;

	public Transform rightContentParent;

	public static Dictionary<int, List<ActivityClass>> GetRegCharges;

	private ChargeLeftPrefab curLeft;

	public void OnDestroy()
	{
		ChargeActityPanel.ins = null;
		ChargeActityPanel.GetRegCharges = null;
	}

	public override void Awake()
	{
		ChargeActityPanel.ins = this;
		this.leftTabel.isRespositonOnStart = false;
		this.InitEvent();
		if (ChargeActityPanel.GetRegCharges == null || ChargeActityPanel.GetRegCharges.Count == 0)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("活动已关闭", "Activities"), HUDTextTool.TextUITypeEnum.Num5);
			this.CloseThisPanel(null);
		}
		else
		{
			this.InitLeftActivityType();
		}
	}

	public void SetBtnState(ActivityClass acty, ButtonClick btnClick, UISprite btnSprite, UILabel btnLabel, bool isYellow, EventManager.EventType GetEnum, string btn_ActyNoEndText = "充值", EventManager.EventType IsBuyRMB = EventManager.EventType.ChargeActityPnael_Charge)
	{
		btnClick.gameObject.name = acty.activityId.ToString();
		if (acty.isReceived)
		{
			btnSprite.spriteName = "hui";
			btnClick.eventType = EventManager.EventType.none;
			btnLabel.text = LanguageManage.GetTextByKey("已领取", "Activities");
		}
		else if (acty.isCanGetAward)
		{
			if (isYellow)
			{
				btnSprite.spriteName = "yellow";
			}
			else
			{
				btnSprite.spriteName = "blue";
			}
			btnClick.eventType = GetEnum;
			btnLabel.text = LanguageManage.GetTextByKey("领取", "Activities");
			if (isYellow)
			{
				btnLabel.effectStyle = UILabel.Effect.Outline;
				btnLabel.effectColor = new Color(0.6666667f, 0.403921574f, 0.0784313753f);
			}
		}
		else
		{
			if (isYellow)
			{
				btnSprite.spriteName = "yellow";
			}
			else
			{
				btnSprite.spriteName = "blue";
			}
			btnLabel.text = LanguageManage.GetTextByKey(btn_ActyNoEndText, "Activities");
			if (btn_ActyNoEndText == "未达成")
			{
				btnSprite.spriteName = "hui";
			}
			if (acty.isPaying)
			{
				btnClick.eventType = EventManager.EventType.none;
				btnSprite.spriteName = "hui";
				btnLabel.text = LanguageManage.GetTextByKey("充值中", "Activities");
			}
			else if (acty.shopID > 0 && IsBuyRMB == EventManager.EventType.ChargeActityPnael_Charge)
			{
				btnClick.eventType = EventManager.EventType.ChargeActityPnael_Charge_NotByShop;
			}
			else
			{
				btnClick.eventType = IsBuyRMB;
			}
			if (btnClick.transform.parent && btnClick.transform.parent.parent && btnClick.transform.parent.parent.parent && btnClick.transform.parent.parent.parent.parent && btnClick.transform.parent.parent.parent.parent.GetComponent<ChargeActity12>() && btnClick.tr.parent.parent.parent.parent.GetComponent<ChargeActity12>().NewActity != 0)
			{
				btnSprite.spriteName = "hui";
				btnClick.eventType = EventManager.EventType.none;
				btnLabel.text = LanguageManage.GetTextByKey("未达成", "Activities");
			}
		}
	}

	public bool isCanRecieveActityRes(ActivityClass cueActivety)
	{
		int coin = 0;
		int oil = 0;
		int steel = 0;
		int earth = 0;
		if (cueActivety.curActivityResReward.ContainsKey(ResType.金币))
		{
			coin = cueActivety.curActivityResReward[ResType.金币];
		}
		if (cueActivety.curActivityResReward.ContainsKey(ResType.石油))
		{
			oil = cueActivety.curActivityResReward[ResType.石油];
		}
		if (cueActivety.curActivityResReward.ContainsKey(ResType.钢铁))
		{
			steel = cueActivety.curActivityResReward[ResType.钢铁];
		}
		if (cueActivety.curActivityResReward.ContainsKey(ResType.稀矿))
		{
			earth = cueActivety.curActivityResReward[ResType.稀矿];
		}
		return !SenceManager.inst.NoResSpace(coin, oil, steel, earth, true);
	}

	public override void OnEnable()
	{
		if (this.AllChareActypeLeftBtns.Count > 0)
		{
			this.ShowChargeActive(this.AllChareActypeLeftBtns[ChargeActityPanel.curActityTypeIndex]);
		}
		base.OnEnable();
	}

	public void CreateRes(GameObject parentGa, Dictionary<ResType, int> reses)
	{
		foreach (KeyValuePair<ResType, int> current in reses)
		{
			GameObject gameObject = NGUITools.AddChild(parentGa.gameObject, ChargeActityPanel.ins.resPrefab);
			ActivityRes component = gameObject.GetComponent<ActivityRes>();
			AtlasManage.SetQuilitySpriteName(gameObject.transform.FindChild("BG").GetComponent<UISprite>(), Quility_ResAndItemAndSkill.绿);
			AtlasManage.SetResSpriteName(component.icon, current.Key);
			component.count.text = current.Value.ToString();
			ItemTipsShow2 component2 = gameObject.GetComponent<ItemTipsShow2>();
			component2.Index = (int)current.Key;
			component2.Type = 2;
		}
	}

	public void CreateMoney(GameObject parentGa, int Money)
	{
		if (Money > 0)
		{
			GameObject gameObject = NGUITools.AddChild(parentGa.gameObject, ChargeActityPanel.ins.resPrefab);
			ActivityRes component = gameObject.GetComponent<ActivityRes>();
			AtlasManage.SetQuilitySpriteName(gameObject.transform.FindChild("BG").GetComponent<UISprite>(), Quility_ResAndItemAndSkill.绿);
			AtlasManage.SetResSpriteName(component.icon, ResType.钻石);
			component.count.text = Money.ToString();
			ItemTipsShow2 component2 = gameObject.GetComponent<ItemTipsShow2>();
			component2.Index = 7;
			component2.Type = 2;
		}
	}

	public void CreateItem(GameObject parentGa, Dictionary<int, int> items)
	{
		foreach (KeyValuePair<int, int> current in items)
		{
			GameObject gameObject = NGUITools.AddChild(parentGa, this.itemPrefab);
			ActivityItemPre component = gameObject.GetComponent<ActivityItemPre>();
			AtlasManage.SetUiItemAtlas(component.icon, UnitConst.GetInstance().ItemConst[current.Key].IconId);
			AtlasManage.SetQuilitySpriteName(component.quality, UnitConst.GetInstance().ItemConst[current.Key].Quality);
			component.count.text = string.Format("X{0}", current.Value);
			ItemTipsShow2 component2 = component.GetComponent<ItemTipsShow2>();
			component2.Index = current.Key;
			component2.Type = 1;
		}
	}

	public void CreateSkill(GameObject parentGa, Dictionary<int, int> skills)
	{
		foreach (KeyValuePair<int, int> current in skills)
		{
			GameObject gameObject = NGUITools.AddChild(parentGa, this.skillPrefab);
			ActivitySkillPrefab component = gameObject.GetComponent<ActivitySkillPrefab>();
			AtlasManage.SetSkillSpritName(component.icon, UnitConst.GetInstance().skillList[current.Key].icon);
			AtlasManage.SetQuilitySpriteName(component.bg, UnitConst.GetInstance().skillList[current.Key].skillQuality);
			component.name.text = UnitConst.GetInstance().skillList[current.Key].name;
			switch (UnitConst.GetInstance().skillList[current.Key].skillQuality)
			{
			case Quility_ResAndItemAndSkill.白:
				component.name.color = Color.white;
				break;
			case Quility_ResAndItemAndSkill.绿:
				component.name.color = new Color(0.243137255f, 0.8862745f, 0.117647059f);
				break;
			case Quility_ResAndItemAndSkill.蓝:
				component.name.color = new Color(0.007843138f, 0.8039216f, 1f);
				break;
			case Quility_ResAndItemAndSkill.紫:
				component.name.color = new Color(0.7372549f, 0.007843138f, 0.870588243f);
				break;
			case Quility_ResAndItemAndSkill.红:
				component.name.color = new Color(1f, 0.007843138f, 0.09019608f);
				break;
			}
			ItemTipsShow2 component2 = gameObject.GetComponent<ItemTipsShow2>();
			component2.Index = current.Key;
			component2.Type = 3;
		}
	}

	public void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPanel_Close, new EventManager.VoidDelegate(this.CloseThisPanel));
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPanel_LeftBtnClick, new EventManager.VoidDelegate(this.LeftBtnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Charge, new EventManager.VoidDelegate(this.ChareMoney));
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_Charge_NotByShop, new EventManager.VoidDelegate(this.ChareMoney_NotByShop));
		EventManager.Instance.AddEvent(EventManager.EventType.ChargeActityPnael_RecieveActity_SingleType, new EventManager.VoidDelegate(this.ChargeActityPnael_RecieveActity_SingleType));
	}

	private void ChareMoney(GameObject ga)
	{
		HUDTextTool.isGetChareAward = false;
		FuncUIManager.inst.OpenFuncUI("ShopPanel", SenceType.Island);
	}

	private void ChareMoney_NotByShop(GameObject ga)
	{
		int actyID = int.Parse(ga.name);
		ActivityClass activityClass = ChargeActityPanel.GetRegCharges[this.curLeft.actityType].Single((ActivityClass a) => a.activityId == actyID);
		if (activityClass.shopID > 0)
		{
			if (HeroInfo.GetInstance().ActivitiesData_RecieveState.ContainsKey(activityClass.activityId))
			{
				HeroInfo.GetInstance().ActivitiesData_RecieveState[activityClass.activityId] = 3;
			}
			else
			{
				HeroInfo.GetInstance().ActivitiesData_RecieveState.Add(activityClass.activityId, 3);
			}
			ShopHandler.CS_ShopBuyRMB(activityClass.shopID, activityClass.activityId, delegate
			{
				this.AllChareActypeLeftBtns[ChargeActityPanel.curActityTypeIndex].RightPanel.GetComponent<ChargeRightPanel>().OnEnable();
			});
		}
	}

	private void ChargeActityPnael_RecieveActity_SingleType(GameObject ga)
	{
		int actyID = int.Parse(ga.name);
		ActivityClass activityClass = ChargeActityPanel.GetRegCharges[this.curLeft.actityType].Single((ActivityClass a) => a.activityId == actyID);
		if (ChargeActityPanel.ins.isCanRecieveActityRes(activityClass))
		{
			CSgetActivityPrize cSgetActivityPrize = new CSgetActivityPrize();
			cSgetActivityPrize.activityId = activityClass.activityId;
			ClientMgr.GetNet().SendHttp(2112, cSgetActivityPrize, delegate(bool isError, Opcode opcode)
			{
				ShowAwardPanelManger.showAwardList();
			}, null);
		}
	}

	private void CloseThisPanel(GameObject ga)
	{
		ChargeActityPanel.curActityTypeIndex = 0;
		HUDTextTool.isGetChareAward = true;
		FuncUIManager.inst.DestoryFuncUI("ChargeActityPanel");
	}

	private void LeftBtnClick(GameObject ga)
	{
		ChargeLeftPrefab component = ga.GetComponent<ChargeLeftPrefab>();
		this.ShowChargeActive(component);
	}

	private void ShowChargeActive(ChargeLeftPrefab leftBtn)
	{
		this.curLeft = leftBtn;
		ChargeActityPanel.curActityTypeIndex = this.AllChareActypeLeftBtns.FindIndex((ChargeLeftPrefab a) => a.Equals(leftBtn));
		this.ShowLeftState(leftBtn.actityType);
		if (leftBtn.actityType == 1 || leftBtn.actityType == 4 || leftBtn.actityType == 5 || leftBtn.actityType == 6 || leftBtn.actityType == 7 || leftBtn.actityType == 9)
		{
			this.activetyTittleUIlabe.text = LanguageManage.GetTextByKey("活动", "Activities");
		}
		else
		{
			this.activetyTittleUIlabe.text = ChargeActityPanel.GetRegCharges[leftBtn.actityType][0].tittleName;
		}
		if (!leftBtn.RightPanel)
		{
			UnityEngine.Object @object = Resources.Load("UI/ChargeActityPanels/ChargeActity" + leftBtn.actityType);
			if (@object == null)
			{
				@object = Resources.Load("UI/ChargeActityPanels/ChargeActity12");
				ChargeActity12 component = ((GameObject)@object).GetComponent<ChargeActity12>();
				component.SetNewActity(leftBtn.actityType);
				this.activetyTittleUIlabe.text = LanguageManage.GetTextByKey("活动", "Activities");
				component.tittleUIlabel.gameObject.SetActive(false);
				component.contentUIlabel.gameObject.SetActive(false);
				component.NewTitleLabel.gameObject.SetActive(true);
				component.NewContentLabel.gameObject.SetActive(true);
				component.NewTitleLabel.text = LanguageManage.GetTextByKey(ChargeActityPanel.GetRegCharges[leftBtn.actityType][0].tittleName, "Activities");
				foreach (ActivityClass current in HeroInfo.GetInstance().activityClass[leftBtn.actityType])
				{
					component.NewContentLabel.text = string.Concat(new string[]
					{
						LanguageManage.GetTextByKey("活动时间", "Activities"),
						"\n",
						current.startTimeStr.ToString("yyyy/MM/dd"),
						"-",
						current.endTimeStr.ToString("yyyy/MM/dd")
					});
				}
			}
			else if (leftBtn.actityType == 12)
			{
				ChargeActity12 component2 = ((GameObject)@object).GetComponent<ChargeActity12>();
				component2.SetNewActity(leftBtn.actityType);
				component2.tittleUIlabel.gameObject.SetActive(true);
				component2.contentUIlabel.gameObject.SetActive(true);
				component2.NewTitleLabel.gameObject.SetActive(false);
				component2.NewContentLabel.gameObject.SetActive(false);
			}
			if (@object)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(@object) as GameObject;
				gameObject.transform.parent = this.rightContentParent;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				leftBtn.RightPanel = gameObject;
			}
		}
	}

	public void InitLeftActivityType()
	{
		this.AllChareActypeLeftBtns.Clear();
		this.leftScrollView.ResetPosition();
		int num = 0;
		foreach (KeyValuePair<int, List<ActivityClass>> current in ChargeActityPanel.GetRegCharges)
		{
			GameObject gameObject = NGUITools.AddChild(this.leftTabel.gameObject, this.leftPrefab);
			ChargeLeftPrefab component = gameObject.GetComponent<ChargeLeftPrefab>();
			component.tr.localPosition = new Vector3(-300f, this.leftTabel.cellHeight * (float)(-(float)num));
			gameObject.name = current.Key.ToString();
			component.activtiyName.text = LanguageManage.GetTextByKey(current.Value[0].btnName, "Activities");
			component.isShowRed(current.Key);
			component.tr.DOLocalMoveX(0f, 0.16f, false).SetDelay(0.08f * (float)num);
			num++;
			this.AllChareActypeLeftBtns.Add(component);
		}
	}

	public void ShowLeftState(int type)
	{
		foreach (ChargeLeftPrefab current in this.AllChareActypeLeftBtns)
		{
			if (current.actityType == type)
			{
				current.SelectSprite.SetActive(true);
				current.BtnClick.enabled = false;
				current.BtnScale.enabled = false;
				if (current.RightPanel)
				{
					current.RightPanel.SetActive(true);
				}
				current.activtiyName.color = new Color(1f, 1f, 1f);
			}
			else
			{
				current.SelectSprite.SetActive(false);
				current.BtnClick.enabled = true;
				current.BtnScale.enabled = true;
				if (current.RightPanel)
				{
					current.RightPanel.SetActive(false);
				}
				current.activtiyName.color = new Color(0.996078432f, 8.568627f, 0.419607848f);
			}
		}
	}
}
