using LuaInterface;
using System;
using UnityEngine;

public class TestLuaDelegateWrap
{
	private static Type classType = typeof(TestLuaDelegate);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", new LuaCSFunction(TestLuaDelegateWrap._CreateTestLuaDelegate)),
			new LuaMethod("GetClassType", new LuaCSFunction(TestLuaDelegateWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(TestLuaDelegateWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("onClick", new LuaCSFunction(TestLuaDelegateWrap.get_onClick), new LuaCSFunction(TestLuaDelegateWrap.set_onClick))
		};
		LuaScriptMgr.RegisterLib(L, "TestLuaDelegate", typeof(TestLuaDelegate), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateTestLuaDelegate(IntPtr L)
	{
		LuaDLL.luaL_error(L, "TestLuaDelegate class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, TestLuaDelegateWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestLuaDelegate testLuaDelegate = (TestLuaDelegate)luaObject;
		if (testLuaDelegate == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}
		LuaScriptMgr.Push(L, testLuaDelegate.onClick);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestLuaDelegate testLuaDelegate = (TestLuaDelegate)luaObject;
		if (testLuaDelegate == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}
		LuaTypes luaTypes2 = LuaDLL.lua_type(L, 3);
		if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
		{
			testLuaDelegate.onClick = (TestLuaDelegate.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(TestLuaDelegate.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			testLuaDelegate.onClick = delegate(GameObject param0)
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
