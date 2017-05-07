using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmyFuncHandler : MonoBehaviour
{
	private static Action Func;

	private static List<GameObject> DoJobObj = new List<GameObject>();

	private static List<T_Tower> DoJobParent = new List<T_Tower>();

	public static void CG_ArmsConfigure(long buildingId, int itemId, int itemNum, Action fuc = null)
	{
		CSArmyConfigure cSArmyConfigure = new CSArmyConfigure();
		cSArmyConfigure.buildingId = buildingId;
		cSArmyConfigure.itemId = itemId;
		cSArmyConfigure.num = itemNum;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(3000, cSArmyConfigure, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure), null);
	}

	public static void CG_CancelConfigureArmy(long buildingId, int itemId, Action fuc = null)
	{
		CSCancelConfigureArmy cSCancelConfigureArmy = new CSCancelConfigureArmy();
		cSCancelConfigureArmy.buildingId = buildingId;
		cSCancelConfigureArmy.armyId = itemId;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(3018, cSCancelConfigureArmy, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure), null);
	}

	public static void CG_SellConfiguredArmy(long buildingId, int itemId, Action fuc = null)
	{
		CSSellArmy cSSellArmy = new CSSellArmy();
		cSSellArmy.buildingId = buildingId;
		cSSellArmy.armyId = itemId;
		cSSellArmy.num = 1;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(3020, cSSellArmy, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure), null);
	}

	public static void CG_SoliderConfigure(int id, Action fuc = null)
	{
		CSSoldierConfigure cSSoldierConfigure = new CSSoldierConfigure();
		cSSoldierConfigure.id = id;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(2408, cSSoldierConfigure, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure), null);
	}

	public static void CG_CancelConfigureSolider(int itemID, Action fuc = null)
	{
		CScancelConfigSoldier cScancelConfigSoldier = new CScancelConfigSoldier();
		cScancelConfigSoldier.id = itemID;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(2416, cScancelConfigSoldier, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure), null);
	}

	public static void CG_CSArmyConfigureEnd(long buildingID, int itemID, int isRightNow, Action fuc = null)
	{
		CSArmyConfigureEnd cSArmyConfigureEnd = new CSArmyConfigureEnd();
		cSArmyConfigureEnd.buildingId = buildingID;
		cSArmyConfigureEnd.armyId = itemID;
		cSArmyConfigureEnd.isRightNow = isRightNow;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(3016, cSArmyConfigureEnd, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure), null);
	}

	public static void CG_CSSoliderConfigureEnd(int soliderID, int isRightNow, Action fuc = null)
	{
		CSSoldierConfigureEnd cSSoldierConfigureEnd = new CSSoldierConfigureEnd();
		cSSoldierConfigureEnd.id = soliderID;
		cSSoldierConfigureEnd.type = isRightNow;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(2414, cSSoldierConfigureEnd, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigure), null);
	}

	public static void CG_ArmsConfigureAuto(int Money, int type, Action fuc = null)
	{
		CSArmyConfigureAuto cSArmyConfigureAuto = new CSArmyConfigureAuto();
		cSArmyConfigureAuto.id = 1;
		cSArmyConfigureAuto.money = Money;
		cSArmyConfigureAuto.type = type;
		ArmyFuncHandler.Func = fuc;
		ClientMgr.GetNet().SendHttp(3008, cSArmyConfigureAuto, new DataHandler.OpcodeHandler(ArmyFuncHandler.GC_ArmsConfigureAuto), null);
	}

	private static void GC_ArmsConfigureAuto(bool isError, Opcode opcode)
	{
		ArmyFuncHandler.GC_ArmsConfigure(isError, opcode);
	}

	public static void GC_ArmsConfigure(bool isError, Opcode opcode)
	{
		if (ArmyFuncHandler.Func != null)
		{
			ArmyFuncHandler.Func();
			ArmyFuncHandler.Func = null;
		}
	}
}
