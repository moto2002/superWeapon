using DG.Tweening;
using SimpleFramework;
using SimpleFramework.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Init : MonoBehaviour
{
	public static Init inst;

	public UILabel tex;

	public UILabel processText;

	public UISprite process;

	public Transform processSprite;

	public UILabel bugtex;

	public UILabel ClientVersionText;

	public UILabel ResVersionText;

	public GameObject inProcessGa;

	public GameObject noProcessGa;

	public GameObject processGa;

	public GameObject startSprite;

	public GameObject closeSprit;

	public Transform downLog;

	public Transform topLog;

	public static int wwwVerion = 2;

	private bool InitClientDataEnd;

	private float lastfillMount;

	private float lastTaskCount = 7f;

	private float lastfillmount;

	public void OnDestroy()
	{
		Init.inst = null;
	}

	private void Awake()
	{
		LogManage.LogError("Init~~~~~~~~~~~~~~~~~~~~~~~~~~~~s");
		Init.inst = this;
		ResManager.outURL = Util.DataPath_WWW + ResManager.artFile;
		this.downLog.gameObject.SetActive(false);
		this.topLog.gameObject.SetActive(false);
	}

	private void Start()
	{
		if (PoolManage.Ins == null)
		{
			UnityEngine.Object.Instantiate(Resources.Load("PoolManage"), Vector3.zero, Quaternion.identity);
		}
		Application.runInBackground = true;
		this.ClientVersionText.text = GameSetting.Version;
		ResmgrNative.Instance.LoadFromStreamingAssets("IsEditor.txt", string.Empty, delegate(WWW ww, string aa)
		{
			string text = ww.text;
			GameSetting.isEditor = (1 == int.Parse(text.Trim()));
		});
		ResmgrNative.Instance.LoadFromStreamingAssets("Version.txt", string.Empty, delegate(WWW ww, string aa)
		{
			string text = ww.text;
			GameSetting.Version = text;
		});
	}

	public void InitBundle()
	{
		this.InitClientDataEnd = false;
		PoolManage.Ins.loadLoginEnd = false;
		PoolManage.Ins.RegisterLua();
		base.StartCoroutine(PoolManage.Ins.AddSenceManage());
		LogManage.LogError("开始加载  --- 当前时间：" + Time.time);
		this.GoToLogin();
		if (!PoolManage.Ins.loadTankEnd)
		{
			this.InitTankBundle();
		}
		if (!PoolManage.Ins.loadEffectEnd)
		{
			this.InitEffectBundle();
		}
		if (!PoolManage.Ins.loadAudioEnd)
		{
			this.InitAudioBundle();
		}
		if (!PoolManage.Ins.loadDataEnd)
		{
			this.InitDataBundle();
		}
		if (!PoolManage.Ins.loadTowerEnd)
		{
			this.InitTowerBundle();
		}
		if (!PoolManage.Ins.loadResEnd)
		{
			this.InitResBundle();
		}
	}

	private void GoToLogin()
	{
		if (PoolManage.Ins.loadDataEnd && !this.InitClientDataEnd)
		{
			LogManage.LogError("正在应用数据········· · ·");
			this.UpClientDate();
			this.ApplyClientData();
			this.InitClientDataEnd = true;
		}
		if (PoolManage.Ins.loadTankEnd && PoolManage.Ins.loadAudioEnd && PoolManage.Ins.loadEffectEnd && PoolManage.Ins.loadDataEnd)
		{
			if (!ResmgrNative.Instance.verLocal.groups.ContainsKey(ResManager.artFilePlatform))
			{
				foreach (KeyValuePair<string, LocalVersion.VerInfo> current in ResmgrNative.Instance.verLocal.groups)
				{
					LogManage.LogError(current);
				}
				LogManage.LogError("groups不包含：" + ResManager.artFilePlatform);
				return;
			}
			if (!ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles.ContainsKey("Login.map"))
			{
				LogManage.LogError("listfiles 不包含：Login.map");
				return;
			}
			LogManage.LogError("开始加载Login地图 --- 当前时间：" + Time.time);
			LoginInitUI.inst.uicam.clearFlags = CameraClearFlags.Depth;
			ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles["Login.map"].BeginLoadAssetBundle(delegate(AssetBundle bundle, string tag)
			{
				PoolManage.Ins.loadLoginEnd = true;
				ResmgrNative.Instance.taskState.Clear();
				base.StartCoroutine(this.Login(bundle));
			});
		}
	}

	[DebuggerHidden]
	private IEnumerator Login(AssetBundle bundle)
	{
		Init.<Login>c__Iterator47 <Login>c__Iterator = new Init.<Login>c__Iterator47();
		<Login>c__Iterator.bundle = bundle;
		<Login>c__Iterator.<$>bundle = bundle;
		<Login>c__Iterator.<>f__this = this;
		return <Login>c__Iterator;
	}

	private void InitTowerBundle()
	{
		if (!ResmgrNative.Instance.verLocal.groups.ContainsKey(ResManager.artFilePlatform))
		{
			LogManage.LogError("groups不包含：" + ResManager.artFilePlatform);
			return;
		}
		if (!ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles.ContainsKey("AllTowers.msg"))
		{
			LogManage.LogError("listfiles 不包含：AllTowers.msg");
			return;
		}
		LogManage.LogError("InitTowerBundle  --- 当前时间：" + Time.time);
		ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles["AllTowers.msg"].BeginLoadAssetBundle(delegate(AssetBundle bundle, string tag)
		{
			if (bundle != null)
			{
				LogManage.LogError("InitTowerBundle End--- 当前时间：" + Time.time);
				ResmgrNative.Instance.taskState.Clear();
				if (PoolManage.Ins)
				{
					try
					{
						PoolManage.Ins.ObjectInMirrorByBundle(bundle);
					}
					catch (Exception ex)
					{
						LogManage.LogError(ex.ToString());
					}
				}
				else
				{
					LogManage.LogError("PoolManage.Ins is null");
				}
				bundle.Unload(false);
				PoolManage.Ins.loadTowerEnd = true;
			}
		});
	}

	private void InitTankBundle()
	{
		if (!ResmgrNative.Instance.verLocal.groups.ContainsKey(ResManager.artFilePlatform))
		{
			LogManage.LogError("groups不包含：" + ResManager.artFilePlatform);
			return;
		}
		if (!ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles.ContainsKey("AllTanks.msg"))
		{
			LogManage.LogError("listfiles 不包含：AllTanks.msg");
			return;
		}
		LogManage.LogError("InitTankBundle  --- 当前时间：" + Time.time);
		ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles["AllTanks.msg"].BeginLoadAssetBundle(delegate(AssetBundle bundle, string tag)
		{
			if (bundle != null)
			{
				LogManage.LogError("InitTankBundle End--- 当前时间：" + Time.time);
				ResmgrNative.Instance.taskState.Clear();
				if (PoolManage.Ins)
				{
					try
					{
						PoolManage.Ins.ObjectInMirrorByBundle(bundle);
					}
					catch (Exception ex)
					{
						LogManage.LogError(ex.ToString());
					}
				}
				else
				{
					LogManage.LogError("PoolManage.Ins is null");
				}
				bundle.Unload(false);
				PoolManage.Ins.loadTankEnd = true;
				this.GoToLogin();
			}
		});
	}

	private void InitEffectBundle()
	{
		if (!ResmgrNative.Instance.verLocal.groups.ContainsKey(ResManager.artFilePlatform))
		{
			LogManage.LogError("groups不包含：" + ResManager.artFilePlatform);
			return;
		}
		if (!ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles.ContainsKey("FightEffect.zeg"))
		{
			LogManage.LogError("listfiles 不包含：FightEffect.zeg");
			return;
		}
		LogManage.LogError("InitEffectBundle  --- 当前时间：" + Time.time);
		ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles["FightEffect.zeg"].BeginLoadAssetBundle(delegate(AssetBundle bundle, string tag)
		{
			if (bundle != null)
			{
				LogManage.LogError("InitEffectBundle End--- 当前时间：" + Time.time);
				ResmgrNative.Instance.taskState.Clear();
				if (PoolManage.Ins)
				{
					try
					{
						PoolManage.Ins.ObjectInMirrorByBundle(bundle);
					}
					catch (Exception ex)
					{
						LogManage.LogError(ex.ToString());
					}
				}
				else
				{
					LogManage.LogError("PoolManage.Ins is null");
				}
				bundle.Unload(false);
				PoolManage.Ins.loadEffectEnd = true;
				this.GoToLogin();
			}
		});
	}

	private void InitResBundle()
	{
		if (!ResmgrNative.Instance.verLocal.groups.ContainsKey(ResManager.artFilePlatform))
		{
			LogManage.LogError("groups不包含：" + ResManager.artFilePlatform);
			return;
		}
		if (!ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles.ContainsKey("AllResStyle_0.msg"))
		{
			LogManage.LogError("listfiles 不包含：AllResStyle_0.msg");
			return;
		}
		LogManage.LogError("InitResBundle  --- 当前时间：" + Time.time);
		ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles["AllResStyle_0.msg"].BeginLoadAssetBundle(delegate(AssetBundle bundle, string tag)
		{
			if (bundle != null)
			{
				LogManage.LogError("InitResBundle End--- 当前时间：" + Time.time);
				ResmgrNative.Instance.taskState.Clear();
				if (PoolManage.Ins)
				{
					try
					{
						PoolManage.Ins.ObjectInMirrorByBundle(bundle);
					}
					catch (Exception ex)
					{
						LogManage.LogError(ex.ToString());
					}
				}
				else
				{
					LogManage.LogError("PoolManage.Ins is null");
				}
				bundle.Unload(false);
				PoolManage.Ins.loadResEnd = true;
			}
		});
	}

	private void InitAudioBundle()
	{
		if (!ResmgrNative.Instance.verLocal.groups.ContainsKey(ResManager.artFilePlatform))
		{
			LogManage.LogError("groups不包含：" + ResManager.artFilePlatform);
			return;
		}
		if (!ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles.ContainsKey("AllAudio.msg"))
		{
			LogManage.LogError("listfiles 不包含：AllAudio.msg");
			return;
		}
		LogManage.LogError("AllAudio  --- 当前时间：" + Time.time);
		ResmgrNative.Instance.verLocal.groups[ResManager.artFilePlatform].listfiles["AllAudio.msg"].BeginLoadAssetBundle(delegate(AssetBundle bundle, string tag)
		{
			if (bundle != null)
			{
				ResmgrNative.Instance.taskState.Clear();
				LogManage.LogError("AllAudio End --- 当前时间：" + Time.time);
				UnityEngine.Object[] array = bundle.LoadAll(typeof(AudioClip));
				for (int i = 0; i < array.Length; i++)
				{
					UnityEngine.Object @object = array[i];
					AudioManage.inst.audioList.Add(@object as AudioClip);
				}
				bundle.Unload(false);
				PoolManage.Ins.loadAudioEnd = true;
				this.GoToLogin();
			}
		});
	}

	private void InitDataBundle()
	{
		UnitXML.GetInstance().InitDataByClient();
		PoolManage.Ins.loadDataEnd = true;
		this.GoToLogin();
	}

	private void LoginSenceInit()
	{
		if (this.processGa)
		{
			this.processGa.SetActive(false);
		}
		AudioManage.inst.PlayAudioBackground("login", true);
		if (LoginPanelManager._instance == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("UI/LoginPanel")) as GameObject;
			gameObject.transform.parent = base.transform.parent;
			gameObject.transform.localScale = Vector3.one;
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("StartGameCamera")) as GameObject;
		gameObject2.transform.localPosition = new Vector3(187f, 14f, 107f);
		gameObject2.transform.localRotation = Quaternion.Euler(new Vector3(26f, -48f, 0f));
		this.topLog.gameObject.SetActive(true);
		this.downLog.gameObject.SetActive(true);
		DieBall dieBall = PoolManage.Ins.CreatEffect("kaishi_zhedang", Vector3.zero, Quaternion.identity, Init.inst.transform);
		dieBall.tr.localPosition = new Vector3(0f, 14.7f, 0f);
		dieBall.tr.localScale = Vector3.one;
		GameTools.GetCompentIfNoAddOne<RenderQueueEdit>(dieBall.ga);
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(Init.inst.closeSprit, 0.3f);
		tweenAlpha.delay = 1.5f;
		tweenAlpha.from = 1f;
		tweenAlpha.to = 0f;
		tweenAlpha.PlayForward();
		TweenAlpha tweenAlpha2 = UITweener.Begin<TweenAlpha>(Init.inst.startSprite, 0.3f);
		tweenAlpha2.delay = 1.5f;
		tweenAlpha2.from = 0f;
		tweenAlpha2.to = 1f;
		tweenAlpha2.PlayForward();
		Init.inst.transform.DOLocalMoveZ(0f, 2.6f, false).OnComplete(delegate
		{
			this.topLog.DOLocalMoveY(483f, 0.8f, false).OnComplete(delegate
			{
				this.topLog.DOShakePosition(0.5f, new Vector3(0f, 30f, 0f), 10, 90f, false);
			});
			this.downLog.DOLocalMoveY(-425f, 0.8f, false).OnComplete(delegate
			{
				this.downLog.DOShakePosition(0.5f, new Vector3(0f, 30f, 0f), 10, 90f, false);
				if (LoginPanelManager._instance)
				{
					LoginPanelManager._instance.GetServer();
					LoginPanelManager._instance.transform.DOLocalMoveY(5f, 0.1f, false).OnComplete(delegate
					{
						if (!HDSDKInit.isLoginEnd)
						{
							LoginEnterGame._instance.ChangeUserGa.SetActive(true);
						}
					});
				}
			});
		});
	}

	public void SetProcess(float fillMount, string textContent)
	{
		int num = 7;
		if (PoolManage.Ins.loadTankEnd)
		{
			num--;
		}
		if (PoolManage.Ins.loadEffectEnd)
		{
			num--;
		}
		if (PoolManage.Ins.loadAudioEnd)
		{
			num--;
		}
		if (PoolManage.Ins.loadDataEnd)
		{
			num--;
		}
		if (PoolManage.Ins.loadLoginEnd)
		{
			num--;
		}
		if (PoolManage.Ins.loadTowerEnd)
		{
			num--;
		}
		if (PoolManage.Ins.loadResEnd)
		{
			num--;
		}
		if ((float)num < this.lastTaskCount)
		{
			this.lastTaskCount = (float)num;
		}
		if (fillMount >= this.lastfillMount)
		{
			this.lastfillMount = fillMount;
		}
		else if (num < 7 && fillMount == 0f)
		{
			this.lastfillMount = fillMount;
			this.lastTaskCount -= 1f;
		}
		float num2 = this.lastfillMount * 0.2f + (7f - this.lastTaskCount) / 7f;
		if (this.lastfillmount < num2)
		{
			this.lastfillmount = num2;
		}
		if (this.lastfillmount > 1f)
		{
			this.lastfillmount = 1f;
		}
		Init.inst.tex.text = textContent;
		this.SetProcess(this.lastfillmount);
	}

	public void SetProcessForDown(float fillMount, string textContent)
	{
		if (fillMount > 0f)
		{
			if (this.noProcessGa.activeSelf)
			{
				this.noProcessGa.SetActive(false);
			}
			if (!this.inProcessGa.activeSelf)
			{
				this.inProcessGa.SetActive(true);
			}
			this.tex.text = textContent;
			this.processText.text = string.Format("{0}%", (int)(fillMount * 100f));
			this.SetProcess(fillMount);
		}
		else
		{
			if (!this.noProcessGa.activeSelf)
			{
				this.noProcessGa.SetActive(true);
			}
			if (this.inProcessGa.activeSelf)
			{
				this.inProcessGa.SetActive(false);
			}
		}
	}

	public void SetProcess(float jindu)
	{
		if (jindu > 0f)
		{
			if (this.noProcessGa.activeSelf)
			{
				this.noProcessGa.SetActive(false);
			}
			if (!this.inProcessGa.activeSelf)
			{
				this.inProcessGa.SetActive(true);
			}
			this.processText.text = string.Format("{0}%", (int)(jindu * 100f));
			this.process.fillAmount = jindu;
			this.processSprite.localPosition = new Vector3((float)(this.process.width - 10) * this.process.fillAmount, 0f, 0f);
		}
		else
		{
			if (!this.noProcessGa.activeSelf)
			{
				this.noProcessGa.SetActive(true);
			}
			if (this.inProcessGa.activeSelf)
			{
				this.inProcessGa.SetActive(false);
			}
		}
	}

	public void UpClientDate()
	{
		try
		{
			SenceInfo.BuildTerrainList();
			LogManage.Log("BuildTerrainList");
			BuildingQueue.LoadBuildingQueuePriceXML();
			LogManage.Log("LoadBuildingQueuePriceXML");
			ErrorServer.ReadErrorServerXML();
			LogManage.Log("ReadErrorServerXML");
			UniteRD.GetInstance().NewReadArmyConstXML();
			LogManage.Log("NewReadArmyConstXML");
			UniteRD.GetInstance().NewReadArmyLvXML();
			LogManage.Log("NewReadArmyLvXML");
			UniteRD.GetInstance().NewReadBuildingConstXML();
			LogManage.Log("NewReadBuildingConstXML");
			UniteRD.GetInstance().NewReadBuildingLvXML();
			LogManage.Log("NewReadBuildingLvXML");
			TowerUnit.NewReadTowerStrengthXml();
			LogManage.Log("NewReadTowerStrengthXml");
			TowerUnit.NewReadBuildingUpGradeXml();
			LogManage.Log("NewReadBuildingUpGradeXml");
			TowerUnit.NewReadTowerUpdateXml();
			LogManage.Log("NewReadTowerUpdateXml");
			UniteRD.GetInstance().NewReadUNArmyStarXML();
			LogManage.Log("NewReadUNArmyStarXML");
			UniteRD.GetInstance().ReadMilitaryRankXML();
			LogManage.Log(" ReadMilitaryRankXML");
			UniteRD.GetInstance().ReadSevenDayXML();
			LogManage.Log("ReadSevenDayXML");
			UniteRD.GetInstance().ReadGameStartXML();
			LogManage.Log("ReadGameStartXML");
			UniteRD.GetInstance().ReadLoadRewadXML();
			LogManage.Log("ReadLoadRewadXML");
			UniteRD.GetInstance().ReadMilitaryOpenSetXML();
			LogManage.Log("ReadMilitaryOpenSetXML");
			UniteRD.GetInstance().ReadTechnologyAndTechnologyUpsetXML();
			LogManage.Log("ReadTechnologyAndTechnologyUpsetXML");
			UniteRD.GetInstance().ReadItemAndItemMixSetXML();
			LogManage.Log("ReadItemAndItemMixSetXML");
			UniteRD.GetInstance().ReadPlayerUpSetXML();
			LogManage.Log("ReadPlayerUpSetXML");
			UniteRD.GetInstance().ReadBattleConstXML();
			LogManage.Log("ReadBattleConstXML");
			UniteRD.GetInstance().ReadNpc_Box_DropListXML();
			LogManage.Log("ReadNpc_Box_DropListXML");
			UniteRD.GetInstance().ReadTaskAndAchievementXML();
			LogManage.Log("ReadTaskAndAchievementXML");
			UniteRD.GetInstance().ReadResConvertXML();
			LogManage.Log("ReadResConvertXML");
			UniteRD.GetInstance().ReadResIslandOutputXML();
			LogManage.Log("ReadResIslandOutputXML");
			UniteRD.GetInstance().ReadArmsDealerXML();
			LogManage.Log("ReadArmsDealerXML");
			UniteRD.GetInstance().ReadResDesXML();
			LogManage.Log("ReadResDesXML");
			UniteRD.GetInstance().ReadAideXML();
			LogManage.Log("ReadAideXML");
			UniteRD.GetInstance().ReadBuffXML();
			LogManage.Log("ReadBuffXML");
			UniteRD.GetInstance().ReadEliteNpcBoxXML();
			LogManage.Log("ReadEliteNpcBoxXML");
			UniteRD.GetInstance().ReadDesignConfigXML();
			LogManage.Log("ReadDesignConfigXML");
			UniteRD.GetInstance().ReadGoldPurchaseXML();
			LogManage.Log("ReadGoldPurchaseXML");
			UniteRD.GetInstance().ReadVipUpSetXML();
			LogManage.Log("ReadVipUpSetXML");
			UniteRD.GetInstance().ReadTalkItemXML();
			LogManage.Log("ReadTalkItemXML");
			UniteRD.GetInstance().ReadActivityXML();
			LogManage.Log("ReadActivityXML");
			UniteRD.GetInstance().ReadSignXML();
			LogManage.Log("ReadSignXML");
			WMapRD.RDNewWMapXml(1);
			LogManage.Log("RDNewWMapXml");
			UniteRD.GetInstance().ReadShopXML();
			LogManage.Log("ReadShopXML");
			UniteRD.GetInstance().ReadBtnUpsetXML();
			LogManage.Log("ReadBtnUpsetXML");
			UniteRD.GetInstance().ReadMoneyToToken();
			LogManage.Log("ReadMoneyToToken");
			UniteRD.GetInstance().ReadNewGuildXml();
			LogManage.Log("ReadNewGuildXml");
			UniteRD.GetInstance().ReadNewGuildPersonXml();
			LogManage.Log("ReadNewGuildPersonXml");
			UniteRD.GetInstance().ReadRandomEventXml();
			LogManage.Log("ReadRandomEventXml");
			UniteRD.GetInstance().ReadNpcAttarkXml();
			LogManage.Log("ReadNpcAttarkXml");
			UniteRD.GetInstance().ReadEquipXml();
			LogManage.Log("ReadEquipXml");
			UniteRD.GetInstance().ReadTowerTankOrderListXml();
			LogManage.Log("ReadTowerTankOrderListXml");
			ArmyUnit.ReadArmyIconXML();
			LogManage.Log("ReadArmyIconXML");
			ArmyUnit.ReadArmyRightXML();
			LogManage.Log("ReadArmyRightXML");
			UniteRD.GetInstance().ReadSkillXml();
			LogManage.Log("ReadSkillXml");
			UniteRD.GetInstance().ReadCameraXml();
			LogManage.Log("ReadCameraXml");
			UniteRD.GetInstance().HintXML();
			LogManage.Log("HintXML");
			UniteRD.GetInstance().ReadElectricityXML();
			LogManage.Log("ReadElectricityXML");
			UniteRD.GetInstance().ReadLoadingXml();
			LogManage.Log("ReadLoadingXml");
			UniteRD.GetInstance().ReadMapEntity();
			LogManage.Log("ReadMapEntity");
			UniteRD.GetInstance().ReadRandomname();
			LogManage.Log("ReadRandomname");
			UniteRD.GetInstance().ReadNpcAttackBattle();
			LogManage.Log("ReadNpcAttackBattle");
			BattleFieldBox.LoadClientData();
			LogManage.Log("LoadClientData");
			UniteRD.GetInstance().ReadSoldierXml();
			LogManage.Log("ReadSoldierXml");
			UnityEngine.Debug.Log("=========>>>>>>>>>>>>  ReadDataEnd!!");
		}
		catch (Exception ex)
		{
			LogManage.LogError(ex.ToString());
		}
	}

	public void ApplyClientData()
	{
		try
		{
			LanguageManage.LoadLanguageCategory();
			LanguageManage.LoadLanguageContent(LanguageManage.LanguageCategory[User.GetLanguageSetting()]);
			if (UnitXML.GetInstance().GetXMLTextByName("GameStartLua") != null)
			{
				GameManager.Instance.GetLuaManage().DoString(UnitXML.GetInstance().GetXMLTextByName("GameStartLua"));
			}
			else
			{
				LogManage.LogError("GameStartLua is null");
			}
			if (UnitXML.GetInstance().GetXMLTextByName("StartFireLua") != null)
			{
				GameManager.Instance.GetLuaManage().DoString(UnitXML.GetInstance().GetXMLTextByName("StartFireLua"));
			}
			else
			{
				LogManage.LogError("StartFireLua is null");
			}
			if (UnitXML.GetInstance().GetXMLTextByName("NewBi") != null)
			{
				GameManager.Instance.GetLuaManage().DoString(UnitXML.GetInstance().GetXMLTextByName("NewBi"));
			}
			else
			{
				LogManage.LogError("NewBi is null");
			}
		}
		catch (Exception ex)
		{
			LogManage.LogError(ex.ToString());
		}
	}
}
