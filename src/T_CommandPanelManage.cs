using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class T_CommandPanelManage : MonoBehaviour
{
	public static T_CommandPanelManage _instance;

	public T_Tower tower;

	private int cost;

	public T_Res res;

	public float click_protect_time;

	public GameObject tittle;

	public GameObject addArmyAuto;

	public Transform BtnArr;

	public UILabel tittleText;

	public UILabel lv;

	public GameObject resPrefab;

	public GameObject resIconPrefab;

	private List<GameObject> resPool = new List<GameObject>();

	public GameObject[] btns;

	public GameObject tar;

	public Camera cam;

	public Transform tr;

	public GameObject Btnbg;

	public GameObject moveTittle;

	public GameObject tipBack;

	public UILabel labelOne;

	public UILabel electrictyUse;

	public UILabel labelTwo;

	public GameObject electirtySprite;

	[HideInInspector]
	public bool isResOurBuilding;

	[HideInInspector]
	public GameObject other;

	public int money;

	private Action otherOnclick;

	public DateTime endTime;

	public UISprite ShuIcon;

	public UILabel shuLabel;

	public UIGrid BtnGrid;

	public UISprite[] ResArr;

	private GameObject ga;

	public ArmyFuncPanleManage armyFuncPanel;

	private float outProNum;

	private int resType;

	private long BuildCDId;

	private float updateResourceTime;

	private bool IsUpDateText;

	private float btn_time;

	private int coinNeed;

	private Dictionary<int, int> army_Coin = new Dictionary<int, int>();

	private long ids;

	public bool isTextShow;

	public bool isBtnSetpos;

	public Vector3 pos1 = default(Vector3);

	public float pinchAddFloat = 2f;

	private float soldier_talk_time;

	private int soldier_talk_no = 1;

	private DieBall Kapai;

	private T_Res SelRes;

	private int rmbNeed;

	private int timeCost;

	public Character SelCharacter
	{
		get
		{
			if (this.tower)
			{
				return this.tower;
			}
			if (this.res)
			{
				return this.res;
			}
			return null;
		}
	}

	public void OnDestroy()
	{
		T_CommandPanelManage._instance = null;
	}

	public void Awake()
	{
		T_CommandPanelManage._instance = this;
		this.tr = base.transform;
		this.ga = base.gameObject;
		this.Init();
		this.HidePanel();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_AddArmyAuto, new EventManager.VoidDelegate(this.AddArmyAuto));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_BuildCancel, new EventManager.VoidDelegate(this.BuildCancel));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_BuildYes, new EventManager.VoidDelegate(this.BuildYes));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_EndAideCompound, new EventManager.VoidDelegate(this.EndAideCompound));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_Info, new EventManager.VoidDelegate(this.BuildInfo));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_Other, new EventManager.VoidDelegate(this.CommandOther));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_Remove, new EventManager.VoidDelegate(this.RemoveRes));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_Update, new EventManager.VoidDelegate(this.BuildUpdate));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_UpdateBuild, new EventManager.VoidDelegate(this.OnUpdateBuild));
		EventManager.Instance.AddEvent(EventManager.EventType.ReserveDutyPanle_OpenBtn, new EventManager.VoidDelegate(this.OpenReserveDutyPanle));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_OpenDefensiveCover, new EventManager.VoidDelegate(this.OpenDefensiveCover));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_Chouqu, new EventManager.VoidDelegate(this.Chouqu));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_Zhuangbei, new EventManager.VoidDelegate(this.ZhuangBei));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_JiChangPeiBing, new EventManager.VoidDelegate(this.JiChangPeiBing));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_ConvertSkill, new EventManager.VoidDelegate(this.DuiHuanSkill));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_UpdateSkill, new EventManager.VoidDelegate(this.UpdateSkill));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_TowerStrength, new EventManager.VoidDelegate(this.TowerStrength));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_TowerUpgrade, new EventManager.VoidDelegate(this.TowerUpGrade));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_BuildingUpGrade, new EventManager.VoidDelegate(this.BuildingUpGrade));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_UpdateBatches, new EventManager.VoidDelegate(this.UpdateBatches));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_ChangeSolider, new EventManager.VoidDelegate(this.ChangeSolider));
		EventManager.Instance.AddEvent(EventManager.EventType.T_CommandPanel_Apply, new EventManager.VoidDelegate(this.ApplyCdTime));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenArmyClick, new EventManager.VoidDelegate(this.OpenArmyGroup));
	}

	public void OpenArmyGroup(GameObject ga)
	{
		bool isOpen = false;
		if (HeroInfo.GetInstance().playerGroupId == 0L)
		{
			ArmyGroupHandler.CG_CSRandomLegion(1, delegate(bool isError)
			{
				if (!isError)
				{
					this.openArmyGropPanel();
					isOpen = true;
				}
			});
		}
		else
		{
			ArmyGroupHandler.CG_CSLegionData(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
			{
				if (!isError)
				{
					this.openArmyGropPanel();
					isOpen = true;
				}
			});
		}
		if (isOpen)
		{
			return;
		}
		this.openArmyGropPanel();
	}

	private void openArmyGropPanel()
	{
		FuncUIManager.inst.OpenFuncUI("ArmyPeoplePanlManager", SenceType.Island);
		if (!ArmyManager.ins.gameObject.activeSelf)
		{
			ArmyManager.ins.gameObject.SetActive(true);
		}
		base.gameObject.SetActive(false);
	}

	public void ApplyCdTime(GameObject ga)
	{
		if (HeroInfo.GetInstance().legionApply.Count == 0)
		{
			ArmyGroupHandler.CG_CSLegionHelpApply(this.tower.id, delegate(bool isError)
			{
				Debug.Log(this.tower.id);
			});
			return;
		}
		foreach (KeyValuePair<long, LegionHelpApply> current in HeroInfo.GetInstance().legionApply)
		{
			if (current.Value.buildingId == this.tower.id)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("该建筑已经请求协助", "build"), HUDTextTool.TextUITypeEnum.Num5);
				break;
			}
			ArmyGroupHandler.CG_CSLegionHelpApply(this.tower.id, delegate(bool isError)
			{
				Debug.Log(this.tower.id);
			});
		}
	}

	public void UpdateSkill(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.OpenMainPanel();
		FuncUIManager.inst.OpenFuncUI("NewSkillUpdatePanel", SenceType.Island);
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	public void Chouqu(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.OpenMainPanel();
		FuncUIManager.inst.OpenFuncUI("SkillExtractPanel", SenceType.Island);
		HUDTextTool.isSkillEquipment = false;
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	public void DuiHuanSkill(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.OpenMainPanel();
		FuncUIManager.inst.OpenFuncUI("NewConvertPanel", SenceType.Island);
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	public void ZhuangBei(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.OpenMainPanel();
		FuncUIManager.inst.OpenFuncUI("SkillEquipmentPanel", SenceType.Island);
		SkillEquipmentManage.inst.ShowPanel();
		HUDTextTool.isSkillEquipment = true;
		if (T_CommandPanelManage._instance)
		{
			T_CommandPanelManage._instance.gameObject.SetActive(false);
		}
	}

	private void JiChangPeiBing(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.OpenMainPanel();
		if (HeroInfo.GetInstance().PlayerArmyData.Count == 0)
		{
			ArmyHandler.CG_ArmsList(new Action(this.ShowJiChang));
		}
		else
		{
			this.ShowJiChang();
		}
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	private void ChangeSolider(GameObject ga)
	{
		if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 6)
		{
			this.ShowNewArmyFunc();
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 21)
		{
			this.ShowJiChang();
		}
	}

	private void ShowJiChang()
	{
		ArmyControlAndUpdatePanel.isFeijiChang = true;
		ArmyControlAndUpdatePanel.lv = this.tower.lv;
		ArmyControlAndUpdatePanel.posIndex = this.tower.posIdx;
		ArmyControlAndUpdatePanel.index = this.tower.index;
		ArmyControlAndUpdatePanel.towerID = this.tower.id;
		FuncUIManager.inst.OpenFuncUI("NewArmyControlPanel", SenceType.Island);
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	public void OpenDefensiveCover(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		if (this.tower.fangyuzhao.activeSelf)
		{
			this.tower.isfangyuzhao = false;
			this.tower.fangyuzhao.SetActive(false);
		}
		else
		{
			this.tower.isfangyuzhao = true;
			this.tower.fangyuzhao.SetActive(true);
		}
		if (SenceManager.inst.MapElectricity == SenceManager.ElectricityEnum.严重不足 || SenceManager.inst.MapElectricity == SenceManager.ElectricityEnum.电力瘫痪)
		{
			this.tower.isfangyuzhao = false;
			this.tower.fangyuzhao.SetActive(false);
		}
	}

	private void OpenReserveDutyPanle(GameObject ga)
	{
	}

	private void OnUpdateBuild(GameObject o)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.OpenMainPanel();
		FuncUIManager.inst.OpenFuncUI("SpecialSoliderUpdatePanel", SenceType.Island);
		if (!SepcialSoliderPanel.ins.gameObject.activeSelf)
		{
			SepcialSoliderPanel.ins.gameObject.SetActive(true);
		}
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	private void AddArmyAuto(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		if (this.coinNeed > 0)
		{
			if (this.coinNeed > HeroInfo.GetInstance().playerRes.resCoin)
			{
				ExpensePanelManage.ClearCache();
				this.rmbNeed = ResourceMgr.GetRMBNum(BuildingProductType.coin, this.coinNeed - HeroInfo.GetInstance().playerRes.resCoin);
				if (this.rmbNeed > 0)
				{
					ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("获取金币", "ResIsland") + ":" + (this.coinNeed - HeroInfo.GetInstance().playerRes.resCoin));
				}
				else
				{
					ExpensePanelManage.isCanNotBuyCoin = true;
					ExpensePanelManage.strings.Add("[ff0000]" + LanguageManage.GetTextByKey("金币购买已达上限（升级司令部可增加金币购买上限）", "ResIsland") + "[-]");
				}
				MessageBox.GetExpensePanel().Show(this.rmbNeed, DateTime.MinValue, new Action<bool, int>(this.FuncArmyAuto));
			}
			else
			{
				this.FuncArmyAuto(true, 0);
			}
		}
		this.HidePanel();
	}

	private void EndAideCompound(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		if (AdjutantPanel.isCanOpen)
		{
			this.OnUpdatIcon();
			FuncUIManager.inst.AdjutantPanel.ShowAideData(false);
			this.HidePanel();
		}
		else
		{
			this.OnT_commandPanelCommondInfo();
		}
	}

	private void BuildCancel(GameObject ga)
	{
		BuilingStatePanel.inst.BuildingFinishBtn.SetActive(true);
		BuilingStatePanel.inst.WallLineBtn.transform.localScale = Vector3.one;
		BuilingStatePanel.inst.WallRotateBtn.transform.localScale = Vector3.one;
		SenceManager.inst.ShowTowerR(false, null);
		SenceManager.inst.RemoveTempBuilding();
		this.HidePanel();
		if (CameraControl.inst)
		{
			CameraControl.inst.ChangeCameraBuildingState(false);
		}
		if (base.gameObject)
		{
			base.gameObject.SetActive(false);
		}
	}

	private void BuildYes(GameObject ga)
	{
		BuilingStatePanel.inst.BuildingFinishBtn.SetActive(true);
		BuilingStatePanel.inst.WallLineBtn.transform.localScale = Vector3.one;
		BuilingStatePanel.inst.WallRotateBtn.transform.localScale = Vector3.one;
		SenceManager.inst.ShowTowerR(false, null);
		SenceManager.inst.RebackTower();
		if (SenceManager.inst.tempTower != null && SenceManager.inst.tempTower.canBuild)
		{
			SenceManager.inst.SurTempBuilding();
			UIManager.curState = SenceState.Home;
			this.HidePanel();
			if (base.gameObject)
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("不能建造", "build"), HUDTextTool.TextUITypeEnum.Num5);
			LogManage.LogError(string.Format("CanNotBuilid  SenceManager.inst.tempTower==null ={0} SenceManager.inst.tempTower.canBuild={1}", SenceManager.inst.tempTower == null, SenceManager.inst.tempTower.canBuild));
		}
	}

	private void BuildingUpGrade(GameObject ga)
	{
		this.OpenMainPanel();
		SenceManager.inst.ShowTowerR(false, null);
		if (!HeroInfo.GetInstance().BuildCD.Contains(this.tower.id))
		{
			FuncUIManager.inst.OpenFuncUI("BuildingUpGradePanel", SenceType.Island);
			BuildingUpGradePanel._ins.gameObject.SetActive(true);
			if (BuildingUpGradePanel._ins.gameObject.activeSelf)
			{
				BuildingUpGradePanel._ins.ShowBuildingUpGrade(this.tower);
			}
			FuncUIManager.inst.T_CommandPanelManage.HidePanel();
		}
		else
		{
			this.HidePanel();
			if (base.gameObject)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	private void TowerUpGrade(GameObject ga)
	{
		this.OpenMainPanel();
		SenceManager.inst.ShowTowerR(false, null);
		if (!HeroInfo.GetInstance().BuildCD.Contains(this.tower.id))
		{
			FuncUIManager.inst.OpenFuncUI("TowerUpdatePanel", SenceType.Island);
			TowerUpdatePanel._ins.gameObject.SetActive(true);
			if (TowerUpdatePanel._ins.gameObject.activeSelf)
			{
				TowerUpdatePanel._ins.ShowUpGrade(this.tower);
			}
			FuncUIManager.inst.T_CommandPanelManage.HidePanel();
		}
		else
		{
			this.HidePanel();
			if (base.gameObject)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	private void TowerStrength(GameObject ga)
	{
		this.OpenMainPanel();
		SenceManager.inst.ShowTowerR(false, null);
		if (!HeroInfo.GetInstance().BuildCD.Contains(this.tower.id))
		{
			FuncUIManager.inst.OpenFuncUI("TowerStrongPanel", SenceType.Island);
			TowerStrengthPanel.ins.gameObject.SetActive(true);
			if (TowerStrengthPanel.ins.gameObject.activeSelf)
			{
				TowerStrengthPanel.ins.ShowStrengthInfo(this.tower);
			}
			FuncUIManager.inst.T_CommandPanelManage.HidePanel();
		}
		else
		{
			this.HidePanel();
			if (base.gameObject)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	public void BuildUpdateByTask(T_Tower _tower)
	{
		this.tower = _tower;
		this.BuildUpdate(null);
	}

	public void ChangeSoliderByTask(T_Tower _tower)
	{
		this.tower = _tower;
		this.ChangeSolider(null);
	}

	private void BuildUpdate(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.OpenMainPanel();
		if (!HeroInfo.GetInstance().BuildCD.Contains(this.tower.id))
		{
			FuncUIManager.inst.T_InfoPanelManage.Show(this.tower, false);
			FuncUIManager.inst.T_CommandPanelManage.HidePanel();
		}
		else
		{
			this.HidePanel();
			if (base.gameObject)
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	private void UpdateBatches(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		int count = (from a in SenceManager.inst.towers
		where UnitConst.GetInstance().buildingConst[a.index].secType == 20 && a.lv == this.tower.lv
		select a.id).ToList<long>().Count;
		ExpensePanelManage.ClearCache();
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().buildingConst[this.tower.index].lvInfos[this.tower.lv].resCost)
		{
			switch (current.Key)
			{
			case ResType.金币:
			{
				int num = current.Value * count;
				ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("升级所需金币", "ResIsland") + "： " + num);
				break;
			}
			case ResType.石油:
			{
				int num2 = current.Value * count;
				ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("升级所需石油", "ResIsland") + ":" + num2);
				break;
			}
			case ResType.钢铁:
			{
				int num3 = current.Value * count;
				ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("升级所需钢铁", "ResIsland") + ":" + num3);
				break;
			}
			case ResType.稀矿:
			{
				int num4 = current.Value * count;
				ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("升级所需稀矿", "ResIsland") + ":" + num4);
				break;
			}
			}
		}
		if (HeroInfo.GetInstance().BuildCD.Count == BuildingQueue.MaxBuildingQueue)
		{
			ExpensePanelManage.strings.Add(string.Format("{0}:{1}", LanguageManage.GetTextByKey("立即结束冷却", "ResIsland"), TimeTools.ConvertFloatToTimeBySecond((float)(TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[HeroInfo.GetInstance().getBuildingCdId()]) - TimeTools.GetNowTimeSyncServerToDateTime()).TotalSeconds)));
		}
		MessageBox.GetExpensePanel().ShowRes(LanguageManage.GetTextByKey("一键升级城墙", "ResIsland"), delegate(bool isBuy, int i)
		{
			if (isBuy)
			{
				CalcMoneyHandler.CSCalcMoney_Walls(10, (from a in SenceManager.inst.towers
				where UnitConst.GetInstance().buildingConst[a.index].secType == 20 && a.lv == this.tower.lv
				select a.id).ToList<long>(), new Action<bool, int>(this.CalcMoneyCallBack_Wall));
			}
		});
		this.HidePanel();
	}

	private void CalcMoneyCallBack_Wall(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				HUDTextTool.inst.ShowBuyMoney();
				return;
			}
			CSCityWallBatchUpLevel cSCityWallBatchUpLevel = new CSCityWallBatchUpLevel();
			cSCityWallBatchUpLevel.cityWallIds.AddRange((from a in SenceManager.inst.towers
			where UnitConst.GetInstance().buildingConst[a.index].secType == 20 && a.lv == this.tower.lv
			select a.id).ToList<long>());
			ClientMgr.GetNet().SendHttp(2024, cSCityWallBatchUpLevel, new DataHandler.OpcodeHandler(BuildingHandler.GC_NewBuildingUpdateEnd), null);
		}
	}

	private void BuildInfo(GameObject ga)
	{
		this.OpenMainPanel();
		SenceManager.inst.ShowTowerR(false, null);
		FuncUIManager.inst.T_InfoPanelManage.Show(this.tar.GetComponent<T_Tower>(), true);
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	public void CommandOther(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		if (this.otherOnclick != null && ga.name == "副官9" && AdjutantPanelData.Aide_ServerList.Count == 0)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还没有副官，请合成！", "officer"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		this.otherOnclick();
	}

	private void RemoveRes(GameObject ga)
	{
		this.OpenMainPanel();
		SenceManager.inst.ShowTowerR(false, null);
		this.res = this.tar.GetComponent<T_Res>();
		this.cost = UnitConst.GetInstance().buildingConst[this.res.index].lvInfos[0].resCost[ResType.金币];
		if (UnitConst.GetInstance().buildingConst[this.res.index].lvInfos[((this.res.lv != 0) ? this.res.lv : 1) - 1].needCommandLevel > HeroInfo.GetInstance().PlayerCommondLv)
		{
			MessageBox.GetMessagePanel().ShowBtn("铲除", string.Format("长官，您的司令部等级不足，请将司令部升到LV.{0}再铲除！", UnitConst.GetInstance().buildingConst[this.res.index].lvInfos[((this.res.lv != 0) ? this.res.lv : 1) - 1].needCommandLevel), "确定", delegate
			{
				this.HidePanel();
			});
			return;
		}
		this.RemoveResByCoin();
		this.HidePanel();
	}

	private void DisplayResource()
	{
		if (this.btns[2].activeSelf || this.btns[3].activeSelf || this.btns[4].activeSelf || this.btns[5].activeSelf)
		{
			if (this.btns[2].activeSelf)
			{
				this.resType = 1;
			}
			else if (this.btns[3].activeSelf)
			{
				this.resType = 2;
			}
			else if (this.btns[4].activeSelf)
			{
				this.resType = 3;
			}
			else if (this.btns[5].activeSelf)
			{
				this.resType = 4;
			}
			if (this.tower)
			{
				this.outProNum = (float)(TimeTools.GetNowTimeSyncServerToDateTime() - SenceInfo.curMap.ResourceBuildingList[this.tower.id].takeTime).TotalHours * this.tower.ResSpeed_ByStep_Ele_Tech_Vip;
				if (this.outProNum > 1f)
				{
					this.other.GetComponent<UISprite>().color = Color.white;
				}
				else
				{
					LogManage.Log(string.Format("当前与服务器同步后的时间是{3},塔的时间戳是{0},效率是{1},当前产量是{2}", new object[]
					{
						SenceInfo.curMap.ResourceBuildingList[this.tower.id].takeTime,
						UnitConst.GetInstance().buildingConst[this.tower.index].lvInfos[this.tower.lv].outputNum,
						this.outProNum,
						TimeTools.GetNowTimeSyncServerToDateTime()
					}));
					this.other.GetComponent<UISprite>().color = Color.black;
				}
			}
		}
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
			BuildingHandler.CG_BuildingRemoveStart(money, this.res);
		}
	}

	private void RemoveResByCoin()
	{
		CalcMoneyHandler.CSCalcMoney(6, 0, this.res.posIndex, this.res.id, this.res.index, 0, new Action<bool, int>(this.CalcMoneyCallBack));
	}

	private void Update()
	{
		if (this.click_protect_time > 0f)
		{
			this.click_protect_time -= Time.deltaTime;
		}
		if (this.IsUpDateText)
		{
			if (this.ga.layer == 0)
			{
				Transform[] componentsInChildren = this.ga.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = 9;
				}
			}
			this.UpDateText();
		}
		else if (this.ga.layer == 9)
		{
			Transform[] componentsInChildren2 = this.ga.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				transform2.gameObject.layer = 5;
			}
		}
		if (Time.time > this.updateResourceTime)
		{
			this.updateResourceTime = Time.time + 2f;
			this.DisplayResource();
		}
		this.btn_time += Time.deltaTime;
		if (this.btn_time > 0.2f)
		{
			this.btn_time = -0.2f;
		}
		if (this.ga.activeSelf)
		{
			if (this.btn_time >= 0f)
			{
				this.BtnGrid.transform.localScale = Vector3.one * 1f;
				this.moveTittle.transform.localScale = Vector3.one * 1f;
			}
			else if (!this.btns[25].gameObject.activeSelf)
			{
				this.BtnGrid.transform.localScale = new Vector3(1.001f, 1f, 1f);
				this.moveTittle.transform.localScale = new Vector3(1.001f, 1f, 1f);
			}
		}
	}

	public void UpDateText()
	{
		if (SenceManager.inst.WallLineChoose && SenceManager.inst.GetWallLately != null && BuilingStatePanel.inst.BuildingFinishBtn.activeSelf)
		{
			this.moveTittle.SetActive(false);
			this.CLearBtns();
			return;
		}
		this.pos1 = new Vector3(-4f, 3f, 1f);
		this.soldier_talk_time += Time.deltaTime;
		if (this.soldier_talk_time >= 10f)
		{
			this.soldier_talk_time = 0f;
			this.soldier_talk_no++;
			if (this.soldier_talk_no > 3)
			{
				this.soldier_talk_no = 1;
			}
		}
		if (this.tar != null)
		{
			Vector3 zero = Vector3.zero;
			if (!this.tar.GetComponent<T_Res>())
			{
				if (this.tar.GetComponent<T_Tower>())
				{
					zero = new Vector3(this.tar.gameObject.transform.position.x, this.tar.gameObject.transform.position.y + 2f, this.tar.gameObject.transform.position.z);
				}
				else if (this.tar.GetComponent<T_Tank>())
				{
					zero = new Vector3(this.tar.gameObject.transform.position.x + this.pos1.x, this.tar.gameObject.transform.position.y + this.pos1.y, this.tar.gameObject.transform.position.z + this.pos1.z);
				}
				else if (this.tar.GetComponent<T_CommanderHome>())
				{
					zero = new Vector3(this.tar.gameObject.transform.position.x + this.pos1.x, this.tar.gameObject.transform.position.y + this.pos1.y, this.tar.gameObject.transform.position.z + this.pos1.z);
					if (this.btns[25])
					{
						this.btns[25].GetComponent<UIWidget>().SetDimensions(200, 72);
						languageLableKey[] componentsInChildren = this.btns[25].GetComponentsInChildren<languageLableKey>();
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							languageLableKey languageLableKey = componentsInChildren[i];
							if (languageLableKey != null)
							{
								languageLableKey.key = "特种兵对话" + this.soldier_talk_no;
							}
						}
						this.btns[25].transform.position = new Vector3(this.tar.gameObject.transform.position.x + this.pos1.x, this.tar.gameObject.transform.position.y + this.pos1.y, this.tar.gameObject.transform.position.z + this.pos1.z);
						this.btns[25].transform.localScale = Vector3.one * 16f;
						this.btns[25].transform.eulerAngles = new Vector3(Camera_FingerManager.inst.gameObject.transform.eulerAngles.x, -135f, 0f);
					}
				}
			}
		}
	}

	public void ShowResRemoveBtn(T_Res _res)
	{
		this.IsUpDateText = false;
		Transform[] componentsInChildren = this.ga.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 5;
		}
		if (HeroInfo.GetInstance().BuildCD.Contains((long)_res.posIndex))
		{
			this.res = _res;
			this.tar = this.res.ga;
			if (this.tower)
			{
				this.tower.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Idle);
				this.tower = null;
			}
			if (this.SelRes)
			{
				this.SelRes.RebackShader();
				this.SelRes = null;
			}
			this.cam = UIManager.inst.uiCamera;
			this.HidePanel();
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.labelOne.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[this.res.index].name, "build");
			this.tipBack.gameObject.SetActive(false);
			this.btns[18].SetActive(true);
			if (HeroInfo.GetInstance().playerGroupId != 0L)
			{
				this.btns[31].SetActive(true);
			}
			this.ShowResTitle(_res);
			this.onSetBtnPos();
		}
	}

	public void ShowBtn(Character tar1)
	{
		this.IsUpDateText = false;
		bool flag = false;
		T_Tower t_Tower = null;
		if (tar1.GetComponent<T_Tower>())
		{
			t_Tower = tar1.GetComponent<T_Tower>();
		}
		else if (tar1.GetComponent<T_CommanderHome>())
		{
			flag = true;
		}
		if (this.tower)
		{
			this.tower.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Idle);
			this.tower = null;
		}
		if (this.SelRes)
		{
			this.SelRes.RebackShader();
			this.SelRes = null;
		}
		if (!flag)
		{
			this.tower = t_Tower;
			if (this.tower.index == 90)
			{
				SenceManager.inst.ChooseWallForEffect = this.tower;
			}
			this.tower.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Selected);
			if (this.moveTittle)
			{
				this.moveTittle.SetActive(true);
			}
		}
		SenceState curState = UIManager.curState;
		switch (curState)
		{
		case SenceState.Home:
		case SenceState.InBuild:
		case SenceState.WatchResIsland:
			goto IL_116;
		case SenceState.Spy:
		case SenceState.Attacking:
			IL_109:
			if (curState != SenceState.Visit)
			{
				return;
			}
			goto IL_116;
		}
		goto IL_109;
		IL_116:
		this.HidePanel();
		if ((this.ga && !this.ga.activeSelf) || flag)
		{
			this.ga.SetActive(true);
		}
		this.tar = tar1.gameObject;
		this.cam = UIManager.inst.uiCamera;
		this.isResOurBuilding = false;
		this.CLearBtns();
		if (HeroInfo.GetInstance().IsArmyFuncingBuilding(t_Tower.id))
		{
			this.btns[33].SetActive(true);
		}
		if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 15 && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime > 0L)
		{
			this.btns[33].SetActive(true);
		}
		if ((UnitConst.GetInstance().buildingConst[this.tower.index].secType == 15 || UnitConst.GetInstance().buildingConst[this.tower.index].secType == 6 || UnitConst.GetInstance().buildingConst[this.tower.index].secType == 21) && this.tower.trueLv >= 1)
		{
			this.armyFuncPanel.InitData(this.tower.trueLv, this.tower.index, this.tower.id);
		}
		if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 6 && HeroInfo.GetInstance().armyBuildingCDTime > 0L)
		{
			this.btns[18].SetActive(true);
			if (!HeroInfo.GetInstance().IsArmyFuncingBuilding(t_Tower.id) && HeroInfo.GetInstance().playerGroupId != 0L)
			{
				this.btns[31].SetActive(true);
			}
			this.BtnGrid.Reposition();
			return;
		}
		if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 21 && HeroInfo.GetInstance().airBuildingCDTime > 0L)
		{
			this.btns[18].SetActive(true);
			if (!HeroInfo.GetInstance().IsArmyFuncingBuilding(t_Tower.id) && HeroInfo.GetInstance().playerGroupId != 0L)
			{
				this.btns[31].SetActive(true);
			}
			this.BtnGrid.Reposition();
			return;
		}
		if (HeroInfo.GetInstance().BuildCD.Contains(this.tower.id))
		{
			this.btns[18].SetActive(true);
			if (!HeroInfo.GetInstance().IsArmyFuncingBuilding(t_Tower.id) && HeroInfo.GetInstance().playerGroupId != 0L)
			{
				this.btns[31].SetActive(true);
			}
			this.BtnGrid.Reposition();
			return;
		}
		this.tittleText.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[t_Tower.index].name, "build");
		if (UIManager.curState == SenceState.InBuild)
		{
			BuilingStatePanel.inst.BuildingFinishBtn.SetActive(false);
			BuilingStatePanel.inst.WallLineBtn.transform.localScale = Vector3.zero;
			BuilingStatePanel.inst.WallRotateBtn.transform.localScale = Vector3.zero;
			this.btns[31].SetActive(false);
			this.btns[14].SetActive(true);
			this.btns[15].SetActive(true);
			this.btns[14].GetComponent<UIButton>().enabled = false;
			this.btns[15].GetComponent<UIButton>().enabled = false;
			this.moveTittle.SetActive(false);
			this.ids = t_Tower.id;
			this.isTextShow = true;
			this.lv.text = "LV . " + (t_Tower.lv + 1);
		}
		else if (UIManager.curState == SenceState.WatchResIsland)
		{
			this.lv.text = "LV . " + t_Tower.lv;
			this.btns[0].SetActive(true);
		}
		else
		{
			if (this.moveTittle == null)
			{
				return;
			}
			this.moveTittle.SetActive(true);
			this.lv.text = "LV . " + t_Tower.lv.ToString();
			this.lv.gameObject.SetActive(UnitConst.GetInstance().buildingConst[t_Tower.index].resType != 5);
			if (UIManager.curState == SenceState.WatchResIsland || UIManager.curState == SenceState.Visit)
			{
				return;
			}
			this.RefreshBtn();
		}
		if (flag)
		{
			this.CLearBtns();
			if (this.btns[25].gameObject != null)
			{
				this.btns[25].SetActive(true);
			}
			this.moveTittle.SetActive(false);
		}
		this.BtnGrid.Reposition();
		this.onSetBtnPos();
		this.showTitle(t_Tower);
	}

	private void RefreshBtn()
	{
		this.btns[1].SetActive(true);
		this.btns[0].SetActive(UnitConst.GetInstance().buildingConst[this.tower.index].resType != 5);
		if (UnitConst.GetInstance().buildingConst[this.tower.index].lvInfos.Count > 2)
		{
			this.btns[1].SetActive(true);
		}
		else
		{
			this.btns[1].SetActive(false);
		}
		if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 20 && UnitConst.GetInstance().buildingConst[this.tower.index].lvInfos.Count > this.tower.lv + 1 && UnitConst.GetInstance().buildingConst[this.tower.index].lvInfos[this.tower.lv + 1].needCommandLevel <= HeroInfo.GetInstance().PlayerCommondLv && SenceManager.inst.towers.Count((T_Tower a) => UnitConst.GetInstance().buildingConst[a.index].secType == 20 && a.lv == this.tower.lv) > 1)
		{
			this.btns[29].SetActive(true);
		}
		else
		{
			this.btns[29].SetActive(false);
		}
		if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 15 || UnitConst.GetInstance().buildingConst[this.tower.index].secType == 6 || UnitConst.GetInstance().buildingConst[this.tower.index].secType == 21)
		{
			this.armyFuncPanel.InitData(this.tower.trueLv, this.tower.index, this.tower.id);
			if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 6 && HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(this.tower.index) && HeroInfo.GetInstance().PlayerBuildingLevel[this.tower.index] >= 2)
			{
				this.btns[30].SetActive(true);
			}
			if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 21)
			{
				this.btns[22].SetActive(true);
			}
			if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 15)
			{
				this.btns[16].SetActive(true);
			}
		}
		if ((UnitConst.GetInstance().buildingConst[this.tower.index].secType == 2 || UnitConst.GetInstance().buildingConst[this.tower.index].secType == 3) && this.tower.index != 15)
		{
			if (this.tower.star > 0 || UnitConst.GetInstance().buildingConst[this.tower.index].buildGradeInfos[this.tower.star].needLevel <= this.tower.lv)
			{
				this.btns[28].SetActive(true);
			}
			else
			{
				this.btns[28].SetActive(false);
			}
			this.otherOnclick = delegate
			{
				this.HidePanel();
			};
		}
		if (UnitConst.GetInstance().buildingConst[this.tower.index].resType == 1 && UnitConst.GetInstance().buildingConst[this.tower.index].secType == 3)
		{
			this.isResOurBuilding = true;
			switch (UnitConst.GetInstance().buildingConst[this.tower.index].outputType)
			{
			case ResType.金币:
				this.other = this.btns[2];
				this.btns[2].SetActive(true);
				break;
			case ResType.石油:
				this.other = this.btns[3];
				this.btns[3].SetActive(true);
				break;
			case ResType.钢铁:
				this.other = this.btns[4];
				this.btns[4].SetActive(true);
				break;
			case ResType.稀矿:
				this.other = this.btns[5];
				this.btns[5].SetActive(true);
				break;
			}
			BuildingNPC buildingNPC;
			if (SenceInfo.curMap.ResourceBuildingList.ContainsKey(this.tower.id))
			{
				buildingNPC = SenceInfo.curMap.ResourceBuildingList[this.tower.id];
			}
			else
			{
				buildingNPC = null;
			}
			if (buildingNPC == null)
			{
				LogManage.Log("没有这个资源");
			}
			if (SenceInfo.curMap.ResourceBuildingList.ContainsKey(this.tower.id))
			{
				int num;
				if (UnitConst.GetInstance().buildingConst[this.tower.index].UpdateStarInfos.Count == 0)
				{
					num = (int)((double)UnitConst.GetInstance().buildingConst[this.tower.index].lvInfos[this.tower.lv].outputNum / 3600.0 * (TimeTools.GetNowTimeSyncServerToDateTime() - SenceInfo.curMap.ResourceBuildingList[this.tower.id].takeTime).TotalSeconds);
				}
				else
				{
					num = (int)((double)((float)UnitConst.GetInstance().buildingConst[this.tower.index].lvInfos[this.tower.lv].outputNum * (1f + (float)UnitConst.GetInstance().buildingConst[this.tower.index].buildGradeInfos[this.tower.star].output * 0.01f)) / 3600.0 * (TimeTools.GetNowTimeSyncServerToDateTime() - SenceInfo.curMap.ResourceBuildingList[this.tower.id].takeTime).TotalSeconds);
				}
				if (num >= 1)
				{
					this.other.GetComponent<UIButton>().enabled = true;
					this.other.GetComponent<BoxCollider>().enabled = true;
					this.other.GetComponent<UISprite>().color = Color.white;
				}
				else
				{
					this.other.GetComponent<UIButton>().enabled = false;
					this.other.GetComponent<BoxCollider>().enabled = false;
					this.other.GetComponent<UISprite>().color = Color.black;
				}
			}
			this.otherOnclick = delegate
			{
				if (this.other.GetComponent<UISprite>().color == Color.white)
				{
					FuncUIManager.inst.ResourcePanelManage.CollectResource(this.tower, delegate
					{
						this.other.GetComponent<UISprite>().color = Color.black;
					});
					this.other.GetComponent<UIButton>().enabled = false;
					this.other.GetComponent<UISprite>().color = Color.black;
				}
				else
				{
					LogManage.Log("NO ~~~~~~~~~~~~~~~`");
				}
			};
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].resIdx == 10)
		{
			if (Application.platform == RuntimePlatform.WindowsEditor)
			{
				this.btns[32].SetActive(true);
			}
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].resIdx == 11)
		{
			SenceManager.inst.DelaySendHttp();
			this.btns[7].SetActive(true);
			this.btns[20].SetActive(true);
			this.btns[21].SetActive(true);
			this.otherOnclick = delegate
			{
				this.HidePanel();
				FuncUIManager.inst.OpenFuncUI("TechnologyUpdateTreePanel", SenceType.Island);
			};
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].resType == 5 && UnitConst.GetInstance().buildingConst[this.tower.index].secType == 7)
		{
			this.btns[11].SetActive(true);
			this.otherOnclick = delegate
			{
				this.HidePanel();
			};
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].resIdx == 14)
		{
			this.btns[9].SetActive(true);
			this.btns[10].SetActive(true);
			this.OnUpdatIcon();
			this.otherOnclick = delegate
			{
				this.HidePanel();
				FuncUIManager.inst.AdjutantPanel.ShowAideData(true);
			};
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].resIdx == 62)
		{
			this.btns[19].SetActive(true);
			this.otherOnclick = delegate
			{
				this.HidePanel();
			};
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].resType == 2 && UnitConst.GetInstance().buildingConst[this.tower.index].secType == 18)
		{
			this.btns[8].SetActive(true);
			this.btns[17].SetActive(true);
			this.otherOnclick = delegate
			{
				this.HidePanel();
				if (HeroInfo.GetInstance().PlayerArmyData.Count == 0)
				{
					ArmyHandler.CG_ArmsList(new Action(this.ShowArmyFuncInfo));
				}
				else
				{
					this.ShowArmyFuncInfo();
				}
			};
		}
		else if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 8)
		{
			if (this.tower.strengthLevel > 0 || UnitConst.GetInstance().buildingConst[this.tower.index].StrongInfos[this.tower.strengthLevel].needLevel <= this.tower.lv)
			{
				this.btns[27].SetActive(true);
			}
			else
			{
				this.btns[27].SetActive(false);
			}
			if (this.tower.star > 0 || UnitConst.GetInstance().buildingConst[this.tower.index].UpdateStarInfos[this.tower.star].needlevel <= this.tower.lv)
			{
				this.btns[26].SetActive(true);
			}
			else
			{
				this.btns[26].SetActive(false);
			}
			this.otherOnclick = delegate
			{
				this.HidePanel();
			};
		}
	}

	public void showTitle(T_Tower tower)
	{
		this.IsUpDateText = false;
		Transform[] componentsInChildren = this.ga.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 5;
		}
		this.moveTittle.SetActive(true);
		this.labelOne.gameObject.SetActive(true);
		this.tipBack.SetActive(true);
		if (tower.buildingState == T_Tower.TowerBuildingEnum.InBuilding)
		{
			this.labelOne.transform.localPosition = new Vector3(0f, -194.3f, 0f);
			this.labelOne.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].name, "build") + "LV." + tower.lv;
			this.electirtySprite.gameObject.SetActive(false);
			this.tipBack.GetComponent<UIWidget>().width = this.labelOne.width + 100;
			this.tipBack.GetComponent<UIWidget>().height = 45;
			this.tipBack.transform.localPosition = Vector3.zero;
		}
		if (tower.buildingState != T_Tower.TowerBuildingEnum.InBuilding)
		{
			if (UnitConst.GetInstance().buildingConst[tower.index].secType == 3 || UnitConst.GetInstance().buildingConst[tower.index].secType == 8 || UnitConst.GetInstance().buildingConst[tower.index].secType == 2)
			{
				this.labelOne.transform.localPosition = new Vector3(0f, -175f, 0f);
				this.labelOne.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].name, "build") + "LV." + tower.lv;
				this.electirtySprite.gameObject.SetActive(true);
				this.tipBack.GetComponent<UIWidget>().width = this.labelOne.width + 100;
				this.tipBack.GetComponent<UIWidget>().height = 66;
				this.tipBack.transform.localPosition = new Vector3(0f, -13f, 0f);
				this.electrictyUse.text = LanguageManage.GetTextByKey("电力消耗", "build") + UnitConst.GetInstance().buildingConst[tower.index].lvInfos[tower.lv].electricUse;
			}
			else
			{
				this.labelOne.transform.localPosition = new Vector3(0f, -194.3f, 0f);
				this.labelOne.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].name, "build") + "LV." + tower.lv;
				this.electirtySprite.gameObject.SetActive(false);
				this.tipBack.GetComponent<UIWidget>().width = this.labelOne.width + 100;
				this.tipBack.GetComponent<UIWidget>().height = 45;
				this.tipBack.transform.localPosition = Vector3.zero;
			}
		}
	}

	public void ShowSolider(Character tar1)
	{
		this.IsUpDateText = true;
		Transform[] componentsInChildren = this.ga.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 9;
		}
		bool flag = false;
		if (tar1.GetComponent<T_CommanderHome>())
		{
			flag = true;
		}
		if (!flag)
		{
			this.tower.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Selected);
		}
		this.HidePanel();
		if ((this.ga && !this.ga.activeSelf) || flag)
		{
			this.ga.SetActive(true);
		}
		this.tar = tar1.gameObject;
		this.cam = UIManager.inst.uiCamera;
		this.isResOurBuilding = false;
		this.CLearBtns();
		if (flag)
		{
			this.CLearBtns();
			if (this.btns[25].gameObject != null)
			{
				this.btns[25].SetActive(true);
			}
			this.moveTittle.SetActive(false);
		}
		this.onSetBtnPos();
	}

	public void onSetBtnPos()
	{
		this.BtnGrid.Reposition();
	}

	private void ShowArmyFuncInfo()
	{
		this.ShowNewArmyFunc();
	}

	private void ShowNewArmyFunc()
	{
		ArmyControlAndUpdatePanel.isFeijiChang = false;
		ArmyControlAndUpdatePanel.lv = this.tower.lv;
		ArmyControlAndUpdatePanel.posIndex = this.tower.posIdx;
		ArmyControlAndUpdatePanel.index = this.tower.index;
		ArmyControlAndUpdatePanel.towerID = this.tower.id;
		FuncUIManager.inst.OpenFuncUI("NewArmyControlPanel", SenceType.Island);
		FuncUIManager.inst.T_CommandPanelManage.HidePanel();
	}

	public void HideMainPanel()
	{
		if (MainUIPanelManage._instance && MainUIPanelManage._instance.gameObject.activeSelf)
		{
			MainUIPanelManage._instance.downLeft.SetActive(false);
			MainUIPanelManage._instance.downRight.SetActive(false);
		}
	}

	public void OpenMainPanel()
	{
		if (MainUIPanelManage._instance && MainUIPanelManage._instance.gameObject.activeSelf)
		{
			MainUIPanelManage._instance.downLeft.SetActive(true);
			MainUIPanelManage._instance.downRight.SetActive(true);
			if (NewbieGuidePanel.guideIdByServer == 9)
			{
				HUDTextTool.inst.NextLuaCall("第七组 MainUIPanel 调Lua· ·", new object[0]);
			}
		}
	}

	public void ShowResBtn(T_Res resBuilding)
	{
		this.IsUpDateText = false;
		Transform[] componentsInChildren = this.ga.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 5;
		}
		if (this.tower)
		{
			this.tower.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Idle);
			this.tower = null;
		}
		if (this.SelRes)
		{
			this.SelRes.RebackShader();
			this.SelRes = null;
		}
		this.HidePanel();
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this.isTextShow = false;
		this.tipBack.gameObject.SetActive(false);
		if (resBuilding == null)
		{
			this.btns[13].SetActive(false);
		}
		else
		{
			FuncUIManager.inst.OpenFuncUI_NoQueue("T_CommandPanel");
			this.cam = UIManager.inst.uiCamera;
			this.tar = resBuilding.gameObject;
			this.SelRes = resBuilding;
			resBuilding.SetLogoFlash();
			this.btns[13].SetActive(true);
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().buildingConst[resBuilding.index].lvInfos[0].resCost)
			{
				switch (current.Key)
				{
				case ResType.金币:
					this.ShuIcon.spriteName = "新金矿";
					if (HeroInfo.GetInstance().playerRes.resCoin > current.Value)
					{
						this.shuLabel.color = Color.white;
					}
					else
					{
						this.shuLabel.color = Color.red;
					}
					this.shuLabel.text = current.Value.ToString();
					break;
				case ResType.石油:
					this.ShuIcon.spriteName = "晶片";
					if (HeroInfo.GetInstance().playerRes.resOil > current.Value)
					{
						this.shuLabel.color = Color.white;
					}
					else
					{
						this.shuLabel.color = Color.red;
					}
					this.shuLabel.text = current.Value.ToString();
					break;
				case ResType.钢铁:
					this.ShuIcon.spriteName = "新钢铁";
					if (HeroInfo.GetInstance().playerRes.resSteel > current.Value)
					{
						this.shuLabel.color = Color.white;
					}
					else
					{
						this.shuLabel.color = Color.red;
					}
					this.shuLabel.text = current.Value.ToString();
					break;
				case ResType.稀矿:
					this.ShuIcon.spriteName = "新稀矿";
					if (HeroInfo.GetInstance().playerRes.resRareEarth > current.Value)
					{
						this.shuLabel.color = Color.white;
					}
					else
					{
						this.shuLabel.color = Color.red;
					}
					this.shuLabel.text = current.Value.ToString();
					break;
				}
			}
		}
		this.ShowResTitle(resBuilding);
		this.onSetBtnPos();
	}

	public void ShowResTitle(T_Res res)
	{
		this.IsUpDateText = false;
		Transform[] componentsInChildren = this.ga.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 5;
		}
		this.moveTittle.SetActive(true);
		this.labelOne.gameObject.SetActive(true);
		this.labelOne.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[res.index].name, "build");
		this.labelOne.transform.localPosition = new Vector3(0f, -175f, 0f);
		this.tipBack.gameObject.SetActive(true);
		this.tipBack.transform.localPosition = Vector3.zero;
		this.tipBack.GetComponent<UIWidget>().height = 46;
		this.tipBack.GetComponent<UIWidget>().width = this.labelOne.width + 50;
		this.electirtySprite.gameObject.SetActive(false);
	}

	private GameObject AddChildByRes(GameObject parent, GameObject prefab, string resName)
	{
		if (parent.transform.FindChild(resName) != null)
		{
			GameObject gameObject = parent.transform.FindChild(resName).gameObject;
			gameObject.SetActive(true);
			return gameObject;
		}
		GameObject gameObject2 = NGUITools.AddChild(parent, prefab);
		gameObject2.name = resName;
		this.resPool.Add(gameObject2);
		return gameObject2;
	}

	private void OnUpdatIcon()
	{
		if (!AdjutantPanel.isCanOpen)
		{
			this.btns[10].GetComponent<UISprite>().spriteName = "fuguanhecheng";
		}
		else
		{
			this.btns[10].GetComponent<UISprite>().spriteName = "fuguanhecheng";
		}
	}

	private void CLearBtns()
	{
		for (int i = 0; i < this.btns.Length; i++)
		{
			if (this.btns[i])
			{
				this.btns[i].SetActive(false);
			}
		}
		if (this.addArmyAuto != null)
		{
			this.addArmyAuto.SetActive(false);
		}
	}

	public void HidePanel()
	{
		if (UIManager.curState != SenceState.InBuild)
		{
			this.OpenMainPanel();
			this.CLearBtns();
			this.armyFuncPanel.gameObject.SetActive(false);
			foreach (GameObject current in this.resPool)
			{
				if (current != null && current.activeSelf)
				{
					current.SetActive(false);
				}
			}
			if (this.Kapai)
			{
				this.Kapai.DesInPool();
			}
			if (this.ga)
			{
				this.ga.SetActive(false);
			}
		}
	}

	public void OnT_commandPanelCommondInfo()
	{
		ExpensePanelManage.ClearCache();
		this.timeCost = ResourceMgr.GetRmbNum((TimeTools.ConvertLongDateTime(AdjutantPanelData.endTime) - TimeTools.GetNowTimeSyncServerToDateTime()).TotalMilliseconds);
		ExpensePanelManage.strings.Add(LanguageManage.GetTextByKey("立即结束冷却", "others") + ":" + TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(AdjutantPanelData.endTime)));
		MessageBox.GetExpensePanel().Show(0, TimeTools.ConvertLongDateTime(AdjutantPanelData.endTime), delegate(bool a, int rmbNum)
		{
			if (a)
			{
				T_CommandPanelManage.CG_CommandHttp(ResourceMgr.GetRmbNum((TimeTools.ConvertLongDateTime(AdjutantPanelData.endTime) - TimeTools.GetNowTimeSyncServerToDateTime()).TotalMilliseconds));
			}
		});
	}

	public static void CG_CommandHttp(int Money)
	{
		CSAideMixCompleteUseMoney cSAideMixCompleteUseMoney = new CSAideMixCompleteUseMoney();
		cSAideMixCompleteUseMoney.money = Money;
		ClientMgr.GetNet().SendHttp(8010, cSAideMixCompleteUseMoney, null, null);
		AdjutantPanel.isCanOpen = true;
	}

	public void FuncArmyAuto(bool isAuto, int rmb)
	{
		if (isAuto)
		{
			string str = string.Empty;
			foreach (KeyValuePair<int, int> current in this.army_Coin)
			{
				str += string.Format("您购买{0}个{1}  花费金币：{2}\n\r", current.Value, UnitConst.GetInstance().soldierConst[current.Key].name, UnitConst.GetInstance().soldierConst[current.Key].lvInfos[HeroInfo.GetInstance().PlayerArmyData[current.Key].level].BuyCost * current.Value);
			}
			MessageBox.GetMessagePanel().ShowBtn(string.Empty, str, "确定", delegate
			{
				ArmyFuncHandler.CG_ArmsConfigureAuto(rmb, 1, null);
			}, "取消", null);
		}
	}

	private void OnEnable()
	{
		this.moveTittle.SetActive(false);
		DragMgr.ClickTerrSendMessage += new Action(this.HidePanel);
		if (ClientMgr.GetNet() != null)
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
	}

	private void NetDataHandler_DataChange(int opcodeCMD)
	{
		if (this.tower)
		{
			if (opcodeCMD == 10007 || opcodeCMD == 10116 || opcodeCMD == 10090)
			{
				this.CLearBtns();
				if (HeroInfo.GetInstance().IsArmyFuncingBuilding(this.tower.id))
				{
					this.btns[33].SetActive(true);
				}
				if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 15 && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime > 0L)
				{
					this.btns[33].SetActive(true);
				}
				if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 6 && HeroInfo.GetInstance().armyBuildingCDTime > 0L)
				{
					this.btns[18].SetActive(true);
					if (HeroInfo.GetInstance().playerGroupId != 0L)
					{
						this.btns[31].SetActive(true);
					}
				}
				else if (UnitConst.GetInstance().buildingConst[this.tower.index].secType == 21 && HeroInfo.GetInstance().airBuildingCDTime > 0L)
				{
					this.btns[18].SetActive(true);
					if (HeroInfo.GetInstance().playerGroupId != 0L)
					{
						this.btns[31].SetActive(true);
					}
				}
				else if (HeroInfo.GetInstance().BuildCD.Contains(this.tower.id))
				{
					this.btns[18].SetActive(true);
					if (HeroInfo.GetInstance().playerGroupId != 0L)
					{
						this.btns[31].SetActive(true);
					}
				}
				else if (UIManager.curState == SenceState.InBuild)
				{
					this.btns[31].SetActive(false);
					this.btns[18].SetActive(false);
					this.btns[14].SetActive(true);
					this.btns[15].SetActive(true);
				}
				else
				{
					this.RefreshBtn();
				}
				this.BtnGrid.Reposition();
			}
			return;
		}
		if (this.res)
		{
			if (opcodeCMD == 10007)
			{
				this.CLearBtns();
				if (HeroInfo.GetInstance().BuildCD.Contains((long)this.res.posIndex))
				{
					this.btns[18].SetActive(true);
					if (HeroInfo.GetInstance().playerGroupId != 0L)
					{
						this.btns[31].SetActive(true);
					}
				}
				else
				{
					this.btns[13].SetActive(true);
				}
			}
			return;
		}
		this.OpenMainPanel();
		this.HidePanel();
	}

	private void OnDisable()
	{
		if (this.SelRes)
		{
			this.SelRes.ChangeSelectState(Character.CharacterSelectStates.Idle);
		}
		if (this.tower)
		{
			this.tower.ChangeSelectState(Character.CharacterSelectStates.Idle);
		}
		this.OpenMainPanel();
		if (ClientMgr.GetNet() != null)
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.NetDataHandler_DataChange);
		}
		DragMgr.ClickTerrSendMessage -= new Action(this.HidePanel);
	}
}
