using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class T_BulletNew : IMonoBehaviour
{
	public Enum_ModelColor modelClore;

	public Enum_CharaType ownerType = Enum_CharaType.attacker;

	public int ownerIdx;

	public int ownerLv = 1;

	public int bulletType;

	private bool throwHit;

	private float bullerSpeed = 23f;

	private float damage = 1f;

	public Vector3 targetP;

	public Character target;

	public SphereCollider sc;

	public Transform t;

	public T_TankAbstract Tank_Send;

	public T_Tower Tower_Send;

	public float maxRadius;

	private Vector3 startPos;

	public int shootPIndex;

	private bool isEndOver;

	public Vector3 lightBulletStartPos;

	private Enum_AttackPointType GetEnemyPos;

	private float awakeTime;

	private Tweener twen;

	public bool isSstate;

	public int isSStateY;

	public float maxHurtR;

	private static Material GuanLin;

	private static Material GuanLin_Red;

	private bool isZheSheEnd;

	public string bodyEffect;

	public string damageEffect;

	private Body_Model body;

	private bool isEnd;

	private List<Collider> AllHurtColliderList = new List<Collider>();

	public float hurtRadius;

	private float distanceToTarget;

	private bool move = true;

	public float angelmodu = 15f;

	private Vector3 targetPos;

	private bool isFirstJianShe = true;

	private BaseFightInfo bulletBaseFightInfo;

	public override void Awake()
	{
		base.Awake();
		if (!T_BulletNew.GuanLin)
		{
			T_BulletNew.GuanLin = (Resources.Load("GuangLin", typeof(Material)) as Material);
		}
		if (!T_BulletNew.GuanLin_Red)
		{
			T_BulletNew.GuanLin_Red = (Resources.Load("GuangLin_Red", typeof(Material)) as Material);
		}
		this.sc = base.GetComponent<SphereCollider>();
	}

	private float GetBulletTime()
	{
		if (this.Tank_Send && this.Tank_Send.index == 4)
		{
			return this.bullerSpeed + (Time.time - this.awakeTime) * 6f;
		}
		return this.bullerSpeed;
	}

	private void DoJob()
	{
		this.startPos = this.tr.position;
		this.throwHit = false;
		this.isEndOver = false;
		this.isEnd = false;
		this.awakeTime = Time.time;
		this.move = true;
		this.sc.radius = 0.4f;
		this.tr.parent = SenceManager.inst.bulletPool;
		this.CreatBody();
		if (this.bulletType == 1)
		{
			this.PlayShoot();
			return;
		}
		if (this.bulletType == 2)
		{
			this.Play_S_Shoot();
			return;
		}
		if (this.bulletType == 5)
		{
			if (this.Tank_Send != null)
			{
				CoroutineInstance.DoJob(this.ShootLight(this.target, this.lightBulletStartPos, this.targetP, 0.5f, new Action(this.DOJianShe)));
			}
			if (this.Tower_Send != null)
			{
				CoroutineInstance.DoJob(this.ShootLight(this.target, this.lightBulletStartPos, this.targetP, 0.7f, new Action(this.DOJianShe)));
			}
		}
		int num = this.bulletType;
		if (num == 0)
		{
			this.tr.LookAt(this.targetP);
			this.twen = this.tr.DOMove(this.targetP, Vector3.Distance(this.tr.position, this.targetP) / this.bullerSpeed, false);
			this.twen.OnComplete(delegate
			{
				if (!this.isEndOver)
				{
					if (this.target != null)
					{
						if (!this.Chose(this.target.bodyForAttack, 0))
						{
							this.Hit(null, 2);
						}
					}
					else
					{
						this.Hit(null, 2);
					}
				}
				else
				{
					this.Hit(null, 2);
				}
			});
		}
	}

	[DebuggerHidden]
	public IEnumerator ShootLight(Character attackTar, Vector3 start, Vector3 end, float width = 0.5f, Action HurtCallBack = null)
	{
		T_BulletNew.<ShootLight>c__Iterator24 <ShootLight>c__Iterator = new T_BulletNew.<ShootLight>c__Iterator24();
		<ShootLight>c__Iterator.start = start;
		<ShootLight>c__Iterator.width = width;
		<ShootLight>c__Iterator.end = end;
		<ShootLight>c__Iterator.attackTar = attackTar;
		<ShootLight>c__Iterator.HurtCallBack = HurtCallBack;
		<ShootLight>c__Iterator.<$>start = start;
		<ShootLight>c__Iterator.<$>width = width;
		<ShootLight>c__Iterator.<$>end = end;
		<ShootLight>c__Iterator.<$>attackTar = attackTar;
		<ShootLight>c__Iterator.<$>HurtCallBack = HurtCallBack;
		<ShootLight>c__Iterator.<>f__this = this;
		return <ShootLight>c__Iterator;
	}

	public void PlayAudio()
	{
		if (AudioManage.inst.IsOpenMusic)
		{
			if (this.Tank_Send != null)
			{
				AudioClip audio = this.GetAudio(UnitConst.GetInstance().soldierConst[this.Tank_Send.index].hitSound);
				if (audio != null)
				{
					base.GetComponent<AudioSource>().clip = audio;
					base.GetComponent<AudioSource>().Play();
					base.GetComponent<AudioSource>().loop = false;
				}
			}
			else if (this.Tower_Send)
			{
				AudioClip audio2 = this.GetAudio(UnitConst.GetInstance().buildingConst[this.Tower_Send.index].hitSound);
				if (audio2 != null)
				{
					base.GetComponent<AudioSource>().clip = audio2;
					base.GetComponent<AudioSource>().Play();
					base.GetComponent<AudioSource>().loop = false;
				}
			}
		}
	}

	private AudioClip GetAudio(string audioName)
	{
		AudioClip result = null;
		for (int i = 0; i < AudioManage.inst.audioList.Count; i++)
		{
			if (AudioManage.inst.audioList[i].name == audioName)
			{
				result = AudioManage.inst.audioList[i];
			}
		}
		return result;
	}

	private void Update()
	{
		if (!this.isEndOver && this.bulletType == -1)
		{
			if (this.target != null && !this.target.IsDie)
			{
				this.tr.LookAt(this.target.tr);
				this.tr.Translate(Vector3.forward * this.bullerSpeed * Time.deltaTime);
			}
			else
			{
				this.Hit(null, 2);
			}
		}
	}

	private void FixedUpdate()
	{
		if (this.throwHit)
		{
			if (this.sc.radius >= this.maxHurtR)
			{
				this.DestoryBullet();
				return;
			}
			float num = this.sc.radius + 150f * Time.deltaTime;
			this.sc.radius = ((num < this.maxHurtR) ? num : this.maxHurtR);
		}
	}

	private void CreatBody()
	{
		this.body = PoolManage.Ins.GetEffectByName(this.bodyEffect, this.tr);
		if (this.body)
		{
			if (this.modelClore == Enum_ModelColor.Blue)
			{
				if (this.body.BlueModel)
				{
					this.body.BlueModel.gameObject.SetActive(true);
				}
				if (this.body.RedModel)
				{
					this.body.RedModel.gameObject.SetActive(false);
				}
			}
			else if (this.modelClore == Enum_ModelColor.Red)
			{
				if (this.body.BlueModel)
				{
					this.body.BlueModel.gameObject.SetActive(false);
				}
				if (this.body.RedModel)
				{
					this.body.RedModel.gameObject.SetActive(true);
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!this.ga.activeSelf)
		{
			return;
		}
		this.PlayAudio();
		if (!this.throwHit)
		{
			if (other.name.Equals("TerrBox"))
			{
				this.Hit(other, 2);
			}
			else
			{
				this.Chose(other, 0);
			}
		}
		else if (other.name.Equals("TerrBox"))
		{
			this.Hit(other, 2);
		}
		else
		{
			this.Chose(other, 1);
		}
	}

	private bool Chose(Collider other, int hitType)
	{
		if (this.AllHurtColliderList.Contains(other))
		{
			return false;
		}
		this.AllHurtColliderList.Add(other);
		if (this.isEnd)
		{
			return false;
		}
		if (this.Tower_Send == null && other.CompareTag("Tower"))
		{
			this.t = other.transform;
			T_Tower componentInParent = other.GetComponentInParent<T_Tower>();
			if (componentInParent && componentInParent.type != 5 && componentInParent.charaType != this.ownerType)
			{
				if (hitType == 0)
				{
					if (this.Tank_Send != null && UnitConst.GetInstance().soldierConst[this.Tank_Send.index].hurtRadius <= 0f)
					{
						this.isEnd = true;
					}
					if (this.Tower_Send != null && UnitConst.GetInstance().buildingConst[this.Tower_Send.index].hurtRadius <= 0f)
					{
						this.isEnd = true;
					}
				}
				if (this.isSstate && this.isSStateY == 1)
				{
					this.damage = 0.5f;
				}
				if (this.isSstate && this.isSStateY == -1)
				{
					this.damage = 0.5f;
				}
				if (this.Tank_Send.index == 3 && this.shootPIndex == 1)
				{
					this.damage = 2f;
				}
				if (this.bulletType != 5)
				{
					if (this.Tank_Send)
					{
						componentInParent.MyBuffRuntime.AddBuffIndex(0, this.Tank_Send, UnitConst.GetInstance().soldierConst[this.Tank_Send.index].BuffIdx.ToArray());
					}
					else if (this.Tower_Send)
					{
						componentInParent.MyBuffRuntime.AddBuffIndex(0, this.Tower_Send, UnitConst.GetInstance().buildingConst[this.Tower_Send.index].BuffIdx.ToArray());
					}
					componentInParent.New_HurtByBaseFightInfo(this.bulletBaseFightInfo, this.Tank_Send.towerID, this.damage);
					this.Hit(other, hitType);
				}
				else
				{
					CoroutineInstance.DoJob(this.ShootLight(componentInParent, this.targetP, this.GetShootPos(componentInParent, null), 0.5f, null));
				}
				return true;
			}
			return false;
		}
		else if (other.CompareTag("Tank"))
		{
			this.t = other.transform;
			T_TankAbstract component = other.GetComponent<T_TankAbstract>();
			if (!component || component.charaType == this.ownerType)
			{
				return false;
			}
			if (this.Tank_Send && !this.Tank_Send.IsCanShootByCharFlak(component))
			{
				return false;
			}
			if (this.Tower_Send && !this.Tower_Send.IsCanShootByCharFlak(component))
			{
				return false;
			}
			if (hitType == 0)
			{
				if (this.Tank_Send && UnitConst.GetInstance().soldierConst[this.Tank_Send.index].hurtRadius <= 0f)
				{
					this.isEnd = true;
				}
				if (this.Tower_Send && UnitConst.GetInstance().buildingConst[this.Tower_Send.index].hurtRadius <= 0f)
				{
					this.isEnd = true;
				}
			}
			if (this.bulletType != 5)
			{
				component.HurtByBaseFightInfo(this.bulletBaseFightInfo);
				if (this.Tank_Send)
				{
					component.MyBuffRuntime.AddBuffIndex(0, this.Tank_Send, UnitConst.GetInstance().soldierConst[this.Tank_Send.index].BuffIdx.ToArray());
				}
				else if (this.Tower_Send)
				{
					component.MyBuffRuntime.AddBuffIndex(0, this.Tower_Send, UnitConst.GetInstance().buildingConst[this.Tower_Send.index].BuffIdx.ToArray());
				}
				this.Hit(other, hitType);
			}
			else
			{
				CoroutineInstance.DoJob(this.ShootLight(component, this.targetP, this.GetShootPos(null, component), 0.5f, null));
			}
			return true;
		}
		else
		{
			if (!other.CompareTag("FlyTank"))
			{
				return false;
			}
			this.t = other.transform;
			T_AirShip component2 = other.GetComponent<T_AirShip>();
			if (component2 && component2.charaType != this.ownerType)
			{
				if (hitType == 0)
				{
					if (this.Tank_Send && UnitConst.GetInstance().soldierConst[this.Tank_Send.index].hurtRadius <= 0f)
					{
						this.isEnd = true;
					}
					if (this.Tower_Send && UnitConst.GetInstance().buildingConst[this.Tower_Send.index].hurtRadius <= 0f)
					{
						this.isEnd = true;
					}
				}
				component2.HurtByBaseFightInfo(this.bulletBaseFightInfo);
				if (this.Tank_Send)
				{
					component2.MyBuffRuntime.AddBuffIndex(0, this.Tank_Send, UnitConst.GetInstance().soldierConst[this.Tank_Send.index].BuffIdx.ToArray());
				}
				else if (this.Tower_Send)
				{
					component2.MyBuffRuntime.AddBuffIndex(0, this.Tower_Send, UnitConst.GetInstance().buildingConst[this.Tower_Send.index].BuffIdx.ToArray());
				}
				this.Hit(other, hitType);
				return true;
			}
			return false;
		}
	}

	public Vector3 GetShootPos(T_Tower tower, T_TankAbstract tank)
	{
		switch (this.GetEnemyPos)
		{
		case Enum_AttackPointType.head:
			if (tower)
			{
				return tower.tr.position + new Vector3(0f, UnitConst.GetInstance().buildingConst[tower.index].hight, 0f);
			}
			return tank.tr.position + new Vector3(0f, UnitConst.GetInstance().soldierConst[tank.index].hight, 0f);
		case Enum_AttackPointType.body:
			if (tower)
			{
				return tower.tr.position + new Vector3(0f, UnitConst.GetInstance().buildingConst[tower.index].hight / 2f, 0f);
			}
			return tank.tr.position + new Vector3(0f, UnitConst.GetInstance().soldierConst[tank.index].hight / 2f, 0f);
		case Enum_AttackPointType.foot:
			if (tower)
			{
				return tower.tr.position;
			}
			return tank.tr.position;
		default:
			return Vector3.zero;
		}
	}

	private void Hit(Collider targetColider, int hitType)
	{
		this.isEndOver = true;
		this.CreatEffect(this.damageEffect, this.tr.position);
		if (this.maxHurtR > 0f)
		{
			this.DOJianShe();
			if (this.ga)
			{
				MeshRenderer[] componentsInChildren = this.ga.GetComponentsInChildren<MeshRenderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = false;
				}
			}
		}
		if (hitType == 0)
		{
			if (!this.throwHit)
			{
				this.DestoryBullet();
			}
		}
		else if (hitType == 1)
		{
			if (!this.throwHit)
			{
				this.DestoryBullet();
			}
		}
		else if (hitType == 2)
		{
			this.DestoryBullet();
		}
	}

	private void CreatEffect(string resName, Vector3 pos)
	{
		if (this.damageEffect != string.Empty)
		{
			DieBall dieBall = PoolManage.Ins.CreatEffect(resName, pos, Quaternion.identity, null);
		}
	}

	private void DOJianShe()
	{
		if (!this.isFirstJianShe)
		{
			return;
		}
		this.isFirstJianShe = false;
		if (this.Tower_Send == null)
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (SenceManager.inst.towers[i] != null && SenceManager.inst.towers[i].charaType != this.ownerType && Vector3.Distance(this.tr.position, SenceManager.inst.towers[i].tr.position) - (float)UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].size * 0.5f <= this.maxHurtR)
				{
					this.Chose(SenceManager.inst.towers[i].bodyForAttack, 1);
				}
			}
		}
		if (this.ownerType == Enum_CharaType.attacker)
		{
			for (int j = 0; j < SenceManager.inst.Tanks_Defend.Count; j++)
			{
				if (SenceManager.inst.Tanks_Defend[j] != null && Vector3.Distance(this.tr.position, SenceManager.inst.Tanks_Defend[j].tr.position) - UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Defend[j].index].size * 0.5f <= this.maxHurtR)
				{
					this.Chose(SenceManager.inst.Tanks_Defend[j].bodyForAttack, 1);
				}
			}
		}
		else if (this.ownerType == Enum_CharaType.defender)
		{
			for (int k = 0; k < SenceManager.inst.Tanks_Attack.Count; k++)
			{
				if (SenceManager.inst.Tanks_Attack[k] != null && Vector3.Distance(this.tr.position, SenceManager.inst.Tanks_Attack[k].tr.position) - UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Attack[k].index].size * 0.5f <= this.maxHurtR)
				{
					this.Chose(SenceManager.inst.Tanks_Attack[k].bodyForAttack, 1);
				}
			}
		}
		this.DestoryBullet();
	}

	public void PlayShoot()
	{
		this.distanceToTarget = Vector3.Distance(base.transform.position, this.targetP);
		this.targetPos = this.targetP;
		base.StartCoroutine(this.Shoot_PaoWuXian());
	}

	private void Play_S_Shoot()
	{
		this.distanceToTarget = Vector3.Distance(base.transform.position, this.targetP);
		this.targetPos = this.targetP;
		this.bullerSpeed *= 0.6f;
		this.angelmodu = 60f;
		base.StartCoroutine(this.Shoot_SSS((base.transform.position + this.targetP) / 2f));
	}

	[DebuggerHidden]
	private IEnumerator Shoot_PaoWuXian()
	{
		T_BulletNew.<Shoot_PaoWuXian>c__Iterator25 <Shoot_PaoWuXian>c__Iterator = new T_BulletNew.<Shoot_PaoWuXian>c__Iterator25();
		<Shoot_PaoWuXian>c__Iterator.<>f__this = this;
		return <Shoot_PaoWuXian>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator Shoot_SSS(Vector3 pos1)
	{
		T_BulletNew.<Shoot_SSS>c__Iterator26 <Shoot_SSS>c__Iterator = new T_BulletNew.<Shoot_SSS>c__Iterator26();
		<Shoot_SSS>c__Iterator.pos1 = pos1;
		<Shoot_SSS>c__Iterator.<$>pos1 = pos1;
		<Shoot_SSS>c__Iterator.<>f__this = this;
		return <Shoot_SSS>c__Iterator;
	}

	private void DestoryBullet()
	{
		this.DesInPool();
	}

	public void DesInPool()
	{
		if (this.body)
		{
			this.body.DesInsInPool();
			this.body = null;
		}
		UnityEngine.Object.Destroy(this.ga);
	}

	public void SetInfo(T_Tower tower)
	{
		this.ownerType = tower.charaType;
		this.modelClore = tower.modelClore;
		this.ownerIdx = tower.index;
		this.ownerLv = tower.lv;
		this.maxRadius = tower.ShootRadius;
		if (UnitConst.GetInstance().buildingConst[tower.index].UpdateStarInfos.Count == 0)
		{
			this.maxHurtR = UnitConst.GetInstance().buildingConst[tower.index].hurtRadius / this.tr.localScale.x;
		}
		else
		{
			this.maxHurtR = UnitConst.GetInstance().GetOurPropertyByUpGradeLv(tower.index, tower.star, Specialshow.溅射) / this.tr.localScale.x;
		}
		this.bullerSpeed = (float)UnitConst.GetInstance().buildingConst[tower.index].bulletSpeed;
		this.bodyEffect = UnitConst.GetInstance().buildingConst[tower.index].BodyEffect;
		this.damageEffect = UnitConst.GetInstance().buildingConst[tower.index].DamageEffect;
		this.angelmodu = (float)UnitConst.GetInstance().buildingConst[tower.index].angle;
		this.bulletBaseFightInfo = tower.CharacterBaseFightInfo;
		this.GetEnemyPos = UnitConst.GetInstance().buildingConst[tower.index].attackPoint;
		this.Tower_Send = tower;
		if (this.target != null && this.target.roleType == Enum_RoleType.FlyGa)
		{
			this.bulletType = -1;
		}
		else
		{
			this.bulletType = UnitConst.GetInstance().buildingConst[tower.index].bulletType;
		}
		this.isFirstJianShe = true;
		this.AllHurtColliderList.Clear();
		this.sc.enabled = false;
		this.DoJob();
	}

	public void SetInfo(T_TankAbstract tank, int shootIndex = 0)
	{
		this.ownerType = tank.charaType;
		this.ownerIdx = tank.index;
		this.modelClore = tank.modelClore;
		this.ownerLv = tank.lv;
		this.maxRadius = tank.CharacterBaseFightInfo.ShootMaxRadius;
		this.maxHurtR = UnitConst.GetInstance().soldierConst[tank.index].hurtRadius / this.tr.localScale.x;
		this.bullerSpeed = (float)UnitConst.GetInstance().soldierConst[tank.index].bulletSpeed;
		this.bodyEffect = UnitConst.GetInstance().soldierConst[tank.index].BodyEffect;
		if (tank.tankType == T_TankAbstract.TankType.特种兵 && (tank as T_Commander).commanderType == CommanderType.Tanya)
		{
			this.bodyEffect = UnitConst.GetInstance().soldierConst[10].BodyEffect;
		}
		this.damageEffect = UnitConst.GetInstance().soldierConst[tank.index].DamageEffect;
		this.angelmodu = (float)UnitConst.GetInstance().soldierConst[tank.index].angle;
		this.bulletType = UnitConst.GetInstance().soldierConst[tank.index].bulletType;
		if (tank.index == 3 && shootIndex != 2)
		{
			if (this.target != null && this.target.roleType == Enum_RoleType.FlyGa)
			{
				this.bulletType = -1;
			}
			else
			{
				this.bulletType = 0;
			}
			this.bodyEffect = "A_2_guiji";
		}
		else if (tank.index == 3 && shootIndex == 2)
		{
			if (this.target != null && this.target.roleType == Enum_RoleType.FlyGa)
			{
				this.bulletType = -1;
			}
			else
			{
				this.bulletType = 1;
			}
			this.angelmodu = 60f;
			this.bodyEffect = UnitConst.GetInstance().soldierConst[tank.index].BodyEffect;
			this.damageEffect = "A_2_baozha_01";
		}
		this.isFirstJianShe = true;
		this.AllHurtColliderList.Clear();
		bool flag = shootIndex == 2;
		this.bulletBaseFightInfo = tank.CharacterBaseFightInfo;
		this.GetEnemyPos = UnitConst.GetInstance().soldierConst[tank.index].attackPoint;
		this.Tank_Send = tank;
		if (this.bulletType == 0 && !UnitConst.GetInstance().soldierConst[this.Tank_Send.index].isCanFly)
		{
			this.sc.enabled = true;
		}
		else
		{
			this.sc.enabled = false;
		}
		this.DoJob();
	}
}
