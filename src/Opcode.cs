using msg;
using System;
using System.Collections.Generic;
using System.IO;

public sealed class Opcode
{
	public const int CSLogin = 1000;

	public const int SCSessionData = 10000;

	public const int SCIslandData = 10001;

	public const int SCIslandOfficerData = 10069;

	public const int SCPlayerBase = 10002;

	public const int SCOtherPlayerInfo = 10072;

	public const int SCPlayerResource = 10003;

	public const int CSAddResore = 1016;

	public const int SCResourceLimit = 10039;

	public const int CSUseItem = 1002;

	public const int CSBuildingBuild = 2000;

	public const int SCPlayerBuilding = 10006;

	public const int CSBuildingUp = 2002;

	public const int SCCDEndTime = 10007;

	public const int CSBuildingEnd = 2030;

	public const int CSBuildingMove = 2004;

	public const int SCBuildingMove = 10008;

	public const int CSTakeResource = 2006;

	public const int SCResIslandTake = 10051;

	public const int SCGoldIslandChanged = 10052;

	public const int CSTakeAllResource = 2012;

	public const int CSBuildingRemove = 2008;

	public const int SCBuildingRemove = 10040;

	public const int CSCancelCDTime = 2010;

	public const int CSBuildingRemoveEnd = 2014;

	public const int CSTowerUpGrade = 2018;

	public const int CSTowerStrength = 2020;

	public const int CSCityWallBatchMove = 2022;

	public const int SCCityWallBatchMove = 10094;

	public const int SCTakeResShow = 10095;

	public const int CSCityWallBatchUpLevel = 2024;

	public const int CSArmyConfigure = 3000;

	public const int CSPatrolArmyConfigure = 3010;

	public const int CSArmyConfigureAuto = 3008;

	public const int CSArmyStarUp = 3002;

	public const int CSArmyLevelUp = 3004;

	public const int SCArmyData = 10010;

	public const int CSArmyList = 3006;

	public const int SCOpenArmy = 10082;

	public const int CSArmyLevelUpEnd = 3014;

	public const int SCArmyCDTimes = 10091;

	public const int CSArmyConfigureEnd = 3016;

	public const int SCConfigureArmyData = 10116;

	public const int CSCancelConfigureArmy = 3018;

	public const int CSSellArmy = 3020;

	public const int CSMilitaryRankPrize = 3022;

	public const int SCExtraArmy = 10121;

	public const int CSClearExtarArmy = 3024;

	public const int CSPlayerWorldMap = 4000;

	public const int SCPlayerWorldMap = 10011;

	public const int SCMapEntity = 10012;

	public const int CSOpenCloud = 4002;

	public const int SCMapIndexNotice = 10013;

	public const int CSReplacePlayer = 4004;

	public const int CSSpyIsland = 4006;

	public const int SCSpyIsland = 10014;

	public const int CSReportReaded = 4008;

	public const int SCReportReadData = 10093;

	public const int CSBattleStart = 5000;

	public const int SCBattleStart = 10067;

	public const int CSBattleEnd = 5002;

	public const int SWValidateFightEvent = 161;

	public const int SCBattleEnd = 10015;

	public const int CSFightingEvent = 5004;

	public const int SCFightingEvent = 10016;

	public const int SCVideoData = 10060;

	public const int CSWarZoneList = 5006;

	public const int SCWarBattleData = 10017;

	public const int SCReportData = 10018;

	public const int SCReportCountData = 10019;

	public const int CSBattleReport = 5008;

	public const int CSReceiveReportMoney = 5010;

	public const int CSWarStarPrize = 5012;

	public const int CSShareBattleVideo = 5014;

	public const int CSSweep = 5016;

	public const int SCSweep = 10061;

	public const int CSOpenBox = 5020;

	public const int SCNPCAttackData = 10066;

	public const int SCIslandBeAttackedData = 10073;

	public const int CSOpenBattleFieldBox = 5024;

	public const int SCBattleFieldBox = 10078;

	public const int CSOpenBattleBox = 5026;

	public const int CSPVP = 5028;

	public const int CSReportList = 5030;

	public const int SCEliteBoxStatus = 10115;

	public const int CSOpenEliteBox = 5032;

	public const int CSTechList = 6000;

	public const int CSTechUp = 6002;

