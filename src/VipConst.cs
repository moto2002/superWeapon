using System;
using System.Collections.Generic;

public class VipConst
{
	public enum Enum_VipRight
	{
		无,
		石油产量,
		金币产量,
		钢铁产量,
		稀矿产量,
		建筑生命值,
		建筑攻击力,
		兵种攻击力,
		兵种生命值,
		资源保护
	}

	public int level;

	public int money_buyed;

	public Dictionary<ResType, int> resReward = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemReward = new Dictionary<int, int>();

	public Dictionary<int, int> skillReward = new Dictionary<int, int>();

	public List<VipRight> rights = new List<VipRight>();

	public static int GetVipAddtion(float curValue, int vipLv, VipConst.Enum_VipRight type)
	{
		if (vipLv <= 0)
		{
			return 0;
		}
		float num = 0f;
		int i = 0;
		while (i < UnitConst.GetInstance().VipConstData[vipLv].rights.Count)
		{
			if (UnitConst.GetInstance().VipConstData[vipLv].rights[i].type == type)
			{
				if (UnitConst.GetInstance().VipConstData[vipLv].rights[i].isPer)
				{
					num = curValue * (float)UnitConst.GetInstance().VipConstData[vipLv].rights[i].value * 0.01f;
					return (int)num;
				}
				num = (float)UnitConst.GetInstance().VipConstData[vipLv].rights[i].value;
				return (int)num;
			}
			else
			{
				i++;
			}
		}
		return (int)num;
	}

	public static string GetVipAddtion(int vipLv, VipConst.Enum_VipRight type)
	{
		if (vipLv <= 0)
		{
			return string.Empty;
		}
		int i = 0;
		while (i < UnitConst.GetInstance().VipConstData[vipLv].rights.Count)
		{
			if (UnitConst.GetInstance().VipConstData[vipLv].rights[i].type == type)
			{
				if (UnitConst.GetInstance().VipConstData[vipLv].rights[i].isPer)
				{
					return string.Format("+{0}%", UnitConst.GetInstance().VipConstData[vipLv].rights[i].value);
				}
				return string.Format("+{0}", UnitConst.GetInstance().VipConstData[vipLv].rights[i].value);
			}
			else
			{
				i++;
			}
		}
		return string.Empty;
	}
}
