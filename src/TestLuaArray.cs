using LuaInterface;
using System;
using UnityEngine;

public class TestLuaArray : MonoBehaviour
{
	private string script = "                                   \n            function TestArray(objs)                \n                local len = objs.Length\n                \n                for i = 0, len - 1 do\n                    print(objs[i])\n                end\n                return 1, '123', true\n            end\n        ";

	private string[] objs = new string[]
	{
		"aaa",
		"bbb",
		"ccc"
	};

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.DoString(this.script);
		LuaFunction luaFunction = luaScriptMgr.GetLuaFunction("TestArray");
		object[] array = luaFunction.Call(new object[]
		{
			this.objs
		});
		luaFunction.Release();
		for (int i = 0; i < this.objs.Length; i++)
		{
			LogManage.Log(array[i].ToString());
		}
	}
}
