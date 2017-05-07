using LuaInterface;
using System;
using UnityEngine;

public class UIPanelWrap
{
	private static Type classType = typeof(UIPanel);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("CompareFunc", new LuaCSFunction(UIPanelWrap.CompareFunc)),
			new LuaMethod("GetSides", new LuaCSFunction(UIPanelWrap.GetSides)),
			new LuaMethod("Invalidate", new LuaCSFunction(UIPanelWrap.Invalidate)),
			new LuaMethod("CalculateFinalAlpha", new LuaCSFunction(UIPanelWrap.CalculateFinalAlpha)),
			new LuaMethod("SetRect", new LuaCSFunction(UIPanelWrap.SetRect)),
			new LuaMethod("IsVisible", new LuaCSFunction(UIPanelWrap.IsVisible)),
			new LuaMethod("Affects", new LuaCSFunction(UIPanelWrap.Affects)),
			new LuaMethod("RebuildAllDrawCalls", new LuaCSFunction(UIPanelWrap.RebuildAllDrawCalls)),
			new LuaMethod("SetDirty", new LuaCSFunction(UIPanelWrap.SetDirty)),
			new LuaMethod("ParentHasChanged", new LuaCSFunction(UIPanelWrap.ParentHasChanged)),
			new LuaMethod("SortWidgets", new LuaCSFunction(UIPanelWrap.SortWidgets)),
			new LuaMethod("FindDrawCall", new LuaCSFunction(UIPanelWrap.FindDrawCall)),
			new LuaMethod("AddWidget", new LuaCSFunction(UIPanelWrap.AddWidget)),
			new LuaMethod("RemoveWidget", new LuaCSFunction(UIPanelWrap.RemoveWidget)),
			new LuaMethod("Refresh", new LuaCSFunction(UIPanelWrap.Refresh)),
			new LuaMethod("CalculateConstrainOffset", new LuaCSFunction(UIPanelWrap.CalculateConstrainOffset)),
			new LuaMethod("ConstrainTargetToBounds", new LuaCSFunction(UIPanelWrap.ConstrainTargetToBounds)),
			new LuaMethod("Find", new LuaCSFunction(UIPanelWrap.Find)),
			new LuaMethod("GetViewSize", new LuaCSFunction(UIPanelWrap.GetViewSize)),
			new LuaMethod("GetMainGameViewSize", new LuaCSFunction(UIPanelWrap.GetMainGameViewSize)),
			new LuaMethod("New", new LuaCSFunction(UIPanelWrap._CreateUIPanel)),
			new LuaMethod("GetClassType", new LuaCSFunction(UIPanelWrap.GetClassType)),
			new LuaMethod("__eq", new LuaCSFunction(UIPanelWrap.Lua_Eq))
		};
		LuaField[] fields = new LuaField[]
		{
			new LuaField("list", new LuaCSFunction(UIPanelWrap.get_list), new LuaCSFunction(UIPanelWrap.set_list)),
			new LuaField("onGeometryUpdated", new LuaCSFunction(UIPanelWrap.get_onGeometryUpdated), new LuaCSFunction(UIPanelWrap.set_onGeometryUpdated)),
			new LuaField("showInPanelTool", new LuaCSFunction(UIPanelWrap.get_showInPanelTool), new LuaCSFunction(UIPanelWrap.set_showInPanelTool)),
			new LuaField("generateNormals", new LuaCSFunction(UIPanelWrap.get_generateNormals), new LuaCSFunction(UIPanelWrap.set_generateNormals)),
			new LuaField("widgetsAreStatic", new LuaCSFunction(UIPanelWrap.get_widgetsAreStatic), new LuaCSFunction(UIPanelWrap.set_widgetsAreStatic)),
			new LuaField("cullWhileDragging", new LuaCSFunction(UIPanelWrap.get_cullWhileDragging), new LuaCSFunction(UIPanelWrap.set_cullWhileDragging)),
			new LuaField("alwaysOnScreen", new LuaCSFunction(UIPanelWrap.get_alwaysOnScreen), new LuaCSFunction(UIPanelWrap.set_alwaysOnScreen)),
			new LuaField("anchorOffset", new LuaCSFunction(UIPanelWrap.get_anchorOffset), new LuaCSFunction(UIPanelWrap.set_anchorOffset)),
			new LuaField("renderQueue", new LuaCSFunction(UIPanelWrap.get_renderQueue), new LuaCSFunction(UIPanelWrap.set_renderQueue)),
			new LuaField("startingRenderQueue", new LuaCSFunction(UIPanelWrap.get_startingRenderQueue), new LuaCSFunction(UIPanelWrap.set_startingRenderQueue)),
			new LuaField("widgets", new LuaCSFunction(UIPanelWrap.get_widgets), new LuaCSFunction(UIPanelWrap.set_widgets)),
			new LuaField("drawCalls", new LuaCSFunction(UIPanelWrap.get_drawCalls), new LuaCSFunction(UIPanelWrap.set_drawCalls)),
			new LuaField("worldToLocal", new LuaCSFunction(UIPanelWrap.get_worldToLocal), new LuaCSFunction(UIPanelWrap.set_worldToLocal)),
			new LuaField("drawCallClipRange", new LuaCSFunction(UIPanelWrap.get_drawCallClipRange), new LuaCSFunction(UIPanelWrap.set_drawCallClipRange)),
			new LuaField("onClipMove", new LuaCSFunction(UIPanelWrap.get_onClipMove), new LuaCSFunction(UIPanelWrap.set_onClipMove)),
			new LuaField("nextUnusedDepth", new LuaCSFunction(UIPanelWrap.get_nextUnusedDepth), null),
			new LuaField("canBeAnchored", new LuaCSFunction(UIPanelWrap.get_canBeAnchored), null),
			new LuaField("alpha", new LuaCSFunction(UIPanelWrap.get_alpha), new LuaCSFunction(UIPanelWrap.set_alpha)),
			new LuaField("depth", new LuaCSFunction(UIPanelWrap.get_depth), new LuaCSFunction(UIPanelWrap.set_depth)),
			new LuaField("sortingOrder", new LuaCSFunction(UIPanelWrap.get_sortingOrder), new LuaCSFunction(UIPanelWrap.set_sortingOrder)),
			new LuaField("width", new LuaCSFunction(UIPanelWrap.get_width), null),
			new LuaField("height", new LuaCSFunction(UIPanelWrap.get_height), null),
			new LuaField("halfPixelOffset", new LuaCSFunction(UIPanelWrap.get_halfPixelOffset), null),
			new LuaField("usedForUI", new LuaCSFunction(UIPanelWrap.get_usedForUI), null),
			new LuaField("drawCallOffset", new LuaCSFunction(UIPanelWrap.get_drawCallOffset), null),
			new LuaField("clipping", new LuaCSFunction(UIPanelWrap.get_clipping), new LuaCSFunction(UIPanelWrap.set_clipping)),
			new LuaField("parentPanel", new LuaCSFunction(UIPanelWrap.get_parentPanel), null),
			new LuaField("clipCount", new LuaCSFunction(UIPanelWrap.get_clipCount), null),
			new LuaField("hasClipping", new LuaCSFunction(UIPanelWrap.get_hasClipping), null),
			new LuaField("hasCumulativeClipping", new LuaCSFunction(UIPanelWrap.get_hasCumulativeClipping), null),
			new LuaField("clipOffset", new LuaCSFunction(UIPanelWrap.get_clipOffset), new LuaCSFunction(UIPanelWrap.set_clipOffset)),
			new LuaField("baseClipRegion", new LuaCSFunction(UIPanelWrap.get_baseClipRegion), new LuaCSFunction(UIPanelWrap.set_baseClipRegion)),
			new LuaField("finalClipRegion", new LuaCSFunction(UIPanelWrap.get_finalClipRegion), null),
			new LuaField("clipSoftness", new LuaCSFunction(UIPanelWrap.get_clipSoftness), new LuaCSFunction(UIPanelWrap.set_clipSoftness)),
			new LuaField("localCorners", new LuaCSFunction(UIPanelWrap.get_localCorners), null),
			new LuaField("worldCorners", new LuaCSFunction(UIPanelWrap.get_worldCorners), null)
		};
		LuaScriptMgr.RegisterLib(L, "UIPanel", typeof(UIPanel), regs, fields, typeof(UIRect));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUIPanel(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIPanel class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIPanelWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_list(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, UIPanel.list);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onGeometryUpdated(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onGeometryUpdated");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onGeometryUpdated on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.onGeometryUpdated);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_showInPanelTool(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showInPanelTool");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showInPanelTool on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.showInPanelTool);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_generateNormals(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name generateNormals");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index generateNormals on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.generateNormals);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_widgetsAreStatic(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name widgetsAreStatic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index widgetsAreStatic on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.widgetsAreStatic);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cullWhileDragging(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullWhileDragging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullWhileDragging on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.cullWhileDragging);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_alwaysOnScreen(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alwaysOnScreen");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alwaysOnScreen on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.alwaysOnScreen);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anchorOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.anchorOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_renderQueue(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderQueue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderQueue on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.renderQueue);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_startingRenderQueue(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startingRenderQueue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startingRenderQueue on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.startingRenderQueue);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_widgets(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name widgets");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index widgets on a nil value");
			}
		}
		LuaScriptMgr.PushObject(L, uIPanel.widgets);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_drawCalls(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name drawCalls");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index drawCalls on a nil value");
			}
		}
		LuaScriptMgr.PushObject(L, uIPanel.drawCalls);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldToLocal(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldToLocal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldToLocal on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, uIPanel.worldToLocal);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_drawCallClipRange(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name drawCallClipRange");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index drawCallClipRange on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.drawCallClipRange);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onClipMove(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClipMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClipMove on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.onClipMove);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_nextUnusedDepth(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIPanel.nextUnusedDepth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_canBeAnchored(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canBeAnchored");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canBeAnchored on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.canBeAnchored);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_alpha(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alpha");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alpha on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.alpha);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_depth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depth on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.depth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sortingOrder(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.sortingOrder);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_width(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index width on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.width);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_height(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.height);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_halfPixelOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name halfPixelOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index halfPixelOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.halfPixelOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_usedForUI(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name usedForUI");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index usedForUI on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.usedForUI);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_drawCallOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name drawCallOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index drawCallOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.drawCallOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clipping(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clipping");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clipping on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.clipping);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_parentPanel(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parentPanel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parentPanel on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.parentPanel);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clipCount(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clipCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clipCount on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.clipCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hasClipping(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasClipping");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasClipping on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.hasClipping);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hasCumulativeClipping(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasCumulativeClipping");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasCumulativeClipping on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.hasCumulativeClipping);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clipOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clipOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clipOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.clipOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_baseClipRegion(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name baseClipRegion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index baseClipRegion on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.baseClipRegion);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_finalClipRegion(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name finalClipRegion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index finalClipRegion on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.finalClipRegion);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clipSoftness(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clipSoftness");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clipSoftness on a nil value");
			}
		}
		LuaScriptMgr.Push(L, uIPanel.clipSoftness);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localCorners(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localCorners");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localCorners on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, uIPanel.localCorners);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldCorners(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldCorners");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldCorners on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, uIPanel.worldCorners);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_list(IntPtr L)
	{
		UIPanel.list = (BetterList<UIPanel>)LuaScriptMgr.GetNetObject(L, 3, typeof(BetterList<UIPanel>));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onGeometryUpdated(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onGeometryUpdated");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onGeometryUpdated on a nil value");
			}
		}
		LuaTypes luaTypes2 = LuaDLL.lua_type(L, 3);
		if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
		{
			uIPanel.onGeometryUpdated = (UIPanel.OnGeometryUpdated)LuaScriptMgr.GetNetObject(L, 3, typeof(UIPanel.OnGeometryUpdated));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			uIPanel.onGeometryUpdated = delegate
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_showInPanelTool(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showInPanelTool");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showInPanelTool on a nil value");
			}
		}
		uIPanel.showInPanelTool = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_generateNormals(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name generateNormals");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index generateNormals on a nil value");
			}
		}
		uIPanel.generateNormals = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_widgetsAreStatic(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name widgetsAreStatic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index widgetsAreStatic on a nil value");
			}
		}
		uIPanel.widgetsAreStatic = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cullWhileDragging(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullWhileDragging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullWhileDragging on a nil value");
			}
		}
		uIPanel.cullWhileDragging = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_alwaysOnScreen(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alwaysOnScreen");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alwaysOnScreen on a nil value");
			}
		}
		uIPanel.alwaysOnScreen = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anchorOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorOffset on a nil value");
			}
		}
		uIPanel.anchorOffset = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_renderQueue(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderQueue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderQueue on a nil value");
			}
		}
		uIPanel.renderQueue = (UIPanel.RenderQueue)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(UIPanel.RenderQueue)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_startingRenderQueue(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startingRenderQueue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startingRenderQueue on a nil value");
			}
		}
		uIPanel.startingRenderQueue = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_widgets(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name widgets");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index widgets on a nil value");
			}
		}
		uIPanel.widgets = (BetterList<UIWidget>)LuaScriptMgr.GetNetObject(L, 3, typeof(BetterList<UIWidget>));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_drawCalls(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name drawCalls");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index drawCalls on a nil value");
			}
		}
		uIPanel.drawCalls = (BetterList<UIDrawCall>)LuaScriptMgr.GetNetObject(L, 3, typeof(BetterList<UIDrawCall>));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_worldToLocal(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldToLocal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldToLocal on a nil value");
			}
		}
		uIPanel.worldToLocal = (Matrix4x4)LuaScriptMgr.GetNetObject(L, 3, typeof(Matrix4x4));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_drawCallClipRange(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name drawCallClipRange");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index drawCallClipRange on a nil value");
			}
		}
		uIPanel.drawCallClipRange = LuaScriptMgr.GetVector4(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onClipMove(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClipMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClipMove on a nil value");
			}
		}
		LuaTypes luaTypes2 = LuaDLL.lua_type(L, 3);
		if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
		{
			uIPanel.onClipMove = (UIPanel.OnClippingMoved)LuaScriptMgr.GetNetObject(L, 3, typeof(UIPanel.OnClippingMoved));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			uIPanel.onClipMove = delegate(UIPanel param0)
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
	private static int set_alpha(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alpha");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alpha on a nil value");
			}
		}
		uIPanel.alpha = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_depth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depth on a nil value");
			}
		}
		uIPanel.depth = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sortingOrder(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}
		uIPanel.sortingOrder = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clipping(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clipping");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clipping on a nil value");
			}
		}
		uIPanel.clipping = (UIDrawCall.Clipping)((int)LuaScriptMgr.GetNetObject(L, 3, typeof(UIDrawCall.Clipping)));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clipOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clipOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clipOffset on a nil value");
			}
		}
		uIPanel.clipOffset = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_baseClipRegion(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name baseClipRegion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index baseClipRegion on a nil value");
			}
		}
		uIPanel.baseClipRegion = LuaScriptMgr.GetVector4(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clipSoftness(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UIPanel uIPanel = (UIPanel)luaObject;
		if (uIPanel == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clipSoftness");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clipSoftness on a nil value");
			}
		}
		uIPanel.clipSoftness = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel a = (UIPanel)LuaScriptMgr.GetUnityObject(L, 1, typeof(UIPanel));
		UIPanel b = (UIPanel)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIPanel));
		int d = UIPanel.CompareFunc(a, b);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSides(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		Transform relativeTo = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		Vector3[] sides = uIPanel.GetSides(relativeTo);
		LuaScriptMgr.PushArray(L, sides);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Invalidate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		bool boolean = LuaScriptMgr.GetBoolean(L, 2);
		uIPanel.Invalidate(boolean);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CalculateFinalAlpha(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		int frameID = (int)LuaScriptMgr.GetNumber(L, 2);
		float d = uIPanel.CalculateFinalAlpha(frameID);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetRect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		float x = (float)LuaScriptMgr.GetNumber(L, 2);
		float y = (float)LuaScriptMgr.GetNumber(L, 3);
		float width = (float)LuaScriptMgr.GetNumber(L, 4);
		float height = (float)LuaScriptMgr.GetNumber(L, 5);
		uIPanel.SetRect(x, y, width, height);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsVisible(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(UIPanel), typeof(UIWidget)))
		{
			UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
			UIWidget w = (UIWidget)LuaScriptMgr.GetLuaObject(L, 2);
			bool b = uIPanel.IsVisible(w);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(UIPanel), typeof(LuaTable)))
		{
			UIPanel uIPanel2 = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			bool b2 = uIPanel2.IsVisible(vector);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		if (num == 5)
		{
			UIPanel uIPanel3 = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 3);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 4);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 5);
			bool b3 = uIPanel3.IsVisible(vector2, vector3, vector4, vector5);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: UIPanel.IsVisible");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Affects(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		UIWidget w = (UIWidget)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIWidget));
		bool b = uIPanel.Affects(w);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RebuildAllDrawCalls(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		uIPanel.RebuildAllDrawCalls();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetDirty(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		uIPanel.SetDirty();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ParentHasChanged(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		uIPanel.ParentHasChanged();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SortWidgets(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		uIPanel.SortWidgets();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindDrawCall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		UIWidget w = (UIWidget)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIWidget));
		UIDrawCall obj = uIPanel.FindDrawCall(w);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddWidget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		UIWidget w = (UIWidget)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIWidget));
		uIPanel.AddWidget(w);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveWidget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		UIWidget w = (UIWidget)LuaScriptMgr.GetUnityObject(L, 2, typeof(UIWidget));
		uIPanel.RemoveWidget(w);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Refresh(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		uIPanel.Refresh();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CalculateConstrainOffset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		Vector2 vector = LuaScriptMgr.GetVector2(L, 2);
		Vector2 vector2 = LuaScriptMgr.GetVector2(L, 3);
		Vector3 v = uIPanel.CalculateConstrainOffset(vector, vector2);
		LuaScriptMgr.Push(L, v);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ConstrainTargetToBounds(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3)
		{
			UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
			Transform target = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			bool b = uIPanel.ConstrainTargetToBounds(target, boolean);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 4)
		{
			UIPanel uIPanel2 = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
			Transform target2 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			Bounds bounds = LuaScriptMgr.GetBounds(L, 3);
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 4);
			bool b2 = uIPanel2.ConstrainTargetToBounds(target2, ref bounds, boolean2);
			LuaScriptMgr.Push(L, b2);
			LuaScriptMgr.Push(L, bounds);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: UIPanel.ConstrainTargetToBounds");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Find(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			Transform trans = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
			UIPanel obj = UIPanel.Find(trans);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 2)
		{
			Transform trans2 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
			bool boolean = LuaScriptMgr.GetBoolean(L, 2);
			UIPanel obj2 = UIPanel.Find(trans2, boolean);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		if (num == 3)
		{
			Transform trans3 = (Transform)LuaScriptMgr.GetUnityObject(L, 1, typeof(Transform));
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 2);
			int layer = (int)LuaScriptMgr.GetNumber(L, 3);
			UIPanel obj3 = UIPanel.Find(trans3, boolean2, layer);
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: UIPanel.Find");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetViewSize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIPanel uIPanel = (UIPanel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIPanel");
		Vector2 viewSize = uIPanel.GetViewSize();
		LuaScriptMgr.Push(L, viewSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMainGameViewSize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		return 1;
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
