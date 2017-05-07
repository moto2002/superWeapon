using LuaInterface;
using System;
using UnityEngine;

public class CallLuaFunction_02 : MonoBehaviour
{
	private string script = "\n            function luaFunc(num)                \n                return num\n            end\n        ";

	private LuaFunction func;

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.DoString(this.script);
		this.func = luaScriptMgr.GetLuaFunction("luaFunc");
		object[] array = this.func.Call(123456.0);
		MonoBehaviour.print(array[0]);
		int num = this.CallFunc();
		MonoBehaviour.print(num);
	}

	private void OnDestroy()
	{
		if (this.func != null)
		{
			this.func.Release();
		}
	}

	private int CallFunc()
	{
		int oldTop = this.func.BeginPCall();
		IntPtr luaState = this.func.GetLuaState();
		LuaScriptMgr.Push(luaState, 123456);
		this.func.PCall(oldTop, 1);
		int result = (int)LuaScriptMgr.GetNumber(luaState, -1);
		this.func.EndPCall(oldTop);
		return result;
	}

	private void Update()
	{
	}
}
