using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class T_SupperBullet : MonoBehaviour
{
	private Transform tr;

	private GameObject bodyRes;

	public int type;

	public int idx;

	public int skillID;

	public int hitCount;

	public Vector3 shootPos;

	public int renju;

	public int index;

	private float speed = 24f;

	public SphereCollider sc;

	public bool track;

	private bool flying = true;

	private GameObject juji;

	private bool isEndOver;

	private float distanceToTarget;

	private Body_Model res;

	private void Awake()
	{
		this.tr = base.transform;
		this.bodyRes = (GameObject)Resources.Load(ResManager.BulletRes_Path + "SupperBullet");
	}

	[DebuggerHidden]
	private IEnumerator Shoot_PaoWuXian()
	{
		T_SupperBullet.<Shoot_PaoWuXian>c__Iterator27 <Shoot_PaoWuXian>c__Iterator = new T_SupperBullet.<Shoot_PaoWuXian>c__Iterator27();
		<Shoot_PaoWuXian>c__Iterator.<>f__this = this;
		return <Shoot_PaoWuXian>c__Iterator;
	}

	private void Start()
	{
		this.CreatBody();
		this.tr.LookAt(new Vector3(this.shootPos.x, this.tr.position.y, this.shootPos.z));
		base.StartCoroutine(this.Shoot_PaoWuXian());
	}

	private void Update()
	{
		if (!this.flying)
		{
			this.sc.radius += 50f * Time.deltaTime;
			if (this.sc.radius > (float)UnitConst.GetInstance().skillList[this.idx].hurtRadius)
			{
				if (this.index == this.renju - 1)
				{
					CameraControl.inst.IsGraySence = false;
					if (this.renju == 1)
					{
					}
				}
				UnityEngine.Object.Destroy(this.tr.gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	private void Hit(Collider cli)
	{
		if (this.juji != null)
		{
			UnityEngine.Object.Destroy(this.juji);
		}
		this.flying = false;
		this.isEndOver = true;
		this.CreateDieEffect();
		if (cli == null)
		{
			return;
		}
		if (cli.CompareTag("Tower"))
		{
			this.HurtObj(1, cli);
		}
		else if (cli.CompareTag("Tank"))
		{
			if (cli.GetComponent<T_Tank>().charaType == Enum_CharaType.defender)
			{
				this.HurtObj(1, cli);
			}
			else
			{
				this.HurtObj(2, cli);
			}
		}
	}

	private void CreateDieEffect()
	{
		DieBall dieBall = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().skillList[this.skillID].damageEffect, this.tr.position + new Vector3(0f, 0.3f, 0f), this.tr.rotation, SenceManager.inst.bulletPool);
	}

	private void CreatBody()
	{
		this.res = PoolManage.Ins.GetEffectByName(UnitConst.GetInstance().skillList[this.skillID].bodyEffect, this.tr);
		if (this.res == null)
		{
			UnityEngine.Object.Destroy(base.gameObject, 1.5f);
		}
	}

	private void HurtObj(int type, Collider other)
	{
		if (this.juji != null)
		{
			UnityEngine.Object.Destroy(this.juji);
		}
		if (type == 1)
		{
			if (UnitConst.GetInstance().skillList[this.idx].targetType == 2)
			{
				return;
			}
			T_Tower componentInParent = other.GetComponentInParent<T_Tower>();
			if (componentInParent != null)
			{
				if (UnitConst.GetInstance().buildingConst[componentInParent.index].secType == 9)
				{
					componentInParent.MineHurt();
				}
				else
				{
					componentInParent.SuSkillHurt(this.idx);
				}
			}
			else
			{
				T_Tank component = other.GetComponent<T_Tank>();
				if (component != null)
				{
					component.SuSkillHurt(this.idx);
				}
			}
		}
		else if (type == 2)
		{
			if (UnitConst.GetInstance().skillList[this.idx].targetType == 1)
			{
				return;
			}
			T_Tank component2 = other.GetComponent<T_Tank>();
			if (component2 != null)
			{
				component2.SuSkillHurt(this.idx);
			}
		}
		this.hitCount--;
		if (this.hitCount < 1)
		{
			UnityEngine.Object.Destroy(this.tr.gameObject, 1f);
		}
	}
}
