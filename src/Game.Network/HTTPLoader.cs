using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Game.Network
{
	public class HTTPLoader : MonoBehaviour
	{
		private enum STATE
		{
			START,
			STOP,
			CLOSE
		}

		private HTTPLoader.STATE m_eState;

		private float timeOut = 0.2f;

		private int timeMaxOut = 10;

		private bool isDownloadingMinTime;

		private bool isDownloadingMaxTime;

		private DateTime startTime;

		[SerializeField]
		private int cmd;

		public string URL;

		private WWWForm mForm;

		private byte[] mByteData;

		private Dictionary<string, string> mHeaders;

		private Action<string, Action> mError_callback;

		private Action<Dictionary<string, string>, Opcode> mCallbackData_Object;

		private Action<Dictionary<string, string>, string> mCallbackData_String;

		private IHttpSession.DownLoadTimeWaiting DownLoadTimeWaiting;

		private IHttpSession.DownLoadTimeOut DownLoadTimeOut;

		[SerializeField]
		private int reSendCount = 5;

		public float TimeOut
		{
			get
			{
				return this.timeOut;
			}
			set
			{
				this.timeOut = value;
			}
		}

		public int TimeMaxOut
		{
			get
			{
				return this.timeMaxOut;
			}
			set
			{
				this.timeMaxOut = value;
			}
		}

		public void OnDestroy()
		{
			if (HttpMgr.inst != null && HttpMgr.inst.httpSession != null)
			{
				HttpMgr.inst.httpSession.allHttpRequst.Remove(this);
			}
		}

		internal static HTTPLoader GoWWW(string url, WWWForm form, byte[] byteData, Dictionary<string, string> headers, Action<string, Action> error_callback, Action<Dictionary<string, string>, Opcode> callbackData, IHttpSession.DownLoadTimeWaiting DownLoadTimeWaiting, IHttpSession.DownLoadTimeOut downLoadTimeOut)
		{
			HTTPLoader hTTPLoader = new GameObject().AddComponent<HTTPLoader>();
			hTTPLoader.m_eState = HTTPLoader.STATE.START;
			hTTPLoader.DownLoadTimeWaiting = DownLoadTimeWaiting;
			hTTPLoader.DownLoadTimeOut = downLoadTimeOut;
			hTTPLoader.StartCoroutine(hTTPLoader.StartHTTP(url, form, byteData, headers, error_callback, callbackData));
			return hTTPLoader;
		}

		internal static HTTPLoader GoWWW(string url, WWWForm form, byte[] byteData, Dictionary<string, string> headers, Action<string, Action> error_callback, Action<Dictionary<string, string>, string> callbackData, IHttpSession.DownLoadTimeWaiting DownLoadTimeWaiting, IHttpSession.DownLoadTimeOut downLoadTimeOut)
		{
			HTTPLoader hTTPLoader = new GameObject().AddComponent<HTTPLoader>();
			hTTPLoader.m_eState = HTTPLoader.STATE.START;
			hTTPLoader.DownLoadTimeWaiting = DownLoadTimeWaiting;
			hTTPLoader.DownLoadTimeOut = downLoadTimeOut;
			hTTPLoader.StartCoroutine(hTTPLoader.StartHTTP(url, form, byteData, headers, error_callback, callbackData));
			return hTTPLoader;
		}

		internal void ReSend()
		{
			Time.timeScale = 1f;
			this.isDownloadingMinTime = false;
			this.isDownloadingMaxTime = false;
			if (NewbieGuidePanel._instance)
			{
				NewbieGuidePanel._instance.HideSelf();
			}
			CSReconnection cSReconnection = new CSReconnection();
			cSReconnection.session = NetMgr.session;
			LogManage.LogError("ReSend ReSend ReSend ::    " + NetMgr.session);
			FuncUIManager.inst.ClearAllUIPanel();
			UIManager.curState = SenceState.Home;
			this.Close();
			Loading.IsRefreshSence = true;
			HTTPLoader hTTPLoader = ClientMgr.GetNet().SendHttpNoQueue(1004, cSReconnection, true, new DataHandler.OpcodeHandler(LoginHandler.GC_H_Connect));
		}

		internal void ReSendInCount()
		{
			base.StopAllCoroutines();
			ClientMgr.GetNet().HttpError = false;
			this.m_eState = HTTPLoader.STATE.START;
			if (this.mCallbackData_Object != null)
			{
				base.StartCoroutine(this.StartHTTP(this.URL, this.mForm, this.mByteData, this.mHeaders, this.mError_callback, this.mCallbackData_Object));
			}
			if (this.mCallbackData_String != null)
			{
				base.StartCoroutine(this.StartHTTP(this.URL, this.mForm, this.mByteData, this.mHeaders, this.mError_callback, this.mCallbackData_String));
			}
		}

		internal void Close()
		{
			this.m_eState = HTTPLoader.STATE.CLOSE;
		}

		[DebuggerHidden]
		internal IEnumerator StartHTTP(string url, WWWForm form, byte[] byteData, Dictionary<string, string> headers, Action<string, Action> error_callback, Action<Dictionary<string, string>, Opcode> callbackData)
		{
			HTTPLoader.<StartHTTP>c__Iterator4A <StartHTTP>c__Iterator4A = new HTTPLoader.<StartHTTP>c__Iterator4A();
			<StartHTTP>c__Iterator4A.url = url;
			<StartHTTP>c__Iterator4A.form = form;
			<StartHTTP>c__Iterator4A.byteData = byteData;
			<StartHTTP>c__Iterator4A.headers = headers;
			<StartHTTP>c__Iterator4A.error_callback = error_callback;
			<StartHTTP>c__Iterator4A.callbackData = callbackData;
			<StartHTTP>c__Iterator4A.<$>url = url;
			<StartHTTP>c__Iterator4A.<$>form = form;
			<StartHTTP>c__Iterator4A.<$>byteData = byteData;
			<StartHTTP>c__Iterator4A.<$>headers = headers;
			<StartHTTP>c__Iterator4A.<$>error_callback = error_callback;
			<StartHTTP>c__Iterator4A.<$>callbackData = callbackData;
			<StartHTTP>c__Iterator4A.<>f__this = this;
			return <StartHTTP>c__Iterator4A;
		}

		[DebuggerHidden]
		internal IEnumerator StartHTTP(string url, WWWForm form, byte[] byteData, Dictionary<string, string> headers, Action<string, Action> error_callback, Action<Dictionary<string, string>, string> callbackData)
		{
			HTTPLoader.<StartHTTP>c__Iterator4B <StartHTTP>c__Iterator4B = new HTTPLoader.<StartHTTP>c__Iterator4B();
			<StartHTTP>c__Iterator4B.url = url;
			<StartHTTP>c__Iterator4B.form = form;
			<StartHTTP>c__Iterator4B.byteData = byteData;
			<StartHTTP>c__Iterator4B.headers = headers;
			<StartHTTP>c__Iterator4B.error_callback = error_callback;
			<StartHTTP>c__Iterator4B.callbackData = callbackData;
			<StartHTTP>c__Iterator4B.<$>url = url;
			<StartHTTP>c__Iterator4B.<$>form = form;
			<StartHTTP>c__Iterator4B.<$>byteData = byteData;
			<StartHTTP>c__Iterator4B.<$>headers = headers;
			<StartHTTP>c__Iterator4B.<$>error_callback = error_callback;
			<StartHTTP>c__Iterator4B.<$>callbackData = callbackData;
			<StartHTTP>c__Iterator4B.<>f__this = this;
			return <StartHTTP>c__Iterator4B;
		}

		private void FixedUpdate()
		{
			if (this.m_eState == HTTPLoader.STATE.START && this.isDownloadingMinTime && this.cmd != 9000 && this.cmd != 5004 && (DateTime.Now - this.startTime).TotalSeconds > (double)this.timeOut)
			{
				if (this.DownLoadTimeWaiting != null)
				{
					this.DownLoadTimeWaiting(this.cmd);
				}
				this.isDownloadingMinTime = false;
			}
			if ((this.m_eState == HTTPLoader.STATE.START || this.m_eState == HTTPLoader.STATE.STOP) && this.isDownloadingMaxTime && (DateTime.Now - this.startTime).TotalSeconds > (double)this.timeMaxOut)
			{
				base.StopAllCoroutines();
				if (this.DownLoadTimeOut != null)
				{
					this.DownLoadTimeOut(new Action(this.ReSendInCount));
				}
				this.isDownloadingMaxTime = false;
			}
		}

		private void Awake()
		{
			base.gameObject.name = "HttpLoader";
			GameTools.DontDestroyOnLoad(base.gameObject);
			HttpMgr.inst.httpSession.allHttpRequst.Add(this);
		}
	}
}
