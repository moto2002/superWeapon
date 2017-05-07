using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoxPanel : FuncUIPanel
{
	private bool startTimeCD;

	public UISprite timeUISprite;

	public UILabel timeUILabel;

	public DateTime beginTime;

	public DateTime endTime;

	public UILabel timeUIBuyRMB;

	public UITable itemTable;

	public Transform boxSprite;

	public static BattleBoxPanel inst;

	[HideInInspector]
	public GameObject ga;

	[HideInInspector]
	public Transform tr;

	public Transform Bac;

	public GameObject resPrefab;

	public GameObject itemPrefab;

	public GameObject skillPrefab;

	public GameObject mianfei;

	public GameObject Timeend;

	private bool isCanBack;

	public static int OpenBoxQuality;

	private T_Island island_Sel;

	private int boxItemId;

	private Body_Model battleBox;

	public void OnDestroy()
	{
		BattleBoxPanel.inst = null;
	}

	public void FixedUpdate()
	{
		if (this.Timeend.activeSelf && this.startTimeCD)
		{
			if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime) > 0.0)
			{
				if (this.timeUISprite)
				{
					this.timeUISprite.fillAmount = (float)(TimeTools.DateDiffToDouble(this.beginTime, TimeTools.GetNowTimeSyncServerToDateTime()) / TimeTools.DateDiffToDouble(this.beginTime, this.endTime));
				}
				int rmbNum = ResourceMgr.GetRmbNum(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime));
				this.timeUIBuyRMB.text = string.Format("[{0}]{1}", (rmbNum <= HeroInfo.GetInstance().playerRes.RMBCoin) ? "ffffff" : "ff0000", rmbNum);
				this.timeUILabel.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime);
			}
			else
			{
				this.startTimeCD = false;
				this.Timeend.SetActive(false);
				this.mianfei.SetActive(true);
			}
		}
	}

	public override void Awake()
	{
		BattleBoxPanel.inst = this;
		this.ga = base.gameObject;
		this.tr = base.transform;
		this.InitEvent();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.BattleBoxBack, new EventManager.VoidDelegate(this.Back));
		EventManager.Instance.AddEvent(EventManager.EventType.BattleBoxPickup, new EventManager.VoidDelegate(this.PickUpBox));
	}

	private void Back(GameObject go)
	{
		if (!this.isCanBack)
		{
			return;
		}
		FuncUIManager.inst.DestoryFuncUI("BattleBoxPanel");
	}

	private void PickUpBox(GameObject ga)
	{
		BattleFieldBox.BattleBoxConst battleBoxConst = BattleFieldBox.BattleBox_PlannerData[this.boxItemId];
		if (this.startTimeCD)
		{
			ExpensePanelManage.ClearCache();
			int rmbNum = ResourceMgr.GetRmbNum(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(UnitConst.GetInstance().BattleConst[this.island_Sel.battleItem.id].EndBattleBoxTime)));
			ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("立即结束冷却", "ResIsland") + ":" + TimeTools.ConvertFloatToTimeByMilliseconds(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(UnitConst.GetInstance().BattleConst[this.island_Sel.battleItem.id].EndBattleBoxTime))));
			MessageBox.GetExpensePanel().Show(0, TimeTools.ConvertLongDateTime(UnitConst.GetInstance().BattleConst[this.island_Sel.battleItem.id].EndBattleBoxTime), delegate(bool isbuy, int rmb)
			{
				if (isbuy)
				{
					if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(UnitConst.GetInstance().BattleConst[this.island_Sel.battleItem.id].EndBattleBoxTime)) > 0.0)
					{
						if (HeroInfo.GetInstance().playerRes.RMBCoin < ResourceMgr.GetRmbNum(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(UnitConst.GetInstance().BattleConst[this.island_Sel.battleItem.id].EndBattleBoxTime))))
						{
							HUDTextTool.inst.ShowBuyMoney();
							return;
						}
						this.PickUpBattleFieldBoxCS(ResourceMgr.GetRmbNum(TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(UnitConst.GetInstance().BattleConst[this.island_Sel.battleItem.id].EndBattleBoxTime))));
					}
					else
					{
						this.PickUpBattleFieldBoxCS(0);
					}
				}
			});
		}
		else
		{
			this.PickUpBattleFieldBoxCS(0);
		}
	}

	private void PickUpBattleFieldBoxCS(int money)
	{
		CSOpenBattleBox cSOpenBattleBox = new CSOpenBattleBox();
		cSOpenBattleBox.battleId = this.island_Sel.battleItem.id;
		cSOpenBattleBox.itemId = this.boxItemId;
		cSOpenBattleBox.money = money;
		ClientMgr.GetNet().SendHttp(5026, cSOpenBattleBox, new DataHandler.OpcodeHandler(this.OpenBattleFieldBoxCallBack), null);
	}

	private void OpenBattleFieldBoxCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
			ShowAwardPanelManger._ins.CloseCallBack = new Action(this.ShowTislandInfo);
		}
	}

	public void ShowTislandInfo()
	{
		if (this.island_Sel)
		{
			this.island_Sel.ResetInfo();
		}
		this.Back(null);
	}

	public static void ShowBattleBox(int boxId, T_Island island)
	{
		FuncUIManager.inst.OpenFuncUI("BattleBoxPanel", SenceType.WorldMap);
		BattleBoxPanel.inst.ShowBattle(boxId, island);
	}

	public void ShowBattle(int boxID, T_Island island)
	{
		this.island_Sel = island;
		this.isCanBack = false;
		this.Bac.localPosition = new Vector3(-2600f, 0f, 0f);
		this.boxItemId = boxID;
		BattleFieldBox.BattleBoxConst battleBoxConst = BattleFieldBox.BattleBox_PlannerData[boxID];
		this.itemTable.DestoryChildren(true);
		if (!this.battleBox)
		{
			this.battleBox = PoolManage.Ins.GetModelByBundleByName("case", this.boxSprite);
			this.battleBox.tr.localRotation = Quaternion.Euler(0f, 85.8f, -15f);
			this.battleBox.tr.localScale = new Vector3(60f, 60f, 60f);
			this.boxSprite.gameObject.AddComponent<TweenPosition>();
			TweenPosition component = this.boxSprite.GetComponent<TweenPosition>();
			component.from = new Vector3(0f, 203f, 0f);
			component.to = new Vector3(0f, 208f, 0f);
			component.duration = 0.5f;
			component.style = UITweener.Style.PingPong;
		}
		Transform transform = this.battleBox.tr.FindChild("case_w");
		Transform transform2 = this.battleBox.tr.FindChild("case_y");
		Transform transform3 = this.battleBox.tr.FindChild("case_g");
		transform3.gameObject.SetActive(battleBoxConst.quility == 1);
		transform2.gameObject.SetActive(battleBoxConst.quility == 3);
		transform.gameObject.SetActive(battleBoxConst.quility == 2);
		this.battleBox.tr.localPosition = new Vector3(0f, -58.56f, -500f);
		Transform[] componentsInChildren = this.battleBox.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform4 = componentsInChildren[i];
			transform4.gameObject.layer = 5;
		}
		if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(this.island_Sel.battleItem.EndBattleBoxTime)) > 0.0)
		{
			this.mianfei.SetActive(false);
			this.Timeend.SetActive(true);
			this.beginTime = TimeTools.GetNowTimeSyncServerToDateTime();
			this.endTime = TimeTools.ConvertLongDateTime(this.island_Sel.battleItem.EndBattleBoxTime);
			this.startTimeCD = true;
		}
		else
		{
			this.mianfei.SetActive(true);
			this.Timeend.SetActive(false);
		}
		foreach (KeyValuePair<ResType, int> current in battleBoxConst.res_View)
		{
			ItemUnit component2 = NGUITools.AddChild(this.itemTable.gameObject, this.resPrefab).GetComponent<ItemUnit>();
			AtlasManage.SetResSpriteName(component2.itemSprite, current.Key);
			AtlasManage.SetQuilitySpriteName(component2.itemQuSprite, (Quility_ResAndItemAndSkill)battleBoxConst.res_ViewQuility[current.Key]);
		}
		foreach (KeyValuePair<int, int> current2 in battleBoxConst.items_View)
		{
			ItemUnit component3 = NGUITools.AddChild(this.itemTable.gameObject, this.itemPrefab).GetComponent<ItemUnit>();
			AtlasManage.SetUiItemAtlas(component3.itemSprite, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
			AtlasManage.SetQuilitySpriteName(component3.itemQuSprite, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
			ItemTipsShow2 component4 = component3.GetComponent<ItemTipsShow2>();
			component4.Index = current2.Key;
			component4.Type = 1;
		}
		foreach (KeyValuePair<int, int> current3 in battleBoxConst.skills_View)
		{
			ItemUnit component5 = NGUITools.AddChild(this.itemTable.gameObject, this.skillPrefab).GetComponent<ItemUnit>();
			component5.itemSprite.spriteName = UnitConst.GetInstance().skillList[current3.Key].icon;
			AtlasManage.SetQuilitySpriteName(component5.itemQuSprite, UnitConst.GetInstance().skillList[current3.Key].skillQuality);
		}
		this.itemTable.Reposition();
		this.Bac.DOLocalMoveX(-246.2f, 0.2f, false).OnComplete(delegate
		{
			this.Bac.DOPunchPosition(new Vector3(10f, 0f, 0f), 0.3f, 20, 3f, false);
			this.isCanBack = true;
		});
	}
}
