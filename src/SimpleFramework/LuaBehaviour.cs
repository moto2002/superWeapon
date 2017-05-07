using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFramework
{
	public class LuaBehaviour : View
	{
		protected static bool initialize;

		private string data;

		private AssetBundle bundle;

		private List<LuaFunction> buttons = new List<LuaFunction>();

		protected void Awake()
		{
			this.CallMethod("Awake", new object[]
			{
				base.gameObject
			});
		}

		protected void Start()
		{
			if (base.LuaManager != null && LuaBehaviour.initialize)
			{
				LuaState lua = base.LuaManager.lua;
				lua[base.name + ".transform"] = base.transform;
				lua[base.name + ".gameObject"] = base.gameObject;
			}
			this.CallMethod("Start", new object[0]);
		}

		protected void OnClick()
		{
			this.CallMethod("OnClick", new object[0]);
		}

		protected void OnClickEvent(GameObject go)
		{
			this.CallMethod("OnClick", new object[]
			{
				go
			});
		}

		public void OnInit(AssetBundle bundle, string text = null)
		{
			this.data = text;
			this.bundle = bundle;
			LogManage.LogWarning("OnInit---->>>" + base.name + " text:>" + text);
		}

		public GameObject GetGameObject(string name)
		{
			if (this.bundle == null)
			{
				return null;
			}
			return Util.LoadAsset(this.bundle, name);
		}

		public void AddClick(GameObject go, LuaFunction luafunc)
		{
			if (go == null)
			{
				return;
			}
			UIEventListener.Get(go).onClick = delegate(GameObject o)
			{
				luafunc.Call(new object[]
				{
					go
				});
				this.buttons.Add(luafunc);
			};
		}

		public void ClearClick()
		{
			for (int i = 0; i < this.buttons.Count; i++)
			{
				if (this.buttons[i] != null)
				{
					this.buttons[i].Dispose();
					this.buttons[i] = null;
				}
			}
		}

		protected object[] CallMethod(string func, params object[] args)
		{
			if (!LuaBehaviour.initialize)
			{
				return null;
			}
			return Util.CallMethod(base.name, func, args);
		}

		protected void OnDestroy()
		{
			if (this.bundle)
			{
				this.bundle.Unload(true);
				this.bundle = null;
			}
			this.ClearClick();
			base.LuaManager = null;
			Util.ClearMemory();
			LogManage.Log("~" + base.name + " was destroy!");
		}
	}
}
