using msg;
using System;
using System.Collections.Generic;

public class ActivityClass
{
	public int activityId;

	public int shopID;

	public int activityType;

	public string btnName;

	public string tittleName;

	public DateTime startTimeStr = DateTime.MinValue;

	public DateTime endTimeStr = DateTime.MinValue;

	public DateTime showEndTimeStr = DateTime.MinValue;

	public int rewardType;

	public string bannerID;

	public string nameID;

	public int rewardCount;

	public int conditionType;

	public int conditionID;

	public string conditionName;

	public string totalDiscription;

	public Dictionary<ResType, int> curActivityResReward = new Dictionary<ResType, int>();

	public Dictionary<int, int> curActivityItemReward = new Dictionary<int, int>();

	public Dictionary<int, int> curActivitySkillReward = new Dictionary<int, int>();

	public Dictionary<ResType, int> totalActivityResReward = new Dictionary<ResType, int>();

	public Dictionary<int, int> totalActivityItemReward = new Dictionary<int, int>();

	public Dictionary<int, int> totalActivitySkillReward = new Dictionary<int, int>();

	public List<KVStruct> numNeed = new List<KVStruct>();

	public List<KVStruct> numGet = new List<KVStruct>();

	public int sort;

	public string userName;

	public int type;

	public int repeatCount;

	public string uName;

	private int haveGetAwardCount
	{
		get
		{
			if (HeroInfo.GetInstance().ActivitiesData_RecieveCountServer.ContainsKey(this.conditionID))
			{
				return HeroInfo.GetInstance().ActivitiesData_RecieveCountServer[this.conditionID].count;
			}
			return 0;
		}
	}

	public int AwardCount
	{
		get
		{
			if (this.rewardCount > this.haveGetAwardCount)
			{
				return this.rewardCount - this.haveGetAwardCount;
			}
			return 0;
		}
	}

	public bool isPaying
	{
		get
		{
			return HeroInfo.GetInstance().ActivitiesData_RecieveState.ContainsKey(this.activityId) && HeroInfo.GetInstance().ActivitiesData_RecieveState[this.activityId] == 3;
		}
	}

	public bool isCanGetAward
	{
		get
		{
			return HeroInfo.GetInstance().ActivitiesData_RecieveState.ContainsKey(this.activityId) && HeroInfo.GetInstance().ActivitiesData_RecieveState[this.activityId] == 1;
		}
	}

	public bool isGetSpecilaGift
	{
		get
		{
			return HeroInfo.GetInstance().ActivitiesData_RecieveState.ContainsKey(this.activityId) && HeroInfo.GetInstance().ActivitiesData_RecieveState[this.activityId] == 2;
		}
	}

	public bool isReceived
	{
		get
		{
			if (HeroInfo.GetInstance().ActivitiesData_RecieveState.ContainsKey(this.activityId))
			{
				if (this.repeatCount == 0)
				{
					return HeroInfo.GetInstance().ActivitiesData_RecieveState[this.activityId] == 2;
				}
				if (HeroInfo.GetInstance().ActivitiesData_RecieveCount.ContainsKey(this.activityId) && this.repeatCount == HeroInfo.GetInstance().ActivitiesData_RecieveCount[this.activityId])
				{
					return HeroInfo.GetInstance().ActivitiesData_RecieveState[this.activityId] == 2;
				}
			}
			return false;
		}
	}

	public int receiveCount
	{
		get
		{
			if (HeroInfo.GetInstance().ActivitiesData_RecieveCount.ContainsKey(this.activityId))
			{
				return HeroInfo.GetInstance().ActivitiesData_RecieveCount[this.activityId];
			}
			return 0;
		}
	}
}
