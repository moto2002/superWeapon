using DG.Tweening;
using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Container : MonoBehaviour
{
	private Transform tr;

	public ContainerData c_soldierData = new ContainerData();

	private static CommanderType Create_CommanderType = CommanderType.None;

	private static int Create_CommanderIndex;

	private static int Create_CommanderLevel;

	private static int Create_CommanderStar;

	private static int Create_CommanderSkillLevel;

	private static bool Create_CommanderMasterIsEnemy;

	public static CommanderType commanderType = CommanderType.None;

	private static int TankCardNo;

	[HideInInspector]
	public bool IsSanBing;

	private float timeSecond;

	private Body_Model modelTempArmy;

	private float Y;

	private bool isFirst = true;

	private Tweener YY;

	private float rowSpan = 2f;

	public static int tankTeamNum;

	private int ExtraArmyID_Add;

	public static void CreateCommander(Vector3 pos, long tower_id, long soldier_id, int index, int level, int star, int skill_level, bool MasterIsEnemy = false)
	{
		CommanderType commanderType = CommanderType.Normal;
		if (index != 1)
		{
			if (index == 2)
			{
				commanderType = CommanderType.Tanya;
			}
		}
		else
		{
			commanderType = CommanderType.Boris;
		}
		Container.Create_CommanderType = commanderType;
		Container.Create_CommanderIndex = index;
		Container.Create_CommanderLevel = level;
		Container.Create_CommanderStar = star;
		Container.Create_CommanderSkillLevel = skill_level;
		Container.Create_CommanderMasterIsEnemy = MasterIsEnemy;
		Container.CreateContainer(pos, tower_id, soldier_id, index, level, true, false, commanderType);
	}

	public static Vector3 CheckHitPos(Vector3 pos, int soldier_index)
	{
		Vector3 result = pos;
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				bool flag = true;
				for (int k = 0; k < SenceManager.inst.Tanks_Attack.Count; k++)
				{
					if (UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Attack[k].index].isCanFly == UnitConst.GetInstance().soldierConst[soldier_index].isCanFly)
					{
						if (Vector2.Distance(new Vector2(SenceManager.inst.Tanks_Attack[k].tr.position.x, SenceManager.inst.Tanks_Attack[k].tr.position.z), new Vector2(result.x, result.z)) < 1f)
						{
							flag = false;
							float num = 1.8f + (float)i * 1.5f;
							if (UnitConst.GetInstance().soldierConst[soldier_index].isCanFly)
							{
								num = 5f + (float)i * 1.5f;
							}
							switch (j)
							{
							case 0:
								result = pos + new Vector3(-0.4f * num, 0f, -0.8f * num);
								break;
							case 1:
								result = pos + new Vector3(-num, 0f, 0f);
								break;
							case 2:
								result = pos + new Vector3(-0.4f * num, 0f, 0.8f * num);
								break;
							case 3:
								result = pos + new Vector3(0.7f * num, 0f, 0.5f * num);
								break;
							case 4:
								result = pos + new Vector3(0.7f * num, 0f, -0.5f * num);
								break;
							case 5:
								result = pos + new Vector3(0.4f * num, 0f, 0.8f * num);
								break;
							case 6:
								result = pos + new Vector3(num, 0f, 0f);
								break;
							case 7:
								result = pos + new Vector3(0.4f * num, 0f, -0.8f * num);
								break;
							case 8:
								result = pos + new Vector3(-0.7f * num, 0f, -0.5f * num);
								break;
							case 9:
								result = pos + new Vector3(-0.7f * num, 0f, 0.5f * num);
								break;
							}
							break;
						}
					}
				}
				if (flag)
				{
					return result;
				}
			}
		}
		return result;
	}

	public static Container CreateContainer(Vector3 hit, long towerID, long soliderID, int soldierIdx, int lv, bool isSendEventToServer = true, bool isSanbing = false, CommanderType commanderType1 = CommanderType.None)
	{
		hit += new Vector3(0f, 0.2f, 0f);
		hit = Container.CheckHitPos(hit, soldierIdx);
		if (FightHundler.FightEnd)
		{
			return null;
		}
		GameObject otherModelByName;
		if (!isSanbing)
		{
			otherModelByName = PoolManage.Ins.GetOtherModelByName("Container", null);
			otherModelByName.transform.position = hit + new Vector3(0f, 0f, 0f);
		}
		else
		{
			otherModelByName = PoolManage.Ins.GetOtherModelByName("SanBing", null);
			otherModelByName.transform.position = hit + new Vector3(0f, 15f, 0f);
			AudioManage.inst.PlayAuido("skill2_open", false);
		}
		Container componentInChildren = otherModelByName.GetComponentInChildren<Container>();
		componentInChildren.IsSanBing = isSanbing;
		componentInChildren.c_soldierData.containPos = EventNoteMgr.VectorToMsgPos(hit);
		componentInChildren.c_soldierData.containerId = towerID;
		componentInChildren.c_soldierData.id = soliderID;
		componentInChildren.c_soldierData.index = soldierIdx;
		componentInChildren.c_soldierData.soldierLV = lv;
		if (towerID > 0L)
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("JJDJT", null);
			modelByBundleByName.tr.position = new Vector3(hit.x, 0.3f, hit.z);
			modelByBundleByName.DesInsInPool(5f);
		}
		Container.commanderType = commanderType1;
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck && Container.commanderType != CommanderType.SupplyTank_Star1 && Container.commanderType != CommanderType.SupplyTank_Star2 && Container.commanderType != CommanderType.SupplyTank_Star3)
		{
			if (Container.commanderType != CommanderType.None)
			{
				componentInChildren.c_soldierData.soldierSkillLV = Container.Create_CommanderSkillLevel;
				componentInChildren.c_soldierData.soldieStar = Container.Create_CommanderStar;
				componentInChildren.c_soldierData.soldierType = (int)Container.commanderType;
				EventNoteMgr.NoticeSend(componentInChildren.c_soldierData);
			}
			else
			{
				componentInChildren.c_soldierData.soldierType = -1;
				EventNoteMgr.NoticeSend(componentInChildren.c_soldierData);
			}
		}
		componentInChildren.SendSolider();
		return componentInChildren;
	}

	public static void SetTankCardNo(int cardNo)
	{
		Container.TankCardNo = cardNo;
	}

	private void Awake()
	{
		this.tr = base.transform;
	}

	public void SendSolider()
	{
		this.PerfectData();
		if (this.IsSanBing)
		{
			base.StartCoroutine(this.JLS());
		}
		else
		{
			GameObject gameObject = this.tr.Find("JLS").gameObject;
			gameObject.SetActive(false);
			base.StartCoroutine(this.SendSoldier());
		}
	}

	[DebuggerHidden]
	private IEnumerator JLS()
	{
		Container.<JLS>c__Iterator17 <JLS>c__Iterator = new Container.<JLS>c__Iterator17();
		<JLS>c__Iterator.<>f__this = this;
		return <JLS>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator SendSoldier()
	{
		Container.<SendSoldier>c__Iterator18 <SendSoldier>c__Iterator = new Container.<SendSoldier>c__Iterator18();
		<SendSoldier>c__Iterator.<>f__this = this;
		return <SendSoldier>c__Iterator;
	}

	private void PerfectData()
	{
	}

	private void LandingTank()
	{
		T_TankAbstract tank;
		if (Container.commanderType != CommanderType.None && Container.commanderType != CommanderType.SupplyTank_Star1 && Container.commanderType != CommanderType.SupplyTank_Star2 && Container.commanderType != CommanderType.SupplyTank_Star3)
		{
			tank = PoolManage.Ins.GetTank<T_Commander>(EventNoteMgr.MsgPosToVector(this.c_soldierData.containPos), this.tr.rotation, SenceManager.inst.tankPool);
			tank.roleType = Enum_RoleType.tank;
			tank.charaType = Enum_CharaType.attacker;
			tank.towerID = this.c_soldierData.containerId;
			tank.index = this.c_soldierData.index;
			tank.sceneId = this.c_soldierData.id;
			tank.lv = this.c_soldierData.soldierLV;
			tank.star = this.c_soldierData.soldieStar;
			tank.Card_No = Container.TankCardNo;
			tank.InitInfo();
			(tank as T_Commander).Init(Container.commanderType, true, Container.Create_CommanderIndex, Container.Create_CommanderLevel, Container.Create_CommanderStar, Container.Create_CommanderSkillLevel, Container.Create_CommanderMasterIsEnemy);
		}
		else
		{
			if (!UnitConst.GetInstance().soldierConst[this.c_soldierData.index].isCanFly)
			{
				tank = PoolManage.Ins.GetTank<T_Tank>(EventNoteMgr.MsgPosToVector(this.c_soldierData.containPos), this.tr.rotation, SenceManager.inst.tankPool);
			}
			else
			{
				tank = PoolManage.Ins.GetTank<T_AirShip>(EventNoteMgr.MsgPosToVector(this.c_soldierData.containPos), this.tr.rotation, SenceManager.inst.tankPool);
			}
			tank.roleType = Enum_RoleType.tank;
			tank.charaType = Enum_CharaType.attacker;
			tank.towerID = this.c_soldierData.containerId;
			tank.index = this.c_soldierData.index;
			tank.sceneId = this.c_soldierData.id;
			tank.lv = this.c_soldierData.soldierLV;
			tank.star = this.c_soldierData.soldieStar;
			tank.Card_No = Container.TankCardNo;
			tank.InitInfo();
		}
		SkillManage.inst.skill_tanks.Add(tank);
		int soldierLV = this.c_soldierData.soldierLV;
		if (MovePoint.target && MovePoint.target.modelClore != tank.modelClore)
		{
			tank.ForceAttack(MovePoint.target.tr, MovePoint.target.tr.position);
		}
		if (Container.commanderType != CommanderType.None && Container.commanderType != CommanderType.SupplyTank_Star1 && Container.commanderType != CommanderType.SupplyTank_Star2 && Container.commanderType != CommanderType.SupplyTank_Star3)
		{
			List<T_TankAbstract> list = new List<T_TankAbstract>();
			list.Add(tank);
			for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
			{
				list.Add(SenceManager.inst.Tanks_Attack[i]);
			}
			SenceManager.inst.Tanks_Attack = list;
		}
		else
		{
			SenceManager.inst.Tanks_Attack.Add(tank);
		}
		if (FightPanelManager.inst && FightPanelManager.inst.curSelectUIItem && FightPanelManager.inst.curSelectUIItem.GetComponent<SoldierUIITem>().IsExtraArmyCard)
		{
			FightPanelManager.inst.ExtraArmyID_Add++;
			tank.ExtraArmyId = FightPanelManager.inst.ExtraArmyID_Add;
		}
		if (Container.commanderType == CommanderType.SupplyTank_Star1 || Container.commanderType == CommanderType.SupplyTank_Star2 || Container.commanderType == CommanderType.SupplyTank_Star3)
		{
			int num = 1;
			switch (Container.commanderType)
			{
			case CommanderType.SupplyTank_Star1:
				num = 1;
				break;
			case CommanderType.SupplyTank_Star2:
				num = 2;
				break;
			case CommanderType.SupplyTank_Star3:
				num = 3;
				break;
			}
			int[] buffId = UnitConst.GetInstance().skillList[24 + num].buffId;
			tank.MyBuffRuntime.AddBuffIndex(0, tank, buffId);
		}
	}
}
