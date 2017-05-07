using msg;
using System;
using System.Collections.Generic;

public class DailyTask
{
	public enum taskSkilType
	{
		建造列表 = 1,
		兵种升级,
		作战图,
		科技升级,
		建筑升级,
		兵种配置 = 7,
		特种兵升级 = 9,
		特种兵升星,
		技能制卡,
		副本,
		军团副本,
		兵种升级_战车工厂,
		兵种升级_飞机场,
		兵种升级_兵营
	}

	public int id;

	public int PanelType;

	public int PanelId;

	public string name;

	public string description;

	public int conditionId;

	public bool isTips = true;

	public int secondConditionId;

	public int step;

	public bool isUIShow;

	private int stepRecord;

	public string NewBieGroup;

	public int type;

	public int mainTaskType;

	public int rewardNum;

	public Dictionary<int, int> rewardItems = new Dictionary<int, int>();

	public Dictionary<int, int> rewardEquips = new Dictionary<int, int>();

	public Dictionary<ResType, int> rewardRes = new Dictionary<ResType, int>();

	public Dictionary<int, int> skillAward = new Dictionary<int, int>();

	public DailyTask.taskSkilType skipType;

	public int skipValue;

	public bool isReceived;

	public int StepRecord
	{
		get
		{
			if (this.conditionId == 33)
			{
				if (HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(this.secondConditionId))
				{
					return HeroInfo.GetInstance().PlayerBuildingLevel[this.secondConditionId];
				}
				return 0;
			}
			else
			{
				if (this.conditionId == 11)
				{
					int num = 0;
					num += HeroInfo.GetInstance().islandResData.OilIslandes.Count;
					num += HeroInfo.GetInstance().islandResData.SteelIslandes.Count;
					return num + HeroInfo.GetInstance().islandResData.RareEarthIslandes.Count;
				}
				if (this.conditionId == 26)
				{
					return SenceManager.inst.ElliteBallteBoxes.Count;
				}
				if (this.conditionId == 35)
				{
					int num2 = 0;
					int count = SenceManager.inst.towers.Count;
					for (int i = 0; i < count; i++)
					{
						if (SenceManager.inst.towers[i] && SenceManager.inst.towers[i].index == this.secondConditionId)
						{
							if (!HeroInfo.GetInstance().BuildCD.Contains(SenceManager.inst.towers[i].id) || SenceManager.inst.towers[i].trueLv != 0)
							{
								num2++;
							}
						}
					}
					return num2;
				}
				if (this.conditionId == 36)
				{
					if (HeroInfo.GetInstance().PlayerArmyData.ContainsKey(this.secondConditionId))
					{
						return HeroInfo.GetInstance().PlayerArmyData[this.secondConditionId].level;
					}
					return 0;
				}
				else if (this.conditionId == 37)
				{
					if (HeroInfo.GetInstance().PlayerTechnologyInfo.ContainsKey(this.secondConditionId))
					{
						return HeroInfo.GetInstance().PlayerTechnologyInfo[this.secondConditionId];
					}
					return 0;
				}
				else
				{
					if (this.conditionId == 13)
					{
						return HeroInfo.GetInstance().playerRes.playermedal;
					}
					if (this.conditionId == 51)
					{
						if (UnitConst.GetInstance().BattleFieldConst.ContainsKey(this.secondConditionId) && UnitConst.GetInstance().BattleFieldConst[this.secondConditionId].fightRecord.isFight)
						{
							return 1;
						}
						return 0;
					}
					else if (this.conditionId == 54)
					{
						if (HeroInfo.GetInstance().PlayerCommandoes.ContainsKey(this.secondConditionId))
						{
							return HeroInfo.GetInstance().PlayerCommandoes[this.secondConditionId].star;
						}
						return 0;
					}
					else if (this.conditionId == 27)
					{
						if (HeroInfo.GetInstance().PlayerCommandoes.ContainsKey(this.secondConditionId))
						{
							return HeroInfo.GetInstance().PlayerCommandoes[this.secondConditionId].level;
						}
						return 0;
					}
					else
					{
						if (this.conditionId == 40)
						{
							int num3 = 0;
							foreach (armyInfoInBuilding current in HeroInfo.GetInstance().AllArmyInfo.Values)
							{
								foreach (KVStruct current2 in current.armyFunced)
								{
									if ((long)this.secondConditionId == current2.key)
									{
										num3 += (int)current2.value;
									}
								}
							}
							return num3;
						}
						if (this.conditionId != 53)
						{
							return this.stepRecord;
						}
						if (HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.soldierItemId == this.secondConditionId && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime == 0L)
						{
							return 1;
						}
						return 0;
					}
				}
			}
		}
		set
		{
			this.stepRecord = value;
		}
	}

	public bool isCanRecieved
	{
		get
		{
			return !this.isReceived && this.StepRecord >= this.step;
		}
	}

	public string GetMainTaskTittle()
	{
		switch (this.mainTaskType)
		{
		case 1:
			return "基地";
		case 2:
			return "飞艇";
		case 3:
			return "地球";
		case 4:
			return "新手引导";
		default:
			return string.Empty;
		}
	}
}
