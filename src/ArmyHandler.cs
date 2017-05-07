using msg;
using System;

public class ArmyHandler
{
	private static Action updateFun;

	private static Action armyListFun;

	private static Action<int[]> endFun;

	private static int[] updatingItem;

	public static void CG_ArmsUpdateLevel(int armyId, int isMoney, Action _Fun)
	{
		CSArmyLevelUp cSArmyLevelUp = new CSArmyLevelUp();
		ArmyHandler.updateFun = _Fun;
		cSArmyLevelUp.armyId = armyId;
		cSArmyLevelUp.money = isMoney;
		ClientMgr.GetNet().SendHttp(3004, cSArmyLevelUp, null, null);
	}

	public static void GC_ArmsUpdateLevel(bool isError, Opcode opcode)
	{
		if (!isError && ArmyHandler.updateFun != null)
		{
			ArmyHandler.updateFun();
		}
	}

	public static void CG_CSArmyLevelUpEnd(int _isClear, Action<int[]> _Fun, params int[] itemIDs)
	{
		CSArmyLevelUpEnd cSArmyLevelUpEnd = new CSArmyLevelUpEnd();
		ArmyHandler.endFun = _Fun;
		cSArmyLevelUpEnd.itemId = itemIDs[0];
		cSArmyLevelUpEnd.isClear = _isClear;
		ArmyHandler.updatingItem = itemIDs;
		ClientMgr.GetNet().SendHttp(3014, cSArmyLevelUpEnd, new DataHandler.OpcodeHandler(ArmyHandler.GC_CSArmyLevelUpEnd), null);
	}

	private static void GC_CSArmyLevelUpEnd(bool isError, Opcode opcode)
	{
		if (ArmyHandler.endFun != null)
		{
			ArmyHandler.endFun(ArmyHandler.updatingItem);
		}
	}

	public static void CG_ArmsList(Action _Fun)
	{
		CSArmyList cSArmyList = new CSArmyList();
		ArmyHandler.armyListFun = _Fun;
		cSArmyList.typeId = 22;
		ClientMgr.GetNet().SendHttp(3006, cSArmyList, new DataHandler.OpcodeHandler(ArmyHandler.GC_ArmsList), null);
	}

	private static void GC_ArmsList(bool isError, Opcode opcode)
	{
		if (ArmyHandler.armyListFun != null)
		{
			ArmyHandler.armyListFun();
		}
	}
}
