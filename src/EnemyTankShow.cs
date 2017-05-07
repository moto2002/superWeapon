using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShow : MonoBehaviour
{
	public UILabel nameUIlabel;

	public UILabel[] tankNum;

	public UISprite[] tankIcon;

	public Dictionary<int, NpcAttark> npc;

	public void ShowUI(int wavaid)
	{
		this.npc = UnitConst.GetInstance().npcAttartList;
		NpcAttark npcAttark = this.npc[wavaid];
		for (int i = npcAttark.armyNum.Count; i < this.tankIcon.Length; i++)
		{
			this.tankIcon[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < npcAttark.armyNum.Count; j++)
		{
			this.tankIcon[j].gameObject.SetActive(true);
		}
		int num = 0;
		foreach (int current in npcAttark.armyNum.Keys)
		{
			this.tankIcon[num].spriteName = UnitConst.GetInstance().soldierConst[current].icon;
			this.tankNum[num].text = npcAttark.armyNum[current].ToString();
			num++;
		}
	}
}
