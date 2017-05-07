using LuaInterface;
using System;
using UnityEngine;

public class QueueModeWrap
{
	private static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("CompleteOthers", new LuaCSFunction(QueueModeWrap.GetCompleteOthers)),
		new LuaMethod("PlayNow", new LuaCSFunction(QueueModeWrap.GetPlayNow)),
		new LuaMethod("IntToEnum", new LuaCSFunction(QueueModeWrap.IntToEnum))
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.QueueMode", typeof(QueueMode), QueueModeWrap.enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetCompleteOthers(IntPtr L)
	{
		LuaScriptMgr.Push(L, QueueMode.CompleteOthers);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPlayNow(IntPtr L)
	{
		LuaScriptMgr.Push(L, QueueMode.PlayNow);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		QueueMode queueMode = (QueueMode)num;
		LuaScriptMgr.Push(L, queueMode);
		return 1;
	}
}