	public const int SCTechData = 10021;

	public const int CSTechUpEnd = 6004;

	public const int CSTechDiamondPrize = 6006;

	public const int CSOfficerList = 7000;

	public const int SCOfficerData = 10022;

	public const int SCExpCardData = 10070;

	public const int SCOfficerTenRecruitData = 10071;

	public const int SCOfficerEqu = 10068;

	public const int CSOfficerEquOn = 7016;

	public const int CSOfficerAddExp = 7002;

	public const int CSOfficerSkillUp = 7004;

	public const int CSOfficerRecruit = 7006;

	public const int CSOfficerEmploy = 7008;

	public const int CSOfficerDismiss = 7010;

	public const int SCOfficerRemove = 10026;

	public const int SCRecruitInfoData = 10025;

	public const int CSOfficerFreeze = 7014;

	public const int CSEquipMix = 7020;

	public const int CSOfficerUpQuality = 7022;

	public const int CSAideList = 8000;

	public const int SCPlayerAideData = 10028;

	public const int CSAideMix = 8002;

	public const int CSAideSend = 8004;

	public const int CSAideRecycle = 8006;

	public const int CSAideIntensify = 8008;

	public const int CSAideMixCompleteUseMoney = 8010;

	public const int CSHeartbeat = 9000;

	public const int SCHeartbeat = 10030;

	public const int SCTipsData = 10031;

	public const int SCTodayBuyResData = 10045;

	public const int CSDebug = 9002;

	public const int CSLoginData = 9004;

	public const int CSNewGuide = 9008;

	public const int SCBtnOpen = 10063;

	public const int SCNoticeList = 10064;

	public const int CSReadNotice = 9010;

	public const int CSCalcMoney = 9012;

	public const int SCCalcMoney = 10088;

	public const int CSExchangeGift = 9014;

	public const int SCProfile = 10109;

	public const int CSItemList = 1100;

	public const int SCPlayerItem = 10041;

	public const int CSItemMix = 1102;

	public const int CSRankingList = 1200;

	public const int SCRankingList = 10033;

	public const int CSRankingPrize = 1202;

	public const int CSRankingPrizeList = 1204;

	public const int SCRankingPrizeList = 10035;

	public const int SCRankingPrizeData = 10036;

	public const int CSCompleteTask = 1300;

	public const int SCPlayerTaskData = 10037;

	public const int SCClearDailyTask = 10042;

	public const int SCAchievementData = 10044;

	public const int CSCompleteAchievement = 1400;

	public const int SCGrowableItemData = 10047;

	public const int CSRequestChitChatData = 1500;

	public const int SCChitChatData = 10048;

	public const int CSRelationList = 1600;

	public const int SCRelationData = 10049;

	public const int SCRelationRemove = 10050;

	public const int SCPlayerVipData = 10054;

	public const int CSUseMoneyBuyGold = 1700;

	public const int SCPlayerMailData = 10055;

	public const int SCPlayerMailStatus = 10056;

	public const int CSReadMail = 1800;

	public const int CSReceiveMailAttachment = 1802;

	public const int SCArmsDealerData = 10057;

	public const int CSRefreshArmsDealerUseMoney = 1902;

	public const int CSRefreshArmsDealer = 1904;

	public const int CSBuyItem = 1906;

	public const int CSBuyGrowthFund = 1912;

	public const int SCActivityData = 10058;

	public const int CSgetActivityPrize = 2112;

	public const int SCActivitiesData = 10096;

	public const int SCActivityCountsData = 10097;

	public const int CSRefreshActivity = 2100;

	public const int CSRequestActivity = 2102;

	public const int SCSignInData = 10059;

	public const int CSSignIn = 2104;

	public const int CSRetroactiveSignIn = 2106;

	public const int CSRetroactiveAllSignIn = 2108;

	public const int CSReceiveAccumulativeSignRewards = 2110;

	public const int CSOnlineRewards = 2208;

	public const int SCOnlineRewards = 10079;

	public const int CSSevenDaysPrize = 2210;

	public const int SCSevenDaysPrize = 10081;

	public const int SCActivityRanking = 10099;

	public const int CSGetRanking = 2114;

	public const int SCRanking = 10103;

	public const int CSReconnection = 1004;

	public const int CSBuyArmyToken = 1006;

