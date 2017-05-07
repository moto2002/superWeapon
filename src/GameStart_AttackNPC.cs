using System;
using System.Collections.Generic;

public class GameStart_AttackNPC
{
	public struct armyData_NpcAttack
	{
		public int armyIndex;

		public int armyNum;

		public int armyLV;
	}

	public int soliderIndex;

	public int soliderlV;

	public List<GameStart_AttackNPC.armyData_NpcAttack> armys = new List<GameStart_AttackNPC.armyData_NpcAttack>();

	public List<int> skills = new List<int>();
}
