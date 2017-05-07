using System;
using UnityEngine;

public class GameSetting
{
	public static bool autoFight = true;

	public static int FrameNum = 30;

	public static bool isEditor;

	public static string BundleIdentifier = "com.xmdy.cjwq.t2";

	public static string Mi_AppKey = "5951752045398";

	public static string Mi_AppID = "2882303761517520398";

	public static string TalkingdataAppID = "93192408559143A0B422B214659FDBCF";

	public static string Version = "1.0.113";

	public static string appid = string.Empty;

	public static string userid = "1";

	public static string channel = "1";

	public static string platformId = "1";

	public static string Token = "1";

	private static string channelId = "1";

	public static bool IsByPhsic;

	public static bool armyFightBySingle = true;

	public static int MaxLuaProcess = 10000;

	public static float BattleRandomEvevtTime = 30f;

	private static int randomSeed;

	public static bool isUseSDK
	{
		get
		{
			return Application.platform != RuntimePlatform.WindowsEditor;
		}
	}

	public static string ServerIP
	{
		get
		{
			return "106.75.61.38:9090";
		}
	}

	public static string ChannelId
	{
		get
		{
			return GameSetting.channelId;
		}
		set
		{
			GameSetting.channelId = value;
		}
	}

	public static string ServerList
	{
		get
		{
			return "http://" + GameSetting.ServerIP + "/LoginServlet";
		}
	}

	public static string ServerListSuccess
	{
		get
		{
			return "http://" + GameSetting.ServerIP + "/LoginSuccessServlet";
		}
	}

	public static string VersionByServer
	{
		get
		{
			return "http://" + GameSetting.ServerIP + "/VersionServlet";
		}
	}

	public static int RandomSeed
	{
		get
		{
			return GameSetting.randomSeed;
		}
		set
		{
			GameSetting.randomSeed = value;
			UnityEngine.Random.seed = value;
		}
	}
}
