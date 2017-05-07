using System;
using UnityEngine;

public class UIFloat3DObj : MonoBehaviour
{
	[HideInInspector]
	public Transform target;

	public Camera worldCamera;

	public Camera uiCamera;

	public float xOffset;

	public float yOffset;

	public float zOffset;

	public float x;

	public float y;

	public float z;

	private Vector3 staticPos;

	private Transform trans;

	private void Start()
	{
		this.trans = base.transform;
		this.staticPos = new Vector3(this.x, this.y, this.z);
	}

	private void Update()
	{
		if (this.target != null)
		{
			if (this.uiCamera == null)
			{
				this.uiCamera = UIManager.inst.uiCamera;
			}
			Vector3 vector = WMap_DragManager.inst.camer.WorldToScreenPoint(this.target.position);
			Vector3 vector2 = this.uiCamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, this.uiCamera.transform.position.z));
			this.trans.position = new Vector3(vector2.x, vector2.y, 0f);
		}
		else
		{
			this.trans.localPosition = this.staticPos;
		}
	}
}
