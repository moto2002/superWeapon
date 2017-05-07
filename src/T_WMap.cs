using DicForUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class T_WMap : MonoBehaviour
{
	public static T_WMap inst;

	public Transform mapPic;

	public int mapSize = 25;

	public string islandRes = "island";

	public string islandRes_NoOpen = "island_NoOpen";

	public GameObject islandObj;

	public GameObject islandObj_NoOpen;

	public Transform clownRoot;

	public Transform islandRoot;

	public Dictionary<int, T_Island> islandList = new Dictionary<int, T_Island>();

	public T_Island curIsland_Sel;

	public static Dictionary<int, int> newWapIndex = new Dictionary<int, int>();

	public GameObject uifont;

	private List<WMapConst> AllIslandConst = new List<WMapConst>();

	public List<GameObject> NBattleItemList = new List<GameObject>();

	public List<Battle> AllBattle = new List<Battle>();

	public void OnDestroy()
	{
		T_WMap.inst = null;
	}

	public void Start()
	{
		this.mapSize = 25;
		T_WMap.inst = this;
		this.islandObj = (GameObject)Resources.Load(ResManager.WMapRes_Path + this.islandRes);
		this.islandObj_NoOpen = (GameObject)Resources.Load(ResManager.WMapRes_Path + this.islandRes_NoOpen);
		this.InitMapData();
		base.StartCoroutine(this.AddData());
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10012 && Loading.senceType == SenceType.WorldMap)
		{
			this.StartOpenNewIsLand();
		}
	}

	private void InitMapData()
	{
		DicForU.GetValues<int, WMapConst>(HeroInfo.GetInstance().worldMapInfo.MapConst, this.AllIslandConst);
		LogManage.LogError("岛屿数目是:" + this.AllIslandConst.Count);
		for (int i = 0; i < this.AllIslandConst.Count; i++)
		{
			IslandType mapType = (IslandType)this.AllIslandConst[i].mapType;
			if (mapType != IslandType.sea)
			{
				this.CreatIsland_NoOpen(this.AllIslandConst[i].idx);
			}
		}
	}

	private void BuildingHandler_RadarBuildingUp(int playerRadarLv)
	{
		this.CreateBattleItem(playerRadarLv);
	}

	private void OnDisable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	public void OnEnable()
	{
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
	}

	[DebuggerHidden]
	private IEnumerator AddData()
	{
		T_WMap.<AddData>c__Iterator3E <AddData>c__Iterator3E = new T_WMap.<AddData>c__Iterator3E();
		<AddData>c__Iterator3E.<>f__this = this;
		return <AddData>c__Iterator3E;
	}

	private void CreatAllIslands()
	{
		using (Dictionary<int, PlayerWMapData>.Enumerator enumerator = HeroInfo.GetInstance().worldMapInfo.playerWMap.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Dictionary<int, int> arg_2E_0 = T_WMap.newWapIndex;
				KeyValuePair<int, PlayerWMapData> current = enumerator.Current;
				if (!arg_2E_0.ContainsKey(current.Key))
				{
					KeyValuePair<int, PlayerWMapData> current2 = enumerator.Current;
					this.CreatIsland(current2.Value);
				}
			}
		}
	}

	public void CreatIsland(PlayerWMapData data)
	{
		if (this.islandList.ContainsKey(data.idx))
		{
			UnityEngine.Object.Destroy(this.islandList[data.idx].gameObject);
			this.islandList.Remove(data.idx);
		}
		Vector3 coord = UnitConst.GetInstance().mapEntityList[data.idx].coord;
		GameObject gameObject = UnityEngine.Object.Instantiate(this.islandObj, coord, Quaternion.identity) as GameObject;
		gameObject.transform.parent = this.islandRoot;
		gameObject.transform.localPosition = coord;
		T_Island component = gameObject.GetComponent<T_Island>();
		component.SetInfo(data, this);
		if (!this.islandList.ContainsKey(component.mapIdx))
		{
			this.islandList.Add(component.mapIdx, component);
		}
		else
		{
			this.islandList[component.mapIdx] = component;
		}
	}

	public void CreatIsland_NoOpen(int index)
	{
		if (this.islandList.ContainsKey(index))
		{
			UnityEngine.Object.Destroy(this.islandList[index].gameObject);
			this.islandList.Remove(index);
		}
		Vector3 coord = UnitConst.GetInstance().mapEntityList[index].coord;
		GameObject gameObject = UnityEngine.Object.Instantiate(this.islandObj_NoOpen, coord, Quaternion.identity) as GameObject;
		gameObject.transform.parent = this.islandRoot;
		gameObject.transform.localPosition = coord;
		T_Island_NoOpen component = gameObject.GetComponent<T_Island_NoOpen>();
		component.SetInfo(index, this);
		if (!this.islandList.ContainsKey(component.mapIdx))
		{
			this.islandList.Add(component.mapIdx, component);
		}
		else
		{
			this.islandList[component.mapIdx] = component;
		}
	}

	public void BattleItem(Battle item)
	{
		if (item != null)
		{
			SenceInfo.CurBattle = item;
			NTollgateManage.inst.curBattle = item;
			NTollgateManage.inst.StartCoroutine(NTollgateManage.inst.ShowTollgateItem());
		}
	}

	public void CreateBattleItem(int playerRadarLv)
	{
		DicForU.GetValues<int, Battle>(UnitConst.GetInstance().BattleConst, this.AllBattle);
		for (int i = 0; i < this.AllBattle.Count; i++)
		{
			Battle battle = this.AllBattle[i];
			if (battle.radarLevel <= playerRadarLv && !this.islandList.ContainsKey(battle.id * -1))
			{
				this.CreateBattleIsland(battle);
			}
		}
	}

	private void CreateBattleIsland(Battle item)
	{
		if (this.islandList.ContainsKey(item.id * -1) || this.islandRoot == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(this.islandObj, new Vector3(item.coord[0], item.coord[1], item.coord[2]), Quaternion.identity) as GameObject;
		gameObject.transform.parent = this.islandRoot;
		gameObject.transform.localPosition = new Vector3(item.coord[0], item.coord[1], item.coord[2]);
		gameObject.name = item.id.ToString();
		T_Island component = gameObject.GetComponent<T_Island>();
		component.SetInfo(item, this);
		this.islandList.Add(item.id * -1, component);
	}

	public static IslandType IdxGetMapType(int idx)
	{
		if (HeroInfo.GetInstance().worldMapInfo.MapConst.ContainsKey(idx))
		{
			return (IslandType)HeroInfo.GetInstance().worldMapInfo.MapConst[idx].mapType;
		}
		return IslandType.sea;
	}

	public T_Island GetT_IslandByIndex(int index)
	{
		if (this.islandList.ContainsKey(index))
		{
			return this.islandList[index];
		}
		return null;
	}

	public void StartOpenNewIsLand()
	{
		LogManage.Log("StartOpenNewIsLand");
		if (ElliteBattleBoxManager._inst)
		{
			ElliteBattleBoxManager._inst.Init();
		}
		AudioManage.inst.PlayAudioBackground("home", true);
		base.StartCoroutine(this.OpenNewIsLand());
	}

	public void Fly(Vector3 pos)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Exp"), pos + new Vector3(0f, 0.1f, 0f), Quaternion.identity) as GameObject;
		Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("jindu", gameObject.transform);
		modelByBundleByName.tr.localScale = Vector3.one;
		gameObject.transform.parent = SenceManager.inst.bulletPool;
		ResFly component = gameObject.GetComponent<ResFly>();
		component.startCam = WMap_DragManager.inst.camer;
		component.callBack = new ResFly.voidDelegate(WorldMapManager.inst.RefreshIslandOpenInfo);
		GameTools.RemoveComponent<ResRotate>(component.gameObject);
		component.FlyTo2(WMap_DragManager.inst.moveTarget, Vector3.zero, 0);
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 22;
		}
	}

	[DebuggerHidden]
	public IEnumerator OpenNewIsLand()
	{
		T_WMap.<OpenNewIsLand>c__Iterator3F <OpenNewIsLand>c__Iterator3F = new T_WMap.<OpenNewIsLand>c__Iterator3F();
		<OpenNewIsLand>c__Iterator3F.<>f__this = this;
		return <OpenNewIsLand>c__Iterator3F;
	}

	private void OpenBattle()
	{
		WorldMapManager.inst.nBattlPanel.SetActive(true);
		NTollgateManage.inst.gameObject.SetActive(true);
		CameraZhedang.inst.uiInUseBox.SetActive(true);
	}

	private void HideBattle()
	{
		WorldMapManager.inst.nBattlPanel.SetActive(false);
		NTollgateManage.inst.gameObject.SetActive(false);
		CameraZhedang.inst.uiInUseBox.SetActive(false);
	}
}
