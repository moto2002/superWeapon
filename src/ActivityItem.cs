using System;
using System.Collections.Generic;

public class ActivityItem
{
	public class Solider
	{
		public int id;

		public int index;

		public int lv;

		public int num;
	}

	public int id;

	public string name;

	public string desc;

	public ActType actType;

	public int npcId;

	public List<int> beginDayOfWeek = new List<int>();

	public int beginTime;

	public int durationTime;

	public int minLv;

	public int maxLv;

	public int freeTimes;

	public int times;

	public int needMoney;

	public int nextNeedMoneyMultiplier;

	public List<ActivityItem.Solider> soliders = new List<ActivityItem.Solider>();
}
