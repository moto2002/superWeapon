using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class FuncUIManager : IMonoBehaviour
{
	public List<GameObject> FuncUIList = new List<GameObject>();

	private GameObject uiRes;

	private GameObject clone;

	public Transform worldMapUI;

	public Transform islandMapUI;

	public static FuncUIManager inst;

	[HideInInspector]
	public GameObject coin;

	[HideInInspector]
	public GameObject oil;

	[HideInInspector]
	public GameObject steel;

	[HideInInspector]
	public GameObject rareEarth;

	private HomeUpdateOpenSetData homeUpdateOpenSetData;

	private List<int> upOpenArmys = new List<int>();

	private Stack<GameObject> FuncPanelList_ForQueueSetActive = new Stack<GameObject>();

	public T_CommandPanelManage T_CommandPanelManage
	{
		get
		{
			if (T_CommandPanelManage._instance == null)
			{
				this.OpenFuncUI("T_CommandPanel", SenceType.Island);
			}
			return T_CommandPanelManage._instance;
		}
	}

	public T_InfoPanelManage T_InfoPanelManage
	{
		get
		{
			this.OpenFuncUI("T_InfoPanel", SenceType.Island);
			return T_InfoPanelManage._instance;
		}
	}

	public MainUIPanelManage MainUIPanelManage
	{
		get
		{
			if (MainUIPanelManage._instance == null)
			{
				this.OpenFuncUI("MainUIPanel", SenceType.Island);
			}
			return MainUIPanelManage._instance;
		}
	}

	public ResourcePanelManage ResourcePanelManage
	{
		get
		{
			if (ResourcePanelManage.inst == null)
			{
				this.OpenFuncUI("ResourcePanel", SenceType.Other);
			}
			return ResourcePanelManage.inst;
		}
	}

	public BuildingStorePanelManage BuildingStorePanelManage
	{
		get
		{
			if (BuildingStorePanelManage._instance == null)
			{
				this.OpenFuncUI("BuildingStorePanel", SenceType.Island);
			}
			return BuildingStorePanelManage._instance;
		}
	}

	public CompoundPanel ItemMixPanelManage
	{
		get
		{
			if (CompoundPanel.ins == null)
			{
				this.OpenFuncUI("CompoundItemPanel", SenceType.Island);
			}
			return CompoundPanel.ins;
		}
	}

	public AdjutantPanel AdjutantPanel
	{
		get
		{
			if (AdjutantPanel.ins == null)
			{
				this.OpenFuncUI("AdjutantPanel", SenceType.Island);
				if (!this.OpenFuncUI("AdjutantPanel", SenceType.Island).GetComponent<AdjutantPanel>())
				{
					this.OpenFuncUI("AdjutantPanel", SenceType.Island).AddComponent<AdjutantPanel>();
					this.OpenFuncUI("AdjutantPanel", SenceType.Island).GetComponent<AdjutantPanel>().enabled = true;
				}
			}
			return AdjutantPanel.ins;
		}
	}

	public TaskGetawardNew TaskGetaward
	{
		get
		{
			if (TaskGetawardNew.init == null)
			{
				this.OpenFuncUI("TaskJiangliPanel", SenceType.Island);
			}
			return TaskGetawardNew.init;
		}
	}

	public void OnDestroy()
	{
		FuncUIManager.inst = null;
	}

	public override void Awake()
	{
		base.Awake();
		FuncUIManager.inst = this;
	}

	private void OnEnable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnGoHome += new UnityAction(this.SenceManager_OnGoHome);
			SenceManager.inst.OnGoHomeInitUI += new UnityAction(this.inst_OnGoHomeInitUI);
		}
		BuildingHandler.UnloadBuilding += new Action<HomeUpdateOpenSetData>(this.BuildingHandler_UnloadBuilding);
		BuildingHandler.UnloadArmys += new Action<List<int>>(this.BuildingHandler_UnloadArmys);
		Loading.ChaneSence += new Action<SenceType>(this.Loading_ChaneSence);
		CameraInitMove.inst.HomeOnMoved += new Action(this.InitUIPanel);
		this.InitUIPanel();
	}

	private void BuildingHandler_UnloadArmys(List<int> obj)
	{
		this.upOpenArmys.AddRange(obj);
		LogManage.Log("解封兵种" + this.upOpenArmys.Count);
		this.HomeUpOpenBuildingAndArmy();
	}

	private void BuildingHandler_UnloadBuilding(HomeUpdateOpenSetData obj)
	{
		this.homeUpdateOpenSetData = obj;
		this.HomeUpOpenBuildingAndArmy();
	}

	public void HomeUpOpenBuildingAndArmy()
	{
		if (this.homeUpdateOpenSetData != null && this.homeUpdateOpenSetData.buildingIds.Count > 0)
		{
			GameObject gameObject = this.OpenFuncUI("HomeUPOpenBuildingPanel", SenceType.Island);
			gameObject.GetComponent<HomeUpOpenBuilding>().SetInfo(this.homeUpdateOpenSetData);
		}
		else if (this.upOpenArmys.Count > 0)
		{
			int num = this.upOpenArmys[0];
			this.ArmyFuncHandler_ArmyInfoNew(num);
			this.upOpenArmys.Remove(num);
		}
	}

	private void inst_OnGoHomeInitUI()
	{
		if (NewbieGuidePanel.guideIdByServer >= GameSetting.MaxLuaProcess && (string.IsNullOrEmpty(HeroInfo.GetInstance().userName) || HeroInfo.GetInstance().userName.Equals(HeroInfo.GetInstance().userName_Default)))
		{
			FuncUIManager.inst.OpenFuncUI("RandomNamePanel", SenceType.Island);
		}
	}

	private void SenceManager_OnGoHome()
	{
		if (SenceInfo.CurBattle != null && NewbieGuidePanel.isNewGuidBattle)
		{
			SenceInfo.CurBattle = null;
		}
		if (HeroInfo.GetInstance().AllDisplayActives.Count > 0 && !HeroInfo.GetInstance().HavedDisplayActity && HeroInfo.GetInstance().PlayerCommondLv >= int.Parse(UnitConst.GetInstance().DesighConfigDic[96].value))
		{
			Dictionary<int, List<ActivityClass>> dictionary = new Dictionary<int, List<ActivityClass>>();
			foreach (int current in HeroInfo.GetInstance().AllDisplayActives)
			{
				if (HeroInfo.GetInstance().reChargeClass.ContainsKey(current))
				{
					dictionary.Add(current, HeroInfo.GetInstance().reChargeClass[current]);
				}
				if (HeroInfo.GetInstance().activityClass.ContainsKey(current))
				{
					dictionary.Add(current, HeroInfo.GetInstance().activityClass[current]);
				}
				if (HeroInfo.GetInstance().ShouChongChargeClass.ContainsKey(current))
				{
					dictionary.Add(current, HeroInfo.GetInstance().ShouChongChargeClass[current]);
				}
				if (HeroInfo.GetInstance().OneYuanGouChargeClass.ContainsKey(current))
				{
					dictionary.Add(current, HeroInfo.GetInstance().OneYuanGouChargeClass[current]);
				}
			}
			if (dictionary.Count > 0)
			{
				ChargeActityPanel.GetRegCharges = dictionary;
				FuncUIManager.inst.OpenFuncUI_NoQueue("ChargeActityPanel");
			}
			HeroInfo.GetInstance().HavedDisplayActity = true;
		}
	}

	private void CameraInitMove_HomeOnMoved()
	{
		this.InitUIPanel();
	}

	private void OnDisable()
	{
		if (SenceManager.inst)
		{
			SenceManager.inst.OnGoHome -= new UnityAction(this.SenceManager_OnGoHome);
			SenceManager.inst.OnGoHomeInitUI -= new UnityAction(this.inst_OnGoHomeInitUI);
		}
		BuildingHandler.UnloadBuilding -= new Action<HomeUpdateOpenSetData>(this.BuildingHandler_UnloadBuilding);
		BuildingHandler.UnloadArmys -= new Action<List<int>>(this.BuildingHandler_UnloadArmys);
		Loading.ChaneSence -= new Action<SenceType>(this.Loading_ChaneSence);
		if (CameraInitMove.inst)
		{
			CameraInitMove.inst.HomeOnMoved -= new Action(this.CameraInitMove_HomeOnMoved);
		}
		this.ClearIslandData();
	}

	private void ClearIslandData()
	{
		if (this.coin)
		{
			UnityEngine.Object.Destroy(this.coin);
		}
		if (this.oil)
		{
			UnityEngine.Object.Destroy(this.oil);
		}
		if (this.steel)
		{
			UnityEngine.Object.Destroy(this.steel);
		}
		if (this.rareEarth)
		{
			UnityEngine.Object.Destroy(this.rareEarth);
		}
	}

	private void Loading_ChaneSence(SenceType obj)
	{
		this.ClearAllUIPanel();
		FuncUIManager.ClearResourcePanel();
		this.ClearIslandData();
	}

	public void InitUIPanel()
	{
		if (UIManager.curState == SenceState.Home || UIManager.curState == SenceState.InBuild)
		{
			this.OpenFuncUI_NoQueue("T_CommandPanel").SetActive(false);
			this.OpenFuncUI_NoQueue("T_InfoPanel").SetActive(false);
			if (SenceInfo.battleResource == SenceInfo.BattleResource.LegionBattleFight || SenceInfo.battleResource == SenceInfo.BattleResource.NormalBattleFight)
			{
				this.OpenFuncUI_NoQueue("MainUIPanel").SetActive(false);
				this.OpenFuncUI_NoQueue("ResourcePanel", SenceType.Other).SetActive(false);
			}
			else
			{
				this.OpenFuncUI_NoQueue("MainUIPanel");
				this.OpenFuncUI_NoQueue("ResourcePanel", SenceType.Other);
			}
			MainUIPanelManage._instance.PlayMedalCollect();
			base.StartCoroutine(this.CreateIslandCollect());
		}
		else if (UIManager.curState == SenceState.Visit)
		{
			this.OpenFuncUI_NoQueue("T_CommandPanel");
		}
		else if (UIManager.curState == SenceState.WatchVideo)
		{
			this.OpenFuncUI_NoQueue("WatchNotePanel");
		}
	}

	[DebuggerHidden]
	private IEnumerator CreateIslandCollect()
	{
		return new FuncUIManager.<CreateIslandCollect>c__Iterator69();
	}

	public void ArmyFuncHandler_ArmyInfoNew(int id)
	{
		GameObject gameObject = this.OpenFuncUI("ArmyOpenPanel", SenceType.Island);
		NewarmyInfo component = gameObject.GetComponent<NewarmyInfo>();
		component.armyname.text = LanguageManage.GetTextByKey(UnitConst.GetInstance().soldierConst[id].name, "Army");
		component.armyIcon.spriteName = UnitConst.GetInstance().soldierConst[id].icon;
		component.itemintro.text = UnitConst.GetInstance().soldierConst[id].description;
		component.armyID = id;
		for (int i = 0; i < UnitConst.GetInstance().soldierConst[id].lifeStar; i++)
		{
			component.lifeSp[i].GetComponent<UISprite>().spriteName = "亮星";
		}
		for (int j = 0; j < UnitConst.GetInstance().soldierConst[id].speedStar; j++)
		{
			component.SpeedSp[j].GetComponent<UISprite>().spriteName = "亮星";
		}
		for (int k = 0; k < UnitConst.GetInstance().soldierConst[id].shootFarStar; k++)
		{
			component.AttakeSp[k].GetComponent<UISprite>().spriteName = "亮星";
		}
		for (int l = 0; l < UnitConst.GetInstance().soldierConst[id].defendStar; l++)
		{
			component.defenseSp[l].GetComponent<UISprite>().spriteName = "亮星";
		}
		for (int m = 0; m < UnitConst.GetInstance().soldierConst[id].shootFarStar; m++)
		{
			component.SpeedAttakeSp[m].GetComponent<UISprite>().spriteName = "亮星";
		}
		string text = UnitConst.GetInstance().GethintText(2, -1, 1, 1, 0, 0).Replace("<code>", LanguageManage.GetTextByKey(UnitConst.GetInstance().buildingConst[id].name, "build"));
	}

	public GameObject OpenFuncUI(string uiName, SenceType openSenceType = SenceType.Island)
	{
		this.FuncUIList.RemoveAll((GameObject a) => a == null);
		GameObject gameObject = null;
		for (int i = 0; i < this.FuncUIList.Count; i++)
		{
			if (this.FuncUIList[i] != null)
			{
				if (this.FuncUIList[i].name == uiName)
				{
					gameObject = this.FuncUIList[i];
				}
				else if (this.FuncUIList[i].activeSelf && !this.FuncUIList[i].name.Equals("ResourcePanel") && !this.FuncUIList[i].name.Equals("T_CommandPanel"))
				{
					this.FuncUIList[i].SetActive(false);
					LogManage.LogError(string.Format("池中面板数目:{1}   ------   {0}入池  ", this.FuncUIList[i].name, this.FuncPanelList_ForQueueSetActive.Count));
					this.FuncPanelList_ForQueueSetActive.Push(this.FuncUIList[i]);
				}
			}
		}
		if (gameObject)
		{
			NGUITools.SetActive(gameObject, true);
			return gameObject;
		}
		return this.AddNewFuncUI(uiName, openSenceType);
	}

	public void ClearFuncPanelList_ForQueue()
	{
		this.FuncPanelList_ForQueueSetActive.Clear();
	}

	public GameObject OpenFuncUI_NoQueue(string uiName, SenceType senceType)
	{
		this.FuncUIList.RemoveAll((GameObject a) => a == null);
		GameObject gameObject = null;
		for (int i = 0; i < this.FuncUIList.Count; i++)
		{
			if (this.FuncUIList[i] != null && this.FuncUIList[i].name == uiName)
			{
				gameObject = this.FuncUIList[i];
			}
		}
		if (gameObject)
		{
			NGUITools.SetActive(gameObject, true);
			return gameObject;
		}
		return this.AddNewFuncUI(uiName, senceType);
	}

	public GameObject OpenFuncUI_NoQueue(string uiName)
	{
		this.FuncUIList.RemoveAll((GameObject a) => a == null);
		GameObject gameObject = null;
		for (int i = 0; i < this.FuncUIList.Count; i++)
		{
			if (this.FuncUIList[i] != null && this.FuncUIList[i].name == uiName)
			{
				gameObject = this.FuncUIList[i];
			}
		}
		if (gameObject)
		{
			NGUITools.SetActive(gameObject, true);
			return gameObject;
		}
		return this.AddNewFuncUI(uiName, Loading.senceType);
	}

	public void HideFuncUI(string uiName)
	{
		for (int i = 0; i < this.FuncUIList.Count; i++)
		{
			if (this.FuncUIList[i] != null && this.FuncUIList[i].name == uiName)
			{
				NGUITools.SetActive(this.FuncUIList[i], false);
			}
		}
		if (this.FuncPanelList_ForQueueSetActive.Count > 0)
		{
			GameObject gameObject = this.FuncPanelList_ForQueueSetActive.Pop();
			LogManage.LogError(string.Format("池中面板数目:{1}   ------    {0}出池", gameObject.name, this.FuncPanelList_ForQueueSetActive.Count));
			NGUITools.SetActive(gameObject, true);
		}
	}

	public void DestoryFuncUI(string uiName)
	{
		for (int i = this.FuncUIList.Count - 1; i >= 0; i--)
		{
			if (this.FuncUIList[i] != null && this.FuncUIList[i].name == uiName)
			{
				GameObject gameObject = this.FuncUIList[i];
				this.FuncUIList.Remove(gameObject);
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		if (this.FuncPanelList_ForQueueSetActive.Count > 0)
		{
			GameObject gameObject2 = this.FuncPanelList_ForQueueSetActive.Pop();
			LogManage.LogError(string.Format("池中面板数目:{1}   ------   {0}出池", gameObject2.name, this.FuncPanelList_ForQueueSetActive.Count));
			NGUITools.SetActive(gameObject2, true);
		}
	}

	private GameObject AddNewFuncUI(string uiName, SenceType sence)
	{
		this.uiRes = (GameObject)Resources.Load(ResManager.FuncUI_Path + uiName);
		this.clone = (UnityEngine.Object.Instantiate(this.uiRes) as GameObject);
		if (sence != SenceType.Island)
		{
			if (sence != SenceType.WorldMap)
			{
				this.clone.transform.parent = this.tr;
			}
			else
			{
				this.clone.transform.parent = this.worldMapUI;
			}
		}
		else
		{
			this.clone.transform.parent = this.islandMapUI;
		}
		this.clone.transform.localPosition = Vector3.zero;
		this.clone.transform.localScale = Vector3.one;
		this.clone.name = uiName;
		this.FuncUIList.Add(this.clone);
		return this.clone;
	}

	public void ClearAllUIPanel()
	{
		if (this.FuncUIList.Count > 0)
		{
			for (int i = this.FuncUIList.Count - 1; i >= 0; i--)
			{
				if (this.FuncUIList[i] != null)
				{
					GameObject gameObject = this.FuncUIList[i];
					this.FuncUIList.Remove(gameObject);
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
		this.FuncPanelList_ForQueueSetActive.Clear();
	}

	public static void ClearResourcePanel()
	{
		if (ResourcePanelManage.inst)
		{
			ResourcePanelManage.inst.ClearChildren();
		}
	}
}
