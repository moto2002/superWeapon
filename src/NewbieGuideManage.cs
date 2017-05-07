using DG.Tweening;
using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewbieGuideManage : MonoBehaviour
{
	public static NewbieGuideManage _instance;

	public static List<int> btnIDList = new List<int>();

	public static bool isAllOpenbtn = false;

	public static List<int> btnID = new List<int>();

	public Tweener cc;

	public static bool isInBuildingToCameraMove;

	public bool LockTaskByNewBie;

	public bool MainNewbieStay;

	public bool AlreadyPassNewGuide;

	public void OnDestroy()
	{
		NewbieGuideManage._instance = null;
	}

	private void Awake()
	{
		NewbieGuideManage._instance = this;
	}

	public int GetNextGuideID(int id)
	{
		Dictionary<int, NewbieGuide> newbieGuide = UnitConst.GetInstance().newbieGuide;
		foreach (NewbieGuide current in newbieGuide.Values)
		{
			if (current.preGuild == id.ToString())
			{
				return current.id;
			}
		}
		return -1;
	}

	public GameObject GetBtnTransform(int id)
	{
		switch (id + 6)
		{
		case 0:
			if (T_CommandPanelManage._instance)
			{
				if (T_CommandPanelManage._instance.armyFuncPanel.allArmyList.Count > 1)
				{
					GameObject gameObject = T_CommandPanelManage._instance.armyFuncPanel.allArmyList[1].gameObject;
					if (gameObject && gameObject.activeInHierarchy)
					{
						return gameObject;
					}
				}
				return null;
			}
			return null;
		case 1:
			if (T_CommandPanelManage._instance)
			{
				if (T_CommandPanelManage._instance.armyFuncPanel.allArmyList.Count > 0)
				{
					GameObject gameObject2 = T_CommandPanelManage._instance.armyFuncPanel.allArmyList[0].gameObject;
					if (gameObject2 && gameObject2.activeInHierarchy)
					{
						return gameObject2;
					}
				}
				return null;
			}
			return null;
		case 2:
			if (FightPanelManager.inst && FightPanelManager.inst.skillUIList.Count > 0)
			{
				return FightPanelManager.inst.skillUIList[0].ga;
			}
			return null;
		case 3:
			return null;
		case 4:
			return null;
		case 5:
			return null;
		case 7:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.MainPanel_OpenBuildingStore))
			{
				GameObject gameObject3 = ButtonClick.AllButtonClick[EventManager.EventType.MainPanel_OpenBuildingStore];
				if (gameObject3 && gameObject3.activeInHierarchy)
				{
					return gameObject3;
				}
			}
			return null;
		case 8:
			if (BuildingStorePanelManage._instance)
			{
				GameObject defenseBtn = BuildingStorePanelManage._instance.defenseBtn;
				if (defenseBtn != null && defenseBtn.activeInHierarchy)
				{
					return defenseBtn;
				}
			}
			return null;
		case 9:
			if (BuildingStorePanelManage._instance && BuildingStorePanelManage._instance.sniperTower)
			{
				GameObject gameObject4 = BuildingStorePanelManage._instance.sniperTower.gameObject;
				if (gameObject4 && gameObject4.activeInHierarchy)
				{
					return gameObject4;
				}
			}
			return null;
		case 10:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject5 = T_CommandPanelManage._instance.btns[15];
				if (gameObject5 && gameObject5.activeInHierarchy)
				{
					return gameObject5;
				}
			}
			return null;
		case 11:
			if (SpyPanelManager.inst)
			{
				GameObject gameObject6 = SpyPanelManager.inst.attackBtn.gameObject;
				if (gameObject6 && gameObject6.activeInHierarchy)
				{
					return gameObject6;
				}
			}
			return null;
		case 12:
			if (SettlementManager.inst)
			{
				GameObject gameObject7 = SettlementManager.inst.Victorybtn.gameObject;
				if (gameObject7 && gameObject7.activeInHierarchy)
				{
					return gameObject7;
				}
			}
			return null;
		case 13:
			if (BuildingStorePanelManage._instance)
			{
				GameObject gameObject8 = BuildingStorePanelManage._instance.economyBtn.gameObject;
				if (gameObject8 != null && gameObject8.activeInHierarchy)
				{
					return gameObject8;
				}
			}
			return null;
		case 14:
			if (MainUIPanelManage._instance)
			{
				GameObject gameObject9 = MainUIPanelManage._instance.goToWorld.gameObject;
				if (gameObject9 && gameObject9.activeInHierarchy)
				{
					return gameObject9;
				}
			}
			return null;
		case 15:
			return null;
		case 16:
			if (BuildingStorePanelManage._instance)
			{
				GameObject gameObject10 = BuildingStorePanelManage._instance.supportBtn.gameObject;
				if (gameObject10 != null && gameObject10.activeInHierarchy)
				{
					return gameObject10;
				}
			}
			return null;
		case 17:
			if (ExpensePanelManage.inst)
			{
				GameObject gameObject11 = ExpensePanelManage.inst.expenseBtn.gameObject;
				if (gameObject11 && gameObject11.activeInHierarchy)
				{
					return gameObject11;
				}
			}
			return null;
		case 18:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject12 = T_CommandPanelManage._instance.btns[8];
				if (gameObject12 && gameObject12.activeInHierarchy)
				{
					return gameObject12;
				}
			}
			return null;
		case 20:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.WorldMap_Spy))
			{
				GameObject gameObject13 = ButtonClick.AllButtonClick[EventManager.EventType.WorldMap_Spy];
				if (gameObject13 && gameObject13.activeInHierarchy)
				{
					return gameObject13;
				}
			}
			return null;
		case 21:
			if (TipsManager.inst && TipsManager.inst.tipses[0].gameObject.activeInHierarchy)
			{
				GameObject gameObject14 = TipsManager.inst.tipses[0].transform.FindChild("Btn").gameObject;
				if (gameObject14 && gameObject14.activeInHierarchy)
				{
					return gameObject14;
				}
			}
			return null;
		case 22:
			if (SpyPanelManager.inst)
			{
				GameObject gameObject15 = SpyPanelManager.inst.backBtn.gameObject;
				if (gameObject15 && gameObject15.activeInHierarchy)
				{
					return gameObject15;
				}
			}
			return null;
		case 23:
			if (FightPanelManager.inst)
			{
				GameObject gameObject16 = FightPanelManager.inst.retreatBtn.gameObject;
				if (gameObject16 && gameObject16.activeInHierarchy)
				{
					return gameObject16;
				}
			}
			return null;
		case 24:
			return null;
		case 25:
			return null;
		case 26:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject17 = T_CommandPanelManage._instance.btns[1];
				if (gameObject17 && gameObject17.activeInHierarchy)
				{
					return gameObject17;
				}
			}
			return null;
		case 27:
			if (T_InfoPanelManage._instance)
			{
				GameObject gameObject18 = T_InfoPanelManage._instance.clickToUpdate.gameObject;
				if (gameObject18 && gameObject18.activeInHierarchy)
				{
					return gameObject18;
				}
			}
			return null;
		case 28:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject19 = T_CommandPanelManage._instance.btns[6];
				if (gameObject19 && gameObject19.activeInHierarchy)
				{
					return gameObject19;
				}
			}
			return null;
		case 29:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.ArmyNewControlpanel_Update))
			{
				GameObject gameObject20 = ButtonClick.AllButtonClick[EventManager.EventType.ArmyNewControlpanel_Update];
				if (gameObject20 && gameObject20.activeInHierarchy)
				{
					return gameObject20;
				}
			}
			return null;
		case 30:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject21 = T_CommandPanelManage._instance.btns[12];
				if (gameObject21 && gameObject21.activeInHierarchy)
				{
					return gameObject21;
				}
			}
			return null;
		case 36:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject22 = T_CommandPanelManage._instance.btns[7];
				if (gameObject22 && gameObject22.activeInHierarchy)
				{
					return gameObject22;
				}
			}
			return null;
		case 37:
			return null;
		case 38:
			return null;
		case 39:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject23 = T_CommandPanelManage._instance.btns[10];
				if (gameObject23 && gameObject23.activeInHierarchy)
				{
					return gameObject23;
				}
			}
			return null;
		case 40:
			if (AideCompound._instance)
			{
				GameObject gameObject24 = AideCompound._instance.allAideUI[0].gameObject;
				if (gameObject24 && gameObject24.activeInHierarchy)
				{
					return gameObject24;
				}
			}
			return null;
		case 41:
			if (AideSend._instance)
			{
				GameObject gameObject25 = AideSend._instance.dispatchBtn.gameObject;
				if (gameObject25 && gameObject25.activeInHierarchy)
				{
					return gameObject25;
				}
			}
			return null;
		case 42:
			if (NewbieGuidePanel._instance)
			{
				GameObject gameObject26 = NewbieGuidePanel._instance.m_btnLeft.gameObject;
				if (gameObject26 && gameObject26.activeInHierarchy)
				{
					return gameObject26;
				}
			}
			return null;
		case 43:
			if (UIManager.inst)
			{
				GameObject senderBtn = UIManager.inst.senderBtn;
				if (senderBtn && senderBtn.activeInHierarchy)
				{
					return senderBtn;
				}
			}
			return null;
		case 44:
			if (BuildingStorePanelManage._instance)
			{
				GameObject coffersBtn = BuildingStorePanelManage._instance.coffersBtn;
				if (coffersBtn != null && coffersBtn.activeInHierarchy)
				{
					return coffersBtn;
				}
			}
			return null;
		case 46:
			if (WorldMapManager.inst)
			{
				GameObject gotoPlayer = WorldMapManager.inst.gotoPlayer;
				if (gotoPlayer && gotoPlayer.activeInHierarchy)
				{
					return gotoPlayer;
				}
			}
			return null;
		case 47:
			if (BuildingStorePanelManage._instance)
			{
				GameObject satelliteBtn = BuildingStorePanelManage._instance.satelliteBtn;
				if (satelliteBtn != null && satelliteBtn.activeInHierarchy)
				{
					return satelliteBtn;
				}
			}
			return null;
		case 49:
			if (BuildingStorePanelManage._instance)
			{
				GameObject wallBtn = BuildingStorePanelManage._instance.WallBtn;
				if (wallBtn != null && wallBtn.activeInHierarchy)
				{
					return wallBtn;
				}
			}
			return null;
		case 52:
			if (BuildingStorePanelManage._instance)
			{
				GameObject drillingBtn = BuildingStorePanelManage._instance.DrillingBtn;
				if (drillingBtn != null && drillingBtn.activeInHierarchy)
				{
					return drillingBtn;
				}
			}
			return null;
		case 53:
			if (BuildingStorePanelManage._instance)
			{
				GameObject tankfarmBtn = BuildingStorePanelManage._instance.tankfarmBtn;
				if (tankfarmBtn != null && tankfarmBtn.activeInHierarchy)
				{
					return tankfarmBtn;
				}
			}
			return null;
		case 54:
			if (NewTollgateManage.inst && NewTollgateManage.inst.tollgateItemList.Count > 0)
			{
				GameObject gameObject27 = NewTollgateManage.inst.tollgateItemList[0];
				if (gameObject27 && gameObject27.activeInHierarchy)
				{
					return gameObject27;
				}
			}
			return null;
		case 55:
			if (NTollgateManage.inst)
			{
				GameObject battleFightUIInfoBtn = NTollgateManage.inst.BattleFightUIInfoBtn;
				if (battleFightUIInfoBtn && battleFightUIInfoBtn.activeInHierarchy)
				{
					return battleFightUIInfoBtn;
				}
			}
			return null;
		case 56:
			return null;
		case 57:
			return null;
		case 58:
			if (NewTollgateManage.inst && NewTollgateManage.inst.tollgateItemList.Count > 0)
			{
				GameObject gameObject28 = NewTollgateManage.inst.tollgateItemList[1];
				if (gameObject28 && gameObject28.activeInHierarchy)
				{
					return gameObject28;
				}
			}
			return null;
		case 59:
			if (NewTollgateManage.inst && NewTollgateManage.inst.tollgateItemList.Count > 0)
			{
				GameObject gameObject29 = NewTollgateManage.inst.tollgateItemList[2];
				if (gameObject29 && gameObject29.activeInHierarchy)
				{
					return gameObject29;
				}
			}
			return null;
		case 62:
			if (BuildingStorePanelManage._instance)
			{
				GameObject chariotBtn = BuildingStorePanelManage._instance.chariotBtn;
				if (chariotBtn != null && chariotBtn.activeInHierarchy)
				{
					return chariotBtn;
				}
			}
			return null;
		case 64:
			if (BuildingStorePanelManage._instance)
			{
				GameObject bingYing = BuildingStorePanelManage._instance.BingYing;
				if (bingYing != null && bingYing.activeInHierarchy)
				{
					return bingYing;
				}
			}
			return null;
		case 66:
			if (BuildingStorePanelManage._instance)
			{
				GameObject steelWarehouseBtn = BuildingStorePanelManage._instance.steelWarehouseBtn;
				if (steelWarehouseBtn != null && steelWarehouseBtn.activeInHierarchy)
				{
					return steelWarehouseBtn;
				}
			}
			return null;
		case 67:
			return null;
		case 70:
			if (T_InfoPanelManage._instance)
			{
				GameObject jianzhushengjiClose = T_InfoPanelManage._instance.jianzhushengjiClose;
				if (jianzhushengjiClose && jianzhushengjiClose.activeInHierarchy)
				{
					return null;
				}
			}
			return null;
		case 73:
			if (NBattleManage.inst)
			{
				GameObject closeBtn = NBattleManage.inst.closeBtn;
				if (closeBtn && closeBtn.activeInHierarchy)
				{
					return closeBtn;
				}
			}
			return null;
		case 74:
			if (NewarmyInfo.inst)
			{
				GameObject xinbingzhongClose = NewarmyInfo.inst.xinbingzhongClose;
				if (xinbingzhongClose && xinbingzhongClose.activeInHierarchy)
				{
					return xinbingzhongClose;
				}
			}
			return null;
		case 75:
			if (TaskGetawardNew.init)
			{
				GameObject btn = TaskGetawardNew.init.btn;
				if (btn && btn.activeInHierarchy)
				{
					return btn;
				}
			}
			return null;
		case 76:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject30 = T_CommandPanelManage._instance.btns[16];
				if (gameObject30 && gameObject30.activeInHierarchy)
				{
					return gameObject30;
				}
			}
			return null;
		case 85:
			if (BuildingStorePanelManage._instance)
			{
				GameObject jiqiangta = BuildingStorePanelManage._instance.jiqiangta;
				if (jiqiangta != null && jiqiangta.activeInHierarchy)
				{
					return jiqiangta;
				}
			}
			return null;
		case 86:
			if (NewFightPanelMange.inst && NewFightPanelMange.inst.battleItem.Count > 0)
			{
				GameObject gameObject31 = NewFightPanelMange.inst.battleItem[0];
				if (gameObject31 && gameObject31.activeInHierarchy)
				{
					return gameObject31;
				}
			}
			return null;
		case 88:
			if (NewTollgateManage.inst && NewTollgateManage.inst.tollgateItemList.Count > 0)
			{
				GameObject gameObject32 = NewTollgateManage.inst.tollgateItemList[3];
				if (gameObject32 && gameObject32.activeInHierarchy)
				{
					return gameObject32;
				}
			}
			return null;
		case 89:
			if (NewTollgateManage.inst && NewTollgateManage.inst.tollgateItemList.Count > 0)
			{
				GameObject gameObject33 = NewTollgateManage.inst.tollgateItemList[4];
				if (gameObject33 && gameObject33.activeInHierarchy)
				{
					return gameObject33;
				}
			}
			return null;
		case 90:
			if (NewTollgateManage.inst && NewTollgateManage.inst.tollgateItemList.Count > 0)
			{
				GameObject gameObject34 = NewTollgateManage.inst.tollgateItemList[5];
				if (gameObject34 && gameObject34.activeInHierarchy)
				{
					return gameObject34;
				}
			}
			return null;
		case 91:
			if (NewTollgateManage.inst && NewTollgateManage.inst.tollgateItemList.Count > 0)
			{
				GameObject gameObject35 = NewTollgateManage.inst.tollgateItemList[6];
				if (gameObject35 && gameObject35.activeInHierarchy)
				{
					return gameObject35;
				}
			}
			return null;
		case 92:
			if (NewTollgateManage.inst)
			{
				GameObject gameObject36 = NewTollgateManage.inst.tollgateItemList[7];
				if (gameObject36 && gameObject36.activeInHierarchy)
				{
					return gameObject36;
				}
			}
			return null;
		case 93:
			if (NewTollgateManage.inst)
			{
				GameObject gameObject37 = NewTollgateManage.inst.tollgateItemList[8];
				if (gameObject37 && gameObject37.activeInHierarchy)
				{
					return gameObject37;
				}
			}
			return null;
		case 95:
			if (BuildingStorePanelManage._instance)
			{
				GameObject kuangshiBtn = BuildingStorePanelManage._instance.kuangshiBtn;
				if (kuangshiBtn != null && kuangshiBtn.activeInHierarchy)
				{
					return kuangshiBtn;
				}
			}
			return null;
		case 96:
			if (BuildingStorePanelManage._instance)
			{
				GameObject fadianchang = BuildingStorePanelManage._instance.fadianchang;
				if (fadianchang != null && fadianchang.activeInHierarchy)
				{
					return fadianchang;
				}
			}
			return null;
		case 97:
			return null;
		case 98:
			return null;
		case 99:
			return null;
		case 100:
			if (NewFightPanelMange.inst)
			{
				GameObject closeBtn2 = NewFightPanelMange.inst.CloseBtn;
				if (closeBtn2 && closeBtn2.activeInHierarchy)
				{
					return closeBtn2;
				}
			}
			return null;
		case 101:
			if (SpyPanelManager.inst)
			{
				GameObject closeBtn3 = SpyPanelManager.inst.closeBtn;
				if (closeBtn3 && closeBtn3.activeInHierarchy)
				{
					return closeBtn3;
				}
			}
			return null;
		case 103:
			if (MainUIPanelManage._instance)
			{
				GameObject addArrmybtn = MainUIPanelManage._instance.addArrmybtn;
				if (addArrmybtn && addArrmybtn.activeInHierarchy)
				{
					return addArrmybtn;
				}
			}
			return null;
		case 104:
			if (MessagePanelManage.inst)
			{
				GameObject gameObject38 = GameTools.GetTranformChildByName(MessagePanelManage.inst.gameObject, "btn1").gameObject;
				if (gameObject38 && gameObject38.activeInHierarchy)
				{
					return gameObject38;
				}
			}
			return null;
		case 105:
			if (FightPanelManager.inst)
			{
				GameObject boxSkill = FightPanelManager.inst.BoxSkill;
				if (boxSkill && boxSkill.activeInHierarchy)
				{
					return boxSkill;
				}
			}
			return null;
		case 107:
			if (SynopsisTaskPanelManager.Inis)
			{
				GameObject jiqingqianwang = SynopsisTaskPanelManager.Inis.jiqingqianwang;
				if (jiqingqianwang && jiqingqianwang.activeInHierarchy)
				{
					return jiqingqianwang;
				}
			}
			return null;
		case 108:
			if (NewbieGuidePanel._instance)
			{
				GameObject btnRight = NewbieGuidePanel._instance.m_btnRight;
				if (btnRight && btnRight.activeInHierarchy)
				{
					return btnRight;
				}
			}
			return null;
		case 110:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject39 = T_CommandPanelManage._instance.btns[18];
				if (gameObject39 && gameObject39.activeInHierarchy)
				{
					return gameObject39;
				}
			}
			return null;
		case 111:
			if (NTollgateManage.inst)
			{
				GameObject goBack = NTollgateManage.inst.goBack;
				if (goBack && goBack.activeInHierarchy)
				{
					return goBack;
				}
			}
			return null;
		case 112:
			if (NBattleManage.inst)
			{
				GameObject closeBtn4 = NTollgateManage.inst.CloseBtn;
				if (closeBtn4 && closeBtn4.activeInHierarchy)
				{
					return closeBtn4;
				}
			}
			return null;
		case 113:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject40 = T_CommandPanelManage._instance.btns[20];
				if (gameObject40 && gameObject40.activeInHierarchy)
				{
					return gameObject40;
				}
			}
			return null;
		case 114:
			if (T_CommandPanelManage._instance)
			{
				GameObject gameObject41 = T_CommandPanelManage._instance.btns[21];
				if (gameObject41 && gameObject41.activeInHierarchy)
				{
					return gameObject41;
				}
			}
			return null;
		case 116:
			if (SkillEquipmentManage.inst && SkillEquipmentManage.inst.skillList.Count > 0)
			{
				GameObject ga = SkillEquipmentManage.inst.skillList[0].ga;
				if (ga && ga.activeInHierarchy)
				{
					return ga;
				}
			}
			return null;
		case 117:
			if (SkillEquipmentManage.inst)
			{
				GameObject closeBtn5 = SkillEquipmentManage.inst.closeBtn;
				if (closeBtn5 && closeBtn5.activeInHierarchy)
				{
					return closeBtn5;
				}
			}
			return null;
		case 118:
			if (BuildingStorePanelManage._instance)
			{
				GameObject kejiZhongxin = BuildingStorePanelManage._instance.kejiZhongxin;
				if (kejiZhongxin != null && kejiZhongxin.activeInHierarchy)
				{
					return kejiZhongxin;
				}
			}
			return null;
		case 119:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.RandomNamePanel_RandomNameSure))
			{
				GameObject gameObject42 = ButtonClick.AllButtonClick[EventManager.EventType.RandomNamePanel_RandomNameSure];
				if (gameObject42 != null && gameObject42.activeInHierarchy)
				{
					return gameObject42;
				}
			}
			return null;
		case 120:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.ArmyNewControlpanel_Close))
			{
				GameObject gameObject43 = ButtonClick.AllButtonClick[EventManager.EventType.ArmyNewControlpanel_Close];
				if (gameObject43 != null && gameObject43.activeInHierarchy)
				{
					return gameObject43;
				}
			}
			return null;
		case 121:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.SkillExtractPanel_Close))
			{
				GameObject gameObject44 = ButtonClick.AllButtonClick[EventManager.EventType.SkillExtractPanel_Close];
				if (gameObject44 != null && gameObject44.activeInHierarchy)
				{
					return gameObject44;
				}
			}
			return null;
		case 122:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.SkillExtractPanel_CloseSkillShow))
			{
				GameObject gameObject45 = ButtonClick.AllButtonClick[EventManager.EventType.SkillExtractPanel_CloseSkillShow];
				if (gameObject45 != null && gameObject45.activeInHierarchy)
				{
					return gameObject45;
				}
			}
			return null;
		case 123:
			if (BuildingStorePanelManage._instance)
			{
				GameObject feijichang = BuildingStorePanelManage._instance.feijichang;
				if (feijichang != null && feijichang.activeInHierarchy)
				{
					return feijichang;
				}
			}
			return null;
		case 124:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.MainPanel_CloseBuildingMovingOrInBuilding))
			{
				GameObject gameObject46 = ButtonClick.AllButtonClick[EventManager.EventType.MainPanel_CloseBuildingMovingOrInBuilding];
				if (gameObject46 && gameObject46.activeInHierarchy)
				{
					return gameObject46;
				}
			}
			return null;
		case 125:
			if (BuildingStorePanelManage._instance)
			{
				GameObject junTunZongBu = BuildingStorePanelManage._instance.JunTunZongBu;
				if (junTunZongBu != null && junTunZongBu.activeInHierarchy)
				{
					return junTunZongBu;
				}
			}
			return null;
		case 126:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.MainPanel_AddBuildingQueue))
			{
				GameObject gameObject47 = ButtonClick.AllButtonClick[EventManager.EventType.MainPanel_AddBuildingQueue];
				if (gameObject47 != null && gameObject47.activeInHierarchy)
				{
					return gameObject47;
				}
			}
			return null;
		case 127:
			if (MainUIPanelManage._instance)
			{
				GameObject gameObject48 = MainUIPanelManage._instance.goToPVP.gameObject;
				if (gameObject48 && gameObject48.activeInHierarchy)
				{
					return gameObject48;
				}
			}
			return null;
		case 128:
			if (ButtonClick.AllButtonClick.ContainsKey(EventManager.EventType.MainPanel_OpenActivity))
			{
				GameObject gameObject49 = ButtonClick.AllButtonClick[EventManager.EventType.MainPanel_OpenActivity];
				if (gameObject49 && gameObject49.activeInHierarchy)
				{
					return gameObject49;
				}
			}
			return null;
		case 129:
			if (ActivityPanelManager.ins && ActivityPanelManager.ins.RecieveActiveBtnBySevenDay && ActivityPanelManager.ins.RecieveActiveBtnBySevenDay.activeInHierarchy)
			{
				return ActivityPanelManager.ins.RecieveActiveBtnBySevenDay;
			}
			return null;
		case 131:
			if (BuildingStorePanelManage._instance)
			{
				GameObject steelworksBtn = BuildingStorePanelManage._instance.steelworksBtn;
				if (steelworksBtn != null && steelworksBtn.activeInHierarchy)
				{
					return steelworksBtn;
				}
			}
			return null;
		}
		if (ButtonClick.AllButtonClick.ContainsKey((EventManager.EventType)id))
		{
			GameObject gameObject50 = ButtonClick.AllButtonClick[(EventManager.EventType)id];
			if (gameObject50 != null && gameObject50.activeInHierarchy)
			{
				return gameObject50;
			}
		}
		return null;
	}

	public GameObject GetTower(int id)
	{
		if (id == 12)
		{
			List<T_Tower> list = (from a in SenceManager.inst.towers
			where a.index == 12
			select a).ToList<T_Tower>();
			T_Tower t_Tower = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].myTanks.Count == 0)
				{
					t_Tower = list[i];
				}
			}
			if (t_Tower != null)
			{
				return t_Tower.gameObject;
			}
		}
		else
		{
			T_Tower t_Tower2 = SenceManager.inst.towers.FirstOrDefault((T_Tower a) => a.index == id);
			if (t_Tower2 != null)
			{
				return t_Tower2.gameObject;
			}
		}
		return null;
	}

	public GameObject GetJiJieDianTower(int posIndex)
	{
		(from a in SenceManager.inst.towers
		where a.index == 12
		select a).ToList<T_Tower>();
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i].index == 12 && Mathf.Abs(SenceManager.inst.towers[i].posIdx) == posIndex)
			{
				return SenceManager.inst.towers[i].ga;
			}
		}
		return null;
	}

	public GameObject GetTower(int index, bool isInBuildingCD)
	{
		int count = SenceManager.inst.towers.Count;
		for (int i = 0; i < count; i++)
		{
			if (SenceManager.inst.towers[i] && SenceManager.inst.towers[i].index == index && SenceManager.inst.towers[i].buildingState == T_Tower.TowerBuildingEnum.InBuilding == isInBuildingCD)
			{
				return SenceManager.inst.towers[i].ga;
			}
		}
		return null;
	}

	public void CameraMoveTo(int towerIndex)
	{
		LogManage.LogError("相机移动了哦");
		List<T_Tower> list = (from a in SenceManager.inst.towers
		where a.index == towerIndex
		select a).ToList<T_Tower>();
		T_Tower t_Tower = null;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].myTanks.Count == 0)
			{
				t_Tower = list[i];
			}
		}
		if (t_Tower == null)
		{
			LogManage.Log(string.Format("没找到{0}", UnitConst.GetInstance().buildingConst[towerIndex].name));
			return;
		}
		NewbieGuidePanel._instance.camerOldPos = CameraControl.inst.Tr;
		float angle;
		if (NewbieGuideManage.isInBuildingToCameraMove || CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			angle = 50f;
		}
		else
		{
			angle = 50f;
		}
		Vector3 cameraMoveEndPos = HUDTextTool.inst.GetCameraMoveEndPos(t_Tower.transform.position, CameraControl.inst.Tr.position, angle);
		LogManage.LogError(cameraMoveEndPos);
		CameraControl.inst.openDragCameraAndInertia = false;
		this.cc = CameraControl.inst.Tr.DOMove(cameraMoveEndPos, 0.26f, false);
		HUDTextTool.inst.GetCenterPos();
	}

	public void CameraMoveTo(Transform build)
	{
		NewbieGuidePanel._instance.camerOldPos = CameraControl.inst.Tr;
		float num;
		if (NewbieGuideManage.isInBuildingToCameraMove || CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			num = 60f;
		}
		else
		{
			num = 50f;
		}
		Vector3 cameraMoveEndPos = HUDTextTool.inst.GetCameraMoveEndPos(build.position, CameraControl.inst.Tr.position, num);
		LogManage.LogError(string.Format("Angel：{0} cameraTaget: {1}", num, cameraMoveEndPos));
		this.cc = CameraControl.inst.Tr.DOMove(cameraMoveEndPos, 0.26f, false);
		CameraControl.inst.openDragCameraAndInertia = false;
		HUDTextTool.inst.GetCenterPos();
	}

	public void CS_NewGuide(int id)
	{
		if (this.AlreadyPassNewGuide)
		{
			id = 10000;
		}
		CSNewGuide cSNewGuide = new CSNewGuide();
		cSNewGuide.guideId = id;
		cSNewGuide.taskGuideId = NewbieGuidePanel.TaskGuideID;
		ClientMgr.GetNet().SendHttp(9008, cSNewGuide, null, null);
	}

	public GameObject OnSetTeXiao(bool isObjShow = true)
	{
		if (SpyPanelManager.inst != null)
		{
			SpyPanelManager.inst.OnSetTeXiao();
		}
		return null;
	}
}
