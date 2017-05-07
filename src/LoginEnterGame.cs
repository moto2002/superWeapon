using System;
using UnityEngine;

public class LoginEnterGame : MonoBehaviour
{
	public UILabel serverName;

	public UILabel perUserName;

	public static LoginEnterGame _instance;

	public GameObject EnterGame;

	public static string userName;

	public static string ServerIP;

	public static string ServerName;

	public GameObject changeServerGa;

	public GameObject ChangeUserGa;

	private void Start()
	{
	}

	private void Awake()
	{
		LoginEnterGame._instance = this;
		UIEventListener.Get(this.changeServerGa).onClick = new UIEventListener.VoidDelegate(this.OnChangServer);
		UIEventListener.Get(this.ChangeUserGa).onClick = new UIEventListener.VoidDelegate(this.OnChangeUserName);
	}

	private void OnEnable()
	{
		this.perUserName.text = User.GetUserName();
		this.serverName.text = LoginEnterGame.ServerName;
	}

	public void OnChangServer(GameObject go)
	{
		if (GameStartNotice._inst && GameStartNotice._inst.gameObject.activeSelf)
		{
			return;
		}
		LoginPanelManager._instance.LoginUserNamePanel.gameObject.SetActive(false);
		LoginPanelManager._instance.LoginEnterGamePanel.gameObject.SetActive(false);
		LoginPanelManager._instance.SelectServerPanel.gameObject.SetActive(true);
	}

	public void OnChangeUserName(GameObject go)
	{
		if (GameStartNotice._inst && GameStartNotice._inst.gameObject.activeSelf)
		{
			return;
		}
		User.GetUserName();
		LoginPanelManager._instance.LoginUserNamePanel.gameObject.SetActive(true);
		LoginPanelManager._instance.LoginEnterGamePanel.gameObject.SetActive(false);
	}

	public void Update()
	{
		if (HUDTextTool.isCloseGameStartNotice)
		{
			this.EnterGame.GetComponent<BoxCollider>().enabled = true;
			this.ChangeUserGa.GetComponent<BoxCollider>().enabled = true;
		}
	}
}
