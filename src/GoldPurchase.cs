using System;
using System.Collections.Generic;

public class GoldPurchase
{
	public int id;

	public string name;

	public string desc;

	public Dictionary<ResType, int> number = new Dictionary<ResType, int>();
}
