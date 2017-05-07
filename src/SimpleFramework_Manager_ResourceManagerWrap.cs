using LuaInterface;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class SimpleFramework_Manager_ResourceManagerWrap
{
	private static Type classType = typeof(ResourceManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("initialize", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.initialize)),
			new LuaMethod("LoadBundle", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.LoadBundle)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap._CreateSimpleFramework_Manager_ResourceManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_ResourceManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.ResourceManager", typeof(ResourceManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_ResourceManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.ResourceManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_ResourceManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceManager resourceManager = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.ResourceManager");
		LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
		Action func2;
		if (luaTypes != LuaTypes.LUA_TFUNCTION)
		{
			func2 = (Action)LuaScriptMgr.GetNetObject(L, 2, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			func2 = delegate
			{
				func.Call();
			};
		}
		resourceManager.initialize(func2);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadBundle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceManager resourceManager = (ResourceManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.ResourceManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		AssetBundle obj = resourceManager.LoadBundle(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object x = LuaScriptMgr.GetLuaObject(L, 1) as UnityEngine.Object;
		UnityEngine.Object y = LuaScriptMgr.GetLuaObject(L, 2) as UnityEngine.Object;
		bool b = x == y;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
