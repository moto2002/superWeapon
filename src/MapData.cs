using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
	public int mapIndex;

	public int isLandItemID;

	public long ID;

	public long ownerID;

	public int terrType;

	public int mapSizeX;

	public int mapSizeZ;

	public Dictionary<int, BuildingNPC> growList = new Dictionary<int, BuildingNPC>();

	public Dictionary<long, BuildingNPC> towerList_Data = new Dictionary<long, BuildingNPC>();

	public int baseResType;

	public Dictionary<long, BuildingNPC> ResourceBuildingList = new Dictionary<long, BuildingNPC>();

	public Dictionary<int, BuildingNPC> resList = new Dictionary<int, BuildingNPC>();

	public List<int> resRemoveList = new List<int>();

	public Dictionary<long, armyInfoInBuilding> mapSoldierInfo = new Dictionary<long, armyInfoInBuilding>();

	public Dictionary<int, ArmyInfo> mapArmyData = new Dictionary<int, ArmyInfo>();

	public List<SCIslandOfficerData> EnemyOfficer = new List<SCIslandOfficerData>();

	public Dictionary<int, int> EnemyTech = new Dictionary<int, int>();

	public Dictionary<ResType, int> baseRes = new Dictionary<ResType, int>();

	public Dictionary<ResType, int> addRes = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemRes = new Dictionary<int, int>();

	public Dictionary<int, int> equips = new Dictionary<int, int>();

	public Dictionary<int, int> ItemList = new Dictionary<int, int>();

	public int map_InitIndex;

	public long map_InitOwerID;

	public Dictionary<long, long> ResRemoveCDTime = new Dictionary<long, long>();

	public Dictionary<long, long> BuildingInCDTime = new Dictionary<long, long>();

	public long armyBuildingCDTime;

	public long airBuildingCDTime;

	public long armyBuildingCDTime_BuildingID;

	public long airBuildingCDTime_BuildingID;

	public long curCoin
	{
		get
		{
			if (SenceInfo.SpyPlayerInfo != null && SenceInfo.SpyPlayerInfo.targetCurRes != null)
			{
				foreach (KVStruct current in SenceInfo.SpyPlayerInfo.targetCurRes)
				{
					if (current.key == 1L)
					{
						return current.value;
					}
				}
			}
			return 0L;
		}
	}

	public long curOil
	{
		get
		{
			if (SenceInfo.SpyPlayerInfo != null && SenceInfo.SpyPlayerInfo.targetCurRes != null)
			{
				foreach (KVStruct current in SenceInfo.SpyPlayerInfo.targetCurRes)
				{
					if (current.key == 2L)
					{
						return current.value;
					}
				}
			}
			return 0L;
		}
	}

	public long curSteel
	{
		get
		{
			if (SenceInfo.SpyPlayerInfo != null && SenceInfo.SpyPlayerInfo.targetCurRes != null)
			{
				foreach (KVStruct current in SenceInfo.SpyPlayerInfo.targetCurRes)
				{
					if (current.key == 3L)
					{
						return current.value;
					}
				}
			}
			return 0L;
		}
	}

	public long curRareEarth
	{
		get
		{
			if (SenceInfo.SpyPlayerInfo != null && SenceInfo.SpyPlayerInfo.targetCurRes != null)
			{
				foreach (KVStruct current in SenceInfo.SpyPlayerInfo.targetCurRes)
				{
					if (current.key == 4L)
					{
						return current.value;
					}
				}
			}
			return 0L;
		}
	}

	public bool IsMyMap
	{
		get
		{
			return this.map_InitOwerID == HeroInfo.GetInstance().userId;
		}
	}

	public bool IsMyHome
	{
		get
		{
			return this.ID == HeroInfo.GetInstance().homeMapID;
		}
	}

	public void ClearData()
	{
		this.growList.Clear();
		this.towerList_Data.Clear();
		this.ResourceBuildingList.Clear();
		this.resList.Clear();
		this.resRemoveList.Clear();
		this.mapSoldierInfo.Clear();
		this.ResRemoveCDTime.Clear();
		this.BuildingInCDTime.Clear();
		this.mapArmyData.Clear();
		this.EnemyOfficer.Clear();
		this.EnemyTech.Clear();
		this.baseRes.Clear();
		this.addRes.Clear();
		this.itemRes.Clear();
		this.equips.Clear();
	}

	public List<int> GetArmysDef()
	{
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, int> current in this.EnemyTech)
		{
			if (UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props.ContainsKey(Technology.Enum_TechnologyProps.兵种支援))
			{
				list.Add(UnitConst.GetInstance().TechnologyDataConst[new Vector2((float)current.Key, (float)current.Value)].Props[Technology.Enum_TechnologyProps.兵种支援]);
			}
		}
		return list;
	}

	public void ReSetArmy_AirBuildingCD()
	{
		this.armyBuildingCDTime = 0L;
		this.airBuildingCDTime = 0L;
		foreach (KeyValuePair<long, long> current in this.BuildingInCDTime)
		{
			if (UnitConst.GetInstance().buildingConst[this.towerList_Data[current.Key].buildingIdx].secType == 6 && current.Value > 0L)
			{
				this.armyBuildingCDTime_BuildingID = current.Key;
				this.armyBuildingCDTime = current.Value;
			}
			if (UnitConst.GetInstance().buildingConst[this.towerList_Data[current.Key].buildingIdx].secType == 21 && current.Value > 0L)
			{
				this.airBuildingCDTime_BuildingID = current.Key;
				this.airBuildingCDTime = current.Value;
			}
		}
	}
}
