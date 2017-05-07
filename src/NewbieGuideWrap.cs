using LuaInterface;
using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewbieGuideWrap
{
	public static string nextNewBi;

	public static string CurNewBiFuncName;

	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("GetButton", new LuaCSFunction(NewbieGuideWrap.GetButton)),
		new LuaMethod("GetMaxTowerNum_CurMap", new LuaCSFunction(NewbieGuideWrap.GetMaxTowerNum_CurMap)),
		new LuaMethod("GetCurTowerNum_CurMap", new LuaCSFunction(NewbieGuideWrap.GetCurTowerNum_CurMap)),
		new LuaMethod("CameraLookAt", new LuaCSFunction(NewbieGuideWrap.CameraLookAt)),
		new LuaMethod("TowerDieCallLua", new LuaCSFunction(NewbieGuideWrap.TowerDieCallLua)),
		new LuaMethod("GetCommandButtonFight", new LuaCSFunction(NewbieGuideWrap.GetCommandButtonFight)),
		new LuaMethod("GetSoliderNumFightByIndex", new LuaCSFunction(NewbieGuideWrap.GetSoliderNumFightByIndex)),
		new LuaMethod("GetSoliderButtonFightByIndex", new LuaCSFunction(NewbieGuideWrap.GetSoliderButtonFightByIndex)),
		new LuaMethod("GetSkillButtonFightByIndex", new LuaCSFunction(NewbieGuideWrap.GetSkillButtonFightByIndex)),
		new LuaMethod("GetSoliderFuncingNumByGa", new LuaCSFunction(NewbieGuideWrap.GetSoliderFuncingNumByGa)),
		new LuaMethod("GetSoliderFuncedNumByGa", new LuaCSFunction(NewbieGuideWrap.GetSoliderFuncedNumByGa)),
		new LuaMethod("ShowDes", new LuaCSFunction(NewbieGuideWrap.ShowDes)),
		new LuaMethod("IsTaskCanRecieve", new LuaCSFunction(NewbieGuideWrap.IsTaskCanRecieve)),
		new LuaMethod("IsRecieved", new LuaCSFunction(NewbieGuideWrap.IsRecieved)),
		new LuaMethod("ShowTowerAllUI", new LuaCSFunction(NewbieGuideWrap.ShowTowerAllUI)),
		new LuaMethod("ShowTower_InBuildingAllUI", new LuaCSFunction(NewbieGuideWrap.ShowTower_InBuildingAllUI)),
		new LuaMethod("ShowButtonAllUI", new LuaCSFunction(NewbieGuideWrap.ShowButtonAllUI)),
		new LuaMethod("DianDi", new LuaCSFunction(NewbieGuideWrap.DianDi)),
		new LuaMethod("ShowPerson", new LuaCSFunction(NewbieGuideWrap.ShowPerson)),
		new LuaMethod("NextNewbi", new LuaCSFunction(NewbieGuideWrap.NextNewbi)),
		new LuaMethod("GetTower", new LuaCSFunction(NewbieGuideWrap.GetTower)),
		new LuaMethod("GetSenceState", new LuaCSFunction(NewbieGuideWrap.GetSenceState)),
		new LuaMethod("GetHomeTowerLv", new LuaCSFunction(NewbieGuideWrap.GetHomeTowerLv)),
		new LuaMethod("GetArmyFac_NoArmy", new LuaCSFunction(NewbieGuideWrap.GetArmyFac_NoArmy)),
		new LuaMethod("GetTowerInBuildingQueue", new LuaCSFunction(NewbieGuideWrap.GetTowerInBuildingQueue)),
		new LuaMethod("GetJiJieDian", new LuaCSFunction(NewbieGuideWrap.GetJiJieDian)),
		new LuaMethod("IsFuncArmying", new LuaCSFunction(NewbieGuideWrap.IsFuncArmying)),
		new LuaMethod("IsFuncAir", new LuaCSFunction(NewbieGuideWrap.IsFuncAir)),
		new LuaMethod("ISHomeUpBuildingOpen", new LuaCSFunction(NewbieGuideWrap.ISHomeUpBuildingOpen)),
		new LuaMethod("ISArmyOpenPanel", new LuaCSFunction(NewbieGuideWrap.ISArmyOpenPanel)),
		new LuaMethod("ISCommandPanel", new LuaCSFunction(NewbieGuideWrap.ISCommandPanel)),
		new LuaMethod("CloseCommandPanelToOpenMainUIPanel", new LuaCSFunction(NewbieGuideWrap.CloseCommandPanelToOpenMainUIPanel)),
		new LuaMethod("ISFightPanel", new LuaCSFunction(NewbieGuideWrap.ISFightPanel)),
		new LuaMethod("GetTowerNumByIndex", new LuaCSFunction(NewbieGuideWrap.GetTowerNumByIndex)),
		new LuaMethod("GetTowerIsCanCollectResource", new LuaCSFunction(NewbieGuideWrap.GetTowerIsCanCollectResource)),
		new LuaMethod("GetIslandTypeInMap", new LuaCSFunction(NewbieGuideWrap.GetIslandTypeInMap)),
		new LuaMethod("GetIslandByIndex", new LuaCSFunction(NewbieGuideWrap.GetIslandByIndex)),
		new LuaMethod("GetBattleIslandByID", new LuaCSFunction(NewbieGuideWrap.GetBattleIslandByID)),
		new LuaMethod("StrikeBox", new LuaCSFunction(NewbieGuideWrap.StrikeBox)),
		new LuaMethod("ShowForceGuide", new LuaCSFunction(NewbieGuideWrap.ShowForceGuide)),
		new LuaMethod("GetAllTower", new LuaCSFunction(NewbieGuideWrap.GetAllTower)),
		new LuaMethod("GetAmy", new LuaCSFunction(NewbieGuideWrap.GetAmy)),
		new LuaMethod("GetBattleField", new LuaCSFunction(NewbieGuideWrap.GetBattleField)),
		new LuaMethod("GetBattleFieldState", new LuaCSFunction(NewbieGuideWrap.GetBattleFieldState)),
		new LuaMethod("SetNewBieGudieID", new LuaCSFunction(NewbieGuideWrap.SetNewBieGudieID)),
		new LuaMethod("GetAllTank", new LuaCSFunction(NewbieGuideWrap.GetAllTank)),
		new LuaMethod("GetSkillBox", new LuaCSFunction(NewbieGuideWrap.GetSkillBox)),
		new LuaMethod("isLandingBoxMouseUp", new LuaCSFunction(NewbieGuideWrap.isLandingBoxMouseUp)),
		new LuaMethod("CurNewbi", new LuaCSFunction(NewbieGuideWrap.SetCurNewBiFuncName)),
		new LuaMethod("NewEnemyAttack", new LuaCSFunction(NewbieGuideWrap.NewEnemyAttack)),
		new LuaMethod("NewSoliderAttack", new LuaCSFunction(NewbieGuideWrap.NewSoliderAttack)),
		new LuaMethod("NewEnemyAttackStart", new LuaCSFunction(NewbieGuideWrap.NewEnemyAttackStart)),
		new LuaMethod("BattlefieldReportBefore", new LuaCSFunction(NewbieGuideWrap.BattlefieldReportBefore)),
		new LuaMethod("OptionNextNewBi", new LuaCSFunction(NewbieGuideWrap.OptionNextNewBi)),
		new LuaMethod("OpenBattlefieldReport", new LuaCSFunction(NewbieGuideWrap.OpenBattlefieldReport)),
		new LuaMethod("TexiaoShow", new LuaCSFunction(NewbieGuideWrap.TexiaoShow)),
		new LuaMethod("GetBattleState", new LuaCSFunction(NewbieGuideWrap.GetBattleState)),
		new LuaMethod("GetBattleIndex", new LuaCSFunction(NewbieGuideWrap.GetBattleIndex)),
		new LuaMethod("GoHome", new LuaCSFunction(NewbieGuideWrap.GoHome)),
		new LuaMethod("ShowShield", new LuaCSFunction(NewbieGuideWrap.ShowShield)),
		new LuaMethod("IsInBuildingCD", new LuaCSFunction(NewbieGuideWrap.IsInBuildingCD)),
		new LuaMethod("ShowReNamePanel", new LuaCSFunction(NewbieGuideWrap.ShowReNamePanel)),
		new LuaMethod("SetBuildingPosition", new LuaCSFunction(NewbieGuideWrap.SetBuildingPosition)),
		new LuaMethod("GetSenceType", new LuaCSFunction(NewbieGuideWrap.GetSenceType)),
		new LuaMethod("IsHaveArmyInBuild", new LuaCSFunction(NewbieGuideWrap.IsHaveArmyInBuild)),
		new LuaMethod("CreateLandArmyGuid", new LuaCSFunction(NewbieGuideWrap.CreateLandArmyGuid)),
		new LuaMethod("IsHaveComandoInFight", new LuaCSFunction(NewbieGuideWrap.IsHaveComandoInFight)),
		new LuaMethod("ComandoIndexInFight", new LuaCSFunction(NewbieGuideWrap.ComandoIndexInFight)),
		new LuaMethod("CloseExpensePanel", new LuaCSFunction(NewbieGuideWrap.CloseExpensePanel)),
		new LuaMethod("IsShopPanel", new LuaCSFunction(NewbieGuideWrap.IsShopPanel)),
		new LuaMethod("IsTaskPanel", new LuaCSFunction(NewbieGuideWrap.IsTaskPanel)),
		new LuaMethod("IsAchievementPanel", new LuaCSFunction(NewbieGuideWrap.IsAchievementPanel)),
		new LuaMethod("IsTopTenPanel", new LuaCSFunction(NewbieGuideWrap.IsTopTenPanel)),
		new LuaMethod("IsPVPMessage", new LuaCSFunction(NewbieGuideWrap.IsPVPMessage)),
		new LuaMethod("IsEmailPanel", new LuaCSFunction(NewbieGuideWrap.IsEmailPanel)),
		new LuaMethod("IsSettlementPanel", new LuaCSFunction(NewbieGuideWrap.IsSettlementPanel)),
		new LuaMethod("IsActivityPanel", new LuaCSFunction(NewbieGuideWrap.IsActivityPanel)),
		new LuaMethod("IsShowAwardPanel", new LuaCSFunction(NewbieGuideWrap.IsShowAwardPanel)),
		new LuaMethod("CloseRunAwayBtn", new LuaCSFunction(NewbieGuideWrap.CloseRunAwayBtn)),
		new LuaMethod("IsSkillZhanshiPanel", new LuaCSFunction(NewbieGuideWrap.IsSkillZhanshiPanel)),
		new LuaMethod("BuildQueueCount", new LuaCSFunction(NewbieGuideWrap.BuildQueueCount)),
		new LuaMethod("IsHaveFreeBuildQueue", new LuaCSFunction(NewbieGuideWrap.IsHaveFreeBuildQueue)),
		new LuaMethod("IsMessageBoxPanel", new LuaCSFunction(NewbieGuideWrap.IsMessageBoxPanel)),
		new LuaMethod("IsExpensePanel", new LuaCSFunction(NewbieGuideWrap.IsExpensePanel)),
		new LuaMethod("IsSkillIDInBattle", new LuaCSFunction(NewbieGuideWrap.IsSkillIDInBattle)),
		new LuaMethod("IsSkillIDInBag", new LuaCSFunction(NewbieGuideWrap.IsSkillIDInBag)),
		new LuaMethod("IsShowPlayerLevelManagerPanel", new LuaCSFunction(NewbieGuideWrap.IsShowPlayerLevelManagerPanel)),
		new LuaMethod("IsPlayerNameExist", new LuaCSFunction(NewbieGuideWrap.IsPlayerNameExist)),
		new LuaMethod("Lock2D", new LuaCSFunction(NewbieGuideWrap.Lock2D)),
		new LuaMethod("Lock3D", new LuaCSFunction(NewbieGuideWrap.Lock3D)),
		new LuaMethod("GetResource", new LuaCSFunction(NewbieGuideWrap.GetResource)),
		new LuaMethod("PauseGame", new LuaCSFunction(NewbieGuideWrap.PauseGame)),
		new LuaMethod("LogError", new LuaCSFunction(NewbieGuideWrap.LogError)),
		new LuaMethod("SendAirOrSkill", new LuaCSFunction(NewbieGuideWrap.SendAirOrSkill)),
		new LuaMethod("OpenTaskByNewBiePanel", new LuaCSFunction(NewbieGuideWrap.OpenTaskByNewBiePanel)),
		new LuaMethod("MainNewBieStay", new LuaCSFunction(NewbieGuideWrap.MainNewBieStay)),
		new LuaMethod("LockTaskByNewBie", new LuaCSFunction(NewbieGuideWrap.LockTaskByNewBie)),
		new LuaMethod("TaskByNewBieOver", new LuaCSFunction(NewbieGuideWrap.TaskByNewBieOver)),
		new LuaMethod("CloseSettlementPanel", new LuaCSFunction(NewbieGuideWrap.CloseSettlementPanel)),
		new LuaMethod("GetArmyLevelById", new LuaCSFunction(NewbieGuideWrap.GetArmyLevelById)),
		new LuaMethod("GetArmyUpdatingLevelById", new LuaCSFunction(NewbieGuideWrap.GetArmyUpdatingLevelById)),
		new LuaMethod("GetEliteBattle", new LuaCSFunction(NewbieGuideWrap.GetEliteBattle)),
		new LuaMethod("GetEarthStar_Battle", new LuaCSFunction(NewbieGuideWrap.GetEarthStar_Battle))
	};

	public static bool IsPause = true;

	public static LuaField[] fields = new LuaField[0];

	public static string optionNextNewBi;

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetEarthStar_Battle(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		if (LegionMapManager._inst)
		{
			foreach (EarthStar current in LegionMapManager._inst.EarthStarList)
			{
				if (current)
				{
					Debug.Log(string.Concat(new object[]
					{
						"id:",
						num,
						"   int.Parse(star.transform.parent.name):",
						int.Parse(current.transform.parent.name)
					}));
					if (num == int.Parse(current.transform.parent.name))
					{
						Debug.Log("获得");
						LuaScriptMgr.Push(L, current.gameObject);
						return 1;
					}
				}
			}
			LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
			return 1;
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetEliteBattle(IntPtr L)
	{
		T_Island t_Island = (from a in T_WMap.inst.islandList.Values
		where !string.IsNullOrEmpty(a.ownerId) && UnitConst.GetInstance().AllNpc.ContainsKey(int.Parse(a.ownerId)) && UnitConst.GetInstance().AllNpc[int.Parse(a.ownerId)].Star > 0
		orderby UnitConst.GetInstance().AllNpc[int.Parse(a.ownerId)].Star
		select a).FirstOrDefault<T_Island>();
		if (t_Island != null)
		{
			if (Loading.senceType == SenceType.WorldMap && CameraSmoothMove.inst)
			{
				CameraSmoothMove.inst.MovePosition(new Vector3(t_Island.tr.position.x, 0f, t_Island.tr.position.z), null);
			}
			LuaScriptMgr.Push(L, t_Island.ga);
		}
		else
		{
			LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetArmyLevelById(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		int d = 0;
		if (HeroInfo.GetInstance().PlayerArmyData.ContainsKey(key))
		{
			d = HeroInfo.GetInstance().PlayerArmyData[key].level;
		}
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetArmyUpdatingLevelById(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		int d = 0;
		if (HeroInfo.GetInstance().PlayerArmyData.ContainsKey(key))
		{
			d = HeroInfo.GetInstance().PlayerArmyData[key].updatingLv();
		}
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int CloseSettlementPanel(IntPtr L)
	{
		if (SettlementManager.inst != null)
		{
			SettlementManager.inst.gameObject.SetActive(false);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int TaskByNewBieOver(IntPtr L)
	{
		LogManage.LogError("TaskByNewBieOver");
		NewbieGuidePanel.TaskGuideID = 1001;
		NewbieGuideManage._instance.CS_NewGuide(NewbieGuidePanel.guideIdByServer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int LockTaskByNewBie(IntPtr L)
	{
		LogManage.LogError("LockTaskByNewBie");
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		if (num == 1)
		{
			NewbieGuideManage._instance.LockTaskByNewBie = true;
		}
		else
		{
			NewbieGuideManage._instance.LockTaskByNewBie = false;
		}
		NewbieGuideManage._instance.CS_NewGuide(NewbieGuidePanel.guideIdByServer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int MainNewBieStay(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		if (num == 1)
		{
			NewbieGuideManage._instance.MainNewbieStay = true;
		}
		else
		{
			NewbieGuideManage._instance.MainNewbieStay = false;
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int OpenTaskByNewBiePanel(IntPtr L)
	{
		if (TaskByNewBieManager._inst && TaskByNewBieManager._inst.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No])
		{
			int num = (int)LuaDLL.lua_tonumber(L, 1);
			if (num == 1)
			{
				TaskByNewBieManager._inst.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Message_Open();
			}
			else
			{
				TaskByNewBieManager._inst.Task_Right_Btn[TaskByNewBieManager._inst.TaskByNewBiePanel_No].Message_Close();
			}
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int SendAirOrSkill(IntPtr L)
	{
		int skillID = (int)LuaDLL.lua_tonumber(L, 1);
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 2);
		int skillLV = (int)LuaDLL.lua_tonumber(L, 3);
		if (SkillManage.inst && unityObject)
		{
			SkillManage.inst.AcquittalSkill(skillID, unityObject.transform.position, skillLV);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetButton(IntPtr L)
	{
		int id = (int)LuaDLL.lua_tonumber(L, 1);
		LuaScriptMgr.Push(L, NewbieGuideManage._instance.GetBtnTransform(id));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSoliderButtonFightByIndex(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (FightPanelManager.inst && FightPanelManager.inst.solider_UIDIC.ContainsKey(key))
		{
			LuaScriptMgr.Push(L, FightPanelManager.inst.solider_UIDIC[key].ga);
		}
		else
		{
			LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSoliderNumFightByIndex(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (FightPanelManager.inst && FightPanelManager.inst.solider_UIDIC.ContainsKey(key))
		{
			LuaScriptMgr.Push(L, FightPanelManager.inst.solider_UIDIC[key].GetSoliderNum_plus());
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int CameraLookAt(IntPtr L)
	{
		if (SenceManager.inst.Tanks_Attack.Count > 0)
		{
			for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
			{
				if (!UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Attack[i].index].isCanFly && SenceManager.inst.Tanks_Attack[i].tankType == T_TankAbstract.TankType.坦克)
				{
					CameraControl.inst.target = SenceManager.inst.Tanks_Attack[i].ga;
					break;
				}
			}
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int TowerDieCallLua(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		NewbieGuidePanel.towerDieCallLua_InStarFire = (num == 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetCommandButtonFight(IntPtr L)
	{
		if (FightPanelManager.inst && FightPanelManager.inst.CommandSoliderUI)
		{
			LuaScriptMgr.Push(L, FightPanelManager.inst.CommandSoliderUI.gameObject);
		}
		else
		{
			LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetMaxTowerNum_CurMap(IntPtr L)
	{
		int d = SenceInfo.curMap.towerList_Data.Values.Count((BuildingNPC a) => UnitConst.GetInstance().buildingConst[a.buildingIdx].resType < 3);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetCurTowerNum_CurMap(IntPtr L)
	{
		int d = SenceManager.inst.towers.Count((T_Tower a) => UnitConst.GetInstance().buildingConst[a.index].resType < 3);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSkillButtonFightByIndex(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		if (FightPanelManager.inst && FightPanelManager.inst.skillUIList.Count > num)
		{
			LuaScriptMgr.Push(L, FightPanelManager.inst.skillUIList[num].ga);
		}
		else
		{
			LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSoliderFuncingNumByGa(IntPtr L)
	{
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		if (unityObject)
		{
			T_Tower component = unityObject.GetComponent<T_Tower>();
			if (HeroInfo.GetInstance().AllArmyInfo.ContainsKey(component.id))
			{
				LuaScriptMgr.Push(L, HeroInfo.GetInstance().AllArmyInfo[component.id].armyFuncing.Count);
			}
			else
			{
				LuaScriptMgr.Push(L, 0);
			}
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSoliderFuncedNumByGa(IntPtr L)
	{
		int num = 0;
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		if (unityObject)
		{
			T_Tower component = unityObject.GetComponent<T_Tower>();
			if (component && HeroInfo.GetInstance().AllArmyInfo.ContainsKey(component.id))
			{
				foreach (KVStruct current in HeroInfo.GetInstance().AllArmyInfo[component.id].armyFunced)
				{
					num += (int)current.value;
				}
			}
		}
		LuaScriptMgr.Push(L, num);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int PauseGame(IntPtr L)
	{
		NewbieGuideWrap.IsPause = LuaDLL.lua_toboolean(L, 1);
		if (NewbieGuideWrap.IsPause)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int LogError(IntPtr L)
	{
		string message = LuaDLL.lua_tostring(L, 1);
		LogManage.LogError(message);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowDes(IntPtr L)
	{
		string textKey = LuaDLL.lua_tostring(L, 2);
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		Vector3 vector2 = LuaScriptMgr.GetVector3(L, 4);
		if (NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.ClearDes();
			switch (num)
			{
			case 1:
				NewbieGuidePanel._instance.left.SetActive(true);
				NewbieGuidePanel._instance.arrowDes_left.text = LanguageManage.GetTextByKey(textKey, "Halftalk");
				break;
			case 2:
				NewbieGuidePanel._instance.right.SetActive(true);
				NewbieGuidePanel._instance.arrowDes_right.text = LanguageManage.GetTextByKey(textKey, "Halftalk");
				break;
			case 3:
				NewbieGuidePanel._instance.up.SetActive(true);
				NewbieGuidePanel._instance.arrowDes_up.text = LanguageManage.GetTextByKey(textKey, "Halftalk");
				break;
			case 4:
				NewbieGuidePanel._instance.down.SetActive(true);
				NewbieGuidePanel._instance.arrowDes_down.text = LanguageManage.GetTextByKey(textKey, "Halftalk");
				break;
			}
			NewbieGuidePanel._instance.pianyi.localPosition = vector;
			NewbieGuidePanel._instance.pianyi.localScale = vector2;
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsInBuildingCD(IntPtr L)
	{
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		LuaScriptMgr.Push(L, HeroInfo.GetInstance().BuildCD.Contains(unityObject.GetComponent<T_Tower>().id));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowReNamePanel(IntPtr L)
	{
		FuncUIManager.inst.OpenFuncUI("RandomNamePanel", SenceType.Island);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSenceType(IntPtr L)
	{
		LuaScriptMgr.Push(L, (int)Loading.senceType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsHaveComandoInFight(IntPtr L)
	{
		LuaScriptMgr.Push(L, (HeroInfo.GetInstance().Commando_Fight != null) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ComandoIndexInFight(IntPtr L)
	{
		if (HeroInfo.GetInstance().Commando_Fight != null)
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().Commando_Fight.index);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int CreateLandArmyGuid(IntPtr L)
	{
		Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
		LuaScriptMgr.Push(L, SenceManager.inst.CreateLandArmy(vector));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsHaveArmyInBuild(IntPtr L)
	{
		foreach (KeyValuePair<long, armyInfoInBuilding> current in HeroInfo.GetInstance().AllArmyInfo)
		{
			foreach (KVStruct current2 in current.Value.armyFunced)
			{
				if (current2.value > 0L)
				{
					LuaScriptMgr.Push(L, 1);
					return 1;
				}
			}
		}
		LuaScriptMgr.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int CloseExpensePanel(IntPtr L)
	{
		LogManage.LogError("关闭消费面板 以及 弹窗面板");
		if (ExpensePanelManage.inst)
		{
			UnityEngine.Object.Destroy(ExpensePanelManage.inst.gameObject);
		}
		if (MessagePanelManage.inst)
		{
			UnityEngine.Object.Destroy(MessagePanelManage.inst.gameObject);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsShopPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(ShopPanelManage.shop == null) && ShopPanelManage.shop.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsTaskPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(NewTaskPanelManager.ins == null) && NewTaskPanelManager.ins.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsActivityPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(ActivityPanelManager.ins == null) && ActivityPanelManager.ins.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsSettlementPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(SettlementManager.inst == null) && SettlementManager.inst.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsAchievementPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(AchievementPanelManage.inst == null) && AchievementPanelManage.inst.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsTopTenPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(TopTenPanelManage._ins == null) && TopTenPanelManage._ins.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsPVPMessage(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(PVPMessage.inst == null) && PVPMessage.inst.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsEmailPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(EmailPanel.ins == null) && EmailPanel.ins.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsShowAwardPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(ShowAwardPanelManger._ins == null) && ShowAwardPanelManger._ins.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int CloseRunAwayBtn(IntPtr L)
	{
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsSkillZhanshiPanel(IntPtr L)
	{
		if (SkillExtractManage.inst && SkillExtractManage.inst.zhanshi.activeSelf)
		{
			LuaScriptMgr.Push(L, 1);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int BuildQueueCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, BuildingQueue.MaxBuildingQueue);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsHaveFreeBuildQueue(IntPtr L)
	{
		LuaScriptMgr.Push(L, (BuildingQueue.MaxBuildingQueue <= HeroInfo.GetInstance().BuildCD.Count) ? 0 : 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsMessageBoxPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(MessagePanelManage.inst == null)) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsExpensePanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(ExpensePanelManage.inst == null) && ExpensePanelManage.inst.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsSkillIDInBattle(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		for (int i = 0; i < HeroInfo.GetInstance().skillCarteList.Count; i++)
		{
			if (HeroInfo.GetInstance().skillCarteList[i].index > 0 && HeroInfo.GetInstance().skillCarteList[i].itemID == num)
			{
				LuaScriptMgr.Push(L, 1);
				return 1;
			}
		}
		LuaScriptMgr.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsSkillIDInBag(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		for (int i = 0; i < HeroInfo.GetInstance().skillCarteList.Count; i++)
		{
			if (HeroInfo.GetInstance().skillCarteList[i].itemID == num)
			{
				LuaScriptMgr.Push(L, 1);
				return 1;
			}
		}
		LuaScriptMgr.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsShowPlayerLevelManagerPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(ShowPlayerLevelManager.ins == null) && ShowPlayerLevelManager.ins.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsPlayerNameExist(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!string.IsNullOrEmpty(HeroInfo.GetInstance().userName) && !HeroInfo.GetInstance().userName.Equals(HeroInfo.GetInstance().userName_Default)) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int Lock2D(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		ButtonClick.newbiLock = (num == 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int Lock3D(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		Camera_FingerManager.newbiLock = (num == 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetResource(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		if (num == 1)
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().playerRes.resCoin);
		}
		else if (num == 2)
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().playerRes.resOil);
		}
		else if (num == 3)
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().playerRes.resSteel);
		}
		else if (num == 4)
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().playerRes.resRareEarth);
		}
		else if (num == 7)
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().playerRes.RMBCoin);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int SetBuildingPosition(IntPtr L)
	{
		Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
		SenceManager.inst.tmpBuildingPostion = vector;
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int TexiaoShow(IntPtr L)
	{
		bool isObjShow = LuaDLL.lua_toboolean(L, 1);
		LuaScriptMgr.Push(L, NewbieGuideManage._instance.OnSetTeXiao(isObjShow));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetTower(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		NewBieSenceType newBieSenceType = (NewBieSenceType)LuaDLL.lua_tonumber(L, 2);
		bool flag = LuaDLL.lua_toboolean(L, 3);
		switch (newBieSenceType)
		{
		case NewBieSenceType.Home:
			if (UIManager.curState == SenceState.Home)
			{
				if (flag)
				{
					LuaScriptMgr.Push(L, NewbieGuideManage._instance.GetTower(num, flag));
				}
				else
				{
					LuaScriptMgr.Push(L, NewbieGuideManage._instance.GetTower(num));
				}
			}
			else
			{
				LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
			}
			return 1;
		case NewBieSenceType.spy:
			if (NewbieGuidePanel.isZhanyi && UIManager.curState == SenceState.Spy)
			{
				LuaScriptMgr.Push(L, NewbieGuideManage._instance.GetTower(num));
			}
			else
			{
				LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
			}
			return 1;
		case NewBieSenceType.Attart:
			if (UIManager.curState == SenceState.Attacking)
			{
				LuaScriptMgr.Push(L, NewbieGuideManage._instance.GetTower(num));
			}
			else
			{
				LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
			}
			return 1;
		}
		LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSenceState(IntPtr L)
	{
		SenceState curState = UIManager.curState;
		switch (curState)
		{
		case SenceState.Home:
			LuaScriptMgr.Push(L, 2);
			break;
		case SenceState.Spy:
			LuaScriptMgr.Push(L, 3);
			break;
		case SenceState.Attacking:
			LuaScriptMgr.Push(L, 4);
			break;
		case SenceState.InBuild:
			LuaScriptMgr.Push(L, 1);
			break;
		case SenceState.WatchResIsland:
			LuaScriptMgr.Push(L, 5);
			break;
		case SenceState.WatchVideo:
			LuaScriptMgr.Push(L, 6);
			break;
		default:
			if (curState != SenceState.Visit)
			{
				LuaScriptMgr.Push(L, 0);
			}
			else
			{
				LuaScriptMgr.Push(L, 7);
			}
			break;
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetHomeTowerLv(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (HeroInfo.GetInstance().PlayerBuildingLevel.ContainsKey(key))
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().PlayerBuildingLevel[key]);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetArmyFac_NoArmy(IntPtr L)
	{
		T_Tower t_Tower = null;
		int num = 0;
		foreach (T_Tower current in SenceManager.inst.towers)
		{
			if (UnitConst.GetInstance().buildingConst[current.index].secType == 6)
			{
				if (!HeroInfo.GetInstance().AllArmyInfo.ContainsKey(current.id) || HeroInfo.GetInstance().AllArmyInfo[current.id].armyFunced.Count == 0)
				{
					LuaScriptMgr.Push(L, current.ga);
					int result = 1;
					return result;
				}
				if (HeroInfo.GetInstance().AllArmyInfo.ContainsKey(current.id))
				{
					int num2 = UnitConst.GetInstance().buildingConst[current.index].lvInfos[current.lv].outputLimit[ResType.兵种];
					int num3 = 0;
					for (int i = 0; i < HeroInfo.GetInstance().AllArmyInfo[current.id].armyFunced.Count; i++)
					{
						num3 += (int)(HeroInfo.GetInstance().AllArmyInfo[current.id].armyFunced[i].value * (long)UnitConst.GetInstance().soldierConst[(int)HeroInfo.GetInstance().AllArmyInfo[current.id].armyFunced[i].key].peopleNum);
					}
					if (num3 < num2)
					{
						if (num == 0 || num3 < num)
						{
							num = num3;
							t_Tower = current;
						}
					}
				}
			}
		}
		if (t_Tower != null)
		{
			LuaScriptMgr.Push(L, t_Tower.ga);
			return 1;
		}
		LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetTowerInBuildingQueue(IntPtr L)
	{
		if (HeroInfo.GetInstance().BuildCD.Count > 0)
		{
			foreach (T_Tower current in SenceManager.inst.towers)
			{
				if (current.id == HeroInfo.GetInstance().BuildCD[0])
				{
					LuaScriptMgr.Push(L, current.ga);
					return 1;
				}
			}
			return 1;
		}
		LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetJiJieDian(IntPtr L)
	{
		int posIndex = (int)LuaDLL.lua_tonumber(L, 1);
		LuaScriptMgr.Push(L, NewbieGuideManage._instance.GetJiJieDianTower(posIndex));
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsFuncArmying(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsFuncAir(IntPtr L)
	{
		T_Tower t_Tower = SenceManager.inst.towers.SingleOrDefault((T_Tower a) => UnitConst.GetInstance().buildingConst[a.index].secType == 21);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ISHomeUpBuildingOpen(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(HomeUpOpenBuilding.inst == null)) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ISArmyOpenPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(NewarmyInfo.inst == null)) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ISFightPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(FightPanelManager.inst == null) && FightPanelManager.inst.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ISCommandPanel(IntPtr L)
	{
		LuaScriptMgr.Push(L, (!(T_CommandPanelManage._instance == null) && T_CommandPanelManage._instance.gameObject.activeInHierarchy) ? 1 : 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int CloseCommandPanelToOpenMainUIPanel(IntPtr L)
	{
		if (T_CommandPanelManage._instance)
		{
			T_CommandPanelManage._instance.HidePanel();
			T_CommandPanelManage._instance.OpenMainPanel();
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetTowerNumByIndex(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		int count = SenceManager.inst.towers.Count;
		int num2 = 0;
		for (int i = 0; i < count; i++)
		{
			if (SenceManager.inst.towers[i] && SenceManager.inst.towers[i].index == num)
			{
				num2++;
			}
		}
		LuaScriptMgr.Push(L, num2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetTowerIsCanCollectResource(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		int count = SenceManager.inst.towers.Count;
		bool flag = false;
		for (int i = 0; i < count; i++)
		{
			if (SenceManager.inst.towers[i] && SenceManager.inst.towers[i].index == num)
			{
				flag = SenceManager.inst.towers[i].IsCanCollectResource;
				break;
			}
		}
		LuaScriptMgr.Push(L, (!flag) ? 0 : 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetIslandTypeInMap(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		bool flag = false;
		if (HeroInfo.GetInstance().worldMapInfo.playerWMap.ContainsKey(key))
		{
			flag = (HeroInfo.GetInstance().worldMapInfo.playerWMap[key].ownerType == 1);
		}
		LuaScriptMgr.Push(L, (!flag) ? 0 : 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetIslandByIndex(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (T_WMap.inst && T_WMap.inst.islandList.ContainsKey(key))
		{
			LuaScriptMgr.Push(L, T_WMap.inst.islandList[key].ga);
			if (Loading.senceType == SenceType.WorldMap && CameraSmoothMove.inst)
			{
				CameraSmoothMove.inst.MovePosition(new Vector3(T_WMap.inst.islandList[key].tr.position.x, 0f, T_WMap.inst.islandList[key].tr.position.z), null);
			}
		}
		else
		{
			LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetBattleIslandByID(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		if (T_WMap.inst && T_WMap.inst.islandList.ContainsKey(num * -1))
		{
			LuaScriptMgr.Push(L, T_WMap.inst.islandList[num * -1].ga);
			if (Loading.senceType == SenceType.WorldMap && CameraSmoothMove.inst)
			{
				CameraSmoothMove.inst.MovePosition(new Vector3(T_WMap.inst.islandList[num * -1].tr.position.x, 0f, T_WMap.inst.islandList[num * -1].tr.position.z), null);
			}
		}
		else
		{
			LuaScriptMgr.Push(L, NewbieGuideWrap.returnNull());
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowForceGuide(IntPtr L)
	{
		GameObject netObject = LuaScriptMgr.GetNetObject<GameObject>(L, 1);
		if (netObject != null && BuildingStorePanelManage._instance != null)
		{
			if (BuildingStorePanelManage._instance.steelworksBtn != null && netObject.Equals(BuildingStorePanelManage._instance.steelworksBtn))
			{
				BuildingStorePanelManage._instance.steelworksBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.steelWarehouseBtn != null && netObject.Equals(BuildingStorePanelManage._instance.steelWarehouseBtn))
			{
				BuildingStorePanelManage._instance.steelWarehouseBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.feijichang != null && netObject.Equals(BuildingStorePanelManage._instance.feijichang))
			{
				BuildingStorePanelManage._instance.feijichang.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.kejiZhongxin != null && netObject.Equals(BuildingStorePanelManage._instance.kejiZhongxin))
			{
				BuildingStorePanelManage._instance.kejiZhongxin.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.jiqiangta != null && netObject.Equals(BuildingStorePanelManage._instance.jiqiangta))
			{
				BuildingStorePanelManage._instance.jiqiangta.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.kuangshiBtn != null && netObject.Equals(BuildingStorePanelManage._instance.kuangshiBtn))
			{
				BuildingStorePanelManage._instance.kuangshiBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.BingYing != null && netObject.Equals(BuildingStorePanelManage._instance.BingYing))
			{
				BuildingStorePanelManage._instance.BingYing.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.chariotBtn != null && netObject.Equals(BuildingStorePanelManage._instance.chariotBtn))
			{
				BuildingStorePanelManage._instance.chariotBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.tankfarmBtn != null && netObject.Equals(BuildingStorePanelManage._instance.tankfarmBtn))
			{
				BuildingStorePanelManage._instance.tankfarmBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.WallBtn != null && netObject.Equals(BuildingStorePanelManage._instance.WallBtn))
			{
				BuildingStorePanelManage._instance.WallBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.DrillingBtn != null && netObject.Equals(BuildingStorePanelManage._instance.DrillingBtn))
			{
				BuildingStorePanelManage._instance.DrillingBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.satelliteBtn != null && netObject.Equals(BuildingStorePanelManage._instance.satelliteBtn))
			{
				BuildingStorePanelManage._instance.satelliteBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.coffersBtn != null && netObject.Equals(BuildingStorePanelManage._instance.coffersBtn))
			{
				BuildingStorePanelManage._instance.coffersBtn.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
			if (BuildingStorePanelManage._instance.sniperTower != null && netObject.Equals(BuildingStorePanelManage._instance.sniperTower))
			{
				BuildingStorePanelManage._instance.sniperTower.transform.parent.GetComponent<UICenterOnChild>().CenterOn(netObject.transform);
			}
		}
		NewbieGuidePanel._instance.ShowForceGuide(netObject, 0);
		return 1;
	}

	public static GameObject returnNull()
	{
		return null;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GoHome(IntPtr L)
	{
		NewbieGuidePanel._instance.GoHome();
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetBattleIndex(IntPtr L)
	{
		if (SenceInfo.CurBattle != null)
		{
			LuaScriptMgr.Push(L, SenceInfo.CurBattle.id);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetBattleState(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (UnitConst.GetInstance().BattleConst[key].battleBox > 0)
		{
			LuaScriptMgr.PushValue(L, 1);
		}
		else
		{
			LuaScriptMgr.PushValue(L, 0);
		}
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "NewbieGuideWrap", typeof(NewbieGuideWrap), NewbieGuideWrap.regs, NewbieGuideWrap.fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetAllTower(IntPtr L)
	{
		if (SenceManager.inst)
		{
			LuaScriptMgr.PushValue(L, SenceManager.inst.towers);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetAmy(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (HeroInfo.GetInstance().PlayerArmyData.ContainsKey(key))
		{
			LuaScriptMgr.Push(L, HeroInfo.GetInstance().PlayerArmyData[key].level);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetBattleField(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, UnitConst.GetInstance().BattleFieldConst);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetBattleFieldState(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (UnitConst.GetInstance().BattleFieldConst.ContainsKey(key) && UnitConst.GetInstance().BattleFieldConst[key].fightRecord.isFight)
		{
			LuaScriptMgr.Push(L, 1);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetAllTank(IntPtr L)
	{
		if (SenceManager.inst)
		{
			LuaScriptMgr.PushValue(L, SenceManager.inst.Tanks_Attack);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int isLandingBoxMouseUp(IntPtr L)
	{
		if (LandingBox.ins)
		{
			LuaScriptMgr.Push(L, LandingBox.ins.isLandingBoxMouseUp);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetSkillBox(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, NewbieGuidePanel._instance.box);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int StrikeBox(IntPtr L)
	{
		int id = (int)LuaDLL.lua_tonumber(L, 1);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
		NewbieGuidePanel._instance.StrikeBox(id, vector, vector2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsTaskCanRecieve(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (UnitConst.GetInstance().DailyTask[key].isCanRecieved)
		{
			LuaScriptMgr.Push(L, 1);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IsRecieved(IntPtr L)
	{
		int key = (int)LuaDLL.lua_tonumber(L, 1);
		if (UnitConst.GetInstance().DailyTask[key].isReceived)
		{
			LuaScriptMgr.Push(L, 1);
		}
		else
		{
			LuaScriptMgr.Push(L, 0);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowTowerAllUI(IntPtr L)
	{
		int type = (int)LuaDLL.lua_tonumber(L, 1);
		int angle = (int)LuaDLL.lua_tonumber(L, 2);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 4);
		NewbieGuideManage.isInBuildingToCameraMove = false;
		NewbieGuidePanel._instance.ShowTowerAllUI(type, angle, vector, unityObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowTower_InBuildingAllUI(IntPtr L)
	{
		int type = (int)LuaDLL.lua_tonumber(L, 1);
		int angle = (int)LuaDLL.lua_tonumber(L, 2);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 4);
		NewbieGuideManage.isInBuildingToCameraMove = true;
		NewbieGuidePanel._instance.ShowTowerAllUI(type, angle, vector, unityObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int SetNewBieGudieID(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		if (GameSetting.isEditor)
		{
			NGUIDebug.Log(new object[]
			{
				string.Format("告诉服务器当前组ID{0}", num)
			});
		}
		NewbieGuidePanel.guideIdByServer = num;
		NewbieGuideManage._instance.CS_NewGuide(num);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowButtonAllUI(IntPtr L)
	{
		int type = (int)LuaDLL.lua_tonumber(L, 1);
		int angle = (int)LuaDLL.lua_tonumber(L, 2);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 4);
		NewbieGuidePanel._instance.ShowButtonAllUI(type, angle, vector, unityObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int DianDi(IntPtr L)
	{
		int type = (int)LuaDLL.lua_tonumber(L, 1);
		int angle = (int)LuaDLL.lua_tonumber(L, 2);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 3);
		NewbieGuidePanel._instance.ShowDianJiDiMian(type, angle, vector);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int NextNewbi(IntPtr L)
	{
		string str = LuaDLL.lua_tostring(L, 1);
		NewbieGuideWrap.nextNewBi = str;
		LogManage.Log("设置 Lua 下一步：" + str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int SetCurNewBiFuncName(IntPtr L)
	{
		string curNewBiFuncName = LuaDLL.lua_tostring(L, 1);
		NewbieGuideWrap.CurNewBiFuncName = curNewBiFuncName;
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowPerson(IntPtr L)
	{
		int personId = (int)LuaDLL.lua_tonumber(L, 1);
		int buildingIndex = (int)LuaDLL.lua_tonumber(L, 2);
		NewbieGuidePanel._instance.ShowPerson(personId, buildingIndex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int NewEnemyAttack(IntPtr L)
	{
		int tankID = (int)LuaDLL.lua_tonumber(L, 1);
		int tankNum = (int)LuaDLL.lua_tonumber(L, 2);
		int tankLv = (int)LuaDLL.lua_tonumber(L, 3);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 4);
		if (NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.NewEnemyAttack(tankID, tankNum, tankLv, vector);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int NewSoliderAttack(IntPtr L)
	{
		int tankID = (int)LuaDLL.lua_tonumber(L, 1);
		int tankLv = (int)LuaDLL.lua_tonumber(L, 2);
		int tankStar = (int)LuaDLL.lua_tonumber(L, 3);
		int tankSkillLv = (int)LuaDLL.lua_tonumber(L, 4);
		Vector3 vector = LuaScriptMgr.GetVector3(L, 5);
		if (NewbieGuidePanel._instance)
		{
			NewbieGuidePanel._instance.SoliderAttack(tankID, tankLv, tankStar, tankSkillLv, vector);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int NewEnemyAttackStart(IntPtr L)
	{
		NewbieGuidePanel._instance.EnemyAttackStart();
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int BattlefieldReportBefore(IntPtr L)
	{
		NewbieGuidePanel._instance.isEnable = LuaDLL.lua_toboolean(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int OptionNextNewBi(IntPtr L)
	{
		NewbieGuideWrap.optionNextNewBi = LuaDLL.lua_tostring(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int OpenBattlefieldReport(IntPtr L)
	{
		NewbieGuidePanel._instance.isZhanbao = LuaDLL.lua_toboolean(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int ShowShield(IntPtr L)
	{
		GameObject unityObject = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		NewbieGuidePanel._instance.ShowShield(unityObject);
		return 1;
	}
}
