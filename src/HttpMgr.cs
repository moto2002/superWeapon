using DG.Tweening;
using Game.Network;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HttpMgr : MonoBehaviour
{
	public static HttpMgr inst;

	public bool isShowLoading = true;

	private Dictionary<string, string> headers = new Dictionary<string, string>();

	public HTTPSession httpSession;

	public Action<string> ErrorLogic;

	private int downCmd;

	private int upCmd;

	private int errorid;

	public int Errorid
	{
		get
		{
			return this.errorid;
		}
		set
		{
			this.errorid = value;
		}
	}

	private void Awake()
	{
		HttpMgr.inst = this;
		this.headers.Add("cmd", string.Empty);
		this.headers.Add("user", string.Empty);
		this.headers.Add("reqid", string.Empty);
		this.headers.Add("session", string.Empty);
		this.headers.Add("Expect", string.Empty);
	}

	public HTTPLoader request(string nOpCode, object body)
	{
		HTTPLoader result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			Opcode.Serialize(memoryStream, body);
			this.headers["cmd"] = nOpCode;
			this.headers["user"] = HeroInfo.GetInstance().userId.ToString();
			this.headers["reqid"] = NetMgr.reqid.ToString();
			this.headers["session"] = NetMgr.session;
			this.headers["Expect"] = string.Empty;
			this.httpSession.m_cHeader = this.headers;
			this.httpSession.onDataError = new Action<string, Action>(this.ErrWWW);
			result = this.httpSession.SendBYTE(memoryStream.ToArray(), new Action<Dictionary<string, string>, Opcode>(this.DownDataCallback), new IHttpSession.DownLoadTimeWaiting(this.DownLoadTimeWaiting), new IHttpSession.DownLoadTimeOut(this.DownLoadTimeOut));
		}
		return result;
	}

	private void DownDataCallback(Dictionary<string, string> _headers, Opcode opcode)
	{
		if (ClientMgr.GetNet().WaitingCommunication)
		{
			ClientMgr.GetNet().WaitingCommunication.SetActiveFalse();
		}
		this.downCmd = int.Parse(_headers["CMD"]);
		if (this.downCmd != 9000 && this.downCmd != 1500)
		{
			NetMgr.reqid++;
		}
		bool isError = false;
		if (_headers.ContainsKey("CODE") && !_headers["CODE"].Equals("200"))
		{
			isError = true;
			this.Errorid = int.Parse(_headers["CODE"]);
			Debug.LogError("错误码：" + this.Errorid);
			if (ErrorServer.ErrorServerString.ContainsKey(_headers["CODE"]))
			{
				if (this.ErrorLogic != null)
				{
					if (SenceInfo.battleResource == SenceInfo.BattleResource.WorldMap && int.Parse(_headers["CODE"]) == 100)
					{
						HUDTextTool.inst.SetText("金币不足", HUDTextTool.TextUITypeEnum.Num5);
					}
					else
					{
						this.ErrorLogic(string.Format("{0}", LanguageManage.GetTextByKey(ErrorServer.ErrorServerString[_headers["CODE"]], "ErrorMassage")));
					}
				}
			}
			else if (int.Parse(_headers["CODE"]) == 1600)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("目前没有符合匹配的对手", "others"), HUDTextTool.TextUITypeEnum.Num5);
			}
			else
			{
				HUDTextTool.inst.SetText("不包含" + _headers["CODE"] + "错误码的提示", HUDTextTool.TextUITypeEnum.Num5);
			}
		}
		else
		{
			this.Errorid = 0;
		}
		if (_headers.ContainsKey("CODE") && (_headers["CODE"].Equals("409") || _headers["CODE"].Equals("410")))
		{
			UnityEngine.Object.Destroy(ClientMgr.GetNet().gameObject);
			MessageBox.GetNetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("掉线", "others"), LanguageManage.GetTextByKey("已掉线 需重新登录", "others"), LanguageManage.GetTextByKey("确定", "others"), new Action(HttpMgr.ReStartGame));
			return;
		}
		if (_headers.ContainsKey("CODE") && _headers["CODE"].Equals("411"))
		{
			UnityEngine.Object.Destroy(ClientMgr.GetNet().gameObject);
			Debug.Log("消除特写摄像机");
			if (Camera_FingerManager.inst.dragCamera != null)
			{
				UnityEngine.Object.Destroy(Camera_FingerManager.inst.dragCamera.gameObject);
			}
			MessageBox.GetNetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("异地登录", "others"), LanguageManage.GetTextByKey("您的账户异地登录，是否重新登录", "others"), LanguageManage.GetTextByKey("确定", "others"), new Action(HttpMgr.ReStartGame), LanguageManage.GetTextByKey("取消", "others"), new Action(HDSDKInit.ExitGame));
			return;
		}
		if (this.downCmd == 0)
		{
			this.downCmd = this.upCmd;
			LogManage.LogError("下行CMD 是 0 ");
		}
		try
		{
			ClientMgr.GetNet().NetDataHandler.ReadData(this.downCmd, opcode);
		}
		catch (Exception ex)
		{
			if (GameSetting.isEditor)
			{
				LogManage.LogError("客户端读取数据错误: " + ex.ToString());
			}
		}
		try
		{
			ClientMgr.GetNet().NetDataHandler.CallBack(this.downCmd, isError, opcode);
		}
		catch (Exception ex2)
		{
			if (GameSetting.isEditor)
			{
				LogManage.LogError("客户端处理数据错误: " + ex2.ToString());
			}
		}
	}

	public static void ReStartGame()
	{
		Time.timeScale = 0f;
		Loading.IsRefreshSence = true;
		Loading.senceName = "null";
		Loading.IslandSenceName = string.Empty;
		Loading.senceType = SenceType.Login;
		SenceInfo.battleResource = SenceInfo.BattleResource.HOME;
		DOTween.Clear(false);
		ButtonClick.newbiLock = false;
		Camera_FingerManager.newbiLock = false;
		NewbieGuidePanel.isEnemyAttck = true;
		if (AudioManage.inst)
		{
			AudioManage.inst.StopAudio();
		}
		if (PoolManage.Ins)
		{
			GameTools.RemoveChilderns(PoolManage.Ins.TmpPool_Del);
		}
		NewbieGuidePanel.curGuideIndex = -1;
		NewbieGuideWrap.nextNewBi = string.Empty;
		NewbieGuideWrap.CurNewBiFuncName = string.Empty;
		NewbieGuidePanel.guideIdByServer = 0;
		UIManager.curState = SenceState.None;
		for (int i = 0; i < GameStartLoadSync.AllNeedDelInReStartGame.Count; i++)
		{
			UnityEngine.Object.Destroy(GameStartLoadSync.AllNeedDelInReStartGame[i]);
		}
		GameStartLoadSync.AllNeedDelInReStartGame.Clear();
		AppFacade.ClearData_Client();
		Facade.ClearData();
		Loading.changeSence.Clear();
		HeroInfo.GetInstance().ClearData();
		ButtonClick.AllButtonClick.Clear();
		UnitConst.Clear();
		Application.LoadLevelAsync(0);
		Time.timeScale = 1f;
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

	public void DownLoadTimeWaiting(int cmd)
	{
		if (CommunicationPanel._Inst)
		{
			CommunicationPanel._Inst.SetActiveTrue();
		}
	}

	private void ErrWWW(string errContent, Action Resend)
	{
		LogManage.Log(errContent);
		ClientMgr.GetNet().ClearMessageQueue();
		ClientMgr.GetNet().HttpError = true;
		NewbieGuidePanel._instance.HideSelf();
		ClientMgr.GetNet().HttpError = true;
		if (CommunicationPanel._Inst)
		{
			CommunicationPanel._Inst.SetActiveFalse();
		}
		MessageBox.GetNetMessagePanel().ShowBtn(string.Empty, LanguageManage.GetTextByKey("您当前的网络环境不太好，请重试或者重新登陆游戏！", "client_Res"), LanguageManage.GetTextByKey("重新登陆", "client_Res"), new Action(HttpMgr.ReStartGame));
	}
}
