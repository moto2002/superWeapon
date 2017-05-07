using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerStrong
{
	public int id;

	public int towerId;

	public int level;

	public int needLevel;

	public Dictionary<ResType, int> rescost = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemCost = new Dictionary<int, int>();

	public Dictionary<int, int> attribute = new Dictionary<int, int>();

	public string des;

	public Vector3[] makeShow;
}
