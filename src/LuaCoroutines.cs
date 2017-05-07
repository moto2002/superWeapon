using LuaInterface;
using System;
using UnityEngine;

public class LuaCoroutines : MonoBehaviour
{
	private string script = "                                   \n            function fib(n)\n                local a, b = 0, 1\n                while n > 0 do\n                    a, b = b, a + b\n                    n = n - 1\n                end\n\n                return a\n            end\n\n            function CoFunc()\n              while  true do\n                print('Coroutine started')\n                local i = 0\n                for i = 0, 10, 1 do\n                    print(fib(i))                    \n                    coroutine.wait(1)\n                end\n                print('Coroutine ended')\nend\n            end\n\n            function myFunc()\n                coroutine.start(CoFunc)\n            end\n        ";

	private LuaScriptMgr lua;

	private void Awake()
	{
		this.lua = new LuaScriptMgr();
		this.lua.Start();
		this.lua.DoString(this.script);
		LuaFunction luaFunction = this.lua.GetLuaFunction("myFunc");
		luaFunction.Call();
		luaFunction.Release();
	}

	private void Update()
	{
		this.lua.Update();
	}

	private void LateUpdate()
	{
		this.lua.LateUpate();
	}

	private void FixedUpdate()
	{
		this.lua.FixedUpdate();
	}
}
