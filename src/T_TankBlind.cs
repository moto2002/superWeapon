using System;
using UnityEngine;

public class T_TankBlind : MonoBehaviour
{
	public T_Tank tank;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!GameSetting.IsByPhsic)
		{
			return;
		}
		if (other.CompareTag("Tank") || other.CompareTag("Tower"))
		{
			Character component = other.GetComponent<Character>();
			if (component && this.tank.IsCanShootByCharType(component) && this.tank.Targetes.Contains(component))
			{
				this.tank.Targetes.Remove(component);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!GameSetting.IsByPhsic)
		{
			return;
		}
		if (other.CompareTag("Tank") || other.CompareTag("Tower"))
		{
			Character component = other.GetComponent<Character>();
			if (component && this.tank.IsCanShootByCharType(component))
			{
				if (!this.tank.Targetes.Contains(component))
				{
					this.tank.Targetes.Add(component);
				}
				if (this.tank.Targetes.Count == 1)
				{
					this.tank.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
				}
			}
		}
	}
}
