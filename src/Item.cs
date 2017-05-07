using System;
using System.Collections.Generic;

public class Item
{
	private int id;

	private string name;

	private string description;

	private Quility_ResAndItemAndSkill quality;

	private TypeItem type;

	private int price;

	private int convertMoney;

	private Dictionary<int, int> giveItems = new Dictionary<int, int>();

	private Dictionary<ResType, int> giveRes = new Dictionary<ResType, int>();

	private Dictionary<ResType, int> costRes = new Dictionary<ResType, int>();

	private string iconId;

	public int Id
	{
		get
		{
			return this.id;
		}
		set
		{
			this.id = value;
		}
	}

	public string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.name = value;
		}
	}

	public string Description
	{
		get
		{
			return this.description;
		}
		set
		{
			this.description = value;
		}
	}

	public Quility_ResAndItemAndSkill Quality
	{
		get
		{
			return this.quality;
		}
		set
		{
			this.quality = value;
		}
	}

	public TypeItem Type
	{
		get
		{
			return this.type;
		}
		set
		{
			this.type = value;
		}
	}

	public int Price
	{
		get
		{
			return this.price;
		}
		set
		{
			this.price = value;
		}
	}

	public int ConvertMoney
	{
		get
		{
			return this.convertMoney;
		}
		set
		{
			this.convertMoney = value;
		}
	}

	public Dictionary<int, int> GiveItems
	{
		get
		{
			return this.giveItems;
		}
		set
		{
			this.giveItems = value;
		}
	}

	public Dictionary<ResType, int> GiveRes
	{
		get
		{
			return this.giveRes;
		}
		set
		{
			this.giveRes = value;
		}
	}

	public Dictionary<ResType, int> CostRes
	{
		get
		{
			return this.costRes;
		}
		set
		{
			this.costRes = value;
		}
	}

	public string IconId
	{
		get
		{
			return this.iconId;
		}
		set
		{
			this.iconId = value;
		}
	}

	public bool IsCanBuy
	{
		get
		{
			return this.convertMoney > 0;
		}
	}

	public bool IsCanMix
	{
		get
		{
			if (UnitConst.GetInstance().ItemMixSetConst.ContainsKey(this.id))
			{
				foreach (KeyValuePair<Item, int> current in UnitConst.GetInstance().ItemMixSetConst[this.id].NeedItems)
				{
					if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current.Key.id) || HeroInfo.GetInstance().PlayerItemInfo[current.Key.id] < current.Value)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}
