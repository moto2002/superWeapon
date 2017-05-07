using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpdatePanel : FuncUIPanel
{
	public static TowerUpdatePanel _ins;

	public UILabel itemCount;

	public GameObject nowPrefab;

	public GameObject nextPrefab;

	public UITable nowTabel;

	public UITable nextTabel;

	public UITable bestTabel;

	public UILabel coinNeed;

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

	public UISpriteAnimation btnSpriteAni;

	public UITable needResItemTable;

	public GameObject resPrefab;

	public GameObject itemPrefab;

	public void OnDestroy()
	{
		TowerUpdatePanel._ins = null;
	}

	public override void Awake()
	{
		TowerUpdatePanel._ins = this;
		this.init();
	}

	private void init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.TowerUpdatePanel_Close, new EventManager.VoidDelegate(this.ClosePanel));
		EventManager.Instance.AddEvent(EventManager.EventType.TowerUpdatePanel_UpGrade, new EventManager.VoidDelegate(this.UpGradeTower));
	}

	public void ShowUpGrade(T_Tower tar)
	{
		this.tower = tar;
		this.nowTabel.DestoryChildren(true);
		this.nextTabel.DestoryChildren(true);
		if (tar.star < UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos.Count - 1)
		{
			if (this.nowModel != null)
			{
				this.nowModel.DesInsInPool();
			}
			this.nowModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].bodyName, this.nowModelPos);
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
			Transform[] componentsInChildren = this.nowModel.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 5;
			}
			this.nowModel.tr.localPosition = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[0];
			this.nowModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[1]);
			this.nowModel.tr.localScale = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[2];
			if (this.nextModel != null)
			{
				this.nextModel.DesInsInPool();
			}
			this.nextModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].bodyName, this.nextModelPos);
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
			Transform[] componentsInChildren2 = this.nextModel.GetComponentsInChildren<Transform>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].gameObject.layer = 5;
			}
			this.nextModel.tr.localPosition = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[0];
			this.nextModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[1]);
			this.nextModel.tr.localScale = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[2];
			this.now.SetActive(true);
			this.next.SetActive(true);
			this.btnUpGrade.SetActive(true);
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].resCost)
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
			foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].itemCost)
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
			if (tar.lv >= UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].needlevel)
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
					UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].needlevel
				});
			}
			foreach (int current3 in UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].specialshow)
			{
				GameObject gameObject3 = NGUITools.AddChild(this.nowTabel.gameObject, this.nowPrefab);
				ShowUpGradeInfo component3 = gameObject3.GetComponent<ShowUpGradeInfo>();
				switch (current3)
				{
				case 1:
					component3.UpGradeName.text = LanguageManage.GetTextByKey("连击", "build");
					component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].renjuCD.ToString();
					break;
				case 2:
					component3.UpGradeName.text = LanguageManage.GetTextByKey("连珠", "build");
					component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].renju.ToString();
					break;
				case 3:
					component3.UpGradeName.text = LanguageManage.GetTextByKey("暴击率", "build");
					component3.changCount.text = Mathf.CeilToInt(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].critPer / 100f) + "%";
					break;
				case 4:
					component3.UpGradeName.text = LanguageManage.GetTextByKey("暴击抵抗", "build");
					component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].resistPer.ToString();
					break;
				case 5:
					component3.UpGradeName.text = LanguageManage.GetTextByKey("破甲", "build");
					component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].avoidDef.ToString();
					break;
				case 7:
					component3.UpGradeName.text = LanguageManage.GetTextByKey("溅射范围", "build");
					component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].hurtRadius.ToString();
					break;
				case 8:
					component3.UpGradeName.text = LanguageManage.GetTextByKey("发射间隔", "build");
					component3.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].frequency.ToString();
					break;
				}
			}
			this.nowTabel.Reposition();
			foreach (int current4 in UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].specialshow)
			{
				GameObject gameObject4 = NGUITools.AddChild(this.nextTabel.gameObject, this.nextPrefab);
				ShowUpGradeInfo component4 = gameObject4.GetComponent<ShowUpGradeInfo>();
				switch (current4)
				{
				case 1:
					component4.UpGradeName.text = LanguageManage.GetTextByKey("连击", "build");
					component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].renjuCD.ToString();
					break;
				case 2:
					component4.UpGradeName.text = LanguageManage.GetTextByKey("连珠", "build");
					component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].renju.ToString();
					break;
				case 3:
					component4.UpGradeName.text = LanguageManage.GetTextByKey("暴击率", "build");
					component4.changCount.text = Mathf.CeilToInt(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].critPer / 100f) + "%";
					break;
				case 4:
					component4.UpGradeName.text = LanguageManage.GetTextByKey("暴击抵抗", "build");
					component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].resistPer.ToString();
					break;
				case 5:
					component4.UpGradeName.text = LanguageManage.GetTextByKey("破甲", "build");
					component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].avoidDef.ToString();
					break;
				case 7:
					component4.UpGradeName.text = LanguageManage.GetTextByKey("溅射范围", "build");
					component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].hurtRadius.ToString();
					break;
				case 8:
					component4.UpGradeName.text = LanguageManage.GetTextByKey("发射间隔", "build");
					component4.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star + 1].frequency.ToString();
					break;
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
				this.bestModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].bodyName, this.bestModelPos);
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
			Transform[] componentsInChildren3 = this.bestModel.GetComponentsInChildren<Transform>(true);
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				componentsInChildren3[k].gameObject.layer = 5;
			}
			this.bestModel.tr.localPosition = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[0];
			this.bestModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[1]);
			this.bestModel.tr.localScale = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].makeShow[2];
			foreach (int current5 in UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].specialshow)
			{
				GameObject gameObject5 = NGUITools.AddChild(this.bestTabel.gameObject, this.nextPrefab);
				ShowUpGradeInfo component5 = gameObject5.GetComponent<ShowUpGradeInfo>();
				switch (current5)
				{
				case 1:
					component5.UpGradeName.text = LanguageManage.GetTextByKey("连击", "build");
					component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].renjuCD.ToString();
					break;
				case 2:
					component5.UpGradeName.text = LanguageManage.GetTextByKey("连珠", "build");
					component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].renju.ToString();
					break;
				case 3:
					component5.UpGradeName.text = LanguageManage.GetTextByKey("暴击率", "build");
					component5.changCount.text = Mathf.CeilToInt(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].critPer / 100f) + "%";
					break;
				case 4:
					component5.UpGradeName.text = LanguageManage.GetTextByKey("暴击抵抗", "build");
					component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].resistPer.ToString();
					break;
				case 5:
					component5.UpGradeName.text = LanguageManage.GetTextByKey("破甲", "build");
					component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].avoidDef.ToString();
					break;
				case 7:
					component5.UpGradeName.text = LanguageManage.GetTextByKey("溅射范围", "build");
					component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].hurtRadius.ToString();
					break;
				case 8:
					component5.UpGradeName.text = LanguageManage.GetTextByKey("发射间隔", "build");
					component5.changCount.text = UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].frequency.ToString();
					break;
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
		FuncUIManager.inst.DestoryFuncUI("TowerUpdatePanel");
	}

	private void UpGradeTower(GameObject ga)
	{
		foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().buildingConst[this.tower.index].UpdateStarInfos[this.tower.star].itemCost)
		{
			if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current.Key) || HeroInfo.GetInstance().PlayerItemInfo[current.Key] < current.Value)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
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
				this.ShowUpGrade(this.tower);
				ShowUpGradeInfo[] componentsInChildren = this.nowTabel.GetComponentsInChildren<ShowUpGradeInfo>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					ShowUpGradeInfo showUpGradeInfo = componentsInChildren[i];
					showUpGradeInfo.PlayEffect();
				}
				ShowUpGradeInfo[] componentsInChildren2 = this.nextTabel.GetComponentsInChildren<ShowUpGradeInfo>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					ShowUpGradeInfo showUpGradeInfo2 = componentsInChildren2[j];
					showUpGradeInfo2.PlayEffect();
				}
				ShowUpGradeInfo[] componentsInChildren3 = this.bestTabel.GetComponentsInChildren<ShowUpGradeInfo>();
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					ShowUpGradeInfo showUpGradeInfo3 = componentsInChildren3[k];
					showUpGradeInfo3.PlayEffect();
				}
				this.btnSpriteAni.Reset();
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
