using LuaInterface;
using System;
using UnityEngine;

public class PlayModeWrap
{
	private static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("StopSameLayer", new LuaCSFunction(PlayModeWrap.GetStopSameLayer)),
		new LuaMethod("StopAll", new LuaCSFunction(PlayModeWrap.GetStopAll)),
		new LuaMethod("IntToEnum", new LuaCSFunction(PlayModeWrap.IntToEnum))
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.PlayMode", typeof(PlayMode), PlayModeWrap.enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetStopSameLayer(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayMode.StopSameLayer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetStopAll(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayMode.StopAll);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		PlayMode playMode = (PlayMode)num;
		LuaScriptMgr.Push(L, playMode);
		return 1;
	}
}
