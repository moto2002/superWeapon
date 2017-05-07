using System;
using System.Collections.Generic;
using UnityEngine;

public class VipContent : MonoBehaviour
{
	public UILabel tequan;

	public UILabel zunxian;

	public UITable textTable;

	public UITable itemTable;

	public int level;

	private void Start()
	{
	}

	public void InitInfo(int _level)
	{
		this.level = _level;
		this.tequan.text = string.Format("VIP{0}{1}", this.level, LanguageManage.GetTextByKey("特权", "Vip"));
		this.zunxian.text = string.Format("VIP{0}{1}", this.level, LanguageManage.GetTextByKey("尊享", "Vip"));
		for (int i = 0; i < UnitConst.GetInstance().VipConstData[this.level].rights.Count; i++)
		{
			GameObject gameObject = this.textTable.CreateChildren(i.ToString(), true);
			gameObject.GetComponent<UILabel>().text = LanguageManage.GetTextByKey(UnitConst.GetInstance().VipConstData[this.level].rights[i].description, "Vip");
		}
		this.textTable.Reposition();
		foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().VipConstData[this.level].resReward)
		{
			GameObject gameObject2 = this.itemTable.CreateChildren(current.Key.ToString() + "_res", true);
			AtlasManage.SetResSpriteName(gameObject2.GetComponent<UISprite>(), current.Key);
			gameObject2.transform.Find("num").GetComponent<UILabel>().text = string.Format("X{0}", current.Value);
			ItemTipsShow2 component = gameObject2.GetComponent<ItemTipsShow2>();
			component.Index = (int)current.Key;
			component.Type = 2;
		}
		foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().VipConstData[this.level].itemReward)
		{
			GameObject gameObject3 = this.itemTable.CreateChildren(current2.Key.ToString() + "_item", true);
			AtlasManage.SetItemUISprite(gameObject3.GetComponent<UISprite>(), current2.Key);
			gameObject3.transform.Find("num").GetComponent<UILabel>().text = string.Format("X{0}", current2.Value);
			AtlasManage.SetQuilitySpriteName(gameObject3.transform.Find("qua").GetComponent<UISprite>(), UnitConst.GetInstance().ItemConst[current2.Key].Quality);
			ItemTipsShow2 component2 = gameObject3.GetComponent<ItemTipsShow2>();
			component2.Index = current2.Key;
			component2.Type = 1;
		}
		foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().VipConstData[this.level].skillReward)
		{
			GameObject gameObject4 = this.itemTable.CreateChildren(current3.Key.ToString() + "_skill", true);
			AtlasManage.SetSkillSSpritName(gameObject4.GetComponent<UISprite>(), current3.Key);
			gameObject4.transform.Find("num").GetComponent<UILabel>().text = string.Format("X{0}", current3.Value);
			AtlasManage.SetQuilitySpriteName(gameObject4.transform.Find("qua").GetComponent<UISprite>(), UnitConst.GetInstance().skillList[current3.Key].skillQuality);
			ItemTipsShow2 component3 = gameObject4.GetComponent<ItemTipsShow2>();
			component3.Index = current3.Key;
			component3.Type = 3;
		}
		this.itemTable.Reposition();
	}
}
