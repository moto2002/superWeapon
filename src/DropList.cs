using System;
using System.Collections.Generic;

public class DropList
{
	public int id;

	public string name;

	public string description;

	public Dictionary<int, int> boxRate = new Dictionary<int, int>();

	public Dictionary<ResType, int> res = new Dictionary<ResType, int>();
}
