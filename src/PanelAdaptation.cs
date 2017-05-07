using System;
using UnityEngine;

public class PanelAdaptation : MonoBehaviour
{
	private UIPanel curPanel;

	private void Start()
	{
		this.curPanel = base.GetComponent<UIPanel>();
		LogManage.Log(this.curPanel.finalClipRegion);
		LogManage.Log(Screen.width + string.Empty + Screen.height);
	}

	private void Update()
	{
	}
}
