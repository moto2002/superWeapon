using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowSkillInfo : MonoBehaviour
{
	public static ShowSkillInfo ins;

	public UILabel title;

	public UILabel des;

	public int showId;

	public GameObject tip;

	public UILabel tipLabel;

	public UISprite icon;

	public GameObject[] btns = new GameObject[3];

	public static bool isCanUpdate = true;

	public static bool isBuilding = true;

	public static bool isEnougUpdate = true;

	public UIGrid itemGrid;

	public SkillCreateInfo curSelSKillInfo;

	public void OnDestroy()
	{
		ShowSkillInfo.ins = null;
	}

	public void Awake()
	{
		ShowSkillInfo.ins = this;
	}

	public void ShowMain(SkillCreateInfo info)
	{
		ShowSkillInfo.isCanUpdate = true;
		this.curSelSKillInfo = info;
		int id = UnitConst.GetInstance().skillUpdateConst.Values.SingleOrDefault((SkillUpdate a) => a.itemId == info.itemId && a.level == HeroInfo.GetInstance().SkillInfo[info.itemId]).id;
		info.name_Client.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillUpdateConst[id].name, "skill") + "lv." + HeroInfo.GetInstance().SkillInfo[info.itemId];
		this.title.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillUpdateConst[id].name, "skill") + "lv." + HeroInfo.GetInstance().SkillInfo[info.itemId];
		this.des.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().skillUpdateConst[id].des, "skill");
		AtlasManage.SetBigSkillAtlas(this.icon, info.icon.spriteName);
		this.showId = id;
		this.tipLabel.text = LanguageManage.GetTextByKey("占用卡槽", "skill") + ":" + UnitConst.GetInstance().skillUpdateConst[this.showId].skillVoloum;
		if (!this.tip.GetComponent<SkillTipshow>())
		{
			this.tip.AddComponent<SkillTipshow>();
		}
		SkillTipshow component = this.tip.GetComponent<SkillTipshow>();
		component.Index = this.showId;
		component.type = 2;
		component.JianTouPostion = 2;
		int num = 0;
		foreach (KeyValuePair<int, int> item in UnitConst.GetInstance().skillUpdateConst[id].coistItems)
		{
			this.btns[num].gameObject.SetActive(true);
			this.itemGrid.Reposition();
			ShowNeedSKillCard component2 = this.btns[num].GetComponent<ShowNeedSKillCard>();
			int num2 = HeroInfo.GetInstance().skillCarteList.Count((SkillCarteData a) => a.itemID == item.Key && a.index == 0);
			component2.count.text = num2 + "/" + item.Value;
			component2.count.color = ((num2 >= item.Value) ? new Color(0.196078435f, 0.972549f, 0.117647059f) : new Color(1f, 0.1882353f, 0.101960786f));
			AtlasManage.SetSkillSpritName(component2.icon, UnitConst.GetInstance().skillList.Values.First((Skill a) => a.id == item.Key).icon);
			AtlasManage.SetQuilitySpriteName(component2.quality, UnitConst.GetInstance().skillList.Values.First((Skill a) => a.id == item.Key).skillQuality);
			component2.showForward.fillAmount = (float)num2 * 1f / (float)item.Value * 1f;
			num++;
			if (HeroInfo.GetInstance().PlayerBuildingLevel[92] < UnitConst.GetInstance().skillUpdateConst[id].commandLevel)
			{
				NewSkillUpdateManager.ins.buildingLv.gameObject.SetActive(true);
				NewSkillUpdateManager.ins.buildingLv.GetComponent<UILabel>().text = LanguageManage.GetTextByKey("需要战术研究院达到", "skill") + "LV." + UnitConst.GetInstance().skillUpdateConst[id].commandLevel;
				NewSkillUpdateManager.ins.btnUpdate.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.needSkill.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.kuang.gameObject.SetActive(false);
				ShowSkillInfo.isBuilding = false;
			}
			else
			{
				ShowSkillInfo.isBuilding = true;
			}
			if (num2 < item.Value)
			{
				NewSkillUpdateManager.ins.buildingLv.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.btnUpdate.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.needSkill.gameObject.SetActive(true);
				NewSkillUpdateManager.ins.kuang.gameObject.SetActive(false);
				ShowSkillInfo.isCanUpdate = false;
			}
			if (ShowSkillInfo.isCanUpdate)
			{
				ShowSkillInfo.isCanUpdate = true;
			}
			if (HeroInfo.GetInstance().SkillInfo[UnitConst.GetInstance().skillUpdateConst[id].itemId] < UnitConst.GetInstance().MaxSkillUpSet(info.itemId))
			{
				NewSkillUpdateManager.ins.bestLevel.gameObject.SetActive(false);
			}
			else
			{
				NewSkillUpdateManager.ins.buildingLv.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.btnUpdate.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.needSkill.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.bestLevel.gameObject.SetActive(true);
			}
			if (ShowSkillInfo.isCanUpdate && ShowSkillInfo.isBuilding && ShowSkillInfo.isEnougUpdate)
			{
				NewSkillUpdateManager.ins.buildingLv.gameObject.SetActive(false);
				NewSkillUpdateManager.ins.btnUpdate.gameObject.SetActive(true);
				NewSkillUpdateManager.ins.needSkill.gameObject.SetActive(false);
			}
			else
			{
				NewSkillUpdateManager.ins.kuang.gameObject.SetActive(false);
			}
		}
	}
}
