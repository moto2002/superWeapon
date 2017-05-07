using System;
using System.Collections.Generic;

public sealed class IslandResData
{
	public class ResIslandOut
	{
		public ResType resType;

		public int speendValue;

		public int limit;
	}

	public Dictionary<ResType, int> IslandResNum = new Dictionary<ResType, int>();

	public Dictionary<long, int> CoinIslandes = new Dictionary<long, int>();

	public Dictionary<long, int> OilIslandes = new Dictionary<long, int>();

	public Dictionary<long, int> SteelIslandes = new Dictionary<long, int>();

	public Dictionary<long, int> RareEarthIslandes = new Dictionary<long, int>();

	public Dictionary<ResType, DateTime> IslandResTake = new Dictionary<ResType, DateTime>();

	public Dictionary<ResType, int> IslandResTmp = new Dictionary<ResType, int>();

	public Dictionary<ResType, Dictionary<long, List<DateTime>>> IslandChanged = new Dictionary<ResType, Dictionary<long, List<DateTime>>>();

	public Dictionary<int, Dictionary<ResType, IslandResData.ResIslandOut>> ResIslandOutputConst = new Dictionary<int, Dictionary<ResType, IslandResData.ResIslandOut>>();

	private float speed;

	private float add;

	private int limit;

	public void ClearData()
	{
		this.IslandResNum.Clear();
		this.CoinIslandes.Clear();
		this.OilIslandes.Clear();
		this.SteelIslandes.Clear();
		this.RareEarthIslandes.Clear();
		this.IslandResTake.Clear();
		this.IslandResTmp.Clear();
		this.ResIslandOutputConst.Clear();
		this.IslandChanged.Clear();
	}

	public float GetAllIslandSpeedValue(ResType restype)
	{
		switch (restype)
		{
		case ResType.金币:
			this.speed = (float)this.ResIslandOutputConst[HeroInfo.GetInstance().PlayerCommondLv][restype].speendValue;
			return this.speed;
		case ResType.石油:
		{
			float num = 0f;
			foreach (KeyValuePair<long, int> current in this.OilIslandes)
			{
				this.speed = (float)this.ResIslandOutputConst[current.Value][restype].speendValue;
				this.add = (float)AdjutantPanelData.GetResHomeAddttion((int)restype, 2);
				if (this.add > 0f)
				{
					this.speed *= 1f + this.add * 0.01f;
				}
				num += this.speed;
			}
			return num;
		}
		case ResType.钢铁:
		{
			float num2 = 0f;
			foreach (KeyValuePair<long, int> current2 in this.SteelIslandes)
			{
				this.speed = (float)this.ResIslandOutputConst[current2.Value][restype].speendValue;
				this.add = (float)AdjutantPanelData.GetResHomeAddttion((int)restype, 2);
				if (this.add > 0f)
				{
					this.speed *= 1f + this.add * 0.01f;
				}
				num2 += this.speed;
			}
			return num2;
		}
		case ResType.稀矿:
		{
			float num3 = 0f;
			foreach (KeyValuePair<long, int> current3 in this.RareEarthIslandes)
			{
				this.speed = (float)this.ResIslandOutputConst[current3.Value][restype].speendValue;
				this.add = (float)AdjutantPanelData.GetResHomeAddttion((int)restype, 2);
				if (this.add > 0f)
				{
					this.speed *= 1f + this.add * 0.01f;
				}
				num3 += this.speed;
			}
			return num3;
		}
		default:
			return 0f;
		}
	}

	public int GetLimitValue(ResType restype)
	{
		switch (restype)
		{
		case ResType.金币:
			return this.ResIslandOutputConst[HeroInfo.GetInstance().PlayerCommondLv][restype].limit * ((!this.IslandResNum.ContainsKey(ResType.金币)) ? 0 : this.IslandResNum[ResType.金币]);
		case ResType.石油:
		{
			int num = 0;
			foreach (KeyValuePair<long, int> current in this.OilIslandes)
			{
				this.limit = this.ResIslandOutputConst[current.Value][restype].limit;
				num += this.limit;
			}
			return num;
		}
		case ResType.钢铁:
		{
			int num2 = 0;
			foreach (KeyValuePair<long, int> current2 in this.SteelIslandes)
			{
				this.limit = this.ResIslandOutputConst[current2.Value][restype].limit;
				num2 += this.limit;
			}
			return num2;
		}
		case ResType.稀矿:
		{
			int num3 = 0;
			foreach (KeyValuePair<long, int> current3 in this.RareEarthIslandes)
			{
				this.limit = this.ResIslandOutputConst[current3.Value][restype].limit;
				num3 += this.limit;
			}
			return num3;
		}
		default:
			return 0;
		}
	}

	public int GetNumValue(ResType restype)
	{
		switch (restype)
		{
		case ResType.金币:
			return (!this.IslandResNum.ContainsKey(ResType.金币)) ? 0 : this.IslandResNum[ResType.金币];
		case ResType.石油:
			if (this.OilIslandes.Count > 0)
			{
				return 1;
			}
			return 0;
		case ResType.钢铁:
			if (this.SteelIslandes.Count > 0)
			{
				return 1;
			}
			return 0;
		case ResType.稀矿:
			if (this.RareEarthIslandes.Count > 0)
			{
				return 1;
			}
			return 0;
		default:
			return 0;
		}
	}
}
