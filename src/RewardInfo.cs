using System;

public class RewardInfo
{
	public int playerExp;

	public int junLing;

	public int tanSuoLing;

	public int playermedal;

	public int resCoin;

	public int maxCoin;

	public int resOil;

	public int maxOil;

	public int resRareEarth;

	public int maxRareEarth;

	public int resSteel;

	public int normalTimes;

	public int specialTimes;

	public int maxSteel;

	public int skillPoint;

	public int RMBCoin;

	public int skillDebris;

	public bool IsCanCollectCoin
	{
		get
		{
			return this.maxCoin > this.resCoin;
		}
	}

	public bool IsCanCollectOil
	{
		get
		{
			return this.maxOil > this.resOil;
		}
	}

	public bool IsCanCollectSteel
	{
		get
		{
			return this.maxSteel > this.resSteel;
		}
	}

	public bool IsCanCollectRarearth
	{
		get
		{
			return this.maxRareEarth > this.resRareEarth;
		}
	}
}
