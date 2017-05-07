using msg;
using System;
using UnityEngine;

public class topTenItem : MonoBehaviour
{
	private int rank;

	public UILabel topNum;

	public UILabel lv;

	public UILabel Name;

	public UILabel topTypeValue;

	public UISprite topBack;

	public UISprite topTypeBack;

	public GameObject visitBtn;

	public void NextRankData()
	{
		TopTenPanelManage._ins.InitRankingData(this.rank);
	}

	private void Awake()
	{
	}

	public void InitData(RankingData data)
	{
		this.lv.text = "(Lv." + data.level.ToString() + ")";
		this.Name.text = data.name;
		this.topTypeValue.text = data.score.ToString();
		this.topNum.text = data.rank.ToString();
		this.rank = data.rank;
		this.visitBtn.name = data.userId.ToString();
		if (HeroInfo.GetInstance().userId == data.userId)
		{
			this.visitBtn.SetActive(false);
		}
		switch (data.rank)
		{
		case 1:
			this.topBack.spriteName = "1";
			break;
		case 2:
			this.topBack.spriteName = "2";
			break;
		case 3:
			this.topBack.spriteName = "3";
			break;
		default:
			this.topBack.spriteName = "4";
			this.topBack.SetDimensions(57, 57);
			break;
		}
	}
}
