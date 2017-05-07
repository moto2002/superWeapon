using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleFieldBoxPanel : FuncUIPanel
{
	public UITable itemTable;

	public Transform Btn;

	public Transform boxSprite;

	public UILabel boxName;

	public UISprite boxKeySprite;

	public ShowBuyKey buyKey;

	public static BattleFieldBoxPanel inst;

	[HideInInspector]
	public GameObject ga;

	[HideInInspector]
	public Transform tr;

	public Transform Bac;

	public GameObject resPrefab;

	public GameObject itemPrefab;

	public GameObject skillPrefab;

	public GameObject lingQuLabel;

	private bool isCanBack;

	public static int OpenBoxQuality;

	private int boxItemId;

	private Body_Model battleBox;

	public void OnDestroy()
	{
		BattleFieldBoxPanel.inst = null;
	}

	public override void Awake()
	{
		BattleFieldBoxPanel.inst = this;
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
		FuncUIManager.inst.DestoryFuncUI("BattleFieldBoxPanel");
	}

	private void PickUpBox(GameObject ga)
	{
		BattleFieldBox.BattleFieldBoxConst battleFieldBoxConst = BattleFieldBox.BattleFieldBox_PlannerData[this.boxItemId];
		if (battleFieldBoxConst.keyId > 0)
		{
			if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(battleFieldBoxConst.keyId) && HeroInfo.GetInstance().PlayerItemInfo[battleFieldBoxConst.keyId] > 0)
			{
				AtlasManage.SetUiItemAtlas(this.boxKeySprite, UnitConst.GetInstance().ItemConst[battleFieldBoxConst.keyId].IconId);
				this.lingQuLabel.SetActive(false);
			}
			else
			{
				if (UnitConst.GetInstance().ItemConst[battleFieldBoxConst.keyId].ConvertMoney > 0)
				{
					this.buyKey.gameObject.SetActive(true);
					this.buyKey.BuyKeyInfo(string.Concat(new object[]
					{
						LanguageManage.GetTextByKey("是否花费", "others"),
						UnitConst.GetInstance().ItemConst[battleFieldBoxConst.keyId].ConvertMoney,
						LanguageManage.GetTextByKey("钻石购买钥匙", "others"),
						"?"
					}), UnitConst.GetInstance().ItemConst[battleFieldBoxConst.keyId].ConvertMoney, delegate
					{
						this.PickUpByRMB();
					}, null);
					return;
				}
				HUDTextTool.inst.SetText("钥匙不足", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		this.PickUpBattleFieldBoxCS(0);
	}

	private void PickUpByRMB()
	{
		if (HeroInfo.GetInstance().playerRes.RMBCoin < UnitConst.GetInstance().ItemConst[BattleFieldBox.BattleFieldBox_PlannerData[this.boxItemId].keyId].ConvertMoney)
		{
			HUDTextTool.inst.ShowBuyMoney();
		}
		else
		{
			this.PickUpBattleFieldBoxCS(UnitConst.GetInstance().ItemConst[BattleFieldBox.BattleFieldBox_PlannerData[this.boxItemId].keyId].ConvertMoney);
		}
	}

	private void PickUpBattleFieldBoxCS(int money)
	{
		CSOpenBattleFieldBox cSOpenBattleFieldBox = new CSOpenBattleFieldBox();
		cSOpenBattleFieldBox.itemId = this.boxItemId;
		cSOpenBattleFieldBox.money = money;
		ClientMgr.GetNet().SendHttp(5024, cSOpenBattleFieldBox, new DataHandler.OpcodeHandler(this.OpenBattleFieldBoxCallBack), null);
	}

	private void OpenBattleFieldBoxCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
			ShowAwardPanelManger._ins.CloseCallBack = new Action(this.ShowNextBattleFieldID);
		}
		if (!BattleFieldBox.battleFieldBoxes.ContainsKey(BattleFieldBoxPanel.OpenBoxQuality) || BattleFieldBox.battleFieldBoxes[BattleFieldBoxPanel.OpenBoxQuality].Count == 0)
		{
			if (BattleFieldBoxPanel.OpenBoxQuality == 1)
			{
				ResourcePanelManage.inst.Qua1battleBox.DesInsInPool();
			}
			else if (BattleFieldBoxPanel.OpenBoxQuality == 2)
			{
				ResourcePanelManage.inst.Qua2battleBox.DesInsInPool();
			}
			else
			{
				ResourcePanelManage.inst.Qua3battleBox.DesInsInPool();
			}
		}
		else if (BattleFieldBoxPanel.OpenBoxQuality == 1)
		{
			ResourcePanelManage.inst.battleBoxQua1DesciptInfo.textDes.text = LanguageManage.GetTextByKey("箱子数量", "others") + ":" + BattleFieldBox.battleFieldBoxes[BattleFieldBoxPanel.OpenBoxQuality].Sum((KVStruct a) => a.value);
		}
		else if (BattleFieldBoxPanel.OpenBoxQuality == 2)
		{
			ResourcePanelManage.inst.battleBoxQua2DesciptInfo.textDes.text = LanguageManage.GetTextByKey("箱子数量", "others") + ":" + BattleFieldBox.battleFieldBoxes[BattleFieldBoxPanel.OpenBoxQuality].Sum((KVStruct a) => a.value);
		}
		else
		{
			ResourcePanelManage.inst.battleBoxQua3DesciptInfo.textDes.text = LanguageManage.GetTextByKey("箱子数量", "others") + ":" + BattleFieldBox.battleFieldBoxes[BattleFieldBoxPanel.OpenBoxQuality].Sum((KVStruct a) => a.value);
		}
	}

	public void ShowNextBattleFieldID()
	{
		if (BattleFieldBox.battleFieldBoxes.ContainsKey(BattleFieldBoxPanel.OpenBoxQuality) && BattleFieldBox.battleFieldBoxes[BattleFieldBoxPanel.OpenBoxQuality].Count > 0)
		{
			int boxId = (int)(from a in BattleFieldBox.battleFieldBoxes[BattleFieldBoxPanel.OpenBoxQuality]
			orderby BattleFieldBox.BattleFieldBox_PlannerData[(int)a.key].level descending
			select a).First<KVStruct>().key;
			BattleFieldBoxPanel.ShowBattleFieldBox(boxId);
		}
		else
		{
			this.Back(null);
		}
	}

	public static void ShowBattleFieldBox(int boxId)
	{
		FuncUIManager.inst.OpenFuncUI("BattleFieldBoxPanel", SenceType.Island);
		BattleFieldBoxPanel.inst.ShowBattleField(boxId);
	}

	public void ShowBattleField(int boxID)
	{
		this.isCanBack = false;
		this.Bac.localPosition = new Vector3(-2600f, 0f, 0f);
		this.boxItemId = boxID;
		BattleFieldBox.BattleFieldBoxConst battleFieldBoxConst = BattleFieldBox.BattleFieldBox_PlannerData[boxID];
		this.boxName.text = battleFieldBoxConst.name;
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
		transform3.gameObject.SetActive(battleFieldBoxConst.quility == 1);
		transform2.gameObject.SetActive(battleFieldBoxConst.quility == 3);
		transform.gameObject.SetActive(battleFieldBoxConst.quility == 2);
		this.battleBox.tr.localPosition = new Vector3(0f, -58.56f, -500f);
		Transform[] componentsInChildren = this.battleBox.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform4 = componentsInChildren[i];
			transform4.gameObject.layer = 5;
		}
		if (battleFieldBoxConst.keyId > 0)
		{
			AtlasManage.SetUiItemAtlas(this.boxKeySprite, UnitConst.GetInstance().ItemConst[battleFieldBoxConst.keyId].IconId);
			this.lingQuLabel.SetActive(false);
		}
		else
		{
			this.lingQuLabel.SetActive(true);
			this.boxKeySprite.spriteName = string.Empty;
		}
		foreach (KeyValuePair<ResType, int> current in battleFieldBoxConst.res_View)
		{
			ItemUnit component2 = NGUITools.AddChild(this.itemTable.gameObject, this.resPrefab).GetComponent<ItemUnit>();
			AtlasManage.SetResSpriteName(component2.itemSprite, current.Key);
			AtlasManage.SetQuilitySpriteName(component2.itemQuSprite, (Quility_ResAndItemAndSkill)battleFieldBoxConst.res_ViewQuility[current.Key]);
			ItemTipsShow2 component3 = component2.GetComponent<ItemTipsShow2>();
			component3.Index = (int)current.Key;
			component3.Type = 2;
		}
		foreach (KeyValuePair<int, int> current2 in battleFieldBoxConst.items_View)
		{
			ItemUnit component4 = NGUITools.AddChild(this.itemTable.gameObject, this.itemPrefab).GetComponent<ItemUnit>();
			AtlasManage.SetUiItemAtlas(component4.itemSprite, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
			AtlasManage.SetQuilitySpriteName(component4.itemQuSprite, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
			ItemTipsShow2 component5 = component4.GetComponent<ItemTipsShow2>();
			component5.Index = current2.Key;
			component5.Type = 1;
		}
		foreach (KeyValuePair<int, int> current3 in battleFieldBoxConst.skills_View)
		{
			ItemUnit component6 = NGUITools.AddChild(this.itemTable.gameObject, this.skillPrefab).GetComponent<ItemUnit>();
			component6.itemSprite.spriteName = UnitConst.GetInstance().skillList[current3.Key].icon;
			AtlasManage.SetQuilitySpriteName(component6.itemQuSprite, UnitConst.GetInstance().skillList[current3.Key].skillQuality);
			ItemTipsShow2 component7 = component6.GetComponent<ItemTipsShow2>();
			component7.Index = current3.Key;
			component7.Type = 3;
		}
		this.itemTable.Reposition();
		if (this.itemTable.transform.childCount <= 9)
		{
			this.Btn.transform.localPosition = new Vector3(0f, -244f, 0f);
		}
		else
		{
			this.Btn.transform.localPosition = new Vector3(0f, -300f, 0f);
		}
		this.Bac.DOLocalMoveX(-246.2f, 0.2f, false).OnComplete(delegate
		{
			this.Bac.DOPunchPosition(new Vector3(10f, 0f, 0f), 0.3f, 20, 3f, false);
			this.isCanBack = true;
		});
	}
}
