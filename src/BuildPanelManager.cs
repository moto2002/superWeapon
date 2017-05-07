using System;
using UnityEngine;

public class BuildPanelManager : MonoBehaviour
{
	public static BuildPanelManager inst;

	public int uiIdx;

	public void OnDestroy()
	{
		BuildPanelManager.inst = null;
	}

	private void Awake()
	{
		BuildPanelManager.inst = this;
	}

	public void PressButton(BuildBtnType btnType)
	{
		switch (btnType)
		{
		case BuildBtnType.home:
			LogManage.Log("点击回家啦！");
			break;
		case BuildBtnType.goOut:
			LogManage.Log("点击出征啦！");
			break;
		case BuildBtnType.goBattle:
			PlayerHandle.GOTO_WorldMap();
			break;
		case BuildBtnType.build:
			FuncUIManager.inst.OpenFuncUI("BuildingStorePanel", SenceType.Island);
			LogManage.Log("点击打开建筑面板！");
			break;
		}
	}
}
