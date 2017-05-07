using DicForUnity;
using System;
using System.Collections.Generic;

public class AdjutantPanelData
{
	public class AideData
	{
		public int id;

		public int aideId;

		public int abilityId;

		public long time;
	}

	public static AdjutantPanelData.AideData Aide_Send = null;

	public static Dictionary<int, AdjutantPanelData.AideData> Aide_ServerList = new Dictionary<int, AdjutantPanelData.AideData>();

	public static long endTime;

	public static List<AdjutantPanelData.AideData> aide_ServerList = new List<AdjutantPanelData.AideData>();

	public static int GetResHomeAddttion(int resType, int reSouces)
	{
		int num = 0;
		DicForU.GetValues<int, AdjutantPanelData.AideData>(AdjutantPanelData.Aide_ServerList, AdjutantPanelData.aide_ServerList);
		for (int i = 0; i < AdjutantPanelData.aide_ServerList.Count; i++)
		{
			if ((UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.aide_ServerList[i].abilityId].aideType == reSouces && UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.aide_ServerList[i].abilityId].abilitity == resType) || (UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.aide_ServerList[i].abilityId].abilitity == 5 && resType < UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.aide_ServerList[i].abilityId].abilitity))
			{
				if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(AdjutantPanelData.aide_ServerList[i].time)) > 0.0)
				{
					num += UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.aide_ServerList[i].abilityId].value * 3;
				}
				else
				{
					num += UnitConst.GetInstance().AideAbilityConst[AdjutantPanelData.aide_ServerList[i].abilityId].value;
				}
			}
		}
		return num;
	}
}
