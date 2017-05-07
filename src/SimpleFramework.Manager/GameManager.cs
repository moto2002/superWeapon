using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace SimpleFramework.Manager
{
	public class GameManager : LuaBehaviour
	{
		private List<string> downloadFiles = new List<string>();

		public static GameManager Instance;

		protected new void Awake()
		{
			GameManager.Instance = this;
			this.InitData();
		}

		public LuaScriptMgr GetLuaManage()
		{
			return base.LuaManager;
		}

		private void OnGUI()
		{
		}

		private void InitData()
		{
			GameTools.DontDestroyOnLoad(base.gameObject);
			if (Init.inst)
			{
				Init.inst.noProcessGa.GetComponent<UILabel>().text = "开始解包";
			}
			this.CheckExtractResource();
			Application.targetFrameRate = 60;
		}

		public void CheckExtractResource()
		{
			if (((Directory.Exists(Util.DataPath) && Directory.Exists(Util.DataPath + "lua/") && File.Exists(Util.DataPath + "files.txt")) || false) && User.GetJY() == 1 && User.GetVer().Equals(GameSetting.Version))
			{
				this.OnResourceInited();
				return;
			}
			base.StartCoroutine(this.OnExtractResource());
		}

		[DebuggerHidden]
		private IEnumerator OnExtractResource()
		{
			GameManager.<OnExtractResource>c__Iterator9C <OnExtractResource>c__Iterator9C = new GameManager.<OnExtractResource>c__Iterator9C();
			<OnExtractResource>c__Iterator9C.<>f__this = this;
			return <OnExtractResource>c__Iterator9C;
		}

		public void OnResourceInited()
		{
			base.LuaManager.Start();
			Screen.sleepTimeout = -2;
			if (Init.inst)
			{
				Init.inst.noProcessGa.GetComponent<UILabel>().text = "解包完成";
			}
			LoginInitUI.inst.GetVerByServer();
		}

		private void OnUpdateFailed(string file)
		{
			string body = "更新失败!>" + file;
			base.facade.SendMessageCommand("UpdateMessage", body);
		}

		private void Update()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				base.LuaManager.Update();
			}
		}

		private void LateUpdate()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				base.LuaManager.LateUpate();
			}
		}

		private void FixedUpdate()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				base.LuaManager.FixedUpdate();
			}
		}

		protected new void OnDestroy()
		{
			if (base.NetManager != null)
			{
				base.NetManager.Unload();
			}
			if (base.LuaManager != null)
			{
				base.LuaManager.Destroy();
			}
			LogManage.Log("~GameManager was destroyed");
		}
	}
}
