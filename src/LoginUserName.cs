using System;
using System.Collections.Generic;
using UnityEngine;

public class LoginUserName : MonoBehaviour
{
	public UILabel password;

	public UILabel userName;

	public UIInput passwordInput;

	public UIInput userNameInput;

	private string getName;

	private string setName;

	public GameObject loginBtn;

	public UITable utable;

	public UIScrollView uscrollview;

	public GameObject userItemPerfab;

	public GameObject dounBtn;

	public GameObject loginUserNameDown;

	public static Dictionary<string, string> userNameDic;

	public static List<string> uNameStr = new List<string>();

	public static LoginUserName _instance;

	private void Awake()
	{
		LoginUserName._instance = this;
	}

	private void OnEnable()
	{
		this.userNameInput.value = User.GetUserName();
	}

	private void Start()
	{
		LoginUserName.userNameDic = User.GetUserNames();
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_DownList, new EventManager.VoidDelegate(this.OnDownClic));
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_ChooseUserName, new EventManager.VoidDelegate(this.OnChooseUserName));
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_ClearUserName, new EventManager.VoidDelegate(this.OnClearUserName));
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_DownList, new EventManager.VoidDelegate(this.OnDownClick));
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_CloseChooseUserName, new EventManager.VoidDelegate(this.ClearDown));
		foreach (KeyValuePair<string, string> current in LoginUserName.userNameDic)
		{
			GameObject gameObject = NGUITools.AddChild(this.utable.gameObject, this.userItemPerfab);
			RecoverUserName component = gameObject.GetComponent<RecoverUserName>();
			component.userNameInput.text = current.Key;
		}
		UIEventListener.Get(this.loginBtn).onClick = new UIEventListener.VoidDelegate(this.OnLoginGameBtn);
	}

	public void OnDownClic(GameObject go)
	{
		if (this.loginUserNameDown.transform.childCount == 0)
		{
			this.loginUserNameDown.gameObject.SetActive(false);
		}
		else
		{
			this.loginUserNameDown.gameObject.SetActive(true);
		}
	}

	public void OnLoginGameBtn(GameObject go)
	{
		if (string.IsNullOrEmpty(this.userNameInput.value) || this.userNameInput.value.Length > 16)
		{
			LogManage.LogError("用户名不能为空且长度不能超过16");
			MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("提示", "others"), LanguageManage.GetTextByKey("用户名不能为空且长度不能超过16", "others"), LanguageManage.GetTextByKey("确定", "others"), null);
			return;
		}
		User.SetUserName(this.userNameInput.value, this.passwordInput.value);
		HeroInfo.GetInstance().platformId = this.userNameInput.value;
		LoginPanelManager._instance.LoginEnterGamePanel.gameObject.SetActive(true);
		LoginPanelManager._instance.LoginUserNamePanel.gameObject.SetActive(false);
	}

	public void OnChooseUserName(GameObject go)
	{
		this.userNameInput.value = go.GetComponent<RecoverUserName>().userNameInput.text;
		this.loginUserNameDown.gameObject.SetActive(false);
	}

	public void OnClearUserName(GameObject go)
	{
		if (this.utable.transform.childCount == 1)
		{
			this.loginUserNameDown.gameObject.SetActive(false);
		}
		User.RemoveUserName(RecoverUserName._ins.userNameInput.text);
		UnityEngine.Object.Destroy(go);
		this.utable.Reposition();
	}

	public void OnDownClick(GameObject go)
	{
		if (this.utable.transform.childCount == 0)
		{
			this.loginUserNameDown.gameObject.SetActive(false);
		}
		else
		{
			this.loginUserNameDown.gameObject.SetActive(true);
		}
	}

	private void ClearDown(GameObject ga = null)
	{
		if (this.loginUserNameDown != null)
		{
			this.loginUserNameDown.gameObject.SetActive(false);
		}
	}
}
