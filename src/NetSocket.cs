using msg;
using NetWork.Layer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

public class NetSocket
{
	private static NetSocket inst;

	private IPAddress m_kIPAddress;

	private IPEndPoint m_kIPEndPoint;

	private static Socket clientSocket;

	private int m_nMillisecondsTimeOut = 6000;

	private Thread m_hReceiveThread;

	public NetErrorDelegate NetErrorFunc;

	private string _IP;

	private int Port;

	private bool isHavedRegister;

	private Queue<Packet> m_lsWorldPackage = new Queue<Packet>();

	private static Queue<string> receieveMsg = new Queue<string>();

	private byte[] aTempByte;

	public Queue<Packet> LsWorldPackage
	{
		get
		{
			return this.m_lsWorldPackage;
		}
	}

	public Queue<string> ReceieveMsg
	{
		get
		{
			return NetSocket.receieveMsg;
		}
	}

	private NetSocket()
	{
	}

	public static NetSocket CreateIntanse()
	{
		NetSocket.inst = new NetSocket();
		return NetSocket.inst;
	}

	public void OnDestroy()
	{
		this.ReceieveMsg.Enqueue("NetSocket。。。。。。。。。。。。OnDestroy");
		this.CloseSocket();
		this.NetErrorFunc = null;
		if (!object.ReferenceEquals(this.m_hReceiveThread, null))
		{
			try
			{
				this.m_hReceiveThread.Abort();
				this.m_hReceiveThread.Join();
				this.m_hReceiveThread = null;
			}
			catch (Exception var_0_50)
			{
				this.m_hReceiveThread = null;
			}
		}
		NetSocket.inst = null;
	}

	public static void ResetSocket()
	{
	}

	public void Reset()
	{
		this.m_lsWorldPackage.Clear();
		NetSocket.receieveMsg.Clear();
		this.aTempByte = null;
		this.isHavedRegister = false;
	}

	public void ConnectNet(string ip, int port)
	{
		this._IP = ip;
		this.Port = port;
		this.m_kIPAddress = IPAddress.Parse(this._IP);
		this.m_kIPEndPoint = new IPEndPoint(this.m_kIPAddress, this.Port);
		this.NetErrorFunc = new NetErrorDelegate(this.ReStart);
		this.ConnectMain();
	}

	private void heartTime_Elapsed(object sender, ElapsedEventArgs e)
	{
		this.SocketHeart();
	}

