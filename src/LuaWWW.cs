using System;
using UnityEngine;

public class LuaWWW : MonoBehaviour
{
	private LuaScriptMgr lua;

	private string script = "      \n        local WWW = UnityEngine.WWW\n                             \n        function testFunc()\n            local www = WWW('http://bbs.ulua.org/readme.txt');\n            coroutine.www(www);\n            print(www.text);    \n        end\n        \n        coroutine.start(testFunc)\n    ";

	private void Start()
	{
		this.lua = new LuaScriptMgr();
		this.lua.Start();
		this.lua.DoString(this.script);
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
