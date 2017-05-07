using System;
using UnityEngine;

public class T_MotherShip : MonoBehaviour
{
	public static T_MotherShip inst;

	public Transform tr;

	private GameObject S_BulletRes;

	private int type;

	private int idx;

	private int renju = 1;

	private float lastTime;

	private float random;

	private Vector3[] shootPos;

	public void OnDestroy()
	{
		T_MotherShip.inst = null;
	}

	private void Awake()
	{
		this.tr = base.transform;
		this.S_BulletRes = (GameObject)Resources.Load(ResManager.BulletRes_Path + "SupperBullet");
		T_MotherShip.inst = this;
	}
}
