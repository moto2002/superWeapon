using System;
using UnityEngine;

public class AtlasManage
{
	private static UIAtlas buildAtlas;

	private static UIAtlas itemAtlas;

	private static UIAtlas shopAtlas1;

	private static UIAtlas shopAtals2;

	private static UIAtlas BigSkillOne;

	private static UIAtlas BigSkillTwo;

	private static UIAtlas BigSkillThree;

	private static UIAtlas BigSkillFour;

	public static void SetSpritName(UISprite sprite, string name)
	{
		sprite.spriteName = name;
	}

	public static void SetArmyIconSpritName(UISprite sprite, int armyIndex)
	{
		sprite.spriteName = UnitConst.GetInstance().soldierConst[armyIndex].icon;
	}

	public static void SetNumSpriteName(UISprite sprite, int name)
	{
		switch (name)
		{
		case 0:
			sprite.spriteName = name.ToString();
			break;
		case 1:
			sprite.spriteName = name.ToString();
			break;
		case 2:
			sprite.spriteName = name.ToString();
			break;
		case 3:
			sprite.spriteName = name.ToString();
			break;
		case 4:
			sprite.spriteName = name.ToString();
			break;
		case 5:
			sprite.spriteName = name.ToString();
			break;
		case 6:
			sprite.spriteName = name.ToString();
			break;
		case 7:
			sprite.spriteName = name.ToString();
			break;
		case 8:
			sprite.spriteName = name.ToString();
			break;
		case 9:
			sprite.spriteName = name.ToString();
			break;
		}
	}

	public static void SetResSpriteName(UISprite sprite, ResType resType)
	{
		switch (resType)
		{
		case ResType.金币:
			sprite.spriteName = "新金矿";
			return;
		case ResType.石油:
			sprite.spriteName = "新石油";
			return;
		case ResType.钢铁:
			sprite.spriteName = "新钢铁";
			return;
		case ResType.稀矿:
			sprite.spriteName = "新稀矿";
			return;
		case ResType.技能点:
		case ResType.天赋点:
		case ResType.指挥官经验:
		case ResType.战绩积分:
		case ResType.探索令:
		case ResType.普通技能抽卡:
		case ResType.传奇技能抽卡:
		case ResType.军团体力:
		case ResType.转盘免费剩余次数:
			IL_66:
			if (resType != ResType.弹药)
			{
				return;
			}
			sprite.spriteName = "弹药";
			return;
		case ResType.钻石:
			sprite.spriteName = "新钻石";
			return;
		case ResType.兵种:
			return;
		case ResType.经验:
			sprite.spriteName = "exp";
			return;
		case ResType.等级:
			return;
		case ResType.奖牌:
			sprite.spriteName = "勋章";
			return;
		case ResType.军令:
			return;
		case ResType.技能碎片:
			sprite.spriteName = "积分";
			return;
		case ResType.电力:
			sprite.spriteName = "dianli";
			return;
		case ResType.破甲:
			sprite.spriteName = "posui";
			return;
		case ResType.电力消耗:
			sprite.spriteName = "dianli";
			return;
		}
		goto IL_66;
	}

	public static void SetQuilitySpriteName(UISprite sprite, Quility_ResAndItemAndSkill quility)
	{
		switch (quility)
		{
		case Quility_ResAndItemAndSkill.白:
			sprite.spriteName = "白";
			break;
		case Quility_ResAndItemAndSkill.绿:
			sprite.spriteName = "绿";
			break;
		case Quility_ResAndItemAndSkill.蓝:
			sprite.spriteName = "蓝";
			break;
		case Quility_ResAndItemAndSkill.紫:
			sprite.spriteName = "紫";
			break;
		case Quility_ResAndItemAndSkill.红:
			sprite.spriteName = "红";
			break;
		}
	}

	public static void SetSkillQuilitySpriteName(UISprite sprite, Quility_ResAndItemAndSkill quility)
	{
		switch (quility)
		{
		case Quility_ResAndItemAndSkill.白:
			sprite.spriteName = "白卡";
			break;
		case Quility_ResAndItemAndSkill.绿:
			sprite.spriteName = "绿卡";
			break;
		case Quility_ResAndItemAndSkill.蓝:
			sprite.spriteName = "蓝卡";
			break;
		case Quility_ResAndItemAndSkill.紫:
			sprite.spriteName = "紫卡";
			break;
		case Quility_ResAndItemAndSkill.红:
			sprite.spriteName = "红卡";
			break;
		}
	}

	public static void SetSkillSpritName(UISprite sprit, string iconId)
	{
		sprit.spriteName = iconId;
	}

