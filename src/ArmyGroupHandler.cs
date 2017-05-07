using msg;
using System;
using UnityEngine;

public class ArmyGroupHandler : MonoBehaviour
{
	private static Action<bool> CreateArmy;

	private static Action<bool> EnterArmy;

	private static Action<bool> SerachArmy;

	private static Action<bool> ChangeArmy;

	private static Action<bool> DeletMember;

	private static Action<bool> ChangMemberJob;

	private static Action<bool> AgreeSubmit;

	private static Action<bool> NoAgreeSubmit;

	private static Action<bool> ClearSubmitList;

	private static Action<bool> ExitOrDissLoveArmy;

	private static Action<bool> ChangeArmyInfo;

	private static Action<bool> ApplyArmyHelp;

	private static Action<bool> LegionMember;

	private static Action<bool> LegionData;

	private static Action<bool> ApplySubmitList;

	private static Action<bool> applyeFromLegion;

	private static Action<bool> GetLegionApplyList;

	private static Action<bool> DeleteLegionHelpApply;

	public static void CG_CSLegionCreate(string armyName, int logoId, int minMedal, int openType, string notice, Action<bool> func = null)
	{
		ArmyGroupHandler.CreateArmy = func;
		CSLegionCreate cSLegionCreate = new CSLegionCreate();
		cSLegionCreate.name = armyName;
		cSLegionCreate.logo = logoId;
		cSLegionCreate.needMinMedal = minMedal;
		cSLegionCreate.openType = openType;
		cSLegionCreate.notice = notice;
		ClientMgr.GetNet().SendHttp(2600, cSLegionCreate, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnCreateArmy), null);
	}

	private static void OnCreateArmy(bool isError, Opcode opcede)
	{
		if (ArmyGroupHandler.CreateArmy != null)
		{
			ArmyGroupHandler.CreateArmy(isError);
		}
	}

	public static void CG_CSLegionAdd(long legionId, Action<bool> func = null)
	{
		ArmyGroupHandler.EnterArmy = func;
		CSLegionAdd cSLegionAdd = new CSLegionAdd();
		cSLegionAdd.legionId = legionId;
		ClientMgr.GetNet().SendHttp(2602, cSLegionAdd, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnEnterArmy), null);
	}

	private static void OnEnterArmy(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.EnterArmy != null)
		{
			ArmyGroupHandler.EnterArmy(isError);
		}
	}

	public static void CG_CSSearchLegion(string armyName, Action<bool> func = null)
	{
		ArmyGroupHandler.SerachArmy = func;
		CSSearchLegion cSSearchLegion = new CSSearchLegion();
		cSSearchLegion.legionName = armyName;
		ClientMgr.GetNet().SendHttp(2626, cSSearchLegion, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnSearchArmy), null);
	}

	private static void OnSearchArmy(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.SerachArmy != null)
		{
			ArmyGroupHandler.SerachArmy(isError);
		}
	}

	public static void CG_CSRandomLegion(int id, Action<bool> func = null)
	{
		ArmyGroupHandler.ChangeArmy = func;
		CSRandomLegion cSRandomLegion = new CSRandomLegion();
		cSRandomLegion.id = id;
		ClientMgr.GetNet().SendHttp(2618, cSRandomLegion, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnChangeArmy), null);
	}

	private static void OnChangeArmy(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.ChangeArmy != null)
		{
			ArmyGroupHandler.ChangeArmy(isError);
		}
	}

	public static void CG_CSLegionJobUpOrDown(long legionId, long targetId, int type, Action<bool> func = null)
	{
		ArmyGroupHandler.ChangMemberJob = func;
		CSLegionJobUpOrDown cSLegionJobUpOrDown = new CSLegionJobUpOrDown();
		cSLegionJobUpOrDown.legionId = legionId;
		cSLegionJobUpOrDown.targetId = targetId;
		cSLegionJobUpOrDown.type = type;
		ClientMgr.GetNet().SendHttp(2608, cSLegionJobUpOrDown, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnChangeMemberJob), null);
	}

	private static void OnChangeMemberJob(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.ChangMemberJob != null)
		{
			ArmyGroupHandler.ChangMemberJob(isError);
		}
	}

	public static void CG_CSDisMissLegionMember(long legionId, long targetId, Action<bool> func = null)
	{
		ArmyGroupHandler.DeletMember = func;
		CSDisMissLegionMember cSDisMissLegionMember = new CSDisMissLegionMember();
		cSDisMissLegionMember.legionId = legionId;
		cSDisMissLegionMember.targetId = targetId;
		ClientMgr.GetNet().SendHttp(2610, cSDisMissLegionMember, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnDeleteMember), null);
	}

	private static void OnDeleteMember(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.DeletMember != null)
		{
			ArmyGroupHandler.DeletMember(isError);
		}
	}

	public static void CG_CSAgreeLegionApply(long id, Action<bool> func = null)
	{
		ArmyGroupHandler.AgreeSubmit = func;
		CSAgreeLegionApply cSAgreeLegionApply = new CSAgreeLegionApply();
		cSAgreeLegionApply.applyId = id;
		ClientMgr.GetNet().SendHttp(2612, cSAgreeLegionApply, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnAgreeSubmit), null);
	}

	private static void OnAgreeSubmit(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.AgreeSubmit != null)
		{
			ArmyGroupHandler.AgreeSubmit(isError);
		}
	}

	public static void CG_CSIgnoreLegionApply(long id, Action<bool> func = null)
	{
		ArmyGroupHandler.NoAgreeSubmit = func;
		CSIgnoreLegionApply cSIgnoreLegionApply = new CSIgnoreLegionApply();
		cSIgnoreLegionApply.applyId = id;
		ClientMgr.GetNet().SendHttp(2614, cSIgnoreLegionApply, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnNogreeSubmit), null);
	}

	private static void OnNogreeSubmit(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.NoAgreeSubmit != null)
		{
			ArmyGroupHandler.NoAgreeSubmit(isError);
		}
	}

	public static void CG_CSIgnoreAllLegionApply(int id, Action<bool> func = null)
	{
		ArmyGroupHandler.ClearSubmitList = func;
		CSIgnoreAllLegionApply cSIgnoreAllLegionApply = new CSIgnoreAllLegionApply();
		cSIgnoreAllLegionApply.id = id;
		ClientMgr.GetNet().SendHttp(2616, cSIgnoreAllLegionApply, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnClearSubmitList), null);
	}

	private static void OnClearSubmitList(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.ClearSubmitList != null)
		{
			ArmyGroupHandler.ClearSubmitList(isError);
		}
	}

	public static void CG_CSLegionOut(long id, Action<bool> func = null)
	{
		ArmyGroupHandler.ExitOrDissLoveArmy = func;
		CSLegionOut cSLegionOut = new CSLegionOut();
		cSLegionOut.legionId = id;
		ClientMgr.GetNet().SendHttp(2606, cSLegionOut, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnExitArmy), null);
	}

	public static void OnExitArmy(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.ExitOrDissLoveArmy != null)
		{
			ArmyGroupHandler.ExitOrDissLoveArmy(isError);
		}
	}

	public static void CG_CSModifyLegionInfo(long LegionId, int id, string val, Action<bool> func = null)
	{
		ArmyGroupHandler.ChangeArmyInfo = func;
		CSModifyLegionInfo cSModifyLegionInfo = new CSModifyLegionInfo();
		cSModifyLegionInfo.legionId = LegionId;
		cSModifyLegionInfo.data.Add(new KVStructStr
		{
			key = id.ToString(),
			value = val
		});
		ClientMgr.GetNet().SendHttp(2620, cSModifyLegionInfo, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnChangeArmyInfo), null);
	}

	private static void OnChangeArmyInfo(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.ChangeArmyInfo != null)
		{
			ArmyGroupHandler.ChangeArmyInfo(isError);
		}
	}

	public static void CG_CSLegionHelpApply(long buildingId, Action<bool> func)
	{
		ArmyGroupHandler.ApplyArmyHelp = func;
		CSLegionHelpApply cSLegionHelpApply = new CSLegionHelpApply();
		cSLegionHelpApply.buildingId = buildingId;
		ClientMgr.GetNet().SendHttp(2622, cSLegionHelpApply, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnApplyArmyHelp), null);
	}

	private static void OnApplyArmyHelp(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.ApplyArmyHelp != null)
		{
			ArmyGroupHandler.ApplyArmyHelp(isError);
		}
	}

	public static void CG_CSLegionHelp(long id, Action<bool> func)
	{
		ArmyGroupHandler.ApplyArmyHelp = func;
		CSLegionHelp cSLegionHelp = new CSLegionHelp();
		cSLegionHelp.applyId = id;
		ClientMgr.GetNet().SendHttp(2624, cSLegionHelp, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnapplyeFromLegion), null);
	}

	private static void OnapplyeFromLegion(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.applyeFromLegion != null)
		{
			ArmyGroupHandler.applyeFromLegion(isError);
		}
	}

	public static void CG_CSLegionMemberData(long legionId, Action<bool> func)
	{
		ArmyGroupHandler.LegionMember = func;
		CSLegionMemberData cSLegionMemberData = new CSLegionMemberData();
		cSLegionMemberData.legionId = legionId;
		ClientMgr.GetNet().SendHttp(2632, cSLegionMemberData, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnLegionMember), null);
	}

	private static void OnLegionMember(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.LegionMember != null)
		{
			ArmyGroupHandler.LegionMember(isError);
		}
	}

	public static void CG_CSLegionData(long legionId, Action<bool> func)
	{
		ArmyGroupHandler.LegionData = func;
		CSLegionData cSLegionData = new CSLegionData();
		cSLegionData.legionId = legionId;
		ClientMgr.GetNet().SendHttp(2630, cSLegionData, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnLegionData), null);
	}

	private static void OnLegionData(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.LegionData != null)
		{
			ArmyGroupHandler.LegionData(isError);
		}
	}

	public static void CG_CSGetLegionApply(long legionId, Action<bool> func)
	{
		ArmyGroupHandler.ApplySubmitList = func;
		CSGetLegionApplyList cSGetLegionApplyList = new CSGetLegionApplyList();
		cSGetLegionApplyList.legionId = legionId;
		ClientMgr.GetNet().SendHttp(2634, cSGetLegionApplyList, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnApplySubmitList), null);
	}

	private static void OnApplySubmitList(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.ApplySubmitList != null)
		{
			ArmyGroupHandler.ApplySubmitList(isError);
		}
	}

	public static void CG_CSGetLegionHelpApplyList(long ID, Action<bool> func)
	{
		ArmyGroupHandler.GetLegionApplyList = func;
		CSGetLegionHelpApplyList cSGetLegionHelpApplyList = new CSGetLegionHelpApplyList();
		cSGetLegionHelpApplyList.legionId = ID;
		ClientMgr.GetNet().SendHttp(2636, cSGetLegionHelpApplyList, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnGetLegionApplyList), null);
	}

	private static void OnGetLegionApplyList(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.GetLegionApplyList != null)
		{
			ArmyGroupHandler.GetLegionApplyList(isError);
		}
	}

	public static void CG_CSDeleteLegionHelpApply(long id, Action<bool> func)
	{
		ArmyGroupHandler.DeleteLegionHelpApply = func;
		CSDeleteLegionHelpApply cSDeleteLegionHelpApply = new CSDeleteLegionHelpApply();
		cSDeleteLegionHelpApply.applyId = id;
		ClientMgr.GetNet().SendHttp(2638, cSDeleteLegionHelpApply, new DataHandler.OpcodeHandler(ArmyGroupHandler.OnDeleteLegionHelpApply), null);
	}

	private static void OnDeleteLegionHelpApply(bool isError, Opcode opcode)
	{
		if (ArmyGroupHandler.DeleteLegionHelpApply != null)
		{
			ArmyGroupHandler.DeleteLegionHelpApply(isError);
		}
	}
}
