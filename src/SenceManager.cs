using DG.Tweening;
using msg;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class SenceManager : MonoBehaviour
{
	public enum ElectricityEnum
	{
		电力充沛 = 1,
		电力不足,
		严重不足,
		电力瘫痪
	}

	public SettlementType SettType;

	public static SenceManager inst;

	[HideInInspector]
	public int arrayX;

	[HideInInspector]
	public int arrayY;

	[HideInInspector]
	public int terrIdx;

	public FightingType fightType;

	private string commanderHome_TalkText;

	[SerializeField]
	private List<T_TankAbstract> tanks_Attack = new List<T_TankAbstract>();

	private List<T_CommanderHome> tanks_CommanderHome = new List<T_CommanderHome>();

	[SerializeField]
	private List<T_TankAbstract> tanks_Defend = new List<T_TankAbstract>();

	public Dictionary<int, SCExtraArmy> ExtraArmyList = new Dictionary<int, SCExtraArmy>();

	public Dictionary<int, SCExtraArmy> OtherIslandExtraArmyList = new Dictionary<int, SCExtraArmy>();

	public Dictionary<int, SCExtraArmy> PlayerExtraArmyList = new Dictionary<int, SCExtraArmy>();

	public Dictionary<int, ExtraArmyData> ExtraArmyDataList = new Dictionary<int, ExtraArmyData>();

	public Dictionary<int, T_Tank> ExtraAymyTankList = new Dictionary<int, T_Tank>();

	public Dictionary<int, int> Des_ExtraArmyDataList = new Dictionary<int, int>();

	private int id;

	public bool isFirstEnemy = true;

	public static bool IsHaveNoMovingBuilding = false;

	public Dictionary<int, T_Res> reses = new Dictionary<int, T_Res>();

	public static List<long> DestroyId = new List<long>();

	public static bool IsDestroy = false;

	public static long islandid = 0L;

	public List<T_Tower> towers = new List<T_Tower>();

	public List<T_Tower> buildNpc = new List<T_Tower>();

	private T_Tower mainBuilding;

	private int mainBuildingIndex;

	private int mainBuildingLv;

	public T_Tower tempTower;

	public T_Tower curTower;

	public T_Tower mover_Tower;

	public int RMBNeed_AddTempTower;

	public Transform shipPool;

	public Transform tankPool;

	public Transform towerPool;

	public Transform resPool;

	public Transform bulletPool;

	public T_Tower CurSelectTower;

	private T_TowerR towerR;

	public GameObject lineObj;

	public T_MotherShip m_ship;

	public Vector3 hidePos = new Vector3(-1000f, 0f, 1000f);

	public string test;

	public TowerGrid[] towerRows;

	public bool sameTower;

	public int coinDepot;

	public int oilDepot;

	public int steelDepot;

	public int rareEarthDepot;

	public GameObject fireRes;

	private long soldierId = 1L;

	private Transform tr;

	public float last = -1000f;

	private Body_Model plane;

	public SenceManager.ElectricityEnum mapElectricity;

	public List<SCOtherPlayerInfo> oldPlayerList = new List<SCOtherPlayerInfo>();

	public List<SCOtherPlayerInfo> newPlayerList = new List<SCOtherPlayerInfo>();

	private bool isBack;

	public Dictionary<int, ElliteBattleBox> ElliteBallteBoxes = new Dictionary<int, ElliteBattleBox>();

	public int ElliteBallteBoxMax;

	public bool ShowNotice;

	private List<BuildingNPC> TmpBuildingNPC = new List<BuildingNPC>();

	public bool IsCreateMapEnd;

	private bool areadly_CSSkillList;

	private bool areadly_CSSkillConfigList;

	private bool areadly_CSTechList;

	private bool isShow = true;

	public int sizeBeiShu = 1;

	public AstarPath astarPath;

	public Vector3 tmpBuildingPostion;

	private int wall_no;

	public T_Tower ChooseWallForEffect;

	public List<T_Tower> EffectWallList = new List<T_Tower>();

	public bool WallLineChoose;

	public T_Tower GetWallLately;

	public Transform WallParent;

	private float WallParent_Y;

	public float rotate_time;

	private Dictionary<int, GameObject> allResGaes = new Dictionary<int, GameObject>();

	public T_Tower FirstNewWall;

	public T_Tower SecondNewWall;

	public DateTime ArmyTokenCdTime;

	public string ArmyTokenCdTime_Text;

	private float time;

	private float planeTime;

	public float TowerTankMessage_CDTime;

	public float WallBuildEndAudio;

	public Tweener cc;

	public int EnemyLv;

	public bool TowerSelfDestruct;

	private GameObject qiZi;

	private float lastPersonSoundTime = -10f;

	public List<string> attackingName = new List<string>();

	public List<string> moveAudioName = new List<string>();

	private Vector3[] movePoses = new Vector3[60];

	public float rowSpan = 1.5f;

	private float colSpan = 2f;

	private List<float> radiuses;

	public float tankWidth = 1.5f;

	private List<List<Vector3>> attrackPoint;

	public static float GetVPosDistance = 0.7f;

	private List<Vector3> posUsedList = new List<Vector3>();

	private List<int> rowList = new List<int>();

	public int needOil_Build;

	public int needSteel_Build;

	public int needRareEarth_Build;

	public event UnityAction Tank_AttackAllDie
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.Tank_AttackAllDie = (UnityAction)Delegate.Combine(this.Tank_AttackAllDie, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.Tank_AttackAllDie = (UnityAction)Delegate.Remove(this.Tank_AttackAllDie, value);
		}
	}

	public event UnityAction OnCreateMapDataEnd
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnCreateMapDataEnd = (UnityAction)Delegate.Combine(this.OnCreateMapDataEnd, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnCreateMapDataEnd = (UnityAction)Delegate.Remove(this.OnCreateMapDataEnd, value);
		}
	}

	public event UnityAction OnGoHome
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnGoHome = (UnityAction)Delegate.Combine(this.OnGoHome, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnGoHome = (UnityAction)Delegate.Remove(this.OnGoHome, value);
		}
	}

	public event UnityAction OnGoHomeInitUI
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			this.OnGoHomeInitUI = (UnityAction)Delegate.Combine(this.OnGoHomeInitUI, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			this.OnGoHomeInitUI = (UnityAction)Delegate.Remove(this.OnGoHomeInitUI, value);
		}
	}

	public SettlementType settType
	{
		get
		{
			return this.SettType;
		}
		set
		{
			this.SettType = value;
		}
	}

	public string CommanderHome_TalkText
	{
		get
		{
			return this.commanderHome_TalkText;
		}
		set
		{
			this.commanderHome_TalkText = value;
		}
	}

	public List<T_TankAbstract> Tanks_Attack
	{
		get
		{
			int count = this.tanks_Attack.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (this.tanks_Attack[i] == null || !this.tanks_Attack[i].ga.activeSelf)
				{
					this.tanks_Attack.Remove(this.tanks_Attack[i]);
				}
			}
			return this.tanks_Attack;
		}
		set
		{
			this.tanks_Attack = value;
		}
	}

	public List<T_CommanderHome> Tanks_CommanderHome
	{
		get
		{
			int count = this.tanks_CommanderHome.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (this.tanks_CommanderHome[i] == null || !this.tanks_CommanderHome[i].ga.activeSelf)
				{
					this.tanks_CommanderHome.Remove(this.tanks_CommanderHome[i]);
				}
			}
			return this.tanks_CommanderHome;
		}
		set
		{
			this.tanks_CommanderHome = value;
		}
	}

	public List<T_TankAbstract> Tanks_Defend
	{
		get
		{
			int count = this.tanks_Defend.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (this.tanks_Defend[i] == null || !this.tanks_Defend[i].ga.activeSelf)
				{
					this.tanks_Defend.Remove(this.tanks_Defend[i]);
				}
			}
			return this.tanks_Defend;
		}
		set
		{
			this.tanks_Defend = value;
		}
	}

	public int MainBuildingIndex
	{
		get
		{
			return this.mainBuildingIndex;
		}
	}

	public int MainBuildingLv
	{
		get
		{
			return this.mainBuildingLv;
		}
	}

	public T_Tower MainBuilding
	{
		get
		{
			return this.mainBuilding;
		}
		set
		{
			this.mainBuilding = value;
			if (value)
			{
				this.mainBuildingIndex = value.index;
				this.mainBuildingLv = value.lv;
			}
		}
	}

	public long SoldierId
	{
		get
		{
			long result;
			this.soldierId = (result = this.soldierId) + 1L;
			return result;
		}
		set
		{
			this.soldierId = value;
		}
	}

	public SenceManager.ElectricityEnum MapElectricity
	{
		get
		{
			return this.mapElectricity;
		}
		set
		{
			this.mapElectricity = value;
			for (int i = 0; i < this.towers.Count; i++)
			{
				this.towers[i].MapElectricity = value;
				if (UnitConst.GetInstance().buildingConst[this.towers[i].index].electricityShow == 1 && UnitConst.GetInstance().buildingConst[this.towers[i].index].secType != 99 && this.towers[i].index != 90)
				{
					if (value == SenceManager.ElectricityEnum.电力充沛)
					{
						this.towers[i].SetColorBlack(false);
					}
					else
					{
						this.towers[i].SetColorBlack(true);
					}
				}
				if (UnitConst.GetInstance().buildingConst[this.towers[i].index].secType == 19)
				{
					this.towers[i].CreateDefensiveCover();
				}
			}
		}
	}

	public List<float> Radiuses
	{
		get
		{
			if (this.radiuses == null)
			{
				this.radiuses = (from p in UnitConst.GetInstance().soldierConst
				where p.maxRadius != 0f
				orderby p.maxRadius
				select p.maxRadius).Distinct<float>().ToList<float>();
				this.radiuses.Insert(0, 4f);
				this.radiuses.Insert(0, 2f);
				for (int i = 0; i < this.radiuses.Count; i++)
				{
					this.radiuses[i] = this.radiuses[i] - SenceManager.GetVPosDistance;
				}
			}
			return this.radiuses;
		}
	}

	public List<List<Vector3>> AttrackPoint
	{
		get
		{
			if (this.attrackPoint == null)
			{
				this.attrackPoint = new List<List<Vector3>>();
				for (int i = 0; i < this.Radiuses.Count; i++)
				{
					float num = this.Radiuses[i];
					int num2 = Mathf.FloorToInt(6.28318548f * num / this.tankWidth);
					List<Vector3> list = new List<Vector3>();
					for (int j = 0; j < num2; j++)
					{
						Vector3 item = new Vector3(num * Mathf.Cos((float)(2 * j) * 3.14159274f / (float)num2), 0f, num * Mathf.Sin((float)(2 * j) * 3.14159274f / (float)num2));
						list.Add(item);
					}
					this.attrackPoint.Add(list);
				}
			}
			return this.attrackPoint;
		}
	}

	public void OnDestroy()
	{
		SenceManager.inst = null;
	}

	public bool PlunderResources()
	{
		return (SenceInfo.curMap.baseRes.ContainsKey(ResType.金币) && SenceInfo.curMap.baseRes[ResType.金币] > 0) || (SenceInfo.curMap.baseRes.ContainsKey(ResType.石油) && SenceInfo.curMap.baseRes[ResType.石油] > 0) || (SenceInfo.curMap.baseRes.ContainsKey(ResType.钢铁) && SenceInfo.curMap.baseRes[ResType.钢铁] > 0) || (SenceInfo.curMap.baseRes.ContainsKey(ResType.稀矿) && SenceInfo.curMap.baseRes[ResType.稀矿] > 0);
	}

	public bool NoResSpace(int coin = 0, int oil = 0, int steel = 0, int earth = 0, bool ShowNotice = true)
	{
		if (coin > 0 && coin + HeroInfo.GetInstance().playerRes.resCoin > HeroInfo.GetInstance().playerRes.maxCoin)
		{
			if (ShowNotice)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金币", "others") + LanguageManage.GetTextByKey("存储空间不足", "ResIsland"), HUDTextTool.TextUITypeEnum.Num5);
			}
			return true;
		}
		if (oil > 0 && oil + HeroInfo.GetInstance().playerRes.resOil > HeroInfo.GetInstance().playerRes.maxOil)
		{
			if (ShowNotice)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("石油", "others") + LanguageManage.GetTextByKey("存储空间不足", "ResIsland"), HUDTextTool.TextUITypeEnum.Num5);
			}
			return true;
		}
		if (steel > 0 && steel + HeroInfo.GetInstance().playerRes.resSteel > HeroInfo.GetInstance().playerRes.maxSteel)
		{
			if (ShowNotice)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("钢铁", "others") + LanguageManage.GetTextByKey("存储空间不足", "ResIsland"), HUDTextTool.TextUITypeEnum.Num5);
			}
			return true;
		}
		if (earth > 0 && earth + HeroInfo.GetInstance().playerRes.resRareEarth > HeroInfo.GetInstance().playerRes.maxRareEarth)
		{
			if (ShowNotice)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("稀矿", "others") + LanguageManage.GetTextByKey("存储空间不足", "ResIsland"), HUDTextTool.TextUITypeEnum.Num5);
			}
			return true;
		}
		return false;
	}

	public void ShowExtraArmyInHome()
	{
		this.PlayerExtraArmyList = this.ExtraArmyList;
		this.id = 0;
		foreach (KeyValuePair<int, SCExtraArmy> current in this.ExtraArmyList)
		{
			for (int i = 0; i < current.Value.itemId2Level.Count; i++)
			{
				int iD = (int)current.Value.id;
				int index = (int)current.Value.itemId2Level[i].key;
				int level = (int)current.Value.itemId2Level[i].value;
				int num = (int)current.Value.itemId2Num[i].value;
				for (int j = 0; j < num; j++)
				{
					this.id++;
					ExtraArmyData extraArmyData = new ExtraArmyData();
					extraArmyData.ID = iD;
					extraArmyData.id = this.id;
					extraArmyData.index = index;
					extraArmyData.level = level;
					extraArmyData.life_time = current.Value.espireTime;
					if (!this.ExtraArmyDataList.ContainsKey(extraArmyData.id))
					{
						this.ExtraArmyDataList.Add(extraArmyData.id, extraArmyData);
					}
					if (!this.ExtraAymyTankList.ContainsKey(this.id))
					{
						int num2 = this.id;
						Vector3 position = T_CommanderRoad.inst.T_CommanderRoad_tr[num2].transform.position;
						base.StartCoroutine(T_TowerTankManager.inst.CreateTowerTank(position, index, level, 0f + 0.3f * (float)this.id, T_TowerTank.TowerTankAttType.Patrol_Road, num2, this.id));
					}
				}
			}
		}
	}

	public void ShowExtraArmyInIsland()
	{
		int num = 0;
		foreach (KeyValuePair<int, SCExtraArmy> current in this.OtherIslandExtraArmyList)
		{
			for (int i = 0; i < current.Value.itemId2Level.Count; i++)
			{
				int num2 = (int)current.Value.id;
				int index = (int)current.Value.itemId2Level[i].key;
				int level = (int)current.Value.itemId2Level[i].value;
				int num3 = (int)current.Value.itemId2Num[i].value;
				for (int j = 0; j < num3; j++)
				{
					num++;
					Vector3 position = T_CommanderRoad.inst.T_CommanderRoad_tr[num].transform.position;
					base.StartCoroutine(T_TowerTankManager.inst.CreateTowerTank(position, index, level, 0f + 0.3f * (float)num, T_TowerTank.TowerTankAttType.Patrol_Road, num, num));
				}
			}
		}
	}

	public void ReturnHome()
	{
		this.ExtraArmyDataList.Clear();
		this.ExtraAymyTankList.Clear();
		this.ShowExtraArmyInHome();
	}

	public void RemoveAttackTank(T_TankAbstract tank)
	{
		if (this.Tanks_Attack.Contains(tank))
		{
			this.Tanks_Attack.Remove(tank);
		}
		if (this.Tanks_Attack.Count == 0 && this.Tank_AttackAllDie != null)
		{
			this.Tank_AttackAllDie();
		}
	}

	public void RemoveDefendTank(T_TankAbstract tank)
	{
		this.Tanks_Defend.Remove(tank);
	}

	public void SetBuildGridActive(bool isDisplay)
	{
		if (isDisplay)
		{
			for (int i = 0; i < this.towers.Count; i++)
			{
				if (this.towers[i] && SenceInfo.curMap.ID != HeroInfo.GetInstance().homeMapID && FightPanelManager.inst && this.towers[i].RedPiece)
				{
					this.towers[i].RedPiece_Show(true);
				}
				if (!this.towers[i].Equals(this.curTower) && !this.towers[i].Equals(this.tempTower))
				{
					this.towers[i].OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.helpinfo);
				}
			}
			foreach (KeyValuePair<int, T_Res> current in this.reses)
			{
				current.Value.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.helpinfo);
			}
		}
		else
		{
			for (int j = 0; j < this.towers.Count; j++)
			{
				if (this.towers[j] && this.towers[j].RedPiece)
				{
					this.towers[j].RedPiece_Show(false);
				}
				if (this.towers[j])
				{
					this.towers[j].OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.nodisplay);
				}
			}
			foreach (KeyValuePair<int, T_Res> current2 in this.reses)
			{
				if (current2.Value)
				{
					current2.Value.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.nodisplay);
				}
			}
		}
	}

	public void SetBuildGridActiveInAttacking(bool isDisplay)
	{
		if (isDisplay)
		{
			for (int i = 0; i < this.towers.Count; i++)
			{
				if (this.towers[i] && !this.towers[i].Equals(this.curTower) && !this.towers[i].Equals(this.tempTower) && this.towers[i].towerGridPlane_Attack)
				{
					this.towers[i].towerGridPlane_Attack.renderer.enabled = true;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.towers.Count; j++)
			{
				if (this.towers[j] && this.towers[j].towerGridPlane_Attack)
				{
					this.towers[j].towerGridPlane_Attack.renderer.enabled = false;
				}
			}
		}
	}

	public void SetBuildGrid_AttackTrue()
	{
		base.StopCoroutine(this.SetBuildGridActiveInAttacking());
		base.StartCoroutine(this.SetBuildGridActiveInAttacking());
	}

	[DebuggerHidden]
	private IEnumerator SetBuildGridActiveInAttacking()
	{
		SenceManager.<SetBuildGridActiveInAttacking>c__Iterator1D <SetBuildGridActiveInAttacking>c__Iterator1D = new SenceManager.<SetBuildGridActiveInAttacking>c__Iterator1D();
		<SetBuildGridActiveInAttacking>c__Iterator1D.<>f__this = this;
		return <SetBuildGridActiveInAttacking>c__Iterator1D;
	}

	private void Awake()
	{
		SenceManager.inst = this;
		this.tr = base.transform;
		this.fireRes = (GameObject)Resources.Load(ResManager.F_EffectRes_Path + "Fire_Ball");
		this.lineObj = (Resources.Load(ResManager.UnitTank_Path + "Line") as GameObject);
		base.gameObject.AddComponent<T_TowerTankManager>();
		GameTools.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		this.CreateTargeted();
	}

	public void CreateTargeted()
	{
	}

	public GameObject[] GetMovePath()
	{
		return new GameObject[3];
	}

	public void CreateMCV()
	{
	}

	public Body_Model CreatePlane()
	{
		if (this.plane == null)
		{
			this.plane = PoolManage.Ins.GetModelByBundleByName("kirov", this.tankPool);
			if (this.plane)
			{
				Transform[] componentsInChildren = this.plane.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = 9;
				}
				this.plane.transform.position = new Vector3(100f, 15f, 57f);
			}
		}
		return this.plane;
	}

	public void PlanePatrolling()
	{
		Body_Model body_Model = this.CreatePlane();
		if (body_Model)
		{
			if (body_Model.BlueModel)
			{
				NGUITools.SetActiveSelf(body_Model.BlueModel.gameObject, true);
			}
			if (body_Model.RedModel)
			{
				NGUITools.SetActiveSelf(body_Model.RedModel.gameObject, false);
			}
			if (this.isBack)
			{
				body_Model.tr.rotation = Quaternion.Euler(0f, 90f, 0f);
				body_Model.tr.DOMoveX(100f, 30f, false);
				this.isBack = false;
			}
			else
			{
				body_Model.tr.rotation = Quaternion.Euler(0f, -90f, 0f);
				body_Model.tr.DOMoveX(-80f, 30f, false);
				this.isBack = true;
			}
			return;
		}
	}

	public void DelaySendHttp()
	{
		if (HeroInfo.GetInstance().skillCarteList.Count == 0)
		{
			this.DelaySendHttp_CSSkillList();
			this.DelaySendHttp_CSSkillConfigList();
		}
		if (HeroInfo.GetInstance().PlayerTechnologyInfo.Count == 0)
		{
			this.DelaySendHttp_CSTechList();
		}
	}

	public void DelaySendHttp_CSSkillList()
	{
		if (!this.areadly_CSSkillList)
		{
			CSSkillList cSSkillList = new CSSkillList();
			cSSkillList.id = 1;
			ClientMgr.GetNet().SendHttp(2316, cSSkillList, null, null);
			this.areadly_CSSkillList = true;
		}
	}

	public void DelaySendHttp_CSSkillConfigList()
	{
		if (!this.areadly_CSSkillConfigList)
		{
			CSSkillConfigList cSSkillConfigList = new CSSkillConfigList();
			cSSkillConfigList.id = 1;
			ClientMgr.GetNet().SendHttp(2314, cSSkillConfigList, null, null);
			this.areadly_CSSkillConfigList = true;
		}
	}

	public void DelaySendHttp_CSTechList()
	{
		if (!this.areadly_CSTechList)
		{
			CSTechList cSTechList = new CSTechList();
			cSTechList.typeId = 1;
			ClientMgr.GetNet().SendHttp(6000, cSTechList, null, null);
			this.areadly_CSTechList = true;
		}
	}

	[DebuggerHidden]
	public IEnumerator CreateMap()
	{
		SenceManager.<CreateMap>c__Iterator1E <CreateMap>c__Iterator1E = new SenceManager.<CreateMap>c__Iterator1E();
		<CreateMap>c__Iterator1E.<>f__this = this;
		return <CreateMap>c__Iterator1E;
	}

	[DebuggerHidden]
	public IEnumerator regressBuildings()
	{
		SenceManager.<regressBuildings>c__Iterator1F <regressBuildings>c__Iterator1F = new SenceManager.<regressBuildings>c__Iterator1F();
		<regressBuildings>c__Iterator1F.<>f__this = this;
		return <regressBuildings>c__Iterator1F;
	}

	public void CreateRandomBoxArea()
	{
	}

	public void RefreshPath()
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		this.SetBoxColider(true);
		this.astarPath.ScanLoop(delegate(Progress progress)
		{
			if (progress.progress > 0.9f)
			{
				this.SetBoxColider(false);
			}
		});
	}

	private void SetBoxColider(bool isEnable)
	{
		for (int i = 0; i < this.towers.Count; i++)
		{
			if (this.towers[i] && this.towers[i].bodyForAttack)
			{
				this.towers[i].bodyForAttack.enabled = isEnable;
			}
		}
	}

	public void UpdateGraphs(Bounds bounds, float t)
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		this.astarPath.UpdateGraphs(bounds, t);
	}

	public void UpdateGraphs(Bounds bounds)
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		this.astarPath.UpdateGraphs(bounds);
	}

	public void DisplayLandingArea(bool isActive)
	{
		if (isActive)
		{
			SenceManager.inst.SetBuildGridActive(true);
		}
		else
		{
			SenceManager.inst.SetBuildGridActive(false);
		}
	}

	public void WallListUpdateEffect()
	{
		this.wall_no = 0;
		base.StartCoroutine(this.CreateWallListUpdateEffect(this.wall_no));
	}

	[DebuggerHidden]
	private IEnumerator CreateWallListUpdateEffect(int no)
	{
		SenceManager.<CreateWallListUpdateEffect>c__Iterator20 <CreateWallListUpdateEffect>c__Iterator = new SenceManager.<CreateWallListUpdateEffect>c__Iterator20();
		<CreateWallListUpdateEffect>c__Iterator.<>f__this = this;
		return <CreateWallListUpdateEffect>c__Iterator;
	}

	public T_Tower AddTempNPC(int idx, int RMBNeed = 0)
	{
		UIManager.inst.ResetSenceState(SenceState.InBuild);
		SenceManager.inst.WallLineChoose = false;
		this.tempTower = this.CreateTower(new BuildingNPC
		{
			buildingIdx = idx,
			row = 45,
			num = 45
		});
		this.RMBNeed_AddTempTower = RMBNeed;
		Vector3 center = this.GetCenter();
		if (this.tmpBuildingPostion != Vector3.zero)
		{
			this.tempTower.row = (int)this.tmpBuildingPostion.x;
			this.tempTower.num = (int)this.tmpBuildingPostion.z;
			this.tempTower.tr.localPosition = this.tmpBuildingPostion;
			if (!MapGridManager.VerifyTowerGrid(this.tempTower.index, this.tempTower.row, this.tempTower.num))
			{
				this.SearchSpace(this.tempTower, center);
				this.tempTower.tr.localPosition = new Vector3((float)this.tempTower.row, this.tempTower.tr.localPosition.y, (float)this.tempTower.num);
			}
			this.tmpBuildingPostion = Vector3.zero;
		}
		else
		{
			this.SearchSpace(this.tempTower, center);
			this.tempTower.tr.localPosition = new Vector3((float)this.tempTower.row, this.tempTower.tr.localPosition.y, (float)this.tempTower.num);
		}
		this.tempTower.posIdx = InfoMgr.PosGetMapIdx(this.tempTower.row, this.tempTower.num);
		this.tempTower.temp = true;
		this.curTower = this.tempTower;
		this.tempTower.SetInfo();
		Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("JT1", this.tempTower.tr);
		if (modelByBundleByName)
		{
			modelByBundleByName.tr.localPosition = new Vector3(0f, UnitConst.GetInstance().buildingConst[idx].hight / 2f, 0f);
			modelByBundleByName.tr.localScale = Vector3.one * 3f;
			TweenPosition tweenPosition = TweenPosition.Begin(modelByBundleByName.ga, 0.4f, new Vector3(0f, modelByBundleByName.tr.localPosition.y + 4f, 0f));
			tweenPosition.style = UITweener.Style.PingPong;
		}
		this.tempTower.BroadTowerClick();
		this.tempTower.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.canmove);
		if (CameraControl.inst)
		{
			CameraControl.inst.Tr.DOMove(HUDTextTool.inst.GetCameraMoveEndPos(SenceManager.inst.tempTower.tr.position, CameraControl.inst.Tr.position, 65f), 0.56f, false);
			CameraControl.inst.openDragCameraAndInertia = false;
		}
		return this.tempTower;
	}

	public void SetChooseEffect(T_Tower tower, bool set, bool centre = false)
	{
		if (set)
		{
			return;
		}
		if (tower.WallChoosseEffect != null)
		{
			UnityEngine.Object.Destroy(tower.WallChoosseEffect.ga);
			tower.WallChoosseEffect = null;
		}
	}

	public void SetAllBuildingDown()
	{
		for (int i = 0; i < this.towers.Count; i++)
		{
			if (this.towers[i].tr.position.y == 0f)
			{
				this.towers[i].SetModelToTeXiaoLayer(false);
			}
		}
	}

	public void DesGetWallLatelyEffect()
	{
		if (this.GetWallLately != null)
		{
			this.SetChooseEffect(this.GetWallLately, false, false);
			this.GetWallLately.SetModelToTeXiaoLayer(false);
			for (int i = 0; i < this.GetWallLately.Walltower_list.Count; i++)
			{
				this.SetChooseEffect(this.GetWallLately.Walltower_list[i], false, false);
				this.GetWallLately.Walltower_list[i].SetModelToTeXiaoLayer(false);
			}
		}
		this.GetWallLately = null;
		for (int j = 0; j < SenceManager.inst.towers.Count; j++)
		{
			if (SenceManager.inst.towers[j].secType == 20)
			{
				SenceManager.inst.towers[j].Walltower_list.Clear();
			}
		}
	}

	public List<T_Tower> GetTowerByLine(T_Tower tower)
	{
		this.DesGetWallLatelyEffect();
		List<T_Tower> list = new List<T_Tower>();
		this.GetWallLately = tower;
		this.SetChooseEffect(tower, true, true);
		if (!this.WallLineChoose)
		{
			return list;
		}
		tower.Walltower_list.Clear();
		for (int i = 1; i <= 4; i++)
		{
			T_Tower t_Tower = tower;
			for (int j = 0; j < 20; j++)
			{
				t_Tower = this.GetTopWall(t_Tower, i);
				if (!(t_Tower != null))
				{
					break;
				}
				this.SetChooseEffect(t_Tower, true, false);
				list.Add(t_Tower);
				t_Tower.WallToPos = new Vector2(t_Tower.tr.position.x - tower.tr.position.x, t_Tower.tr.position.z - tower.tr.position.z);
			}
		}
		return list;
	}

	public T_Tower GetTopWall(T_Tower tower, int direction)
	{
		if (direction == 1)
		{
			if (tower.topWall != null && tower.topWall.tr.position.x == tower.tr.position.x - 2f && tower.topWall.tr.position.z == tower.tr.position.z)
			{
				return tower.topWall;
			}
			return null;
		}
		else if (direction == 2)
		{
			if (tower.bottomWall != null && tower.bottomWall.tr.position.x == tower.tr.position.x + 2f && tower.bottomWall.tr.position.z == tower.tr.position.z)
			{
				return tower.bottomWall;
			}
			return null;
		}
		else if (direction == 3)
		{
			if (tower.leftWall != null && tower.leftWall.tr.position.x == tower.tr.position.x && tower.leftWall.tr.position.z == tower.tr.position.z - 2f)
			{
				return tower.leftWall;
			}
			return null;
		}
		else
		{
			if (direction != 4)
			{
				return null;
			}
			if (tower.rightWall != null && tower.rightWall.tr.position.x == tower.tr.position.x && tower.rightWall.tr.position.z == tower.tr.position.z + 2f)
			{
				return tower.rightWall;
			}
			return null;
		}
	}

	public void WallRotate()
	{
		if (this.rotate_time != 0f)
		{
			return;
		}
		if (this.GetWallLately == null)
		{
			return;
		}
		if (this.WallParent != null)
		{
			return;
		}
		this.rotate_time = 10f;
		T_Tower getWallLately = this.GetWallLately;
		GameObject gameObject = new GameObject("Wall_P");
		gameObject.transform.parent = getWallLately.tr.parent;
		gameObject.transform.position = getWallLately.tr.position;
		getWallLately.tr.parent = gameObject.transform;
		this.DesWallconnect(getWallLately);
		getWallLately.EditeMapGrid(false);
		if (getWallLately.Walltower_list.Count > 0)
		{
			for (int i = 0; i < getWallLately.Walltower_list.Count; i++)
			{
				getWallLately.Walltower_list[i].tr.parent = gameObject.transform;
				this.DesWallconnect(getWallLately.Walltower_list[i]);
				getWallLately.Walltower_list[i].EditeMapGrid(false);
			}
		}
		this.WallParent = gameObject.transform;
		this.WallParent_Y = this.WallParent.transform.eulerAngles.y + 90f;
	}

	private void WallRotateEnd()
	{
		if (this.WallParent != null)
		{
			DragMgr.inst.buildDraging = true;
			T_Tower getWallLately = this.GetWallLately;
			getWallLately.tr.parent = this.WallParent.parent.transform;
			getWallLately.topWall = null;
			getWallLately.bottomWall = null;
			getWallLately.leftWall = null;
			getWallLately.rightWall = null;
			getWallLately.tr.localPosition = new Vector3((float)((int)Math.Round((double)getWallLately.tr.localPosition.x)), (float)((int)Math.Round((double)getWallLately.tr.localPosition.y)), (float)((int)Math.Round((double)getWallLately.tr.localPosition.z)));
			for (int i = 0; i < getWallLately.Walltower_list.Count; i++)
			{
				getWallLately.Walltower_list[i].tr.parent = this.WallParent.parent.transform;
				getWallLately.Walltower_list[i].tr.localPosition = new Vector3((float)((int)Math.Round((double)getWallLately.Walltower_list[i].tr.localPosition.x)), (float)((int)Math.Round((double)getWallLately.Walltower_list[i].tr.localPosition.y)), (float)((int)Math.Round((double)getWallLately.Walltower_list[i].tr.localPosition.z)));
				getWallLately.Walltower_list[i].WallToPos = new Vector2(getWallLately.Walltower_list[i].tr.position.x - getWallLately.tr.position.x, getWallLately.Walltower_list[i].tr.position.z - getWallLately.tr.position.z);
				getWallLately.Walltower_list[i].topWall = null;
				getWallLately.Walltower_list[i].bottomWall = null;
				getWallLately.Walltower_list[i].leftWall = null;
				getWallLately.Walltower_list[i].rightWall = null;
			}
			getWallLately.OnTowerPosShow();
			this.CreateMuraille(getWallLately);
			if (getWallLately.Walltower_list.Count > 0)
			{
				for (int j = 0; j < getWallLately.Walltower_list.Count; j++)
				{
					this.CreateMuraille(getWallLately.Walltower_list[j]);
					getWallLately.Walltower_list[j].OnTowerPosShow();
				}
			}
			UnityEngine.Object.Destroy(this.WallParent.gameObject);
			this.WallParent = null;
			if (!getWallLately.canBuild)
			{
				this.rotate_time = 0f;
				return;
			}
			if (getWallLately.Walltower_list.Count > 0)
			{
				for (int k = 0; k < getWallLately.Walltower_list.Count; k++)
				{
					if (!getWallLately.Walltower_list[k].canBuild)
					{
						this.rotate_time = 0f;
						return;
					}
				}
			}
			getWallLately.row = Mathf.RoundToInt(getWallLately.tr.localPosition.x);
			getWallLately.num = Mathf.RoundToInt(getWallLately.tr.localPosition.z);
			for (int l = 0; l < getWallLately.Walltower_list.Count; l++)
			{
				getWallLately.Walltower_list[l].row = Mathf.RoundToInt(getWallLately.Walltower_list[l].tr.localPosition.x);
				getWallLately.Walltower_list[l].num = Mathf.RoundToInt(getWallLately.Walltower_list[l].tr.localPosition.z);
			}
			this.MovedTower(getWallLately, true);
			for (int m = 0; m < getWallLately.Walltower_list.Count; m++)
			{
				this.MovedTower(getWallLately.Walltower_list[m], false);
			}
		}
	}

	public void DesWallconnect(T_Tower tower)
	{
		if (tower.topObj != null)
		{
			UnityEngine.Object.Destroy(tower.topObj);
		}
		tower.topObj = null;
		if (tower.bottomObj != null)
		{
			UnityEngine.Object.Destroy(tower.bottomObj);
		}
		tower.bottomObj = null;
		if (tower.leftObj != null)
		{
			UnityEngine.Object.Destroy(tower.leftObj);
		}
		tower.leftObj = null;
		if (tower.rightObj != null)
		{
			UnityEngine.Object.Destroy(tower.rightObj);
		}
		tower.rightObj = null;
	}

	public T_Tower AddNewTower(BuildingNPC npc)
	{
		for (int i = 0; i < this.towers.Count; i++)
		{
			if (this.towers[i] && this.towers[i].id == npc.buildingId)
			{
				return null;
			}
		}
		T_Tower t_Tower = this.CreateTower(npc);
		t_Tower.state = TowerState.normal;
		t_Tower.SetInfo();
		this.towers.Add(t_Tower);
		t_Tower.EditeMapGrid(true);
		if (t_Tower.secType == 1)
		{
			this.MainBuilding = t_Tower;
			NumericalMgr.commandHp = t_Tower.MaxLife;
		}
		else if (t_Tower.type != 5 && UnitConst.GetInstance().buildingConst[t_Tower.index].secType != 1 && t_Tower.secType != 20)
		{
			NumericalMgr.sumHp += (float)t_Tower.MaxLife;
		}
		if (t_Tower.secType == 20)
		{
			this.SetNewWall(t_Tower);
		}
		return t_Tower;
	}

	public void AddNewRes(BuildingNPC npc)
	{
		T_Res t_Res = this.CreateRes(npc);
		t_Res.SetInfo();
		if (this.reses.ContainsKey(t_Res.posIndex))
		{
			this.RemoveRes(t_Res.posIndex);
		}
		this.reses.Add(t_Res.posIndex, t_Res);
		t_Res.EditeMapGrid(true);
	}

	public void RemoveRes(int posIndex)
	{
		if (this.reses.ContainsKey(posIndex))
		{
			T_Res t_Res = this.reses[posIndex];
			SenceInfo.curMap.resRemoveList.Add(t_Res.posIndex);
			if (SenceInfo.curMap.growList.ContainsKey(t_Res.posIndex))
			{
				SenceInfo.curMap.growList.Remove(t_Res.posIndex);
			}
			t_Res.EditeMapGrid(false);
			SenceManager.inst.reses.Remove(posIndex);
			t_Res.DesInPool();
		}
	}

	public void MovedTower(T_Tower tower, bool wallcentre = false)
	{
		tower.row = Mathf.RoundToInt(tower.tr.localPosition.x);
		tower.num = Mathf.RoundToInt(tower.tr.localPosition.z);
		int state = (int)tower.state;
		BuildingNPC buildingNPC = SenceInfo.curMap.towerList_Data[tower.id];
		if (MapGridManager.VerifyMapGrid(tower.row, tower.num))
		{
			buildingNPC.row = tower.row;
			buildingNPC.num = tower.num;
			if (wallcentre)
			{
				if (SenceManager.inst.WallLineChoose)
				{
					if (tower.Walltower_list.Count > 0)
					{
						BuildingHandler.CG_WallListMoveStart(tower);
					}
					else
					{
						if (this.rotate_time > 0f)
						{
							this.rotate_time = 0f;
						}
						BuildingHandler.CG_BuildingMoveStart(SenceInfo.curMap.ID, tower.row, tower.num, tower.id);
					}
				}
				else
				{
					BuildingHandler.CG_BuildingMoveStart(SenceInfo.curMap.ID, tower.row, tower.num, tower.id);
				}
			}
		}
		else
		{
			tower.EditeMapGrid(false);
			tower.RebackTower(buildingNPC.row, buildingNPC.num);
		}
	}

	public void RebackTower()
	{
		DragMgr.inst.buildDraging = false;
		if (!this.sameTower)
		{
			if (this.mover_Tower == null)
			{
				return;
			}
			this.DesWallconnect(this.mover_Tower);
			this.mover_Tower.RebackTower(this.mover_Tower.row, this.mover_Tower.num);
			if (this.WallLineChoose)
			{
				for (int i = 0; i < this.mover_Tower.Walltower_list.Count; i++)
				{
					this.DesWallconnect(this.mover_Tower.Walltower_list[i]);
					this.mover_Tower.Walltower_list[i].RebackTower(this.mover_Tower.Walltower_list[i].row, this.mover_Tower.Walltower_list[i].num);
				}
			}
			if (this.mover_Tower.temp)
			{
				SenceManager.inst.ShowTowerR(true, this.mover_Tower);
				this.mover_Tower.canBuild = this.mover_Tower.VerifyMapGrid();
			}
		}
		SenceManager.inst.CreateMuraille(this.mover_Tower);
		if (this.WallLineChoose)
		{
			for (int j = 0; j < this.mover_Tower.Walltower_list.Count; j++)
			{
				SenceManager.inst.CreateMuraille(this.mover_Tower.Walltower_list[j]);
			}
		}
	}

	public T_Res CreateRes(BuildingNPC npc)
	{
		T_Res res = PoolManage.Ins.GetRes(new Vector3((float)npc.row, 0f, (float)npc.num), Quaternion.identity, this.resPool);
		res.id = npc.buildingId;
		res.index = npc.buildingIdx;
		res.posIndex = npc.posIdx;
		res.lv = npc.lv;
		res.row = npc.row;
		res.num = npc.num;
		return res;
	}

	public T_Tower CreateTower(BuildingNPC npc)
	{
		T_Tower tower = PoolManage.Ins.GetTower<T_Tower>(new Vector3((float)npc.row, 0f, (float)npc.num), Quaternion.identity, this.towerPool);
		tower.id = npc.buildingId;
		if (SenceInfo.curMap.IsMyHome && UnitConst.GetInstance().buildingConst[npc.buildingIdx].secType == 15)
		{
			HeroInfo.GetInstance().PlayerCommandoBuildingID = npc.buildingId;
		}
		tower.index = npc.buildingIdx;
		tower.type = UnitConst.GetInstance().GetBuildingType(npc.buildingIdx);
		tower.secType = UnitConst.GetInstance().GetBuildingSecType(npc.buildingIdx);
		for (int i = 0; i < tower.myTanks.Count; i++)
		{
			UnityEngine.Object.Destroy(tower.myTanks[i]);
		}
		tower.myTanks.Clear();
		tower.posIdx = npc.posIdx;
		if (npc.lv == 0)
		{
			tower.lv = 1;
			tower.trueLv = npc.lv;
		}
		else
		{
			tower.lv = npc.lv;
			tower.trueLv = npc.lv;
		}
		tower.star = npc.star;
		tower.strengthLevel = npc.strength;
		tower.row = npc.row;
		tower.num = npc.num;
		tower.roleType = Enum_RoleType.tower;
		tower.protductNum = npc.protductNum;
		tower.charaType = Enum_CharaType.defender;
		if (npc.productType == BuildingProductType.soldier)
		{
		}
		return tower;
	}

	public void DestoryMuraille(T_Tower muraille)
	{
		if (muraille.leftWall != null && muraille.leftWall.rightObj)
		{
			UnityEngine.Object.Destroy(muraille.leftWall.rightObj);
		}
		if (muraille.rightWall != null && muraille.rightWall.leftObj)
		{
			UnityEngine.Object.Destroy(muraille.rightWall.leftObj);
		}
		if (muraille.topWall != null && muraille.topWall.bottomObj)
		{
			UnityEngine.Object.Destroy(muraille.topWall.bottomObj);
		}
		if (muraille.bottomWall != null && muraille.bottomWall.topObj)
		{
			UnityEngine.Object.Destroy(muraille.bottomWall.topObj);
		}
		if (muraille.leftObj != null)
		{
			UnityEngine.Object.Destroy(muraille.leftObj);
		}
		if (muraille.rightObj)
		{
			UnityEngine.Object.Destroy(muraille.rightObj);
		}
		if (muraille.topObj)
		{
			UnityEngine.Object.Destroy(muraille.topObj);
		}
		if (muraille.bottomObj)
		{
			UnityEngine.Object.Destroy(muraille.bottomObj);
		}
		muraille.leftObj = null;
		muraille.rightObj = null;
		muraille.topObj = null;
		muraille.bottomObj = null;
	}

	public void SetNewWall(T_Tower tower)
	{
		this.SecondNewWall = this.FirstNewWall;
		this.FirstNewWall = tower;
	}

	public Vector3 GetNextWallPos()
	{
		Vector3 result = Vector3.zero;
		Vector3 vector = Vector3.zero;
		if (this.FirstNewWall != null)
		{
			vector = this.FirstNewWall.tr.position;
		}
		if (this.FirstNewWall == null)
		{
			return result;
		}
		if (this.FirstNewWall.leftWall == this.SecondNewWall)
		{
			if (this.FirstNewWall.rightWall == null && vector.z + 2f < (float)this.arrayY && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x, (int)(vector.z + 2f)))
			{
				return this.FirstNewWall.tr.position + new Vector3(0f, 0f, 2f);
			}
		}
		else if (this.FirstNewWall.rightWall == this.SecondNewWall)
		{
			if (this.FirstNewWall.leftWall == null && vector.z - 2f > 0f && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x, (int)vector.z - 2))
			{
				return this.FirstNewWall.tr.position + new Vector3(0f, 0f, -2f);
			}
		}
		else if (this.FirstNewWall.topWall == this.SecondNewWall)
		{
			if (this.FirstNewWall.bottomWall == null && vector.x + 2f < (float)this.arrayX && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x + 2, (int)vector.z))
			{
				return this.FirstNewWall.tr.position + new Vector3(2f, 0f, 0f);
			}
		}
		else if (this.FirstNewWall.bottomWall == this.SecondNewWall)
		{
			if (this.FirstNewWall.topWall == null && vector.x - 2f > 0f && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x - 2, (int)vector.z))
			{
				return this.FirstNewWall.tr.position + new Vector3(-2f, 0f, 0f);
			}
		}
		else if (this.FirstNewWall.leftWall != null)
		{
			if (this.FirstNewWall.rightWall == null && vector.z + 2f < (float)this.arrayY && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x, (int)(vector.z + 2f)))
			{
				return this.FirstNewWall.tr.position + new Vector3(0f, 0f, 2f);
			}
		}
		else if (this.FirstNewWall.rightWall != null)
		{
			if (this.FirstNewWall.leftWall == null && vector.z - 2f > 0f && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x, (int)vector.z - 2))
			{
				return this.FirstNewWall.tr.position + new Vector3(0f, 0f, -2f);
			}
		}
		else if (this.FirstNewWall.topWall != null)
		{
			if (this.FirstNewWall.bottomWall == null && vector.x + 2f < (float)this.arrayX && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x + 2, (int)vector.z))
			{
				return this.FirstNewWall.tr.position + new Vector3(2f, 0f, 0f);
			}
		}
		else if (this.FirstNewWall.bottomWall != null && this.FirstNewWall.topWall == null && vector.x - 2f > 0f && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x - 2, (int)vector.z))
		{
			return this.FirstNewWall.tr.position + new Vector3(-2f, 0f, 0f);
		}
		for (int i = 0; i < 20; i++)
		{
			if (this.FirstNewWall.rightWall == null && vector.z + 2f < (float)this.arrayY && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x, (int)(vector.z + 2f)))
			{
				result = this.FirstNewWall.tr.position + new Vector3(0f, 0f, 2f);
				break;
			}
			if (this.FirstNewWall.bottomWall == null && vector.x + 2f < (float)this.arrayX && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x + 2, (int)vector.z))
			{
				result = this.FirstNewWall.tr.position + new Vector3(2f, 0f, 0f);
				break;
			}
			if (this.FirstNewWall.leftWall == null && vector.z - 2f > 0f && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x, (int)vector.z - 2))
			{
				result = this.FirstNewWall.tr.position + new Vector3(0f, 0f, -2f);
				break;
			}
			if (this.FirstNewWall.topWall == null && vector.x - 2f > 0f && MapGridManager.VerifyTowerGrid(this.FirstNewWall.index, (int)vector.x - 2, (int)vector.z))
			{
				result = this.FirstNewWall.tr.position + new Vector3(-2f, 0f, 0f);
				break;
			}
			if (this.FirstNewWall.rightWall != null)
			{
				this.FirstNewWall = this.FirstNewWall.rightWall;
			}
			else if (this.FirstNewWall.bottomWall != null)
			{
				this.FirstNewWall = this.FirstNewWall.bottomWall;
			}
			else if (this.FirstNewWall.leftWall != null)
			{
				this.FirstNewWall = this.FirstNewWall.leftWall;
			}
			else if (this.FirstNewWall.topWall != null)
			{
				this.FirstNewWall = this.FirstNewWall.topWall;
			}
			vector = this.FirstNewWall.tr.position;
		}
		return result;
	}

	public void CreateMuraille(T_Tower muraille)
	{
		if (muraille.ModelBody == null)
		{
			return;
		}
		if (UnitConst.GetInstance().buildingConst[muraille.index].secType == 20)
		{
			for (int i = 0; i < this.towers.Count; i++)
			{
				if (UnitConst.GetInstance().buildingConst[this.towers[i].index].secType == 20)
				{
					if (muraille.tr.position.x == this.towers[i].tr.position.x)
					{
						if (muraille.tr.position.z - this.towers[i].tr.position.z == 2f)
						{
							string wall_Line = this.GetWall_Line(muraille, this.towers[i]);
							if (muraille.leftObj == null || !muraille.leftObj.name.Equals(wall_Line))
							{
								if (muraille.leftObj)
								{
									UnityEngine.Object.Destroy(muraille.leftObj);
								}
								Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName(wall_Line, null);
								if (modelByBundleByName.BlueModel)
								{
									modelByBundleByName.BlueModel.gameObject.SetActive(SenceInfo.curMap.IsMyMap);
								}
								if (modelByBundleByName.RedModel)
								{
									modelByBundleByName.RedModel.gameObject.SetActive(!SenceInfo.curMap.IsMyMap);
								}
								GameObject ga = modelByBundleByName.ga;
								ga.transform.position = new Vector3(muraille.tr.position.x, 0f, muraille.tr.position.z - 1f);
								ga.transform.parent = this.towerPool;
								ga.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
								if (muraille.leftObj != null)
								{
									UnityEngine.Object.Destroy(muraille.leftObj.gameObject);
								}
								if (this.towers[i].rightObj != null)
								{
									UnityEngine.Object.Destroy(this.towers[i].rightObj.gameObject);
								}
								muraille.leftObj = ga;
								this.towers[i].rightObj = ga;
								ga.AddComponent<WallConnect>();
								ga.GetComponent<WallConnect>().RightWall = muraille;
								ga.GetComponent<WallConnect>().LeftWall = this.towers[i];
								muraille.leftWall = this.towers[i];
								this.towers[i].rightWall = muraille;
							}
						}
						else if (muraille.tr.position.z - this.towers[i].tr.position.z == -2f)
						{
							string wall_Line2 = this.GetWall_Line(muraille, this.towers[i]);
							if (muraille.rightObj == null || !muraille.rightObj.name.Equals(wall_Line2))
							{
								if (muraille.rightObj)
								{
									UnityEngine.Object.Destroy(muraille.rightObj);
								}
								Body_Model modelByBundleByName2 = PoolManage.Ins.GetModelByBundleByName(wall_Line2, null);
								if (modelByBundleByName2.BlueModel)
								{
									modelByBundleByName2.BlueModel.gameObject.SetActive(SenceInfo.curMap.IsMyMap);
								}
								if (modelByBundleByName2.RedModel)
								{
									modelByBundleByName2.RedModel.gameObject.SetActive(!SenceInfo.curMap.IsMyMap);
								}
								GameObject ga2 = modelByBundleByName2.ga;
								ga2.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
								ga2.transform.position = new Vector3(muraille.tr.position.x, 0f, muraille.tr.position.z + 1f);
								ga2.transform.parent = this.towerPool;
								if (muraille.rightObj != null)
								{
									UnityEngine.Object.Destroy(muraille.rightObj.gameObject);
								}
								if (this.towers[i].leftObj != null)
								{
									UnityEngine.Object.Destroy(this.towers[i].leftObj.gameObject);
								}
								muraille.rightObj = ga2;
								this.towers[i].leftObj = ga2;
								ga2.AddComponent<WallConnect>();
								ga2.GetComponent<WallConnect>().LeftWall = muraille;
								ga2.GetComponent<WallConnect>().RightWall = this.towers[i];
								muraille.rightWall = this.towers[i];
								this.towers[i].leftWall = muraille;
							}
						}
					}
					else if (muraille.tr.position.z == this.towers[i].tr.position.z)
					{
						if (muraille.tr.position.x - this.towers[i].tr.position.x == 2f)
						{
							string wall_Line3 = this.GetWall_Line(muraille, this.towers[i]);
							if (muraille.topObj == null || !muraille.topObj.name.Equals(wall_Line3))
							{
								if (muraille.topObj)
								{
									UnityEngine.Object.Destroy(muraille.topObj);
								}
								Body_Model modelByBundleByName3 = PoolManage.Ins.GetModelByBundleByName(wall_Line3, null);
								if (modelByBundleByName3.BlueModel)
								{
									modelByBundleByName3.BlueModel.gameObject.SetActive(SenceInfo.curMap.IsMyMap);
								}
								if (modelByBundleByName3.RedModel)
								{
									modelByBundleByName3.RedModel.gameObject.SetActive(!SenceInfo.curMap.IsMyMap);
								}
								GameObject ga3 = modelByBundleByName3.ga;
								ga3.transform.position = new Vector3(muraille.tr.position.x - 1f, 0f, muraille.tr.position.z);
								ga3.transform.parent = this.towerPool;
								if (muraille.topObj != null)
								{
									UnityEngine.Object.Destroy(muraille.topObj.gameObject);
								}
								if (this.towers[i].bottomObj != null)
								{
									UnityEngine.Object.Destroy(this.towers[i].bottomObj.gameObject);
								}
								muraille.topObj = ga3;
								this.towers[i].bottomObj = ga3;
								ga3.AddComponent<WallConnect>();
								ga3.GetComponent<WallConnect>().BottomWall = muraille;
								ga3.GetComponent<WallConnect>().TopWall = this.towers[i];
								muraille.topWall = this.towers[i];
								this.towers[i].bottomWall = muraille;
							}
						}
						else if (muraille.tr.position.x - this.towers[i].tr.position.x == -2f)
						{
							string wall_Line4 = this.GetWall_Line(muraille, this.towers[i]);
							if (muraille.bottomObj == null || !muraille.bottomObj.name.Equals(wall_Line4))
							{
								if (muraille.bottomObj)
								{
									UnityEngine.Object.Destroy(muraille.bottomObj);
								}
								Body_Model modelByBundleByName4 = PoolManage.Ins.GetModelByBundleByName(wall_Line4, null);
								if (modelByBundleByName4.BlueModel)
								{
									modelByBundleByName4.BlueModel.gameObject.SetActive(SenceInfo.curMap.IsMyMap);
								}
								if (modelByBundleByName4.RedModel)
								{
									modelByBundleByName4.RedModel.gameObject.SetActive(!SenceInfo.curMap.IsMyMap);
								}
								GameObject ga4 = modelByBundleByName4.ga;
								ga4.transform.position = new Vector3(muraille.tr.position.x + 1f, 0f, muraille.tr.position.z);
								ga4.transform.parent = this.towerPool;
								if (muraille.bottomObj != null)
								{
									UnityEngine.Object.Destroy(muraille.bottomObj.gameObject);
								}
								if (this.towers[i].topObj != null)
								{
									UnityEngine.Object.Destroy(this.towers[i].topObj.gameObject);
								}
								muraille.bottomObj = ga4;
								this.towers[i].topObj = ga4;
								ga4.AddComponent<WallConnect>();
								ga4.GetComponent<WallConnect>().TopWall = muraille;
								ga4.GetComponent<WallConnect>().BottomWall = this.towers[i];
								muraille.bottomWall = this.towers[i];
								this.towers[i].topWall = muraille;
							}
						}
					}
				}
			}
		}
	}

	private string GetWall_Line(T_Tower one, T_Tower two)
	{
		int num = int.Parse(one.BodyName.Substring(two.BodyName.Length - 1, 1));
		int num2 = int.Parse(two.BodyName.Substring(two.BodyName.Length - 1, 1));
		if (num > num2)
		{
			return string.Format("WQ{0}_L", num2);
		}
		return string.Format("WQ{0}_L", num);
	}

	public DateTime LongToDataTime(long time)
	{
		this.ArmyTokenCdTime = TimeTools.ConvertLongDateTime(time);
		this.ArmyTokenCdTime_Text = string.Concat(new object[]
		{
			this.ArmyTokenCdTime.Hour,
			":",
			this.ArmyTokenCdTime.Minute,
			":",
			this.ArmyTokenCdTime.Second
		});
		return this.ArmyTokenCdTime;
	}

	private void Update()
	{
		if (this.WallBuildEndAudio > 0f)
		{
			this.WallBuildEndAudio -= Time.deltaTime;
		}
		if (this.TowerTankMessage_CDTime > -1f)
		{
			this.TowerTankMessage_CDTime -= Time.deltaTime;
		}
		if (this.WallParent != null)
		{
			this.WallParent.transform.eulerAngles = new Vector3(0f, Mathf.Min(this.WallParent_Y, this.WallParent.transform.eulerAngles.y + 250f * Time.deltaTime), 0f);
			if (this.WallParent.transform.eulerAngles.y >= this.WallParent_Y)
			{
				this.WallRotateEnd();
			}
		}
		if (UIManager.curState == SenceState.Attacking && !FightHundler.FightEnd && NewbieGuidePanel.isEnemyAttck && this.time + 16f < Time.time && this.towers.Count > 0 && !FightHundler.isSendFightEnd)
		{
			this.RefreshTowerLife();
			this.time = Time.time;
		}
		if (UIManager.curState == SenceState.Home)
		{
			this.planeTime += Time.deltaTime;
			if (this.planeTime >= 30f)
			{
				this.PlanePatrolling();
				this.CreateMCV();
				this.planeTime = 0f;
			}
		}
	}

	public void RefreshTowerLife()
	{
		for (int i = 0; i < this.towers.Count; i++)
		{
			if (UnitConst.GetInstance().buildingConst[this.towers[i].index].secType < 4 || UnitConst.GetInstance().buildingConst[this.towers[i].index].secType == 17)
			{
				FightHundler.AddDeadSettleUnites(new SettleUniteData
				{
					deadType = 6,
					deadIdx = this.towers[i].index,
					deadSenceId = this.towers[i].id,
					deadBuildingID = (long)this.towers[i].posIdx,
					num = (int)this.towers[i].CurLife
				});
			}
		}
	}

	public void ShowLandArray(bool show, bool isMoveTo = false, bool isSTARTBATTLE = false)
	{
		if (!show)
		{
			return;
		}
		if (!show || isSTARTBATTLE)
		{
		}
	}

	public Vector3 GetCenter()
	{
		Vector3 position = new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f);
		UIManager.inst.UIInUsed(false);
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			Vector3 result = new Vector3((float)Mathf.RoundToInt(raycastHit.point.x), 0f, (float)Mathf.RoundToInt(raycastHit.point.z));
			if (result.x < 1f)
			{
				result.x = 1f;
			}
			else if (result.x >= (float)this.arrayY)
			{
				result.x = (float)(this.arrayY - 1);
			}
			if (result.z < 1f)
			{
				result.z = 1f;
			}
			else if (result.z >= (float)this.arrayX)
			{
				result.z = (float)(this.arrayX - 1);
			}
			return result;
		}
		return new Vector3(45f, 0f, 45f);
	}

	public bool SearchSpace(T_Tower _tempTower, Vector3 centerPos)
	{
		int num = Mathf.RoundToInt(centerPos.x);
		int num2 = Mathf.RoundToInt(centerPos.z);
		if (_tempTower.secType == 20)
		{
			Vector3 nextWallPos = this.GetNextWallPos();
			nextWallPos = new Vector3((float)Mathf.RoundToInt(nextWallPos.x), 0f, (float)Mathf.RoundToInt(nextWallPos.z));
			if (nextWallPos != Vector3.zero)
			{
				if (nextWallPos.x < (float)this.arrayX && nextWallPos.z < (float)this.arrayY && MapGridManager.VerifyTowerGrid(_tempTower.index, (int)nextWallPos.x, (int)nextWallPos.z))
				{
					_tempTower.row = Mathf.RoundToInt(nextWallPos.x);
					_tempTower.num = Mathf.RoundToInt(nextWallPos.z);
				}
				return true;
			}
		}
		for (int i = 1; i <= 50; i++)
		{
			for (int j = -i; j <= i; j++)
			{
				if (num + i < this.arrayX && MapGridManager.VerifyTowerGrid(_tempTower.index, num + i, num2 + j))
				{
					_tempTower.row = num + i;
					_tempTower.num = num2 + j;
					return true;
				}
				if (num - i > 1 && MapGridManager.VerifyTowerGrid(_tempTower.index, num - i, num2 + j))
				{
					_tempTower.row = num - i;
					_tempTower.num = num2 + j;
					return true;
				}
				if (num2 + i < this.arrayY && MapGridManager.VerifyTowerGrid(_tempTower.index, num + j, num2 + i))
				{
					_tempTower.row = num + j;
					_tempTower.num = num2 + i;
					return true;
				}
				if (num2 - i > 1 && MapGridManager.VerifyTowerGrid(_tempTower.index, num + j, num2 - i))
				{
					_tempTower.row = num + j;
					_tempTower.num = num2 - i;
					return true;
				}
			}
		}
		return false;
	}

	public void TankSearching()
	{
		for (int i = 0; i < this.Tanks_Attack.Count; i++)
		{
			if (this.Tanks_Attack[i].State == T_TankFightState.TankFightState.Idle)
			{
				this.Tanks_Attack[i].NewSearching();
			}
		}
	}

	public void UnitOver(int type)
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		if (type == 3)
		{
			FightHundler.FightEnd = true;
			this.settType = SettlementType.failure;
			TimePanel.inst.isEnd = true;
			FightHundler.CG_FinishFight();
		}
		if (type == 4)
		{
			FightHundler.FightEnd = true;
			this.settType = SettlementType.success;
			TimePanel.inst.isEnd = true;
			FightHundler.CG_FinishFight();
		}
	}

	[DebuggerHidden]
	private IEnumerator SelfDestruct()
	{
		SenceManager.<SelfDestruct>c__Iterator21 <SelfDestruct>c__Iterator = new SenceManager.<SelfDestruct>c__Iterator21();
		<SelfDestruct>c__Iterator.<>f__this = this;
		return <SelfDestruct>c__Iterator;
	}

	public void NoteUnitOver(int type, long id)
	{
		if (id == 0L)
		{
			UnityEngine.Debug.LogError("回放   有ID 为 0 的坦克");
		}
		if (type == 0)
		{
			for (int i = 0; i < this.towers.Count; i++)
			{
				if (this.towers[i].id == id)
				{
					this.towers[i].SelfDestruct();
					return;
				}
			}
		}
		else
		{
			for (int j = 0; j < this.Tanks_Attack.Count; j++)
			{
				if (this.Tanks_Attack[j].sceneId == id)
				{
					this.Tanks_Attack[j].SelfDestruct();
					return;
				}
			}
			for (int k = 0; k < this.Tanks_Defend.Count; k++)
			{
				if (this.Tanks_Defend[k].sceneId == id)
				{
					this.Tanks_Defend[k].SelfDestruct();
					return;
				}
			}
		}
	}

	public void NoteSend(ContainerData data)
	{
		if (data.soldierType == -1)
		{
			Container.CreateContainer(EventNoteMgr.MsgPosToVector(data.containPos), data.containerId, data.id, data.index, data.soldierLV, false, false, CommanderType.None);
		}
		else
		{
			Container.CreateCommander(EventNoteMgr.MsgPosToVector(data.containPos), data.containerId, data.id, data.index, data.soldierLV, data.soldieStar, data.soldierSkillLV, false);
		}
	}

	private void BuildSeleR()
	{
		GameObject gameObject = (GameObject)Resources.Load(ResManager.UnitTower_Path + "SelectRang");
		if (this.tr.FindChild("SelectRang") == null)
		{
			gameObject = (UnityEngine.Object.Instantiate(gameObject) as GameObject);
			gameObject.name = "SelectRang";
			gameObject.transform.parent = base.transform;
		}
		else
		{
			gameObject = this.tr.FindChild("SelectRang").gameObject;
		}
		this.towerR = gameObject.GetComponent<T_TowerR>();
		this.ShowTowerR(false, null);
	}

	public void ForceMove(Vector3 _pos)
	{
		try
		{
			if (Time.time >= this.last + HeroInfo.GetInstance().posCD)
			{
				if (PosCD.inst)
				{
					PosCD.inst.NewTime();
				}
				this.last = Time.time;
				if (this.qiZi == null)
				{
					this.qiZi = PoolManage.Ins.GetOtherModelByName("Qizi", null);
				}
				this.qiZi.transform.position = new Vector3(_pos.x, -0.1f, _pos.z);
				this.qiZi.SetActive(true);
				MovePoint component = this.qiZi.GetComponent<MovePoint>();
				MovePoint.ChangeTaget(null);
				component.type = 0;
				if (this.Tanks_Attack.Count > 0 && Time.time - this.lastPersonSoundTime > 0.5f)
				{
					this.lastPersonSoundTime = Time.time;
					int num = UnityEngine.Random.Range(0, 10);
					if (num > 5)
					{
						string moveAudio = SenceManager.inst.GetMoveAudio(SenceManager.inst.attackingName);
						AudioManage.inst.PlayAuido(moveAudio, false);
					}
				}
				this.RefreshPos(_pos, false);
				if (FightPanelManager.inst != null && FightPanelManager.inst.TankTeamOperation_inst() != null)
				{
					if (FightPanelManager.inst.TankTeamOperation_inst().TankTeamOperationOpen)
					{
						int num2 = 0;
						for (int i = 0; i < FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam.Count; i++)
						{
							if (FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i] != null)
							{
								if (!T_TankAIManager.inst.On_Off)
								{
									GameObject lineTank = FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].GetLineTank();
									LineRenderer component2 = lineTank.GetComponent<LineRenderer>();
									component2.SetPosition(0, new Vector3(FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].tr.position.x, 0.5f, FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].tr.position.z));
									component2.SetPosition(1, new Vector3(this.movePoses[i].x, 0.5f, this.movePoses[i].z));
									lineTank.renderer.material.color = Color.green;
								}
								FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].ForceMoving(this.movePoses[i], _pos, 1f);
							}
						}
						if (T_TankAIManager.inst.On_Off)
						{
							if (num2 == 0)
							{
								num2 = 999;
							}
							T_TankAIManager.inst.SetAIOrder(_pos, this.movePoses[0], true, null, num2);
						}
					}
					else
					{
						for (int j = 0; j < this.Tanks_Attack.Count; j++)
						{
							if (this.Tanks_Attack[j] != null && !T_TankAIManager.inst.On_Off)
							{
								GameObject lineTank2 = this.Tanks_Attack[j].GetLineTank();
								LineRenderer component3 = lineTank2.GetComponent<LineRenderer>();
								component3.SetPosition(0, new Vector3(this.Tanks_Attack[j].tr.position.x, 0.5f, this.Tanks_Attack[j].tr.position.z));
								component3.SetPosition(1, new Vector3(this.movePoses[j].x, 0.5f, this.movePoses[j].z));
								lineTank2.renderer.material.color = Color.green;
								this.Tanks_Attack[j].ForceMoving(this.movePoses[j], _pos, 1f);
							}
						}
						if (T_TankAIManager.inst.On_Off)
						{
							T_TankAIManager.inst.SetAIOrder(_pos, this.movePoses[0], true, null, 0);
						}
					}
				}
				else
				{
					for (int k = 0; k < this.Tanks_Attack.Count; k++)
					{
						if (this.Tanks_Attack[k] != null)
						{
							GameObject lineTank3 = this.Tanks_Attack[k].GetLineTank();
							LineRenderer component4 = lineTank3.GetComponent<LineRenderer>();
							component4.SetPosition(0, new Vector3(this.Tanks_Attack[k].tr.position.x, 0.5f, this.Tanks_Attack[k].tr.position.z));
							component4.SetPosition(1, new Vector3(this.movePoses[k].x, 0.5f, this.movePoses[k].z));
							lineTank3.renderer.material.color = Color.green;
							this.Tanks_Attack[k].ForceMoving(this.movePoses[k], _pos, 1f);
						}
					}
					AudioManage.inst.PlayAuido(this.GetMoveAudio(this.moveAudioName), false);
				}
				SenceManager.inst.RefreshPath();
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.ToString());
		}
	}

	public string GetMoveAudio(List<string> audioNameList)
	{
		return audioNameList[UnityEngine.Random.Range(0, audioNameList.Count)];
	}

	public void ForceMoveRandomEventBox(Vector3 _pos)
	{
		if (Time.time < this.last + HeroInfo.GetInstance().posCD)
		{
			return;
		}
		if (PosCD.inst)
		{
			PosCD.inst.NewTime();
		}
		this.last = Time.time;
		this.RefreshPos(_pos, false);
		for (int i = 0; i < this.Tanks_Attack.Count; i++)
		{
			if (this.Tanks_Attack[i] != null)
			{
				this.Tanks_Attack[i].ForceMoving(this.movePoses[i], _pos, 1f);
			}
		}
		SenceManager.inst.RefreshPath();
	}

	public Vector3 GetVPos(T_TankAbstract thisTank, float r, Vector3 pos)
	{
		int num = this.Radiuses.IndexOf(r - SenceManager.GetVPosDistance);
		Vector3 vector = Vector3.zero;
		for (int i = num; i < this.AttrackPoint.Count; i++)
		{
			vector = (from p in this.AttrackPoint[i]
			where this.posUsedList.IndexOf(p) < 0
			orderby Vector3.Distance(thisTank.tr.position, pos + p)
			select p).FirstOrDefault<Vector3>();
			if (vector != Vector3.zero)
			{
				break;
			}
		}
		if (vector == Vector3.zero)
		{
			LogManage.Log("坦克没有找到攻击位置点");
		}
		else
		{
			this.posUsedList.Add(vector);
		}
		return vector;
	}

	public void RefreshPos(Vector3 pos, bool attrack)
	{
		if (attrack)
		{
			this.posUsedList.Clear();
			for (int i = 0; i < this.Tanks_Attack.Count; i++)
			{
				float shootMaxRadius = this.Tanks_Attack[i].CharacterBaseFightInfo.ShootMaxRadius;
				Vector3 vPos = this.GetVPos(this.Tanks_Attack[i], shootMaxRadius, pos);
				this.movePoses[i] = pos + vPos;
			}
		}
		else
		{
			this.rowList.Clear();
			float num = 0f;
			int num2 = -1;
			for (int j = 0; j < this.Tanks_Attack.Count; j++)
			{
				if (num == this.Tanks_Attack[j].CharacterBaseFightInfo.ShootMaxRadius)
				{
					if (num2 >= 0 && num2 < this.rowList.Count && this.rowList[num2] < 6)
					{
						List<int> list;
						List<int> expr_E2 = list = this.rowList;
						int num3;
						int expr_E7 = num3 = num2;
						num3 = list[num3];
						expr_E2[expr_E7] = num3 + 1;
					}
					else
					{
						this.rowList.Add(1);
						num2++;
					}
				}
				else
				{
					this.rowList.Add(1);
					num2++;
					num = this.Tanks_Attack[j].CharacterBaseFightInfo.ShootMaxRadius;
				}
			}
			for (int k = 0; k < this.rowList.Count; k++)
			{
				int startIndex = this.GetStartIndex(k);
				if (this.rowList[k] % 2 == 0)
				{
					for (int l = 0; l < this.rowList[k] / 2; l++)
					{
						this.movePoses[startIndex + l].x = pos.x + (float)k * this.colSpan;
						this.movePoses[startIndex + l].y = pos.y;
						this.movePoses[startIndex + l].z = pos.z - (float)(this.rowList[k] / 2 - 1 - l) * this.rowSpan - this.rowSpan / 2f;
					}
					for (int m = this.rowList[k] / 2; m < this.rowList[k]; m++)
					{
						this.movePoses[startIndex + m].x = pos.x + (float)k * this.colSpan;
						this.movePoses[startIndex + m].y = pos.y;
						this.movePoses[startIndex + m].z = pos.z + (float)(m - this.rowList[k] / 2) * this.rowSpan + this.rowSpan / 2f;
					}
				}
				else
				{
					for (int n = 0; n < this.rowList[k]; n++)
					{
						this.movePoses[startIndex + n].x = pos.x + (float)k * this.colSpan;
						this.movePoses[startIndex + n].y = pos.y;
						this.movePoses[startIndex + n].z = pos.z - (float)(this.rowList[k] / 2 - n) * this.rowSpan;
					}
				}
			}
		}
	}

	private float GetX(Vector3 A, Vector3 B, Vector3 M)
	{
		float num = (A.x - B.x) / (B.z - A.z);
		return M.x / Mathf.Sqrt(num * num + 1f);
	}

	private Vector3 GetMainBuildingPos()
	{
		for (int i = 0; i < this.towers.Count; i++)
		{
			if (UnitConst.GetInstance().buildingConst[this.towers[i].index].secType == 1)
			{
				return this.towers[i].tr.position;
			}
		}
		return Vector3.zero;
	}

	private float GetZ(Vector3 A, Vector3 B, Vector3 M)
	{
		float num = (A.x - B.x) / (B.z - A.z);
		float num2 = M.x / num * Mathf.Sqrt(num * num + 1f);
		return num2 + M.z;
	}

	private int GetStartIndex(int index)
	{
		int num = 0;
		for (int i = 0; i < index; i++)
		{
			num += this.rowList[i];
		}
		return num;
	}

	public void ForceAttack(Character tar)
	{
		if (Time.time < this.last + HeroInfo.GetInstance().posCD)
		{
			return;
		}
		if (PosCD.inst)
		{
			PosCD.inst.NewTime();
		}
		this.last = Time.time;
		if (this.qiZi == null)
		{
			this.qiZi = PoolManage.Ins.GetOtherModelByName("Qizi", null);
		}
		this.qiZi.transform.position = new Vector3(tar.tr.position.x, -0.1f, tar.tr.position.z);
		if (tar.roleType == Enum_RoleType.tower && UnitConst.GetInstance().buildingConst[tar.index].secType == 9)
		{
			this.qiZi.SetActive(true);
			this.ForceMove(tar.tr.position);
			return;
		}
		MovePoint.ChangeTaget(tar.GetComponent<Character>());
		MovePoint component = this.qiZi.GetComponent<MovePoint>();
		component.type = 1;
		this.RefreshPos(tar.tr.position, true);
		if (FightPanelManager.inst && FightPanelManager.inst.TankTeamOperation_inst() != null)
		{
			if (FightPanelManager.inst.TankTeamOperation_inst().TankTeamOperationOpen)
			{
				int num = 0;
				for (int i = 0; i < FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam.Count; i++)
				{
					if (FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i] != null)
					{
						if (!T_TankAIManager.inst.On_Off)
						{
							GameObject lineTank = FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].GetLineTank();
							LineRenderer component2 = lineTank.GetComponent<LineRenderer>();
							component2.SetPosition(0, new Vector3(FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].tr.position.x, 0.5f, FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].tr.position.z));
							component2.SetPosition(1, new Vector3(tar.tr.position.x, 0.5f, tar.tr.position.z));
							lineTank.renderer.material.color = Color.red;
						}
						FightPanelManager.inst.TankTeamOperation_inst().ChooseTankTeam[i].ForceAttack(tar.tr, this.movePoses[i]);
					}
				}
				if (T_TankAIManager.inst.On_Off)
				{
					if (num == 0)
					{
						num = 999;
					}
					T_TankAIManager.inst.SetAIOrder(tar.tr.position, this.movePoses[0], false, tar.tr, num);
				}
			}
			else
			{
				for (int j = 0; j < this.Tanks_Attack.Count; j++)
				{
					if (this.Tanks_Attack[j] != null && !T_TankAIManager.inst.On_Off)
					{
						GameObject lineTank2 = this.Tanks_Attack[j].GetLineTank();
						LineRenderer component3 = lineTank2.GetComponent<LineRenderer>();
						component3.SetPosition(0, new Vector3(this.Tanks_Attack[j].tr.position.x, 0.5f, this.Tanks_Attack[j].tr.position.z));
						component3.SetPosition(1, new Vector3(tar.tr.position.x, 0.5f, tar.tr.position.z));
						lineTank2.renderer.material.color = Color.red;
						this.Tanks_Attack[j].ForceAttack(tar.tr, this.movePoses[j]);
					}
				}
				if (T_TankAIManager.inst.On_Off)
				{
					T_TankAIManager.inst.SetAIOrder(tar.tr.position, this.movePoses[0], false, tar.tr, 0);
				}
			}
		}
		else
		{
			for (int k = 0; k < this.Tanks_Attack.Count; k++)
			{
				if (this.Tanks_Attack[k] != null)
				{
					GameObject lineTank3 = this.Tanks_Attack[k].GetLineTank();
					LineRenderer component4 = lineTank3.GetComponent<LineRenderer>();
					component4.SetPosition(0, new Vector3(this.Tanks_Attack[k].tr.position.x, 0.5f, this.Tanks_Attack[k].tr.position.z));
					component4.SetPosition(1, new Vector3(tar.tr.position.x, 0.5f, tar.tr.position.z));
					lineTank3.renderer.material.color = Color.red;
					this.Tanks_Attack[k].ForceAttack(tar.tr, this.movePoses[k]);
				}
			}
		}
		SenceManager.inst.RefreshPath();
	}

	public void ForceFindAttack(long id)
	{
		for (int i = 0; i < this.towers.Count; i++)
		{
			if (this.towers[i].id == id)
			{
				this.ForceAttack(this.towers[i]);
			}
		}
	}

	public void ShowTowerR(bool show, T_Tower tower)
	{
		if (show)
		{
			this.curTower = tower;
			this.towerR.ShowTowerR(tower);
		}
		else
		{
			if (UIManager.curState != SenceState.InBuild)
			{
				this.curTower = null;
			}
			this.towerR.Hiden();
		}
	}

	public void RemoveTempBuilding()
	{
		if (this.tempTower)
		{
			if (this.tempTower.index == 90)
			{
				this.DesWallconnect(this.tempTower);
			}
			UnityEngine.Object.Destroy(this.tempTower.gameObject);
			UIManager.inst.ResetSenceState(SenceState.Home);
		}
	}

	public void SurTempBuilding()
	{
		int num = Mathf.RoundToInt(this.curTower.tr.localPosition.x);
		int num2 = Mathf.RoundToInt(this.curTower.tr.localPosition.z);
		CalcMoneyHandler.CSCalcMoney(5, 0, InfoMgr.PosGetMapIdx(this.curTower.row, this.curTower.num), (long)this.curTower.index, this.curTower.index, 0, new Action<bool, int>(this.CalcMoneyCallBack));
	}

	private void CalcMoneyCallBack(bool isBuy, int money)
	{
		if (isBuy)
		{
			if (HeroInfo.GetInstance().playerRes.RMBCoin < money)
			{
				ShopPanelManage.ShowHelp_NoRMB(delegate
				{
					SenceManager.inst.RemoveTempBuilding();
				}, delegate
				{
					SenceManager.inst.RemoveTempBuilding();
					if (CameraControl.inst)
					{
						CameraControl.inst.ChangeCameraBuildingState(false);
					}
				});
				return;
			}
			BuildingHandler.CG_BuildingAddStart(InfoMgr.PosGetMapIdx(this.tempTower.row, this.tempTower.num), this.tempTower.index, money, this.tempTower);
		}
		else
		{
			SenceManager.inst.RemoveTempBuilding();
			if (CameraControl.inst)
			{
				CameraControl.inst.ChangeCameraBuildingState(false);
			}
		}
	}

	[DebuggerHidden]
	public IEnumerator ClearTankAndTowers()
	{
		SenceManager.<ClearTankAndTowers>c__Iterator22 <ClearTankAndTowers>c__Iterator = new SenceManager.<ClearTankAndTowers>c__Iterator22();
		<ClearTankAndTowers>c__Iterator.<>f__this = this;
		return <ClearTankAndTowers>c__Iterator;
	}

	public GameObject CreateLandArmy(Vector3 pos)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.position = pos;
		GameTools.GetCompentIfNoAddOne<LandArmy_Guid>(gameObject);
		return gameObject;
	}
}
