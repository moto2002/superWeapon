using System;
using UnityEngine;

public class SkillCarteItem : IMonoBehaviour
{
	public UISprite icon;

	public UISprite type;

	public UISprite tips;

	public UILabel _name;

	public UILabel des;

	public UILabel tipLabel;

	public SkillCarteData item;

	public bool isSkill;

	public UILabel count;

	public GameObject coinCost;

	public UILabel coinUILabel;

	public int skillID;

	public int skillIndex;

	public override void Awake()
	{
		base.Awake();
		this.Initialize();
	}

	private void Initialize()
	{
		if (!this.icon)
		{
			this.icon = base.transform.FindChild("Icon").GetComponent<UISprite>();
		}
		if (!this.type)
		{
			this.type = base.transform.FindChild("type").GetComponent<UISprite>();
		}
		if (!this._name)
		{
			this._name = base.transform.FindChild("Name").GetComponent<UILabel>();
		}
		if (!this.des && base.transform.FindChild("Des"))
		{
			this.des = base.transform.FindChild("Des").GetComponent<UILabel>();
		}
	}

	public void ShowItem()
	{
		Skill skill = UnitConst.GetInstance().skillList[this.item.itemID];
		this._name.text = LanguageManage.GetTextByKey(skill.name, "skill");
		this.des.text = skill.Description;
		if (this.tips)
		{
			SkillTipshow compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<SkillTipshow>(this.tips.gameObject);
			compentIfNoAddOne.Index = skill.id;
			compentIfNoAddOne.type = 1;
			compentIfNoAddOne.JianTouPostion = 2;
		}
		AtlasManage.SetBigSkillAtlas(this.icon, skill.Ficon);
		if (this.coinUILabel)
		{
			this.coinUILabel.text = skill.sellPrice.ToString();
		}
		switch (skill.skillQuality)
		{
		case Quility_ResAndItemAndSkill.白:
			this.type.spriteName = "绿卡";
			break;
		case Quility_ResAndItemAndSkill.绿:
			this.type.spriteName = "绿卡";
			break;
		case Quility_ResAndItemAndSkill.蓝:
			this.type.spriteName = "蓝卡";
			break;
		case Quility_ResAndItemAndSkill.紫:
			this.type.spriteName = "紫卡";
			break;
		case Quility_ResAndItemAndSkill.红:
			this.type.spriteName = "橙卡";
			break;
		}
	}

	public void ShowItem(int id)
	{
		this.skillID = id;
		Skill skill = UnitConst.GetInstance().skillList[id];
		this._name.text = LanguageManage.GetTextByKey(skill.name, "skill");
		this.des.text = skill.Description;
		AtlasManage.SetBigSkillAtlas(this.icon, skill.Ficon);
		if (this.coinUILabel)
		{
			this.coinUILabel.text = skill.sellPrice.ToString();
		}
		switch (skill.skillQuality)
		{
		case Quility_ResAndItemAndSkill.白:
			this.type.spriteName = "绿卡";
			break;
		case Quility_ResAndItemAndSkill.绿:
			this.type.spriteName = "绿卡";
			break;
		case Quility_ResAndItemAndSkill.蓝:
			this.type.spriteName = "蓝卡";
			break;
		case Quility_ResAndItemAndSkill.紫:
			this.type.spriteName = "紫卡";
			break;
		case Quility_ResAndItemAndSkill.红:
			this.type.spriteName = "橙卡";
			break;
		}
	}

	public void RefreshSold()
	{
	}
}
