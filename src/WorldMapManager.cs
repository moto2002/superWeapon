using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
	public static WorldMapManager inst;

	public NotarizeWindowManage notarize;

	public TipsManager tipsManager;

	public UILabel playerName;

	public UILabel playerLV;

	public UILabel playerMedal;

	public UISprite playerExp;

	public UISprite playerElectricity;

	public GameObject isElectrictyRay;

	public UISprite vip;

	private ResTips restips;

	public GameObject gotoPlayer;

	public GameObject nBattlPanel;

	public GameObject GuidPanel;

	public UILabel island_OpenCount;

	public UISprite island_OpenProcess;

	public GameObject blueLight;

	public GameObject redLight;

	public UILabel ResourceGet_Title;

	public UILabel ResourceGet_Coin_Label;

	public UILabel ResourceGet_Oil_Label;

	public UILabel ResourceGet_Steel_Label;

	public UILabel ResourceGet_RareEarth_Label;

	public void OnDestroy()
	{
		WorldMapManager.inst = null;
	}

	private void Awake()
	{
		WorldMapManager.inst = this;
		this.isElectrictyRay.SetActive(false);
		this.nBattlPanel.SetActive(false);
		this.gotoPlayer = base.transform.FindChild("GotoPlayer/Button").gameObject;
		EventManager.Instance.AddEvent(EventManager.EventType.WorldMap_BackHome, new EventManager.VoidDelegate(this.BackHome));
		EventManager.Instance.AddEvent(EventManager.EventType.MainPanel_OpenTopTen, new EventManager.VoidDelegate(this.OpenTopTen));
	}

	private void OpenTopTen(GameObject go)
	{
		if (TopTenPanelManage.rank.Count <= 0)
		{
			TopTenHandler.CG_TopTenListStart(1);
		}
		else
		{
			FuncUIManager.inst.OpenFuncUI("TopTenPanel", Loading.senceType);
		}
	}

	private void BackHome(GameObject ga)
	{
		if (Loading.ins.ga.activeSelf)
		{
			return;
		}
		UIManager.curState = SenceState.Home;
		this.restips = HUDTextTool.inst.restip;
		this.restips.gameObject.SetActive(false);
		this.ClearPanelUI();
		this.GetIslandData();
	}

	private void ClearPanelUI()
	{
		for (int i = 0; i < this.tipsManager.tipses.Length; i++)
		{
			this.tipsManager.tipses[i].gameObject.SetActive(false);
		}
	}

	public void OnEnable()
	{
		LogManage.Log("testOne");
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		this.RefreshHeroInfo();
		this.RefreshIslandOpenInfo();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10003 || opcodeCMD == 10002)
		{
			this.RefreshHeroInfo();
		}
		if (opcodeCMD == 10012)
		{
			this.RefreshIslandOpenInfo();
		}
	}

	public void RefreshIslandOpenInfo()
	{
		if (T_WMap.inst)
		{
			int num = T_WMap.inst.islandList.Count((KeyValuePair<int, T_Island> a) => a.Value.IsOpen && a.Value.OwnerType == OwnerType.user);
			if (num > 0)
			{
				num--;
			}
			this.island_OpenProcess.fillAmount = (float)num * 1f / (float)T_WMap.inst.islandList.Count;
			this.island_OpenCount.text = string.Format("{0}", string.Concat(new object[]
			{
				LanguageManage.GetTextByKey("占领度", "others"),
				":",
				(int)((float)num * 100f / (float)T_WMap.inst.islandList.Count),
				"%"
			}));
		}
	}

	public void RefreshIslandOpenInfo(int num)
	{
		try
		{
			if (T_WMap.inst)
			{
				int num2 = T_WMap.inst.islandList.Count((KeyValuePair<int, T_Island> a) => a.Value.IsOpen && a.Value.OwnerType == OwnerType.user);
				if (num2 > 0)
				{
					num2--;
				}
				float process = (float)num2 * 1f / (float)T_WMap.inst.islandList.Count;
				TweenFillAmount tweenFillAmount = UITweener.Begin<TweenFillAmount>(this.island_OpenProcess.gameObject, 3f);
				tweenFillAmount.SetStartToCurrentValue();
				tweenFillAmount.to = process;
				tweenFillAmount.PlayForward();
				tweenFillAmount.SetOnFinished(new EventDelegate(delegate
				{
					this.redLight.SetActive(false);
					this.blueLight.SetActive(false);
					this.island_OpenCount.text = string.Format("{0}", string.Concat(new object[]
					{
						LanguageManage.GetTextByKey("占领度", "others"),
						":",
						(int)(process * 100f),
						"%"
					}));
				}));
				if (this.island_OpenProcess.fillAmount <= process)
				{
					this.blueLight.SetActive(true);
				}
				else
				{
					this.redLight.SetActive(true);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Fly end ~~~~~~~~~~::::  " + ex.ToString());
		}
	}

	private void OnDisable()
	{
		if (ClientMgr.GetNet() != null && ClientMgr.GetNet().NetDataHandler != null)
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	public void BtnEvent(WorldMapBtnType type)
	{
		if (type != WorldMapBtnType.backHome)
		{
			if (type != WorldMapBtnType.PlayerInfo)
			{
			}
		}
	}

	private void GetIslandData()
	{
		SenceHandler.CG_GetMapData(HeroInfo.GetInstance().homeInWMapIdx, 1, 0, null);
	}

	public void RefreshHeroInfo()
	{
		this.ResourceGet_Title.text = "当前资源点总产出";
		if (HeroInfo.GetInstance().islandResData.IslandResNum.ContainsKey(ResType.金币))
		{
			int num = HeroInfo.GetInstance().islandResData.IslandResNum[ResType.金币];
		}
		int count = HeroInfo.GetInstance().islandResData.OilIslandes.Count;
		int count2 = HeroInfo.GetInstance().islandResData.SteelIslandes.Count;
		int count3 = HeroInfo.GetInstance().islandResData.RareEarthIslandes.Count;
	}
}
