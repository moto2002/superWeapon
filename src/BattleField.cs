using System;
using System.Collections.Generic;

public class BattleField
{
	public int id;

	public string name;

	public int commondLevel;

	public string description;

	public int battleId;

	public int cost;

	public int preId;

	public int nextId;

	public int needBattleStar;

	public List<StarCondition> star1Condition = new List<StarCondition>();

	public List<StarCondition> star2Condition = new List<StarCondition>();

	public List<StarCondition> star3Condition = new List<StarCondition>();

	public int battleType;

	public int npcId;

	public string target1Description;

	public string target2Description;

	public string target3Description;

	public FightRecord fightRecord = new FightRecord();

	public List<int> buildingId = new List<int>();

	public List<float> coord = new List<float>();

	public bool isCompleteUI;
}
