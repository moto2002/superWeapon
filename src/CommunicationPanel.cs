using System;
using UnityEngine;

public class CommunicationPanel : MonoBehaviour
{
	public GameObject communication;

	public static CommunicationPanel _Inst;

	public void OnDestroy()
	{
		CommunicationPanel._Inst = null;
	}

	public void Awake()
	{
		CommunicationPanel._Inst = this;
	}

	public void SetActiveTrue()
	{
		if (!this.communication.gameObject.activeInHierarchy)
		{
			this.communication.gameObject.SetActive(true);
		}
	}

	public void SetActiveFalse()
	{
		base.StopAllCoroutines();
		if (this.communication.gameObject.activeInHierarchy)
		{
			this.communication.gameObject.SetActive(false);
		}
	}
}
