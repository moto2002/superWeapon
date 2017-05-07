using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
	public enum CameraBuildingStateEnum
	{
		Normal,
		BuildingMovingOrInBuilding
	}

	public static CameraControl inst;

	public Transform wordTr;

	public Transform cameraPos;

	public Transform cameraMain;

	public Camera MainCamera;

	public Camera EffectCamera;

	private float maxWheel = 80f;

	public float bigfar;

	public float minfar;

	[HideInInspector]
	public float maxRotat;

	[HideInInspector]
	public float minRotat;

	private float far = 14f;

	private float farForce;

	private float rat;

	public CameraMove_Client camM;

	public InertiaMove inerM;

	public InertiaZoom inerZ;

	public Transform backPos;

	public Blur blur;

	private Transform tr;

	public GameObject uiInUseBox;

	private GrayscaleEffect grayscaleEffect;

	private bool isGraySence;

	public CameraFollow cameraFollow;

	public Vector3 lastCamPos;

	public float lastCameraMainZ;

	public bool isMouseScrollWheel = true;

	public bool isLogin = true;

	public CameraControl.CameraBuildingStateEnum cameraBuildingStateEnum;

	private float lastFar;

	private GameObject buildingStatePanel;

	private float tweenSeconds = 0.36f;

	public float minX;

	public float maxX;

	public float minZ;

	public float maxZ;

	public float move_x;

	public float move_y;

	public float move_time;

	public bool isDragAuto;

	public GameObject target;

	private float pinchSpeed = 1.5f;

	private float camera_dis;

	private float camera_dis_0;

	private float cannot_drag_time;

	private float mTargetPosAddFloat = 0.06f;

	private float deltaTimeAddFloat = 12f;

	public static float pinchAddFloat = 20f;

	[HideInInspector]
	public Vector2 TargetPos;

	[HideInInspector]
	public Vector3 mTargetPos;

	[HideInInspector]
	public bool openDragCameraAndInertia;

	private Tweener NewRotaCameraFar;

	private Tweener NewRotaCameraNear;

	private bool moveFar;

	private bool moveNear;

	private int mubiao = 25;

	public bool isMove;

	private float MaxCameraOrthoSize = 45f;

	private float cameraOrthoSize;

	private float minFarHuanChong = 5f;

	private float maxFarHuanChong = 15f;

	private float lidu = 10f;

	private int canshu2 = 20;

	private int canshu3 = 15;

	public float Far
	{
		get
		{
			return this.far;
		}
		set
		{
			this.far = value;
		}
	}

	public Transform Tr
	{
		get
		{
			return this.tr;
		}
		set
		{
			this.tr = value;
		}
	}

	public bool IsGraySence
	{
		get
		{
			return this.isGraySence;
		}
		set
		{
			if (value == this.isGraySence)
			{
				return;
			}
			this.isGraySence = value;
			this.grayscaleEffect.enabled = value;
		}
	}

	public float CameraOrthoSize
	{
		get
		{
			return this.cameraOrthoSize;
		}
		set
		{
			this.cameraMain.localPosition = new Vector3(0f, 0f, value);
			this.cameraOrthoSize = value;
		}
	}

	public void OnDestroy()
	{
		CameraControl.inst = null;
	}

	private void Awake()
	{
		CameraControl.inst = this;
		this.tr = base.transform;
		this.minX = -25f;
		this.maxX = 25f;
		this.minZ = -110f;
		this.maxZ = -31f;
		GameTools.DontDestroyOnLoad(this.wordTr.gameObject);
	}

	private void OnEnable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
		CameraControl.inst = this;
	}

	private void OnDisable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		if (!this.MainCamera.gameObject.activeSelf)
		{
			this.MainCamera.gameObject.SetActive(true);
		}
		if (UIManager.curState == SenceState.Home)
		{
			CameraData cameraData = null;
			for (int i = 0; i < UnitConst.GetInstance().cameraDataList.Count; i++)
			{
				if (UnitConst.GetInstance().cameraDataList[i].type == 1)
				{
					cameraData = UnitConst.GetInstance().cameraDataList[i];
				}
			}
			this.bigfar = cameraData.farest;
			this.minfar = cameraData.nearest;
			this.maxRotat = cameraData.biggestAngle;
			this.minRotat = cameraData.smallestAngle;
		}
		if (UIManager.curState == SenceState.Spy)
		{
			CameraData cameraData2 = null;
			for (int j = 0; j < UnitConst.GetInstance().cameraDataList.Count; j++)
			{
				if (UnitConst.GetInstance().cameraDataList[j].type == 2)
				{
					cameraData2 = UnitConst.GetInstance().cameraDataList[j];
				}
			}
			this.bigfar = cameraData2.farest;
			this.minfar = cameraData2.nearest;
			this.maxRotat = cameraData2.biggestAngle;
			this.minRotat = cameraData2.smallestAngle;
		}
		if (UIManager.curState == SenceState.Attacking)
		{
			CameraData cameraData3 = null;
			for (int k = 0; k < UnitConst.GetInstance().cameraDataList.Count; k++)
			{
				if (UnitConst.GetInstance().cameraDataList[k].type == 3)
				{
					cameraData3 = UnitConst.GetInstance().cameraDataList[k];
				}
			}
			this.bigfar = cameraData3.farest;
			this.minfar = cameraData3.nearest;
			this.maxRotat = cameraData3.biggestAngle;
			this.minRotat = cameraData3.smallestAngle;
		}
	}

	private void Start()
	{
		this.grayscaleEffect = this.cameraMain.GetComponent<GrayscaleEffect>();
		if (this.grayscaleEffect)
		{
			this.grayscaleEffect.enabled = false;
		}
		this.cameraFollow = this.cameraMain.GetComponent<CameraFollow>();
		this.isGraySence = false;
	}

	public void Begain()
	{
		this.Far = this.MainCamera.orthographicSize;
		this.MoveCamera(0f);
	}

	public void ChangeCameraBuildingState(bool isBuilingState)
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.SetBuildGridActive(isBuilingState);
		}
		if (isBuilingState && this.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.Normal)
		{
			this.cameraBuildingStateEnum = CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding;
			this.openDragCameraAndInertia = false;
			this.Tr.DOLocalRotate(new Vector3(30f, 0f, 0f), this.tweenSeconds, RotateMode.Fast);
			this.minZ += 10f;
			this.maxZ += 30f;
			this.minfar -= 40f;
			if (this.cameraMain.localPosition.z > this.minfar)
			{
				this.lastFar = this.cameraMain.localPosition.z;
				this.cameraMain.DOLocalMoveZ(-5f + 20f * this.camera_dis, this.tweenSeconds, false);
				this.Far = -5f + 20f * this.camera_dis;
			}
			else
			{
				this.lastFar = -100f;
			}
			LogManage.LogError(this.Tr.localPosition);
			this.Tr.DOLocalMoveZ(this.Tr.localPosition.z + 30f, this.tweenSeconds, false);
			foreach (T_Tower current in SenceManager.inst.towers)
			{
				current.HideLogo();
				current.HideResModel();
				current.HideUpdateTittle();
			}
		}
		else if (!isBuilingState && this.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			this.cameraBuildingStateEnum = CameraControl.CameraBuildingStateEnum.Normal;
			this.openDragCameraAndInertia = false;
			this.Tr.DOLocalRotate(Vector3.zero, this.tweenSeconds, RotateMode.Fast);
			this.minZ -= 10f;
			this.maxZ -= 30f;
			this.minfar += 40f;
			if (this.lastFar != -100f)
			{
				this.cameraMain.DOLocalMoveZ(this.lastFar, this.tweenSeconds, false);
			}
			this.Tr.DOLocalMoveZ(this.Tr.localPosition.z - 30f, this.tweenSeconds, false);
			foreach (T_Tower current2 in SenceManager.inst.towers)
			{
				current2.DisplayLogo();
				current2.CalcResShowOrNo();
				current2.RefreshUpdateTittle();
			}
			SenceManager.inst.RemoveTempBuilding();
		}
		if (isBuilingState)
		{
			if (T_CommandPanelManage._instance && SenceManager.inst.tempTower == null)
			{
				T_CommandPanelManage._instance.HidePanel();
			}
			if (this.buildingStatePanel == null || !this.buildingStatePanel.activeSelf)
			{
				this.buildingStatePanel = FuncUIManager.inst.OpenFuncUI("BuilingStatePanel", SenceType.Island);
			}
			DragMgr.inst.buildDraging = true;
		}
		else
		{
			DragMgr.inst.buildDraging = false;
			if (this.buildingStatePanel && this.buildingStatePanel.activeSelf)
			{
				if (UIManager.curState == SenceState.WatchResIsland)
				{
					FuncUIManager.inst.ClearFuncPanelList_ForQueue();
					FuncUIManager.inst.OpenFuncUI("WatchPanel", SenceType.Island);
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
					FuncUIManager.inst.ClearFuncPanelList_ForQueue();
					FuncUIManager.inst.OpenFuncUI("MainUIPanel", SenceType.Island);
				}
			}
		}
	}

	public void CloseBuildingState()
	{
		if (this.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			this.cameraBuildingStateEnum = CameraControl.CameraBuildingStateEnum.Normal;
			this.openDragCameraAndInertia = false;
			this.Tr.localRotation = Quaternion.Euler(Vector3.zero);
			this.minZ -= 10f;
			this.maxZ -= 30f;
			this.minfar += 40f;
			if (this.lastFar != -100f)
			{
				this.cameraMain.localPosition = new Vector3(this.cameraMain.localPosition.x, this.cameraMain.localPosition.y, this.lastFar);
			}
			this.Tr.localPosition = new Vector3(this.Tr.localPosition.x, this.Tr.localPosition.y, this.Tr.localPosition.z - 30f);
		}
	}

	private void Update()
	{
		if (!NewbieGuidePanel.isEnemyAttck && NewbieGuidePanel.guideIdByServer != -1)
		{
			if (SenceManager.inst.Tanks_Attack.Count > 1)
			{
				if (SenceManager.inst.Tanks_Attack[0] && !SenceManager.inst.Tanks_Attack[0].IsDie)
				{
					this.target = SenceManager.inst.Tanks_Attack[0].ga;
					this.MainCamera.transform.localPosition = new Vector3(this.MainCamera.transform.localPosition.x, this.MainCamera.transform.localPosition.y, 20f);
				}
				else
				{
					this.target = null;
				}
			}
			else
			{
				this.target = null;
			}
		}
		if (BuilingStatePanel.inst && this.tr.localEulerAngles.x >= 10f)
		{
			this.camera_dis = BuilingStatePanel.inst.Camera_DisSlider.value;
			BuilingStatePanel.inst.Camera_DisSlider.GetComponent<UISprite>().autoResizeBoxCollider = false;
			BuilingStatePanel.inst.Camera_DisSlider.GetComponent<BoxCollider>().size = new Vector3(400f, 100f, 0f);
			if (this.camera_dis_0 != this.camera_dis)
			{
				this.cannot_drag_time = 0.3f;
				this.camera_dis_0 = this.camera_dis;
			}
			if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
			{
				this.MainCamera.transform.localPosition = new Vector3(this.MainCamera.transform.localPosition.x, this.MainCamera.transform.localPosition.y, -5f + 20f * this.camera_dis);
			}
			else
			{
				BuilingStatePanel.inst.Camera_DisSlider.value = 0f;
			}
			BuilingStatePanel.inst.Camera_DisLabel.text = string.Empty + (int)(this.camera_dis * 200f + 100f) + "%";
		}
		this.cannot_drag_time -= Time.deltaTime;
		if (this.target && this.target.activeSelf)
		{
			Vector3 position = Vector3.zero;
			position = HUDTextTool.inst.GetCameraMoveEndPos(this.target.transform.position, CameraControl.inst.tr.position, CameraControl.inst.cameraPos.eulerAngles.x);
			CameraControl.inst.tr.position = position;
			HUDTextTool.inst.GetCenterPos();
		}
		if (this.cannot_drag_time <= 0f && ((this.openDragCameraAndInertia && Input.touchCount < 2 && Loading.senceType == SenceType.Island) || this.isDragAuto))
		{
			if (Camera_FingerManager.inst.isDraging || this.isDragAuto)
			{
				if ((this.TargetPos.x > 0f && this.tr.localPosition.x > this.maxX) || (this.TargetPos.x < 0f && this.tr.localPosition.x < this.minX))
				{
					this.TargetPos.x = 0f;
				}
				if ((this.TargetPos.y < 0f && this.tr.localPosition.z < this.minZ) || (this.TargetPos.y > 0f && this.tr.localPosition.z > this.maxZ))
				{
					this.TargetPos.y = 0f;
				}
				this.mTargetPos += new Vector3(this.TargetPos.x * this.mTargetPosAddFloat, 0f, this.TargetPos.y * this.mTargetPosAddFloat);
			}
			if (this.mTargetPos.x > this.maxX)
			{
				this.mTargetPos.x = this.maxX;
			}
			if (this.mTargetPos.x < this.minX)
			{
				this.mTargetPos.x = this.minX;
			}
			if (this.mTargetPos.z > this.maxZ)
			{
				this.mTargetPos.z = this.maxZ;
			}
			if (this.mTargetPos.z < this.minZ)
			{
				this.mTargetPos.z = this.minZ;
			}
			float t = Time.deltaTime * this.deltaTimeAddFloat;
			this.tr.localPosition = Vector3.Lerp(this.tr.localPosition, new Vector3(this.mTargetPos.x, 40.79f, this.mTargetPos.z), t);
			if (this.tr.localPosition.x > this.maxX || (this.TargetPos.x < 0f && this.tr.localPosition.x < this.minX))
			{
				this.TargetPos.x = 0f;
			}
			if ((this.TargetPos.y < 0f && this.tr.localPosition.z < this.minZ) || (this.TargetPos.y > 0f && this.tr.localPosition.z > this.maxZ))
			{
				this.TargetPos.y = 0f;
			}
		}
		this.move_time -= Time.deltaTime;
		if (this.move_x != 0f || this.move_y != 0f)
		{
			float num = Input.mousePosition.x / (float)Screen.width;
			float num2 = Input.mousePosition.y / (float)Screen.height;
			if (NewbieGuidePanel._instance.ga_Self.activeSelf)
			{
				num = 0.5f;
				num2 = 0.5f;
			}
			if (num < 0.8f && num > 0.2f)
			{
				this.move_x = 0f;
			}
			if (num2 < 0.85f && num2 > 0.15f)
			{
				this.move_y = 0f;
			}
			Camera_FingerManager.inst.Skill_Excursion_pos(this.move_x, this.move_y);
		}
		if (this.move_time < 0f)
		{
			this.isDragAuto = false;
		}
		if (Input.touchCount == 0 && !Camera_FingerManager.inst.Pinching && this.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.Normal)
		{
			if (this.cameraMain.localPosition.z > this.minfar)
			{
				float num3 = (this.cameraMain.localPosition.z - this.minfar) * this.pinchSpeed * Time.deltaTime;
				this.CameraOrthoSize = this.cameraMain.localPosition.z - num3;
			}
			else if (this.cameraMain.localPosition.z < this.bigfar)
			{
				float num4 = (this.bigfar - this.cameraMain.localPosition.z) * this.pinchSpeed * Time.deltaTime;
				this.CameraOrthoSize = this.cameraMain.localPosition.z + num4;
			}
		}
	}

	private void OnGUI()
	{
	}

	public void DOLocalMoveZ(float to, float duration)
	{
		this.cameraMain.DOLocalMoveZ(to, duration, false);
	}

	public void MoveCamera(float _ang)
	{
		if (this.isMove)
		{
			this.farForce = (-this.Far + (this.Far + 60f) * 0.5f) * 0.01f;
			this.Far += _ang * this.farForce * Time.deltaTime;
			if (this.Far > this.minfar + this.maxFarHuanChong)
			{
				this.Far = this.minfar + this.maxFarHuanChong;
			}
			else if (this.Far < this.bigfar - this.minFarHuanChong)
			{
				this.Far = this.bigfar - this.minFarHuanChong;
			}
			this.CameraOrthoSize = this.Far;
		}
	}

	public void ResetFar(float _ang)
	{
		this.Far = _ang;
		this.CameraOrthoSize = this.Far;
	}

	public void Shake(float time = 0.3f, float strength = 10f)
	{
		this.cameraMain.DOShakePosition(time, new Vector3(0f, 0f, strength), 20, 15f, false);
	}
}
