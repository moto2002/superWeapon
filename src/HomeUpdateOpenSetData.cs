using System;
using System.Collections.Generic;

public class HomeUpdateOpenSetData
{
	public int homeLevel;

	public Dictionary<int, int> buildingIds = new Dictionary<int, int>();

	public List<int> armsIds = new List<int>();

	public int officerNum;

	public int sendAide;

	public int skillNum;

	public int rallypoint;

	public int skillBox;

	public HomeUpdateOpenSetData HomeUpdateOpenSetAddData()
	{
		HomeUpdateOpenSetData homeUpdateOpenSetData = null;
		if (UnitConst.GetInstance().HomeUpdateOpenSetDataConst.ContainsKey(this.homeLevel + 1))
		{
			homeUpdateOpenSetData = new HomeUpdateOpenSetData();
			foreach (int current in UnitConst.GetInstance().HomeUpdateOpenSetDataConst[this.homeLevel + 1].buildingIds.Keys)
			{
				if (!this.buildingIds.ContainsKey(current))
				{
					homeUpdateOpenSetData.buildingIds.Add(current, UnitConst.GetInstance().HomeUpdateOpenSetDataConst[this.homeLevel + 1].buildingIds[current]);
				}
				else if (UnitConst.GetInstance().HomeUpdateOpenSetDataConst[this.homeLevel + 1].buildingIds[current] > this.buildingIds[current])
				{
					homeUpdateOpenSetData.buildingIds.Add(current, UnitConst.GetInstance().HomeUpdateOpenSetDataConst[this.homeLevel + 1].buildingIds[current] - this.buildingIds[current]);
				}
			}
			foreach (int current2 in UnitConst.GetInstance().HomeUpdateOpenSetDataConst[this.homeLevel + 1].armsIds)
			{
				if (!this.armsIds.Contains(current2))
				{
					homeUpdateOpenSetData.armsIds.Add(current2);
				}
			}
			homeUpdateOpenSetData.officerNum = UnitConst.GetInstance().HomeUpdateOpenSetDataConst[this.homeLevel + 1].officerNum - this.officerNum;
			homeUpdateOpenSetData.sendAide = UnitConst.GetInstance().HomeUpdateOpenSetDataConst[this.homeLevel + 1].sendAide - this.sendAide;
		}
		return homeUpdateOpenSetData;
	}
}
