using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class T_TankFightState : StateBehaviour
{
	public enum TankFightState
	{
		Idle,
		Moving,
		Searching,
		Attacking,
		ForceMoving,
		ForceAttack,
		LandingMoving,
		LandingDowning,
		BulletIn,
		Attacked,
		Patrol
	}

	private T_TankAbstract myTank;

	private GameObject ga;

	public Stack<Character> ForcsTarget_PathFailedList = new Stack<Character>();

	private Quaternion headQuater;

	private Quaternion muzzleQuater;

	public Vector3 targetPos;

	public Character target;

	private Transform targetTran;

	public Character forcsTarget_Line;

	public Vector3 forcsMoveTargets_Line = Vector3.zero;

	private int shootNum;

	private float ShootCD;

	private float AttackCD;

	private bool IsMultipleAttack;

	private bool isEndLianji;

	private bool isShootSearchTarget;

	private bool isFollowTarPos;

	private int shootIndex;

	private float HeadRotationSpeed;

	private DieBall XuliteXiao;

	private GameObject DaoDan;

	private DieBall danke;

	public T_TankAbstract MyTank
	{
		get
		{
			if (this.myTank == null)
			{
				if (!(this.ga != null))
				{
					return null;
				}
				this.myTank = base.gameObject.GetComponentInParent<T_TankAbstract>();
			}
			return this.myTank;
		}
	}

	private void Awake()
	{
		this.ga = base.gameObject;
		base.Initialize<T_TankFightState.TankFightState>();
		this.ChangeState(T_TankFightState.TankFightState.Idle);
	}

	private void OnEnable()
	{
		this.ChangeState(T_TankFightState.TankFightState.Idle);
	}

	[DebuggerHidden]
	private IEnumerator Idle_Enter()
	{
		T_TankFightState.<Idle_Enter>c__Iterator35 <Idle_Enter>c__Iterator = new T_TankFightState.<Idle_Enter>c__Iterator35();
		<Idle_Enter>c__Iterator.<>f__this = this;
		return <Idle_Enter>c__Iterator;
	}

	private void Idle_Update()
	{
		if (UIManager.curState == SenceState.Attacking && !this.MyTank.IsDie)
		{
			if (GameSetting.autoFight && !FightPanelManager.IsRetreat)
			{
				this.MyTank.NewSearching();
			}
			if (this.MyTank.forcsTarget && this.MyTank.Targetes.Contains(this.MyTank.forcsTarget))
			{
				this.ChangeState(T_TankFightState.TankFightState.Searching);
				return;
			}
			if (this.MyTank.Target)
			{
				this.ChangeState(T_TankFightState.TankFightState.Searching);
				return;
			}
		}
	}

	private void Moving_Enter()
	{
		this.MyTank.SetAIPathSucceddCallBack();
		this.MyTank.SetMove(true);
		if (this.MyTank.tankType == T_TankAbstract.TankType.特种兵)
		{
			if (this.MyTank.index == 1)
			{
				AudioManage.inst.PlayAuido("yescommand", false);
			}
			else if (this.MyTank.index == 2)
			{
				AudioManage.inst.PlayAuido("yesw", false);
			}
		}
		this.MyTank.UpdateGraphs(false);
		if (UIManager.curState == SenceState.Attacking && this.MyTank.TankAIPath.target && this.MyTank.TankAIPath.target.Equals(this.MyTank.TargetPTransform) && this.MyTank.TargetP == Vector3.zero)
		{
			this.MyTank.TargetP = this.MyTank.tr.position;
		}
		this.MyTank.State = T_TankFightState.TankFightState.Moving;
		if (this.MyTank.ModelBody)
		{
			this.MyTank.ModelBody.DisplayTrail();
		}
		if (this.MyTank.AnimationControler && !this.MyTank.IsDie)
		{
			if (this.MyTank.AnimationControler.AnimPlay("Start"))
			{
				this.MyTank.AnimationControler.AnimPlayQuened("Run");
			}
			else
			{
				this.MyTank.AnimationControler.AnimPlay("Run");
			}
		}
		if (this.MyTank.AnimationControler != null && this.MyTank.AnimationControler.uvbody != null)
		{
			this.MyTank.AnimationControler.uvbody.scrollSpeed_Y = this.MyTank.CharacterBaseFightInfo.moveSpeed;
			this.MyTank.AnimationControler.uvbody.enabled = true;
		}
	}

	public void ClearForcsTarget_PathFailedList()
	{
		while (this.ForcsTarget_PathFailedList.Count > 0)
		{
			Character character = this.ForcsTarget_PathFailedList.Pop();
			if (character != null)
			{
				character.DieCallBack.Remove(new Action(this.Attack_ForcsTarget_PathFailed));
			}
		}
	}

	private void Attack_ForcsTarget_PathFailed()
	{
		LogManage.LogError("Do Attack_ForcsTarget_PathFailed");
		if (this.ForcsTarget_PathFailedList.Count > 0 && this.MyTank)
		{
			Character character = null;
			while (this.ForcsTarget_PathFailedList.Count > 0)
			{
				Character character2 = this.ForcsTarget_PathFailedList.Pop();
				if (character2 != null)
				{
					character = character2;
					if (this.ForcsTarget_PathFailedList.Count > 0)
					{
						character.DieCallBack.Remove(new Action(this.Attack_ForcsTarget_PathFailed));
					}
				}
			}
			if (character)
			{
				this.MyTank.ForceAttack(character.tr, character.tr.position);
			}
			else if (this.target != null)
			{
				this.MyTank.ForceAttack(this.target.tr, this.target.tr.position);
			}
			else
			{
				LogManage.LogError("Attack_ForcsTarget_PathFailed  tar is null");
			}
		}
		else if (this.MyTank)
		{
			if (this.target != null)
			{
				this.MyTank.ForceAttack(this.target.tr, this.target.tr.position);
			}
			else
			{
				LogManage.LogError("Attack_ForcsTarget_PathFailed Count is 0 or MyTank is null");
			}
		}
	}

	private void Moving_Exit()
	{
		this.MyTank.TankAIPath.endReachedDistance = 1f;
		if (this.MyTank.forcsTarget != null || this.MyTank.Targetes.Count > 0)
		{
			if (UIManager.curState != SenceState.WatchVideo)
			{
				this.MyTank.UpdateGraphs(true);
			}
			this.MyTank.SetMove(false);
		}
		this.MyTank.ClearAIPathCallBack();
		if (this.MyTank.AnimationControler && this.MyTank.AnimationControler.uvbody)
		{
			this.MyTank.AnimationControler.uvbody.enabled = false;
		}
	}

	private void Moving_Update()
	{
		if (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.WatchVideo)
		{
			if (this.MyTank.continueType == 1 && (this.MyTank.forcsTarget == null || this.MyTank.forcsTarget.IsDie))
			{
				this.ChangeState(T_TankFightState.TankFightState.Searching);
			}
			if (this.MyTank.TankAIPath.GetPath() != null)
			{
				List<Vector3> vectorPath = this.MyTank.TankAIPath.GetPath().vectorPath;
				float distance = GameTools.GetDistance(vectorPath);
				float num = Vector3.Distance(this.MyTank.tr.position, this.MyTank.targetPosToMove);
				if (distance > num * 1.5f && num > 2f)
				{
					this.AttackForTarget_Line_Path();
				}
			}
			if (this.MyTank.forcsTarget == null || this.MyTank.forcsTarget.IsDie)
			{
				this.target = this.MyTank.GetTarget();
				if (!(this.target == null) && !this.target.IsDie)
				{
					this.targetTran = this.target.tr;
				}
			}
			else
			{
				this.targetTran = this.MyTank.forcsTarget.tr;
				if (this.MyTank.Targetes.Contains(this.MyTank.forcsTarget))
				{
					this.ChangeState(T_TankFightState.TankFightState.Searching);
					return;
				}
			}
			if (this.targetTran)
			{
				if (this.MyTank.head)
				{
					this.headQuater = Quaternion.LookRotation(this.targetTran.position - this.MyTank.head.position);
					this.MyTank.head.rotation = Quaternion.Slerp(this.MyTank.head.rotation, this.headQuater, this.MyTank.CharacterBaseFightInfo.rotaSpeed * Time.deltaTime);
				}
				if (this.MyTank.muzzle)
				{
					this.muzzleQuater = Quaternion.LookRotation(this.targetTran.position - this.MyTank.muzzle.position);
					this.MyTank.muzzle.rotation = Quaternion.Slerp(this.MyTank.muzzle.rotation, this.muzzleQuater, this.MyTank.CharacterBaseFightInfo.rotaSpeed * Time.deltaTime);
				}
			}
		}
	}

	public void ReAttackForTarget()
	{
		if (this.MyTank.forcsTarget != null)
		{
			float num = (this.MyTank.forcsTarget.roleType != Enum_RoleType.tower) ? ((float)(this.MyTank.forcsTarget as T_Tank).size * 0.5f + this.MyTank.CharacterBaseFightInfo.ShootMaxRadius) : ((float)(this.MyTank.forcsTarget as T_Tower).size * 0.5f + this.MyTank.CharacterBaseFightInfo.ShootMaxRadius);
			if (Vector3.Distance(this.MyTank.tr.position, this.MyTank.forcsTarget.tr.position) > num)
			{
				if (!this.ForcsTarget_PathFailedList.Contains(this.MyTank.forcsTarget))
				{
					this.ForcsTarget_PathFailedList.Push(this.MyTank.forcsTarget);
				}
				Character nearestAndAngelsTar = this.GetNearestAndAngelsTar();
				if (nearestAndAngelsTar)
				{
					this.MyTank.ForceAttack_FindPathFailed(nearestAndAngelsTar.tr, nearestAndAngelsTar.tr.position);
					if (!nearestAndAngelsTar.DieCallBack.Contains(new Action(this.Attack_ForcsTarget_PathFailed)))
					{
						nearestAndAngelsTar.DieCallBack.Add(new Action(this.Attack_ForcsTarget_PathFailed));
					}
				}
			}
		}
	}

	public void ClearLineTemp()
	{
		this.forcsTarget_Line = null;
		this.forcsMoveTargets_Line = Vector3.zero;
	}

	public void AttackForTarget_Line_Path()
	{
		if (this.MyTank.forcsTarget != null && !this.MyTank.forcsTarget.IsDie)
		{
			if (this.forcsTarget_Line == null || this.forcsTarget_Line.IsDie)
			{
				this.forcsTarget_Line = this.MyTank.forcsTarget;
				Character nearestAndAngelsWall = this.GetNearestAndAngelsWall(this.MyTank.forcsTarget.tr.position);
				if (nearestAndAngelsWall)
				{
					LogManage.LogError("找到最近的城墙了 ： " + UnitConst.GetInstance().buildingConst[nearestAndAngelsWall.index].name);
					if (!nearestAndAngelsWall.DieCallBack.Contains(new Action(this.ReAttack_ByLine)))
					{
						nearestAndAngelsWall.DieCallBack.Add(new Action(this.ReAttack_ByLine));
					}
					LogManage.LogError("默认目标是 ： " + UnitConst.GetInstance().buildingConst[this.MyTank.forcsTarget.index].name);
					this.MyTank.ForceAttack(nearestAndAngelsWall.tr, nearestAndAngelsWall.tr.position);
				}
				else
				{
					LogManage.LogError("没有·最近的城墙！！");
				}
				return;
			}
		}
		else if (this.forcsMoveTargets_Line == Vector3.zero)
		{
			this.forcsMoveTargets_Line = this.MyTank.targetPos;
			Character nearestAndAngelsWall2 = this.GetNearestAndAngelsWall(this.forcsMoveTargets_Line);
			if (nearestAndAngelsWall2)
			{
				LogManage.LogError("找到最近的城墙了 ： " + UnitConst.GetInstance().buildingConst[nearestAndAngelsWall2.index].name);
				if (!nearestAndAngelsWall2.DieCallBack.Contains(new Action(this.ReAttack_ByLine)))
				{
					nearestAndAngelsWall2.DieCallBack.Add(new Action(this.ReAttack_ByLine));
				}
				LogManage.LogError("默认位置是 ： " + this.forcsMoveTargets_Line);
				Vector3 vector = this.forcsMoveTargets_Line;
				this.MyTank.ForceAttack(nearestAndAngelsWall2.tr, nearestAndAngelsWall2.tr.position);
				this.forcsMoveTargets_Line = vector;
			}
			else
			{
				LogManage.LogError("没有·最近的城墙！！");
			}
			return;
		}
	}

	private void ReAttack_ByLine()
	{
		LogManage.LogError("执行回调了·");
		if (this.MyTank)
		{
			if (this.forcsTarget_Line != null && !this.forcsTarget_Line.IsDie)
			{
				LogManage.LogError("执行攻击原先目标   回调了·");
				this.MyTank.ForceAttack(this.forcsTarget_Line.tr, this.forcsTarget_Line.tr.position);
			}
			else if (this.forcsMoveTargets_Line != Vector3.zero)
			{
				LogManage.LogError("执行移动原先位置   回调了·");
				this.MyTank.ForceMoving(this.forcsMoveTargets_Line, this.forcsMoveTargets_Line, 1f);
			}
		}
	}

	private Character GetNearestAndAngelsWall(Vector3 TargetPos_Line)
	{
		Character result = null;
		float num = Vector3.Distance(this.MyTank.tr.position, TargetPos_Line);
		float num2 = 3.40282347E+38f;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType == 20)
			{
				float num3 = Quaternion.Angle(Quaternion.LookRotation(SenceManager.inst.towers[i].tr.position - this.MyTank.tr.position), Quaternion.LookRotation(TargetPos_Line - this.MyTank.tr.position));
				float num4 = Vector3.Distance(SenceManager.inst.towers[i].tr.position, this.MyTank.tr.position);
				if (num4 < num && num4 > 0f && num3 < 10f)
				{
					num = num4;
					if (num3 < num2)
					{
						num2 = num3;
						result = SenceManager.inst.towers[i];
					}
					else
					{
						result = SenceManager.inst.towers[i];
					}
				}
			}
		}
		return result;
	}

	private Character GetNearestAndAngelsTar()
	{
		Character result = null;
		Character character = this.ForcsTarget_PathFailedList.Peek();
		float num = Vector3.Distance(this.MyTank.tr.position, character.tr.position);
		float num2 = 3.40282347E+38f;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			float num3 = Quaternion.Angle(Quaternion.LookRotation(this.MyTank.tr.position - character.tr.position), Quaternion.LookRotation(SenceManager.inst.towers[i].tr.position - character.tr.position));
			float num4 = Vector3.Distance(SenceManager.inst.towers[i].tr.position, character.tr.position);
			if (num3 < num2 && num4 < num && num4 > 0f)
			{
				num = num4;
				num2 = num3;
				result = SenceManager.inst.towers[i];
			}
		}
		return result;
	}

	private void Searching_Enter()
	{
		this.MyTank.State = T_TankFightState.TankFightState.Searching;
		this.AttackCD = this.MyTank.CharacterBaseFightInfo.magazineCD;
		if (this.MyTank.index == 4)
		{
			Transform tranformActiveChildByName = GameTools.GetTranformActiveChildByName(this.MyTank.ModelBody.ga, "daodan");
			if (tranformActiveChildByName)
			{
				this.DaoDan = tranformActiveChildByName.gameObject;
			}
		}
		if (this.MyTank.AnimationControler && !this.MyTank.IsDie)
		{
			if (this.MyTank.AnimationControler.AnimPlay("Start"))
			{
				this.MyTank.AnimationControler.AnimPlayQuened("Idle");
			}
			else
			{
				this.MyTank.AnimationControler.AnimPlay("Idle");
			}
		}
	}

	private void Searching_Update()
	{
		if (this.MyTank.Target != null || this.MyTank.forcsTarget != null)
		{
			if (!this.MyTank.MyBuffRuntime.IsCanBedShoot() && this.MyTank.forcsTarget == null)
			{
				return;
			}
			if (this.MyTank.forcsTarget != null)
			{
				if (!this.MyTank.Targetes.Contains(this.MyTank.forcsTarget))
				{
					this.MyTank.ForceAttack(this.MyTank.forcsTarget.tr, this.MyTank.forcsTarget.tr.position);
					return;
				}
				if (Time.time > this.MyTank.lastAttack + this.AttackCD)
				{
					this.MyTank.lastAttack = Time.time;
					this.ChangeState(T_TankFightState.TankFightState.BulletIn);
				}
			}
			else if (this.MyTank.Target != null)
			{
				if (!this.MyTank.Targetes.Contains(this.MyTank.Target))
				{
					this.MyTank.ForceAttack(this.MyTank.Target.tr, this.MyTank.Target.tr.position);
					return;
				}
				if (Time.time > this.MyTank.lastAttack + this.AttackCD)
				{
					this.MyTank.lastAttack = Time.time;
					this.ChangeState(T_TankFightState.TankFightState.BulletIn);
				}
			}
		}
		else if (GameSetting.autoFight)
		{
			this.MyTank.NewSearching();
		}
	}

	[DebuggerHidden]
	private IEnumerator BulletIn_Enter()
	{
		T_TankFightState.<BulletIn_Enter>c__Iterator36 <BulletIn_Enter>c__Iterator = new T_TankFightState.<BulletIn_Enter>c__Iterator36();
		<BulletIn_Enter>c__Iterator.<>f__this = this;
		return <BulletIn_Enter>c__Iterator;
	}

	private void BulletIn_Exit()
	{
		if (this.XuliteXiao)
		{
			UnityEngine.Object.Destroy(this.XuliteXiao.ga);
		}
	}

	[DebuggerHidden]
	private IEnumerator Attacked_Enter()
	{
		T_TankFightState.<Attacked_Enter>c__Iterator37 <Attacked_Enter>c__Iterator = new T_TankFightState.<Attacked_Enter>c__Iterator37();
		<Attacked_Enter>c__Iterator.<>f__this = this;
		return <Attacked_Enter>c__Iterator;
	}

	private void Attacking_Enter()
	{
		this.MyTank.State = T_TankFightState.TankFightState.Attacking;
		this.shootIndex = 0;
	}

	private void Attacking_Exit()
	{
		this.shootIndex = 0;
		if (this.danke)
		{
			UnityEngine.Object.Destroy(this.danke.ga);
		}
	}

	private void Attacking_Update()
	{
		if (this.IsCanShoot())
		{
			if (this.shootNum <= this.shootIndex)
			{
				this.ChangeState(T_TankFightState.TankFightState.Attacked);
				return;
			}
			if (Time.time > this.MyTank.lastShoot + this.ShootCD)
			{
				if (this.MyTank.index == 4 && this.DaoDan)
				{
					this.DaoDan.SetActive(false);
				}
				if (this.target.IsDie)
				{
					this.ChangeState(T_TankFightState.TankFightState.Attacked);
					return;
				}
				if (!this.MyTank.IsDie)
				{
					if (this.MyTank.index == 1 && this.danke == null)
					{
						this.danke = PoolManage.Ins.CreatEffect("danke", this.MyTank.shootPList[0].position, this.MyTank.shootPList[0].rotation, null);
						this.danke.LifeTime = 0.5f;
					}
					if (!this.IsMultipleAttack)
					{
						this.MyTank.ShootNew(this.shootIndex, this.target, this.targetPos);
					}
					else
					{
						for (int i = 0; i < this.MyTank.shootPList.Count; i++)
						{
							this.MyTank.ShootNew(i, this.target, this.targetPos);
						}
					}
				}
				this.MyTank.lastShoot = Time.time;
				this.shootIndex++;
			}
		}
		else if (this.target == null)
		{
			this.ChangeState(T_TankFightState.TankFightState.Attacked);
			return;
		}
	}

	private bool IsCanShoot()
	{
		if (this.MyTank == null || this.MyTank.IsDie)
		{
			this.ChangeState(T_TankFightState.TankFightState.Idle);
			return false;
		}
		if (!this.MyTank.IsCanShootByBuff())
		{
			return false;
		}
		if (this.MyTank.forcsTarget == null)
		{
			if (this.isShootSearchTarget)
			{
				this.target = this.MyTank.GetTarget();
				if (this.target != null)
				{
					this.targetPos = this.MyTank.GetShootPos(this.target);
				}
			}
			else if (!this.isEndLianji && this.target != this.MyTank.GetTarget())
			{
				this.target = null;
				return false;
			}
			if ((this.isFollowTarPos || this.shootIndex == 0) && this.target != null)
			{
				this.targetPos = this.target.tr.position;
				this.targetPos = this.MyTank.GetShootPos(this.target);
			}
		}
		else
		{
			this.target = this.MyTank.forcsTarget;
			this.targetPos = this.MyTank.GetShootPos(this.MyTank.forcsTarget);
		}
		if (this.MyTank.head && this.MyTank.CharacterBaseFightInfo.rotaSpeed > 0f)
		{
			Vector3 forward = new Vector3(this.targetPos.x, this.MyTank.head.position.y, this.targetPos.z) - this.MyTank.head.position;
			forward.y = 0f;
			this.headQuater = Quaternion.LookRotation(forward);
			this.MyTank.head.rotation = Quaternion.Slerp(this.MyTank.head.rotation, this.headQuater, this.MyTank.CharacterBaseFightInfo.rotaSpeed * Time.deltaTime);
			float num = Quaternion.Angle(this.MyTank.head.rotation, this.headQuater);
			if (num > 10f && this.MyTank.CharacterBaseFightInfo.rotaSpeed != 0f)
			{
				return false;
			}
		}
		else
		{
			Vector3 forward2 = new Vector3(this.targetPos.x, this.MyTank.tr.position.y, this.targetPos.z) - this.MyTank.tr.position;
			forward2.y = 0f;
			this.headQuater = Quaternion.LookRotation(forward2);
			this.MyTank.tr.rotation = Quaternion.Slerp(this.MyTank.tr.rotation, this.headQuater, this.MyTank.CharacterBaseFightInfo.rotaSpeed * Time.deltaTime);
			float num2 = Quaternion.Angle(this.MyTank.tr.rotation, this.headQuater);
			if (num2 > 10f && this.MyTank.CharacterBaseFightInfo.rotaSpeed != 0f)
			{
				return false;
			}
		}
		if (this.MyTank.muzzle)
		{
			Vector3 forward3 = this.targetPos - this.MyTank.muzzle.position;
			forward3.x = 0f;
			this.muzzleQuater = Quaternion.LookRotation(forward3);
			this.MyTank.muzzle.rotation = Quaternion.Slerp(this.MyTank.muzzle.rotation, this.muzzleQuater, this.MyTank.CharacterBaseFightInfo.rotaSpeed * Time.deltaTime);
			float num3 = Quaternion.Angle(this.MyTank.muzzle.rotation, this.muzzleQuater);
			if (num3 > 10f && this.MyTank.CharacterBaseFightInfo.rotaSpeed != 0f)
			{
				return false;
			}
		}
		if (FightPanelManager.IsRetreat)
		{
			this.ChangeState(T_TankFightState.TankFightState.Idle);
			return false;
		}
		if (FightHundler.FightEnd)
		{
			return false;
		}
		float num4;
		if (this.target)
		{
			num4 = GameTools.GetDistance(this.MyTank.tr.position, this.target.tr.position, GameTools.Distance_No_X_Y_Z.NoY) - (float)this.target.size * 0.5f;
		}
		else
		{
			num4 = GameTools.GetDistance(this.MyTank.tr.position, this.targetPos, GameTools.Distance_No_X_Y_Z.NoY) - (float)this.MyTank.size * 0.5f;
		}
		if (num4 <= this.MyTank.CharacterBaseFightInfo.ShootMaxRadius)
		{
			return true;
		}
		this.ChangeState(T_TankFightState.TankFightState.Moving);
		return false;
	}
}
