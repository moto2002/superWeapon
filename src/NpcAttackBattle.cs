using System;
using System.Collections.Generic;

public class NpcAttackBattle
{
	public int id;

	public int level;

	public Dictionary<int, List<NpcAttackBattleItem>> item = new Dictionary<int, List<NpcAttackBattleItem>>();
}
