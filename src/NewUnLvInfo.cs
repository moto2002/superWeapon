using System;
using System.Collections.Generic;

public class NewUnLvInfo
{
	public int lv;

	public Dictionary<ResType, int> resCost = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemCost = new Dictionary<int, int>();

	public int playerExp;

	private int buyCost;

	public int cdTime;

	public BaseFightInfo fightInfo;

	public string skillIds;

	public string skillId;

	public int BuyCost
	{
		get
		{
			int num = this.buyCost;
			return num + Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.兵种购买金币减少);
		}
		set
		{
			this.buyCost = value;
		}
	}
}
