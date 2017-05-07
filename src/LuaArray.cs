using LuaInterface;
using System;
using UnityEngine;

public class LuaArray : MonoBehaviour
{
	private string source = "\n        function luaFunc(objs, len)\n            for i = 0, len - 1 do\n                print(objs[i])\n            end\n            local table1 = {'111', '222', '333'}\n            return unpack(table1)\n        end\n    ";

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
		LuaState lua = luaScriptMgr.lua;
		lua.DoString(this.source);
		LuaFunction function = lua.GetFunction("luaFunc");
		object[] array = function.Call(new object[]
		{
			this.objs,
			this.objs.Length
		});
		function.Release();
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			object obj = array2[i];
			LogManage.Log(obj.ToString());
		}
	}
}
