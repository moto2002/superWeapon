using LuaInterface;
using SimpleFramework;
using System;
using UnityEngine;

public class SimpleFramework_LuaBehaviourWrap
{
	private static Type classType = typeof(LuaBehaviour);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnInit", new LuaCSFunction(SimpleFramework_LuaBehaviourWrap.OnInit)),
			new LuaMethod("GetGameObject", new LuaCSFunction(SimpleFramework_LuaBehaviourWrap.GetGameObject)),
			new LuaMethod("AddClick", new LuaCSFunction(SimpleFramework_LuaBehaviourWrap.AddClick)),
			new LuaMethod("ClearClick", new LuaCSFunction(SimpleFramework_LuaBehaviourWrap.ClearClick)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_LuaBehaviourWrap._CreateSimpleFramework_LuaBehaviour)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_LuaBehaviourWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_LuaBehaviourWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.LuaBehaviour", typeof(LuaBehaviour), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_LuaBehaviour(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.LuaBehaviour class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_LuaBehaviourWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OnInit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		LuaBehaviour luaBehaviour = (LuaBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.LuaBehaviour");
		AssetBundle bundle = (AssetBundle)LuaScriptMgr.GetUnityObject(L, 2, typeof(AssetBundle));
		string luaString = LuaScriptMgr.GetLuaString(L, 3);
		luaBehaviour.OnInit(bundle, luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetGameObject(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LuaBehaviour luaBehaviour = (LuaBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.LuaBehaviour");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		GameObject gameObject = luaBehaviour.GetGameObject(luaString);
		LuaScriptMgr.Push(L, gameObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		LuaBehaviour luaBehaviour = (LuaBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.LuaBehaviour");
		GameObject go = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 3);
		luaBehaviour.AddClick(go, luaFunction);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaBehaviour luaBehaviour = (LuaBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.LuaBehaviour");
		luaBehaviour.ClearClick();
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
