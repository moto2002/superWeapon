using msg;
using SimpleFramework.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class T_Tower : Character
{
	public enum Enum_GridPlaneDisplayType
	{
		canmove = 1,
		helpinfo,
		nomove,
		nodisplay
	}

	public enum TowerBuildingEnum
	{
		Normal = 1,
		InBuilding,
		BuildingEnd
	}

	public TowerState state = TowerState.normal;

	public bool temp;

	public bool towerBuild;

	public int row;

	public int num;

	public int posIdx = -1;

	public int TowerUpdateType;

	[HideInInspector]
	public float ShootRadius;

	public int type;

	public int secType;

	public int bulletType;

	public BoxCollider bodyForSelect;

	public GameObject topObj;

	public GameObject bottomObj;

	public GameObject leftObj;

	public GameObject rightObj;

	public T_Tower topWall;

	public T_Tower bottomWall;

	public T_Tower leftWall;

	public T_Tower rightWall;

	public Vector2 WallToPos;

	public float lastAttack;

	public float lastShoot;

	public float shootCD = 2f;

	public T_Info m_lifeInfo;

	protected float lastPress;

	protected T_InfoPro m_proInfo;

	public SpyInfo spyTowerInfo;

	public DateTime finishTime;

	public TowerGrid[] towerRows;

	public bool canBuild;

	public Transform towerGridPlane;

	public Transform towerGridPlane_Attack;

	protected bool towerMoved;

	protected GameObject dieRes;

	protected GameObject brokenTower;

	protected GameObject effectRes;

	protected float lastAngle;

	protected int n = 1;

	protected float maBiCdTimes;

	public Transform dizuo;

	public Transform xuanZhong;

	protected float doRenjuCD = 0.1f;

	[HideInInspector]
	public Transform updateTittle;

	public float HeadRotationSpeed;

	protected bool isHeadRotainon;

	public int protductNum;

	public int tankMaxNum;

	public int tankIndex;

	public int tanksNum;

	public string myTanksBodyID;

	public List<GameObject> myTanks = new List<GameObject>();

	public bool isChoose = true;

	public GameObject xuetiao;

	public List<GameObject> xuetiaoList = new List<GameObject>();

	public static int xuetiaoNum = 20;

	private float newDamage;

	private bool isTexiao = true;

	private Body_Model HalfBloodEffect;

	public GameObject fangyuzhao;

	public bool isfangyuzhao;

	private T_TowerSelectState t_TowerSelectState;

	private T_TowerFightingState t_TowerFightingState;

	public T_TowerFightingState.TowerFightingStates towerFightState = T_TowerFightingState.TowerFightingStates.Idle;

	public GameObject ArmyCDInfo;

	public GameObject NoElectiony;

	public TimeTittleBtn BuilindingCDInfo;

	public ArmyTitleShow ArmyTitleNew;

	public BuffRuntime MyBuffRuntime;

	public NoelectricityPowManager nolectricityPow;

	public ResouceTittleBtn resTip;

	public bool isDarg;

	public bool isNotMove;

	private bool selected;

	public Transform biankuang;

	private float resSpeed_ByStep_Ele_Tech_Vip;

	private DieBall IdleEffect;

	[HideInInspector]
	public bool isCanDisplayInfoBtn = true;

	private Body_Model resource3DModel;

	public bool isMaxNum;

	private float timeSet = -10f;

	private string personSound = string.Empty;

	private float lastPersonSoundTime = -10f;

	private Color GreenGridPlane = new Color(0.03529412f, 0.8666667f, 0.007843138f);

	public List<T_Tower> Walltower_list = new List<T_Tower>();

	public GameObject Logo;

	private BaseFightInfo nextLVBaseFightInfo;

	public Body_Model mining_Car;

	public BuildingDes _BuildingDes;

	public Dictionary<ResType, float> CurRes_Predat = new Dictionary<ResType, float>();

	public static bool isTowerBom = true;

	public static bool isBomEnd;

	private bool UnActivateDefenceTank = true;

	private List<Character> AllTargetBySanShe = new List<Character>();

	[HideInInspector]
	public DieBall FightEffect;

	public Dictionary<Vector3, T_TankAbstract> posUsedList = new Dictionary<Vector3, T_TankAbstract>();

	public bool isDianciFight;

	private float dianciTime;

	private float dianciFightTime = 3f;

	public List<Transform> BuildBasic = new List<Transform>();

	public List<Material> BuildBasic_Material = new List<Material>();

	public List<Shader> BuildBasic_Shader = new List<Shader>();

	public List<Animation> BuildBasic_Animation = new List<Animation>();

	public List<ParticleSystem> BuildBasic_ParticleSystem = new List<ParticleSystem>();

	public bool BuildFrozen;

	private float frozen_level;

	private float frozen_on_time;

	private float frozen_time;

	private float frozen_off_time;

	private int layer_out;

	private bool animation_Play;

	public bool NoPower;

	private Color color_A_0;

	private Color color_B_0;

	private Color color_C_0;

	private int CBA_no;

	private bool CBA_getcolor;

	public Color CBA_colorA_basic;

	public Color CBA_colorB_basic;

	public Color CBA_colorC_basic;

	public bool WallRedPiece = true;

	public bool WallRedPiece0;

	public GameObject RedPiece;

	public GameObject RedPiece1;

	private bool mine_boom;

	public Body_Model Choose_Effect;

	public Body_Model WallChoosseEffect;

	private DieBall DieEffect;

	private GameObject peibinging;

	private TimeTittleBtn peibingTime;

	private List<KVStruct> ThisArmyFunced = new List<KVStruct>();

	public Dictionary<int, List<ParadeTank>> ParadeTanks = new Dictionary<int, List<ParadeTank>>();

	public GameObject[] AllParadeTanks_Pos = new GameObject[16];

	private ParadeTank paradeTank_Commander;

	private GameObject bubing;

	private Body_Model bubingJinatou;

	private bool isFirst = true;

	public T_Tower.TowerBuildingEnum buildingState;

	public static event Action ClickTowerSendMessage
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			T_Tower.ClickTowerSendMessage = (Action)Delegate.Combine(T_Tower.ClickTowerSendMessage, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			T_Tower.ClickTowerSendMessage = (Action)Delegate.Remove(T_Tower.ClickTowerSendMessage, value);
		}
	}

	public float MaBiCdTimes
	{
		set
		{
			this.maBiCdTimes = Time.time + value;
			this.lastShoot += value;
			this.lastAttack += value;
		}
	}

	public T_TowerSelectState T_TowerSelectState
	{
		get
		{
			if (this.t_TowerSelectState == null)
			{
				if (this.tr.Find("towerSelectState"))
				{
					this.t_TowerSelectState = GameTools.GetCompentIfNoAddOne<T_TowerSelectState>(this.tr.Find("towerSelectState").gameObject);
				}
				else
				{
					GameObject gameObject = NGUITools.AddChild(this.ga);
					gameObject.name = "towerSelectState";
					this.t_TowerSelectState = gameObject.AddComponent<T_TowerSelectState>();
				}
			}
			return this.t_TowerSelectState;
		}
	}

	public T_TowerFightingState T_TowerFightingState
	{
		get
		{
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 8 || UnitConst.GetInstance().buildingConst[this.index].secType == 9)
			{
				if (this.t_TowerFightingState == null)
				{
					if (this.tr.Find("towerFightState"))
					{
						this.t_TowerFightingState = GameTools.GetCompentIfNoAddOne<T_TowerFightingState>(this.tr.Find("towerFightState").gameObject);
					}
					else
					{
						GameObject gameObject = NGUITools.AddChild(this.ga);
						gameObject.name = "towerFightState";
						this.t_TowerFightingState = gameObject.AddComponent<T_TowerFightingState>();
					}
				}
				return this.t_TowerFightingState;
			}
			return null;
		}
	}

	public float ResSpeed_ByStep_Ele_Tech_Vip
	{
		get
		{
			float num;
			if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count > 0)
			{
				num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum * (1f + (float)UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].output * 0.01f);
			}
			else
			{
				num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum;
			}
			num *= HUDTextTool.ResProduceSpeedByPower;
			switch (UnitConst.GetInstance().GetBuildingResType(this.index))
			{
			case ResType.石油:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产量);
				break;
			case ResType.钢铁:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产量);
				break;
			case ResType.稀矿:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产量);
				break;
			}
			VipConst.Enum_VipRight enum_VipRight = VipConst.Enum_VipRight.无;
			switch (UnitConst.GetInstance().buildingConst[this.index].outputType)
			{
			case ResType.金币:
				enum_VipRight = VipConst.Enum_VipRight.金币产量;
				break;
			case ResType.石油:
				enum_VipRight = VipConst.Enum_VipRight.石油产量;
				break;
			case ResType.钢铁:
				enum_VipRight = VipConst.Enum_VipRight.钢铁产量;
				break;
			case ResType.稀矿:
				enum_VipRight = VipConst.Enum_VipRight.稀矿产量;
				break;
			}
			if (SenceInfo.curMap.IsMyMap)
			{
				num += (float)VipConst.GetVipAddtion(num, HeroInfo.GetInstance().vipData.VipLevel, enum_VipRight);
			}
			else
			{
				num += (float)VipConst.GetVipAddtion(num, SenceInfo.SpyPlayerInfo.vip, enum_VipRight);
			}
			return num;
		}
	}

	public float ResSpeed_ByStep_Ele_Tech_Vip_NextLV
	{
		get
		{
			float num;
			if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count > 0)
			{
				num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv + 1].outputNum * (1f + (float)UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].output * 0.01f);
			}
			else
			{
				num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv + 1].outputNum;
			}
			num *= HUDTextTool.ResProduceSpeedByPower;
			switch (UnitConst.GetInstance().GetBuildingResType(this.index))
			{
			case ResType.石油:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产量);
				break;
			case ResType.钢铁:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产量);
				break;
			case ResType.稀矿:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产量);
				break;
			}
			VipConst.Enum_VipRight enum_VipRight = VipConst.Enum_VipRight.无;
			switch (UnitConst.GetInstance().buildingConst[this.index].outputType)
			{
			case ResType.金币:
				enum_VipRight = VipConst.Enum_VipRight.金币产量;
				break;
			case ResType.石油:
				enum_VipRight = VipConst.Enum_VipRight.石油产量;
				break;
			case ResType.钢铁:
				enum_VipRight = VipConst.Enum_VipRight.钢铁产量;
				break;
			case ResType.稀矿:
				enum_VipRight = VipConst.Enum_VipRight.稀矿产量;
				break;
			}
			if (SenceInfo.curMap.IsMyMap)
			{
				num += (float)VipConst.GetVipAddtion(num, HeroInfo.GetInstance().vipData.VipLevel, enum_VipRight);
			}
			else
			{
				num += (float)VipConst.GetVipAddtion(num, SenceInfo.SpyPlayerInfo.vip, enum_VipRight);
			}
			return num;
		}
	}

	public float ResSpeed_No_Ele_Tech_Vip
	{
		get
		{
			float result;
			if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count > 0)
			{
				result = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum * (1f + (float)UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].output * 0.01f);
			}
			else
			{
				result = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum;
			}
			return result;
		}
	}

	public float ResMaxLimit_ProdByTech
	{
		get
		{
			return this.ResMaxLimit_ByTech(UnitConst.GetInstance().GetBuildingResType(this.index));
		}
	}

	public bool IsCanCollectResource
	{
		get
		{
			return this.resource3DModel && this.resource3DModel.ga.activeSelf;
		}
	}

	public BaseFightInfo NextLVBaseFightInfo
	{
		get
		{
			return this.nextLVBaseFightInfo;
		}
	}

	public string BodyName
	{
		get
		{
			if (UnitConst.GetInstance().buildingConst.Length <= this.index || UnitConst.GetInstance().buildingConst[this.index].lvInfos.Count <= this.lv)
			{
				UnityEngine.Debug.LogError(string.Format("此建筑有问题·  · ·Index：{0} Lv{2} 当前地图mapIndex：{1}  ID:{3}", new object[]
				{
					this.index,
					SenceInfo.curMap.mapIndex,
					this.lv,
					SenceInfo.curMap.ID
				}));
				return string.Empty;
			}
			int num = this.secType;
			if (num != 2)
			{
				if (num != 3)
				{
					if (num != 8)
					{
						return UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].bodyID;
					}
					if (UnitConst.GetInstance().buildingConst[this.index].UpdateStarInfos.Count == 0)
					{
						return UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].bodyID;
					}
					if (UnitConst.GetInstance().buildingConst[this.index].UpdateStarInfos.Count <= this.star)
					{
						UnityEngine.Debug.LogError(string.Format("此建筑有问题·  · ·Index：{0} upGrade{2} 当前地图mapIndex：{1}  ID:{3}", new object[]
						{
							this.index,
							SenceInfo.curMap.mapIndex,
							this.star,
							SenceInfo.curMap.ID
						}));
						return string.Empty;
					}
					return UnitConst.GetInstance().buildingConst[this.index].UpdateStarInfos[this.star].bodyName;
				}
				else
				{
					if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count == 0)
					{
						return UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].bodyID;
					}
					if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count <= this.star)
					{
						UnityEngine.Debug.LogError(string.Format("此建筑有问题·  · ·Index：{0} upGrade{2} 当前地图mapIndex：{1}  ID:{3}", new object[]
						{
							this.index,
							SenceInfo.curMap.mapIndex,
							this.star,
							SenceInfo.curMap.ID
						}));
						return string.Empty;
					}
					return UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].bodyName;
				}
			}
			else
			{
				if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count == 0)
				{
					return UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].bodyID;
				}
				if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count <= this.star)
				{
					UnityEngine.Debug.LogError(string.Format("此建筑有问题·  · ·Index：{0} upGrade{2} 当前地图mapIndex：{1}  ID:{3}", new object[]
					{
						this.index,
						SenceInfo.curMap.mapIndex,
						this.star,
						SenceInfo.curMap.ID
					}));
					return string.Empty;
				}
				return UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].bodyName;
			}
		}
	}

	public string GetTextTips(T_InfoTextType tipsType)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string value = string.Empty;
		string value2 = string.Empty;
		switch (tipsType)
		{
		case T_InfoTextType.金币产量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("金币产量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResSpeed_ByStep_Ele_Tech_Vip);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].output));
				stringBuilder.Append("[-]\n\t");
			}
			if (SenceManager.inst.MapElectricity != SenceManager.ElectricityEnum.电力充沛)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [FF0000]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从电力", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				string arg = string.Empty;
				switch (SenceManager.inst.MapElectricity)
				{
				case SenceManager.ElectricityEnum.电力不足:
					arg = ((1f - UnitConst.GetInstance().ElectricityCont[2].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.严重不足:
					arg = ((1f - UnitConst.GetInstance().ElectricityCont[3].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.电力瘫痪:
					arg = ((1f - UnitConst.GetInstance().ElectricityCont[4].actualReduce) * 100f).ToString();
					break;
				}
				stringBuilder.Append(string.Format("-{0}%", arg));
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.金币产量);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.金币储量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("金币储量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResMaxLimit_ProdByTech);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[UnitConst.GetInstance().GetBuildingResType(this.index)]);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].outlimit));
				stringBuilder.Append("[-]\n\t");
			}
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 2)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.仓库存储上限);
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 3)
			{
				switch (UnitConst.GetInstance().GetBuildingResType(this.index))
				{
				case ResType.石油:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产出上限);
					break;
				case ResType.钢铁:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产出上限);
					break;
				case ResType.稀矿:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产出上限);
					break;
				}
			}
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.石油产量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("石油产量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResSpeed_ByStep_Ele_Tech_Vip);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].output));
				stringBuilder.Append("[-]\n\t");
			}
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产量);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			if (SenceManager.inst.MapElectricity != SenceManager.ElectricityEnum.电力充沛)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [FF0000]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从电力", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				string arg2 = string.Empty;
				switch (SenceManager.inst.MapElectricity)
				{
				case SenceManager.ElectricityEnum.电力不足:
					arg2 = ((1f - UnitConst.GetInstance().ElectricityCont[2].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.严重不足:
					arg2 = ((1f - UnitConst.GetInstance().ElectricityCont[3].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.电力瘫痪:
					arg2 = ((1f - UnitConst.GetInstance().ElectricityCont[4].actualReduce) * 100f).ToString();
					break;
				}
				stringBuilder.Append(string.Format("-{0}%", arg2));
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.石油产量);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.石油储量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("石油储量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResMaxLimit_ProdByTech);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[UnitConst.GetInstance().GetBuildingResType(this.index)]);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].outlimit));
				stringBuilder.Append("[-]\n\t");
			}
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 2)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.仓库存储上限);
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 3)
			{
				switch (UnitConst.GetInstance().GetBuildingResType(this.index))
				{
				case ResType.石油:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产出上限);
					break;
				case ResType.钢铁:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产出上限);
					break;
				case ResType.稀矿:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产出上限);
					break;
				}
			}
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.钢铁产量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("钢铁产量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResSpeed_ByStep_Ele_Tech_Vip);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].output));
				stringBuilder.Append("[-]\n\t");
			}
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产量);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			if (SenceManager.inst.MapElectricity != SenceManager.ElectricityEnum.电力充沛)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [FF0000]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从电力", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				string arg3 = string.Empty;
				switch (SenceManager.inst.MapElectricity)
				{
				case SenceManager.ElectricityEnum.电力不足:
					arg3 = ((1f - UnitConst.GetInstance().ElectricityCont[2].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.严重不足:
					arg3 = ((1f - UnitConst.GetInstance().ElectricityCont[3].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.电力瘫痪:
					arg3 = ((1f - UnitConst.GetInstance().ElectricityCont[4].actualReduce) * 100f).ToString();
					break;
				}
				stringBuilder.Append(string.Format("-{0}%", arg3));
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.钢铁产量);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.钢铁储量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("钢铁储量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResMaxLimit_ProdByTech);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[UnitConst.GetInstance().GetBuildingResType(this.index)]);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].outlimit));
				stringBuilder.Append("[-]\n\t");
			}
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 2)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.仓库存储上限);
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 3)
			{
				switch (UnitConst.GetInstance().GetBuildingResType(this.index))
				{
				case ResType.石油:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产出上限);
					break;
				case ResType.钢铁:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产出上限);
					break;
				case ResType.稀矿:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产出上限);
					break;
				}
			}
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.稀土产量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("稀土产量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResSpeed_ByStep_Ele_Tech_Vip);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputNum);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].output));
				stringBuilder.Append("[-]\n\t");
			}
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产量);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			if (SenceManager.inst.MapElectricity != SenceManager.ElectricityEnum.电力充沛)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [FF0000]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从电力", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				string arg4 = string.Empty;
				switch (SenceManager.inst.MapElectricity)
				{
				case SenceManager.ElectricityEnum.电力不足:
					arg4 = ((1f - UnitConst.GetInstance().ElectricityCont[2].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.严重不足:
					arg4 = ((1f - UnitConst.GetInstance().ElectricityCont[3].actualReduce) * 100f).ToString();
					break;
				case SenceManager.ElectricityEnum.电力瘫痪:
					arg4 = ((1f - UnitConst.GetInstance().ElectricityCont[4].actualReduce) * 100f).ToString();
					break;
				}
				stringBuilder.Append(string.Format("-{0}%", arg4));
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.稀矿产量);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.稀矿储量:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("稀矿储量", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((int)this.ResMaxLimit_ProdByTech);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[UnitConst.GetInstance().GetBuildingResType(this.index)]);
			if (this.star > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从升阶", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].outlimit));
				stringBuilder.Append("[-]\n\t");
			}
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 2)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.仓库存储上限);
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 3)
			{
				switch (UnitConst.GetInstance().GetBuildingResType(this.index))
				{
				case ResType.石油:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产出上限);
					break;
				case ResType.钢铁:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产出上限);
					break;
				case ResType.稀矿:
					value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产出上限);
					break;
				}
			}
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.生命值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("生命值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.life);
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 3 || UnitConst.GetInstance().buildingConst[this.index].secType == 2)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.资源建筑生命值);
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 20)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.围墙生命值);
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 8)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.防御塔生命值);
			}
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].fightInfo.life);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			if (this.strengthLevel > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从强化", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().GetUpPropertyByStrengthLV(this.index, this.strengthLevel, 1)));
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.建筑生命值);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.攻击值:
		case T_InfoTextType.伤害值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("伤害值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.breakArmor);
			value2 = string.Empty;
			if (this.index == 20)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.地雷攻击);
			}
			else if (this.index == 21)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.高爆雷攻击);
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 8)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.防御塔攻击);
			}
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].fightInfo.breakArmor);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			if (this.strengthLevel > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从强化", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().GetUpPropertyByStrengthLV(this.index, this.strengthLevel, 2)));
				stringBuilder.Append("[-]\n\t");
			}
			value = VipConst.GetVipAddtion(HeroInfo.GetInstance().vipData.VipLevel, VipConst.Enum_VipRight.建筑攻击力);
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从VIP特权", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.防御值:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("防御值", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.defBreak);
			value2 = string.Empty;
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 8)
			{
				value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.防御塔防御力);
			}
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].fightInfo.defBreak);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			if (this.strengthLevel > 0)
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从强化", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(string.Format("+{0}%", UnitConst.GetInstance().GetUpPropertyByStrengthLV(this.index, this.strengthLevel, 3)));
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.升级消耗时间:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("升级消耗时间", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].BuildTime);
			value2 = string.Empty;
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.建筑建造升级速度);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].BasicBuildTime);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.暴击率:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("暴击率", "build"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((float)this.CharacterBaseFightInfo.crit / 100f + "%");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append((float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].fightInfo.crit / 100f + "%");
			value2 = string.Empty;
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.防御塔暴击率);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.暴击抵抗:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("暴击抵抗", "build"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.resist);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].fightInfo.resist);
			value2 = string.Empty;
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.防御塔暴抗);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.伤害范围:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("伤害范围", "build"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.hurtRadius);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.hurtRadius);
			break;
		case T_InfoTextType.额外伤害:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("额外伤害", "build"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.trueDamage);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.trueDamage);
			value2 = string.Empty;
			value2 = Technology.GetTechAddtion(HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.防御塔额外伤害);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      [00FF00]");
				stringBuilder.Append(LanguageManage.GetTextByKey("从科技", "others"));
				stringBuilder.Append(":");
				stringBuilder.Append("\n\t");
				stringBuilder.Append("      ");
				stringBuilder.Append(value2);
				stringBuilder.Append("[-]\n\t");
			}
			break;
		case T_InfoTextType.射速:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("射速", "build"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.shootSpeed);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.shootSpeed);
			break;
		case T_InfoTextType.射程:
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("射程", "build"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.ShootMaxRadius);
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(LanguageManage.GetTextByKey("从基础", "others"));
			stringBuilder.Append(":");
			stringBuilder.Append("\n\t");
			stringBuilder.Append("      ");
			stringBuilder.Append(this.CharacterBaseFightInfo.ShootMaxRadius);
			break;
		}
		return stringBuilder.ToString();
	}

	public void FixedUpdate()
	{
		if (!UnitConst.IsHaveInstance())
		{
			return;
		}
		if (!GameSetting.IsByPhsic && (UnitConst.GetInstance().buildingConst[this.index].secType == 8 || UnitConst.GetInstance().buildingConst[this.index].secType == 9) && (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.WatchVideo))
		{
			for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
			{
				if (SenceManager.inst.Tanks_Attack[i] != null)
				{
					if (base.IsCanShootByCharFlak(SenceManager.inst.Tanks_Attack[i]))
					{
						float num = Vector3.Distance(this.tr.position, SenceManager.inst.Tanks_Attack[i].tr.position) - UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Attack[i].index].size * 0.5f;
						if (num < 0f)
						{
							num = 0f;
						}
						if (num <= UnitConst.GetInstance().buildingConst[this.index].maxRadius && num >= UnitConst.GetInstance().buildingConst[this.index].minRadius)
						{
							if (UnitConst.GetInstance().buildingConst[this.index].secType == 9)
							{
								this.MineHurt();
							}
							if (!this.Targetes.Contains(SenceManager.inst.Tanks_Attack[i]))
							{
								this.Targetes.Add(SenceManager.inst.Tanks_Attack[i]);
							}
							if (this.Targetes.Count > 0 && !this.IsDie && this.towerFightState == T_TowerFightingState.TowerFightingStates.Idle)
							{
								this.T_TowerFightingState.ChangeState(T_TowerFightingState.TowerFightingStates.Searching);
							}
						}
						else if (this.Targetes.Contains(SenceManager.inst.Tanks_Attack[i]))
						{
							this.Targetes.Remove(SenceManager.inst.Tanks_Attack[i]);
						}
					}
				}
			}
			for (int j = this.Targetes.Count - 1; j >= 0; j--)
			{
				if (this.Targetes[j] == null || this.Targetes[j].IsDie)
				{
					this.Targetes.Remove(this.Targetes[j]);
				}
			}
		}
		if (UIManager.curState == SenceState.Home || UIManager.curState == SenceState.InBuild)
		{
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 6)
			{
				if (this.buildingState == T_Tower.TowerBuildingEnum.InBuilding && HeroInfo.GetInstance().armyBuildingCDTime_BuildingID == this.id && HeroInfo.GetInstance().armyBuildingCDTime > 0L && SenceManager.inst.IsCreateMapEnd && !HeroInfo.GetInstance().BuildCD.Contains(this.id))
				{
					this.buildingState = T_Tower.TowerBuildingEnum.BuildingEnd;
					this.SenndBuildCompleteNoBuyTime();
				}
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].secType == 21)
			{
				if (this.buildingState == T_Tower.TowerBuildingEnum.InBuilding && HeroInfo.GetInstance().airBuildingCDTime_BuildingID == this.id && HeroInfo.GetInstance().airBuildingCDTime > 0L && SenceManager.inst.IsCreateMapEnd && !HeroInfo.GetInstance().BuildCD.Contains(this.id))
				{
					this.buildingState = T_Tower.TowerBuildingEnum.BuildingEnd;
					this.SenndBuildCompleteNoBuyTime();
				}
			}
			else if (this.buildingState == T_Tower.TowerBuildingEnum.InBuilding && !HeroInfo.GetInstance().BuildCD.Contains(this.id) && SenceManager.inst.IsCreateMapEnd)
			{
				this.buildingState = T_Tower.TowerBuildingEnum.BuildingEnd;
				this.SenndBuildCompleteNoBuyTime();
			}
			if ((UnitConst.GetInstance().buildingConst[this.index].secType == 6 || UnitConst.GetInstance().buildingConst[this.index].secType == 21) && HeroInfo.GetInstance().IsArmyFuncingBuilding(this.id) && TimeTools.GetNowTimeSyncServerToDateTime() > TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().GetArmyFuncingDataByBuilding(this.id).key))
			{
				this.SendArmyFuncEnd();
			}
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 15 && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd != null && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime > 0L && TimeTools.GetNowTimeSyncServerToDateTime() > TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime))
			{
				this.SendArmyFuncEnd();
			}
		}
	}

	public void SendArmyFuncEnd()
	{
		if (UnitConst.GetInstance().buildingConst[this.index].secType == 15)
		{
			HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime = 0L;
			ArmyFuncHandler.CG_CSSoliderConfigureEnd(HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.soldierItemId, 2, delegate
			{
				HUDTextTool.inst.NextLuaCall("配兵完成", new object[0]);
			});
		}
		else
		{
			int itemID = (int)HeroInfo.GetInstance().GetArmyFuncingDataByBuilding(this.id).value;
			HeroInfo.GetInstance().AllArmyInfo[this.id].RemoveArmyFuncingData(HeroInfo.GetInstance().GetArmyFuncingDataByBuilding(this.id).key);
			ArmyFuncHandler.CG_CSArmyConfigureEnd(this.id, itemID, 0, delegate
			{
				HUDTextTool.inst.NextLuaCall("配兵完成", new object[0]);
			});
		}
	}

	public override void Awake()
	{
		base.Awake();
		this.bodyForSelect = this.tr.GetComponent<BoxCollider>();
		this.bodyForAttack = this.tr.Find("BodyBox").GetComponent<BoxCollider>();
		this.towerGridPlane = this.tr.Find("plane_Up");
		this.towerGridPlane_Attack = this.tr.Find("plane_Up_Attack");
		this.towerGridPlane.gameObject.layer = 0;
		this.InitBuffRuntime();
	}

	public float ResMaxLimit_ByTech(ResType resType)
	{
		float num = 0f;
		if (!UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(resType))
		{
			return num;
		}
		if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count > 0)
		{
			num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[resType] * (1f + (float)UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].outlimit * 0.01f);
		}
		else
		{
			num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[resType];
		}
		if (UnitConst.GetInstance().buildingConst[this.index].secType == 2)
		{
			num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.仓库存储上限);
		}
		else if (UnitConst.GetInstance().buildingConst[this.index].secType == 3)
		{
			switch (UnitConst.GetInstance().GetBuildingResType(this.index))
			{
			case ResType.石油:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产出上限);
				break;
			case ResType.钢铁:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产出上限);
				break;
			case ResType.稀矿:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产出上限);
				break;
			}
		}
		return num;
	}

	public float ResMaxLimit_ByTech_NextLV(ResType resType)
	{
		float num = 0f;
		if (!UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv + 1].outputLimit.ContainsKey(resType))
		{
			return num;
		}
		if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count > 0)
		{
			num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv + 1].outputLimit[resType] * (1f + (float)UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].outlimit * 0.01f);
		}
		else
		{
			num = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv + 1].outputLimit[resType];
		}
		if (UnitConst.GetInstance().buildingConst[this.index].secType == 2)
		{
			num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.仓库存储上限);
		}
		else if (UnitConst.GetInstance().buildingConst[this.index].secType == 3)
		{
			switch (UnitConst.GetInstance().GetBuildingResType(this.index))
			{
			case ResType.石油:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.石油产出上限);
				break;
			case ResType.钢铁:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.钢铁产出上限);
				break;
			case ResType.稀矿:
				num += Technology.GetTechAddtion(num, HeroInfo.GetInstance().PlayerTechnologyInfo, Technology.Enum_TechnologyProps.稀矿产出上限);
				break;
			}
		}
		return num;
	}

	public float ResMaxLimit_StoreNoByTech(ResType resType)
	{
		if (!UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(resType))
		{
			return 0f;
		}
		float result;
		if (UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos.Count > 0)
		{
			result = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[resType] * (1f + (float)UnitConst.GetInstance().buildingConst[this.index].buildGradeInfos[this.star].outlimit * 0.01f);
		}
		else
		{
			result = (float)UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit[resType];
		}
		return result;
	}

	private void InitBuffRuntime()
	{
		this.MyBuffRuntime = GameTools.GetCompentIfNoAddOne<BuffRuntime>(this.ga);
		this.MyBuffRuntime.myTower = this;
	}

	public void RefreshUpdateTittle()
	{
		if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			return;
		}
		if (UIManager.curState != SenceState.Home && UIManager.curState != SenceState.InBuild)
		{
			return;
		}
		if (SenceManager.inst.tempTower && SenceManager.inst.tempTower.Equals(this))
		{
			return;
		}
		if (UnitConst.GetInstance().buildingConst[this.index].resType < 3)
		{
			if (this.updateTittle)
			{
				this.updateTittle.gameObject.SetActive(false);
			}
			if (UnitConst.GetInstance().buildingConst[this.index].lvInfos.Count <= this.lv + 1 || this.lv <= 0)
			{
				return;
			}
			foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].resCost)
			{
				switch (current.Key)
				{
				case ResType.金币:
					if (current.Value > HeroInfo.GetInstance().playerRes.resCoin)
					{
						return;
					}
					break;
				case ResType.石油:
					if (current.Value > HeroInfo.GetInstance().playerRes.resOil)
					{
						return;
					}
					break;
				case ResType.钢铁:
					if (current.Value > HeroInfo.GetInstance().playerRes.resSteel)
					{
						return;
					}
					break;
				case ResType.稀矿:
					if (current.Value > HeroInfo.GetInstance().playerRes.resRareEarth)
					{
						return;
					}
					break;
				}
			}
			if (this.index == 1)
			{
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv + 1].needCommandLevel > HeroInfo.GetInstance().playerlevel)
				{
					return;
				}
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv + 1].needCommandLevel > HeroInfo.GetInstance().PlayerCommondLv)
			{
				return;
			}
			if (this.buildingState == T_Tower.TowerBuildingEnum.InBuilding)
			{
				return;
			}
			if (this.updateTittle)
			{
				this.updateTittle.gameObject.SetActive(true);
			}
			if (MainUIPanelManage._instance)
			{
				MainUIPanelManage._instance.SetUpdateTittleInfo(this.index);
			}
			if (this.updateTittle)
			{
				this.updateTittle.transform.localPosition = UnitConst.GetInstance().buildingConst[this.index].updateTittlePos;
			}
		}
	}

	public void HideUpdateTittle()
	{
		if (this.updateTittle)
		{
			this.updateTittle.gameObject.SetActive(false);
		}
	}

	public void Start()
	{
	}

	public void CreateDefensiveCover()
	{
		if (this.index == 62)
		{
			if (SenceManager.inst.MapElectricity == SenceManager.ElectricityEnum.电力充沛)
			{
				if (this.fangyuzhao == null)
				{
					DieBall dieBall = PoolManage.Ins.CreatEffect("dianchang_fangyu", this.tr.position, Quaternion.identity, null);
					dieBall.IsAutoDes = false;
					this.fangyuzhao = dieBall.ga;
					this.fangyuzhao.name = "mumu";
					this.fangyuzhao.transform.parent = this.tr;
					ParticleSystemRenderer[] componentsInChildren = this.fangyuzhao.GetComponentsInChildren<ParticleSystemRenderer>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						ParticleSystemRenderer particleSystemRenderer = componentsInChildren[i];
						particleSystemRenderer.maxParticleSize = 2f;
					}
				}
				else
				{
					this.fangyuzhao.SetActive(true);
				}
			}
			else if (this.fangyuzhao)
			{
				this.fangyuzhao.SetActive(false);
			}
		}
	}

	public void CreateNewXuetiao()
	{
		if (this.xuetiao != null)
		{
			for (int i = 0; i < this.xuetiaoList.Count; i++)
			{
				UnityEngine.Object.Destroy(this.xuetiaoList[i]);
			}
			this.xuetiaoList.Clear();
			UnityEngine.Object.Destroy(this.xuetiao);
			this.xuetiao = null;
		}
		this.xuetiao = (UnityEngine.Object.Instantiate(Resources.Load("Xuetiao/XTN"), Vector3.zero, Quaternion.identity) as GameObject);
		for (int j = 0; j < T_Tower.xuetiaoNum; j++)
		{
			this.xuetiaoList.Add(this.xuetiao.transform.FindChild("xt/" + (j + 1)).gameObject);
		}
		if (this.xuetiao != null)
		{
			this.xuetiao.transform.parent = this.tr;
			if (!string.IsNullOrEmpty(UnitConst.GetInstance().buildingConst[this.index].xuetiao_home))
			{
				string[] array = UnitConst.GetInstance().buildingConst[this.index].xuetiao_home.Split(new char[]
				{
					';'
				});
				string[] array2 = array[0].Split(new char[]
				{
					','
				});
				this.xuetiao.transform.localPosition = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
				string[] array3 = array[1].Split(new char[]
				{
					','
				});
				this.xuetiao.transform.localScale = new Vector3(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]));
			}
			this.xuetiao.SetActive(false);
		}
	}

	public void ReloadModelColorAndShootP()
	{
		if (this.ModelBody)
		{
			if (this.ModelBody.Red_DModel)
			{
				this.ModelBody.Red_DModel.gameObject.SetActive(false);
			}
			if (this.ModelBody.Blue_DModel)
			{
				this.ModelBody.Blue_DModel.gameObject.SetActive(false);
			}
		}
		this.AnimationControler = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ga);
		this.AnimationControler.SetAnimaInfo();
		if (SenceInfo.curMap.IsMyMap)
		{
			this.modelClore = Enum_ModelColor.Blue;
			if (this.ModelBody && this.ModelBody.BlueModel)
			{
				this.ModelBody.BlueModel.gameObject.SetActive(true);
			}
			if (this.ModelBody && this.ModelBody.RedModel)
			{
				this.ModelBody.RedModel.gameObject.SetActive(false);
			}
		}
		else
		{
			this.modelClore = Enum_ModelColor.Red;
			if (this.ModelBody && this.ModelBody.BlueModel)
			{
				this.ModelBody.BlueModel.gameObject.SetActive(false);
			}
			if (this.ModelBody && this.ModelBody.RedModel)
			{
				this.ModelBody.RedModel.gameObject.SetActive(true);
			}
		}
		if (this.ModelBody)
		{
			ShootP[] componentsInChildren = this.ModelBody.GetComponentsInChildren<ShootP>();
			this.shootPList.Clear();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].gameObject.activeInHierarchy)
				{
					this.shootPList.Add(componentsInChildren[i].transform);
				}
			}
		}
		if (this.ModelBody)
		{
			Transform[] componentsInChildren2 = this.ModelBody.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].gameObject.layer = 0;
			}
		}
		if ((this.index == 19 || this.index == 23) && this.shootPList.Count > 0)
		{
			if (this.index == 19)
			{
				if (SenceInfo.curMap.IsMyMap)
				{
					this.IdleEffect = PoolManage.Ins.CreatEffect("T_19_idle_blue", this.tr.position, this.shootPList[0].rotation, this.shootPList[0]);
				}
				else
				{
					this.IdleEffect = PoolManage.Ins.CreatEffect("T_19_idle", this.tr.position, this.shootPList[0].rotation, this.shootPList[0]);
				}
			}
			else if (this.index == 23)
			{
				this.IdleEffect = PoolManage.Ins.CreatEffect("T_23_idle", this.tr.position, this.shootPList[0].rotation, this.shootPList[0]);
			}
		}
	}

	public void OnDestyNoElectricityIcon()
	{
		if (this.NoElectiony != null)
		{
			UnityEngine.Object.Destroy(this.NoElectiony);
		}
	}

	public void OnT_Towermark(int power, int buffid)
	{
		if (UIManager.curState == SenceState.Home)
		{
			if (power == 0)
			{
				ResourcePanelManage.inst.gameObject.SetActive(true);
				if (this.index == 23 || this.index == 19)
				{
					base.AddBuffIndex(null, new int[]
					{
						buffid
					});
					if (this.IdleEffect)
					{
						this.IdleEffect.DesInPool();
					}
				}
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].electricityShow == 1 && ResourcePanelManage.inst && this.index != 90)
			{
				this.SetColorBlack(true);
			}
		}
		else if (UIManager.curState == SenceState.Attacking)
		{
			if (power == 0)
			{
				ResourcePanelManage.inst.gameObject.SetActive(true);
				if (this.index == 19 || this.index == 23)
				{
					base.AddBuffIndex(null, new int[]
					{
						buffid
					});
					if (this.IdleEffect)
					{
						this.IdleEffect.DesInPool();
					}
				}
			}
			if (UnitConst.GetInstance().buildingConst[this.index].electricityShow == 1 && UnitConst.GetInstance().buildingConst[this.index].secType != 99 && this.index != 90)
			{
				this.SetColorBlack(true);
			}
		}
	}

	public void MineHurt()
	{
		foreach (Character current in this.Targetes)
		{
			for (int i = 0; i < this.Targetes.Count; i++)
			{
				if (this.Targetes[i].charaType != this.charaType && this.Targetes[i].Tank)
				{
					this.Targetes[i].Tank.DoHurt(this.CharacterBaseFightInfo.breakArmor);
				}
			}
		}
		FightPanelManager.inst.CreatFightMessage("触发地雷！", Color.red, base.transform);
		this.DoHurt((int)base.CurLife + 50, -10L, true);
	}

	private void OnEnable()
	{
		ClientMgr.GetNet().NetDataHandler.DataChange += new DataHandler.Data_Change(this.DataHandler_DataChange);
		BuildingHandler.HomeBuildingUp += new Action<int>(this.BuildingHandler_HomeBuildingUp);
		SenceManager.inst.OnCreateMapDataEnd += new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		this.SetMiniCar();
	}

	private void SenceManager_OnCreateMapDataEnd()
	{
		this.RefreshUpdateTittle();
	}

	private void BuildingHandler_HomeBuildingUp(int obj)
	{
		this.RefreshUpdateTittle();
	}

	private void DataHandler_DataChange(int opcodeCMD)
	{
		if (opcodeCMD == 10003)
		{
			this.RefreshUpdateTittle();
		}
		if (opcodeCMD == 10116 || opcodeCMD == 10090)
		{
			this.FreshArmyFuncUITimeBehaviour();
			this.FreshArmyFuncEnd(false);
		}
	}

	public void InitBuildingOrEndState()
	{
		if (UnitConst.GetInstance().buildingConst[this.index].secType == 6)
		{
			if (SenceInfo.curMap.armyBuildingCDTime > 0L)
			{
				this.InBuilding();
			}
			else
			{
				this.BuildingEnd();
			}
		}
		else if (UnitConst.GetInstance().buildingConst[this.index].secType == 21)
		{
			if (SenceInfo.curMap.airBuildingCDTime > 0L)
			{
				this.InBuilding();
			}
			else
			{
				this.BuildingEnd();
			}
		}
		else if (SenceInfo.curMap.BuildingInCDTime.ContainsKey(this.id))
		{
			if (SenceInfo.curMap.BuildingInCDTime[this.id] > 0L)
			{
				this.InBuilding();
			}
			else
			{
				this.BuildingEnd();
			}
		}
		else
		{
			this.BuildingEnd();
		}
	}

	private void OnDisable()
	{
		if (this.HalfBloodEffect)
		{
			UnityEngine.Object.Destroy(this.HalfBloodEffect.ga);
		}
		if (this.MyBuffRuntime)
		{
			UnityEngine.Object.Destroy(this.MyBuffRuntime);
		}
		if (this.t_TowerFightingState)
		{
			UnityEngine.Object.Destroy(this.t_TowerFightingState.gameObject);
		}
		if (this.t_TowerSelectState)
		{
			UnityEngine.Object.Destroy(this.t_TowerSelectState.gameObject);
		}
		if (this.mining_Car)
		{
			UnityEngine.Object.Destroy(this.mining_Car.ga);
		}
		this.RebackShader();
		if (ClientMgr.GetNet())
		{
			ClientMgr.GetNet().NetDataHandler.DataChange -= new DataHandler.Data_Change(this.DataHandler_DataChange);
		}
		BuildingHandler.HomeBuildingUp -= new Action<int>(this.BuildingHandler_HomeBuildingUp);
		if (SenceManager.inst)
		{
			SenceManager.inst.OnCreateMapDataEnd -= new UnityAction(this.SenceManager_OnCreateMapDataEnd);
		}
	}

	public override void MouseDown()
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		this.isDarg = false;
		if (T_CommandPanelManage._instance && this.isCanDisplayInfoBtn)
		{
			T_CommandPanelManage._instance.moveTittle.GetComponent<UILabel>().color = new Color(0.7294118f, 0.921568632f, 0.9764706f);
		}
		if (SenceManager.inst.fightType == FightingType.Guard)
		{
			return;
		}
		if (SenceManager.inst.curTower != null && !SenceManager.inst.curTower.Equals(this) && UIManager.curState == SenceState.InBuild)
		{
			return;
		}
		if (SenceManager.inst.mover_Tower == this || SenceManager.inst.CurSelectTower == this)
		{
			SenceManager.inst.sameTower = true;
		}
		else
		{
			SenceManager.inst.sameTower = false;
			if (DragMgr.inst.buildDraging)
			{
				SenceManager.inst.RebackTower();
			}
		}
		if (this.isNotMove)
		{
		}
		DragMgr.inst.NewMouseDown();
		this.lastPress = Time.time;
		this.towerMoved = false;
	}

	public void HideResModel()
	{
		if (this.resource3DModel)
		{
			UnityEngine.Object.Destroy(this.resource3DModel.ga);
		}
	}

	public void CalcResShowOrNo_Heart()
	{
		if (Time.time - this.timeSet > 20f)
		{
			this.timeSet = Time.time;
			this.CalcResShowOrNo();
			return;
		}
	}

	public void CalcResShowOrNo()
	{
		if (UIManager.curState != SenceState.Home)
		{
			return;
		}
		BuildingNPC buildingNPC = null;
		if (SenceInfo.curMap.ResourceBuildingList.ContainsKey(this.id))
		{
			buildingNPC = SenceInfo.curMap.ResourceBuildingList[this.id];
		}
		if (buildingNPC == null)
		{
			return;
		}
		float num = (float)(TimeTools.GetNowTimeSyncServerToDateTime() - buildingNPC.takeTime).TotalHours * this.ResSpeed_ByStep_Ele_Tech_Vip + (float)buildingNPC.protductNum;
		if (!UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].lvInfos[buildingNPC.lv].outputLimit.ContainsKey((ResType)buildingNPC.productType))
		{
			LogManage.LogError(string.Format("建筑{0}等级{1}产量限制 不包含", UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].name, buildingNPC.lv));
		}
		else if (UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].buildGradeInfos.Count == 0)
		{
			if (num >= (float)UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].lvInfos[buildingNPC.lv].outputLimit[(ResType)buildingNPC.productType])
			{
				num = (float)UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].lvInfos[buildingNPC.lv].outputLimit[(ResType)buildingNPC.productType];
				this.isMaxNum = true;
				this.ShowResModel();
			}
			else if (num > 100f)
			{
				this.isMaxNum = false;
				this.ShowResModel();
			}
		}
		else if (num >= (float)UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].lvInfos[buildingNPC.lv].outputLimit[(ResType)buildingNPC.productType] * (1f + (float)UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].buildGradeInfos[buildingNPC.star].outlimit * 0.01f))
		{
			num = (float)UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].lvInfos[buildingNPC.lv].outputLimit[(ResType)buildingNPC.productType] * (1f + (float)UnitConst.GetInstance().buildingConst[buildingNPC.buildingIdx].buildGradeInfos[buildingNPC.star].outlimit * 0.01f);
			this.isMaxNum = true;
			this.ShowResModel();
		}
		else if (num > 100f)
		{
			this.isMaxNum = false;
			this.ShowResModel();
		}
	}

	public void ShowResModel()
	{
		if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			return;
		}
		if (this.buildingState == T_Tower.TowerBuildingEnum.InBuilding)
		{
			return;
		}
		if (!this.resource3DModel)
		{
			switch (UnitConst.GetInstance().buildingConst[this.index].outputType)
			{
			case ResType.金币:
				this.resource3DModel = PoolManage.Ins.GetModelByBundleByName("Jb", this.tr);
				break;
			case ResType.石油:
				this.resource3DModel = PoolManage.Ins.GetModelByBundleByName("Sy", this.tr);
				break;
			case ResType.钢铁:
				this.resource3DModel = PoolManage.Ins.GetModelByBundleByName("Gt", this.tr);
				break;
			case ResType.稀矿:
				this.resource3DModel = PoolManage.Ins.GetModelByBundleByName("Xk", this.tr);
				break;
			}
			if (!this.resource3DModel)
			{
				return;
			}
			this.resource3DModel.tr.position = new Vector3(this.tr.position.x, this.tr.position.y + 3f, this.tr.position.z);
			this.resource3DModel.tr.localScale = Vector3.one * 2.6f;
			this.resource3DModel.ga.AddComponent<TransRotate>();
			TransRotate component = this.resource3DModel.ga.GetComponent<TransRotate>();
			component.Bg = this.resource3DModel.tr;
			component.FuDu = 120f;
			component.xyz = TransRotate.RotateXYZ.Y;
			if (this.isMaxNum)
			{
				this.resource3DModel.ga.AddComponent<TweenPosition>();
				TweenPosition component2 = this.resource3DModel.GetComponent<TweenPosition>();
				component2.from = new Vector3(0f, 3f, 0f);
				component2.to = new Vector3(0f, 3.5f, 0f);
				component2.duration = 0.5f;
				component2.style = UITweener.Style.PingPong;
			}
		}
	}

	public void ShowYueKaModel()
	{
		if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			return;
		}
		if (this.buildingState == T_Tower.TowerBuildingEnum.InBuilding)
		{
			return;
		}
		if (!this.resource3DModel)
		{
			this.resource3DModel = PoolManage.Ins.GetModelByBundleByName("Zs", this.tr);
		}
		if (!this.resource3DModel)
		{
			return;
		}
		this.resource3DModel.tr.position = new Vector3(this.tr.position.x, this.tr.position.y + 3f, this.tr.position.z);
		this.resource3DModel.tr.localScale = Vector3.one * 2.6f;
		this.resource3DModel.ga.AddComponent<TransRotate>();
		TransRotate component = this.resource3DModel.ga.GetComponent<TransRotate>();
		component.Bg = this.resource3DModel.tr;
		component.FuDu = 120f;
		component.xyz = TransRotate.RotateXYZ.Y;
		if (this.isMaxNum)
		{
			this.resource3DModel.ga.AddComponent<TweenPosition>();
			TweenPosition component2 = this.resource3DModel.GetComponent<TweenPosition>();
			component2.from = new Vector3(0f, 3f, 0f);
			component2.to = new Vector3(0f, 3.5f, 0f);
			component2.duration = 0.5f;
			component2.style = UITweener.Style.PingPong;
		}
	}

	public override void MouseUp()
	{
		if (Camera_FingerManager.newbiLock && SkillManage.inst.ReadyUseSkill)
		{
			SkillManage.inst.Use_Skill();
			return;
		}
		if (!ButtonClick.newbiLock && Input.touchCount > 1)
		{
			return;
		}
		if (SenceManager.inst.fightType == FightingType.Guard)
		{
			return;
		}
		if (this.isDarg)
		{
			this.SetModelToTeXiaoLayer(false);
			if (SenceManager.inst.WallLineChoose)
			{
				for (int i = 0; i < this.Walltower_list.Count; i++)
				{
					this.Walltower_list[i].SetModelToTeXiaoLayer(false);
				}
			}
			this.isDarg = false;
			if (UIManager.curState != SenceState.Attacking)
			{
				AudioManage.inst.PlayAuidoBySelf_3D("clickbuild", this.ga, false, 0uL);
			}
		}
		else
		{
			if (UIManager.curState != SenceState.Attacking)
			{
				string audioName = string.Empty;
				if (UnitConst.GetInstance().buildingConst[this.index].resType == 5)
				{
					audioName = "clickrp";
				}
				else
				{
					audioName = "xuanqujianzhu";
				}
				if (Time.time - this.lastPersonSoundTime > 2f)
				{
					AudioManage.inst.PlayAuidoBySelf_3D(audioName, this.ga, false, 0uL);
					this.lastPersonSoundTime = Time.time;
					this.personSound = audioName;
				}
			}
			if (UIManager.curState == SenceState.Attacking && SenceManager.inst.Tanks_Attack.Count > 0)
			{
				string moveAudio = SenceManager.inst.GetMoveAudio(SenceManager.inst.attackingName);
				if (Time.time - this.lastPersonSoundTime > 2f)
				{
					AudioManage.inst.PlayAuidoBySelf_3D(moveAudio, this.ga, false, 0uL);
					this.lastPersonSoundTime = Time.time;
					this.personSound = moveAudio;
				}
			}
		}
		SenceManager.inst.sameTower = false;
		SenceState curState = UIManager.curState;
		switch (curState)
		{
		case SenceState.Home:
		case SenceState.WatchResIsland:
			if (UIManager.curState == SenceState.Visit)
			{
				goto IL_73E;
			}
			if (this.resource3DModel)
			{
				this.isMaxNum = false;
				ResourcePanelManage.inst.CollectResource(this, delegate
				{
					UnityEngine.Object.Destroy(this.resource3DModel.ga, 0.6f);
				});
				if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.Normal)
				{
					goto IL_73E;
				}
			}
			this.TowerInfoShow(true);
			this.BroadTowerClick();
			if ((float)this.row != this.tr.localPosition.x || (float)this.num != this.tr.localPosition.z)
			{
				if (SenceManager.inst.WallLineChoose)
				{
					for (int j = 0; j < this.Walltower_list.Count; j++)
					{
						if (!this.Walltower_list[j].canBuild)
						{
							this.canBuild = false;
						}
					}
				}
				if (this.canBuild)
				{
					this.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.nodisplay);
					if (this.fangyuzhao != null)
					{
						this.fangyuzhao.SetActive(this.isfangyuzhao);
					}
					SenceManager.inst.CreateMuraille(base.Tower);
					if ((UIManager.curState == SenceState.Home || UIManager.curState == SenceState.InBuild) && UnitConst.GetInstance().buildingConst[this.index].resType < 4)
					{
						base.Tower.tr.position = new Vector3(base.Tower.tr.position.x, 0f, base.Tower.tr.position.z);
						PoolManage.Ins.CreatEffect("jiangluosan_yanwu", this.towerGridPlane.position, Quaternion.identity, this.towerGridPlane);
						for (int k = 0; k < this.Walltower_list.Count; k++)
						{
							this.Walltower_list[k].tr.position = new Vector3(this.Walltower_list[k].tr.position.x, 0f, this.Walltower_list[k].tr.position.z);
							SenceManager.inst.MovedTower(this.Walltower_list[k], false);
							SenceManager.inst.CreateMuraille(this.Walltower_list[k]);
							this.Walltower_list[k].EditeMapGrid(true);
						}
					}
					SenceManager.inst.SetBuildGrid_AttackTrue();
					SenceManager.inst.MovedTower(this, true);
					if (!this.temp)
					{
						this.EditeMapGrid(true);
					}
				}
			}
			else
			{
				this.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.nodisplay);
				if (this.fangyuzhao != null)
				{
					this.fangyuzhao.SetActive(this.isfangyuzhao);
				}
				SenceManager.inst.CreateMuraille(base.Tower);
				if (UIManager.curState == SenceState.Home && this.index != 12)
				{
					base.Tower.tr.position = new Vector3(base.Tower.tr.position.x, 0f, base.Tower.tr.position.z);
					for (int l = 0; l < this.Walltower_list.Count; l++)
					{
						this.Walltower_list[l].tr.position = new Vector3(this.Walltower_list[l].tr.position.x, 0f, this.Walltower_list[l].tr.position.z);
						SenceManager.inst.CreateMuraille(this.Walltower_list[l]);
						this.Walltower_list[l].EditeMapGrid(true);
					}
				}
				this.isChoose = true;
				SenceManager.inst.CreateMuraille(base.Tower);
				this.EditeMapGrid(true);
			}
			goto IL_73E;
		case SenceState.Spy:
			break;
		case SenceState.Attacking:
			if (UnitConst.GetInstance().buildingConst[this.index].resType != 5)
			{
				if (FightPanelManager.supperSkillReady)
				{
					if (FightPanelManager.inst.isSpColor)
					{
						if (UnitConst.GetInstance().skillList[FightPanelManager.inst.SkillId].skillType == 1)
						{
							HUDTextTool.inst.SetText("此地不可投放,请选择陆地进行投放", HUDTextTool.TextUITypeEnum.Num1);
							goto IL_73E;
						}
						SkillManage.inst.AcquittalSkill(FightPanelManager.inst.SkillId, this.tr.position, -1);
					}
				}
				else
				{
					EventNoteMgr.NoticeFoceAttack(this.id);
					SenceManager.inst.ForceAttack(this);
				}
				if (FightPanelManager.inst)
				{
					FightPanelManager.inst.CloseAllSkillSlot();
				}
			}
			goto IL_73E;
		case SenceState.InBuild:
			if (this.canBuild)
			{
				this.row = Mathf.RoundToInt(this.tr.localPosition.x);
				this.num = Mathf.RoundToInt(this.tr.localPosition.z);
			}
			goto IL_73E;
		default:
			if (curState != SenceState.Visit)
			{
				goto IL_73E;
			}
			break;
		}
		this.TowerInfoShow(true);
		IL_73E:
		HUDTextTool.inst.NextLuaCallByIsEnemyAttck("Tower 点击的时候 调用···", new object[]
		{
			this.ga
		});
	}

	public void OnTowerPosShow()
	{
		this.SetModelToTeXiaoLayer(true);
		SenceManager.inst.DesWallconnect(this);
		this.canBuild = this.VerifyMapGrid();
		if (!this.canBuild)
		{
			T_CommandPanelManage._instance.moveTittle.GetComponent<UILabel>().color = Color.red;
			this.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.nomove);
		}
		else
		{
			T_CommandPanelManage._instance.moveTittle.GetComponent<UILabel>().color = new Color(0.7294118f, 0.921568632f, 0.9764706f);
			this.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.canmove);
		}
	}

	public void SetModelToTeXiaoLayer(bool isTeXiaoLayer)
	{
		if (isTeXiaoLayer)
		{
			if (this.ModelBody)
			{
				Transform[] componentsInChildren = this.ModelBody.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = 9;
				}
			}
			this.towerGridPlane.gameObject.layer = 0;
		}
		else
		{
			if (this.ModelBody)
			{
				Transform[] componentsInChildren2 = this.ModelBody.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform2 = componentsInChildren2[j];
					transform2.gameObject.layer = 0;
				}
			}
			this.towerGridPlane_Attack.gameObject.layer = 17;
			this.towerGridPlane.gameObject.layer = 0;
		}
	}

	public override void MouseDrag()
	{
		if (Input.touchCount > 1)
		{
			return;
		}
		if (UnitConst.GetInstance().buildingConst[this.index].resType > 3 && UnitConst.GetInstance().buildingConst[this.index].resType != 99)
		{
			return;
		}
		if (UIManager.curState == SenceState.Home || UIManager.curState == SenceState.InBuild || UIManager.curState == SenceState.WatchResIsland)
		{
			if (UIManager.curState == SenceState.Home && this.index != 12)
			{
				base.Tower.tr.position = new Vector3(base.Tower.tr.position.x, 0.5f, base.Tower.tr.position.z);
				SenceManager.inst.DesWallconnect(base.Tower);
				for (int i = 0; i < this.Walltower_list.Count; i++)
				{
					this.Walltower_list[i].tr.position = new Vector3(this.Walltower_list[i].tr.position.x, 0.5f, this.Walltower_list[i].tr.position.z);
					SenceManager.inst.DesWallconnect(this.Walltower_list[i]);
				}
			}
			if (!this.towerMoved)
			{
				if (this.fangyuzhao != null)
				{
					this.fangyuzhao.SetActive(this.isfangyuzhao);
				}
				SenceManager.inst.DestoryMuraille(base.Tower);
				if (T_CommandPanelManage._instance)
				{
					T_CommandPanelManage._instance.HidePanel();
				}
				this.EditeMapGrid(false);
				this.towerMoved = true;
				SenceManager.inst.mover_Tower = this;
				SenceManager.inst.SetBuildGridActive(true);
				this.OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.canmove);
				this.OnTowerPosShow();
				for (int j = 0; j < this.Walltower_list.Count; j++)
				{
					this.Walltower_list[j].OnTowerPosShow();
					this.Walltower_list[j].EditeMapGrid(false);
				}
			}
			if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1f)
			{
				DragMgr.inst.fingerMove = true;
			}
			if (!DragMgr.inst.fingerMove)
			{
				return;
			}
			DragMgr.inst.buildDraging = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 100f, LayerMask.GetMask(new string[]
			{
				"Ground"
			})))
			{
				int num = Mathf.RoundToInt(raycastHit.point.x);
				int num2 = Mathf.RoundToInt(raycastHit.point.z);
				if (num < 0)
				{
					num = 0;
				}
				else if (num > SenceManager.inst.arrayX)
				{
					num = SenceManager.inst.arrayX;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				else if (num2 > SenceManager.inst.arrayY)
				{
					num2 = SenceManager.inst.arrayY;
				}
				this.tr.localPosition = new Vector3((float)num, this.tr.localPosition.y, (float)num2);
				for (int k = 0; k < this.Walltower_list.Count; k++)
				{
					this.Walltower_list[k].tr.position = new Vector3(this.tr.position.x + this.Walltower_list[k].WallToPos.x, 0.5f, this.tr.position.z + this.Walltower_list[k].WallToPos.y);
					SenceManager.inst.DesWallconnect(this.Walltower_list[k]);
					this.Walltower_list[k].OnTowerPosShow();
				}
				SenceManager.inst.DesWallconnect(this);
				this.OnTowerPosShow();
				if (Camera_FingerManager.inst.SelingDragCharInIsland == this)
				{
					float num3 = Input.mousePosition.x / (float)Screen.width;
					float num4 = Input.mousePosition.y / (float)Screen.height;
					if (NewbieGuidePanel._instance.ga_Self.activeSelf)
					{
						num3 = 0.5f;
						num4 = 0.5f;
					}
					if (num4 <= 0.15f)
					{
						if (CameraControl.inst.move_time <= 0f)
						{
							CameraControl.inst.move_time = 0.5f;
							CameraControl.inst.move_y = 10f;
						}
					}
					else if (num4 >= 0.85f)
					{
						if (CameraControl.inst.move_time <= 0f)
						{
							CameraControl.inst.move_time = 0.5f;
							CameraControl.inst.move_y = -10f;
						}
					}
					else if (num3 >= 0.8f)
					{
						if (CameraControl.inst.move_time <= 0f)
						{
							CameraControl.inst.move_time = 0.5f;
							CameraControl.inst.move_x = -10f;
						}
					}
					else if (num3 <= 0.2f)
					{
						if (CameraControl.inst.move_time <= 0f)
						{
							CameraControl.inst.move_time = 0.5f;
							CameraControl.inst.move_x = 10f;
						}
					}
					else
					{
						CameraControl.inst.move_x = 0f;
						CameraControl.inst.move_y = 0f;
					}
				}
			}
			this.TowerInfoShow(true);
			this.isDarg = true;
		}
	}

	public void BroadTowerClick()
	{
		if ((SenceManager.inst.tempTower == null || !SenceManager.inst.tempTower.Equals(this)) && CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			return;
		}
		if (T_Tower.ClickTowerSendMessage != null)
		{
			T_Tower.ClickTowerSendMessage();
		}
		T_CommandPanelManage._instance.ShowBtn(this);
		T_CommandPanelManage._instance.HideMainPanel();
	}

	public void TowerInfoShow(bool b)
	{
		if (SenceManager.inst.curTower != this && SenceManager.inst.curTower != null && SenceManager.inst.curTower != null)
		{
			SenceManager.inst.curTower.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Idle);
		}
		SenceManager.inst.ShowTowerR(b, this);
		if (b)
		{
			this.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Selected);
		}
		else
		{
			this.T_TowerSelectState.ChangeState(Character.CharacterSelectStates.Idle);
		}
		if (UIManager.curState == SenceState.Spy || UIManager.curState == SenceState.Visit)
		{
			InfoPanel.inst.ShowSimpleInfo(UIManager.curState, b, this);
		}
		this.selected = b;
		if (this.type == 1 || this.type == 2 || this.type == 3)
		{
			this.towerBuild = b;
		}
	}

	protected void NewTempTower()
	{
		this.TowerInfoShow(true);
	}

	public void SetLogoFlash()
	{
	}

	public void RebackShader()
	{
	}

	public void SetInfo()
	{
		this.IsDie = false;
		this.isTexiao = true;
		this.isDarg = false;
		this.isNotMove = false;
		if (!this.T_TowerSelectState)
		{
		}
		if (this.towerGridPlane)
		{
			this.towerGridPlane.renderer.enabled = false;
		}
		if (this.updateTittle == null)
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("JT1", this.tr);
			if (modelByBundleByName)
			{
				this.updateTittle = modelByBundleByName.tr;
				this.updateTittle.transform.localPosition = UnitConst.GetInstance().buildingConst[this.index].updateTittlePos;
				this.updateTittle.transform.localRotation = Quaternion.Euler(UnitConst.GetInstance().buildingConst[this.index].updateTittleRotion);
				TweenPosition.Begin(this.updateTittle.gameObject, 0.8f, this.updateTittle.transform.localPosition + new Vector3(0f, 1f, 0f)).style = UITweener.Style.PingPong;
				this.updateTittle.gameObject.SetActive(false);
			}
		}
		else
		{
			this.updateTittle.gameObject.SetActive(false);
		}
		if (this.index != 0)
		{
			this.HeadRotationSpeed = (float)UnitConst.GetInstance().buildingConst[this.index].headRotationSpeed;
			this.height = UnitConst.GetInstance().buildingConst[this.index].hight;
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 20)
			{
				this.size = UnitConst.GetInstance().buildingConst[this.index].size;
				this.bodyForAttack.center = new Vector3(0f, UnitConst.GetInstance().buildingConst[this.index].hight / 2f, 0f);
				this.bodyForAttack.size = new Vector3(1.5f, UnitConst.GetInstance().buildingConst[this.index].hight, 1.5f);
				this.bodyForSelect.center = new Vector3(0f, UnitConst.GetInstance().buildingConst[this.index].hight / 2f, 0f);
				this.bodyForSelect.size = new Vector3(1.5f, UnitConst.GetInstance().buildingConst[this.index].hight, 1.5f);
			}
			else
			{
				this.size = UnitConst.GetInstance().buildingConst[this.index].size;
				this.bodyForAttack.center = new Vector3(0f, UnitConst.GetInstance().buildingConst[this.index].hight / 2f, 0f);
				this.bodyForAttack.size = new Vector3(UnitConst.GetInstance().buildingConst[this.index].bodySize, UnitConst.GetInstance().buildingConst[this.index].hight, UnitConst.GetInstance().buildingConst[this.index].bodySize);
				this.bodyForSelect.center = new Vector3(0f, UnitConst.GetInstance().buildingConst[this.index].hight / 2f, 0f);
				this.bodyForSelect.size = new Vector3((float)UnitConst.GetInstance().buildingConst[this.index].size, UnitConst.GetInstance().buildingConst[this.index].hight, (float)UnitConst.GetInstance().buildingConst[this.index].size);
			}
			UnityEngine.Object.Destroy(this.bodyForSelect);
			this.ShootRadius = UnitConst.GetInstance().buildingConst[this.index].maxRadius;
			this.doRenjuCD = UnitConst.GetInstance().buildingConst[this.index].renjuCD;
			this.bulletType = UnitConst.GetInstance().buildingConst[this.index].bulletType;
			this.CreatTowerGrid();
			this.EditeTowerGrid(this.row, this.num);
			this.InitBuildingOrEndState();
			this.FreshArmyFuncEnd(true);
			if (this.type == 1 || this.type == 3 || this.type == 2)
			{
				this.towerBuild = true;
			}
			if (UnitConst.GetInstance().buildingConst[this.index].resType < 3 || UnitConst.GetInstance().buildingConst[this.index].secType == 20)
			{
				this.bodyForAttack.gameObject.layer = 27;
			}
			else
			{
				this.bodyForAttack.gameObject.layer = 0;
			}
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 20)
			{
				BoxCollider boxCollider = GameTools.CreateChildren<BoxCollider>(this.tr);
				boxCollider.gameObject.layer = 29;
				boxCollider.tag = this.bodyForAttack.tag;
				boxCollider.isTrigger = true;
				boxCollider.center = new Vector3(0f, UnitConst.GetInstance().buildingConst[this.index].hight / 2f, 0f);
				boxCollider.size = new Vector3(UnitConst.GetInstance().buildingConst[this.index].bodySize, UnitConst.GetInstance().buildingConst[this.index].hight, UnitConst.GetInstance().buildingConst[this.index].bodySize);
			}
			this.SetBaseFightInfo();
			base.MaxLife = this.CharacterBaseFightInfo.life;
			base.CurLife = (float)base.MaxLife;
			if (this.temp)
			{
				this.tr.localPosition = new Vector3((float)this.row, this.tr.localPosition.y, (float)this.num);
				this.canBuild = this.VerifyMapGrid();
				this.NewTempTower();
			}
			this.DisplayLogo();
			this.RefreshUpdateTittle();
		}
		this.Targetes.Clear();
		this.InitBuffRuntime();
		if (this.fangyuzhao != null)
		{
			UnityEngine.Object.Destroy(this.fangyuzhao);
		}
		this.CreateNewXuetiao();
		this.CreateDefensiveCover();
		if (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.WatchVideo || UIManager.curState == SenceState.Spy)
		{
			if (this.index == 23)
			{
				this.TESLAFight();
			}
			this.resSpeed_ByStep_Ele_Tech_Vip = this.ResSpeed_ByStep_Ele_Tech_Vip;
		}
		this.SetMiniCar();
		this.AnimationControler = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ga);
		this.AnimationControler.SetAnimaInfo();
	}

	public void SetBaseFightInfo()
	{
		if (UIManager.curState != SenceState.WatchVideo)
		{
			if (SenceInfo.curMap.IsMyMap)
			{
				this.CharacterBaseFightInfo = InfoMgr.GetTowerFightData(this.index, this.lv, this.strengthLevel, this.star, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
				this.nextLVBaseFightInfo = InfoMgr.GetTowerFightData(this.index, this.lv + 1, this.strengthLevel, this.star, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
			}
			else
			{
				this.nextLVBaseFightInfo = InfoMgr.GetTowerFightData(this.index, this.lv + 1, this.strengthLevel, this.star, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
				this.CharacterBaseFightInfo = InfoMgr.GetTowerFightData(this.index, this.lv, this.strengthLevel, this.star, SenceInfo.SpyPlayerInfo.vip, SenceInfo.curMap.EnemyTech);
			}
		}
	}

	private void SetMiniCar()
	{
		if (this.mining_Car)
		{
			return;
		}
		if (UIManager.curState != SenceState.WatchVideo && UIManager.curState != SenceState.Attacking && CameraControl.inst.cameraBuildingStateEnum != CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding && this.index == 6)
		{
			this.mining_Car = PoolManage.Ins.GetModelByBundleByName("miningcar", null);
			if (this.mining_Car)
			{
				GameTools.GetCompentIfNoAddOne<Mining_Car>(this.mining_Car.ga).SetInfo(this);
			}
		}
	}

	public void DisplayLogo()
	{
		if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			return;
		}
		if (UIManager.curState != SenceState.Home || this.index == 11)
		{
		}
	}

	public void HideLogo()
	{
		if (this.Logo)
		{
			this.Logo.SetActive(false);
		}
	}

	private void CreatTowerGrid()
	{
		this.towerRows = new TowerGrid[this.size * this.size];
		this.towerGridPlane.localScale = Vector2.one * (float)this.size;
		this.towerGridPlane_Attack.localScale = Vector2.one * ((float)this.size + 3f);
		this.towerGridPlane.renderer.material.mainTextureScale = new Vector2((float)this.size * 0.2f, (float)this.size * 0.2f);
		this.RedPiece.transform.localScale = Vector2.one * ((float)this.size + 3f);
		this.towerGridPlane_Attack.gameObject.SetActive(false);
		this.RedPiece.SetActive(false);
	}

	public void EditeTowerGrid(int x, int y)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.towerRows.Length; i++)
		{
			if (num2 == this.size)
			{
				num++;
				num2 = 0;
			}
			this.towerRows[i] = new TowerGrid();
			this.towerRows[i].numX = num - Mathf.CeilToInt((float)(this.size / 2));
			this.towerRows[i].numZ = num2 - Mathf.CeilToInt((float)(this.size / 2));
			num2++;
		}
	}

	public void EditeMapGrid(bool add)
	{
		if (this.temp)
		{
			return;
		}
		for (int i = 0; i < this.towerRows.Length; i++)
		{
			int num = this.row + this.towerRows[i].numX;
			int num2 = this.num + this.towerRows[i].numZ;
			if (add)
			{
				if (num > 0 && num <= SenceManager.inst.arrayX && num2 > 0 && num2 <= SenceManager.inst.arrayY)
				{
					MapGridManager.AddMapGrid(Mathf.RoundToInt((float)(this.row + this.towerRows[i].numX)), Mathf.RoundToInt((float)(this.num + this.towerRows[i].numZ)));
				}
			}
			else if (num > 0 && num <= SenceManager.inst.arrayX && num2 > 0 && num2 <= SenceManager.inst.arrayY)
			{
				MapGridManager.RemoveMapGrid(Mathf.RoundToInt((float)(this.row + this.towerRows[i].numX)), Mathf.RoundToInt((float)(this.num + this.towerRows[i].numZ)));
			}
		}
	}

	public bool VerifyMapGrid()
	{
		for (int i = 0; i < this.towerRows.Length; i++)
		{
			int num = Mathf.RoundToInt(this.tr.localPosition.x + (float)this.towerRows[i].numX);
			if (num < 0 || num >= SenceManager.inst.arrayX)
			{
				return false;
			}
			int num2 = Mathf.RoundToInt(this.tr.localPosition.z + (float)this.towerRows[i].numZ);
			if (num2 < 0 || num2 >= SenceManager.inst.arrayY)
			{
				return false;
			}
			if (!MapGridManager.VerifyMapGrid(num, num2))
			{
				return false;
			}
		}
		return true;
	}

	public void RebackTower(int _row, int _num)
	{
		this.tr.localPosition = new Vector3((float)_row, this.tr.localPosition.y, (float)_num);
		if (UIManager.curState != SenceState.InBuild)
		{
			this.TowerInfoShow(true);
		}
		this.towerGridPlane.renderer.enabled = false;
		this.EditeMapGrid(true);
		if (UIManager.curState == SenceState.Home && this.index != 12)
		{
			base.Tower.tr.position = new Vector3(base.Tower.tr.position.x, 0f, base.Tower.tr.position.z);
			this.towerGridPlane.localPosition = new Vector3(0f, -0.1f, 0f);
		}
	}

	public void OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType enum_GridPlane)
	{
		if (this.towerGridPlane && this.towerGridPlane.renderer)
		{
			switch (enum_GridPlane)
			{
			case T_Tower.Enum_GridPlaneDisplayType.canmove:
				for (int i = 0; i < SenceManager.inst.towers.Count; i++)
				{
					SenceManager.inst.towers[i].OpenOrCloseGridPlane(T_Tower.Enum_GridPlaneDisplayType.nomove);
				}
				this.towerGridPlane.localPosition = new Vector3(0f, this.tr.position.y * -1f + 0.1f, 0f);
				this.towerGridPlane.renderer.material.SetColor("_Emission", this.GreenGridPlane);
				this.towerGridPlane.renderer.enabled = true;
				break;
			case T_Tower.Enum_GridPlaneDisplayType.helpinfo:
				this.towerGridPlane.localPosition = new Vector3(0f, 0.1f, 0f);
				this.towerGridPlane.renderer.material.SetColor("_Emission", Color.red);
				this.towerGridPlane.renderer.enabled = true;
				break;
			case T_Tower.Enum_GridPlaneDisplayType.nomove:
				this.towerGridPlane.localPosition = new Vector3(0f, this.tr.position.y * -1f + 0.1f, 0f);
				this.towerGridPlane.renderer.material.SetColor("_Emission", Color.red);
				this.towerGridPlane.renderer.enabled = true;
				break;
			case T_Tower.Enum_GridPlaneDisplayType.nodisplay:
				this.towerGridPlane.renderer.enabled = false;
				break;
			}
		}
	}

	public void New_HurtByBaseFightInfo(BaseFightInfo attackFightInfo, long containerID, float damageLv = 1f)
	{
		if (UnitConst.GetInstance().buildingConst[this.index].resType != 5)
		{
			bool towerToTank = false;
			bool flag;
			int num = NumericalMgr.S_AttackDamage(attackFightInfo, this.CharacterBaseFightInfo, towerToTank, out flag);
			this.DoHurt(Mathf.CeilToInt((float)num * damageLv), containerID, true);
		}
	}

	public void SuSkillHurt(int idx)
	{
		if (UnitConst.GetInstance().buildingConst[this.index].resType != 5)
		{
			int num;
			if (!SkillManage.inst.EnemySkillOpen)
			{
				num = (int)((float)UnitConst.GetInstance().skillList[idx].basePower * (1f + (float)HeroInfo.GetInstance().SkillInfo[UnitConst.GetInstance().skillList[idx].skillType] * 0.1f));
			}
			else
			{
				num = (int)((float)UnitConst.GetInstance().skillList[idx].basePower * (1f + (float)SkillManage.inst.EnemySkillList[idx] * 0.1f));
			}
			HUDTextTool.inst.SetText("-" + num, this.tr, Color.red, 0.8f, 40);
			this.DoHurt(num, -10L, true);
		}
	}

	public void PlunderResource()
	{
		this.CurRes_Predat.Clear();
		if (UnitConst.GetInstance().buildingConst[this.index].secType == 3)
		{
			BuildingNPC buildingNPC = (!SenceInfo.curMap.ResourceBuildingList.ContainsKey(this.id)) ? null : SenceInfo.curMap.ResourceBuildingList[this.id];
			if (buildingNPC != null)
			{
				float num = (float)((TimeTools.GetNowTimeSyncServerToDateTime() - buildingNPC.takeTime).TotalHours * (double)this.resSpeed_ByStep_Ele_Tech_Vip);
				if (num >= this.ResMaxLimit_ProdByTech)
				{
					num = this.ResMaxLimit_ProdByTech;
				}
				this.CurRes_Predat.Add((ResType)buildingNPC.productType, num);
			}
		}
		else if (UnitConst.GetInstance().buildingConst[this.index].secType == 1)
		{
			if (SenceInfo.SpyPlayerInfo.battleType == 3)
			{
				foreach (KeyValuePair<ResType, int> current in UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit)
				{
					this.CurRes_Predat.Add(current.Key, this.ResMaxLimit_ByTech(current.Key));
				}
			}
			else
			{
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.金币))
				{
					if (this.ResMaxLimit_ByTech(ResType.金币) > (float)SenceInfo.curMap.curCoin)
					{
						this.CurRes_Predat.Add(ResType.金币, (float)SenceInfo.curMap.curCoin);
					}
					else
					{
						this.CurRes_Predat.Add(ResType.金币, this.ResMaxLimit_ByTech(ResType.金币));
					}
				}
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.石油))
				{
					if (this.ResMaxLimit_ByTech(ResType.石油) > (float)SenceInfo.curMap.curOil)
					{
						this.CurRes_Predat.Add(ResType.石油, (float)SenceInfo.curMap.curOil);
					}
					else
					{
						this.CurRes_Predat.Add(ResType.石油, this.ResMaxLimit_ByTech(ResType.石油));
					}
				}
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.钢铁))
				{
					if (this.ResMaxLimit_ByTech(ResType.钢铁) > (float)SenceInfo.curMap.curSteel)
					{
						this.CurRes_Predat.Add(ResType.钢铁, (float)SenceInfo.curMap.curSteel);
					}
					else
					{
						this.CurRes_Predat.Add(ResType.钢铁, this.ResMaxLimit_ByTech(ResType.钢铁));
					}
				}
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.稀矿))
				{
					if (this.ResMaxLimit_ByTech(ResType.稀矿) > (float)SenceInfo.curMap.curRareEarth)
					{
						this.CurRes_Predat.Add(ResType.稀矿, (float)SenceInfo.curMap.curRareEarth);
					}
					else
					{
						this.CurRes_Predat.Add(ResType.稀矿, this.ResMaxLimit_ByTech(ResType.稀矿));
					}
				}
			}
		}
		else if (UnitConst.GetInstance().buildingConst[this.index].secType == 2)
		{
			if (SenceInfo.SpyPlayerInfo.battleType == 3)
			{
				foreach (KeyValuePair<ResType, int> current2 in UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit)
				{
					this.CurRes_Predat.Add(current2.Key, this.ResMaxLimit_ByTech(current2.Key));
				}
			}
			else
			{
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.金币))
				{
					if (UnitConst.GetInstance().buildingConst[SenceManager.inst.MainBuilding.index].lvInfos[SenceManager.inst.MainBuildingLv].outputLimit.ContainsKey(ResType.金币))
					{
						if ((float)SenceInfo.curMap.curCoin > SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.金币))
						{
							this.CurRes_Predat.Add(ResType.金币, (float)SenceInfo.curMap.curCoin - SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.金币));
						}
					}
					else
					{
						this.CurRes_Predat.Add(ResType.金币, (float)SenceInfo.curMap.curCoin);
					}
				}
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.石油))
				{
					if (UnitConst.GetInstance().buildingConst[SenceManager.inst.MainBuilding.index].lvInfos[SenceManager.inst.MainBuildingLv].outputLimit.ContainsKey(ResType.石油))
					{
						if ((float)SenceInfo.curMap.curOil > SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.石油))
						{
							this.CurRes_Predat.Add(ResType.石油, (float)SenceInfo.curMap.curOil - SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.石油));
						}
					}
					else
					{
						this.CurRes_Predat.Add(ResType.石油, (float)SenceInfo.curMap.curOil);
					}
				}
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.钢铁))
				{
					if (UnitConst.GetInstance().buildingConst[SenceManager.inst.MainBuilding.index].lvInfos[SenceManager.inst.MainBuildingLv].outputLimit.ContainsKey(ResType.钢铁))
					{
						if ((float)SenceInfo.curMap.curSteel > SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.钢铁))
						{
							this.CurRes_Predat.Add(ResType.钢铁, (float)SenceInfo.curMap.curSteel - SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.钢铁));
						}
					}
					else
					{
						this.CurRes_Predat.Add(ResType.钢铁, (float)SenceInfo.curMap.curSteel);
					}
				}
				if (UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].outputLimit.ContainsKey(ResType.稀矿))
				{
					if (UnitConst.GetInstance().buildingConst[SenceManager.inst.MainBuilding.index].lvInfos[SenceManager.inst.MainBuildingLv].outputLimit.ContainsKey(ResType.稀矿))
					{
						if ((float)SenceInfo.curMap.curRareEarth > SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.稀矿))
						{
							this.CurRes_Predat.Add(ResType.稀矿, (float)SenceInfo.curMap.curRareEarth - SenceManager.inst.MainBuilding.ResMaxLimit_ByTech(ResType.稀矿));
						}
					}
					else
					{
						this.CurRes_Predat.Add(ResType.稀矿, (float)SenceInfo.curMap.curRareEarth);
					}
				}
			}
		}
		float num2 = PlunderPanel.inst.GetCommand() * PlunderPanel.inst.GetStrongRoom();
		num2 *= PlunderPanel.inst.canshu3;
		num2 = num2 * this.newDamage / (float)base.MaxLife;
		foreach (KeyValuePair<ResType, float> current3 in this.CurRes_Predat)
		{
			switch (current3.Key)
			{
			case ResType.金币:
				if (PlunderPanel.inst.CanPlunderCoin && current3.Value > 0f)
				{
					float num3 = num2 * current3.Value;
					int num4 = PlunderPanel.inst.PluderCoin(num3);
					LogManage.LogError(string.Format("掠夺金币{0} 掠夺后比率是{1}", num3, num4));
				}
				break;
			case ResType.石油:
				if (PlunderPanel.inst.CanPlunderOil && current3.Value > 0f)
				{
					float num5 = num2 * current3.Value;
					int num4 = PlunderPanel.inst.PluderOil(num5);
					LogManage.LogError(string.Format("掠夺石油{0} 掠夺后比率是{1}", num5, num4));
				}
				break;
			case ResType.钢铁:
				if (PlunderPanel.inst.CanPlunderSteel && current3.Value > 0f)
				{
					float num6 = num2 * current3.Value;
					int num4 = PlunderPanel.inst.PluderSteel(num6);
					LogManage.LogError(string.Format("掠夺钢铁{0} 掠夺后比率是{1}", num6, num4));
				}
				break;
			case ResType.稀矿:
				if (PlunderPanel.inst.CanPlunderRareEarth && current3.Value > 0f)
				{
					float num7 = num2 * current3.Value;
					int num4 = PlunderPanel.inst.PliderRareEarth(num7);
					LogManage.LogError(string.Format("掠夺稀矿{0} 掠夺后比率是{1}", num7, num4));
				}
				break;
			}
		}
	}

	public void PlayPlundeEffects()
	{
		switch (this.index)
		{
		case 1:
			PoolManage.Ins.CreatEffect("ziyuan_jinkuang", this.tr.position, Quaternion.identity, null);
			PoolManage.Ins.CreatEffect("ziyuan_jinkuang", this.tr.position, Quaternion.identity, null);
			PoolManage.Ins.CreatEffect("ziyuan_jinkuang", this.tr.position, Quaternion.identity, null);
			PoolManage.Ins.CreatEffect("ziyuan_xikuang", this.tr.position, Quaternion.identity, null);
			break;
		case 2:
		case 6:
			PoolManage.Ins.CreatEffect("ziyuan_jinkuang", this.tr.position, Quaternion.identity, null);
			break;
		case 3:
		case 7:
			PoolManage.Ins.CreatEffect("ziyuan_shiyou", this.tr.position, Quaternion.identity, null);
			break;
		case 4:
		case 8:
			PoolManage.Ins.CreatEffect("ziyuan_jinkuang", this.tr.position, Quaternion.identity, null);
			break;
		case 5:
		case 9:
			PoolManage.Ins.CreatEffect("ziyuan_xikuang", this.tr.position, Quaternion.identity, null);
			break;
		}
	}

	public bool IsFangyuJiacheng()
	{
		List<T_Tower> towers = SenceManager.inst.towers;
		for (int i = 0; i < towers.Count; i++)
		{
			if (towers[i].index == 62 && Vector3.Distance(base.Tower.tr.position, towers[i].tr.position) < (float)(12 + base.Tower.size / 2))
			{
				return true;
			}
		}
		return false;
	}

	public virtual void DoHurt(int damage, long containerID, bool isExtraAttack = true)
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		if (UnitConst.GetInstance().buildingConst[this.index].secType == 9)
		{
			this.SelfDestruct();
			EventNoteMgr.NoticeDie(0, this.id);
		}
		else
		{
			if (isExtraAttack)
			{
				this.newDamage = (float)damage * (1f + MainUIPanelManage.extraAttack / 100f);
			}
			else if (this.IsFangyuJiacheng())
			{
				this.newDamage = (float)damage * (1f + MainUIPanelManage.extraAttack / 100f);
			}
			else
			{
				int extraDamage = UnitConst.GetInstance().ElectricityCont[4].extraDamage;
				this.newDamage = (float)damage * (1f + (float)(extraDamage / 100));
			}
			if (this.CharacterBaseFightInfo.Shield > 0)
			{
				this.CharacterBaseFightInfo.Shield = this.CharacterBaseFightInfo.Shield - (int)this.newDamage;
				if (this.CharacterBaseFightInfo.Shield > 0 || !(this.MyBuffRuntime != null))
				{
					this.newDamage = 0f;
					return;
				}
				this.MyBuffRuntime.RemoveBuff(Buff.BuffType.Shield);
				this.newDamage -= (float)this.CharacterBaseFightInfo.Shield;
			}
			if (this.newDamage > base.CurLife)
			{
				this.newDamage = base.CurLife;
			}
			base.CurLife -= this.newDamage;
			if (this.UnActivateDefenceTank)
			{
				this.UnActivateDefenceTank = T_TowerTankManager.inst.T_TowerDoHurt_Feedback(this, base.CurLife / (float)base.MaxLife * 100f, (int)this.newDamage);
			}
			if ((double)(base.CurLife / (float)base.MaxLife) <= 0.5)
			{
				this.RefreshModel();
			}
			if (SenceInfo.CurBattle == null && UIManager.curState != SenceState.Visit && NewbieGuidePanel.isEnemyAttck)
			{
				if (this.isTexiao && (double)(base.CurLife / (float)base.MaxLife) <= 0.5)
				{
					this.isTexiao = false;
					this.PlayPlundeEffects();
				}
				if (UIManager.curState == SenceState.Attacking && SenceInfo.SpyPlayerInfo.battleType != 2 && SenceInfo.SpyPlayerInfo.battleType != 4 && UnitConst.GetInstance().buildingConst[this.index].secType < 4)
				{
					this.PlunderResource();
				}
			}
			if (base.CurLife < (float)base.MaxLife && base.CurLife > 0f)
			{
				if (this.m_lifeInfo != null)
				{
					this.m_lifeInfo.ShowTankLife(null, this);
				}
				if (this.xuetiao != null)
				{
					this.xuetiao.SetActive(true);
					for (int i = 0; i < this.xuetiaoList.Count; i++)
					{
						if (i > (int)((float)T_Tower.xuetiaoNum * base.CurLife / (float)base.MaxLife))
						{
							this.xuetiaoList[i].SetActive(false);
						}
						if (base.CurLife / (float)base.MaxLife <= 0.75f)
						{
							if (base.CurLife / (float)base.MaxLife > 0.2f && this.xuetiaoList[0].activeSelf)
							{
								this.xuetiaoList[i].renderer.material.color = Color.yellow;
							}
							else
							{
								this.xuetiaoList[i].renderer.material.color = Color.red;
							}
						}
					}
				}
			}
			if (UnitConst.GetInstance().buildingConst[this.index].secType != 20)
			{
				if (base.CurLife < (float)base.MaxLife * 0.5f)
				{
					if (!this.HalfBloodEffect && this.tr)
					{
						this.HalfBloodEffect = PoolManage.Ins.GetEffectByName("ranshao_01_half", this.tr);
						if (this.HalfBloodEffect)
						{
							this.HalfBloodEffect.tr.position = this.tr.position - new Vector3(0f, 0.7f, 0f);
						}
					}
				}
				else if (this.HalfBloodEffect)
				{
					this.HalfBloodEffect.DesInsInPool(0f);
				}
			}
			if (base.CurLife < 1f && !this.IsDie)
			{
				if (UIManager.curState == SenceState.WatchVideo)
				{
					return;
				}
				this.IsDie = true;
				if (SenceManager.inst.fightType == FightingType.Attack && NewbieGuidePanel.isEnemyAttck)
				{
					this.AddRes();
				}
				if (UIManager.curState != SenceState.WatchVideo)
				{
					EventNoteMgr.NoticeDie(0, this.id);
					if (WarStarManager._inst)
					{
						WarStarManager._inst.SomeTowerDead(this);
						T_TankAIManager.inst.AttPointUsedList.Clear();
					}
					if (UnitConst.GetInstance().buildingConst[this.index].secType < 4 && !FightHundler.FightEnd)
					{
						FightHundler.AddDeadSettleUnites(new SettleUniteData
						{
							deadType = 6,
							deadSenceId = this.id,
							deadIdx = this.index,
							deadBuildingID = (long)this.posIdx,
							num = 0
						});
					}
					if (this.secType == 1)
					{
						SenceManager.inst.RefreshTowerLife();
						SenceManager.inst.UnitOver(4);
					}
					else if (SenceManager.inst.MainBuilding != null)
					{
						int damage2 = NumericalMgr.MainBuildingDamage(base.MaxLife);
						SenceManager.inst.MainBuilding.DoHurt(damage2, containerID, false);
					}
				}
				else if (this.secType == 1)
				{
					FightHundler.FightEnd = true;
				}
				this.SelfDestruct();
				if (UIManager.curState != SenceState.WatchVideo)
				{
					if (!string.IsNullOrEmpty(NewbieGuideWrap.nextNewBi) && GameManager.Instance.GetLuaManage() != null && NewbieGuidePanel.isEnemyAttck)
					{
						if (NewbieGuidePanel._instance)
						{
							NewbieGuidePanel._instance.HideSelf();
						}
						HUDTextTool.inst.NextLuaCallByIsEnemyAttck("建筑死亡的时候· 调用", new object[]
						{
							this.ga
						});
					}
					if (NewbieGuidePanel.towerDieCallLua_InStarFire && NewbieGuidePanel.curGuideIndex == -1)
					{
						HUDTextTool.inst.NextLuaCallByIsEnemyAttck("建筑死亡的时候· 调用", new object[]
						{
							this.ga
						});
					}
				}
			}
		}
	}

	private void RefreshModel()
	{
		if (this.ModelBody)
		{
			if (this.modelClore == Enum_ModelColor.Blue)
			{
				if (this.ModelBody.Blue_DModel && !this.ModelBody.Blue_DModel.gameObject.activeSelf)
				{
					if (this.ModelBody.BlueModel && this.ModelBody.BlueModel.gameObject.activeSelf)
					{
						this.ModelBody.BlueModel.gameObject.SetActive(false);
					}
					this.ModelBody.Blue_DModel.gameObject.SetActive(true);
					ShootP[] componentsInChildren = this.ModelBody.Blue_DModel.GetComponentsInChildren<ShootP>();
					this.shootPList.Clear();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i].gameObject.activeInHierarchy)
						{
							this.shootPList.Add(componentsInChildren[i].transform);
						}
					}
					HeadTr componentInChildren = this.ModelBody.Blue_DModel.GetComponentInChildren<HeadTr>();
					if (this.head)
					{
						componentInChildren.transform.rotation = this.head.rotation;
					}
					if (componentInChildren)
					{
						this.head = componentInChildren.transform;
					}
					else
					{
						this.head = null;
					}
					Muzzle componentInChildren2 = this.ModelBody.Blue_DModel.GetComponentInChildren<Muzzle>();
					if (this.muzzle)
					{
						componentInChildren2.transform.rotation = this.muzzle.rotation;
					}
					if (componentInChildren2)
					{
						this.muzzle = componentInChildren2.transform;
					}
					else
					{
						this.muzzle = null;
					}
					this.AnimationControler = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ga);
					if (this.AnimationControler != null)
					{
						this.AnimationControler.AllAnimation.Clear();
						this.AnimationControler.AllAnimation.AddRange(this.ModelBody.ga.GetComponentsInChildren<Animation>());
					}
				}
			}
			else if (this.modelClore == Enum_ModelColor.Red && this.ModelBody.Red_DModel && !this.ModelBody.Red_DModel.gameObject.activeSelf)
			{
				if (this.ModelBody.RedModel && this.ModelBody.RedModel.gameObject.activeSelf)
				{
					this.ModelBody.RedModel.gameObject.SetActive(false);
				}
				this.ModelBody.Red_DModel.gameObject.SetActive(true);
				ShootP[] componentsInChildren2 = this.ModelBody.Red_DModel.GetComponentsInChildren<ShootP>();
				this.shootPList.Clear();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					if (componentsInChildren2[j].gameObject.activeInHierarchy)
					{
						this.shootPList.Add(componentsInChildren2[j].transform);
					}
				}
				HeadTr componentInChildren3 = this.ModelBody.Red_DModel.GetComponentInChildren<HeadTr>();
				if (this.head && componentInChildren3)
				{
					componentInChildren3.transform.rotation = this.head.rotation;
				}
				if (componentInChildren3)
				{
					this.head = componentInChildren3.transform;
				}
				else
				{
					this.head = null;
				}
				Muzzle componentInChildren4 = this.ModelBody.Red_DModel.GetComponentInChildren<Muzzle>();
				if (this.muzzle && componentInChildren4)
				{
					componentInChildren4.transform.rotation = this.muzzle.rotation;
				}
				if (componentInChildren4)
				{
					this.muzzle = componentInChildren4.transform;
				}
				else
				{
					this.muzzle = null;
				}
				this.AnimationControler = GameTools.GetCompentIfNoAddOne<Ani_CharacterControler>(this.ga);
				if (this.AnimationControler != null)
				{
					this.AnimationControler.AllAnimation.Clear();
					this.AnimationControler.AllAnimation.AddRange(this.ModelBody.ga.GetComponentsInChildren<Animation>());
				}
			}
			Transform[] componentsInChildren3 = this.ModelBody.GetComponentsInChildren<Transform>();
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				componentsInChildren3[k].gameObject.layer = 0;
			}
		}
	}

	protected void CreateBrokenTower()
	{
		if (UnitConst.GetInstance().buildingConst[this.index].secType != 20 && UnitConst.GetInstance().buildingConst[this.index].secType != 9 && UnitConst.GetInstance().buildingConst[this.index].resType < 3)
		{
			if (this.size >= 7)
			{
				Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("77feixu", SenceManager.inst.towerPool);
				if (modelByBundleByName)
				{
					modelByBundleByName.transform.position = new Vector3(this.tr.position.x, 0f, this.tr.position.z);
					Body_Model effectByName = PoolManage.Ins.GetEffectByName("ranshao_01", modelByBundleByName.transform);
					effectByName.tr.position = modelByBundleByName.transform.position - new Vector3(0f, 0.7f, 0f);
				}
			}
			else if (this.size >= 4)
			{
				Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("55feixu", SenceManager.inst.towerPool);
				if (modelByBundleByName)
				{
					modelByBundleByName.transform.position = new Vector3(this.tr.position.x, 0f, this.tr.position.z);
					Body_Model effectByName2 = PoolManage.Ins.GetEffectByName("ranshao_01", modelByBundleByName.transform);
					effectByName2.tr.position = modelByBundleByName.transform.position - new Vector3(0f, 0.7f, 0f);
				}
			}
			else if (this.size >= 3)
			{
				Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("33feixu", SenceManager.inst.towerPool);
				if (modelByBundleByName)
				{
					modelByBundleByName.transform.position = new Vector3(this.tr.position.x, 0f, this.tr.position.z);
					Body_Model effectByName3 = PoolManage.Ins.GetEffectByName("ranshao_01", modelByBundleByName.transform);
					effectByName3.tr.position = modelByBundleByName.transform.position - new Vector3(0f, 0.7f, 0f);
				}
			}
		}
	}

	protected void CreateTowerDeadEffect()
	{
		if (UnitConst.GetInstance().buildingConst[this.index].secType != 20 && UnitConst.GetInstance().buildingConst[this.index].secType != 9 && this.tr)
		{
			PoolManage.Ins.CreatEffect("buildingDie", this.tr.position, Quaternion.identity, null);
			PoolManage.Ins.CreatEffect("dkeng", this.tr.position, Quaternion.identity, null).LifeTime = 0f;
		}
	}

	public void SelfDestruct()
	{
		if (this.tr == null)
		{
			return;
		}
		this.CreateTowerDeadEffect();
		this.CreateBrokenTower();
		CameraControl.inst.Shake(0.3f, 10f);
		if (this.spyTowerInfo != null)
		{
			UnityEngine.Object.Destroy(this.spyTowerInfo.gameObject);
			InfoPanel.inst.RevoveSpyInfo(this.spyTowerInfo);
		}
		if (this.m_lifeInfo != null)
		{
			InfoPanel.inst.RemoveInfo(this.m_lifeInfo);
		}
		AudioManage.inst.PlayAuidoBySelf_3D("builddie", this.ga, false, 0uL);
		if (this.HalfBloodEffect)
		{
			UnityEngine.Object.Destroy(this.HalfBloodEffect);
		}
		SenceManager.inst.towers.Remove(this);
		SenceManager.inst.DestoryMuraille(this);
		base.Destory();
	}

	protected void AddRes()
	{
		int energypoints = UnitConst.GetInstance().buildingConst[this.index].lvInfos[this.lv].energypoints;
		if (energypoints > 0 && FightPanelManager.inst)
		{
			FightPanelManager.inst.OnEnd(energypoints);
		}
	}

	private bool Ctir(float ctr)
	{
		int num = UnityEngine.Random.Range(1, 100);
		return (float)num < ctr;
	}

	[DebuggerHidden]
	private IEnumerator DianCiLine(Transform ShootP, Vector3 tarPos)
	{
		T_Tower.<DianCiLine>c__Iterator30 <DianCiLine>c__Iterator = new T_Tower.<DianCiLine>c__Iterator30();
		<DianCiLine>c__Iterator.tarPos = tarPos;
		<DianCiLine>c__Iterator.<$>tarPos = tarPos;
		<DianCiLine>c__Iterator.<>f__this = this;
		return <DianCiLine>c__Iterator;
	}

	public void ShootNew(int shootIndex, Character tarTr, Vector3 tarPos)
	{
		AudioManage.inst.PlayAuidoBySelf_3D(UnitConst.GetInstance().buildingConst[this.index].fightSound, this.ga, false, 0uL);
		if (this.AnimationControler)
		{
			this.AnimationControler.AnimPlay("Attack1");
		}
		float num = 6f;
		float num2 = 0f;
		if (this.shootPList.Count == 0)
		{
			return;
		}
		Transform transform = null;
		if (this.shootPList.Count > shootIndex)
		{
			transform = this.shootPList[shootIndex];
		}
		else
		{
			transform = this.shootPList[0];
			LogManage.Log(string.Format("塔的攻击点个数是{0} 当前第几个攻击点{1}", this.shootPList.Count, shootIndex));
		}
		Quaternion rotation = transform.rotation;
		if (this.index == 17)
		{
			if (this.FightEffect == null)
			{
				this.FightEffect = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().buildingConst[this.index].fightEffect, transform.position, rotation, transform);
				this.FightEffect.LifeTime = 0f;
			}
			else
			{
				this.FightEffect.tr.position = transform.position;
				this.FightEffect.tr.rotation = rotation;
				this.FightEffect.Particle.Play();
			}
		}
		else
		{
			this.FightEffect = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().buildingConst[this.index].fightEffect, transform.position, rotation, transform);
		}
		T_BulletNew bullet;
		if (this.bulletType != 5)
		{
			bullet = PoolManage.Ins.GetBullet(transform.position, rotation, null);
		}
		else
		{
			bullet = PoolManage.Ins.GetBullet(tarPos, rotation, null);
		}
		bullet.target = ((!(tarTr == null) && !tarTr.IsDie) ? tarTr : null);
		bullet.targetP = tarPos;
		bullet.lightBulletStartPos = transform.position;
		if (this.bulletType == 0 && UnitConst.GetInstance().buildingConst[this.index].angle > 0)
		{
			this.AllTargetBySanShe.Clear();
			Vector3 to = (!(tarTr == null) && !tarTr.IsDie) ? (tarTr.tr.position - this.tr.position) : (tarPos - this.tr.position);
			for (int i = 0; i < this.Targetes.Count; i++)
			{
				if (this.Targetes[i])
				{
					Vector3 from = this.Targetes[i].tr.position - this.tr.position;
					float num3 = Vector3.Angle(from, to);
					if (num3 < (float)UnitConst.GetInstance().buildingConst[this.index].angle / 2f && num3 > (float)(-(float)UnitConst.GetInstance().buildingConst[this.index].angle) / 2f)
					{
						this.AllTargetBySanShe.Add(this.Targetes[i]);
					}
				}
			}
			if (this.AllTargetBySanShe.Count > 0)
			{
				IOrderedEnumerable<Character> orderedEnumerable = from a in this.AllTargetBySanShe
				orderby Vector3.Distance(a.tr.position, this.tr.position)
				select a;
				foreach (Character current in orderedEnumerable)
				{
					float num4 = Vector3.Distance(current.tr.position, this.tr.position) * -0.85f / 6f + 1.14166665f;
					if ((double)num4 < 0.1)
					{
						num4 = 0.1f;
					}
					if (num4 >= 1f)
					{
						bullet.target = current;
						bullet.transform.rotation = Quaternion.Euler(current.tr.position - this.tr.position);
						bullet.targetP = current.tr.position;
						bullet.SetInfo(this);
						return;
					}
					if (UnityEngine.Random.Range(0f, 1f) < num4)
					{
						bullet.target = current;
						bullet.transform.rotation = Quaternion.Euler(current.tr.position - this.tr.position);
						bullet.targetP = current.tr.position;
						bullet.SetInfo(this);
						return;
					}
				}
			}
			float num5 = UnityEngine.Random.Range((float)(-(float)UnitConst.GetInstance().buildingConst[this.index].angle) / 2f, (float)UnitConst.GetInstance().buildingConst[this.index].angle / 2f);
			if (Mathf.Abs(num5 - this.lastAngle) < 12f)
			{
				num5 += (float)(12 * this.n);
				this.n *= -1;
			}
			float num6 = Vector3.Distance(this.tr.position, tarPos);
			float num7 = UnityEngine.Random.Range(-num / 2f, num / 2f);
			if ((double)Mathf.Abs(num7 - num2) < 0.5)
			{
				num7 += 0.5f * (float)this.n;
				this.n *= -1;
			}
			float num8 = num6 + num7;
			if (num8 > UnitConst.GetInstance().buildingConst[this.index].maxRadius)
			{
				num7 = num8 - UnitConst.GetInstance().buildingConst[this.index].maxRadius;
				num8 = UnitConst.GetInstance().buildingConst[this.index].maxRadius;
			}
			float num9 = tarPos.x - this.tr.position.x;
			float num10 = tarPos.z - this.tr.position.z;
			float num11 = tarPos.x - this.tr.position.x;
			float num12 = tarPos.z - this.tr.position.z;
			float num13 = Mathf.Atan(num10 / num9);
			float num14 = (Mathf.Sqrt(Mathf.Pow(num9, 2f) + Mathf.Pow(num10, 2f)) + num7) / Mathf.Sqrt(1f + Mathf.Pow(Mathf.Tan(num13 - 0.0174532924f * num5), 2f));
			float num15 = num14 * Mathf.Tan(num13 - 0.0174532924f * num5);
			if (num11 > 0f && num12 > 0f)
			{
				num14 *= 1f;
				num15 *= 1f;
			}
			if (num11 < 0f && num12 > 0f)
			{
				num14 *= -1f;
				num15 *= -1f;
			}
			if (num11 > 0f && num12 < 0f)
			{
				num14 *= 1f;
				num15 *= 1f;
			}
			if (num11 < 0f && num12 < 0f)
			{
				num14 *= -1f;
				num15 *= -1f;
			}
			if ((num10 > 0f && num15 < 0f) || (num10 < 0f && num15 > 0f))
			{
				num15 *= -1f;
			}
			bullet.target = null;
			bullet.targetP = this.tr.position + new Vector3(num14, 0f, num15);
			bullet.transform.rotation = Quaternion.Euler(57.29578f * Mathf.Atan(transform.position.y / num8), transform.eulerAngles.y + num5, transform.eulerAngles.z);
			num2 = num7;
			this.lastAngle = num5;
		}
		bullet.SetInfo(this);
	}

	public override Vector3 GetVPos(T_TankAbstract thisTank)
	{
		Vector3 vector = Vector3.zero;
		if (thisTank == null)
		{
			return vector;
		}
		int num = SenceManager.inst.Radiuses.IndexOf(thisTank.CharacterBaseFightInfo.ShootMaxRadius - SenceManager.GetVPosDistance);
		if (UnitConst.GetInstance().soldierConst[thisTank.index].isCanFly)
		{
			num = SenceManager.inst.Radiuses.IndexOf(thisTank.CharacterBaseFightInfo.ShootMaxRadius - 3f - SenceManager.GetVPosDistance);
		}
		for (int i = num; i < SenceManager.inst.AttrackPoint.Count; i++)
		{
			List<Vector3> list = SenceManager.inst.AttrackPoint[i];
			float num2 = 10000f;
			for (int j = 0; j < list.Count; j++)
			{
				if (!this.posUsedList.ContainsKey(list[j]) || this.posUsedList[list[j]] == null)
				{
					float num3 = Vector3.Distance(thisTank.tr.position, this.tr.position + list[j]);
					if (num3 < num2)
					{
						num2 = num3;
						vector = list[j];
					}
				}
			}
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
			this.posUsedList[vector] = thisTank;
		}
		return T_TankAIManager.inst.CheckAttPoint(vector + this.tr.position, thisTank, this.tr) - this.tr.position;
	}

	public void TESLAFight()
	{
		this.dianciTime = UnitConst.GetInstance().buildingConst[this.index].frequency + this.dianciFightTime;
		base.InvokeRepeating("TESLAShoot", 0f, this.dianciTime);
		base.InvokeRepeating("TESLAShootCD", this.dianciFightTime, this.dianciTime);
	}

	public void TESLAShoot()
	{
		if (SenceManager.inst.MapElectricity == SenceManager.ElectricityEnum.电力充沛 && this.index == 23 && this.buildingState != T_Tower.TowerBuildingEnum.InBuilding)
		{
			AudioManage.inst.PlayAuidoBySelf_3D(UnitConst.GetInstance().buildingConst[this.index].fightSound, this.ga, false, 0uL);
			DieBall dieBall = PoolManage.Ins.CreatEffect("T_23_guiji", Vector3.zero, Quaternion.identity, this.tr);
			dieBall.tr.localPosition = Vector3.zero;
			dieBall.tr.localScale = Vector3.one;
			this.isDianciFight = true;
		}
		else
		{
			this.isDianciFight = false;
		}
	}

	public void TESLAShootCD()
	{
		this.isDianciFight = false;
		for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
		{
			SenceManager.inst.Tanks_Attack[i].MyBuffRuntime.RemoveBuff(Buff.BuffType.Halo);
		}
	}

	public void GetBuildBasic()
	{
		this.BuildBasic.Clear();
		this.BuildBasic_Animation.Clear();
		this.BuildBasic_Material.Clear();
		this.BuildBasic_Shader.Clear();
		if (this.ModelBody != null)
		{
			SkinnedMeshRenderer[] componentsInChildren = this.ModelBody.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = componentsInChildren[i];
				if (skinnedMeshRenderer.name != "Object04")
				{
					this.BuildBasic.Add(skinnedMeshRenderer.transform);
				}
			}
			MeshRenderer[] componentsInChildren2 = this.ModelBody.GetComponentsInChildren<MeshRenderer>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				MeshRenderer meshRenderer = componentsInChildren2[j];
				if ((!meshRenderer.transform.parent.GetComponent<Body_Model>() || meshRenderer.name == "RED" || meshRenderer.name == "BLUE") && meshRenderer.name.Length >= 3 && meshRenderer.name != "qiu01" && meshRenderer.transform.parent.name != "p_Q_build3_3_1")
				{
					this.BuildBasic.Add(meshRenderer.transform);
				}
			}
			Animation[] componentsInChildren3 = this.ModelBody.GetComponentsInChildren<Animation>();
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				Animation animation = componentsInChildren3[k];
				this.BuildBasic_Animation.Add(animation.GetComponent<Animation>());
			}
		}
		ParticleSystem[] componentsInChildren4 = this.tr.GetComponentsInChildren<ParticleSystem>();
		for (int l = 0; l < componentsInChildren4.Length; l++)
		{
			ParticleSystem particleSystem = componentsInChildren4[l];
			this.BuildBasic_ParticleSystem.Add(particleSystem.GetComponent<ParticleSystem>());
		}
		if (this.secType == 1)
		{
			UVAnimation[] componentsInChildren5 = this.ModelBody.GetComponentsInChildren<UVAnimation>();
			for (int m = 0; m < componentsInChildren5.Length; m++)
			{
				UVAnimation uVAnimation = componentsInChildren5[m];
				if (uVAnimation.name == "center_3_lvdai")
				{
					this.BuildBasic.Add(uVAnimation.transform);
				}
			}
		}
		if (this.secType == 20)
		{
			if (this.rightObj)
			{
				this.BuildBasic.Add(this.rightObj.GetComponentInChildren<MeshRenderer>().transform);
			}
			if (this.topObj)
			{
				this.BuildBasic.Add(this.topObj.GetComponentInChildren<MeshRenderer>().transform);
			}
		}
		if (this.secType == 21)
		{
			SkinnedMeshRenderer[] componentsInChildren6 = this.tr.GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int n = 0; n < componentsInChildren6.Length; n++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer2 = componentsInChildren6[n];
				if (skinnedMeshRenderer2.name != "Object04")
				{
					this.BuildBasic.Add(skinnedMeshRenderer2.transform);
				}
			}
		}
	}

	public void BuildFrozen_On(float on_time, float all_time, float off_time)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Ice_Debuff")) as GameObject;
		Material material = gameObject.GetComponent<MeshRenderer>().material;
		Shader shader = gameObject.GetComponent<MeshRenderer>().material.shader;
		UnityEngine.Object.Destroy(gameObject.gameObject);
		this.BuildFrozen = true;
		this.frozen_on_time = on_time;
		this.frozen_time = all_time;
		this.frozen_off_time = off_time;
		this.GetBuildBasic();
		this.frozen_level = 0.2f;
		for (int i = 0; i < this.BuildBasic.Count; i++)
		{
			if (this.BuildBasic[i] != null)
			{
				if (this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>())
				{
					SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
					skinnedMeshRenderer = this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>();
					this.BuildBasic_Material.Add(skinnedMeshRenderer.material);
					this.BuildBasic_Shader.Add(skinnedMeshRenderer.renderer.material.shader);
					skinnedMeshRenderer.material = material;
					skinnedMeshRenderer.renderer.material.shader = shader;
					skinnedMeshRenderer.material.SetTexture("_node_2", this.BuildBasic_Material[i].mainTexture);
					skinnedMeshRenderer.material.SetFloat("_ice", this.frozen_level);
					this.layer_out = skinnedMeshRenderer.gameObject.layer;
				}
				else if (this.BuildBasic[i].GetComponent<MeshRenderer>())
				{
					MeshRenderer meshRenderer = new MeshRenderer();
					meshRenderer = this.BuildBasic[i].GetComponent<MeshRenderer>();
					this.BuildBasic_Material.Add(meshRenderer.material);
					this.BuildBasic_Shader.Add(meshRenderer.renderer.material.shader);
					meshRenderer.material = material;
					meshRenderer.renderer.material.shader = shader;
					meshRenderer.material.SetTexture("_node_2", this.BuildBasic_Material[i].mainTexture);
					meshRenderer.material.SetFloat("_ice", this.frozen_level);
					this.layer_out = meshRenderer.gameObject.layer;
				}
				if (this.BuildBasic[i].GetComponent<UVAnimation>())
				{
					this.BuildBasic[i].GetComponent<UVAnimation>().enabled = false;
				}
			}
		}
		this.MyBuffRuntime.AddBuffIndex(0, this, new int[]
		{
			33
		});
		for (int j = 0; j < this.BuildBasic_Animation.Count; j++)
		{
			if (this.BuildBasic_Animation[j].Play())
			{
				this.BuildBasic_Animation[j].Stop();
				this.animation_Play = true;
			}
		}
		for (int k = 0; k < this.BuildBasic_ParticleSystem.Count; k++)
		{
			if (this.BuildBasic_ParticleSystem[k] != null)
			{
				this.BuildBasic_ParticleSystem[k].gameObject.SetActive(false);
			}
		}
	}

	private void BuildFrozen_Aready()
	{
		for (int i = 0; i < this.BuildBasic.Count; i++)
		{
			if (this.BuildBasic[i] != null)
			{
				if (this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>())
				{
					SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
					skinnedMeshRenderer = this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>();
					skinnedMeshRenderer.material.SetFloat("_ice", this.frozen_level);
				}
				else if (this.BuildBasic[i].GetComponent<MeshRenderer>())
				{
					MeshRenderer meshRenderer = new MeshRenderer();
					meshRenderer = this.BuildBasic[i].GetComponent<MeshRenderer>();
					meshRenderer.material.SetFloat("_ice", this.frozen_level);
				}
			}
		}
	}

	public void BuildFrozen_Off()
	{
		this.BuildFrozen = false;
		for (int i = 0; i < this.BuildBasic.Count; i++)
		{
			if (this.BuildBasic[i] != null)
			{
				if (this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>())
				{
					SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
					skinnedMeshRenderer = this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>();
					skinnedMeshRenderer.material = this.BuildBasic_Material[i];
					skinnedMeshRenderer.renderer.material.shader = this.BuildBasic_Shader[i];
					skinnedMeshRenderer.gameObject.layer = this.layer_out;
				}
				else if (this.BuildBasic[i].GetComponent<MeshRenderer>())
				{
					MeshRenderer meshRenderer = new MeshRenderer();
					meshRenderer = this.BuildBasic[i].GetComponent<MeshRenderer>();
					meshRenderer.material = this.BuildBasic_Material[i];
					meshRenderer.renderer.material.shader = this.BuildBasic_Shader[i];
					meshRenderer.gameObject.layer = this.layer_out;
				}
				if (this.BuildBasic[i].GetComponent<UVAnimation>())
				{
					this.BuildBasic[i].GetComponent<UVAnimation>().enabled = true;
				}
			}
		}
		for (int j = 0; j < this.BuildBasic_Animation.Count; j++)
		{
			if (this.animation_Play)
			{
				this.BuildBasic_Animation[j].Play();
			}
		}
		for (int k = 0; k < this.BuildBasic_ParticleSystem.Count; k++)
		{
			if (this.BuildBasic_ParticleSystem[k] != null)
			{
				this.BuildBasic_ParticleSystem[k].gameObject.SetActive(true);
			}
		}
	}

	public void SetColorBlack(bool True)
	{
		if (UnitConst.GetInstance().buildingConst[this.index].electricityShow != 1 || UnitConst.GetInstance().buildingConst[this.index].secType == 99 || this.index == 90)
		{
			return;
		}
		if (this.buildingState == T_Tower.TowerBuildingEnum.InBuilding)
		{
			return;
		}
		if (this.NoPower == True && !True)
		{
			return;
		}
		this.NoPower = True;
		if (True)
		{
			if (this.NoElectiony == null && ResourcePanelManage.inst)
			{
				this.NoElectiony = ResourcePanelManage.inst.OnNoelectricityPow(this.tr, 0);
			}
		}
		else if (this.NoElectiony)
		{
			UnityEngine.Object.Destroy(this.NoElectiony);
		}
		this.GetBuildBasic();
		Color color = new Color(0f, 0f, 0f, 1f);
		Color color2 = new Color(1f, 1f, 1f, 1f);
		Color color3 = new Color(0.3f, 0.3f, 0.3f, 1f);
		this.color_A_0 = new Color(1f, 1f, 1f, 1f);
		this.color_B_0 = new Color(1f, 1f, 1f, 1f);
		this.color_C_0 = new Color(0.3f, 0.3f, 0.3f, 1f);
		for (int i = 0; i < this.BuildBasic.Count; i++)
		{
			if (this.BuildBasic[i] != null)
			{
				if (this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>())
				{
					if (!this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>().enabled)
					{
						goto IL_4A9;
					}
					SkinnedMeshRenderer skinnedMeshRenderer = new SkinnedMeshRenderer();
					skinnedMeshRenderer = this.BuildBasic[i].GetComponent<SkinnedMeshRenderer>();
					skinnedMeshRenderer.renderer.material.shader = Shader.Find("VertexLit");
					if (True)
					{
						this.color_A_0 = skinnedMeshRenderer.renderer.sharedMaterial.GetColor("_Color");
						this.color_B_0 = skinnedMeshRenderer.renderer.sharedMaterial.GetColor("_SpecColor");
						this.color_C_0 = skinnedMeshRenderer.renderer.sharedMaterial.GetColor("_Emission");
						skinnedMeshRenderer.renderer.sharedMaterial.SetColor("_Color", color);
						skinnedMeshRenderer.renderer.sharedMaterial.SetColor("_SpecColor", color2);
						skinnedMeshRenderer.renderer.sharedMaterial.SetColor("_Emission", color3);
					}
					else
					{
						skinnedMeshRenderer.renderer.sharedMaterial.SetColor("_Color", this.color_A_0);
						skinnedMeshRenderer.renderer.sharedMaterial.SetColor("_SpecColor", this.color_B_0);
						skinnedMeshRenderer.renderer.sharedMaterial.SetColor("_Emission", this.color_C_0);
					}
				}
				else if (this.BuildBasic[i].GetComponent<MeshRenderer>())
				{
					if (!this.BuildBasic[i].GetComponent<MeshRenderer>().enabled)
					{
						goto IL_4A9;
					}
					MeshRenderer meshRenderer = new MeshRenderer();
					meshRenderer = this.BuildBasic[i].GetComponent<MeshRenderer>();
					meshRenderer.renderer.material.shader = Shader.Find("VertexLit");
					if (True)
					{
						this.color_A_0 = meshRenderer.renderer.sharedMaterial.GetColor("_Color");
						this.color_B_0 = meshRenderer.renderer.sharedMaterial.GetColor("_SpecColor");
						this.color_C_0 = meshRenderer.renderer.sharedMaterial.GetColor("_Emission");
						meshRenderer.renderer.sharedMaterial.SetColor("_Color", color);
						meshRenderer.renderer.sharedMaterial.SetColor("_SpecColor", color2);
						meshRenderer.renderer.sharedMaterial.SetColor("_Emission", color3);
					}
					else
					{
						meshRenderer.renderer.sharedMaterial.SetColor("_Color", this.color_A_0);
						meshRenderer.renderer.sharedMaterial.SetColor("_SpecColor", this.color_B_0);
						meshRenderer.renderer.sharedMaterial.SetColor("_Emission", this.color_C_0);
					}
				}
				if (this.BuildBasic[i].GetComponent<UVAnimation>())
				{
					this.BuildBasic[i].GetComponent<UVAnimation>().enabled = false;
				}
			}
			IL_4A9:;
		}
		Animation[] componentsInChildren = base.GetComponentsInChildren<Animation>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			Animation animation = componentsInChildren[j];
			animation.GetComponent<Animation>().enabled = !True;
		}
		for (int k = 0; k < this.BuildBasic_Animation.Count; k++)
		{
			if (this.BuildBasic_Animation[k].Play())
			{
				this.BuildBasic_Animation[k].Stop();
				this.BuildBasic_Animation[k].enabled = false;
				this.animation_Play = true;
			}
		}
		for (int l = 0; l < this.BuildBasic_ParticleSystem.Count; l++)
		{
			if (this.BuildBasic_ParticleSystem[l] != null)
			{
				this.BuildBasic_ParticleSystem[l].gameObject.SetActive(false);
			}
		}
	}

	public void ChooseByAtt()
	{
		if (UIManager.curState != SenceState.Attacking)
		{
			return;
		}
	}

	[DebuggerHidden]
	private IEnumerator chooseByAtt_Effect()
	{
		T_Tower.<chooseByAtt_Effect>c__Iterator31 <chooseByAtt_Effect>c__Iterator = new T_Tower.<chooseByAtt_Effect>c__Iterator31();
		<chooseByAtt_Effect>c__Iterator.<>f__this = this;
		return <chooseByAtt_Effect>c__Iterator;
	}

	public void RedPiece_Show1(bool True)
	{
		this.RedPiece.gameObject.SetActive(True);
	}

	public void RedPiece_Show(bool True)
	{
		if ((this.secType == 9 || this.secType == 5) && (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.Spy))
		{
			this.RedPiece.gameObject.SetActive(false);
			this.towerGridPlane_Attack.gameObject.SetActive(false);
			this.towerGridPlane.gameObject.SetActive(false);
			return;
		}
		if (True)
		{
			if (this.WallRedPiece)
			{
				this.RedPiece.gameObject.SetActive(true);
				if (this.index == 90)
				{
					bool flag = false;
					this.RedPiece.transform.localPosition = Vector3.zero;
					this.RedPiece.transform.localScale = Vector2.one * (float)(this.size + 3);
					if (this.RedPiece1 != null)
					{
						this.RedPiece1.transform.localPosition = Vector3.zero;
						this.RedPiece1.transform.localScale = Vector2.one * (float)(this.size + 3);
					}
					if (this.leftWall != null && this.rightWall == null)
					{
						this.WallRedPiece0 = true;
						flag = true;
						T_Tower t_Tower = this;
						int num = 1;
						while (t_Tower.leftWall != null)
						{
							t_Tower = t_Tower.leftWall;
							if (!t_Tower.WallRedPiece0)
							{
								t_Tower.WallRedPiece = false;
								t_Tower.RedPiece.gameObject.SetActive(false);
							}
							num++;
							this.RedPiece.transform.position = new Vector3(0.5f * (this.tr.position.x + t_Tower.tr.position.x), 0f, 0.5f * (this.tr.position.z + t_Tower.tr.position.z));
							this.RedPiece.transform.localScale = new Vector2((float)(this.size + 3), (float)(this.size * num + 3));
						}
					}
					if (this.topWall != null && this.bottomWall == null)
					{
						this.WallRedPiece0 = true;
						if (flag)
						{
							if (!(this.RedPiece1 != null) || !(this.RedPiece1 != this.RedPiece))
							{
								if (this.RedPiece1 == null)
								{
									this.RedPiece1 = (UnityEngine.Object.Instantiate(this.RedPiece, this.tr.position, this.RedPiece.transform.rotation) as GameObject);
									this.RedPiece1.transform.parent = this.tr;
								}
								else
								{
									this.RedPiece1 = this.RedPiece;
								}
							}
						}
						else
						{
							this.RedPiece1 = this.RedPiece;
						}
						T_Tower t_Tower2 = this;
						int num2 = 1;
						while (t_Tower2.topWall != null)
						{
							t_Tower2 = t_Tower2.topWall;
							if (!t_Tower2.WallRedPiece0)
							{
								t_Tower2.WallRedPiece = false;
								t_Tower2.RedPiece.gameObject.SetActive(false);
							}
							num2++;
							this.RedPiece1.transform.position = new Vector3(0.5f * (this.tr.position.x + t_Tower2.tr.position.x), 0f, 0.5f * (this.tr.position.z + t_Tower2.tr.position.z));
							this.RedPiece1.transform.localScale = new Vector2((float)(this.size * num2 + 3), (float)(this.size + 3));
						}
					}
				}
			}
			else if ((this.leftWall != null && this.rightWall == null) || (this.topWall != null && this.bottomWall == null))
			{
				this.WallRedPiece = true;
				this.RedPiece_Show(true);
			}
		}
		else
		{
			if (this.RedPiece)
			{
				this.RedPiece.gameObject.SetActive(false);
			}
			if (this.RedPiece1)
			{
				this.RedPiece1.gameObject.SetActive(false);
			}
		}
	}

	private void BuffTimePass()
	{
	}

	private void Update()
	{
		if (this.BuildFrozen)
		{
			if (this.frozen_time > 0f)
			{
				this.frozen_level = Mathf.Min(1f, this.frozen_level + 1f / this.frozen_on_time * Time.deltaTime);
				this.BuildFrozen_Aready();
				if (this.frozen_level >= 1f)
				{
					this.frozen_time -= Time.deltaTime;
				}
			}
			else if (this.frozen_time <= 0f)
			{
				this.frozen_level = Mathf.Max(0.2f, this.frozen_level - 1f / this.frozen_off_time * Time.deltaTime);
				this.BuildFrozen_Aready();
				if (this.frozen_level <= 0.2f)
				{
					this.BuildFrozen_Off();
				}
			}
		}
		this.BuffTimePass();
		if (this.index == 20)
		{
			if (!this.mine_boom)
			{
				for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
				{
					if (base.IsCanShootByCharFlak(SenceManager.inst.Tanks_Attack[i]))
					{
						if (Vector3.Distance(SenceManager.inst.Tanks_Attack[i].tr.position, this.tr.position) < UnitConst.GetInstance().buildingConst[this.index].maxRadius)
						{
							this.Mine_Boom();
							this.mine_boom = true;
							break;
						}
					}
				}
			}
		}
		else if (this.index == 23 && this.isDianciFight && base.IsCanShootByBuff())
		{
			for (int j = 0; j < SenceManager.inst.Tanks_Attack.Count; j++)
			{
				if (!SenceManager.inst.Tanks_Attack[j] || !base.IsCanShootByCharFlak(SenceManager.inst.Tanks_Attack[j]))
				{
				}
			}
		}
	}

	private void Mine_Boom()
	{
		PoolManage.Ins.CreatEffect(UnitConst.GetInstance().skillList[5].damageEffect, base.transform.position, new Quaternion(0f, 0f, 0f, 0f), SenceManager.inst.bulletPool);
		for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
		{
			if (base.IsCanShootByCharFlak(SenceManager.inst.Tanks_Attack[i]))
			{
				if (Vector3.Distance(SenceManager.inst.Tanks_Attack[i].tr.position, this.tr.position) < 2f * UnitConst.GetInstance().buildingConst[this.index].maxRadius)
				{
					SenceManager.inst.Tanks_Attack[i].DoHurt(this.CharacterBaseFightInfo.breakArmor);
				}
			}
		}
		if (FightPanelManager.inst)
		{
			FightPanelManager.inst.CreatFightMessage("地雷爆炸！", Color.red, base.transform);
		}
		this.DoHurt((int)base.CurLife + 50, -10L, true);
	}

	public void UpdateGraphs(bool isEnable)
	{
		this.bodyForAttack.enabled = true;
		if (isEnable)
		{
			this.bodyForAttack.gameObject.layer = 27;
		}
		else
		{
			this.bodyForAttack.gameObject.layer = 18;
		}
		SenceManager.inst.UpdateGraphs(this.bodyForAttack.bounds);
	}

	public void SenndBuildComplete(int money, string laiyuan, bool NoBuyTime = false)
	{
		if (NoBuyTime || HeroInfo.GetInstance().BuildCD.Contains(this.id))
		{
			CSBuildingEnd cSBuildingEnd = new CSBuildingEnd();
			cSBuildingEnd.buildingId = this.id;
			cSBuildingEnd.money = money;
			if (this.trueLv < 1)
			{
				this.isCanDisplayInfoBtn = false;
				ClientMgr.GetNet().SendHttp(2030, cSBuildingEnd, new DataHandler.OpcodeHandler(BuildingHandler.GC_NewBuildingAddEnd), null);
			}
			else
			{
				this.isCanDisplayInfoBtn = false;
				ClientMgr.GetNet().SendHttp(2030, cSBuildingEnd, new DataHandler.OpcodeHandler(BuildingHandler.GC_NewBuildingUpdateEnd), null);
			}
		}
	}

	[DebuggerHidden]
	public IEnumerator UpdateingOrRecovering(Action<T_Tower> CallBack = null)
	{
		T_Tower.<UpdateingOrRecovering>c__Iterator32 <UpdateingOrRecovering>c__Iterator = new T_Tower.<UpdateingOrRecovering>c__Iterator32();
		<UpdateingOrRecovering>c__Iterator.CallBack = CallBack;
		<UpdateingOrRecovering>c__Iterator.<$>CallBack = CallBack;
		<UpdateingOrRecovering>c__Iterator.<>f__this = this;
		return <UpdateingOrRecovering>c__Iterator;
	}

	protected void InBuilding()
	{
		if (this.buildingState != T_Tower.TowerBuildingEnum.InBuilding)
		{
			this.buildingState = T_Tower.TowerBuildingEnum.InBuilding;
			this.isCanDisplayInfoBtn = false;
			this.HideLogo();
			this.InBuildingDoBehaviour();
		}
	}

	protected virtual void BuildingEnd()
	{
		this.DisplayLogo();
		this.BuildingEndDoBehaviour();
	}

	protected virtual void InBuildingDoBehaviour()
	{
		if (UIManager.curState != SenceState.Spy && UIManager.curState != SenceState.Visit && UIManager.curState != SenceState.Attacking && ResourcePanelManage.inst)
		{
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 6 || UnitConst.GetInstance().buildingConst[this.index].secType == 21)
			{
				if (UnitConst.GetInstance().GetArmyId(this.index, this.trueLv) > 0)
				{
					if (this.ArmyTitleNew == null)
					{
						this.ArmyTitleNew = ResourcePanelManage.inst.AddArmyTitleByTime(this.tr).GetComponent<ArmyTitleShow>();
						this.ArmyTitleNew.tar = this.tr;
						if (UnitConst.GetInstance().buildingConst[this.index].secType == 6)
						{
							this.ArmyTitleNew.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().armyBuildingCDTime);
						}
						else
						{
							this.ArmyTitleNew.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().airBuildingCDTime);
						}
						this.ArmyTitleNew.SetUpdatingEum(this.index, this.trueLv);
					}
				}
				else if (this.BuilindingCDInfo == null)
				{
					this.BuilindingCDInfo = ResourcePanelManage.inst.AddChildByTime(this.tr).GetComponent<TimeTittleBtn>();
					this.BuilindingCDInfo.tar = this.tr;
					if (UnitConst.GetInstance().buildingConst[this.index].secType == 6)
					{
						this.BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().armyBuildingCDTime);
					}
					else
					{
						this.BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().airBuildingCDTime);
					}
					this.BuilindingCDInfo.SetUpdatingEum(1);
				}
			}
			else if (this.BuilindingCDInfo == null)
			{
				this.BuilindingCDInfo = ResourcePanelManage.inst.AddChildByTime(this.tr).GetComponent<TimeTittleBtn>();
				this.BuilindingCDInfo.tar = this.tr;
				if (HeroInfo.GetInstance().BuildingCDEndTime.ContainsKey(this.id))
				{
					this.BuilindingCDInfo.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().BuildingCDEndTime[this.id]);
				}
				this.BuilindingCDInfo.SetUpdatingEum(1);
			}
			if (this.nolectricityPow != null)
			{
				this.nolectricityPow.ga.SetActive(false);
			}
			if (this.resTip != null)
			{
				UnityEngine.Object.Destroy(this.resTip.ga);
			}
		}
		if (this.T_TowerFightingState)
		{
			this.T_TowerFightingState.enabled = false;
		}
		if (UnitConst.GetInstance().buildingConst[this.index].resType < 3)
		{
			AudioManage.inst.PlayAuidoBySelf_3D("quedingjianzao", this.ga, false, 0uL);
			if (this.updateTittle && this.updateTittle.gameObject.activeInHierarchy)
			{
				this.updateTittle.gameObject.SetActive(false);
			}
			if (this.size >= 7)
			{
				base.CreateBody("77shengji");
			}
			else if (this.size >= 4)
			{
				base.CreateBody("55shengji");
			}
			else
			{
				base.CreateBody("33shengji");
			}
			if (this.ModelBody)
			{
				if (this.ModelBody.Red_DModel)
				{
					this.ModelBody.Red_DModel.gameObject.SetActive(false);
				}
				if (this.ModelBody.Blue_DModel)
				{
					this.ModelBody.Blue_DModel.gameObject.SetActive(false);
				}
			}
			if (SenceInfo.curMap.IsMyMap)
			{
				this.modelClore = Enum_ModelColor.Blue;
				if (this.ModelBody && this.ModelBody.BlueModel)
				{
					this.ModelBody.BlueModel.gameObject.SetActive(true);
				}
				if (this.ModelBody && this.ModelBody.RedModel)
				{
					this.ModelBody.RedModel.gameObject.SetActive(false);
				}
			}
			else
			{
				this.modelClore = Enum_ModelColor.Red;
				if (this.ModelBody && this.ModelBody.BlueModel)
				{
					this.ModelBody.BlueModel.gameObject.SetActive(false);
				}
				if (this.ModelBody && this.ModelBody.RedModel)
				{
					this.ModelBody.RedModel.gameObject.SetActive(true);
				}
			}
			if (this.ModelBody)
			{
				Transform[] componentsInChildren = this.ModelBody.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].gameObject.layer = 0;
				}
			}
			if (this.index == 23 || this.index == 19)
			{
				this.DieEffect = PoolManage.Ins.CreatEffect("build_yanwu_ta", this.tr.position, Quaternion.identity, this.tr);
				this.DieEffect.LifeTime = 3f;
			}
			else if (UnitConst.GetInstance().buildingConst[this.index].hight > 3f)
			{
				this.DieEffect = PoolManage.Ins.CreatEffect("build_yanwu_leida", this.tr.position, Quaternion.identity, this.tr);
				this.DieEffect.LifeTime = 3f;
			}
			else if (this.size > 4)
			{
				this.DieEffect = PoolManage.Ins.CreatEffect("build_yanwu_7x7", this.tr.position, Quaternion.identity, this.tr);
				this.DieEffect.LifeTime = 3f;
			}
			else if (this.size > 3)
			{
				this.DieEffect = PoolManage.Ins.CreatEffect("build_yanwu_4x4", this.tr.position, Quaternion.identity, this.tr);
				this.DieEffect.LifeTime = 3f;
			}
			else
			{
				this.DieEffect = PoolManage.Ins.CreatEffect("build_yanwu_3x3", this.tr.position, Quaternion.identity, this.tr);
				this.DieEffect.LifeTime = 3f;
			}
		}
	}

	public void FreshArmyFuncUITimeBehaviour()
	{
		if (UIManager.curState == SenceState.Home || UIManager.curState == SenceState.InBuild)
		{
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 15)
			{
				if (HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd != null && HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime > 0L)
				{
					if (this.peibingTime == null && ResourcePanelManage.inst)
					{
						this.peibingTime = ResourcePanelManage.inst.AddChildByTime(this.tr).GetComponent<TimeTittleBtn>();
						this.peibingTime.tar = this.tr;
					}
					if (this.peibingTime)
					{
						this.peibingTime.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().PlayerCommandoFuncingOrEnd.cdTime);
						this.peibingTime.SetUpdatingEum(2);
					}
					if (this.peibinging == null && ResourcePanelManage.inst)
					{
						this.peibinging = ResourcePanelManage.inst.AddChildByDescriptInfo(this.tr, "配兵中");
						this.peibinging.GetComponent<DescripUIInfo>().tipSprite.enabled = false;
						this.peibinging.GetComponent<DescripUIInfo>().textDes.text = LanguageManage.GetTextByKey("配兵中", "others");
					}
				}
				else
				{
					if (this.peibinging)
					{
						UnityEngine.Object.Destroy(this.peibinging);
					}
					if (this.peibingTime)
					{
						UnityEngine.Object.Destroy(this.peibingTime.ga);
					}
				}
			}
			else if (HeroInfo.GetInstance().IsArmyFuncingBuilding(this.id))
			{
				if (this.peibingTime == null && ResourcePanelManage.inst)
				{
					this.peibingTime = ResourcePanelManage.inst.AddChildByTime(this.tr).GetComponent<TimeTittleBtn>();
					this.peibingTime.tar = this.tr;
				}
				if (this.peibingTime)
				{
					this.peibingTime.endTime = TimeTools.ConvertLongDateTime(HeroInfo.GetInstance().GetArmyFuncingDataByBuilding(this.id).key);
					this.peibingTime.SetUpdatingEum(2);
				}
				if (this.peibinging == null && ResourcePanelManage.inst)
				{
					this.peibinging = ResourcePanelManage.inst.AddChildByDescriptInfo(this.tr, "配兵中");
					this.peibinging.GetComponent<DescripUIInfo>().tipSprite.enabled = false;
					this.peibinging.GetComponent<DescripUIInfo>().textDes.text = LanguageManage.GetTextByKey("配兵中", "others");
				}
			}
			else
			{
				if (this.peibinging)
				{
					UnityEngine.Object.Destroy(this.peibinging);
				}
				if (this.peibingTime)
				{
					UnityEngine.Object.Destroy(this.peibingTime.ga);
				}
			}
		}
	}

	public void FreshArmyFuncEnd(bool isFirst = false)
	{
		if (UIManager.curState == SenceState.Home || UIManager.curState == SenceState.InBuild)
		{
			if (UnitConst.GetInstance().buildingConst[this.index].secType == 15)
			{
				if (HeroInfo.GetInstance().Commando_Fight != null)
				{
					if (this.paradeTank_Commander == null)
					{
						Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierList[HeroInfo.GetInstance().Commando_Fight.index].bodyId, null);
						this.paradeTank_Commander = GameTools.GetCompentIfNoAddOne<ParadeTank>(modelByBundleByName.ga);
						this.paradeTank_Commander.tr.localScale = Vector3.one * 5f;
						this.paradeTank_Commander.SetInfo(this, this.ParadeTanks.Count + 1);
					}
				}
				else if (this.paradeTank_Commander)
				{
					UnityEngine.Object.Destroy(this.paradeTank_Commander.ga);
				}
			}
			else if (!HeroInfo.GetInstance().AllArmyInfo.ContainsKey(this.id) || UnitConst.GetInstance().buildingConst[this.index].secType != 6)
			{
				if (HeroInfo.GetInstance().AllArmyInfo.ContainsKey(this.id) && UnitConst.GetInstance().buildingConst[this.index].secType == 21)
				{
					bool flag = false;
					if (base.GetComponent<T_TowerAir>() && SenceManager.inst.CurSelectTower != this)
					{
						return;
					}
					foreach (KVStruct current in HeroInfo.GetInstance().AllArmyInfo[this.id].armyFunced)
					{
						if (current.value > 0L)
						{
							flag = true;
							if (!base.GetComponent<T_TowerAir>())
							{
								this.ga.AddComponent<T_TowerAir>();
							}
							T_TowerAir component = base.GetComponent<T_TowerAir>();
							if ((long)component.airModelIndex != current.key)
							{
								if (component.Air_Model)
								{
									UnityEngine.Object.Destroy(component.Air_Model.ga);
								}
								Body_Model modelByBundleByName2 = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[(int)(checked((IntPtr)current.key))].bodyId, null);
								modelByBundleByName2.tr.parent = this.tr;
								modelByBundleByName2.tr.localPosition = Vector3.zero + new Vector3(0.9f, 0f, 0f);
								component.Air_Model = modelByBundleByName2;
								component.Air_Return();
							}
							break;
						}
					}
					if (!flag)
					{
						T_TowerAir component2 = base.GetComponent<T_TowerAir>();
						if (component2 && component2.Air_Model)
						{
							UnityEngine.Object.Destroy(component2.Air_Model.ga);
						}
					}
				}
			}
		}
		else if (isFirst)
		{
		}
	}

	public void ShowArmyFuncShow()
	{
		if (this.bubing == null)
		{
			this.bubing = ResourcePanelManage.inst.AddChildByDescriptInfo(this.tr, "补兵");
			this.bubing.GetComponent<DescripUIInfo>().tipSprite.enabled = false;
			this.bubing.GetComponent<DescripUIInfo>().textDes.text = LanguageManage.GetTextByKey("补兵", "others");
		}
		if (this.bubingJinatou == null)
		{
			this.bubingJinatou = PoolManage.Ins.GetModelByBundleByName("JT1", this.tr);
			this.bubingJinatou.tr.localPosition = new Vector3(0f, 4f, 0f);
			this.bubingJinatou.tr.localScale = Vector3.one * 2f;
			TweenPosition.Begin(this.bubingJinatou.ga, 0.6f, new Vector3(0f, 2f, 0f)).style = UITweener.Style.PingPong;
		}
	}

	public void HideBuBing()
	{
		if (this.bubing)
		{
			UnityEngine.Object.Destroy(this.bubing);
		}
		if (this.bubingJinatou)
		{
			UnityEngine.Object.Destroy(this.bubingJinatou.ga);
		}
	}

	protected virtual void BuildingEndDoBehaviour()
	{
		if (this.buildingState != T_Tower.TowerBuildingEnum.Normal || this.isFirst)
		{
			if (this.DieEffect != null)
			{
				UnityEngine.Object.Destroy(this.DieEffect.ga);
				this.DieEffect = null;
			}
			SenceManager.inst.CreateMuraille(this);
			if (this.nolectricityPow != null)
			{
				this.nolectricityPow.ga.SetActive(true);
			}
			if (SenceInfo.curMap.towerList_Data.ContainsKey(this.id))
			{
				this.lv = (this.trueLv = SenceInfo.curMap.towerList_Data[this.id].lv);
			}
			else
			{
				this.lv = 1;
				this.trueLv = 0;
			}
			this.isFirst = false;
			this.ReLoadModelAndFightState();
			this.buildingState = T_Tower.TowerBuildingEnum.Normal;
		}
	}

	public void ReLoadModelAndFightState()
	{
		base.CreateBody(this.BodyName);
		this.ReloadModelColorAndShootP();
		base.GetHeadOrMuzzle();
		if (this.T_TowerFightingState)
		{
			this.T_TowerFightingState.enabled = true;
		}
		if (this.head)
		{
			this.head.rotation = Quaternion.identity;
		}
		if (this.muzzle)
		{
			this.muzzle.rotation = Quaternion.identity;
		}
		this.RebackShader();
	}

	public void SenndBuildCompleteNoBuyTime()
	{
		this.SenndBuildComplete(0, "时间结束事件回调", true);
	}
}
