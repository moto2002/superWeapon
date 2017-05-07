using System;
using UnityEngine;

public class T_InfoPro : MonoBehaviour
{
	public Transform tar;

	public UILabel timer;

	public InfoPrograme programe;

	public T_Tower tower;

	public int buildTime;

	public DateTime finiTime;

	private Camera cam;

	private Transform tr;

	private float leftTime;

	private float lifeRatio;

	private bool send;

	private void Awake()
	{
		this.tr = base.transform;
		this.cam = UIManager.inst.uiCamera;
	}

	private void Start()
	{
	}

	private void Update()
	{
		this.timer.text = TimeTools.DateDiffToString(DateTime.Now, this.finiTime);
		this.programe.uis.value = this.lifeRatio;
		if (this.tar == null || !this.tar.gameObject.activeSelf)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			Vector3 position = new Vector3(this.tar.position.x, this.tar.position.y + 1.5f, this.tar.position.z);
			Vector3 position2 = Camera.main.WorldToScreenPoint(position);
			Vector3 position3 = this.cam.ScreenToWorldPoint(position2);
			if (position3.z < 0f)
			{
			}
			position3 = new Vector3(position3.x, position3.y + 0.1f, this.tr.position.z);
			this.tr.position = position3;
		}
	}
}
