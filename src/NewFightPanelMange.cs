using DicForUnity;
using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NewFightPanelMange : FuncUIPanel
{
	public static NewFightPanelMange inst;

	public Transform parent;

	public List<GameObject> battleItem = new List<GameObject>();

	public UILabel battleName;

	public GameObject tollgate;

	public GameObject battle;

	public GameObject backBtn;

	public GameObject CloseBtn;

	public GameObject Mop_upPanel;

	public GameObject junling;

	public NewTollgateManage battleFightUI;

	public UISprite bg;

	private BattleField curBattleField;

	private int isNewBattle;

	private List<Battle> AllBattle = new List<Battle>();

	public void OnDestroy()
	{
		NewFightPanelMange.inst = null;
	}

	public override void Awake()
	{
		NewFightPanelMange.inst = this;
		this.Initialize();
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_battleItem, new EventManager.VoidDelegate(this.BattleItemClick));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_GoToFight, new EventManager.VoidDelegate(this.GoToFight));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_ClosePanle, new EventManager.VoidDelegate(this.ClosePanle));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanrl_NewBackBtn, new EventManager.VoidDelegate(this.BackBtnOnClick));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_SweepOne, new EventManager.VoidDelegate(this.SweepOne));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_SweepTen, new EventManager.VoidDelegate(this.SweepTen));
		this.battleName.text = LanguageManage.GetTextByKey("战役", "Battle");
	}

	private void Initialize()
	{
		this.parent = base.transform.FindChild("Battle/BattleItemList/Grid");
		this.battleName = base.transform.FindChild("Bottom/Right/Name").GetComponent<UILabel>();
		this.tollgate = base.transform.FindChild("Tollgate_BG").gameObject;
		this.tollgate.AddComponent<NewTollgateManage>();
		this.battle = base.transform.FindChild("Battle").gameObject;
		this.backBtn = base.transform.FindChild("NewBG/ReturnBtn").gameObject;
		this.CloseBtn = base.transform.FindChild("NewBG/CloseBtn").gameObject;
		this.Mop_upPanel = base.transform.FindChild("Tollgate_BG/battle/mop upPanel").gameObject;
		this.junling = base.transform.FindChild("Junling").gameObject;
		this.junling.AddComponent<MilitaryOrders>();
		this.junling.AddComponent<ButtonClick>();
		ButtonClick component = this.junling.GetComponent<ButtonClick>();
		component.eventType = EventManager.EventType.BattlePanel_BuyJunlingBtn;
		this.battleFightUI = base.transform.FindChild("Tollgate_BG").GetComponent<NewTollgateManage>();
	}

	private void Start()
	{
		this.battle.SetActive(false);
		this.tollgate.SetActive(true);
	}

	public void BackBtnOnClick(GameObject ga)
	{
		HUDTextTool.inst.isEndTollgate = null;
		this.battle.SetActive(true);
		this.tollgate.SetActive(false);
		this.junling.SetActive(false);
		for (int i = 0; i < this.battleItem.Count; i++)
		{
			if (this.isNewBattle <= this.battleItem.Count - 5)
			{
				this.parent.GetComponent<UIGrid>().pivot = UIWidget.Pivot.TopLeft;
				this.battleItem[i].transform.localPosition = new Vector3((float)(i - this.isNewBattle) * this.parent.GetComponent<UIGrid>().cellWidth, 0f, 0f);
			}
			else
			{
				this.parent.GetComponent<UIGrid>().pivot = UIWidget.Pivot.TopRight;
				this.parent.GetComponent<UIGrid>().Reposition();
			}
		}
		ga.SetActive(false);
	}

	private void ClosePanle(GameObject ga)
	{
		this.junling.SetActive(true);
		FuncUIManager.inst.MainUIPanelManage.gameObject.SetActive(true);
		base.gameObject.SetActive(false);
	}

	private void GoToFight(GameObject ga)
	{
		this.curBattleField = ga.transform.parent.GetComponent<BattleFightUIInfo>().curBattfiled;
		if (HeroInfo.GetInstance().PlayerCommondLv >= this.curBattleField.commondLevel)
		{
			if (this.curBattleField.cost <= HeroInfo.GetInstance().playerRes.junLing)
			{
				base.gameObject.SetActive(false);
				NewbieGuidePanel.isZhanyi = false;
				SenceHandler.CG_GetMapData(this.curBattleField.id, 2, 0, delegate(bool isError)
				{
					if (!isError)
					{
						UIManager.curState = SenceState.Spy;
						SenceInfo.battleResource = SenceInfo.BattleResource.NormalBattleFight;
					}
				});
			}
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军令不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
			}
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("司令部等级不够", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	private void SweepOne(GameObject ga)
	{
		this.curBattleField = ga.transform.parent.GetComponent<BattleFightUIInfo>().curBattfiled;
		if (this.curBattleField.cost <= HeroInfo.GetInstance().playerRes.junLing && HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(2) && 1 <= HeroInfo.GetInstance().PlayerItemInfo[2])
		{
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().AllDropList[UnitConst.GetInstance().AllNpc[this.curBattleField.npcId].dropListId].res)
			{
				switch (current.Key)
				{
				case ResType.金币:
					if (HeroInfo.GetInstance().playerRes.resCoin + current.Value > HeroInfo.GetInstance().playerRes.maxCoin)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepOne), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				case ResType.石油:
					if (HeroInfo.GetInstance().playerRes.resOil + current.Value > HeroInfo.GetInstance().playerRes.maxOil)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepOne), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				case ResType.钢铁:
					if (HeroInfo.GetInstance().playerRes.resSteel + current.Value > HeroInfo.GetInstance().playerRes.maxSteel)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepOne), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				case ResType.稀矿:
					if (HeroInfo.GetInstance().playerRes.resRareEarth + current.Value > HeroInfo.GetInstance().playerRes.maxRareEarth)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepOne), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				}
			}
			this.OnSweepOne();
		}
		else if (this.curBattleField.cost > HeroInfo.GetInstance().playerRes.junLing)
		{
			HUDTextTool.inst.SetText("军令不足", HUDTextTool.TextUITypeEnum.Num5);
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("扫荡令不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10061)
		{
			this.Mop_upPanel.SetActive(true);
		}
	}

	private void SweepTen(GameObject ga)
	{
		this.curBattleField = ga.transform.parent.GetComponent<BattleFightUIInfo>().curBattfiled;
		if (!HeroInfo.GetInstance().vipData.IsVIP)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("请先开通VIP", "others"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (this.curBattleField.cost <= HeroInfo.GetInstance().playerRes.junLing && HeroInfo.GetInstance().PlayerItemInfo.ContainsKey(2) && 1 <= HeroInfo.GetInstance().PlayerItemInfo[2])
		{
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().AllDropList[UnitConst.GetInstance().AllNpc[this.curBattleField.npcId].dropListId].res)
			{
				switch (current.Key)
				{
				case ResType.金币:
					if (HeroInfo.GetInstance().playerRes.resCoin + current.Value > HeroInfo.GetInstance().playerRes.maxCoin)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepTen), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				case ResType.石油:
					if (HeroInfo.GetInstance().playerRes.resOil + current.Value > HeroInfo.GetInstance().playerRes.maxOil)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepTen), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				case ResType.钢铁:
					if (HeroInfo.GetInstance().playerRes.resRareEarth + current.Value > HeroInfo.GetInstance().playerRes.maxRareEarth)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepTen), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				case ResType.稀矿:
					if (HeroInfo.GetInstance().playerRes.resSteel + current.Value > HeroInfo.GetInstance().playerRes.maxSteel)
					{
						MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("仓库资源不足", "Battle"), LanguageManage.GetTextByKey("资源仓库不足以存放战利品，是否确认扫荡，扫荡完成后会有部分资源损失", "Battle"), LanguageManage.GetTextByKey("确定", "Battle"), new Action(this.OnSweepTen), LanguageManage.GetTextByKey("取消", "Battle"), null);
						return;
					}
					break;
				}
			}
			this.OnSweepTen();
		}
		else if (this.curBattleField.cost > HeroInfo.GetInstance().playerRes.junLing)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军令不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("扫荡令不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	public void OnSweepOne()
	{
		CSSweep cSSweep = new CSSweep();
		cSSweep.battleId = this.curBattleField.battleId;
		ClientMgr.GetNet().SendHttp(5016, cSSweep, null, null);
	}

	public void OnSweepTen()
	{
	}

	public void BattleItemClick(GameObject ga)
	{
		NewFightBattleItem component = ga.GetComponent<NewFightBattleItem>();
		if (component.item.FightRecord.isFight || component.isNowBattle)
		{
			this.backBtn.SetActive(true);
			this.battle.SetActive(false);
			this.battleName.text = LanguageManage.GetTextByKey(component.item.name, "Battle");
			for (int i = 0; i < this.battleItem.Count; i++)
			{
				if (this.battleItem[i].GetComponent<NewFightBattleItem>().item != component.item)
				{
					this.battleItem[i].GetComponent<NewFightBattleItem>().ShowBattleItem();
				}
			}
			SenceInfo.CurBattle = component.item;
			this.tollgate.SetActive(true);
			this.junling.SetActive(true);
			this.tollgate.GetComponent<NewTollgateManage>().item = component.item;
			this.tollgate.GetComponent<NewTollgateManage>().ShowTollgateItem();
		}
	}

	public void BattleItem(Battle item)
	{
		if (item != null)
		{
			this.backBtn.SetActive(true);
			this.battle.SetActive(false);
			this.battleName.text = LanguageManage.GetTextByKey(item.name, "Battle");
			for (int i = 0; i < this.battleItem.Count; i++)
			{
				if (this.battleItem[i].GetComponent<NewFightBattleItem>().item != item)
				{
					this.battleItem[i].GetComponent<NewFightBattleItem>().ShowBattleItem();
				}
			}
			SenceInfo.CurBattle = item;
			this.tollgate.SetActive(true);
			this.tollgate.GetComponent<NewTollgateManage>().item = item;
			this.tollgate.GetComponent<NewTollgateManage>().ShowTollgateItem();
		}
	}

	public override void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnEnable();
	}

	public override void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		base.OnDisable();
	}

	public void CreateBattleItem()
	{
		this.parent.GetComponent<UIGrid>().isRespositonOnStart = false;
		for (int i = 0; i < this.battleItem.Count; i++)
		{
			UnityEngine.Object.Destroy(this.battleItem[i]);
		}
		this.battleItem.Clear();
		DicForU.GetValues<int, Battle>(UnitConst.GetInstance().BattleConst, this.AllBattle);
		for (int j = 0; j < this.AllBattle.Count; j++)
		{
			Battle battle = this.AllBattle[j];
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(ResManager.FuncUI_Path + "NewBattleItem") as GameObject) as GameObject;
			gameObject.transform.position = Vector3.zero;
			gameObject.GetComponent<NewFightBattleItem>().item = battle;
			gameObject.transform.parent = this.parent;
			gameObject.transform.localScale = Vector3.one;
			if (battle.preId != 0)
			{
				if (!battle.FightRecord.isFight && UnitConst.GetInstance().BattleConst[battle.preId].FightRecord.isFight && !UnitConst.GetInstance().BattleConst[battle.nextId].FightRecord.isFight)
				{
					gameObject.GetComponent<NewFightBattleItem>().isNowBattle = true;
				}
			}
			else if (battle.preId == 0 && !UnitConst.GetInstance().BattleConst[battle.nextId].FightRecord.isFight)
			{
				gameObject.GetComponent<NewFightBattleItem>().isNowBattle = true;
			}
			gameObject.GetComponent<NewFightBattleItem>().ShowBattleItem();
			this.battleItem.Add(gameObject);
		}
		if (SenceInfo.CurBattle == null || SenceInfo.CurBattle.FightRecord.isFight)
		{
			for (int k = 0; k < this.battleItem.Count; k++)
			{
				if (this.battleItem[k].GetComponent<NewFightBattleItem>().isNowBattle)
				{
					this.isNewBattle = k;
					this.BattleItemClick(this.battleItem[this.isNewBattle]);
				}
			}
		}
	}
}
