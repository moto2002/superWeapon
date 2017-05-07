using System;
using UnityEngine;

public class resTextInfo : MonoBehaviour
{
	public UILabel resName;

	public UISprite resSprite;

	public UILabel resValue;

	public UILabel addValue;

	public UILabel resLabel;

	public T_InfoTextType curT_InfoTextType;

	public SpecialPro curTechUpTexttype;

	private string text_Tips;

	private void Awake()
	{
	}

	public void OnPress(bool isPress)
	{
		if (isPress)
		{
			if (!string.IsNullOrEmpty(this.text_Tips))
			{
				HUDTextTool.inst.TextTips.OnDown(base.gameObject, this.text_Tips);
			}
		}
		else
		{
			HUDTextTool.inst.TextTips.OnUp();
		}
	}

	public void SetT_InfoUpdateResText(string _resValue, int _addValue, string tips = "")
	{
		this.text_Tips = tips;
		switch (this.curT_InfoTextType)
		{
		case T_InfoTextType.金币产量:
			this.resName.text = LanguageManage.GetTextByKey("金币产出", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.金币);
			break;
		case T_InfoTextType.金币储量:
			this.resName.text = LanguageManage.GetTextByKey("金币储量", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.金币);
			break;
		case T_InfoTextType.石油产量:
			this.resName.text = LanguageManage.GetTextByKey("石油产出", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.石油);
			break;
		case T_InfoTextType.石油储量:
			this.resName.text = LanguageManage.GetTextByKey("石油储量", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.石油);
			break;
		case T_InfoTextType.钢铁产量:
			this.resName.text = LanguageManage.GetTextByKey("钢铁产出", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.钢铁);
			break;
		case T_InfoTextType.钢铁储量:
			this.resName.text = LanguageManage.GetTextByKey("钢铁储量", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.钢铁);
			break;
		case T_InfoTextType.稀土产量:
			this.resName.text = LanguageManage.GetTextByKey("稀矿产出", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.稀矿);
			break;
		case T_InfoTextType.稀矿储量:
			this.resName.text = LanguageManage.GetTextByKey("稀矿储量", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.稀矿);
			break;
		case T_InfoTextType.生命值:
			this.resName.text = LanguageManage.GetTextByKey("生命值", "build") + ":";
			this.resSprite.spriteName = "shengming";
			break;
		case T_InfoTextType.经验值:
			this.resName.text = LanguageManage.GetTextByKey("经验增加", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.经验);
			break;
		case T_InfoTextType.攻击值:
			this.resName.text = LanguageManage.GetTextByKey("攻击值", "build") + ":";
			this.resSprite.spriteName = "att";
			break;
		case T_InfoTextType.防御值:
			this.resName.text = LanguageManage.GetTextByKey("防御值", "build") + ":";
			this.resSprite.spriteName = "fangyu";
			break;
		case T_InfoTextType.受保护百分比:
			this.resName.text = LanguageManage.GetTextByKey("保护资源", "build") + ":";
			this.resSprite.spriteName = "受保护百分比";
			break;
		case T_InfoTextType.伤害值:
			this.resName.text = LanguageManage.GetTextByKey("伤害值", "build") + ":";
			this.resSprite.spriteName = "att";
			break;
		case T_InfoTextType.最大容量:
			this.resName.text = LanguageManage.GetTextByKey("最大容量", "build") + ":";
			switch (T_InfoPanelManage.BuildingResType)
			{
			case 6:
				this.resSprite.spriteName = "新金矿";
				break;
			case 7:
				this.resSprite.spriteName = "新石油";
				break;
			case 8:
				this.resSprite.spriteName = "新钢铁";
				break;
			case 9:
				this.resSprite.spriteName = "新稀矿";
				break;
			}
			switch (BuildingStorePanelManage.BuildingResType)
			{
			case 6:
				this.resSprite.spriteName = "新金矿";
				break;
			case 7:
				this.resSprite.spriteName = "新石油";
				break;
			case 8:
				this.resSprite.spriteName = "新钢铁";
				break;
			case 9:
				this.resSprite.spriteName = "新稀矿";
				break;
			}
			break;
		case T_InfoTextType.产电量:
			this.resName.text = LanguageManage.GetTextByKey("产电量", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.电力);
			break;
		case T_InfoTextType.电力消耗:
			this.resName.text = LanguageManage.GetTextByKey("电力消耗", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.电力消耗);
			break;
		case T_InfoTextType.破甲:
			this.resName.text = LanguageManage.GetTextByKey("破甲", "build") + ":";
			AtlasManage.SetResSpriteName(this.resSprite, ResType.破甲);
			break;
		case T_InfoTextType.升级消耗时间:
			this.resName.text = LanguageManage.GetTextByKey("升级时间", "build") + ":";
			this.resSprite.spriteName = "秒钟";
			break;
		case T_InfoTextType.暴击率:
			this.resName.text = LanguageManage.GetTextByKey("暴击率", "build") + ":";
			this.resSprite.spriteName = "crit";
			break;
		case T_InfoTextType.暴击抵抗:
			this.resName.text = LanguageManage.GetTextByKey("暴击抵抗", "build") + ":";
			this.resSprite.spriteName = "resist";
			break;
		case T_InfoTextType.伤害范围:
			this.resName.text = LanguageManage.GetTextByKey("伤害范围", "build") + ":";
			this.resSprite.spriteName = "hurtRadius";
			break;
		case T_InfoTextType.额外伤害:
			this.resName.text = LanguageManage.GetTextByKey("额外伤害", "build") + ":";
			this.resSprite.spriteName = "trueDmg";
			break;
		case T_InfoTextType.射速:
			this.resName.text = LanguageManage.GetTextByKey("射速", "build") + ":";
			this.resSprite.spriteName = "shootSpeed";
			break;
		case T_InfoTextType.射程:
			this.resName.text = LanguageManage.GetTextByKey("射程", "build") + ":";
			this.resSprite.spriteName = "shootLength";
			break;
		}
		if (this.curT_InfoTextType == T_InfoTextType.受保护百分比)
		{
			this.resValue.text = _resValue.ToString() + "%";
			if (_addValue > 0)
			{
				this.addValue.gameObject.SetActive(true);
				this.addValue.text = _addValue.ToString() + "%";
			}
			else
			{
				this.addValue.gameObject.SetActive(false);
			}
		}
		else
		{
			this.resValue.text = _resValue.ToString();
			if (_addValue > 0)
			{
				this.addValue.gameObject.SetActive(true);
				this.addValue.text = _addValue.ToString();
			}
			else
			{
				this.addValue.gameObject.SetActive(false);
			}
		}
	}

