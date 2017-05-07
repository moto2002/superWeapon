using LuaInterface;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public static class DelegateFactory
{
	private delegate Delegate DelegateValue(LuaFunction func);

	private static Dictionary<Type, DelegateFactory.DelegateValue> dict = new Dictionary<Type, DelegateFactory.DelegateValue>();

	[NoToLua]
	public static void Register(IntPtr L)
	{
		DelegateFactory.dict.Add(typeof(Action<GameObject>), new DelegateFactory.DelegateValue(DelegateFactory.Action_GameObject));
		DelegateFactory.dict.Add(typeof(Action), new DelegateFactory.DelegateValue(DelegateFactory.Action));
		DelegateFactory.dict.Add(typeof(UnityAction), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_Events_UnityAction));
		DelegateFactory.dict.Add(typeof(MemberFilter), new DelegateFactory.DelegateValue(DelegateFactory.System_Reflection_MemberFilter));
		DelegateFactory.dict.Add(typeof(TypeFilter), new DelegateFactory.DelegateValue(DelegateFactory.System_Reflection_TypeFilter));
		DelegateFactory.dict.Add(typeof(UIEventListener.VoidDelegate), new DelegateFactory.DelegateValue(DelegateFactory.UIEventListener_VoidDelegate));
		DelegateFactory.dict.Add(typeof(UIEventListener.BoolDelegate), new DelegateFactory.DelegateValue(DelegateFactory.UIEventListener_BoolDelegate));
		DelegateFactory.dict.Add(typeof(UIEventListener.FloatDelegate), new DelegateFactory.DelegateValue(DelegateFactory.UIEventListener_FloatDelegate));
		DelegateFactory.dict.Add(typeof(UIEventListener.VectorDelegate), new DelegateFactory.DelegateValue(DelegateFactory.UIEventListener_VectorDelegate));
		DelegateFactory.dict.Add(typeof(UIEventListener.ObjectDelegate), new DelegateFactory.DelegateValue(DelegateFactory.UIEventListener_ObjectDelegate));
		DelegateFactory.dict.Add(typeof(UIEventListener.KeyCodeDelegate), new DelegateFactory.DelegateValue(DelegateFactory.UIEventListener_KeyCodeDelegate));
		DelegateFactory.dict.Add(typeof(UIPanel.OnGeometryUpdated), new DelegateFactory.DelegateValue(DelegateFactory.UIPanel_OnGeometryUpdated));
		DelegateFactory.dict.Add(typeof(UIPanel.OnClippingMoved), new DelegateFactory.DelegateValue(DelegateFactory.UIPanel_OnClippingMoved));
		DelegateFactory.dict.Add(typeof(UIWidget.OnDimensionsChanged), new DelegateFactory.DelegateValue(DelegateFactory.UIWidget_OnDimensionsChanged));
		DelegateFactory.dict.Add(typeof(UIWidget.HitCheck), new DelegateFactory.DelegateValue(DelegateFactory.UIWidget_HitCheck));
		DelegateFactory.dict.Add(typeof(UIGrid.OnReposition), new DelegateFactory.DelegateValue(DelegateFactory.UIGrid_OnReposition));
		DelegateFactory.dict.Add(typeof(Comparison<Transform>), new DelegateFactory.DelegateValue(DelegateFactory.Comparison_Transform));
		DelegateFactory.dict.Add(typeof(TestLuaDelegate.VoidDelegate), new DelegateFactory.DelegateValue(DelegateFactory.TestLuaDelegate_VoidDelegate));
		DelegateFactory.dict.Add(typeof(EventDelegate.Callback), new DelegateFactory.DelegateValue(DelegateFactory.EventDelegate_Callback));
		DelegateFactory.dict.Add(typeof(AudioClip.PCMReaderCallback), new DelegateFactory.DelegateValue(DelegateFactory.AudioClip_PCMReaderCallback));
		DelegateFactory.dict.Add(typeof(AudioClip.PCMSetPositionCallback), new DelegateFactory.DelegateValue(DelegateFactory.AudioClip_PCMSetPositionCallback));
		DelegateFactory.dict.Add(typeof(Application.LogCallback), new DelegateFactory.DelegateValue(DelegateFactory.Application_LogCallback));
	}

	[NoToLua]
	public static Delegate CreateDelegate(Type t, LuaFunction func)
	{
		DelegateFactory.DelegateValue delegateValue = null;
		if (!DelegateFactory.dict.TryGetValue(t, out delegateValue))
		{
			Debugger.LogError("Delegate {0} not register", new object[]
			{
				t.FullName
			});
			return null;
		}
		return delegateValue(func);
	}

	public static Delegate Action_GameObject(LuaFunction func)
	{
		return new Action<GameObject>(delegate(GameObject param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate Action(LuaFunction func)
	{
		return new Action(delegate
		{
			func.Call();
		});
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		return new UnityAction(delegate
		{
			func.Call();
		});
	}

	public static Delegate System_Reflection_MemberFilter(LuaFunction func)
	{
		return new MemberFilter(delegate(MemberInfo param0, object param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.PushObject(luaState, param0);
			LuaScriptMgr.PushVarObject(luaState, param1);
			func.PCall(oldTop, 2);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (bool)array[0];
		});
	}

	public static Delegate System_Reflection_TypeFilter(LuaFunction func)
	{
		return new TypeFilter(delegate(Type param0, object param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.PushVarObject(luaState, param1);
			func.PCall(oldTop, 2);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (bool)array[0];
		});
	}

	public static Delegate UIEventListener_VoidDelegate(LuaFunction func)
	{
		return new UIEventListener.VoidDelegate(delegate(GameObject param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate UIEventListener_BoolDelegate(LuaFunction func)
	{
		return new UIEventListener.BoolDelegate(delegate(GameObject param0, bool param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			func.PCall(oldTop, 2);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate UIEventListener_FloatDelegate(LuaFunction func)
	{
		return new UIEventListener.FloatDelegate(delegate(GameObject param0, float param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			func.PCall(oldTop, 2);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate UIEventListener_VectorDelegate(LuaFunction func)
	{
		return new UIEventListener.VectorDelegate(delegate(GameObject param0, Vector2 param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			func.PCall(oldTop, 2);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate UIEventListener_ObjectDelegate(LuaFunction func)
	{
		return new UIEventListener.ObjectDelegate(delegate(GameObject param0, GameObject param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			func.PCall(oldTop, 2);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate UIEventListener_KeyCodeDelegate(LuaFunction func)
	{
		return new UIEventListener.KeyCodeDelegate(delegate(GameObject param0, KeyCode param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			func.PCall(oldTop, 2);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate UIPanel_OnGeometryUpdated(LuaFunction func)
	{
		return new UIPanel.OnGeometryUpdated(delegate
		{
			func.Call();
		});
	}

	public static Delegate UIPanel_OnClippingMoved(LuaFunction func)
	{
		return new UIPanel.OnClippingMoved(delegate(UIPanel param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate UIWidget_OnDimensionsChanged(LuaFunction func)
	{
		return new UIWidget.OnDimensionsChanged(delegate
		{
			func.Call();
		});
	}

	public static Delegate UIWidget_OnPostFillCallback(LuaFunction func)
	{
		return null;
	}

	public static Delegate UIDrawCall_OnRenderCallback(LuaFunction func)
	{
		return null;
	}

	public static Delegate UIWidget_HitCheck(LuaFunction func)
	{
		return new UIWidget.HitCheck(delegate(Vector3 param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (bool)array[0];
		});
	}

	public static Delegate UIGrid_OnReposition(LuaFunction func)
	{
		return new UIGrid.OnReposition(delegate
		{
			func.Call();
		});
	}

	public static Delegate Comparison_Transform(LuaFunction func)
	{
		return new Comparison<Transform>(delegate(Transform param0, Transform param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			func.PCall(oldTop, 2);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (int)array[0];
		});
	}

	public static Delegate TestLuaDelegate_VoidDelegate(LuaFunction func)
	{
		return new TestLuaDelegate.VoidDelegate(delegate(GameObject param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate EventDelegate_Callback(LuaFunction func)
	{
		return new EventDelegate.Callback(delegate
		{
			func.Call();
		});
	}

	public static Delegate AudioClip_PCMReaderCallback(LuaFunction func)
	{
		return new AudioClip.PCMReaderCallback(delegate(float[] param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.PushArray(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate AudioClip_PCMSetPositionCallback(LuaFunction func)
	{
		return new AudioClip.PCMSetPositionCallback(delegate(int param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		});
	}

	public static Delegate Application_LogCallback(LuaFunction func)
	{
		return new Application.LogCallback(delegate(string param0, string param1, LogType param2)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			LuaScriptMgr.Push(luaState, param2);
			func.PCall(oldTop, 3);
			func.EndPCall(oldTop);
		});
	}

	public static void Clear()
	{
		DelegateFactory.dict.Clear();
	}
}
