using System;
using UnityEngine;

public class IPPort : MonoBehaviour
{
	public static IPPort inst;

	public string[] ipList;

	public int[] portList;

	public string[] ipNames;

	private int ipAndPortIdx;

	public UIPopupList popList;

	public string Ip
	{
		get
		{
			return this.ipList[this.ipAndPortIdx];
		}
	}

	public int Port
	{
		get
		{
			return this.portList[this.ipAndPortIdx];
		}
	}

	public void OnDestroy()
	{
		IPPort.inst = null;
	}

	private void Awake()
	{
		IPPort.inst = this;
	}

	public void ChangeServer()
	{
		this.ipAndPortIdx = this.popList.PopIndex;
	}
}