	private void ConnectMain()
	{
		IPAddress[] hostAddresses = Dns.GetHostAddresses(this._IP);
		AddressFamily addressFamily = AddressFamily.InterNetwork;
		for (int i = 0; i < hostAddresses.Length; i++)
		{
			if (!object.ReferenceEquals(hostAddresses[i], null))
			{
				this.ReceieveMsg.Enqueue(hostAddresses[i].AddressFamily.ToString());
				if (hostAddresses[i].AddressFamily == AddressFamily.InterNetworkV6)
				{
					addressFamily = AddressFamily.InterNetworkV6;
					break;
				}
			}
		}
		this.ReceieveMsg.Enqueue(string.Format("Socket Connecting: addressType:{0} IP:{1} Port:{2}", addressFamily, this._IP, this.Port));
		NetSocket.clientSocket = new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp);
		NetSocket.clientSocket.BeginConnect(this.m_kIPEndPoint, new AsyncCallback(this.ConnectCallback), NetSocket.clientSocket);
	}

	private void ConnectCallback(IAsyncResult asyncConnect)
	{
		if (!object.ReferenceEquals(asyncConnect, null))
		{
			Socket socket = asyncConnect.AsyncState as Socket;
			if (!object.ReferenceEquals(socket, null) && socket.Connected)
			{
				this.ReceieveMsg.Enqueue("ConnectCallback ·· Success");
				this.SendMessage(30002, new CSSocketRegister
				{
					playerId = HeroInfo.GetInstance().userId
				}, new Action<IAsyncResult>(this.RegisterCallback));
				return;
			}
		}
		this.ReceieveMsg.Enqueue("ConnectCallback ·· TimeOut...........");
		this.CloseSocket();
		if (!object.ReferenceEquals(this.NetErrorFunc, null))
		{
			this.NetErrorFunc(null);
		}
	}

	private void RegisterCallback(IAsyncResult asyncConnect)
	{
		if (!object.ReferenceEquals(asyncConnect, null))
		{
			Socket socket = asyncConnect.AsyncState as Socket;
			if (!object.ReferenceEquals(socket, null))
			{
				int num = socket.EndSend(asyncConnect);
				if (num > 0)
				{
					this.ReceieveMsg.Enqueue("RegisterCallback  Success·· Length:" + num);
					this.isHavedRegister = true;
					if (object.ReferenceEquals(this.m_hReceiveThread, null))
					{
						this.m_hReceiveThread = new Thread(new ThreadStart(this.ReceiveThread));
						this.m_hReceiveThread.IsBackground = true;
						this.m_hReceiveThread.Name = "ReceiveThread";
						this.m_hReceiveThread.Start();
					}
					else
					{
						this.m_hReceiveThread.Resume();
					}
					return;
				}
			}
		}
		this.ReceieveMsg.Enqueue("RegisterCallback TimeOut.........");
		this.CloseSocket();
		if (!object.ReferenceEquals(this.NetErrorFunc, null))
		{
			this.NetErrorFunc(null);
		}
	}

	private void ReceiveThread()
	{
		try
		{
			while (true)
			{
				if (object.ReferenceEquals(NetSocket.clientSocket, null) || !NetSocket.clientSocket.Connected)
				{
					NetSocket.receieveMsg.Enqueue("Failed to clientSocket server.");
					Thread.Sleep(10000);
				}
				else
				{
					try
					{
						byte[] array = new byte[4096];
						NetSocket.receieveMsg.Enqueue("ReceiveThread Doing :....ReceiveingBytes........ ");
						int num = NetSocket.clientSocket.Receive(array);
						NetSocket.receieveMsg.Enqueue("ReceiveThread............... Bytes  :    " + num);
						if (num <= 0)
						{
							NetSocket.receieveMsg.Enqueue("ReceiveThread :ReceiveThread is Abort.");
							NetSocket.clientSocket.Shutdown(SocketShutdown.Both);
							NetSocket.clientSocket.Close();
							Thread.CurrentThread.Abort();
							break;
						}
						this.SplitPackage(array, num);
					}
					catch (Exception var_2_C4)
					{
						NetSocket.receieveMsg.Enqueue("ReceiveThread :ReceiveThread is Abort.");
						NetSocket.clientSocket.Shutdown(SocketShutdown.Both);
						NetSocket.clientSocket.Close();
						Thread.CurrentThread.Abort();
						break;
					}
				}
			}
		}
		catch (ThreadAbortException var_3_107)
		{
			NetSocket.receieveMsg.Enqueue("ReceiveThread :ReceiveThread is Abort.");
			NetSocket.clientSocket.Shutdown(SocketShutdown.Both);
			NetSocket.clientSocket.Close();
			Thread.CurrentThread.Abort();
		}
	}

	private void SplitPackage(byte[] bytes, int msgLen)
	{
		NetSocket.receieveMsg.Enqueue("接收--------------- Length: " + msgLen);
		byte[] array;
		if (!object.ReferenceEquals(this.aTempByte, null))
		{
			array = new byte[msgLen + this.aTempByte.Length];
			Array.Copy(this.aTempByte, 0, array, 0, this.aTempByte.Length);
			Array.Copy(bytes, 0, array, this.aTempByte.Length, msgLen);
		}
		else
		{
			array = new byte[msgLen];
			Array.Copy(bytes, 0, array, 0, msgLen);
		}
		this.aTempByte = null;
		while (array.Length >= 4)
		{
			short num = (short)PacketBundle.ByteReadShort(array, 0);
			short num2 = (short)PacketBundle.ByteReadShort(array, 2);
			if ((int)(num + 4) > array.Length)
			{
				if (array.Length > 0)
				{
					this.aTempByte = new byte[array.Length];
					Array.Copy(this.aTempByte, 0, array, 0, array.Length);
				}
				return;
			}
			byte[] array2 = new byte[(int)num];
			Array.Copy(array, 4, array2, 0, (int)num);
			NetSocket.receieveMsg.Enqueue(string.Format("!!!!!!!!!!!!!接收到的消息 CMD :{0}  Length:{1}", num2, array2.Length));
			Opcode instance_Socket = Opcode.GetInstance_Socket((int)num2, new MemoryStream(array2)
			{
				Position = 0L
			});
			this.m_lsWorldPackage.Enqueue(new Packet((int)num2, instance_Socket));
			int num3 = array.Length - 4 - (int)num;
			if (num3 > 0)
			{
				byte[] array3 = new byte[num3];
				Array.Copy(array, (int)(num + 4), array3, 0, num3);
				array = array3;
			}
			else
			{
				array = new byte[0];
			}
		}
		if (array.Length > 0)
		{
			this.aTempByte = new byte[array.Length];
			Array.Copy(this.aTempByte, 0, array, 0, array.Length);
		}
	}

	public void SendMessage(short nOpCode, object obj)
	{
		this.SendMessage(nOpCode, obj, new Action<IAsyncResult>(this.SendMessageBack));
	}

	private void SendMessageBack(IAsyncResult asyncSend)
	{
		if (!object.ReferenceEquals(asyncSend, null))
		{
			Socket socket = asyncSend.AsyncState as Socket;
			int num = socket.EndSend(asyncSend);
			if (num > 0)
			{
				this.ReceieveMsg.Enqueue("SendMessageCallBack Success~~Length:" + num);
				return;
			}
		}
		this.ReceieveMsg.Enqueue("SendMessageCallBack  Time Out !");
		this.CloseSocket();
		if (!object.ReferenceEquals(this.NetErrorFunc, null))
		{
			this.NetErrorFunc(null);
		}
	}

	public void SendMessage(short nOpCode, object obj, Action<IAsyncResult> _SendCallBack)
	{
		if (object.ReferenceEquals(NetSocket.clientSocket, null) || !NetSocket.clientSocket.Connected)
		{
			this.ReceieveMsg.Enqueue(string.Format("clientSocket is null:{0}    clientSocket.Connected:{1}   ", NetSocket.clientSocket == null, NetSocket.clientSocket != null && NetSocket.clientSocket.Connected));
			this.CloseSocket();
			if (!object.ReferenceEquals(this.NetErrorFunc, null))
			{
				this.NetErrorFunc(null);
			}
			return;
		}
		try
		{
			this.ReceieveMsg.Enqueue("nOpCode  " + nOpCode + "  obj    ");
			byte[] array;
			PacketBundle.ToMsg(nOpCode, obj, out array);
			this.ReceieveMsg.Enqueue(string.Concat(new object[]
			{
				"newByte  ",
				array,
				"     newByte   ",
				array.Length
			}));
			IAsyncResult asyncResult = NetSocket.clientSocket.BeginSend(array, 0, array.Length, SocketFlags.None, new AsyncCallback(_SendCallBack.Invoke), NetSocket.clientSocket);
		}
		catch (Exception arg)
		{
			this.ReceieveMsg.Enqueue("send message error: " + arg);
		}
	}

	public void SocketHeart()
	{
		try
		{
			if (this.isHavedRegister)
			{
				this.ReceieveMsg.Enqueue("SocketHeart ·············· ");
				this.SendMessage(30004, new CSSocketHeat
				{
					playerid = HeroInfo.GetInstance().userId
				}, new Action<IAsyncResult>(this.HeartCallback));
			}
			else
			{
				this.ReceieveMsg.Enqueue("NotHavedRegister SocketHeart Fail");
				this.CloseSocket();
			}
		}
		catch (ThreadAbortException var_1_69)
		{
			this.ReceieveMsg.Enqueue("HeartThread SocketHeart Fail......");
			this.CloseSocket();
		}
	}

	private void HeartCallback(IAsyncResult asyncConnect)
	{
		if (!object.ReferenceEquals(asyncConnect, null))
		{
			Socket socket = asyncConnect.AsyncState as Socket;
			int num = socket.EndSend(asyncConnect);
			if (num > 0)
			{
				this.ReceieveMsg.Enqueue("SocketHeartCallBack  success Length:" + num);
				return;
			}
		}
		this.ReceieveMsg.Enqueue("SocketHeartCallBack  TimeOut~~~~~~~ ");
		this.CloseSocket();
		if (!object.ReferenceEquals(this.NetErrorFunc, null))
		{
			this.NetErrorFunc(null);
		}
	}

	private void CloseSocket()
	{
		this.ReceieveMsg.Enqueue("CloseSocket.........");
		this.isHavedRegister = false;
		if (!object.ReferenceEquals(NetSocket.clientSocket, null) && NetSocket.clientSocket.Connected)
		{
			NetSocket.clientSocket.Shutdown(SocketShutdown.Both);
			NetSocket.clientSocket.Close();
		}
		NetSocket.clientSocket = null;
	}

	private void ReStart(object objc)
	{
		LogManage.LogError("ReStart~~~~~~~~~~~~~~~~~~");
		this.Reset();
		this.ConnectNet(this._IP, this.Port);
	}
}
