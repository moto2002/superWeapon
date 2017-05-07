using System;

public static class Debugger
{
	public static void Log(string str, params object[] args)
	{
		str = string.Format(str, args);
		LogManage.Log(str);
	}

	public static void LogWarning(string str, params object[] args)
	{
		str = string.Format(str, args);
		LogManage.Log(str);
	}

	public static void LogError(string str, params object[] args)
	{
		str = string.Format(str, args);
		LogManage.Log(str);
	}
}
