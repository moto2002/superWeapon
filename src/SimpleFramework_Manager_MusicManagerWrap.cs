using LuaInterface;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class SimpleFramework_Manager_MusicManagerWrap
{
	private static Type classType = typeof(MusicManager);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("LoadAudioClip", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap.LoadAudioClip)),
			new LuaMethod("CanPlayBackSound", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap.CanPlayBackSound)),
			new LuaMethod("PlayBacksound", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap.PlayBacksound)),
			new LuaMethod("CanPlaySoundEffect", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap.CanPlaySoundEffect)),
			new LuaMethod("Play", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap.Play)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap._CreateSimpleFramework_Manager_MusicManager)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(SimpleFramework_Manager_MusicManagerWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.Manager.MusicManager", typeof(MusicManager), regs, fields, typeof(View));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_Manager_MusicManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimpleFramework.Manager.MusicManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_Manager_MusicManagerWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAudioClip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MusicManager musicManager = (MusicManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.MusicManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		AudioClip obj = musicManager.LoadAudioClip(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CanPlayBackSound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MusicManager musicManager = (MusicManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.MusicManager");
		bool b = musicManager.CanPlayBackSound();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayBacksound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MusicManager musicManager = (MusicManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.MusicManager");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool boolean = LuaScriptMgr.GetBoolean(L, 3);
		musicManager.PlayBacksound(luaString, boolean);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CanPlaySoundEffect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MusicManager musicManager = (MusicManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.MusicManager");
		bool b = musicManager.CanPlaySoundEffect();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Play(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MusicManager musicManager = (MusicManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SimpleFramework.Manager.MusicManager");
		AudioClip clip = (AudioClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AudioClip));
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		musicManager.Play(clip, vector);
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
