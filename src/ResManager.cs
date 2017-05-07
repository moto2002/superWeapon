using System;
using System.Collections.Generic;
using UnityEngine;

public class ResManager
{
	public const string pcURL = "_PC";

	public const string allDataFile = "ClientDatas.ts";

	public const string skillData = "SkillConst";

	public const string armyData = "Army";

	public const string armyUpData = "ArmyUpSet";

	public const string GameStartCameraAni = "GameStartCameraAni";

	public const string HomeCameraAni = "HomeCameraAni";

	public const string armyStarSet = "ArmyStarSet";

	public const string armyStar = "ArmyStar";

	public const string buildData = "Building";

	public const string buildUpData = "BuildingUpSet";

	public const string HalfTaskData = "HalfTalk";

	public const string languageXmlName = "Language_Chinese";

	public const string loadReward = "OnlinePrize";

	public const string IslandTypeToModel = "IslandTypeToModel";

	public const string RandomnameXMl = "Randomname";

	public const string sevenDayXml = "DailySign";

	public const string ErrorMassage = "ErrorMassage";

	public const string Activities = "Activities";

	public const string legionFlag = "LegionFlag";

	public const string legionRight = "LegionsRight";

	public const string MilitaryRank = "MilitaryRank";

	public const string militaryOpenSet = "MilitaryOpenSet";

	public const string ArmyFactory = "ArmyFactory";

	public const string electricity = "Electricity";

	public const string technology = "TechnologyTree";

	public const string terrainDatas = "TerrainDatas";

	public const string item = "Item";

	public const string itemMixSet = "ItemMixSet";

	public const string playerUpSet = "PlayerUpSet";

	public const string Officer = "Officer";

	public const string OfficerLevelSet = "OfficerLevelSet";

	public const string OfficerRecruit = "OfficerRecruitRule";

	public const string OfficerRankSet = "OfficerRankSet";

	public const string OfficerTalentSet = "TalentUpSet";

	public const string OfficerSkillSet2 = "SkillUpSet";

	public const string Buff = "Buff";

	public const string RankingPrize = "RankingPrize";

	public const string StarCondition = "StarCondition";

	public const string BattleField = "BattleField";

	public const string Battle = "Battle";

	public const string WarZone = "WarZone";

	public const string DropList = "DropList";

	public const string Box = "Box";

	public const string EliteNpcBox = "EliteNpcBox";

	public const string NPC = "NPC";

	public const string Task = "Task";

	public const string Achievement = "Achievement";

	public const string ResConvert = "ResConvert";

	public const string BattleFieldBoxXml = "BattleBox";

	public const string BattleBoxXml = "BattleAllPassBox";

	public const string ResIslandOutput = "ResIslandOutput";

	public const string DesignConfig = "DesignConfig";

	public const string GoldPurchase = "GoldPurchase";

	public const string Vip = "Vip";

	public const string VipRight = "VipRight";

	public const string Mall = "Mall";

	public const string TalkItem = "talk";

	public const string Activity = "Activity";

	public const string Sign = "DailySign";

	public const string SignCumulative = "AccumulativeSignRewards";

	public const string Aide = "Aide";

	public const string AideAbility = "AideAbility";

	public const string ResDes = "ResDes";

	public const string MapEntity = "MapEntity_1";

	public const string NpcAttackBattle = "NpcAttackBattle";

	public const string towerUpdate = "TowerUpGrade";

	public const string towerStrength = "TowerStrength";

	public const string BuildingUpGrade = "BuildingUpGrade";

	public const string Shop = "Shop";

	public const string BtnUpSet = "BtnUpSet";

	public const string BuildingQueuePrice = "BuildWorkers";

	public const string SkillUpset = "SkillUpSet";

	public const string SkillMix = "skillMix";

	public const string MontyToToken = "MoneyToToken";

	public const string NewbieGuide = "NewGuild";

	public const string language = "Language_Chinese";

	public const string HalfTalk = "HalfTalk";

	public const string gameStart = "gameStart";

	public const string RndBox = "RndBox";

	public const string Events = "Events";

	public const string Sound = "Sound";

	public const string NpcAttark = "NPCAttackWave";

	public const string DeathToArmy = "DeathToArmy";

	public const string equip = "equip";

	public const string Skill = "Skill";

	public const string ReserveDuty = "Patrol";

	public const string CameraData = "Camera";

	public const string Loading = "loading";

	public const string soldier = "soldier";

	public const string soldierExpSet = "soldierExpSet";

	public const string soldierLevelSet = "soldierLevelSet";

	public const string soldierUpset = "soldierUpSet";

	public const string soldierTalk = "sodierTalk";

	public const string towerFile = "AllTowers.msg";

	public const string uniteFile = "AllTanks.msg";

	public const string resFile = "AllResStyle_";

	public const string LegionsRight = "LegionsRight";

	public static GameObject bulletRes;

	public static GameObject blastRes;

	public static GameObject characterRes;

	public static AssetBundle scene;

	public static string Data_Path = "Data/";

	public static string PlanerDataXMlPath = "PlannerDataXMl/";

	public static string BaseUI_Path = "UI/GameBase/";

	public static string FuncUI_Path = "UI/FuncUI/";

	public static string TextureRes_Path = "Texture/";

	public static string UnitBoat_Path = "Unit/LandingShip/";

	public static string UnitTank_Path = "Unit/Tank/";

	public static string UnitTower_Path = "Unit/Tower/";

	public static string UnitRes_Path = "Unit/Res/";

	public static string BulletRes_Path = "Bullet/";

	public static string F_EffectRes_Path = "Effect/Fight/";

	public static string WMapRes_Path = "WMap/";

	public static string outDataURL;

	public static string outResURL;

	public static string wwwDataPath;

	public static string wwwArtPath;

	public static string outURL;

	public static List<string> forDatas = new List<string>();

	public static List<string> forTowers = new List<string>();

	public static List<string> forTanks = new List<string>();

	public static List<string> forReses = new List<string>();

	public static List<string> forMaps = new List<string>();

	public static List<string> forUIs = new List<string>();

	public static List<string> forEffects = new List<string>();

	public static List<string> forAudio = new List<string>();

	public static string artFile
	{
		get
		{
			return "vercache/android/";
		}
	}

	public static string DataPath_ForDown
	{
		get
		{
			if (Application.isMobilePlatform)
			{
				return Application.persistentDataPath;
			}
			return Application.streamingAssetsPath;
		}
	}

	public static string artFilePlatform
	{
		get
		{
			return "android";
		}
	}

	public static string DataPathURL(string fileName)
	{
		if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer)
		{
			return Application.dataPath + fileName;
		}
		if (Application.platform == RuntimePlatform.OSXEditor)
		{
			return "file://" + Application.streamingAssetsPath + "/" + fileName;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return "file://" + Application.streamingAssetsPath + "/" + fileName;
		}
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			return "file://" + Application.streamingAssetsPath + "/" + fileName;
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			return Application.streamingAssetsPath + "/" + fileName;
		}
		return "file://" + Application.dataPath + "/不存在的路径/";
	}
}
