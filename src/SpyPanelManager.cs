using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpyPanelManager : MonoBehaviour
{
	public static SpyPanelManager inst;

	public UIButton btnAttrack;

	public UIScrollView scrollView;

	public UIGrid armyGrid;

	public GameObject armyPrefab;

	public GameObject skillPrefab;

	public UISprite bac;

	public GameObject armytext;

	public GameObject armyNotext;

	public GameObject showGotoPVPTips;

	public Transform attackBtn;

	public Transform backBtn;

	public GameObject addSlolider;

	public GameObject ResText;

	public GameObject CloseTips;

	public UILabel resTipLable;

	public UILabel attackCostLing;

	public GameObject NeedMoney;

	public UILabel enemyName;

	public UILabel enemyLv;

	public UILabel enemyPhec;

	public UILabel enemyKuoKu;

	public UILabel NeedMoneyUILabel;

	public GameObject zhanyitishiPanel;

	public GameObject box;

	public GameObject closeBtn;

	public GameObject powerTiao;

	public GameObject bottomLeft;

	public GameObject bottomRight;

	private bool SetTeXiao;

	private Body_Model btnEffect;

	public float protect_time;

	private float RefreshTime;

	private int costRmb;

	private float last = -1000f;

	public void OnDestroy()
	{
		SpyPanelManager.inst = null;
	}

	private void Awake()
	{
		SpyPanelManager.inst = this;
		this.InitEvent();
		this.armyGrid.isRespositonOnStart = false;
	}

	public void OnSetTeXiao()
	{
		if (this.SetTeXiao)
		{
			return;
		}
		this.SetTeXiao = true;
		if (this.btnEffect == null)
		{
			this.btnEffect = PoolManage.Ins.GetEffectByName("xinshou_zhiyin", this.attackBtn);
			Transform[] componentsInChildren = this.btnEffect.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 5;
			}
		}
	}

	public void ShwoPvpTips()
	{
		this.showGotoPVPTips.gameObject.SetActive(true);
		this.showGotoPVPTips.GetComponent<ShowPVPTips>().showPVPTips(LanguageManage.GetTextByKey("系统即将为您匹配到了一个对手，如果您确定要开始战斗，我们会从您的金库扣除", "others") + UnitConst.GetInstance().DesighConfigDic[65].value + LanguageManage.GetTextByKey("金币", "others"), delegate
		{
			if (HeroInfo.GetInstance().playerRes.resCoin > int.Parse(UnitConst.GetInstance().DesighConfigDic[65].value))
			{
				PVPHandler.CS_PVP();
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金币不足", "ResIsland") + "!", HUDTextTool.TextUITypeEnum.Num5);
			}
		}, null);
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		HUDTextTool.inst.Powerhouse();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.SpyPanel_Attack, new EventManager.VoidDelegate(this.Attack));
		EventManager.Instance.AddEvent(EventManager.EventType.SpyPanel_Back, new EventManager.VoidDelegate(this.Back));
		this.protect_time = 0.2f;
	}

	public void BtnSearchAgain(GameObject ga)
	{
		this.ShwoPvpTips();
	}

	private void Attack(GameObject ga)
	{
		if (InfoPanel.inst.transform.childCount > 0)
		{
			GameTools.RemoveChilderns(InfoPanel.inst.transform);
		}
		if (this.protect_time > 0f)
		{
			return;
		}
		this.protect_time = 1f;
		this.SetTeXiao = false;
		ga.GetComponent<ButtonClick>().isSendLua = false;
		this.Attack();
		SenceManager.inst.fightType = FightingType.Attack;
	}

	private void Back(GameObject ga)
	{
		if (this.protect_time > 0f)
		{
			return;
		}
		this.SetTeXiao = false;
		SenceInfo.CurReportData = null;
		if (SenceInfo.battleResource == SenceInfo.BattleResource.WorldMap)
		{
			PlayerHandle.GOTO_WorldMap();
			return;
		}
		UIManager.curState = SenceState.Home;
		Loading.IsRefreshSence = true;
		SenceHandler.CG_GetMapData(HeroInfo.GetInstance().homeInWMapIdx, 1, 0, null);
	}

	public void ShowTank()
	{
		int num = 0;
		if (HeroInfo.GetInstance().Commando_Fight != null)
		{
			GameObject gameObject = GameTools.CreateChildrenInGrid(this.armyGrid, this.armyPrefab, HeroInfo.GetInstance().Commando_Fight.index.ToString());
			UISprite component = gameObject.transform.FindChild("icon").GetComponent<UISprite>();
			AtlasManage.SetSpritName(component, UnitConst.GetInstance().soldierList[HeroInfo.GetInstance().Commando_Fight.index].icon);
			gameObject.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("x{0}", 1);
			gameObject.transform.localPosition = new Vector3(-200f, 0f, 0f);
			gameObject.transform.DOLocalMoveX((float)num * this.armyGrid.cellWidth, 0.2f, false).SetDelay(0.1f * (float)num);
			num++;
		}
		Dictionary<long, long> armysToBattle = HeroInfo.GetInstance().ArmysToBattle;
		foreach (KeyValuePair<long, long> current in armysToBattle)
		{
			GameObject gameObject2 = GameTools.CreateChildrenInGrid(this.armyGrid, this.armyPrefab, current.Key.ToString());
			UISprite component2 = gameObject2.transform.FindChild("icon").GetComponent<UISprite>();
			AtlasManage.SetArmyIconSpritName(component2, (int)current.Key);
			gameObject2.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("x{0}", current.Value);
			gameObject2.transform.localPosition = new Vector3(-200f, 0f, 0f);
			gameObject2.transform.DOLocalMoveX((float)num * this.armyGrid.cellWidth, 0.2f, false).SetDelay(0.1f * (float)num);
			num++;
		}
		foreach (SkillCarteData current2 in HeroInfo.GetInstance().skillCarteList)
		{
			if (current2.index > 0)
			{
				GameObject gameObject3 = GameTools.CreateChildrenInGrid(this.armyGrid, this.skillPrefab, current2.id.ToString());
				AtlasManage.SetSkillFSpritName(gameObject3.transform.FindChild("icon").GetComponent<UISprite>(), current2.itemID);
				AtlasManage.SetSkillQuilityInBattleSpriteName(gameObject3.GetComponent<UISprite>(), UnitConst.GetInstance().skillList[current2.itemID].skillQuality);
				gameObject3.transform.localPosition = new Vector3(-200f, 0f, 0f);
				gameObject3.transform.DOLocalMoveX((float)num * this.armyGrid.cellWidth, 0.2f, false).SetDelay(0.1f * (float)num);
				num++;
			}
		}
		if (num > 7)
		{
			TweenWidth.Begin(this.bac, 0.66f, 700);
		}
		else
		{
			TweenWidth.Begin(this.bac, 0.1f * (float)num, num * 100 + 40);
		}
		HUDTextTool.inst.NextLuaCall("侦察敌人 调用Lua", new object[0]);
	}

	private void PlayTextTip(Transform tar, string text)
	{
		if (!this.ResText.activeSelf)
		{
			this.ResText.SetActive(true);
		}
		this.ResText.transform.parent = tar;
		this.ResText.transform.localPosition = Vector3.zero;
		this.resTipLable.text = text;
		TweenScale component = this.ResText.GetComponent<TweenScale>();
		if (!component.enabled)
		{
			component.enabled = true;
		}
		component.ResetToBeginning();
	}

	private void OnDisable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
	}

	private void OnEnable()
	{
		this.RefreshTime = Time.time;
		this.OnSetTeXiao();
		if (UIManager.curState != SenceState.Spy)
		{
			return;
		}
		SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		this.armyNotext.SetActive(!HeroInfo.GetInstance().ISCanAttackByAllSoldier);
		if (UIManager.curState == SenceState.WatchResIsland || UIManager.curState == SenceState.Visit)
		{
			return;
		}
		this.ShowTank();
		this.enemyName.text = SenceInfo.SpyPlayerInfo.ownerName;
		this.enemyLv.text = SenceInfo.SpyPlayerInfo.ownerLevel.ToString();
		SenceManager.inst.EnemyLv = SenceInfo.SpyPlayerInfo.ownerLevel;
		this.enemyKuoKu.text = "敌军";
		if (SenceInfo.battleResource == SenceInfo.BattleResource.WorldMap && SenceInfo.CurSelectIslandData != null)
		{
			this.attackCostLing.text = string.Format("{0}{1}/{2}", (1 <= HeroInfo.GetInstance().playerRes.junLing) ? string.Empty : "[ff0000]", HeroInfo.GetInstance().playerRes.tanSuoLing, 1);
			if (T_WMap.inst && T_WMap.inst.islandList.ContainsKey(SenceInfo.curMap.mapIndex))
			{
				foreach (KeyValuePair<ResType, int> current in SenceInfo.curMap.baseRes)
				{
					if (!T_WMap.inst.islandList[SenceInfo.curMap.mapIndex].reward.ContainsKey(current.Key))
					{
						T_WMap.inst.islandList[SenceInfo.curMap.mapIndex].reward.Add(current.Key, 0);
					}
					T_WMap.inst.islandList[SenceInfo.curMap.mapIndex].reward[current.Key] = current.Value;
				}
				T_WMap.inst.islandList[SenceInfo.curMap.mapIndex].commandLV = SenceManager.inst.MainBuilding.lv;
			}
		}
		if (SenceInfo.battleResource == SenceInfo.BattleResource.WorldMap)
		{
			int num = int.Parse(UnitConst.GetInstance().DesighConfigDic[65].value) * HeroInfo.GetInstance().PlayerCommondLv;
			this.NeedMoney.SetActive(true);
			this.NeedMoneyUILabel.text = num.ToString();
			if (HeroInfo.GetInstance().playerRes.resCoin < num)
			{
				this.NeedMoneyUILabel.color = Color.red;
			}
			else
			{
				this.NeedMoneyUILabel.color = Color.white;
			}
		}
	}

	private void TryAttack(int rmb)
	{
		this.Attack();
	}

	public void Attack()
	{
		if (HeroInfo.GetInstance().ISCanAttackByAllSoldier)
		{
			if (SenceInfo.battleResource == SenceInfo.BattleResource.WorldMap)
			{
				int num = int.Parse(UnitConst.GetInstance().DesighConfigDic[65].value) * HeroInfo.GetInstance().PlayerCommondLv;
				if (HeroInfo.GetInstance().playerRes.resCoin < num)
				{
					this.costRmb = ResourceMgr.GetRMBNum(BuildingProductType.coin, num - HeroInfo.GetInstance().playerRes.resCoin);
					MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("金币不足", "ResIsland"), string.Format("{0}", LanguageManage.GetTextByKey("是否用钻石兑换金币来用于战斗", "Battle")), LanguageManage.GetTextByKey("花费钻石", "Battle") + this.costRmb, delegate
					{
						if (this.costRmb > HeroInfo.GetInstance().playerRes.RMBCoin)
						{
							ShopPanelManage.ShowHelp_NoRMB(null, null);
							return;
						}
						this.Attack_PlunderResTips();
					}, LanguageManage.GetTextByKey("取消", "Battle"), null);
					return;
				}
			}
			else
			{
				this.costRmb = 0;
			}
			this.Attack_PlunderResTips();
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("没有可出战的坦克", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	private void Attack_PlunderResTips()
	{
		if (PlunderPanel.inst.maxCoinNum > 0 && HeroInfo.GetInstance().playerRes.resCoin + PlunderPanel.inst.maxCoinNum > HeroInfo.GetInstance().playerRes.maxCoin)
		{
			MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("储量不足", "Battle"), string.Format("{0}", LanguageManage.GetTextByKey("您仓库储量不足以存放战利品，是否确认攻击？(战斗胜利后有部分资源损失)", "Battle")), LanguageManage.GetTextByKey("确认", "Battle"), new Action(this.AttackTrue), LanguageManage.GetTextByKey("取消", "Battle"), null);
		}
		else if (PlunderPanel.inst.maxOilNum > 0 && HeroInfo.GetInstance().playerRes.resOil + PlunderPanel.inst.maxOilNum > HeroInfo.GetInstance().playerRes.maxOil)
		{
			MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("储量不足", "Battle"), string.Format("{0}", LanguageManage.GetTextByKey("您仓库储量不足以存放战利品，是否确认攻击？(战斗胜利后有部分资源损失)", "Battle")), LanguageManage.GetTextByKey("确认", "Battle"), new Action(this.AttackTrue), LanguageManage.GetTextByKey("取消", "Battle"), null);
		}
		else if (PlunderPanel.inst.maxSteelNum > 0 && HeroInfo.GetInstance().playerRes.resSteel + PlunderPanel.inst.maxSteelNum > HeroInfo.GetInstance().playerRes.maxSteel)
		{
			MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("储量不足", "Battle"), string.Format("{0}", LanguageManage.GetTextByKey("您仓库储量不足以存放战利品，是否确认攻击？(战斗胜利后有部分资源损失)", "Battle")), LanguageManage.GetTextByKey("确认", "Battle"), new Action(this.AttackTrue), LanguageManage.GetTextByKey("取消", "Battle"), null);
		}
		else if (PlunderPanel.inst.maxRareEarthNum > 0 && HeroInfo.GetInstance().playerRes.resRareEarth + PlunderPanel.inst.maxRareEarthNum > HeroInfo.GetInstance().playerRes.maxRareEarth)
		{
			MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("储量不足", "Battle"), string.Format("{0}", LanguageManage.GetTextByKey("您仓库储量不足以存放战利品，是否确认攻击？(战斗胜利后有部分资源损失)", "Battle")), LanguageManage.GetTextByKey("确认", "Battle"), new Action(this.AttackTrue), LanguageManage.GetTextByKey("取消", "Battle"), null);
		}
		else
		{
			this.AttackTrue();
		}
	}

	public void AttackTrue()
	{
		bool isRefresh = Time.time - this.RefreshTime > 120f;
		if (Time.time < this.last + 1.2f)
		{
			return;
		}
		this.last = Time.time;
		if (SenceInfo.battleResource == SenceInfo.BattleResource.WorldMap)
		{
			Debug.Log("作战图 PVP开战!");
			FightHundler.CG_StartFight((long)SenceInfo.CurSelectIslandData.idx, 1, this.costRmb, 0L, 0, isRefresh);
			return;
		}
		if (SenceInfo.battleResource == SenceInfo.BattleResource.PVPFightBack_Home || SenceInfo.battleResource == SenceInfo.BattleResource.PVPFightBack_WorldMap)
		{
			Debug.Log("PVP复仇开战!");
			FightHundler.CG_StartFight(SenceInfo.curMap.ownerID, 4, this.costRmb, PVPMessage.inst.PVPFightBackID, 0, isRefresh);
			return;
		}
		if (SenceInfo.battleResource == SenceInfo.BattleResource.LegionBattleFight)
		{
			Debug.Log("军团副本开战!");
			Debug.Log("SenceInfo.curMap.ownerID:" + SenceInfo.curMap.ownerID);
			FightHundler.CG_StartFight((long)SenceInfo.CurBattleField.id, 8, this.costRmb, 0L, 0, isRefresh);
			return;
		}
		FightHundler.CG_StartFight((long)SenceInfo.CurBattleField.id, 2, this.costRmb, 0L, 0, isRefresh);
	}

	private void Update()
	{
		if (this.protect_time > 0f)
		{
			this.protect_time -= Time.deltaTime;
		}
	}
}
