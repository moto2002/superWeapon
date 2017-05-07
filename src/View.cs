using SimpleFramework.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour, IView
{
	private AppFacade m_Facade;

	private LuaScriptMgr m_LuaMgr;

	private ResourceManager m_ResMgr;

	private NetworkManager m_NetMgr;

	private MusicManager m_MusicMgr;

	private TimerManager m_TimerMgr;

	private ThreadManager m_ThreadMgr;

	protected AppFacade facade
	{
		get
		{
			if (this.m_Facade == null)
			{
				this.m_Facade = AppFacade.Instance;
			}
			return this.m_Facade;
		}
	}

	protected LuaScriptMgr LuaManager
	{
		get
		{
			if (this.m_LuaMgr == null)
			{
				this.m_LuaMgr = this.facade.GetManager<LuaScriptMgr>("LuaScriptMgr");
			}
			return this.m_LuaMgr;
		}
		set
		{
			this.m_LuaMgr = value;
		}
	}

	protected ResourceManager ResManager
	{
		get
		{
			if (this.m_ResMgr == null)
			{
				this.m_ResMgr = this.facade.GetManager<ResourceManager>("ResourceManager");
			}
			return this.m_ResMgr;
		}
	}

	protected NetworkManager NetManager
	{
		get
		{
			if (this.m_NetMgr == null)
			{
				this.m_NetMgr = this.facade.GetManager<NetworkManager>("NetworkManager");
			}
			return this.m_NetMgr;
		}
	}

	protected MusicManager MusicManager
	{
		get
		{
			if (this.m_MusicMgr == null)
			{
				this.m_MusicMgr = this.facade.GetManager<MusicManager>("MusicManager");
			}
			return this.m_MusicMgr;
		}
	}

	protected TimerManager TimerManger
	{
		get
		{
			if (this.m_TimerMgr == null)
			{
				this.m_TimerMgr = this.facade.GetManager<TimerManager>("TimeManager");
			}
			return this.m_TimerMgr;
		}
	}

	protected ThreadManager ThreadManager
	{
		get
		{
			if (this.m_ThreadMgr == null)
			{
				this.m_ThreadMgr = this.facade.GetManager<ThreadManager>("ThreadManager");
			}
			return this.m_ThreadMgr;
		}
	}

	public virtual void OnMessage(IMessage message)
	{
	}

	protected void RegisterMessage(IView view, List<string> messages)
	{
		if (messages == null || messages.Count == 0)
		{
			return;
		}
		Controller.Instance.RegisterViewCommand(view, messages.ToArray());
	}

	protected void RemoveMessage(IView view, List<string> messages)
	{
		if (messages == null || messages.Count == 0)
		{
			return;
		}
		Controller.Instance.RemoveViewCommand(view, messages.ToArray());
	}
}
