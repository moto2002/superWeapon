using System;
using System.Linq;
using UnityEngine;

public class ClickTimeUIBtn : MonoBehaviour
{
	public static ClickTimeUIBtn inst;

	public bool isVip;

	[HideInInspector]
	public static bool IsVip;

	private ButtonClick buyTimeAddBuidlingEnd;

	public void Awake()
	{
		ClickTimeUIBtn.inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.TimeEndClick, new EventManager.VoidDelegate(this.ClickTime));
		EventManager.Instance.AddEvent(EventManager.EventType.TimeEndClick_ArmyFuncEnd, new EventManager.VoidDelegate(this.ClickTime_ArmyFuncEnd));
	}

	private void OnEnable()
	{
		if (NetMgr.inst)
		{
			NetMgr.inst.NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10007)
		{
		}
	}

	private void OnDisable()
	{
		if (NetMgr.inst)
		{
			NetMgr.inst.NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	public void ClickTime(GameObject ga)
	{
		SenceManager.inst.ShowTowerR(false, null);
		int cdType = 0;
		long num = 0L;
		int index = 0;
		int num2 = 0;
		int configureType = 0;
		if (T_CommandPanelManage._instance.SelCharacter.roleType == Enum_RoleType.tower)
		{
			num = (T_CommandPanelManage._instance.SelCharacter as T_Tower).id;
			BuildingHandler.towerId = (T_CommandPanelManage._instance.SelCharacter as T_Tower).id;
			index = (T_CommandPanelManage._instance.SelCharacter as T_Tower).posIdx;
			num2 = (T_CommandPanelManage._instance.SelCharacter as T_Tower).index;
			if (UnitConst.GetInstance().buildingConst[num2].secType == 6 && HeroInfo.GetInstance().armyBuildingCDTime > 0L)
			{
				cdType = 1;
				T_Tower t_Tower = SenceManager.inst.towers.Single((T_Tower a) => a.id == HeroInfo.GetInstance().armyBuildingCDTime_BuildingID);
				BuildingHandler.towerId = t_Tower.id;
				num = t_Tower.id;
				index = t_Tower.posIdx;
				num2 = t_Tower.index;
			}
			else if (UnitConst.GetInstance().buildingConst[num2].secType == 21 && HeroInfo.GetInstance().airBuildingCDTime > 0L)
			{
				cdType = 1;
				T_Tower t_Tower2 = SenceManager.inst.towers.Single((T_Tower a) => a.id == HeroInfo.GetInstance().airBuildingCDTime_BuildingID);
				BuildingHandler.towerId = t_Tower2.id;
				num = t_Tower2.id;
				index = t_Tower2.posIdx;
				num2 = t_Tower2.index;
			}
			else if (HeroInfo.GetInstance().BuildCD.Contains(num))
			{
				cdType = 1;
			}
		}
		else if (T_CommandPanelManage._instance.SelCharacter.roleType == Enum_RoleType.Res)
		{
			cdType = 4;
			num = (T_CommandPanelManage._instance.SelCharacter as T_Res).id;
			index = (T_CommandPanelManage._instance.SelCharacter as T_Res).posIndex;
			num2 = (T_CommandPanelManage._instance.SelCharacter as T_Res).index;
		}
		CalcMoneyHandler.CSCalcMoney(1, cdType, index, num, num2, configureType, new Action<bool, int>(this.CalcMoneyCallBack));
	}

	private void ClickTime_ArmyFuncEnd(GameObject ga)
	{
		if (T_CommandPanelManage._instance.SelCharacter.roleType == Enum_RoleType.tower)
		{
			long id = (T_CommandPanelManage._instance.SelCharacter as T_Tower).id;
			BuildingHandler.towerId = (T_CommandPanelManage._instance.SelCharacter as T_Tower).id;
			int posIdx = (T_CommandPanelManage._instance.SelCharacter as T_Tower).posIdx;
			int index = (T_CommandPanelManage._instance.SelCharacter as T_Tower).index;
			if (HeroInfo.GetInstance().IsArmyFuncingBuilding(id))
			{
				CalcMoneyHandler.CSCalcMoney(1, 7, posIdx, id, index, 2, new Action<bool, int>(this.CalcMoneyCallBack_FuncArmyEnd));
			}
			else if (UnitConst.GetInstance().buildingConst[T_CommandPanelManage._instance.tower.index].secType == 15)
			{
				CalcMoneyHandler.CSCalcMoney(1, 9, posIdx, id, index, 2, new Action<bool, int>(this.CalcMoneyCallBack_FuncArmyEnd));
			}
		}
	}

	private void CalcMoneyCallBack_FuncArmyEnd(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				ShopPanelManage.ShowHelp_NoRMB(null, null);
				return;
			}
			if (T_CommandPanelManage._instance.tower)
			{
				if (HeroInfo.GetInstance().IsArmyFuncingBuilding(T_CommandPanelManage._instance.tower.id))
				{
					ArmyFuncHandler.CG_CSArmyConfigureEnd(T_CommandPanelManage._instance.tower.id, 0, 1, delegate
					{
						HUDTextTool.inst.NextLuaCall("立即完成配兵", new object[0]);
					});
					if (T_CommandPanelManage._instance)
					{
						T_CommandPanelManage._instance.OpenMainPanel();
					}
				}
				else if (UnitConst.GetInstance().buildingConst[T_CommandPanelManage._instance.tower.index].secType == 15)
				{
					ArmyFuncHandler.CG_CSSoliderConfigureEnd(HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.soldierItemId, 1, delegate
					{
						HUDTextTool.inst.NextLuaCall("立即完成配兵", new object[0]);
					});
					if (T_CommandPanelManage._instance)
					{
						T_CommandPanelManage._instance.OpenMainPanel();
					}
				}
			}
			if (T_CommandPanelManage._instance)
			{
				T_CommandPanelManage._instance.gameObject.SetActive(false);
			}
			if (T_CommandPanelManage._instance.SelCharacter.roleType == Enum_RoleType.Res)
			{
				T_CommandPanelManage._instance.OpenMainPanel();
			}
		}
	}

	private void CalcMoneyCallBack(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				ShopPanelManage.ShowHelp_NoRMB(null, null);
				return;
			}
			if (T_CommandPanelManage._instance.tower)
			{
				if (UnitConst.GetInstance().buildingConst[T_CommandPanelManage._instance.tower.index].secType == 6)
				{
					SenceManager.inst.towers.Single((T_Tower a) => a.id == HeroInfo.GetInstance().armyBuildingCDTime_BuildingID).SenndBuildComplete(money, "点  立即结束", false);
				}
				else if (UnitConst.GetInstance().buildingConst[T_CommandPanelManage._instance.tower.index].secType == 21)
				{
					SenceManager.inst.towers.Single((T_Tower a) => a.id == HeroInfo.GetInstance().airBuildingCDTime_BuildingID).SenndBuildComplete(money, "点  立即结束", false);
				}
				else if (HeroInfo.GetInstance().BuildCD.Contains(T_CommandPanelManage._instance.tower.id))
				{
					T_CommandPanelManage._instance.tower.SenndBuildComplete(money, "点  立即结束", false);
				}
			}
			else if (T_CommandPanelManage._instance.res)
			{
				T_CommandPanelManage._instance.res.SenndResComplete(money, false);
			}
			if (T_CommandPanelManage._instance)
			{
				T_CommandPanelManage._instance.gameObject.SetActive(false);
			}
			if (T_CommandPanelManage._instance.SelCharacter.roleType == Enum_RoleType.Res)
			{
				T_CommandPanelManage._instance.OpenMainPanel();
			}
		}
	}
}
