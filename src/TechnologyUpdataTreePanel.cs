using System;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyUpdataTreePanel : FuncUIPanel
{
	public enum TechnologyTreeType
	{
		经济 = 1,
		防御,
		进攻
	}

	public static TechnologyUpdataTreePanel _inst;

	public GameObject TreePanel_Economy;

	public GameObject TreePanel_Defense;

	public GameObject TreePanel_Attack;

	public TechnologyUpdataTreePoint Point_Prefab;

	public Dictionary<int, TechnologyUpdataTreePoint> TreePointList = new Dictionary<int, TechnologyUpdataTreePoint>();

	public UISprite BtnSprite_Economy;

	public UISprite BtnSprite_Defense;

	public UISprite BtnSprite_Attack;

	public UILabel BtnLabel_Economy;

	public UILabel BtnLabel_Defense;

	public UILabel BtnLabel_Attack;

	public List<GameObject> PointList;

	public UISprite ChooseSprite;

	public GameObject DesPanel_Ga;

	public UISprite DesPanel_Icon;

	public UILabel DesPanel_Name;

	public UILabel DesPanel_Level_Label;

	public UISprite DesPanel_Level_Line;

	public UILabel DesPanel_NowDes;

	public UILabel DesPanel_UpdataDes;

	public UILabel DesPanel_UpdataCDLabel;

	public UISprite DesPanel_UpdataBtn;

	public UILabel DesPanel_UpdataBtn_Label;

	public TechnologyUpdataTreePoint NowChoosePoint;

	public GameObject ResPrefab;

	public GameObject ItemPrefab;

	public GameObject SkillPrefab;

	public UIGrid CostResGrid;

	public UIGrid CostItemGrid;

	public TechnologyUpdataTreePanel.TechnologyTreeType NowTechnologyTreeType = TechnologyUpdataTreePanel.TechnologyTreeType.经济;

	private new void Awake()
	{
		TechnologyUpdataTreePanel._inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.TechnologyUpdateTreePanel_Close, new EventManager.VoidDelegate(this.TechnologyUpdateTreePanel_Close));
		EventManager.Instance.AddEvent(EventManager.EventType.TechnologyUpdateTreePanel_Btn_Economy, new EventManager.VoidDelegate(this.TechnologyUpdateTreePanel_Btn_Economy));
		EventManager.Instance.AddEvent(EventManager.EventType.TechnologyUpdateTreePanel_Btn_Defense, new EventManager.VoidDelegate(this.TechnologyUpdateTreePanel_Btn_Defense));
		EventManager.Instance.AddEvent(EventManager.EventType.TechnologyUpdateTreePanel_Btn_Attack, new EventManager.VoidDelegate(this.TechnologyUpdateTreePanel_Btn_Attack));
		EventManager.Instance.AddEvent(EventManager.EventType.TechnologyUpdateTreePanel_Ponit, new EventManager.VoidDelegate(this.TechnologyUpdateTreePanel_Ponit));
		EventManager.Instance.AddEvent(EventManager.EventType.TechnologyUpdateTreePanel_DoWork, new EventManager.VoidDelegate(this.TechnologyUpdateTreePanel_DoWork));
		this.TreePointList.Clear();
		for (int i = 0; i < this.PointList.Count; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.PointList[i].gameObject, this.Point_Prefab.gameObject);
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.GetComponent<TechnologyUpdataTreePoint>().BG = this.PointList[i].GetComponent<UISprite>();
			gameObject.transform.GetComponent<TechnologyUpdataTreePoint>().Name.text = UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)(i + 1), 1f)].name;
			gameObject.transform.GetComponent<TechnologyUpdataTreePoint>().SetInfo();
			this.TreePointList.Add(i + 1, gameObject.transform.GetComponent<TechnologyUpdataTreePoint>());
		}
	}

	private void TechnologyUpdateTreePanel_Close(GameObject ga)
	{
		FuncUIManager.inst.HideFuncUI("TechnologyUpdateTreePanel");
	}

	private void TechnologyUpdateTreePanel_Btn_Economy(GameObject ga)
	{
		this.NowTechnologyTreeType = TechnologyUpdataTreePanel.TechnologyTreeType.经济;
		this.TreePanel_Economy.gameObject.SetActive(true);
		this.TreePanel_Defense.gameObject.SetActive(false);
		this.TreePanel_Attack.gameObject.SetActive(false);
		this.BtnSprite_Economy.spriteName = "选中状态按钮";
		this.BtnSprite_Defense.spriteName = "未选中状态";
		this.BtnSprite_Attack.spriteName = "未选中状态";
		this.BtnLabel_Economy.effectStyle = UILabel.Effect.Outline;
		this.BtnLabel_Economy.color = Color.white;
		this.BtnLabel_Defense.effectStyle = UILabel.Effect.None;
		this.BtnLabel_Defense.color = Color.gray;
		this.BtnLabel_Attack.effectStyle = UILabel.Effect.None;
		this.BtnLabel_Attack.color = Color.gray;
		this.TechnologyUpdateTreePanel_Ponit(this.TreePanel_Economy.transform.FindChild("1").GetComponentInChildren<TechnologyUpdataTreePoint>().Icon.gameObject);
	}

	private void TechnologyUpdateTreePanel_Btn_Defense(GameObject ga)
	{
		this.NowTechnologyTreeType = TechnologyUpdataTreePanel.TechnologyTreeType.防御;
		this.TreePanel_Economy.gameObject.SetActive(false);
		this.TreePanel_Defense.gameObject.SetActive(true);
		this.TreePanel_Attack.gameObject.SetActive(false);
		this.BtnSprite_Economy.spriteName = "未选中状态";
		this.BtnSprite_Defense.spriteName = "选中状态按钮";
		this.BtnSprite_Attack.spriteName = "未选中状态";
		this.BtnLabel_Economy.effectStyle = UILabel.Effect.None;
		this.BtnLabel_Economy.color = Color.gray;
		this.BtnLabel_Defense.effectStyle = UILabel.Effect.Outline;
		this.BtnLabel_Defense.color = Color.white;
		this.BtnLabel_Attack.effectStyle = UILabel.Effect.None;
		this.BtnLabel_Attack.color = Color.gray;
		this.TechnologyUpdateTreePanel_Ponit(this.TreePanel_Defense.transform.FindChild("11").GetComponentInChildren<TechnologyUpdataTreePoint>().Icon.gameObject);
	}

	private void TechnologyUpdateTreePanel_Btn_Attack(GameObject ga)
	{
		this.NowTechnologyTreeType = TechnologyUpdataTreePanel.TechnologyTreeType.进攻;
		this.TreePanel_Economy.gameObject.SetActive(false);
		this.TreePanel_Defense.gameObject.SetActive(false);
		this.TreePanel_Attack.gameObject.SetActive(true);
		this.BtnSprite_Economy.spriteName = "未选中状态";
		this.BtnSprite_Defense.spriteName = "未选中状态";
		this.BtnSprite_Attack.spriteName = "选中状态按钮";
		this.BtnLabel_Economy.effectStyle = UILabel.Effect.None;
		this.BtnLabel_Economy.color = Color.gray;
		this.BtnLabel_Defense.effectStyle = UILabel.Effect.None;
		this.BtnLabel_Defense.color = Color.gray;
		this.BtnLabel_Attack.effectStyle = UILabel.Effect.Outline;
		this.BtnLabel_Attack.color = Color.white;
		this.TechnologyUpdateTreePanel_Ponit(this.TreePanel_Attack.transform.FindChild("22").GetComponentInChildren<TechnologyUpdataTreePoint>().Icon.gameObject);
	}

	private void TechnologyUpdateTreePanel_Ponit(GameObject ga)
	{
		this.SetDesPanel(ga.transform.parent.GetComponent<TechnologyUpdataTreePoint>());
	}

	private void TechnologyUpdateTreePanel_DoWork(GameObject ga)
	{
		if (!this.NowChoosePoint.UnLock)
		{
			return;
		}
		if (HeroInfo.GetInstance().PlayerTechnologyUpdatingTime > 0L && HeroInfo.GetInstance().PlayerTechnologyUpdatingItemID == this.NowChoosePoint.PointID)
		{
			CalcMoneyHandler.CSCalcMoney(1, 5, 0, (long)this.NowChoosePoint.PointID, this.NowChoosePoint.PointID, 0, delegate(bool isBuy, int Money)
			{
				if (isBuy)
				{
					if (HeroInfo.GetInstance().playerRes.RMBCoin < Money)
					{
						ShopPanelManage.ShowHelp_NoRMB(null, null);
						return;
					}
					TechHandler.CG_CSTechUpEnd(this.NowChoosePoint.PointID, Money, delegate
					{
						this.SetDesPanel(this.NowChoosePoint);
					});
				}
			});
			return;
		}
		if (HeroInfo.GetInstance().PlayerTechnologyInfo[this.NowChoosePoint.PointID] >= this.NowChoosePoint.MaxLevel)
		{
			return;
		}
		if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)this.NowChoosePoint.PointID, (float)(HeroInfo.GetInstance().PlayerTechnologyInfo[this.NowChoosePoint.PointID] + 1))].buildingLevel > HeroInfo.GetInstance().PlayerTechBuildingLv)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("科技中心建筑等级不足，要求：", "build") + UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)this.NowChoosePoint.PointID, (float)(HeroInfo.GetInstance().PlayerTechnologyInfo[this.NowChoosePoint.PointID] + 1))].buildingLevel, HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)this.NowChoosePoint.PointID, (float)HeroInfo.GetInstance().PlayerTechnologyInfo[this.NowChoosePoint.PointID])].itemCost)
		{
			if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current.Key))
			{
				HUDTextTool.inst.SetText(string.Format("{0}数量不足", UnitConst.GetInstance().ItemConst[current.Key].Name), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			if (current.Value > HeroInfo.GetInstance().PlayerItemInfo[current.Key])
			{
				HUDTextTool.inst.SetText(string.Format("{0}数量不足", UnitConst.GetInstance().ItemConst[current.Key].Name), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		CalcMoneyHandler.CSCalcMoney(3, 0, 0, (long)this.NowChoosePoint.PointID, this.NowChoosePoint.PointID, 0, delegate(bool isBuy, int Money)
		{
			if (isBuy)
			{
				if (HeroInfo.GetInstance().playerRes.RMBCoin < Money)
				{
					ShopPanelManage.ShowHelp_NoRMB(null, null);
					return;
				}
				TechHandler.CG_TechUpdate(this.NowChoosePoint.PointID, Money, delegate
				{
				});
			}
		});
	}

	private void SetDesPanel(TechnologyUpdataTreePoint point)
	{
		if (!point)
		{
			return;
		}
		this.NowChoosePoint = point;
		this.ChooseSprite.transform.position = point.transform.position;
		this.DesPanel_Icon.spriteName = point.Icon.spriteName;
		this.DesPanel_Icon.color = point.Icon.color;
		this.DesPanel_Name.text = point.Name.text;
		if (HeroInfo.GetInstance().PlayerTechnologyInfo.ContainsKey(point.PointID))
		{
			this.DesPanel_Level_Label.text = string.Format("{0}/{1}", HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID], point.MaxLevel);
			this.DesPanel_Level_Line.fillAmount = (float)HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID] / (float)point.MaxLevel;
		}
		else
		{
			this.DesPanel_Level_Label.text = string.Format("0/{0}", point.MaxLevel);
			this.DesPanel_Level_Line.fillAmount = 0f;
		}
		this.CostResGrid.ClearChild();
		this.CostResGrid.Reposition();
		this.CostItemGrid.ClearChild();
		this.CostItemGrid.Reposition();
		if (HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID] >= point.MaxLevel)
		{
			this.DesPanel_NowDes.text = UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)point.PointID, (float)HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID])].des;
			this.DesPanel_UpdataDes.text = "已满级";
			this.DesPanel_UpdataCDLabel.text = string.Empty;
		}
		else
		{
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)point.PointID, (float)HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID])].resCost)
			{
				GameObject gameObject = NGUITools.AddChild(this.CostResGrid.gameObject, this.ResPrefab);
				GameObject gameObject2 = NGUITools.AddChild(this.CostResGrid.gameObject, this.ResPrefab);
				gameObject.gameObject.transform.localScale = Vector3.one * 0.75f;
				gameObject2.gameObject.transform.localScale = Vector3.one * 0.75f;
				ActivityRes component = gameObject.GetComponent<ActivityRes>();
				ActivityRes component2 = gameObject2.GetComponent<ActivityRes>();
				switch (current.Key)
				{
				case ResType.金币:
					AtlasManage.SetResSpriteName(component.icon, ResType.金币);
					component.count.text = current.Value.ToString();
					AtlasManage.SetResSpriteName(component2.icon, ResType.金币);
					component2.count.text = HeroInfo.GetInstance().playerRes.resCoin.ToString();
					if (HeroInfo.GetInstance().playerRes.resCoin < current.Value)
					{
						component2.count.color = Color.red;
					}
					else
					{
						component2.count.color = Color.white;
					}
					break;
				case ResType.石油:
					AtlasManage.SetResSpriteName(component.icon, ResType.石油);
					component.count.text = current.Value.ToString();
					AtlasManage.SetResSpriteName(component2.icon, ResType.石油);
					component2.count.text = HeroInfo.GetInstance().playerRes.resOil.ToString();
					if (HeroInfo.GetInstance().playerRes.resOil < current.Value)
					{
						component2.count.color = Color.red;
					}
					else
					{
						component2.count.color = Color.white;
					}
					break;
				case ResType.钢铁:
					AtlasManage.SetResSpriteName(component.icon, ResType.钢铁);
					component.count.text = current.Value.ToString();
					AtlasManage.SetResSpriteName(component2.icon, ResType.钢铁);
					component2.count.text = HeroInfo.GetInstance().playerRes.resSteel.ToString();
					if (HeroInfo.GetInstance().playerRes.resSteel < current.Value)
					{
						component2.count.color = Color.red;
					}
					else
					{
						component2.count.color = Color.white;
					}
					break;
				case ResType.稀矿:
					AtlasManage.SetResSpriteName(component.icon, ResType.稀矿);
					component.count.text = current.Value.ToString();
					AtlasManage.SetResSpriteName(component2.icon, ResType.稀矿);
					component2.count.text = HeroInfo.GetInstance().playerRes.resRareEarth.ToString();
					if (HeroInfo.GetInstance().playerRes.resRareEarth < current.Value)
					{
						component2.count.color = Color.red;
					}
					else
					{
						component2.count.color = Color.white;
					}
					break;
				case ResType.钻石:
					AtlasManage.SetResSpriteName(component.icon, ResType.钻石);
					component.count.text = current.Value.ToString();
					AtlasManage.SetResSpriteName(component2.icon, ResType.钻石);
					component2.count.text = HeroInfo.GetInstance().playerRes.RMBCoin.ToString();
					if (HeroInfo.GetInstance().playerRes.RMBCoin < current.Value)
					{
						component2.count.color = Color.red;
					}
					else
					{
						component2.count.color = Color.white;
					}
					break;
				}
			}
			foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)point.PointID, (float)HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID])].itemCost)
			{
				GameObject gameObject3 = NGUITools.AddChild(this.CostItemGrid.gameObject, this.ItemPrefab);
				gameObject3.gameObject.transform.localScale = Vector3.one * 0.75f;
				ActivityItemPre component3 = gameObject3.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component3.icon, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component3.quality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
				component3.count.text = current2.Value.ToString();
				component3.name.text = string.Empty;
				LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[current2.Key].Name, "item");
				ItemTipsShow2 component4 = component3.GetComponent<ItemTipsShow2>();
				component4.Index = current2.Key;
				component4.Type = 1;
				GameObject gameObject4 = NGUITools.AddChild(this.CostItemGrid.gameObject, this.ItemPrefab);
				gameObject4.gameObject.transform.localScale = Vector3.one * 0.75f;
				ActivityItemPre component5 = gameObject4.GetComponent<ActivityItemPre>();
				AtlasManage.SetUiItemAtlas(component5.icon, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
				AtlasManage.SetQuilitySpriteName(component5.quality, UnitConst.GetInstance().ItemConst[current2.Key].Quality);
				if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current2.Key))
				{
					component5.count.text = HeroInfo.GetInstance().PlayerItemInfo[current2.Key].ToString();
					if (HeroInfo.GetInstance().PlayerItemInfo[current2.Key] < current2.Value)
					{
						component5.count.color = Color.red;
					}
					else
					{
						component5.count.color = Color.white;
					}
				}
				else
				{
					component5.count.text = "0";
					component5.count.color = Color.red;
				}
				component5.name.text = string.Empty;
				LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[current2.Key].Name, "item");
				ItemTipsShow2 component6 = component5.GetComponent<ItemTipsShow2>();
				component6.Index = current2.Key;
				component6.Type = 1;
			}
			base.StartCoroutine(this.CostResGrid.RepositionAfterFrame());
			base.StartCoroutine(this.CostItemGrid.RepositionAfterFrame());
			this.DesPanel_NowDes.text = UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)point.PointID, (float)HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID])].des;
			this.DesPanel_UpdataDes.text = UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)point.PointID, (float)(HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID] + 1))].des;
			DateTime dateTime = DateTime.Parse(DateTime.Now.ToString("1970-01-01\u00a000:00:00")).AddSeconds((double)UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)point.PointID, (float)HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID])].costTime);
			string arg = string.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
			if (dateTime.Second >= 10)
			{
				text2 = dateTime.Second.ToString();
			}
			else
			{
				text2 = "0" + dateTime.Second.ToString();
			}
			if (dateTime.Minute >= 10)
			{
				text = dateTime.Minute.ToString();
			}
			else
			{
				text = "0" + dateTime.Minute.ToString();
			}
			if (dateTime.Hour > 0)
			{
				if (dateTime.Hour >= 10)
				{
					arg = dateTime.Hour.ToString();
				}
				else
				{
					arg = "0" + dateTime.Hour.ToString();
				}
				this.DesPanel_UpdataCDLabel.text = string.Format("升级时间：{0}:{1}:{2}", arg, text, text2);
			}
			else
			{
				this.DesPanel_UpdataCDLabel.text = string.Format("升级时间：{0}:{1}", text, text2);
			}
		}
		if (point.UnLock)
		{
			this.DesPanel_UpdataBtn.spriteName = "blue";
			this.DesPanel_UpdataBtn_Label.text = string.Format("研发", new object[0]);
			if (HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID] == 0)
			{
				this.DesPanel_UpdataBtn_Label.text = string.Format("激活", new object[0]);
			}
			if (HeroInfo.GetInstance().PlayerTechnologyInfo[point.PointID] >= point.MaxLevel)
			{
				this.DesPanel_UpdataBtn.spriteName = "hui";
				this.DesPanel_UpdataBtn_Label.text = string.Format("已满级", new object[0]);
			}
		}
		else
		{
			this.DesPanel_UpdataBtn.spriteName = "hui";
			this.DesPanel_UpdataBtn_Label.text = string.Format("尚未解锁", new object[0]);
		}
	}

	private void Start()
	{
	}

	public override void OnEnable()
	{
		base.OnEnable();
		this.TechnologyUpdateTreePanel_Btn_Economy(null);
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10021)
		{
			foreach (KeyValuePair<int, TechnologyUpdataTreePoint> current in this.TreePointList)
			{
				current.Value.SetInfo();
			}
			this.SetDesPanel(this.NowChoosePoint);
		}
	}

	private new void OnDisable()
	{
		base.OnDisable();
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void Update()
	{
		if (this.NowChoosePoint)
		{
			if (HeroInfo.GetInstance().PlayerTechnologyUpdatingTime > 0L)
			{
				if (HeroInfo.GetInstance().PlayerTechnologyUpdatingItemID == this.NowChoosePoint.PointID)
				{
					TimeSpan timeSpan = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerTechnologyUpdatingTime) - TimeTools.GetNowTimeSyncServerToDateTime();
					this.DesPanel_UpdataCDLabel.text = string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
					this.DesPanel_UpdataBtn_Label.text = string.Format("立即结束冷却", new object[0]);
				}
			}
			else if (this.DesPanel_UpdataBtn_Label.text == string.Format("立即结束冷却", new object[0]))
			{
				this.SetDesPanel(this.NowChoosePoint);
			}
		}
	}
}
