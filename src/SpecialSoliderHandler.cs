using msg;
using System;

public class SpecialSoliderHandler
{
	private static Action Func;

	private static Action levelUp;

	private static Action UpStar;

	private static Action ReliveSolider;

	private static Action UseReliveRMB;

	private static Action BuySoliderByRmb;

	public static void CS_SkillUpSet(long id, Action fucn = null)
	{
		SpecialSoliderHandler.Func = fucn;
		CSSoldierSkillUp cSSoldierSkillUp = new CSSoldierSkillUp();
		cSSoldierSkillUp.id = id;
		ClientMgr.GetNet().SendHttp(2404, cSSoldierSkillUp, new DataHandler.OpcodeHandler(SpecialSoliderHandler.OnHttpShow), null);
	}

	public static void OnHttpShow(bool isError, Opcode opcode)
	{
		if (SpecialSoliderHandler.Func != null)
		{
			SpecialSoliderHandler.Func();
		}
	}

	public static void CS_UseItemToAddExp(long soliderId, int itemid, int count, Action func = null)
	{
		CSUseItemToAddExp cSUseItemToAddExp = new CSUseItemToAddExp();
		cSUseItemToAddExp.soldierId = soliderId;
		cSUseItemToAddExp.itemId = itemid;
		cSUseItemToAddExp.num = count;
		SpecialSoliderHandler.levelUp = func;
		ClientMgr.GetNet().SendHttp(2412, cSUseItemToAddExp, new DataHandler.OpcodeHandler(SpecialSoliderHandler.LevelUp), null);
	}

	public static void LevelUp(bool isError, Opcode opcode)
	{
		if (SpecialSoliderHandler.levelUp != null)
		{
			SpecialSoliderHandler.levelUp();
		}
	}

	public static void CS_SoldierUpStar(long id, Action func)
	{
		CSSoldierUpStar cSSoldierUpStar = new CSSoldierUpStar();
		cSSoldierUpStar.id = id;
		SpecialSoliderHandler.UpStar = func;
		ClientMgr.GetNet().SendHttp(2402, cSSoldierUpStar, new DataHandler.OpcodeHandler(SpecialSoliderHandler.UpStarAciton), null);
	}

	public static void UpStarAciton(bool isError, Opcode opcode)
	{
		if (SpecialSoliderHandler.UpStar != null)
		{
			SpecialSoliderHandler.UpStar();
		}
	}

	public static void CS_SoldierRelive(long id, Action func)
	{
		CSSoldierRelive cSSoldierRelive = new CSSoldierRelive();
		SpecialSoliderHandler.ReliveSolider = func;
		cSSoldierRelive.id = id;
		ClientMgr.GetNet().SendHttp(2406, cSSoldierRelive, new DataHandler.OpcodeHandler(SpecialSoliderHandler.ReliveTimeEnd), null);
	}

	public static void ReliveTimeEnd(bool isError, Opcode opcode)
	{
		if (SpecialSoliderHandler.ReliveSolider != null)
		{
			SpecialSoliderHandler.ReliveSolider();
		}
	}

	public static void CS_UserItemToRelieve(int itemId, int num, long soliderID, Action func)
	{
		CSUseItemToRelieve cSUseItemToRelieve = new CSUseItemToRelieve();
		cSUseItemToRelieve.itemId = itemId;
		cSUseItemToRelieve.num = num;
		cSUseItemToRelieve.soldierId = soliderID;
		SpecialSoliderHandler.UseReliveRMB = func;
		ClientMgr.GetNet().SendHttp(2410, cSUseItemToRelieve, new DataHandler.OpcodeHandler(SpecialSoliderHandler.UseRelieveRMBAcion), null);
	}

	public static void UseRelieveRMBAcion(bool isError, Opcode opcode)
	{
		if (SpecialSoliderHandler.UseReliveRMB != null)
		{
			SpecialSoliderHandler.UseReliveRMB();
		}
	}

	public static void CS_SoldierAdd(int id, int type, Action func)
	{
		SpecialSoliderHandler.BuySoliderByRmb = func;
		CSSoldierAdd cSSoldierAdd = new CSSoldierAdd();
		cSSoldierAdd.id = id;
		cSSoldierAdd.type = type;
		ClientMgr.GetNet().SendHttp(2400, cSSoldierAdd, new DataHandler.OpcodeHandler(SpecialSoliderHandler.BuySolider), null);
	}

	public static void BuySolider(bool isError, Opcode opcode)
	{
		if (SpecialSoliderHandler.BuySoliderByRmb != null)
		{
			SpecialSoliderHandler.BuySoliderByRmb();
		}
	}
}
