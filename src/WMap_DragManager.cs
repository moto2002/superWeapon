using System;
using UnityEngine;

public class WMap_DragManager : MonoBehaviour
{
	public static WMap_DragManager inst;

	public bool btnInUse;

	public float speed = 0.01f;

	public bool draging;

	public bool zooming;

	private Vector3 tempDir;

	public Transform tr;

	public Camera camer;

	private float bigfar = 9f;

	private float minfar = 5f;

	private float far = 5f;

	public float farForce = 1f;

	private float rat;

	public WMapCam_Inertia cameIner;

	public Transform WMapMove;

	public Transform moveTarget;

	private float linJie = 0.1f;

	public static float pinchAddFloat = 200f;

	public void OnDestroy()
	{
		WMap_DragManager.inst = null;
	}

	private void Awake()
	{
		this.speed = 0.01f;
		WMap_DragManager.inst = this;
		this.tr = base.transform;
		if (this.cameIner == null)
		{
			this.cameIner = this.tr.GetComponent<WMapCam_Inertia>();
		}
	}

	private void OnEnable()
	{
		this.btnInUse = false;
	}

	public void Drag(float horizontal, float vertical)
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		this.WMapDraging(horizontal * GameConst.resolutionTimes, vertical * GameConst.resolutionTimes);
	}

	public bool JudgeIsDrag(float horizontal, float vertical)
	{
		if (Mathf.Abs(horizontal) > 0.2f || Mathf.Abs(vertical) > 0.2f)
		{
			this.draging = true;
			return true;
		}
		return false;
	}

	public bool JudgeBoundary()
	{
		return this.tr.localPosition.x >= (float)(-(float)T_WMap.inst.mapSize) * this.linJie && this.tr.localPosition.x <= (float)T_WMap.inst.mapSize * this.linJie && this.tr.localPosition.z >= (float)(-(float)T_WMap.inst.mapSize) * this.linJie && this.tr.localPosition.z <= (float)T_WMap.inst.mapSize * this.linJie;
	}

	public void WMapDraging(float horizontal, float vertical)
	{
		if (this.zooming)
		{
			return;
		}
		if (!this.JudgeIsDrag(horizontal, vertical))
		{
			this.tempDir = new Vector3(horizontal, 0f, vertical) * this.speed;
			return;
		}
		if (!this.tr)
		{
			return;
		}
		if ((horizontal > 0f && this.tr.localPosition.x > 7f) || (horizontal < 0f && this.tr.localPosition.x < -8f))
		{
			horizontal = 0f;
		}
		if ((vertical < 0f && this.tr.localPosition.z < -7f) || (vertical > 0f && this.tr.localPosition.z > 8f))
		{
			vertical = 0f;
		}
		this.tempDir = new Vector3(horizontal, 0f, vertical) * this.speed * this.farForce;
		this.tr.localPosition += this.tempDir;
	}

	public void WMapInertia()
	{
		this.cameIner.InertiaMove(this.tempDir);
	}

	private void OnGUI()
	{
	}

	public void ZoomCamera(float _ang)
	{
		this.far += -_ang * this.farForce * Time.deltaTime;
		if (this.far < this.minfar)
		{
			this.far = this.minfar;
		}
		else if (this.far > this.bigfar)
		{
			this.far = this.bigfar;
		}
		this.camer.transform.localPosition = new Vector3(0f, this.far, 0f);
		if (TipsManager.inst)
		{
			TipsManager.inst.CloseAllTips();
		}
	}

	public void MouseUp()
	{
		TipsManager.inst.CloseAllTips();
	}

	public void MouseDown()
	{
		this.btnInUse = false;
		this.draging = false;
	}
}
