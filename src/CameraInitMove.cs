using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class CameraInitMove : MonoBehaviour
{
	private struct cameraHome
	{
		public Vector3 postion;

		public Vector3 rotation;

		public float time;

		public float pathTime;

		public bool isCameraBlack;
	}

	public static CameraInitMove inst;

	public static bool FirstMove = true;

	private float z;

	private Transform tr;

	public CameraData data;

	public Transform camPos;

	public Camera UICamera;

	private float R_X;

	private float p_z;

	private float p_x;

	private float zDemp;

	private float waitTime;

	private bool cameMoved;

	private Vector3 cameraTaget = Vector3.zero;

	public bool isMoved;

	private bool isFirst = true;

	public Tweener cc;

	private Body_Model BX;

	public static bool isPlayMainBuildingAnimation;

	public event Action SpyOnMoved
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.SpyOnMoved = (Action)Delegate.Combine(this.SpyOnMoved, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.SpyOnMoved = (Action)Delegate.Remove(this.SpyOnMoved, value);
		}
	}

	public event Action AttackingOnMoved
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.AttackingOnMoved = (Action)Delegate.Combine(this.AttackingOnMoved, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.AttackingOnMoved = (Action)Delegate.Remove(this.AttackingOnMoved, value);
		}
	}

	public event Action HomeOnMoved
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.HomeOnMoved = (Action)Delegate.Combine(this.HomeOnMoved, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.HomeOnMoved = (Action)Delegate.Remove(this.HomeOnMoved, value);
		}
	}

	public bool IsFirst
	{
		get
		{
			return this.isFirst;
		}
		set
		{
			this.isFirst = value;
		}
	}

	public void OnDestroy()
	{
		CameraInitMove.inst = null;
	}

	private void Awake()
	{
		CameraInitMove.inst = this;
		this.tr = base.transform;
	}

	private void OnEnable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
	}

	private void OnDisable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
	}

	public void InitCamera()
	{
		this.UICamera = NGUITools.FindCameraForLayer(5);
		if (UIManager.curState == SenceState.Home)
		{
			for (int i = 0; i < UnitConst.GetInstance().cameraDataList.Count; i++)
			{
				if (UnitConst.GetInstance().cameraDataList[i].type == 1)
				{
					this.data = UnitConst.GetInstance().cameraDataList[i];
				}
			}
		}
		if (UIManager.curState == SenceState.Spy)
		{
			for (int j = 0; j < UnitConst.GetInstance().cameraDataList.Count; j++)
			{
				if (UnitConst.GetInstance().cameraDataList[j].type == 2)
				{
					this.data = UnitConst.GetInstance().cameraDataList[j];
				}
			}
		}
		if (UIManager.curState == SenceState.Attacking)
		{
			for (int k = 0; k < UnitConst.GetInstance().cameraDataList.Count; k++)
			{
				if (UnitConst.GetInstance().cameraDataList[k].type == 3)
				{
					this.data = UnitConst.GetInstance().cameraDataList[k];
				}
			}
		}
		if (UIManager.curState == SenceState.Spy)
		{
			CameraControl.inst.CameraOrthoSize = -10f;
			CameraControl.inst.cameraPos.localEulerAngles = new Vector3(this.data.normalAagle, 0f, 0f);
			CameraControl.inst.Tr.localPosition = new Vector3(-0.5331697f, 40.79f, -79.7725f);
			CameraControl.inst.openDragCameraAndInertia = false;
		}
		else if (UIManager.curState == SenceState.Home && !CameraInitMove.FirstMove)
		{
			CameraInitMove.FirstMove = true;
			CameraControl.inst.Tr.localPosition = new Vector3(-0.5331697f, 40.79f, -79.7725f);
			CameraControl.inst.cameraPos.localRotation = Quaternion.Euler(new Vector3(50f, 0f, 0f));
			CameraControl.inst.CameraOrthoSize = 0f;
			CameraControl.inst.openDragCameraAndInertia = false;
		}
		else
		{
			CameraControl.inst.Tr.localPosition = new Vector3(-0.5331697f, 40.79f, -79.7725f);
			CameraControl.inst.cameraPos.localRotation = Quaternion.Euler(new Vector3(50f, 0f, 0f));
			CameraControl.inst.CameraOrthoSize = 0f;
			CameraControl.inst.openDragCameraAndInertia = false;
		}
		this.tr.localPosition = new Vector3(0f, 0f, this.z);
		this.R_X = this.tr.parent.eulerAngles.x;
		this.isMoved = true;
		this.waitTime = 0f;
		this.cameMoved = false;
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		LogManage.LogError("创建完地图调用");
		this.InitCamera();
		this.CameraDoMove();
	}

	private void FirstGoHome()
	{
		this.BX = PoolManage.Ins.GetModelByBundleByName("BX", null);
		CameraControl.inst.target = this.BX.ga;
		Vector3 position = SenceManager.inst.MainBuilding.tr.position;
		DieBall huichen = PoolManage.Ins.CreatEffect("huichen_M", this.BX.tr.position - new Vector3(0f, 0f, 0.4f), Quaternion.identity, this.BX.tr);
		this.BX.tr.position = new Vector3(60f, position.y, position.z);
		this.BX.tr.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
		Ani_CharacterControler ani = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.BX.ga);
		ani.AnimPlay("car");
		this.BX.tr.DOLocalMoveX(position.x, 5f, false).OnComplete(delegate
		{
			if (huichen)
			{
				huichen.DesInPool();
			}
			this.BX.tr.DORotate(new Vector3(0f, 90f, 0f), 2f, RotateMode.Fast).OnComplete(delegate
			{
				CameraControl.inst.target = null;
				AudioManage.inst.PlayAuidoBySelf_3D("Opencenter", this.BX.ga, false, 0uL);
				ani.AnimPlay("base");
				ani.AnimPlayQuened("stop");
				CameraInitMove.isPlayMainBuildingAnimation = false;
				this.CameraDoMove();
			});
		});
	}

	public void CameraDoMove()
	{
		UIManager.inst.UIInUsed(true);
		if (CameraInitMove.isPlayMainBuildingAnimation)
		{
			this.FirstGoHome();
			return;
		}
		if (UIManager.curState == SenceState.Spy)
		{
			T_Tower mainBuilding = SenceManager.inst.MainBuilding;
			if (mainBuilding != null && this.isMoved)
			{
				this.cameraTaget = HUDTextTool.inst.GetCameraMoveEndPos(mainBuilding.tr.position, CameraControl.inst.Tr.position, this.data.moveAngle);
				if (this.data != null)
				{
					CameraControl.inst.DOLocalMoveZ(this.data.moveHeight, 3f);
					CameraControl.inst.cameraPos.DOLocalRotate(new Vector3(this.data.moveAngle, 0f, 0f), 3f, RotateMode.Fast);
				}
				CameraControl.inst.Tr.DOMove(this.cameraTaget, 3f, false).OnComplete(delegate
				{
					UIManager.inst.UIInUsed(false);
					if (this.SpyOnMoved != null)
					{
						this.SpyOnMoved();
					}
					if (SpyPanelManager.inst)
					{
						SpyPanelManager.inst.box.SetActive(false);
					}
				});
				CameraControl.inst.openDragCameraAndInertia = false;
			}
		}
		else if (UIManager.curState == SenceState.Attacking && (NewbieGuidePanel.isEnemyAttck || NewbieGuidePanel.curGuideIndex == -1))
		{
			T_Tower mainBuilding2 = SenceManager.inst.MainBuilding;
			if (mainBuilding2 != null && this.isMoved)
			{
				this.cameraTaget = HUDTextTool.inst.GetCameraMoveEndPos(mainBuilding2.tr.position, CameraControl.inst.Tr.position, this.data.moveAngle);
				if (this.data != null)
				{
					CameraControl.inst.DOLocalMoveZ(this.data.moveHeight, 3f);
					CameraControl.inst.cameraPos.DOLocalRotate(new Vector3(this.data.moveAngle, 0f, 0f), 3f, RotateMode.Fast);
				}
				CameraControl.inst.Tr.DOMove(this.cameraTaget, 3f, false).OnComplete(delegate
				{
					UIManager.inst.UIInUsed(false);
					if (this.AttackingOnMoved != null)
					{
						this.AttackingOnMoved();
					}
				});
				CameraControl.inst.openDragCameraAndInertia = false;
			}
		}
		else if (NewbieGuidePanel.isEnemyAttck)
		{
			if (SenceInfo.battleResource == SenceInfo.BattleResource.LegionBattleFight || SenceInfo.battleResource == SenceInfo.BattleResource.NormalBattleFight)
			{
				this.SetHomeCamera();
			}
			else
			{
				T_Tower mainBuilding3 = SenceManager.inst.MainBuilding;
				if (mainBuilding3 != null && this.isMoved)
				{
					this.cameraTaget = HUDTextTool.inst.GetCameraMoveEndPos(mainBuilding3.tr.position, CameraControl.inst.Tr.position, this.data.moveAngle);
					if (this.data != null)
					{
						CameraControl.inst.DOLocalMoveZ(this.data.moveHeight, 3f);
						CameraControl.inst.cameraPos.DOLocalRotate(new Vector3(this.data.moveAngle, 0f, 0f), 3f, RotateMode.Fast);
					}
					CameraControl.inst.Tr.DOMove(this.cameraTaget, 3f, false).OnComplete(new TweenCallback(this.GoHomeCameraMoveEnd));
					CameraControl.inst.openDragCameraAndInertia = false;
				}
				else
				{
					UIManager.inst.UIInUsed(false);
					if (this.HomeOnMoved != null)
					{
						this.HomeOnMoved();
					}
				}
			}
		}
	}

	private void GoHomeCameraMoveEnd()
	{
		this.SetHomeCamera();
		if (this.isFirst)
		{
			NewbieGuidePanel.CallLuaByStart();
			this.isFirst = false;
		}
		else if (SenceInfo.battleResource != SenceInfo.BattleResource.LegionBattleFight && SenceInfo.battleResource != SenceInfo.BattleResource.NormalBattleFight)
		{
			HUDTextTool.inst.NextLuaCall("回家相机移动结束· · ", new object[]
			{
				base.gameObject
			});
		}
	}

	private void SetHomeCamera()
	{
		UIManager.inst.UIInUsed(false);
		if (this.HomeOnMoved != null)
		{
			this.HomeOnMoved();
		}
		if (!SenceManager.inst.MainBuilding.ga.activeSelf)
		{
			SenceManager.inst.MainBuilding.ga.SetActive(true);
		}
		if (this.BX)
		{
			this.BX.DesInsInPool();
		}
		CameraControl.inst.isMove = true;
		HUDTextTool.inst.GetCenterPos();
		CameraControl.inst.Far = this.data.moveHeight;
	}
}
