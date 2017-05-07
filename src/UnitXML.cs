using SimpleFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public sealed class UnitXML
{
	private static UnitXML instance;

	private Dictionary<string, string> allPlannerDataXMl = new Dictionary<string, string>();

	public Dictionary<string, string> AllPlannerDataXMl
	{
		get
		{
			return this.allPlannerDataXMl;
		}
		set
		{
			this.allPlannerDataXMl = value;
		}
	}

	private UnitXML()
	{
	}

	public static UnitXML GetInstance()
	{
		if (UnitXML.instance == null)
		{
			UnitXML.instance = new UnitXML();
		}
		return UnitXML.instance;
	}

	public string GetXMLTextByName(string nameKey)
	{
		if (this.allPlannerDataXMl.ContainsKey(nameKey))
		{
			return this.allPlannerDataXMl[nameKey];
		}
		return null;
	}

	public void InitDataByClient()
	{
		this.ReadDataByClient();
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			LogManage.LogError(Util.DataPath + "/PlannerDataXMl/");
			if (Directory.Exists(Util.DataPath + "/PlannerDataXMl/"))
			{
				FileInfo[] fileInfos = GameTools.GetFileInfos(Util.DataPath + "/PlannerDataXMl/");
				LogManage.LogError("allXML.Length ：" + fileInfos.Length);
				FileInfo[] array = fileInfos;
				for (int i = 0; i < array.Length; i++)
				{
					FileInfo fileInfo = array[i];
					try
					{
						string key = (!fileInfo.Name.Contains(".")) ? fileInfo.Name : fileInfo.Name.Split(new char[]
						{
							'.'
						})[0];
						if (this.allPlannerDataXMl.ContainsKey(key))
						{
							this.allPlannerDataXMl[key] = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
						}
						else
						{
							this.allPlannerDataXMl.Add(key, File.ReadAllText(fileInfo.FullName, Encoding.UTF8));
						}
					}
					catch (Exception ex)
					{
						LogManage.LogError("读取数据报错:" + ex.ToString());
					}
				}
			}
		}
	}

	public void ReadDataByClient()
	{
		this.allPlannerDataXMl.Clear();
		if (Application.platform != RuntimePlatform.Android)
		{
			LogManage.LogError(Util.AppContentPath() + "/PlannerDataXMl/");
			FileInfo[] fileInfos = GameTools.GetFileInfos(Util.AppContentPath() + "/PlannerDataXMl/");
			LogManage.LogError("allXML.Length ：" + fileInfos.Length);
			FileInfo[] array = fileInfos;
			for (int i = 0; i < array.Length; i++)
			{
				FileInfo fileInfo = array[i];
				if (!fileInfo.Name.Contains(".meta"))
				{
					try
					{
						this.allPlannerDataXMl.Add(fileInfo.Name.Split(new char[]
						{
							'.'
						})[0], File.ReadAllText(fileInfo.FullName, Encoding.UTF8));
					}
					catch (Exception ex)
					{
						LogManage.LogError(fileInfo.Name + "读取数据报错:" + ex.ToString());
					}
				}
			}
		}
	}
}
