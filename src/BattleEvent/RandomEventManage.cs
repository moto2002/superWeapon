using msg;
using System;
using System.Collections.Generic;

namespace BattleEvent
{
	public class RandomEventManage
	{
		public static Dictionary<int, RandomEvent> RandomEventConst = new Dictionary<int, RandomEvent>();

		public static Dictionary<int, RandomBox> RandomBoxConst = new Dictionary<int, RandomBox>();

		public static List<KVStruct> RandomBoxesByServer = new List<KVStruct>();
	}
}
