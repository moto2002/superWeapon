using Game.Network;
using NetWork.Layer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NetMgr : MonoBehaviour
{
	public class HttpMessage
	{
		internal int nOpCode;

		internal object body;

		internal ButtonClick btn_Click;
	}

	public static NetMgr inst;

	public static int resendList;

	public static int reqid;

	public static string session = string.Empty;

	public static int lastReqid = -1;

	public HttpMgr http;

	public CommunicationPanel WaitingCommunication;

	public CommunicationPanel SocketWaitingCommunication;

	public GameObject communicationPrefab;

	public DataHandler NetDataHandler;

	public NetSocket Net_Socket;

	public Queue<NetMgr.HttpMessage> messageQueue;

	private bool httpError;

	private Dictionary<int, NetMgr.HttpMessage> AllHttpMessage = new Dictionary<int, NetMgr.HttpMessage>();

	public bool HttpError
	{
		get
		{
			return this.httpError;
		}
		set
		{
			this.httpError = value;
		}
	}

	private void Awake()
	{
		NetMgr.inst = this;
		this.Net_Socket = NetSocket.CreateIntanse();
		this.NetDataHandler = new DataHandler();
		this.http = base.gameObject.AddComponent<HttpMgr>();
		this.messageQueue = new Queue<NetMgr.HttpMessage>();
		GameTools.DontDestroyOnLoad(base.gameObject);
		this.http.ErrorLogic = delegate(string errorContent)
		{
			HUDTextTool.inst.SetText(errorContent, HUDTextTool.TextUITypeEnum.Num5);
		};
	}

	private void Start()
	{
		base.StartCoroutine(this.Init());
	}

	[DebuggerHidden]
	private IEnumerator Init()
	{
		NetMgr.<Init>c__Iterator4C <Init>c__Iterator4C = new NetMgr.<Init>c__Iterator4C();
		<Init>c__Iterator4C.<>f__this = this;
		return <Init>c__Iterator4C;
	}

	public void SorcketConnect()
	{
		this.Net_Socket.ConnectNet(NetConst.ip, NetConst.port);
	}

	public void SendSorcket(short bodyCMD, object body)
	{
		this.Net_Socket.SendMessage(bodyCMD, body);
	}

	public void SendHttp(int nOpCode, object body, DataHandler.OpcodeHandler CallBack = null, ButtonClick _BtnClick = null)
	{
		if (this.httpError)
		{
			LogManage.LogError("httpError ~ ~ !!!        !!!              !!!");
			return;
		}
		if (CallBack != null)
		{
			ClientMgr.GetNet().NetDataHandler.AddCallBack(nOpCode, CallBack);
		}
		NetMgr.HttpMessage httpMessage = new NetMgr.HttpMessage();
		httpMessage.nOpCode = nOpCode;
		httpMessage.body = body;
		httpMessage.btn_Click = _BtnClick;
		if (_BtnClick)
		{
			_BtnClick.IsCanDoEvent = false;
		}
		if (httpMessage.nOpCode == 9000)
		{
			if (this.AllHttpMessage.ContainsKey(httpMessage.nOpCode))
			{
				this.AllHttpMessage[httpMessage.nOpCode] = httpMessage;
			}
			else
			{
				this.AllHttpMessage.Add(httpMessage.nOpCode, httpMessage);
			}
		}
		this.messageQueue.Enqueue(httpMessage);
	}

	public HTTPLoader SendHttpNoQueue(int nOpCode, object body, bool reconnection, DataHandler.OpcodeHandler CallBack = null)
	{
		if (!this.httpError || reconnection)
		{
			if (CallBack != null)
			{
				ClientMgr.GetNet().NetDataHandler.AddCallBack(nOpCode, CallBack);
			}
			this.ClearMessageQueue();
			return this.http.request(nOpCode.ToString(), body);
		}
		return null;
	}

	public void ClearMessageQueue()
	{
		this.messageQueue.Clear();
	}

	private void FixedUpdate()
	{
		if (this.http.httpSession != null && this.http.httpSession.allHttpRequst.Count == 0 && !this.httpError && this.messageQueue != null && this.messageQueue.Count > 0)
		{
			if (this.messageQueue.Count > 1)
			{
				LogManage.Log("MessageQueue.Count > 1");
			}
			NetMgr.HttpMessage httpMessage = this.messageQueue.Dequeue();
			if (httpMessage.nOpCode == 9000)
			{
				if (this.AllHttpMessage.ContainsKey(httpMessage.nOpCode))
				{
					NetMgr.HttpMessage httpMessage2 = this.AllHttpMessage[httpMessage.nOpCode];
					this.AllHttpMessage.Remove(httpMessage2.nOpCode);
					this.http.request(httpMessage2.nOpCode.ToString(), httpMessage2.body);
					LogManage.LogError(string.Format("cmd:{0} 消息 ", httpMessage2.nOpCode));
				}
			}
			else
			{
				this.http.request(httpMessage.nOpCode.ToString(), httpMessage.body);
				LogManage.LogError(string.Format("cmd:{0} 消息  ", httpMessage.nOpCode));
			}
		}
		if (this.Net_Socket != null)
		{
			if (this.Net_Socket.LsWorldPackage.Count > 0)
			{
				Packet packet = this.Net_Socket.LsWorldPackage.Dequeue();
				this.NetDataHandler.ReadData(packet.nOpCode, packet.kBody);
			}
			if (this.Net_Socket.ReceieveMsg.Count > 0)
			{
				string message = this.Net_Socket.ReceieveMsg.Dequeue();
				LogManage.LogError(message);
			}
		}
	}

	private void OnDestroy()
	{
		if (this.Net_Socket == null)
		{
			return;
		}
		this.Net_Socket.OnDestroy();
		NetMgr.inst = null;
	}

	public void OnDisable()
	{
		if (this.Net_Socket == null)
		{
			return;
		}
		this.Net_Socket.OnDestroy();
	}
}
