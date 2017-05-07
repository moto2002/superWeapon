using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Loading : IMonoBehaviour
{
	private float wwwProcess;

	private bool isBeginLoad;

	public static string senceName = "Island";

	public static string IslandSenceName = "Island";

	public static SenceType senceType = SenceType.Login;

	private AsyncOperation async;

	public static Loading ins = null;

	public static bool IsRefreshSence = true;

	public List<Vector3> dianList = new List<Vector3>();

	public static List<CameraManage> changeSence = new List<CameraManage>();

	public UILabel des;

	public UITexture bgTexture;

	public UITexture pvpTexture;

	public GameObject pvpLoading;

	public UISprite leftProcessUISprite;

	public UISprite rightProcessUISprite;

	public GameObject ProcessGa;

	public Transform topGameObject;

	public Transform bottomGameObject;

	public Transform effectCenter;

	public DieBall effect;

	public GameObject pvpLoadingText;

	public GameObject Door_Loading_Ga;

	public GameObject New_Loading_Ga;

	public bool NewLoading;

	public UISprite NewLoadingLine;

	public UILabel NewLoadingLine_Label;

	public UITexture Newloading_BG;

	public Texture[] NewLoading_BG_Texture;

	public UILabel[] NewLoading_MessageLabel;

	public UILabel NewLoading_Notice;

	private bool isPlayMusic;

	public bool isMove = true;

	public static event Action<SenceType> ChaneSence
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			Loading.ChaneSence = (Action<SenceType>)Delegate.Combine(Loading.ChaneSence, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			Loading.ChaneSence = (Action<SenceType>)Delegate.Remove(Loading.ChaneSence, value);
		}
	}

	public void OnDestroy()
	{
		Loading.ins = null;
	}

	public void Update()
	{
		ResmgrNative.Instance.Update(ref this.wwwProcess);
		if (this.isBeginLoad)
		{
			this.SetProcess(this.wwwProcess);
		}
		if (this.New_Loading_Ga.gameObject.activeSelf && this.async != null)
		{
			if (this.NewLoadingLine)
			{
				this.NewLoadingLine.fillAmount = this.async.progress;
			}
			if (this.NewLoadingLine_Label)
			{
				this.NewLoadingLine_Label.text = (int)(this.async.progress * 100f) + "%";
			}
		}
	}

	public void SetNewLoading_Message(string title, string message1 = "", string message2 = "", string message3 = "", string message4 = "")
	{
		this.NewLoading_MessageLabel[0].text = title;
		if (message1 == string.Empty)
		{
			this.NewLoading_MessageLabel[1].transform.parent.gameObject.SetActive(false);
		}
		else
		{
			this.NewLoading_MessageLabel[1].transform.parent.gameObject.SetActive(true);
			this.NewLoading_MessageLabel[1].text = message1;
		}
		if (message2 == string.Empty)
		{
			this.NewLoading_MessageLabel[2].transform.parent.gameObject.SetActive(false);
		}
		else
		{
			this.NewLoading_MessageLabel[2].transform.parent.gameObject.SetActive(true);
			this.NewLoading_MessageLabel[2].text = message2;
		}
		if (message3 == string.Empty)
		{
			this.NewLoading_MessageLabel[3].transform.parent.gameObject.SetActive(false);
		}
		else
		{
			this.NewLoading_MessageLabel[3].transform.parent.gameObject.SetActive(true);
			this.NewLoading_MessageLabel[3].text = message3;
		}
		if (message4 == string.Empty)
		{
			this.NewLoading_MessageLabel[4].transform.parent.gameObject.SetActive(false);
		}
		else
		{
			this.NewLoading_MessageLabel[4].transform.parent.gameObject.SetActive(true);
			this.NewLoading_MessageLabel[4].text = message4;
		}
		if (UnitConst.GetInstance().LoadList.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, UnitConst.GetInstance().LoadList.Count);
			LoadDes loadDes = UnitConst.GetInstance().LoadList[index];
			this.NewLoading_Notice.text = LanguageManage.GetTextByKey(loadDes.description, "Halftalk");
		}
	}

	public override void Awake()
	{
		base.Awake();
		if (Loading.ins)
		{
			UnityEngine.Object.Destroy(this.ga);
			return;
		}
		Loading.ins = this;
		this.des.text = string.Empty;
		GameTools.DontDestroyOnLoad(base.gameObject);
	}

	private void OnDisable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
	}

	private void Start()
	{
		this.isPlayMusic = true;
	}

	private void OnEnable()
	{
		if (this.isPlayMusic)
		{
			AudioManage.inst.PlayAuido("gohome", false);
		}
		UnityEngine.Debug.Log("===UIManager.curState========" + UIManager.curState);
		if (UIManager.curState == SenceState.Spy || UIManager.curState == SenceState.Attacking)
		{
			this.NewLoading = true;
			this.NewLoadingLine.fillAmount = 0f;
			this.NewLoadingLine_Label.text = "0%";
		}
		else
		{
			this.NewLoading = false;
		}
		if (this.NewLoading)
		{
			this.des.gameObject.SetActive(false);
			int num = (int)UnityEngine.Random.Range(0f, 2.9f);
			this.Newloading_BG.mainTexture = this.NewLoading_BG_Texture[num];
			switch (num)
			{
			case 0:
				this.SetNewLoading_Message("基洛夫空艇", "攻击：爆炸伤害", "对建筑伤害：120%", "对装甲伤害：85%", string.Empty);
				break;
			case 1:
				this.SetNewLoading_Message("谭雅", "攻击：子弹伤害", "对建筑伤害：120%", "对装甲伤害：100%", "特种兵技能：定向爆破");
				break;
			case 2:
				this.SetNewLoading_Message("天启坦克", "攻击：炮弹伤害", "对建筑伤害：100%", "对建筑伤害：100%", string.Empty);
				break;
			}
			this.Door_Loading_Ga.gameObject.SetActive(false);
			this.New_Loading_Ga.gameObject.SetActive(true);
		}
		else
		{
			this.des.gameObject.SetActive(true);
			this.Door_Loading_Ga.gameObject.SetActive(true);
			this.New_Loading_Ga.gameObject.SetActive(false);
		}
		this.topGameObject.DOLocalMoveY(193.23f, 0.8f, false).OnComplete(delegate
		{
			this.ProcessGa.SetActive(true);
		});
		this.bottomGameObject.DOLocalMoveY(-145f, 0.8f, false).OnComplete(delegate
		{
			this.pvpLoading.SetActive(false);
			this.pvpLoadingText.SetActive(false);
			this.InfoTip();
			if (HUDTextTool.inst.isLoading)
			{
				base.StartCoroutine_Auto(this.loadScene());
			}
		});
		AudioManage.inst.PauseAudioBackground();
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		if (UIManager.curState == SenceState.Spy)
		{
			NewbieGuidePanel.isZhanyi = true;
		}
		else
		{
			NewbieGuidePanel.isZhanyi = false;
		}
		this.DesIE();
	}

	private void DesIE()
	{
		if (Loading.senceType == SenceType.Island && SenceManager.inst && SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
		{
			if (SenceInfo.battleResource == SenceInfo.BattleResource.NormalBattleFight)
			{
				LegionMapManager._inst.Init(LegionMapManager.BattleType.普通副本);
				LegionMapManager._inst.OpenPlayBattleBySelf();
			}
			else if (SenceInfo.battleResource == SenceInfo.BattleResource.LegionBattleFight)
			{
				LegionMapManager._inst.Init(LegionMapManager.BattleType.军团副本);
				LegionMapManager._inst.OpenPlayBattleBySelf();
			}
		}
		if (this.NewLoading)
		{
			this.New_Loading_Ga.gameObject.SetActive(false);
		}
		this.pvpLoading.SetActive(false);
		this.pvpLoadingText.SetActive(false);
		this.bgTexture.mainTexture = null;
		this.ProcessGa.SetActive(false);
		this.leftProcessUISprite.transform.localPosition = new Vector3((float)this.leftProcessUISprite.width * -0.5f, 20f, 0f);
		this.rightProcessUISprite.transform.localPosition = new Vector3((float)this.leftProcessUISprite.width * 0.5f, 20f, 0f);
		if (this.isPlayMusic)
		{
			AudioManage.inst.PlayAuido("gohome", false);
		}
		this.topGameObject.DOLocalMoveY(800f, 0.8f, false).OnComplete(delegate
		{
			this.ga.SetActive(false);
			if (Loading.senceType == SenceType.WorldMap && T_WMap.inst)
			{
				T_WMap.inst.StartOpenNewIsLand();
			}
		});
		this.bottomGameObject.DOLocalMoveY(-700f, 0.8f, false);
	}

	public void InfoTip()
	{
		if (UnitConst.GetInstance().LoadList.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, UnitConst.GetInstance().LoadList.Count);
			LoadDes loadDes = UnitConst.GetInstance().LoadList[index];
			this.des.text = LanguageManage.GetTextByKey(loadDes.description, "Halftalk");
		}
	}

	[DebuggerHidden]
	public IEnumerator loadScene()
	{
		Loading.<loadScene>c__Iterator19 <loadScene>c__Iterator = new Loading.<loadScene>c__Iterator19();
		<loadScene>c__Iterator.<>f__this = this;
		return <loadScene>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator waitForSecondsPvp(float times)
	{
		Loading.<waitForSecondsPvp>c__Iterator1A <waitForSecondsPvp>c__Iterator1A = new Loading.<waitForSecondsPvp>c__Iterator1A();
		<waitForSecondsPvp>c__Iterator1A.times = times;
		<waitForSecondsPvp>c__Iterator1A.<$>times = times;
		return <waitForSecondsPvp>c__Iterator1A;
	}

	public static void ChangeSence()
	{
		for (int i = 0; i < Loading.changeSence.Count; i++)
		{
			Loading.changeSence[i].ChangeSence(Loading.senceType);
		}
		if (Loading.ChaneSence != null)
		{
			Loading.ChaneSence(Loading.senceType);
		}
	}

	private void SetProcess(float JinDU)
	{
		this.leftProcessUISprite.transform.localPosition = new Vector3((float)this.leftProcessUISprite.width * 0.75f * JinDU - (float)this.leftProcessUISprite.width * 0.5f, 20f, 0f);
		this.rightProcessUISprite.transform.localPosition = new Vector3((float)this.leftProcessUISprite.width * 0.5f - (float)this.leftProcessUISprite.width * 0.75f * JinDU, 20f, 0f);
	}
}