	public void SetTechUpdateResText(int _resValue, int _addValue, string tips = "")
	{
		this.text_Tips = tips;
		switch (this.curTechUpTexttype)
		{
		case SpecialPro.生命值:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("生命值", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "shengming";
			}
			break;
		case SpecialPro.攻击1:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("攻击", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "att";
			}
			break;
		case SpecialPro.攻击2:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("攻击", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "att";
			}
			break;
		case SpecialPro.防御1:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("防御", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "fangyu";
			}
			break;
		case SpecialPro.防御2:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("防御", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "fangyu";
			}
			break;
		case SpecialPro.暴击率:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("暴击率", "build") + ":";
			}
			break;
		case SpecialPro.暴击抵抗:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("暴击抵抗", "build") + ":";
			}
			break;
		case SpecialPro.溅射伤害1:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("溅射伤害", "build") + ":";
			}
			break;
		case SpecialPro.溅射伤害2:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("溅射伤害", "build") + ":";
			}
			break;
		case SpecialPro.穿甲:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("穿甲", "build") + ":";
			}
			break;
		case SpecialPro.破甲:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("破甲", "build") + ":";
			}
			break;
		case SpecialPro.干扰效果:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("干扰效果", "build") + ":";
			}
			break;
		case SpecialPro.生命回复:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("生命回复", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "shengming";
			}
			break;
		case SpecialPro.持续伤害:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("持续伤害", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "att";
			}
			break;
		case SpecialPro.攻速:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("攻速", "build") + ":";
			}
			break;
		case SpecialPro.技能伤害:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("伤害", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "att";
			}
			break;
		case SpecialPro.资源保护百分比:
			if (this.resName)
			{
				this.resName.text = LanguageManage.GetTextByKey("资源保护百分比", "build") + ":";
			}
			if (this.resSprite)
			{
				this.resSprite.spriteName = "防御";
			}
			break;
		}
		this.resValue.text = _resValue.ToString();
		if (_addValue > 0)
		{
			this.addValue.gameObject.SetActive(true);
			this.addValue.text = _addValue.ToString();
		}
		else
		{
			this.addValue.gameObject.SetActive(false);
		}
	}
}
