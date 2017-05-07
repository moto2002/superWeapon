using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class T_InfoPanelManage : FuncUIPanel
{
	public static T_InfoPanelManage _instance;

	public Transform clickToUpdate;

	public GameObject info;

	public GameObject update;

	public GameObject unLocked;

	public UISprite towerUISprite;

	public UILabel itemInfo;

	private Body_Model towerModel;

	public UILabel InfoTips;

	public UILabel tittleText;

	public UILabel towerInfo;

	public UILabel lv;

	public UILabel t_Content;

	public UILabel tecInfo;

	public UITable resTable;

	public UITable itemTable;

	public UIGrid updateTextTable;

	public UIGrid unLockTable;

	public UILabel infos;

	public bool isTrans;

	public GameObject jianzhushengjiClose;

	public GameObject updateBtn;

	public GameObject lvMax;

	public GameObject UpdateKuang;

	public GameObject xuzBtn;

	public GameObject playerLeveInfo;

	public UILabel LeveLabel;

	public GameObject itemText;

	public UILabel BuildingLeve;

	public static int BuildingResType;

	public UILabel NoelectricityLabel;

	public UILabel showTextForInfo;

	public GameObject unlockItemBuild;

	public UIScrollView openBuildingScroll;

	public GameObject unlockArmy;

	private T_Tower tar;

	private bool isCanUpdate = true;

	private bool IsInit;

	private resTextInfo textItem;

	private GameObject resItem;

	public UIScrollView UISV;

	public void OnDestroy()
	{
		T_InfoPanelManage._instance = null;
	}

	public override void Awake()
	{
		T_InfoPanelManage._instance = this;
		EventManager.Instance.AddEvent(EventManager.EventType.T_InfoPanel_Upgrade, new EventManager.VoidDelegate(this.UpadteThisTower));
		EventManager.Instance.AddEvent(EventManager.EventType.T_InfoPanel_BackClick, new EventManager.VoidDelegate(this.ClosePanelT_Info));
		this.IsInit = true;
	}

	public void OnCloseSetNoelectricityLabel()
	{
		this.NoelectricityLabel.gameObject.SetActive(false);
	}

	public void OnSetNoelectricityLabel()
	{
		if (HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(62))
		{
			if (this.NoelectricityLabel == null)
			{
				this.NoelectricityLabel = base.transform.FindChild("Panel/Noelectricity").GetComponent<UILabel>();
			}
			SenceManager.ElectricityEnum mapElectricity = SenceManager.inst.MapElectricity;
			if (mapElectricity != SenceManager.ElectricityEnum.电力充沛)
			{
				this.NoelectricityLabel.gameObject.SetActive(true);
				if (this.unLocked.gameObject.activeSelf)
				{
					this.NoelectricityLabel.gameObject.transform.localPosition = new Vector3(134f, 38f, 0f);
				}
				else
				{
					this.NoelectricityLabel.gameObject.transform.localPosition = new Vector3(134f, -68f, 0f);
				}
				if (this.tar.index == 18)
				{
					this.NoelectricityLabel.gameObject.transform.localPosition = new Vector3(134f, -100f, 0f);
				}
			}
			else
			{
				this.OnCloseSetNoelectricityLabel();
			}
		}
	}

	public void Start()
	{
		Body_Model effectByName = PoolManage.Ins.GetEffectByName("jianzao_glow", base.transform);
		if (effectByName)
		{
			effectByName.tr.localPosition = new Vector3(-297.5f, 43.2f, 0f);
		}
	}

	private void ClosePanelT_Info(GameObject go)
	{
		if (this.infos)
		{
			this.infos.gameObject.SetActive(false);
		}
		if (this.towerModel)
		{
			this.towerModel.DesInsInPool();
		}
		if (this.tecInfo)
		{
			this.tecInfo.gameObject.SetActive(false);
		}
		FuncUIManager.inst.DestoryFuncUI("T_InfoPanel");
	}

	public void OnAddResInfo(UILabel label)
	{
		switch (SenceManager.inst.MapElectricity)
		{
		case SenceManager.ElectricityEnum.电力充沛:
			label.text = string.Empty;
			break;
		case SenceManager.ElectricityEnum.电力不足:
			label.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ElectricityCont[2].resdamaged, "build");
			break;
		case SenceManager.ElectricityEnum.严重不足:
			label.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ElectricityCont[3].resdamaged, "build");
			break;
		case SenceManager.ElectricityEnum.电力瘫痪:
			label.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ElectricityCont[4].resdamaged, "build");
			break;
		}
	}

	private void DestToryGridChild()
	{
		GameObject gameObject = this.unLockTable.gameObject;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.transform.parent == gameObject.transform)
			{
				transform.transform.parent = this.openBuildingScroll.transform;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	public void Show(T_Tower building, bool isInfo)
	{
		if (!this.IsInit)
		{
			this.Awake();
		}
		base.gameObject.SetActive(true);
		this.tar = building;
		building.SetBaseFightInfo();
		this.unLocked.SetActive(false);
		this.info.SetActive(false);
		this.update.SetActive(false);
		this.lv.text = "LV : " + building.lv.ToString();
		this.updateTextTable.ClearChild();
		if (building.secType == 8)
		{
			if (UnitConst.GetInstance().buildingConst[building.index].UpdateStarInfos.Count == 0)
			{
				this.towerModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[building.index].bodyID, this.towerUISprite.transform);
			}
			else
			{
				this.towerModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[building.index].UpdateStarInfos[building.star].bodyName, this.towerUISprite.transform);
			}
		}
		else if (building.secType == 2 || building.secType == 3)
		{
			if (UnitConst.GetInstance().buildingConst[building.index].buildGradeInfos.Count == 0)
			{
				this.towerModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[building.index].bodyID, this.towerUISprite.transform);
			}
			else
			{
				this.towerModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[building.index].buildGradeInfos[building.star].bodyName, this.towerUISprite.transform);
			}
		}
		else
		{
			this.towerModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].bodyID, this.towerUISprite.transform);
		}
		if (this.towerModel && this.towerModel.RedModel)
		{
			NGUITools.SetActiveSelf(this.towerModel.RedModel.gameObject, false);
		}
		if (this.towerModel && this.towerModel.Blue_DModel)
		{
			NGUITools.SetActiveSelf(this.towerModel.Blue_DModel.gameObject, false);
		}
		if (this.towerModel && this.towerModel.Red_DModel)
		{
			NGUITools.SetActiveSelf(this.towerModel.Red_DModel.gameObject, false);
		}
		if (this.towerModel)
		{
			Transform[] componentsInChildren = this.towerModel.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 5;
			}
			if (this.towerModel.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren2 = this.towerModel.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[building.index].particleInfo != null)
				{
					int num = (componentsInChildren2.Length <= UnitConst.GetInstance().buildingConst[building.index].particleInfo.Length) ? componentsInChildren2.Length : UnitConst.GetInstance().buildingConst[building.index].particleInfo.Length;
					for (int j = 0; j < num; j++)
					{
						componentsInChildren2[j].startSize.GetType();
						componentsInChildren2[j].startSize = UnitConst.GetInstance().buildingConst[building.index].particleInfo[j];
					}
				}
				else
				{
					for (int k = 0; k < componentsInChildren2.Length; k++)
					{
						componentsInChildren2[k].gameObject.SetActive(false);
					}
				}
			}
			this.towerModel.tr.localPosition = new Vector3(-2.2f, -40f, -466f);
			this.towerModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[building.index].modelRostion[0], UnitConst.GetInstance().buildingConst[building.index].modelRostion[1], UnitConst.GetInstance().buildingConst[building.index].modelRostion[2]);
			this.towerModel.tr.localScale = new Vector3(UnitConst.GetInstance().buildingConst[building.index].TowerSize * 100f, UnitConst.GetInstance().buildingConst[building.index].TowerSize * 100f, UnitConst.GetInstance().buildingConst[building.index].TowerSize * 100f);
			this.InfoTips.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[building.index].infTips, "build");
			if (building.index == 11)
			{
				if (this.towerModel)
				{
					this.towerModel.tr.localPosition = new Vector3(-2.2f, -40f, -466f);
				}
				this.tecInfo.gameObject.SetActive(true);
				this.tecInfo.text = LanguageManage.GetTextByKey("增加科技等级上限", "build");
				this.tecInfo.transform.localPosition = new Vector3(-45.65f, 32f, 0f);
			}
			if (building.index == 13)
			{
				this.towerModel.tr.localPosition = new Vector3(-33.7f, -66.51f, -466f);
			}
			if (building.index == 10)
			{
				this.infos.gameObject.SetActive(true);
				int num2 = UnitConst.GetInstance().mapEntityList.Values.Count((MapEntity a) => a.radarLevel == building.lv && !string.IsNullOrEmpty(a.assignNPC));
				this.towerModel.tr.localPosition = new Vector3(-2.2f, -40f, -466f);
				this.infos.text = string.Empty;
			}
			if (building.index == 23)
			{
				this.towerModel.tr.localPosition = new Vector3(-2f, -40f, -461f);
			}
			if (building.index == 16)
			{
				this.towerModel.tr.localPosition = new Vector3(-2f, -40f, -466f);
			}
			if (isInfo)
			{
				this.infos.transform.localPosition = new Vector3(-44.65f, 62.6f, 0f);
				this.tecInfo.transform.localPosition = new Vector3(-45.65f, 66f, 0f);
				this.UpdateKuang.SetActive(true);
				this.LeveLabel.gameObject.SetActive(false);
				this.playerLeveInfo.SetActive(false);
				this.showTextForInfo.gameObject.SetActive(false);
				this.info.SetActive(true);
				this.t_Content.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[building.index].description, "build");
				this.itemInfo.text = string.Empty;
				this.towerInfo.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[building.index].name, "build");
				this.DisplayInfoText(building);
			}
			else
			{
				this.showTextForInfo.gameObject.SetActive(true);
				this.showTextForInfo.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[building.index].description, "build");
				this.itemInfo.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[building.index].description, "build");
				this.infos.transform.localPosition = new Vector3(-44.65f, 27f, 0f);
				if (UnitConst.GetInstance().buildingConst[building.index].lvInfos.Count > building.lv + 1)
				{
					this.update.SetActive(true);
					this.updateBtn.SetActive(true);
					this.lvMax.SetActive(false);
					if (this.tar.index == 1)
					{
						this.UpdateKuang.SetActive(false);
						this.unLocked.SetActive(true);
						this.isTrans = true;
						this.DestToryGridChild();
						this.unLockTable.ClearChild();
						int num3 = 0;
						foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().HomeUpdateOpenSetDataConst[HeroInfo.GetInstance().PlayerCommondLv].HomeUpdateOpenSetAddData().buildingIds)
						{
							num3++;
							if (num3 < 7)
							{
								this.unLockTable.transform.localScale = Vector3.one;
							}
							else if (num3 == 7)
							{
								this.unLockTable.transform.localScale = Vector3.one * 0.8f;
							}
							else
							{
								this.unLockTable.transform.localScale = Vector3.one * (0.8f - (float)(num3 - 7) * 0.1f);
							}
							GameObject gameObject = NGUITools.AddChild(this.unLockTable.gameObject, this.unlockItemBuild);
							unLockItem component = gameObject.GetComponent<unLockItem>();
							component.model = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[current.Key].bodyID, component.parentTran);
							component.model.transform.localScale = new Vector3(UnitConst.GetInstance().buildingConst[current.Key].modelclearScale[0], UnitConst.GetInstance().buildingConst[current.Key].modelclearScale[1], UnitConst.GetInstance().buildingConst[current.Key].modelclearScale[2]);
							component.model.transform.localPosition = new Vector3(UnitConst.GetInstance().buildingConst[current.Key].modelclearPos[0], UnitConst.GetInstance().buildingConst[current.Key].modelclearPos[1], UnitConst.GetInstance().buildingConst[current.Key].modelclearPos[2]);
							component.model.transform.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[current.Key].modelRostion[0], UnitConst.GetInstance().buildingConst[current.Key].modelRostion[1], UnitConst.GetInstance().buildingConst[current.Key].modelRostion[2]);
							component.nameLabel.text = string.Format("{0}X{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[current.Key].name, "build"), current.Value);
							Transform[] componentsInChildren3 = component.model.GetComponentsInChildren<Transform>();
							for (int l = 0; l < componentsInChildren3.Length; l++)
							{
								componentsInChildren3[l].gameObject.layer = 5;
							}
							if (component.model.GetComponentsInChildren<ParticleSystem>() != null)
							{
								ParticleSystem[] componentsInChildren4 = component.model.GetComponentsInChildren<ParticleSystem>();
								if (UnitConst.GetInstance().buildingConst[current.Key].particleInfo != null)
								{
									int num4 = (componentsInChildren4.Length <= UnitConst.GetInstance().buildingConst[current.Key].particleInfo.Length) ? componentsInChildren4.Length : UnitConst.GetInstance().buildingConst[current.Key].particleInfo.Length;
									for (int m = 0; m < num4; m++)
									{
										componentsInChildren4[m].startSize.GetType();
										componentsInChildren4[m].startSize = UnitConst.GetInstance().buildingConst[current.Key].particleInfo[m];
									}
								}
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
						}
						this.unLockTable.Reposition();
					}
					else if (this.tar.index == 13)
					{
						int armyId = UnitConst.GetInstance().GetArmyId(13, this.tar.lv);
						if (armyId > 0)
						{
							this.UpdateKuang.SetActive(false);
							this.unLocked.SetActive(true);
							this.isTrans = true;
							this.DestToryGridChild();
							this.unLockTable.ClearChild();
							for (int n = 0; n < 1; n++)
							{
								GameObject gameObject2 = NGUITools.AddChild(this.unLockTable.gameObject, this.unlockArmy);
								unLockItem component2 = gameObject2.GetComponent<unLockItem>();
								component2.model = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[armyId].bodyId, component2.parentTran);
								component2.model.tr.localPosition = UnitConst.GetInstance().soldierConst[armyId].modelclearPos_TInfo;
								component2.model.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().soldierConst[armyId].modelclearRotation_TInfo);
								component2.model.tr.localScale = UnitConst.GetInstance().soldierConst[armyId].modelclearScale_TInfo;
								component2.nameLabel.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[armyId].name, "Army");
								Transform[] componentsInChildren5 = component2.model.GetComponentsInChildren<Transform>();
								for (int num5 = 0; num5 < componentsInChildren5.Length; num5++)
								{
									componentsInChildren5[num5].gameObject.layer = 5;
								}
							}
							this.unLockTable.Reposition();
						}
						else
						{
							this.UpdateKuang.SetActive(true);
							this.isTrans = false;
						}
					}
					else if (this.tar.index == 91)
					{
						int armyId2 = UnitConst.GetInstance().GetArmyId(91, this.tar.lv);
						if (armyId2 > 0)
						{
							this.UpdateKuang.SetActive(false);
							this.unLocked.SetActive(true);
							this.isTrans = true;
							this.DestToryGridChild();
							this.unLockTable.ClearChild();
							for (int num6 = 0; num6 < 1; num6++)
							{
								GameObject gameObject3 = NGUITools.AddChild(this.unLockTable.gameObject, this.unlockArmy);
								unLockItem component3 = gameObject3.GetComponent<unLockItem>();
								component3.model = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[armyId2].bodyId, component3.parentTran);
								component3.model.tr.localPosition = UnitConst.GetInstance().soldierConst[armyId2].modelclearPos_TInfo;
								component3.model.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().soldierConst[armyId2].modelclearRotation_TInfo);
								component3.model.tr.localScale = UnitConst.GetInstance().soldierConst[armyId2].modelclearScale_TInfo;
								component3.nameLabel.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[armyId2].name, "Army");
								Transform[] componentsInChildren6 = component3.model.GetComponentsInChildren<Transform>();
								for (int num7 = 0; num7 < componentsInChildren6.Length; num7++)
								{
									componentsInChildren6[num7].gameObject.layer = 5;
								}
							}
							this.unLockTable.Reposition();
						}
						else
						{
							this.UpdateKuang.SetActive(true);
							this.isTrans = false;
						}
					}
					else
					{
						this.UpdateKuang.SetActive(true);
						this.isTrans = false;
					}
					this.update.SetActive(true);
					this.OnLabelInfo();
					this.towerInfo.text = string.Concat(new object[]
					{
						LanguageManage.GetTextByKey("将", "build"),
						LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[building.index].name, "build"),
						LanguageManage.GetTextByKey("升到", "build"),
						UnitConst.GetInstance().buildingConst[building.index].lvInfos[this.tar.lv].lv + 1,
						LanguageManage.GetTextByKey("级", "build")
					});
					this.resTable.DestoryChildren(true);
					if (this.resTable.gameObject.activeSelf)
					{
						foreach (KeyValuePair<ResType, int> current2 in UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv].resCost)
						{
							this.resItem = this.resTable.CreateChildren(current2.Key.ToString(), true);
							switch (current2.Key)
							{
							case ResType.金币:
								this.LeveLabel.gameObject.transform.localPosition = new Vector3(-222.54f, -220.3f, 0f);
								this.playerLeveInfo.gameObject.transform.localPosition = new Vector3(-173f, -223.6f, 0f);
								this.resItem.GetComponentInChildren<UILabel>().text = current2.Value.ToString();
								if (current2.Value < HeroInfo.GetInstance().playerRes.resCoin)
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(0.196078435f, 0.972549f, 0.117647059f);
								}
								else
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(1f, 0.1882353f, 0.101960786f);
								}
								break;
							case ResType.石油:
								this.LeveLabel.gameObject.transform.localPosition = new Vector3(-257f, -240f, 0f);
								this.playerLeveInfo.gameObject.transform.localPosition = new Vector3(-208.2f, -247.6f, 0f);
								this.resItem.GetComponentInChildren<UILabel>().text = current2.Value.ToString();
								if (current2.Value < HeroInfo.GetInstance().playerRes.resOil)
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(0.196078435f, 0.972549f, 0.117647059f);
								}
								else
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(1f, 0.1882353f, 0.101960786f);
								}
								break;
							case ResType.钢铁:
								this.LeveLabel.gameObject.transform.localPosition = new Vector3(-254.6f, -247.1f, 0f);
								this.playerLeveInfo.gameObject.transform.localPosition = new Vector3(-204.2f, -247.1f, 0f);
								this.resItem.GetComponentInChildren<UILabel>().text = current2.Value.ToString();
								if (current2.Value < HeroInfo.GetInstance().playerRes.resSteel)
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(0.196078435f, 0.972549f, 0.117647059f);
								}
								else
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(1f, 0.1882353f, 0.101960786f);
								}
								break;
							case ResType.稀矿:
								this.LeveLabel.gameObject.transform.localPosition = new Vector3(18.99f, -242.73f, 0f);
								this.playerLeveInfo.gameObject.transform.localPosition = new Vector3(69.4f, -242.73f, 0f);
								this.resItem.GetComponentInChildren<UILabel>().text = current2.Value.ToString();
								if (current2.Value < HeroInfo.GetInstance().playerRes.resRareEarth)
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(0.196078435f, 0.972549f, 0.117647059f);
								}
								else
								{
									this.resItem.GetComponentInChildren<UILabel>().color = new Color(1f, 0.1882353f, 0.101960786f);
								}
								break;
							}
							AtlasManage.SetResSpriteName(this.resItem.GetComponent<UISprite>(), current2.Key);
						}
					}
					this.resTable.Reposition();
					this.isCanUpdate = true;
					this.itemTable.DestoryChildren(true);
					foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv].itemCost)
					{
						if (current3.Value > 0)
						{
							this.resItem = this.itemTable.CreateChildren(UnitConst.GetInstance().ItemConst[current3.Key].Name, true);
							this.resItem.GetComponentInChildren<UILabel>().text = current3.Value.ToString();
							this.resItem.GetComponent<UISprite>().spriteName = UnitConst.GetInstance().ItemConst[current3.Key].IconId;
							if (!UnitConst.GetInstance().ItemConst[current3.Key].IsCanBuy)
							{
								this.isCanUpdate = false;
							}
						}
					}
					this.itemTable.Reposition();
					this.updateTextTable.ClearChild();
					if (this.isTrans)
					{
						this.updateTextTable.Reposition();
					}
					else
					{
						this.updateTextTable.Reposition();
					}
					using (List<int>.Enumerator enumerator4 = UnitConst.GetInstance().buildingConst[building.index].uiShows.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							switch (enumerator4.Current)
							{
							case 1:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "金币产量").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.金币产量;
								this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip_NextLV - building.ResSpeed_ByStep_Ele_Tech_Vip), building.GetTextTips(T_InfoTextType.金币产量));
								break;
							case 2:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "金币储量").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.金币储量;
								this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.金币) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.金币储量));
								break;
							case 3:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "石油产量").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.石油产量;
								this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip_NextLV - building.ResSpeed_ByStep_Ele_Tech_Vip), building.GetTextTips(T_InfoTextType.石油产量));
								break;
							case 4:
								if (UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv + 1].outputLimit.ContainsKey(ResType.石油))
								{
									this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "石油储量").GetComponent<resTextInfo>();
									this.textItem.curT_InfoTextType = T_InfoTextType.石油储量;
									this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.石油) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.石油储量));
								}
								break;
							case 5:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "钢铁产量").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.钢铁产量;
								this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip_NextLV - building.ResSpeed_ByStep_Ele_Tech_Vip), building.GetTextTips(T_InfoTextType.钢铁产量));
								break;
							case 6:
								if (UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv + 1].outputLimit.ContainsKey(ResType.钢铁))
								{
									this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "钢铁储量").GetComponent<resTextInfo>();
									this.textItem.curT_InfoTextType = T_InfoTextType.钢铁储量;
									this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.钢铁) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.钢铁储量));
								}
								break;
							case 7:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "稀土产量").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.稀土产量;
								this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip_NextLV - building.ResSpeed_ByStep_Ele_Tech_Vip), building.GetTextTips(T_InfoTextType.稀土产量));
								break;
							case 8:
								if (UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv + 1].outputLimit.ContainsKey(ResType.稀矿))
								{
									this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "稀矿储量").GetComponent<resTextInfo>();
									this.textItem.curT_InfoTextType = T_InfoTextType.稀矿储量;
									this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.稀矿) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.稀矿储量));
								}
								break;
							case 9:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "生命力值").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.生命值;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.life.ToString(), building.NextLVBaseFightInfo.life - building.CharacterBaseFightInfo.life, building.GetTextTips(T_InfoTextType.生命值));
								break;
							case 11:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "攻击力值").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.攻击值;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.breakArmor.ToString(), building.NextLVBaseFightInfo.breakArmor - building.CharacterBaseFightInfo.breakArmor, building.GetTextTips(T_InfoTextType.攻击值));
								break;
							case 12:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "防御力值").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.防御值;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.defBreak.ToString(), building.NextLVBaseFightInfo.defBreak - building.CharacterBaseFightInfo.defBreak, building.GetTextTips(T_InfoTextType.防御值));
								break;
							case 15:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "伤害力值").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.伤害值;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.breakArmor.ToString(), building.NextLVBaseFightInfo.breakArmor - building.CharacterBaseFightInfo.breakArmor, building.GetTextTips(T_InfoTextType.伤害值));
								break;
							case 17:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "最大容量").GetComponent<resTextInfo>();
								T_InfoPanelManage.BuildingResType = building.index;
								this.textItem.curT_InfoTextType = T_InfoTextType.最大容量;
								switch (UnitConst.GetInstance().GetBuildingResType(building.index))
								{
								case ResType.金币:
									this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.金币) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.金币储量));
									break;
								case ResType.石油:
									this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.石油) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.石油储量));
									break;
								case ResType.钢铁:
									this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.钢铁) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.钢铁储量));
									break;
								case ResType.稀矿:
									this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), Mathf.CeilToInt(building.ResMaxLimit_ByTech_NextLV(ResType.稀矿) - building.ResMaxLimit_ProdByTech), building.GetTextTips(T_InfoTextType.稀矿储量));
									break;
								}
								break;
							case 18:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "产电量").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.产电量;
								this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].outputLimit[ResType.电力] + "/" + LanguageManage.GetTextByKey("小时", "others"), UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv + 1].outputLimit[ResType.电力] - UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].outputLimit[ResType.电力], string.Empty);
								break;
							case 19:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "电力消耗").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.电力消耗;
								this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].electricUse + string.Empty, UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv + 1].electricUse - UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].electricUse, string.Empty);
								break;
							case 20:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "破甲").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.破甲;
								this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].fightInfo.avoiddef + string.Empty, UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv + 1].fightInfo.avoiddef - UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].fightInfo.avoiddef, string.Empty);
								break;
							case 22:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "暴击率").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.暴击率;
								this.textItem.SetT_InfoUpdateResText((float)building.CharacterBaseFightInfo.crit / 100f + "%", 0, building.GetTextTips(T_InfoTextType.暴击率));
								break;
							case 23:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "暴击抵抗").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.暴击抵抗;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.resist.ToString(), 0, building.GetTextTips(T_InfoTextType.暴击抵抗));
								break;
							case 24:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "伤害范围").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.伤害范围;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.hurtRadius.ToString(), 0, building.GetTextTips(T_InfoTextType.伤害范围));
								break;
							case 25:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "额外伤害").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.额外伤害;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.trueDamage.ToString(), 0, building.GetTextTips(T_InfoTextType.额外伤害));
								break;
							case 26:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "射速").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.射速;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.shootSpeed.ToString(), 0, building.GetTextTips(T_InfoTextType.射速));
								break;
							case 27:
								this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "射程").GetComponent<resTextInfo>();
								this.textItem.curT_InfoTextType = T_InfoTextType.射程;
								this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.ShootMaxRadius.ToString(), 0, building.GetTextTips(T_InfoTextType.射程));
								break;
							}
						}
					}
					if (UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].playerExp > 0)
					{
						this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "经验").GetComponent<resTextInfo>();
						this.textItem.curT_InfoTextType = T_InfoTextType.经验值;
						this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].playerExp + string.Empty, 0, building.GetTextTips(T_InfoTextType.经验值));
					}
					foreach (int current4 in UnitConst.GetInstance().buildingConst[building.index].uiShows)
					{
						if (current4 == 21 && building.lv < UnitConst.GetInstance().buildingConst[building.index].lvInfos.Count - 1)
						{
							this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "升级消耗").GetComponent<resTextInfo>();
							this.textItem.curT_InfoTextType = T_InfoTextType.升级消耗时间;
							this.textItem.SetT_InfoUpdateResText(TimeTools.ConvertFloatToTimeBySecond((float)UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].BuildTime), 0, building.GetTextTips(T_InfoTextType.升级消耗时间));
						}
					}
					this.updateTextTable.StartCoroutine(this.updateTextTable.RepositionAfterFrame());
				}
				else
				{
					this.isCanUpdate = false;
					this.updateBtn.SetActive(false);
					this.lvMax.SetActive(true);
					this.towerInfo.text = string.Format("{0}" + LanguageManage.GetTextByKey("已满级", "build"), LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[building.index].name, "build"));
					this.UpdateKuang.SetActive(true);
					this.playerLeveInfo.SetActive(false);
					this.t_Content.text = UnitConst.GetInstance().buildingConst[building.index].description;
					this.DisplayInfoText(building);
				}
			}
			this.OnSetNoelectricityLabel();
			if (this.unLocked.gameObject.activeSelf)
			{
				this.UISV.transform.localPosition = new Vector3(-3f, -58f, 0f);
				this.UISV.GetComponent<UIPanel>().clipOffset = new Vector2(165f, 58f);
				this.UISV.GetComponent<UIPanel>().baseClipRegion = new Vector4(-1f, 156f, 610f, 204f);
			}
			else
			{
				this.UISV.transform.localPosition = new Vector3(-3f, -20f, 0f);
				this.UISV.GetComponent<UIPanel>().clipOffset = new Vector2(165f, 20f);
				this.UISV.GetComponent<UIPanel>().baseClipRegion = new Vector4(-1f, 62f, 610f, 392f);
			}
			return;
		}
	}

	private void DisplayInfoText(T_Tower building)
	{
		this.updateTextTable.ClearChild();
		using (List<int>.Enumerator enumerator = UnitConst.GetInstance().buildingConst[building.index].uiShows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				switch (enumerator.Current)
				{
				case 1:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "金币产量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.金币产量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), 0, building.GetTextTips(T_InfoTextType.金币产量));
					break;
				case 2:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "金币储量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.金币储量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.金币储量));
					break;
				case 3:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "石油产量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.石油产量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), 0, building.GetTextTips(T_InfoTextType.石油产量));
					break;
				case 4:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "石油储量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.石油储量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.石油储量));
					break;
				case 5:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "钢铁产量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.钢铁产量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), 0, building.GetTextTips(T_InfoTextType.钢铁产量));
					break;
				case 6:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "钢铁储量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.钢铁储量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.钢铁储量));
					break;
				case 7:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "稀土产量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.稀土产量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResSpeed_ByStep_Ele_Tech_Vip).ToString(), 0, building.GetTextTips(T_InfoTextType.稀土产量));
					break;
				case 8:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "稀矿储量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.稀矿储量;
					this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.稀矿储量));
					break;
				case 9:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "生命力值").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.生命值;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.life.ToString(), 0, building.GetTextTips(T_InfoTextType.生命值));
					break;
				case 11:
				case 15:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "攻击力值").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.攻击值;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.breakArmor.ToString(), 0, building.GetTextTips(T_InfoTextType.攻击值));
					break;
				case 12:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "防御力值").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.防御值;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.defBreak.ToString(), 0, building.GetTextTips(T_InfoTextType.防御值));
					break;
				case 13:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "受保护百分比").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.受保护百分比;
					this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].outputNum + string.Empty, 0, string.Empty);
					break;
				case 17:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "最大容量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.最大容量;
					T_InfoPanelManage.BuildingResType = building.index;
					switch (UnitConst.GetInstance().GetBuildingResType(building.index))
					{
					case ResType.金币:
						this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.金币储量));
						break;
					case ResType.石油:
						this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.石油储量));
						break;
					case ResType.钢铁:
						this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.钢铁储量));
						break;
					case ResType.稀矿:
						this.textItem.SetT_InfoUpdateResText(Mathf.CeilToInt(building.ResMaxLimit_ProdByTech).ToString(), 0, building.GetTextTips(T_InfoTextType.稀矿储量));
						break;
					}
					break;
				case 18:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "产电量").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.产电量;
					this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].outputLimit[ResType.电力] + "/" + LanguageManage.GetTextByKey("小时", "others"), 0, string.Empty);
					break;
				case 19:
					if (UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].electricUse > 0)
					{
						this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "电力消耗").GetComponent<resTextInfo>();
						this.textItem.curT_InfoTextType = T_InfoTextType.电力消耗;
						this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].electricUse + string.Empty, 0, building.GetTextTips(T_InfoTextType.电力消耗));
					}
					break;
				case 20:
					if (UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].fightInfo.avoiddef > 0)
					{
						this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "破甲").GetComponent<resTextInfo>();
						this.textItem.curT_InfoTextType = T_InfoTextType.破甲;
						this.textItem.SetT_InfoUpdateResText(UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].fightInfo.avoiddef + string.Empty, 0, string.Empty);
					}
					break;
				case 21:
					if (building.lv < UnitConst.GetInstance().buildingConst[building.index].lvInfos.Count - 1)
					{
						this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "升级消耗").GetComponent<resTextInfo>();
						this.textItem.curT_InfoTextType = T_InfoTextType.升级消耗时间;
						this.textItem.SetT_InfoUpdateResText(TimeTools.ConvertFloatToTimeBySecond((float)UnitConst.GetInstance().buildingConst[building.index].lvInfos[building.lv].BuildTime), 0, building.GetTextTips(T_InfoTextType.升级消耗时间));
					}
					break;
				case 22:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "暴击率").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.暴击率;
					this.textItem.SetT_InfoUpdateResText((float)building.CharacterBaseFightInfo.crit / 100f + "%", 0, building.GetTextTips(T_InfoTextType.暴击率));
					break;
				case 23:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "暴击抵抗").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.暴击抵抗;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.resist.ToString(), 0, building.GetTextTips(T_InfoTextType.暴击抵抗));
					break;
				case 24:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "伤害范围").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.伤害范围;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.hurtRadius.ToString(), 0, building.GetTextTips(T_InfoTextType.伤害范围));
					break;
				case 25:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "额外伤害").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.额外伤害;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.trueDamage.ToString(), 0, building.GetTextTips(T_InfoTextType.额外伤害));
					break;
				case 26:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "射速").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.射速;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.shootSpeed.ToString(), 0, building.GetTextTips(T_InfoTextType.射速));
					break;
				case 27:
					this.textItem = GameTools.CreateChildrenInGrid(this.updateTextTable, this.itemText, "射程").GetComponent<resTextInfo>();
					this.textItem.curT_InfoTextType = T_InfoTextType.射程;
					this.textItem.SetT_InfoUpdateResText(building.CharacterBaseFightInfo.ShootMaxRadius.ToString(), 0, building.GetTextTips(T_InfoTextType.射程));
					break;
				}
			}
		}
		this.updateTextTable.StartCoroutine(this.updateTextTable.RepositionAfterFrame());
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}

	public override void OnEnable()
	{
		base.OnEnable();
	}

	public void EventBtn(T_InfoBtnType btnType)
	{
		if (btnType != T_InfoBtnType.GoBack)
		{
			if (btnType != T_InfoBtnType.Update)
			{
			}
		}
		else
		{
			base.gameObject.SetActive(false);
			FuncUIManager.inst.T_CommandPanelManage.ShowBtn(this.tar);
		}
	}

	public void UpadteThisTower(GameObject ga)
	{
		this.infos.gameObject.SetActive(false);
		this.tecInfo.gameObject.SetActive(false);
		if (this.isCanUpdate)
		{
			this.UpdateTower();
		}
	}

	public void OnLabelInfo()
	{
		if (UnitConst.GetInstance().buildingConst[this.tar.index].resType == 1 && UnitConst.GetInstance().buildingConst[this.tar.index].secType == 1)
		{
			if (UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv + 1].needCommandLevel > HeroInfo.GetInstance().playerlevel)
			{
				this.LeveLabel.gameObject.SetActive(false);
				this.playerLeveInfo.SetActive(true);
				this.playerLeveInfo.GetComponent<UILabel>().text = UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv + 1].needCommandLevel.ToString();
				this.playerLeveInfo.transform.GetChild(0).gameObject.SetActive(true);
				this.updateBtn.GetComponent<BoxCollider>().enabled = false;
				this.updateBtn.GetComponent<UISprite>().spriteName = "hui";
				this.updateBtn.GetComponent<UIButton>().normalSprite = "hui";
				this.updateBtn.GetComponent<UIButton>().hoverSprite = "hui";
				this.updateBtn.GetComponent<UIButton>().pressedSprite = "hui";
				this.updateBtn.GetComponent<UIButton>().disabledSprite = "hui";
				this.xuzBtn.SetActive(false);
			}
			else
			{
				this.LeveLabel.gameObject.SetActive(false);
				this.playerLeveInfo.SetActive(false);
				this.updateBtn.GetComponent<BoxCollider>().enabled = true;
				this.updateBtn.GetComponent<UISprite>().spriteName = "blue";
				this.updateBtn.GetComponent<UIButton>().normalSprite = "blue";
				this.updateBtn.GetComponent<UIButton>().hoverSprite = "blued";
				this.updateBtn.GetComponent<UIButton>().pressedSprite = "blued";
				this.updateBtn.GetComponent<UIButton>().disabledSprite = "blue";
				this.xuzBtn.SetActive(true);
			}
		}
		else if (UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv + 1].needCommandLevel > SenceManager.inst.MainBuilding.lv)
		{
			this.LeveLabel.gameObject.SetActive(true);
			this.playerLeveInfo.SetActive(false);
			this.resTable.gameObject.SetActive(true);
			this.LeveLabel.text = UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv + 1].needCommandLevel.ToString();
			this.LeveLabel.gameObject.transform.localPosition = new Vector3(-68.52f, -192.2f, 0f);
			this.updateBtn.GetComponent<BoxCollider>().enabled = false;
			this.updateBtn.GetComponent<UISprite>().spriteName = "hui";
			this.updateBtn.GetComponent<UIButton>().normalSprite = "hui";
			this.updateBtn.GetComponent<UIButton>().hoverSprite = "hui";
			this.updateBtn.GetComponent<UIButton>().pressedSprite = "hui";
			this.updateBtn.GetComponent<UIButton>().disabledSprite = "hui";
			this.xuzBtn.SetActive(false);
		}
		else
		{
			this.LeveLabel.gameObject.SetActive(false);
			this.resTable.gameObject.SetActive(true);
			this.playerLeveInfo.SetActive(false);
			this.updateBtn.GetComponent<BoxCollider>().enabled = true;
			this.updateBtn.GetComponent<UISprite>().spriteName = "blue";
			this.updateBtn.GetComponent<UIButton>().normalSprite = "blue";
			this.updateBtn.GetComponent<UIButton>().hoverSprite = "blued";
			this.updateBtn.GetComponent<UIButton>().pressedSprite = "blued";
			this.updateBtn.GetComponent<UIButton>().disabledSprite = "blue";
			this.xuzBtn.SetActive(true);
		}
	}

	public void UpdateTower()
	{
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
		if (UnitConst.GetInstance().buildingConst[this.tar.index].resType == 1 && UnitConst.GetInstance().buildingConst[this.tar.index].secType == 1)
		{
			if (UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv + 1].needCommandLevel > HeroInfo.GetInstance().playerlevel)
			{
				MessageBox.GetMessagePanel().ShowBtn("升级提示", LanguageManage.GetTextByKey("升级提示", "build"), LanguageManage.GetTextByKey("玩家需要", "build") + UnitConst.GetInstance().buildingConst[FuncUIManager.inst.T_CommandPanelManage.tar.GetComponent<T_Tower>().index].lvInfos[FuncUIManager.inst.T_CommandPanelManage.tar.GetComponent<T_Tower>().lv + 1].needCommandLevel + LanguageManage.GetTextByKey("级才能开启司令部升级", "build"), null);
				return;
			}
		}
		else if (UnitConst.GetInstance().buildingConst[this.tar.index].lvInfos[this.tar.lv + 1].needCommandLevel > SenceManager.inst.MainBuilding.lv)
		{
			MessageBox.GetMessagePanel().ShowBtn("升级提示", LanguageManage.GetTextByKey("升级提示", "build"), LanguageManage.GetTextByKey("需要", "build") + UnitConst.GetInstance().buildingConst[FuncUIManager.inst.T_CommandPanelManage.tar.GetComponent<T_Tower>().index].lvInfos[FuncUIManager.inst.T_CommandPanelManage.tar.GetComponent<T_Tower>().lv + 1].needCommandLevel + "级司令部", null);
			return;
		}
		CalcMoneyHandler.CSCalcMoney(2, 0, this.tar.posIdx, this.tar.id, this.tar.index, 0, new Action<bool, int>(this.CalcMoneyCallBack));
	}

	private void CalcMoneyCallBack(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				HUDTextTool.inst.ShowBuyMoney();
				return;
			}
			this.ClosePanelT_Info(null);
			BuildingHandler.CG_BuildingUpdateStart(this.tar.id, money);
		}
	}
}
