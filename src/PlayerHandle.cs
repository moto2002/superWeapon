using System;
using UnityEngine;

public class PlayerHandle
{
	public static void EnterSence(string type)
	{
		if (type == "island")
		{
			Loading.senceType = SenceType.Island;
			string text = SenceInfo.terrainTypeList[SenceInfo.curMap.terrType].terrainRes;
			if (DateTime.Now.Hour >= 18 || DateTime.Now.Hour <= 6)
			{
				if (!text.Contains("_night"))
				{
					text += "_night";
				}
			}
			else if (text.Contains("_night"))
			{
				text = text.Replace("_night", string.Empty);
			}
			Loading.senceName = text;
			Loading.IslandSenceName = Loading.senceName;
			if (!Loading.IslandSenceName.Equals(text) || (PoolManage.Ins.IsContainSence(Loading.senceType) && !PoolManage.Ins.GetSenceName(Loading.senceType).Equals(text)))
			{
				PoolManage.Ins.RemoveSence(Loading.senceType);
			}
			if (!Loading.IsRefreshSence)
			{
				FuncUIManager.inst.InitUIPanel();
				return;
			}
		}
		else if (type == "WorldMap")
		{
			Loading.senceName = "WorldMap";
			Loading.senceType = SenceType.WorldMap;
			if (PoolManage.Ins.GetMapSence(SenceType.WorldMap) != null)
			{
				if (T_WMap.inst)
				{
					T_WMap.inst.StartOpenNewIsLand();
				}
				return;
			}
		}
		if (Loading.ins == null)
		{
			Application.LoadLevelAdditiveAsync("Loading");
		}
		else
		{
			Loading.ins.ga.SetActive(true);
		}
	}

	public static void CloseCamera()
	{
		if (CameraControl.inst)
		{
			CameraControl.inst.cameraMain.gameObject.SetActive(false);
		}
		if (UIManager.inst)
		{
			UIManager.inst.uiCamera.gameObject.SetActive(false);
		}
		if (UIManager.inst)
		{
			UIManager.inst._3DUICamera.gameObject.SetActive(false);
		}
	}

	public static void GOTO_WorldMap()
	{
		if (HeroInfo.GetInstance().firstWMap)
		{
			SenceHandler.CG_GetPalyerWMapData();
			HeroInfo.GetInstance().firstWMap = false;
		}
		else if (HUDTextTool.WorldMapRedNotice)
		{
			HUDTextTool.WorldMapRedNotice = false;
			SenceHandler.CG_GetPalyerWMapData();
		}
		else
		{
			PlayerHandle.EnterSence("WorldMap");
		}
	}
}