	public const int CSBuySearchToken = 1008;

	public const int CSReceiveVipDailyMoney = 1010;

	public const int CSReName = 1012;

	public const int CSBuyBuildingQueue = 2016;

	public const int SCBuyBuildingQueue = 10080;

	public const int CSBuySkillToken = 1014;

	public const int SCBuyArmyToken = 10087;

	public const int SCArmyTokenCdTime = 10108;

	public const int CSGetActivityList = 2212;

	public const int SCGetActivitiesList = 10117;

	public const int CSTurntableDraw = 2216;

	public const int SCLotteryPrize = 10118;

	public const int CSShopPay = 2218;

	public const int CSGetSkillCard = 2300;

	public const int CSSkillExchange = 2302;

	public const int CSSkillConfig = 2304;

	public const int CSSkillRemove = 2306;

	public const int SCSkillData = 10075;

	public const int SCSkillShow = 10076;

	public const int SCFlushSkill = 10083;

	public const int CSSkillUp = 2308;

	public const int SCSkillUpData = 10084;

	public const int SCSkillConfigData = 10085;

	public const int SCSkillRandomCdTime = 10086;

	public const int CSSkillMix = 2310;

	public const int CSSkillMixEnd = 2312;

	public const int CSSkillConfigList = 2314;

	public const int CSSkillList = 2316;

	public const int CSSoldierAdd = 2400;

	public const int SCPlayerSoldier = 10089;

	public const int CSSoldierUpStar = 2402;

	public const int CSSoldierSkillUp = 2404;

	public const int CSSoldierRelive = 2406;

	public const int CSSoldierConfigure = 2408;

	public const int SCSoldierConfigure = 10090;

	public const int CSUseItemToRelieve = 2410;

	public const int SCSkillPointCd = 10092;

	public const int CSUseItemToAddExp = 2412;

	public const int CSSoldierConfigureEnd = 2414;

	public const int CScancelConfigSoldier = 2416;

	public const int CSGetOrderId = 2502;

	public const int SCGetOrderId = 10101;

	public const int CSPayCallback = 2504;

	public const int SCPayCallback = 10102;

	public const int CSLegionCreate = 2600;

	public const int SCLegionData = 10104;

	public const int SCLegionPVE = 10120;

	public const int SCLegionMember = 10112;

	public const int CSLegionAdd = 2602;

	public const int CSLegionOut = 2606;

	public const int CSLegionJobUpOrDown = 2608;

	public const int CSDisMissLegionMember = 2610;

	public const int SCLegionApply = 10105;

	public const int CSAgreeLegionApply = 2612;

	public const int CSIgnoreLegionApply = 2614;

	public const int SCIgnoreLegionApply = 10106;

	public const int CSIgnoreAllLegionApply = 2616;

	public const int CSRandomLegion = 2618;

	public const int SCSearchLegionData = 10107;

	public const int CSModifyLegionInfo = 2620;

	public const int CSLegionHelpApply = 2622;

	public const int SCLegionHelpApply = 10110;

	public const int CSLegionHelp = 2624;

	public const int CSSearchLegion = 2626;

	public const int CSGetContributionPrize = 2628;

	public const int SCPlayerLegionProp = 10111;

	public const int CSLegionData = 2630;

	public const int CSLegionMemberData = 2632;

	public const int SCOperation = 10114;

	public const int CSGetLegionApplyList = 2634;

	public const int CSGetLegionHelpApplyList = 2636;

	public const int CSDeleteLegionHelpApply = 2638;

	public const int CSGM = 20000;

	public const int SCGM = 20001;

	public const int SCMonitorData = 10065;

	public const int CSSocketRegister = 30002;

	public const int SCSocketRegister = 30003;

	public const int CSSocketHeat = 30004;

	public const int CSSendMessage = 30100;

	public const int SCSendMessage = 30101;

	private Dictionary<int, List<object>> dic = new Dictionary<int, List<object>>();

	private static ProtoMsgSerializer opcodeSerializer = new ProtoMsgSerializer();

	private Opcode()
	{
	}

	public List<T> Get<T>(int id) where T : class
	{
		List<T> list = new List<T>();
		if (this.dic.ContainsKey(id))
		{
			foreach (object current in this.dic[id])
			{
				list.Add(current as T);
			}
		}
		return list;
	}

