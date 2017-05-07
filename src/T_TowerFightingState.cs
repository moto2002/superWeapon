using FSM;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class T_TowerFightingState : StateBehaviour
{
	public enum TowerFightingStates
	{
		Idle = 1,
		Searching,
		BulletIn,
		Attacking,
		Attacked
	}

	private T_Tower myTower;

	public GameObject UpdateSp;

	public static bool isUpdate;

	private int IdleAnimationCD;

	private Character target;

	private Vector3 targetPos;

	private LineRenderer JuJiLine;

	private GameObject juLiTittle;

	private bool isFollowTarPos;

	private GameObject yujing;

	private int shootNum;

	private bool IsMultipleAttack;

	private float ShootCD;

	private float AttackCD;

	private bool isEndLianji;

	private bool isShootSearchTarget;

	private int shootIndex;

	private int HeadRotationSpeed;

	private Quaternion headQuater;

	private Quaternion muzzleQuater;

	public T_Tower MyTower
	{
		get
		{
			if (this.myTower == null)
			{
				this.myTower = base.GetComponentInParent<T_Tower>();
			}
			return this.myTower;
		}
	}

	private void Awake()
	{
		base.Initialize<T_TowerFightingState.TowerFightingStates>();
		this.ChangeState(T_TowerFightingState.TowerFightingStates.Idle);
	}

	private void OnEnable()
	{
		this.ChangeState(T_TowerFightingState.TowerFightingStates.Idle);
	}

	private void OnUpdate()
	{
	}

	private void Idle_Enter()
	{
		this.MyTower.towerFightState = T_TowerFightingState.TowerFightingStates.Idle;
		this.IdleAnimationCD = UnityEngine.Random.Range(5, 20);
	}

	private void Idle_FixedUpdate()
	{
		if (this.MyTower.Targetes.Count == 0 && Time.time % (float)this.IdleAnimationCD == 0f && this.MyTower.AnimationControler)
		{
			this.MyTower.AnimationControler.AnimPlay("Idle");
		}
	}

	private void Searching_Enter()
	{
		this.MyTower.towerFightState = T_TowerFightingState.TowerFightingStates.Searching;
		this.AttackCD = this.MyTower.CharacterBaseFightInfo.magazineCD;
	}

	private void Searching_FixedUpdate()
	{
		if (this.MyTower.GetTarget() != null && Time.time > this.MyTower.lastAttack + this.AttackCD)
		{
			this.MyTower.lastAttack = Time.time;
			this.ChangeState(T_TowerFightingState.TowerFightingStates.BulletIn);
		}
	}

	[DebuggerHidden]
	private IEnumerator BulletIn_Enter()
	{
		T_TowerFightingState.<BulletIn_Enter>c__Iterator3A <BulletIn_Enter>c__Iterator3A = new T_TowerFightingState.<BulletIn_Enter>c__Iterator3A();
		<BulletIn_Enter>c__Iterator3A.<>f__this = this;
		return <BulletIn_Enter>c__Iterator3A;
	}

	private void BulletIn_Exit()
	{
		if (this.MyTower.index == 16 || this.MyTower.index == 86)
		{
			if (this.JuJiLine)
			{
				this.JuJiLine.enabled = false;
			}
			if (this.juLiTittle)
			{
				UnityEngine.Object.Destroy(this.juLiTittle);
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator Attacked_Enter()
	{
		T_TowerFightingState.<Attacked_Enter>c__Iterator3B <Attacked_Enter>c__Iterator3B = new T_TowerFightingState.<Attacked_Enter>c__Iterator3B();
		<Attacked_Enter>c__Iterator3B.<>f__this = this;
		return <Attacked_Enter>c__Iterator3B;
	}

	private void Attacking_Enter()
	{
		this.MyTower.towerFightState = T_TowerFightingState.TowerFightingStates.Attacking;
		this.shootIndex = 0;
	}

	private void Attacking_Exit()
	{
		this.shootIndex = 0;
	}

	private void Attacking_Update()
	{
		if (this.MyTower.index == 23)
		{
			return;
		}
		if (this.IsCanShoot())
		{
			if (this.shootNum <= this.shootIndex)
			{
				this.ChangeState(T_TowerFightingState.TowerFightingStates.Attacked);
				return;
			}
			if (Time.time > this.MyTower.lastShoot + this.ShootCD)
			{
				if (this.target.IsDie)
				{
					this.ChangeState(T_TowerFightingState.TowerFightingStates.Attacked);
					return;
				}
				if (!this.MyTower.IsDie)
				{
					if (!this.IsMultipleAttack)
					{
						if (this.MyTower.index == 22)
						{
							this.MyTower.ShootNew(this.shootIndex, null, this.yujing.transform.position);
						}
						else
						{
							this.MyTower.ShootNew(this.shootIndex, this.target, this.targetPos);
						}
					}
					else
					{
						for (int i = 0; i < this.myTower.shootPList.Count; i++)
						{
							if (this.MyTower.index == 22)
							{
								this.MyTower.ShootNew(i, null, this.yujing.transform.position);
							}
							else
							{
								this.MyTower.ShootNew(i, this.target, this.targetPos);
							}
						}
					}
				}
				this.MyTower.lastShoot = Time.time;
				this.shootIndex++;
			}
		}
	}

	private bool IsCanShoot()
	{
		if (this.MyTower == null || this.MyTower.IsDie)
		{
			this.ChangeState(T_TowerFightingState.TowerFightingStates.Idle);
			return false;
		}
		if (!this.MyTower.IsCanShootByBuff())
		{
			return false;
		}
		if (this.isShootSearchTarget)
		{
			this.target = this.MyTower.GetTarget();
			if (!(this.target != null) || this.target.IsDie)
			{
				return false;
			}
			this.targetPos = this.MyTower.GetShootPos(this.target);
		}
		else if (!this.isEndLianji && this.target != null && !this.target.IsDie && this.target != this.MyTower.GetTarget())
		{
			this.target = null;
			return false;
		}
		if ((this.isFollowTarPos || this.shootIndex == 0) && this.target != null && !this.target.IsDie)
		{
			this.targetPos = this.MyTower.GetShootPos(this.target);
		}
		if (this.MyTower.head)
		{
			Vector3 forward = this.targetPos - this.MyTower.head.position;
			forward.y = 0f;
			this.headQuater = Quaternion.LookRotation(forward);
			this.MyTower.head.rotation = Quaternion.Slerp(this.MyTower.head.rotation, this.headQuater, this.MyTower.HeadRotationSpeed * Time.deltaTime);
			float num = Quaternion.Angle(this.MyTower.head.rotation, this.headQuater);
			if (num > 10f && this.MyTower.HeadRotationSpeed != 0f)
			{
				return false;
			}
		}
		if (this.MyTower.muzzle)
		{
			Vector3 forward2 = this.targetPos - this.MyTower.muzzle.position;
			forward2.x = 0f;
			this.muzzleQuater = Quaternion.LookRotation(forward2);
			this.MyTower.muzzle.rotation = Quaternion.Slerp(this.MyTower.muzzle.rotation, this.muzzleQuater, this.MyTower.HeadRotationSpeed * Time.deltaTime);
			float num2 = Quaternion.Angle(this.MyTower.muzzle.rotation, this.muzzleQuater);
			if (num2 > 10f && this.MyTower.HeadRotationSpeed != 0f)
			{
				return false;
			}
		}
		if (FightPanelManager.IsRetreat)
		{
			this.ChangeState(T_TowerFightingState.TowerFightingStates.Idle);
			return false;
		}
		return !FightHundler.FightEnd;
	}

	private void OnDisable()
	{
		if (this.juLiTittle)
		{
			UnityEngine.Object.Destroy(this.juLiTittle);
		}
		if (this.yujing)
		{
			UnityEngine.Object.Destroy(this.yujing);
		}
	}
}
