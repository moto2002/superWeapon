using LuaInterface;
using SimpleFramework.Manager;
using System;
using System.Reflection;
using UnityEngine;

namespace SimpleFramework
{
	public static class LuaHelper
	{
		public static Type GetType(string classname)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type type = executingAssembly.GetType(classname);
			if (type == null)
			{
				type = executingAssembly.GetType(classname);
			}
			return type;
		}

		public static PanelManager GetPanelManager()
		{
			return AppFacade.Instance.GetManager<PanelManager>("PanelManager");
		}

		public static ResourceManager GetResManager()
		{
			return AppFacade.Instance.GetManager<ResourceManager>("ResourceManager");
		}

		public static NetworkManager GetNetManager()
		{
			return AppFacade.Instance.GetManager<NetworkManager>("NetworkManager");
		}

		public static MusicManager GetMusicManager()
		{
			return AppFacade.Instance.GetManager<MusicManager>("MusicManager");
		}

		public static Action Action(LuaFunction func)
		{
			return delegate
			{
				func.Call();
			};
		}

		public static UIEventListener.VoidDelegate VoidDelegate(LuaFunction func)
		{
			return delegate(GameObject go)
			{
				func.Call(new object[]
				{
					go
				});
			};
		}

		public static void OnCallLuaFunc(LuaStringBuffer data, LuaFunction func)
		{
			byte[] buffer = data.buffer;
			if (func != null)
			{
				LuaScriptMgr manager = AppFacade.Instance.GetManager<LuaScriptMgr>("LuaScriptMgr");
				int oldTop = func.BeginPCall();
				LuaDLL.lua_pushlstring(manager.lua.L, buffer, buffer.Length);
				if (func.PCall(oldTop, 1))
				{
					func.EndPCall(oldTop);
				}
			}
			LogManage.LogWarning(string.Concat(new object[]
			{
				"OnCallLuaFunc buffer:>>",
				buffer,
				" lenght:>>",
				buffer.Length
			}));
		}

		public static void OnJsonCallFunc(string data, LuaFunction func)
		{
			LogManage.LogWarning(string.Concat(new object[]
			{
				"OnJsonCallback data:>>",
				data,
				" lenght:>>",
				data.Length
			}));
			if (func != null)
			{
				func.Call(new object[]
				{
					data
				});
			}
		}
	}
}
