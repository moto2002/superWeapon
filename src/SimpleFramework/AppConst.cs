using System;
using UnityEngine;

namespace SimpleFramework
{
	public class AppConst
	{
		public const bool DebugMode = false;

		public const bool ExampleMode = false;

		public const bool UpdateMode = false;

		public const bool AutoWrapMode = true;

		public const bool UsePbc = true;

		public const bool UseLpeg = true;

		public const bool UsePbLua = true;

		public const bool UseCJson = true;

		public const bool UseSproto = true;

		public const bool LuaEncode = false;

		public const int TimerInterval = 1;

		public const int GameFrameRate = 60;

		public const string AppName = "SimpleFramework";

		public const string AppPrefix = "SimpleFramework_";

		public const string WebUrl = "http://localhost:6688/";

		public static string UserId = string.Empty;

		public static int SocketPort = 0;

		public static string SocketAddress = string.Empty;

		public static string LuaBasePath
		{
			get
			{
				return Application.dataPath + "/uLua/Source/";
			}
		}

		public static string LuaWrapPath
		{
			get
			{
				return AppConst.LuaBasePath + "LuaWrap/";
			}
		}
	}
}
