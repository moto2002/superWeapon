using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SettlementManager : FuncUIPanel
{
	public static int Star;

	public static SettlementManager inst;

	public UISprite title;

	public GameObject defeatedSp;

	public GameObject winSp;

	public GameObject winAnimatior;

	public GameObject defeatedObj;

	public GameObject WinObj;

	public GameObject showBoxObj;

	public LangeuageLabel ShangWang;

	public LangeuageLabel DefeatedShangwang;

	public static int maxCoin;

	public static int maxOil;

	public static int maxSteel;

	public static int maxRareth;

	public LangeuageLabel rewardDesc;

	public LangeuageLabel loseDesc;

	public UISprite defeatedLabel;

	public GameObject[] starArray;

	public GameObject ResWin;

	public GameObject SoliderDesBG;

	public static int soliderId;

	public static float exp;

	public static List<KVStruct> itemList = new List<KVStruct>();

	public static List<KVStruct> rewardList = new List<KVStruct>();

	public static List<KVStruct> equipsList = new List<KVStruct>();

	public static List<KVStruct> deadList = new List<KVStruct>();

	private List<GameObject> deaditemList = new List<GameObject>();

	private List<GameObject> rewardTweenList = new List<GameObject>();

	private float offset = 225f;

	public Transform resRoot;

	public Transform itemRoot;

	public Transform armyRoot;

	public Transform kuang;

	public Transform shibai;

	private GameObject rewardRes;

	private GameObject itemRes;

	private GameObject armyRes;

	public GameObject specialRes;

	public GameObject WinBg;

	public static bool isDead = false;

	public static bool isHave = false;

	public static int getExp;

	public float timer;

	private DateTime finiTime;

	public int leftTime = 5;

	private ResTips resTip;

	public Transform Victorybtn;

	[HideInInspector]
	public bool isCanBack;

	public static bool isPanelShow = false;

	public GameObject SueecssEffectGa;

	private bool isnewBiLock;

	private bool is3DnewibiLock;

	public static bool IsNeedAutoAddArmy = false;

	public void OnDestroy()
	{
		SettlementManager.inst = null;
	}

	public override void OnDisable()
	{
		AudioManage.inst.IsPlayAudio_3D = true;
		base.OnDisable();
	}

	public override void Awake()
	{
		SettlementManager.inst = this;
		base.Invoke("OnGetBoxColider", 4f);
		this.Victorybtn = base.transform.FindChild("back/Success/BtnSprite");
		this.rewardRes = (GameObject)Resources.Load(ResManager.FuncUI_Path + "SettleIcon");
		this.itemRes = (GameObject)Resources.Load(ResManager.FuncUI_Path + "SettleItem");
		this.armyRes = (GameObject)Resources.Load(ResManager.FuncUI_Path + "SettleArmy");
		this.specialRes = (GameObject)Resources.Load(ResManager.FuncUI_Path + "SettleSpecial");
		this.InitEvent();
	}

	private void InitEvent()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.SettlePanel_Close, new EventManager.VoidDelegate(this.Close));
		EventManager.Instance.AddEvent(EventManager.EventType.SettlePanel_CloseTips, new EventManager.VoidDelegate(this.CloseTips));
		EventManager.Instance.AddEvent(EventManager.EventType.SettlePanel_Failure, new EventManager.VoidDelegate(this.Failure));
		EventManager.Instance.AddEvent(EventManager.EventType.SettlePanel_WinClick, new EventManager.VoidDelegate(this.WinClick));
		EventManager.Instance.AddEvent(EventManager.EventType.SettlePanel_LoseClick, new EventManager.VoidDelegate(this.DefeatedClick));
	}

	public void OnGetBoxColider()
	{
		this.WinBg.GetComponent<BoxCollider>().enabled = true;
	}

	private void Failure(GameObject ga)
	{
	}

	public void OnStarTween()
	{
		base.StartCoroutine(this.starShowTween());
	}

	[DebuggerHidden]
	public IEnumerator starShowTween()
	{
		SettlementManager.<starShowTween>c__Iterator7B <starShowTween>c__Iterator7B = new SettlementManager.<starShowTween>c__Iterator7B();
		<starShowTween>c__Iterator7B.<>f__this = this;
		return <starShowTween>c__Iterator7B;
	}

	private void CloseTips(GameObject ga)
	{
		if (this.resTip != null && this.resTip.gameObject.activeSelf)
		{
			NGUITools.SetActive(this.resTip.gameObject, false);
		}
	}

	private void Close(GameObject ga)
	{
		if (!this.isCanBack)
		{
			return;
		}
		if (!NewbieGuidePanel.isEnemyAttck)
		{
			return;
		}
		this.resTip.gameObject.SetActive(false);
		ButtonClick.newbiLock = this.isnewBiLock;
		Camera_FingerManager.newbiLock = this.is3DnewibiLock;
		NGUITools.SetActive(this.winSp, false);
		NGUITools.SetActive(this.defeatedSp, false);
		NGUITools.SetActive(this.loseDesc.gameObject, false);
		NGUITools.SetActive(this.rewardDesc.gameObject, false);
		for (int i = 0; i < this.rewardTweenList.Count; i++)
		{
			UnityEngine.Object.Destroy(this.rewardTweenList[i]);
		}
		for (int j = 0; j < this.deaditemList.Count; j++)
		{
			UnityEngine.Object.Destroy(this.deaditemList[j]);
		}
		this.resRoot.GetComponent<UIGrid>().ClearChild();
		this.rewardTweenList.Clear();
		this.deaditemList.Clear();
		if (SenceInfo.battleResource != SenceInfo.BattleResource.WorldMap)
		{
			if (HeroInfo.GetInstance().addBox.Count > 0)
			{
				LogManage.LogError("HeroInfo.GetInstance().addBox:" + HeroInfo.GetInstance().addBox.Count);
				NGUITools.SetActive(this.showBoxObj, true);
				NGUITools.SetActive(this.SueecssEffectGa, false);
			}
			else
			{
				UIManager.curState = SenceState.Home;
				Loading.IsRefreshSence = true;
				SenceHandler.CG_GetMapData(HeroInfo.GetInstance().homeInWMapIdx, 1, 0, null);
			}
		}
		else
		{
			NGUITools.SetActive(this.showBoxObj, false);
			PlayerHandle.GOTO_WorldMap();
		}
		if (FightPanelManager.inst)
		{
			FightPanelManager.inst.gameObject.SetActive(false);
		}
		UIManager.inst.UIInUsed(false);
	}

	public override void OnEnable()
	{
		if (FightPanelManager.inst)
		{
			FightPanelManager.inst.gameObject.SetActive(false);
		}
		if (HUDTextTool.inst)
		{
			this.resTip = HUDTextTool.inst.restip;
		}
		this.isCanBack = false;
		this.isnewBiLock = ButtonClick.newbiLock;
		this.is3DnewibiLock = Camera_FingerManager.newbiLock;
		ButtonClick.newbiLock = false;
		Camera_FingerManager.newbiLock = false;
		if (SenceManager.inst != null)
		{
			this.finiTime = TimeTools.ConvertSecondDateTime(this.leftTime);
			this.DiaplayResult();
			AudioManage.inst.PauseAudioBackground();
			if (SenceManager.inst.settType == SettlementType.failure)
			{
				HUDTextTool.inst.Kaiqixinshouyindao = false;
				NewbieGuidePanel._instance.HideSelf();
				this.DefeatedClick(null);
				AudioManage.inst.IsPlayAudio_3D = false;
				AudioManage.inst.PlayAuido("field", false);
			}
			else
			{
				this.SueecssEffectGa.SetActive(true);
				AudioManage.inst.IsPlayAudio_3D = false;
				AudioManage.inst.PlayAuido("win", false);
			}
		}
		HUDTextTool.inst.NextLuaCall("战报弹出调用Lua", new object[0]);
		base.OnEnable();
	}

	public void OnDideItemTweenShow()
	{
		for (int i = 0; i < this.deaditemList.Count; i++)
		{
			this.deaditemList[i].GetComponent<TweenAlpha>().PlayForward();
		}
	}

	public void OnRewardTweenShow()
	{
		for (int i = 0; i < this.rewardTweenList.Count; i++)
		{
			this.rewardTweenList[i].GetComponent<TweenAlpha>().PlayForward();
		}
	}

	public void WinClick(GameObject go)
	{
		base.StartCoroutine(this.WinPanelTiemShow(this.winSp));
	}

	public void DefeatedClick(GameObject go)
	{
		base.StartCoroutine(this.DefeatedPanelTimeShow(this.defeatedSp));
		Body_Model effectByName = PoolManage.Ins.GetEffectByName("shibai_effect", null);
		effectByName.tr.parent = this.kuang;
		effectByName.tr.localPosition = new Vector3(0f, 123.86f, 0f);
		effectByName.ga.AddComponent<RenderQueueEdit>();
		Body_Model effectByName2 = PoolManage.Ins.GetEffectByName("shibai_effect_shandian", null);
		effectByName2.tr.parent = this.shibai;
		effectByName2.tr.localPosition = Vector3.zero;
		effectByName2.ga.AddComponent<RenderQueueEdit>();
	}

	[DebuggerHidden]
	private IEnumerator WinPanelTiemShow(GameObject o)
	{
		SettlementManager.<WinPanelTiemShow>c__Iterator7C <WinPanelTiemShow>c__Iterator7C = new SettlementManager.<WinPanelTiemShow>c__Iterator7C();
		<WinPanelTiemShow>c__Iterator7C.o = o;
		<WinPanelTiemShow>c__Iterator7C.<$>o = o;
		<WinPanelTiemShow>c__Iterator7C.<>f__this = this;
		return <WinPanelTiemShow>c__Iterator7C;
	}

	[DebuggerHidden]
	private IEnumerator DefeatedPanelTimeShow(GameObject o)
	{
		SettlementManager.<DefeatedPanelTimeShow>c__Iterator7D <DefeatedPanelTimeShow>c__Iterator7D = new SettlementManager.<DefeatedPanelTimeShow>c__Iterator7D();
		<DefeatedPanelTimeShow>c__Iterator7D.o = o;
		<DefeatedPanelTimeShow>c__Iterator7D.<$>o = o;
		<DefeatedPanelTimeShow>c__Iterator7D.<>f__this = this;
		return <DefeatedPanelTimeShow>c__Iterator7D;
	}

	public void WinShow()
	{
		base.StartCoroutine(this.WinHaidBtn(this.winSp));
	}

	public void defeatedShow()
	{
		base.StartCoroutine(this.defeatedHiadBtn(this.defeatedSp));
	}

	[DebuggerHidden]
	private IEnumerator WinHaidBtn(GameObject o)
	{
		SettlementManager.<WinHaidBtn>c__Iterator7E <WinHaidBtn>c__Iterator7E = new SettlementManager.<WinHaidBtn>c__Iterator7E();
		<WinHaidBtn>c__Iterator7E.o = o;
		<WinHaidBtn>c__Iterator7E.<$>o = o;
		return <WinHaidBtn>c__Iterator7E;
	}

	[DebuggerHidden]
	private IEnumerator defeatedHiadBtn(GameObject o)
	{
		SettlementManager.<defeatedHiadBtn>c__Iterator7F <defeatedHiadBtn>c__Iterator7F = new SettlementManager.<defeatedHiadBtn>c__Iterator7F();
		<defeatedHiadBtn>c__Iterator7F.o = o;
		<defeatedHiadBtn>c__Iterator7F.<$>o = o;
		return <defeatedHiadBtn>c__Iterator7F;
	}

	private void DiaplayResult()
	{
		this.SetData();
	}

	private void Update()
	{
	}

	private void SetData()
	{
		if (SenceManager.inst.settType == SettlementType.failure)
		{
			this.armyRoot.localPosition = new Vector3(0f, -75f, 0f);
		}
	}

	public void ButtnEvent(SettleBtnType type)
	{
		switch (type)
		{
		}
	}

	public void OnGetTeXiao()
	{
	}

	public static void AddNewDead(int id)
	{
		for (int i = 0; i < SettlementManager.deadList.Count; i++)
		{
			if ((long)id == SettlementManager.deadList[i].key)
			{
				SettlementManager.deadList[i].value += 1L;
				return;
			}
		}
		KVStruct kVStruct = new KVStruct();
		kVStruct.key = (long)id;
		kVStruct.value = 1L;
		SettlementManager.deadList.Add(kVStruct);
	}

	public static void GetRewardInfo(List<KVStruct> cs)
	{
		SettlementManager.rewardList.Clear();
		SettlementManager.rewardList.AddRange(cs);
	}

	public static void GetEquipInfo(List<KVStruct> cs)
	{
		SettlementManager.equipsList.Clear();
		SettlementManager.equipsList.AddRange(cs);
	}

	public static void GetItemReward(List<KVStruct> cs)
	{
		SettlementManager.itemList.Clear();
		SettlementManager.itemList.AddRange(cs);
	}

	[DebuggerHidden]
	public IEnumerator CreatRewardList()
	{
		SettlementManager.<CreatRewardList>c__Iterator80 <CreatRewardList>c__Iterator = new SettlementManager.<CreatRewardList>c__Iterator80();
		<CreatRewardList>c__Iterator.<>f__this = this;
		return <CreatRewardList>c__Iterator;
	}

	public string OnSetQuatiry(int leve)
	{
		switch (leve)
		{
		case 1:
			return "白";
		case 2:
			return "绿";
		case 3:
			return "蓝";
		case 4:
			return "紫";
		case 5:
			return "红";
		default:
			return string.Empty;
		}
	}

	private GameObject CreatIcon(Transform root, int i, int count, int IconType)
	{
		GameObject original = null;
		switch (IconType)
		{
		case 1:
			original = this.rewardRes;
			break;
		case 2:
			original = this.itemRes;
			break;
		case 3:
			original = this.armyRes;
			break;
		case 4:
			original = this.specialRes;
			break;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(original) as GameObject;
		gameObject.transform.parent = root;
		gameObject.transform.localScale = Vector3.one;
		float y = 0f;
		switch (IconType)
		{
		case 1:
			this.offset = -112.8f;
			y = -132.2f;
			break;
		case 2:
			this.offset = -112.8f;
			y = -132.2f;
			break;
		case 3:
			this.offset = -112.8f;
			y = -132.2f;
			break;
		}
		float xPosInCenter = GameTools.GetXPosInCenter(i, this.offset, count);
		gameObject.transform.localPosition = new Vector3(xPosInCenter, y, 0f);
		return gameObject;
	}

	public static void ClearBattleInfo()
	{
		SettlementManager.rewardList.Clear();
		SettlementManager.deadList.Clear();
		SettlementManager.itemList.Clear();
	}
}
