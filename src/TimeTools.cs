using System;
using System.Text;

public class TimeTools
{
	public static TimeSpan asnySeverTimeDiff;

	public static DateTime severTime;

	public static DateTime StampTimeStart;

	static TimeTools()
	{
		// 注意: 此类型已标记为 'beforefieldinit'.
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
		TimeTools.StampTimeStart = dateTime.ToLocalTime();
	}

	public static DateTime ConvertSecondDateTime(int second)
	{
		TimeSpan value = new TimeSpan(long.Parse(second + "0000000"));
		return TimeTools.GetNowTimeSyncServerToDateTime().Add(value);
	}

	public static string ConvertFloatToTimeBySecond(float second)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(0);
		stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		if (second <= 0f)
		{
			return stringBuilder.ToString();
		}
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)second);
		if (timeSpan.Days > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Days);
			stringBuilder.Append(LanguageManage.GetTextByKey("天", "build"));
			stringBuilder.Append(timeSpan.Hours);
			stringBuilder.Append(LanguageManage.GetTextByKey("时", "build"));
		}
		else if (timeSpan.Hours > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Hours);
			stringBuilder.Append(LanguageManage.GetTextByKey("时", "build"));
			stringBuilder.Append(timeSpan.Minutes);
			stringBuilder.Append(LanguageManage.GetTextByKey("分", "build"));
		}
		else if (timeSpan.Minutes > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Minutes);
			stringBuilder.Append(LanguageManage.GetTextByKey("分", "build"));
			stringBuilder.Append(timeSpan.Seconds);
			stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		}
		else if (timeSpan.Seconds > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Seconds);
			stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		}
		return stringBuilder.ToString();
	}

	public static string ConvertFloatToTimeByMilliseconds(double Milliseconds)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(0);
		stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		if (Milliseconds <= 0.0)
		{
			return stringBuilder.ToString();
		}
		TimeSpan timeSpan = TimeSpan.FromMilliseconds(Milliseconds);
		if (timeSpan.Days > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Days);
			stringBuilder.Append(LanguageManage.GetTextByKey("天", "build"));
			stringBuilder.Append(timeSpan.Hours);
			stringBuilder.Append(LanguageManage.GetTextByKey("时", "build"));
		}
		else if (timeSpan.Hours > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Hours);
			stringBuilder.Append(LanguageManage.GetTextByKey("时", "build"));
			stringBuilder.Append(timeSpan.Minutes);
			stringBuilder.Append(LanguageManage.GetTextByKey("分", "build"));
		}
		else if (timeSpan.Minutes > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Minutes);
			stringBuilder.Append(LanguageManage.GetTextByKey("分", "build"));
			stringBuilder.Append(timeSpan.Seconds);
			stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		}
		else if (timeSpan.Seconds > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Seconds);
			stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		}
		else
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(1);
			stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		}
		return stringBuilder.ToString();
	}

	public static DateTime ConvertLongDateTime(long longdate)
	{
		return TimeTools.StampTimeStart.AddMilliseconds((double)longdate);
	}

	public static string DateDiffToString(DateTime DateTime1, DateTime DateTime2)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(0);
		stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		if (DateTime1 >= DateTime2)
		{
			return stringBuilder.ToString();
		}
		TimeSpan timeSpan = DateTime2 - DateTime1;
		if (timeSpan.Days > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Days);
			stringBuilder.Append(LanguageManage.GetTextByKey("天", "build"));
			stringBuilder.Append(timeSpan.Hours);
			stringBuilder.Append(LanguageManage.GetTextByKey("时", "build"));
		}
		else if (timeSpan.Hours > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Hours);
			stringBuilder.Append(LanguageManage.GetTextByKey("时", "build"));
			stringBuilder.Append(timeSpan.Minutes);
			stringBuilder.Append(LanguageManage.GetTextByKey("分", "build"));
		}
		else if (timeSpan.Minutes > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Minutes);
			stringBuilder.Append(LanguageManage.GetTextByKey("分", "build"));
			stringBuilder.Append(timeSpan.Seconds);
			stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		}
		else
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(timeSpan.Seconds + 1);
			stringBuilder.Append(LanguageManage.GetTextByKey("秒", "build"));
		}
		return stringBuilder.ToString();
	}

	public static double DateDiffToDouble(DateTime DateTime1, DateTime DateTime2)
	{
		if (DateTime1 >= DateTime2)
		{
			return 0.0;
		}
		return (DateTime2 - DateTime1).TotalMilliseconds;
	}

	public static DateTime GetNowTimeSyncServerToDateTime()
	{
		return DateTime.Now.Subtract(TimeTools.asnySeverTimeDiff);
	}

	public static long GetNowTimeSyncServerToLong()
	{
		return (long)(DateTime.Now.Subtract(TimeTools.asnySeverTimeDiff) - TimeTools.StampTimeStart).TotalMilliseconds;
	}

	public static long GetNowTimeSyncServerToLongSecond()
	{
		return (long)(DateTime.Now.Subtract(TimeTools.asnySeverTimeDiff) - TimeTools.StampTimeStart).TotalSeconds;
	}

	public static long GetTimeLongByDateTime(DateTime dateTime)
	{
		return (long)(dateTime - TimeTools.StampTimeStart).TotalMilliseconds;
	}

	public static bool IsSmallOrEquByDay(DateTime curTime, DateTime endTime)
	{
		TimeSpan timeSpan = endTime - curTime;
		return timeSpan.TotalDays > 1.0 || (timeSpan.TotalDays >= -1.0 && curTime.Day <= endTime.Day);
	}

	public static bool IsSmallByDay(DateTime curTime, DateTime endTime)
	{
		TimeSpan timeSpan = endTime - curTime;
		return timeSpan.TotalDays > 1.0 || (timeSpan.TotalDays >= -1.0 && curTime.Day < endTime.Day);
	}
}
