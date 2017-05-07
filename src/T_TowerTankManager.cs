using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class T_TowerTankManager : MonoBehaviour
{
	public static T_TowerTankManager inst;

	private float allBuildLife;

	private float allBuildLifeMax;

	public List<T_Tower> OrderTower = new List<T_Tower>();

	private bool set_team;

	private float createtank_time;

	public void OnDestroy()
	{
		T_TowerTankManager.inst = null;
	}

	private void Awake()
	{
		T_TowerTankManager.inst = this;
		this.set_team = false;
	}

	private void Update()
	{
		if (UIManager.curState == SenceState.Attacking)
		{
			if (!this.set_team)
			{
				this.set_team = true;
			}
		}
		else
		{
			this.set_team = false;
		}
	}

	public void SetDefTeam()
	{
		base.StartCoroutine(this.CreateTowerTank(T_CommanderRoad.inst.T_CommanderRoad_tr[0].transform.position, 3, 2, 0f, T_TowerTank.TowerTankAttType.Patrol_Road, 0, 0));
		base.StartCoroutine(this.CreateTowerTank(T_CommanderRoad.inst.T_CommanderRoad_tr[3].transform.position, 3, 2, 0f, T_TowerTank.TowerTankAttType.Patrol_Road, 3, 0));
		base.StartCoroutine(this.CreateTowerTank(T_CommanderRoad.inst.T_CommanderRoad_tr[6].transform.position, 3, 2, 0f, T_TowerTank.TowerTankAttType.Patrol_Road, 6, 0));
		base.StartCoroutine(this.CreateTowerTank(T_CommanderRoad.inst.T_CommanderRoad_tr[9].transform.position, 3, 2, 0f, T_TowerTank.TowerTankAttType.Patrol_Road, 9, 0));
	}

	public bool T_TowerDoHurt_Feedback(T_Tower t_tower, float life_scale, int damage)
	{
		if (t_tower.secType == 6 || t_tower.secType == 20)
		{
			return false;
		}
		bool result = true;
		if (this.allBuildLifeMax == 0f)
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (SenceManager.inst.towers[i].secType != 6 && SenceManager.inst.towers[i].secType != 20)
				{
					this.allBuildLifeMax += SenceManager.inst.towers[i].CurLife;
					this.allBuildLife = this.allBuildLifeMax;
				}
			}
			foreach (KeyValuePair<int, TowerTankOrder> current in UnitConst.GetInstance().TowerTankOrderList)
			{
				if (current.Value.damagetype == DamageType.Null)
				{
					current.Value.use = true;
				}
				else if (current.Value.damagetype == DamageType.NormalBuild)
				{
					current.Value.use = true;
				}
				else
				{
					current.Value.use = false;
					for (int j = 0; j < SenceManager.inst.towers.Count; j++)
					{
						if (!current.Value.use)
						{
							if (SenceManager.inst.towers[j].index == current.Value.buildindex && SenceManager.inst.towers[j].lv == current.Value.buildlevel)
							{
								for (int k = 0; k < this.OrderTower.Count; k++)
								{
									if (this.OrderTower[k] == SenceManager.inst.towers[j])
									{
									}
								}
								current.Value.use = true;
								this.OrderTower.Add(SenceManager.inst.towers[j]);
							}
							else
							{
								current.Value.use = false;
							}
						}
						else if (SenceManager.inst.towers[j].index == current.Value.buildindex && SenceManager.inst.towers[j].lv == current.Value.buildlevel)
						{
							for (int l = 0; l < this.OrderTower.Count; l++)
							{
								if (this.OrderTower[l] == SenceManager.inst.towers[j])
								{
								}
							}
							this.OrderTower.Add(SenceManager.inst.towers[j]);
						}
					}
				}
			}
		}
		this.allBuildLife -= (float)damage;
		foreach (KeyValuePair<int, TowerTankOrder> current2 in UnitConst.GetInstance().TowerTankOrderList)
		{
			if (current2.Value.use)
			{
				switch (current2.Value.damagetype)
				{
				case DamageType.AllBuild:
					if (this.allBuildLife / this.allBuildLifeMax * 100f <= current2.Value.life_crisis && current2.Value.use)
					{
						this.TowerTankOrder_Activate(current2.Value, t_tower);
						current2.Value.use = false;
					}
					break;
				case DamageType.CommandCenter:
					if (SenceManager.inst.MainBuilding.CurLife / (float)SenceManager.inst.MainBuilding.MaxLife * 100f <= current2.Value.life_crisis && current2.Value.use)
					{
						this.TowerTankOrder_Activate(current2.Value, t_tower);
						current2.Value.use = false;
					}
					break;
				case DamageType.NormalBuild:
					if (t_tower.index == current2.Value.buildindex && life_scale <= current2.Value.life_crisis && t_tower.lv == current2.Value.buildlevel && current2.Value.use)
					{
						this.TowerTankOrder_Activate(current2.Value, t_tower);
						result = false;
					}
					break;
				}
			}
		}
		return result;
	}

	private void TowerTankOrder_Activate(TowerTankOrder order, T_Tower t_tower)
	{
		if (order.damagetype == DamageType.NormalBuild)
		{
			for (int i = 0; i < order.tank_num; i++)
			{
				Vector3 vector = t_tower.tr.position;
				float d = 1.2f;
				switch (i)
				{
				case 0:
					vector += new Vector3(1f, 0f, 1f) * d;
					break;
				case 1:
					vector += new Vector3(1f, 0f, 0f) * d;
					break;
				case 2:
					vector += new Vector3(0f, 0f, 1f) * d;
					break;
				case 3:
					vector += new Vector3(1f, 0f, -1f) * d;
					break;
				case 4:
					vector += new Vector3(-1f, 0f, 1f) * d;
					break;
				}
				base.StartCoroutine(this.CreateTowerTank(vector, order.tank_index, 1, this.createtank_time, T_TowerTank.TowerTankAttType.SortieByDistance, 0, 0));
				this.createtank_time += 0.5f;
			}
		}
		else
		{
			for (int j = 0; j < this.OrderTower.Count; j++)
			{
				if (this.OrderTower[j] != null)
				{
					if (this.OrderTower[j].index == order.buildindex && this.OrderTower[j].lv == order.buildlevel)
					{
						for (int k = 0; k < order.tank_num; k++)
						{
							Vector3 vector2 = this.OrderTower[j].tr.position;
							float d2 = 1.2f;
							switch (k)
							{
							case 0:
								vector2 += new Vector3(1f, 0f, 1f) * d2;
								break;
							case 1:
								vector2 += new Vector3(1f, 0f, 0f) * d2;
								break;
							case 2:
								vector2 += new Vector3(0f, 0f, 1f) * d2;
								break;
							case 3:
								vector2 += new Vector3(1f, 0f, -1f) * d2;
								break;
							case 4:
								vector2 += new Vector3(-1f, 0f, 1f) * d2;
								break;
							}
							base.StartCoroutine(this.CreateTowerTank(vector2, order.tank_index, 1, this.createtank_time, T_TowerTank.TowerTankAttType.SortieByDistance, 0, 0));
							this.createtank_time += 0.5f;
						}
					}
				}
			}
		}
	}

	[DebuggerHidden]
	public IEnumerator CreateTowerTank(Vector3 pos, int index, int level, float time = 0f, T_TowerTank.TowerTankAttType attType = T_TowerTank.TowerTankAttType.SortieByDistance, int patrol = 0, int extraArmyID = 0)
	{
		T_TowerTankManager.<CreateTowerTank>c__Iterator3C <CreateTowerTank>c__Iterator3C = new T_TowerTankManager.<CreateTowerTank>c__Iterator3C();
		<CreateTowerTank>c__Iterator3C.time = time;
		<CreateTowerTank>c__Iterator3C.index = index;
		<CreateTowerTank>c__Iterator3C.pos = pos;
		<CreateTowerTank>c__Iterator3C.extraArmyID = extraArmyID;
		<CreateTowerTank>c__Iterator3C.level = level;
		<CreateTowerTank>c__Iterator3C.attType = attType;
		<CreateTowerTank>c__Iterator3C.patrol = patrol;
		<CreateTowerTank>c__Iterator3C.<$>time = time;
		<CreateTowerTank>c__Iterator3C.<$>index = index;
		<CreateTowerTank>c__Iterator3C.<$>pos = pos;
		<CreateTowerTank>c__Iterator3C.<$>extraArmyID = extraArmyID;
		<CreateTowerTank>c__Iterator3C.<$>level = level;
		<CreateTowerTank>c__Iterator3C.<$>attType = attType;
		<CreateTowerTank>c__Iterator3C.<$>patrol = patrol;
		<CreateTowerTank>c__Iterator3C.<>f__this = this;
		return <CreateTowerTank>c__Iterator3C;
	}

	public GameObject CreateTowerTank_NoIE(Vector3 pos, int index, int level, float time = 0f, T_TowerTank.TowerTankAttType attType = T_TowerTank.TowerTankAttType.StandBy, int patrol = 0)
	{
		this.createtank_time -= 0.5f;
		T_TankAbstract tank;
		if (UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			tank = PoolManage.Ins.GetTank<T_AirShip>(pos, Quaternion.identity, SenceManager.inst.tankPool);
		}
		else
		{
			tank = PoolManage.Ins.GetTank<T_Tank>(pos, Quaternion.identity, SenceManager.inst.tankPool);
		}
		tank.isDowning = false;
		tank.roleType = Enum_RoleType.tank;
		tank.charaType = Enum_CharaType.defender;
		tank.index = index;
		tank.sceneId = SenceManager.inst.SoldierId;
		tank.lv = level;
		tank.TargetP = pos;
		PoolManage.Ins.CreatEffect("chubing", tank.tr.position, Quaternion.identity, null);
		if (tank.charaType == Enum_CharaType.defender)
		{
			tank.InitInfo();
			SenceManager.inst.Tanks_Defend.Add(tank);
		}
		else
		{
			tank.InitInfo();
			SenceManager.inst.Tanks_Attack.Add(tank);
		}
		tank.ga.AddComponent<T_TowerTank>();
		tank.ga.GetComponent<T_TowerTank>().Init(attType);
		tank.ga.GetComponent<T_TowerTank>().Patrol_Node_no = patrol;
		return tank.ga;
	}
}
