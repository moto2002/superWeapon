using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceMgr
{
	public static int totalCoinChanged;

	public static int totalOilChanged;

	public static int totalSteelChanged;

	public static int totalRareEarthChanged;

	public static Dictionary<int, int> CoinPool = new Dictionary<int, int>();

	public static Dictionary<int, int> OilPool = new Dictionary<int, int>();

	public static Dictionary<int, int> SteelPool = new Dictionary<int, int>();

	public static Dictionary<int, int> RareEarthPool = new Dictionary<int, int>();

	private static int A
	{
		get
		{
			if (UnitConst.GetInstance().DesighConfigDic.ContainsKey(87))
			{
				return int.Parse(UnitConst.GetInstance().DesighConfigDic[87].value);
			}
			return 0;
		}
	}

	private static int B
	{
		get
		{
			if (UnitConst.GetInstance().DesighConfigDic.ContainsKey(88))
			{
				return int.Parse(UnitConst.GetInstance().DesighConfigDic[88].value);
			}
			return 0;
		}
	}

	private static int C
	{
		get
		{
			if (UnitConst.GetInstance().DesighConfigDic.ContainsKey(89))
			{
				return int.Parse(UnitConst.GetInstance().DesighConfigDic[89].value);
			}
			return 0;
		}
	}

	private static int D
	{
		get
		{
			if (UnitConst.GetInstance().DesighConfigDic.ContainsKey(90))
			{
				return int.Parse(UnitConst.GetInstance().DesighConfigDic[90].value);
			}
			return 0;
		}
	}

	public static bool IsOutBuyCoinLimit(int resNum)
	{
		return false;
	}

	public static bool IsOutBuyOilLimit(int resNum)
	{
		return false;
	}

	public static bool IsOutBuySteelLimit(int resNum)
	{
		return false;
	}

	public static bool IsOutBuyRareEarthLimit(int resNum)
	{
		return false;
	}

	public static int GetRMBNum(BuildingProductType resType, int resNum)
	{
		double num = 0.0;
		int num2 = 0;
		int num3 = 0;
		switch (resType)
		{
		case BuildingProductType.coin:
		{
			num3 = HeroInfo.GetInstance().playerRes.resCoin;
			if (ResourceMgr.IsOutBuyCoinLimit(resNum))
			{
				return -1;
			}
			List<int> list = ResourceMgr.CoinPool.Keys.ToList<int>();
			int num4 = ResourceMgr.totalCoinChanged;
			num2 = ResourceMgr.totalCoinChanged;
			int num5 = list.FindIndex((int a) => ResourceMgr.totalCoinChanged + resNum < a || a == 0);
			if (num5 == -1)
			{
				num5 = list.Count - 1;
			}
			for (int i = 0; i <= num5; i++)
			{
				if (i == num5)
				{
					int num6 = Math.Abs(ResourceMgr.totalCoinChanged + resNum - num4);
					double num7 = (double)ResourceMgr.CoinPool[list[i]] * 0.0001;
					num += (double)num6 * num7;
				}
				else if (list[i] > ResourceMgr.totalCoinChanged)
				{
					num += (double)Math.Abs(list[i] - num4) * ((double)ResourceMgr.CoinPool[list[i]] * 0.0001);
					num4 = list[i];
				}
			}
			break;
		}
		case BuildingProductType.oil:
		{
			num3 = HeroInfo.GetInstance().playerRes.resOil;
			if (ResourceMgr.IsOutBuyOilLimit(resNum))
			{
				return -1;
			}
			List<int> list = ResourceMgr.OilPool.Keys.ToList<int>();
			int num4 = ResourceMgr.totalOilChanged;
			num2 = ResourceMgr.totalOilChanged;
			int num5 = list.FindIndex((int a) => ResourceMgr.totalOilChanged + resNum < a || a == 0);
			if (num5 == -1)
			{
				num5 = list.Count - 1;
			}
			LogManage.Log(num5);
			for (int j = 0; j <= num5; j++)
			{
				if (j == num5)
				{
					int num8 = Math.Abs(ResourceMgr.totalOilChanged + resNum - num4);
					double num9 = (double)((float)ResourceMgr.OilPool[list[j]] * 0.0001f);
					num += (double)num8 * num9;
				}
				else if (list[j] > ResourceMgr.totalOilChanged)
				{
					num += (double)((float)Math.Abs(list[j] - num4) * ((float)ResourceMgr.OilPool[list[j]] * 0.0001f));
					num4 = list[j];
				}
			}
			break;
		}
		case BuildingProductType.steel:
		{
			num3 = HeroInfo.GetInstance().playerRes.resSteel;
			if (ResourceMgr.IsOutBuySteelLimit(resNum))
			{
				return -1;
			}
			List<int> list = ResourceMgr.SteelPool.Keys.ToList<int>();
			int num4 = ResourceMgr.totalSteelChanged;
			num2 = ResourceMgr.totalSteelChanged;
			int num5 = list.FindIndex((int a) => ResourceMgr.totalSteelChanged + resNum < a || a == 0);
			if (num5 == -1)
			{
				num5 = list.Count - 1;
			}
			for (int k = 0; k <= num5; k++)
			{
				if (k == num5)
				{
					num += (double)Math.Abs(ResourceMgr.totalSteelChanged + resNum - num4) * ((double)ResourceMgr.SteelPool[list[k]] * 0.0001);
				}
				else if (list[k] > ResourceMgr.totalSteelChanged)
				{
					num += (double)Math.Abs(list[k] - num4) * ((double)ResourceMgr.SteelPool[list[k]] * 0.0001);
					num4 = list[k];
				}
			}
			break;
		}
		case BuildingProductType.rareEarth:
		{
			num3 = HeroInfo.GetInstance().playerRes.resRareEarth;
			if (ResourceMgr.IsOutBuyRareEarthLimit(resNum))
			{
				return -1;
			}
			List<int> list = ResourceMgr.RareEarthPool.Keys.ToList<int>();
			int num4 = ResourceMgr.totalRareEarthChanged;
			num2 = ResourceMgr.totalRareEarthChanged;
			int num5 = list.FindIndex((int a) => ResourceMgr.totalRareEarthChanged + resNum < a || a == 0);
			if (num5 == -1)
			{
				num5 = list.Count - 1;
			}
			for (int l = 0; l <= num5; l++)
			{
				if (l == num5)
				{
					num += (double)((float)Math.Abs(ResourceMgr.totalRareEarthChanged + resNum - num4) * ((float)ResourceMgr.RareEarthPool[list[l]] * 0.0001f));
				}
				else if (list[l] > ResourceMgr.totalRareEarthChanged)
				{
					num += (double)((float)Math.Abs(list[l] - num4) * ((float)ResourceMgr.RareEarthPool[list[l]] * 0.0001f));
					num4 = list[l];
				}
			}
			break;
		}
		}
		LogManage.Log(string.Format("需要的资源类型：{0},  数量：{2},  钻石数{1}  当前客户端拥有的数目{3}  当前已购买数目:{4}", new object[]
		{
			resType.ToString(),
			(int)Math.Ceiling(num),
			resNum,
			num3,
			num2
		}));
		return (int)Math.Ceiling(Math.Round(num, 5));
	}

	public static int GetRmbNum(double Milliseconds)
	{
		if (Milliseconds <= 0.0)
		{
			return 0;
		}
		int num = (int)Math.Ceiling(Milliseconds * 0.001);
		if (num <= 60)
		{
			return ResourceMgr.A;
		}
		if (num <= 3600)
		{
			float num2 = (float)(ResourceMgr.B - ResourceMgr.A) * ((float)num - 60f);
			float f = num2 / 3540f;
			return Mathf.RoundToInt(f) + ResourceMgr.A;
		}
		if (num <= 86400)
		{
			float num3 = (float)(ResourceMgr.C - ResourceMgr.B) * ((float)num - 3600f);
			float f2 = num3 / 82800f;
			return Mathf.RoundToInt(f2) + ResourceMgr.B;
		}
		float num4 = (float)(ResourceMgr.D - ResourceMgr.C) * ((float)num - 86400f);
		float f3 = num4 / 518400f;
		return Mathf.RoundToInt(f3) + ResourceMgr.C;
	}
}
