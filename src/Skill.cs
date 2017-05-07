using System;
using System.Collections.Generic;

public class Skill
{
	public int id;

	public string name;

	public int skillType;

	public int sceneType;

	public int targetType;

	public int level;

	public int castTime;

	public int coldDown;

	public int basecost;

	public int xishu;

	public int basePower;

	public int renjuType;

	public int renju;

	public string renjuCd;

	public int attarkRadius;

	public int hurtRadius;

	public int hitCount;

	public int[] buffId;

	public string icon;

	public string Ficon;

	public string fighticon;

	public Quility_ResAndItemAndSkill skillQuality;

	public string effect;

	public string fightEffect;

	public string bodyEffect;

	public string damageEffect;

	public string circleEffect;

	private string description;

	public int sellPrice;

	public int needChips;

	public int skillVolume;

	public string Description
	{
		get
		{
			return this.ReplaceDescription();
		}
		set
		{
			this.description = value;
		}
	}

	private string ReplaceDescription()
	{
		if (!HeroInfo.GetInstance().SkillInfo.ContainsKey(this.skillType))
		{
			return LanguageManage.GetTextByKey(this.description, "skill");
		}
		if (this.skillType == 16)
		{
			int key = this.buffId[0] + 3;
			if (UnitConst.GetInstance().BuffConst.ContainsKey(key))
			{
				return LanguageManage.GetTextByKey(this.description, "skill").Replace("[attack]", UnitConst.GetInstance().BuffConst[key].lifeTime.ToString());
			}
		}
		if (this.basePower <= 0 && UnitConst.GetInstance().BuffConst.ContainsKey(this.buffId[0]))
		{
			return LanguageManage.GetTextByKey(this.description, "skill").Replace("[attack]", UnitConst.GetInstance().BuffConst[this.buffId[0]].power.ToString());
		}
		if (this.id == 1001)
		{
			int num = 1;
			foreach (KeyValuePair<int, PlayerCommando> current in HeroInfo.GetInstance().PlayerCommandoes)
			{
				if (current.Value.index == 2)
				{
					num = current.Value.skillLevel;
					break;
				}
			}
			return LanguageManage.GetTextByKey(this.description, "skill").Replace("[attack]", ((int)((float)this.basePower * (1f + (float)num * 0.1f))).ToString());
		}
		if (this.id == 1002)
		{
			int num2 = 1;
			foreach (KeyValuePair<int, PlayerCommando> current2 in HeroInfo.GetInstance().PlayerCommandoes)
			{
				if (current2.Value.index == 1)
				{
					num2 = current2.Value.skillLevel;
					break;
				}
			}
			return LanguageManage.GetTextByKey(this.description, "skill").Replace("[attack]", ((int)((float)this.basePower * (1f + (float)num2 * 0.1f))).ToString());
		}
		return LanguageManage.GetTextByKey(this.description, "skill").Replace("[attack]", ((int)((float)this.basePower * (1f + (float)HeroInfo.GetInstance().SkillInfo[this.skillType] * 0.1f))).ToString());
	}
}
