using msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
	public static TipsManager inst;

	public TipsRoot[] tipses;

	public WMapTipsType tipsId = WMapTipsType.myHome;

	public T_Island curIsland;

	public Transform investigationBtn;

	public Transform probeBtn;

	public Transform tipsChild;

	public Material clownMaterial;

	public Material clownMaterial2;

	public Material clownMaterial3;

	public Material clown_RedMaterial;

	public Material clown_SelectMaterial;

	public string clownIconRes = "ClownPrice";

	public string islandIconRes = "islandIcon";

	public GameObject clownIcon;

	public GameObject islandIcon;

	public Camera uiCamera;

	public ResTips restips;

	private bool first = true;

	private int openClownId;

	private int costRmb;

	public void OnDestroy()
	{
		TipsManager.inst = null;
	}

	private void Awake()
	{
		TipsManager.inst = this;
		this.uiCamera = UIManager.inst.uiCamera;
		this.clownIcon = (GameObject)Resources.Load(ResManager.WMapRes_Path + this.clownIconRes);
		this.islandIcon = (GameObject)Resources.Load(ResManager.WMapRes_Path + this.islandIconRes);
		this.investigationBtn = base.transform.FindChild("TipsPanel/MainTips/tips_5/Btn");
		this.probeBtn = base.transform.FindChild("TipsPanel/MainTips/tips_0/Btn");
		this.InitEvent();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_OpenCloud, new EventManager.VoidDelegate(this.OpenCloud));
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_QuickWarring, new EventManager.VoidDelegate(this.QuickWarring));
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_ReFind, new EventManager.VoidDelegate(this.ReFind));
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_Spy, new EventManager.VoidDelegate(this.Spy));
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_Warring, new EventManager.VoidDelegate(this.Warring));
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_Watch, new EventManager.VoidDelegate(this.Watch));
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_Enter, new EventManager.VoidDelegate(this.Enter));
	}

	private void Enter(GameObject go)
	{
		this.BtnEvent(TipsBtnType.enter, 0);
	}

	private void OpenCloud(GameObject ga)
	{
		this.BtnEvent(TipsBtnType.openCloud, 0);
	}

	private void QuickWarring(GameObject ga)
	{
		this.BtnEvent(TipsBtnType.quickWarring, 0);
	}

	private void ReFind(GameObject ga)
	{
		this.BtnEvent(TipsBtnType.reFind, 0);
	}

	private void Spy(GameObject ga)
	{
		this.BtnEvent(TipsBtnType.spy, 0);
	}

	private void Warring(GameObject ga)
	{
		this.BtnEvent(TipsBtnType.warring, 0);
	}

	private void Watch(GameObject ga)
	{
		this.BtnEvent(TipsBtnType.watch, 0);
	}

	private void OnEnable()
	{
		this.CloseAllTips();
	}

	public void OpenTips(T_Island t)
	{
		if (t.uiType == WMapTipsType.enemy || t.uiType == WMapTipsType.enemyRes)
		{
			this.curIsland = t;
			this.tipsId = t.uiType;
			this.tipses[(int)this.tipsId].gameObject.SetActive(false);
			this.tipses[(int)this.tipsId].tar = t;
			TipsEnemy component = this.tipses[(int)this.tipsId].gameObject.GetComponent<TipsEnemy>();
			this.SetTipsPos(t.transform, (int)this.tipsId);
		}
		else if (t.uiType == WMapTipsType.dailyAct && ActivityManager.GetIns().IsFirstOpen)
		{
			ActivityManager.GetIns().IsFirstOpen = false;
			if (TalkManager.ins == null)
			{
				MessageBox.GetTalkPanel().Talk(1, new TalkManager.VoidDelegate(this.TalkCallBack), new UnityEngine.Object[]
				{
					t
				});
			}
		}
		else if (t.uiType == WMapTipsType.weekAct && ActivityManager.GetIns().IsWeekFirstOpen)
		{
			ActivityManager.GetIns().IsWeekFirstOpen = false;
			if (TalkManager.ins == null)
			{
				MessageBox.GetTalkPanel().Talk(1, new TalkManager.VoidDelegate(this.TalkCallBack), new UnityEngine.Object[]
				{
					t
				});
			}
		}
		else
		{
			this.curIsland = t;
			this.tipsId = t.uiType;
			if (this.tipsId == WMapTipsType.battle)
			{
				this.tipses[5].gameObject.SetActive(false);
				this.tipses[5].tar = t;
				this.SetTipsPos(t.transform, 5);
				LogManage.Log(this.tipses[5].gameObject.activeSelf);
			}
			else
			{
				this.tipses[(int)this.tipsId].gameObject.SetActive(false);
				this.tipses[(int)this.tipsId].tar = t;
				this.SetTipsPos(t.transform, (int)this.tipsId);
				LogManage.Log(this.tipses[(int)this.tipsId].gameObject.activeSelf);
			}
		}
	}

	public void TalkCallBack(params UnityEngine.Object[] args)
	{
		this.curIsland = (args[0] as T_Island);
		this.tipsId = this.curIsland.uiType;
		this.tipses[(int)this.tipsId].gameObject.SetActive(false);
		this.SetTipsPos(this.curIsland.transform, (int)this.tipsId);
		LogManage.Log(this.tipses[(int)this.tipsId].gameObject.activeSelf);
	}

	private void SetTipsPos(Transform t, int i)
	{
		if (i == 2)
		{
			this.tipses[i].SetPosNoPianyi(t);
		}
		else
		{
			this.tipses[i].SetPos(t);
		}
	}

	public void CloseAllTips()
	{
		this.restips = HUDTextTool.inst.restip;
		this.restips.gameObject.SetActive(false);
		for (int i = 0; i < this.tipses.Length; i++)
		{
			this.tipses[i].gameObject.SetActive(false);
		}
		this.curIsland = null;
	}

	public void CreatChild(T_Island t)
	{
		GameObject original = this.islandIcon;
		GameObject gameObject = UnityEngine.Object.Instantiate(original, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.transform.parent = this.tipsChild;
		TitleIsland component = gameObject.GetComponent<TitleIsland>();
		component.island = t;
		component.ResetInfo();
	}

	public void BtnEvent(TipsBtnType type, int npcId = 0)
	{
		UIManager.curState = (SenceState)type;
		if (type != TipsBtnType.warring)
		{
			this.GetIslandData(npcId);
			this.CloseAllTips();
			return;
		}
		UIManager.inst.NoSpyDirectAtt = true;
		if (!HeroInfo.GetInstance().ISCanAttackByAllSoldier)
		{
			HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("兵种不足", "others"), HUDTextTool.TextUITypeEnum.Num5);
			return;
		}
		if (this.curIsland.islandId != 0L && SenceInfo.curMap.ID == this.curIsland.islandId && SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
		{
			SenceInfo.battleResource = SenceInfo.BattleResource.WorldMap;
			PlayerHandle.EnterSence("island");
			this.CloseAllTips();
			return;
		}
		SenceInfo.battleResource = SenceInfo.BattleResource.WorldMap;
		SenceInfo.CurSelectIslandData = HeroInfo.GetInstance().worldMapInfo.playerWMap[this.curIsland.mapIdx];
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
				FightHundler.CG_StartFight((long)this.curIsland.mapIdx, 1, this.costRmb, 0L, 0, true);
			}, LanguageManage.GetTextByKey("取消", "Battle"), null);
			return;
		}
		FightHundler.CG_StartFight((long)this.curIsland.mapIdx, 1, this.costRmb, 0L, 0, true);
		this.CloseAllTips();
	}

	public void GetIslandData(int npcId = 0)
	{
		if (ActivityManager.GetIns().IsDailyAct)
		{
			SenceInfo.CurSelectIslandData = HeroInfo.GetInstance().worldMapInfo.playerWMap[this.curIsland.mapIdx];
			SenceInfo.battleResource = SenceInfo.BattleResource.WorldMap;
			SenceHandler.CG_GetMapData(this.curIsland.mapIdx, 1, npcId, null);
			return;
		}
		if (ActivityManager.GetIns().IsWeekAct)
		{
			SenceInfo.CurSelectIslandData = HeroInfo.GetInstance().worldMapInfo.playerWMap[this.curIsland.mapIdx];
			SenceInfo.battleResource = SenceInfo.BattleResource.WorldMap;
			SenceHandler.CG_GetMapData(this.curIsland.mapIdx, 1, 0, null);
			return;
		}
		if (this.curIsland.islandId != 0L && SenceInfo.curMap.ID == this.curIsland.islandId && SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
		{
			SenceInfo.battleResource = SenceInfo.BattleResource.WorldMap;
			PlayerHandle.EnterSence("island");
		}
		else
		{
			SenceInfo.battleResource = SenceInfo.BattleResource.WorldMap;
			SenceInfo.CurSelectIslandData = HeroInfo.GetInstance().worldMapInfo.playerWMap[this.curIsland.mapIdx];
			SenceHandler.CG_GetMapData(this.curIsland.mapIdx, 1, 0, null);
		}
		this.first = true;
	}

	private void SweepOne()
	{
		Battle battleItem = this.curIsland.battleItem;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		foreach (KeyValuePair<int, BattleField> current in battleItem.allBattleField)
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

	public void OnSweepOne()
	{
		CSSweep cSSweep = new CSSweep();
		cSSweep.battleId = this.curIsland.battleItem.id;
		ClientMgr.GetNet().SendHttp(5016, cSSweep, new DataHandler.OpcodeHandler(this.OpenBattleFieldBoxCallBack), null);
	}

	private void OpenBattleFieldBoxCallBack(bool isError, Opcode code)
	{
		if (!isError)
		{
			ShowAwardPanelManger.showAwardList();
		}
	}
}
