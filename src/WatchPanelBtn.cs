using System;
using UnityEngine;

public class WatchPanelBtn : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnClick()
	{
		if (!SenceInfo.curMap.IsMyMap)
		{
			SenceHandler.CG_GetMapData(HeroInfo.GetInstance().homeInWMapIdx, 1, 0, null);
		}
		else
		{
			PlayerHandle.EnterSence("WorldMap");
		}
	}
}
