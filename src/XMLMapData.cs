using System;
using System.Collections.Generic;

public class XMLMapData
{
	public int dataId;

	public string dataName;

	public int dataType;

	public int terrainType;

	public int baseResType;

	public Dictionary<int, TowerListData> towerList = new Dictionary<int, TowerListData>();
}
