using System;
using System.Collections.Generic;

public class BuildingModelData
{
	public int id;

	public string name;

	public string bodyId;

	public int size;

	public int bodySize;

	public int type;

	public int secondType;

	public Dictionary<int, string> level_bodyId = new Dictionary<int, string>();

	public Dictionary<int, string> rank_bodyId = new Dictionary<int, string>();
}
