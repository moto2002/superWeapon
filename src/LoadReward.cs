using System;
using System.Collections.Generic;

public class LoadReward
{
	public int id;

	public int step;

	public string des;

	public DateTime time;

	public Dictionary<ResType, int> res = new Dictionary<ResType, int>();

	public Dictionary<int, int> item = new Dictionary<int, int>();

	public Dictionary<int, int> skill = new Dictionary<int, int>();

	public Dictionary<ResType, int> money = new Dictionary<ResType, int>();

	public bool isCanGetOnLine;
}
