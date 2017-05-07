using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LocalVersion
{
	public enum ResState
	{
		ResState_Error,
		ResState_UseLocal,
		ResState_UseDownloaded,
		ResState_UseRemote
	}

	public class ResInfo
	{
		public string FileName;

		public string[] PathList;

		public LocalVersion.ResState state;

		public bool needupdate;

		public string group;

		public string name;

		public int size;

		public string hash;

		public ResInfo(string group, string name, string hash, int size)
		{
			this.name = name;
			this.hash = hash;
			this.size = size;
			this.group = group;
			this.state = LocalVersion.ResState.ResState_UseLocal;
			this.PathList = LocalVersion.ResInfo.GetPathList(name);
			this.FileName = this.PathList[this.PathList.Length - 1];
		}

		public static string[] GetPathList(string pathname)
		{
			pathname = pathname.Replace('\\', '/');
			return pathname.Split(new char[]
			{
				'/'
			});
		}

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.name,
				"|",
				this.hash,
				"|",
				this.size,
				"|",
				(int)((!this.needupdate) ? this.state : (16 + this.state))
			});
		}

		public void Download(Action<LocalVersion.ResInfo, Exception> onDown, bool UpdateVerInfo = true)
		{
			Action<WWW, string> onLoad = delegate(WWW WWW, string tag)
			{
				Exception ex = null;
				try
				{
					ResmgrNative.Instance.SaveToCache(this.group + "/" + this.name, WWW.bytes);
					this.size = WWW.bytes.Length;
					this.hash = Convert.ToBase64String(ResmgrNative.Instance.sha1.ComputeHash(WWW.bytes));
					this.state = LocalVersion.ResState.ResState_UseDownloaded;
					this.needupdate = false;
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				if (this.size == 0)
				{
					ex = new Exception("下载size==0" + WWW.url);
				}
				if (ex == null && UpdateVerInfo)
				{
					ResmgrNative.Instance.verLocal.SaveGroup(this.group);
				}
				if (onDown != null)
				{
					onDown(this, ex);
				}
			};
			ResmgrNative.Instance.LoadFromRemote(this.group + "/" + this.name, string.Empty, onLoad);
			this.needupdate = true;
			Debug.Log("-Download-------->" + this.state);
		}

		public void BeginLoadAssetBundle(Action<AssetBundle, string> onLoad)
		{
			if ((this.state & LocalVersion.ResState.ResState_UseLocal) == LocalVersion.ResState.ResState_UseLocal)
			{
				Action<WWW, string> onLoad2 = delegate(WWW WWW, string tag)
				{
					try
					{
						if (WWW.error != null)
						{
							LogManage.LogError(WWW.error);
						}
						if (WWW.assetBundle == null)
						{
							LogManage.LogError(string.Concat(new object[]
							{
								"assetBundle is null",
								tag,
								" www.Bytes.Length==",
								WWW.bytes.Length
							}));
						}
						onLoad(WWW.assetBundle, tag);
					}
					catch (Exception ex)
					{
						LogManage.LogError("assetBundle加载失败  ：： " + this.name + "  Ex:" + ex.ToString());
					}
				};
				ResmgrNative.Instance.LoadFromStreamingAssets(this.group + "/" + this.name, this.group + "/" + this.name, onLoad2);
			}
			else
			{
				if ((this.state & LocalVersion.ResState.ResState_UseDownloaded) != LocalVersion.ResState.ResState_UseDownloaded)
				{
					LogManage.LogError("这个资源不能加载  ：： " + this.name);
					throw new Exception("这个资源不能加载");
				}
				ResmgrNative.Instance.LoadAssetBundleFromCache(this.group + "/" + this.name, this.group + "/" + this.name, onLoad);
			}
		}

		public void BeginLoadBytes(Action<byte[], string> onLoad)
		{
			if ((this.state & LocalVersion.ResState.ResState_UseLocal) == LocalVersion.ResState.ResState_UseLocal)
			{
				Action<WWW, string> onLoad2 = delegate(WWW WWW, string tag)
				{
					onLoad(WWW.bytes, tag);
				};
				ResmgrNative.Instance.LoadFromStreamingAssets(this.group + "/" + this.name, this.group + "/" + this.name, onLoad2);
			}
			else
			{
				if ((this.state & LocalVersion.ResState.ResState_UseDownloaded) != LocalVersion.ResState.ResState_UseDownloaded)
				{
					throw new Exception("这个资源不能加载");
				}
				ResmgrNative.Instance.LoadBytesFromCache(this.group + "/" + this.name, this.group + "/" + this.name, onLoad);
			}
		}

		public void BeginLoadTexture2D(Action<Texture2D, string> onLoad)
		{
			if ((this.state & LocalVersion.ResState.ResState_UseLocal) == LocalVersion.ResState.ResState_UseLocal)
			{
				Action<WWW, string> onLoad2 = delegate(WWW WWW, string tag)
				{
					Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
					texture2D.LoadImage(WWW.bytes);
					onLoad(texture2D, tag);
				};
				ResmgrNative.Instance.LoadFromStreamingAssets(this.group + "/" + this.name, this.group + "/" + this.name, onLoad2);
			}
			else
			{
				if ((this.state & LocalVersion.ResState.ResState_UseDownloaded) != LocalVersion.ResState.ResState_UseDownloaded)
				{
					throw new Exception("这个资源不能加载");
				}
				ResmgrNative.Instance.LoadTexture2DFromCache(this.group + "/" + this.name, this.group + "/" + this.name, onLoad);
			}
		}

		public void BeginLoadString(Action<string, string> onLoad)
		{
			if ((this.state & LocalVersion.ResState.ResState_UseLocal) == LocalVersion.ResState.ResState_UseLocal)
			{
				Action<WWW, string> onLoad2 = delegate(WWW WWW, string tag)
				{
					string text = WWW.text;
					if (text.Length <= 0)
					{
						return;
					}
					if (text[0] == '﻿')
					{
						text = text.Substring(1);
					}
					onLoad(text, tag);
				};
				ResmgrNative.Instance.LoadFromStreamingAssets(this.group + "/" + this.name, this.group + "/" + this.name, onLoad2);
			}
			else
			{
				if ((this.state & LocalVersion.ResState.ResState_UseDownloaded) != LocalVersion.ResState.ResState_UseDownloaded)
				{
					throw new Exception("这个资源不能加载");
				}
				ResmgrNative.Instance.LoadStringFromCache(this.group + "/" + this.name, this.group + "/" + this.name, onLoad);
			}
		}
	}

	public class VerInfo
	{
		public Dictionary<string, LocalVersion.ResInfo> listfiles = new Dictionary<string, LocalVersion.ResInfo>();

		public string group
		{
			get;
			private set;
		}

		public string grouphash
		{
			get;
			set;
		}

		public int groupfilecount
		{
			get;
			set;
		}

		public DateTime listverid
		{
			get;
			set;
		}

		public VerInfo(string group, string hash, int filecount)
		{
			this.group = group;
			this.grouphash = hash;
			this.groupfilecount = filecount;
		}

		public void SaveLocal(string filename)
		{
			string text = string.Concat(new object[]
			{
				"Ver:",
				this.listverid.ToString("G"),
				"|FileCount:",
				this.groupfilecount,
				"\n"
			});
			foreach (KeyValuePair<string, LocalVersion.ResInfo> current in this.listfiles)
			{
				text = text + current.Value.ToString() + "\n";
			}
			using (FileStream fileStream = File.Create(filename))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		public bool ReadLocal(string filename)
		{
			try
			{
				string text = null;
				using (FileStream fileStream = File.OpenRead(filename))
				{
					byte[] array = new byte[fileStream.Length];
					fileStream.Read(array, 0, array.Length);
					text = Encoding.UTF8.GetString(array, 0, array.Length);
				}
				string[] array2 = text.Split(new string[]
				{
					"\n",
					"\r"
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array2.Length == 0)
				{
					bool result = false;
					return result;
				}
				string[] array3 = array2;
				for (int i = 0; i < array3.Length; i++)
				{
					string text2 = array3[i];
					if (text2.IndexOf("Ver:") == 0)
					{
						string[] array4 = text2.Split(new string[]
						{
							"Ver:",
							"|FileCount:"
						}, StringSplitOptions.RemoveEmptyEntries);
						DateTime listverid = Convert.ToDateTime(array4[0]);
						int groupfilecount = int.Parse(array4[1]);
						this.groupfilecount = groupfilecount;
						this.listverid = listverid;
					}
					else
					{
						string[] array5 = text2.Split(new char[]
						{
							'|'
						});
						this.listfiles[array5[0]] = new LocalVersion.ResInfo(this.group, array5[0], array5[1], int.Parse(array5[2]));
						int num = int.Parse(array5[3]);
						this.listfiles[array5[0]].state = (LocalVersion.ResState)(num % 16);
						int num2 = num / 16;
						this.listfiles[array5[0]].needupdate = (num2 > 0);
					}
				}
				if (this.listfiles.Count == 0)
				{
					bool result = false;
					return result;
				}
			}
			catch
			{
				bool result = false;
				return result;
			}
			return true;
		}
	}

	public Dictionary<string, LocalVersion.VerInfo> groups = new Dictionary<string, LocalVersion.VerInfo>();

	public DateTime ver
	{
		get;
		set;
	}

	public void BeginInit(Action onInit, IEnumerable<string> _groups)
	{
		Action onInitEmbed = delegate
		{
			string text = Path.Combine(ResManager.DataPath_ForDown, "vercache");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string filename = Path.Combine(text, "vercache.ver.txt");
			this.MergeLocalVer(text, filename, _groups);
			onInit();
		};
		this.InitEmbedVer(_groups, onInitEmbed);
	}

	public void Save(IEnumerable<string> groups)
	{
		string text = Path.Combine(ResManager.DataPath_ForDown, "vercache");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string filename = Path.Combine(text, "vercache.ver.txt");
		this.SaveLocalVer(text, filename, groups);
	}

	public void SaveGroup(string group)
	{
		string text = Path.Combine(ResManager.DataPath_ForDown, "vercache");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		this.groups[group].SaveLocal(Path.Combine(text, group + ".ver.txt"));
	}

	private void InitEmbedVer(IEnumerable<string> _groups, Action onInitEmbed)
	{
		int groupcount = 0;
		Action<WWW, string> onLoadGroupInfo = delegate(WWW www, string tag)
		{
			string text = www.text;
			if (text.Length > 0 && text[0] == '﻿')
			{
				text = text.Substring(1);
			}
			byte[] inArray = ResmgrNative.Instance.sha1.ComputeHash(www.bytes);
			string text2 = Convert.ToBase64String(inArray);
			if (text2 != this.groups[tag].grouphash)
			{
				Debug.Log(string.Format("(ver)hash 不匹配:{0} shash:{1}  groups[tag].grouphash :{2} ", tag, text2, this.groups[tag].grouphash));
			}
			this.InitGroupOne(text, tag);
			groupcount--;
			if (groupcount == 0)
			{
				Debug.Log("(ver)InitEmbedVer LoadFinish.");
				onInitEmbed();
			}
		};
		Action<WWW, string> onLoad = delegate(WWW www, string tag)
		{
			if (!string.IsNullOrEmpty(www.error))
			{
				onInitEmbed();
				return;
			}
			string text = www.text;
			if (text[0] == '﻿')
			{
				text = text.Substring(1);
			}
			this.InitGroups(text);
			foreach (KeyValuePair<string, LocalVersion.VerInfo> current in this.groups)
			{
				LogManage.LogError("groups:" + current.Key);
			}
			foreach (string current2 in _groups)
			{
				LogManage.LogError("g:" + current2 + "本地没有");
				if (!this.groups.ContainsKey(current2))
				{
					LogManage.LogError("请求的Group:" + current2 + "本地没有");
				}
				else
				{
					groupcount++;
					ResmgrNative.Instance.LoadFromStreamingAssets(current2 + ".ver.txt", current2, onLoadGroupInfo);
				}
			}
		};
		ResmgrNative.Instance.LoadFromStreamingAssets("allver.ver.txt", string.Empty, onLoad);
	}

	private void InitGroups(string txt)
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
				string[] array3 = text.Split(new char[]
				{
					'|'
				});
				this.groups[array3[0]] = new LocalVersion.VerInfo(array3[0], array3[1], int.Parse(array3[2]));
			}
		}
	}

	private void InitGroupOne(string txt, string group)
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
				this.groups[group].listverid = dateTime;
				int num = int.Parse(array3[1]);
				if (this.ver != dateTime)
				{
					Debug.Log("版本不匹配:" + group);
				}
				if (num != this.groups[group].groupfilecount)
				{
					Debug.Log("文件数量不匹配:" + group);
				}
			}
			else
			{
				string[] array4 = text.Split(new char[]
				{
					'|',
					'@'
				});
				this.groups[group].listfiles[array4[0]] = new LocalVersion.ResInfo(group, array4[0], array4[1], int.Parse(array4[2]));
			}
		}
	}

	private void MergeLocalVer(string path, string filename, IEnumerable<string> groups)
	{
		bool flag = true;
		if (File.Exists(filename))
		{
			try
			{
				flag = !this.ReadLocalVer(path, filename, groups);
			}
			catch
			{
			}
		}
		if (flag)
		{
			this.SaveLocalVer(path, filename, groups);
		}
	}

	private void SaveLocalVer(string path, string filename, IEnumerable<string> groups)
	{
		string text = "Ver:" + this.ver + "\n";
		foreach (string current in groups)
		{
			if (this.groups.ContainsKey(current))
			{
				this.groups[current].SaveLocal(Path.Combine(path, current + ".ver.txt"));
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					this.groups[current].group,
					"|",
					this.groups[current].grouphash,
					"|",
					this.groups[current].groupfilecount,
					"\n"
				});
			}
			else
			{
				Debug.LogWarning("指定的Group:" + current + " 不存在于版本库中，无法保存");
			}
			Debug.Log("生成本地信息:" + current);
		}
		using (FileStream fileStream = File.Create(filename))
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			fileStream.Write(bytes, 0, bytes.Length);
			Debug.Log("(ver)SaveLocalVer 生成全部信息");
		}
	}

	private bool ReadLocalVer(string path, string filename, IEnumerable<string> groups)
	{
		string text = null;
		using (FileStream fileStream = File.OpenRead(filename))
		{
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			text = Encoding.UTF8.GetString(array, 0, array.Length);
		}
		string[] array2 = text.Split(new string[]
		{
			"\n",
			"\r"
		}, StringSplitOptions.RemoveEmptyEntries);
		DateTime t = DateTime.MinValue;
		string[] array3 = array2;
		for (int i = 0; i < array3.Length; i++)
		{
			string text2 = array3[i];
			if (text2.IndexOf("Ver:") == 0)
			{
				t = Convert.ToDateTime(text2.Substring(4));
				if (t < this.ver)
				{
					Debug.Log("(ver)储存版本旧了,使用嵌入");
					return false;
				}
				if (t > this.ver)
				{
					Debug.Log("(ver)储存版本新，覆盖嵌入");
				}
			}
			else
			{
				Debug.Log(text2);
				string[] array4 = text2.Split(new char[]
				{
					'|'
				});
				string text3 = array4[0];
				string text4 = array4[1];
				int num = int.Parse(array4[2]);
				if (this.groups.ContainsKey(text3) && this.groups[text3].grouphash == text4 && this.groups[text3].groupfilecount == num)
				{
					Debug.Log("group未改变:" + text3);
				}
				else
				{
					LocalVersion.VerInfo verInfo = new LocalVersion.VerInfo(text3, text4, num);
					bool flag = verInfo.ReadLocal(Path.Combine(path, text3 + ".ver.txt"));
					if (flag)
					{
						this.groups[text3] = verInfo;
						Debug.Log("(ver)覆盖Group:" + text3);
					}
					else
					{
						Debug.Log("Group读取失败:" + text3);
					}
				}
			}
		}
		return true;
	}

	public List<LocalVersion.ResInfo> GetResInfoList(string path)
	{
		string[] pathList = LocalVersion.ResInfo.GetPathList(path);
		if (!this.groups.ContainsKey(pathList[0]))
		{
			Debug.Log("path err" + path);
		}
		Dictionary<string, LocalVersion.ResInfo> listfiles = this.groups[pathList[0]].listfiles;
		List<LocalVersion.ResInfo> list = new List<LocalVersion.ResInfo>();
		foreach (LocalVersion.ResInfo current in listfiles.Values)
		{
			bool flag = true;
			if (pathList.Length > 1)
			{
				for (int i = 1; i < pathList.Length; i++)
				{
					if (current.PathList[i - 1] != pathList[i])
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				list.Add(current);
			}
		}
		return list;
	}
}
