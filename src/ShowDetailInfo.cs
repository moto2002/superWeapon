using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShowDetailInfo : MonoBehaviour
{
	public static ShowDetailInfo specialInfo;

	public UILabel soliderName;

	public UILabel soliderLevel;

	public UILabel soliderExp;

	public UILabel tankName;

	public UILabel tankeDes;

	public UISprite tankIcon;

	public resTextInfo[] showResInfo;

	public SpecialSoliderSkill[] spcialSkill;

	public SpecialSoliderExp[] specialExp;

	public UISprite ExpForward;

	public UISprite UpdateStarForward;

	public UILabel updateStar;

	public UILabel upStarCoin;

	public GameObject[] stars;

	public GameObject updateStarBtn;

	public GameObject deadHide;

	public int NeedRoleLevel;

	public GameObject deadShow;

	public UILabel showRelieveTime;

	public int _itemId;

	public GameObject buySolider;

	public UILabel showMoney;

	public GameObject labelPanel;

	public UISprite reliveTimeSprite;

	public bool isHaveSolier;

	public bool CoinEnough;

	public UILabel showReliveCount;

	public UISprite itemName;

	private DateTime beginTime;

	private DateTime endTime;

	private void Awake()
	{
		ShowDetailInfo.specialInfo = this;
	}

	public void SpecialSoliderInfo(int id)
	{
		this._itemId = id;
		for (int i = 0; i < this.stars.Length; i++)
		{
			this.stars[i].SetActive(false);
		}
		SepcialSoliderPanel.ins.ShowReliveCount();
		LogManage.LogError(HeroInfo.GetInstance().PlayerCommandoes.Count);
		PlayerCommando playerCommando = null;
		foreach (KeyValuePair<int, PlayerCommando> current in HeroInfo.GetInstance().PlayerCommandoes)
		{
			if (current.Value.index == id)
			{
				playerCommando = current.Value;
				break;
			}
		}
		if (SepcialSoliderPanel.specialSolider[id].preItemid == 0)
		{
			SepcialSoliderPanel.ins.moveLeft.SetActive(false);
		}
		else
		{
			SepcialSoliderPanel.ins.moveLeft.SetActive(true);
		}
		if (SepcialSoliderPanel.specialSolider[id].nextItemID == 0)
		{
			SepcialSoliderPanel.ins.moveRight.SetActive(false);
		}
		else
		{
			SepcialSoliderPanel.ins.moveRight.SetActive(true);
		}
		if (playerCommando == null)
		{
			this.isHaveSolier = false;
			this.buySolider.gameObject.SetActive(true);
			this.deadHide.gameObject.SetActive(false);
			this.showMoney.text = UnitConst.GetInstance().soldierList[id].unLock.ToString();
		}
		else
		{
			this.isHaveSolier = true;
			this.buySolider.gameObject.SetActive(false);
			this.deadHide.gameObject.SetActive(true);
		}
		int num = (playerCommando != null) ? playerCommando.star : UnitConst.GetInstance().soldierList[id].star;
		int num2 = (playerCommando != null) ? playerCommando.level : 1;
		int skillLevel = (playerCommando != null) ? playerCommando.skillLevel : 1;
		int num3 = (playerCommando != null) ? playerCommando.exp : 0;
		int id2 = UnitConst.GetInstance().skillUpdateConst.Values.SingleOrDefault((SkillUpdate a) => a.itemId == UnitConst.GetInstance().skillList[UnitConst.GetInstance().soldierList[id].skillID].skillType && a.level == skillLevel).id;
		if (playerCommando != null)
		{
			SepcialSoliderPanel.specialSolider[id].Idel();
			this.deadHide.gameObject.SetActive(true);
			this.deadShow.gameObject.SetActive(false);
			if (num < UnitConst.GetInstance().MaxSpecialSoliderStar(id))
			{
				this.labelPanel.SetActive(false);
				this.deadHide.SetActive(true);
			}
			else
			{
				this.deadHide.SetActive(false);
				this.labelPanel.SetActive(true);
			}
		}
		else
		{
			SepcialSoliderPanel.ins.funcArmy.SetActive(false);
			this.labelPanel.SetActive(false);
			this.deadShow.SetActive(false);
		}
		this.NeedRoleLevel = ((playerCommando != null) ? UnitConst.GetInstance().skillUpdateConst[id2].commandLevel : 0);
		this.soliderName.text = ((playerCommando != null) ? LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierList[id].name, "soldier") : LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierList[id].name, "soldier"));
		this.soliderLevel.text = string.Format("LV.{0}", num2);
		this.soliderExp.text = num3 + "/" + UnitConst.GetInstance().soldierExpSetConst[num2].exp[num];
		this.ExpForward.fillAmount = (float)num3 * 1f / (float)UnitConst.GetInstance().soldierExpSetConst[num2].exp[num];
		this.tankIcon.spriteName = UnitConst.GetInstance().soldierConst[UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].armyId].icon;
		this.tankName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].armyId].name, "Army");
		this.tankeDes.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].des, "soldier");
		int life = UnitConst.GetInstance().soldierLevelSetConst[new Vector2((float)id, (float)num2)].life;
		int breakArmor = UnitConst.GetInstance().soldierLevelSetConst[new Vector2((float)id, (float)num2)].breakArmor;
		int defBreak = UnitConst.GetInstance().soldierLevelSetConst[new Vector2((float)id, (float)num2)].defBreak;
		this.showResInfo[0].resValue.text = this.GetAllData(T_InfoTextType.生命值, life).ToString();
		this.showResInfo[1].resValue.text = this.GetAllData(T_InfoTextType.攻击值, breakArmor).ToString();
		this.showResInfo[2].resValue.text = this.GetAllData(T_InfoTextType.防御值, defBreak).ToString();
		resTextInfo component = this.showResInfo[0].GetComponent<resTextInfo>();
		component.curT_InfoTextType = T_InfoTextType.生命值;
		component.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.生命值, life).ToString(), 0, this.GetTextTips(T_InfoTextType.生命值, life));
		resTextInfo component2 = this.showResInfo[1].GetComponent<resTextInfo>();
		component2.curT_InfoTextType = T_InfoTextType.攻击值;
		component2.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.攻击值, breakArmor).ToString(), 0, this.GetTextTips(T_InfoTextType.攻击值, breakArmor));
		resTextInfo component3 = this.showResInfo[2].GetComponent<resTextInfo>();
		component3.curT_InfoTextType = T_InfoTextType.防御值;
		component3.SetT_InfoUpdateResText(this.GetAllData(T_InfoTextType.防御值, defBreak).ToString(), 0, this.GetTextTips(T_InfoTextType.防御值, defBreak));
		foreach (KeyValuePair<int, int> current2 in UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpItem)
		{
			AtlasManage.SetUiItemAtlas(this.itemName, UnitConst.GetInstance().ItemConst[current2.Key].IconId);
			if (this.itemName.GetComponent<ItemTipsShow2>())
			{
				ItemTipsShow2 component4 = this.itemName.GetComponent<ItemTipsShow2>();
				component4.JianTouPostion = 2;
				component4.Type = 1;
				component4.Index = current2.Key;
			}
			if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(current2.Key))
			{
				this.updateStar.text = string.Format("{0}", HeroInfo.GetInstance().PlayerItemInfo[current2.Key] + "/" + UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpItem[current2.Key]);
				this.UpdateStarForward.fillAmount = (float)HeroInfo.GetInstance().PlayerItemInfo[current2.Key] * 1f / (float)UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpItem[current2.Key];
				if (HeroInfo.GetInstance().PlayerItemInfo[current2.Key] >= UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpItem[current2.Key])
				{
					SepcialSoliderPanel.isItemEnough = true;
				}
				else
				{
					SepcialSoliderPanel.isItemEnough = false;
				}
			}
			else
			{
				this.updateStar.text = string.Format("{0}", 0 + "/" + UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpItem[current2.Key]);
				this.UpdateStarForward.fillAmount = 0f / (float)UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpItem[current2.Key];
			}
			this.updateStarBtn.name = current2.Key.ToString();
		}
		foreach (KeyValuePair<int, int> current3 in UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpRes)
		{
			this.upStarCoin.text = UnitConst.GetInstance().soldierUpSetConst[new Vector2((float)id, (float)num)].starUpRes[current3.Key].ToString();
		}
		AtlasManage.SetBigSkillAtlas(this.spcialSkill[0].skilIcon, UnitConst.GetInstance().skillList[UnitConst.GetInstance().soldierList[id].skillID].Ficon);
		if (this.spcialSkill[0].skilIcon.GetComponent<SkillTipshow>() && playerCommando != null)
		{
			SkillTipshow component5 = this.spcialSkill[0].skilIcon.GetComponent<SkillTipshow>();
			component5.type = 2;
			component5.JianTouPostion = 2;
			component5.Index = UnitConst.GetInstance().soldierList[id].skillID;
		}
		else
		{
			this.spcialSkill[0].skilIcon.gameObject.AddComponent<SkillTipshow>();
			SkillTipshow component6 = this.spcialSkill[0].skilIcon.GetComponent<SkillTipshow>();
			component6.type = 2;
			component6.JianTouPostion = 2;
			component6.Index = UnitConst.GetInstance().soldierList[id].skillID;
		}
		this.spcialSkill[0].skillLevel.text = string.Format("LV.{0}", (playerCommando != null) ? playerCommando.skillLevel : 1);
		this.spcialSkill[0].skillName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillList[UnitConst.GetInstance().soldierList[id].skillID].name, "skill");
		this.spcialSkill[0].updateBTN.name = id.ToString();
		this.spcialSkill[0].skillpoint.text = UnitConst.GetInstance().skillUpdateConst[id2].needSkillPoint.ToString();
		this.spcialSkill[0].coinCount.text = UnitConst.GetInstance().skillUpdateConst[id2].resCost[ResType.金币].ToString();
		this.specialExp[0].itemName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[4].Name, "item");
		if (HeroInfo.GetInstance().playerRes.resCoin > UnitConst.GetInstance().skillUpdateConst[id2].resCost[ResType.金币])
		{
			this.CoinEnough = true;
		}
		else
		{
			this.CoinEnough = false;
		}
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(4))
		{
			if (HeroInfo.GetInstance().PlayerItemInfo[4] <= 0)
			{
				this.specialExp[0].itemcount.color = Color.red;
			}
			this.specialExp[0].itemcount.text = HeroInfo.GetInstance().PlayerItemInfo[4].ToString();
		}
		else
		{
			this.specialExp[0].itemcount.text = "0";
			this.specialExp[0].itemcount.color = Color.red;
		}
		this.specialExp[1].itemName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[5].Name, "item");
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(5))
		{
			if (HeroInfo.GetInstance().PlayerItemInfo[5] <= 0)
			{
				this.specialExp[1].itemcount.color = Color.red;
			}
			this.specialExp[1].itemcount.text = HeroInfo.GetInstance().PlayerItemInfo[5].ToString();
		}
		else
		{
			this.specialExp[1].itemcount.text = "0";
			this.specialExp[1].itemcount.color = Color.red;
		}
		this.specialExp[2].itemName.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[6].Name, "item");
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(6))
		{
			if (HeroInfo.GetInstance().PlayerItemInfo[6] <= 0)
			{
				this.specialExp[2].itemcount.color = Color.red;
			}
			this.specialExp[2].itemcount.text = HeroInfo.GetInstance().PlayerItemInfo[6].ToString();
		}
		else
		{
			this.specialExp[2].itemcount.text = "0";
			this.specialExp[2].itemcount.color = Color.red;
		}
		this.specialExp[0].itemDes.text = "EXP:" + UnitConst.GetInstance().ItemConst[4].GiveRes[ResType.指挥官经验].ToString();
		this.specialExp[1].itemDes.text = "EXP:" + UnitConst.GetInstance().ItemConst[5].GiveRes[ResType.指挥官经验].ToString();
		this.specialExp[2].itemDes.text = "EXP:" + UnitConst.GetInstance().ItemConst[6].GiveRes[ResType.指挥官经验].ToString();
		for (int j = 0; j < num; j++)
		{
			this.stars[j].SetActive(true);
		}
	}

	public int GetAllData(T_InfoTextType type, int n)
	{
		int num = 0;
		switch (type)
		{
		case T_InfoTextType.生命值:
			num = n + Technology.GetTechAddtion(n, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.特种兵生命值);
			num += VipConst.GetVipAddtion((float)n, HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种生命值);
			break;
		case T_InfoTextType.攻击值:
			num = n + Technology.GetTechAddtion(n, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.特种兵攻击);
			num += VipConst.GetVipAddtion((float)n, HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种攻击力);
			break;
		case T_InfoTextType.防御值:
			num = n + Technology.GetTechAddtion(n, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.特种兵防御力);
			break;
		}
		return num;
	}

	public string GetTextTips(T_InfoTextType tipsType, int n)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string value = string.Empty;
		string value2 = string.Empty;
		switch (tipsType)
		{
		case T_InfoTextType.生命值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("生命值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.GetAllData(tipsType, n));
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.特种兵生命值);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(n);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种生命值);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.攻击值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("攻击值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.GetAllData(tipsType, n));
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.特种兵攻击);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(n);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.兵种攻击力);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.防御值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("防御值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.GetAllData(tipsType, n));
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.特种兵防御力);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(n);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		}
		return stringBuilder.ToString();
	}
}
