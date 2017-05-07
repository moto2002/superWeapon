using System;
using System.Collections.Generic;

namespace SimpleFramework.Manager
{
	public class NetworkManager : View
	{
		private SocketClient socket;

		private static Queue<KeyValuePair<int, ByteBuffer>> sEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

		private SocketClient SocketClient
		{
			get
			{
				if (this.socket == null)
				{
					this.socket = new SocketClient();
				}
				return this.socket;
			}
		}

		private void Awake()
		{
			this.Init();
		}

		private void Init()
		{
			this.SocketClient.OnRegister();
		}

		public void OnInit()
		{
			this.CallMethod("Start", new object[0]);
		}

		public void Unload()
		{
			this.CallMethod("Unload", new object[0]);
		}

		public object[] CallMethod(string func, params object[] args)
		{
			return Util.CallMethod("Network", func, args);
		}

		public static void AddEvent(int _event, ByteBuffer data)
		{
			NetworkManager.sEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
		}

		private void Update()
		{
			if (NetworkManager.sEvents.Count > 0)
			{
				while (NetworkManager.sEvents.Count > 0)
				{
					KeyValuePair<int, ByteBuffer> keyValuePair = NetworkManager.sEvents.Dequeue();
					base.facade.SendMessageCommand("DispatchMessage", keyValuePair);
				}
			}
		}

		public void SendConnect()
		{
			this.SocketClient.SendConnect();
		}

		public void SendMessage(ByteBuffer buffer)
		{
			this.SocketClient.SendMessage(buffer);
		}

		private void OnDestroy()
		{
			this.SocketClient.OnRemove();
			LogManage.Log("~NetworkManager was destroy");
		}
	}
}
