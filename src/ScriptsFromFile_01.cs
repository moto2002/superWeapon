using LuaInterface;
using System;
using UnityEngine;

public class ScriptsFromFile_01 : MonoBehaviour
{
	public TextAsset scriptFile;

	private void Start()
	{
		LuaState luaState = new LuaState();
		luaState.DoString(this.scriptFile.text);
	}

	private void Update()
	{
	}
}
