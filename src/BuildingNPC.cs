using System;

public class BuildingNPC
{
	public long buildingId;

	public int buildingIdx;

	public int posIdx;

	public int row;

	public int num;

	public int lv = 1;

	public int star;

	public int strength;

	public bool isdestroy;

	public DateTime finishTime;

	public BuildingProductType productType = BuildingProductType.soldier;

	public int protductNum = 8;

	public DateTime takeTime;
}
