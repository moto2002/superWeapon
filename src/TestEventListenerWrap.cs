using LuaInterface;
using System;
using UnityEngine;

public class TestEventListenerWrap
{
	private static Type classType = typeof(TestEventListener);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", new LuaCSFunction(TestEventListenerWrap._CreateTestEventListener)),
			new LuaMethod("GetClassType", new LuaCSFunction(TestEventListenerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(TestEventListenerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("OnClick", new LuaCSFunction(TestEventListenerWrap.get_OnClick), new LuaCSFunction(TestEventListenerWrap.set_OnClick))
		};
		LuaScriptMgr.RegisterLib(L, "TestEventListener", typeof(TestEventListener), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateTestEventListener(IntPtr L)
	{
		LuaDLL.luaL_error(L, "TestEventListener class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, TestEventListenerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_OnClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestEventListener testEventListener = (TestEventListener)luaObject;
		if (testEventListener == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnClick on a nil value");
			}
		}
		LuaScriptMgr.Push(L, testEventListener.OnClick);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_OnClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestEventListener testEventListener = (TestEventListener)luaObject;
		if (testEventListener == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnClick on a nil value");
			}
		}
		LuaTypes luaTypes2 = LuaDLL.lua_type(L, 3);
		if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
		{
			testEventListener.OnClick = (Action<GameObject>)LuaScriptMgr.GetNetObject(L, 3, typeof(Action<GameObject>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			testEventListener.OnClick = delegate(GameObject param0)
			{
				int oldTop = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(oldTop, 1);
				func.EndPCall(oldTop);
			};
		}
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
