using System;
using System.Collections.Generic;
using UnityEngine;

public class SynopsisTaskPanelManager : FuncUIPanel
{
	public static SynopsisTaskPanelManager Inis;

	public GameObject jiqingqianwang;

	public UILabel TaskInfo;

	public UILabel TaskDes;

	public UITable resTable;

	public UITable itemTable;

	public List<DailyTask> dailyTask = new List<DailyTask>();

	public DailyTask daily;

	public static int type;

	public static int Id;

	public static bool isMainUIShow;

	public override void Awake()
	{
		SynopsisTaskPanelManager.Inis = this;
		this.jiqingqianwang = base.transform.FindChild("SynopsisInfo/Btn").gameObject;
		this.TaskInfo = base.transform.FindChild("SynopsisInfo/targetInfo").GetComponent<UILabel>();
		this.TaskDes = base.transform.FindChild("SynopsisInfo/desInfo").GetComponent<UILabel>();
		this.resTable = base.transform.FindChild("SynopsisInfo/GetAwardBg/ResTable").GetComponent<UITable>();
		this.itemTable = base.transform.FindChild("SynopsisInfo/GetAwardBg/ItemTable").GetComponent<UITable>();
		EventManager.Instance.AddEvent(EventManager.EventType.TaskPane_SynopsisTaskClose, new EventManager.VoidDelegate(this.OnPanelClose));
		EventManager.Instance.AddEvent(EventManager.EventType.TaskPane_SynopsisTaskTiaoZhuan, new EventManager.VoidDelegate(this.OnTiaoZhuanPanel));
	}

	private void OnTiaoZhuanPanel(GameObject go)
	{
		if (this.daily != null && this.daily.PanelType != 0)
		{
			switch (this.daily.PanelType)
			{
			case 1:
			{
				GameObject gameObject = FuncUIManager.inst.OpenFuncUI("NBattlePanel", SenceType.Island);
				SenceInfo.CurBattle = UnitConst.GetInstance().BattleConst[UnitConst.GetInstance().BattleFieldConst[this.daily.PanelId].battleId];
				NTollgateManage.inst.tollgateID = this.daily.PanelId;
				NBattleManage.inst.CreateBattleItem();
				base.gameObject.SetActive(false);
				break;
			}
			case 2:
				if (BuildingStorePanelManage._instance == null)
				{
					BuildingStorePanelManage.Buildtype = UnitConst.GetInstance().buildingConst[this.daily.PanelId].storeType;
					FuncUIManager.inst.OpenFuncUI("BuildingStorePanel", SenceType.Island);
					base.gameObject.SetActive(false);
				}
				else
				{
					BuildingStorePanelManage._instance.gameObject.SetActive(true);
					base.gameObject.SetActive(false);
				}
				break;
			case 4:
			{
				T_Tower building = new T_Tower();
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					if (SenceManager.inst.towers[i].index == this.daily.PanelId)
					{
						if (HeroInfo.GetInstance().BuildCD.Contains(SenceManager.inst.towers[i].id))
						{
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("该建筑正在冷却，请稍后重试", "build"), HUDTextTool.TextUITypeEnum.Num5);
							return;
						}
						building = SenceManager.inst.towers[i];
					}
				}
				if (T_InfoPanelManage._instance == null)
				{
					FuncUIManager.inst.OpenFuncUI("T_InfoPanel", SenceType.Island);
					T_InfoPanelManage._instance.Show(building, false);
					T_InfoPanelManage._instance.gameObject.SetActive(true);
					base.gameObject.SetActive(false);
				}
				else
				{
					T_InfoPanelManage._instance.Show(building, false);
					T_InfoPanelManage._instance.gameObject.SetActive(true);
					base.gameObject.SetActive(false);
				}
				break;
			}
			}
		}
	}

	private void OnPanelClose(GameObject go)
	{
		SynopsisTaskPanelManager.isMainUIShow = true;
		FuncUIManager.inst.HideFuncUI("SynopsisTaskPanel");
	}

	public void OnPanelInfoShow()
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this.OnPanelInfoGet();
	}

	public void OnPanelInfoGet()
	{
		this.resTable.DestoryChildren(true);
		this.itemTable.DestoryChildren(true);
		this.dailyTask = UnitConst.GetInstance().ALLlineTask();
		for (int i = 0; i < this.dailyTask.Count; i++)
		{
			if (this.dailyTask[i].mainTaskType == 4 && this.dailyTask[i].isUIShow)
			{
				this.daily = this.dailyTask[i];
			}
		}
		if (this.daily != null)
		{
			this.TaskInfo.text = this.daily.name;
			this.TaskDes.text = this.daily.description;
			foreach (KeyValuePair<ResType, int> current in this.daily.rewardRes)
			{
				GameObject gameObject = this.resTable.CreateChildren(current.Key.ToString(), true);
				AtlasManage.SetResSpriteName(gameObject.GetComponent<UISprite>(), current.Key);
				gameObject.GetComponentInChildren<UILabel>().text = current.Value.ToString();
			}
			this.resTable.Reposition();
			foreach (KeyValuePair<int, int> current2 in this.daily.rewardItems)
			{
				GameObject gameObject2 = this.itemTable.CreateChildren(current2.Key.ToString(), true);
				if (UnitConst.GetInstance().ItemConst.ContainsKey(current2.Key))
				{
					AtlasManage.SetUiItemAtlas(gameObject2.GetComponent<UISprite>(), UnitConst.GetInstance().ItemConst[current2.Key].IconId);
					gameObject2.transform.GetChild(1).GetComponent<UISprite>().spriteName = this.OnSetQuitry((int)UnitConst.GetInstance().ItemConst[current2.Key].Quality);
					gameObject2.transform.GetChild(0).GetComponent<UILabel>().text = current2.Value.ToString();
				}
			}
			foreach (KeyValuePair<int, int> current3 in this.daily.rewardEquips)
			{
				GameObject gameObject3 = this.itemTable.CreateChildren(current3.Key.ToString(), false);
				if (UnitConst.GetInstance().equipList.ContainsKey(current3.Key))
				{
					AtlasManage.SetUiItemAtlas(gameObject3.GetComponent<UISprite>(), UnitConst.GetInstance().equipList[current3.Key].icon);
					gameObject3.transform.GetChild(1).GetComponent<UISprite>().spriteName = this.OnSetQuitry((int)UnitConst.GetInstance().equipList[current3.Key].equipQuality);
					gameObject3.transform.GetChild(0).GetComponent<UILabel>().text = current3.Value.ToString();
				}
			}
			this.resTable.Reposition();
		}
	}

	private string OnSetQuitry(int leve)
	{
		switch (leve)
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
