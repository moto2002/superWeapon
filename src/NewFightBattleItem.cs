using System;
using System.Collections.Generic;
using UnityEngine;

public class NewFightBattleItem : MonoBehaviour
{
	public UILabel nameUIlable;

	public UITexture Bg;

	public UISprite start;

	public UILabel startNum;

	public UISprite Btn;

	public UISprite itemIcon;

	public UISprite itemBg;

	public GameObject lockIcon;

	public Battle item;

	public bool isNowBattle;

	public List<Transform> dianList = new List<Transform>();

	private void Awake()
	{
		this.Initialize();
	}

	private void Initialize()
	{
		this.nameUIlable = base.transform.FindChild("Name").GetComponent<UILabel>();
		this.Bg = base.transform.GetComponent<UITexture>();
		this.start = base.transform.FindChild("Start").GetComponent<UISprite>();
		this.startNum = base.transform.FindChild("Start/Num").GetComponent<UILabel>();
		this.Btn = base.transform.FindChild("AwardBtn").GetComponent<UISprite>();
		this.itemIcon = base.transform.FindChild("ArticleBG/Icon").GetComponent<UISprite>();
		this.itemBg = base.transform.FindChild("ArticleBG").GetComponent<UISprite>();
		this.lockIcon = base.transform.FindChild("Lock").gameObject;
	}

	private void Start()
	{
	}

	public void ShowBattleItem()
	{
		this.nameUIlable.text = LanguageManage.GetTextByKey(this.item.name, "Battle");
		AtlasManage.SetUiItemAtlas(this.itemIcon, UnitConst.GetInstance().ItemConst[1].IconId);
		this.startNum.text = this.item.FightRecord.star + "/" + this.item.allBattleField.Count * 3;
		this.itemBg.spriteName = this.SetQuSprite((int)UnitConst.GetInstance().ItemConst[UnitConst.GetInstance().BattleConst[this.item.id].stagePrize[1]].Quality);
		if (this.item.FightRecord.isFight)
		{
			this.nameUIlable.color = Color.green;
			this.Bg.ShaderToNormal();
			this.start.spriteName = "星星";
			this.Btn.spriteName = "购买按钮";
			this.itemIcon.ShaderToNormal();
			this.itemBg.ShaderToNormal();
			this.lockIcon.SetActive(false);
		}
		else if (this.isNowBattle)
		{
			this.nameUIlable.color = Color.blue;
			this.SetUITexture(this.Bg, "Texture/", "战役奖励领取托盘");
			this.start.spriteName = "胜利星星";
			this.Btn.spriteName = "购买按钮";
			this.itemIcon.ShaderToNormal();
			this.itemBg.ShaderToNormal();
			this.lockIcon.SetActive(false);
		}
		else
		{
			this.nameUIlable.color = Color.gray;
			this.SetUITexture(this.Bg, "Texture/", "战役奖励领取托盘灰");
			this.start.spriteName = "胜利星星灰";
			this.Btn.spriteName = "灰色按钮";
			this.lockIcon.SetActive(true);
		}
		if (this.item.FightRecord.star == this.item.allBattleField.Count * 3)
		{
			this.Btn.spriteName = "购买按钮";
		}
		else
		{
			this.Btn.spriteName = "升级、确定按钮灰";
		}
	}

	public void SetUITexture(UITexture texture, string path, string name)
	{
		texture.mainTexture = (Resources.Load(path + name) as Texture);
	}

	public string SetQuSprite(int quaty)
	{
		switch (quaty)
		{
		case 1:
			return "白";
		case 2:
			return "绿";
		case 3:
			return "蓝";
		case 4:
			return "紫";
		case 5:
			return "红";
		default:
			return string.Empty;
		}
	}
}
