using System;
using System.Collections.Generic;
using UnityEngine;

public class NewBuildingInfo
{
	public int resType;

	public int secType;

	public int storeType;

	public int electricityShow;

	public int resIdx;

	public float TowerSize;

	public string infTips;

	public float[] particleSizeArr;

	public float[] particleInfo;

	public float[] modelRostion;

	public float[] modlePosition;

	public int[] TowerQuaternion;

	public string name;

	public string bodyID;

	public string bigIcon;

	public string iconId;

	public string description;

	public int size;

	public float bodySize;

	public int battleIdFieldId;

	public int sizeForSelect;

	public string PlayerLevelInfo;

	public float hight;

	public float hightForShanDian;

	public ResType outputType;

	public int[] dropType;

	public float maxRadius;

	public float minRadius;

	public string NewbieGroup;

	public List<NewTowerLvInfo> lvInfos = new List<NewTowerLvInfo>();

	public List<TowerUpdate> UpdateStarInfos = new List<TowerUpdate>();

	public List<TowerStrong> StrongInfos = new List<TowerStrong>();

	public List<BuildingGrade> buildGradeInfos = new List<BuildingGrade>();

	public TowerGrid[] towerGrids;

	public BuildUIType buildUIType;

	public List<int> uiShows = new List<int>();

	public int headRotationSpeed;

	public string desForNewBie;

	public Dictionary<int, int> NewbiArant = new Dictionary<int, int>();

	public int MaxNum;

	public Vector3 updateTittlePos;

	public Vector3 updateTittleRotion;

	public Vector3 modelclearPos;

	public Vector3 modelclearScale;

	public bool IsMultipleAttack;

	public bool isTrack;

	public Enum_AttackPointType attackPoint;

	public Enum_GetTargetType GetTarType;

	public int bulletType;

	public bool isByPhysic;

	public Vector3[] selectBlue_Home;

	public Vector3[] selectRed_Attack;

	public Vector3[] guid_display;

	public int flak = 1;

	public string fightEffect;

	public string BodyEffect;

	public string DamageEffect;

	public string fightSound;

	public string DamageSound;

	public float frequency;

	public int angle;

	public int renju;

	public float renjuCD;

	public bool isShootSearchTarget;

	public bool isEndLianji;

	public int bulletSpeed;

	public float hurtRadius;

	public List<int> BuffIdx = new List<int>();

	public int BulletInAngle;

	public int fireSound;

	public string hitSound;

	public int dieSound;

	public int upgradeSound;

	public int pickSound;

	public string xuetiao_home;
}
