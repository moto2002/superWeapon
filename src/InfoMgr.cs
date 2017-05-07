using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InfoMgr
{
	public static MapData GetMapData(SCIslandData s_data)
	{
		MapData mapData = SenceInfo.curMap;
		mapData.ClearData();
		mapData.mapIndex = s_data.index;
		mapData.terrType = s_data.terrainTypeId;
		mapData.baseResType = s_data.baseResId;
		mapData.ID = s_data.id;
		mapData.isLandItemID = s_data.islandId;
		mapData.mapSizeX = SenceInfo.terrainTypeList[mapData.terrType].sizeX;
		mapData = InfoMgr.GetFinishbuildList(s_data.playerBuildings, mapData);
		mapData = InfoMgr.GetFinalResList(s_data.cutTrees, mapData);
		InfoMgr.GetGrowbuildList(mapData, s_data.growableItem);
		mapData.EnemyOfficer.Clear();
		mapData.EnemyOfficer.AddRange(s_data.officers);
		mapData.ownerID = s_data.ownerId;
		mapData.BuildingInCDTime.Clear();
		foreach (KVStruct current in s_data.CDEndTime)
		{
			mapData.BuildingInCDTime.Add(current.key, current.value);
		}
		mapData.ReSetArmy_AirBuildingCD();
		mapData.ResRemoveCDTime.Clear();
		foreach (KVStruct current2 in s_data.removeCdTime)
		{
			mapData.ResRemoveCDTime.Add(current2.key, current2.value);
		}
		if (mapData.IsMyHome)
		{
			HeroInfo.GetInstance().BuildingCDEndTime.Clear();
			foreach (KVStruct current3 in s_data.CDEndTime)
			{
				HeroInfo.GetInstance().BuildingCDEndTime.Add(current3.key, current3.value);
			}
			HeroInfo.GetInstance().ReSetArmy_AirBuildingCD();
			foreach (KVStruct current4 in s_data.removeCdTime)
			{
				HeroInfo.GetInstance().BuildingCDEndTime.Add(current4.key, current4.value);
			}
		}
		mapData.ItemList.Clear();
		foreach (KVStruct current5 in s_data.dropItems)
		{
			mapData.ItemList.Add((int)current5.key, (int)current5.value);
		}
		mapData.EnemyTech.Clear();
		foreach (KVStruct current6 in s_data.tech)
		{
			mapData.EnemyTech.Add((int)current6.key, (int)current6.value);
		}
		mapData.baseRes.Clear();
		foreach (KVStruct current7 in s_data.baseRes)
		{
			mapData.baseRes.Add((ResType)current7.key, (int)current7.value);
		}
		mapData.addRes.Clear();
		mapData.itemRes.Clear();
		if (UnitConst.GetInstance().AllDropList.ContainsKey(s_data.dropListId))
		{
			foreach (int current8 in UnitConst.GetInstance().AllDropList[s_data.dropListId].boxRate.Keys)
			{
				foreach (KeyValuePair<int, int> current9 in UnitConst.GetInstance().AllBox[current8].items)
				{
					if (!mapData.itemRes.ContainsKey(current9.Key))
					{
						mapData.itemRes.Add(current9.Key, 0);
					}
					Dictionary<int, int> itemRes;
					Dictionary<int, int> expr_415 = itemRes = mapData.itemRes;
					int num;
					int expr_41F = num = current9.Key;
					num = itemRes[num];
					expr_415[expr_41F] = num + current9.Value;
				}
			}
			foreach (int current10 in UnitConst.GetInstance().AllDropList[s_data.dropListId].boxRate.Keys)
			{
				foreach (KeyValuePair<int, int> current11 in UnitConst.GetInstance().AllBox[current10].equips)
				{
					if (!mapData.equips.ContainsKey(current11.Key))
					{
						mapData.equips.Add(current11.Key, 0);
					}
					Dictionary<int, int> equips;
					Dictionary<int, int> expr_507 = equips = mapData.equips;
					int num;
					int expr_511 = num = current11.Key;
					num = equips[num];
					expr_507[expr_511] = num + current11.Value;
				}
			}
		}
		mapData.EnemyOfficer.Clear();
		mapData.EnemyOfficer.AddRange(s_data.officers);
		foreach (SCArmyData current12 in s_data.targetArmy)
		{
			if (!mapData.mapArmyData.ContainsKey((int)current12.id))
			{
				mapData.mapArmyData.Add((int)current12.id, new ArmyInfo((int)current12.id));
			}
			mapData.mapArmyData[(int)current12.id].level = current12.level;
			mapData.mapArmyData[(int)current12.id].starLevel = current12.starLevel;
			if (mapData.IsMyHome)
			{
				if (!HeroInfo.GetInstance().PlayerArmyData.ContainsKey((int)current12.id))
				{
					HeroInfo.GetInstance().PlayerArmyData.Add((int)current12.id, new ArmyInfo((int)current12.id));
				}
				HeroInfo.GetInstance().PlayerArmyData[(int)current12.id].level = current12.level;
				HeroInfo.GetInstance().PlayerArmyData[(int)current12.id].starLevel = current12.starLevel;
			}
		}
		foreach (SCConfigureArmyData current13 in s_data.targetConfArmys)
		{
			if (!mapData.mapSoldierInfo.ContainsKey(current13.id))
			{
				mapData.mapSoldierInfo.Add(current13.id, new armyInfoInBuilding());
			}
			mapData.mapSoldierInfo[current13.id].buildingID = current13.id;
			mapData.mapSoldierInfo[current13.id].armyFuncing.Clear();
			mapData.mapSoldierInfo[current13.id].armyFuncing.AddRange(current13.cdTime2ArmyId);
			mapData.mapSoldierInfo[current13.id].armyFunced.Clear();
			mapData.mapSoldierInfo[current13.id].armyFunced.AddRange(current13.armyId2Num);
			mapData.mapSoldierInfo[current13.id].armyFuncedToUI.Clear();
			mapData.mapSoldierInfo[current13.id].armyFuncedToUI.AddRange(current13.endArmyId2Num);
			if (mapData.IsMyHome)
			{
				if (!HeroInfo.GetInstance().AllArmyInfo.ContainsKey(current13.id))
				{
					HeroInfo.GetInstance().AllArmyInfo.Add(current13.id, new armyInfoInBuilding());
				}
				HeroInfo.GetInstance().AllArmyInfo[current13.id].buildingID = current13.id;
				HeroInfo.GetInstance().AllArmyInfo[current13.id].armyFuncing.Clear();
				HeroInfo.GetInstance().AllArmyInfo[current13.id].armyFuncing.AddRange(current13.cdTime2ArmyId);
				HeroInfo.GetInstance().AllArmyInfo[current13.id].armyFunced.Clear();
				HeroInfo.GetInstance().AllArmyInfo[current13.id].armyFunced.AddRange(current13.armyId2Num);
				HeroInfo.GetInstance().AllArmyInfo[current13.id].armyFuncedToUI.Clear();
				HeroInfo.GetInstance().AllArmyInfo[current13.id].armyFuncedToUI.AddRange(current13.endArmyId2Num);
			}
		}
		return mapData;
	}

	public static void GetGrowbuildList(MapData data, List<SCGrowableItemData> growList)
	{
		data.growList.Clear();
		for (int i = 0; i < growList.Count; i++)
		{
			BuildingNPC buildingNPC = new BuildingNPC();
			buildingNPC.posIdx = (int)growList[i].id;
			buildingNPC.buildingIdx = growList[i].itemId;
			Vector3 vector = InfoMgr.IdxGetMapPos(buildingNPC.posIdx, data.mapSizeX);
			buildingNPC.row = Mathf.RoundToInt(vector.x);
			buildingNPC.num = Mathf.RoundToInt(vector.z);
			data.growList.Add(buildingNPC.posIdx, buildingNPC);
		}
	}

	public static MapData GetFinishbuildList(List<SCPlayerBuilding> fromList, MapData data)
	{
		for (int i = 0; i < fromList.Count; i++)
		{
			InfoMgr.GetFinishbuildList(data, new SCPlayerBuilding[]
			{
				fromList[i]
			});
		}
		return data;
	}

	public static MapData GetFinishbuildList(MapData data, params SCPlayerBuilding[] fromList)
	{
		for (int i = 0; i < fromList.Length; i++)
		{
			BuildingNPC buildingNPC;
			if (data.towerList_Data.ContainsKey(fromList[i].id))
			{
				buildingNPC = data.towerList_Data[fromList[i].id];
			}
			else
			{
				buildingNPC = new BuildingNPC();
				data.towerList_Data.Add(fromList[i].id, buildingNPC);
			}
			buildingNPC.buildingId = fromList[i].id;
			buildingNPC.buildingIdx = fromList[i].itemId;
			buildingNPC.strength = fromList[i].strengthLevel;
			buildingNPC.posIdx = fromList[i].index;
			buildingNPC.star = fromList[i].upGradeLevel;
			Vector3 vector = InfoMgr.IdxGetMapPos(fromList[i].index, data.mapSizeX);
			buildingNPC.row = Mathf.RoundToInt(vector.x);
			buildingNPC.num = Mathf.RoundToInt(vector.z);
			buildingNPC.lv = fromList[i].level;
			if (buildingNPC.lv == 0)
			{
				LogManage.Log("服务器返回建筑等级为0");
			}
			if (UnitConst.GetInstance().buildingConst.Length <= buildingNPC.buildingIdx || buildingNPC.buildingIdx < 0 || UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].dropType.Length == 0)
			{
				LogManage.Log(buildingNPC.buildingIdx + "::");
			}
			buildingNPC.productType = (BuildingProductType)UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].dropType[0];
			buildingNPC.protductNum = fromList[i].protductNum;
			buildingNPC.takeTime = TimeTools.ConvertLongDateTime(fromList[i].takeTime);
			if (UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].resType == 4)
			{
				if (data.resList.ContainsKey(buildingNPC.posIdx))
				{
					data.resList[buildingNPC.posIdx] = buildingNPC;
				}
				else
				{
					data.resList.Add(buildingNPC.posIdx, buildingNPC);
				}
			}
			else if (UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].secType == 3 && (buildingNPC.productType == BuildingProductType.coin || buildingNPC.productType == BuildingProductType.rareEarth || buildingNPC.productType == BuildingProductType.oil || buildingNPC.productType == BuildingProductType.steel))
			{
				if (data.ResourceBuildingList.ContainsKey(buildingNPC.buildingId))
				{
					data.ResourceBuildingList[buildingNPC.buildingId] = buildingNPC;
				}
				else
				{
					data.ResourceBuildingList.Add(buildingNPC.buildingId, buildingNPC);
				}
			}
			if (data.mapIndex == HeroInfo.GetInstance().homeInWMapIdx)
			{
				if (!HeroInfo.GetInstance().BuildCD.Contains(buildingNPC.buildingId))
				{
					if (HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(buildingNPC.buildingIdx))
					{
						HeroInfo.GetInstance().PlayerBuildingLevel[buildingNPC.buildingIdx] = Math.Max(HeroInfo.GetInstance().PlayerBuildingLevel[buildingNPC.buildingIdx], buildingNPC.lv);
					}
					else
					{
						HeroInfo.GetInstance().PlayerBuildingLevel.Add(buildingNPC.buildingIdx, buildingNPC.lv);
					}
				}
			}
		}
		return data;
	}

	public static MapData GetFinalResList(List<int> fromList, MapData data)
	{
		data.resRemoveList.Clear();
		data.resRemoveList.AddRange(fromList);
		for (int i = 0; i < fromList.Count; i++)
		{
			data.resList.Remove(fromList[i]);
		}
		return data;
	}

	public static Vector3 IdxGetMapPos(int idx, int sizeX)
	{
		Vector3 result = new Vector3(0f, 0f, 0f);
		result.x = (float)Mathf.RoundToInt((float)(idx / sizeX));
		result.z = (float)idx - (float)sizeX * result.x;
		return result;
	}

	public static int PosGetMapIdx(int row, int num)
	{
		return row * SenceInfo.terrainTypeList[SenceInfo.curMap.terrType].sizeX + num;
	}

	public static int GetEnemyTowerDamageBySecond(int index, int lv)
	{
		return 1;
	}

	public static SoldierData GetSoldierData(int index, int star, int lv)
	{
		SoldierData soldierData = new SoldierData();
		soldierData.soldier = UnitConst.GetInstance().soldierList[index];
		foreach (KeyValuePair<int, SoldierExpSet> current in UnitConst.GetInstance().soldierExpSetConst)
		{
			if (current.Value.id == lv)
			{
				soldierData.soldierExpSet = current.Value;
				break;
			}
		}
		int num = 0;
		foreach (KeyValuePair<Vector2, SoldierUpSet> current2 in UnitConst.GetInstance().soldierUpSetConst)
		{
			if (current2.Value.itemId == index && current2.Value.level == star)
			{
				soldierData.soldierUpSet = current2.Value;
				num = current2.Value.id;
				break;
			}
		}
		foreach (KeyValuePair<Vector2, SoldierLevelSet> current3 in UnitConst.GetInstance().soldierLevelSetConst)
		{
			if (current3.Value.itemId == num && current3.Value.level == lv)
			{
				soldierData.soldierLevelSet = current3.Value;
				break;
			}
		}
		return soldierData;
	}

	public static BaseFightInfo GetTowerFightData(int index, int lv, int strengthLevel, int upGrade, int vipLv, Dictionary<int, int> teches)
	{
		BaseFightInfo result = default(BaseFightInfo);
		result.lv = lv;
		if (lv >= UnitConst.GetInstance().buildingConst[index].lvInfos.Count)
		{
			return result;
		}
		result.life = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.life;
		result.life += (int)((float)(result.life * UnitConst.GetInstance().GetUpPropertyByStrengthLV(index, strengthLevel, 1)) * 0.01f);
		if (UnitConst.GetInstance().buildingConst[index].secType == 8)
		{
			result.life += Technology.GetTechAddtion(result.life, teches, Technology.Enum_TechnologyProps.防御塔生命值);
		}
		if (UnitConst.GetInstance().buildingConst[index].secType == 3 || UnitConst.GetInstance().buildingConst[index].secType == 2)
		{
			result.life += Technology.GetTechAddtion(result.life, teches, Technology.Enum_TechnologyProps.资源建筑生命值);
		}
		if (UnitConst.GetInstance().buildingConst[index].secType == 20)
		{
			result.life += Technology.GetTechAddtion(result.life, teches, Technology.Enum_TechnologyProps.围墙生命值);
		}
		result.life += VipConst.GetVipAddtion((float)result.life, vipLv, VipConst.Enum_VipRight.建筑生命值);
		result.breakArmor = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.breakArmor;
		result.breakArmor += (int)((float)(result.breakArmor * UnitConst.GetInstance().GetUpPropertyByStrengthLV(index, strengthLevel, 2)) * 0.01f);
		if (index == 20)
		{
			result.breakArmor += Technology.GetTechAddtion(result.breakArmor, teches, Technology.Enum_TechnologyProps.地雷攻击);
		}
		if (UnitConst.GetInstance().buildingConst[index].secType == 8)
		{
			result.breakArmor += Technology.GetTechAddtion(result.breakArmor, teches, Technology.Enum_TechnologyProps.防御塔攻击);
		}
		if (index == 21)
		{
			result.breakArmor += Technology.GetTechAddtion(result.breakArmor, teches, Technology.Enum_TechnologyProps.高爆雷攻击);
		}
		result.breakArmor += VipConst.GetVipAddtion((float)result.breakArmor, vipLv, VipConst.Enum_VipRight.建筑攻击力);
		result.defBreak = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.defBreak;
		result.defBreak += (int)((float)(result.defBreak * UnitConst.GetInstance().GetUpPropertyByStrengthLV(index, strengthLevel, 3)) * 0.01f);
		if (UnitConst.GetInstance().buildingConst[index].secType == 8)
		{
			result.defBreak += Technology.GetTechAddtion(result.defBreak, teches, Technology.Enum_TechnologyProps.防御塔防御力);
		}
		if (upGrade > 0 && UnitConst.GetInstance().buildingConst[index].secType == 8)
		{
			result.shootCD = UnitConst.GetInstance().buildingConst[index].UpdateStarInfos[upGrade].renjuCD;
			result.shootNum = UnitConst.GetInstance().buildingConst[index].UpdateStarInfos[upGrade].renju;
			result.magazineCD = UnitConst.GetInstance().buildingConst[index].UpdateStarInfos[upGrade].frequency;
		}
		else
		{
			result.shootCD = UnitConst.GetInstance().buildingConst[index].renjuCD;
			result.shootNum = UnitConst.GetInstance().buildingConst[index].renju;
			result.magazineCD = UnitConst.GetInstance().buildingConst[index].frequency;
		}
		result.hurtRadius = UnitConst.GetInstance().buildingConst[index].hurtRadius;
		result.shootSpeed = (float)UnitConst.GetInstance().buildingConst[index].bulletSpeed;
		result.hitArmor = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.hitArmor;
		result.ShootMaxRadius = UnitConst.GetInstance().buildingConst[index].maxRadius;
		result.ShootMinRadius = UnitConst.GetInstance().buildingConst[index].minRadius;
		result.crit = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.crit;
		if (UnitConst.GetInstance().buildingConst[index].secType == 8)
		{
			result.crit += Technology.GetTechAddtion(result.crit, teches, Technology.Enum_TechnologyProps.防御塔暴击率);
		}
		result.critHR = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.critHR;
		result.trueDamage = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.trueDamage;
		if (UnitConst.GetInstance().buildingConst[index].secType == 8)
		{
			result.trueDamage += Technology.GetTechAddtion(result.trueDamage, teches, Technology.Enum_TechnologyProps.防御塔额外伤害);
		}
		result.trueChancePer = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.trueChancePer;
		result.avoiddef = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.avoiddef;
		result.defHit = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.defHit;
		result.reChancePer = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.reChancePer;
		result.reDamage = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.reDamage;
		result.resist = UnitConst.GetInstance().buildingConst[index].lvInfos[lv].fightInfo.resist;
		if (UnitConst.GetInstance().buildingConst[index].secType == 8)
		{
			result.resist += Technology.GetTechAddtion(result.resist, teches, Technology.Enum_TechnologyProps.防御塔暴抗);
		}
		return result;
	}

	public static BaseFightInfo GetTankFightData(int index, int lv, int vipLv, Dictionary<int, int> teches)
	{
		BaseFightInfo result = default(BaseFightInfo);
		result.lv = lv;
		result.life = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.life;
		if (!UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			result.life += Technology.GetTechAddtion(result.life, teches, Technology.Enum_TechnologyProps.坦克生命值);
		}
		else
		{
			result.life += Technology.GetTechAddtion(result.life, teches, Technology.Enum_TechnologyProps.飞机生命值);
		}
		result.life += VipConst.GetVipAddtion((float)result.life, vipLv, VipConst.Enum_VipRight.兵种生命值);
		result.breakArmor = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.breakArmor;
		if (!UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			result.breakArmor += Technology.GetTechAddtion(result.breakArmor, teches, Technology.Enum_TechnologyProps.坦克攻击);
		}
		else
		{
			result.breakArmor += Technology.GetTechAddtion(result.breakArmor, teches, Technology.Enum_TechnologyProps.飞机攻击);
		}
		result.breakArmor += VipConst.GetVipAddtion((float)result.breakArmor, vipLv, VipConst.Enum_VipRight.兵种攻击力);
		result.defBreak = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.defBreak;
		if (!UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			result.defBreak += Technology.GetTechAddtion(result.defBreak, teches, Technology.Enum_TechnologyProps.坦克防御力);
		}
		else
		{
			result.defBreak += Technology.GetTechAddtion(result.defBreak, teches, Technology.Enum_TechnologyProps.飞机防御力);
		}
		result.shootCD = UnitConst.GetInstance().soldierConst[index].renjuCD;
		result.shootNum = UnitConst.GetInstance().soldierConst[index].renju;
		result.magazineCD = UnitConst.GetInstance().soldierConst[index].frequency;
		result.moveSpeed = UnitConst.GetInstance().soldierConst[index].speed;
		if (!UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			result.moveSpeed += Technology.GetTechAddtion(result.moveSpeed, teches, Technology.Enum_TechnologyProps.坦克移动速度);
		}
		else
		{
			result.moveSpeed += Technology.GetTechAddtion(result.moveSpeed, teches, Technology.Enum_TechnologyProps.飞机移动速度);
		}
		result.rotaSpeed = UnitConst.GetInstance().soldierConst[index].roatSpeed;
		result.ShootMaxRadius = UnitConst.GetInstance().soldierConst[index].maxRadius;
		result.ShootMinRadius = UnitConst.GetInstance().soldierConst[index].minRadius;
		result.hurtRadius = UnitConst.GetInstance().soldierConst[index].hurtRadius;
		result.shootSpeed = (float)UnitConst.GetInstance().soldierConst[index].bulletSpeed;
		result.EyeMaxRadius = ((UnitConst.GetInstance().soldierConst[index].eyeRadius <= 0f) ? (UnitConst.GetInstance().soldierConst[index].maxRadius * 1.5f) : UnitConst.GetInstance().soldierConst[index].eyeRadius);
		result.EyeMaxRadius += Technology.GetTechAddtion(result.EyeMaxRadius, teches, Technology.Enum_TechnologyProps.兵种防御视野);
		result.hitArmor = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.hitArmor;
		result.crit = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.crit;
		if (!UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			result.crit += Technology.GetTechAddtion(result.crit, teches, Technology.Enum_TechnologyProps.坦克暴击率);
		}
		else
		{
			result.crit += Technology.GetTechAddtion(result.crit, teches, Technology.Enum_TechnologyProps.飞机暴击率);
		}
		result.critHR = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.critHR;
		result.trueDamage = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.trueDamage;
		if (!UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			result.trueDamage += Technology.GetTechAddtion(result.trueDamage, teches, Technology.Enum_TechnologyProps.坦克额外伤害);
		}
		else
		{
			result.trueDamage += Technology.GetTechAddtion(result.trueDamage, teches, Technology.Enum_TechnologyProps.飞机额外伤害);
		}
		result.trueChancePer = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.trueChancePer;
		result.avoiddef = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.avoiddef;
		result.defHit = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.defHit;
		result.reChancePer = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.reChancePer;
		result.reDamage = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.reDamage;
		result.resist = UnitConst.GetInstance().soldierConst[index].lvInfos[lv].fightInfo.resist;
		if (!UnitConst.GetInstance().soldierConst[index].isCanFly)
		{
			result.resist += Technology.GetTechAddtion(result.resist, teches, Technology.Enum_TechnologyProps.坦克暴抗);
		}
		else
		{
			result.resist += Technology.GetTechAddtion(result.resist, teches, Technology.Enum_TechnologyProps.飞机暴抗);
		}
		return result;
	}

	public static BaseFightInfo GetSoliderFightData(int index, int lv, int star, int vipLv, Dictionary<int, int> teches)
	{
		BaseFightInfo result = default(BaseFightInfo);
		result.lv = lv;
		result.life = UnitConst.GetInstance().soldierLevelSetConst[new Vector2((float)index, (float)lv)].life;
		if (UnitConst.GetInstance().soldierUpSetConst.ContainsKey(new Vector2((float)index, (float)star)) && UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)index, (float)star)].armyAffixes.ContainsKey(SpecialPro.生命值))
		{
			result.life += (int)((float)(result.life * UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)index, (float)star)].armyAffixes[SpecialPro.生命值]) * 0.01f);
		}
		result.life += Technology.GetTechAddtion(result.life, teches, Technology.Enum_TechnologyProps.特种兵生命值);
		result.life += VipConst.GetVipAddtion((float)result.life, vipLv, VipConst.Enum_VipRight.兵种生命值);
		result.breakArmor = UnitConst.GetInstance().soldierLevelSetConst[new Vector2((float)index, (float)lv)].breakArmor;
		if (UnitConst.GetInstance().soldierUpSetConst.ContainsKey(new Vector2((float)index, (float)star)) && UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)index, (float)star)].armyAffixes.ContainsKey(SpecialPro.攻击1))
		{
			result.breakArmor += (int)((float)(result.breakArmor * UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)index, (float)star)].armyAffixes[SpecialPro.攻击1]) * 0.01f);
		}
		result.breakArmor += Technology.GetTechAddtion(result.breakArmor, teches, Technology.Enum_TechnologyProps.特种兵攻击);
		result.breakArmor += VipConst.GetVipAddtion((float)result.breakArmor, vipLv, VipConst.Enum_VipRight.兵种攻击力);
		result.defBreak = UnitConst.GetInstance().soldierLevelSetConst[new Vector2((float)index, (float)lv)].defBreak;
		if (UnitConst.GetInstance().soldierUpSetConst.ContainsKey(new Vector2((float)index, (float)star)) && UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)index, (float)star)].armyAffixes.ContainsKey(SpecialPro.防御1))
		{
			result.defBreak += (int)((float)(result.defBreak * UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)index, (float)star)].armyAffixes[SpecialPro.防御1]) * 0.01f);
		}
		result.defBreak += Technology.GetTechAddtion(result.defBreak, teches, Technology.Enum_TechnologyProps.特种兵防御力);
		result.moveSpeed = UnitConst.GetInstance().soldierList[index].speed;
		result.moveSpeed += Technology.GetTechAddtion(result.moveSpeed, teches, Technology.Enum_TechnologyProps.特种兵移动速度);
		result.rotaSpeed = UnitConst.GetInstance().soldierList[index].speed;
		result.ShootMaxRadius = (float)UnitConst.GetInstance().soldierList[index].maxRadius;
		result.ShootMinRadius = (float)UnitConst.GetInstance().soldierList[index].minRadius;
		result.EyeMaxRadius = UnitConst.GetInstance().soldierConst[index].maxRadius * 1.5f;
		result.shootCD = UnitConst.GetInstance().soldierList[index].Cd;
		result.shootNum = UnitConst.GetInstance().soldierList[index].renju;
		result.magazineCD = UnitConst.GetInstance().soldierList[index].renjuCd;
		result.hitArmor = UnitConst.GetInstance().soldierLevelSetConst[new Vector2((float)index, (float)lv)].hitArmor;
		result.crit += Technology.GetTechAddtion(result.crit, teches, Technology.Enum_TechnologyProps.特种兵暴击率);
		result.trueDamage += Technology.GetTechAddtion(result.trueDamage, teches, Technology.Enum_TechnologyProps.特种兵额外伤害);
		result.resist += Technology.GetTechAddtion(result.resist, teches, Technology.Enum_TechnologyProps.特种兵暴抗);
		return result;
	}
}
