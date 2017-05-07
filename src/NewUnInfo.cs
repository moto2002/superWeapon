using System;
using System.Collections.Generic;
using UnityEngine;

public class NewUnInfo
{
	public NewUnInfo info;

	public int unitType;

	public int unitId;

	public string name;

	public string description;

	public string icon;

	public float uisize;

	public string bodyId;

	public float size;

	public float hight;

	public int peopleNum;

	public int counts;

	public int restraint;

	public bool IsMultipleAttack;

	public float speed;

	public float roatSpeed;

	public float headSpeed;

	public bool isByPhysic;

	public float floowRadius;

	public float eyeRadius;

	public float maxRadius;

	public float minRadius;

	public int presence;

	public bool patrol;

	public bool inFeiji;

	public bool isCanFly;

	public int bornType;

	public float accelerationSpeed;

	public string dirtyBack;

	public List<NewUnLvInfo> lvInfos = new List<NewUnLvInfo>();

	public float funcTime;

	public List<int> uiShows = new List<int>();

	public int lifeStar;

	public int attackStar;

	public int defendStar;

	public int speedStar;

	public int shootFarStar;

	public Vector3[] armyModelShow;

	public Vector3 army_UpdateScale;

	public Vector3 army_JiJieDianScale;

	public Vector3 army_UpdatePosition;

	public Vector3 army_JiJieDianPosition;

	public Vector3 army_UpdateRotation;

	public Vector3 army_JiJieDianRotation;

	public Vector3 army_UpdateTaziScale;

	public Vector3 army_UpdateTaziPosition;

	public Vector3 army_UpdateTaziRotation;

	public Vector3 army_TuoZhuaiScale;

	public Vector3 army_UpdateTaziScale_InCenter;

	public Vector3 army_UpdateScale_InCenter;

	public Vector3 modelclearRotation_TInfo;

	public Vector3 modelclearPos_TInfo;

	public Vector3 modelclearScale_TInfo;

	public bool isTrack;

	public Enum_AttackPointType attackPoint;

	public Enum_GetTargetType GetTarType;

	public int bulletType;

	public string fightEffect;

	public string BodyEffect;

	public string DamageEffect;

	public string fightSound;

	public string DamageSound;

	public float frequency;

	public float frequency1;

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

	public int moveSound;
}
