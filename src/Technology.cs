using System;
using System.Collections.Generic;
using UnityEngine;

public class Technology
{
	public enum Enum_TechnologyProps
	{
		建筑建造升级速度 = 1,
		仓库存储上限,
		资源保护,
		石油产出上限,
		钢铁产出上限,
		稀矿产出上限,
		石油产量,
		钢铁产量,
		稀矿产量,
		钻石开采,
		防御塔生命值,
		资源建筑生命值,
		围墙生命值,
		坦克生命值,
		飞机生命值,
		特种兵生命值,
		防御塔攻击,
		坦克攻击,
		飞机攻击,
		特种兵攻击,
		地雷攻击,
		高爆雷攻击,
		防御塔防御力,
		坦克防御力,
		飞机防御力,
		特种兵防御力,
		防御塔暴击率,
		坦克暴击率,
		飞机暴击率,
		特种兵暴击率,
		防御塔暴抗,
		坦克暴抗,
		飞机暴抗,
		特种兵暴抗,
		防御塔额外伤害,
		坦克额外伤害,
		飞机额外伤害,
		特种兵额外伤害,
		地雷建造上限,
		高爆雷建造上限,
		兵种支援,
		兵种防御视野,
		兵种购买金币减少,
		坦克移动速度,
		飞机移动速度,
		特种兵移动速度
	}

	public int itemid;

	public int level;

	public bool unlocked;

	public string name;

	public string des;

	public int buildingLevel;

	public Dictionary<Technology.Enum_TechnologyProps, int> Props = new Dictionary<Technology.Enum_TechnologyProps, int>();

	public Dictionary<Technology.Enum_TechnologyProps, bool> Props_Presence = new Dictionary<Technology.Enum_TechnologyProps, bool>();

	public int type;

	public Dictionary<ResType, int> resCost = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemCost = new Dictionary<int, int>();

	public Dictionary<int, int> needTech = new Dictionary<int, int>();

	public List<int> nextTech = new List<int>();

	public int costTime;

	public string iconId;

	public bool isCanUpLevel(ref string msg)
	{
		return true;
	}

	public static int GetTechAddtion(int curValue, Dictionary<int, int> teches, Technology.Enum_TechnologyProps type)
	{
		float num = 0f;
		if (teches == null || teches.Count == 0)
		{
			return 0;
		}
		foreach (KeyValuePair<int, int> current in teches)
		{
			if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props.ContainsKey(type))
			{
				int result;
				if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props_Presence[type])
				{
					num = (float)(curValue * UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type]) * 0.01f;
					result = (int)num;
					return result;
				}
				num = (float)UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type];
				result = (int)num;
				return result;
			}
		}
		return (int)num;
	}

	public static string GetTechAddtion(Dictionary<int, int> teches, Technology.Enum_TechnologyProps type)
	{
		if (teches == null || teches.Count == 0)
		{
			return string.Empty;
		}
		foreach (KeyValuePair<int, int> current in teches)
		{
			if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props.ContainsKey(type))
			{
				if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props_Presence[type])
				{
					string result;
					if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type] > 0)
					{
						result = string.Format("+{0}%", UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type]);
						return result;
					}
					result = string.Format("{0}%", UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type]);
					return result;
				}
				else
				{
					string result;
					if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type] > 0)
					{
						result = string.Format("+{0}", UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type]);
						return result;
					}
					result = string.Format("{0}", UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type]);
					return result;
				}
			}
		}
		return string.Empty;
	}

	public static float GetTechAddtion(float curValue, Dictionary<int, int> teches, Technology.Enum_TechnologyProps type)
	{
		float num = 0f;
		if (teches == null || teches.Count == 0)
		{
			return 0f;
		}
		foreach (KeyValuePair<int, int> current in teches)
		{
			if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props.ContainsKey(type))
			{
				float result;
				if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props_Presence[type])
				{
					num = curValue * (float)UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type] * 0.01f;
					result = num;
					return result;
				}
				num = (float)UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[type];
				result = num;
				return result;
			}
		}
		return num;
	}
}
