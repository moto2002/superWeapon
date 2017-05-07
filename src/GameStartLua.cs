using SimpleFramework.Manager;
using System;
using UnityEngine;

public class GameStartLua : MonoBehaviour
{
	private LuaScriptMgr lua;

	public Camera cam;

	public static GameStartLua ins;

	public void OnDestroy()
	{
		GameStartLua.ins = null;
	}

	private void Awake()
	{
		GameTools.GetCompentIfNoAddOne<GameStart_C>(this.cam.gameObject);
		GameStartLua.ins = this;
		this.lua = GameManager.Instance.GetLuaManage();
	}

	private void Start()
	{
		HUDTextTool.CallLua("GameStartLua.DoJob", new object[]
		{
			base.transform
		});
	}

	private void Update()
	{
		if (this.lua != null)
		{
			this.lua.Update();
		}
	}

	private void LateUpdate()
	{
		if (this.lua != null)
		{
			this.lua.LateUpate();
		}
	}

	private void FixedUpdate()
	{
		if (this.lua != null)
		{
			this.lua.FixedUpdate();
		}
	}
}
