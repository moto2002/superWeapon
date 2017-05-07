using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildingHandler
{
	public static long towerId;

	private static T_Tower AddBuldingNew;

	private static int _row;

	private static int _num;

	public static event Action<int> HomeBuildingUp
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			BuildingHandler.HomeBuildingUp = (Action<int>)Delegate.Combine(BuildingHandler.HomeBuildingUp, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			BuildingHandler.HomeBuildingUp = (Action<int>)Delegate.Remove(BuildingHandler.HomeBuildingUp, value);
		}
	}

	public static event Action<int> RadarBuildingUp
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			BuildingHandler.RadarBuildingUp = (Action<int>)Delegate.Combine(BuildingHandler.RadarBuildingUp, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			BuildingHandler.RadarBuildingUp = (Action<int>)Delegate.Remove(BuildingHandler.RadarBuildingUp, value);
		}
	}

	public static event Action<HomeUpdateOpenSetData> UnloadBuilding
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			BuildingHandler.UnloadBuilding = (Action<HomeUpdateOpenSetData>)Delegate.Combine(BuildingHandler.UnloadBuilding, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			BuildingHandler.UnloadBuilding = (Action<HomeUpdateOpenSetData>)Delegate.Remove(BuildingHandler.UnloadBuilding, value);
		}
	}

	public static event Action<List<int>> UnloadArmys
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			BuildingHandler.UnloadArmys = (Action<List<int>>)Delegate.Combine(BuildingHandler.UnloadArmys, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			BuildingHandler.UnloadArmys = (Action<List<int>>)Delegate.Remove(BuildingHandler.UnloadArmys, value);
		}
	}

	public static void CG_BuildingUpdateStart(long buildingId, int money)
	{
		CSBuildingUp cSBuildingUp = new CSBuildingUp();
		cSBuildingUp.buildingId = buildingId;
		cSBuildingUp.itemId = SenceInfo.curMap.towerList_Data[buildingId].buildingIdx;
		cSBuildingUp.money = money;
		LogManage.Log("建筑升级 花费钻石" + money);
		ClientMgr.GetNet().SendHttp(2002, cSBuildingUp, new DataHandler.OpcodeHandler(BuildingHandler.GC_NewBuildingUpdateEnd), null);
	}

	public static void GC_NewBuildingUpdateEnd(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			BuildingHandler.RemoveRes(opcode);
			BuildingHandler.BuildingAddEnd(opcode);
			FuncUIManager.inst.MainUIPanelManage.RefreshBuildingStoreNotice();
			HUDTextTool.inst.StartCoroutine(HUDTextTool.inst.NextLuaCallByIsEnemyAttck_IE("建筑升级完成 调用· ·", new object[0]));
		}
	}

	public static void GC_NewBuildingAddEnd(bool isError, Opcode opcode)
	{
		if (BuildingHandler.AddBuldingNew != null)
		{
			UnityEngine.Object.Destroy(BuildingHandler.AddBuldingNew.ga);
		}
		if (!isError)
		{
			BuildingHandler.RemoveRes(opcode);
			BuildingHandler.BuildingAddEnd(opcode);
			FuncUIManager.inst.MainUIPanelManage.RefreshBuildingStoreNotice();
			HUDTextTool.inst.StartCoroutine(HUDTextTool.inst.NextLuaCallByIsEnemyAttck_IE("建筑建造完成 调用· ·", new object[0]));
		}
	}

	private static void BuildingAddEnd(Opcode opcode)
	{
		List<SCPlayerBuilding> list = opcode.Get<SCPlayerBuilding>(10006);
		for (int i = 0; i < list.Count; i++)
		{
			BuildingNPC buildNpc = SenceInfo.curMap.towerList_Data[list[i].id];
			foreach (KeyValuePair<long, LegionHelpApply> current in HeroInfo.GetInstance().legionApply)
			{
				if (BuildingHandler.towerId == current.Value.buildingId && MessagePanel._Inst)
				{
					MessagePanel._Inst.ShowApplyInfo();
				}
			}
			bool flag = true;
			for (int j = 0; j < SenceManager.inst.towers.Count; j++)
			{
				if (SenceManager.inst.towers[j].id == buildNpc.buildingId)
				{
					SenceManager.inst.towers[j].lv = buildNpc.lv;
					flag = false;
					if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[j].index].secType == 20 || UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[j].index].secType == 9)
					{
						BuildingHandler.UpdateTowerEnd(SenceManager.inst.towers[j]);
						SenceManager.inst.towers[j].ReLoadModelAndFightState();
					}
				}
			}
			if (flag)
			{
				T_Tower t_Tower = SenceManager.inst.AddNewTower(buildNpc);
				BuildingHandler.TongBuZhanCheGongChang(t_Tower.id, t_Tower.index);
				AudioManage.inst.PlayAuidoBySelf_3D("quedingjianzao", t_Tower.ga, false, 0uL);
				T_CommandPanelManage._instance.OpenMainPanel();
				if (t_Tower.buildingState != T_Tower.TowerBuildingEnum.InBuilding)
				{
					BuildingHandler.UpdateTowerEnd(t_Tower);
				}
				bool flag2 = false;
				if (UnitConst.GetInstance().buildingConst[buildNpc.buildingIdx].secType == 9 || UnitConst.GetInstance().buildingConst[buildNpc.buildingIdx].secType == 20)
				{
					int num = SenceInfo.curMap.towerList_Data.Values.Count((BuildingNPC a) => a.buildingIdx == buildNpc.buildingIdx);
					if (HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(buildNpc.buildingIdx) && num < HeroInfo.GetInstance().PlayerBuildingLimit[buildNpc.buildingIdx])
					{
						flag2 = true;
					}
				}
				if (flag2 && (UnitConst.GetInstance().buildingConst[buildNpc.buildingIdx].secType == 9 || UnitConst.GetInstance().buildingConst[buildNpc.buildingIdx].secType == 20))
				{
					BuildingHandler.NoCDBuilding(buildNpc.buildingIdx);
					return;
				}
				if (CameraControl.inst)
				{
					CameraControl.inst.ChangeCameraBuildingState(false);
				}
			}
		}
		List<SCCDEndTime> list2 = opcode.Get<SCCDEndTime>(10007);
		for (int k = 0; k < list2.Count; k++)
		{
			if (list2[k].type == 1)
			{
				for (int l = 0; l < SenceManager.inst.towers.Count; l++)
				{
					if (SenceManager.inst.towers[l].id == list2[k].id)
					{
						SenceManager.inst.towers[l].lv = list2[k].level;
						if (list2[k].CDEndTime > 0L && SenceManager.inst.towers[l].buildingState != T_Tower.TowerBuildingEnum.InBuilding)
						{
							SenceManager.inst.towers[l].InitBuildingOrEndState();
							BuildingHandler.TongBuZhanCheGongChang(SenceManager.inst.towers[l].id, SenceManager.inst.towers[l].index);
						}
						else if (list2[k].CDEndTime == 0L && SenceManager.inst.towers[l].buildingState != T_Tower.TowerBuildingEnum.Normal)
						{
							SenceManager.inst.towers[l].InitBuildingOrEndState();
							BuildingHandler.UpdateTowerEnd(SenceManager.inst.towers[l]);
							BuildingHandler.TongBuZhanCheGongChang(SenceManager.inst.towers[l].id, SenceManager.inst.towers[l].index);
						}
					}
				}
			}
		}
	}

	private static void TongBuZhanCheGongChang(long id, int index)
	{
		if (UnitConst.GetInstance().buildingConst[index].secType == 6 || UnitConst.GetInstance().buildingConst[index].secType == 21)
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (SenceManager.inst.towers[i].id != id && SenceManager.inst.towers[i].index == index)
				{
					SenceManager.inst.towers[i].InitBuildingOrEndState();
					if (SenceManager.inst.towers[i].BuilindingCDInfo)
					{
						UnityEngine.Object.Destroy(SenceManager.inst.towers[i].BuilindingCDInfo.ga);
					}
					if (SenceManager.inst.towers[i].ArmyTitleNew)
					{
						UnityEngine.Object.Destroy(SenceManager.inst.towers[i].ArmyTitleNew.ga);
					}
				}
			}
		}
	}

	public static void UpdateTowerEnd(T_Tower tower)
	{
		if (TaskByNewBieManager._inst)
		{
			TaskByNewBieManager._inst.SomeBuildingEnd(tower);
		}
		if (tower.lv == 1)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("恭喜您", "build") + LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].name, "build") + LanguageManage.GetTextByKey("建造成功", "build"), HUDTextTool.TextUITypeEnum.Num1);
			tower.isCanDisplayInfoBtn = true;
			if (SenceManager.inst)
			{
				SenceManager.inst.UpdateGraphs(tower.bodyForAttack.bounds);
			}
		}
		else
		{
			foreach (KeyValuePair<long, LegionHelpApply> current in HeroInfo.GetInstance().legionApply)
			{
				if (tower.id == current.Value.buildingId && MessagePanel._Inst)
				{
					MessagePanel._Inst.ShowApplyInfo();
				}
			}
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("恭喜您", "build") + LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[tower.index].name, "build") + LanguageManage.GetTextByKey("升级成功", "build"), HUDTextTool.TextUITypeEnum.Num1);
		}
		if (MainUIPanelManage._instance && UnitConst.GetInstance().buildingConst[tower.index].lvInfos[tower.lv].playerExp > 0)
		{
			tower.StartCoroutine(MainUIPanelManage._instance.DisplayExpAdd(tower.tr.position, HeroInfo.GetInstance().addExp));
		}
		if (tower.index == 1 && BuildingHandler.UnloadBuilding != null)
		{
			BuildingHandler.UnloadBuilding(UnitConst.GetInstance().HomeUpdateOpenSetDataConst[tower.lv - 1].HomeUpdateOpenSetAddData());
		}
		if (tower.index == 90)
		{
			if (SenceManager.inst.WallBuildEndAudio <= 0f)
			{
				SenceManager.inst.WallBuildEndAudio = 1f;
				AudioManage.inst.PlayAuido("buildcomplete", false);
			}
		}
		else
		{
			AudioManage.inst.PlayAuido("buildcomplete", false);
		}
		if (tower.index == 10)
		{
			if (BuildingHandler.RadarBuildingUp != null)
			{
				BuildingHandler.RadarBuildingUp(tower.lv);
			}
		}
		else if (tower.index == 1 && BuildingHandler.HomeBuildingUp != null)
		{
			BuildingHandler.HomeBuildingUp(tower.lv);
		}
		if ((tower.index == 13 || tower.index == 91) && BuildingHandler.UnloadArmys != null)
		{
			BuildingHandler.UnloadArmys(HeroInfo.GetInstance().addArmy_open);
		}
		if (tower.index == 90)
		{
			SenceManager.inst.EffectWallList.Add(tower);
			if (tower == SenceManager.inst.ChooseWallForEffect)
			{
				SenceManager.inst.WallListUpdateEffect();
			}
		}
		else
		{
			DieBall dieBall = PoolManage.Ins.CreatEffect("shengji", tower.tr.position, tower.tr.rotation, null);
			dieBall.LifeTime = 0.5f;
		}
		if (tower.BuilindingCDInfo)
		{
			UnityEngine.Object.Destroy(tower.BuilindingCDInfo.ga);
		}
		if (tower.ArmyTitleNew)
		{
			UnityEngine.Object.Destroy(tower.ArmyTitleNew.ga);
		}
		HUDTextTool.inst.Powerhouse();
	}

	public static void CG_BuildingAddStart(int mapIndex, int buildIdx, int money, T_Tower newBuilding)
	{
		CSBuildingBuild cSBuildingBuild = new CSBuildingBuild();
		cSBuildingBuild.index = mapIndex;
		cSBuildingBuild.itemId = buildIdx;
		cSBuildingBuild.money = money;
		BuildingHandler.AddBuldingNew = newBuilding;
		LogManage.Log("建筑建造 花费钻石" + money);
		ClientMgr.GetNet().SendHttp(2000, cSBuildingBuild, new DataHandler.OpcodeHandler(BuildingHandler.GC_NewBuildingAddEnd), null);
	}

	public static void CG_BuildingMoveStart(long mapId, int row, int num, long buildId)
	{
		BuildingHandler._row = row;
		BuildingHandler._num = num;
		CSBuildingMove cSBuildingMove = new CSBuildingMove();
		cSBuildingMove.buildingId = buildId;
		cSBuildingMove.index = InfoMgr.PosGetMapIdx(row, num);
		cSBuildingMove.itemId = SenceInfo.curMap.towerList_Data[buildId].buildingIdx;
		cSBuildingMove.islandId = mapId;
		ClientMgr.GetNet().SendHttp(2004, cSBuildingMove, null, null);
	}

	public static void GC_BuildingMoveEnd(bool isError, Opcode opcode)
	{
		if (SenceManager.inst.rotate_time > 0f)
		{
			SenceManager.inst.rotate_time = 0f;
		}
		if (!isError)
		{
			List<SCBuildingMove> list = opcode.Get<SCBuildingMove>(10008);
			if (list.Count > 0)
			{
				SenceInfo.curMap.towerList_Data[list[0].id].row = BuildingHandler._row;
				SenceInfo.curMap.towerList_Data[list[0].id].num = BuildingHandler._num;
				SenceInfo.curMap.towerList_Data[list[0].id].posIdx = InfoMgr.PosGetMapIdx(BuildingHandler._row, BuildingHandler._num);
				SenceManager.inst.RefreshPath();
			}
		}
		else
		{
			SenceManager.inst.RebackTower();
		}
	}

	public static void CG_BuildingRemoveStart(int money, T_Res _tar)
	{
		CSBuildingRemove cSBuildingRemove = new CSBuildingRemove();
		cSBuildingRemove.index = _tar.posIndex;
		cSBuildingRemove.islandId = SenceInfo.curMap.ID;
		cSBuildingRemove.money = money;
		ClientMgr.GetNet().SendHttp(2008, cSBuildingRemove, new DataHandler.OpcodeHandler(BuildingHandler.GC_BuildingRemoveEnd), null);
	}

	public static void GC_BuildingRemoveEnd(bool isError, Opcode opcode)
	{
		if (!isError)
		{
			BuildingHandler.RemoveRes(opcode);
			BuildingHandler.BuildingAddEnd(opcode);
			FuncUIManager.inst.MainUIPanelManage.RefreshBuildingStoreNotice();
		}
	}

	public static void CG_WallListMoveStart(T_Tower tower)
	{
		CSCityWallBatchMove cSCityWallBatchMove = new CSCityWallBatchMove();
		KVStruct kVStruct = new KVStruct();
		kVStruct.key = tower.id;
		tower.row = Mathf.RoundToInt(tower.tr.localPosition.x);
		tower.num = Mathf.RoundToInt(tower.tr.localPosition.z);
		kVStruct.value = (long)InfoMgr.PosGetMapIdx(tower.row, tower.num);
		cSCityWallBatchMove.CityWall.Add(kVStruct);
		if (SenceManager.inst.WallLineChoose && tower.Walltower_list.Count > 0)
		{
			for (int i = 0; i < tower.Walltower_list.Count; i++)
			{
				kVStruct = new KVStruct();
				kVStruct.key = tower.Walltower_list[i].id;
				tower.Walltower_list[i].row = Mathf.RoundToInt(tower.Walltower_list[i].tr.localPosition.x);
				tower.Walltower_list[i].num = Mathf.RoundToInt(tower.Walltower_list[i].tr.localPosition.z);
				kVStruct.value = (long)InfoMgr.PosGetMapIdx(tower.Walltower_list[i].row, tower.Walltower_list[i].num);
				cSCityWallBatchMove.CityWall.Add(kVStruct);
			}
		}
		cSCityWallBatchMove.islandId = SenceInfo.curMap.ID;
		ClientMgr.GetNet().SendHttp(2022, cSCityWallBatchMove, null, null);
	}

	public static void GC_WallListMoveEnd(bool isError, Opcode opcode)
	{
		if (SenceManager.inst.rotate_time > 0f)
		{
			SenceManager.inst.rotate_time = 0f;
		}
		if (!isError)
		{
			Dictionary<int, List<object>> dic = opcode.getDic();
			if (dic.ContainsKey(10094))
			{
				for (int i = 0; i < dic[10094].Count; i++)
				{
					SCCityWallBatchMove sCCityWallBatchMove = dic[10094][i] as SCCityWallBatchMove;
					foreach (SCBuildingMove current in sCCityWallBatchMove.cityWalls)
					{
						SenceInfo.curMap.towerList_Data[current.id].row = BuildingHandler._row;
						SenceInfo.curMap.towerList_Data[current.id].num = BuildingHandler._num;
						SenceInfo.curMap.towerList_Data[current.id].posIdx = InfoMgr.PosGetMapIdx(BuildingHandler._row, BuildingHandler._num);
					}
				}
			}
		}
	}

	private static void RemoveRes(Opcode opcode)
	{
		List<SCBuildingRemove> list = opcode.Get<SCBuildingRemove>(10040);
		foreach (SCBuildingRemove current in list)
		{
			if (SenceManager.inst.reses.ContainsKey((int)current.id))
			{
				T_Res t_Res = SenceManager.inst.reses[(int)current.id];
				if (HeroInfo.GetInstance().addResouce.Count > 0)
				{
					foreach (SCPlayerResource current2 in HeroInfo.GetInstance().addResouce)
					{
						switch (current2.resId)
						{
						case 1:
							CoroutineInstance.DoJob(ResFly2.CreateRes(t_Res.transform, ResType.金币, current2.resVal, null, null));
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("获取金币", "ResIsland") + ":" + current2.resVal, HUDTextTool.TextUITypeEnum.Num1);
							break;
						case 2:
							CoroutineInstance.DoJob(ResFly2.CreateRes(t_Res.transform, ResType.石油, current2.resVal, null, null));
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("获取石油", "ResIsland") + ":" + current2.resVal, HUDTextTool.TextUITypeEnum.Num1);
							break;
						case 3:
							CoroutineInstance.DoJob(ResFly2.CreateRes(t_Res.transform, ResType.钢铁, current2.resVal, null, null));
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("获取钢铁", "ResIsland") + ":" + current2.resVal, HUDTextTool.TextUITypeEnum.Num1);
							break;
						case 4:
							CoroutineInstance.DoJob(ResFly2.CreateRes(t_Res.transform, ResType.稀矿, current2.resVal, null, null));
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("获取稀矿", "ResIsland") + ":" + current2.resVal, HUDTextTool.TextUITypeEnum.Num1);
							break;
						case 7:
							CoroutineInstance.DoJob(ResFly2.CreateRes(t_Res.transform, ResType.钻石, current2.resVal, null, null));
							HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("获取钻石", "ResIsland") + ":" + current2.resVal, HUDTextTool.TextUITypeEnum.Num1);
							break;
						}
					}
				}
				Body_Model effectByName = PoolManage.Ins.GetEffectByName("kanshu", null);
				if (effectByName != null)
				{
					effectByName.tr.position = t_Res.transform.position + new Vector3(0f, 0.3f, 0f);
					effectByName.DesInsInPool(effectByName.GetComponentInChildren<ParticleSystem>().duration);
				}
				SenceManager.inst.RemoveRes((int)current.id);
				AudioManage.inst.PlayAuido("getres", false);
			}
		}
		if (list.Count > 0)
		{
			HUDTextTool.inst.SetText(string.Format(LanguageManage.GetTextByKey("移除成功", "build"), new object[0]), HUDTextTool.TextUITypeEnum.Num1);
		}
	}

	public static void NoCDBuilding(int index)
	{
		if (UnitConst.GetInstance().buildingConst[index].secType == 9 || UnitConst.GetInstance().buildingConst[index].secType == 20)
		{
			int num = SenceInfo.curMap.towerList_Data.Values.Count((BuildingNPC a) => a.buildingIdx == index);
			if (HeroInfo.GetInstance().PlayerBuildingLimit.ContainsKey(index))
			{
				if (num >= HeroInfo.GetInstance().PlayerBuildingLimit[index])
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("升级司令部可以获得更多数量", "build"), HUDTextTool.TextUITypeEnum.Num5);
					if (MainUIPanelManage._instance)
					{
						MainUIPanelManage._instance.OpenPanelMian();
					}
					if (T_CommandPanelManage._instance)
					{
						T_CommandPanelManage._instance.gameObject.SetActive(false);
					}
					return;
				}
				foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().buildingConst[index].lvInfos[0].resCost)
				{
					switch (current.Key)
					{
					case ResType.石油:
						SenceManager.inst.needOil_Build = current.Value - HeroInfo.GetInstance().playerRes.resOil;
						break;
					case ResType.钢铁:
						SenceManager.inst.needSteel_Build = current.Value - HeroInfo.GetInstance().playerRes.resSteel;
						break;
					case ResType.稀矿:
						SenceManager.inst.needRareEarth_Build = current.Value - HeroInfo.GetInstance().playerRes.resRareEarth;
						break;
					}
				}
				Camera_FingerManager.inst.GetDragCamera(SenceManager.inst.AddTempNPC(index, 0).tr);
			}
		}
	}
}
