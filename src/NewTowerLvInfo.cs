using System;
using System.Collections.Generic;

public class NewTowerLvInfo
{
	public int lv;

	public Dictionary<ResType, int> resCost = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemCost = new Dictionary<int, int>();

	public int outputNum;

	public Dictionary<ResType, int> outputLimit = new Dictionary<ResType, int>();

	private int buildTime;

	public int playerExp;

	public int needCommandLevel;

	public int reduceCommondLifePer;

	public BaseFightInfo fightInfo;

	public int electricUse;

	public string skillIds;

	public string skillId;

	public int energypoints;

	public int haveenergypoints;

	public string bodyID;

	public int armyExp;

	public int BuildTime
	{
		get
		{
			int num = this.buildTime;
			return num + Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.建筑建造升级速度);
		}
		set
		{
			this.buildTime = value;
		}
	}

	public int BasicBuildTime
	{
		get
		{
			return this.buildTime;
		}
		set
		{
			this.buildTime = value;
		}
	}
}
