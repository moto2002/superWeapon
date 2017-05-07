using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPanelManage : FuncUIPanel
{
	[SerializeField]
	private UITable itemTable;

	[SerializeField]
	private UILabel itemDes;

	[SerializeField]
	private GameObject itemPrefab;

	[SerializeField]
	private UILabel itemNumBi;

	[SerializeField]
	private UILabel itemName;

	[SerializeField]
	public GameObject etixBtn;

	public UILabel equipmentName;

	public UILabel equipmentInfo;

	public GameObject compositionruleBtn;

	public UILabel propNum;

	public UILabel equipmentInit;

	public static ItemPanelManage ins;

	public UISprite itemDesd;

	public UISprite equipmentIcon;

	public UISprite equipmentKuang;

	public UISprite itemDesBreack;

	private List<ItemUnit> allItemUIGameObject;

	public List<GameObject> allUIItem;

	private HeroInfo item;

	public GameObject propPanel;

	public GameObject equipmentPanel;

	public GameObject equipmentBtn;

	public GameObject propBtn;

	public UILabel equipementColor;

	public GameObject HeChengBtn;

	public GameObject employBtn;

	public GameObject[] compoundPanelArray;

	public GameObject itemCompoundParent;

	public GameObject BtnCompoundPanelClose;

	public GameObject noItemTip;

	public GameObject UseItemBtn;

	public GameObject UseItemProfess;

	public UISlider UseItemProfess_Slider;

	public UILabel UseItemProfess_Label;

	public UILabel NoSpaceLabel;

	private ResTips resTips;

	private int CompositionitemId;

	private int itemId;

	private long ItemStaticId;

	public List<Sprite> itemNum;

	public bool isPares;

	public UITable itemequipmentTabe;

	public GameObject newItemBg;

	public UISprite newItemSp;

	public UILabel newitemName;

	public UILabel newitemQuat;

	public GameObject newItemBtn;

	public UISprite itemquat;

	public UILabel dressLevel;

	private int basic_UseItem_Num;

	private int UseItem_Num = 1;

	public static bool ResCallBack;

	private int basic_item_num;

	private ItemUnit CurItem;

	public void OnDestroy()
	{
		ItemPanelManage.ins = null;
	}

	public override void Awake()
	{
		this.OnGetObj();
		this.allItemUIGameObject = new List<ItemUnit>();
		this.allUIItem = new List<GameObject>();
		this.itemNum = new List<Sprite>();
		ItemPanelManage.ins = this;
	}

	public void OnGetObj()
	{
		this.itemTable = base.transform.FindChild("ItemScroll View/Table").GetComponent<UITable>();
		this.itemDes = base.transform.FindChild("propInfo/Label").GetComponent<UILabel>();
		this.itemPrefab = base.transform.FindChild("1").gameObject;
		this.itemPrefab.AddComponent<ItemUnit>();
		this.itemName = base.transform.FindChild("propInfo/ItemName").GetComponent<UILabel>();
		this.etixBtn = base.transform.FindChild("exitBtn").gameObject;
		this.equipmentName = base.transform.FindChild("equipmentInfo/ItemName").GetComponent<UILabel>();
		this.equipmentInfo = base.transform.FindChild("equipmentInfo/Label").GetComponent<UILabel>();
		this.compositionruleBtn = base.transform.FindChild("equipmentInfo/composition ruleBtn").gameObject;
		this.propNum = base.transform.FindChild("propInfo/propNum/Label").GetComponent<UILabel>();
		this.equipmentInit = base.transform.FindChild("equipmentInfo/equipmentNum").GetComponent<UILabel>();
		this.itemDesd = base.transform.FindChild("propInfo/itemDescription").GetComponent<UISprite>();
		this.equipmentIcon = base.transform.FindChild("equipmentInfo/itemDescription").GetComponent<UISprite>();
		this.equipmentKuang = base.transform.FindChild("equipmentInfo/Itemquity").GetComponent<UISprite>();
		this.itemDesBreack = base.transform.FindChild("propInfo/itenBg").GetComponent<UISprite>();
		this.propPanel = base.transform.FindChild("propInfo").gameObject;
		this.equipmentPanel = base.transform.FindChild("equipmentInfo").gameObject;
		this.equipmentBtn = base.transform.FindChild("equipmentBtn").gameObject;
		this.propBtn = base.transform.FindChild("propBtn").gameObject;
		this.newItemBg = base.transform.FindChild("NewItemPanel").gameObject;
		this.newItemSp = base.transform.FindChild("NewItemPanel/NewItemBg/NewItem").GetComponent<UISprite>();
		this.newitemName = base.transform.FindChild("NewItemPanel/NewItemBg/name").GetComponent<UILabel>();
		this.newitemQuat = base.transform.FindChild("NewItemPanel/NewItemBg/quit").GetComponent<UILabel>();
		this.newItemBtn = base.transform.FindChild("NewItemPanel/NewItemBg/OkBtn").gameObject;
		this.itemquat = base.transform.FindChild("NewItemPanel/NewItemBg/NewItem/Sprite").GetComponent<UISprite>();
		this.dressLevel = base.transform.FindChild("NewItemPanel/NewItemBg/Leve").GetComponent<UILabel>();
		this.equipementColor = base.transform.FindChild("equipmentInfo/equipmentColor").GetComponent<UILabel>();
		this.HeChengBtn = base.transform.FindChild("equipmentInfo/HeChengBtn").gameObject;
		this.employBtn = base.transform.FindChild("propInfo/Use ItemBtn").gameObject;
		this.itemCompoundParent = base.transform.FindChild("CompoundPanel").gameObject;
		this.BtnCompoundPanelClose = base.transform.FindChild("CompoundPanel/twoItemCompound/CloseBtn").gameObject;
		this.itemequipmentTabe = base.transform.FindChild("Scroll View/Table").GetComponent<UITable>();
		this.compoundPanelArray = new GameObject[3];
		this.compoundPanelArray[0] = base.transform.FindChild("CompoundPanel/twoItemCompound").gameObject;
		this.compoundPanelArray[0].AddComponent<EquipMixMonoBehaviour>();
	}

	public void Start()
	{
		if (this.allUIItem.Count > 0)
		{
			this.ClickItem(this.allUIItem[0]);
			this.noItemTip.gameObject.SetActive(false);
		}
		else
		{
			this.propPanel.SetActive(false);
			this.noItemTip.gameObject.SetActive(true);
		}
		this.OnItemUpdate();
		this.resTips = HUDTextTool.inst.restip;
		UIEventListener.Get(this.equipmentBtn).onClick = new UIEventListener.VoidDelegate(this.OnEquipmentShow);
		UIEventListener.Get(this.compositionruleBtn).onPress = new UIEventListener.BoolDelegate(this.OnCompositionClickShow);
		UIEventListener.Get(this.HeChengBtn).onClick = new UIEventListener.VoidDelegate(this.OnHechengShow);
		UIEventListener.Get(this.employBtn).onClick = new UIEventListener.VoidDelegate(this.OnEmployBtn);
		UIEventListener.Get(this.propBtn).onClick = new UIEventListener.VoidDelegate(this.OnPropShow);
		UIEventListener.Get(this.BtnCompoundPanelClose).onClick = new UIEventListener.VoidDelegate(this.OnCloseCompoundPanel);
		EventManager.Instance.AddEvent(EventManager.EventType.Bag_UseItem, new EventManager.VoidDelegate(this.Bag_UseItem));
		this.UseItemProfess_Slider = this.UseItemProfess.GetComponent<UISlider>();
	}

	private void Update()
	{
		if (this.CurItem != null)
		{
			this.UseItem_Num = Mathf.Max(1, (int)(this.UseItemProfess_Slider.value * (float)this.CurItem.itemNum));
			this.UseItemProfess_Label.text = this.UseItem_Num.ToString();
		}
		else
		{
			this.UseItem_Num = 1;
		}
		if (this.basic_UseItem_Num != this.UseItem_Num)
		{
			this.CheckNoSpace();
			this.basic_UseItem_Num = this.UseItem_Num;
		}
	}

	private void CheckNoSpace()
	{
		if (this.CurItem == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		foreach (KeyValuePair<ResType, int> current in this.CurItem.item.GiveRes)
		{
			num = (int)current.Key;
			num2 = Mathf.Max(1, this.UseItem_Num) * current.Value;
		}
		if (!SenceManager.inst.NoResSpace((num != 1) ? 0 : num2, (num != 2) ? 0 : num2, (num != 3) ? 0 : num2, (num != 4) ? 0 : num2, false))
		{
			this.UseItemProfess_Label.color = Color.white;
			this.NoSpaceLabel.gameObject.SetActive(false);
		}
		else
		{
			this.UseItemProfess_Label.color = Color.red;
			this.NoSpaceLabel.gameObject.SetActive(true);
		}
	}

	private void Bag_UseItem(GameObject go)
	{
		if (this.CurItem == null)
		{
			return;
		}
		if (this.CurItem.item.Type == TypeItem.资源卡)
		{
			ItemPanelManage.ResCallBack = true;
		}
		else
		{
			ItemPanelManage.ResCallBack = false;
			int num = 0;
			foreach (KeyValuePair<int, SCExtraArmy> current in SenceManager.inst.ExtraArmyList)
			{
				for (int i = 0; i < current.Value.itemId2Level.Count; i++)
				{
					int num2 = (int)current.Value.itemId2Num[i].value;
					for (int j = 0; j < num2; j++)
					{
						num++;
					}
				}
			}
			if (num >= 6)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("特殊兵种过多，无法继续使用特殊兵种卡", "others"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		int num3 = 0;
		int num4 = 0;
		foreach (KeyValuePair<ResType, int> current2 in this.CurItem.item.GiveRes)
		{
			num3 = (int)current2.Key;
			num4 = Mathf.Max(1, this.UseItem_Num) * current2.Value;
		}
		if (!SenceManager.inst.NoResSpace((num3 != 1) ? 0 : num4, (num3 != 2) ? 0 : num4, (num3 != 3) ? 0 : num4, (num3 != 4) ? 0 : num4, true))
		{
			ItemPanelManage.CS_UseItem(this.CurItem.item.Id, this.UseItem_Num);
		}
	}

	public static void CS_UseItem(int itemid, int count)
	{
		CSUseItem cSUseItem = new CSUseItem();
		cSUseItem.itemId = itemid;
		cSUseItem.num = count;
		ClientMgr.GetNet().SendHttp(1002, cSUseItem, new DataHandler.OpcodeHandler(ItemPanelManage.GetUseItem), null);
	}

	public static void GetUseItem(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			Debug.Log("获得资源");
			if (ItemPanelManage.ResCallBack)
			{
				ShowAwardPanelManger.showAwardList();
			}
			else
			{
				FuncUIManager.inst.HideFuncUI("ItemPanel");
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("使用特殊兵种卡召唤了特殊单位", "others"), HUDTextTool.TextUITypeEnum.Num5);
			}
		}
	}

	private void OnCloseCompoundPanel(GameObject go)
	{
		this.compoundPanelArray[0].GetComponent<EquipMixMonoBehaviour>().itemList.Clear();
		this.OnTypeShow();
		this.OnItemSetActive();
		this.itemCompoundParent.SetActive(false);
		this.OnCompoundPanelClear();
		this.isPares = false;
	}

	public void OnTypeShow()
	{
		for (int i = 0; i < this.allUIItem.Count; i++)
		{
			ItemUnit component = this.allUIItem[i].GetComponent<ItemUnit>();
			if (this.itemId == component.id)
			{
				component.selecting.SetActive(true);
				component.oprforSp.gameObject.SetActive(false);
			}
			else
			{
				component.oprforSp.gameObject.SetActive(false);
				component.selecting.SetActive(false);
			}
		}
	}

	private void OnEmployBtn(GameObject go)
	{
	}

	private void OnHechengShow(GameObject go)
	{
		for (int i = 0; i < HeroInfo.GetInstance().EquipItem.Count; i++)
		{
			if (this.CurItem.id == HeroInfo.GetInstance().EquipItem[i].equipID && HeroInfo.GetInstance().EquipItem[i].commanderID != 0L)
			{
				HUDTextTool.inst.SetText("此武器已被装备，不可合成", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		if (UnitConst.GetInstance().equipList[this.itemId].equipQuality == Quility_ResAndItemAndSkill.红)
		{
			HUDTextTool.inst.SetText("最高品质不能合成", HUDTextTool.TextUITypeEnum.Num5);
		}
		else
		{
			for (int j = 0; j < this.allUIItem.Count; j++)
			{
				ItemUnit component = this.allUIItem[j].GetComponent<ItemUnit>();
				if (component.ID == this.ItemStaticId)
				{
					component.oprforSp.gameObject.SetActive(true);
				}
			}
			this.equipmentPanel.SetActive(true);
			this.itemCompoundParent.SetActive(true);
			this.OnCompoundPanelClear();
			this.OnCompoundTwo();
		}
	}

	public void OnCompoundPanelClear()
	{
		this.compoundPanelArray[0].gameObject.SetActive(false);
	}

	public void OnCompoundClearDesty(long id)
	{
		this.itemCompoundParent.SetActive(false);
		this.OnCompoundPanelClear();
		this.OnCompoundItemInfoShow(id);
		this.OnItemSetActive();
		this.isPares = false;
	}

	private void OnCompoundItemInfoShow(long id)
	{
		for (int i = 0; i < this.allUIItem.Count; i++)
		{
			if (id == (long)this.allUIItem[i].GetComponent<ItemUnit>().id)
			{
				this.ClickItemShow(this.allUIItem[i]);
			}
		}
	}

	public void OnCompoundTwo()
	{
		this.compoundPanelArray[0].gameObject.SetActive(true);
		EquipMixMonoBehaviour component = this.compoundPanelArray[0].GetComponent<EquipMixMonoBehaviour>();
		component.OnGetBg(UnitConst.GetInstance().equipList[this.itemId].equipQuality.ToString(), this.itemId, this.ItemStaticId);
		component.OnPanelShow();
		AtlasManage.SetUiItemAtlas(component.item, UnitConst.GetInstance().equipList[this.itemId].icon);
		AtlasManage.SetQuilitySpriteName(component.itembg, UnitConst.GetInstance().equipList[this.itemId].equipQuality);
	}

	public void OnItemUpdate()
	{
	}

	private void OnCompositionClickShow(GameObject go, bool state)
	{
		Vector3 tras = new Vector3(273.5f, -100.3f, 0f);
		if (state)
		{
			this.resTips.OnPlayTextTipsPostion(tras, "合成规则：\r\n1：可手动选择相同品质的\r\n装备进行合成\r\n2：点击一键放入则由系统\r\n选择相同品质装备放入\r\n合成栏");
		}
		else
		{
			this.resTips.gameObject.SetActive(false);
		}
	}

	public void OnEquipmentShow(GameObject o)
	{
		this.propBtn.GetComponent<UISprite>().spriteName = "未选中状态";
		this.propBtn.GetComponentInChildren<UILabel>().color = new Color(0.483050853f, 0.623529434f, 0.6313726f);
		o.GetComponent<UISprite>().spriteName = "选中状态按钮";
		o.GetComponentInChildren<UILabel>().color = new Color(0.6666667f, 0.9254902f, 1f);
		this.OnEquipementPanelShow();
		this.OnEquipementItem();
	}

	public void OnCompoundPanelShowIsTrue(GameObject o)
	{
		if (this.isPares)
		{
			this.OnItemShow(o);
		}
		else
		{
			this.ClickItemShow(o);
		}
	}

	public void OnItemSetActive()
	{
		EquipMixMonoBehaviour component = this.compoundPanelArray[0].GetComponent<EquipMixMonoBehaviour>();
		component.itemList.Clear();
		for (int i = 0; i < component.itemSprite.Count; i++)
		{
			component.itemSprite[i].spriteName = string.Empty;
			component.itemSprite[i].gameObject.transform.parent.gameObject.SetActive(false);
		}
	}

	public void OnItemShow(GameObject o)
	{
		EquipMixMonoBehaviour component = this.compoundPanelArray[0].GetComponent<EquipMixMonoBehaviour>();
		ItemUnit item = o.GetComponent<ItemUnit>();
		component.EquipMixList.Clear();
		for (int i = 0; i < HeroInfo.GetInstance().EquipItem.Count; i++)
		{
			if (item.id == HeroInfo.GetInstance().EquipItem[i].equipID && HeroInfo.GetInstance().EquipItem[i].commanderID != 0L)
			{
				HUDTextTool.inst.SetText("此武器已被装备，不可合成", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		if (UnitConst.GetInstance().equipList[this.itemId].equipQuality == UnitConst.GetInstance().equipList[item.id].equipQuality)
		{
			if (item.oprforSp.gameObject.activeSelf)
			{
				return;
			}
			if (component.EquipMixList.Count >= component.itemSprite.Count)
			{
				HUDTextTool.inst.SetText("合成栏已满", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			for (int j = 0; j < component.itemSprite.Count; j++)
			{
				if (item.ID == this.ItemStaticId || component.itemList.Contains(item.ID))
				{
					HUDTextTool.inst.SetText("需要同品质装备才能够合成", HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				if (component.itemSprite[j].spriteName == string.Empty)
				{
					AtlasManage.SetUiItemAtlas(component.itemSprite[j], UnitConst.GetInstance().equipList[item.id].icon);
					component.itemSprite[j].name = item.ID.ToString();
					component.itemSprite[j].transform.GetChild(0).gameObject.SetActive(true);
					item.oprforSp.gameObject.SetActive(true);
					component.itemList.Add(item.ID);
					component.EquipMixList.Add(HeroInfo.GetInstance().EquipItem.SingleOrDefault((EquipItem a) => a.id == item.ID));
					break;
				}
				component.isHeCheng = false;
				item.oprforSp.gameObject.SetActive(false);
				item.selecting.SetActive(false);
			}
			this.RefeshMixEqipInfo();
		}
		else
		{
			HUDTextTool.inst.SetText("等级不同 不可合成", HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	private void RefeshMixEqipInfo()
	{
		EquipMixMonoBehaviour component = this.compoundPanelArray[0].GetComponent<EquipMixMonoBehaviour>();
		for (int i = 0; i < component.itemSprite.Count; i++)
		{
			if (component.itemSprite[i].spriteName == string.Empty)
			{
				return;
			}
		}
		component.isHeCheng = true;
		component.heChengBtn[0].gameObject.SetActive(true);
		component.heChengBtn[0].GetComponent<UISprite>().ShaderToGray();
		component.heChengBtn[0].GetComponent<BoxCollider>().enabled = false;
		component.heChengBtn[1].GetComponent<UISprite>().ShaderToNormal();
		component.heChengBtn[1].GetComponent<BoxCollider>().enabled = true;
		component.heChengBtn[1].gameObject.SetActive(true);
	}

	public void OnEquipementItem()
	{
		this.allUIItem.Clear();
		this.itemTable.DestoryChildren(true);
		this.itemequipmentTabe.DestoryChildren(true);
		int index = 0;
		for (int i = 0; i < HeroInfo.GetInstance().EquipItem.Count; i++)
		{
			EquipItem equipItem = HeroInfo.GetInstance().EquipItem[i];
			if (UnitConst.GetInstance().equipList[equipItem.equipID].equipQuality == Quility_ResAndItemAndSkill.红)
			{
				GameObject gameObject = NGUITools.AddChild(this.itemequipmentTabe.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject).onClick = new UIEventListener.VoidDelegate(this.OnCompoundPanelShowIsTrue);
				ItemUnit component = gameObject.GetComponent<ItemUnit>();
				component.selecting.SetActive(false);
				component.id = equipItem.equipID;
				component.Officertype = UnitConst.GetInstance().equipList[equipItem.equipID].commanderType;
				component.ID = equipItem.id;
				LogManage.LogError("OfficerId" + equipItem.id);
				component.num.gameObject.SetActive(false);
				component.num.text = UnitConst.GetInstance().equipList[equipItem.equipID].level.ToString();
				AtlasManage.SetUiItemAtlas(component.itemSprite, UnitConst.GetInstance().equipList[equipItem.equipID].icon);
				AtlasManage.SetQuilitySpriteName(component.itemQuSprite, UnitConst.GetInstance().equipList[equipItem.equipID].equipQuality);
				if (component.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject);
			}
		}
		for (int j = 0; j < HeroInfo.GetInstance().EquipItem.Count; j++)
		{
			EquipItem equipItem2 = HeroInfo.GetInstance().EquipItem[j];
			if (UnitConst.GetInstance().equipList[equipItem2.equipID].equipQuality == Quility_ResAndItemAndSkill.紫)
			{
				GameObject gameObject2 = NGUITools.AddChild(this.itemequipmentTabe.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject2).onClick = new UIEventListener.VoidDelegate(this.OnCompoundPanelShowIsTrue);
				ItemUnit component2 = gameObject2.GetComponent<ItemUnit>();
				component2.selecting.SetActive(false);
				component2.id = equipItem2.equipID;
				component2.Officertype = UnitConst.GetInstance().equipList[equipItem2.equipID].commanderType;
				component2.ID = equipItem2.id;
				LogManage.LogError("OfficerId" + equipItem2.id);
				component2.num.gameObject.SetActive(false);
				component2.num.text = UnitConst.GetInstance().equipList[equipItem2.equipID].level.ToString();
				AtlasManage.SetUiItemAtlas(component2.itemSprite, UnitConst.GetInstance().equipList[equipItem2.equipID].icon);
				AtlasManage.SetQuilitySpriteName(component2.itemQuSprite, UnitConst.GetInstance().equipList[equipItem2.equipID].equipQuality);
				if (component2.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject2);
			}
		}
		for (int k = 0; k < HeroInfo.GetInstance().EquipItem.Count; k++)
		{
			EquipItem equipItem3 = HeroInfo.GetInstance().EquipItem[k];
			if (UnitConst.GetInstance().equipList[equipItem3.equipID].equipQuality == Quility_ResAndItemAndSkill.蓝)
			{
				GameObject gameObject3 = NGUITools.AddChild(this.itemequipmentTabe.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject3).onClick = new UIEventListener.VoidDelegate(this.OnCompoundPanelShowIsTrue);
				ItemUnit component3 = gameObject3.GetComponent<ItemUnit>();
				component3.selecting.SetActive(false);
				component3.id = equipItem3.equipID;
				component3.Officertype = UnitConst.GetInstance().equipList[equipItem3.equipID].commanderType;
				component3.ID = equipItem3.id;
				LogManage.LogError("OfficerId" + equipItem3.id);
				component3.num.gameObject.SetActive(false);
				component3.num.text = UnitConst.GetInstance().equipList[equipItem3.equipID].level.ToString();
				AtlasManage.SetUiItemAtlas(component3.itemSprite, UnitConst.GetInstance().equipList[equipItem3.equipID].icon);
				AtlasManage.SetQuilitySpriteName(component3.itemQuSprite, UnitConst.GetInstance().equipList[equipItem3.equipID].equipQuality);
				if (component3.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject3);
			}
		}
		for (int l = 0; l < HeroInfo.GetInstance().EquipItem.Count; l++)
		{
			EquipItem equipItem4 = HeroInfo.GetInstance().EquipItem[l];
			if (UnitConst.GetInstance().equipList[equipItem4.equipID].equipQuality == Quility_ResAndItemAndSkill.绿)
			{
				GameObject gameObject4 = NGUITools.AddChild(this.itemequipmentTabe.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject4).onClick = new UIEventListener.VoidDelegate(this.OnCompoundPanelShowIsTrue);
				ItemUnit component4 = gameObject4.GetComponent<ItemUnit>();
				component4.selecting.SetActive(false);
				component4.id = equipItem4.equipID;
				component4.Officertype = UnitConst.GetInstance().equipList[equipItem4.equipID].commanderType;
				component4.ID = equipItem4.id;
				LogManage.LogError("OfficerId" + equipItem4.id);
				component4.num.gameObject.SetActive(false);
				component4.num.text = UnitConst.GetInstance().equipList[equipItem4.equipID].level.ToString();
				AtlasManage.SetUiItemAtlas(component4.itemSprite, UnitConst.GetInstance().equipList[equipItem4.equipID].icon);
				AtlasManage.SetQuilitySpriteName(component4.itemQuSprite, UnitConst.GetInstance().equipList[equipItem4.equipID].equipQuality);
				if (component4.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject4);
			}
		}
		for (int m = 0; m < HeroInfo.GetInstance().EquipItem.Count; m++)
		{
			EquipItem equipItem5 = HeroInfo.GetInstance().EquipItem[m];
			if (UnitConst.GetInstance().equipList[equipItem5.equipID].equipQuality == Quility_ResAndItemAndSkill.白)
			{
				GameObject gameObject5 = NGUITools.AddChild(this.itemequipmentTabe.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject5).onClick = new UIEventListener.VoidDelegate(this.OnCompoundPanelShowIsTrue);
				ItemUnit component5 = gameObject5.GetComponent<ItemUnit>();
				component5.selecting.SetActive(false);
				component5.id = equipItem5.equipID;
				component5.Officertype = UnitConst.GetInstance().equipList[equipItem5.equipID].commanderType;
				component5.ID = equipItem5.id;
				LogManage.LogError("OfficerId" + equipItem5.id);
				component5.num.gameObject.SetActive(false);
				component5.num.text = UnitConst.GetInstance().equipList[equipItem5.equipID].level.ToString();
				AtlasManage.SetUiItemAtlas(component5.itemSprite, UnitConst.GetInstance().equipList[equipItem5.equipID].icon);
				AtlasManage.SetQuilitySpriteName(component5.itemQuSprite, UnitConst.GetInstance().equipList[equipItem5.equipID].equipQuality);
				if (component5.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject5);
			}
		}
		this.itemequipmentTabe.Reposition();
		if (this.allUIItem.Count > 0)
		{
			this.ClickItemShow(this.allUIItem[index]);
		}
		else
		{
			this.equipmentPanel.SetActive(false);
		}
	}

	public void OnEquipementPanelShow()
	{
		this.OnCompoundPanelClear();
		this.itemCompoundParent.SetActive(false);
		this.isPares = false;
		this.propPanel.SetActive(false);
		this.equipmentPanel.SetActive(true);
	}

	public void OnpropPanelShow()
	{
		this.OnCompoundPanelClear();
		this.itemCompoundParent.SetActive(false);
		this.equipmentPanel.SetActive(false);
		this.propPanel.SetActive(true);
	}

	public void OnPropShow(GameObject o)
	{
		o.GetComponent<UISprite>().spriteName = "选中状态按钮";
		o.GetComponentInChildren<UILabel>().color = new Color(0.6666667f, 0.9254902f, 1f);
		if (this.allUIItem.Count > 0)
		{
			this.OnpropPanelShow();
		}
		this.OnpropItem();
	}

	public override void OnEnable()
	{
		base.OnEnable();
		this.OnpropItem();
		this.OnItemUpdate();
		if (this.allUIItem.Count > 0)
		{
			this.OnpropPanelShow();
		}
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10068)
		{
			this.OnEquipementItem();
		}
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	public void OnpropItem()
	{
		this.itemDesd.gameObject.SetActive(false);
		this.itemequipmentTabe.DestoryChildren(true);
		this.itemTable.DestoryChildren(true);
		this.allUIItem.Clear();
		List<KeyValuePair<Item, int>> list = (from a in HeroInfo.GetInstance().PlayerItemInfo
		where UnitConst.GetInstance().ItemConst[a.Key].Type != TypeItem.指挥官招募令 && a.Value > 0
		select a).ToDictionary((KeyValuePair<int, int> key) => UnitConst.GetInstance().ItemConst[key.Key], (KeyValuePair<int, int> value) => value.Value).ToList<KeyValuePair<Item, int>>();
		int num = 0;
		int index = 0;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Key.Quality == Quility_ResAndItemAndSkill.红)
			{
				GameObject gameObject = NGUITools.AddChild(this.itemTable.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject).onClick = new UIEventListener.VoidDelegate(this.ClickItem);
				ItemUnit component = gameObject.GetComponent<ItemUnit>();
				component.selecting.SetActive(false);
				component.num.text = string.Format("{0}", list[i].Value);
				component.item = list[i].Key;
				AtlasManage.SetUiItemAtlas(component.itemSprite, list[i].Key.IconId);
				component.itemQuSprite.spriteName = this.SetQuSprite((int)list[i].Key.Quality).ToString();
				component.itemQuSprite.GetComponent<UIButton>().normalSprite = this.SetQuSprite((int)list[i].Key.Quality).ToString();
				component.itemQuSprite.GetComponent<UIButton>().hoverSprite = this.SetQuSprite((int)list[i].Key.Quality).ToString();
				component.itemQuSprite.GetComponent<UIButton>().pressedSprite = this.SetQuSprite((int)list[i].Key.Quality).ToString();
				component.itemQuSprite.GetComponent<UIButton>().disabledSprite = this.SetQuSprite((int)list[i].Key.Quality).ToString();
				component.itemNum = list[i].Value;
				num += list[i].Value;
				if (this.CurItem != null && component.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].Key.Quality == Quility_ResAndItemAndSkill.紫)
			{
				GameObject gameObject2 = NGUITools.AddChild(this.itemTable.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject2).onClick = new UIEventListener.VoidDelegate(this.ClickItem);
				ItemUnit component2 = gameObject2.GetComponent<ItemUnit>();
				component2.selecting.SetActive(false);
				component2.num.text = string.Format("{0}", list[j].Value);
				component2.item = list[j].Key;
				AtlasManage.SetUiItemAtlas(component2.itemSprite, list[j].Key.IconId);
				component2.itemQuSprite.spriteName = this.SetQuSprite((int)list[j].Key.Quality).ToString();
				component2.itemQuSprite.GetComponent<UIButton>().normalSprite = this.SetQuSprite((int)list[j].Key.Quality).ToString();
				component2.itemQuSprite.GetComponent<UIButton>().hoverSprite = this.SetQuSprite((int)list[j].Key.Quality).ToString();
				component2.itemQuSprite.GetComponent<UIButton>().pressedSprite = this.SetQuSprite((int)list[j].Key.Quality).ToString();
				component2.itemQuSprite.GetComponent<UIButton>().disabledSprite = this.SetQuSprite((int)list[j].Key.Quality).ToString();
				component2.itemNum = list[j].Value;
				num += list[j].Value;
				if (this.CurItem != null && component2.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject2);
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			if (list[k].Key.Quality == Quility_ResAndItemAndSkill.蓝)
			{
				GameObject gameObject3 = NGUITools.AddChild(this.itemTable.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject3).onClick = new UIEventListener.VoidDelegate(this.ClickItem);
				ItemUnit component3 = gameObject3.GetComponent<ItemUnit>();
				component3.selecting.SetActive(false);
				component3.num.text = string.Format("{0}", list[k].Value);
				component3.item = list[k].Key;
				AtlasManage.SetUiItemAtlas(component3.itemSprite, list[k].Key.IconId);
				component3.itemQuSprite.spriteName = this.SetQuSprite((int)list[k].Key.Quality).ToString();
				component3.itemQuSprite.GetComponent<UIButton>().normalSprite = this.SetQuSprite((int)list[k].Key.Quality).ToString();
				component3.itemQuSprite.GetComponent<UIButton>().hoverSprite = this.SetQuSprite((int)list[k].Key.Quality).ToString();
				component3.itemQuSprite.GetComponent<UIButton>().pressedSprite = this.SetQuSprite((int)list[k].Key.Quality).ToString();
				component3.itemQuSprite.GetComponent<UIButton>().disabledSprite = this.SetQuSprite((int)list[k].Key.Quality).ToString();
				component3.itemNum = list[k].Value;
				num += list[k].Value;
				if (this.CurItem != null && component3.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject3);
			}
		}
		for (int l = 0; l < list.Count; l++)
		{
			if (list[l].Key.Quality == Quility_ResAndItemAndSkill.绿)
			{
				GameObject gameObject4 = NGUITools.AddChild(this.itemTable.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject4).onClick = new UIEventListener.VoidDelegate(this.ClickItem);
				ItemUnit component4 = gameObject4.GetComponent<ItemUnit>();
				component4.selecting.SetActive(false);
				component4.num.text = string.Format("{0}", list[l].Value);
				component4.item = list[l].Key;
				AtlasManage.SetUiItemAtlas(component4.itemSprite, list[l].Key.IconId);
				component4.itemQuSprite.spriteName = this.SetQuSprite((int)list[l].Key.Quality).ToString();
				component4.itemQuSprite.GetComponent<UIButton>().normalSprite = this.SetQuSprite((int)list[l].Key.Quality).ToString();
				component4.itemQuSprite.GetComponent<UIButton>().hoverSprite = this.SetQuSprite((int)list[l].Key.Quality).ToString();
				component4.itemQuSprite.GetComponent<UIButton>().pressedSprite = this.SetQuSprite((int)list[l].Key.Quality).ToString();
				component4.itemQuSprite.GetComponent<UIButton>().disabledSprite = this.SetQuSprite((int)list[l].Key.Quality).ToString();
				component4.itemNum = list[l].Value;
				num += list[l].Value;
				if (this.CurItem != null && component4.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject4);
			}
		}
		for (int m = 0; m < list.Count; m++)
		{
			if (list[m].Key.Quality == Quility_ResAndItemAndSkill.白)
			{
				GameObject gameObject5 = NGUITools.AddChild(this.itemTable.gameObject, this.itemPrefab);
				UIEventListener.Get(gameObject5).onClick = new UIEventListener.VoidDelegate(this.ClickItem);
				ItemUnit component5 = gameObject5.GetComponent<ItemUnit>();
				component5.selecting.SetActive(false);
				component5.num.text = string.Format("{0}", list[m].Value);
				component5.item = list[m].Key;
				AtlasManage.SetUiItemAtlas(component5.itemSprite, list[m].Key.IconId);
				AtlasManage.SetQuilitySpriteName(component5.itemQuSprite, list[m].Key.Quality);
				component5.itemQuSprite.GetComponent<UIButton>().normalSprite = this.SetQuSprite((int)list[m].Key.Quality).ToString();
				component5.itemQuSprite.GetComponent<UIButton>().hoverSprite = this.SetQuSprite((int)list[m].Key.Quality).ToString();
				component5.itemQuSprite.GetComponent<UIButton>().pressedSprite = this.SetQuSprite((int)list[m].Key.Quality).ToString();
				component5.itemQuSprite.GetComponent<UIButton>().disabledSprite = this.SetQuSprite((int)list[m].Key.Quality).ToString();
				component5.itemNum = list[m].Value;
				num += list[m].Value;
				if (this.CurItem != null && component5.item.Id == this.CurItem.item.Id)
				{
					index = this.allUIItem.Count;
				}
				this.allUIItem.Add(gameObject5);
			}
		}
		this.itemNumBi.text = num.ToString();
		this.itemTable.Reposition();
		if (this.allUIItem.Count > 0)
		{
			this.ClickItem(this.allUIItem[index]);
		}
		else
		{
			this.propPanel.SetActive(false);
		}
	}

	public void ButtonClick(ItemBtnType type, GameObject go)
	{
		if (type != ItemBtnType.close)
		{
			if (type != ItemBtnType.oneKeyReward)
			{
			}
		}
		else
		{
			FuncUIManager.inst.HideFuncUI("ItemPanel");
		}
	}

	private void ClickItem(GameObject ga)
	{
		this.itemDesd.gameObject.SetActive(true);
		ItemUnit component = ga.GetComponent<ItemUnit>();
		if (this.CurItem != null && this.CurItem.selecting.activeSelf)
		{
			this.CurItem.selecting.SetActive(false);
		}
		if (component.item != null)
		{
			this.itemDes.text = string.Format(LanguageManage.GetTextByKey(component.item.Description, "item"), new object[0]);
			this.propNum.text = component.num.text;
			this.itemName.text = string.Format(LanguageManage.GetTextByKey(component.item.Name, "item"), new object[0]);
			component.selecting.SetActive(true);
		}
		this.UseItemBtn.gameObject.SetActive(component.item.Type == TypeItem.资源卡 || component.item.Type == (TypeItem)11);
		this.UseItemProfess.gameObject.SetActive(component.item.Type == TypeItem.资源卡);
		this.UseItemProfess_Slider.value = 0f;
		this.CurItem = component;
		AtlasManage.SetUiItemAtlas(this.itemDesd, component.itemSprite.spriteName);
		this.itemDesBreack.spriteName = component.itemQuSprite.spriteName;
	}

	private void ClickItemShow(GameObject ga)
	{
		ItemUnit component = ga.GetComponent<ItemUnit>();
		if (this.CurItem != null && this.CurItem.selecting.activeSelf)
		{
			this.CurItem.selecting.SetActive(false);
		}
		component.selecting.SetActive(true);
		this.equipmentName.text = UnitConst.GetInstance().equipList[component.id].name;
		this.itemId = component.id;
		this.ItemStaticId = component.ID;
		this.equipementColor.text = component.itemQuSprite.spriteName + "色";
		this.equipmentInit.text = string.Format("穿戴等级{0}", UnitConst.GetInstance().equipList[component.id].level);
		this.CurItem = component;
		AtlasManage.SetUiItemAtlas(this.equipmentIcon, component.itemSprite.spriteName);
		this.equipmentKuang.spriteName = component.itemQuSprite.spriteName;
	}

	public void PingZhiShow(UILabel itemLabel, int PingZhileve)
	{
		switch (PingZhileve)
		{
		case 1:
			itemLabel.text = "绿色";
			break;
		case 2:
			itemLabel.text = "蓝色";
			break;
		case 3:
			itemLabel.text = "紫色";
			break;
		case 4:
			itemLabel.text = "黄色";
			break;
		case 5:
			itemLabel.text = "橙色";
			break;
		}
	}

	public string SetQuSprite(int quaty)
	{
		switch (quaty)
		{
		case 1:
			return "白";
		case 2:
			return "绿";
		case 3:
			return "蓝";
		case 4:
			return "紫";
		case 5:
			return "红";
		default:
			return string.Empty;
		}
	}
}
