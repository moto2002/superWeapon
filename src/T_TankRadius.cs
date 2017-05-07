using System;
using UnityEngine;

public class T_TankRadius : MonoBehaviour
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
		if (other.CompareTag("Tower"))
		{
			T_Tower componentInParent = other.GetComponentInParent<T_Tower>();
			if (componentInParent == null || UnitConst.GetInstance().buildingConst[componentInParent.index].resType == 5 || UnitConst.GetInstance().buildingConst[componentInParent.index].resType == 3)
			{
				return;
			}
			if (this.tank.IsCanShootByCharType(componentInParent) && componentInParent.ga.activeSelf)
			{
				if (!this.tank.Targetes.Contains(componentInParent))
				{
					this.tank.Targetes.Add(componentInParent);
				}
				if (this.tank.State == T_TankFightState.TankFightState.Idle && this.tank.Targetes.Count > 0)
				{
					this.tank.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
				}
			}
		}
		if (other.CompareTag("Tank"))
		{
			T_Tank component = other.GetComponent<T_Tank>();
			if (component == null)
			{
				return;
			}
			if (this.tank.IsCanShootByCharType(component) && component.ga.activeSelf)
			{
				if (!this.tank.Targetes.Contains(component))
				{
					this.tank.Targetes.Add(component);
				}
				if (this.tank.State == T_TankFightState.TankFightState.Idle && this.tank.Targetes.Count > 0 && !this.tank.IsDie)
				{
					this.tank.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
				}
			}
		}
		if (other.CompareTag("FlyTank"))
		{
			T_AirShip component2 = other.GetComponent<T_AirShip>();
			if (component2 == null)
			{
				return;
			}
			if (this.tank.IsCanShootByCharType(component2) && component2.ga.activeSelf)
			{
				if (!this.tank.Targetes.Contains(component2))
				{
					this.tank.Targetes.Add(component2);
				}
				if (this.tank.State == T_TankFightState.TankFightState.Idle && this.tank.Targetes.Count > 0 && !this.tank.IsDie)
				{
					this.tank.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
				}
			}
		}
		T_TankFightState.TankFightState state = this.tank.State;
		if (state != T_TankFightState.TankFightState.ForceMoving)
		{
			if (state != T_TankFightState.TankFightState.ForceAttack)
			{
				if (other.CompareTag("Tower"))
				{
					Character componentInParent2 = other.GetComponentInParent<Character>();
					this.tank.canFire = true;
				}
			}
			else if (other.transform == this.tank.forcsTarget)
			{
				Character component3 = this.tank.forcsTarget.GetComponent<Character>();
				this.tank.canFire = true;
			}
		}
		else if (other.CompareTag("Tower"))
		{
			T_Tower componentInParent3 = other.GetComponentInParent<T_Tower>();
			if (UnitConst.GetInstance().buildingConst[componentInParent3.index].resType == 5)
			{
				return;
			}
			if (GameSetting.autoFight)
			{
				this.tank.lookTransform = other.transform;
				this.tank.resetLookTransform = true;
			}
			this.tank.attack = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!GameSetting.IsByPhsic)
		{
			return;
		}
		if (other.CompareTag("Tower"))
		{
			T_Tower componentInParent = other.GetComponentInParent<T_Tower>();
			if (componentInParent == null || UnitConst.GetInstance().buildingConst[componentInParent.index].resType == 5 || UnitConst.GetInstance().buildingConst[componentInParent.index].resType == 3)
			{
				return;
			}
			this.tank.Targetes.Remove(componentInParent);
		}
		if (other.CompareTag("FlyTank"))
		{
			T_AirShip component = other.GetComponent<T_AirShip>();
			if (component != null && component.charaType != this.tank.charaType)
			{
				this.tank.Targetes.Remove(component);
			}
		}
		if (other.CompareTag("Tank"))
		{
			T_Tank component2 = other.GetComponent<T_Tank>();
			if (component2 != null && component2.charaType != this.tank.charaType)
			{
				this.tank.Targetes.Remove(component2);
			}
		}
	}
}
