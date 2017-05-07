using System;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
	public static HeartBeat inst;

	public void OnDestroy()
	{
		HeartBeat.inst = null;
	}

	private void Awake()
	{
		HeartBeat.inst = this;
	}

	public void StartHeartbeat()
	{
		base.InvokeRepeating("Heartbeat", 1f, 10f);
		base.InvokeRepeating("Heartbeat_Socket", 10f, 60f);
	}

	public void StopHeartbeat()
	{
		base.CancelInvoke("Heartbeat");
	}

	public void Heartbeat()
	{
		HeartbeatHandler.CG_Heartbeat();
		ActivityManager.GetIns().CheckTimeOut();
	}

	public void Heartbeat_Socket()
	{
		if (NetMgr.inst != null && NetMgr.inst.Net_Socket != null)
		{
			NetMgr.inst.Net_Socket.SocketHeart();
		}
	}
}
