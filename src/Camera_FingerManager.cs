using DicForUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FingerManager : MonoBehaviour
{
	public Camera cam;

	public Transform cameraPos;

	public Vector3 centerPos;

	public float turnAngleDelta;

	public float turnAngle;

	public static Camera_FingerManager inst;

	public static bool newbiLock;

	[HideInInspector]
	public bool isDraging;

	private bool yindao;

	private float yindao_time;

	public DragCamera dragCamera;

	private Ray ray;

	private RaycastHit hit;

	private Character SelingCharInIsland;

	private Character SeledCharInIsland;

	public Character SelingDragCharInIsland;

	private T_Island SelingIslandInWorldMap;

	[HideInInspector]
	public bool isUp = true;

	[HideInInspector]
	public bool isDragedSingleFinger;

	private float sizePeriphery;

	private List<T_Island> AllIsland = new List<T_Island>();

	public static bool YinDaoDianji;

	public float time;

	public static bool ismove;

	private float theta;

	private float shazhe;

	private bool isFist = true;

	private float r;

	private Vector3 campos;

	public PinchRecognizer pinch;

	public TwistRecognizer twist;

	public FingerDownDetector fingerDown;

	public FingerUpDetector fingerUp;

	public LongPressRecognizer fingerLongPress;

	public DragRecognizer fingerDrag;

	[HideInInspector]
	public bool Rotating;

	[HideInInspector]
	public bool Pinching;

	private Vector3 HitPoint
	{
		get
		{
			return this.hit.point;
		}
	}

	public void OnDestroy()
	{
		Camera_FingerManager.inst = null;
	}

	private void Awake()
	{
		Camera_FingerManager.inst = this;
		this.cameraPos = base.transform;
		if (this.pinch)
		{
			this.pinch.OnGesture += new GestureRecognizer<PinchGesture>.GestureEventHandler(this.pinch_OnGesture);
		}
		if (this.fingerDown)
		{
			this.fingerDown.OnFingerDown += new FingerEventDetector<FingerDownEvent>.FingerEventHandler(this.fingerDown_OnFingerDown);
		}
		if (this.fingerUp)
		{
			this.fingerUp.OnFingerUp += new FingerEventDetector<FingerUpEvent>.FingerEventHandler(this.fingerUp_OnFingerUp);
		}
		if (this.fingerLongPress)
		{
			this.fingerLongPress.OnGesture += new GestureRecognizer<LongPressGesture>.GestureEventHandler(this.fingerLongPress_OnGesture);
		}
		if (this.fingerDrag)
		{
			this.fingerDrag.OnGesture += new GestureRecognizer<DragGesture>.GestureEventHandler(this.fingerDrag_OnGesture);
		}
	}

	public void Skill_Excursion_pos(float move_x, float move_y)
	{
		if (NewbieGuidePanel._instance.ga_Self.activeSelf)
		{
			return;
		}
		if (this.yindao)
		{
			return;
		}
		if (!Camera_FingerManager.newbiLock)
		{
			CameraControl.inst.isDragAuto = true;
			CameraControl.inst.TargetPos = new Vector3(-move_x, -move_y, 0f);
		}
	}

	private void Update()
	{
		if (FightPanelManager.inst && this.yindao)
		{
			this.yindao_time -= Time.deltaTime;
			if (this.yindao_time <= 0f)
			{
				this.yindao = false;
			}
		}
	}

	private void fingerDrag_OnGesture(DragGesture gesture)
	{
		if (this.Pinching || this.Rotating)
		{
			return;
		}
		if (!this.SelingDragCharInIsland)
		{
			this.isDragedSingleFinger = true;
		}
		switch (Loading.senceType)
		{
		case SenceType.Island:
			if (this.IsNotCanDragOrClickTerrInIsland())
			{
				return;
			}
			if (gesture.Phase == ContinuousGesturePhase.Started)
			{
				this.isDraging = true;
				if (!this.SelingDragCharInIsland)
				{
					CameraControl.inst.mTargetPos = new Vector3(CameraControl.inst.Tr.localPosition.x, 40.79f, CameraControl.inst.Tr.localPosition.z);
					CameraControl.inst.openDragCameraAndInertia = true;
				}
				else
				{
					CameraControl.inst.openDragCameraAndInertia = false;
				}
			}
			if (gesture.Phase == ContinuousGesturePhase.Ended)
			{
				this.isDraging = false;
			}
			if (gesture.Phase == ContinuousGesturePhase.Updated)
			{
				if (this.Pinching || this.Rotating || !this.isDraging)
				{
					return;
				}
				if (this.SelingDragCharInIsland)
				{
					this.SelingDragCharInIsland.MouseDrag();
					return;
				}
				DragMgr.inst.dragX = gesture.DeltaMove.x;
				DragMgr.inst.dragY = gesture.DeltaMove.y;
				CameraControl.inst.TargetPos = gesture.DeltaMove * -1f;
			}
			break;
		case SenceType.WorldMap:
			if (this.IsNotCanDragOrClickTerrInWorldMap())
			{
				return;
			}
			if (gesture.Phase == ContinuousGesturePhase.Started)
			{
				this.isDraging = true;
			}
			if (gesture.Phase == ContinuousGesturePhase.Ended)
			{
				this.isDraging = false;
			}
			if (gesture.Phase == ContinuousGesturePhase.Updated)
			{
				if (NewbieGuidePanel._instance.ga_Self.activeSelf)
				{
					return;
				}
				if (WMap_DragManager.inst == null)
				{
					return;
				}
				if (WMap_DragManager.inst.btnInUse || WorldMapManager.inst.nBattlPanel.activeInHierarchy || WorldMapManager.inst.GuidPanel.activeSelf || (CommunicationPanel._Inst && CommunicationPanel._Inst.communication.activeSelf))
				{
					return;
				}
				WMap_DragManager.inst.Drag(-gesture.DeltaMove.y, gesture.DeltaMove.x);
				if (WMap_DragManager.inst.JudgeIsDrag(-gesture.DeltaMove.y, gesture.DeltaMove.x) && TipsManager.inst)
				{
					TipsManager.inst.CloseAllTips();
				}
			}
			break;
		}
	}

	private void fingerLongPress_OnGesture(LongPressGesture gesture)
	{
		if (SenceManager.inst.rotate_time > 0f)
		{
			return;
		}
		if (this.Pinching || this.Rotating || this.isDraging)
		{
			return;
		}
		switch (Loading.senceType)
		{
		case SenceType.Island:
			if (this.IsNotCanDragOrClickTerrInIsland())
			{
				return;
			}
			if (this.SelingCharInIsland && this.SelingCharInIsland.roleType == Enum_RoleType.tower && (UIManager.curState == SenceState.Home || UIManager.curState == SenceState.InBuild || UIManager.curState == SenceState.WatchResIsland))
			{
				SenceManager.inst.SetAllBuildingDown();
				CameraControl.inst.ChangeCameraBuildingState(true);
				if (this.SelingDragCharInIsland != this.SelingCharInIsland)
				{
					Camera_FingerManager.inst.GetDragCamera(this.SelingCharInIsland.tr);
				}
				this.SelingDragCharInIsland = this.SelingCharInIsland;
				if (this.SelingCharInIsland.GetComponent<T_Tower>().index == 90)
				{
					this.SelingCharInIsland.GetComponent<T_Tower>().Walltower_list.Clear();
					this.SelingCharInIsland.GetComponent<T_Tower>().Walltower_list = SenceManager.inst.GetTowerByLine(this.SelingCharInIsland.GetComponent<T_Tower>());
					BuilingStatePanel.inst.WallLineBtn.gameObject.SetActive(SenceManager.inst.WallLineChoose);
					BuilingStatePanel.inst.WallLineBtn.FindChild("Light").gameObject.SetActive(SenceManager.inst.WallLineChoose);
					BuilingStatePanel.inst.WallRotateBtn.gameObject.SetActive(SenceManager.inst.WallLineChoose);
				}
				else
				{
					BuilingStatePanel.inst.WallLineBtn.gameObject.SetActive(false);
					BuilingStatePanel.inst.WallRotateBtn.gameObject.SetActive(false);
				}
			}
			if (gesture.PreviousState == GestureRecognitionState.InProgress && UIManager.curState == SenceState.Attacking && this.HitPoint.x < 57f && this.HitPoint.x > -5f && this.HitPoint.z < 57f && this.HitPoint.z > -5f && (this.HitPoint.x <= 50f || this.HitPoint.z <= 50f))
			{
				if (FightPanelManager.inst != null && FightPanelManager.inst.curSelectUIItem != null)
				{
					if (FightPanelManager.inst.curSelectUIItem.GetComponent<SoldierUIITem>())
					{
						if (!this.IsCanSendSolider(this.HitPoint))
						{
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("此处不可放置兵种", "others"), HUDTextTool.TextUITypeEnum.Num5);
							return;
						}
						SoldierUIITem soldierUIITem = FightPanelManager.inst.curSelectUIItem as SoldierUIITem;
						if (soldierUIITem.soliderType == SoliderType.Normal)
						{
							armyData armyToInBattle = soldierUIITem.GetArmyToInBattle();
							if (soldierUIITem.soliderNum <= 0)
							{
								FightPanelManager.inst.CurSelectUIItem = null;
							}
							Container.CreateContainer(this.HitPoint, armyToInBattle.buildingID, SenceManager.inst.SoldierId, armyToInBattle.index, armyToInBattle.lv, true, false, CommanderType.None);
						}
						else
						{
							soldierUIITem.soliderNum = 0;
							soldierUIITem.ResetArmyNum();
							if (soldierUIITem.soliderNum <= 0)
							{
								FightPanelManager.inst.CurSelectUIItem = null;
							}
							Container.CreateCommander(this.HitPoint, HeroInfo.GetInstance().PlayerCommandoBuildingID, HeroInfo.GetInstance().Commando_Fight.id, HeroInfo.GetInstance().Commando_Fight.index, HeroInfo.GetInstance().Commando_Fight.level, HeroInfo.GetInstance().Commando_Fight.star, HeroInfo.GetInstance().Commando_Fight.skillLevel, true);
						}
					}
				}
				else
				{
					DragMgr.inst.MouseUp(MouseCommonType.forceMove, this.HitPoint, null);
					if (Camera_FingerManager.YinDaoDianji)
					{
						Camera_FingerManager.YinDaoDianji = false;
						HUDTextTool.inst.NextLuaCall("新手引导点击地面回馈", new object[0]);
					}
				}
			}
			break;
		case SenceType.WorldMap:
			if (this.IsNotCanDragOrClickTerrInWorldMap())
			{
				return;
			}
			break;
		}
	}

	public bool IsNotCanDragOrClickTerrInIsland()
	{
		return (!Camera_FingerManager.YinDaoDianji && (DragMgr.inst.BtnInUse || (CommunicationPanel._Inst && CommunicationPanel._Inst.communication.activeSelf))) || Camera_FingerManager.newbiLock;
	}

	public bool IsNotCanDragOrClickTerrInWorldMap()
	{
		return (CommunicationPanel._Inst && CommunicationPanel._Inst.communication.activeSelf) || Camera_FingerManager.newbiLock;
	}

	public void GetDragCamera(Transform tr)
	{
		GameTools.GetCompentIfNoAddOne<DragCameraMaster>(tr.gameObject);
		GameObject gameObject = new GameObject("DragCamera");
		gameObject.AddComponent<Camera>();
		gameObject.GetComponent<Camera>().rect = new Rect(0f, 1f, 0f, 0f);
		gameObject.AddComponent<DragCamera>();
		gameObject.GetComponent<DragCamera>().Parent = tr;
		tr.GetComponent<DragCameraMaster>().dragCamera = gameObject.transform;
		if (this.dragCamera != null)
		{
			UnityEngine.Object.Destroy(this.dragCamera.gameObject);
		}
		this.dragCamera = gameObject.GetComponent<DragCamera>();
	}

	public void DestroyDragCamera()
	{
		if (this.dragCamera != null)
		{
			UnityEngine.Object.Destroy(this.dragCamera.gameObject);
		}
	}

	private void fingerUp_OnFingerUp(FingerUpEvent eventData)
	{
		if (SenceManager.inst.rotate_time > 0f)
		{
			return;
		}
		if (!this.isUp || this.isDragedSingleFinger)
		{
			return;
		}
		switch (Loading.senceType)
		{
		case SenceType.Island:
			if (this.IsNotCanDragOrClickTerrInIsland())
			{
				return;
			}
			if (this.SelingCharInIsland)
			{
				if (!this.SelingCharInIsland.GetComponent<T_CommanderHome>())
				{
					this.SelingCharInIsland.MouseUp();
				}
				if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding && this.SelingCharInIsland.Tower)
				{
					SenceManager.inst.SetAllBuildingDown();
					if (this.SelingCharInIsland.Tower.index == 90)
					{
						this.SelingCharInIsland.GetComponent<T_Tower>().Walltower_list.Clear();
						this.SelingCharInIsland.GetComponent<T_Tower>().Walltower_list = SenceManager.inst.GetTowerByLine(this.SelingCharInIsland.GetComponent<T_Tower>());
						BuilingStatePanel.inst.WallLineBtn.gameObject.SetActive(true);
						BuilingStatePanel.inst.WallLineBtn.FindChild("Light").gameObject.SetActive(SenceManager.inst.WallLineChoose);
						BuilingStatePanel.inst.WallRotateBtn.gameObject.SetActive(SenceManager.inst.WallLineChoose);
					}
					else
					{
						SenceManager.inst.DesGetWallLatelyEffect();
						SenceManager.inst.DesGetWallLatelyEffect();
						BuilingStatePanel.inst.WallLineBtn.gameObject.SetActive(false);
						BuilingStatePanel.inst.WallRotateBtn.gameObject.SetActive(false);
					}
				}
				if (Camera_FingerManager.YinDaoDianji)
				{
					Camera_FingerManager.YinDaoDianji = false;
					HUDTextTool.inst.NextLuaCall("新手引导点击地面回馈", new object[0]);
				}
				return;
			}
			if (FightPanelManager.inst == null)
			{
				DragMgr.inst.MouseUp(MouseCommonType.canncel, this.HitPoint, null);
				if (Camera_FingerManager.YinDaoDianji)
				{
					Camera_FingerManager.YinDaoDianji = false;
					HUDTextTool.inst.NextLuaCall("新手引导点击地面回馈", new object[0]);
				}
				return;
			}
			if (UIManager.curState == SenceState.Attacking)
			{
				if (this.HitPoint.x < 57f && this.HitPoint.x > -5f && this.HitPoint.z < 57f && this.HitPoint.z > -5f && (this.HitPoint.x <= 50f || this.HitPoint.z <= 50f))
				{
					if (FightPanelManager.supperSkillReady || FightPanelManager.inst.isSpColor)
					{
						DragMgr.inst.MouseUp(MouseCommonType.supperSkill, this.HitPoint, null);
						if (Camera_FingerManager.YinDaoDianji)
						{
							Camera_FingerManager.YinDaoDianji = false;
							HUDTextTool.inst.NextLuaCall("新手引导点击地面回馈", new object[0]);
						}
					}
					if (FightPanelManager.inst != null && FightPanelManager.inst.curSelectUIItem != null)
					{
						if (FightPanelManager.inst.curSelectUIItem.GetComponent<SoldierUIITem>())
						{
							if (!this.IsCanSendSolider(this.HitPoint))
							{
								HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("此处不可放置兵种", "others"), HUDTextTool.TextUITypeEnum.Num5);
								return;
							}
							SoldierUIITem soldierUIITem = FightPanelManager.inst.curSelectUIItem as SoldierUIITem;
							if (soldierUIITem.soliderType == SoliderType.Normal)
							{
								armyData armyToInBattle = soldierUIITem.GetArmyToInBattle();
								Container.CreateContainer(this.HitPoint, armyToInBattle.buildingID, SenceManager.inst.SoldierId, armyToInBattle.index, armyToInBattle.lv, true, false, CommanderType.None);
								if (soldierUIITem.soliderNum <= 0)
								{
									FightPanelManager.inst.CurSelectUIItem = null;
								}
							}
							else
							{
								soldierUIITem.soliderNum = 0;
								soldierUIITem.ResetArmyNum();
								if (soldierUIITem.soliderNum <= 0)
								{
									FightPanelManager.inst.CurSelectUIItem = null;
								}
								Container.CreateCommander(this.HitPoint, HeroInfo.GetInstance().PlayerCommandoBuildingID, HeroInfo.GetInstance().Commando_Fight.id, HeroInfo.GetInstance().Commando_Fight.index, HeroInfo.GetInstance().Commando_Fight.level, HeroInfo.GetInstance().Commando_Fight.star, HeroInfo.GetInstance().Commando_Fight.skillLevel, true);
							}
						}
					}
					else
					{
						DragMgr.inst.MouseUp(MouseCommonType.forceMove, this.HitPoint, null);
						if (Camera_FingerManager.YinDaoDianji)
						{
							Camera_FingerManager.YinDaoDianji = false;
							HUDTextTool.inst.NextLuaCall("新手引导点击地面回馈", new object[0]);
						}
					}
				}
			}
			else if (UIManager.curState == SenceState.Spy)
			{
				DragMgr.inst.MouseUp(MouseCommonType.canncel, this.HitPoint, null);
			}
			break;
		case SenceType.WorldMap:
			if (this.IsNotCanDragOrClickTerrInWorldMap())
			{
				return;
			}
			if (NewbieGuidePanel._instance.ga_Self.activeSelf)
			{
				return;
			}
			if (WMap_DragManager.inst)
			{
				if (WMap_DragManager.inst.btnInUse || (WorldMapManager.inst.nBattlPanel && WorldMapManager.inst.nBattlPanel.activeInHierarchy) || (CommunicationPanel._Inst && CommunicationPanel._Inst.communication.activeSelf) || WorldMapManager.inst.GuidPanel.activeSelf)
				{
					return;
				}
				if (this.SelingIslandInWorldMap)
				{
					this.SelingIslandInWorldMap.MouseUp();
				}
				else
				{
					WMap_DragManager.inst.MouseUp();
				}
			}
			break;
		}
	}

	public bool IsCanSendSolider(Vector3 hitPoint)
	{
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i] && Mathf.Abs(hitPoint.x - SenceManager.inst.towers[i].tr.position.x) < (float)SenceManager.inst.towers[i].size / 2f + 1.5f && Mathf.Abs(hitPoint.z - SenceManager.inst.towers[i].tr.position.z) < (float)SenceManager.inst.towers[i].size / 2f + 1.5f)
			{
				return SenceManager.inst.towers[i].secType == 9 && (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.Spy);
			}
		}
		return true;
	}

	private void fingerDown_OnFingerDown(FingerDownEvent eventData)
	{
		if (SenceManager.inst.rotate_time != 0f)
		{
			return;
		}
		if (FingerGestures.Touches.Count > 1)
		{
			this.isUp = (FingerGestures.Touches.Count == 1);
			return;
		}
		this.isUp = true;
		this.isDragedSingleFinger = false;
		if (this.Pinching || this.Rotating || this.isDraging)
		{
			return;
		}
		switch (Loading.senceType)
		{
		case SenceType.Island:
			if (this.IsNotCanDragOrClickTerrInIsland())
			{
				return;
			}
			if (CameraControl.inst && CameraControl.inst.MainCamera)
			{
				this.SelingCharInIsland = null;
				this.SelingDragCharInIsland = null;
				this.ray = CameraControl.inst.MainCamera.ScreenPointToRay(eventData.Position);
				if (Physics.Raycast(this.ray, out this.hit, 100f, LayerMask.GetMask(new string[]
				{
					"Ground"
				})) && SenceManager.inst)
				{
					if (UIManager.curState == SenceState.Attacking)
					{
						this.sizePeriphery = 0.6f;
					}
					else
					{
						this.sizePeriphery = 0.2f;
					}
					if (UIManager.curState == SenceState.InBuild)
					{
						if (SenceManager.inst.tempTower && Vector3.Distance(this.hit.point, SenceManager.inst.tempTower.tr.position) <= (float)SenceManager.inst.tempTower.size * 0.5f + this.sizePeriphery)
						{
							this.SelingCharInIsland = SenceManager.inst.tempTower;
							this.SelingDragCharInIsland = this.SelingCharInIsland;
						}
					}
					else
					{
						int i = 0;
						while (i < SenceManager.inst.towers.Count)
						{
							if (SenceManager.inst.towers[i] && Vector3.Distance(this.hit.point, SenceManager.inst.towers[i].tr.position) <= (float)SenceManager.inst.towers[i].size * 0.5f + this.sizePeriphery)
							{
								if (SenceManager.inst.towers[i].secType == 9 && (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.Spy))
								{
									break;
								}
								this.SelingCharInIsland = SenceManager.inst.towers[i];
								SenceManager.inst.towers[i].ChooseByAtt();
								if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
								{
									if (this.SelingDragCharInIsland != this.SelingCharInIsland)
									{
										bool flag = false;
										if (this.SelingCharInIsland.GetComponent<T_Tower>().index == 90)
										{
											this.SelingCharInIsland.MouseDown();
											if (SenceManager.inst.GetWallLately == this.SelingCharInIsland && this.SelingCharInIsland.GetComponent<T_Tower>().Walltower_list.Count > 0)
											{
												flag = true;
											}
											if (!flag)
											{
												this.SelingCharInIsland.GetComponent<T_Tower>().Walltower_list.Clear();
												this.SelingCharInIsland.GetComponent<T_Tower>().Walltower_list = SenceManager.inst.GetTowerByLine(this.SelingCharInIsland.GetComponent<T_Tower>());
											}
										}
										Camera_FingerManager.inst.GetDragCamera(this.SelingCharInIsland.tr);
									}
									if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].resType < 4 || UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].resType == 99)
									{
										this.SelingDragCharInIsland = this.SelingCharInIsland;
									}
								}
								break;
							}
							else
							{
								i++;
							}
						}
						if (UIManager.curState == SenceState.Attacking)
						{
							for (int j = 0; j < SenceManager.inst.Tanks_Defend.Count; j++)
							{
								if (SenceManager.inst.Tanks_Defend[j] && Vector3.Distance(this.hit.point, SenceManager.inst.Tanks_Defend[j].tr.position) <= (float)SenceManager.inst.Tanks_Defend[j].size * 0.5f + this.sizePeriphery)
								{
									this.SelingCharInIsland = SenceManager.inst.Tanks_Defend[j];
									break;
								}
							}
						}
						if (UIManager.curState == SenceState.Home)
						{
							for (int k = 0; k < SenceManager.inst.Tanks_CommanderHome.Count; k++)
							{
								if (SenceManager.inst.Tanks_CommanderHome[k] && Vector3.Distance(this.hit.point, SenceManager.inst.Tanks_CommanderHome[k].transform.position) <= 3f)
								{
									SenceManager.inst.Tanks_CommanderHome[k].Sound_Play();
									FuncUIManager.inst.T_CommandPanelManage.gameObject.SetActive(true);
									FuncUIManager.inst.T_CommandPanelManage.ShowSolider(SenceManager.inst.Tanks_CommanderHome[k]);
									this.SelingCharInIsland = SenceManager.inst.Tanks_CommanderHome[k];
									break;
								}
							}
						}
						if (this.SelingCharInIsland == null && SenceInfo.curMap.IsMyMap && (UIManager.curState == SenceState.Home || UIManager.curState == SenceState.WatchResIsland))
						{
							foreach (T_Res current in SenceManager.inst.reses.Values)
							{
								if (current != null && Vector3.Distance(this.hit.point, current.tr.position) <= (float)current.size * 0.5f + this.sizePeriphery)
								{
									this.SelingCharInIsland = current;
									break;
								}
							}
						}
					}
					if (this.SelingCharInIsland)
					{
						this.SelingCharInIsland.MouseDown();
						return;
					}
				}
				if (this.SeledCharInIsland)
				{
					this.SeledCharInIsland.ChangeSelectState(Character.CharacterSelectStates.Idle);
				}
				this.SeledCharInIsland = this.SelingCharInIsland;
				SenceManager.inst.sameTower = false;
				DragMgr.inst.NewMouseDown();
			}
			break;
		case SenceType.WorldMap:
			if (this.IsNotCanDragOrClickTerrInWorldMap())
			{
				return;
			}
			if (NewbieGuidePanel._instance.ga_Self.activeSelf)
			{
				return;
			}
			if (WMap_DragManager.inst && WMap_DragManager.inst.camer)
			{
				if (WMap_DragManager.inst.btnInUse || (WorldMapManager.inst.nBattlPanel && WorldMapManager.inst.nBattlPanel.activeInHierarchy) || WorldMapManager.inst.GuidPanel.activeSelf || (CommunicationPanel._Inst && CommunicationPanel._Inst.communication.activeSelf))
				{
					return;
				}
				this.SelingIslandInWorldMap = null;
				this.ray = WMap_DragManager.inst.camer.ScreenPointToRay(eventData.Position);
				if (Physics.Raycast(this.ray, out this.hit, 100f, LayerMask.GetMask(new string[]
				{
					"Ground"
				})) && T_WMap.inst)
				{
					DicForU.GetValues<int, T_Island>(T_WMap.inst.islandList, this.AllIsland);
					int count = this.AllIsland.Count;
					for (int l = 0; l < count; l++)
					{
						if (this.AllIsland[l])
						{
							float num = this.hit.point.x - this.AllIsland[l].tr.position.x;
							float num2 = this.hit.point.z - this.AllIsland[l].tr.position.z;
							if (this.AllIsland[l].uiType == WMapTipsType.battle)
							{
								num += 0.5f * this.AllIsland[l].tr.localPosition.y;
								num2 += 1f * this.AllIsland[l].tr.localPosition.y;
								if (num <= 0.25f && num2 <= 0.3f && num >= -0.55f && num2 >= -0.35f)
								{
									this.SelingIslandInWorldMap = this.AllIsland[l];
									break;
								}
							}
							else if (num <= 0.25f && num2 <= 0.25f && num >= -0.25f && num2 >= -0.25f)
							{
								this.SelingIslandInWorldMap = this.AllIsland[l];
								break;
							}
						}
					}
					WMap_DragManager.inst.btnInUse = false;
					WMap_DragManager.inst.MouseDown();
				}
			}
			break;
		}
	}

	public void LateUpdate()
	{
	}

	private void RotateCamera(float angle)
	{
		if (this.Rotating)
		{
			this.campos = base.transform.position;
			float x = this.campos.x;
			float z = this.campos.z;
			float x2 = HUDTextTool.inst.centerPos.x;
			float z2 = HUDTextTool.inst.centerPos.z;
			float num = x - x2;
			float num2 = z - z2;
			if (this.isFist)
			{
				this.isFist = false;
				this.r = Mathf.Sqrt(Mathf.Pow(num, 2f) + Mathf.Pow(num2, 2f));
				this.theta = Mathf.Atan2(num2, num);
				this.shazhe = this.theta + angle;
			}
			else
			{
				this.shazhe += angle;
			}
			if (this.shazhe < 0f)
			{
				this.shazhe = 0f;
			}
			if (this.shazhe > 1.57079637f)
			{
				this.shazhe = 1.57079637f;
			}
			float x3 = this.r * Mathf.Cos(this.shazhe) + x2;
			float z3 = this.r * Mathf.Sin(this.shazhe) + z2;
			this.cameraPos.LookAt(HUDTextTool.inst.centerPos);
			this.cameraPos.position = new Vector3(x3, this.cameraPos.position.y, z3);
		}
	}

	private float Angle(Vector2 pos1, Vector2 pos2)
	{
		Vector2 vector = pos2 - pos1;
		Vector2 vector2 = new Vector2(1f, 0f);
		float num = Vector2.Angle(vector, vector2);
		if (Vector3.Cross(vector, vector2).z > 0f)
		{
			num = 360f - num;
		}
		return num;
	}

	private void twist_OnGesture(TwistGesture gesture)
	{
		if (gesture.Phase == ContinuousGesturePhase.Started)
		{
			this.Rotating = true;
			HUDTextTool.inst.GetCenterPos();
		}
		else if (gesture.Phase == ContinuousGesturePhase.Updated)
		{
			if (this.Rotating)
			{
				LogManage.Log("DeltaRotation" + gesture.DeltaRotation);
				this.RotateCamera(gesture.DeltaRotation * 0.0174532924f);
			}
		}
		else if (this.Rotating)
		{
			this.Rotating = false;
		}
	}

	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	private void pinch_OnGesture(PinchGesture gesture)
	{
		if (gesture.Phase == ContinuousGesturePhase.Started)
		{
			this.Pinching = true;
		}
		else if (gesture.Phase == ContinuousGesturePhase.Updated)
		{
			switch (Loading.senceType)
			{
			case SenceType.Island:
				if (CameraControl.inst)
				{
					CameraControl.inst.openDragCameraAndInertia = false;
				}
				if (this.IsNotCanDragOrClickTerrInIsland())
				{
					return;
				}
				if (this.Pinching && !this.Rotating)
				{
					CameraControl.inst.MoveCamera(gesture.Delta * CameraControl.pinchAddFloat);
				}
				break;
			case SenceType.WorldMap:
				if (this.IsNotCanDragOrClickTerrInWorldMap())
				{
					return;
				}
				if (NewbieGuidePanel._instance.ga_Self.activeSelf)
				{
					return;
				}
				if ((WMap_DragManager.inst && WMap_DragManager.inst.btnInUse) || (WorldMapManager.inst && WorldMapManager.inst.nBattlPanel && WorldMapManager.inst.nBattlPanel.activeInHierarchy) || (WorldMapManager.inst && WorldMapManager.inst.GuidPanel.activeSelf) || (CommunicationPanel._Inst && CommunicationPanel._Inst.communication.activeSelf))
				{
					return;
				}
				WMap_DragManager.pinchAddFloat = 0.68f;
				if (this.Pinching && !this.Rotating && WMap_DragManager.inst)
				{
					WMap_DragManager.inst.ZoomCamera(gesture.Delta * WMap_DragManager.pinchAddFloat);
				}
				break;
			}
		}
		else if (gesture.Phase == ContinuousGesturePhase.Ended && this.Pinching)
		{
			this.Pinching = false;
		}
	}
}
