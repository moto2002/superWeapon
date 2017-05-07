using System;
using UnityEngine;

public class SelectServerPanelManager : MonoBehaviour
{
	public UILabel serverName;

	public UILabel serverState;

	public static SelectServerPanelManager _instance;

	private void Awake()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_CloseServerPanel, new EventManager.VoidDelegate(this.CloseServerPanel));
		SelectServerPanelManager._instance = this;
	}

	private void Start()
	{
		if (!LoginPanelManager._instance.ServerList.ContainsKey(User.GetServerName()))
		{
			LoginPanelManager._instance.GetServer();
		}
		this.serverName.text = ((!LoginPanelManager._instance.ServerList.ContainsKey(User.GetServerName())) ? "NoName" : LoginPanelManager._instance.ServerList[User.GetServerName()].serverName);
		this.serverState.text = ((!LoginPanelManager._instance.ServerList.ContainsKey(User.GetServerName())) ? "NoName" : LoginPanelManager._instance.ServerList[User.GetServerName()].serverStateText);
	}

	public void CloseServerPanel(GameObject go)
	{
		LoginPanelManager._instance.LoginEnterGamePanel.gameObject.SetActive(true);
		LoginPanelManager._instance.SelectServerPanel.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
	}
}
