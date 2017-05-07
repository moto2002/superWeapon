using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DragMgr : MonoBehaviour
{
	[SerializeField]
	private bool btnInUse;

	public bool isInScrollViewDrag;

	public bool fingerMove;

	public static DragMgr inst;

	public int maxSize = 70;

	public Transform lastPos;

	public bool cameraBack;

	public float baseSpeed = 1.27f;

	public float speedSpan = 0.44f;

	private Transform camer;

	private float rota;

	public bool zooming;

	public bool draging;

	public bool buildDraging;

	public bool towTouch;

	public float width;

	public float height;

	public float times;

	public GUIStyle style = new GUIStyle();

	private int maxSizeX = 70;

	private int maxSizeZ = 70;

	private Vector2 oldPosition01;

	private Vector2 oldPosition02;

	private float dis1;

	private float sub;

	private float maxDis = 60f;

	private float speed2;

	public float dragX;

	public float dragY;

	private Vector3 tempDir;

	private float MoveSpeedA = 0.038f;

	private float MoveSpeedB = 0.124f;

	private float chubingTime;

	private float lastSendTime;

	public bool isMoveMap;

	private bool isChubing = true;

	public static event Action ClickTerrSendMessage
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			DragMgr.ClickTerrSendMessage = (Action)Delegate.Combine(DragMgr.ClickTerrSendMessage, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			DragMgr.ClickTerrSendMessage = (Action)Delegate.Remove(DragMgr.ClickTerrSendMessage, value);
		}
	}

	public bool BtnInUse
	{
		get
		{
			for (int i = FuncUIPanel.AllUIPanel.Count - 1; i >= 0; i--)
			{
				if (!FuncUIPanel.AllUIPanel[i].gameObject.activeInHierarchy)
				{
					FuncUIPanel.AllUIPanel.RemoveAt(i);
				}
			}
			return FuncUIPanel.AllUIPanel.Count > 0 || this.btnInUse;
		}
		set
		{
			for (int i = FuncUIPanel.AllUIPanel.Count - 1; i >= 0; i--)
			{
				if (!FuncUIPanel.AllUIPanel[i].gameObject.activeInHierarchy)
				{
					FuncUIPanel.AllUIPanel.RemoveAt(i);
				}
			}
			if (FuncUIPanel.AllUIPanel.Count > 0)
			{
				this.btnInUse = true;
			}
			else
			{
				this.btnInUse = value;
			}
		}
	}

	public float speed
	{
		get
		{
			return this.MoveSpeedA + this.MoveSpeedB / CameraControl.inst.Far;
		}
	}

	public void OnDestroy()
	{
		DragMgr.inst = null;
	}

	private void Awake()
	{
		DragMgr.inst = this;
		this.width = (float)Screen.width;
		this.height = (float)Screen.height;
		this.baseSpeed = 1.27f;
		this.times = this.width / 1366f;
		this.style.normal.textColor = new Color(1f, 0f, 0f);
		this.style.fontSize = Mathf.CeilToInt(16f * this.times);
	}

	private void Start()
	{
		this.camer = CameraControl.inst.Tr;
		this.rota = this.camer.localEulerAngles.y;
	}

	private void OnMouseDown()
	{
		SenceManager.inst.sameTower = false;
		this.NewMouseDown();
	}

	private void OnMouseDrag()
	{
		if (this.btnInUse)
		{
			return;
		}
		if (Input.touchCount > 1)
		{
			this.ZoomDoing();
			return;
		}
		this.towTouch = false;
		if (Input.touchCount == 1)
		{
			HUDTextTool.inst.isCenterPos = false;
			this.dragX = Input.GetTouch(0).deltaPosition.x;
			this.dragY = Input.GetTouch(0).deltaPosition.y;
		}
		else
		{
			this.dragX = -Input.GetAxis("Mouse X");
			this.dragY = -Input.GetAxis("Mouse Y");
		}
		this.ConversionFormulas(this.speed);
	}

	private void OnMouseUp()
	{
		if (this.btnInUse)
		{
			return;
		}
		this.MouseUp(MouseCommonType.canncel, Vector3.zero, null);
	}

	public void NewMouseDown()
	{
		this.btnInUse = false;
		this.fingerMove = false;
		this.draging = false;
		this.isMoveMap = true;
		this.ZoomBegain();
	}

	public bool JudgeDraged()
	{
		if (this.zooming)
		{
			CameraControl.inst.camM.ReZoomCamera();
			return true;
		}
		if (!this.draging)
		{
			return false;
		}
		if (this.buildDraging)
		{
			return false;
		}
		CameraControl.inst.camM.InertiaCameraMove(this.tempDir);
		return true;
	}

	private void Update()
	{
		if (!this.isChubing)
		{
			this.chubingTime += Time.deltaTime;
			if (this.chubingTime > 3f)
			{
				this.chubingTime = 0f;
				this.isChubing = true;
			}
		}
	}

	public void MouseUp(MouseCommonType _type, Vector3 _position, T_Tower tower)
	{
		if (T_CommandPanelManage._instance && T_CommandPanelManage._instance.click_protect_time > 0f)
		{
			return;
		}
		if (this.buildDraging && this.isMoveMap)
		{
			SenceManager.inst.RebackTower();
		}
		if (FightPanelManager.IsRetreat)
		{
			return;
		}
		if (this.zooming)
		{
			CameraControl.inst.camM.ReZoomCamera();
			return;
		}
		if (this.draging)
		{
			this.tempDir *= 1.2f;
			CameraControl.inst.camM.InertiaCameraMove(this.tempDir);
			return;
		}
		this.zooming = false;
		this.draging = false;
		switch (_type)
		{
		case MouseCommonType.forceMove:
		{
			if (UIManager.curState == SenceState.WatchVideo)
			{
				return;
			}
			if (SenceManager.inst.fightType == FightingType.Guard)
			{
				return;
			}
			if (UIManager.curState != SenceState.Attacking && UIManager.curState != SenceState.InBuild && SenceManager.inst.curTower != null)
			{
				SenceManager.inst.curTower.TowerInfoShow(false);
			}
			if (FightPanelManager.inst == null)
			{
				return;
			}
			FightPanelManager.inst.CloseAllSkillSlot();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && raycastHit.point.x < 57f && raycastHit.point.x > 0f && raycastHit.point.z < 57f && raycastHit.point.z > 0f && (raycastHit.point.x <= 45f || raycastHit.point.z <= 45f) && (raycastHit.point.x <= 50f || raycastHit.point.z >= 20f))
			{
				EventNoteMgr.NoticeFoceMove(raycastHit.point);
				SenceManager.inst.ForceMove(raycastHit.point);
			}
			break;
		}
		case MouseCommonType.canncel:
			if (UIManager.curState != SenceState.Attacking && UIManager.curState != SenceState.InBuild)
			{
				if (SenceManager.inst.curTower != null)
				{
					SenceManager.inst.curTower.TowerInfoShow(false);
				}
				if (SenceManager.inst.mover_Tower != null)
				{
					SenceManager.inst.mover_Tower.TowerInfoShow(false);
				}
			}
			break;
		case MouseCommonType.supperSkill:
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && FightPanelManager.inst.isSpColor)
			{
				SkillManage.inst.AcquittalSkill(FightPanelManager.inst.SkillId, raycastHit.point, -1);
			}
			break;
		}
		}
		if (DragMgr.ClickTerrSendMessage != null)
		{
			DragMgr.ClickTerrSendMessage();
		}
	}

	public bool JudgeIsDrag(float horizontal, float vertical)
	{
		if (Mathf.Abs(horizontal) > 0.2f || Mathf.Abs(vertical) > 0.2f)
		{
			this.dragX = horizontal;
			this.dragY = vertical;
			this.draging = true;
			return true;
		}
		return false;
	}

	public void ConversionFormulas(float _speed)
	{
		float horizontal = this.dragX / this.times;
		float vertical = this.dragY / this.times;
		this.MapDraging2(horizontal, vertical);
	}

	public void MapDraging2(float horizontal, float vertical)
	{
		if (this.btnInUse)
		{
			return;
		}
		if (this.zooming)
		{
			return;
		}
		if (Mathf.Abs(this.dragX) > 0.2f || Mathf.Abs(this.dragY) > 0.2f)
		{
			this.draging = true;
			if (this.camer.localPosition.x > -25f && this.camer.localPosition.x < 25f && this.camer.localPosition.z > -55f && this.camer.localPosition.z < -110f)
			{
				this.cameraBack = false;
			}
			else
			{
				if ((horizontal > 0f && this.camer.localPosition.x > 25f) || (horizontal < 0f && this.camer.localPosition.x < -25f))
				{
					horizontal = 0f;
				}
				if ((vertical < 0f && this.camer.localPosition.z < -110f) || (vertical > 0f && this.camer.localPosition.z > -31f))
				{
					vertical = 0f;
				}
				this.cameraBack = true;
			}
			if (horizontal != 0f || vertical != 0f)
			{
				this.isMoveMap = false;
			}
			this.tempDir = new Vector3(horizontal, 0f, vertical) * this.speed;
			this.camer.localPosition += this.tempDir;
			CameraControl.inst.camM.enabled = false;
		}
	}

	public void GetBackPos()
	{
		float num = this.camer.localPosition.x;
		float num2 = this.camer.localPosition.z;
		if (num < -20f)
		{
			num = -20f;
		}
		else if (num > 20f)
		{
			num = 20f;
		}
		if (num2 < -80f)
		{
			num2 = -80f;
		}
		else if (num2 > -15f)
		{
			num2 = -15f;
		}
		CameraControl.inst.backPos.localPosition = new Vector3(num, this.camer.localPosition.y, num2);
	}

	public void ZoomBegain()
	{
		CameraControl.inst.camM.enabled = false;
		this.zooming = false;
		if (Input.touchCount > 1)
		{
			this.oldPosition01 = Input.GetTouch(0).position;
			this.oldPosition02 = Input.GetTouch(1).position;
			this.dis1 = Vector2.Distance(this.oldPosition01, this.oldPosition02);
			this.zooming = true;
		}
	}

	public bool JudgeIsZoom()
	{
		if (Input.touchCount > 1)
		{
			this.ZoomDoing();
			return true;
		}
		this.towTouch = false;
		this.zooming = false;
		return false;
	}

	public void ZoomDoing()
	{
		if (Input.touchCount < 2)
		{
			return;
		}
		if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			if (!this.towTouch)
			{
				this.oldPosition01 = Input.GetTouch(0).position;
				this.oldPosition02 = Input.GetTouch(1).position;
				this.dis1 = Vector2.Distance(this.oldPosition01, this.oldPosition02);
			}
			this.towTouch = true;
			this.zooming = true;
			Vector2 position = Input.GetTouch(0).position;
			Vector2 position2 = Input.GetTouch(1).position;
			float num = Vector2.Distance(position, position2);
			this.sub = num - this.dis1;
			this.oldPosition01 = position;
			this.oldPosition02 = position2;
			this.dis1 = num;
			if (this.sub > -this.maxDis * this.times && this.sub < this.maxDis * this.times)
			{
				this.speed2 = this.sub * (50f / this.times);
				return;
			}
		}
	}
}
