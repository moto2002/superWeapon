using LuaInterface;
using SimpleFramework;
using System;
using UnityEngine;

public class SimpleFramework_WrapGridWrap
{
	private static Type classType = typeof(WrapGrid);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("InitGrid", new LuaCSFunction(SimpleFramework_WrapGridWrap.InitGrid)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_WrapGridWrap._CreateSimpleFramework_WrapGrid)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_WrapGridWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_WrapGridWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.WrapGrid", typeof(WrapGrid), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_WrapGrid(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.WrapGrid class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_WrapGridWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InitGrid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		WrapGrid wrapGrid = (WrapGrid)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.WrapGrid");
		wrapGrid.InitGrid();
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
