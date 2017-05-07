using System;
using UnityEngine;

public class TitleIsland : IMonoBehaviour
{
	public UILabel lvLa;

	public UILabel nameLa;

	public UISprite iconUIs;

	public GameObject replace;

	public UITimer actTime;

	public UISprite actSp;

	public T_Island island;

	public GameObject timeTittle;

	public UISprite timeUISprite;

	public UILabel timeUILabel;

	public DateTime beginTime;

	public DateTime endTime;

	public GameObject IslandIcon_ga;

	public GameObject EliteBattleIcon_ga;

	public UISprite EliteBattleIcon_BJ;

	public UILabel EliteBattleIcon_Title;

	public UISprite[] EliteBattleIcon_Star;

	private bool startTimeCD;

	public void ResetInfo()
	{
		if (this.island.OwnerType == OwnerType.user)
		{
			this.nameLa.color = new Color(0.0627451f, 0.8509804f, 0.996078432f);
			this.lvLa.color = new Color(0.0627451f, 0.8509804f, 0.996078432f);
		}
		else
		{
			this.nameLa.color = Color.red;
			this.lvLa.color = Color.red;
		}
		switch (this.island.iconType)
		{
		case IconType.myBase:
			this.nameLa.text = LanguageManage.GetTextByKey("您的主基地", "NPC");
			this.lvLa.text = string.Format("LV.{0}", HeroInfo.GetInstance().playerlevel);
			this.iconUIs.spriteName = "您在这里";
			this.replace.SetActive(false);
			this.actSp.gameObject.SetActive(false);
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.mySteel:
			this.OnResTipsSet(string.Format(LanguageManage.GetTextByKey("每小时生产钢铁", "ResIsland") + "{0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[int.Parse(this.island.ownerLv)][ResType.钢铁].speendValue));
			break;
		case IconType.myOil:
			this.OnResTipsSet(string.Format(LanguageManage.GetTextByKey("每小时生产石油", "ResIsland") + "{0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[int.Parse(this.island.ownerLv)][ResType.石油].speendValue));
			break;
		case IconType.myRareEarth:
			this.OnResTipsSet(string.Format(LanguageManage.GetTextByKey("每小时生产稀矿", "ResIsland") + "{0}", HeroInfo.GetInstance().islandResData.ResIslandOutputConst[int.Parse(this.island.ownerLv)][ResType.稀矿].speendValue));
			break;
		case IconType.enemyBase:
			this.nameLa.text = this.island.ownerName + LanguageManage.GetTextByKey("(玩家)", "NPC");
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "敌人的";
			this.iconUIs.SetDimensions(106, 74);
			this.actSp.gameObject.SetActive(false);
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.enemySteel:
		case IconType.npcSteel:
			this.nameLa.text = this.island.ownerName + LanguageManage.GetTextByKey("的钢铁岛", "others");
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "敌方资源岛";
			this.actSp.spriteName = "新钢铁";
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.enemyOil:
		case IconType.npcOil:
			this.nameLa.text = this.island.ownerName + LanguageManage.GetTextByKey("的石油岛", "others");
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "敌方资源岛";
			this.actSp.spriteName = "石油icon";
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.enemyRareEarth:
		case IconType.npcRareEarth:
			this.nameLa.text = this.island.ownerName + LanguageManage.GetTextByKey("的稀矿岛", "others");
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "敌方资源岛";
			this.actSp.spriteName = "稀矿icon";
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.npcBase:
			this.nameLa.text = LanguageManage.GetTextByKey(this.island.ownerName, "NPC");
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "系统敌人的";
			this.iconUIs.SetDimensions(56, 74);
			this.actSp.gameObject.SetActive(false);
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.dailyAct:
		{
			this.nameLa.text = this.island.ownerName;
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "tubiao3";
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.timeFormater = new UITimer.TimeFormater(ActivityManager.GetIns().TimeFormat);
			float time = (float)(TimeTools.ConvertLongDateTime(ActivityManager.GetIns().curActData.dailyActivityEndTime) - TimeTools.GetNowTimeSyncServerToDateTime()).TotalSeconds;
			this.actTime.StartCountDown(time);
			break;
		}
		case IconType.weekAct:
		{
			this.nameLa.text = LanguageManage.GetTextByKey("关卡阶段", "NPC") + ":" + ((ActivityManager.GetIns().GetWeekStage() != -1) ? ActivityManager.GetIns().GetWeekStage().ToString() : LanguageManage.GetTextByKey("已通关", "NPC"));
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "tubiao3";
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.timeFormater = new UITimer.TimeFormater(ActivityManager.GetIns().TimeFormat);
			float time2 = (float)(TimeTools.ConvertLongDateTime(ActivityManager.GetIns().curActData.weekActivityEndTime) - TimeTools.GetNowTimeSyncServerToDateTime()).TotalSeconds;
			this.actTime.StartCountDown(time2);
			break;
		}
		case IconType.myRareZiyuan:
			this.nameLa.text = LanguageManage.GetTextByKey("您的资源岛", "NPC");
			this.lvLa.text = string.Empty;
			this.iconUIs.spriteName = "我方资源岛";
			this.replace.SetActive(false);
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.npcJiqir:
			this.nameLa.text = LanguageManage.GetTextByKey(this.island.ownerName, "NPC");
			this.lvLa.text = string.Format("LV.{0}", this.island.ownerLv);
			this.iconUIs.spriteName = "系统敌人的";
			this.iconUIs.SetDimensions(56, 74);
			this.actSp.gameObject.SetActive(false);
			if (!this.island.replace)
			{
				this.replace.SetActive(false);
			}
			this.actTime.gameObject.SetActive(false);
			break;
		case IconType.battle:
			this.lvLa.text = string.Empty;
			this.startTimeCD = false;
			if (this.island.battleItem.battleBox > 0)
			{
				this.timeTittle.SetActive(true);
				if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), TimeTools.ConvertLongDateTime(this.island.battleItem.EndBattleBoxTime)) > 0.0)
				{
					this.beginTime = TimeTools.GetNowTimeSyncServerToDateTime();
					this.endTime = TimeTools.ConvertLongDateTime(this.island.battleItem.EndBattleBoxTime);
					this.startTimeCD = true;
				}
				else
				{
					this.timeUISprite.fillAmount = 1f;
					this.timeUILabel.text = LanguageManage.GetTextByKey("箱子可领取", "others");
				}
				this.nameLa.text = string.Empty;
			}
			else
			{
				this.nameLa.text = LanguageManage.GetTextByKey(this.island.battleItem.name, "Battle");
				this.timeTittle.SetActive(false);
			}
			this.replace.SetActive(false);
			this.actSp.gameObject.SetActive(false);
			this.actTime.gameObject.SetActive(false);
			break;
		}
		if (this.island.EliteBattleStar != 0)
		{
			this.IslandIcon_ga.gameObject.SetActive(false);
			this.EliteBattleIcon_ga.gameObject.SetActive(true);
			for (int i = 4; i >= this.island.EliteBattleStar; i--)
			{
				this.EliteBattleIcon_Star[i].gameObject.SetActive(false);
			}
		}
	}

	public void OnResTipsSet(string info)
	{
		this.nameLa.text = info;
		this.nameLa.fontSize = 18;
		this.nameLa.gameObject.transform.localPosition = new Vector3(2f, 14.9f, 0f);
		this.iconUIs.gameObject.SetActive(false);
		this.actTime.gameObject.SetActive(false);
		this.iconUIs.gameObject.transform.localPosition = new Vector3(102.08f, 48f, 0f);
		this.iconUIs.spriteName = "攻打资源岛胜利生产提示框";
		this.replace.SetActive(false);
		this.iconUIs.SetDimensions(292, 100);
		this.actSp.gameObject.SetActive(false);
		this.lvLa.gameObject.SetActive(false);
	}

	public void LateUpdate()
	{
		if (this.island)
		{
			Vector3 position = this.island.tr.position;
			Vector3 position2 = WMap_DragManager.inst.camer.WorldToScreenPoint(position);
			Vector3 position3 = TipsManager.inst.uiCamera.ScreenToWorldPoint(position2);
			if (position3.z < 0f)
			{
			}
			this.tr.localScale = new Vector3(-0.005f * position3.z + 0.955f, -0.005f * position3.z + 0.955f, 0f);
			position3 = new Vector3(position3.x, position3.y + 0.2f);
			this.tr.position = position3;
			this.tr.localPosition = new Vector3(this.tr.localPosition.x, this.tr.localPosition.y, 0f);
			if (this.timeTittle.activeSelf && this.startTimeCD)
			{
				if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime) > 0.0)
				{
					if (this.timeUISprite)
					{
						this.timeUISprite.fillAmount = (float)(TimeTools.DateDiffToDouble(this.beginTime, TimeTools.GetNowTimeSyncServerToDateTime()) / TimeTools.DateDiffToDouble(this.beginTime, this.endTime));
					}
					this.timeUILabel.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime) + LanguageManage.GetTextByKey("后可领取宝箱", "others");
				}
				else
				{
					this.startTimeCD = false;
					this.timeUISprite.fillAmount = 1f;
					this.timeUILabel.text = LanguageManage.GetTextByKey("箱子可领取", "others");
				}
			}
		}
		else
		{
			UnityEngine.Object.Destroy(this.ga);
		}
	}
}
