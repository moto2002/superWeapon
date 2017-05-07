using System;
using UnityEngine;

public class DoLua : MonoBehaviour
{
	public string luaCallText;

	private void Start()
	{
		HUDTextTool.CallLua(this.luaCallText, new object[0]);
	}
}
