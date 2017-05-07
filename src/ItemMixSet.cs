using System;
using System.Collections.Generic;

public class ItemMixSet
{
	private string name;

	private string description;

	private int mixedId;

	private Dictionary<Item, int> needItems = new Dictionary<Item, int>();

	private int gold;

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

	public int MixedId
	{
		get
		{
			return this.mixedId;
		}
		set
		{
			this.mixedId = value;
		}
	}

	public Dictionary<Item, int> NeedItems
	{
		get
		{
			return this.needItems;
		}
		set
		{
			this.needItems = value;
		}
	}

	public int Gold
	{
		get
		{
			return this.gold;
		}
		set
		{
			this.gold = value;
		}
	}
}
