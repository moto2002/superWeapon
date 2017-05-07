using msg;
using System;
using System.Collections.Generic;

public class ActivityManager
{
	private static ActivityManager ins;

	public static Dictionary<int, int> npcActMap = new Dictionary<int, int>();

	public bool IsFromAct;

	public bool IsDailyAct;

	public bool IsWeekAct;

	public SCActivityData curActData;

	public List<Npc> dailyNpcList = new List<Npc>();

	public T_Island CurActIsland;

	public int selectedLevel;

	public ActivityItem curActItem;

	public Npc curNpc;

	public bool IsFirstOpen = true;

	public bool IsWeekFirstOpen = true;

	public static ActivityManager GetIns()
	{
		if (ActivityManager.ins == null)
		{
			ActivityManager.ins = new ActivityManager();
		}
		return ActivityManager.ins;
	}

	public static void InitNpcActMap()
	{
		ActivityManager.npcActMap.Clear();
		foreach (ActivityItem current in UnitConst.GetInstance().ActivityItemConst.Values)
		{
			if (current.npcId > 0)
			{
				ActivityManager.npcActMap.Add(current.npcId, current.id);
				Npc npc = UnitConst.GetInstance().AllNpc[current.npcId];
				while (npc.nextId > 0)
				{
					npc = UnitConst.GetInstance().AllNpc[npc.nextId];
					ActivityManager.npcActMap.Add(npc.id, current.id);
				}
			}
		}
	}

	public string TimeFormat(string d, string h, string m, string s)
	{
		return string.Format("剩余时间：{0}时{1}分", h, m);
	}

	public void ToActivitySlotInfo()
	{
	}

	public void ToPlayerSlotInfo()
	{
	}

	public static ActivityItem GetActByNpcId(int npcId)
	{
		ActivityItem result = null;
		if (ActivityManager.npcActMap.ContainsKey(npcId))
		{
			int key = ActivityManager.npcActMap[npcId];
			result = UnitConst.GetInstance().ActivityItemConst[key];
		}
		return result;
	}

	public void CheckTimeOut()
	{
		if (this.curActData != null)
		{
			DateTime nowTimeSyncServerToDateTime = TimeTools.GetNowTimeSyncServerToDateTime();
			DateTime dateTime = TimeTools.ConvertLongDateTime(this.curActData.dailyActivityEndTime);
			DateTime dateTime2 = TimeTools.ConvertLongDateTime(this.curActData.weekActivityEndTime);
			if ((dateTime != TimeTools.StampTimeStart && nowTimeSyncServerToDateTime > dateTime) || (dateTime2 != TimeTools.StampTimeStart && nowTimeSyncServerToDateTime > dateTime2))
			{
				ActivityHandler.CSRequestActivity();
			}
		}
	}

	public int GetWeekStage()
	{
		int num = 0;
		if (this.curActData != null)
		{
			if (this.curActData.weekActivityLastNpcId == 0 || (this.IsFirstNpcId(this.curActData.weekActivityLastNpcId) && this.curActData.weekActivityLastNpcId == this.curActData.weekNpcId))
			{
				num = 1;
			}
			else if (this.curActData.weekActivityLastNpcId == this.curActData.weekNpcId)
			{
				num = -1;
			}
			else
			{
				ActivityItem actByNpcId = ActivityManager.GetActByNpcId(this.curActData.weekNpcId);
				if (actByNpcId != null)
				{
					int num2 = 1;
					bool flag = false;
					Npc npc = UnitConst.GetInstance().AllNpc[actByNpcId.npcId];
					while (npc.nextId > 0)
					{
						if (npc.nextId == this.curActData.weekNpcId)
						{
							flag = true;
							break;
						}
						num2++;
						npc = UnitConst.GetInstance().AllNpc[npc.nextId];
					}
					if (flag)
					{
						num = num2 + 1;
					}
				}
			}
		}
		if (num == 0)
		{
			LogManage.Log("阶段错误");
		}
		return num;
	}

	public bool IsFirstNpcId(int npcId)
	{
		bool result = false;
		foreach (KeyValuePair<int, ActivityItem> current in UnitConst.GetInstance().ActivityItemConst)
		{
			if (current.Value.npcId == npcId)
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
