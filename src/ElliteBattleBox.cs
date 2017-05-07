using System;
using System.Collections.Generic;

public class ElliteBattleBox
{
	public long id;

	public int state;

	public bool showTips;

	public int level;

	public int star;

	public Dictionary<int, int> item = new Dictionary<int, int>();

	public Dictionary<ResType, int> res = new Dictionary<ResType, int>();

	public int diamond;
}
