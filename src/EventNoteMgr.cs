using msg;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EventNoteMgr
{
	public static global::EventData EvenData;

	public static StringBuilder TankText = new StringBuilder();

	public static List<KVStruct> attackTech = new List<KVStruct>();

	public static List<KVStruct> attackArmy = new List<KVStruct>();

	public static void BegainNewWar()
	{
		if (UIManager.curState != SenceState.WatchVideo)
		{
			EventNoteMgr.EvenData = new global::EventData();
			EventNoteMgr.EvenData.eventType = 0;
			EventNoteMgr.EvenData.eventId = (long)SenceInfo.curMap.mapIndex;
			EventNoteMgr.EvenData.randomSeed = GameSetting.RandomSeed;
			SenceManager.inst.SoldierId = 1L;
		}
	}

	public static void SaveSupperSkill(int skillID, Vector3 pos)
	{
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck)
		{
			OperationEventData operationEventData = new OperationEventData();
			operationEventData.time = (int)(TimePanel.inst.timer * 10f);
			operationEventData.OperationType = 3;
			operationEventData.superSkill = new SupperSkillEvent();
			operationEventData.superSkill.skillId = skillID;
			operationEventData.superSkill.pos = EventNoteMgr.VectorToMsgPos(pos);
			EventNoteMgr.SynchronizationTanksPos(operationEventData.superSkill.tanksPos);
			if (EventNoteMgr.EvenData != null)
			{
				EventNoteMgr.EvenData.operData.Add(operationEventData);
			}
		}
	}

	public static void NoticeSend(ContainerData c_soldierData)
	{
		if (UIManager.curState != SenceState.WatchVideo)
		{
			OperationEventData operationEventData = new OperationEventData();
			if (TimePanel.inst)
			{
				operationEventData.time = (int)(TimePanel.inst.timer * 10f);
			}
			operationEventData.OperationType = 1;
			operationEventData.send = c_soldierData;
			if (EventNoteMgr.EvenData != null)
			{
				EventNoteMgr.EvenData.operData.Add(operationEventData);
			}
		}
	}

	public static void NoticeOpenBox(int boxIndex)
	{
		if (UIManager.curState != SenceState.WatchVideo)
		{
		}
	}

	public static void NoticeOpenAutoFight(bool isOpenAuto)
	{
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck)
		{
			OperationEventData operationEventData = new OperationEventData();
			operationEventData.time = (int)(TimePanel.inst.timer * 10f);
			operationEventData.OperationType = 6;
			operationEventData.IsAutoFight = isOpenAuto;
			EventNoteMgr.EvenData.operData.Add(operationEventData);
		}
	}

	public static void NoticeFoceMove(Vector3 pos)
	{
		if (UIManager.curState != SenceState.WatchVideo)
		{
			if (EventNoteMgr.EvenData == null)
			{
				return;
			}
			OperationEventData operationEventData = new OperationEventData();
			operationEventData.time = (int)(TimePanel.inst.timer * 10f);
			operationEventData.OperationType = 2;
			operationEventData.move = new OperationMove();
			operationEventData.move.pos = EventNoteMgr.VectorToMsgPos(pos);
			EventNoteMgr.SynchronizationTanksPos(operationEventData.move.tanksPos);
			EventNoteMgr.EvenData.operData.Add(operationEventData);
		}
	}

	private static void SynchronizationTanksPos(List<OperationTanksPos> data)
	{
	}

	public static void NoteTankMove(long tankID, Vector3 curPos, Vector3 moveTarPos, bool isMove)
	{
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck)
		{
			if (EventNoteMgr.EvenData == null)
			{
				return;
			}
			OperationTankPos operationTankPos = new OperationTankPos();
			EventNoteMgr.EvenData.tankPoses.Add(operationTankPos);
			operationTankPos.tankID = tankID;
			operationTankPos.curPos = EventNoteMgr.VectorToMsgPos(curPos);
			operationTankPos.moveTargetPos = EventNoteMgr.VectorToMsgPos(moveTarPos);
			operationTankPos.isMove = isMove;
			operationTankPos.time = (int)(TimePanel.inst.timer * 10f);
		}
	}

	public static void NoteFileTankMove(long tankID, Vector3 curPos)
	{
	}

	public static void GetNoteTankPos(T_Tank tank)
	{
	}

	public static void NoticeFoceAttack(long id)
	{
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck)
		{
			if (EventNoteMgr.EvenData == null)
			{
				return;
			}
			OperationEventData operationEventData = new OperationEventData();
			if (TimePanel.inst)
			{
				operationEventData.time = (int)(TimePanel.inst.timer * 10f);
			}
			operationEventData.OperationType = 5;
			operationEventData.foceAtt = new OperationFoceAttack();
			operationEventData.foceAtt.id = id;
			EventNoteMgr.SynchronizationTanksPos(operationEventData.foceAtt.tanksPos);
			EventNoteMgr.EvenData.operData.Add(operationEventData);
		}
	}

	public static void NoticeDie(int _type, long _idx)
	{
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck)
		{
			OperationEventData operationEventData = new OperationEventData();
			if (TimePanel.inst)
			{
				operationEventData.time = (int)(TimePanel.inst.timer * 10f);
			}
			operationEventData.OperationType = 4;
			operationEventData.die = new DieEvent();
			operationEventData.die.type = _type;
			operationEventData.die.id = _idx;
			EventNoteMgr.SynchronizationTanksPos(operationEventData.die.tanksPos);
			if (EventNoteMgr.EvenData != null)
			{
				EventNoteMgr.EvenData.operData.Add(operationEventData);
			}
		}
	}

	public static void NoticeSelControlArmyID(int type, int id)
	{
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck)
		{
			OperationEventData operationEventData = new OperationEventData();
			if (TimePanel.inst)
			{
				operationEventData.time = (int)(TimePanel.inst.timer * 10f);
			}
			operationEventData.OperationType = 8;
			if (operationEventData.armyControl == null)
			{
				return;
			}
			operationEventData.armyControl.index = id;
			operationEventData.armyControl.type = type;
			if (EventNoteMgr.EvenData != null)
			{
				EventNoteMgr.EvenData.operData.Add(operationEventData);
			}
		}
	}

	public static void EndThisWar()
	{
		if (UIManager.curState != SenceState.WatchVideo && NewbieGuidePanel.isEnemyAttck && TimePanel.inst)
		{
			EventNoteMgr.EvenData.endTime = TimePanel.inst.timer;
		}
	}

	public static BaseFightInfo GetTankFightData(int index, int tankLv, int techLV)
	{
		return default(BaseFightInfo);
	}

	public static BaseFightInfo GetTowerFightData(int index, int lv, int techLV)
	{
		return default(BaseFightInfo);
	}

	private static string VectorToStr(Vector3[] pos)
	{
		string text = string.Empty;
		string format = "{0},{1}";
		for (int i = 0; i < pos.Length; i++)
		{
			if (i != 0)
			{
				text += "|";
			}
			text += string.Format(format, pos[i].x, pos[i].z);
		}
		return text;
	}

	public static Vector3[] StrToVector(string str)
	{
		string[] array = str.Split(new char[]
		{
			'|'
		});
		Vector3[] array2 = new Vector3[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array3 = array[i].Split(new char[]
			{
				','
			});
			array2[i] = new Vector3(float.Parse(array3[0]), 0f, float.Parse(array3[1]));
		}
		return array2;
	}

	public static string VectorToStr(Vector3 pos)
	{
		string format = "{0},{1}";
		return string.Format(format, pos.x, pos.z);
	}

	public static Position VectorToMsgPos(Vector3 pos)
	{
		return new Position
		{
			x = (int)(pos.x * 100f),
			y = (int)(pos.z * 100f)
		};
	}

	public static Vector3 MsgPosToVector(Position pos)
	{
		Vector3 result = new Vector3((float)pos.x * 0.01f, 0f, (float)pos.y * 0.01f);
		return result;
	}

	public static Vector3 StrToOneVector(string str)
	{
		string[] array = str.Split(new char[]
		{
			','
		});
		Vector3 result = new Vector3(float.Parse(array[0]), 0f, float.Parse(array[1]));
		return result;
	}
}
