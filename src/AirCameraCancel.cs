using System;
using UnityEngine;

public class AirCameraCancel : MonoBehaviour
{
	public GameObject go1;

	public GameObject go2;

	private void Awake()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanel_AirCameraCancel, new EventManager.VoidDelegate(this.Click));
	}

	private void Click(GameObject ga)
	{
	}

	private void Update()
	{
		this.go1.layer = 30;
		this.go2.layer = 30;
		if (FightPanelManager.inst)
		{
			this.go1.transform.position = FightPanelManager.inst.AutoFight.transform.position;
		}
	}
}
