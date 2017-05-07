using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrade
{
	public int id;

	public int itemid;

	public int level;

	public int needLevel;

	public Dictionary<ResType, int> resCost = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemCost = new Dictionary<int, int>();

	public Vector3[] makeShow;

	public List<int> buildUpGradeShow = new List<int>();

	public string bodyName;

	public string des;

	public int output;

	public int outlimit;
}