	public static void SetSkillFSpritName(UISprite sprit, int skillIndex)
	{
		sprit.spriteName = UnitConst.GetInstance().skillList[skillIndex].fighticon;
	}

	public static void SetSkillSSpritName(UISprite sprit, int skillIndex)
	{
		sprit.spriteName = UnitConst.GetInstance().skillList[skillIndex].icon;
	}

	public static void SetSkillQuilityInBattleSpriteName(UISprite sprite, Quility_ResAndItemAndSkill quility)
	{
		switch (quility)
		{
		case Quility_ResAndItemAndSkill.白:
			sprite.spriteName = "白";
			break;
		case Quility_ResAndItemAndSkill.绿:
			sprite.spriteName = "绿";
			break;
		case Quility_ResAndItemAndSkill.蓝:
			sprite.spriteName = "蓝";
			break;
		case Quility_ResAndItemAndSkill.紫:
			sprite.spriteName = "紫";
			break;
		case Quility_ResAndItemAndSkill.红:
			sprite.spriteName = "红";
			break;
		}
	}

	public static void SetItemUISprite(UISprite item, int itemIndex)
	{
		item.spriteName = UnitConst.GetInstance().ItemConst[itemIndex].IconId;
	}

	public static void SetBuildingAtlas(UISprite item, string iconId)
	{
		if (AtlasManage.buildAtlas == null)
		{
			AtlasManage.buildAtlas = Resources.Load<UIAtlas>("Atlas/Buildings/BuildingAtlas");
		}
		for (int i = 0; i < AtlasManage.buildAtlas.spriteList.Count; i++)
		{
			if (AtlasManage.buildAtlas.spriteList[i].name == iconId)
			{
				item.atlas = AtlasManage.buildAtlas;
				item.spriteName = iconId;
				return;
			}
		}
	}

	public static void SetUiItemAtlas(UISprite item, string iconId)
	{
		if (AtlasManage.itemAtlas == null)
		{
			AtlasManage.itemAtlas = Resources.Load<UIAtlas>("Atlas/Items/ItemAtlas");
		}
		for (int i = 0; i < AtlasManage.itemAtlas.spriteList.Count; i++)
		{
			if (AtlasManage.itemAtlas.spriteList[i].name == iconId)
			{
				item.atlas = AtlasManage.itemAtlas;
				item.spriteName = iconId;
				return;
			}
		}
	}

	public static void SetShopAtals(UISprite shop, string iconId)
	{
		shop.spriteName = iconId;
	}

	public static void SetBigSkillAtlas(UISprite skill, string iconId)
	{
		if (AtlasManage.BigSkillOne == null)
		{
			AtlasManage.BigSkillOne = Resources.Load<UIAtlas>("Atlas/SKill/BigSkillAtlas");
		}
		if (AtlasManage.BigSkillTwo == null)
		{
			AtlasManage.BigSkillTwo = Resources.Load<UIAtlas>("Atlas/SKill/BigSkillAtlasTwo");
		}
		if (AtlasManage.BigSkillThree == null)
		{
			AtlasManage.BigSkillThree = Resources.Load<UIAtlas>("Atlas/SKill/BigSkillAtlasThree");
		}
		if (AtlasManage.BigSkillFour == null)
		{
			AtlasManage.BigSkillFour = Resources.Load<UIAtlas>("Atlas/SKill/BigSkillAtlasFour");
		}
		for (int i = 0; i < AtlasManage.BigSkillOne.spriteList.Count; i++)
		{
			if (AtlasManage.BigSkillOne.spriteList[i].name == iconId)
			{
				skill.atlas = AtlasManage.BigSkillOne;
				skill.spriteName = iconId;
				return;
			}
		}
		for (int j = 0; j < AtlasManage.BigSkillTwo.spriteList.Count; j++)
		{
			if (AtlasManage.BigSkillTwo.spriteList[j].name == iconId)
			{
				skill.atlas = AtlasManage.BigSkillTwo;
				skill.spriteName = iconId;
				return;
			}
		}
		for (int k = 0; k < AtlasManage.BigSkillThree.spriteList.Count; k++)
		{
			if (AtlasManage.BigSkillThree.spriteList[k].name == iconId)
			{
				skill.atlas = AtlasManage.BigSkillThree;
				skill.spriteName = iconId;
				return;
			}
		}
		for (int l = 0; l < AtlasManage.BigSkillFour.spriteList.Count; l++)
		{
			if (AtlasManage.BigSkillFour.spriteList[l].name == iconId)
			{
				skill.atlas = AtlasManage.BigSkillFour;
				skill.spriteName = iconId;
				return;
			}
		}
	}
}
