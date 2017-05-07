using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FightHundler
{
	public static List<SettleUniteData> DeadSettleUnites = new List<SettleUniteData>();

	public static List<VFDeadData> VFData = new List<VFDeadData>();

	public static bool FightEnd = false;

	public static bool isSendFightEnd = false;

	public static event Action OnFighting
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			FightHundler.OnFighting = (Action)Delegate.Combine(FightHundler.OnFighting, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			FightHundler.OnFighting = (Action)Delegate.Remove(FightHundler.OnFighting, value);
		}
	}

	public static void CG_StartFight(long idx, int from, int money, long reportId, int npcId, bool isRefresh = true)
	{
		CSBattleStart cSBattleStart = new CSBattleStart();
		cSBattleStart.id = idx;
		cSBattleStart.from = from;
		cSBattleStart.money = money;
		cSBattleStart.reportId = reportId;
		cSBattleStart.npcId = npcId;
		Loading.IsRefreshSence = isRefresh;
		LogManage.LogError("告诉韩荣  攻击 了· ~ ~ ~ · ");
		ClientMgr.GetNet().SendHttp(5000, cSBattleStart, new DataHandler.OpcodeHandler(FightHundler.GC_StartFight), null);
	}

	public static void GC_StartFight(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			List<SCBattleStart> list = opcode.Get<SCBattleStart>(10067);
			if (list.Count > 0 && list[0].start)
			{
				FightHundler.FightEnd = false;
				FightHundler.DeadSettleUnites.Clear();
				FightHundler.VFData.Clear();
				UIManager.curState = SenceState.Attacking;
				EventNoteMgr.TankText.Remove(0, EventNoteMgr.TankText.Length);
				if (Loading.IsRefreshSence)
				{
					PlayerHandle.EnterSence("island");
				}
				else if (FightHundler.OnFighting != null)
				{
					FightHundler.OnFighting();
				}
			}
		}
	}

	public static void CG_FinishFight()
	{
		FightHundler.FightEnd = true;
		if (FightHundler.isSendFightEnd)
		{
			return;
		}
		FightHundler.isSendFightEnd = true;
		FightPanelManager.IsRetreat = false;
		EventNoteMgr.EndThisWar();
		FightHundler.CG_UniteDead();
		FightHundler.CG_FinishBattle();
	}

	private static void CG_FinishBattle()
	{
		if (NewbieGuidePanel.isEnemyAttck)
		{
			CSBattleEnd cSBattleEnd = new CSBattleEnd();
			cSBattleEnd.win = (SenceManager.inst.settType == SettlementType.success);
			cSBattleEnd.star = 0;
			if (WarStarManager._inst)
			{
				cSBattleEnd.star = WarStarManager._inst.WarStar;
			}
			cSBattleEnd.video = FightHundler.GetEventData();
			if (cSBattleEnd.video == null)
			{
				cSBattleEnd.video = new byte[1];
			}
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"发战斗结束的消息了！！！-----------------------战斗结果： ",
				cSBattleEnd.win,
				"战斗星级：",
				cSBattleEnd.star,
				" video Bytes Length:",
				cSBattleEnd.video.Length
			}));
			ClientMgr.GetNet().SendHttp(5002, cSBattleEnd, new DataHandler.OpcodeHandler(FightHundler.GC_FinishFight), null);
		}
	}

	public static byte[] GetEventData()
	{
		if (EventNoteMgr.EvenData == null)
		{
			return null;
		}
		msg.EventData eventData = new msg.EventData();
		eventData.eventType = EventNoteMgr.EvenData.eventType;
		eventData.eventId = EventNoteMgr.EvenData.eventId;
		eventData.endTime = (long)EventNoteMgr.EvenData.endTime;
		eventData.tankPoses.AddRange(EventNoteMgr.EvenData.tankPoses);
		eventData.operData.AddRange(EventNoteMgr.EvenData.operData);
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			Opcode.Serialize(memoryStream, eventData);
			byte[] array = memoryStream.ToArray();
			result = array;
		}
		return result;
	}

	public static void GC_FinishFight(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			List<SCBattleEnd> list = opcode.Get<SCBattleEnd>(10015);
			if (SenceManager.inst.fightType == FightingType.Attack)
			{
				if (list.Count > 0 && list[0].win && list[0].battlefieldStar != null)
				{
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						"服务器通知星数：",
						list[0].battlefieldStar.value,
						" 对应Key值：",
						list[0].battlefieldStar.key
					}));
					if (list[0].battlefieldStar.key > 0L && UnitConst.GetInstance().BattleFieldConst.ContainsKey((int)list[0].battlefieldStar.key))
					{
						if ((int)list[0].battlefieldStar.value > UnitConst.GetInstance().BattleFieldConst[(int)list[0].battlefieldStar.key].fightRecord.star)
						{
							UnitConst.GetInstance().BattleFieldConst[(int)list[0].battlefieldStar.key].fightRecord.star = (int)list[0].battlefieldStar.value;
						}
						if ((int)list[0].battlefieldStar.value > 0)
						{
							UnitConst.GetInstance().BattleFieldConst[(int)list[0].battlefieldStar.key].fightRecord.isFight = true;
						}
					}
				}
				if (list != null && list.Count > 0)
				{
					SettlementManager.GetRewardInfo(list[0].addRes);
					SettlementManager.GetItemReward(list[0].addItems);
					SettlementManager.GetEquipInfo(list[0].addEquips);
					SettlementManager.getExp = list[0].soldierExp;
					if (list[0].battlefieldStar != null && WarStarManager._inst)
					{
						SettlementManager.Star = WarStarManager._inst.WarStar;
					}
				}
				FuncUIManager.inst.OpenFuncUI("SettlementPanel", SenceType.Island);
			}
			if (SenceManager.inst.fightType == FightingType.Guard && list.Count > 0)
			{
				HeroInfo.GetInstance().RemoveEnemyNpcAttack((int)list[0].npcAttackId);
				EnemyAttackManage.inst.enemyScore.item.AddRange(list[0].addItems);
				EnemyAttackManage.inst.enemyScore.res.AddRange(list[0].addRes);
				EnemyAttackManage.inst.enemyScore.equips.AddRange(list[0].addEquips);
				UIManager.inst.enemyScore.gameObject.SetActive(true);
			}
		}
		else
		{
			FuncUIManager.inst.OpenFuncUI("SettlementPanel", SenceType.Island);
		}
	}

	[DebuggerHidden]
	public static IEnumerator OpenSettlementPanle()
	{
		return new FightHundler.<OpenSettlementPanle>c__Iterator4D();
	}

	public static void AddDeadSettleUnites(SettleUniteData data)
	{
		if (NewbieGuidePanel.isEnemyAttck && !FightHundler.isSendFightEnd)
		{
			FightHundler.DeadSettleUnites.Add(data);
		}
	}

	public static void AddVFData(VFDeadData data)
	{
		if (NewbieGuidePanel.isEnemyAttck && !FightHundler.isSendFightEnd)
		{
			FightHundler.VFData.Add(data);
		}
	}

	public static void CG_UniteDead()
	{
		if (FightHundler.DeadSettleUnites.Count == 0 && FightHundler.VFData.Count == 0)
		{
			return;
		}
		CSFightingEvent cSFightingEvent = new CSFightingEvent();
		for (int i = 0; i < FightHundler.DeadSettleUnites.Count; i++)
		{
			SettleUniteData settleUniteData = FightHundler.DeadSettleUnites[i];
			FightEventData fightEventData = new FightEventData();
			fightEventData.deadId = settleUniteData.deadSenceId;
			fightEventData.deadIdx = settleUniteData.deadIdx;
			fightEventData.deadContid = settleUniteData.deadBuildingID;
			fightEventData.deadType = settleUniteData.deadType;
			fightEventData.poxX = settleUniteData.posX;
			fightEventData.poxZ = settleUniteData.posZ;
			fightEventData.randomSeed = GameSetting.RandomSeed;
			fightEventData.buyArmyMoney = settleUniteData.buyArmyMoney;
			fightEventData.num = settleUniteData.num;
			if (fightEventData.deadType == 1)
			{
				LogManage.LogError(string.Format("死亡兵上传 了", new object[0]));
			}
			if (fightEventData.deadType == 2)
			{
				LogManage.LogError(string.Format("死亡建筑上传 了", new object[0]));
			}
			if (fightEventData.deadType == 3)
			{
				LogManage.LogError(string.Format("派兵 上传 了", new object[0]));
			}
			if (fightEventData.deadType == 8)
			{
				LogManage.LogError(string.Format("特种兵派兵 上传 了 ID{0} Index{1} BuildingID{2}", fightEventData.deadId, fightEventData.deadIdx, fightEventData.deadContid));
			}
			if (fightEventData.deadType == 7)
			{
				LogManage.LogError(string.Format("特种兵死亡 上传 了 ID{0} Index{1} BuildingID{2}", fightEventData.deadId, fightEventData.deadIdx, fightEventData.deadContid));
			}
			bool flag = false;
			if (fightEventData.deadType == 6)
			{
				for (int j = 0; j < cSFightingEvent.data.Count; j++)
				{
					if (cSFightingEvent.data[j].deadType == 6 && cSFightingEvent.data[j].deadId == fightEventData.deadId)
					{
						flag = true;
						cSFightingEvent.data[j].num = Mathf.Min(cSFightingEvent.data[j].num, fightEventData.num);
					}
				}
			}
			if (!flag)
			{
				cSFightingEvent.data.Add(fightEventData);
				if (fightEventData.deadType == 6)
				{
					LogManage.LogError(string.Format("建筑血量 上传 了 ID{0} Index{1} PosIndex{2}", fightEventData.deadId, fightEventData.deadIdx, fightEventData.deadContid));
				}
			}
		}
		FightHundler.DeadSettleUnites.Clear();
		ClientMgr.GetNet().SendHttp(5004, cSFightingEvent, null, null);
	}
}
