using System;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
	public UILabel ItemName;

	public UISprite ItemIcon;

	public UILabel HpLabel;

	public UILabel HpAddLabel;

	public UILabel ATKLabel;

	public UILabel ATKaddLabel;

	public UILabel DefenseLabel;

	public UILabel DefenseAddLabel;

	public UILabel SpecialPowaLbel;

	public UILabel SpecialPowAddLabel;

	public UILabel SpeedLabe;

	public UILabel ShootjourneyLabe;

	public UILabel ShootSpeedLabel;

	public UILabel Armyinfo;

	public UILabel itemInfo;

	public GameObject[] SpeedSp;

	public GameObject[] defenseSp;

	public GameObject[] AttakeSp;

	public GameObject[] lifeSp;

	public GameObject[] SpeedAttakeSp;

	public Body_Model model;

	public UITable textTable;

	public GameObject TryBuildingBtn;

	private void BuildingStore_ItemInfo_TryBuilding_CallBack(GameObject ga)
	{
		BuildingStorePanelManage._instance.TryBuilding();
	}

	private void OnEnable()
	{
		this.TryBuildingBtn.gameObject.SetActive(BuildingStorePanelManage._instance.IsTryBuilding);
		if (BuildingStorePanelManage._instance.IsTryBuilding)
		{
			BuildingStorePanelManage._instance.IsTryBuilding = false;
		}
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.BuildingStore_ItemInfo_TryBuilding, new EventManager.VoidDelegate(this.BuildingStore_ItemInfo_TryBuilding_CallBack));
		if (this.itemInfo)
		{
			Body_Model effectByName = PoolManage.Ins.GetEffectByName("jianzao_glow", base.transform);
			if (effectByName)
			{
				effectByName.tr.localPosition = new Vector3(-287f, 26f, 0f);
			}
			Body_Model effectByName2 = PoolManage.Ins.GetEffectByName("jianzao_dian", base.transform);
			if (effectByName2)
			{
				effectByName2.tr.localPosition = new Vector3(-478f, 224f, 0f);
			}
			Body_Model effectByName3 = PoolManage.Ins.GetEffectByName("jianzao_dian", base.transform);
			if (effectByName3)
			{
				effectByName3.tr.localPosition = new Vector3(478f, 224f, 0f);
			}
		}
	}
}
