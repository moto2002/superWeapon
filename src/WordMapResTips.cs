using System;
using UnityEngine;

public class WordMapResTips : IMonoBehaviour
{
	private Camera cam;

	public Transform tar;

	public UILabel textDes;

	public UISprite tipSprite;

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
		Vector3 position2 = Camera.main.WorldToScreenPoint(position);
		Vector3 vector = this.cam.ScreenToWorldPoint(position2);
		if (vector.z < 0f)
		{
		}
		this.tr.localScale = new Vector3(-0.005f * vector.z + 1f, -0.005f * vector.z + 1f, 0f);
		vector = new Vector3(vector.x, vector.y + 0.1f, this.tr.position.z);
		this.tr.position = position;
	}
}
