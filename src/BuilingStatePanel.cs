using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuilingStatePanel : MonoBehaviour
{
	public static BuilingStatePanel inst;

	public Transform coinTrans;

	public Transform oilTrans;

	public Transform steelTrans;

	public Transform rareTrans;

	public Transform resource;

	public UILabel playerName;

	public UILabel playerLevel;

	public UILabel playerModel;

	public UISprite Exp;

	public UISprite electry;

	public Transform table;

	public Transform WallLineBtn;

	public Transform WallRotateBtn;

	public UITable grid;

	public GameObject showGame;

	public GameObject BuildingFinishBtn;

	public UISlider Camera_DisSlider;

	public UILabel Camera_DisLabel;

	public int ElectricityNum;

	private ShowTimeTip time;

	private List<KVStruct> allArmyCDTime
	{
		get
		{
			return (from a in HeroInfo.GetInstance().PlayerArmy_LandDataCDTime
			orderby a.value
			select a).ToList<KVStruct>();
		}
	}

	private List<KVStruct> allAirCDTime
	{
		get
		{
			return (from a in HeroInfo.GetInstance().PlayerArmy_AirDataCDTime
			orderby a.value
			select a).ToList<KVStruct>();
		}
	}

	public void OnDestroy()
	{
		BuilingStatePanel.inst = null;
	}

	public void Awake()
	{
		BuilingStatePanel.inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.Building_WallLine, new EventManager.VoidDelegate(BuilingStatePanel.inst.WallLineClick));
		EventManager.Instance.AddEvent(EventManager.EventType.Building_WallRatate, new EventManager.VoidDelegate(BuilingStatePanel.inst.WallRotateClick));
		EventManager.Instance.AddEvent(EventManager.EventType.Building_EndCD, new EventManager.VoidDelegate(this.EndCD));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_CloseBuildingMovingOrInBuilding, new EventManager.VoidDelegate(this.CloseBuildingMovingOrInBuilding));
		this.WallLineBtn.gameObject.SetActive(false);
		this.WallRotateBtn.gameObject.SetActive(false);
	}

	private void CloseBuildingMovingOrInBuilding(GameObject ga)
	{
		if (SenceManager.inst.rotate_time > 0f)
		{
			return;
		}
		DragMgr.inst.MouseUp(MouseCommonType.canncel, Vector2.zero, null);
		SenceManager.inst.DesGetWallLatelyEffect();
		if (CameraControl.inst)
		{
			CameraControl.inst.ChangeCameraBuildingState(false);
		}
	}

	public void WallLineClick(GameObject ga)
	{
		DragMgr.inst.MouseUp(MouseCommonType.canncel, Vector2.zero, null);
		if (!SenceManager.inst.WallLineChoose)
		{
			SenceManager.inst.WallLineChoose = true;
		}
		else
		{
			SenceManager.inst.WallLineChoose = false;
		}
		if (!SenceManager.inst.WallLineChoose)
		{
			SenceManager.inst.DesGetWallLatelyEffect();
		}
		BuilingStatePanel.inst.WallLineBtn.FindChild("Light").gameObject.SetActive(SenceManager.inst.WallLineChoose);
		BuilingStatePanel.inst.WallRotateBtn.gameObject.SetActive(SenceManager.inst.WallLineChoose);
	}

	public void WallRotateClick(GameObject ga)
	{
		SenceManager.inst.WallRotate();
	}

	public void OnEnable()
	{
		if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.Normal)
		{
			if (UIManager.curState == SenceState.WatchResIsland)
			{
				FuncUIManager.inst.OpenFuncUI("WatchPanel", SenceType.Island);
				FuncUIManager.inst.ClearFuncPanelList_ForQueue();
			}
			else
			{
				if (T_CommandPanelManage._instance)
				{
					T_CommandPanelManage._instance.OpenMainPanel();
				}
				if (MainUIPanelManage._instance)
				{
					MainUIPanelManage._instance.OpenPanelMian();
				}
				FuncUIManager.inst.OpenFuncUI("MainUIPanel", SenceType.Island);
				FuncUIManager.inst.ClearFuncPanelList_ForQueue();
			}
			return;
		}
		this.ShowRefresh();
		this.showBuildingTime();
		this.Powerhouse();
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10039 || opcodeCMD == 10003)
		{
			this.ShowRefresh();
		}
		if (opcodeCMD == 10021 || opcodeCMD == 10091 || opcodeCMD == 10007)
		{
			this.showBuildingTime();
		}
	}

	public void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	public void showBuildingTime()
	{
	}

	public void ShowRefresh()
	{
		if (HeroInfo.GetInstance().playerRes.maxCoin > 0)
		{
			this.table.transform.localPosition = new Vector3(-202.1f, 0f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxOil > 0)
		{
			this.table.transform.localPosition = new Vector3(-518.45f, 0f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxSteel > 0)
		{
			this.steelTrans.gameObject.SetActive(true);
			this.table.transform.localPosition = new Vector3(-606.1f, 0f, 0f);
		}
		if (HeroInfo.GetInstance().playerRes.maxRareEarth > 0)
		{
			this.rareTrans.gameObject.SetActive(true);
			this.table.transform.localPosition = new Vector3(-678.6f, 0f, 0f);
		}
	}

	public void Powerhouse()
	{
		MainUIPanelManage.power = 0;
		MainUIPanelManage.electricUse = 0;
		this.ElectricityNum = 0;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i].index == 62)
			{
				this.ElectricityNum++;
				MainUIPanelManage.power += UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].outputLimit[ResType.电力];
			}
			else
			{
				MainUIPanelManage.electricUse += UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].electricUse;
			}
		}
		if (UIManager.curState == SenceState.Attacking && FightPanelManager.inst)
		{
			MainUIPanelManage.Powerconsumption(MainUIPanelManage.power, MainUIPanelManage.electricUse, FightPanelManager.inst.powerelectricity);
		}
		if (UIManager.curState == SenceState.Home && MainUIPanelManage._instance)
		{
			MainUIPanelManage.power_Home = MainUIPanelManage.power;
			MainUIPanelManage.electricUse_Home = MainUIPanelManage.electricUse;
			MainUIPanelManage.Powerconsumption(MainUIPanelManage.power, MainUIPanelManage.electricUse, this.electry.gameObject);
		}
		if (UIManager.curState == SenceState.Spy && SpyPanelManager.inst && SpyPanelManager.inst.powerTiao)
		{
			MainUIPanelManage.Powerconsumption(MainUIPanelManage.power, MainUIPanelManage.electricUse, SpyPanelManager.inst.powerTiao);
		}
	}

	private void EndCD(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		this.time = ga.GetComponent<ShowTimeTip>();
		CalcMoneyHandler.CSCalcMoney(1, this.time.cdType, this.time.posIndex, this.time.id, this.time.itemid, 0, new Action<bool, int>(this.CalcMoneyCallBack));
	}

	private void CalcMoneyCallBack(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				ShopPanelManage.ShowHelp_NoRMB(null, null);
				return;
			}
			if (this.time.btnType == 1)
			{
				this.time.tar.SenndBuildComplete(money, "点  立即结束", false);
			}
			else if (this.time.btnType == 2)
			{
				this.time.res.SenndResComplete(money, false);
			}
			else if (this.time.btnType == 3)
			{
				ArmyHandler.CG_CSArmyLevelUpEnd(1, null, new int[]
				{
					(int)this.time.id
				});
			}
			else if (this.time.btnType == 4)
			{
				ArmyHandler.CG_CSArmyLevelUpEnd(1, null, new int[]
				{
					(int)this.time.id
				});
			}
			else if (this.time.btnType == 5)
			{
				TechHandler.CG_CSTechUpEnd(HeroInfo.GetInstance().GetUpdatingTech.itemid, money, null);
			}
		}
	}
}
