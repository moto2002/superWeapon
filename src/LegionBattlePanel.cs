using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LegionBattlePanel : FuncUIPanel
{
	public static LegionBattlePanel _inst;

	public UITexture AllBattlePanelSprite;

	public UISprite ChooseBattlePanelSprite;

	public UILabel Panel_NameLabel;

	public UILabel Label_Left;

	public UILabel Label_Right;

	public UILabel ProgressNumLabel;

	public UISprite ProgressSprite;

	public GameObject LeginBattleButton;

	public UITexture BJ;

	public List<LegionBattleButton> LegionBattleButtonList = new List<LegionBattleButton>();

	public UILabel Title_Label;

	public UISprite Title_Des_BG;

	public UILabel Title_Des_Vit;

	public UILabel Title_Des_NowBattle;

	public UILabel Title_Des_Prograss;

	public UILabel Title_Des_RefreshTime;

	public GameObject BattlePassedPanel;

	public UILabel BPPBtnA_Label;

	public UILabel BPPBtnB_Label;

	public BattleField GotoFight_BattleField;

	public int IsCompleteNum;

	private bool ChooseLegionBattle_0;

	private float Title_x;

	public GameObject NormalBattleBtn;

	public GameObject LegionBattleBtn;

	public GameObject NBattlePanel;

	private new void Awake()
	{
		LegionBattlePanel._inst = this;
		this.LegionBattleBtn.gameObject.SetActive(false);
	}

	public void RefreshTitleDes()
	{
		this.Title_Des_Vit.text = string.Format("我的军团体力剩余：[FFFF46]{0}[-]", HeroInfo.GetInstance().MyLegionBattleData.Vit);
		this.Title_Des_NowBattle.text = string.Format("当前进行的军团副本\n第[FFFF46]{0}[-]关", UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].number);
		this.Title_Des_Prograss.text = string.Format(" 当前进行的军团副本进度\n[FFFF46]{0}[-]/20", HeroInfo.GetInstance().MyLegionBattleData.NowBattleProgress);
		DateTime dateTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().MyLegionBattleData.BattleRefreshTime);
		this.Title_Des_RefreshTime.text = string.Format("军团副本下次刷新时间\n[FFFF46]{0}/{1}/{2}[-]", dateTime.Year, dateTime.Month, dateTime.Day);
	}

	private void Start()
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = NGUITools.AddChild(this.BJ.gameObject, this.LeginBattleButton.gameObject);
			gameObject.transform.localPosition = new Vector3((float)(-400 + 200 * i), -45f, 0f);
			this.LegionBattleButtonList.Add(gameObject.GetComponent<LegionBattleButton>());
		}
		EventManager.Instance.AddEvent(EventManager.EventType.LegionBattleButton, new EventManager.VoidDelegate(this.LegionBattleButton_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.LegionBattleEnter, new EventManager.VoidDelegate(this.LegionBattleEnter_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.LegionBattleBack, new EventManager.VoidDelegate(this.LegionBattleBack_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.Geo_Earth_Back, new EventManager.VoidDelegate(this.Geo_Earth_Back_CallBack));
		EventManager.Instance.AddEvent(EventManager.EventType.Change_NormalBattleBtn, new EventManager.VoidDelegate(this.Change_NormalBattleBtn));
		EventManager.Instance.AddEvent(EventManager.EventType.Change_LegionBattleBtn, new EventManager.VoidDelegate(this.Change_LegionBattleBtn));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePassed_BtnA, new EventManager.VoidDelegate(this.BattlePassed_Att));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePassed_BtnB, new EventManager.VoidDelegate(this.BattlePassed_SaoDang));
		this.AllBattlePanelSprite.gameObject.SetActive(false);
	}

	private void Change_NormalBattleBtn(GameObject ga)
	{
		LegionMapManager._inst.ChangeBattleType(LegionMapManager.BattleType.普通副本, false);
	}

	private void Change_LegionBattleBtn(GameObject ga)
	{
		if (HeroInfo.GetInstance().playerGroupId != 0L)
		{
			ArmyGroupHandler.CG_CSLegionData(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
			{
				if (!isError)
				{
					LegionMapManager._inst.ChangeBattleType(LegionMapManager.BattleType.军团副本, false);
					this.RefreshTitleDes();
				}
			});
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("尚未加入军团，军团副本未开放", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	public void BattlePassed_Att(GameObject ga)
	{
		this.BattlePassedPanel.gameObject.SetActive(false);
		LegionBattlePanel._inst.Init();
	}

	public void OnSweepOne()
	{
		this.BattlePassedPanel.gameObject.SetActive(false);
		CSSweep cSSweep = new CSSweep();
		cSSweep.battleId = SenceInfo.CurBattle.id;
		ClientMgr.GetNet().SendHttp(5016, cSSweep, new DataHandler.OpcodeHandler(this.OpenBattleFieldBoxCallBack), null);
	}

	private void OpenBattleFieldBoxCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
		}
	}

	public void BattlePassed_SaoDang(GameObject ga)
	{
		Battle curBattle = SenceInfo.CurBattle;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		foreach (KeyValuePair<int, BattleField> current in curBattle.allBattleField)
		{
			num5 += current.Value.cost;
			foreach (KeyValuePair<ResType, int> current2 in UnitConst.GetInstance().AllDropList[UnitConst.GetInstance().AllNpc[current.Value.npcId].dropListId].res)
			{
				switch (current2.Key)
				{
				case ResType.金币:
					num += current2.Value;
					break;
				case ResType.石油:
					num2 += current2.Value;
					break;
				case ResType.钢铁:
					num3 += current2.Value;
					break;
				case ResType.稀矿:
					num4 += current2.Value;
					break;
				}
			}
		}
		if (num5 <= HeroInfo.GetInstance().playerRes.junLing && HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(2) && 1 <= HeroInfo.GetInstance().PlayerItemInfo[2])
		{
			if ((num > 0 && HeroInfo.GetInstance().playerRes.resCoin + num > HeroInfo.GetInstance().playerRes.maxCoin) || (num2 > 0 && HeroInfo.GetInstance().playerRes.resOil + num2 > HeroInfo.GetInstance().playerRes.maxOil) || (num3 > 0 && HeroInfo.GetInstance().playerRes.resSteel + num3 > HeroInfo.GetInstance().playerRes.maxSteel) || (num4 > 0 && HeroInfo.GetInstance().playerRes.resRareEarth + num4 > HeroInfo.GetInstance().playerRes.maxRareEarth))
			{
				MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "others"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "others"), LanguageManage.GetTextByKey("确定", "others"), new Action(this.OnSweepOne), LanguageManage.GetTextByKey("取消", "others"), null);
				return;
			}
			this.OnSweepOne();
		}
		else if (num5 > HeroInfo.GetInstance().playerRes.junLing)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军令不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("扫荡令不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	public void Init()
	{
		if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.普通副本)
		{
			this.NBattlePanel.SetActive(true);
			NTollgateManage.inst.gameObject.SetActive(true);
			NTollgateManage.inst.curBattle = SenceInfo.CurBattle;
			NTollgateManage.inst.StartCoroutine(NTollgateManage.inst.ShowTollgateItem());
		}
		else if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.军团副本)
		{
			this.ProgressNumLabel.text = string.Format("{0}/20", HeroInfo.GetInstance().MyLegionBattleData.NowBattleProgress);
			this.ProgressSprite.fillAmount = (float)HeroInfo.GetInstance().MyLegionBattleData.NowBattleProgress / 20f;
			this.AllBattlePanelSprite.gameObject.SetActive(true);
			for (int i = 0; i < this.LegionBattleButtonList.Count; i++)
			{
				this.LegionBattleButtonList[i].IsComplete.gameObject.SetActive(false);
				this.LegionBattleButtonList[i].NameLabel.text = string.Format("第{0}关", i + 1);
				this.LegionBattleButtonList[i].BMP.color = new Color(0.25f, 0.25f, 0.25f, 1f);
			}
			this.ChooseLegionBattle_0 = false;
			this.AllBattlePanelSprite.alpha = 0f;
			this.ChooseBattlePanelSprite.gameObject.SetActive(false);
			if (UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].ID == 0L)
			{
				Debug.Log("未获得军团关卡的实例ID，表示为初始状态");
				int num = 0;
				int key = UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].firstBattleFieldId;
				while (UnitConst.GetInstance().BattleFieldConst[key].nextId != 0)
				{
					Debug.Log("替换id:" + UnitConst.GetInstance().BattleFieldConst[key].nextId);
					int id = UnitConst.GetInstance().BattleFieldConst[key].id;
					key = UnitConst.GetInstance().BattleFieldConst[key].nextId;
					this.LegionBattleButtonList[num].IsComplete.gameObject.SetActive(false);
					if (num == 0)
					{
						this.GotoFight_BattleField = UnitConst.GetInstance().BattleFieldConst[id];
						this.LegionBattleButtonList[num].BMP.color = Color.white;
					}
					num = Mathf.Min(4, num + 1);
				}
			}
			else if (UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].ID != 0L)
			{
				Debug.Log("获得军团关卡的实例ID，表示已被服务器记录");
				if (TimeTools.ConvertLongDateTime(UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].EndBattleBoxTime).Day != TimeTools.GetNowTimeSyncServerToDateTime().Day)
				{
					Debug.Log("与服务器记录非同一天，则本场战役已被重置");
					int num2 = 0;
					int key2 = UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].firstBattleFieldId;
					while (UnitConst.GetInstance().BattleFieldConst[key2].nextId != 0)
					{
						Debug.Log("替换id:" + UnitConst.GetInstance().BattleFieldConst[key2].nextId);
						int id2 = UnitConst.GetInstance().BattleFieldConst[key2].id;
						key2 = UnitConst.GetInstance().BattleFieldConst[key2].nextId;
						this.LegionBattleButtonList[num2].IsComplete.gameObject.SetActive(false);
						if (num2 == 0)
						{
							this.GotoFight_BattleField = UnitConst.GetInstance().BattleFieldConst[id2];
							this.LegionBattleButtonList[num2].BMP.color = Color.white;
						}
						num2 = Mathf.Min(4, num2 + 1);
					}
				}
				else
				{
					Debug.Log("与服务器记录同一天，按照服务器记录设置");
					int num3 = 0;
					int num4 = UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].firstBattleFieldId;
					while (UnitConst.GetInstance().BattleFieldConst[num4].nextId != 0)
					{
						Debug.Log("替换id:" + UnitConst.GetInstance().BattleFieldConst[num4].nextId);
						int id3 = UnitConst.GetInstance().BattleFieldConst[num4].id;
						num4 = UnitConst.GetInstance().BattleFieldConst[num4].nextId;
						this.LegionBattleButtonList[num3].IsComplete.gameObject.SetActive(false);
						if (UnitConst.GetInstance().BattleFieldConst[id3].isCompleteUI)
						{
							this.LegionBattleButtonList[num3].IsComplete.gameObject.SetActive(true);
							if (num4 == 0)
							{
								this.LegionBattleButtonList[num3 + 1].IsComplete.gameObject.SetActive(true);
							}
							else if (!UnitConst.GetInstance().BattleFieldConst[num4].isCompleteUI)
							{
								this.GotoFight_BattleField = UnitConst.GetInstance().BattleFieldConst[num4];
								this.LegionBattleButtonList[num3 + 1].BMP.color = Color.white;
							}
						}
						num3 = Mathf.Min(4, num3 + 1);
					}
					if (UnitConst.GetInstance().BattleFieldConst[num4].nextId == 0 && UnitConst.GetInstance().BattleFieldConst[num4].isCompleteUI)
					{
						this.LegionBattleButtonList[num3].IsComplete.gameObject.SetActive(true);
					}
				}
			}
		}
	}

	private void Update()
	{
		this.LegionBattleBtn.gameObject.SetActive(HeroInfo.GetInstance().playerGroupId != 0L);
		if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.普通副本)
		{
			this.Title_Label.text = "普通副本";
			this.Title_x = Mathf.Max(0f, this.Title_x - 4f * Time.deltaTime);
		}
		else if (LegionMapManager._inst.NowBattleType == LegionMapManager.BattleType.军团副本)
		{
			this.Title_Label.text = "军团副本";
			this.Title_x = Mathf.Min(1f, this.Title_x + 4f * Time.deltaTime);
		}
		this.Title_Des_BG.SetDimensions(250, (int)(272f * this.Title_x));
		this.Title_Des_BG.alpha = (float)((this.Title_x > 0f) ? 1 : 0);
		this.Title_Des_Vit.alpha = (float)((this.Title_x < 1f) ? 0 : 1);
		this.Title_Des_NowBattle.alpha = (float)((this.Title_x < 1f) ? 0 : 1);
		this.Title_Des_Prograss.alpha = (float)((this.Title_x < 1f) ? 0 : 1);
		this.Title_Des_RefreshTime.alpha = (float)((this.Title_x < 1f) ? 0 : 1);
		if (this.ChooseLegionBattle_0)
		{
			this.AllBattlePanelSprite.alpha = Mathf.Max(0f, this.AllBattlePanelSprite.alpha - 2f * Time.deltaTime);
			this.ChooseBattlePanelSprite.alpha = 1f - this.AllBattlePanelSprite.alpha;
		}
		else
		{
			this.AllBattlePanelSprite.alpha = Mathf.Min(1f, this.AllBattlePanelSprite.alpha + 2f * Time.deltaTime);
			this.ChooseBattlePanelSprite.alpha = 1f - this.AllBattlePanelSprite.alpha;
		}
	}

	private void LegionBattleButton_CallBack(GameObject ga)
	{
		if (ga.GetComponent<LegionBattleButton>().IsComplete.gameObject.activeSelf)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("所选军团副本已被攻陷", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (ga.GetComponent<LegionBattleButton>().BMP.color.a <= 0.5f)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("所选军团副本尚未解锁", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (CameraControl.inst)
		{
			CameraControl.inst.gameObject.SetActive(true);
		}
		SenceInfo.CurBattleField = this.GotoFight_BattleField;
		NewbieGuidePanel.isZhanyi = false;
		UIManager.curState = SenceState.Spy;
		Debug.Log("================================================攻打军团副本ID：" + this.GotoFight_BattleField.id);
		SenceHandler.CG_GetMapData(this.GotoFight_BattleField.id, 2, 0, delegate(bool isError)
		{
			if (!isError)
			{
				UIManager.curState = SenceState.Spy;
				SenceInfo.battleResource = SenceInfo.BattleResource.LegionBattleFight;
			}
		});
		if (CameraZhedang.inst)
		{
			CameraZhedang.inst.uiInUseBox.SetActive(false);
		}
		base.transform.parent.gameObject.SetActive(false);
		if (LegionMapManager._inst.gameObject)
		{
			Transform[] componentsInChildren = LegionMapManager._inst.gameObject.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				if (transform && transform.gameObject != LegionMapManager._inst.gameObject)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}
	}

	private void LegionBattleEnter_CallBack(GameObject ga)
	{
	}

	private void LegionBattleBack_CallBack(GameObject ga)
	{
		if (this.ChooseBattlePanelSprite.alpha < 1f)
		{
			return;
		}
		if (!this.ChooseLegionBattle_0)
		{
			return;
		}
		this.ChooseLegionBattle_0 = false;
	}

	private void Geo_Earth_Back_CallBack(GameObject ga)
	{
		if (SenceInfo.battleResource == SenceInfo.BattleResource.NormalBattleFight)
		{
			ga.GetComponent<ButtonClick>().isSendLua = false;
			return;
		}
		if (SenceInfo.battleResource == SenceInfo.BattleResource.LegionBattleFight)
		{
			SenceInfo.battleResource = SenceInfo.BattleResource.None;
		}
		ga.GetComponent<ButtonClick>().isSendLua = true;
		if (this.BattlePassedPanel.gameObject.activeSelf)
		{
			this.BattlePassedPanel.gameObject.SetActive(false);
		}
		else
		{
			if (this.AllBattlePanelSprite.gameObject.activeSelf)
			{
				this.AllBattlePanelSprite.gameObject.SetActive(false);
				this.ChooseBattlePanelSprite.gameObject.SetActive(false);
				return;
			}
			if (LegionMapManager._inst.gameObject)
			{
				Transform[] componentsInChildren = LegionMapManager._inst.gameObject.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					if (transform && transform.gameObject != LegionMapManager._inst.gameObject)
					{
						UnityEngine.Object.Destroy(transform.gameObject);
					}
				}
			}
			FuncUIManager.inst.DestoryFuncUI("LegionBattlePanel");
			FuncUIManager.inst.OpenFuncUI_NoQueue("ResourcePanel", SenceType.Other);
			FuncUIManager.inst.OpenFuncUI("MainUIPanel", SenceType.Island);
			if (CameraControl.inst)
			{
				CameraControl.inst.gameObject.SetActive(true);
			}
		}
	}
}
