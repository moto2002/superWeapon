using LuaInterface;
using SimpleFramework;
using SimpleFramework.Manager;
using System;

public class SimpleFramework_LuaHelperWrap
{
	private static Type classType = typeof(LuaHelper);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetType", new LuaCSFunction(SimpleFramework_LuaHelperWrap.GetType)),
			new LuaMethod("GetPanelManager", new LuaCSFunction(SimpleFramework_LuaHelperWrap.GetPanelManager)),
			new LuaMethod("GetResManager", new LuaCSFunction(SimpleFramework_LuaHelperWrap.GetResManager)),
			new LuaMethod("GetNetManager", new LuaCSFunction(SimpleFramework_LuaHelperWrap.GetNetManager)),
			new LuaMethod("GetMusicManager", new LuaCSFunction(SimpleFramework_LuaHelperWrap.GetMusicManager)),
			new LuaMethod("Action", new LuaCSFunction(SimpleFramework_LuaHelperWrap.Action)),
			new LuaMethod("VoidDelegate", new LuaCSFunction(SimpleFramework_LuaHelperWrap.VoidDelegate)),
			new LuaMethod("OnCallLuaFunc", new LuaCSFunction(SimpleFramework_LuaHelperWrap.OnCallLuaFunc)),
			new LuaMethod("OnJsonCallFunc", new LuaCSFunction(SimpleFramework_LuaHelperWrap.OnJsonCallFunc)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_LuaHelperWrap._CreateSimpleFramework_LuaHelper)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_LuaHelperWrap.GetClassType))
		};
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.LuaHelper", regs);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_LuaHelper(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.LuaHelper class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_LuaHelperWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Type type = LuaHelper.GetType(luaString);
		LuaScriptMgr.Push(L, type);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPanelManager(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		PanelManager panelManager = LuaHelper.GetPanelManager();
		LuaScriptMgr.Push(L, panelManager);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetResManager(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ResourceManager resManager = LuaHelper.GetResManager();
		LuaScriptMgr.Push(L, resManager);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetNetManager(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		NetworkManager netManager = LuaHelper.GetNetManager();
		LuaScriptMgr.Push(L, netManager);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMusicManager(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		MusicManager musicManager = LuaHelper.GetMusicManager();
		LuaScriptMgr.Push(L, musicManager);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Action(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Action o = LuaHelper.Action(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		UIEventListener.VoidDelegate o = LuaHelper.VoidDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnCallLuaFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LuaStringBuffer stringBuffer = LuaScriptMgr.GetStringBuffer(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 2);
		LuaHelper.OnCallLuaFunc(stringBuffer, luaFunction);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnJsonCallFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 2);
		LuaHelper.OnJsonCallFunc(luaString, luaFunction);
		return 0;
	}
}
