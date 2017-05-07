using System;
using UnityEngine;

public class DescripUIInfo : IMonoBehaviour
{
	private Camera cam;

	public Transform tar;

	public UILabel textDes;

	public UISprite tipSprite;

	public void OnDisable()
	{
		UnityEngine.Object.Destroy(this.ga);
	}

	private void Start()
	{
		this.cam = UIManager.inst.uiCamera;
	}

	private void TweenAni()
	{
		if (this.tr.localScale == Vector3.zero)
		{
			TweenScale.Begin(base.gameObject, 1f, Vector3.one);
		}
		else
		{
			TweenScale.Begin(base.gameObject, 1f, Vector3.zero);
		}
	}

	private void Update()
	{
		if (this.tar == null || !this.tar.gameObject.activeInHierarchy)
		{
			NGUITools.Destroy(this.ga);
			return;
		}
		if (Camera.main == null)
		{
			return;
		}
		Vector3 position = new Vector3(this.tar.position.x, this.tar.position.y + 3f, this.tar.position.z);
		Vector3 vector = CameraControl.inst.MainCamera.WorldToScreenPoint(position);
		Vector3 vector2 = UIManager.inst.uiCamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, UIManager.inst.uiCamera.transform.position.z));
		this.tr.position = new Vector3(vector2.x, vector2.y, 0f);
		this.tr.localScale = Vector3.one * 0.6f;
	}
}
