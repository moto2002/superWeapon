using System;
using System.Collections.Generic;
using UnityEngine;

public class RemoteVersion
{
	public class Group
	{
		public class FileInfo
		{
			public string name;

			public string hash;

			public int length;

			public FileInfo(string name, string hash, int len)
			{
				this.name = name;
				this.hash = hash;
				this.length = len;
			}
		}

		public string group;

		public string hash;

		public int filecount;

		public DateTime ver;

		public Dictionary<string, RemoteVersion.Group.FileInfo> files = new Dictionary<string, RemoteVersion.Group.FileInfo>();

		public Group(string group, string hash, int filecount)
		{
			this.group = group;
			this.hash = hash;
			this.filecount = filecount;
		}

		public void Read(string txt)
		{
			string[] array = txt.Split(new string[]
			{
				"\n",
				"\r"
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text.IndexOf("Ver:") == 0)
				{
					string[] array3 = text.Split(new string[]
					{
						"Ver:",
						"|FileCount:"
					}, StringSplitOptions.RemoveEmptyEntries);
					DateTime dateTime = Convert.ToDateTime(array3[0]);
					int num = int.Parse(array3[1]);
					this.filecount = num;
					this.ver = dateTime;
				}
				else
				{
					string[] array4 = text.Split(new char[]
					{
						'|',
						'@'
					});
					Debug.Log(text);
					this.files[array4[0]] = new RemoteVersion.Group.FileInfo(array4[0], array4[1], int.Parse(array4[2]));
				}
			}
		}
	}

	public Dictionary<string, RemoteVersion.Group> groups = new Dictionary<string, RemoteVersion.Group>();

	public DateTime ver
	{
		get;
		private set;
	}

	public void BeginInit(Action<Exception> onload, IEnumerable<string> _groups)
	{
		int groupcount = 0;
		Action<WWW, string> onLoadGroup = delegate(WWW www, string group)
		{
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.LogError("下载" + www.url + "错误");
			}
			else
			{
				string text = www.text;
				Debug.LogError(string.Format("ww.URl:{0} ww.text:{1}", www.url, text));
				if (text[0] == '﻿')
				{
					text = text.Substring(1);
				}
				byte[] inArray = ResmgrNative.Instance.sha1.ComputeHash(www.bytes);
				string a = Convert.ToBase64String(inArray);
				if (a != this.groups[group].hash)
				{
					Debug.Log("hash 不匹配:" + group);
				}
				else
				{
					Debug.Log("hash 匹配:" + group);
					this.groups[group].Read(text);
					if (this.groups[group].ver != this.ver)
					{
						Debug.Log("ver 不匹配:" + group);
					}
					if (this.groups[group].filecount != this.groups[group].files.Count)
					{
						Debug.Log("FileCount 不匹配:" + group);
					}
				}
			}
			groupcount--;
			Debug.Log(string.Concat(new object[]
			{
				"groupcount=",
				groupcount,
				"|",
				group
			}));
			if (groupcount == 0)
			{
				onload(null);
			}
		};
		Action<WWW, string> onLoad = delegate(WWW www, string tag)
		{
			if (!string.IsNullOrEmpty(www.error))
			{
				onload(new Exception(www.error));
				return;
			}
			string text = www.text;
			LogManage.LogError(string.Format("ww.URl:{0} ww.text:{1}", www.url, text));
			if (text[0] == '﻿')
			{
				text = text.Substring(1);
			}
			this.ReadVerAll(text);
			foreach (string current in _groups)
			{
				if (!this.groups.ContainsKey(current))
				{
					Debug.LogWarning("(ver)指定的group:" + current + " 在资源服务器上不存在");
				}
				else
				{
					LogManage.LogError(string.Format("groups[g].hash :{0}    ResmgrNative.Instance.verLocal.groups[g].grouphash  :{1}   ResmgrNative.Instance.verLocal.ver :{2}  ResmgrNative.Instance.verRemote.ver：{3}", new object[]
					{
						this.groups[current].hash,
						ResmgrNative.Instance.verLocal.groups[current].grouphash,
						ResmgrNative.Instance.verLocal.ver,
						ResmgrNative.Instance.verRemote.ver
					}));
					if (ResmgrNative.Instance.verLocal.ver < ResmgrNative.Instance.verRemote.ver && (!ResmgrNative.Instance.verLocal.groups.ContainsKey(current) || this.groups[current].hash != ResmgrNative.Instance.verLocal.groups[current].grouphash))
					{
						Debug.Log("(ver)group改变:" + current + " 下载同步");
						groupcount++;
						ResmgrNative.Instance.LoadFromRemote(current + ".ver.txt", current, onLoadGroup);
					}
					else
					{
						ResmgrNative.Instance.ClientVerErr = (ResmgrNative.Instance.verLocal.ver > ResmgrNative.Instance.verRemote.ver);
						Debug.Log(string.Format("groups[g].hash :{0}  ResmgrNative.Instance.verLocal.groups[g].grouphash:{1}", this.groups[current].hash, ResmgrNative.Instance.verLocal.groups[current].grouphash));
						Debug.Log("(ver)group未改变:" + current);
					}
				}
			}
			if (groupcount == 0)
			{
				onload(null);
			}
		};
		ResmgrNative.Instance.LoadFromRemote("allver.ver.txt", string.Empty, onLoad);
	}

	private void ReadVerAll(string txt)
	{
		string[] array = txt.Split(new string[]
		{
			"\n",
			"\r"
		}, StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i];
			if (text.IndexOf("Ver:") == 0)
			{
				this.ver = Convert.ToDateTime(text.Substring(4));
			}
			else
			{
				Debug.Log(text);
				string[] array3 = text.Split(new char[]
				{
					'|'
				});
				this.groups[array3[0]] = new RemoteVersion.Group(array3[0], array3[1], int.Parse(array3[2]));
			}
		}
	}
}
