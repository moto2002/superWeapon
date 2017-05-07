using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SepcialSoliderPanel : FuncUIPanel
{
	public static SepcialSoliderPanel ins;

	public UICenterOnChild onCenterOnChild;

	public UIGrid showSpecilaSoliderGrid;

	public ShowDetailInfo ifnos;

	public UILabel skillCount;

	public UILabel skillTime;

	public Transform SkillBg;

	public Transform ExpBg;

	public Transform left_Border;

	public Transform rightBorder;

	public GameObject soliderPrefab;

	public GameObject updateStar;

	public GameObject bottomSolider;

	public GameObject timeSprit;

	public UIScrollView scroll;

	public GameObject skillred;

	public GameObject expred;

	public UILabel fuhuoMoney;

	public UITable table;

	public GameObject moveLeft;

	public GameObject moveRight;

	public GameObject funcArmy;

	public static bool isItemEnough = false;

	[HideInInspector]
	public int SelectSoliderIndex;

	[HideInInspector]
	public int fightSoliderIndex;

	[HideInInspector]
	public SmallSoliderModelInfo smallInfo;

	[HideInInspector]
	public bool isDragInfo;

	public static ShowSkillPointClass skillShowTime = new ShowSkillPointClass();

	public ArmyNewBiGuid armyGuid;

	public static bool isDisplayComandoGuid = false;

	private static string des;

	private static int num;

	private static Vector3 pianyi;

	private static Vector3 scale;

	public int preID;

	public int nextId;

	public static Dictionary<int, ShowSpecialInfo> specialSolider = new Dictionary<int, ShowSpecialInfo>();

	private int top = 1;

	private int bottom = 1;

	private GameObject _ga;

	public override void Awake()
	{
		this.onCenterOnChild.CenterOnBack = new Action<GameObject>(this.OnCenterBack);
		this.skillred.SetActive(false);
		this.expred.SetActive(false);
		SepcialSoliderPanel.ins = this;
	}

	private void OnCenterBack(GameObject ga)
	{
		this.SelectSoliderIndex = ga.GetComponent<ShowSpecialInfo>().itemID;
		this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
		this.RefresSkillRed();
		this.RefreshExpRed();
	}

	public override void OnEnable()
	{
		this.Init();
		this.table.Reposition();
		this.scroll.ResetPosition();
		this.RefreshDeadInfo();
		this.showSpecialSoliderInfo();
		this.showSpecilaSoliderGrid.Reposition();
		this.showSpecilaSoliderGrid.GetComponentInParent<UIScrollView>().ResetPosition();
		this.RefreshSkillCount();
		if (HeroInfo.GetInstance().Commando_Fight != null)
		{
			this.smallInfo.ShowGotoFightSolider(HeroInfo.GetInstance().Commando_Fight.index);
			LogManage.LogError("she ding id:" + HeroInfo.GetInstance().Commando_Fight.index);
		}
		else
		{
			LogManage.LogError("当前没有配兵");
		}
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			SepcialSoliderPanel.ShowNewBiGuid();
		}
		this.RefresSkillRed();
		this.RefreshExpRed();
		base.OnEnable();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		this.RefreshSkillCount();
		if (opcodeCMD == 10041)
		{
			this.RefresSkillRed();
			this.RefreshExpRed();
		}
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	public void RefresSkillRed()
	{
		if (this.ifnos.isHaveSolier)
		{
			if (HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].level >= this.ifnos.NeedRoleLevel)
			{
				if (HeroInfo.GetInstance().playerRes.skillPoint > int.Parse(this.ifnos.spcialSkill[0].skillpoint.text) && this.ifnos.CoinEnough)
				{
					this.skillred.gameObject.SetActive(true);
				}
				else
				{
					this.skillred.gameObject.SetActive(false);
				}
			}
			else
			{
				this.skillred.SetActive(false);
			}
		}
		else
		{
			this.skillred.gameObject.SetActive(false);
			this.expred.SetActive(false);
		}
	}

	public void RefreshExpRed()
	{
		if (this.ifnos.isHaveSolier)
		{
			if (HeroInfo.GetInstance().playerlevel > HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].level)
			{
				if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(4))
				{
					if (HeroInfo.GetInstance().PlayerItemInfo[4] > 0)
					{
						this.expred.SetActive(true);
					}
					else
					{
						this.expred.SetActive(false);
					}
				}
				if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(5))
				{
					if (HeroInfo.GetInstance().PlayerItemInfo[5] > 0)
					{
						this.expred.SetActive(true);
					}
					else
					{
						this.expred.SetActive(false);
					}
				}
				if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(6))
				{
					if (HeroInfo.GetInstance().PlayerItemInfo[6] > 0)
					{
						this.expred.SetActive(true);
					}
					else
					{
						this.expred.SetActive(false);
					}
				}
			}
			else
			{
				this.expred.SetActive(false);
			}
		}
		else
		{
			this.skillred.gameObject.SetActive(false);
			this.expred.SetActive(false);
		}
	}

	public void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_Close, new EventManager.VoidDelegate(this.ClosePanel));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_OpenExp, new EventManager.VoidDelegate(this.OpenExpBg));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_OpenSkill, new EventManager.VoidDelegate(this.OpenSKillBg));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_updateStar, new EventManager.VoidDelegate(this.UpdateStar));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_otherClick, new EventManager.VoidDelegate(this.OtherClick));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_updateSkill, new EventManager.VoidDelegate(this.UpdateSkill));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_UserExpItem, new EventManager.VoidDelegate(this.UseItemLevelUp));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_UseReliveRMB, new EventManager.VoidDelegate(this.UserReliveRMB));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_BuySoliderRMB, new EventManager.VoidDelegate(this.BuySpecialSolider));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_MoveLeft, new EventManager.VoidDelegate(this.MoveLeftSolider));
		EventManager.Instance.AddEvent(EventManager.EventType.SepcialSoliderPanel_MoveRight, new EventManager.VoidDelegate(this.MoveRightSolider));
	}

	public void MoveLeftSolider(GameObject ga)
	{
		this.onCenterOnChild.CenterOn(SepcialSoliderPanel.specialSolider[SepcialSoliderPanel.specialSolider[this.SelectSoliderIndex].preItemid].transform);
		this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
	}

	public void MoveRightSolider(GameObject ga)
	{
		this.onCenterOnChild.CenterOn(SepcialSoliderPanel.specialSolider[SepcialSoliderPanel.specialSolider[this.SelectSoliderIndex].nextItemID].transform);
		this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
	}

	public static void ShowNewBiGuidText(string _des, int _num, Vector3 _pianyi, Vector3 _Scale)
	{
		SepcialSoliderPanel.des = _des;
		SepcialSoliderPanel.num = _num;
		SepcialSoliderPanel.pianyi = _pianyi;
		SepcialSoliderPanel.scale = _Scale;
	}

	public static void ShowNewBiGuid()
	{
		if (!SepcialSoliderPanel.isDisplayComandoGuid)
		{
			if (SepcialSoliderPanel.ins.armyGuid)
			{
				SepcialSoliderPanel.ins.armyGuid.gameObject.SetActive(false);
			}
		}
		else if (SepcialSoliderPanel.ins)
		{
			SepcialSoliderPanel.ins.armyGuid.DisplayNewBiGuid(SepcialSoliderPanel.des, SepcialSoliderPanel.num, SepcialSoliderPanel.pianyi, SepcialSoliderPanel.scale);
		}
	}

	public void showSpecialSoliderInfo()
	{
		this.showSpecilaSoliderGrid.ClearChild();
		this.preID = 0;
		this.nextId = 0;
		this.isDragInfo = false;
		foreach (KeyValuePair<int, Soldier> current in UnitConst.GetInstance().soldierList)
		{
			GameObject gameObject = NGUITools.AddChild(this.showSpecilaSoliderGrid.gameObject, this.soliderPrefab);
			ShowSpecialInfo component = gameObject.GetComponent<ShowSpecialInfo>();
			component.setSpecialSoliderInfo(current.Key, HeroInfo.GetInstance().PlayerCommandoes.ContainsKey(current.Value.id));
			SepcialSoliderPanel.specialSolider[current.Key] = component;
			if (this.nextId > 0)
			{
				SepcialSoliderPanel.specialSolider[this.preID].nextItemID = current.Key;
				component.preItemid = SepcialSoliderPanel.specialSolider[this.preID].itemID;
			}
			this.preID = current.Key;
			this.nextId++;
		}
		this.smallInfo = this.bottomSolider.GetComponent<SmallSoliderModelInfo>();
		this.isDragInfo = true;
		this.showSpecilaSoliderGrid.Reposition();
		this.showSpecilaSoliderGrid.GetComponentInParent<UIScrollView>().ResetPosition();
	}

	private void ClosePanel(GameObject ga)
	{
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (this.top == 2)
		{
			this.SkillBg.transform.DOLocalMoveX(-333f, 0.1f, false);
		}
		else if (this.bottom == 2)
		{
			this.ExpBg.transform.DOLocalMoveX(-333f, 0.1f, false);
		}
		FuncUIManager.inst.HideFuncUI("SpecialSoliderUpdatePanel");
	}

	private void OpenSKillBg(GameObject ga)
	{
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (this.bottom == 1 && this.top == 1)
		{
			this.SkillBg.transform.DOLocalMoveX(290f, 0.1f, false);
			this.top = 2;
			return;
		}
		if (this.top == 2 && this.bottom != 2)
		{
			this.SkillBg.transform.DOLocalMoveX(-333f, 0.1f, false);
			this.top = 1;
		}
		if (this.top == 1 && this.bottom == 2)
		{
			this.ExpBg.transform.DOLocalMoveX(-333f, 0.1f, false).OnComplete(delegate
			{
				this.SkillBg.transform.DOLocalMoveX(290f, 0.1f, false);
			});
			this.bottom = 1;
			this.top = 2;
		}
	}

	private void OpenExpBg(GameObject ga)
	{
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (this.bottom == 1 && this.top == 1)
		{
			this.ExpBg.transform.DOLocalMoveX(290f, 0.1f, false).OnComplete(delegate
			{
				this.table.Reposition();
				this.scroll.ResetPosition();
			});
			this.bottom = 2;
			return;
		}
		if (this.bottom == 2 && this.top != 2)
		{
			this.ExpBg.transform.DOLocalMoveX(-333f, 0.1f, false).OnComplete(delegate
			{
				this.table.Reposition();
				this.scroll.ResetPosition();
			});
			this.bottom = 1;
		}
		if (this.bottom == 1 && this.top == 2)
		{
			this.SkillBg.transform.DOLocalMoveX(-333f, 0.1f, false).OnComplete(delegate
			{
				this.ExpBg.transform.DOLocalMoveX(299f, 0.1f, false).OnComplete(delegate
				{
					this.table.Reposition();
					this.scroll.ResetPosition();
				});
			});
			this.bottom = 2;
			this.top = 1;
		}
	}

	private void CalcMoneyCallBack(bool isBuy, int money)
	{
		GameObject ga = this._ga;
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				HUDTextTool.inst.ShowBuyMoney();
				return;
			}
			if (HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].star < UnitConst.GetInstance().MaxSpecialSoliderStar(this.SelectSoliderIndex))
			{
				if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(int.Parse(ga.name)))
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还没有该道具", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				if (HeroInfo.GetInstance().PlayerItemInfo[int.Parse(ga.name)] > 0)
				{
					SpecialSoliderHandler.CS_SoldierUpStar(HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, delegate
					{
						this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
						SepcialSoliderPanel.specialSolider[this.SelectSoliderIndex].UpStar();
					});
				}
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已到最高星级", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			}
		}
	}

	private void CalcMoneyCallBack1(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				HUDTextTool.inst.ShowBuyMoney();
				return;
			}
			SpecialSoliderHandler.CS_SkillUpSet(HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, delegate
			{
				this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
				this.RefreshExpRed();
				this.RefresSkillRed();
			});
		}
	}

	private void UpdateStar(GameObject ga)
	{
		this._ga = ga;
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (!this.ifnos.isHaveSolier)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还未拥有该兵种", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (!SepcialSoliderPanel.isItemEnough)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().playerRes.resCoin < int.Parse(this.ifnos.upStarCoin.text))
		{
			CalcMoneyHandler.CSCalcMoney(14, 0, 0, HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, 0, 0, 1, new Action<bool, int>(this.CalcMoneyCallBack));
			return;
		}
		if (HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].star < UnitConst.GetInstance().MaxSpecialSoliderStar(this.SelectSoliderIndex))
		{
			if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(int.Parse(ga.name)))
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还没有该道具", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			if (HeroInfo.GetInstance().PlayerItemInfo[int.Parse(ga.name)] > 0)
			{
				SpecialSoliderHandler.CS_SoldierUpStar(HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, delegate
				{
					this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
					SepcialSoliderPanel.specialSolider[this.SelectSoliderIndex].UpStar();
				});
			}
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已到最高星级", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	private void OtherClick(GameObject ga)
	{
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (this.top == 2)
		{
			this.SkillBg.transform.DOLocalMoveX(-333f, 0.1f, false);
		}
		else
		{
			if (this.bottom != 2)
			{
				return;
			}
			this.ExpBg.transform.DOLocalMoveX(-333f, 0.1f, false);
		}
	}

	private void UpdateSkill(GameObject ga)
	{
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (!this.ifnos.isHaveSolier)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还未拥有该兵种", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].skillLevel >= UnitConst.GetInstance().MaxSkillUpSet(this.SelectSoliderIndex))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已到最高等级", "soldier") + this.ifnos.NeedRoleLevel, HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().PlayerCommandoes[int.Parse(ga.name)].level < this.ifnos.NeedRoleLevel)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("需要特种兵达到", "soldier") + "LV." + this.ifnos.NeedRoleLevel, HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().playerRes.resCoin < int.Parse(ga.GetComponentInParent<SpecialSoliderSkill>().coinCount.text))
		{
			CalcMoneyHandler.CSCalcMoney(15, 0, 0, HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, 0, 0, 1, new Action<bool, int>(this.CalcMoneyCallBack1));
			return;
		}
		SpecialSoliderHandler.CS_SkillUpSet(HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, delegate
		{
			this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
			this.RefreshExpRed();
			this.RefresSkillRed();
		});
	}

	public void RefreshSkillCount()
	{
		this.skillCount.text = LanguageManage.GetTextByKey("技能点", "soldier") + ":" + HeroInfo.GetInstance().playerRes.skillPoint;
	}

	public void RefreshRelieveTime()
	{
	}

	public void ReliveTimeEnd(int _id)
	{
		SpecialSoliderHandler.CS_SoldierRelive(HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, delegate
		{
			this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
		});
		this.RefreshDeadInfo();
	}

	private void Update()
	{
		TimeSpan timeSpan = SepcialSoliderPanel.skillShowTime.skillRecoverTime - TimeTools.GetNowTimeSyncServerToDateTime();
		if (HeroInfo.GetInstance().playerRes.skillPoint >= int.Parse(UnitConst.GetInstance().DesighConfigDic[66].value))
		{
			this.timeSprit.SetActive(false);
		}
		else
		{
			this.timeSprit.SetActive(true);
			if (timeSpan.TotalSeconds > 0.0)
			{
				if (timeSpan.Hours > 0)
				{
					this.skillTime.text = string.Format("{0}时{1}分{2}秒", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				}
				else if (timeSpan.Minutes > 0)
				{
					this.skillTime.text = string.Format("{0}分{1}秒", timeSpan.Minutes, timeSpan.Seconds);
				}
				else
				{
					this.skillTime.text = string.Format("{0}秒", timeSpan.Seconds);
				}
			}
			else
			{
				this.timeSprit.SetActive(false);
			}
		}
	}

	public void UseItemLevelUp(GameObject ga)
	{
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (!this.ifnos.isHaveSolier)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还未拥有该兵种", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].level >= UnitConst.GetInstance().MaxSoecialSoliderLevel(this.SelectSoliderIndex))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("已到最高等级", "soldier") + this.ifnos.NeedRoleLevel, HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().playerlevel <= HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].level)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您的等级不足", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (!HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(int.Parse(ga.name)))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("您还没有该道具", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().PlayerItemInfo[int.Parse(ga.name)] > 0)
		{
			SpecialSoliderHandler.CS_UseItemToAddExp(HeroInfo.GetInstance().PlayerCommandoes[this.SelectSoliderIndex].id, int.Parse(ga.name), 1, delegate
			{
				this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
				this.RefreshExpRed();
				this.RefresSkillRed();
			});
			return;
		}
		HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("道具不足", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num5);
	}

	public void BuySpecialSolider(GameObject ga)
	{
		if (SepcialSoliderPanel.isDisplayComandoGuid)
		{
			return;
		}
		if (HeroInfo.GetInstance().playerRes.RMBCoin > UnitConst.GetInstance().soldierList[this.SelectSoliderIndex].unLock)
		{
			SpecialSoliderHandler.CS_SoldierAdd(this.SelectSoliderIndex, 2, delegate
			{
				this.ifnos.SpecialSoliderInfo(this.SelectSoliderIndex);
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("恭喜您获得了新兵种", "soldier") + "!", HUDTextTool.TextUITypeEnum.Num1);
				SepcialSoliderPanel.specialSolider[this.SelectSoliderIndex].BuySoliderByMoney();
			});
			return;
		}
		HUDTextTool.inst.ShowBuyMoney();
	}

	public void RefreshDeadInfo()
	{
	}

	public void UserReliveRMB(GameObject ga)
	{
	}

	public void ShowReliveCount()
	{
		if (HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(108))
		{
			this.fuhuoMoney.text = HeroInfo.GetInstance().PlayerItemInfo[108].ToString();
			if (HeroInfo.GetInstance().PlayerItemInfo[108] > 0)
			{
				this.fuhuoMoney.color = Color.white;
			}
			else
			{
				this.fuhuoMoney.color = Color.red;
			}
		}
		else
		{
			this.fuhuoMoney.text = "0";
			this.fuhuoMoney.color = Color.red;
		}
	}
}
