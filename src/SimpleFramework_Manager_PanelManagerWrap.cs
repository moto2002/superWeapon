using LuaInterface;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class SimpleFramework_Manager_PanelManagerWrap
{
	private static Type classType = typeof(PanelManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("CreatePanel", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.CreatePanel)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap._CreateSimpleFramework_Manager_PanelManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_PanelManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.PanelManager", typeof(PanelManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_PanelManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.PanelManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_PanelManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreatePanel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PanelManager panelManager = (PanelManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.PanelManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 3);
		panelManager.CreatePanel(luaString, luaFunction);
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
