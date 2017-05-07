using System;
using System.Collections.Generic;

public class MilitaryRankData
{
	public int id;

	public string name;

	public string description;

	public int commondLevel;

	public int medal;

	public string icon;

	public int money;

	public Dictionary<int, int> items = new Dictionary<int, int>();

	public Dictionary<int, int> skill = new Dictionary<int, int>();

	public Dictionary<ResType, int> res = new Dictionary<ResType, int>();
}
