using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class EquipMixMonoBehaviour : MonoBehaviour
{
	public UISprite itembg;

	public UISprite debrisItemBg;

	public UISprite debrisItemBg1;

	public UISprite NewItemBg;

	public UISprite CurrentItemBg;

	public UISprite item;

	public UISprite NewItem;

	public List<UISprite> itemSprite;

	public UISprite[] tweItemSprite;

	public GameObject[] heChengBtn;

	private long itemId;

	public List<EquipItem> randomId;

	public List<long> itemList;

	private int PingZhiId;

	public bool isHeCheng;

	private int eqid;

	public List<EquipItem> EquipMixList = new List<EquipItem>();

	private void Awake()
	{
		this.OnGetObj();
		this.itemSprite = new List<UISprite>();
		this.itemList = new List<long>();
		this.randomId = new List<EquipItem>();
		EventManager.Instance.AddEvent(EventManager.EventType.ItemPanelCheChengPanelClose, new EventManager.VoidDelegate(this.OnPanelClose));
		UIEventListener.Get(ItemPanelManage.ins.newItemBtn).onClick = new UIEventListener.VoidDelegate(this.OnNewItemPanel);
	}

	public void OnGetObj()
	{
		this.itembg = base.transform.FindChild("CurrentItemBg").GetComponent<UISprite>();
		this.debrisItemBg = base.transform.FindChild("itemBg/debrisItem/debrisItemBg").GetComponent<UISprite>();
		this.debrisItemBg1 = base.transform.FindChild("itemBg1/debrisItem/debrisItemBg").GetComponent<UISprite>();
		this.NewItemBg = base.transform.FindChild("NewItemBg").GetComponent<UISprite>();
		this.CurrentItemBg = base.transform.FindChild("itemBg3/CurrentItem/CurrentItemBg").GetComponent<UISprite>();
		this.item = base.transform.FindChild("CurrentItemBg/CurrentItem").GetComponent<UISprite>();
		this.NewItem = base.transform.FindChild("NewItemBg/NewItem").GetComponent<UISprite>();
		this.tweItemSprite = new UISprite[4];
		this.tweItemSprite[0] = base.transform.FindChild("itemBg/debrisItem").GetComponent<UISprite>();
		this.tweItemSprite[1] = base.transform.FindChild("itemBg1/debrisItem").GetComponent<UISprite>();
		this.tweItemSprite[2] = base.transform.FindChild("itemBg3/CurrentItem").GetComponent<UISprite>();
		this.tweItemSprite[3] = base.transform.FindChild("itemBg4/CurrentItem").GetComponent<UISprite>();
		this.heChengBtn = new GameObject[2];
		this.heChengBtn[0] = base.transform.FindChild("FangRuBtn").gameObject;
		this.heChengBtn[1] = base.transform.FindChild("HeChengBtn").gameObject;
	}

	private void OnPanelClose(GameObject go)
	{
		base.gameObject.SetActive(false);
		this.heChengBtn[1].SetActive(false);
		this.heChengBtn[0].SetActive(false);
	}

	private void OnNewItemPanel(GameObject go)
	{
		ItemPanelManage.ins.newItemBg.SetActive(false);
	}

	private void Start()
	{
		this.OnSetBtn(true);
		UIEventListener.Get(this.heChengBtn[0]).onClick = new UIEventListener.VoidDelegate(this.OnHeChengClick);
		UIEventListener.Get(this.heChengBtn[1]).onClick = new UIEventListener.VoidDelegate(this.OnSendHttp);
	}

	public void OnPanelShow()
	{
		ItemPanelManage.ins.isPares = true;
		this.OnSetBtn(true);
		switch (this.PingZhiId)
		{
		case 1:
			this.TwoItemShow(2);
			this.itemList.Clear();
			break;
		case 2:
			this.TwoItemShow(2);
			this.itemList.Clear();
			break;
		case 3:
			this.TwoItemShow(3);
			this.itemList.Clear();
			break;
		case 4:
			this.TwoItemShow(4);
			this.itemList.Clear();
			break;
		}
	}

	public void OnPanelIsItemShow(ItemUnit item)
	{
	}

	private void OnHeChengClick(GameObject go)
	{
		base.StartCoroutine(this.SetSprite());
	}

	public void TwoItemShow(int i)
	{
		this.itemSprite.Clear();
		for (int j = 0; j < this.tweItemSprite.Length; j++)
		{
			if (j < i)
			{
				if (i == 2)
				{
					this.tweItemSprite[2].parent.gameObject.SetActive(false);
					this.itembg.gameObject.transform.localPosition = new Vector3(73.53f, 88.4f, 0f);
					this.tweItemSprite[3].parent.gameObject.SetActive(false);
				}
				else if (i == 3)
				{
					this.tweItemSprite[3].parent.gameObject.SetActive(false);
					this.itembg.gameObject.transform.localPosition = new Vector3(-2.75f, 88.4f, 0f);
					this.tweItemSprite[2].parent.gameObject.SetActive(true);
					this.tweItemSprite[2].parent.transform.localPosition = new Vector3(147.3f, 88.4f, 0f);
				}
				else
				{
					this.tweItemSprite[2].gameObject.SetActive(true);
					this.tweItemSprite[3].gameObject.SetActive(true);
					this.itembg.gameObject.transform.localPosition = new Vector3(73.4f, 88.4f, 0f);
					this.tweItemSprite[2].parent.transform.localPosition = new Vector3(-44.39f, 88.4f, 0f);
					this.tweItemSprite[3].parent.transform.localPosition = new Vector3(195.1f, 88.4f, 0f);
				}
				this.itemSprite.Add(this.tweItemSprite[j].GetComponent<UISprite>());
				this.itemSprite[j].spriteName = string.Empty;
				this.itemSprite[j].gameObject.SetActive(true);
				this.itemSprite[j].gameObject.transform.parent.gameObject.SetActive(true);
				this.itemSprite[j].gameObject.transform.GetChild(0).gameObject.SetActive(false);
				this.itemSprite[j].gameObject.transform.GetChild(0).GetComponent<UISprite>().spriteName = ItemPanelManage.ins.SetQuSprite(this.PingZhiId);
				UIEventListener.Get(this.itemSprite[j].gameObject).onClick = new UIEventListener.VoidDelegate(this.OnIconDesty);
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator SetSprite()
	{
		EquipMixMonoBehaviour.<SetSprite>c__Iterator73 <SetSprite>c__Iterator = new EquipMixMonoBehaviour.<SetSprite>c__Iterator73();
		<SetSprite>c__Iterator.<>f__this = this;
		return <SetSprite>c__Iterator;
	}

	public void OnIconDesty(GameObject o)
	{
		if (o.GetComponent<UISprite>().spriteName != string.Empty)
		{
			long itemRemoveID = this.itemList.SingleOrDefault((long a) => a == (long)int.Parse(o.name));
			this.itemList.Remove(itemRemoveID);
			o.GetComponent<UISprite>().spriteName = string.Empty;
			o.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			this.OnSetBtn(true);
			this.isHeCheng = false;
			GameObject gameObject = ItemPanelManage.ins.allUIItem.SingleOrDefault((GameObject a) => a.GetComponent<ItemUnit>().ID == itemRemoveID);
			if (gameObject)
			{
				gameObject.GetComponent<ItemUnit>().oprforSp.gameObject.SetActive(false);
			}
			this.EquipMixList.Remove(this.EquipMixList.SingleOrDefault((EquipItem a) => a.id == itemRemoveID));
			return;
		}
	}

	public void OnSetBtn(bool isSet)
	{
		if (isSet)
		{
			this.heChengBtn[0].gameObject.SetActive(true);
			this.heChengBtn[0].GetComponent<UIButton>().enabled = true;
			this.heChengBtn[0].GetComponent<BoxCollider>().enabled = true;
			this.heChengBtn[0].GetComponent<UISprite>().ShaderToNormal();
			this.heChengBtn[1].gameObject.SetActive(true);
			this.heChengBtn[1].GetComponent<UIButton>().enabled = false;
			this.heChengBtn[1].GetComponent<BoxCollider>().enabled = false;
			this.heChengBtn[1].GetComponent<UISprite>().ShaderToGray();
		}
		else
		{
			this.heChengBtn[0].gameObject.SetActive(true);
			this.heChengBtn[0].GetComponent<BoxCollider>().enabled = false;
			this.heChengBtn[0].GetComponent<UIButton>().enabled = false;
			this.heChengBtn[0].GetComponent<UISprite>().ShaderToGray();
			this.heChengBtn[1].gameObject.SetActive(true);
			this.heChengBtn[1].GetComponent<UISprite>().ShaderToNormal();
			this.heChengBtn[1].GetComponent<UIButton>().enabled = true;
			this.heChengBtn[1].GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void OnPanelUpdate()
	{
		this.heChengBtn[0].gameObject.SetActive(true);
		this.heChengBtn[0].GetComponent<BoxCollider>().enabled = true;
		this.heChengBtn[1].gameObject.SetActive(true);
		this.heChengBtn[1].GetComponent<UISprite>().ShaderToGray();
		this.heChengBtn[1].GetComponent<BoxCollider>().enabled = false;
	}

	public void OnSendHttp(GameObject o)
	{
		CSEquipMix cSEquipMix = new CSEquipMix();
		cSEquipMix.equipId = this.itemId;
		cSEquipMix.equip.AddRange(this.itemList);
		ClientMgr.GetNet().SendHttp(7020, cSEquipMix, new DataHandler.OpcodeHandler(this.EquipSendCallBack), null);
	}

	private void EquipSendCallBack(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			List<SCOfficerEqu> list = opcode.Get<SCOfficerEqu>(10068);
			HeroInfo.GetInstance().EquipItem.RemoveAll((EquipItem a) => a.id == this.itemId);
			HeroInfo.GetInstance().EquipItem.RemoveAll((EquipItem a) => this.itemList.Contains(a.id));
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].id != this.itemId && !this.itemList.Contains(list[i].id))
				{
					AtlasManage.SetUiItemAtlas(this.NewItem, UnitConst.GetInstance().equipList[list[i].equId].icon);
					AtlasManage.SetQuilitySpriteName(this.NewItemBg, UnitConst.GetInstance().equipList[list[i].equId].equipQuality);
					ItemPanelManage.ins.OnEquipementItem();
					this.OnPanelShow(list[i].equId);
					ItemPanelManage.ins.OnCompoundClearDesty((long)list[i].equId);
					this.EquipMixList.Clear();
				}
			}
			this.itemList.Clear();
		}
	}

	public void OnPanelShow(int id)
	{
		ItemPanelManage.ins.newItemBg.SetActive(true);
		AtlasManage.SetUiItemAtlas(ItemPanelManage.ins.newItemSp, UnitConst.GetInstance().equipList[id].icon);
		ItemPanelManage.ins.newitemName.text = UnitConst.GetInstance().equipList[id].name;
		AtlasManage.SetQuilitySpriteName(ItemPanelManage.ins.itemquat, UnitConst.GetInstance().equipList[id].equipQuality);
		ItemPanelManage.ins.dressLevel.text = "穿戴等级" + UnitConst.GetInstance().equipList[id].level.ToString();
		ItemPanelManage.ins.PingZhiShow(ItemPanelManage.ins.newitemQuat, (int)UnitConst.GetInstance().equipList[id].equipQuality);
	}

	public void OnGetBg(string iconid, int id, long StaticId)
	{
		this.eqid = id;
		this.itemId = StaticId;
		this.PingZhiId = int.Parse(iconid);
		this.debrisItemBg.spriteName = ItemPanelManage.ins.SetQuSprite(int.Parse(iconid));
		this.debrisItemBg1.spriteName = ItemPanelManage.ins.SetQuSprite(int.Parse(iconid));
		this.NewItem.spriteName = string.Empty;
		this.NewItemBg.spriteName = "1";
	}
}
