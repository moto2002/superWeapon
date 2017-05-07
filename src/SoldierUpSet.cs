using System;
using System.Collections.Generic;

public class SoldierUpSet
{
	public int id;

	public int itemId;

	public string name;

	public int level;

	public int armyId;

	public string des;

	public Dictionary<int, int> starUpItem = new Dictionary<int, int>();

	public Dictionary<int, int> starUpRes = new Dictionary<int, int>();

	private int funcMoney;

	public Dictionary<SpecialPro, int> armyAffixes = new Dictionary<SpecialPro, int>();

	public int FuncMoney
	{
		get
		{
			int num = this.funcMoney;
			return num + Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.兵种购买金币减少);
		}
		set
		{
			this.funcMoney = value;
		}
	}
}
