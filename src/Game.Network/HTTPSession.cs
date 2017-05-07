using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Network
{
	public class HTTPSession
	{
		private string m_strURL = string.Empty;

		public Action<string, Action> onDataError;

		public Dictionary<string, string> m_cHeader;

		public List<HTTPLoader> allHttpRequst = new List<HTTPLoader>();

		public string StrURL
		{
			get
			{
				return this.m_strURL;
			}
		}

		public HTTPSession(string url)
		{
			this.m_strURL = url;
		}

		public void ChangeUrl(string url)
		{
			this.m_strURL = url;
		}

		public HTTPLoader SendGET(Action<Dictionary<string, string>, Opcode> callbackData = null, IHttpSession.DownLoadTimeWaiting DownLoadTimeWaiting = null, IHttpSession.DownLoadTimeOut downLoadTimeOut = null)
		{
			return HTTPLoader.GoWWW(this.m_strURL, null, null, this.m_cHeader, this.onDataError, callbackData, DownLoadTimeWaiting, downLoadTimeOut);
		}

		public HTTPLoader SendPOST(WWWForm packet, Action<Dictionary<string, string>, Opcode> callbackData = null, IHttpSession.DownLoadTimeWaiting DownLoadTimeWaiting = null, IHttpSession.DownLoadTimeOut downLoadTimeOut = null)
		{
			return HTTPLoader.GoWWW(this.m_strURL, packet, null, this.m_cHeader, this.onDataError, callbackData, DownLoadTimeWaiting, downLoadTimeOut);
		}

		public HTTPLoader SendPOST(WWWForm packet, Action<Dictionary<string, string>, string> callbackData = null, IHttpSession.DownLoadTimeWaiting DownLoadTimeWaiting = null, IHttpSession.DownLoadTimeOut downLoadTimeOut = null)
		{
			return HTTPLoader.GoWWW(this.m_strURL, packet, null, this.m_cHeader, this.onDataError, callbackData, DownLoadTimeWaiting, downLoadTimeOut);
		}

		public HTTPLoader SendBYTE(byte[] packet, Action<Dictionary<string, string>, Opcode> callbackData = null, IHttpSession.DownLoadTimeWaiting DownLoadTimeWaiting = null, IHttpSession.DownLoadTimeOut downLoadTimeOut = null)
		{
			return HTTPLoader.GoWWW(this.m_strURL, null, packet, this.m_cHeader, this.onDataError, callbackData, DownLoadTimeWaiting, downLoadTimeOut);
		}
	}
}
