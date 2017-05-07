using System;
using System.Collections.Generic;

public class Battle
{
	public long ID;

	public int id;

	public int type;

	public int number;

	public string name;

	public string description;

	public int zoneId;

	public int needZoneStar;

	public int preId;

	public int nextId;

	public int firstBattleFieldId;

	public int starNum1;

	public Dictionary<int, int> itemPrize1 = new Dictionary<int, int>();

	public int starNum2;

	public Dictionary<int, int> itemPrize2 = new Dictionary<int, int>();

	public int starNum3;

	public Dictionary<int, int> itemPrize3 = new Dictionary<int, int>();

	public Dictionary<int, int> stagePrize = new Dictionary<int, int>();

	public List<float> coord = new List<float>();

	public int radarLevel;

	public int mapId;

	public Dictionary<int, BattleField> allBattleField = new Dictionary<int, BattleField>();

	public List<int> prizes = new List<int>();

	public long EndBattleBoxTime;

	public bool isCanSweep;

	public int battleBox;

	public FightRecord FightRecord
	{
		get
		{
			FightRecord fightRecord = new FightRecord();
			int num = 0;
			fightRecord.isFight = false;
			foreach (KeyValuePair<int, BattleField> current in this.allBattleField)
			{
				if (current.Value.fightRecord.star != -1)
				{
					num += current.Value.fightRecord.star;
				}
				if (current.Value.nextId == 0 && current.Value.fightRecord.isFight)
				{
					fightRecord.isFight = true;
				}
			}
			fightRecord.star = num;
			return fightRecord;
		}
	}

	public int NeedAttackBattleFightID()
	{
		foreach (KeyValuePair<int, BattleField> current in this.allBattleField)
		{
			if (current.Value.preId == 0 && !current.Value.fightRecord.isFight)
			{
				int result = current.Key - 1;
				return result;
			}
			if (current.Value.nextId == 0 && current.Value.fightRecord.isFight)
			{
				int result = current.Key;
				return result;
			}
			if (!current.Value.fightRecord.isFight)
			{
				int result = current.Key;
				return result;
			}
		}
		return 0;
	}

	public void ClearBattleFightRecord()
	{
		foreach (KeyValuePair<int, BattleField> current in this.allBattleField)
		{
			current.Value.fightRecord.isFight = false;
			current.Value.fightRecord.star = 0;
		}
	}
}
