using System;
using System.Collections.Generic;

public class EquipSiftManage
{
	public static List<EquipItem> GetEquipType(int equipType)
	{
		List<EquipItem> equipItem = HeroInfo.GetInstance().EquipItem;
		List<EquipItem> list = new List<EquipItem>();
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < equipItem.Count; i++)
		{
			Equip equip = equipList[equipItem[i].equipID];
			if (equip.type == equipType)
			{
				list.Add(equipItem[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetFurnishedEquipItem(int equipType)
	{
		List<EquipItem> equipType2 = EquipSiftManage.GetEquipType(equipType);
		List<EquipItem> list = new List<EquipItem>();
		for (int i = 0; i < equipType2.Count; i++)
		{
			if (equipType2[i].commanderID != 0L)
			{
				list.Add(equipType2[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetNotFurnishedEquipItem(int equipType)
	{
		List<EquipItem> equipType2 = EquipSiftManage.GetEquipType(equipType);
		List<EquipItem> list = new List<EquipItem>();
		for (int i = 0; i < equipType2.Count; i++)
		{
			if (equipType2[i].commanderID == 0L)
			{
				list.Add(equipType2[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetWhiteEquip(List<EquipItem> equipItemList)
	{
		List<EquipItem> list = new List<EquipItem>();
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < equipItemList.Count; i++)
		{
			if (equipList[equipItemList[i].equipID].equipQuality == Quility_ResAndItemAndSkill.白)
			{
				list.Add(equipItemList[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetGreenEquip(List<EquipItem> equipItemList)
	{
		List<EquipItem> list = new List<EquipItem>();
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < equipItemList.Count; i++)
		{
			if (equipList[equipItemList[i].equipID].equipQuality == Quility_ResAndItemAndSkill.绿)
			{
				list.Add(equipItemList[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetBlueEquip(List<EquipItem> equipItemList)
	{
		List<EquipItem> list = new List<EquipItem>();
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < equipItemList.Count; i++)
		{
			if (equipList[equipItemList[i].equipID].equipQuality == Quility_ResAndItemAndSkill.蓝)
			{
				list.Add(equipItemList[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetPurpleEquip(List<EquipItem> equipItemList)
	{
		List<EquipItem> list = new List<EquipItem>();
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < equipItemList.Count; i++)
		{
			if (equipList[equipItemList[i].equipID].equipQuality == Quility_ResAndItemAndSkill.紫)
			{
				list.Add(equipItemList[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetOrangeEquip(List<EquipItem> equipItemList)
	{
		List<EquipItem> list = new List<EquipItem>();
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < equipItemList.Count; i++)
		{
			if (equipList[equipItemList[i].equipID].equipQuality == Quility_ResAndItemAndSkill.红)
			{
				list.Add(equipItemList[i]);
			}
		}
		return list;
	}

	public static List<EquipItem> GetPropertyEquip(List<EquipItem> equipItemList)
	{
		EquipItem value = new EquipItem();
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < equipItemList.Count - 1; i++)
		{
			for (int j = 0; j < equipItemList.Count - 1 - i; j++)
			{
				if (equipList[equipItemList[j].equipID].level < equipList[equipItemList[j + 1].equipID].level)
				{
					value = equipItemList[j];
					equipItemList[j] = equipItemList[j + 1];
					equipItemList[j + 1] = value;
				}
			}
		}
		return equipItemList;
	}

	public static List<EquipItem> GetSortedEquipList(int equipType, long commanderID, int commanderType)
	{
		List<EquipItem> list = new List<EquipItem>();
		List<EquipItem> furnishedEquipItem = EquipSiftManage.GetFurnishedEquipItem(equipType);
		Dictionary<int, Equip> equipList = UnitConst.GetInstance().equipList;
		for (int i = 0; i < furnishedEquipItem.Count; i++)
		{
			if (furnishedEquipItem[i].commanderID == commanderID)
			{
				list.Add(furnishedEquipItem[i]);
				furnishedEquipItem.RemoveAt(i);
			}
		}
		for (int j = 0; j < furnishedEquipItem.Count; j++)
		{
			list.Add(furnishedEquipItem[j]);
		}
		List<EquipItem> notFurnishedEquipItem = EquipSiftManage.GetNotFurnishedEquipItem(equipType);
		List<EquipItem> orangeEquip = EquipSiftManage.GetOrangeEquip(notFurnishedEquipItem);
		for (int k = 0; k < EquipSiftManage.GetPropertyEquip(orangeEquip).Count; k++)
		{
			list.Add(EquipSiftManage.GetPropertyEquip(orangeEquip)[k]);
		}
		List<EquipItem> purpleEquip = EquipSiftManage.GetPurpleEquip(notFurnishedEquipItem);
		for (int l = 0; l < EquipSiftManage.GetPropertyEquip(purpleEquip).Count; l++)
		{
			list.Add(EquipSiftManage.GetPropertyEquip(purpleEquip)[l]);
		}
		List<EquipItem> blueEquip = EquipSiftManage.GetBlueEquip(notFurnishedEquipItem);
		for (int m = 0; m < EquipSiftManage.GetPropertyEquip(blueEquip).Count; m++)
		{
			list.Add(EquipSiftManage.GetPropertyEquip(blueEquip)[m]);
		}
		List<EquipItem> greenEquip = EquipSiftManage.GetGreenEquip(notFurnishedEquipItem);
		for (int n = 0; n < EquipSiftManage.GetPropertyEquip(greenEquip).Count; n++)
		{
			list.Add(EquipSiftManage.GetPropertyEquip(greenEquip)[n]);
		}
		List<EquipItem> whiteEquip = EquipSiftManage.GetWhiteEquip(notFurnishedEquipItem);
		for (int num = 0; num < EquipSiftManage.GetPropertyEquip(whiteEquip).Count; num++)
		{
			list.Add(EquipSiftManage.GetPropertyEquip(whiteEquip)[num]);
		}
		LogManage.Log("可选装备的数量" + list.Count);
		LogManage.Log("可选装备的类型" + commanderType);
		for (int num2 = list.Count - 1; num2 >= 0; num2--)
		{
			if (equipList[list[num2].equipID].commanderType != commanderType)
			{
				list.RemoveAt(num2);
			}
		}
		LogManage.Log("可选装备的数量" + list.Count);
		return list;
	}
}
