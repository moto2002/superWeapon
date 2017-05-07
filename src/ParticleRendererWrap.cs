using LuaInterface;
using System;
using UnityEngine;

public class ParticleRendererWrap
{
	private static Type classType = typeof(ParticleRenderer);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", new LuaCSFunction(ParticleRendererWrap._CreateParticleRenderer)),
			new LuaMethod("GetClassType", new LuaCSFunction(ParticleRendererWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(ParticleRendererWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("particleRenderMode", new LuaCSFunction(ParticleRendererWrap.get_particleRenderMode), new LuaCSFunction(ParticleRendererWrap.set_particleRenderMode)),
			new LuaField("lengthScale", new LuaCSFunction(ParticleRendererWrap.get_lengthScale), new LuaCSFunction(ParticleRendererWrap.set_lengthScale)),
			new LuaField("velocityScale", new LuaCSFunction(ParticleRendererWrap.get_velocityScale), new LuaCSFunction(ParticleRendererWrap.set_velocityScale)),
			new LuaField("cameraVelocityScale", new LuaCSFunction(ParticleRendererWrap.get_cameraVelocityScale), new LuaCSFunction(ParticleRendererWrap.set_cameraVelocityScale)),
			new LuaField("maxParticleSize", new LuaCSFunction(ParticleRendererWrap.get_maxParticleSize), new LuaCSFunction(ParticleRendererWrap.set_maxParticleSize)),
			new LuaField("uvAnimationXTile", new LuaCSFunction(ParticleRendererWrap.get_uvAnimationXTile), new LuaCSFunction(ParticleRendererWrap.set_uvAnimationXTile)),
			new LuaField("uvAnimationYTile", new LuaCSFunction(ParticleRendererWrap.get_uvAnimationYTile), new LuaCSFunction(ParticleRendererWrap.set_uvAnimationYTile)),
			new LuaField("uvAnimationCycles", new LuaCSFunction(ParticleRendererWrap.get_uvAnimationCycles), new LuaCSFunction(ParticleRendererWrap.set_uvAnimationCycles)),
			new LuaField("maxPartileSize", new LuaCSFunction(ParticleRendererWrap.get_maxPartileSize), new LuaCSFunction(ParticleRendererWrap.set_maxPartileSize)),
			new LuaField("uvTiles", new LuaCSFunction(ParticleRendererWrap.get_uvTiles), new LuaCSFunction(ParticleRendererWrap.set_uvTiles))
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.ParticleRenderer", typeof(ParticleRenderer), regs, fields, typeof(Renderer));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateParticleRenderer(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			ParticleRenderer obj = new ParticleRenderer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: ParticleRenderer.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, ParticleRendererWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particleRenderMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particleRenderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particleRenderMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.particleRenderMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lengthScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lengthScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lengthScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.lengthScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.velocityScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cameraVelocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cameraVelocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cameraVelocityScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.cameraVelocityScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxParticleSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxParticleSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxParticleSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.maxParticleSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationXTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationXTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationXTile on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.uvAnimationXTile);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationYTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationYTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationYTile on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.uvAnimationYTile);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationCycles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationCycles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationCycles on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.uvAnimationCycles);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxPartileSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxPartileSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxPartileSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.maxPartileSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvTiles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvTiles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvTiles on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, particleRenderer.uvTiles);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_particleRenderMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particleRenderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particleRenderMode on a nil value");
			}
		}
		particleRenderer.particleRenderMode = (ParticleRenderMode)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(ParticleRenderMode)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lengthScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lengthScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lengthScale on a nil value");
			}
		}
		particleRenderer.lengthScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_velocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityScale on a nil value");
			}
		}
		particleRenderer.velocityScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cameraVelocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cameraVelocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cameraVelocityScale on a nil value");
			}
		}
		particleRenderer.cameraVelocityScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxParticleSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxParticleSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxParticleSize on a nil value");
			}
		}
		particleRenderer.maxParticleSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationXTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationXTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationXTile on a nil value");
			}
		}
		particleRenderer.uvAnimationXTile = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationYTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationYTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationYTile on a nil value");
			}
		}
		particleRenderer.uvAnimationYTile = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationCycles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationCycles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationCycles on a nil value");
			}
		}
		particleRenderer.uvAnimationCycles = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxPartileSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxPartileSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxPartileSize on a nil value");
			}
		}
		particleRenderer.maxPartileSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvTiles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvTiles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvTiles on a nil value");
			}
		}
		particleRenderer.uvTiles = LuaScriptMgr.GetArrayObject<Rect>(L, 3);
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
