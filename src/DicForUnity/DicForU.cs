using System;
using System.Collections.Generic;

namespace DicForUnity
{
	public class DicForU
	{
		public static void GetKeys<T, V>(Dictionary<T, V> Dic, List<T> keys)
		{
			keys.Clear();
			foreach (KeyValuePair<T, V> current in Dic)
			{
				keys.Add(current.Key);
			}
		}

		public static void GetValues<T, V>(Dictionary<T, V> Dic, List<V> Values)
		{
			Values.Clear();
			foreach (KeyValuePair<T, V> current in Dic)
			{
				Values.Add(current.Value);
			}
		}
	}
}
