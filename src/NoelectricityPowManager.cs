using System;
using UnityEngine;

public class NoelectricityPowManager : Tittle_3D
{
	private Camera cam;

	public UILabel textDes;

	public float distance;

	private void Start()
	{
		this.cam = UIManager.inst.uiCamera;
		this.distance = CameraControl.inst.minfar - CameraControl.inst.bigfar;
	}

	private void Update()
	{
		if (this.tar == null || !this.tar.gameObject.activeSelf)
		{
			NGUITools.Destroy(this.ga);
			return;
		}
		if (Camera.main == null)
		{
			return;
		}
		float num = this.yOffect - 1f / this.distance * (CameraControl.inst.cameraMain.localPosition.z + Mathf.Abs(CameraControl.inst.bigfar));
		Vector3 position = new Vector3(this.tar.position.x, this.tar.position.y + num, this.tar.position.z);
		Vector3 vector = CameraControl.inst.MainCamera.WorldToScreenPoint(position);
		Vector3 vector2 = UIManager.inst.uiCamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, UIManager.inst.uiCamera.transform.position.z));
		this.tr.position = new Vector3(vector2.x, vector2.y, 0f);
	}
}
