using System;
using UnityEngine;

public class T_ToweBlind : MonoBehaviour
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
		if (other.CompareTag("Tank") || other.CompareTag("FlyTank"))
		{
			Character component = other.GetComponent<Character>();
			if (this.tower.IsCanShootByCharType(component) && this.tower.Targetes.Contains(component))
			{
				this.tower.Targetes.Remove(component);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!GameSetting.IsByPhsic)
		{
			return;
		}
		if (other.CompareTag("Tank") && (UnitConst.GetInstance().buildingConst[this.tower.index].flak == 1 || UnitConst.GetInstance().buildingConst[this.tower.index].flak == 2))
		{
			Character component = other.GetComponent<Character>();
			if (this.tower.IsCanShootByCharType(component))
			{
				if (!this.tower.Targetes.Contains(component) && component.ga.activeSelf)
				{
					this.tower.Targetes.Add(component);
				}
				if (this.tower.Targetes.Count == 1)
				{
					this.tower.T_TowerFightingState.ChangeState(T_TowerFightingState.TowerFightingStates.Searching);
				}
			}
		}
		else if (other.CompareTag("FlyTank") && (UnitConst.GetInstance().buildingConst[this.tower.index].flak == 3 || UnitConst.GetInstance().buildingConst[this.tower.index].flak == 2))
		{
			Character component2 = other.GetComponent<Character>();
			if (this.tower.IsCanShootByCharType(component2))
			{
				if (!this.tower.Targetes.Contains(component2) && component2.ga.activeSelf)
				{
					this.tower.Targetes.Add(component2);
				}
				if (this.tower.Targetes.Count == 1)
				{
					this.tower.T_TowerFightingState.ChangeState(T_TowerFightingState.TowerFightingStates.Searching);
				}
			}
		}
	}
}
