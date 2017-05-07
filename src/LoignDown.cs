using System;
using UnityEngine;

public class LoignDown : MonoBehaviour
{
	private void Awake()
	{
	}

	public void OnChangServer(GameObject go)
	{
		LoginPanelManager._instance.LoginUserNamePanel.gameObject.SetActive(false);
		LoginPanelManager._instance.LoginEnterGamePanel.gameObject.SetActive(false);
		LoginPanelManager._instance.SelectServerPanel.gameObject.SetActive(true);
	}
}
