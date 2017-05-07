using System;

public struct VIP
{
	public int superCard;

	public int money_Buyed;

	public long cardEndTime;

	public long cardPrizeTime;

	public long vipPrizeTime;

	public long scardPrizeTime;

	public bool IsVIP
	{
		get
		{
			return this.money_Buyed >= UnitConst.GetInstance().VipConstData[1].money_buyed;
		}
	}

	public int VipLevel
	{
		get
		{
			int result = 0;
			for (int i = 1; i < 100; i++)
			{
				if (!UnitConst.GetInstance().VipConstData.ContainsKey(i))
				{
					break;
				}
				if (this.money_Buyed < UnitConst.GetInstance().VipConstData[i].money_buyed)
				{
					break;
				}
				result = i;
			}
			return result;
		}
	}

	public int NextVipNeedMoney
	{
		get
		{
			if (UnitConst.GetInstance().VipConstData.ContainsKey(this.VipLevel + 1))
			{
				return UnitConst.GetInstance().VipConstData[this.VipLevel + 1].money_buyed - this.money_Buyed;
			}
			return 0;
		}
	}
}
