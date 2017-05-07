using LuaInterface;
using System;

public class UITweener_StyleWrap
{
	private static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Once", new LuaCSFunction(UITweener_StyleWrap.GetOnce)),
		new LuaMethod("Loop", new LuaCSFunction(UITweener_StyleWrap.GetLoop)),
		new LuaMethod("PingPong", new LuaCSFunction(UITweener_StyleWrap.GetPingPong)),
		new LuaMethod("IntToEnum", new LuaCSFunction(UITweener_StyleWrap.IntToEnum))
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UITweener.Style", typeof(UITweener.Style), UITweener_StyleWrap.enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetOnce(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Style.Once);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetLoop(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Style.Loop);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPingPong(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Style.PingPong);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		UITweener.Style style = (UITweener.Style)num;
		LuaScriptMgr.Push(L, style);
		return 1;
	}
}
