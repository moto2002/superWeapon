using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
	public static UIManager inst;

	public bool uiInUse;

	public static SenceState curState = SenceState.None;

	public EnemyManage enemyPanel;

	public EnemyScorePanel enemyScore;

	public UITexture DragCameraTexture;

	public bool NoSpyDirectAtt;

	public UITexture Geo_BMP;

	public Camera uiCamera;

	public Camera _3DUICamera;

	private bool isFirst = true;

	public GameObject senderBtn
	{
		get
		{
			if (base.transform.Find("senderBtn0"))
			{
				return base.transform.Find("senderBtn0").gameObject;
			}
			return null;
		}
	}

	public void OnDestroy()
	{
		UIManager.inst = null;
	}

	private void Awake()
	{
		UIManager.inst = this;
		GameTools.DontDestroyOnLoad(base.gameObject);
	}

	private void OnEnable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
		FightHundler.OnFighting += new Action(this.PlayerHandle_OnFighting);
		CameraInitMove.inst.HomeOnMoved += new Action(this.CameraInitMove_HomeOnMoved);
		CameraInitMove.inst.AttackingOnMoved += new Action(this.CameraInitMove_AttackingOnMoved);
		CameraInitMove.inst.SpyOnMoved += new Action(this.CameraInitMove_SpyOnMoved);
	}

	private void PlayerHandle_OnFighting()
	{
		if (!Loading.IsRefreshSence)
		{
			this.CameraInitMove_AttackingOnMoved();
		}
	}

	private void CameraInitMove_SpyOnMoved()
	{
		this.ResetSenceState(UIManager.curState);
		FuncUIManager.inst.OpenFuncUI_NoQueue("ResourcePanel", SenceType.Other);
	}

	private void CameraInitMove_HomeOnMoved()
	{
		AudioManage.inst.PlayAudioBackground("home", true);
		if (NewbieGuidePanel.isEnemyAttck)
		{
			this._3DUICamera.gameObject.SetActive(true);
		}
		this.ResetSenceState(UIManager.curState);
		FuncUIManager.inst.OpenFuncUI_NoQueue("ResourcePanel", SenceType.Other);
		if (this.isFirst)
		{
			this.isFirst = false;
			ClientMgr.GetNet().SorcketConnect();
		}
	}

	public void CameraInitMove_AttackingOnMoved()
	{
		FuncUIManager.inst.ClearAllUIPanel();
		AudioManage.inst.PlayAudioBackground("battle", true);
		CoroutineInstance.DoJob(this.StartFight());
	}

	[DebuggerHidden]
	public IEnumerator StartFight()
	{
		return new UIManager.<StartFight>c__Iterator96();
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		if (!this.uiCamera.gameObject.activeSelf)
		{
			this.uiCamera.gameObject.SetActive(true);
		}
		if (!this._3DUICamera.gameObject.activeSelf && NewbieGuidePanel.isEnemyAttck)
		{
			this._3DUICamera.gameObject.SetActive(true);
		}
		if (UIManager.curState == SenceState.Visit || UIManager.curState == SenceState.WatchResIsland || UIManager.curState == SenceState.WatchVideo)
		{
			this.ResetSenceState(UIManager.curState);
		}
		else if (UIManager.curState == SenceState.Attacking)
		{
			this.ResetSenceState(UIManager.curState);
			CameraInitMove.inst.CameraDoMove();
		}
	}

	private void OnDisbale()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
		FightHundler.OnFighting -= new Action(this.PlayerHandle_OnFighting);
		if (CameraInitMove.inst)
		{
			CameraInitMove.inst.HomeOnMoved -= new Action(this.CameraInitMove_HomeOnMoved);
			CameraInitMove.inst.AttackingOnMoved -= new Action(this.CameraInitMove_AttackingOnMoved);
			CameraInitMove.inst.SpyOnMoved -= new Action(this.CameraInitMove_SpyOnMoved);
		}
	}

	public void ResetSenceState(SenceState _state)
	{
		UIManager.curState = _state;
		if (SettlementManager.inst)
		{
			SettlementManager.inst.gameObject.SetActive(false);
		}
		switch (_state)
		{
		case SenceState.Home:
			if (SenceManager.inst != null && SenceManager.inst.curTower != null)
			{
				SenceManager.inst.curTower.TowerInfoShow(false);
			}
			if (ResourcePanelManage.inst)
			{
				ResourcePanelManage.inst.gameObject.SetActive(true);
			}
			return;
		case SenceState.Spy:
		{
			GameObject gameObject = FuncUIManager.inst.OpenFuncUI("SpyPanel", SenceType.Island);
			AudioManage.inst.PlayAudioBackground("home", true);
			if (ActivityManager.GetIns().IsFromAct)
			{
			}
			if (SenceManager.inst.curTower != null)
			{
				SenceManager.inst.curTower.TowerInfoShow(false);
			}
			SenceManager.inst.ShowLandArray(true, false, false);
			return;
		}
		case SenceState.Attacking:
			if (NewbieGuidePanel.isEnemyAttck)
			{
				LogManage.Log("进入战斗啦！！！");
				SenceInfo.CurReportData = null;
				CameraInitMove.inst.isMoved = false;
				if (SenceManager.inst.curTower != null)
				{
					SenceManager.inst.curTower.TowerInfoShow(false);
				}
				InfoPanel.inst.DelletAllProInfo();
			}
			return;
		case SenceState.InBuild:
			return;
		case SenceState.WatchResIsland:
			break;
		case SenceState.WatchVideo:
			if (SenceManager.inst.curTower != null)
			{
				SenceManager.inst.curTower.TowerInfoShow(false);
			}
			return;
		default:
			if (_state != SenceState.Visit)
			{
				return;
			}
			break;
		}
		FuncUIManager.inst.OpenFuncUI("WatchPanel", SenceType.Island);
		FuncUIManager.inst.OpenFuncUI_NoQueue("T_CommandPanel");
		if (SenceManager.inst.curTower != null)
		{
			SenceManager.inst.curTower.TowerInfoShow(false);
		}
	}

	public void UIInUsed(bool b)
	{
		if (DragMgr.inst)
		{
			DragMgr.inst.BtnInUse = b;
		}
	}

	public static GameObject AddItemToTable(UITable table, string name)
	{
		if (table == null || table.ItemModel == null)
		{
			return null;
		}
		GameObject itemModel = table.ItemModel;
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(itemModel, Vector3.zero, Quaternion.identity);
		gameObject.transform.parent = table.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		gameObject.name = name;
		return gameObject;
	}
}
