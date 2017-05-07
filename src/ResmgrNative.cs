using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class ResmgrNative
{
	public class TaskState
	{
		public int taskcount;

		public int downloadcount;

		public void Clear()
		{
			this.taskcount = 0;
			this.downloadcount = 0;
			foreach (ResmgrNative.DownTask current in ResmgrNative.Instance.task)
			{
				this.taskcount++;
			}
			foreach (ResmgrNative.DownTaskRunner current2 in ResmgrNative.Instance.runnner)
			{
				this.taskcount++;
				this.downloadcount++;
			}
		}

		public override string ToString()
		{
			return this.downloadcount + "/" + this.taskcount;
		}

		public float per()
		{
			return (float)this.downloadcount / (float)this.taskcount;
		}
	}

	private class DownTask
	{
		public string path;

		public string tag;

		public Action<WWW, string> onload;

		public DownTask(string path, string tag, Action<WWW, string> onload)
		{
			this.path = path;
			this.tag = tag;
			this.onload = onload;
		}
	}

	public enum FrameState
	{
		Nothing,
		Slow,
		Finish
	}

	private abstract class FrameTask
	{
		public string path;

		public string tag;

		public abstract ResmgrNative.FrameState Update();
	}

	private class FrameTaskAssetBundle : ResmgrNative.FrameTask
	{
		private AssetBundleCreateRequest request;

		public Action<AssetBundle, string> onload;

		public FrameTaskAssetBundle(string path, string tag, Action<AssetBundle, string> onLoad)
		{
			this.onload = onLoad;
			this.tag = tag;
			this.path = path;
		}

		public override ResmgrNative.FrameState Update()
		{
			if (this.request == null)
			{
				byte[] binary = ResmgrNative.Instance.LoadFromCacheDirect(this.path);
				this.request = AssetBundle.CreateFromMemory(binary);
				return ResmgrNative.FrameState.Slow;
			}
			if (this.request.isDone)
			{
				this.onload(this.request.assetBundle, this.tag);
				return ResmgrNative.FrameState.Finish;
			}
			return ResmgrNative.FrameState.Nothing;
		}
	}

	private class FrameTaskBytes : ResmgrNative.FrameTask
	{
		public Action<byte[], string> onload;

		public FrameTaskBytes(string path, string tag, Action<byte[], string> onLoad)
		{
			this.onload = onLoad;
			this.tag = tag;
			this.path = path;
		}

		public override ResmgrNative.FrameState Update()
		{
			byte[] arg = ResmgrNative.Instance.LoadFromCacheDirect(this.path);
			this.onload(arg, this.tag);
			return ResmgrNative.FrameState.Finish;
		}
	}

	private class FrameTaskTexture2D : ResmgrNative.FrameTask
	{
		public Action<Texture2D, string> onload;

		public FrameTaskTexture2D(string path, string tag, Action<Texture2D, string> onLoad)
		{
			this.onload = onLoad;
			this.tag = tag;
			this.path = path;
		}

		public override ResmgrNative.FrameState Update()
		{
			Debug.Log("FrameTaskTexture2D Update" + this.path);
			byte[] data = ResmgrNative.Instance.LoadFromCacheDirect(this.path);
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			texture2D.LoadImage(data);
			this.onload(texture2D, this.tag);
			return ResmgrNative.FrameState.Finish;
		}
	}

	private class FrameTaskString : ResmgrNative.FrameTask
	{
		public Action<string, string> onload;

		public FrameTaskString(string path, string tag, Action<string, string> onLoad)
		{
			this.onload = onLoad;
			this.tag = tag;
			this.path = path;
		}

		public override ResmgrNative.FrameState Update()
		{
			byte[] array = ResmgrNative.Instance.LoadFromCacheDirect(this.path);
			string text = Encoding.UTF8.GetString(array, 0, array.Length);
			if (text[0] == '﻿')
			{
				text = text.Substring(1);
			}
			this.onload(text, this.tag);
			return ResmgrNative.FrameState.Finish;
		}
	}

	private class DownTaskRunner
	{
		public WWW www;

		public ResmgrNative.DownTask task;

		public DownTaskRunner(ResmgrNative.DownTask task)
		{
			this.task = task;
			this.www = new WWW(this.task.path);
		}
	}

	public bool ClientVerErr;

	private static ResmgrNative g_this;

	private Action TaskFinish;

	private Queue<ResmgrNative.DownTask> task = new Queue<ResmgrNative.DownTask>();

	private List<ResmgrNative.DownTaskRunner> runnner = new List<ResmgrNative.DownTaskRunner>();

	private List<ResmgrNative.FrameTask> frametask = new List<ResmgrNative.FrameTask>();

	public string localurl
	{
		get;
		private set;
	}

	public string remoteurl
	{
		get;
		private set;
	}

	public string cacheurl
	{
		get;
		private set;
	}

	public int taskCount
	{
		get;
		private set;
	}

	public static ResmgrNative Instance
	{
		get
		{
			if (ResmgrNative.g_this == null)
			{
				ResmgrNative.g_this = new ResmgrNative();
			}
			return ResmgrNative.g_this;
		}
	}

	public ResmgrNative.TaskState taskState
	{
		get;
		private set;
	}

	public LocalVersion verLocal
	{
		get;
		private set;
	}

	public RemoteVersion verRemote
	{
		get;
		private set;
	}

	public SHA1Managed sha1
	{
		get;
		private set;
	}

	private ResmgrNative()
	{
		this.localurl = Application.streamingAssetsPath;
		this.cacheurl = Path.Combine(ResManager.DataPath_ForDown, "vercache");
		this.sha1 = new SHA1Managed();
		this.taskState = new ResmgrNative.TaskState();
	}

	public void WaitForTaskFinish(Action finish)
	{
		if (this.TaskFinish != null)
		{
			this.TaskFinish = (Action)Delegate.Combine(this.TaskFinish, finish);
		}
		else
		{
			this.TaskFinish = finish;
		}
	}

	public void SetCacheUrl(string url)
	{
		this.cacheurl = url;
	}

	public void Update(ref float wwwProcess)
	{
		if (this.runnner.Count < this.taskCount && this.task.Count > 0)
		{
			this.runnner.Add(new ResmgrNative.DownTaskRunner(this.task.Dequeue()));
		}
		List<ResmgrNative.DownTaskRunner> list = new List<ResmgrNative.DownTaskRunner>();
		foreach (ResmgrNative.DownTaskRunner current in this.runnner)
		{
			if (current.www.isDone)
			{
				this.taskState.downloadcount++;
				list.Add(current);
				current.task.onload(current.www, current.task.tag);
			}
			else
			{
				wwwProcess = current.www.progress;
			}
		}
		foreach (ResmgrNative.DownTaskRunner current2 in list)
		{
			this.runnner.Remove(current2);
		}
		foreach (ResmgrNative.FrameTask current3 in this.frametask)
		{
			ResmgrNative.FrameState frameState = current3.Update();
			if (frameState == ResmgrNative.FrameState.Slow)
			{
				break;
			}
			if (frameState == ResmgrNative.FrameState.Finish)
			{
				this.frametask.Remove(current3);
				this.taskState.downloadcount++;
				break;
			}
		}
		if (this.task.Count == 0 && this.runnner.Count == 0 && this.TaskFinish != null && this.frametask.Count == 0)
		{
			Action taskFinish = this.TaskFinish;
			this.TaskFinish = null;
			taskFinish();
		}
	}

	public void LoadFromStreamingAssets(string path, string tag, Action<WWW, string> onLoad)
	{
		this.Load(this.localurl + "/" + path, tag, onLoad);
	}

	public byte[] LoadFromCacheDirect(string path)
	{
		string path2 = Path.Combine(this.cacheurl, path);
		byte[] result;
		using (Stream stream = File.OpenRead(path2))
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, (int)stream.Length);
			result = array;
		}
		return result;
	}

	public void LoadAssetBundleFromCache(string path, string tag, Action<AssetBundle, string> onLoad)
	{
		this.frametask.Add(new ResmgrNative.FrameTaskAssetBundle(path, tag, onLoad));
		this.taskState.taskcount++;
	}

	public void LoadBytesFromCache(string path, string tag, Action<byte[], string> onLoad)
	{
		this.frametask.Add(new ResmgrNative.FrameTaskBytes(path, tag, onLoad));
		this.taskState.taskcount++;
	}

	public void LoadTexture2DFromCache(string path, string tag, Action<Texture2D, string> onLoad)
	{
		Debug.Log("LoadTexture2DFromCache" + path);
		this.frametask.Add(new ResmgrNative.FrameTaskTexture2D(path, tag, onLoad));
		this.taskState.taskcount++;
	}

	public void LoadStringFromCache(string path, string tag, Action<string, string> onLoad)
	{
		this.frametask.Add(new ResmgrNative.FrameTaskString(path, tag, onLoad));
		this.taskState.taskcount++;
	}

	public void LoadFromRemote(string path, string tag, Action<WWW, string> onLoad)
	{
		if (!this.remoteurl.EndsWith("/"))
		{
			this.Load(this.remoteurl + "/" + path, tag, onLoad);
		}
		else
		{
			this.Load(this.remoteurl + path, tag, onLoad);
		}
	}

	private void Load(string path, string tag, Action<WWW, string> onLoad)
	{
		this.task.Enqueue(new ResmgrNative.DownTask(path, tag, onLoad));
		this.taskState.taskcount++;
	}

	public void SaveToCache(string path, byte[] data)
	{
		string path2 = Path.Combine(this.cacheurl, path);
		string directoryName = Path.GetDirectoryName(path2);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		using (FileStream fileStream = File.Create(path2))
		{
			fileStream.Write(data, 0, data.Length);
		}
	}

	public void BeginInit(string remotoURL, Action<Exception> onInit, IEnumerable<string> groups, int taskcount = 1, bool checkRemote = true)
	{
		this.remoteurl = remotoURL;
		this.taskCount = taskcount;
		this.verLocal = new LocalVersion();
		Action<Exception> onInitRemote = delegate(Exception err)
		{
			if (err != null)
			{
				onInit(err);
				return;
			}
			Debug.Log("(ver)onInitRemote");
			int num = 0;
			int num2 = 0;
			this.verLocal.ver = this.verRemote.ver;
			foreach (string current in groups)
			{
				if (!this.verLocal.groups.ContainsKey(current))
				{
					if (!this.verRemote.groups.ContainsKey(current))
					{
						Debug.LogWarning("group:" + current + " 在服务器和本地均不存在");
						continue;
					}
					this.verLocal.groups[current] = new LocalVersion.VerInfo(current, string.Empty, 0);
				}
				this.verLocal.groups[current].groupfilecount = this.verRemote.groups[current].filecount;
				this.verLocal.groups[current].grouphash = this.verRemote.groups[current].hash;
				this.verLocal.groups[current].listverid = this.verRemote.ver;
				Debug.Log("check groups=====================>" + current);
				foreach (KeyValuePair<string, RemoteVersion.Group.FileInfo> current2 in this.verRemote.groups[current].files)
				{
					Debug.Log("check files=====>" + current2.Key);
					if (this.verLocal.groups[current].listfiles.ContainsKey(current2.Key))
					{
						LocalVersion.ResInfo resInfo = this.verLocal.groups[current].listfiles[current2.Key];
						if (resInfo.hash != current2.Value.hash || resInfo.size != current2.Value.length)
						{
							resInfo.needupdate = true;
							num2++;
							Debug.Log("(ver)update:" + current2.Key);
						}
					}
					else
					{
						Debug.Log("(ver)add:" + current2.Key);
						this.verLocal.groups[current].listfiles[current2.Key] = new LocalVersion.ResInfo(current, current2.Key, current2.Value.hash, current2.Value.length);
						this.verLocal.groups[current].listfiles[current2.Key].state = LocalVersion.ResState.ResState_UseRemote;
						this.verLocal.groups[current].listfiles[current2.Key].needupdate = true;
						num++;
					}
				}
			}
			Debug.Log(string.Concat(new object[]
			{
				"(ver)addcount:",
				num,
				",updatecount:",
				num2
			}));
			this.verLocal.Save(groups);
			onInit(null);
		};
		Action onInit2 = delegate
		{
			Debug.Log("(ver)onInitLocal");
			if (!checkRemote)
			{
				onInit(null);
			}
			else
			{
				this.verRemote = new RemoteVersion();
				this.verRemote.BeginInit(onInitRemote, groups);
			}
		};
		this.verLocal.BeginInit(onInit2, groups);
	}

	public IEnumerable<LocalVersion.ResInfo> GetNeedDownloadRes(IEnumerable<string> groups)
	{
		List<LocalVersion.ResInfo> list = new List<LocalVersion.ResInfo>();
		foreach (string current in groups)
		{
			if (!this.verLocal.groups.ContainsKey(current))
			{
				Debug.LogWarning("指定的Group:" + current + " 不存在于版本库中");
			}
			else
			{
				foreach (LocalVersion.ResInfo current2 in this.verLocal.groups[current].listfiles.Values)
				{
					if (!(current2.FileName == "Thumbs.db") && !(current2.FileName == "thumbs.db"))
					{
						if (current2.needupdate)
						{
							list.Add(current2);
						}
					}
				}
			}
		}
		return list;
	}
}
