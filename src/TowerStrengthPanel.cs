using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerStrengthPanel : FuncUIPanel
{
	public static TowerStrengthPanel ins;

	public UITable resTabel;

	private Body_Model towerModel;

	public UILabel modelName;

	public GameObject btnStrength;

	public GameObject resPrefab;

	public Transform modelPositon;

	public GameObject showInfoPrb;

	public UITable showInfoTabel;

	public GameObject showBestStrength;

	public GameObject hideStrength;

	public UILabel needLevel;

	public UILabel strengthLevel;

	public UILabel towerStrongName;

	private T_Tower tower;

	public GameObject itemPrefab;

	public void OnDestroy()
	{
		TowerStrengthPanel.ins = null;
	}

	public override void Awake()
	{
		TowerStrengthPanel.ins = this;
		this.Init();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.TowerStrengthPanel_Colse, new EventManager.VoidDelegate(this.ClosePanel));
		EventManager.Instance.AddEvent(EventManager.EventType.TowerStrengthPanel_Strength, new EventManager.VoidDelegate(this.StrengthTower));
	}

	public void ShowStrengthInfo(T_Tower tar)
	{
		this.tower = tar;
		int lv = tar.lv;
		this.hideStrength.gameObject.SetActive(true);
		this.resTabel.gameObject.SetActive(true);
		this.showInfoTabel.DestoryChildren(true);
		this.resTabel.DestoryChildren(true);
		this.strengthLevel.text = LanguageManage.GetTextByKey("强化等级", "build") + "LV." + tar.strengthLevel;
		this.towerStrongName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tar.index].name, "build");
		LogManage.LogError("ta de qiang hua deng ji :" + tar.strengthLevel);
		if (!this.towerModel)
		{
			this.towerModel = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[tar.index].UpdateStarInfos[tar.star].bodyName, this.modelPositon);
		}
		if (this.towerModel)
		{
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
			Transform[] componentsInChildren = this.towerModel.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 5;
			}
			this.towerModel.tr.localPosition = UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel].makeShow[0];
			this.towerModel.tr.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel].makeShow[1]);
			this.towerModel.tr.localScale = UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel].makeShow[2];
		}
		if (tar.strengthLevel < UnitConst.GetInstance().buildingConst[tar.index].StrongInfos.Count - 1)
		{
			this.showBestStrength.SetActive(false);
			if (tar.lv < UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel + 1].needLevel)
			{
				this.needLevel.gameObject.SetActive(true);
				this.btnStrength.gameObject.SetActive(false);
				this.needLevel.text = string.Concat(new object[]
				{
					LanguageManage.GetTextByKey("需要", "build"),
					UnitConst.GetInstance().buildingConst[tar.index].name,
					LanguageManage.GetTextByKey("达到", "build"),
					"LV.",
					UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel + 1].needLevel
				});
			}
			else
			{
				this.needLevel.gameObject.SetActive(false);
				this.btnStrength.gameObject.SetActive(true);
			}
			foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel].attribute)
			{
				GameObject gameObject = NGUITools.AddChild(this.showInfoTabel.gameObject, this.showInfoPrb);
				ShowStrengthInfo component = gameObject.GetComponent<ShowStrengthInfo>();
				string text = current.Key.ToString();
				switch (text)
				{
				case "1":
					component.infoName.text = LanguageManage.GetTextByKey("生命值", "build");
					component.showFillont.fillAmount = (float)current.Value * 1f / 100f;
					component.persencent.text = current.Value + "%";
					break;
				case "2":
					component.infoName.text = LanguageManage.GetTextByKey("攻击", "build");
					component.showFillont.fillAmount = (float)current.Value * 1f / 100f;
					component.persencent.text = current.Value + "%";
					break;
				case "3":
					component.infoName.text = LanguageManage.GetTextByKey("防御", "build");
					component.showFillont.fillAmount = (float)current.Value * 1f / 100f;
					component.persencent.text = current.Value + "%";
					break;
				}
			}
			this.showInfoTabel.Reposition();
			foreach (KeyValuePair<ResType, int> current2 in UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel].rescost)
			{
				GameObject gameObject2 = NGUITools.AddChild(this.resTabel.gameObject, this.resPrefab);
				switch (current2.Key)
				{
				case ResType.金币:
					gameObject2.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resCoin <= current2.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resCoin, current2.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resCoin, current2.Value));
					break;
				case ResType.石油:
					gameObject2.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resOil <= current2.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resOil, current2.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resOil, current2.Value));
					break;
				case ResType.钢铁:
					gameObject2.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resSteel <= current2.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resSteel, current2.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resSteel, current2.Value));
					break;
				case ResType.稀矿:
					gameObject2.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().playerRes.resRareEarth <= current2.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resRareEarth, current2.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().playerRes.resRareEarth, current2.Value));
					break;
				}
				ItemTipsShow2 component2 = gameObject2.GetComponent<ItemTipsShow2>();
				component2.Index = (int)current2.Key;
				component2.Type = 2;
				AtlasManage.SetResSpriteName(gameObject2.GetComponent<UISprite>(), current2.Key);
			}
			foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel].itemCost)
			{
				GameObject gameObject3 = NGUITools.AddChild(this.resTabel.gameObject, this.itemPrefab);
				if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current3.Key))
				{
					gameObject3.GetComponentInChildren<UILabel>().text = string.Format("[FF0000]{0}[-]/{1}", 0, current3.Value);
				}
				else
				{
					gameObject3.GetComponentInChildren<UILabel>().text = ((HeroInfo.GetInstance().PlayerItemInfo[current3.Key] < current3.Value) ? string.Format("[FF0000]{0}[-]/{1}", HeroInfo.GetInstance().PlayerItemInfo[current3.Key], current3.Value) : string.Format("[FFFF00]{0}[-]/{1}", HeroInfo.GetInstance().PlayerItemInfo[current3.Key], current3.Value));
				}
				AtlasManage.SetItemUISprite(gameObject3.GetComponent<UISprite>(), current3.Key);
				ItemTipsShow2 component3 = gameObject3.GetComponent<ItemTipsShow2>();
				component3.Index = current3.Key;
				component3.Type = 1;
			}
			this.resTabel.Reposition();
		}
		else
		{
			foreach (KeyValuePair<int, int> current4 in UnitConst.GetInstance().buildingConst[tar.index].StrongInfos[tar.strengthLevel].attribute)
			{
				GameObject gameObject4 = NGUITools.AddChild(this.showInfoTabel.gameObject, this.showInfoPrb);
				ShowStrengthInfo component4 = gameObject4.GetComponent<ShowStrengthInfo>();
				int key = current4.Key;
				string text = key.ToString();
				switch (text)
				{
				case "1":
					component4.infoName.text = LanguageManage.GetTextByKey("生命值", "build");
					component4.showFillont.fillAmount = (float)current4.Value * 1f / 100f;
					component4.persencent.text = current4.Value + "%";
					break;
				case "2":
					component4.infoName.text = LanguageManage.GetTextByKey("攻击", "build");
					component4.showFillont.fillAmount = (float)current4.Value * 1f / 100f;
					component4.persencent.text = current4.Value + "%";
					break;
				case "3":
					component4.infoName.text = LanguageManage.GetTextByKey("防御", "build");
					component4.showFillont.fillAmount = (float)current4.Value * 1f / 100f;
					component4.persencent.text = current4.Value + "%";
					break;
				}
				IL_CAB:
				this.showInfoTabel.Reposition();
				this.showBestStrength.gameObject.SetActive(true);
				this.hideStrength.gameObject.SetActive(false);
				this.resTabel.gameObject.SetActive(false);
				this.needLevel.gameObject.SetActive(false);
				this.btnStrength.gameObject.SetActive(false);
				continue;
				goto IL_CAB;
			}
		}
	}

	private void ClosePanel(GameObject ga)
	{
		if (this.towerModel)
		{
			UnityEngine.Object.Destroy(this.towerModel.ga);
		}
		FuncUIManager.inst.DestoryFuncUI("TowerStrongPanel");
	}

	private void StrengthTower(GameObject ga)
	{
		if (this.tower.lv < UnitConst.GetInstance().buildingConst[this.tower.index].StrongInfos[this.tower.strengthLevel + 1].needLevel)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您的建筑等级不足", "build") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().buildingConst[this.tower.index].StrongInfos[this.tower.strengthLevel].itemCost)
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
		CalcMoneyHandler.CSCalcMoney(7, 0, this.tower.posIdx, this.tower.id, this.tower.index, 0, new Action<bool, int>(this.CalcMoneyCallBack));
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
			TowerStrenghtHandler.CS_TowerStrength(this.tower.id, delegate
			{
				this.tower.strengthLevel = SenceInfo.curMap.towerList_Data[this.tower.id].strength;
				this.tower.star = SenceInfo.curMap.towerList_Data[this.tower.id].star;
				DieBall dieBall = PoolManage.Ins.CreatEffect("car", base.transform.position, Quaternion.identity, base.transform);
				dieBall.LifeTime = 3f;
				dieBall.tr.localPosition = new Vector3(-273f, 100f, -815f);
				dieBall.tr.localScale = Vector3.one;
				Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = 18;
				}
				this.ShowStrengthInfo(this.tower);
			});
		}
	}
}
