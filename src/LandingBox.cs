using System;
using UnityEngine;

public class LandingBox : MonoBehaviour
{
	public Transform[] moveList;

	private float colSpan = 1.2f;

	private RaycastHit hit;

	private Ray ray;

	private bool drag;

	private float speed = 0.7f;

	private Transform camer;

	private float rota;

	private Transform tr;

	public Transform sendPos;

	public Transform[] SendPoses;

	public float StartZ = -11.6f;

	public float dempz = 4.64f;

	public static LandingBox ins;

	private bool isMouseDown = true;

	public bool isLandingBoxMouseUp;

	public void OnDestroy()
	{
		LandingBox.ins = null;
	}

	private void Awake()
	{
		this.tr = base.transform;
		LandingBox.ins = this;
		this.StartZ = -5f;
		this.dempz = 2.6f;
	}

	private void Start()
	{
		this.camer = CameraControl.inst.Tr;
		this.rota = this.camer.localEulerAngles.y;
	}

	public void OnMouseDown()
	{
		DragMgr.inst.draging = false;
		DragMgr.inst.ZoomBegain();
	}

	public void NewBieGuideMouseUp()
	{
		if (NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.HideSelf();
		}
		HUDTextTool.inst.NextLuaCallByIsEnemyAttck("登陆点的点击· · · ·", new object[]
		{
			base.gameObject
		});
	}

	public void OnMouseUp()
	{
	}

	private void OnMouseDrag()
	{
		if (NewbieGuidePanel.isEnemyAttck)
		{
			if (Input.touchCount > 1)
			{
				DragMgr.inst.ZoomDoing();
				return;
			}
			DragMgr.inst.towTouch = false;
			DragMgr.inst.dragX = -Input.GetAxis("Mouse X");
			DragMgr.inst.dragY = -Input.GetAxis("Mouse Y");
			DragMgr.inst.ConversionFormulas(DragMgr.inst.speed);
		}
	}
}
