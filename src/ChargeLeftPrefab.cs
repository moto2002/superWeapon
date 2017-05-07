using System;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLeftPrefab : IMonoBehaviour
{
	public GameObject red;

	public GameObject xianGou;

	private BoxCollider boxCol;

	private ButtonClick btnClick;

	private UIButtonScale btnScale;

	public UILabel activtiyName;

	public int actityType;

	public GameObject SelectSprite;

	public GameObject RightPanel;

	public UIButtonScale BtnScale
	{
		get
		{
			if (!this.btnScale)
			{
				this.btnScale = base.GetComponent<UIButtonScale>();
			}
			return this.btnScale;
		}
	}

	public ButtonClick BtnClick
	{
		get
		{
			if (!this.btnClick)
			{
				this.btnClick = base.GetComponent<ButtonClick>();
			}
			return this.btnClick;
		}
	}

	public BoxCollider BoxCol
	{
		get
		{
			if (!this.boxCol)
			{
				this.boxCol = base.GetComponent<BoxCollider>();
			}
			return this.boxCol;
		}
	}

	public void OnEnable()
	{
		if (this.actityType != 0)
		{
			this.isShowRed(this.actityType);
		}
	}

	public void isShowRed(int activityTtype)
	{
		this.actityType = activityTtype;
		List<ActivityClass> list = ChargeActityPanel.GetRegCharges[activityTtype];
		this.red.SetActive(false);
		if (activityTtype == 19)
		{
			if (HeroInfo.GetInstance().LotteryDataFreeTimes > 0)
			{
				this.red.SetActive(true);
			}
		}
		else if (activityTtype == 1)
		{
			if (GetawardPanelShow.getId.Count > 0)
			{
				this.red.SetActive(true);
			}
		}
		else
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].isCanGetAward)
				{
					this.red.SetActive(true);
					break;
				}
			}
		}
		if (list[0].conditionType == 13)
		{
			this.xianGou.SetActive(true);
		}
	}
}
