using DicForUnity;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class HeroInfo
{
	public enum MilitaryRank
	{
		三等兵 = 1,
		二等兵,
		一等兵,
		上等兵,
		下士,
		中士,
		上士,
		少尉,
		中尉,
		上尉,
		少校,
		中校,
		上校,
		大校,
		少将,
		中将,
		上将,
		元帅
	}

	public global::ChatMessage chatMessage = new global::ChatMessage();

	public Dictionary<long, LegionHelpApply> legionApply = new Dictionary<long, LegionHelpApply>();

	public List<long> applyIdList = new List<long>();

	private static HeroInfo heroInfo;

	public bool firstWMap = true;

	public GameStart_AttackNPC gameStart = new GameStart_AttackNPC();

	public WorldMapInfo worldMapInfo = new WorldMapInfo();

	public IslandResData islandResData = new IslandResData();

	public List<SkillCarteData> skillCarteList = new List<SkillCarteData>();

	public long userId;

	public string userName;

	public long createTime;

	public string token = "token";

	public string time = "10000";

	public string platformId = "platformId";

	public string channelId = "channelId";

	public string account;

	public string area;

	private int all_PeopleNum_Occupy;

	private Dictionary<int, int> BasicArmyList = new Dictionary<int, int>();

	public int playerlevel;

	public string playerServer;

	public long playerGroupId;

	public string playerGroup;

	public int topScore;

	public int playerGroupJob;

	public int addMedal;

	public DateTime LegionOutTime = DateTime.MinValue;

	public GameAnnouncementData gameAnnouncementData = new GameAnnouncementData();

	public VIP vipData = default(VIP);

	public float posCD = 0.2f;

	public List<EnemyNpcAttack> enemyAttack = new List<EnemyNpcAttack>();

	public List<EquipItem> equipList = new List<EquipItem>();

	public int buyArmyTokenTimes;

	private int ClearDataId;

	private List<long> buildCD = new List<long>();

	public Dictionary<long, long> BuildingCDEndTime = new Dictionary<long, long>();

	public long armyBuildingCDTime;

	public long airBuildingCDTime;

	public long armyBuildingCDTime_BuildingID;

	public long airBuildingCDTime_BuildingID;

	public List<long> BuildId = new List<long>();

	public bool isBuild;

	public Dictionary<int, ArmyInfo> PlayerArmyData = new Dictionary<int, ArmyInfo>();

	public List<KVStruct> PlayerArmy_LandDataCDTime = new List<KVStruct>();

	public List<KVStruct> PlayerArmy_AirDataCDTime = new List<KVStruct>();

	private List<int> canUpLevelArmy = new List<int>();

	private Dictionary<int, int> playerTechnologyInfo = new Dictionary<int, int>();

	public int PlayerTechnologyUpdatingItemID;

	public long PlayerTechnologyUpdatingTime;

	private Dictionary<int, int> skillInfo = new Dictionary<int, int>();

	public Dictionary<int, int> PlayerBuildingLevel = new Dictionary<int, int>();

	public Dictionary<int, PlayerCommando> PlayerCommandoes = new Dictionary<int, PlayerCommando>();

	public SCSoldierConfigure PlayerCommandoFuncingOrEnd = new SCSoldierConfigure();

	public RewardInfo playerRes = new RewardInfo();

	public int addExp;

	public List<int> addArmy_open = new List<int>();

	public List<SCPlayerResource> addResouce = new List<SCPlayerResource>();

	public List<SCPlayerResource> subResouce = new List<SCPlayerResource>();

	public List<SCSkillData> addSkill = new List<SCSkillData>();

	public List<SCSkillData> subSkill = new List<SCSkillData>();

	public Dictionary<int, int> PlayerItemInfo = new Dictionary<int, int>();

	public List<KVStruct_Client> addItem = new List<KVStruct_Client>();

	public List<KVStruct_Client> subItem = new List<KVStruct_Client>();

	public List<EquipItem> EquipItem = new List<EquipItem>();

	public List<EquipItem> AddEquipItem = new List<EquipItem>();

	public List<EquipItem> SubEquipItem = new List<EquipItem>();

	public List<KVStruct_Client> addBox = new List<KVStruct_Client>();

	public int homeInWMapIdx;

	public long homeMapID;

	public long PlayerCommandoBuildingID;

	public Dictionary<long, armyInfoInBuilding> AllArmyInfo = new Dictionary<long, armyInfoInBuilding>();

	public Dictionary<int, int> ActivitiesData_RecieveState = new Dictionary<int, int>();

	public Dictionary<int, int> ActivitiesData_RecieveCount = new Dictionary<int, int>();

	public Dictionary<int, SCActivityCountsData> ActivitiesData_RecieveCountServer = new Dictionary<int, SCActivityCountsData>();

	public Dictionary<int, SCRanking> ActivitiesData_Ranking = new Dictionary<int, SCRanking>();

	public Dictionary<int, List<ActivityClass>> activityClass = new Dictionary<int, List<ActivityClass>>();

	public Dictionary<int, List<ActivityClass>> reChargeClass = new Dictionary<int, List<ActivityClass>>();

	public Dictionary<int, List<ActivityClass>> ShouChongChargeClass = new Dictionary<int, List<ActivityClass>>();

	public Dictionary<int, List<ActivityClass>> OneYuanGouChargeClass = new Dictionary<int, List<ActivityClass>>();

	public Dictionary<int, List<ActivityClass>> BaseGiftClass = new Dictionary<int, List<ActivityClass>>();

	public int Activety_DayOfDay_HavedID;

	public DateTime Activety_DayOfDay_HavedDatetime;

	public long MilitaryRankGift_Time;

	public long TechGetRMB_Time;

	public LotteryData LotteryData;

	public int LotteryDataFreeTimes;

	public bool IsBuyChengZhangJiJin;

	public List<int> AllDisplayActives = new List<int>();

	public bool HavedDisplayActity;

	public EmailManager emilData = new EmailManager();

	public ArmyGroupData armyGroupDataData = new ArmyGroupData();

	public LegionBattleData MyLegionBattleData = new LegionBattleData();

	public List<ReportData> AttrackReport = new List<ReportData>();

	public List<ReportData> DefReport = new List<ReportData>();

	public List<ReportData> attrackTitle = new List<ReportData>();

	public List<ReportData> defTitle = new List<ReportData>();

	public Dictionary<long, ReportData> PVPReportDataList = new Dictionary<long, ReportData>();

	public int ServerID = -1;

	public string IP = string.Empty;

	public string userName_Default
	{
		get
		{
			return "游客" + this.userId;
		}
	}

	public HeroInfo.MilitaryRank PlayerMilitaryRank
	{
		get
		{
			int playerCommondLv = HeroInfo.GetInstance().PlayerCommondLv;
			int playermedal = HeroInfo.GetInstance().playerRes.playermedal;
			int num = 0;
			foreach (KeyValuePair<int, MilitaryRankData> current in UnitConst.GetInstance().MilitaryRankDataList)
			{
				if (playerCommondLv < current.Value.commondLevel || playermedal < current.Value.medal)
				{
					num = current.Value.id;
					break;
				}
			}
			num = Mathf.Max(num - 1, 1);
			return (HeroInfo.MilitaryRank)num;
		}
	}

	public int All_PeopleNum
	{
		get
		{
			int num = 0;
			if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
			{
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					if (SenceManager.inst.towers[i].index == 13 || SenceManager.inst.towers[i].index == 91)
					{
						num += UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].lvInfos[SenceManager.inst.towers[i].lv].outputLimit[ResType.兵种];
					}
				}
			}
			return num;
		}
	}

	public int All_PeopleNum_Occupy
	{
		get
		{
			int num = 0;
			if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
			{
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					if ((SenceManager.inst.towers[i].index == 13 || SenceManager.inst.towers[i].index == 91) && HeroInfo.GetInstance().AllArmyInfo.ContainsKey(SenceManager.inst.towers[i].id))
					{
						armyInfoInBuilding armyInfoInBuilding = HeroInfo.GetInstance().AllArmyInfo[SenceManager.inst.towers[i].id];
						for (int j = 0; j < armyInfoInBuilding.armyFunced.Count; j++)
						{
							if (armyInfoInBuilding.armyFunced[j].value > 0L)
							{
								num += (int)(armyInfoInBuilding.armyFunced[j].value * (long)UnitConst.GetInstance().soldierConst[(int)armyInfoInBuilding.armyFunced[j].key].peopleNum);
							}
						}
						for (int k = 0; k < armyInfoInBuilding.armyFuncing.Count; k++)
						{
							num += UnitConst.GetInstance().soldierConst[(int)armyInfoInBuilding.armyFuncing[k].value].peopleNum;
						}
					}
				}
			}
			return num;
		}
		set
		{
			this.all_PeopleNum_Occupy = value;
		}
	}

	public Dictionary<int, int> CheakArmyList
	{
		get
		{
			if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
			{
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					if ((SenceManager.inst.towers[i].index == 13 || SenceManager.inst.towers[i].index == 91) && HeroInfo.GetInstance().AllArmyInfo.ContainsKey(SenceManager.inst.towers[i].id))
					{
						armyInfoInBuilding armyInfoInBuilding = HeroInfo.GetInstance().AllArmyInfo[SenceManager.inst.towers[i].id];
						for (int j = 0; j < armyInfoInBuilding.armyFunced.Count; j++)
						{
							if (this.BasicArmyList.ContainsKey((int)armyInfoInBuilding.armyFunced[j].key))
							{
								Dictionary<int, int> basicArmyList;
								Dictionary<int, int> expr_D5 = basicArmyList = this.BasicArmyList;
								int num;
								int expr_E9 = num = (int)armyInfoInBuilding.armyFunced[j].key;
								num = basicArmyList[num];
								expr_D5[expr_E9] = num - (int)armyInfoInBuilding.armyFunced[j].value;
								if (this.BasicArmyList[(int)armyInfoInBuilding.armyFunced[j].key] <= 0)
								{
									this.BasicArmyList.Remove((int)armyInfoInBuilding.armyFunced[j].key);
								}
							}
						}
					}
				}
				if (HeroInfo.GetInstance().Commando_Fight != null)
				{
					this.BasicArmyList.Remove(HeroInfo.GetInstance().Commando_Fight.index + 1000);
				}
			}
			return this.BasicArmyList;
		}
		set
		{
			this.BasicArmyList.Clear();
			if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
			{
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					if ((SenceManager.inst.towers[i].index == 13 || SenceManager.inst.towers[i].index == 91) && HeroInfo.GetInstance().AllArmyInfo.ContainsKey(SenceManager.inst.towers[i].id))
					{
						armyInfoInBuilding armyInfoInBuilding = HeroInfo.GetInstance().AllArmyInfo[SenceManager.inst.towers[i].id];
						for (int j = 0; j < armyInfoInBuilding.armyFunced.Count; j++)
						{
							if (!this.BasicArmyList.ContainsKey((int)armyInfoInBuilding.armyFunced[j].key))
							{
								this.BasicArmyList.Add((int)armyInfoInBuilding.armyFunced[j].key, (int)armyInfoInBuilding.armyFunced[j].value);
							}
							else
							{
								Dictionary<int, int> basicArmyList;
								Dictionary<int, int> expr_114 = basicArmyList = this.BasicArmyList;
								int num;
								int expr_128 = num = (int)armyInfoInBuilding.armyFunced[j].key;
								num = basicArmyList[num];
								expr_114[expr_128] = num + (int)armyInfoInBuilding.armyFunced[j].value;
							}
						}
					}
				}
				if (HeroInfo.GetInstance().Commando_Fight != null)
				{
					this.BasicArmyList.Add(HeroInfo.GetInstance().Commando_Fight.index + 1000, 1);
				}
			}
		}
	}

	public int buySoliderInBattleMaxTimes
	{
		get
		{
			if (HeroInfo.GetInstance().vipData.IsVIP)
			{
				return 5;
			}
			return 1;
		}
	}

	public int PlayerRadarLv
	{
		get
		{
			if (this.PlayerBuildingLevel.ContainsKey(10))
			{
				return this.PlayerBuildingLevel[10];
			}
			return 0;
		}
	}

	public int PlayerCommondLv
	{
		get
		{
			if (this.PlayerBuildingLevel.ContainsKey(1))
			{
				return this.PlayerBuildingLevel[1];
			}
			return 0;
		}
	}

	public int PlayerElectricPowerPlantLV
	{
		get
		{
			if (this.PlayerBuildingLevel.ContainsKey(62))
			{
				return this.PlayerBuildingLevel[62];
			}
			return 0;
		}
	}

	public int PlayerTechBuildingLv
	{
		get
		{
			if (this.PlayerBuildingLevel.ContainsKey(11))
			{
				return this.PlayerBuildingLevel[11];
			}
			return 0;
		}
	}

	public int ChronosphereLv
	{
		get
		{
			if (this.PlayerBuildingLevel.ContainsKey(25))
			{
				return this.PlayerBuildingLevel[25];
			}
			return 0;
		}
	}

	public List<long> BuildCD
	{
		get
		{
			DicForU.GetKeys<long, long>(HeroInfo.GetInstance().BuildingCDEndTime, this.buildCD);
			int num = this.buildCD.Count - 1;
			for (int i = num; i >= 0; i--)
			{
				if (HeroInfo.GetInstance().BuildingCDEndTime[this.buildCD[i]] <= TimeTools.GetNowTimeSyncServerToLong())
				{
					this.buildCD.Remove(this.buildCD[i]);
				}
			}
			return this.buildCD;
		}
	}

	public Technology GetUpdatingTech
	{
		get
		{
			return null;
		}
	}

	public Dictionary<int, int> PlayerTechnologyInfo
	{
		get
		{
			if (this.playerTechnologyInfo.Count == 0)
			{
				foreach (KeyValuePair<Vector2, Technology> current in UnitConst.GetInstance().TechnologyDataConst)
				{
					if (current.Key.y == 0f)
					{
						this.playerTechnologyInfo.Add(current.Value.itemid, 0);
					}
				}
			}
			return this.playerTechnologyInfo;
		}
		set
		{
			this.playerTechnologyInfo = value;
		}
	}

	public Dictionary<int, int> SkillInfo
	{
		get
		{
			if (this.skillInfo.Count == 0)
			{
				foreach (SkillUpdate current in UnitConst.GetInstance().skillUpdateConst.Values)
				{
					if (!this.skillInfo.ContainsKey(current.itemId))
					{
						this.skillInfo.Add(current.itemId, 1);
					}
				}
			}
			return this.skillInfo;
		}
		set
		{
			HeroInfo.GetInstance().skillInfo = value;
		}
	}

	public Dictionary<int, int> PlayerBuildingLimit
	{
		get
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (KeyValuePair<int, int> current in UnitConst.GetInstance().HomeUpdateOpenSetDataConst[HeroInfo.GetInstance().PlayerCommondLv].buildingIds)
			{
				dictionary.Add(current.Key, current.Value);
			}
			if (!dictionary.ContainsKey(20))
			{
				dictionary.Add(20, 0);
			}
			if (!dictionary.ContainsKey(21))
			{
				dictionary.Add(21, 0);
			}
			dictionary[20] = dictionary[20] + Technology.GetTechAddtion(dictionary[20], this.PlayerTechnologyInfo, Technology.Enum_TechnologyProps.地雷建造上限);
			dictionary[21] = dictionary[21] + Technology.GetTechAddtion(dictionary[21], this.PlayerTechnologyInfo, Technology.Enum_TechnologyProps.地雷建造上限);
			return dictionary;
		}
	}

	public PlayerCommando Commando_Fight
	{
		get
		{
			if (this.PlayerCommandoes.ContainsKey(this.PlayerCommandoFuncingOrEnd.soldierItemId) && this.PlayerCommandoFuncingOrEnd.cdTime == 0L)
			{
				return this.PlayerCommandoes[this.PlayerCommandoFuncingOrEnd.soldierItemId];
			}
			return null;
		}
	}

	public Dictionary<long, long> ArmysToBattle
	{
		get
		{
			Dictionary<long, long> dictionary = new Dictionary<long, long>();
			foreach (armyInfoInBuilding current in this.AllArmyInfo.Values)
			{
				foreach (KVStruct current2 in current.armyFunced)
				{
					if (current2.value > 0L)
					{
						if (!dictionary.ContainsKey(current2.key))
						{
							dictionary.Add(current2.key, current2.value);
						}
						else
						{
							dictionary[current2.key] = dictionary[current2.key] + current2.value;
						}
					}
				}
			}
			return dictionary;
		}
	}

	public bool ISCanAttackByAllSoldier
	{
		get
		{
			foreach (armyInfoInBuilding current in this.AllArmyInfo.Values)
			{
				foreach (KVStruct current2 in current.armyFunced)
				{
					if (current2.value > 0L)
					{
						bool result = true;
						return result;
					}
				}
			}
			if (this.PlayerCommandoFuncingOrEnd.soldierItemId > 0 && this.PlayerCommandoFuncingOrEnd.cdTime == 0L)
			{
				return true;
			}
			foreach (SkillCarteData current3 in HeroInfo.GetInstance().skillCarteList)
			{
				if (current3.index > 0)
				{
					bool result = true;
					return result;
				}
			}
			return false;
		}
	}

	private HeroInfo()
	{
	}

	public void ClearData()
	{
		HeroInfo.heroInfo = null;
	}

	public static HeroInfo GetInstance()
	{
		if (HeroInfo.heroInfo != null)
		{
			return HeroInfo.heroInfo;
		}
		HeroInfo.heroInfo = new HeroInfo();
		return HeroInfo.heroInfo;
	}

	public void RemoveEnemyNpcAttack(int id)
	{
		for (int i = 0; i < this.enemyAttack.Count; i++)
		{
			if (this.enemyAttack[i].id == id)
			{
				this.enemyAttack.RemoveAt(i);
			}
		}
	}

	public int ResHomeSpeedByStepEleVip(ResType restype)
	{
		float num = 0f;
		if (SenceManager.inst)
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 3 && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].outputType == restype)
				{
					num += SenceManager.inst.towers[i].ResSpeed_ByStep_Ele_Tech_Vip;
				}
			}
		}
		return (int)num;
	}

	public int ResHomeSpeed_NoEleVip(ResType restype)
	{
		float num = 0f;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 3 && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].outputType == restype)
			{
				num += SenceManager.inst.towers[i].ResSpeed_No_Ele_Tech_Vip;
			}
		}
		return (int)num;
	}

	public int ResHomeLimit_ByTech(ResType restype)
	{
		float num = 0f;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 2 || UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 1)
			{
				num += SenceManager.inst.towers[i].ResMaxLimit_ByTech(restype);
			}
		}
		return (int)num;
	}

	public int ResHomeLimit_NoTech(ResType restype)
	{
		float num = 0f;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 2 || UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 1)
			{
				num += SenceManager.inst.towers[i].ResMaxLimit_StoreNoByTech(restype);
			}
		}
		return (int)num;
	}

	public int ResProtectNumByVip()
	{
		int result = 0;
		if (VipConst.GetVipAddtion(100f, this.vipData.VipLevel, VipConst.Enum_VipRight.资源保护) > 0)
		{
			result = VipConst.GetVipAddtion(100f, this.vipData.VipLevel, VipConst.Enum_VipRight.资源保护);
		}
		return result;
	}

	public void ClearAddAndSumData()
	{
		if (this.ClearDataId == NetMgr.reqid)
		{
			return;
		}
		this.ClearDataId = NetMgr.reqid;
		LogManage.Log("清空   增加/减少  的数据~ ~  ~ ·");
		this.addResouce.Clear();
		this.subResouce.Clear();
		this.addItem.Clear();
		this.subItem.Clear();
		this.AddEquipItem.Clear();
		this.SubEquipItem.Clear();
		this.addSkill.Clear();
		this.subSkill.Clear();
		this.addArmy_open.Clear();
		this.addBox.Clear();
		this.addExp = 0;
	}

	public void AddResources(SCPlayerResource item)
	{
		for (int i = 0; i < this.addResouce.Count; i++)
		{
			if (this.addResouce[i].resId == item.resId)
			{
				this.addResouce[i].resVal += item.resVal;
				return;
			}
		}
		this.addResouce.Add(item);
	}

	public void AddItem(SCPlayerItem item)
	{
		for (int i = 0; i < this.addItem.Count; i++)
		{
			if (this.addItem[i].Key == item.itemId)
			{
				this.addItem[i].Value += (int)item.value;
				return;
			}
		}
		this.addItem.Add(new KVStruct_Client(item.itemId, (int)item.value));
	}

	public void ReSetArmy_AirBuildingCD()
	{
		this.armyBuildingCDTime = 0L;
		this.airBuildingCDTime = 0L;
		foreach (KeyValuePair<long, long> current in this.BuildingCDEndTime)
		{
			if (SenceInfo.curMap.towerList_Data.ContainsKey(current.Key))
			{
				if (UnitConst.GetInstance().buildingConst[SenceInfo.curMap.towerList_Data[current.Key].buildingIdx].secType == 6 && current.Value > 0L)
				{
					this.armyBuildingCDTime_BuildingID = current.Key;
					this.armyBuildingCDTime = current.Value;
				}
				if (UnitConst.GetInstance().buildingConst[SenceInfo.curMap.towerList_Data[current.Key].buildingIdx].secType == 21 && current.Value > 0L)
				{
					this.airBuildingCDTime_BuildingID = current.Key;
					this.airBuildingCDTime = current.Value;
				}
			}
		}
	}

	public long getBuildingCdId()
	{
		long result = 0L;
		long num = 9223372036854775807L;
		for (int i = 0; i < this.BuildCD.Count; i++)
		{
			if (num > HeroInfo.GetInstance().BuildingCDEndTime[this.BuildCD[i]])
			{
				result = this.BuildCD[i];
				num = HeroInfo.GetInstance().BuildingCDEndTime[this.BuildCD[i]];
			}
		}
		return result;
	}

	public double GetBuildingTimeCDDouble(long BuildingId)
	{
		if (BuildingId != 0L && HeroInfo.GetInstance().BuildCD.Contains(BuildingId))
		{
			return TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[BuildingId]));
		}
		return 0.0;
	}

	public int GetArmyLevelByID(int id)
	{
		if (this.PlayerArmyData.ContainsKey(id))
		{
			return this.PlayerArmyData[id].level;
		}
		return 1;
	}

	public bool IsCanUplevelArmy()
	{
		DicForU.GetKeys<int, ArmyInfo>(this.PlayerArmyData, this.canUpLevelArmy);
		for (int i = 0; i < this.canUpLevelArmy.Count; i++)
		{
			if (!this.PlayerArmyData[this.canUpLevelArmy[i]].isMaxLevel())
			{
				return true;
			}
		}
		return false;
	}

	public int GetEquipByLongID(long Id)
	{
		for (int i = 0; i < this.EquipItem.Count; i++)
		{
			if (Id == this.EquipItem[i].id)
			{
				return this.EquipItem[i].equipID;
			}
		}
		return 0;
	}

	public int GetItemCountById(int id)
	{
		if (this.PlayerItemInfo.ContainsKey(id))
		{
			return this.PlayerItemInfo[id];
		}
		return 0;
	}

	public int GetTechLevel(int id, int type)
	{
		return 1;
	}

	public bool IsArmyFuncingBuilding(long buildingID)
	{
		return this.AllArmyInfo.ContainsKey(buildingID) && this.AllArmyInfo[buildingID].armyFuncing.Count > 0;
	}

	public KVStruct GetArmyFuncingDataByBuilding(long buildingID)
	{
		if (this.AllArmyInfo.ContainsKey(buildingID) && this.AllArmyInfo[buildingID].armyFuncing.Count > 0)
		{
			return this.AllArmyInfo[buildingID].GetArmyNearestDataFuncing();
		}
		return null;
	}
}
