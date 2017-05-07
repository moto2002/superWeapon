using System;
using System.Collections.Generic;

public class WarZone
{
	public int id;

	public string name;

	public string description;

	public int preId;

	public int nextId;

	public int firstBattleId;

	public int lastBattleId;

	public int commondLevel;

	public int zoneMapId;

	public Dictionary<int, Battle> allBattle = new Dictionary<int, Battle>();

	public FightRecord FightRecord
	{
		get
		{
			FightRecord fightRecord = new FightRecord();
			int num = 0;
			fightRecord.isFight = false;
			foreach (KeyValuePair<int, Battle> current in this.allBattle)
			{
				if (current.Value.FightRecord.star != -1)
				{
					num += current.Value.FightRecord.star;
				}
				if (current.Value.nextId == 0 && current.Value.FightRecord.isFight)
				{
					fightRecord.isFight = true;
				}
			}
			fightRecord.star = num;
			return fightRecord;
		}
	}
}
