using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillEquipmentManage : FuncUIPanel
{
	public static SkillEquipmentManage inst;

	public List<SkillCarteItem> skillList = new List<SkillCarteItem>();

	public SkillKaCaoItem[] kacaoList;

	public UIGrid skillListParent;

	public UILabel skillCardInWarehouse;

	public GameObject BigCardItem;

	public UIScrollView ScrollView;

	public UISprite soldSkillBtn;

	public GameObject closeBtn;

	public float EquipSkillCard_time;

	private ButtonClick ClickItem;

	private Dictionary<int, List<SkillCarteData>> skillCount = new Dictionary<int, List<SkillCarteData>>();

	private int MaxKaCao;

	public override void Awake()
	{
		this.kacaoList = new SkillKaCaoItem[6];
		SkillEquipmentManage.inst = this;
		this.Initialize();
		this.Init();
	}

	public override void OnEnable()
	{
		this.ScrollView.ResetPosition();
		this.skillListParent.StartCoroutine(this.skillListParent.RepositionAfterFrame());
		this.SetSkillCardInWarehouseLable();
		base.OnEnable();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10075)
		{
			this.skillCardInWarehouse.text = string.Format("{0}/{1}", HeroInfo.GetInstance().skillCarteList.Count, UnitConst.GetInstance().HomeUpdateOpenSetDataConst[HeroInfo.GetInstance().PlayerCommondLv].skillBox);
		}
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	public void Update()
	{
		if (this.EquipSkillCard_time >= 0f)
		{
			this.EquipSkillCard_time -= Time.deltaTime;
		}
	}

	private void SetSkillCardInWarehouseLable()
	{
		this.skillCardInWarehouse.text = string.Format("{0}/{1}", HeroInfo.GetInstance().skillCarteList.Count, UnitConst.GetInstance().HomeUpdateOpenSetDataConst[HeroInfo.GetInstance().PlayerCommondLv].skillBox);
		this.soldSkillBtn.spriteName = "卖出";
	}

	private void Initialize()
	{
		this.skillListParent = base.transform.FindChild("SkillList/Grid").GetComponent<UIGrid>();
		for (int i = 0; i < 6; i++)
		{
			SkillKaCaoItem component = base.transform.FindChild("KacaoList/kacao" + (i + 1)).GetComponent<SkillKaCaoItem>();
			this.kacaoList[i] = component;
		}
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.SkillExtractPanel_ItemClick, new EventManager.VoidDelegate(this.ItemClick));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillEquipmentPanel_Close, new EventManager.VoidDelegate(this.CloseSkillEquipment));
		EventManager.Instance.AddEvent(EventManager.EventType.SkillEquipmentPanel_SkillSold, new EventManager.VoidDelegate(this.SkillSold));
	}

	private void ItemClick(GameObject ga)
	{
		if (!ga.GetComponent<SkillCarteItem>().isSkill)
		{
			if (ga.GetComponent<SkillCarteItem>().coinCost.activeSelf)
			{
				this.ClickItem = ga.GetComponent<ButtonClick>();
				this.ClickItem.IsCanDoEvent = false;
				CSSkillRemove cSSkillRemove = new CSSkillRemove();
				cSSkillRemove.skillId = this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID][0].id;
				this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID].RemoveAt(0);
				ga.GetComponent<SkillCarteItem>().count.text = LanguageManage.GetTextByKey("数量", "skill") + ":" + this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID].Count.ToString();
				if (this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID].Count < 1)
				{
					UnityEngine.Object.Destroy(ga);
				}
				cSSkillRemove.type = 1;
				ClientMgr.GetNet().SendHttp(2306, cSSkillRemove, new DataHandler.OpcodeHandler(this.SoldSkillCallBack), null);
				return;
			}
			bool flag = true;
			for (int i = 0; i < this.MaxKaCao; i++)
			{
				if (!this.kacaoList[i].isSkill)
				{
					flag = false;
				}
			}
			if (flag)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("卡槽已满", "skill"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			int j = 0;
			while (j < this.MaxKaCao)
			{
				if (!this.kacaoList[j].isSkill)
				{
					if (UnitConst.GetInstance().skillList[ga.GetComponent<SkillCarteItem>().item.itemID].skillVolume + j > this.MaxKaCao)
					{
						HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("卡槽不够", "skill"), HUDTextTool.TextUITypeEnum.Num5);
						return;
					}
					for (int k = 0; k < UnitConst.GetInstance().skillList[ga.GetComponent<SkillCarteItem>().item.itemID].skillVolume; k++)
					{
						if (this.kacaoList[j + k].isSkill)
						{
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("卡槽不够", "skill"), HUDTextTool.TextUITypeEnum.Num5);
							return;
						}
					}
					base.transform.DOShakePosition(0.3f, new Vector3(0f, -10f, 0f), 10, 90f, false);
					SkillCarteItem component = ga.GetComponent<SkillCarteItem>();
					int itemID = component.item.itemID;
					int skillVolume = UnitConst.GetInstance().skillList[itemID].skillVolume;
					for (int l = 0; l < skillVolume; l++)
					{
						SkillKaCaoItem component2 = this.kacaoList[j + l].GetComponent<SkillKaCaoItem>();
						if (l == 0)
						{
							ga.GetComponent<SkillCarteItem>().item.index = component2.Index;
						}
						if (!component2.isSkill)
						{
							DieBall dieBall = PoolManage.Ins.CreatEffect("jinengka_fang", component2.tr.position, Quaternion.identity, component2.tr);
							Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
							for (int m = 0; m < componentsInChildren.Length; m++)
							{
								Transform transform = componentsInChildren[m];
								transform.gameObject.layer = 8;
							}
							component2.skillItem.transform.localPosition = Vector3.zero;
							component2.skillItem.item = ga.GetComponent<SkillCarteItem>().item;
							component2.isSkill = true;
							component2.skillItem.isSkill = true;
							if (l != 0)
							{
								component2.GetComponent<UISprite>().color = Color.red;
							}
							else
							{
								component2.skillItem.gameObject.SetActive(true);
								component2.skillItem.ShowItem();
							}
							component2.ZhanYongIndexs.Clear();
							for (int n = 0; n < UnitConst.GetInstance().skillList[ga.GetComponent<SkillCarteItem>().item.itemID].skillVolume; n++)
							{
								component2.ZhanYongIndexs.Add(j + n);
							}
						}
					}
					CSSkillConfig cSSkillConfig = new CSSkillConfig();
					cSSkillConfig.skillId = this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID][0].id;
					this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID].RemoveAt(0);
					ga.GetComponent<SkillCarteItem>().count.text = LanguageManage.GetTextByKey("数量", "skill") + ":" + this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID].Count.ToString();
					cSSkillConfig.type = 1;
					AudioManage.inst.PlayAuido("equipmentskill", false);
					ClientMgr.GetNet().SendHttp(2304, cSSkillConfig, null, null);
					if (this.skillCount[ga.GetComponent<SkillCarteItem>().item.itemID].Count < 1)
					{
						UnityEngine.Object.Destroy(ga);
					}
					break;
				}
				else
				{
					j++;
				}
			}
		}
		else
		{
			if (this.soldSkillBtn.spriteName.Equals("卖出选中"))
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已上阵的不能出售", "skill"), HUDTextTool.TextUITypeEnum.Num1);
				return;
			}
			GameObject gameObject = NGUITools.AddChild(this.skillListParent.gameObject, this.BigCardItem);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			SkillCarteItem component3 = gameObject.GetComponent<SkillCarteItem>();
			component3.item = ga.GetComponent<SkillCarteItem>().item;
			SkillTipshow compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<SkillTipshow>(component3.tips.gameObject);
			component3.tipLabel.text = LanguageManage.GetTextByKey("占用卡槽", "skill") + ":" + UnitConst.GetInstance().skillList[component3.item.itemID].skillVolume;
			compentIfNoAddOne.Index = component3.item.itemID;
			compentIfNoAddOne.JianTouPostion = 2;
			compentIfNoAddOne.type = 1;
			if (ga.transform.parent.GetComponent<SkillKaCaoItem>() != null)
			{
				foreach (int current in ga.transform.parent.GetComponent<SkillKaCaoItem>().ZhanYongIndexs)
				{
					SkillKaCaoItem skillKaCaoItem = this.kacaoList[current];
					if (skillKaCaoItem.isSkill)
					{
						skillKaCaoItem.skillItem.gameObject.SetActive(false);
						skillKaCaoItem.isSkill = false;
						skillKaCaoItem.skillItem.isSkill = false;
						skillKaCaoItem.GetComponent<UISprite>().color = Color.white;
						DieBall dieBall2 = PoolManage.Ins.CreatEffect("jinengka_shou", skillKaCaoItem.tr.position, Quaternion.identity, skillKaCaoItem.tr);
						Transform[] componentsInChildren2 = dieBall2.GetComponentsInChildren<Transform>();
						for (int num = 0; num < componentsInChildren2.Length; num++)
						{
							Transform transform2 = componentsInChildren2[num];
							transform2.gameObject.layer = 8;
						}
					}
				}
				base.transform.DOShakePosition(0.3f, new Vector3(0f, 10f, 0f), 10, 90f, false);
			}
			ga.GetComponent<SkillCarteItem>().item.index = 0;
			CSSkillConfig cSSkillConfig2 = new CSSkillConfig();
			cSSkillConfig2.skillId = ga.GetComponent<SkillCarteItem>().item.id;
			cSSkillConfig2.type = 2;
			ClientMgr.GetNet().SendHttp(2304, cSSkillConfig2, new DataHandler.OpcodeHandler(this.SkillRemoveUPCallBack), null);
			component3.ShowItem();
			this.skillList.Add(component3);
			ga.gameObject.SetActive(false);
		}
		this.skillListParent.StartCoroutine(this.skillListParent.RepositionAfterFrame());
	}

	private void SoldSkillCallBack(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			if (this.ClickItem)
			{
				this.ClickItem.IsCanDoEvent = true;
			}
			if (this.skillCount.Count == 0 && this.soldSkillBtn.spriteName.Equals("卖出选中"))
			{
				this.soldSkillBtn.spriteName = "卖出";
			}
			foreach (SCSkillData current in HeroInfo.GetInstance().subSkill)
			{
				if (this.skillCount.ContainsKey(current.itemId))
				{
					for (int i = this.skillCount[current.itemId].Count - 1; i >= 0; i--)
					{
						if (this.skillCount[current.itemId][i].id == current.id)
						{
							this.skillCount[current.itemId].Remove(this.skillCount[current.itemId][i]);
						}
					}
				}
			}
			for (int j = this.skillList.Count - 1; j >= 0; j--)
			{
				SkillCarteItem skillCarteItem = this.skillList[j];
				if (this.skillCount[skillCarteItem.item.itemID].Count < 1)
				{
					this.skillList.Remove(skillCarteItem);
				}
			}
			this.skillListParent.StartCoroutine(this.skillListParent.RepositionAfterFrame());
		}
		else if (this.ClickItem)
		{
			this.ClickItem.IsCanDoEvent = true;
		}
	}

	private void SkillRemoveUPCallBack(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			this.ShowPanel();
		}
		else if (this.ClickItem)
		{
			this.ClickItem.IsCanDoEvent = true;
		}
	}

	private void SkillSold(GameObject ga)
	{
		if (ga.GetComponent<UISprite>().spriteName.Equals("卖出"))
		{
			if (this.skillList.Count > 0)
			{
				ga.GetComponent<UISprite>().spriteName = "卖出选中";
				foreach (SkillCarteItem current in this.skillList)
				{
					if (!current.isSkill && current.coinCost)
					{
						current.coinCost.SetActive(true);
					}
				}
			}
		}
		else
		{
			ga.GetComponent<UISprite>().spriteName = "卖出";
			foreach (SkillCarteItem current2 in this.skillList)
			{
				if (!current2.isSkill && current2.coinCost != null)
				{
					current2.coinCost.SetActive(false);
				}
			}
		}
	}

	public void CreateSkillItem()
	{
		for (int i = 0; i < this.skillList.Count; i++)
		{
			UnityEngine.Object.Destroy(this.skillList[i].ga);
		}
		this.skillList.Clear();
		this.skillCount.Clear();
		this.skillListParent.Reposition();
		for (int j = 0; j < HeroInfo.GetInstance().skillCarteList.Count; j++)
		{
			if (HeroInfo.GetInstance().skillCarteList[j].index == 0)
			{
				if (!this.skillCount.ContainsKey(HeroInfo.GetInstance().skillCarteList[j].itemID))
				{
					this.skillCount.Add(HeroInfo.GetInstance().skillCarteList[j].itemID, new List<SkillCarteData>());
				}
				this.skillCount[HeroInfo.GetInstance().skillCarteList[j].itemID].Add(HeroInfo.GetInstance().skillCarteList[j]);
			}
		}
		foreach (KeyValuePair<int, List<SkillCarteData>> current in this.skillCount)
		{
			GameObject gameObject = NGUITools.AddChild(this.skillListParent.gameObject, this.BigCardItem);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			SkillCarteItem component = gameObject.GetComponent<SkillCarteItem>();
			component.item = current.Value[0];
			component.count.text = LanguageManage.GetTextByKey("数量", "skill") + ":" + current.Value.Count.ToString();
			component.ShowItem();
			this.skillList.Add(component);
			SkillTipshow compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<SkillTipshow>(component.tips.gameObject);
			component.tipLabel.text = LanguageManage.GetTextByKey("占用卡槽", "skill") + ":" + UnitConst.GetInstance().skillList[component.item.itemID].skillVolume;
			compentIfNoAddOne.Index = current.Value[0].itemID;
			compentIfNoAddOne.JianTouPostion = 2;
			compentIfNoAddOne.type = 1;
		}
		base.StartCoroutine(this.skillListParent.RepositionAfterFrame());
	}

	private void KaCaoList()
	{
		List<SkillCarteData> list = new List<SkillCarteData>();
		int count = HeroInfo.GetInstance().skillCarteList.Count;
		for (int i = 1; i <= 20; i++)
		{
			for (int j = 2; j <= 4; j++)
			{
				for (int k = 0; k < count; k++)
				{
					if (UnitConst.GetInstance().skillList[HeroInfo.GetInstance().skillCarteList[k].itemID].skillType == i && UnitConst.GetInstance().skillList[HeroInfo.GetInstance().skillCarteList[k].itemID].skillQuality == (Quility_ResAndItemAndSkill)j)
					{
						list.Add(HeroInfo.GetInstance().skillCarteList[k]);
					}
				}
			}
		}
		HeroInfo.GetInstance().skillCarteList.Clear();
		HeroInfo.GetInstance().skillCarteList = list;
	}

	public void ShowKacao()
	{
		this.MaxKaCao = UnitConst.GetInstance().HomeUpdateOpenSetDataConst[HeroInfo.GetInstance().PlayerCommondLv].skillNum;
		for (int i = 0; i < this.kacaoList.Length; i++)
		{
			this.kacaoList[i].isSkill = false;
		}
		for (int j = 0; j < this.kacaoList.Length; j++)
		{
			SkillKaCaoItem skillKaCaoItem = this.kacaoList[j];
			if (j < this.MaxKaCao)
			{
				skillKaCaoItem.suo.SetActive(false);
				if (!skillKaCaoItem.isSkill)
				{
					skillKaCaoItem.skillItem.gameObject.SetActive(false);
					for (int k = 0; k < HeroInfo.GetInstance().skillCarteList.Count; k++)
					{
						this.kacaoList[j].GetComponent<UISprite>().color = Color.white;
						if (HeroInfo.GetInstance().skillCarteList[k].index == skillKaCaoItem.Index)
						{
							for (int l = 0; l < UnitConst.GetInstance().skillList[HeroInfo.GetInstance().skillCarteList[k].itemID].skillVolume; l++)
							{
								SkillKaCaoItem skillKaCaoItem2 = this.kacaoList[j + l];
								skillKaCaoItem2.skillItem.transform.localPosition = Vector3.zero;
								skillKaCaoItem2.skillItem.item = HeroInfo.GetInstance().skillCarteList[k];
								skillKaCaoItem2.isSkill = true;
								skillKaCaoItem2.skillItem.isSkill = true;
								if (l == 0)
								{
									skillKaCaoItem2.skillItem.gameObject.SetActive(true);
									skillKaCaoItem2.skillItem.ShowItem();
								}
								else
								{
									skillKaCaoItem2.skillItem.gameObject.SetActive(false);
									skillKaCaoItem2.GetComponent<UISprite>().color = Color.red;
								}
								skillKaCaoItem2.ZhanYongIndexs.Clear();
								for (int m = 0; m < UnitConst.GetInstance().skillList[HeroInfo.GetInstance().skillCarteList[k].itemID].skillVolume; m++)
								{
									skillKaCaoItem2.ZhanYongIndexs.Add(j + m);
								}
							}
						}
					}
				}
			}
			else
			{
				skillKaCaoItem.suo.SetActive(true);
				skillKaCaoItem.skillItem.gameObject.SetActive(false);
				foreach (HomeUpdateOpenSetData current in UnitConst.GetInstance().HomeUpdateOpenSetDataConst.Values)
				{
					if (skillKaCaoItem.Index == current.skillNum)
					{
						skillKaCaoItem.tiaojian.text = LanguageManage.GetTextByKey("司令部", "skill") + current.homeLevel + LanguageManage.GetTextByKey("级解锁", "skill");
						break;
					}
				}
			}
		}
	}

	public void ShowPanel()
	{
		this.CreateSkillItem();
		this.ShowKacao();
	}

	public void CloseSkillEquipment(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("SkillEquipmentPanel");
	}
}
