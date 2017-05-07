using System;
using System.Collections.Generic;

public class Achievement
{
	public int id;

	public string name;

	public string description;

	public int conditionId;

	public int secondConditionId;

	public List<int> step = new List<int>();

	public List<int> prizes = new List<int>();

	public string iconName;

	public int stepRecord;

	public int lastStar;

	public bool isCanRecieved
	{
		get
		{
			return (this.lastStar == 0 && this.stepRecord >= this.step[0]) || (this.lastStar == 1 && this.stepRecord >= this.step[1]) || (this.lastStar == 2 && this.stepRecord >= this.step[2]) || (this.lastStar == 3 && this.stepRecord >= this.step[3]) || (this.lastStar == 4 && this.stepRecord >= this.step[4]);
		}
	}
}
