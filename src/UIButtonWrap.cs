using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonWrap
{
	private static Type classType = typeof(UIButton);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetState", new LuaCSFunction(UIButtonWrap.SetState)),
			new LuaMethod("SetSprite", new LuaCSFunction(UIButtonWrap.SetSprite)),
			new LuaMethod("New", new LuaCSFunction(UIButtonWrap._CreateUIButton)),
			new LuaMethod("GetClassType", new LuaCSFunction(UIButtonWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(UIButtonWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", new LuaCSFunction(UIButtonWrap.get_current), new LuaCSFunction(UIButtonWrap.set_current)),
			new LuaField("dragHighlight", new LuaCSFunction(UIButtonWrap.get_dragHighlight), new LuaCSFunction(UIButtonWrap.set_dragHighlight)),
			new LuaField("hoverSprite", new LuaCSFunction(UIButtonWrap.get_hoverSprite), new LuaCSFunction(UIButtonWrap.set_hoverSprite)),
			new LuaField("pressedSprite", new LuaCSFunction(UIButtonWrap.get_pressedSprite), new LuaCSFunction(UIButtonWrap.set_pressedSprite)),
			new LuaField("disabledSprite", new LuaCSFunction(UIButtonWrap.get_disabledSprite), new LuaCSFunction(UIButtonWrap.set_disabledSprite)),
			new LuaField("pixelSnap", new LuaCSFunction(UIButtonWrap.get_pixelSnap), new LuaCSFunction(UIButtonWrap.set_pixelSnap)),
			new LuaField("onClick", new LuaCSFunction(UIButtonWrap.get_onClick), new LuaCSFunction(UIButtonWrap.set_onClick)),
			new LuaField("SetStateCallBack", new LuaCSFunction(UIButtonWrap.get_SetStateCallBack), new LuaCSFunction(UIButtonWrap.set_SetStateCallBack)),
			new LuaField("isEnabled", new LuaCSFunction(UIButtonWrap.get_isEnabled), new LuaCSFunction(UIButtonWrap.set_isEnabled)),
			new LuaField("normalSprite", new LuaCSFunction(UIButtonWrap.get_normalSprite), new LuaCSFunction(UIButtonWrap.set_normalSprite))
		};
		LuaScriptMgr.RegisterLib(L, "UIButton", typeof(UIButton), regs, fields, typeof(UIButtonColor));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUIButton(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIButton class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIButtonWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_current(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIButton.current);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_dragHighlight(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dragHighlight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dragHighlight on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.dragHighlight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hoverSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hoverSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hoverSprite on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.hoverSprite);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pressedSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pressedSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pressedSprite on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.pressedSprite);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_disabledSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disabledSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disabledSprite on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.disabledSprite);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pixelSnap(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelSnap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelSnap on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.pixelSnap);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
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
		LuaScriptMgr.PushObject(L, uIButton.onClick);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_SetStateCallBack(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SetStateCallBack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SetStateCallBack on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.SetStateCallBack);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isEnabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isEnabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isEnabled on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.isEnabled);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_normalSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalSprite on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIButton.normalSprite);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_current(IntPtr L)
	{
		UIButton.current = (UIButton)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIButton));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_dragHighlight(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dragHighlight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dragHighlight on a nil value");
			}
		}
		uIButton.dragHighlight = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_hoverSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hoverSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hoverSprite on a nil value");
			}
		}
		uIButton.hoverSprite = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pressedSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pressedSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pressedSprite on a nil value");
			}
		}
		uIButton.pressedSprite = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_disabledSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disabledSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disabledSprite on a nil value");
			}
		}
		uIButton.disabledSprite = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pixelSnap(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelSnap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelSnap on a nil value");
			}
		}
		uIButton.pixelSnap = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
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
		uIButton.onClick = (List<EventDelegate>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<EventDelegate>));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_SetStateCallBack(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SetStateCallBack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SetStateCallBack on a nil value");
			}
		}
		LuaTypes luaTypes2 = LuaDLL.lua_type(L, 3);
		if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
		{
			uIButton.SetStateCallBack = (Action<UIButtonColor.State, bool>)LuaScriptMgr.GetNetObject(L, 3, typeof(Action<UIButtonColor.State, bool>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			uIButton.SetStateCallBack = delegate(UIButtonColor.State param0, bool param1)
			{
				int oldTop = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				func.PCall(oldTop, 2);
				func.EndPCall(oldTop);
			};
		}
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isEnabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isEnabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isEnabled on a nil value");
			}
		}
		uIButton.isEnabled = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_normalSprite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIButton uIButton = (UIButton)luaObject;
		if (uIButton == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalSprite on a nil value");
			}
		}
		uIButton.normalSprite = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIButton uIButton = (UIButton)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIButton");
		UIButtonColor.State state = (UIButtonColor.State)((int)LuaScriptMgr.GetNetObject(L, 2, typeof(UIButtonColor.State)));
		bool boolean = LuaScriptMgr.GetBoolean(L, 3);
		uIButton.SetState(state, boolean);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetSprite(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIButton uIButton = (UIButton)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIButton");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		uIButton.SetSprite(luaString);
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
