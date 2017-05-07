using System;
using UnityEngine;

public class T_TowerRadius : MonoBehaviour
{
	public T_Tower tower;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!GameSetting.IsByPhsic)
		{
			return;
		}
		if (other.CompareTag("Tank") && (UnitConst.GetInstance().buildingConst[this.tower.index].flak == 1 || UnitConst.GetInstance().buildingConst[this.tower.index].flak == 2))
		{
			this.OnTrigger(other);
		}
		else if (other.CompareTag("FlyTank") && (UnitConst.GetInstance().buildingConst[this.tower.index].flak == 3 || UnitConst.GetInstance().buildingConst[this.tower.index].flak == 2))
		{
			this.OnTrigger(other);
		}
	}

	private void OnTrigger(Collider other)
	{
		if (!GameSetting.IsByPhsic)
		{
			return;
		}
		Character component = other.GetComponent<Character>();
		if (this.tower.IsCanShootByCharType(component))
		{
			if (!this.tower.Targetes.Contains(component) && component.ga.activeSelf)
			{
				this.tower.Targetes.Add(component);
			}
			if (this.tower.Targetes.Count > 0 && !this.tower.IsDie && this.tower.towerFightState == T_TowerFightingState.TowerFightingStates.Idle)
			{
				this.tower.T_TowerFightingState.ChangeState(T_TowerFightingState.TowerFightingStates.Searching);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!GameSetting.IsByPhsic)
		{
			return;
		}
		if (other.CompareTag("Tank") || other.CompareTag("FlyTank"))
		{
			Character component = other.GetComponent<Character>();
			this.tower.Targetes.Remove(component);
		}
	}
}
