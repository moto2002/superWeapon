using System;
using UnityEngine;

public class TClientLogManage : MonoBehaviour
{
	public bool IsLogManageMode;

	private bool isChecked;

	private string widgetPath = string.Empty;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.F9))
		{
			this.IsLogManageMode = !this.IsLogManageMode;
		}
	}

	private void OnGUI()
	{
		if (this.IsLogManageMode)
		{
			if (UICamera.hoveredObject != null)
			{
				this.widgetPath = string.Empty;
				this.FindPath(UICamera.hoveredObject);
				GUI.Label(new Rect(30f, 50f, 400f, 50f), this.widgetPath);
				if (Input.GetMouseButtonDown(1))
				{
				}
			}
			else
			{
				this.widgetPath = string.Empty;
				GUI.Label(new Rect(30f, 50f, 400f, 50f), this.widgetPath);
			}
		}
	}

	private void FindPath(GameObject go)
	{
		this.widgetPath = go.name;
		while (go.transform.parent != null)
		{
			this.widgetPath = go.transform.parent.name + "/" + this.widgetPath;
			go = go.transform.parent.gameObject;
		}
	}
}
