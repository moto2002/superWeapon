using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class LogManageMonoBehaviour : MonoBehaviour
{
	private void OnEnable()
	{
		Application.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
	}

	private void Start()
	{
		LogManageMonoBehaviour.ReWriteFile(Application.persistentDataPath, "LogAssertFile.txt");
		LogManageMonoBehaviour.ReWriteFile(Application.persistentDataPath, "LogErrorFile.txt");
		LogManageMonoBehaviour.ReWriteFile(Application.persistentDataPath, "LogWarningFile.txt");
		LogManageMonoBehaviour.ReWriteFile(Application.persistentDataPath, "LogFile.txt");
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnDisable()
	{
		Application.RegisterLogCallback(null);
	}

	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		switch (type)
		{
		case LogType.Error:
			LogManageMonoBehaviour.CreateFile(Application.persistentDataPath, "LogErrorFile.txt", string.Concat(new object[]
			{
				"LogType.Error---",
				stackTrace,
				"~",
				logString,
				"~",
				DateTime.Now,
				"\n\r"
			}));
			break;
		case LogType.Assert:
			LogManageMonoBehaviour.CreateFile(Application.persistentDataPath, "LogAssertFile.txt", string.Concat(new object[]
			{
				"LogType.Assert---",
				stackTrace,
				"~",
				logString,
				"~",
				DateTime.Now,
				"\n\r"
			}));
			break;
		case LogType.Warning:
			LogManageMonoBehaviour.CreateFile(Application.persistentDataPath, "LogWarningFile.txt", string.Concat(new object[]
			{
				"LogType.Warning---",
				stackTrace,
				"~",
				logString,
				"~",
				DateTime.Now,
				"\n\r"
			}));
			break;
		case LogType.Log:
			LogManageMonoBehaviour.CreateFile(Application.persistentDataPath, "LogFile.txt", string.Concat(new object[]
			{
				"LogType.Log---",
				stackTrace,
				"~",
				logString,
				"~",
				DateTime.Now,
				"\n\r"
			}));
			break;
		case LogType.Exception:
			LogManageMonoBehaviour.CreateFile(Application.persistentDataPath, "LogErrorFile.txt", string.Concat(new object[]
			{
				"LogType.Exception---",
				stackTrace,
				"~",
				logString,
				"~",
				DateTime.Now,
				"\n\r"
			}));
			break;
		}
	}

	public static void CreateFile(string path, string name, string info)
	{
		FileInfo fileInfo = new FileInfo(path + "//" + name);
		File.AppendAllText(path + "//" + name, info);
	}

	public static ArrayList LoadFileToArray(string path, string name)
	{
		StreamReader streamReader = null;
		try
		{
			streamReader = File.OpenText(path + "//" + name);
		}
		catch (Exception var_1_19)
		{
			return null;
		}
		ArrayList arrayList = new ArrayList();
		string value;
		while ((value = streamReader.ReadLine()) != null)
		{
			arrayList.Add(value);
		}
		streamReader.Close();
		streamReader.Dispose();
		return arrayList;
	}

	public static string LoadFileToString(string path, string name)
	{
		StreamReader streamReader = null;
		try
		{
			streamReader = File.OpenText(path + "//" + name);
		}
		catch (Exception var_1_19)
		{
			return null;
		}
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		streamReader.Dispose();
		return result;
	}

	public static void DeleteFile(string path, string name)
	{
		File.Delete(path + "//" + name);
	}

	public static void ReWriteFile(string path, string name)
	{
		File.WriteAllText(path + "//" + name, string.Empty);
	}
}
