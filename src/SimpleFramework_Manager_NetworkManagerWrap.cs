using LuaInterface;
using SimpleFramework;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class SimpleFramework_Manager_NetworkManagerWrap
{
	private static Type classType = typeof(NetworkManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnInit", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.OnInit)),
			new LuaMethod("Unload", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.Unload)),
			new LuaMethod("CallMethod", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.CallMethod)),
			new LuaMethod("AddEvent", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.AddEvent)),
			new LuaMethod("SendConnect", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.SendConnect)),
			new LuaMethod("SendMessage", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.SendMessage)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap._CreateSimpleFramework_Manager_NetworkManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_NetworkManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.NetworkManager", typeof(NetworkManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_NetworkManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.NetworkManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_NetworkManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnInit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		networkManager.OnInit();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Unload(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		networkManager.Unload();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CallMethod(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 3, num - 2);
		object[] o = networkManager.CallMethod(luaString, paramsObject);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		int @event = (int)LuaScriptMgr.GetNumber(L, 1);
		ByteBuffer data = (ByteBuffer)LuaScriptMgr.GetNetObject(L, 2, typeof(ByteBuffer));
		NetworkManager.AddEvent(@event, data);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SendConnect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		networkManager.SendConnect();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SendMessage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager networkManager = (NetworkManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.NetworkManager");
		ByteBuffer buffer = (ByteBuffer)LuaScriptMgr.GetNetObject(L, 2, typeof(ByteBuffer));
		networkManager.SendMessage(buffer);
		return 0;
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
