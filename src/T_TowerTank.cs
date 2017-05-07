using System;
using System.Collections.Generic;
using UnityEngine;

public class T_TowerTank : MonoBehaviour
{
	public enum TowerTankAttType
	{
		StandBy,
		SortieByDistance,
		SortieByLifeless,
		SortieByCommander,
		Patrol,
		Patrol_Road
	}

	public T_TowerTank.TowerTankAttType ThisTankAttType;

	private T_TankAbstract t_tank;

	private float basic_speed;

	private float Patrol_Dis;

	private bool Patril_Chase;

	private List<Vector3> Patrol_Node = new List<Vector3>();

	public int Patrol_Node_no;

	private Vector3 pos0;

	private T_TankAbstract MB0;

	private bool add = true;

	public void Init(T_TowerTank.TowerTankAttType attType1)
	{
		this.t_tank = base.GetComponent<T_TankAbstract>();
		this.basic_speed = this.t_tank.Movespeed;
		this.ThisTankAttType = attType1;
		if (this.ThisTankAttType == T_TowerTank.TowerTankAttType.Patrol)
		{
			float num = 10f;
			for (int i = 0; i < 4; i++)
			{
				switch (i)
				{
				case 0:
					this.Patrol_Node.Add(new Vector3(Mathf.Min(50f, base.transform.position.x + num), base.transform.position.y, base.transform.position.z));
					break;
				case 1:
					this.Patrol_Node.Add(new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Max(-50f, base.transform.position.z - num)));
					break;
				case 2:
					this.Patrol_Node.Add(new Vector3(Mathf.Max(-50f, base.transform.position.x - num), base.transform.position.y, base.transform.position.z));
					break;
				case 3:
					this.Patrol_Node.Add(new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Min(50f, base.transform.position.z + num)));
					break;
				}
			}
			this.Patrol_Dis = 10f;
		}
		else if (this.ThisTankAttType == T_TowerTank.TowerTankAttType.Patrol_Road)
		{
			this.Patrol_Dis = 10f;
		}
		if (SenceInfo.curMap.ID != HeroInfo.GetInstance().homeMapID)
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("xuanzhong_red", base.transform);
			modelByBundleByName.tr.localScale = Vector3.one * (4.5f + 1.5f * (float)base.GetComponent<T_Tank>().size);
		}
		if (SenceManager.inst.TowerTankMessage_CDTime > 0f)
		{
			return;
		}
		SenceManager.inst.TowerTankMessage_CDTime = 0.5f;
		if (FightPanelManager.inst)
		{
			if (SenceInfo.SpyPlayerInfo != null)
			{
				if (SenceInfo.SpyPlayerInfo.ownerName != null && SenceInfo.SpyPlayerInfo.ownerName != string.Empty)
				{
					if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
					{
						FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("我方援军到达战场！", "Battle"), Color.blue, base.transform);
					}
					else
					{
						FightPanelManager.inst.CreatFightMessage("【" + SenceInfo.SpyPlayerInfo.ownerName + "】" + LanguageManage.GetTextByKey("的援军到达战场！", "Battle"), Color.red, base.transform);
					}
				}
				else
				{
					FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("敌方援军到达战场！", "Battle"), Color.red, base.transform);
				}
			}
			else if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
			{
				FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("我方援军到达战场！", "Battle"), Color.blue, base.transform);
			}
			else
			{
				FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("敌方援军到达战场！", "Battle"), Color.red, base.transform);
			}
		}
	}

	private void Update()
	{
		if (this.ThisTankAttType != T_TowerTank.TowerTankAttType.Patrol_Road)
		{
			return;
		}
		T_TankAbstract forcsTargetTank = this.GetForcsTargetTank(this.ThisTankAttType);
		if (this.ThisTankAttType == T_TowerTank.TowerTankAttType.StandBy)
		{
			this.t_tank.Movespeed = 0f;
			this.t_tank.TankAIPath.speed = this.t_tank.Movespeed;
			if (forcsTargetTank != null && this.MB0 != forcsTargetTank)
			{
				this.MB0 = forcsTargetTank;
				this.t_tank.ForceAttack(forcsTargetTank.tr, forcsTargetTank.tr.position);
			}
		}
		else if (this.ThisTankAttType == T_TowerTank.TowerTankAttType.Patrol)
		{
			if (forcsTargetTank != null && Vector3.Distance(base.transform.position, forcsTargetTank.tr.position) < this.Patrol_Dis)
			{
				this.Patrol_Dis = 100f;
				this.Patril_Chase = true;
				this.MB0 = forcsTargetTank;
				this.t_tank.ForceAttack(forcsTargetTank.tr, forcsTargetTank.tr.position);
				this.pos0 = Vector3.zero;
			}
			else
			{
				this.Patril_Chase = false;
				this.t_tank.SetMove(true);
				this.t_tank.ForceMoving(this.Patrol_Node[this.Patrol_Node_no], this.Patrol_Node[this.Patrol_Node_no], 1f);
				this.pos0 = this.Patrol_Node[this.Patrol_Node_no];
				if (Vector3.Distance(base.transform.position, this.Patrol_Node[this.Patrol_Node_no]) <= 2f)
				{
					this.Patrol_Node_no++;
					if (this.Patrol_Node_no >= 4)
					{
						this.Patrol_Node_no = 0;
					}
				}
			}
		}
		else if (this.ThisTankAttType == T_TowerTank.TowerTankAttType.Patrol_Road)
		{
			if (forcsTargetTank != null && Vector3.Distance(base.transform.position, forcsTargetTank.tr.position) < this.Patrol_Dis)
			{
				this.Patril_Chase = true;
				this.MB0 = forcsTargetTank;
				this.t_tank.ForceAttack(forcsTargetTank.tr, forcsTargetTank.tr.position);
				this.pos0 = Vector3.zero;
			}
			else
			{
				this.Patril_Chase = false;
				this.t_tank.SetMove(true);
				if (this.pos0 != T_CommanderRoad.inst.T_CommanderRoad_tr[this.Patrol_Node_no].transform.position)
				{
					this.t_tank.TankAIPath.target = T_CommanderRoad.inst.T_CommanderRoad_tr[this.Patrol_Node_no].transform;
					this.t_tank.ForceMoving(T_CommanderRoad.inst.T_CommanderRoad_tr[this.Patrol_Node_no].transform.position, T_CommanderRoad.inst.T_CommanderRoad_tr[this.Patrol_Node_no].transform.position, 1f);
					this.pos0 = T_CommanderRoad.inst.T_CommanderRoad_tr[this.Patrol_Node_no].transform.position;
				}
				if (Vector3.Distance(base.transform.position, T_CommanderRoad.inst.T_CommanderRoad_tr[this.Patrol_Node_no].transform.position) <= 2f)
				{
					int num = UnityEngine.Random.Range(0, 10);
					if (num >= 8)
					{
						this.add = !this.add;
					}
					if (this.add)
					{
						this.Patrol_Node_no++;
						if (this.Patrol_Node_no == 11)
						{
							this.Patrol_Node_no = 13;
						}
						if (this.Patrol_Node_no >= 14)
						{
							this.Patrol_Node_no = 0;
						}
					}
					else
					{
						this.Patrol_Node_no--;
						if (this.Patrol_Node_no < 0)
						{
							this.Patrol_Node_no = 13;
						}
						if (this.Patrol_Node_no == 12)
						{
							this.Patrol_Node_no = 10;
						}
					}
				}
			}
		}
		else if (forcsTargetTank != null && this.MB0 != forcsTargetTank)
		{
			this.MB0 = forcsTargetTank;
			this.t_tank.ForceAttack(forcsTargetTank.tr, forcsTargetTank.tr.position);
		}
	}

	private T_TankAbstract GetForcsTargetTank(T_TowerTank.TowerTankAttType attType)
	{
		T_TankAbstract result = null;
		int index = 0;
		float num = 100000f;
		if (SenceManager.inst.Tanks_Attack.Count <= 0)
		{
			return null;
		}
		for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
		{
			if (!(SenceManager.inst.Tanks_Attack[i] == null) && !SenceManager.inst.Tanks_Attack[i].IsDie && !UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Attack[i].index].isCanFly)
			{
				if (SenceManager.inst.Tanks_Attack[i].tankType == T_TankAbstract.TankType.特种兵 && attType == T_TowerTank.TowerTankAttType.SortieByCommander)
				{
					return SenceManager.inst.Tanks_Attack[i];
				}
				if (attType == T_TowerTank.TowerTankAttType.StandBy || attType == T_TowerTank.TowerTankAttType.SortieByDistance || attType == T_TowerTank.TowerTankAttType.Patrol || attType == T_TowerTank.TowerTankAttType.SortieByCommander)
				{
					float num2 = Vector2.Distance(new Vector2(base.transform.position.x, base.transform.position.z), new Vector2(SenceManager.inst.Tanks_Attack[i].tr.position.x, SenceManager.inst.Tanks_Attack[i].tr.position.z));
					if (num2 < num)
					{
						num = num2;
						index = i;
					}
				}
				else if (attType == T_TowerTank.TowerTankAttType.SortieByLifeless)
				{
					float curLife = SenceManager.inst.Tanks_Attack[i].CurLife;
					if (curLife < num)
					{
						num = curLife;
						index = i;
					}
				}
			}
		}
		if ((SenceManager.inst.Tanks_Attack[index] != null || !SenceManager.inst.Tanks_Attack[index].IsDie) && !UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Attack[index].index].isCanFly)
		{
			return SenceManager.inst.Tanks_Attack[index];
		}
		return result;
	}
}
