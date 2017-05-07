using System;
using UnityEngine;

public class UIRootExtend : MonoBehaviour
{
	public int ManualWidth = 640;

	public int ManualHeight = 960;

	private UIRoot _UIRoot;

	private void Awake()
	{
		this._UIRoot = base.GetComponent<UIRoot>();
	}

	private void FixedUpdate()
	{
		if (Convert.ToSingle(Screen.height) / (float)Screen.width > Convert.ToSingle(this.ManualHeight) / (float)this.ManualWidth)
		{
			this._UIRoot.manualHeight = Mathf.RoundToInt(Convert.ToSingle(this.ManualWidth) / (float)Screen.width * (float)Screen.height);
		}
		else
		{
			this._UIRoot.manualHeight = this.ManualHeight;
		}
	}
}
