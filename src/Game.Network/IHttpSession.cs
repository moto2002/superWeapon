using System;

namespace Game.Network
{
	public abstract class IHttpSession
	{
		public delegate void DownLoadTimeOut(Action ReSend);

		public delegate void DownLoadTimeWaiting(int cmd);

		public virtual void SendGET<T>(object packet, Action<T> callback = null, IHttpSession.DownLoadTimeOut process = null) where T : class
		{
		}

		public abstract void SendPOST<T>(object packet, Action<T> callback = null, IHttpSession.DownLoadTimeOut process = null) where T : class;

		public abstract void SendBYTE<T>(object packet, Action<T> callback = null, IHttpSession.DownLoadTimeOut process = null) where T : class;
	}
}
