using Junfine.Debuger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace SimpleFramework.Manager
{
	public class ThreadManager : View
	{
		private delegate void ThreadSyncEvent(NotiData data);

		private Thread thread;

		private Action<NotiData> func;

		private Stopwatch sw = new Stopwatch();

		private string currDownFile = string.Empty;

		private static readonly object m_lockObj = new object();

		private static Queue<ThreadEvent> events = new Queue<ThreadEvent>();

		private ThreadManager.ThreadSyncEvent m_SyncEvent;

		private void Awake()
		{
			this.m_SyncEvent = new ThreadManager.ThreadSyncEvent(this.OnSyncEvent);
			this.thread = new Thread(new ThreadStart(this.OnUpdate));
		}

		private void Start()
		{
			this.thread.Start();
		}

		public void AddEvent(ThreadEvent ev, Action<NotiData> func)
		{
			object lockObj = ThreadManager.m_lockObj;
			lock (lockObj)
			{
				this.func = func;
				ThreadManager.events.Enqueue(ev);
			}
		}

		private void OnSyncEvent(NotiData data)
		{
			if (this.func != null)
			{
				this.func(data);
			}
			base.facade.SendMessageCommand(data.evName, data.evParam);
		}

		private void OnUpdate()
		{
			while (true)
			{
				object lockObj = ThreadManager.m_lockObj;
				lock (lockObj)
				{
					if (ThreadManager.events.Count > 0)
					{
						ThreadEvent threadEvent = ThreadManager.events.Dequeue();
						try
						{
							string key = threadEvent.Key;
							if (key != null)
							{
								if (ThreadManager.<>f__switch$mapC == null)
								{
									ThreadManager.<>f__switch$mapC = new Dictionary<string, int>(2)
									{
										{
											"UpdateExtract",
											0
										},
										{
											"UpdateDownload",
											1
										}
									};
								}
								int num;
								if (ThreadManager.<>f__switch$mapC.TryGetValue(key, out num))
								{
									if (num != 0)
									{
										if (num == 1)
										{
											this.OnDownloadFile(threadEvent.evParams);
										}
									}
									else
									{
										this.OnExtractFile(threadEvent.evParams);
									}
								}
							}
						}
						catch (Exception ex)
						{
							LogManage.Log(ex.Message);
						}
					}
				}
				Thread.Sleep(1);
			}
		}

		private void OnDownloadFile(List<object> evParams)
		{
			string uriString = evParams[0].ToString();
			this.currDownFile = evParams[1].ToString();
			using (WebClient webClient = new WebClient())
			{
				this.sw.Start();
				webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.ProgressChanged);
				webClient.DownloadFileAsync(new Uri(uriString), this.currDownFile);
			}
		}

		private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			string param = string.Format("{0} kb/s", ((double)e.BytesReceived / 1024.0 / this.sw.Elapsed.TotalSeconds).ToString("0.00"));
			NotiData data = new NotiData("UpdateProgress", param);
			if (this.m_SyncEvent != null)
			{
				this.m_SyncEvent(data);
			}
			if (e.ProgressPercentage == 100 && e.BytesReceived == e.TotalBytesToReceive)
			{
				this.sw.Reset();
				data = new NotiData("UpdateDownload", this.currDownFile);
				if (this.m_SyncEvent != null)
				{
					this.m_SyncEvent(data);
				}
			}
		}

		private void OnExtractFile(List<object> evParams)
		{
			Debuger.LogWarning("Thread evParams: >>" + evParams.Count, new object[0]);
			NotiData data = new NotiData("UpdateDownload", null);
			if (this.m_SyncEvent != null)
			{
				this.m_SyncEvent(data);
			}
		}

		private void OnDestroy()
		{
			this.thread.Abort();
		}
	}
}
