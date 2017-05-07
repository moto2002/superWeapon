using System;

public struct armyData
{
	public int index;

	public int lv;

	public int star;

	public long buildingID;

	public armyData(int _index, int _lv, int _star, long _buildingID)
	{
		this.index = _index;
		this.lv = _lv;
		this.star = _star;
		this.buildingID = _buildingID;
	}
}
