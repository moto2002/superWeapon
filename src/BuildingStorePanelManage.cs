using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingStorePanelManage : FuncUIPanel
{
	public enum BuildingStoreBtnType
	{
		economyBtn,
		defenseBtn,
		supportBtn,
		ItemClick
	}

	public static BuildingStorePanelManage _instance;

	public GameObject fadianchang;

	public GameObject kejiZhongxin;

	public GameObject feijichang;

	public GameObject JunTunZongBu;

	public GameObject jiqiangta;

	public GameObject kuangshiBtn;

	public GameObject satelliteBtn;

	public GameObject DrillingBtn;

	public GameObject WallBtn;

	public static int BuildingResType;

	public GameObject steelWarehouseBtn;

	public GameObject steelworksBtn;

	public GameObject chariotBtn;

	public GameObject BingYing;

	public GameObject tankfarmBtn;

	public Transform sniperTower;

	public GameObject coffersBtn;

	public GameObject economyBtn;

	public GameObject defenseBtn;

	public GameObject supportBtn;

	public GameObject BuildingItemInfoPanel;

	public GameObject slideTiao;

	public UIGrid itemGrid;

	public GameObject ResCoin;

	public GameObject ResOil;

	public GameObject ResSteel;

	public GameObject ResRareEarth;

	public GameObject Rmb;

	public UISprite ResCoinSp;

	public UISprite ResOilSp;

	public UISprite ResSteelSp;

	public UISprite ResRareEarthSp;

	public GameObject itemPrefab;

	public GameObject jingjiNotice;

	public GameObject fangYuNotice;

	public GameObject zhichiNotice;

	public List<buildingItem> allBuilding;

	public List<buildingItem> buildingitem;

	public GameObject loding;

	public static int Buildtype = 1;

	public UITexture bulingBacNoraml;

	public UITexture bulingBacGray;

	public buildingItem Basic_Go;

	public bool IsTryBuilding;

	public void OnDestroy()
	{
		BuildingStorePanelManage._instance = null;
	}

	public override void Awake()
	{
		BuildingStorePanelManage._instance = this;
		this.InitEvent();
		this.buildingitem = new List<buildingItem>();
		this.allBuilding = new List<buildingItem>();
		this.itemGrid.transform.localPosition = new Vector3(185.2029f, 600f, 0f);
		this.itemGrid.isRespositonOnStart = false;
		this.OnUpdatabudingInfo();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_defenseBtn, new EventManager.VoidDelegate(this.OnClickDefensebtn));
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_EconomyBtn, new EventManager.VoidDelegate(this.OnClickEconomyBtn));
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_supportBtn, new EventManager.VoidDelegate(this.OnClickSupportBtn));
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_ItemClick, new EventManager.VoidDelegate(this.OnClickItem));
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_ItemClose, new EventManager.VoidDelegate(this.OnPanelClose));
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_ItemInfoClick, new EventManager.VoidDelegate(this.OnItemInfoPanelShow));
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_ItemInfoCloseClick, new EventManager.VoidDelegate(this.OnBuildingItemInfoPanelClose));
	}

	private void OnBuildingItemInfoPanelClose(GameObject go)
	{
		this.BuildingItemInfoPanel.SetActive(false);
		if (this.BuildingItemInfoPanel.GetComponent<ItemInfo>().model)
		{
			UnityEngine.Object.Destroy(this.BuildingItemInfoPanel.GetComponent<ItemInfo>().model.ga);
		}
		this.IsTryBuilding = false;
	}

	public void OnResShow()
	{
		if (HeroInfo.GetInstance().playerRes.maxCoin > 0)
		{
			this.ResCoin.transform.localPosition = new Vector3(5f, 5f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxOil > 0)
		{
			this.ResCoin.transform.localPosition = new Vector3(-155f, 5f, 0f);
			this.ResOil.transform.localPosition = new Vector3(5f, 5f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxSteel > 0)
		{
			this.ResCoin.transform.localPosition = new Vector3(-385.3f, 5f, 0f);
			this.ResOil.transform.localPosition = new Vector3(-213f, 5f, 0f);
			this.ResSteel.transform.localPosition = new Vector3(-26.3f, 5f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxRareEarth > 0)
		{
			this.ResCoin.transform.localPosition = new Vector3(-465.11f, 5f, 0f);
			this.ResOil.transform.localPosition = new Vector3(-318f, 5f, 0f);
			this.ResSteel.transform.localPosition = new Vector3(-155f, 5f, 0f);
			this.ResRareEarth.transform.localPosition = new Vector3(5f, 5f, 0f);
		}
		this.ResCoinSp.GetComponentInChildren<UILabel>().text = HeroInfo.GetInstance().playerRes.resCoin.ToString();
		this.ResOilSp.GetComponentInChildren<UILabel>().text = HeroInfo.GetInstance().playerRes.resOil.ToString();
		this.ResSteelSp.GetComponentInChildren<UILabel>().text = HeroInfo.GetInstance().playerRes.resSteel.ToString();
		this.ResRareEarthSp.GetComponentInChildren<UILabel>().text = HeroInfo.GetInstance().playerRes.resRareEarth.ToString();
		this.ResCoinSp.fillAmount = (float)HeroInfo.GetInstance().playerRes.resCoin / (float)HeroInfo.GetInstance().playerRes.maxCoin;
		this.ResOilSp.fillAmount = (float)HeroInfo.GetInstance().playerRes.resOil / (float)HeroInfo.GetInstance().playerRes.maxOil;
		this.ResSteelSp.fillAmount = (float)HeroInfo.GetInstance().playerRes.resSteel / (float)HeroInfo.GetInstance().playerRes.maxSteel;
		this.ResRareEarthSp.fillAmount = (float)HeroInfo.GetInstance().playerRes.resRareEarth / (float)HeroInfo.GetInstance().playerRes.maxRareEarth;
		this.Rmb.GetComponentInChildren<UILabel>().text = HeroInfo.GetInstance().playerRes.RMBCoin.ToString();
	}

	private void OnItemInfoPanelShow(GameObject go)
	{
		int num = int.Parse(go.name.ToString());
		this.BuildingItemInfoPanel.SetActive(true);
		ItemInfo component = this.BuildingItemInfoPanel.GetComponent<ItemInfo>();
		component.ItemName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].name, "build");
		component.model = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].bodyID, component.ItemIcon.transform);
		if (component.model)
		{
			component.model.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].modelRostion[0], UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].modelRostion[1], UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].modelRostion[2]);
			component.model.tr.localScale = new Vector3(UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].TowerSize * 100f, UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].TowerSize * 100f, UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].TowerSize * 100f);
			component.model.tr.localPosition = new Vector3(UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].modlePosition[0], UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].modlePosition[1], UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].modlePosition[2]);
			Transform[] componentsInChildren = component.model.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 5;
			}
			if (component.model && component.model.RedModel)
			{
				NGUITools.SetActiveSelf(component.model.RedModel.gameObject, false);
			}
			if (component.model && component.model.Blue_DModel)
			{
				NGUITools.SetActiveSelf(component.model.Blue_DModel.gameObject, false);
			}
			if (component.model && component.model.Red_DModel)
			{
				NGUITools.SetActiveSelf(component.model.Red_DModel.gameObject, false);
			}
			if (component.model.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren2 = component.model.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].particleInfo != null)
				{
					int num2 = (componentsInChildren2.Length <= UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].particleInfo.Length) ? componentsInChildren2.Length : UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].particleInfo.Length;
					for (int j = 0; j < num2; j++)
					{
						componentsInChildren2[j].startSize.GetType();
						componentsInChildren2[j].startSize = UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].particleInfo[j];
					}
				}
			}
		}
		component.itemInfo.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].description, "build");
		UITable textTable = component.textTable;
		component.textTable.DestoryChildren(true);
		for (int k = 0; k < UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].uiShows.Count; k++)
		{
			switch (UnitConst.GetInstance().buildingConst[int.Parse(go.name.ToString())].uiShows[k])
			{
			case 1:
			{
				resTextInfo component2 = textTable.CreateChildren("金币产量", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.金币产量;
				component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputNum + string.Empty, 0, string.Empty);
				break;
			}
			case 2:
			{
				resTextInfo component2 = textTable.CreateChildren("金币储量", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.金币储量;
				component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit[ResType.金币] + string.Empty, 0, string.Empty);
				break;
			}
			case 3:
			{
				resTextInfo component2 = textTable.CreateChildren("石油产量", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.石油产量;
				component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputNum + string.Empty, 0, string.Empty);
				break;
			}
			case 4:
				if (UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit.ContainsKey(ResType.石油))
				{
					resTextInfo component2 = textTable.CreateChildren("石油储量", true).GetComponent<resTextInfo>();
					component2.curT_InfoTextType = T_InfoTextType.石油储量;
					component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit[ResType.石油] + string.Empty, 0, string.Empty);
				}
				break;
			case 5:
			{
				resTextInfo component2 = textTable.CreateChildren("钢铁产量", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.钢铁产量;
				component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputNum + string.Empty, 0, string.Empty);
				break;
			}
			case 6:
				if (UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit.ContainsKey(ResType.钢铁))
				{
					resTextInfo component2 = textTable.CreateChildren("钢铁储量", true).GetComponent<resTextInfo>();
					component2.curT_InfoTextType = T_InfoTextType.钢铁储量;
					component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit[ResType.钢铁] + string.Empty, 0, string.Empty);
				}
				break;
			case 7:
			{
				resTextInfo component2 = textTable.CreateChildren("稀土产量", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.稀土产量;
				component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputNum + string.Empty, 0, string.Empty);
				break;
			}
			case 8:
				if (UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit.ContainsKey(ResType.稀矿))
				{
					resTextInfo component2 = textTable.CreateChildren("稀矿储量", true).GetComponent<resTextInfo>();
					component2.curT_InfoTextType = T_InfoTextType.稀矿储量;
					component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit[ResType.稀矿] + string.Empty, 0, string.Empty);
				}
				break;
			case 9:
			{
				resTextInfo component2 = textTable.CreateChildren("生命", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.生命值;
				component2.SetT_InfoUpdateResText(InfoMgr.GetTowerFightData(num, 1, 0, 0, 0, null).life + string.Empty, 0, string.Empty);
				break;
			}
			case 11:
			{
				resTextInfo component2 = textTable.CreateChildren("攻击值", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.攻击值;
				component2.SetT_InfoUpdateResText(InfoMgr.GetTowerFightData(num, 1, 0, 0, 0, null).breakArmor + string.Empty, 0, string.Empty);
				break;
			}
			case 12:
			{
				resTextInfo component2 = textTable.CreateChildren("防御值", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.防御值;
				component2.SetT_InfoUpdateResText(InfoMgr.GetTowerFightData(num, 1, 0, 0, 0, null).defBreak + string.Empty, 0, string.Empty);
				break;
			}
			case 13:
			{
				resTextInfo component2 = textTable.CreateChildren("受保护百分比", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.受保护百分比;
				component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputNum + string.Empty, 0, string.Empty);
				break;
			}
			case 15:
			{
				resTextInfo component2 = textTable.CreateChildren("伤害值", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.伤害值;
				component2.SetT_InfoUpdateResText(InfoMgr.GetTowerFightData(num, 1, 0, 0, 0, null).breakArmor + string.Empty, 0, string.Empty);
				break;
			}
			case 17:
			{
				resTextInfo component2 = textTable.CreateChildren("最大容量", true).GetComponent<resTextInfo>();
				component2.curT_InfoTextType = T_InfoTextType.最大容量;
				BuildingStorePanelManage.BuildingResType = num;
				component2.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[num].lvInfos[1].outputLimit.Values.ToList<int>()[0] + string.Empty, 0, string.Empty);
				break;
			}
			}
		}
		component.textTable.Reposition();
	}

	public void OnUpdateInfo()
	{
		this.SortList();
	}

	public void OnGetItemBuild()
	{
		for (int i = 0; i < this.allBuilding.Count; i++)
		{
			this.allBuilding[i].Building = PoolManage.Ins.GetModelByBundleByName(this.allBuilding[i].BodyId, this.allBuilding[i].itemBuildingRoot.transform);
			if (this.allBuilding[i].Building)
			{
				if (this.allBuilding[i].Building && this.allBuilding[i].Building.BlueModel)
				{
					this.allBuilding[i].Building.BlueModel.gameObject.SetActive(true);
				}
				if (this.allBuilding[i].Building && this.allBuilding[i].Building.RedModel)
				{
					this.allBuilding[i].Building.RedModel.gameObject.SetActive(false);
				}
				if (this.allBuilding[i].Building && this.allBuilding[i].Building.Blue_DModel)
				{
					this.allBuilding[i].Building.Blue_DModel.gameObject.SetActive(false);
				}
				if (this.allBuilding[i].Building && this.allBuilding[i].Building.Red_DModel)
				{
					this.allBuilding[i].Building.Red_DModel.gameObject.SetActive(false);
				}
				if (this.allBuilding[i].Index == 8)
				{
					if (this.allBuilding[i].Building.tr.FindChild("p_Q_build8_3_1") != null)
					{
						UnityEngine.Object.Destroy(this.allBuilding[i].Building.tr.FindChild("p_Q_build8_3_1").gameObject);
					}
					if (this.allBuilding[i].Building.tr.FindChild("p_Q_build8_3_2") != null)
					{
						UnityEngine.Object.Destroy(this.allBuilding[i].Building.tr.FindChild("p_Q_build8_3_2").gameObject);
					}
					Body_Model effectByName = PoolManage.Ins.GetEffectByName("kuangshiyou", this.allBuilding[i].itemBuildingRoot.transform);
					if (effectByName)
					{
						effectByName.tr.localPosition = new Vector3(-11.6f, 22.1f, -120.9f);
						effectByName.tr.localRotation = Quaternion.Euler(new Vector3(-44f, 5f, -2f));
					}
				}
				if (this.allBuilding[i].Index == 11 && this.allBuilding[i].Building.tr.FindChild("p_Q_build3_3_1/1") != null)
				{
					UnityEngine.Object.Destroy(this.allBuilding[i].Building.tr.FindChild("p_Q_build3_3_1/1").gameObject);
				}
				this.allBuilding[i].Building.transform.localPosition = new Vector3(0f, 0f, -100f);
				if (this.allBuilding[i].Index == 23)
				{
					this.allBuilding[i].Building.transform.localPosition = new Vector3(0f, -30f, -100f);
				}
				Body_Model effectByName2 = PoolManage.Ins.GetEffectByName("jianzao_glow", this.allBuilding[i].transform);
				if (effectByName2)
				{
					this.allBuilding[i].LightEffect = effectByName2.ga;
				}
				this.allBuilding[i].OnSetBuilding();
			}
		}
		this.itemGrid.transform.localPosition = new Vector3(185.2029f, -80f, 0f);
	}

	public void OnUpdatabudingInfo()
	{
		this.itemGrid.ClearChild();
		for (int i = 0; i < UnitConst.GetInstance().buildingConst.Length; i++)
		{
			NewBuildingInfo newBuildingInfo = UnitConst.GetInstance().buildingConst[i];
			if (newBuildingInfo.storeType != 0)
			{
				GameObject gameObject = NGUITools.AddChild(this.itemGrid.gameObject, this.itemPrefab);
				gameObject.name = newBuildingInfo.resIdx.ToString();
				buildingItem component = gameObject.GetComponent<buildingItem>();
				ButtonClick buttonClick = gameObject.AddComponent<ButtonClick>();
				buttonClick.eventType = EventManager.EventType.BuildingStore_ItemClick;
				gameObject.transform.localPosition = Vector3.zero + new Vector3(0f, 600f, 0f);
				component.itemType = newBuildingInfo.storeType;
				component.buildingId = newBuildingInfo.resIdx;
				component.battleid = newBuildingInfo.battleIdFieldId;
				component.nameLabel.text = LanguageManage.GetTextByKey(newBuildingInfo.name, "build");
				component.BodyId = newBuildingInfo.bodyID;
				component.infobtn.name = newBuildingInfo.resIdx.ToString();
				component.Index = newBuildingInfo.resIdx;
				component.buildingConstInfo = newBuildingInfo;
				if (newBuildingInfo.lvInfos.Count >= 1)
				{
					if (newBuildingInfo.lvInfos[0].resCost.ContainsKey(ResType.石油))
					{
						int num = newBuildingInfo.lvInfos[0].resCost[ResType.石油];
						component.oilLabel.SetActive(true);
						component.oilLabel.GetComponentInChildren<UILabel>().text = num.ToString();
						component.oilLabel.GetComponentInChildren<ShowLabelColor>().resNum = num;
						component.resGrid.transform.localPosition = new Vector3(-100.3f, -79.38f, 0f);
						if (num > HeroInfo.GetInstance().playerRes.resOil)
						{
							component.oilLabel.GetComponentInChildren<UILabel>().color = new Color(1f, 0.1882353f, 0.101960786f);
						}
						else
						{
							component.oilLabel.GetComponentInChildren<UILabel>().color = Color.white;
						}
					}
					if (newBuildingInfo.lvInfos[0].resCost.ContainsKey(ResType.钢铁))
					{
						int num2 = newBuildingInfo.lvInfos[0].resCost[ResType.钢铁];
						component.steelLabel.SetActive(true);
						component.steelLabel.GetComponentInChildren<UILabel>().text = num2.ToString();
						component.resGrid.transform.localPosition = new Vector3(-100.3f, -46.1f, 0f);
						component.oilLabel.GetComponentInChildren<ShowLabelColor>().resNum = num2;
						if (num2 > HeroInfo.GetInstance().playerRes.resSteel)
						{
							component.steelLabel.GetComponentInChildren<UILabel>().color = new Color(1f, 0.1882353f, 0.101960786f);
						}
						else
						{
							component.steelLabel.GetComponentInChildren<UILabel>().color = Color.white;
						}
					}
					if (newBuildingInfo.lvInfos[0].resCost.ContainsKey(ResType.稀矿))
					{
						int num3 = newBuildingInfo.lvInfos[0].resCost[ResType.稀矿];
						component.rareEarthLabel.SetActive(true);
						component.rareEarthLabel.GetComponentInChildren<UILabel>().text = num3.ToString();
						component.resGrid.transform.localPosition = new Vector3(-100.3f, -5.2f, 0f);
						component.oilLabel.GetComponentInChildren<ShowLabelColor>().resNum = num3;
						if (num3 > HeroInfo.GetInstance().playerRes.resRareEarth)
						{
							component.rareEarthLabel.GetComponentInChildren<UILabel>().color = new Color(1f, 0.1882353f, 0.101960786f);
						}
						else
						{
							component.rareEarthLabel.GetComponentInChildren<UILabel>().color = Color.white;
						}
					}
				}
				if (newBuildingInfo.lvInfos.Count > 1)
				{
					component.timerLabel.text = newBuildingInfo.lvInfos[0].BuildTime.ToString();
				}
				gameObject.SetActive(false);
				this.allBuilding.Add(component);
			}
		}
		this.sniperTower = base.transform.FindChild("Scroll View/Grid/16");
		this.coffersBtn = base.transform.FindChild("Scroll View/Grid/2").gameObject;
		this.satelliteBtn = base.transform.FindChild("Scroll View/Grid/10").gameObject;
		this.DrillingBtn = base.transform.FindChild("Scroll View/Grid/7").gameObject;
		this.WallBtn = base.transform.FindChild("Scroll View/Grid/90").gameObject;
		this.tankfarmBtn = base.transform.FindChild("Scroll View/Grid/3").gameObject;
		this.chariotBtn = base.transform.FindChild("Scroll View/Grid/13").gameObject;
		this.BingYing = base.transform.FindChild("Scroll View/Grid/94").gameObject;
		this.steelworksBtn = base.transform.FindChild("Scroll View/Grid/8").gameObject;
		this.steelWarehouseBtn = base.transform.FindChild("Scroll View/Grid/4").gameObject;
		this.kuangshiBtn = base.transform.FindChild("Scroll View/Grid/6").gameObject;
		this.jiqiangta = base.transform.FindChild("Scroll View/Grid/17").gameObject;
		this.fadianchang = base.transform.FindChild("Scroll View/Grid/62").gameObject;
		this.kejiZhongxin = base.transform.FindChild("Scroll View/Grid/11").gameObject;
		this.feijichang = base.transform.FindChild("Scroll View/Grid/91").gameObject;
		this.OnGetItemBuild();
	}

	private void OnPanelClose(GameObject go)
	{
		FuncUIManager.inst.DestoryFuncUI("BuildingStorePanel");
	}

	public void OnClickDefensebtn(GameObject ga)
	{
		this.selectBtnClick(2);
	}

	public void OnClickEconomyBtn(GameObject ga)
	{
		this.selectBtnClick(1);
	}

	public void OnClickSupportBtn(GameObject ga)
	{
		this.selectBtnClick(3);
	}

	public void OnClickItem(GameObject ga)
	{
		this.ClickItem(ga);
	}

	public void selectBtnClick(int id)
	{
		for (int i = 0; i < this.allBuilding.Count; i++)
		{
			buildingItem buildingItem = this.allBuilding[i];
			buildingItem.transform.localPosition = new Vector3(0f, 600f, 0f);
			buildingItem.itemBuildingRoot.localScale = Vector3.zero;
			buildingItem.gameObject.SetActive(buildingItem.itemType == id);
			Transform[] componentsInChildren = buildingItem.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].gameObject.layer = 5;
			}
		}
		this.SortList();
		this.SetBtn(id);
		this.OnBuildItemShow();
	}

	public void SortList()
	{
		List<buildingItem> list = new List<buildingItem>();
		for (int i = 0; i < this.allBuilding.Count; i++)
		{
			if (this.allBuilding[i].gameObject.activeSelf)
			{
				list.Add(this.allBuilding[i]);
				if (this.allBuilding[i].buildlabel.gameObject.activeSelf)
				{
					this.allBuilding[i].sortNum = 1;
				}
				else if (this.allBuilding[i].lockText.gameObject.activeSelf)
				{
					this.allBuilding[i].sortNum = 2;
				}
				else if (this.allBuilding[i].isCanBuilidMore)
				{
					this.allBuilding[i].sortNum = 3;
				}
				else if (this.allBuilding[i].isMaxNum)
				{
					this.allBuilding[i].sortNum = 4;
				}
			}
		}
		list = (from a in list
		orderby a.sortNum
		select a).ToList<buildingItem>();
		this.itemGrid.GetComponentInParent<UIScrollView>().ResetPosition();
		for (int j = 0; j < list.Count; j++)
		{
			list[j].Ani(this.itemGrid.cellWidth, j);
		}
		this.jingjiNotice.SetActive(this.allBuilding.Any((buildingItem a) => a.itemType == 1 && a.notionShow.activeSelf));
		this.fangYuNotice.SetActive(this.allBuilding.Any((buildingItem a) => a.itemType == 2 && a.notionShow.activeSelf));
		this.zhichiNotice.SetActive(this.allBuilding.Any((buildingItem a) => a.itemType == 3 && a.notionShow.activeSelf));
	}

	private void SetBtn(int num)
	{
		switch (num)
		{
		case 1:
			this.economyBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造选择底板";
			this.defenseBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造未选择底板";
			this.supportBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造未选择底板";
			this.economyBtn.GetComponent<UIButton>().normalSprite = "勘油井";
			this.economyBtn.GetComponent<UIButton>().SetSprite("勘油井");
			this.economyBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(1f, 0.6862745f, 0.298039228f);
			this.economyBtn.GetComponent<BoxCollider>().enabled = false;
			this.defenseBtn.GetComponent<UIButton>().normalSprite = "资源建筑2";
			this.defenseBtn.GetComponent<UIButton>().SetSprite("资源建筑2");
			this.defenseBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(0.482352942f, 0.368627459f, 0.227450982f);
			this.defenseBtn.GetComponent<BoxCollider>().enabled = true;
			this.supportBtn.GetComponent<UIButton>().normalSprite = "战车工厂2";
			this.supportBtn.GetComponent<UIButton>().SetSprite("战车工厂2");
			this.supportBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(0.482352942f, 0.368627459f, 0.227450982f);
			this.supportBtn.GetComponent<BoxCollider>().enabled = true;
			this.economyBtn.GetComponent<UISprite>().depth = 4;
			this.defenseBtn.GetComponent<UISprite>().depth = 4;
			this.supportBtn.GetComponent<UISprite>().depth = 4;
			break;
		case 2:
			this.economyBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造未选择底板";
			this.defenseBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造选择底板";
			this.supportBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造未选择底板";
			this.economyBtn.GetComponent<UIButton>().normalSprite = "勘油井 2";
			this.economyBtn.GetComponent<UIButton>().SetSprite("勘油井 2");
			this.economyBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(0.482352942f, 0.368627459f, 0.227450982f);
			this.economyBtn.GetComponent<BoxCollider>().enabled = true;
			this.defenseBtn.GetComponent<UIButton>().normalSprite = "资源建筑";
			this.defenseBtn.GetComponent<UIButton>().SetSprite("资源建筑");
			this.defenseBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(1f, 0.6862745f, 0.298039228f);
			this.defenseBtn.GetComponent<BoxCollider>().enabled = false;
			this.supportBtn.GetComponent<UIButton>().normalSprite = "战车工厂2";
			this.supportBtn.GetComponent<UIButton>().SetSprite("战车工厂2");
			this.supportBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(0.482352942f, 0.368627459f, 0.227450982f);
			this.supportBtn.GetComponent<BoxCollider>().enabled = true;
			this.economyBtn.GetComponent<UISprite>().depth = 4;
			this.defenseBtn.GetComponent<UISprite>().depth = 4;
			this.supportBtn.GetComponent<UISprite>().depth = 4;
			break;
		case 3:
			this.economyBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造未选择底板";
			this.defenseBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造未选择底板";
			this.supportBtn.transform.GetChild(0).GetComponent<UISprite>().spriteName = "建造选择底板";
			this.economyBtn.GetComponent<UIButton>().normalSprite = "勘油井 2";
			this.economyBtn.GetComponent<UIButton>().SetSprite("勘油井 2");
			this.economyBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(0.482352942f, 0.368627459f, 0.227450982f);
			this.economyBtn.GetComponent<BoxCollider>().enabled = true;
			this.defenseBtn.GetComponent<UIButton>().normalSprite = "资源建筑2";
			this.defenseBtn.GetComponent<UIButton>().SetSprite("资源建筑2");
			this.defenseBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(0.482352942f, 0.368627459f, 0.227450982f);
			this.defenseBtn.GetComponent<BoxCollider>().enabled = true;
			this.supportBtn.GetComponent<UIButton>().normalSprite = "战车工厂";
			this.supportBtn.GetComponent<UIButton>().SetSprite("战车工厂");
			this.supportBtn.GetComponent<UIButton>().GetComponentInChildren<UILabel>().color = new Color(1f, 0.6862745f, 0.298039228f);
			this.supportBtn.GetComponent<BoxCollider>().enabled = false;
			this.economyBtn.GetComponent<UISprite>().depth = 4;
			this.defenseBtn.GetComponent<UISprite>().depth = 4;
			this.supportBtn.GetComponent<UISprite>().depth = 4;
			break;
		}
	}

	public override void OnEnable()
	{
		if (this.allBuilding != null && this.allBuilding.Count > 0)
		{
			this.selectBtnClick(BuildingStorePanelManage.Buildtype);
		}
		base.OnEnable();
	}

	public void OnBuildItemShow()
	{
		SenceManager.inst.RemoveTempBuilding();
		if (FuncUIManager.inst.T_CommandPanelManage.gameObject.activeSelf)
		{
			FuncUIManager.inst.T_CommandPanelManage.HidePanel();
		}
		for (int i = 0; i < this.allBuilding.Count; i++)
		{
			buildingItem item = this.allBuilding[i];
			int num = SenceInfo.curMap.towerList_Data.Values.Count((BuildingNPC a) => a.buildingIdx == int.Parse(item.gameObject.name));
			if ((HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(item.buildingId) && HeroInfo.GetInstance().PlayerBuildingLimit[item.buildingId] > 0) || num > 0)
			{
				if (!HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(item.buildingId))
				{
					HeroInfo.GetInstance().PlayerBuildingLimit.Add(item.buildingId, 0);
				}
				item.buildedLabel.text = string.Concat(new object[]
				{
					num,
					"/",
					HeroInfo.GetInstance().PlayerBuildingLimit[item.buildingId],
					"[-]"
				});
				item.buildedLabel.color = new Color(0f, 1f, 0.9882353f);
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)UnitConst.GetInstance().buildingConst[item.buildingId].lvInfos[0].BuildTime);
				item.timerLabel.color = new Color(1f, 1f, 1f);
				item.timerLabel.text = string.Concat(new string[]
				{
					(timeSpan.Days <= 0) ? string.Empty : (timeSpan.Days + LanguageManage.GetTextByKey("天", "build")),
					(timeSpan.Hours <= 0) ? string.Empty : (timeSpan.Hours + LanguageManage.GetTextByKey("时", "build")),
					(timeSpan.Minutes <= 0) ? string.Empty : (timeSpan.Minutes + LanguageManage.GetTextByKey("分", "build")),
					(timeSpan.Seconds <= 0) ? string.Empty : (timeSpan.Seconds + LanguageManage.GetTextByKey("秒", "build")),
					"[-]"
				});
				if (item.battleid != 0)
				{
					if (UnitConst.GetInstance().BattleFieldConst[item.battleid].fightRecord.isFight)
					{
						this.buildingitem.Add(item);
						if (num < HeroInfo.GetInstance().PlayerBuildingLimit[item.buildingId])
						{
							item.notionShow.SetActive(true);
							item.lockText.gameObject.SetActive(false);
							item.bacImg.mainTexture = this.bulingBacNoraml.mainTexture;
							item.lockinfo.gameObject.SetActive(false);
							item.buildlabel.SetActive(true);
							item.lockLabel.SetActive(false);
							item.resGrid.gameObject.SetActive(true);
							item.buildedLabel.gameObject.SetActive(true);
							item.buildedLabel.color = new Color(0.721568644f, 0.917647064f, 0.9882353f);
							item.lockinfo.text = string.Empty;
							item.SetEffectActive(true);
						}
						else
						{
							item.notionShow.SetActive(false);
							item.bacImg.mainTexture = this.bulingBacGray.mainTexture;
							item.buildlabel.SetActive(false);
							item.lockinfo.gameObject.SetActive(true);
							item.resGrid.gameObject.SetActive(false);
							item.buildedLabel.gameObject.SetActive(false);
							item.buildedLabel.color = new Color(0.8235294f, 0.8235294f, 0.8235294f);
							if (!item.buildedLabel.gameObject.activeSelf)
							{
								item.lockinfo.text = ((UnitConst.GetInstance().GetMaxBuildingNum(item.buildingId) > num) ? (LanguageManage.GetTextByKey("司令部升到", "build") + UnitConst.GetInstance().GetNextBuildingNum(item.buildingId, num) + LanguageManage.GetTextByKey("级获得更多数量", "build")) : LanguageManage.GetTextByKey("已建造", "build"));
							}
							item.isCanBuilidMore = (UnitConst.GetInstance().GetMaxBuildingNum(item.buildingId) > num);
							item.isMaxNum = (UnitConst.GetInstance().GetMaxBuildingNum(item.buildingId) <= num);
							if (item.isCanBuilidMore)
							{
								item.lockinfo.color = new Color(0.8235294f, 0.8235294f, 0.8235294f);
								item.lockinfo.gameObject.transform.localPosition = new Vector3(-2f, -137f, 0f);
							}
							else
							{
								item.lockinfo.color = Color.green;
							}
							item.SetEffectActive(false);
						}
					}
					else
					{
						item.notionShow.SetActive(false);
						item.bacImg.mainTexture = this.bulingBacGray.mainTexture;
						item.buildlabel.SetActive(false);
						item.resGrid.gameObject.SetActive(false);
						item.lockLabel.SetActive(true);
						item.lockinfo.gameObject.SetActive(true);
						item.lockText.gameObject.SetActive(false);
						item.buildedLabel.gameObject.SetActive(false);
						item.buildedLabel.color = new Color(0.8235294f, 0.8235294f, 0.8235294f);
						if (item.battleid != 0)
						{
							item.lockinfo.text = LanguageManage.GetTextByKey("通关", "build") + UnitConst.GetInstance().BattleFieldConst[item.battleid].name + LanguageManage.GetTextByKey("解锁", "build");
							item.lockinfo.color = new Color(0.5372549f, 0.533333361f, 0.533333361f);
						}
						item.SetEffectActive(false);
					}
				}
				else if (num < HeroInfo.GetInstance().PlayerBuildingLimit[item.buildingId])
				{
					item.notionShow.SetActive(true);
					item.lockText.gameObject.SetActive(false);
					item.bacImg.mainTexture = this.bulingBacNoraml.mainTexture;
					item.buildlabel.SetActive(true);
					item.lockLabel.SetActive(false);
					item.resGrid.gameObject.SetActive(true);
					item.lockinfo.gameObject.SetActive(false);
					item.buildedLabel.gameObject.SetActive(true);
					item.buildedLabel.color = new Color(0.721568644f, 0.917647064f, 0.9882353f);
					item.lockinfo.text = string.Empty;
					item.SetEffectActive(true);
				}
				else
				{
					item.notionShow.SetActive(false);
					item.bacImg.mainTexture = this.bulingBacGray.mainTexture;
					item.buildlabel.SetActive(false);
					item.lockLabel.SetActive(false);
					item.lockinfo.gameObject.SetActive(true);
					item.resGrid.gameObject.SetActive(false);
					item.buildedLabel.gameObject.SetActive(false);
					item.buildedLabel.color = new Color(0.8235294f, 0.8235294f, 0.8235294f);
					item.lockinfo.color = Color.green;
					item.lockinfo.text = ((UnitConst.GetInstance().GetMaxBuildingNum(item.buildingId) > num) ? (LanguageManage.GetTextByKey("司令部升到", "build") + UnitConst.GetInstance().GetNextBuildingNum(item.buildingId, num) + LanguageManage.GetTextByKey("级获得更多数量", "build")) : LanguageManage.GetTextByKey("已建造", "build"));
					item.isCanBuilidMore = (UnitConst.GetInstance().GetMaxBuildingNum(item.buildingId) > num);
					item.isMaxNum = (UnitConst.GetInstance().GetMaxBuildingNum(item.buildingId) <= num);
					if (item.isCanBuilidMore)
					{
						item.lockinfo.color = new Color(0.8235294f, 0.8235294f, 0.8235294f);
						item.lockinfo.gameObject.transform.localPosition = new Vector3(-2f, -137f, 0f);
					}
					else
					{
						item.lockinfo.color = Color.green;
					}
					item.SetEffectActive(false);
				}
			}
			else
			{
				item.notionShow.SetActive(false);
				item.lockText.gameObject.SetActive(true);
				item.bacImg.mainTexture = this.bulingBacGray.mainTexture;
				item.lockinfo.gameObject.SetActive(false);
				item.buildlabel.SetActive(false);
				item.lockLabel.SetActive(true);
				item.resGrid.gameObject.SetActive(false);
				item.buildedLabel.color = new Color(0.8235294f, 0.8235294f, 0.8235294f);
				item.buildedLabel.gameObject.SetActive(false);
				HomeUpdateOpenSetData homeUpdateOpenSetData = UnitConst.GetInstance().HomeUpdateOpenSetDataConst.Values.FirstOrDefault((HomeUpdateOpenSetData a) => a.buildingIds.ContainsKey(item.buildingId));
				if (homeUpdateOpenSetData != null)
				{
					item.lockText.text = homeUpdateOpenSetData.homeLevel.ToString();
				}
				else
				{
					item.lockText.text = UnitConst.GetInstance().buildingConst[int.Parse(item.name)].name + "未有解封表数据";
				}
				item.SetEffectActive(false);
			}
		}
	}

	private void ClickItem(GameObject ga)
	{
		buildingItem component = ga.GetComponent<buildingItem>();
		if (component.sortNum == 2)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("升级司令部可解锁", "build"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (component.sortNum == 3)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("司令部升级获取更多数量", "build"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (component.sortNum == 4)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已建造", "build"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (component.buildlabel.gameObject.activeSelf)
		{
			this.Basic_Go = ga.GetComponent<buildingItem>();
			this.IsTryBuilding = true;
			this.OnItemInfoPanelShow(ga);
			return;
		}
	}

	public void TryBuilding()
	{
		this.BuildingItemInfoPanel.SetActive(false);
		if (this.BuildingItemInfoPanel.GetComponent<ItemInfo>().model)
		{
			UnityEngine.Object.Destroy(this.BuildingItemInfoPanel.GetComponent<ItemInfo>().model.ga);
		}
		this.IsTryBuilding = false;
		if (this.Basic_Go)
		{
			GameObject gameObject = this.Basic_Go.gameObject;
			buildingItem component = gameObject.GetComponent<buildingItem>();
			if (component.buildlabel.gameObject.activeSelf)
			{
				if (component.oilLabel.activeSelf)
				{
					SenceManager.inst.needOil_Build = int.Parse(component.oilLabel.GetComponentInChildren<UILabel>().text);
				}
				else
				{
					SenceManager.inst.needOil_Build = 0;
				}
				if (component.steelLabel.activeSelf)
				{
					SenceManager.inst.needSteel_Build = int.Parse(component.steelLabel.GetComponentInChildren<UILabel>().text);
				}
				else
				{
					SenceManager.inst.needSteel_Build = 0;
				}
				if (component.rareEarthLabel.activeSelf)
				{
					SenceManager.inst.needRareEarth_Build = int.Parse(component.rareEarthLabel.GetComponentInChildren<UILabel>().text);
				}
				else
				{
					SenceManager.inst.needRareEarth_Build = 0;
				}
				CameraControl.inst.ChangeCameraBuildingState(true);
				Camera_FingerManager.inst.GetDragCamera(SenceManager.inst.AddTempNPC(int.Parse(gameObject.transform.name), 0).tr);
			}
			return;
		}
	}
}
