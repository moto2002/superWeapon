using LuaInterface;
using System;
using UnityEngine;

public class AudioSourceWrap
{
	private static Type classType = typeof(AudioSource);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Play", new LuaCSFunction(AudioSourceWrap.Play)),
			new LuaMethod("PlayDelayed", new LuaCSFunction(AudioSourceWrap.PlayDelayed)),
			new LuaMethod("PlayScheduled", new LuaCSFunction(AudioSourceWrap.PlayScheduled)),
			new LuaMethod("SetScheduledStartTime", new LuaCSFunction(AudioSourceWrap.SetScheduledStartTime)),
			new LuaMethod("SetScheduledEndTime", new LuaCSFunction(AudioSourceWrap.SetScheduledEndTime)),
			new LuaMethod("Stop", new LuaCSFunction(AudioSourceWrap.Stop)),
			new LuaMethod("Pause", new LuaCSFunction(AudioSourceWrap.Pause)),
			new LuaMethod("PlayOneShot", new LuaCSFunction(AudioSourceWrap.PlayOneShot)),
			new LuaMethod("PlayClipAtPoint", new LuaCSFunction(AudioSourceWrap.PlayClipAtPoint)),
			new LuaMethod("GetOutputData", new LuaCSFunction(AudioSourceWrap.GetOutputData)),
			new LuaMethod("GetSpectrumData", new LuaCSFunction(AudioSourceWrap.GetSpectrumData)),
			new LuaMethod("New", new LuaCSFunction(AudioSourceWrap._CreateAudioSource)),
			new LuaMethod("GetClassType", new LuaCSFunction(AudioSourceWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(AudioSourceWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("volume", new LuaCSFunction(AudioSourceWrap.get_volume), new LuaCSFunction(AudioSourceWrap.set_volume)),
			new LuaField("pitch", new LuaCSFunction(AudioSourceWrap.get_pitch), new LuaCSFunction(AudioSourceWrap.set_pitch)),
			new LuaField("time", new LuaCSFunction(AudioSourceWrap.get_time), new LuaCSFunction(AudioSourceWrap.set_time)),
			new LuaField("timeSamples", new LuaCSFunction(AudioSourceWrap.get_timeSamples), new LuaCSFunction(AudioSourceWrap.set_timeSamples)),
			new LuaField("clip", new LuaCSFunction(AudioSourceWrap.get_clip), new LuaCSFunction(AudioSourceWrap.set_clip)),
			new LuaField("isPlaying", new LuaCSFunction(AudioSourceWrap.get_isPlaying), null),
			new LuaField("loop", new LuaCSFunction(AudioSourceWrap.get_loop), new LuaCSFunction(AudioSourceWrap.set_loop)),
			new LuaField("ignoreListenerVolume", new LuaCSFunction(AudioSourceWrap.get_ignoreListenerVolume), new LuaCSFunction(AudioSourceWrap.set_ignoreListenerVolume)),
			new LuaField("playOnAwake", new LuaCSFunction(AudioSourceWrap.get_playOnAwake), new LuaCSFunction(AudioSourceWrap.set_playOnAwake)),
			new LuaField("ignoreListenerPause", new LuaCSFunction(AudioSourceWrap.get_ignoreListenerPause), new LuaCSFunction(AudioSourceWrap.set_ignoreListenerPause)),
			new LuaField("velocityUpdateMode", new LuaCSFunction(AudioSourceWrap.get_velocityUpdateMode), new LuaCSFunction(AudioSourceWrap.set_velocityUpdateMode)),
			new LuaField("panLevel", new LuaCSFunction(AudioSourceWrap.get_panLevel), new LuaCSFunction(AudioSourceWrap.set_panLevel)),
			new LuaField("bypassEffects", new LuaCSFunction(AudioSourceWrap.get_bypassEffects), new LuaCSFunction(AudioSourceWrap.set_bypassEffects)),
			new LuaField("bypassListenerEffects", new LuaCSFunction(AudioSourceWrap.get_bypassListenerEffects), new LuaCSFunction(AudioSourceWrap.set_bypassListenerEffects)),
			new LuaField("bypassReverbZones", new LuaCSFunction(AudioSourceWrap.get_bypassReverbZones), new LuaCSFunction(AudioSourceWrap.set_bypassReverbZones)),
			new LuaField("dopplerLevel", new LuaCSFunction(AudioSourceWrap.get_dopplerLevel), new LuaCSFunction(AudioSourceWrap.set_dopplerLevel)),
			new LuaField("spread", new LuaCSFunction(AudioSourceWrap.get_spread), new LuaCSFunction(AudioSourceWrap.set_spread)),
			new LuaField("priority", new LuaCSFunction(AudioSourceWrap.get_priority), new LuaCSFunction(AudioSourceWrap.set_priority)),
			new LuaField("mute", new LuaCSFunction(AudioSourceWrap.get_mute), new LuaCSFunction(AudioSourceWrap.set_mute)),
			new LuaField("minDistance", new LuaCSFunction(AudioSourceWrap.get_minDistance), new LuaCSFunction(AudioSourceWrap.set_minDistance)),
			new LuaField("maxDistance", new LuaCSFunction(AudioSourceWrap.get_maxDistance), new LuaCSFunction(AudioSourceWrap.set_maxDistance)),
			new LuaField("pan", new LuaCSFunction(AudioSourceWrap.get_pan), new LuaCSFunction(AudioSourceWrap.set_pan)),
			new LuaField("rolloffMode", new LuaCSFunction(AudioSourceWrap.get_rolloffMode), new LuaCSFunction(AudioSourceWrap.set_rolloffMode))
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AudioSource", typeof(AudioSource), regs, fields, typeof(Behaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAudioSource(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			AudioSource obj = new AudioSource();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, AudioSourceWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_volume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volume on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.volume);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pitch(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pitch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pitch on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.pitch);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_time(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.time);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_timeSamples(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeSamples");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeSamples on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.timeSamples);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clip(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clip on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.clip);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPlaying(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPlaying");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPlaying on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.isPlaying);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loop(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loop on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.loop);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ignoreListenerVolume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerVolume on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.ignoreListenerVolume);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_playOnAwake(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playOnAwake");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playOnAwake on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.playOnAwake);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ignoreListenerPause(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerPause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerPause on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.ignoreListenerPause);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocityUpdateMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityUpdateMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityUpdateMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.velocityUpdateMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_panLevel(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name panLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index panLevel on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.panLevel);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bypassEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassEffects on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.bypassEffects);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bypassListenerEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassListenerEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassListenerEffects on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.bypassListenerEffects);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bypassReverbZones(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassReverbZones");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassReverbZones on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.bypassReverbZones);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_dopplerLevel(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dopplerLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dopplerLevel on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.dopplerLevel);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_spread(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spread");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spread on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.spread);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_priority(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name priority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index priority on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.priority);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mute(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mute on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.mute);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minDistance on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.minDistance);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxDistance on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.maxDistance);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pan(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pan");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pan on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.pan);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rolloffMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rolloffMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rolloffMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.rolloffMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_volume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volume on a nil value");
			}
		}
		audioSource.volume = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pitch(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pitch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pitch on a nil value");
			}
		}
		audioSource.pitch = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_time(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}
		audioSource.time = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_timeSamples(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeSamples");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeSamples on a nil value");
			}
		}
		audioSource.timeSamples = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clip(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clip on a nil value");
			}
		}
		audioSource.clip = (AudioClip)LuaScriptMgr.GetUnityObject(L, 3, typeof(AudioClip));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_loop(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loop on a nil value");
			}
		}
		audioSource.loop = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ignoreListenerVolume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerVolume on a nil value");
			}
		}
		audioSource.ignoreListenerVolume = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_playOnAwake(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playOnAwake");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playOnAwake on a nil value");
			}
		}
		audioSource.playOnAwake = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ignoreListenerPause(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerPause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerPause on a nil value");
			}
		}
		audioSource.ignoreListenerPause = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_velocityUpdateMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityUpdateMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityUpdateMode on a nil value");
			}
		}
		audioSource.velocityUpdateMode = (AudioVelocityUpdateMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(AudioVelocityUpdateMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_panLevel(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name panLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index panLevel on a nil value");
			}
		}
		audioSource.panLevel = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bypassEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassEffects on a nil value");
			}
		}
		audioSource.bypassEffects = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bypassListenerEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassListenerEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassListenerEffects on a nil value");
			}
		}
		audioSource.bypassListenerEffects = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bypassReverbZones(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassReverbZones");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassReverbZones on a nil value");
			}
		}
		audioSource.bypassReverbZones = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_dopplerLevel(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dopplerLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dopplerLevel on a nil value");
			}
		}
		audioSource.dopplerLevel = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_spread(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spread");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spread on a nil value");
			}
		}
		audioSource.spread = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_priority(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name priority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index priority on a nil value");
			}
		}
		audioSource.priority = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mute(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mute on a nil value");
			}
		}
		audioSource.mute = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minDistance on a nil value");
			}
		}
		audioSource.minDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxDistance on a nil value");
			}
		}
		audioSource.maxDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pan(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pan");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pan on a nil value");
			}
		}
		audioSource.pan = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rolloffMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rolloffMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rolloffMode on a nil value");
			}
		}
		audioSource.rolloffMode = (AudioRolloffMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(AudioRolloffMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Play(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			audioSource.Play();
			return 0;
		}
		if (num == 2)
		{
			AudioSource audioSource2 = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			ulong delay = (ulong)LuaScriptMgr.GetNumber(L, 2);
			audioSource2.Play(delay);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.Play");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayDelayed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		float delay = (float)LuaScriptMgr.GetNumber(L, 2);
		audioSource.PlayDelayed(delay);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayScheduled(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		double time = LuaScriptMgr.GetNumber(L, 2);
		audioSource.PlayScheduled(time);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetScheduledStartTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		double scheduledStartTime = LuaScriptMgr.GetNumber(L, 2);
		audioSource.SetScheduledStartTime(scheduledStartTime);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetScheduledEndTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		double scheduledEndTime = LuaScriptMgr.GetNumber(L, 2);
		audioSource.SetScheduledEndTime(scheduledEndTime);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Stop(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		audioSource.Stop();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		audioSource.Pause();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayOneShot(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			AudioClip clip = (AudioClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AudioClip));
			audioSource.PlayOneShot(clip);
			return 0;
		}
		if (num == 3)
		{
			AudioSource audioSource2 = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			AudioClip clip2 = (AudioClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AudioClip));
			float volumeScale = (float)LuaScriptMgr.GetNumber(L, 3);
			audioSource2.PlayOneShot(clip2, volumeScale);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.PlayOneShot");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayClipAtPoint(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2)
		{
			AudioClip clip = (AudioClip)LuaScriptMgr.GetUnityObject(L, 1, typeof(AudioClip));
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			AudioSource.PlayClipAtPoint(clip, vector);
			return 0;
		}
		if (num == 3)
		{
			AudioClip clip2 = (AudioClip)LuaScriptMgr.GetUnityObject(L, 1, typeof(AudioClip));
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float volume = (float)LuaScriptMgr.GetNumber(L, 3);
			AudioSource.PlayClipAtPoint(clip2, vector2, volume);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.PlayClipAtPoint");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetOutputData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int channel = (int)LuaScriptMgr.GetNumber(L, 3);
		audioSource.GetOutputData(arrayNumber, channel);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSpectrumData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int channel = (int)LuaScriptMgr.GetNumber(L, 3);
		FFTWindow window = (FFTWindow)((int)LuaScriptMgr.GetNetObject(L, 4, typeof(FFTWindow)));
		audioSource.GetSpectrumData(arrayNumber, channel, window);
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
