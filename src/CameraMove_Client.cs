using System;
using UnityEngine;

public class CameraMove_Client : MonoBehaviour
{
	private Vector3 targetPos;

	private Vector3 daffDirection;

	private float rota;

	private Transform tr;

	private float dis;

	private float z = -80f;

	private bool zoom;

	private bool boundary;

	public float speed;

	public CamMoveType type;

	private void Awake()
	{
		this.tr = base.transform;
	}

	private void Start()
	{
		this.rota = CameraControl.inst.wordTr.localEulerAngles.y;
	}

	private void Update()
	{
		switch (this.type)
		{
		case CamMoveType.boundary:
			if (this.boundary)
			{
				this.dis = Vector3.Distance(this.tr.position, this.targetPos);
				this.tr.Translate(this.daffDirection * this.dis * 5f * Time.deltaTime);
				if (this.dis < 7f)
				{
					this.zoom = true;
				}
				if (this.dis < 0.5f)
				{
					this.boundary = false;
				}
			}
			break;
		}
		if (this.zoom)
		{
			if (CameraControl.inst.CameraOrthoSize < CameraControl.inst.bigfar)
			{
				this.z += 50f * Time.deltaTime;
				CameraControl.inst.ResetFar(this.z);
			}
			else if (CameraControl.inst.CameraOrthoSize > CameraControl.inst.minfar)
			{
				this.z -= 50f * Time.deltaTime;
				CameraControl.inst.ResetFar(this.z);
			}
			else if (!this.boundary)
			{
				base.enabled = false;
				this.zoom = false;
			}
		}
	}

	public void MoveCamera(Vector3 pos)
	{
		this.type = CamMoveType.boundary;
		this.boundary = true;
		this.targetPos = pos;
		this.daffDirection = (this.targetPos - this.tr.position).normalized;
		this.daffDirection = new Vector3(-this.daffDirection.z * Mathf.Sin(0.0174532924f * this.rota) + this.daffDirection.x * Mathf.Cos(0.0174532924f * this.rota), 0f, this.daffDirection.x * Mathf.Sin(0.0174532924f * this.rota) + this.daffDirection.z * Mathf.Cos(0.0174532924f * this.rota));
		base.enabled = true;
		this.z = CameraControl.inst.cameraMain.localPosition.z;
	}

	public void ReZoomCamera()
	{
		this.zoom = true;
		this.boundary = false;
		this.z = CameraControl.inst.cameraMain.localPosition.z;
		base.enabled = true;
	}

	public void InertiaCameraMove(Vector3 tempDir)
	{
		this.type = CamMoveType.inertia;
		this.boundary = true;
		this.daffDirection = tempDir * 0.25f;
		this.speed = DragMgr.inst.baseSpeed * 0.5f + DragMgr.inst.baseSpeed * DragMgr.inst.times * 0.5f;
		base.enabled = true;
		this.z = CameraControl.inst.CameraOrthoSize;
		this.zoom = true;
	}
}
