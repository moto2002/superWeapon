using SimpleFramework;
using SimpleFramework.Manager;
using System;
using UnityEngine;

public class StartUpCommand : ControllerCommand
{
	public override void Execute(IMessage message)
	{
		if (!Util.CheckEnvironment())
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GlobalGenerator");
		if (gameObject != null)
		{
			AppView appView = gameObject.AddComponent<AppView>();
		}
		AppFacade.Instance.RegisterCommand("DispatchMessage", typeof(SocketCommand));
		AppFacade.Instance.AddManager("LuaScriptMgr", new LuaScriptMgr());
		AppFacade.Instance.AddManager<GameManager>("GameManager");
		LogManage.Log("SimpleFramework StartUp-------->>>>>");
	}
}