	public static Opcode GetInstance_Socket(int cmdID, MemoryStream memoryStream)
	{
		Opcode opcode = new Opcode();
		object item = Opcode.Deserialize(cmdID, memoryStream);
		if (!opcode.dic.ContainsKey(cmdID))
		{
			opcode.dic.Add(cmdID, new List<object>());
		}
		opcode.dic[cmdID].Add(item);
		return opcode;
	}

	public static Opcode GetInstance(MemoryStream memoryStream)
	{
		Opcode opcode = new Opcode();
		DownMessage downMessage = Opcode.Deserialize<DownMessage>(memoryStream);
		for (int i = 0; i < downMessage.dataId.Count; i++)
		{
			int num = downMessage.dataId[i];
			byte[] buffer = downMessage.data[i];
			object item = null;
			using (MemoryStream memoryStream2 = new MemoryStream(buffer))
			{
				item = Opcode.Deserialize(num, memoryStream2);
			}
			if (!opcode.dic.ContainsKey(num))
			{
				opcode.dic.Add(num, new List<object>());
			}
			opcode.dic[num].Add(item);
		}
		return opcode;
	}

	public static object Deserialize(int cmdID, MemoryStream mm)
	{
		object result = null;
		switch (cmdID)
		{
		case 10000:
			result = Opcode.Deserialize<SCSessionData>(mm);
			return result;
		case 10001:
			result = Opcode.Deserialize<SCIslandData>(mm);
			return result;
		case 10002:
			result = Opcode.Deserialize<SCPlayerBase>(mm);
			return result;
		case 10003:
			result = Opcode.Deserialize<SCPlayerResource>(mm);
			return result;
		case 10004:
		case 10005:
		case 10009:
		case 10020:
		case 10023:
		case 10024:
		case 10027:
		case 10029:
		case 10032:
		case 10034:
		case 10038:
		case 10043:
		case 10046:
		case 10053:
		case 10062:
		case 10074:
		case 10077:
		case 10098:
		case 10100:
		case 10113:
		case 10119:
			IL_1F8:
			if (cmdID == 20001)
			{
				result = Opcode.Deserialize<SCGM>(mm);
				return result;
			}
			if (cmdID == 30003)
			{
				result = Opcode.Deserialize<SCSocketRegister>(mm);
				return result;
			}
			if (cmdID != 30101)
			{
				return result;
			}
			result = Opcode.Deserialize<SCSendMessage>(mm);
			return result;
		case 10006:
			result = Opcode.Deserialize<SCPlayerBuilding>(mm);
			return result;
		case 10007:
			result = Opcode.Deserialize<SCCDEndTime>(mm);
			return result;
		case 10008:
			result = Opcode.Deserialize<SCBuildingMove>(mm);
			return result;
		case 10010:
			result = Opcode.Deserialize<SCArmyData>(mm);
			return result;
		case 10011:
			result = Opcode.Deserialize<SCPlayerWorldMap>(mm);
			return result;
		case 10012:
			result = Opcode.Deserialize<SCMapEntity>(mm);
			return result;
		case 10013:
			result = Opcode.Deserialize<SCMapIndexNotice>(mm);
			return result;
		case 10014:
			result = Opcode.Deserialize<SCSpyIsland>(mm);
			return result;
		case 10015:
			result = Opcode.Deserialize<SCBattleEnd>(mm);
			return result;
		case 10016:
			result = Opcode.Deserialize<SCFightingEvent>(mm);
			return result;
		case 10017:
			result = Opcode.Deserialize<SCWarBattleData>(mm);
			return result;
		case 10018:
			result = Opcode.Deserialize<SCReportData>(mm);
			return result;
		case 10019:
			result = Opcode.Deserialize<SCReportCountData>(mm);
			return result;
		case 10021:
			result = Opcode.Deserialize<SCTechData>(mm);
			return result;
		case 10022:
			result = Opcode.Deserialize<SCOfficerData>(mm);
			return result;
		case 10025:
			result = Opcode.Deserialize<SCRecruitInfoData>(mm);
			return result;
		case 10026:
			result = Opcode.Deserialize<SCOfficerRemove>(mm);
			return result;
		case 10028:
			result = Opcode.Deserialize<SCPlayerAideData>(mm);
			return result;
		case 10030:
			result = Opcode.Deserialize<SCHeartbeat>(mm);
			return result;
		case 10031:
			result = Opcode.Deserialize<SCTipsData>(mm);
			return result;
		case 10033:
			result = Opcode.Deserialize<SCRankingList>(mm);
			return result;
		case 10035:
			result = Opcode.Deserialize<SCRankingPrizeList>(mm);
			return result;
		case 10036:
			result = Opcode.Deserialize<SCRankingPrizeData>(mm);
			return result;
		case 10037:
			result = Opcode.Deserialize<SCPlayerTaskData>(mm);
			return result;
		case 10039:
			result = Opcode.Deserialize<SCResourceLimit>(mm);
			return result;
		case 10040:
			result = Opcode.Deserialize<SCBuildingRemove>(mm);
			return result;
		case 10041:
			result = Opcode.Deserialize<SCPlayerItem>(mm);
			return result;
		case 10042:
			result = Opcode.Deserialize<SCClearDailyTask>(mm);
			return result;
		case 10044:
			result = Opcode.Deserialize<SCAchievementData>(mm);
			return result;
		case 10045:
			result = Opcode.Deserialize<SCTodayBuyResData>(mm);
			return result;
		case 10047:
			result = Opcode.Deserialize<SCGrowableItemData>(mm);
			return result;
		case 10048:
			result = Opcode.Deserialize<SCChitChatData>(mm);
			return result;
		case 10049:
			result = Opcode.Deserialize<SCRelationData>(mm);
			return result;
		case 10050:
			result = Opcode.Deserialize<SCRelationRemove>(mm);
			return result;
		case 10051:
			result = Opcode.Deserialize<SCResIslandTake>(mm);
			return result;
		case 10052:
			result = Opcode.Deserialize<SCGoldIslandChanged>(mm);
			return result;
		case 10054:
			result = Opcode.Deserialize<SCPlayerVipData>(mm);
			return result;
		case 10055:
			result = Opcode.Deserialize<SCPlayerMailData>(mm);
			return result;
		case 10056:
			result = Opcode.Deserialize<SCPlayerMailStatus>(mm);
			return result;
		case 10057:
			result = Opcode.Deserialize<SCArmsDealerData>(mm);
			return result;
		case 10058:
			result = Opcode.Deserialize<SCActivityData>(mm);
			return result;
		case 10059:
			result = Opcode.Deserialize<SCSignInData>(mm);
			return result;
		case 10060:
			result = Opcode.Deserialize<SCVideoData>(mm);
			return result;
		case 10061:
			result = Opcode.Deserialize<SCSweep>(mm);
			return result;
		case 10063:
			result = Opcode.Deserialize<SCBtnOpen>(mm);
			return result;
		case 10064:
			result = Opcode.Deserialize<SCNoticeList>(mm);
			return result;
		case 10065:
			result = Opcode.Deserialize<SCMonitorData>(mm);
			return result;
		case 10066:
			result = Opcode.Deserialize<SCNPCAttackData>(mm);
			return result;
		case 10067:
			result = Opcode.Deserialize<SCBattleStart>(mm);
			return result;
		case 10068:
			result = Opcode.Deserialize<SCOfficerEqu>(mm);
			return result;
		case 10069:
			result = Opcode.Deserialize<SCIslandOfficerData>(mm);
			return result;
		case 10070:
			result = Opcode.Deserialize<SCExpCardData>(mm);
			return result;
		case 10071:
			result = Opcode.Deserialize<SCOfficerTenRecruitData>(mm);
			return result;
		case 10072:
			result = Opcode.Deserialize<SCOtherPlayerInfo>(mm);
			return result;
		case 10073:
			result = Opcode.Deserialize<SCIslandBeAttackedData>(mm);
			return result;
		case 10075:
			result = Opcode.Deserialize<SCSkillData>(mm);
			return result;
		case 10076:
			result = Opcode.Deserialize<SCSkillShow>(mm);
			return result;
		case 10078:
			result = Opcode.Deserialize<SCBattleFieldBox>(mm);
			return result;
		case 10079:
			result = Opcode.Deserialize<SCOnlineRewards>(mm);
			return result;
		case 10080:
			result = Opcode.Deserialize<SCBuyBuildingQueue>(mm);
			return result;
		case 10081:
			result = Opcode.Deserialize<SCSevenDaysPrize>(mm);
			return result;
		case 10082:
			result = Opcode.Deserialize<SCOpenArmy>(mm);
			return result;
		case 10083:
			result = Opcode.Deserialize<SCFlushSkill>(mm);
			return result;
		case 10084:
			result = Opcode.Deserialize<SCSkillUpData>(mm);
			return result;
		case 10085:
			result = Opcode.Deserialize<SCSkillConfigData>(mm);
			return result;
		case 10086:
			result = Opcode.Deserialize<SCSkillRandomCdTime>(mm);
			return result;
		case 10087:
			result = Opcode.Deserialize<SCBuyArmyToken>(mm);
			return result;
		case 10088:
			result = Opcode.Deserialize<SCCalcMoney>(mm);
			return result;
		case 10089:
			result = Opcode.Deserialize<SCPlayerSoldier>(mm);
			return result;
		case 10090:
			result = Opcode.Deserialize<SCSoldierConfigure>(mm);
			return result;
		case 10091:
			result = Opcode.Deserialize<SCArmyCDTimes>(mm);
			return result;
		case 10092:
			result = Opcode.Deserialize<SCSkillPointCd>(mm);
			return result;
		case 10093:
			result = Opcode.Deserialize<SCReportReadData>(mm);
			return result;
		case 10094:
			result = Opcode.Deserialize<SCCityWallBatchMove>(mm);
			return result;
		case 10095:
			result = Opcode.Deserialize<SCTakeResShow>(mm);
			return result;
		case 10096:
			result = Opcode.Deserialize<SCActivitiesData>(mm);
			return result;
		case 10097:
			result = Opcode.Deserialize<SCActivityCountsData>(mm);
			return result;
		case 10099:
			result = Opcode.Deserialize<SCActivityRanking>(mm);
			return result;
		case 10101:
			result = Opcode.Deserialize<SCGetOrderId>(mm);
			return result;
		case 10102:
			result = Opcode.Deserialize<SCPayCallback>(mm);
			return result;
		case 10103:
			result = Opcode.Deserialize<SCRanking>(mm);
			return result;
		case 10104:
			result = Opcode.Deserialize<SCLegionData>(mm);
			return result;
		case 10105:
			result = Opcode.Deserialize<SCLegionApply>(mm);
			return result;
		case 10106:
			result = Opcode.Deserialize<SCIgnoreLegionApply>(mm);
			return result;
		case 10107:
			result = Opcode.Deserialize<SCSearchLegionData>(mm);
			return result;
		case 10108:
			result = Opcode.Deserialize<SCArmyTokenCdTime>(mm);
			return result;
		case 10109:
			result = Opcode.Deserialize<SCProfile>(mm);
			return result;
		case 10110:
			result = Opcode.Deserialize<SCLegionHelpApply>(mm);
			return result;
		case 10111:
			result = Opcode.Deserialize<SCPlayerLegionProp>(mm);
			return result;
		case 10112:
			result = Opcode.Deserialize<SCLegionMember>(mm);
			return result;
		case 10114:
			result = Opcode.Deserialize<SCOperation>(mm);
			return result;
		case 10115:
			result = Opcode.Deserialize<SCEliteBoxStatus>(mm);
			return result;
		case 10116:
			result = Opcode.Deserialize<SCConfigureArmyData>(mm);
			return result;
		case 10117:
			result = Opcode.Deserialize<SCGetActivitiesList>(mm);
			return result;
		case 10118:
			result = Opcode.Deserialize<SCLotteryPrize>(mm);
			return result;
		case 10120:
			result = Opcode.Deserialize<SCLegionPVE>(mm);
			return result;
		case 10121:
			result = Opcode.Deserialize<SCExtraArmy>(mm);
			return result;
		}
		goto IL_1F8;
	}

	public static T Deserialize<T>(MemoryStream stream) where T : class
	{
		return Opcode.opcodeSerializer.Deserialize(stream, null, typeof(T)) as T;
	}

	public static void Serialize(MemoryStream m, object o)
	{
		Opcode.opcodeSerializer.Serialize(m, o);
	}

	public Dictionary<int, List<object>> getDic()
	{
		return this.dic;
	}
}
