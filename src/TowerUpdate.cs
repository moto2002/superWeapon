using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpdate
{
	public int id;

	public int itemid;

	public int level;

	public int needlevel;

	public Dictionary<ResType, int> resCost = new Dictionary<ResType, int>();

	public Dictionary<int, int> itemCost = new Dictionary<int, int>();

	public List<int> specialshow = new List<int>();

	public Vector3[] makeShow;

	public float critPer;

	public int resistPer;

	public int avoidDef;

	public string bodyName;

	public string des;

	public float frequency;

	public string CD;

	public int renju;

	public float renjuCD;

	public float hurtRadius;

	public List<int> buffid = new List<int>();

	public float headRotationSpeed;

	public int isTrack;

	public int attackPoint;

	public int GetTarType;

	public int bulletType;

	public string fightEffect;

	public string BodyEffect;

	public string DamageEffect;
}
