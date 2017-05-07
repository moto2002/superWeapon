using System;
using System.Collections.Generic;
using UnityEngine;

public class JianzhutishiManage : FuncUIPanel
{
	public List<JianzhutishiItem> threeItem = new List<JianzhutishiItem>();

	public List<JianzhutishiItem> twoItem = new List<JianzhutishiItem>();

	public override void Awake()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.ZhanyijianzhutishiPanel_Close, new EventManager.VoidDelegate(this.CloseThis));
	}

	public void CloseThis(GameObject ga)
	{
		base.gameObject.SetActive(false);
	}

	public void ShowItem(int id)
	{
		try
		{
			for (int i = 0; i < this.threeItem.Count; i++)
			{
				this.threeItem[i].gameObject.SetActive(false);
			}
			for (int j = 0; j < this.twoItem.Count; j++)
			{
				this.twoItem[j].gameObject.SetActive(false);
			}
			List<int> buildingId = UnitConst.GetInstance().BattleFieldConst[id].buildingId;
			switch (buildingId.Count)
			{
			case 0:
				base.gameObject.SetActive(false);
				break;
			case 1:
			{
				JianzhutishiItem jianzhutishiItem = this.threeItem[1];
				if (!(jianzhutishiItem == null))
				{
					jianzhutishiItem.gameObject.SetActive(true);
					NewBuildingInfo newBuildingInfo = UnitConst.GetInstance().buildingConst[buildingId[0]];
					jianzhutishiItem.name_Client.text = newBuildingInfo.name;
					jianzhutishiItem.des.text = newBuildingInfo.description;
					jianzhutishiItem.icon.spriteName = newBuildingInfo.iconId;
				}
				break;
			}
			case 2:
				for (int k = 0; k < buildingId.Count; k++)
				{
					JianzhutishiItem jianzhutishiItem2 = this.twoItem[k];
					NewBuildingInfo newBuildingInfo2 = UnitConst.GetInstance().buildingConst[buildingId[k]];
					jianzhutishiItem2.gameObject.SetActive(true);
					jianzhutishiItem2.name_Client.text = newBuildingInfo2.name;
					jianzhutishiItem2.des.text = newBuildingInfo2.description;
					jianzhutishiItem2.icon.spriteName = newBuildingInfo2.iconId;
				}
				break;
			case 3:
				for (int l = 0; l < buildingId.Count; l++)
				{
					JianzhutishiItem jianzhutishiItem3 = this.threeItem[l];
					NewBuildingInfo newBuildingInfo3 = UnitConst.GetInstance().buildingConst[buildingId[l]];
					jianzhutishiItem3.gameObject.SetActive(true);
					jianzhutishiItem3.name_Client.text = newBuildingInfo3.name;
					jianzhutishiItem3.des.text = newBuildingInfo3.description;
					jianzhutishiItem3.icon.spriteName = newBuildingInfo3.iconId;
				}
				break;
			}
		}
		catch (Exception exception)
		{
			LogManage.LogException(exception);
		}
	}
}
