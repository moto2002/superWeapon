using System;
using UnityEngine;

public class LogManage
{
	public static void Log(object message)
	{
		if (GameSetting.isEditor)
		{
			Debug.Log(message);
		}
	}

	public static void Log(object message, UnityEngine.Object context)
	{
		if (GameSetting.isEditor)
		{
			Debug.Log(message, context);
		}
	}

	public static void LogError(object message)
	{
		if (GameSetting.isEditor)
		{
			NGUIDebug.Log(new object[]
			{
				message
			});
		}
		if (GameSetting.isEditor)
		{
			Debug.Log("LogError : " + message.ToString());
		}
	}

	public static void LogError(object message, UnityEngine.Object context)
	{
		if (GameSetting.isEditor)
		{
			NGUIDebug.Log(new object[]
			{
				message
			});
		}
		if (GameSetting.isEditor)
		{
			Debug.Log("LogError::");
			Debug.Log(message, context);
		}
	}

	public static void LogException(Exception exception)
	{
		if (GameSetting.isEditor)
		{
			Debug.LogException(exception);
		}
	}

	public static void LogException(Exception exception, UnityEngine.Object context)
	{
		if (GameSetting.isEditor)
		{
			Debug.LogException(exception, context);
		}
	}

	public static void LogWarning(object message)
	{
		if (GameSetting.isEditor)
		{
			Debug.LogWarning(message);
		}
	}

	public static void LogWarning(object message, UnityEngine.Object context)
	{
		if (GameSetting.isEditor)
		{
			Debug.LogWarning(message, context);
		}
	}
}
