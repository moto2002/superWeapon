using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NTollgateManage : MonoBehaviour
{
	public static NTollgateManage inst;

	public UILabel titleName;

	public UIGrid tollgateParent;

	public List<GameObject> tollgateItemList = new List<GameObject>();

	public List<BattleField> AllBattleField = new List<BattleField>();

	public UISprite sweepOne;

	public UISprite sweepTen;

	public UISprite goFight;

	public UILabel sweepOneNeed;

	public UILabel sweepTenNeed;

	public UISprite sweepOneNeedIcon;

	public UISprite sweepTenNeedIcon;

	public UILabel sweepOneLable;

	public UILabel sweepTenLable;

	public UILabel goFightLabel;

	public UILabel junlingNum;

	public UILabel junlingDes;

	public Transform leftPos;

	public Transform rightPos;

	public Battle curBattle;

	public BattleField nowTollagte;

	public GameObject Mop_upPanel;

	public GameObject BattleFightUIInfoBtn;

	public GameObject goBack;

	public GameObject CloseBtn;

	public GameObject m_Notarize;

	public int tollgateID;

	private int needMoney;

	private float time;

	private bool tishi = true;

	private float last = -1000f;

	public BattleField curBattleField;

	private int isNowTollgate;

	public bool isFirst = true;

	public GameObject AddDataBG;

	public static bool isBoFangTexiao;

	public static int BattleFightIDToBofangTeXiao;

	public void OnDestroy()
	{
		NTollgateManage.inst = null;
	}

	private void Awake()
	{
		NTollgateManage.inst = this;
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanrl_NewBackBtn, new EventManager.VoidDelegate(this.CloseTollgatePanel));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_SweepOne, new EventManager.VoidDelegate(this.SweepOne));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_SweepTen, new EventManager.VoidDelegate(this.SweepTen));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_GoToFight, new EventManager.VoidDelegate(this.GoToFight));
		EventManager.Instance.AddEvent(EventManager.EventType.BattlePanel_BuyJunlingBtn, new EventManager.VoidDelegate(this.OnClickAddBtn));
		this.Initialize();
		base.gameObject.SetActive(false);
	}

	private void Initialize()
	{
		this.titleName = base.transform.FindChild("BG/Title/Name").GetComponent<UILabel>();
		this.tollgateParent = base.transform.FindChild("tollgateList/Grid").GetComponent<UIGrid>();
		this.sweepOne = base.transform.FindChild("BG/One").GetComponent<UISprite>();
		this.sweepOneNeed = base.transform.FindChild("BG/One/ICON/Num").GetComponent<UILabel>();
		this.sweepOneNeedIcon = base.transform.FindChild("BG/One/ICON").GetComponent<UISprite>();
		this.sweepOneLable = base.transform.FindChild("BG/One/Label").GetComponent<UILabel>();
		this.sweepTen = base.transform.FindChild("BG/Ten").GetComponent<UISprite>();
		this.sweepTenNeed = base.transform.FindChild("BG/Ten/ICON/Num").GetComponent<UILabel>();
		this.sweepTenNeedIcon = base.transform.FindChild("BG/Ten/ICON").GetComponent<UISprite>();
		this.sweepTenLable = base.transform.FindChild("BG/Ten/Label").GetComponent<UILabel>();
		this.goFight = base.transform.FindChild("BG/GotoFight").GetComponent<UISprite>();
		this.goFightLabel = base.transform.FindChild("BG/GotoFight/Label").GetComponent<UILabel>();
		this.leftPos = base.transform.FindChild("BG/Left");
		this.rightPos = base.transform.FindChild("BG/Right");
		this.Mop_upPanel = base.transform.FindChild("Panel/mop upPanel").gameObject;
		this.BattleFightUIInfoBtn = base.transform.FindChild("BG/GotoFight").gameObject;
		this.goBack = base.transform.FindChild("BG/BackBtn").gameObject;
		this.CloseBtn = base.transform.FindChild("BG/CloseTollgate").gameObject;
		this.m_Notarize = base.transform.FindChild("Panel/NotarizeWindow").gameObject;
		this.m_Notarize.AddComponent<NotarizeWindowManage>();
		this.junlingNum = base.transform.FindChild("BG/GotoFight/Num").GetComponent<UILabel>();
		this.junlingDes = base.transform.FindChild("BG/GotoFight/Xiaohao").GetComponent<UILabel>();
	}

	private void BackTollgatePanel(GameObject ga)
	{
		base.transform.parent.gameObject.SetActive(false);
	}

	private void CloseTollgatePanel(GameObject ga)
	{
		if (ga != null)
		{
			SenceInfo.battleResource = SenceInfo.BattleResource.HOME;
		}
		if (CameraZhedang.inst)
		{
			CameraZhedang.inst.uiInUseBox.SetActive(false);
		}
		SenceInfo.CurBattle = null;
		SenceInfo.CurBattleField = null;
		base.transform.parent.gameObject.SetActive(false);
	}

	public void OnClickAddBtn(GameObject go)
	{
		if (HeroInfo.GetInstance().playerRes.junLing >= int.Parse(UnitConst.GetInstance().DesighConfigDic[23].value))
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军令已满", "others"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		for (int i = 0; i < UnitConst.GetInstance().moneyToToken.Count; i++)
		{
			MoneyToToken moneyToToken = UnitConst.GetInstance().moneyToToken[i];
			if (moneyToToken.type == 12 && moneyToToken.times == HeroInfo.GetInstance().buyArmyTokenTimes + 1)
			{
				this.needMoney = moneyToToken.money;
			}
			if (HeroInfo.GetInstance().buyArmyTokenTimes >= 3)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("今日购买军令次数已满", "others"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		this.m_Notarize.SetActive(true);
		string des = string.Concat(new object[]
		{
			LanguageManage.GetTextByKey("花费", "Battle"),
			this.needMoney,
			LanguageManage.GetTextByKey("钻可购买", "Battle"),
			UnitConst.GetInstance().DesighConfigDic[23].value,
			LanguageManage.GetTextByKey("个军令", "Battle")
		});
		this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().ShowUI(des, this.needMoney.ToString(), 1, 3 - HeroInfo.GetInstance().buyArmyTokenTimes);
		if (HeroInfo.GetInstance().playerRes.RMBCoin < int.Parse(this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.text))
		{
			this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.transform.GetComponent<UILabel>().color = new Color(0.7647059f, 0.07058824f, 0f);
		}
		else
		{
			this.m_Notarize.transform.GetComponent<NotarizeWindowManage>().m_NeedNum.transform.GetComponent<UILabel>().color = new Color(1f, 1f, 1f);
		}
	}

	private void Update()
	{
		if (!this.tishi)
		{
			this.time += Time.deltaTime;
			if (this.time > 3f)
			{
				this.time = 0f;
				this.tishi = true;
			}
		}
	}

	private void GoToFight(GameObject ga)
	{
		if (CameraControl.inst)
		{
			CameraControl.inst.gameObject.SetActive(true);
		}
		this.curBattleField = this.nowTollagte;
		if (this.curBattleField.preId != 0 && !UnitConst.GetInstance().BattleFieldConst[this.curBattleField.preId].fightRecord.isFight)
		{
			if (this.tishi)
			{
				this.tishi = false;
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("前置关卡未通过", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			}
			return;
		}
		if (this.curBattleField.preId == 0 && UnitConst.GetInstance().BattleConst[this.curBattleField.battleId].preId != 0 && !UnitConst.GetInstance().BattleConst[UnitConst.GetInstance().BattleConst[this.curBattleField.battleId].preId].isCanSweep && UnitConst.GetInstance().BattleConst[UnitConst.GetInstance().BattleConst[this.curBattleField.battleId].preId].battleBox == 0)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("前置战役未通过", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (HeroInfo.GetInstance().PlayerRadarLv >= UnitConst.GetInstance().BattleConst[this.curBattleField.battleId].radarLevel)
		{
			if (this.curBattleField.cost <= HeroInfo.GetInstance().playerRes.junLing)
			{
				if (Time.time < this.last + 1.2f)
				{
					return;
				}
				this.last = Time.time;
				SenceInfo.CurBattleField = this.curBattleField;
				NewbieGuidePanel.isZhanyi = false;
				SenceInfo.battleResource = SenceInfo.BattleResource.NormalBattleFight;
				UIManager.curState = SenceState.Spy;
				SenceHandler.CG_GetMapData(this.curBattleField.id, 2, 0, null);
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
			else
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("军令不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
			}
		}
		else
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("雷达等级不够", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
		}
	}

	private void SweepOne(GameObject ga)
	{
		this.curBattleField = this.nowTollagte;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		foreach (KeyValuePair<int, BattleField> current in this.curBattle.allBattleField)
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

	private void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
	}

	private void OnDisable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
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
		this.curBattleField = this.nowTollagte;
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
		cSSweep.battleId = this.curBattle.id;
		ClientMgr.GetNet().SendHttp(5016, cSSweep, new DataHandler.OpcodeHandler(this.OpenBattleFieldBoxCallBack), null);
	}

	private void OpenBattleFieldBoxCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
			ShowAwardPanelManger._ins.CloseCallBack = new Action(this.ShowTislandInfo);
		}
	}

	public void ShowTislandInfo()
	{
		if (T_WMap.inst.curIsland_Sel)
		{
			T_WMap.inst.curIsland_Sel.ResetInfo();
		}
		this.CloseTollgatePanel(null);
	}

	public void OnSweepTen()
	{
	}

	public void ButtonShow(BattleField item)
	{
		this.sweepOneNeed.text = item.cost.ToString();
		this.sweepTenNeed.text = (10 * item.cost).ToString();
		this.junlingNum.text = item.cost.ToString();
		if (item.preId != 0 && !UnitConst.GetInstance().BattleFieldConst[item.preId].fightRecord.isFight)
		{
			this.goFight.gameObject.SetActive(true);
			this.goFight.ShaderToGray();
			this.goFightLabel.color = Color.gray;
			this.junlingDes.color = Color.gray;
			this.junlingNum.color = Color.gray;
		}
		else
		{
			this.goFight.gameObject.SetActive(true);
			this.goFight.ShaderToNormal();
			this.goFightLabel.color = Color.white;
			this.junlingDes.color = Color.white;
			this.junlingNum.color = Color.white;
		}
	}

	[DebuggerHidden]
	public IEnumerator ShowTollgateItem()
	{
		NTollgateManage.<ShowTollgateItem>c__Iterator64 <ShowTollgateItem>c__Iterator = new NTollgateManage.<ShowTollgateItem>c__Iterator64();
		<ShowTollgateItem>c__Iterator.<>f__this = this;
		return <ShowTollgateItem>c__Iterator;
	}
}
