using System;
using System.Collections.Generic;
using UnityEngine;

public class NewTollgateItem : MonoBehaviour
{
	public BattleField item;

	public List<GameObject> startList = new List<GameObject>();

	public List<GameObject> startBgList = new List<GameObject>();

	public bool isCurrentTollgate;

	public UISprite bg;

	public UILabel nameUIlabel;

	public GameObject ClearanceIcon;

	private void Awake()
	{
		this.ClearanceIcon = base.transform.FindChild("ClearanceIcon").gameObject;
		this.bg = base.transform.GetComponent<UISprite>();
	}

	public void ShowTollgate()
	{
		this.nameUIlabel.text = this.item.name;
		if (this.item.fightRecord.star <= 0)
		{
			for (int i = 0; i < this.startBgList.Count; i++)
			{
				this.startBgList[i].SetActive(false);
			}
			for (int j = 0; j < this.startList.Count; j++)
			{
				this.startList[j].SetActive(false);
			}
		}
		else
		{
			for (int k = 0; k < this.startBgList.Count; k++)
			{
				this.startBgList[k].SetActive(true);
			}
			for (int l = 0; l < this.startList.Count; l++)
			{
				this.startList[l].SetActive(l + 1 <= this.item.fightRecord.star);
			}
		}
		if (this.item.fightRecord.isFight)
		{
			this.bg.spriteName = "战役地图关卡标记";
			this.ClearanceIcon.SetActive(true);
		}
		else if (this.isCurrentTollgate)
		{
			this.bg.spriteName = "战役关卡开启标记";
			this.ClearanceIcon.SetActive(true);
		}
		else
		{
			this.bg.spriteName = "战役地图关卡标记";
			this.ClearanceIcon.SetActive(false);
		}
	}
}
