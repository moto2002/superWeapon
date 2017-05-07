using System;
using UnityEngine;

public class A_WMap_Car : MonoBehaviour
{
	public int Road_No;

	public A_WMap_Road AW_Road;

	public AIPath Player_AIpath;

	public WMap_CarType WM_CarType;

	public Transform MB;

	private void Start()
	{
		this.MB = base.transform.parent.GetComponent<AIPath>().target.transform;
		this.Player_AIpath = base.transform.parent.GetComponent<AIPath>();
		this.Player_AIpath.MapCar = true;
	}

	private void Update()
	{
		if (Loading.senceType != SenceType.WorldMap)
		{
			UnityEngine.Object.Destroy(this.MB.gameObject);
			UnityEngine.Object.Destroy(this.Player_AIpath.gameObject);
		}
		float num = Vector2.Distance(new Vector2(base.transform.position.x, base.transform.position.z), new Vector2(this.MB.position.x, this.MB.position.z));
		if (num <= 0.15f)
		{
			if (this.Road_No == 1)
			{
				UnityEngine.Object.Destroy(this.MB.gameObject);
				UnityEngine.Object.Destroy(this.Player_AIpath.gameObject);
			}
			if (this.Road_No == 2)
			{
				if (this.WM_CarType == WMap_CarType.OnBridge)
				{
					this.MB.transform.position = this.AW_Road.NodeList2[2].transform.position;
					this.WM_CarType = WMap_CarType.DownBridge1;
				}
				else if (this.WM_CarType == WMap_CarType.DownBridge1)
				{
					this.MB.transform.position = this.AW_Road.NodeList2[3].transform.position;
					this.WM_CarType = WMap_CarType.DownBridge2;
				}
				else if (this.WM_CarType == WMap_CarType.DownBridge2)
				{
					this.MB.transform.position = this.AW_Road.NodeList2[4].transform.position;
					this.WM_CarType = WMap_CarType.Rotate1;
				}
				else if (this.WM_CarType == WMap_CarType.Rotate1)
				{
					this.MB.transform.position = this.AW_Road.NodeList2[5].transform.position;
					this.WM_CarType = WMap_CarType.Rotate2;
				}
				else if (this.WM_CarType == WMap_CarType.Rotate2)
				{
					this.MB.transform.position = this.AW_Road.NodeList2[6].transform.position;
					this.WM_CarType = WMap_CarType.Rotate3;
				}
				else if (this.WM_CarType == WMap_CarType.Rotate3)
				{
					UnityEngine.Object.Destroy(this.MB.gameObject);
					UnityEngine.Object.Destroy(this.Player_AIpath.gameObject);
				}
			}
			else if (this.Road_No == 3)
			{
				if (this.WM_CarType == WMap_CarType.Rotate1)
				{
					this.MB.transform.position = this.AW_Road.NodeList3[2].transform.position;
					this.WM_CarType = WMap_CarType.Rotate2;
				}
				else if (this.WM_CarType == WMap_CarType.Rotate2)
				{
					this.MB.transform.position = this.AW_Road.NodeList3[3].transform.position;
					this.WM_CarType = WMap_CarType.Rotate3;
				}
				else if (this.WM_CarType == WMap_CarType.Rotate3)
				{
					UnityEngine.Object.Destroy(this.MB.gameObject);
					UnityEngine.Object.Destroy(this.Player_AIpath.gameObject);
				}
				else if (this.WM_CarType == WMap_CarType.Rotate4)
				{
				}
			}
		}
		switch (this.WM_CarType)
		{
		case WMap_CarType.OnBridge:
			base.transform.localPosition = new Vector3(0f, 2.6f, 0f);
			break;
		case WMap_CarType.DownBridge1:
			base.transform.localPosition = new Vector3(0f, Mathf.Max(2f, base.transform.localPosition.y - 0.08f * Time.deltaTime), 0f);
			break;
		case WMap_CarType.DownBridge2:
			base.transform.localPosition = new Vector3(0f, Mathf.Max(1.2f, base.transform.localPosition.y - 0.3f * Time.deltaTime), 0f);
			if (base.transform.localPosition.y <= 1.2f)
			{
				base.transform.localEulerAngles = Vector3.zero;
				this.Player_AIpath.speed = 0.5f;
			}
			else
			{
				base.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
				this.Player_AIpath.speed = 0.35f;
			}
			break;
		}
	}
}
