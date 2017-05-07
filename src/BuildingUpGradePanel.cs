using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpGradePanel : FuncUIPanel
{
	public static BuildingUpGradePanel _ins;

	public GameObject nowPrefab;

	public GameObject nextPrefab;

	public UITable nowTabel;

	public UITable nextTabel;

	public UITable bestTabel;

	public UILabel nowName;

	public UILabel nextName;

	public Transform nowModelPos;

	public Transform nextModelPos;

	public Transform bestModelPos;

	public GameObject now;

	public GameObject next;

	public GameObject best;

	public GameObject tipLabel;

	public GameObject btnUpGrade;

	public UILabel bestName;

	public UILabel needLevel;

	private Body_Model nowModel;

	private Body_Model nextModel;

	private Body_Model bestModel;

	private T_Tower tower;

	public UITable needResItemTable;

	public GameObject itemPrefab;

	public GameObject resPrefab;

	private int updGradeId;

	public void OnDestroy()
	{
		BuildingUpGradePanel._ins = null;
	}

	public new void Awake()
	{
		base.Awake();
		BuildingUpGradePanel._ins = this;
	}

	public new void OnEnable()
	{
		base.OnEnable();
		this.init();
	}

	private void init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingUpGradePanel_Close, new EventManager.VoidDelegate(this.ClosePanel));
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingUpGradePanel_BtnGrade, new EventManager.VoidDelegate(this.UpGradeTower));
	}

	public void ShowBuildingUpGrade(T_Tower tar)
	{
		this.tower = tar;
		this.nowTabel.DestoryChildren(true);
		this.nextTabel.DestoryChildren(true);
		if (tar.star < UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos.Count - 1)
		{
			if (this.nowModel != null)
			{
				this.nowModel.DesInsInPool();
			}
			this.nowModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].bodyName, this.nowModelPos);
			if (this.nowModel.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren = this.nowModel.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[tar.index].particleInfo != null)
				{
					int num = (componentsInChildren.Length <= UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length) ? componentsInChildren.Length : UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length;
					for (int i = 0; i < num; i++)
					{
						componentsInChildren[i].startSize.GetType();
						componentsInChildren[i].startSize = UnitConst.GetInstance().buildingConst[tar.index].particleInfo[i];
					}
				}
			}
			if (this.nowModel && this.nowModel.RedModel)
			{
				NGUITools.SetActiveSelf(this.nowModel.RedModel.gameObject, false);
			}
			if (this.nowModel && this.nowModel.Blue_DModel)
			{
				NGUITools.SetActiveSelf(this.nowModel.Blue_DModel.gameObject, false);
			}
			if (this.nowModel && this.nowModel.Red_DModel)
			{
				NGUITools.SetActiveSelf(this.nowModel.Red_DModel.gameObject, false);
			}
			if (this.nowModel.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren2 = this.nowModel.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[tar.index].particleInfo != null)
				{
					int num2 = (componentsInChildren2.Length <= UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length) ? componentsInChildren2.Length : UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length;
					for (int j = 0; j < num2; j++)
					{
						componentsInChildren2[j].startSize.GetType();
						componentsInChildren2[j].startSize = UnitConst.GetInstance().buildingConst[tar.index].particleInfo[j];
					}
				}
			}
			Transform[] componentsInChildren3 = this.nowModel.GetComponentsInChildren<Transform>(true);
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				componentsInChildren3[k].gameObject.layer = 5;
			}
			this.nowModel.tr.localPosition = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[0];
			this.nowModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[1]);
			this.nowModel.tr.localScale = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[2];
			if (this.nextModel != null)
			{
				this.nextModel.DesInsInPool();
			}
			this.nextModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star + 1].bodyName, this.nextModelPos);
			if (this.nextModel.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren4 = this.nextModel.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[tar.index].particleInfo != null)
				{
					int num3 = (componentsInChildren4.Length <= UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length) ? componentsInChildren4.Length : UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length;
					for (int l = 0; l < num3; l++)
					{
						componentsInChildren4[l].startSize.GetType();
						componentsInChildren4[l].startSize = UnitConst.GetInstance().buildingConst[tar.index].particleInfo[l];
					}
				}
			}
			if (this.nextModel && this.nextModel.RedModel)
			{
				NGUITools.SetActiveSelf(this.nextModel.RedModel.gameObject, false);
			}
			if (this.nextModel && this.nextModel.Blue_DModel)
			{
				NGUITools.SetActiveSelf(this.nextModel.Blue_DModel.gameObject, false);
			}
			if (this.nextModel && this.nextModel.Red_DModel)
			{
				NGUITools.SetActiveSelf(this.nextModel.Red_DModel.gameObject, false);
			}
			if (this.nextModel.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren5 = this.nextModel.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[tar.index].particleInfo != null)
				{
					int num4 = (componentsInChildren5.Length <= UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length) ? componentsInChildren5.Length : UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length;
					for (int m = 0; m < num4; m++)
					{
						componentsInChildren5[m].startSize.GetType();
						componentsInChildren5[m].startSize = UnitConst.GetInstance().buildingConst[tar.index].particleInfo[m];
					}
				}
			}
			Transform[] componentsInChildren6 = this.nextModel.GetComponentsInChildren<Transform>(true);
			for (int n = 0; n < componentsInChildren6.Length; n++)
			{
				componentsInChildren6[n].gameObject.layer = 5;
			}
			this.nextModel.tr.localPosition = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[0];
			this.nextModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[1]);
			this.nextModel.tr.localScale = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[2];
			this.now.SetActive(true);
			this.next.SetActive(true);
			this.btnUpGrade.SetActive(true);
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].resCost)
			{
				GameObject gameObject = NGUITools.AddChild(this.needResItemTable.gameObject, this.resPrefab);
				switch (current.Key)
				{
				case ResType.金币:
					gameObject.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resCoin <= current.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resCoin, current.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resCoin, current.Value));
					break;
				case ResType.石油:
					gameObject.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resOil <= current.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resOil, current.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resOil, current.Value));
					break;
				case ResType.钢铁:
					gameObject.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resSteel <= current.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resSteel, current.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resSteel, current.Value));
					break;
				case ResType.稀矿:
					gameObject.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resRareEarth <= current.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resRareEarth, current.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resRareEarth, current.Value));
					break;
				}
				ItemTipsShow2 component = gameObject.GetComponent<ItemTipsShow2>();
				component.Index = (int)current.Key;
				component.Type = 2;
				AtlasManage.SetResSpriteName(gameObject.GetComponent<UISprite>(), current.Key);
			}
			foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].itemCost)
			{
				GameObject gameObject2 = NGUITools.AddChild(this.needResItemTable.gameObject, this.itemPrefab);
				if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current2.Key))
				{
					gameObject2.GetComponentInChildren<UILabel>().text = string.Format("[FF0000]{0}[-]/{1}", 0, current2.Value);
				}
				else
				{
					gameObject2.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().PlayerItemInfo[current2.Key] < current2.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().PlayerItemInfo[current2.Key], current2.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().PlayerItemInfo[current2.Key], current2.Value));
				}
				AtlasManage.SetItemUISprite(gameObject2.GetComponent<UISprite>(), current2.Key);
				ItemTipsShow2 component2 = gameObject2.GetComponent<ItemTipsShow2>();
				component2.Index = current2.Key;
				component2.Type = 1;
			}
			this.nowName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tar.index].name, "build") + tar.star + LanguageManage.GetTextByKey("阶", "build");
			this.nextName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tar.index].name, "build") + (tar.star + 1) + LanguageManage.GetTextByKey("阶", "build");
			this.best.SetActive(false);
			this.tipLabel.SetActive(false);
			if (tar.lv >= UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].needLevel)
			{
				this.btnUpGrade.SetActive(true);
				this.needLevel.gameObject.SetActive(false);
			}
			else
			{
				this.btnUpGrade.SetActive(false);
				this.needLevel.gameObject.SetActive(true);
				this.needLevel.text = string.Concat(new object[]
				{
					LanguageManage.GetTextByKey("需要", "build"),
					UnitConst.GetInstance().buildingConst[tar.index].name,
					LanguageManage.GetTextByKey("达到", "build"),
					"LV.",
					UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].needLevel
				});
			}
			foreach (int current3 in UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].buildUpGradeShow)
			{
				GameObject gameObject3 = NGUITools.AddChild(this.nowTabel.gameObject, this.nowPrefab);
				ShowUpGradeInfo component3 = gameObject3.GetComponent<ShowUpGradeInfo>();
				BuildingGradeShow buildingGradeShow = (BuildingGradeShow)current3;
				if (buildingGradeShow != BuildingGradeShow.产出速率)
				{
					if (buildingGradeShow == BuildingGradeShow.储量上限)
					{
						component3.UpGradeName.text = LanguageManage.GetTextByKey("储量提高", "build");
						component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].outlimit + "%";
					}
				}
				else
				{
					component3.UpGradeName.text = LanguageManage.GetTextByKey("生产速率提高", "build");
					component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].output + "%";
				}
			}
			this.nowTabel.Reposition();
			foreach (int current4 in UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star + 1].buildUpGradeShow)
			{
				GameObject gameObject4 = NGUITools.AddChild(this.nextTabel.gameObject, this.nextPrefab);
				ShowUpGradeInfo component4 = gameObject4.GetComponent<ShowUpGradeInfo>();
				BuildingGradeShow buildingGradeShow = (BuildingGradeShow)current4;
				if (buildingGradeShow != BuildingGradeShow.产出速率)
				{
					if (buildingGradeShow == BuildingGradeShow.储量上限)
					{
						component4.UpGradeName.text = LanguageManage.GetTextByKey("储量提高", "build");
						component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star + 1].outlimit + "%";
					}
				}
				else
				{
					component4.UpGradeName.text = LanguageManage.GetTextByKey("生产速率提高", "build");
					component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star + 1].output + "%";
				}
			}
			this.nextTabel.Reposition();
		}
		else
		{
			this.best.SetActive(true);
			this.tipLabel.SetActive(true);
			this.btnUpGrade.SetActive(false);
			this.now.SetActive(false);
			this.next.SetActive(false);
			this.bestTabel.DestoryChildren(true);
			this.bestName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tar.index].name, "build") + tar.star + LanguageManage.GetTextByKey("阶", "build");
			if (!this.bestModel)
			{
				this.bestModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].bodyName, this.bestModelPos);
			}
			if (this.bestModel && this.bestModel.RedModel)
			{
				NGUITools.SetActiveSelf(this.bestModel.RedModel.gameObject, false);
			}
			if (this.bestModel && this.bestModel.Blue_DModel)
			{
				NGUITools.SetActiveSelf(this.bestModel.Blue_DModel.gameObject, false);
			}
			if (this.bestModel && this.bestModel.Red_DModel)
			{
				NGUITools.SetActiveSelf(this.bestModel.Red_DModel.gameObject, false);
			}
			if (this.bestModel.GetComponentsInChildren<ParticleSystem>() != null)
			{
				ParticleSystem[] componentsInChildren7 = this.bestModel.GetComponentsInChildren<ParticleSystem>();
				if (UnitConst.GetInstance().buildingConst[tar.index].particleInfo != null)
				{
					int num5 = (componentsInChildren7.Length <= UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length) ? componentsInChildren7.Length : UnitConst.GetInstance().buildingConst[tar.index].particleInfo.Length;
					for (int num6 = 0; num6 < num5; num6++)
					{
						componentsInChildren7[num6].startSize.GetType();
						componentsInChildren7[num6].startSize = UnitConst.GetInstance().buildingConst[tar.index].particleInfo[num6];
					}
				}
			}
			Transform[] componentsInChildren8 = this.bestModel.GetComponentsInChildren<Transform>(true);
			for (int num7 = 0; num7 < componentsInChildren8.Length; num7++)
			{
				componentsInChildren8[num7].gameObject.layer = 5;
			}
			this.bestModel.tr.localPosition = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[0];
			this.bestModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[1]);
			this.bestModel.tr.localScale = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].makeShow[2];
			foreach (int current5 in UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].buildUpGradeShow)
			{
				GameObject gameObject5 = NGUITools.AddChild(this.bestTabel.gameObject, this.nextPrefab);
				ShowUpGradeInfo component5 = gameObject5.GetComponent<ShowUpGradeInfo>();
				BuildingGradeShow buildingGradeShow = (BuildingGradeShow)current5;
				if (buildingGradeShow != BuildingGradeShow.产出速率)
				{
					if (buildingGradeShow == BuildingGradeShow.储量上限)
					{
						component5.UpGradeName.text = LanguageManage.GetTextByKey("储量提高", "build");
						component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].outlimit + "%";
					}
				}
				else
				{
					component5.UpGradeName.text = LanguageManage.GetTextByKey("生产速率提高", "build");
					component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].buildGradeInfos[tar.star].output + "%";
				}
			}
			this.bestTabel.Reposition();
		}
	}

	private void ClosePanel(GameObject ga)
	{
		if (this.nowModel)
		{
			UnityEngine.Object.Destroy(this.nowModel.ga);
		}
		if (this.nextModel)
		{
			UnityEngine.Object.Destroy(this.nextModel.ga);
		}
		if (this.bestModel)
		{
			UnityEngine.Object.Destroy(this.bestModel.ga);
		}
		this.needLevel.gameObject.SetActive(false);
		FuncUIManager.inst.DestoryFuncUI("BuildingUpGradePanel");
	}

	private void UpGradeTower(GameObject ga)
	{
		if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(119) || HeroInfo.GetInstance().PlayerItemInfo[119] <= 0 || HeroInfo.GetInstance().PlayerItemInfo[119] < UnitConst.GetInstance().buildingConst[this.tower.index].buildGradeInfos[this.tower.star].itemCost[119])
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		this.StrengthTowerAc();
	}

	public void StrengthTowerAc()
	{
		CalcMoneyHandler.CSCalcMoney(8, 0, this.tower.posIdx, this.tower.id, this.tower.index, 0, new Action<bool, int>(this.CalcMoneyCallBack));
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
			TowerStrenghtHandler.CS_TowerUpGrade(this.tower.id, delegate
			{
				this.tower.strengthLevel = SenceInfo.curMap.towerList_Data[this.tower.id].strength;
				this.tower.star = SenceInfo.curMap.towerList_Data[this.tower.id].star;
				this.tower.ReLoadModelAndFightState();
				this.ShowBuildingUpGrade(this.tower);
			});
		}
	}

	public void DestoryModel()
	{
		if (this.nowModel)
		{
			UnityEngine.Object.Destroy(this.nowModel.ga);
		}
		if (this.nextModel)
		{
			UnityEngine.Object.Destroy(this.nextModel.ga);
		}
	}
}
