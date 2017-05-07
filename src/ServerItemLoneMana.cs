using System;
using UnityEngine;

public class ServerItemLoneMana : MonoBehaviour
{
	public UILabel showServerName;

	public UILabel serverState;

	public UILabel serverTime;

	public GameObject serverBG;

	public string areaIP = User.GetIP();

	public int id;

	public UILabel idLabel;

	public string localareaIP;

	public string localPort;

	public static ServerItemLoneMana _ins;

	public UISprite RoleSprite;

	private void Start()
	{
		ServerItemLoneMana._ins = this;
	}

	private void Update()
	{
	}

	private void OnClick()
	{
		ServerItemLoneMana component = base.GetComponent<ServerItemLoneMana>();
		ClientMgr.GetNet().http.httpSession.ChangeUrl(component.areaIP);
		User.SetIP(component.areaIP);
		User.SetServerName(component.id);
		LoginEnterGame.ServerName = component.showServerName.text;
		LoginPanelManager._instance.LoginEnterGamePanel.gameObject.SetActive(true);
		LoginPanelManager._instance.SelectServerPanel.gameObject.SetActive(false);
	}
}
