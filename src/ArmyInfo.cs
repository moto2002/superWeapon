using msg;
using System;

public class ArmyInfo
{
	public int soldierId;

	public int level = 1;

	public int starLevel = 1;

	public ArmyInfo()
	{
	}

	public ArmyInfo(int _id)
	{
		this.soldierId = _id;
	}

	public int updatingLv()
	{
		int num = this.level;
		foreach (KVStruct current in HeroInfo.GetInstance().PlayerArmy_AirDataCDTime)
		{
			if (current.key == (long)this.soldierId)
			{
				num++;
			}
		}
		foreach (KVStruct current2 in HeroInfo.GetInstance().PlayerArmy_LandDataCDTime)
		{
			if (current2.key == (long)this.soldierId)
			{
				num++;
			}
		}
		return num;
	}

	public bool isMaxLevel()
	{
		return this.level == UnitConst.GetInstance().soldierConst[this.soldierId].lvInfos.Count - 1;
	}
}
