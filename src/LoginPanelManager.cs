using Game.Network;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoginPanelManager : MonoBehaviour
{
	public class ServerData
	{
		public enum ServerState
		{
			正常,
			火爆,
			爆满,
			新服推荐,
			隐藏,
			维护
		}

		public int id;

		public string serverName;

		public string serverIP;

		public LoginPanelManager.ServerData.ServerState serverState;

		public LoginPanelManager.ServerData.ServerState serverRealTimeStatus;

		public string serverStateText
		{
			get
			{
				if (this.serverRealTimeStatus == LoginPanelManager.ServerData.ServerState.维护)
				{
					return "维护";
				}
				switch (this.serverState)
				{
				case LoginPanelManager.ServerData.ServerState.正常:
					return "正常";
				case LoginPanelManager.ServerData.ServerState.火爆:
					return "火爆";
				case LoginPanelManager.ServerData.ServerState.爆满:
					return "爆满";
				case LoginPanelManager.ServerData.ServerState.新服推荐:
					return "新服推荐";
				case LoginPanelManager.ServerData.ServerState.隐藏:
					return "隐藏";
				case LoginPanelManager.ServerData.ServerState.维护:
					return "维护";
				default:
					return "正常";
				}
			}
		}
	}

	public GameObject LoginUserNamePanel;

	public GameObject LoginEnterGamePanel;

	public GameObject SelectServerPanel;

	public GameObject ExitGame;

	public GameObject serverItem;

	public GameObject serverItemLone;

	public UITable serverTable;

	public UITable serverTablelone;

	public static LoginPanelManager _instance;

	public string serverPerName;

	public UILabel ServerUILabel;

	public GameObject LastLoginGa1;

	public GameObject LastLoginGa2;

	public UILabel LastLoginID_Label1;

	public UILabel LastLoginID_Label2;

	public UILabel LastLoginName_Label1;

	public UILabel LastLoginName_Label2;

	private bool IsGetServer;

	public List<int> lastLoginList = new List<int>();

	public List<int> AreaLoginList = new List<int>();

	public Dictionary<int, LoginPanelManager.ServerData> ServerList = new Dictionary<int, LoginPanelManager.ServerData>();

	public List<UISprite> ChangeServerTeamList;

	public GameObject ChangeServerTeamPrefab;

	public UITable ChangeServerTeamTable;

	private JsonData jdItems;

	public int NowServer;

	public int NowServerTeamNum;

	public void OnDestroy()
	{
		LoginPanelManager._instance = null;
	}

	public void LateUpdate()
	{
		this.GetServer();
	}

	private void Awake()
	{
		Loading.senceType = SenceType.Login;
		LoginPanelManager._instance = this;
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_ExitGame, new EventManager.VoidDelegate(this.OnExitGame));
		EventManager.Instance.AddEvent(EventManager.EventType.LoginPanel_ChangeServerTeam, new EventManager.VoidDelegate(this.ChangeServerTeamBtnCallBack));
		if (User.GetUserNames() == null)
		{
			string text = SystemInfo.deviceUniqueIdentifier.Remove(0, SystemInfo.deviceUniqueIdentifier.Length - 10);
			LogManage.Log(SystemInfo.deviceUniqueIdentifier + " ..................." + text);
			User.SetUserName(text, text);
		}
	}

	private void Start()
	{
		this.GetServer();
	}

	public void GetServer()
	{
		if (PoolManage.Ins != null && PoolManage.Ins.loadLoginEnd && !this.IsGetServer && HttpMgr.inst != null && HttpMgr.inst.httpSession != null && HttpMgr.inst.httpSession.allHttpRequst.Count == 0)
		{
			LogManage.LogError("获取服务器列表 请求中··············································");
			HttpMgr.inst.httpSession = new HTTPSession(GameSetting.ServerList);
			WWWForm wWWForm = new WWWForm();
			wWWForm.AddField("platformId", GameSetting.userid);
			wWWForm.AddField("appid", GameSetting.appid);
			wWWForm.AddField("channelId", GameSetting.ChannelId);
			wWWForm.AddField("token", GameSetting.Token);
			wWWForm.AddField("clientVersion", GameSetting.Version);
			HttpMgr.inst.httpSession.onDataError = new Action<string, Action>(this.Eror);
			LogManage.LogError(string.Format("请求的url: {0} channelID :{1}", HttpMgr.inst.httpSession.StrURL, GameSetting.ChannelId));
			HttpMgr.inst.httpSession.SendPOST(wWWForm, new Action<Dictionary<string, string>, string>(this.CallBack), new IHttpSession.DownLoadTimeWaiting(HttpMgr.inst.DownLoadTimeWaiting), new IHttpSession.DownLoadTimeOut(this.DownLoadTimeOut));
		}
	}

	public void LogionSucess()
	{
		HTTPSession hTTPSession = new HTTPSession(GameSetting.ServerListSuccess);
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("platformId", HeroInfo.GetInstance().platformId);
		wWWForm.AddField("channelId", HeroInfo.GetInstance().channelId);
		wWWForm.AddField("time", HeroInfo.GetInstance().time);
		wWWForm.AddField("token", HeroInfo.GetInstance().token);
		wWWForm.AddField("gameAccount", HeroInfo.GetInstance().account);
		wWWForm.AddField("area", User.GetServerName());
		hTTPSession.onDataError = new Action<string, Action>(this.Eror);
		hTTPSession.SendPOST(wWWForm, new Action<Dictionary<string, string>, string>(this.NullBack), null, null);
	}

	private void NullBack(Dictionary<string, string> aa, string bb)
	{
	}

	private void DownLoadTimeOut(Action Resend)
	{
		ClientMgr.GetNet().HttpError = true;
		if (CommunicationPanel._Inst)
		{
			CommunicationPanel._Inst.SetActiveFalse();
		}
		MessageBox.GetNetMessagePanel().ShowBtn(string.Empty, LanguageManage.GetTextByKey("您当前的网络环境不太好，请重试或者重新登陆游戏！", "client_Res"), LanguageManage.GetTextByKey("重新登陆", "client_Res"), new Action(HttpMgr.ReStartGame));
	}

	private void Eror(string content, Action Resend)
	{
		ClientMgr.GetNet().HttpError = true;
		if (CommunicationPanel._Inst)
		{
			CommunicationPanel._Inst.SetActiveFalse();
		}
		MessageBox.GetNetMessagePanel().ShowBtn(string.Empty, LanguageManage.GetTextByKey("您当前的网络环境不太好，请重试或者重新登陆游戏！", "client_Res"), LanguageManage.GetTextByKey("重新登陆", "client_Res"), new Action(HttpMgr.ReStartGame));
	}

	private void ChangeServerTeamBtnCallBack(GameObject ga)
	{
		for (int i = 0; i < this.ChangeServerTeamList.Count; i++)
		{
			this.ChangeServerTeamList[i].fillAmount = 0f;
		}
		this.NowServerTeamNum = int.Parse(ga.name);
		this.ChangeServerTeamList[this.NowServerTeamNum - 1].fillAmount = 1f;
		this.CreateServerList();
	}

	private void CallBack(Dictionary<string, string> aa, string opcode)
	{
		if (ClientMgr.GetNet().WaitingCommunication)
		{
			ClientMgr.GetNet().WaitingCommunication.SetActiveFalse();
		}
		JsonData jsonData = JsonMapper.ToObject(opcode);
		LogManage.LogError(string.Format("下发的数据有:{0}", opcode));
		try
		{
			JsonData jsonData2 = JsonMapper.ToObject(jsonData["lastLogin"].ToString());
			Debug.Log("已登录服务器:长度：" + jsonData2.Count);
			this.lastLoginList.Clear();
			for (int i = 0; i < jsonData2.Count; i++)
			{
				int num = int.Parse(jsonData2[i].ToString());
				if (i == 0)
				{
					this.NowServer = num;
				}
				this.lastLoginList.Add(num);
			}
		}
		catch (Exception ex)
		{
			LogManage.LogError("lastLogin 相关数据 没有下发~~  ex:" + ex.ToString());
		}
		this.LastLoginGa1.gameObject.SetActive(false);
		this.LastLoginGa2.gameObject.SetActive(false);
		try
		{
			JsonData jsonData3 = JsonMapper.ToObject(jsonData["loginAreas"].ToString());
			Debug.Log("存在角色服务器:长度：" + jsonData3.Count);
			this.AreaLoginList.Clear();
			for (int j = 0; j < jsonData3.Count; j++)
			{
				int item = int.Parse(jsonData3[j].ToString());
				this.AreaLoginList.Add(item);
			}
		}
		catch (Exception ex2)
		{
			LogManage.LogError("loginAreas 相关数据 没有下发~~  ex:" + ex2.ToString());
		}
		try
		{
			this.jdItems = JsonMapper.ToObject(jsonData["serverList"].ToString());
			this.IsGetServer = (this.jdItems.Count > 0);
			if (this.IsGetServer)
			{
				for (int k = 0; k < this.jdItems.Count; k++)
				{
					LoginPanelManager.ServerData serverData = new LoginPanelManager.ServerData();
					serverData.id = int.Parse(this.jdItems[k]["areaId"].ToString());
					serverData.serverName = this.jdItems[k]["areaName"].ToString();
					serverData.serverIP = this.jdItems[k]["areaIP"].ToString();
					serverData.serverState = (LoginPanelManager.ServerData.ServerState)int.Parse(this.jdItems[k]["gmSetStatus"].ToString());
					serverData.serverRealTimeStatus = (LoginPanelManager.ServerData.ServerState)int.Parse(this.jdItems[k]["realTimeStatus"].ToString());
					if (!this.ServerList.ContainsKey(serverData.id))
					{
						this.ServerList.Add(serverData.id, serverData);
					}
				}
			}
			this.serverTablelone.DestoryChildren(true);
			LogManage.LogError("获取服务器列表数据 加载中··············································" + this.jdItems.Count);
			LoginEnterGame._instance.changeServerGa.SetActive(this.jdItems.Count > 1);
			if (this.jdItems.Count == 0)
			{
				this.GetServer();
				return;
			}
			int num2 = (int)((float)this.jdItems.Count / 10f - 0.1f) + 1;
			this.ChangeServerTeamList.Clear();
			for (int l = 0; l < num2; l++)
			{
				GameObject gameObject = NGUITools.AddChild(this.ChangeServerTeamTable.gameObject, this.ChangeServerTeamPrefab);
				gameObject.transform.localScale = Vector3.one;
				gameObject.name = (l + 1).ToString();
				gameObject.transform.FindChild("FirstNum").GetComponent<UILabel>().text = (l * 10 + 1).ToString();
				gameObject.transform.FindChild("SecondNum").GetComponent<UILabel>().text = ((l + 1) * 10).ToString();
				this.ChangeServerTeamList.Add(gameObject.GetComponent<UISprite>());
			}
			for (int m = 1; m < 100; m++)
			{
				if (this.NowServer >= (m - 1) * 10 && this.NowServer <= m * 10)
				{
					this.NowServerTeamNum = m;
					this.ChangeServerTeamList[m - 1].fillAmount = 1f;
					break;
				}
			}
			this.CreateServerList();
		}
		catch (Exception ex3)
		{
			LogManage.LogError("serverList 相关数据 没有下发~~  ex:" + ex3.ToString());
		}
		try
		{
			HeroInfo.GetInstance().token = jsonData["token"].ToString();
			HeroInfo.GetInstance().time = jsonData["time"].ToString();
			HeroInfo.GetInstance().platformId = jsonData["platformId"].ToString();
			HeroInfo.GetInstance().channelId = jsonData["channelId"].ToString();
			HeroInfo.GetInstance().account = jsonData["gameAccount"].ToString();
			LogManage.LogError(string.Format("token:{0}  time:{1}  platformId:{2}  channelId:{3}", new object[]
			{
				HeroInfo.GetInstance().token,
				HeroInfo.GetInstance().time,
				HeroInfo.GetInstance().platformId,
				HeroInfo.GetInstance().channelId
			}));
		}
		catch (Exception var_16_580)
		{
			LogManage.LogError("SDK 相关数据 没有下发~~ ");
		}
		try
		{
			JsonData jsonData4 = JsonMapper.ToObject(jsonData["notice"].ToString());
			if (jsonData4.Count > 0 && GameStartNotice._inst == null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("UI/GameStartNotice"), Vector3.zero, Quaternion.identity) as GameObject;
				gameObject2.transform.parent = base.transform.parent;
				gameObject2.transform.localScale = Vector3.one;
				List<string> list = new List<string>();
				for (int n = 0; n < jsonData4.Count; n++)
				{
					list.Add(jsonData4[n]["content"].ToString());
				}
				GameStartNotice component = gameObject2.GetComponent<GameStartNotice>();
				component.InitData(list);
			}
		}
		catch (Exception var_22_666)
		{
			LogManage.LogError("notice 相关数据 没有下发~~ ");
		}
	}

	private void CreateServerList()
	{
		this.serverTablelone.DestoryChildren(true);
		int num = -1;
		if (this.lastLoginList.Count > 0)
		{
			num = this.lastLoginList[0];
		}
		else if (this.ServerList.Count > 0)
		{
			LoginPanelManager.ServerData serverData = this.ServerList.Values.FirstOrDefault((LoginPanelManager.ServerData a) => a.serverRealTimeStatus != LoginPanelManager.ServerData.ServerState.维护 && a.serverState == LoginPanelManager.ServerData.ServerState.新服推荐);
			if (serverData == null)
			{
				num = (from a in this.ServerList.Values
				where a.serverRealTimeStatus != LoginPanelManager.ServerData.ServerState.维护
				select a).Max((LoginPanelManager.ServerData a) => a.id);
			}
			else
			{
				num = serverData.id;
			}
		}
		for (int i = 0; i < this.jdItems.Count; i++)
		{
			int num2 = int.Parse(this.jdItems[i]["areaId"].ToString());
			if (num2 >= (this.NowServerTeamNum - 1) * 10 + 1 && num2 <= this.NowServerTeamNum * 10)
			{
				GameObject gameObject = NGUITools.AddChild(this.serverTablelone.gameObject, this.serverItemLone);
				ServerItemLoneMana component = gameObject.GetComponent<ServerItemLoneMana>();
				component.showServerName.text = this.jdItems[i]["areaName"].ToString();
				component.idLabel.text = this.jdItems[i]["areaId"].ToString();
				component.areaIP = this.jdItems[i]["areaIP"].ToString();
				component.id = int.Parse(this.jdItems[i]["areaId"].ToString());
				if (this.lastLoginList.Contains(component.id))
				{
					if (!this.LastLoginGa1.gameObject.activeSelf)
					{
						this.LastLoginGa1.gameObject.SetActive(true);
						this.LastLoginID_Label1.text = this.jdItems[i]["areaId"].ToString();
						this.LastLoginName_Label1.text = this.jdItems[i]["areaName"].ToString();
					}
					else if (!this.LastLoginGa2.gameObject.activeSelf)
					{
						this.LastLoginGa2.gameObject.SetActive(true);
						this.LastLoginID_Label2.text = this.jdItems[i]["areaId"].ToString();
						this.LastLoginName_Label2.text = this.jdItems[i]["areaName"].ToString();
					}
				}
				if (num == component.id)
				{
					User.SetIP(component.areaIP);
					User.SetServerName(component.id);
					ClientMgr.GetNet().http.httpSession.ChangeUrl(component.areaIP);
					LoginEnterGame.ServerName = this.jdItems[i]["areaName"].ToString();
					if (LoginEnterGame._instance)
					{
						LoginEnterGame._instance.serverName.text = LoginEnterGame.ServerName;
					}
				}
				component.serverState.gameObject.SetActive(true);
				component.RoleSprite.gameObject.SetActive(false);
				if (this.AreaLoginList.Contains(component.id))
				{
					component.serverState.gameObject.SetActive(false);
					component.RoleSprite.gameObject.SetActive(true);
				}
				try
				{
					if (this.jdItems[i]["realTimeStatus"] != null && int.Parse(this.jdItems[i]["realTimeStatus"].ToString()) == 5)
					{
						component.serverState.text = "维护";
					}
					else if (this.jdItems[i]["gmSetStatus"] != null)
					{
						switch (int.Parse(this.jdItems[i]["gmSetStatus"].ToString()))
						{
						case 0:
							component.serverState.text = "正常";
							break;
						case 1:
							component.serverState.text = "火爆";
							break;
						case 2:
							component.serverState.text = "爆满";
							break;
						case 3:
							component.serverState.text = "新服推荐";
							break;
						case 4:
							component.serverState.text = "隐藏";
							break;
						case 5:
							component.serverState.text = "维护";
							break;
						}
					}
				}
				catch (Exception var_7_507)
				{
					LogManage.LogError("不包含   gmSetStatus  Or realTimeStatus");
				}
				this.serverTablelone.Reposition();
			}
		}
	}

	public void OnExitGame(GameObject go)
	{
		HDSDKInit.ExitGame();
	}
}
