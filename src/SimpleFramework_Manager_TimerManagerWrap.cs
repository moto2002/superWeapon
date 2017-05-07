using LuaInterface;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class SimpleFramework_Manager_TimerManagerWrap
{
	private static Type classType = typeof(TimerManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("StartTimer", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.StartTimer)),
			new LuaMethod("StopTimer", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.StopTimer)),
			new LuaMethod("AddTimerEvent", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.AddTimerEvent)),
			new LuaMethod("RemoveTimerEvent", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.RemoveTimerEvent)),
			new LuaMethod("StopTimerEvent", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.StopTimerEvent)),
			new LuaMethod("ResumeTimerEvent", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.ResumeTimerEvent)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap._CreateSimpleFramework_Manager_TimerManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("Interval", new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.get_Interval), new LuaCSFunction(SimpleFramework_Manager_TimerManagerWrap.set_Interval))
		};
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.TimerManager", typeof(TimerManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_TimerManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.TimerManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_TimerManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Interval(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TimerManager timerManager = (TimerManager)luaObject;
		if (timerManager == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Interval on a nil value");
			}
		}
		LuaScriptMgr.Push(L, timerManager.Interval);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_Interval(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TimerManager timerManager = (TimerManager)luaObject;
		if (timerManager == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Interval on a nil value");
			}
		}
		timerManager.Interval = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager timerManager = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.TimerManager");
		float value = (float)LuaScriptMgr.GetNumber(L, 2);
		timerManager.StartTimer(value);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TimerManager timerManager = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.TimerManager");
		timerManager.StopTimer();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager timerManager = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.TimerManager");
		TimerInfo info = (TimerInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(TimerInfo));
		timerManager.AddTimerEvent(info);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager timerManager = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.TimerManager");
		TimerInfo info = (TimerInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(TimerInfo));
		timerManager.RemoveTimerEvent(info);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager timerManager = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.TimerManager");
		TimerInfo info = (TimerInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(TimerInfo));
		timerManager.StopTimerEvent(info);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResumeTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager timerManager = (TimerManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.TimerManager");
		TimerInfo info = (TimerInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(TimerInfo));
		timerManager.ResumeTimerEvent(info);
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
