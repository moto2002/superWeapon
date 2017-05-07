using LuaInterface;
using System;
using UnityEngine;

public class CameraClearFlagsWrap
{
	private static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Skybox", new LuaCSFunction(CameraClearFlagsWrap.GetSkybox)),
		new LuaMethod("Color", new LuaCSFunction(CameraClearFlagsWrap.GetColor)),
		new LuaMethod("SolidColor", new LuaCSFunction(CameraClearFlagsWrap.GetSolidColor)),
		new LuaMethod("Depth", new LuaCSFunction(CameraClearFlagsWrap.GetDepth)),
		new LuaMethod("Nothing", new LuaCSFunction(CameraClearFlagsWrap.GetNothing)),
		new LuaMethod("IntToEnum", new LuaCSFunction(CameraClearFlagsWrap.IntToEnum))
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.CameraClearFlags", typeof(CameraClearFlags), CameraClearFlagsWrap.enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSkybox(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Skybox);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Color);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSolidColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Color);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetDepth(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Depth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetNothing(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Nothing);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		CameraClearFlags cameraClearFlags = (CameraClearFlags)num;
		LuaScriptMgr.Push(L, cameraClearFlags);
		return 1;
	}
}
