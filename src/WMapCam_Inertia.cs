using System;
using UnityEngine;

public class WMapCam_Inertia : MonoBehaviour
{
	private CamMoveType type = CamMoveType.inertia;

	private Vector3 targDir;

	private float speed;

	private Transform tr;

	private void Awake()
	{
		this.tr = base.transform;
		base.enabled = false;
	}

	private void Update()
	{
		switch (this.type)
		{
		case CamMoveType.inertia:
			if (!WMap_DragManager.inst.JudgeBoundary())
			{
				base.enabled = false;
			}
			if (this.speed > 0.2f)
			{
				this.speed -= 1f * Time.deltaTime;
				this.tr.localPosition += this.targDir * this.speed;
			}
			else
			{
				base.enabled = false;
			}
			break;
		}
	}

	public void InertiaMove(Vector3 tempDir)
	{
		LogManage.Log("惯性移动~");
	}
}
