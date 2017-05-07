using DicForUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitConst
{
	public struct resDes
	{
		public int resID;

		public string resName;

		public string resDesciption;
	}

	private static UnitConst instance;

	public NewUnInfo[] soldierConst;

	public NewBuildingInfo[] buildingConst;

	public Dictionary<int, HomeUpdateOpenSetData> HomeUpdateOpenSetDataConst = new Dictionary<int, HomeUpdateOpenSetData>();

	public Dictionary<Vector2, BuildingUpOpenSet> OtherBuildingOpenSetDataConst = new Dictionary<Vector2, BuildingUpOpenSet>();

	public Dictionary<int, UnitConst.resDes> ResDes = new Dictionary<int, UnitConst.resDes>();

	public Dictionary<int, SkillMix> SkillMixConstData = new Dictionary<int, SkillMix>();

	public Dictionary<Vector2, Technology> TechnologyDataConst = new Dictionary<Vector2, Technology>();

	public Dictionary<int, ArmyStar> armyStarConst = new Dictionary<int, ArmyStar>();

	public Dictionary<int, Item> ItemConst = new Dictionary<int, Item>();

	public Dictionary<int, SevenDay> SevenDay = new Dictionary<int, SevenDay>();

	public Dictionary<int, ItemMixSet> ItemMixSetConst = new Dictionary<int, ItemMixSet>();

	public Dictionary<int, int> PlayerExpConst = new Dictionary<int, int>();

	public Dictionary<int, Buff> BuffConst = new Dictionary<int, Buff>();

	public Dictionary<int, WarZone> AllWarZone = new Dictionary<int, WarZone>();

	public Dictionary<int, Battle> BattleConst = new Dictionary<int, Battle>();

	public Dictionary<int, BattleField> BattleFieldConst = new Dictionary<int, BattleField>();

	public Dictionary<int, Npc> AllNpc = new Dictionary<int, Npc>();

	public Dictionary<int, Box> AllBox = new Dictionary<int, Box>();

	public Dictionary<int, DropList> AllDropList = new Dictionary<int, DropList>();

	public Dictionary<int, LoadReward> loadReward = new Dictionary<int, LoadReward>();

	public Dictionary<int, DesignConfig> DesighConfigDic = new Dictionary<int, DesignConfig>();

	public Dictionary<int, GoldPurchase> GoldPurchaseDic = new Dictionary<int, GoldPurchase>();

	public Dictionary<int, VipConst> VipConstData = new Dictionary<int, VipConst>();

	public Dictionary<string, Dictionary<string, string>> languageinfo = new Dictionary<string, Dictionary<string, string>>();

	public Dictionary<int, ArmsDealer> ArmsDealerConst = new Dictionary<int, ArmsDealer>();

	public Dictionary<int, TalkItem> TalkItemConst = new Dictionary<int, TalkItem>();

	public Dictionary<int, ActivityItem> ActivityItemConst = new Dictionary<int, ActivityItem>();

	public Dictionary<int, Electricity> ElectricityCont = new Dictionary<int, Electricity>();

	public Dictionary<int, Sign> signConst = new Dictionary<int, Sign>();

	public Dictionary<int, NewbieGuidePerson> newbieGuidePerson = new Dictionary<int, NewbieGuidePerson>();

	public Dictionary<int, NewbieGuide> newbieGuide = new Dictionary<int, NewbieGuide>();

	public Dictionary<int, HintPanelInfo> hintPanel = new Dictionary<int, HintPanelInfo>();

	public List<MoneyToToken> moneyToToken = new List<MoneyToToken>();

	public Dictionary<int, ShopItem> shopItem = new Dictionary<int, ShopItem>();

	public Dictionary<int, NpcAttark> npcAttartList = new Dictionary<int, NpcAttark>();

	public Dictionary<int, Equip> equipList = new Dictionary<int, Equip>();

	public Dictionary<int, Skill> skillList = new Dictionary<int, Skill>();

	public Dictionary<int, SkillUpdate> skillUpdate = new Dictionary<int, SkillUpdate>();

	public Dictionary<int, ArmyIconClass> armyIcon = new Dictionary<int, ArmyIconClass>();

	public Dictionary<int, ArmyRight> armyRight = new Dictionary<int, ArmyRight>();

	public Dictionary<int, ElliteBattleBox> ElliteBattleBoxList = new Dictionary<int, ElliteBattleBox>();

	public Dictionary<int, MilitaryRankData> MilitaryRankDataList = new Dictionary<int, MilitaryRankData>();

	public Dictionary<int, Soldier> soldierList = new Dictionary<int, Soldier>();

	public Dictionary<Vector2, SoldierUpSet> soldierUpSetConst = new Dictionary<Vector2, SoldierUpSet>();

	public Dictionary<int, SoldierExpSet> soldierExpSetConst = new Dictionary<int, SoldierExpSet>();

	public Dictionary<Vector2, SoldierLevelSet> soldierLevelSetConst = new Dictionary<Vector2, SoldierLevelSet>();

	public Dictionary<int, CommanderTalk> CommanderTalkList = new Dictionary<int, CommanderTalk>();

	public Dictionary<int, string[]> btnUpSet = new Dictionary<int, string[]>();

	public List<CameraData> cameraDataList = new List<CameraData>();

	public List<LoadDes> LoadList = new List<LoadDes>();

	public Dictionary<int, MapEntity> mapEntityList = new Dictionary<int, MapEntity>();

	public Dictionary<int, NpcAttackBattle> npcAttackBattle = new Dictionary<int, NpcAttackBattle>();

	public Dictionary<int, DailyTask> DailyTask = new Dictionary<int, DailyTask>();

	public Dictionary<int, string> IslandTypeToModel = new Dictionary<int, string>();

	public Dictionary<int, Vector3> IslandTypeToModelScale = new Dictionary<int, Vector3>();

	public Dictionary<int, Vector3> IslandTypeToModelAngel = new Dictionary<int, Vector3>();

	public Dictionary<int, SkillUpdate> skillUpdateConst = new Dictionary<int, SkillUpdate>();

	public string[] ReName_Name;

	public string[] ReName_Name1;

	public Dictionary<int, Achievement> AllAchievementConst = new Dictionary<int, Achievement>();

	public Dictionary<int, Aide> AideConst = new Dictionary<int, Aide>();

	public Dictionary<int, AideAbility> AideAbilityConst = new Dictionary<int, AideAbility>();

	public Dictionary<int, DeathToArmy> BuildingDeathToArmy = new Dictionary<int, DeathToArmy>();

	public Dictionary<int, DeathToArmy> ArmyDeathToArmy = new Dictionary<int, DeathToArmy>();

	public Dictionary<int, TowerTankOrder> TowerTankOrderList = new Dictionary<int, TowerTankOrder>();

	public int tankNumber;

	public DailyTask CanRecieveMainlineTask
	{
		get
		{
			DailyTask result;
			try
			{
				result = this.ALLlineTask().SingleOrDefault((DailyTask a) => a.isUIShow && !a.isReceived && a.isCanRecieved && a.isTips);
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}

	public DailyTask CanRecieveXinShouMainlineTask
	{
		get
		{
			DailyTask result;
			try
			{
				result = this.ALLlineTask().SingleOrDefault((DailyTask a) => a.isUIShow && !a.isReceived && a.isCanRecieved && a.mainTaskType == 4);
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}

	private UnitConst()
	{
	}

	public static UnitConst GetInstance()
	{
		if (UnitConst.instance == null)
		{
			UnitConst.instance = new UnitConst();
		}
		return UnitConst.instance;
	}

	public static bool IsHaveInstance()
	{
		return UnitConst.instance != null;
	}

	public static void Clear()
	{
		UnitConst.instance = null;
	}

	public int GetArmyId(int buildingId, int buildingLevel)
	{
		if (!this.OtherBuildingOpenSetDataConst.ContainsKey(new Vector2((float)buildingId, (float)(buildingLevel + 1))))
		{
			return 0;
		}
		List<int> list = new List<int>();
		list.AddRange(this.OtherBuildingOpenSetDataConst[new Vector2((float)buildingId, (float)(buildingLevel + 1))].armsIds);
		for (int i = 0; i < this.OtherBuildingOpenSetDataConst[new Vector2((float)buildingId, (float)buildingLevel)].armsIds.Count; i++)
		{
			list.Remove(this.OtherBuildingOpenSetDataConst[new Vector2((float)buildingId, (float)buildingLevel)].armsIds[i]);
		}
		if (list.Count > 0)
		{
			return list[0];
		}
		return 0;
	}

	public int GetArmyOpenToBuildingLV(int armyId, int buildingID)
	{
		for (int i = 1; i < 100; i++)
		{
			if (this.OtherBuildingOpenSetDataConst[new Vector2((float)buildingID, (float)i)].armsIds.Contains(armyId))
			{
				return i;
			}
		}
		return 0;
	}

	public int GetMaxBuildingNum(int buildingID)
	{
		if (this.HomeUpdateOpenSetDataConst.Values.Any((HomeUpdateOpenSetData a) => a.buildingIds.ContainsKey(buildingID)))
		{
			return this.HomeUpdateOpenSetDataConst[this.HomeUpdateOpenSetDataConst.Keys.Max()].buildingIds[buildingID];
		}
		return 0;
	}

	public int GetNextBuildingNum(int buildingID, int buildingNum)
	{
		foreach (KeyValuePair<int, HomeUpdateOpenSetData> current in this.HomeUpdateOpenSetDataConst)
		{
			if (current.Value.buildingIds.ContainsKey(buildingID) && current.Value.buildingIds[buildingID] > buildingNum)
			{
				return current.Key;
			}
		}
		return 0;
	}

	public int MaxSkillUpSet(int itemCommandID)
	{
		for (int i = 0; i < 100; i++)
		{
			if (!this.skillUpdateConst.ContainsKey(itemCommandID))
			{
				return i - 1;
			}
		}
		return 100;
	}

	public int MaxSkillUpLevel(int itemId)
	{
		for (int i = 0; i < 20; i++)
		{
			if (!this.skillUpdateConst.ContainsKey(itemId))
			{
				return i - 1;
			}
		}
		return 20;
	}

	public int MaxSpecialSoliderStar(int itemCommandoID)
	{
		for (int i = 1; i < 1000; i++)
		{
			if (!this.soldierUpSetConst.ContainsKey(new Vector2((float)itemCommandoID, (float)i)))
			{
				return i - 1;
			}
		}
		return 1000;
	}

	public int MaxSoecialSoliderLevel(int itemCommandoID)
	{
		for (int i = 1; i < 1000; i++)
		{
			if (!this.soldierLevelSetConst.ContainsKey(new Vector2((float)itemCommandoID, (float)i)))
			{
				return i - 1;
			}
		}
		return 1000;
	}

	public List<DailyTask> ALLlineTask()
	{
		List<DailyTask> list = new List<DailyTask>();
		DicForU.GetValues<int, DailyTask>(this.DailyTask, list);
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (!list[i].isUIShow || list[i].type == 1 || list[i].isReceived)
			{
				list.Remove(list[i]);
			}
		}
		return list;
	}

	public List<DailyTask> ALLMainlineTask()
	{
		List<DailyTask> list = new List<DailyTask>();
		DicForU.GetValues<int, DailyTask>(this.DailyTask, list);
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (!list[i].isUIShow || list[i].type == 1 || list[i].type == 2 || list[i].isReceived)
			{
				list.Remove(list[i]);
			}
		}
		return list;
	}

	public List<DailyTask> ALLDailyTask()
	{
		List<DailyTask> list = new List<DailyTask>();
		DicForU.GetValues<int, DailyTask>(this.DailyTask, list);
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (!list[i].isUIShow || list[i].type == 0 || list[i].type == 2 || list[i].isReceived)
			{
				list.Remove(list[i]);
			}
		}
		return list;
	}

	public List<DailyTask> SideQuestTask()
	{
		List<DailyTask> list = (from item in this.DailyTask.Values
		where item.isUIShow && item.type == 2 && !item.isReceived
		select item).ToList<DailyTask>();
		try
		{
			return list;
		}
		catch
		{
			LogManage.Log("出错" + list);
		}
		return null;
	}

	public void GetArmyByArmyDeath(int tankIndex, int armyLV, ref int tankOutIndex, ref int tankNum, ref int tankLV)
	{
		if (!this.ArmyDeathToArmy.ContainsKey(tankIndex))
		{
			return;
		}
		string[] array = this.ArmyDeathToArmy[tankIndex].armyType.Split(new char[]
		{
			','
		});
		string[] array2 = this.ArmyDeathToArmy[tankIndex].armyProbabilaty.Split(new char[]
		{
			','
		});
		int num = UnityEngine.Random.Range(0, 10000);
		for (int i = 0; i < array.Length; i++)
		{
			int num2 = 0;
			for (int j = 0; j <= i; j++)
			{
				num2 += int.Parse(array[j].Split(new char[]
				{
					':'
				})[1]);
			}
			if (num < num2)
			{
				tankOutIndex = int.Parse(array[i].Split(new char[]
				{
					':'
				})[0]);
				break;
			}
		}
		num = UnityEngine.Random.Range(0, 10000);
		for (int k = 0; k < array2.Length; k++)
		{
			int num3 = 0;
			for (int l = 0; l <= k; l++)
			{
				num3 += int.Parse(array2[l].Split(new char[]
				{
					':'
				})[1]);
			}
			if (num < num3)
			{
				tankNum = int.Parse(array2[k].Split(new char[]
				{
					':'
				})[0]);
				break;
			}
		}
		tankLV = armyLV;
		if (tankLV > UnitConst.GetInstance().soldierConst[tankOutIndex].lvInfos.Count - 1)
		{
			tankLV = UnitConst.GetInstance().soldierConst[tankOutIndex].lvInfos.Count - 1;
		}
	}

	public void GetArmyByBuildingDeath(int towerIndex, int towerLV, ref int tankOutIndex, ref int tankNum, ref int tankLV)
	{
		if (towerIndex == 62)
		{
			HUDTextTool.inst.Powerhouse();
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (SenceManager.inst.towers[i].NoElectiony != null && SenceManager.inst.towers[i].type != 99)
				{
					SenceManager.inst.towers[i].NoElectiony = null;
					SenceManager.inst.towers[i].NoElectiony = ResourcePanelManage.inst.OnNoelectricityPow(SenceManager.inst.towers[i].tr, 0);
				}
			}
		}
		if (!this.BuildingDeathToArmy.ContainsKey(towerIndex))
		{
			LogManage.Log("不包含此" + towerIndex + "ID的死亡随机部队");
			return;
		}
		string[] array = this.BuildingDeathToArmy[towerIndex].armyType.Split(new char[]
		{
			','
		});
		string[] array2 = this.BuildingDeathToArmy[towerIndex].armyProbabilaty.Split(new char[]
		{
			','
		});
		int num = UnityEngine.Random.Range(0, 10000);
		for (int j = 0; j < array.Length; j++)
		{
			int num2 = 0;
			for (int k = 0; k <= j; k++)
			{
				num2 += int.Parse(array[k].Split(new char[]
				{
					':'
				})[1]);
			}
			if (num <= num2)
			{
				tankOutIndex = int.Parse(array[j].Split(new char[]
				{
					':'
				})[0]);
				break;
			}
		}
		num = UnityEngine.Random.Range(0, 10000);
		for (int l = 0; l < array2.Length; l++)
		{
			int num3 = 0;
			for (int m = 0; m <= l; m++)
			{
				num3 += int.Parse(array2[m].Split(new char[]
				{
					':'
				})[1]);
			}
			if (num <= num3)
			{
				tankNum = int.Parse(array2[l].Split(new char[]
				{
					':'
				})[0]);
				break;
			}
		}
		tankLV = towerLV;
		if (tankLV <= 0)
		{
			tankLV = 1;
		}
		if (tankLV > UnitConst.GetInstance().soldierConst[tankOutIndex].lvInfos.Count - 1)
		{
			tankLV = UnitConst.GetInstance().soldierConst[tankOutIndex].lvInfos.Count - 1;
		}
	}

	public string GethintText(int type, int dodyid, int bodyPlace, int behavior, int behaviorNum, int rnd)
	{
		string result = null;
		List<string> list = new List<string>();
		list.Clear();
		for (int i = 1; i < UnitConst.GetInstance().hintPanel.Count + 1; i++)
		{
			if (UnitConst.GetInstance().hintPanel[i].rnd != null)
			{
				if (int.Parse(UnitConst.GetInstance().hintPanel[i].rnd) == 0)
				{
					if (UnitConst.GetInstance().hintPanel[i].type == type && UnitConst.GetInstance().hintPanel[i].behavior == behavior)
					{
						if (behaviorNum != 0)
						{
							if (UnitConst.GetInstance().hintPanel[i].behaviorNum == behaviorNum)
							{
								result = UnitConst.GetInstance().hintPanel[i].hintinfo;
							}
						}
						else
						{
							result = UnitConst.GetInstance().hintPanel[i].hintinfo;
						}
					}
				}
				else if (UnitConst.GetInstance().hintPanel[i].behavior == behavior && int.Parse(UnitConst.GetInstance().hintPanel[i].rnd) == 1 && UnitConst.GetInstance().hintPanel[i].type == type)
				{
					if (behaviorNum != 0)
					{
						if (UnitConst.GetInstance().hintPanel[i].behaviorNum == behaviorNum)
						{
							list.Add(UnitConst.GetInstance().hintPanel[i].hintinfo);
						}
					}
					else
					{
						list.Add(UnitConst.GetInstance().hintPanel[i].hintinfo);
					}
				}
			}
		}
		if (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count - 1);
			result = list[index];
		}
		return result;
	}

	public int GetResIdx(int resId)
	{
		for (int i = 0; i < UnitConst.GetInstance().ItemConst.Count; i++)
		{
			if (UnitConst.GetInstance().ItemConst.ContainsKey(i) && resId == UnitConst.GetInstance().ItemConst[i].Id)
			{
				return i;
			}
		}
		return -1;
	}

	public int GetBuildingType(int idx)
	{
		return UnitConst.GetInstance().buildingConst[idx].resType;
	}

	public ResType GetBuildingResType(int idx)
	{
		return UnitConst.GetInstance().buildingConst[idx].outputType;
	}

	public int GetBuildingSecType(int idx)
	{
		return UnitConst.GetInstance().buildingConst[idx].secType;
	}

	public float GetBuildingProNum(int index, int lv, int upGrdLV, SenceManager.ElectricityEnum powerEnum, int restype)
	{
		float num = (float)UnitConst.GetInstance().buildingConst[index].lvInfos[lv].outputNum;
		switch (powerEnum)
		{
		case SenceManager.ElectricityEnum.电力充沛:
			num *= UnitConst.GetInstance().ElectricityCont[1].actualReduce;
			break;
		case SenceManager.ElectricityEnum.电力不足:
			num *= UnitConst.GetInstance().ElectricityCont[2].actualReduce;
			break;
		case SenceManager.ElectricityEnum.严重不足:
			num *= UnitConst.GetInstance().ElectricityCont[3].actualReduce;
			break;
		case SenceManager.ElectricityEnum.电力瘫痪:
			num *= UnitConst.GetInstance().ElectricityCont[4].actualReduce;
			break;
		}
		float num2 = (float)AdjutantPanelData.GetResHomeAddttion(restype, 1);
		if (num2 > 0f)
		{
			num *= 1f + num2 * 0.01f;
		}
		return num;
	}

	public int GetUpPropertyByStrengthLV(int towerID, int strengthLV, int upType)
	{
		if (UnitConst.GetInstance().buildingConst[towerID].StrongInfos.Count > strengthLV && UnitConst.GetInstance().buildingConst[towerID].StrongInfos[strengthLV].attribute.ContainsKey(upType))
		{
			return UnitConst.GetInstance().buildingConst[towerID].StrongInfos[strengthLV].attribute[upType];
		}
		return 0;
	}

	public float GetUpGradeTowerInfo(T_Tower tower, int UpGrade, BuildingGradeShow upType, ResType resType)
	{
		if (UnitConst.GetInstance().buildingConst[tower.index].buildGradeInfos.Count > 0)
		{
			if (upType == BuildingGradeShow.产出速率)
			{
				return (float)UnitConst.GetInstance().buildingConst[tower.index].lvInfos[tower.lv].outputNum * (1f + (float)UnitConst.GetInstance().buildingConst[tower.index].buildGradeInfos[UpGrade].output * 0.01f);
			}
			if (upType != BuildingGradeShow.储量上限)
			{
				return 0f;
			}
			return (float)UnitConst.GetInstance().buildingConst[tower.index].lvInfos[tower.lv].outputLimit[resType] * (1f + (float)UnitConst.GetInstance().buildingConst[tower.index].buildGradeInfos[UpGrade].outlimit * 0.01f);
		}
		else
		{
			if (upType == BuildingGradeShow.产出速率)
			{
				return (float)UnitConst.GetInstance().buildingConst[tower.index].lvInfos[tower.lv].outputNum;
			}
			if (upType != BuildingGradeShow.储量上限)
			{
				return 0f;
			}
			return (float)UnitConst.GetInstance().buildingConst[tower.index].lvInfos[tower.lv].outputLimit[resType];
		}
	}

	public float GetOurPropertyByUpGradeLv(int towerID, int upGradeLv, Specialshow specialType)
	{
		if (UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos.Count > upGradeLv)
		{
			switch (specialType)
			{
			case Specialshow.连击:
				return (float)UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos[upGradeLv].renju;
			case Specialshow.连珠:
				return (float)((int)UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos[upGradeLv].renjuCD);
			case Specialshow.暴击:
				return UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos[upGradeLv].critPer;
			case Specialshow.暴击抵抗:
				return (float)UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos[upGradeLv].resistPer;
			case Specialshow.破甲:
				return (float)UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos[upGradeLv].avoidDef;
			case Specialshow.buff:
				return 0f;
			case Specialshow.溅射:
				return (float)((int)UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos[upGradeLv].hurtRadius);
			case Specialshow.CD:
				return UnitConst.GetInstance().buildingConst[towerID].UpdateStarInfos[upGradeLv].frequency;
			default:
				return 0f;
			}
		}
		else
		{
			switch (specialType)
			{
			case Specialshow.连击:
				return (float)UnitConst.GetInstance().buildingConst[towerID].renju;
			case Specialshow.连珠:
				return (float)((int)UnitConst.GetInstance().buildingConst[towerID].renjuCD);
			case Specialshow.暴击:
				return (float)UnitConst.GetInstance().buildingConst[towerID].lvInfos[towerID].fightInfo.crit;
			case Specialshow.暴击抵抗:
				return (float)UnitConst.GetInstance().buildingConst[towerID].lvInfos[towerID].fightInfo.resist;
			case Specialshow.破甲:
				return (float)UnitConst.GetInstance().buildingConst[towerID].lvInfos[towerID].fightInfo.avoiddef;
			case Specialshow.buff:
			case Specialshow.溅射:
				return (float)((int)UnitConst.GetInstance().buildingConst[towerID].hurtRadius);
			case Specialshow.CD:
				return UnitConst.GetInstance().buildingConst[towerID].frequency;
			default:
				return 0f;
			}
		}
	}
}
