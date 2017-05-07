using DG.Tweening;
using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FightPanelManager : MonoBehaviour
{
	public static FightPanelManager inst;

	public static bool IsRetreat;

	public Dictionary<int, SoldierUIITem> solider_UIDIC = new Dictionary<int, SoldierUIITem>();

	public SoldierUIITem CommandSoliderUI;

	public List<SkillUIITem> skillUIList = new List<SkillUIITem>();

	public int ExtraArmyID_Add;

	public FightPanel_SkillAndSoliderUIItem curSelectUIItem;

	public Transform retreatBtn;

	public UIGrid solider_SkillGrid;

	public UISprite gridBac;

	public GameObject powerelectricity;

	public GameObject BoxSkill;

	public UILabel changeNameLabel;

	public List<GameObject> panelList = new List<GameObject>();

	public static bool supperSkillReady;

	public UISprite changSolider;

	public Dictionary<ResType, int> BattleFightRes = new Dictionary<ResType, int>();

	public GameObject armyPrefab;

	public GameObject skillPrefab;

	public List<GameObject> hideUIList = new List<GameObject>();

	public UISprite AutoFight;

	private float changeSolider_CD_time;

	public Transform sendPos;

	public bool isSpColor;

	public int SkillId;

	public UILabel CoinLabel;

	public UILabel OilLabel;

	public UILabel SteelLabel;

	public UILabel RareEarthLabel;

	private int j;

	public List<int> SoliderKeys = new List<int>();

	public static Body_Model TeXiao;

	private int depth;

	public int SupplyTankCardNum;

	public int Card_No;

	public int Supply_Card_No;

	public TweenScale tween;

	public GameObject FightMessageParent;

	public GameObject FightMessage;

	public FightPanel_SkillAndSoliderUIItem CurSelectUIItem
	{
		get
		{
			return this.curSelectUIItem;
		}
		set
		{
			if (value == null)
			{
				SenceManager.inst.SetBuildGridActive(false);
				this.curSelectUIItem = null;
			}
			FightPanel_SkillAndSoliderUIItem[] componentsInChildren = this.solider_SkillGrid.GetComponentsInChildren<FightPanel_SkillAndSoliderUIItem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				FightPanel_SkillAndSoliderUIItem fightPanel_SkillAndSoliderUIItem = componentsInChildren[i];
				if (fightPanel_SkillAndSoliderUIItem != value)
				{
					fightPanel_SkillAndSoliderUIItem.tr.localPosition = new Vector3(fightPanel_SkillAndSoliderUIItem.tr.localPosition.x, 0f, fightPanel_SkillAndSoliderUIItem.tr.localPosition.z);
					if (fightPanel_SkillAndSoliderUIItem.btnState == SoliderButtonState.inUse)
					{
						fightPanel_SkillAndSoliderUIItem.btnState = SoliderButtonState.canUse;
					}
				}
				else if (this.curSelectUIItem == value)
				{
					fightPanel_SkillAndSoliderUIItem.tr.localPosition = new Vector3(fightPanel_SkillAndSoliderUIItem.tr.localPosition.x, 0f, fightPanel_SkillAndSoliderUIItem.tr.localPosition.z);
					SenceManager.inst.SetBuildGridActive(false);
					this.curSelectUIItem = null;
					if (fightPanel_SkillAndSoliderUIItem.GetComponent<SkillUIITem>())
					{
					}
					if (fightPanel_SkillAndSoliderUIItem.btnState == SoliderButtonState.inUse)
					{
						fightPanel_SkillAndSoliderUIItem.btnState = SoliderButtonState.canUse;
					}
				}
				else
				{
					fightPanel_SkillAndSoliderUIItem.tr.localPosition = new Vector3(fightPanel_SkillAndSoliderUIItem.tr.localPosition.x, 20f, fightPanel_SkillAndSoliderUIItem.tr.localPosition.z);
					if (this.curSelectUIItem != null && this.curSelectUIItem.GetComponent<SkillUIITem>() && value.GetComponent<SoldierUIITem>())
					{
						this.curSelectUIItem.GetComponent<SkillUIITem>().ReadyToUse = false;
						SkillManage.inst.ReadyUseSkill = false;
						SkillManage.inst.ReadyUseSkill_Next = false;
						SkillManage.inst.useskillCard = null;
						UnityEngine.Object.Destroy(SkillManage.inst.ReadyUseSkill_circleEffect.gameObject);
						CameraControl.inst.enabled = true;
					}
					this.curSelectUIItem = value;
					this.curSelectUIItem.btnState = SoliderButtonState.inUse;
					SenceManager.inst.SetBuildGridActive(true);
				}
			}
		}
	}

	public void OnDestroy()
	{
		FightPanelManager.inst = null;
	}

	public void CommandSoliderIsDead()
	{
		if (this.CommandSoliderUI)
		{
			foreach (KeyValuePair<int, SoldierUIITem> current in this.solider_UIDIC)
			{
				if (current.Value != null && current.Value == this.CommandSoliderUI)
				{
					this.solider_UIDIC.Remove(current.Key);
					UnityEngine.Object.Destroy(this.CommandSoliderUI.ga);
					this.CommandSoliderUI = null;
					break;
				}
			}
		}
		this.RefreshGrid();
	}

	public TankTeamOperation TankTeamOperation_inst()
	{
		if (TankTeamOperation.inst != null)
		{
			return TankTeamOperation.inst;
		}
		return null;
	}

	public void Awake()
	{
		FightPanelManager.inst = this;
		this.InitEvent();
		FightPanelManager.supperSkillReady = false;
		this.curSelectUIItem = null;
		this.solider_SkillGrid.isRespositonOnStart = false;
		FightHundler.isSendFightEnd = false;
		if (!NewbieGuidePanel.isEnemyAttck)
		{
			base.gameObject.SetActive(false);
			return;
		}
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanel_ForceMove, new EventManager.VoidDelegate(this.ForceMove));
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanel_Landing, new EventManager.VoidDelegate(this.Landing));
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanel_Retreat, new EventManager.VoidDelegate(this.Retreat));
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanel_ShowSkillClick, new EventManager.VoidDelegate(this.OnSkillShow));
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanle_UIItemClick, new EventManager.VoidDelegate(this.FightPanle_UIItemClick));
	}

	public void ChangeButton_Close(bool close)
	{
	}

	private void Update()
	{
		if (this.changeSolider_CD_time > 0f)
		{
			this.changeSolider_CD_time -= Time.deltaTime;
		}
	}

	public void FightPanle_UIItemClick(GameObject go)
	{
		go.GetComponent<FightPanel_SkillAndSoliderUIItem>().OnClickEvent(true);
	}

	[DebuggerHidden]
	public IEnumerator CreateNpcTank(List<NpcAttackBattleItem> items, int posIndex, NpcAttackBattle npc)
	{
		FightPanelManager.<CreateNpcTank>c__Iterator61 <CreateNpcTank>c__Iterator = new FightPanelManager.<CreateNpcTank>c__Iterator61();
		<CreateNpcTank>c__Iterator.posIndex = posIndex;
		<CreateNpcTank>c__Iterator.items = items;
		<CreateNpcTank>c__Iterator.npc = npc;
		<CreateNpcTank>c__Iterator.<$>posIndex = posIndex;
		<CreateNpcTank>c__Iterator.<$>items = items;
		<CreateNpcTank>c__Iterator.<$>npc = npc;
		return <CreateNpcTank>c__Iterator;
	}

	private void OnSkillShow(GameObject go)
	{
	}

	public void OnBoxSkillClose()
	{
		this.isSpColor = false;
		this.isSpColor = false;
		this.BoxSkill.GetComponent<UISprite>().ShaderToNormal();
		this.BoxSkill.SetActive(false);
	}

	public void HidePanelList()
	{
		for (int i = 0; i < this.panelList.Count; i++)
		{
			this.panelList[i].gameObject.SetActive(false);
		}
	}

	public void OnBoxSkillSet(int SkillIndex)
	{
		this.BoxSkill.SetActive(true);
		this.SkillId = SkillIndex;
		this.BoxSkill.GetComponent<UISprite>().spriteName = UnitConst.GetInstance().skillList[this.SkillId].Ficon;
	}

	public void ShowPanelList()
	{
		for (int i = 0; i < this.panelList.Count; i++)
		{
			this.panelList[i].gameObject.SetActive(true);
		}
	}

	private void Retreat(GameObject ga)
	{
		Time.timeScale = 0f;
		MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("退出战场", "Battle"), LanguageManage.GetTextByKey("是否要退出战场", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.RepeatYes), LanguageManage.GetTextByKey("取消", "Battle"), delegate
		{
			Time.timeScale = 1f;
		});
	}

	private void RepeatYes()
	{
		Time.timeScale = 1f;
		if (FightPanelManager.IsRetreat)
		{
			return;
		}
		FightPanelManager.IsRetreat = true;
		this.retreatBtn.GetComponent<BoxCollider>().enabled = false;
		this.retreatBtn.GetComponent<UIButton>().enabled = false;
		this.retreatBtn.GetComponent<UISprite>().ShaderToGray();
		SenceManager.inst.settType = SettlementType.failure;
		FightHundler.CG_FinishFight();
	}

	public void DisplayBattleAddRes()
	{
		if (this.BattleFightRes.ContainsKey(ResType.金币) && this.BattleFightRes[ResType.金币] > 0)
		{
			this.CoinLabel.gameObject.SetActive(true);
			this.CoinLabel.text = this.BattleFightRes[ResType.金币].ToString();
		}
		else
		{
			this.CoinLabel.gameObject.SetActive(false);
		}
		if (this.BattleFightRes.ContainsKey(ResType.石油) && this.BattleFightRes[ResType.石油] > 0)
		{
			this.OilLabel.gameObject.SetActive(true);
			this.OilLabel.text = this.BattleFightRes[ResType.石油].ToString();
		}
		else
		{
			this.OilLabel.gameObject.SetActive(false);
		}
		if (this.BattleFightRes.ContainsKey(ResType.钢铁) && this.BattleFightRes[ResType.钢铁] > 0)
		{
			this.SteelLabel.gameObject.SetActive(true);
			this.SteelLabel.text = this.BattleFightRes[ResType.钢铁].ToString();
		}
		else
		{
			this.SteelLabel.gameObject.SetActive(false);
		}
		if (this.BattleFightRes.ContainsKey(ResType.稀矿) && this.BattleFightRes[ResType.稀矿] > 0)
		{
			this.RareEarthLabel.gameObject.SetActive(true);
			this.RareEarthLabel.text = this.BattleFightRes[ResType.稀矿].ToString();
		}
		else
		{
			this.RareEarthLabel.gameObject.SetActive(false);
		}
	}

	private void Landing(GameObject ga)
	{
	}

	private void ForceMove(GameObject ga)
	{
	}

	private void Start()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.FightPanel_UseSupperSkill, new EventManager.VoidDelegate(this.UseSupperSkill));
		HUDTextTool.inst.Powerhouse();
		this.BoxSkill.SetActive(false);
		this.InitSkillAndSoliderUI();
		this.DisplayBattleAddRes();
	}

	public void SetAllCardProtectTime(FightPanel_SkillAndSoliderUIItem card, float protect_time)
	{
		for (int i = 0; i < this.skillUIList.Count; i++)
		{
			if (this.skillUIList[i] != card)
			{
				this.skillUIList[i].protect_time = protect_time;
			}
		}
	}

	public void SetSoldierCD()
	{
	}

	public void SetAirCD()
	{
	}

	public void SetSkillCD()
	{
		for (int i = 0; i < this.skillUIList.Count; i++)
		{
			this.skillUIList[i].Now_CD_Time = this.skillUIList[i].CD_Time;
		}
	}

	public void FindSoliderUIButton()
	{
	}

	public void RefershSoliderUIButton(SoldierUIITem selUIButton)
	{
		if (selUIButton)
		{
			selUIButton.tr.localPosition = new Vector3(selUIButton.tr.localPosition.x, 50f, selUIButton.tr.localPosition.y);
			selUIButton.ResetSelect(true);
			SenceManager.inst.SetBuildGridActive(true);
		}
		else
		{
			SenceManager.inst.SetBuildGridActive(false);
		}
	}

	public void RefershSkilUIButton(SkillUIITem selUIButton)
	{
		for (int i = 0; i < this.skillUIList.Count; i++)
		{
			if (!this.skillUIList[i].Equals(selUIButton) || selUIButton == null)
			{
				this.skillUIList[i].ResetSelect(false);
			}
		}
		if (selUIButton)
		{
			selUIButton.ResetSelect(true);
		}
	}

	public bool IsCanBattle()
	{
		if (this.CommandSoliderUI != null)
		{
			return true;
		}
		if (this.skillUIList.Count > 0)
		{
			return true;
		}
		foreach (KeyValuePair<int, SoldierUIITem> current in this.solider_UIDIC)
		{
			if (current.Value && current.Value.soliderNum > 0)
			{
				return true;
			}
		}
		return false;
	}

	public void RemoveSkillButton(SkillUIITem removeSkillUIButton)
	{
		this.skillUIList.Remove(removeSkillUIButton);
		TweenScale.Begin(removeSkillUIButton.ga, 0.3f, Vector3.zero).SetOnFinished(new EventDelegate(delegate
		{
			UnityEngine.Object.Destroy(removeSkillUIButton.ga);
		}));
	}

	public void RreshTankUI(T_TankAbstract tank)
	{
		int num = 0;
		for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
		{
			if (SenceManager.inst.Tanks_Attack[i].index == tank.index)
			{
				num++;
			}
		}
		if (num == 0 && this.solider_UIDIC.ContainsKey(tank.index) && this.solider_UIDIC[tank.index].GetSoliderNum_plus() == 0)
		{
			UnityEngine.Object.Destroy(this.solider_UIDIC[tank.index].ga);
			this.RefreshGrid();
		}
	}

	public void AddNewSupplyTankUIItem(int tank_index, int tank_num)
	{
		if (this.solider_UIDIC.ContainsKey(tank_index))
		{
			UnityEngine.Debug.Log("具有同类型坦克卡牌，无需生成新坦克卡牌");
			return;
		}
		string gaName = string.Empty + tank_index;
		GameObject gameObject = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.armyPrefab, gaName);
		SoldierUIITem component = gameObject.GetComponent<SoldierUIITem>();
		component.Card_BJ.spriteName = "蓝";
		component.soliderIndex = tank_index;
		this.solider_UIDIC.Add(tank_index, component);
		component.SetInfo(true);
		component.tr.localPosition = new Vector3(-1000f, 0f, 0f);
		component.tr.SetSiblingIndex(0);
		this.RefreshGrid();
	}

	private void AddCommanderUIItem()
	{
		SoldierUIITem component = NGUITools.AddChild(this.solider_SkillGrid.gameObject, this.armyPrefab).GetComponent<SoldierUIITem>();
		component.soliderType = SoliderType.Commander;
		component.diamond.gameObject.SetActive(false);
		component.GetComponent<ButtonClick>().IsAutoSetColor = false;
		component.count = 1;
		component.SetInfo(false);
		SettlementManager.isDead = false;
		component.Card_No = this.Card_No;
		this.Card_No++;
	}

	private void InitSkillAndSoliderUI()
	{
		int num = 0;
		this.Card_No = 1;
		Dictionary<long, long> armysToBattle = HeroInfo.GetInstance().ArmysToBattle;
		foreach (armyInfoInBuilding current in HeroInfo.GetInstance().AllArmyInfo.Values)
		{
			foreach (KVStruct current2 in current.armyFunced)
			{
				if (current2.value > 0L)
				{
					if (!this.solider_UIDIC.ContainsKey((int)current2.key))
					{
						GameObject gameObject = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.armyPrefab, current2.key.ToString());
						SoldierUIITem component = gameObject.GetComponent<SoldierUIITem>();
						component.Card_BJ.spriteName = "蓝";
						component.soliderIndex = (int)current2.key;
						this.solider_UIDIC.Add((int)current2.key, component);
						num++;
					}
					int num2 = 0;
					while ((long)num2 < current2.value)
					{
						this.solider_UIDIC[(int)current2.key].allArmyData.Add(new armyData((int)current2.key, HeroInfo.GetInstance().GetArmyLevelByID((int)current2.key), 0, current.buildingID));
						num2++;
					}
				}
			}
		}
		if (HeroInfo.GetInstance().Commando_Fight != null)
		{
			GameObject gameObject2 = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.armyPrefab, "Commando_Fight");
			SoldierUIITem component2 = gameObject2.GetComponent<SoldierUIITem>();
			component2.soliderIndex = 1000 + HeroInfo.GetInstance().Commando_Fight.index;
			component2.Card_BJ.spriteName = "蓝";
			this.solider_UIDIC.Add(component2.soliderIndex, component2);
			this.CommandSoliderUI = component2;
			component2.SetInfo(false);
			num++;
		}
		foreach (SkillCarteData current3 in HeroInfo.GetInstance().skillCarteList)
		{
			if (current3.index > 0)
			{
				GameObject gameObject3 = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.skillPrefab, current3.id.ToString());
				SkillUIITem component3 = gameObject3.GetComponent<SkillUIITem>();
				component3.Card_BJ.spriteName = "绿";
				component3.skill = current3;
				component3.SetInfo();
				this.skillUIList.Add(component3);
				num++;
			}
		}
		foreach (KeyValuePair<int, SCExtraArmy> current4 in SenceManager.inst.PlayerExtraArmyList)
		{
			for (int i = 0; i < current4.Value.itemId2Level.Count; i++)
			{
				int num3 = (int)current4.Value.id;
				int num4 = (int)current4.Value.itemId2Level[i].key;
				int lv = (int)current4.Value.itemId2Level[i].value;
				int num5 = (int)current4.Value.itemId2Num[i].value;
				if (num5 > 0)
				{
					if (!this.solider_UIDIC.ContainsKey(num4))
					{
						GameObject gameObject4 = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.armyPrefab, num4.ToString());
						SoldierUIITem component4 = gameObject4.GetComponent<SoldierUIITem>();
						component4.IsExtraArmyCard = true;
						component4.soliderNum = num5;
						component4.Card_BJ.spriteName = "蓝";
						component4.soliderIndex = num4;
						this.solider_UIDIC.Add(num4, component4);
						num++;
					}
					else
					{
						this.solider_UIDIC[num4].soliderNum += num5;
					}
					for (int j = 0; j < num5; j++)
					{
						this.solider_UIDIC[num4].allArmyData.Add(new armyData(num4, lv, 0, 0L));
					}
				}
			}
		}
		if (NewbieGuidePanel.curGuideIndex == -1)
		{
			if (HeroInfo.GetInstance().gameStart.soliderIndex > 0)
			{
				GameObject gameObject5 = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.armyPrefab, "Commando_Fight");
				SoldierUIITem component5 = gameObject5.GetComponent<SoldierUIITem>();
				component5.soliderIndex = 1000 + HeroInfo.GetInstance().gameStart.soliderIndex;
				component5.Card_BJ.spriteName = "蓝";
				this.CommandSoliderUI = component5;
				this.solider_UIDIC.Add(component5.soliderIndex, component5);
				component5.SetInfo(false);
				num++;
			}
			foreach (GameStart_AttackNPC.armyData_NpcAttack current5 in HeroInfo.GetInstance().gameStart.armys)
			{
				if (!this.solider_UIDIC.ContainsKey(current5.armyIndex))
				{
					GameObject gameObject6 = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.armyPrefab, current5.armyIndex.ToString());
					SoldierUIITem component6 = gameObject6.GetComponent<SoldierUIITem>();
					component6.Card_BJ.spriteName = "蓝";
					component6.soliderIndex = current5.armyIndex;
					this.solider_UIDIC.Add(current5.armyIndex, component6);
					num++;
				}
				for (int k = 0; k < current5.armyNum; k++)
				{
					this.solider_UIDIC[current5.armyIndex].allArmyData.Add(new armyData(current5.armyIndex, current5.armyLV, 0, 0L));
				}
			}
			foreach (int current6 in HeroInfo.GetInstance().gameStart.skills)
			{
				GameObject gameObject7 = GameTools.CreateChildrenInGrid(this.solider_SkillGrid, this.skillPrefab, current6.ToString());
				SkillUIITem component7 = gameObject7.GetComponent<SkillUIITem>();
				SkillCarteData skillCarteData = new SkillCarteData();
				skillCarteData.id = 0L;
				skillCarteData.itemID = current6;
				component7.Card_BJ.spriteName = "绿";
				component7.skill = skillCarteData;
				component7.SetInfo();
				this.skillUIList.Add(component7);
				num++;
			}
		}
		int num6 = 0;
		foreach (SoldierUIITem current7 in this.solider_UIDIC.Values)
		{
			current7.tr.localPosition = new Vector3((float)(1 - num) * this.solider_SkillGrid.cellWidth * 0.5f + this.solider_SkillGrid.cellWidth * (float)num6, -160f, 0f);
			current7.tr.DOLocalMoveY(0f, 0.2f, false).SetDelay(0.1f * (float)num6);
			num6++;
			current7.SetInfo(false);
		}
		foreach (SkillUIITem current8 in this.skillUIList)
		{
			current8.tr.localPosition = new Vector3((float)(1 - num) * this.solider_SkillGrid.cellWidth * 0.5f + this.solider_SkillGrid.cellWidth * (float)num6, -160f, 0f);
			current8.tr.DOLocalMoveY(0f, 0.2f, false).SetDelay(0.1f * (float)num6);
			num6++;
		}
		if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
		{
			this.gridBac.gameObject.SetActive(false);
		}
		else if (Screen.width > num * 100 + 40)
		{
			TweenWidth.Begin(this.gridBac, 0.1f * (float)num6, num * 100 + 40);
		}
		else
		{
			TweenWidth.Begin(this.gridBac, 0.1f * (float)(Screen.width / 100), Screen.width);
		}
		if (!NewbieGuidePanel.isEnemyAttck)
		{
			GameSetting.autoFight = true;
			NewbieGuidePanel.CallLuaByStartFire();
		}
		else
		{
			HUDTextTool.inst.NextLuaCall("开始战斗", new object[0]);
		}
	}

	public void RefreshGrid()
	{
		base.StartCoroutine(this.yield_RefreshGrid());
	}

	[DebuggerHidden]
	private IEnumerator yield_RefreshGrid()
	{
		FightPanelManager.<yield_RefreshGrid>c__Iterator62 <yield_RefreshGrid>c__Iterator = new FightPanelManager.<yield_RefreshGrid>c__Iterator62();
		<yield_RefreshGrid>c__Iterator.<>f__this = this;
		return <yield_RefreshGrid>c__Iterator;
	}

	private void UseSupperSkill(GameObject ga)
	{
		if (this.BoxSkill.activeSelf)
		{
			this.BoxSkill.GetComponent<UISprite>().ShaderToNormal();
		}
		if (FightPanelManager.TeXiao != null)
		{
			UnityEngine.Object.Destroy(FightPanelManager.TeXiao.gameObject);
		}
		ga.GetComponent<FightPanel_SkillAndSoliderUIItem>().DoPress();
	}

	public void CloseAllSkillSlot()
	{
	}

	public void OnEnd(int resNum)
	{
	}

	public void CreatFightMessage(string message, Color color, Transform tr = null)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.FightMessage, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.transform.parent = this.FightMessageParent.transform;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = Vector3.zero;
		Vector3 position = CameraControl.inst.MainCamera.WorldToScreenPoint(tr.position);
		position = UIManager.inst.uiCamera.ScreenToWorldPoint(position);
		gameObject.transform.position = new Vector3(position.x, position.y, 0f);
		gameObject.transform.localPosition += new Vector3(0f, 60f, 0f);
		gameObject.GetComponent<UILabel>().text = message;
		gameObject.GetComponent<UILabel>().color = color;
		gameObject.AddComponent<FightMessage>();
		gameObject.GetComponent<FightMessage>().this_tr = tr;
		gameObject.GetComponent<FightMessage>().pos_no_tr = tr.position;
		gameObject.GetComponent<FightMessage>().Init();
	}
}
