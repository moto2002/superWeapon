using LuaInterface;
using System;

public class DelegateFactoryWrap
{
	private static Type classType = typeof(DelegateFactory);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Action_GameObject", new LuaCSFunction(DelegateFactoryWrap.Action_GameObject)),
			new LuaMethod("Action", new LuaCSFunction(DelegateFactoryWrap.Action)),
			new LuaMethod("UnityEngine_Events_UnityAction", new LuaCSFunction(DelegateFactoryWrap.UnityEngine_Events_UnityAction)),
			new LuaMethod("System_Reflection_MemberFilter", new LuaCSFunction(DelegateFactoryWrap.System_Reflection_MemberFilter)),
			new LuaMethod("System_Reflection_TypeFilter", new LuaCSFunction(DelegateFactoryWrap.System_Reflection_TypeFilter)),
			new LuaMethod("UIEventListener_VoidDelegate", new LuaCSFunction(DelegateFactoryWrap.UIEventListener_VoidDelegate)),
			new LuaMethod("UIEventListener_BoolDelegate", new LuaCSFunction(DelegateFactoryWrap.UIEventListener_BoolDelegate)),
			new LuaMethod("UIEventListener_FloatDelegate", new LuaCSFunction(DelegateFactoryWrap.UIEventListener_FloatDelegate)),
			new LuaMethod("UIEventListener_VectorDelegate", new LuaCSFunction(DelegateFactoryWrap.UIEventListener_VectorDelegate)),
			new LuaMethod("UIEventListener_ObjectDelegate", new LuaCSFunction(DelegateFactoryWrap.UIEventListener_ObjectDelegate)),
			new LuaMethod("UIEventListener_KeyCodeDelegate", new LuaCSFunction(DelegateFactoryWrap.UIEventListener_KeyCodeDelegate)),
			new LuaMethod("UIPanel_OnGeometryUpdated", new LuaCSFunction(DelegateFactoryWrap.UIPanel_OnGeometryUpdated)),
			new LuaMethod("UIPanel_OnClippingMoved", new LuaCSFunction(DelegateFactoryWrap.UIPanel_OnClippingMoved)),
			new LuaMethod("UIWidget_OnDimensionsChanged", new LuaCSFunction(DelegateFactoryWrap.UIWidget_OnDimensionsChanged)),
			new LuaMethod("UIWidget_OnPostFillCallback", new LuaCSFunction(DelegateFactoryWrap.UIWidget_OnPostFillCallback)),
			new LuaMethod("UIDrawCall_OnRenderCallback", new LuaCSFunction(DelegateFactoryWrap.UIDrawCall_OnRenderCallback)),
			new LuaMethod("UIWidget_HitCheck", new LuaCSFunction(DelegateFactoryWrap.UIWidget_HitCheck)),
			new LuaMethod("UIGrid_OnReposition", new LuaCSFunction(DelegateFactoryWrap.UIGrid_OnReposition)),
			new LuaMethod("Comparison_Transform", new LuaCSFunction(DelegateFactoryWrap.Comparison_Transform)),
			new LuaMethod("TestLuaDelegate_VoidDelegate", new LuaCSFunction(DelegateFactoryWrap.TestLuaDelegate_VoidDelegate)),
			new LuaMethod("EventDelegate_Callback", new LuaCSFunction(DelegateFactoryWrap.EventDelegate_Callback)),
			new LuaMethod("AudioClip_PCMReaderCallback", new LuaCSFunction(DelegateFactoryWrap.AudioClip_PCMReaderCallback)),
			new LuaMethod("AudioClip_PCMSetPositionCallback", new LuaCSFunction(DelegateFactoryWrap.AudioClip_PCMSetPositionCallback)),
			new LuaMethod("Application_LogCallback", new LuaCSFunction(DelegateFactoryWrap.Application_LogCallback)),
			new LuaMethod("Clear", new LuaCSFunction(DelegateFactoryWrap.Clear)),
			new LuaMethod("New", new LuaCSFunction(DelegateFactoryWrap._CreateDelegateFactory)),
			new LuaMethod("GetClassType", new LuaCSFunction(DelegateFactoryWrap.GetClassType))
		};
		LuaScriptMgr.RegisterLib(L, "DelegateFactory", regs);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateDelegateFactory(IntPtr L)
	{
		LuaDLL.luaL_error(L, "DelegateFactory class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, DelegateFactoryWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Action_GameObject(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Action_GameObject(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Action(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Action(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_Events_UnityAction(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UnityEngine_Events_UnityAction(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int System_Reflection_MemberFilter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.System_Reflection_MemberFilter(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int System_Reflection_TypeFilter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.System_Reflection_TypeFilter(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIEventListener_VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIEventListener_VoidDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIEventListener_BoolDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIEventListener_BoolDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIEventListener_FloatDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIEventListener_FloatDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIEventListener_VectorDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIEventListener_VectorDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIEventListener_ObjectDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIEventListener_ObjectDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIEventListener_KeyCodeDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIEventListener_KeyCodeDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIPanel_OnGeometryUpdated(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIPanel_OnGeometryUpdated(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIPanel_OnClippingMoved(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIPanel_OnClippingMoved(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIWidget_OnDimensionsChanged(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIWidget_OnDimensionsChanged(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIWidget_OnPostFillCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIWidget_OnPostFillCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIDrawCall_OnRenderCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIDrawCall_OnRenderCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIWidget_HitCheck(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIWidget_HitCheck(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UIGrid_OnReposition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.UIGrid_OnReposition(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Comparison_Transform(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Comparison_Transform(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TestLuaDelegate_VoidDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.TestLuaDelegate_VoidDelegate(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EventDelegate_Callback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.EventDelegate_Callback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AudioClip_PCMReaderCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.AudioClip_PCMReaderCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AudioClip_PCMSetPositionCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.AudioClip_PCMSetPositionCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Application_LogCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LuaFunction luaFunction = LuaScriptMgr.GetLuaFunction(L, 1);
		Delegate o = DelegateFactory.Application_LogCallback(luaFunction);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		DelegateFactory.Clear();
		return 0;
	}
}
