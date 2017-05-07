using System;
using System.Collections.Generic;

public class ArmyGroupData
{
	public Dictionary<int, ArmyGroupInfo> armyInfo = new Dictionary<int, ArmyGroupInfo>();

	public Dictionary<int, ArmyPeopleInfo> armyPeopleInfo = new Dictionary<int, ArmyPeopleInfo>();

	public Dictionary<int, ArmySubmintInfo> armySubmitDic = new Dictionary<int, ArmySubmintInfo>();

	public Dictionary<int, SearchArmyInfo> searchDic = new Dictionary<int, SearchArmyInfo>();
}
