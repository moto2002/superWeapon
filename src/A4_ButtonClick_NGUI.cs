using LuaInterface;
using System;
using UnityEngine;

public class A4_ButtonClick_NGUI : MonoBehaviour
{
	public UIButton button;

	private string script = "                  \n        function doClick(go)\n            print('UIButton Click:>>>'..go.name)\n        end\n\n        function TestClick(go)                     \n            UIEventListener.Get(go).onClick = doClick;\n        end\n    ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.DoString(this.script);
		LuaFunction luaFunction = luaScriptMgr.GetLuaFunction("TestClick");
		luaFunction.Call(new object[]
		{
			this.button.gameObject
		});
	}

	private void Update()
	{
	}
}
