using LuaInterface;
using SimpleFramework;
using System;
using UnityEngine;

public class SimpleFramework_UtilWrap
{
	private static Type classType = typeof(Util);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Int", new LuaCSFunction(SimpleFramework_UtilWrap.Int)),
			new LuaMethod("Float", new LuaCSFunction(SimpleFramework_UtilWrap.Float)),
			new LuaMethod("Long", new LuaCSFunction(SimpleFramework_UtilWrap.Long)),
			new LuaMethod("Random", new LuaCSFunction(SimpleFramework_UtilWrap.Random)),
			new LuaMethod("Uid", new LuaCSFunction(SimpleFramework_UtilWrap.Uid)),
			new LuaMethod("GetTime", new LuaCSFunction(SimpleFramework_UtilWrap.GetTime)),
			new LuaMethod("Child", new LuaCSFunction(SimpleFramework_UtilWrap.Child)),
			new LuaMethod("Peer", new LuaCSFunction(SimpleFramework_UtilWrap.Peer)),
			new LuaMethod("Vibrate", new LuaCSFunction(SimpleFramework_UtilWrap.Vibrate)),
			new LuaMethod("Encode", new LuaCSFunction(SimpleFramework_UtilWrap.Encode)),
			new LuaMethod("Decode", new LuaCSFunction(SimpleFramework_UtilWrap.Decode)),
			new LuaMethod("IsNumeric", new LuaCSFunction(SimpleFramework_UtilWrap.IsNumeric)),
			new LuaMethod("HashToMD5Hex", new LuaCSFunction(SimpleFramework_UtilWrap.HashToMD5Hex)),
			new LuaMethod("md5", new LuaCSFunction(SimpleFramework_UtilWrap.md5)),
			new LuaMethod("md5file", new LuaCSFunction(SimpleFramework_UtilWrap.md5file)),
			new LuaMethod("ClearChild", new LuaCSFunction(SimpleFramework_UtilWrap.ClearChild)),
			new LuaMethod("GetKey", new LuaCSFunction(SimpleFramework_UtilWrap.GetKey)),
			new LuaMethod("GetInt", new LuaCSFunction(SimpleFramework_UtilWrap.GetInt)),
			new LuaMethod("HasKey", new LuaCSFunction(SimpleFramework_UtilWrap.HasKey)),
			new LuaMethod("SetInt", new LuaCSFunction(SimpleFramework_UtilWrap.SetInt)),
			new LuaMethod("GetString", new LuaCSFunction(SimpleFramework_UtilWrap.GetString)),
			new LuaMethod("SetString", new LuaCSFunction(SimpleFramework_UtilWrap.SetString)),
			new LuaMethod("RemoveData", new LuaCSFunction(SimpleFramework_UtilWrap.RemoveData)),
			new LuaMethod("ClearMemory", new LuaCSFunction(SimpleFramework_UtilWrap.ClearMemory)),
			new LuaMethod("IsNumber", new LuaCSFunction(SimpleFramework_UtilWrap.IsNumber)),
			new LuaMethod("GetFileText", new LuaCSFunction(SimpleFramework_UtilWrap.GetFileText)),
			new LuaMethod("AppContentPath", new LuaCSFunction(SimpleFramework_UtilWrap.AppContentPath)),
			new LuaMethod("AddClick", new LuaCSFunction(SimpleFramework_UtilWrap.AddClick)),
			new LuaMethod("LuaPath", new LuaCSFunction(SimpleFramework_UtilWrap.LuaPath)),
			new LuaMethod("SearchLuaPath", new LuaCSFunction(SimpleFramework_UtilWrap.SearchLuaPath)),
			new LuaMethod("AddLuaPath", new LuaCSFunction(SimpleFramework_UtilWrap.AddLuaPath)),
			new LuaMethod("RemoveLuaPath", new LuaCSFunction(SimpleFramework_UtilWrap.RemoveLuaPath)),
			new LuaMethod("Log", new LuaCSFunction(SimpleFramework_UtilWrap.Log)),
			new LuaMethod("LogWarning", new LuaCSFunction(SimpleFramework_UtilWrap.LogWarning)),
			new LuaMethod("LogError", new LuaCSFunction(SimpleFramework_UtilWrap.LogError)),
			new LuaMethod("LoadAsset", new LuaCSFunction(SimpleFramework_UtilWrap.LoadAsset)),
			new LuaMethod("AddComponent", new LuaCSFunction(SimpleFramework_UtilWrap.AddComponent)),
			new LuaMethod("LoadPrefab", new LuaCSFunction(SimpleFramework_UtilWrap.LoadPrefab)),
			new LuaMethod("CallMethod", new LuaCSFunction(SimpleFramework_UtilWrap.CallMethod)),
			new LuaMethod("CheckEnvironment", new LuaCSFunction(SimpleFramework_UtilWrap.CheckEnvironment)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_UtilWrap._CreateSimpleFramework_Util)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_UtilWrap.GetClassType))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("DataPath", new LuaCSFunction(SimpleFramework_UtilWrap.get_DataPath), null),
			new LuaField("NetAvailable", new LuaCSFunction(SimpleFramework_UtilWrap.get_NetAvailable), null),
			new LuaField("IsWifi", new LuaCSFunction(SimpleFramework_UtilWrap.get_IsWifi), null),
			new LuaField("isLogin", new LuaCSFunction(SimpleFramework_UtilWrap.get_isLogin), null),
			new LuaField("isMain", new LuaCSFunction(SimpleFramework_UtilWrap.get_isMain), null),
			new LuaField("isFight", new LuaCSFunction(SimpleFramework_UtilWrap.get_isFight), null),
			new LuaField("isApplePlatform", new LuaCSFunction(SimpleFramework_UtilWrap.get_isApplePlatform), null)
		};
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Util", typeof(Util), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Util(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Util o = new Util();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.Util.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_UtilWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_DataPath(IntPtr L)
	{
		LuaScriptMgr.Push(L, Util.DataPath);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_NetAvailable(IntPtr L)
	{
		LuaScriptMgr.Push(L, Util.NetAvailable);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_IsWifi(IntPtr L)
	{
		LuaScriptMgr.Push(L, Util.IsWifi);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isLogin(IntPtr L)
	{
		LuaScriptMgr.Push(L, Util.isLogin);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isMain(IntPtr L)
	{
		LuaScriptMgr.Push(L, Util.isMain);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isFight(IntPtr L)
	{
		LuaScriptMgr.Push(L, Util.isFight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isApplePlatform(IntPtr L)
	{
		LuaScriptMgr.Push(L, Util.isApplePlatform);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Int(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object varObject = LuaScriptMgr.GetVarObject(L, 1);
		int d = Util.Int(varObject);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Float(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object varObject = LuaScriptMgr.GetVarObject(L, 1);
		float d = Util.Float(varObject);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Long(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object varObject = LuaScriptMgr.GetVarObject(L, 1);
		long d = Util.Long(varObject);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Random(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(float), typeof(float)))
		{
			float min = (float)LuaDLL.lua_tonumber(L, 1);
			float max = (float)LuaDLL.lua_tonumber(L, 2);
			float d = Util.Random(min, max);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(int)))
		{
			int min2 = (int)LuaDLL.lua_tonumber(L, 1);
			int max2 = (int)LuaDLL.lua_tonumber(L, 2);
			int d2 = Util.Random(min2, max2);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.Util.Random");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Uid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.Uid(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		long time = Util.GetTime();
		LuaScriptMgr.Push(L, time);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Child(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(string)))
		{
			Transform go = (Transform)LuaScriptMgr.GetLuaObject(L, 1);
			string @string = LuaScriptMgr.GetString(L, 2);
			GameObject obj = Util.Child(go, @string);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
		{
			GameObject go2 = (GameObject)LuaScriptMgr.GetLuaObject(L, 1);
			string string2 = LuaScriptMgr.GetString(L, 2);
			GameObject obj2 = Util.Child(go2, string2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.Util.Child");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Peer(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(string)))
		{
			Transform go = (Transform)LuaScriptMgr.GetLuaObject(L, 1);
			string @string = LuaScriptMgr.GetString(L, 2);
			GameObject obj = Util.Peer(go, @string);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
		{
			GameObject go2 = (GameObject)LuaScriptMgr.GetLuaObject(L, 1);
			string string2 = LuaScriptMgr.GetString(L, 2);
			GameObject obj2 = Util.Peer(go2, string2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.Util.Peer");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Vibrate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Util.Vibrate();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Encode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.Encode(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Decode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.Decode(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsNumeric(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		bool b = Util.IsNumeric(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HashToMD5Hex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.HashToMD5Hex(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int md5(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.md5(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int md5file(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.md5file(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform go = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
		Util.ClearChild(go);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetKey(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string key = Util.GetKey(luaString);
		LuaScriptMgr.Push(L, key);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		int @int = Util.GetInt(luaString);
		LuaScriptMgr.Push(L, @int);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HasKey(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		bool b = Util.HasKey(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		int value = (int)LuaScriptMgr.GetNumber(L, 2);
		Util.SetInt(luaString, value);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string @string = Util.GetString(luaString);
		LuaScriptMgr.Push(L, @string);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
		Util.SetString(luaString, luaString2);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Util.RemoveData(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearMemory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Util.ClearMemory();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsNumber(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		bool b = Util.IsNumber(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetFileText(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string fileText = Util.GetFileText(luaString);
		LuaScriptMgr.Push(L, fileText);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AppContentPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string str = Util.AppContentPath();
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject go = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		Util.AddClick(go, varObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.LuaPath(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SearchLuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = Util.SearchLuaPath(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddLuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Util.AddLuaPath(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveLuaPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Util.RemoveLuaPath(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Log(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Util.Log(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LogWarning(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Util.LogWarning(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LogError(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Util.LogError(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AssetBundle bundle = (AssetBundle)LuaScriptMgr.GetUnityObject(L, 1, typeof(AssetBundle));
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		GameObject obj = Util.LoadAsset(bundle, luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddComponent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameObject go = (GameObject)LuaScriptMgr.GetUnityObject(L, 1, typeof(GameObject));
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
		Component obj = Util.AddComponent(go, luaString, luaString2);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		GameObject obj = Util.LoadPrefab(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CallMethod(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
		object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 3, num - 2);
		object[] o = Util.CallMethod(luaString, luaString2, paramsObject);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckEnvironment(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		bool b = Util.CheckEnvironment();
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
