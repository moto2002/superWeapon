using msg;
using System;
using System.Collections.Generic;

public class ReportData
{
	public long id;

	public string Index;

	public string title;

	public BattleReportUIType uiType;

	public BattleReportType type;

	public int islandIndex;

	public int targetChangedMedal;

	public int fighterChangedMedal;

	public long time;

	public bool fighterWin;

	public long fighterId;

	public string fighterName;

	public int fighterLevel;

	public int fighterMedal;

	public List<KVStruct> lossRes = new List<KVStruct>();

	public List<KVStruct> sendArmys = new List<KVStruct>();

	public List<KVStruct> destroyArmys = new List<KVStruct>();

	public List<KVStruct> additions = new List<KVStruct>();

	public string targetName;

	public long targetId;

	public int targetLevel;

	public long worldMapId;

	public List<KVStruct> prizeItems = new List<KVStruct>();

	public RelationType relation;

	public long videoId;

	public bool canRevenge;

	public int money;

	public long receiveMoneyTime;

	public List<KVStruct> addRes = new List<KVStruct>();

	public List<KVStruct> addEquips = new List<KVStruct>();

	public bool UnRead;

	public int dailyType;

	public int spyTimes;

	public int beAtkedTimes;

	public int beCaptureTimes;

	public int sendSoldier;

	public int lossSoldier;

	public bool already_used;

	public ReportData()
	{
		this.Index = Guid.NewGuid().ToString();
	}
}
