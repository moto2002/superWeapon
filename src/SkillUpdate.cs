using System;
using System.Collections.Generic;

public class SkillUpdate
{
	public int id;

	public int level;

	public string name;

	public string des;

	public Dictionary<int, int> coistItems = new Dictionary<int, int>();

	public Dictionary<ResType, int> resCost = new Dictionary<ResType, int>();

	public int itemId;

	public int commandLevel;

	public int skillVoloum;

	public int needSkillPoint;
}
