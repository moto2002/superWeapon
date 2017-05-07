using System;
using UnityEngine;

public class NBattleItem : MonoBehaviour
{
	public Battle item;

	public bool isNowBattle;

	public UISprite bg;

	private void Awake()
	{
		this.Initialize();
	}

	private void Initialize()
	{
		this.bg = base.transform.GetComponent<UISprite>();
	}

	public void ShowBattleItem()
	{
		if (this.item.FightRecord.isFight)
		{
			this.bg.spriteName = "战役通关标志";
		}
		else if (this.isNowBattle)
		{
			this.bg.spriteName = "战役开启标志";
		}
		else
		{
			this.bg.spriteName = "战役未开启标志";
		}
	}
}
