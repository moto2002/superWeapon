using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleFightUIInfo : MonoBehaviour
{
	public static BattleFightUIInfo inst;

	public UILabel resLabel1;

	public UILabel resLabel2;

	public UILabel resLabel3;

	public UILabel resLabel4;

	public GameObject BattleFightUIInfoBtn;

	public UITable Table;

	public UILabel ItemLv;

	public UILabel ItemLvLabel;

	public GameObject itemPrefab;

	public GameObject res1;

	public GameObject res2;

	public GameObject res3;

	public GameObject res4;

	public GameObject sweepOne;

	public GameObject sweepTen;

	public UILabel name_Client;

	public UITable restable;

	public UISprite star1;

	public UISprite star2;

	public UISprite star3;

	public UILabel costNum;

	public UILabel sweepNum;

	public ResTips resTip;

	public BattleField curBattfiled;

	public void OnDestroy()
	{
		BattleFightUIInfo.inst = null;
	}

	private void Awake()
	{
		BattleFightUIInfo.inst = this;
		this.Initialize();
		base.gameObject.SetActive(false);
	}

	private void Initialize()
	{
		this.resLabel1 = base.transform.FindChild("Res/Table/res1/Label").GetComponent<UILabel>();
		this.resLabel2 = base.transform.FindChild("Res/Table/res2/Label").GetComponent<UILabel>();
		this.resLabel3 = base.transform.FindChild("Res/Table/res3/Label").GetComponent<UILabel>();
		this.resLabel4 = base.transform.FindChild("Res/Table/res4/Label").GetComponent<UILabel>();
		this.BattleFightUIInfoBtn = base.transform.FindChild("anniu").gameObject;
		this.Table = base.transform.FindChild("diaoluo/Table").GetComponent<UITable>();
		this.ItemLv = base.transform.FindChild("tiaojian/Lv").GetComponent<UILabel>();
		this.ItemLvLabel = base.transform.FindChild("tiaojian/Label").GetComponent<UILabel>();
		this.itemPrefab = base.transform.FindChild("diaoluo/Table/item").gameObject;
		this.res1 = base.transform.FindChild("Res/Table/res1").gameObject;
		this.res2 = base.transform.FindChild("Res/Table/res2").gameObject;
		this.res3 = base.transform.FindChild("Res/Table/res3").gameObject;
		this.res4 = base.transform.FindChild("Res/Table/res4").gameObject;
		this.sweepOne = base.transform.FindChild("topOne").gameObject;
		this.sweepTen = base.transform.FindChild("topTen").gameObject;
		this.name_Client = base.transform.FindChild("Sprite/Label").GetComponent<UILabel>();
		this.restable = base.transform.FindChild("Res/Table").GetComponent<UITable>();
		this.star1 = base.transform.FindChild("star/star1").GetComponent<UISprite>();
		this.star2 = base.transform.FindChild("star/star2").GetComponent<UISprite>();
		this.star3 = base.transform.FindChild("star/star3").GetComponent<UISprite>();
		this.costNum = base.transform.FindChild("xiaohao/Label").GetComponent<UILabel>();
		this.sweepNum = base.transform.FindChild("xianyou/Sprite/Label").GetComponent<UILabel>();
	}

	private void Start()
	{
		this.sweepNum.text = HeroInfo.GetInstance().GetItemCountById(2).ToString();
		this.resTip = HUDTextTool.inst.restip;
	}

	private void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (this.curBattfiled != null && opcodeCMD == 10003)
		{
			this.costNum.text = string.Format("{0}{1}/{2}", (this.curBattfiled.cost <= HeroInfo.GetInstance().playerRes.junLing) ? string.Empty : "[ff0000]", HeroInfo.GetInstance().playerRes.junLing, this.curBattfiled.cost);
		}
		this.sweepNum.text = HeroInfo.GetInstance().GetItemCountById(2).ToString();
	}

	private void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	public void ShowBattelFightInFo(BattleField battleField)
	{
		this.curBattfiled = battleField;
		this.resLabel1.text = string.Empty;
		this.resLabel2.text = string.Empty;
		this.resLabel3.text = string.Empty;
		this.resLabel4.text = string.Empty;
		this.Table.DestoryChildren(true);
		if (battleField.fightRecord.star > 2)
		{
			this.sweepOne.GetComponent<UISprite>().ShaderToNormal();
			this.sweepOne.GetComponentInChildren<UILabel>().color = Color.white;
			this.sweepOne.GetComponent<BoxCollider>().enabled = true;
			this.sweepTen.GetComponent<UISprite>().ShaderToNormal();
			this.sweepTen.GetComponentInChildren<UILabel>().color = Color.white;
			this.sweepTen.GetComponent<BoxCollider>().enabled = true;
		}
		else
		{
			this.sweepOne.GetComponent<UIButton>().enabled = false;
			this.sweepOne.GetComponent<BoxCollider>().enabled = false;
			this.sweepOne.GetComponent<UISprite>().ShaderToGray();
			this.sweepOne.transform.GetChild(0).GetComponent<UILabel>().color = Color.grey;
			this.sweepTen.GetComponent<UIButton>().enabled = false;
			this.sweepOne.GetComponent<BoxCollider>().enabled = false;
			this.sweepTen.GetComponent<UISprite>().ShaderToGray();
			this.sweepTen.transform.GetChild(0).GetComponent<UILabel>().color = Color.grey;
			this.sweepTen.GetComponent<BoxCollider>().enabled = false;
		}
		this.ItemLv.text = battleField.commondLevel.ToString();
		if (HeroInfo.GetInstance().PlayerCommondLv < battleField.commondLevel)
		{
			this.ItemLv.color = new Color(0.7294118f, 0.0117647061f, 0.0117647061f);
			this.ItemLvLabel.color = new Color(0.7294118f, 0.0117647061f, 0.0117647061f);
		}
		else
		{
			this.ItemLv.color = new Color(1f, 1f, 1f);
			this.ItemLvLabel.color = new Color(1f, 1f, 1f);
		}
		this.costNum.text = string.Format("{0}{1}/{2}", (battleField.cost <= HeroInfo.GetInstance().playerRes.junLing) ? string.Empty : "[ff0000]", HeroInfo.GetInstance().playerRes.junLing, battleField.cost);
		if (battleField.fightRecord.star <= 0)
		{
			this.star1.enabled = false;
			this.star2.enabled = false;
			this.star3.enabled = false;
		}
		else if (battleField.fightRecord.star == 1)
		{
			this.star1.enabled = true;
			this.star2.enabled = false;
			this.star3.enabled = false;
		}
		else if (battleField.fightRecord.star == 2)
		{
			this.star1.enabled = true;
			this.star2.enabled = true;
			this.star3.enabled = false;
		}
		else if (battleField.fightRecord.star == 3)
		{
			this.star1.enabled = true;
			this.star2.enabled = true;
			this.star3.enabled = true;
		}
		this.name_Client.text = battleField.name;
		if (!UnitConst.GetInstance().AllNpc.ContainsKey(battleField.npcId))
		{
			LogManage.Log(string.Format("NPC不包含{0}", battleField.npcId));
			return;
		}
		if (!UnitConst.GetInstance().AllDropList.ContainsKey(UnitConst.GetInstance().AllNpc[battleField.npcId].dropListId))
		{
			LogManage.Log(string.Format("AllDropList不包含{0}", UnitConst.GetInstance().AllNpc[battleField.npcId].dropListId));
			return;
		}
		this.res1.SetActive(false);
		this.res2.SetActive(false);
		this.res3.SetActive(false);
		this.res4.SetActive(false);
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().AllDropList[UnitConst.GetInstance().AllNpc[battleField.npcId].dropListId].res)
		{
			switch (current.Key)
			{
			case ResType.金币:
			{
				this.res1.SetActive(true);
				UILabel expr_4C3 = this.resLabel1;
				expr_4C3.text += current.Value;
				break;
			}
			case ResType.石油:
			{
				this.res2.SetActive(true);
				UILabel expr_4F6 = this.resLabel2;
				expr_4F6.text += current.Value;
				break;
			}
			case ResType.钢铁:
			{
				this.res3.SetActive(true);
				UILabel expr_529 = this.resLabel3;
				expr_529.text += current.Value;
				break;
			}
			case ResType.稀矿:
			{
				this.res4.SetActive(true);
				UILabel expr_55C = this.resLabel4;
				expr_55C.text += current.Value;
				break;
			}
			}
		}
		this.restable.Reposition();
		foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().AllDropList[UnitConst.GetInstance().AllNpc[battleField.npcId].dropListId].boxRate)
		{
			foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().AllBox[current2.Key].items)
			{
				GameObject gameObject = this.Table.CreateChildren(current3.Key.ToString(), true);
				gameObject.GetComponent<ItemTipsShow2>().JianTouPostion = 2;
				gameObject.GetComponent<ItemTipsShow2>().Index = current3.Key;
				gameObject.GetComponent<ItemTipsShow2>().Type = 1;
				AtlasManage.SetUiItemAtlas(gameObject.GetComponent<UISprite>(), UnitConst.GetInstance().ItemConst[current3.Key].IconId);
				AtlasManage.SetQuilitySpriteName(gameObject.transform.FindChild("Sprite").GetComponent<UISprite>(), UnitConst.GetInstance().ItemConst[current3.Key].Quality);
			}
			foreach (KeyValuePair<int, int> current4 in UnitConst.GetInstance().AllBox[current2.Key].equips)
			{
				GameObject gameObject2 = this.Table.CreateChildren(current4.Key.ToString(), false);
				gameObject2.GetComponent<ItemTipsShow2>().JianTouPostion = 2;
				gameObject2.GetComponent<ItemTipsShow2>().Index = current4.Key;
				gameObject2.GetComponent<ItemTipsShow2>().Type = 2;
				AtlasManage.SetUiItemAtlas(gameObject2.GetComponent<UISprite>(), UnitConst.GetInstance().equipList[current4.Key].icon);
				AtlasManage.SetQuilitySpriteName(gameObject2.transform.FindChild("Sprite").GetComponent<UISprite>(), UnitConst.GetInstance().equipList[current4.Key].equipQuality);
			}
		}
		this.Table.Reposition();
	}
}
