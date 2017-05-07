using System;
using System.Collections.Generic;

public class ArmsDealer
{
	public int id;

	public string name;

	public string description;

	public int type;

	public int priceType;

	public int show;

	public int sort;

	public int sales;

	public Dictionary<int, int> ItemSeller = new Dictionary<int, int>();

	public Dictionary<int, int> SkillSeller = new Dictionary<int, int>();

	public Dictionary<ResType, int> ResBuyer = new Dictionary<ResType, int>();

	public Dictionary<int, int> ItemBuyer = new Dictionary<int, int>();

	public bool isSelled;
}
