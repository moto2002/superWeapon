using msg;
using System;
using UnityEngine;
using UnityEngine.Events;

public class T_Res : Character
{
	public enum T_ResRemovingEnum
	{
		Normal = 1,
		InReoveTime
	}

	public int row;

	public int num;

	public int posIndex;

	public TimeTittleBtn timeTitle;

	public BoxCollider bodyCC;

	public TowerGrid[] towerRows;

	public Transform resGridPlane;

	private Body_Model UpdateModel;

	private DieBall DieEffect;

	private T_Res.T_ResRemovingEnum resRemovingState = T_Res.T_ResRemovingEnum.Normal;

	public TimeTittleBtn BuilindingCDInfo;

	private GameObject ShuMu;

	public bool IsCanSendUpdateMessage = true;

	public bool IsCanShowResInfoBtn = true;

	public override void Awake()
	{
		base.Awake();
		if (this.bodyCC == null)
		{
			this.bodyCC = base.gameObject.AddComponent<BoxCollider>();
		}
		if (this.resGridPlane == null)
		{
			this.resGridPlane = this.tr.Find("plane_Up");
		}
	}

	private void OnEnable()
	{
		BuildingHandler.HomeBuildingUp += new Action<int>(this.BuildingHandler_HomeBuildingUp);
		SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.CreateResByHomeBuilding);
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	public void OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType enum_GridPlane)
	{
		switch (enum_GridPlane)
		{
		case T_Tower.Enum_GridPlaneDisplayType.helpinfo:
			this.resGridPlane.localPosition = new Vector3(0f, -0f, 0f);
			this.resGridPlane.renderer.material.SetColor("_Emission", Color.white);
			this.resGridPlane.renderer.enabled = true;
			break;
		case T_Tower.Enum_GridPlaneDisplayType.nodisplay:
			this.resGridPlane.renderer.enabled = false;
			break;
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10007)
		{
			this.InitBuildingOrEndState();
		}
	}

	private void InitBuildingOrEndState()
	{
		if (SenceInfo.curMap.ResRemoveCDTime.ContainsKey((long)this.posIndex))
		{
			double num = TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(SenceInfo.curMap.ResRemoveCDTime[(long)this.posIndex]));
			if (num > 0.0)
			{
				LogManage.LogError(string.Format(" 目的{0}  现在{1} 算的插值{2} ", TimeTools.ConvertLongDateTime(SenceInfo.curMap.ResRemoveCDTime[(long)this.posIndex]), TimeTools.GetNowTimeSyncServerToDateTime(), num));
				this.InBuilding();
			}
			else
			{
				this.BuildingEnd();
			}
		}
		else
		{
			this.BuildingEnd();
		}
	}

	protected void InBuilding()
	{
		if (this.resRemovingState != T_Res.T_ResRemovingEnum.InReoveTime)
		{
			this.resRemovingState = T_Res.T_ResRemovingEnum.InReoveTime;
			this.InBuildingDoBehaviour();
		}
	}

	protected virtual void BuildingEnd()
	{
		this.BuildingEndDoBehaviour();
	}

	protected virtual void InBuildingDoBehaviour()
	{
		if (UIManager.curState != SenceState.Spy && UIManager.curState != SenceState.Attacking && UIManager.curState != SenceState.Visit && ResourcePanelManage.inst)
		{
			if (this.BuilindingCDInfo == null)
			{
				this.BuilindingCDInfo = ResourcePanelManage.inst.AddChildByTime(this.tr).GetComponent<TimeTittleBtn>();
				this.BuilindingCDInfo.tar = this.tr;
				this.BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(SenceInfo.curMap.ResRemoveCDTime[(long)this.posIndex]);
				this.BuilindingCDInfo.beginTime = TimeTools.ConvertLongDateTime(SenceInfo.curMap.ResRemoveCDTime[(long)this.posIndex]).AddSeconds((double)(UnitConst.GetInstance().buildingConst[this.index].lvInfos[0].BuildTime * -1));
				this.BuilindingCDInfo.SetUpdatingEum(0);
				this.BuilindingCDInfo.CallBack = new Action(this.SendResRemoveComplete);
			}
			AudioManage.inst.PlayAuido("treemove", false);
		}
	}

	protected virtual void BuildingEndDoBehaviour()
	{
		if (this.resRemovingState == T_Res.T_ResRemovingEnum.InReoveTime)
		{
			this.resRemovingState = T_Res.T_ResRemovingEnum.Normal;
		}
	}

	private void BuildingHandler_HomeBuildingUp(int LV)
	{
		this.CreateResByHomeBuilding();
	}

	private void OnDisable()
	{
		BuildingHandler.HomeBuildingUp -= new Action<int>(this.BuildingHandler_HomeBuildingUp);
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
		SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.CreateResByHomeBuilding);
	}

	private void CreateResByHomeBuilding()
	{
		if (UIManager.curState != SenceState.Home)
		{
			if (UIManager.curState != SenceState.InBuild)
			{
				return;
			}
		}
		try
		{
			if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[((this.lv != 0) ? this.lv : 1) - 1].needCommandLevel <= HeroInfo.GetInstance().PlayerCommondLv)
			{
				if (!this.ModelBody || !this.ModelBody.ga.name.Contains("_die"))
				{
					if (this.ModelBody)
					{
						this.ModelBody.DesInsInPool();
					}
					this.ModelBody = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().buildingConst[this.index].bodyID + "_die", this.tr);
				}
			}
		}
		catch (Exception ex)
		{
			LogManage.Log(ex.ToString() + "Res Index：" + this.index);
		}
	}

	private void SetGameObjectActive()
	{
		if (this.ShuMu)
		{
			this.ShuMu.SetActive(!this.ShuMu.activeSelf);
		}
	}

	private void CreatBody()
	{
		string bodyID = UnitConst.GetInstance().buildingConst[this.index].bodyID;
		int terrStyle = SenceInfo.terrainTypeList[SenceInfo.curMap.terrType].terrStyle;
		this.ModelBody = PoolManage.Ins.GetModelByBundleByName(bodyID, this.tr);
		if (this.ModelBody != null)
		{
			BoxCollider component = this.ModelBody.GetComponent<BoxCollider>();
			if (component != null)
			{
				this.bodyCC.center = component.center + new Vector3(0f, 1f, 0f);
				this.bodyCC.size = component.size;
			}
			this.bodyCC.isTrigger = true;
		}
		else
		{
			LogManage.Log("没有找到该资源  resName: " + bodyID);
		}
	}

	public void SetInfo()
	{
		this.size = UnitConst.GetInstance().buildingConst[this.index].size;
		this.bodyCC.center = new Vector3(0f, UnitConst.GetInstance().buildingConst[this.index].hight * 0.5f, 0f);
		this.bodyCC.size = new Vector3((float)this.size, UnitConst.GetInstance().buildingConst[this.index].hight, (float)this.size);
		this.roleType = Enum_RoleType.Res;
		this.CreatTowerGrid();
		this.CreatBody();
		this.CreateResByHomeBuilding();
		this.InitBuildingOrEndState();
		if (this.resGridPlane)
		{
			this.resGridPlane.renderer.enabled = false;
		}
		if (!SenceInfo.curMap.IsMyMap)
		{
			this.ga.layer = 2;
		}
		else
		{
			this.ga.layer = 0;
		}
		this.resGridPlane.gameObject.layer = 17;
	}

	private void CreatTowerGrid()
	{
		this.towerRows = new TowerGrid[this.size * this.size];
		if (this.resGridPlane)
		{
			this.resGridPlane.localScale = Vector2.one * (float)this.size;
			this.resGridPlane.renderer.material.mainTextureScale = Vector2.one;
		}
		else
		{
			LogManage.Log("resGridPlane is null");
		}
		for (int i = 0; i < UnitConst.GetInstance().buildingConst[this.index].towerGrids.Length; i++)
		{
			this.towerRows[i] = new TowerGrid();
			this.towerRows[i].numX = UnitConst.GetInstance().buildingConst[this.index].towerGrids[i].numX;
			this.towerRows[i].numZ = UnitConst.GetInstance().buildingConst[this.index].towerGrids[i].numZ;
		}
	}

	public void EditeMapGrid(bool add)
	{
		for (int i = 0; i < this.towerRows.Length; i++)
		{
			int num = this.row + this.towerRows[i].numX;
			int num2 = this.num + this.towerRows[i].numZ;
			if (add)
			{
				if (num > 0 && num < SenceManager.inst.arrayX && num2 > 0 && num2 < SenceManager.inst.arrayY)
				{
					MapGridManager.AddMapGrid(Mathf.RoundToInt((float)(this.row + this.towerRows[i].numX)), Mathf.RoundToInt((float)(this.num + this.towerRows[i].numZ)));
				}
			}
			else if (num > 0 && num < SenceManager.inst.arrayX && num2 > 0 && num2 < SenceManager.inst.arrayY)
			{
				MapGridManager.RemoveMapGrid(Mathf.RoundToInt((float)(this.row + this.towerRows[i].numX)), Mathf.RoundToInt((float)(this.num + this.towerRows[i].numZ)));
			}
		}
	}

	public override void MouseDown()
	{
		SenceManager.inst.sameTower = false;
		DragMgr.inst.NewMouseDown();
	}

	public override void MouseUp()
	{
		if (DragMgr.inst.BtnInUse)
		{
			return;
		}
		SenceManager.inst.sameTower = false;
		if (DragMgr.inst.JudgeDraged())
		{
			return;
		}
		SenceState curState = UIManager.curState;
		if (curState == SenceState.Home)
		{
			if (DragMgr.inst.buildDraging)
			{
				SenceManager.inst.RebackTower();
			}
			if (this.resRemovingState == T_Res.T_ResRemovingEnum.Normal)
			{
				T_CommandPanelManage._instance.ShowResBtn(this);
				T_CommandPanelManage._instance.HideMainPanel();
			}
			if (this.resRemovingState == T_Res.T_ResRemovingEnum.InReoveTime)
			{
				T_CommandPanelManage._instance.ShowResRemoveBtn(this);
				T_CommandPanelManage._instance.HideMainPanel();
			}
		}
	}

	public override void MouseDrag()
	{
	}

	public void SetLogoFlash()
	{
		if (this.ModelBody != null)
		{
			Renderer[] componentsInChildren = this.ModelBody.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].gameObject.name != "xuanzhong")
				{
					TweenColor tweenColor = componentsInChildren[i].GetComponent<TweenColor>();
					if (tweenColor == null)
					{
						tweenColor = componentsInChildren[i].gameObject.AddComponent<TweenColor>();
					}
					else
					{
						tweenColor.enabled = true;
					}
					tweenColor.from = Color.white;
					tweenColor.to = Color.grey;
					tweenColor.style = UITweener.Style.PingPong;
					tweenColor.duration = 1f;
				}
			}
		}
	}

	public void RebackShader()
	{
		if (this.ModelBody != null)
		{
			Renderer[] componentsInChildren = this.ModelBody.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				TweenColor component = componentsInChildren[i].GetComponent<TweenColor>();
				if (component != null)
				{
					component.enabled = false;
				}
				componentsInChildren[i].renderer.material.color = new Color(0.5882353f, 0.5882353f, 0.5882353f);
			}
		}
	}

	public override Vector3 GetVPos(T_TankAbstract tank)
	{
		return Vector3.zero;
	}

	public void SenndResComplete(int money, bool NoBuyTime = false)
	{
		if (this.resRemovingState == T_Res.T_ResRemovingEnum.InReoveTime && this.IsCanSendUpdateMessage && (NoBuyTime || SenceInfo.curMap.ResRemoveCDTime.ContainsKey((long)this.posIndex)))
		{
			this.IsCanSendUpdateMessage = false;
			CSBuildingRemoveEnd cSBuildingRemoveEnd = new CSBuildingRemoveEnd();
			cSBuildingRemoveEnd.index = this.posIndex;
			cSBuildingRemoveEnd.islandId = SenceInfo.curMap.ID;
			cSBuildingRemoveEnd.money = money;
			ClientMgr.GetNet().SendHttp(2014, cSBuildingRemoveEnd, new DataHandler.OpcodeHandler(BuildingHandler.GC_BuildingRemoveEnd), null);
		}
	}

	public void SendResRemoveComplete()
	{
		this.SenndResComplete(0, true);
	}
}
