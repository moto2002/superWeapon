using DG.Tweening;
using FSM;
using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class T_TankAbstract : Character
{
	public enum TankType
	{
		坦克 = 1,
		飞机,
		特种兵
	}

	protected List<Character> Targets_InEye = new List<Character>();

	public long towerID;

	public long sceneId;

	public bool CommanderAureole_Action;

	[SerializeField]
	private T_TankFightState.TankFightState state;

	public AIPath TankAIPath;

	public CharacterController CharacterControl;

	public BuffRuntime MyBuffRuntime;

	public T_Info m_lifeInfo;

	public T_Info m_shieldInfo;

	public Character forcsTarget;

	private Vector3 targetP;

	public Transform TargetPTransform;

	protected T_TankFightState t_TankFightState;

	public float lastAttack;

	public float lastShoot;

	public int shootPIndex;

	[HideInInspector]
	public DieBall FightEffect;

	public int bulletType;

	private float lastAngle;

	private int n = 1;

	private GameObject starLevelGa;

	public int TankStar;

	public int SupplyTankStar;

	private float updateTime;

	protected bool isPause;

	public int continueType;

	public Transform continueAttackTr;

	public Vector3 continueAttackPos = Vector3.zero;

	protected float movespeed = 3f;

	protected float roatSpeed = 1f;

	protected DownToDoType downToDoType;

	public bool isDowning;

	public Vector3 targetPos;

	public Vector3 targetPosToMove;

	public GameObject line_Tank;

	protected float lineTime;

	public int ExtraArmyId;

	public int Card_No;

	public int Supply_Card_No;

	protected T_TankSelectState t_TankSelectState;

	[HideInInspector]
	public Body_Model DirtyBack;

	private Character enemy;

	public virtual T_TankAbstract.TankType tankType
	{
		get
		{
			return T_TankAbstract.TankType.坦克;
		}
	}

	public T_TankFightState.TankFightState State
	{
		get
		{
			return this.state;
		}
		set
		{
			this.state = value;
		}
	}

	public Vector3 TargetP
	{
		get
		{
			return this.targetP;
		}
		set
		{
			this.TargetPTransform.position = value;
			this.targetP = value;
		}
	}

	public T_TankFightState T_TankFightState
	{
		get
		{
			if (this.t_TankFightState == null)
			{
				if (this.tr.Find("tankFighttState"))
				{
					this.t_TankFightState = GameTools.GetCompentIfNoAddOne<T_TankFightState>(this.tr.Find("tankFighttState").gameObject);
				}
				else
				{
					GameObject gameObject = NGUITools.AddChild(this.ga);
					gameObject.name = "tankFighttState";
					this.t_TankFightState = GameTools.GetCompentIfNoAddOne<T_TankFightState>(gameObject);
				}
			}
			return this.t_TankFightState;
		}
	}

	public float Movespeed
	{
		get
		{
			return this.movespeed;
		}
		set
		{
			if (this.TankAIPath)
			{
				this.TankAIPath.speed = value;
			}
			this.movespeed = value;
		}
	}

	public float RoatSpeed
	{
		get
		{
			return this.roatSpeed;
		}
		set
		{
			if (this.TankAIPath)
			{
				this.TankAIPath.turningSpeed = value;
			}
			this.roatSpeed = value;
		}
	}

	public bool IsPause
	{
		get
		{
			return this.isPause;
		}
		set
		{
			if (this.isPause == value)
			{
				return;
			}
			if (value)
			{
				this.SetMove(false);
				this.isPause = true;
			}
			else
			{
				this.isPause = false;
				if (this.continueType == 0)
				{
					this.SetMove(true);
				}
				else if (this.continueType == 1)
				{
					if (this.continueAttackTr != null)
					{
						if (this.TankAIPath)
						{
							if (this.TankAIPath.target)
							{
								this.TankAIPath.target.position = this.tr.position;
							}
							this.SetMove(false);
						}
						this.ForceAttack(this.continueAttackTr, this.continueAttackPos);
					}
					else
					{
						this.SetMove(true);
						this.NewSearching();
					}
				}
			}
		}
	}

	public T_TankSelectState T_TankSelectState
	{
		get
		{
			if (this.t_TankSelectState == null)
			{
				if (this.tr.Find("tankSelectState"))
				{
					this.t_TankSelectState = GameTools.GetCompentIfNoAddOne<T_TankSelectState>(this.tr.Find("tankSelectState").gameObject);
				}
				else
				{
					GameObject gameObject = NGUITools.AddChild(this.ga);
					gameObject.name = "tankSelectState";
					this.t_TankSelectState = GameTools.GetCompentIfNoAddOne<T_TankSelectState>(gameObject);
				}
			}
			return this.t_TankSelectState;
		}
	}

	protected Character GetNearest_InEye()
	{
		Character result = null;
		this.tmpGetNearestChar = 3.40282347E+38f;
		for (int i = 0; i < this.Targets_InEye.Count; i++)
		{
			if (this.Targets_InEye[i] != null && this.Targets_InEye[i].IsCanBedShootByBuff() && Vector3.Distance(this.tr.position, this.Targets_InEye[i].tr.position) < this.tmpGetNearestChar)
			{
				this.tmpGetNearestChar = Vector3.Distance(this.tr.position, this.Targets_InEye[i].tr.position);
				result = this.Targets_InEye[i];
			}
		}
		return result;
	}

	public void OnDestroy()
	{
		if (this.TargetPTransform)
		{
			UnityEngine.Object.Destroy(this.TargetPTransform.gameObject);
		}
	}

	public override void Awake()
	{
		base.Awake();
		this.MyBuffRuntime = GameTools.GetCompentIfNoAddOne<BuffRuntime>(this.ga);
		this.MyBuffRuntime.myTank = this;
		this.TankAIPath = base.GetComponent<AIPath>();
		this.bodyForAttack = base.GetComponent<BoxCollider>();
		this.TargetPTransform = PoolManage.Ins.GetGameTemp().transform;
		this.CharacterControl = base.GetComponent<CharacterController>();
	}

	public void Set_NewTechnologyLV_andNewInfo(int newTechnologyLV)
	{
	}

	public virtual void InitInfo()
	{
	}

	public virtual void ShootNew(int shootIndex, Character tarTr, Vector3 tarPos)
	{
		if (this.tankType != T_TankAbstract.TankType.特种兵)
		{
			AudioManage.inst.PlayAuidoBySelf_3D(UnitConst.GetInstance().soldierConst[this.index].fightSound, this.ga, false, 0uL);
		}
		else
		{
			AudioManage.inst.PlayAuidoBySelf_3D(UnitConst.GetInstance().soldierList[this.index].fightSound, this.ga, false, 0uL);
		}
		if (this.index == 3 && this.tankType == T_TankAbstract.TankType.坦克)
		{
			if (shootIndex == 0)
			{
				if (this.AnimationControler && !this.IsDie)
				{
					this.AnimationControler.AnimPlay("Attack1");
				}
				else if (shootIndex == 1 && this.AnimationControler && !this.IsDie)
				{
					this.AnimationControler.AnimPlay("Attack2");
				}
			}
		}
		else if (this.AnimationControler && !this.IsDie)
		{
			this.AnimationControler.AnimPlay("Attack1");
		}
		float num = 6f;
		float num2 = 0f;
		if (this.shootPList.Count == 0)
		{
			return;
		}
		if (this.shootPIndex >= this.shootPList.Count)
		{
			this.shootPIndex = 0;
		}
		Transform transform = this.shootPList[this.shootPIndex];
		this.shootPIndex++;
		Quaternion rotation = transform.rotation;
		if (this.index == 6)
		{
			if (tarTr != null)
			{
				base.StartCoroutine(this.DianCiLine(transform, tarPos));
				tarTr.HurtNew(this.CharacterBaseFightInfo, this.towerID);
				tarTr.AddBuffIndex(this, UnitConst.GetInstance().soldierConst[this.index].BuffIdx.ToArray());
				Body_Model effectByName = PoolManage.Ins.GetEffectByName(UnitConst.GetInstance().soldierConst[this.index].DamageEffect, tarTr.tr);
			}
			return;
		}
		if (this.index == 1)
		{
			if (this.FightEffect == null)
			{
				this.FightEffect = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().soldierConst[this.index].fightEffect, transform.position, rotation, transform);
				if (this.FightEffect)
				{
					this.FightEffect.LifeTime = 0f;
				}
			}
			else
			{
				this.FightEffect.tr.position = transform.position;
				this.FightEffect.tr.rotation = rotation;
				this.FightEffect.Particle.Play();
			}
		}
		else if (this.tankType != T_TankAbstract.TankType.特种兵)
		{
			PoolManage.Ins.CreatEffect(UnitConst.GetInstance().soldierConst[this.index].fightEffect, transform.position, rotation, transform);
		}
		else if ((this as T_Commander).commanderType == CommanderType.Tanya)
		{
			DieBall dieBall = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().soldierConst[10].fightEffect, transform.position, rotation, transform);
			dieBall.tr.localEulerAngles = new Vector3(0f, 90f, 0f);
		}
		else
		{
			PoolManage.Ins.CreatEffect(UnitConst.GetInstance().soldierConst[this.index].fightEffect, transform.position, rotation, transform);
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
		if (this.bulletType == 0 && UnitConst.GetInstance().soldierConst[this.index].angle > 0)
		{
			float num3 = UnityEngine.Random.Range((float)(-(float)UnitConst.GetInstance().soldierConst[this.index].angle) / 2f, (float)UnitConst.GetInstance().soldierConst[this.index].angle / 2f);
			if (Mathf.Abs(num3 - this.lastAngle) < 12f)
			{
				num3 += (float)(12 * this.n);
				this.n *= -1;
			}
			float num4 = Vector3.Distance(this.tr.position, tarPos);
			float num5 = UnityEngine.Random.Range(-num / 2f, num / 2f);
			if ((double)Mathf.Abs(num5 - num2) < 0.5)
			{
				num5 += 0.5f * (float)this.n;
				this.n *= -1;
			}
			float num6 = num4 + num5;
			if (num6 > UnitConst.GetInstance().soldierConst[this.index].maxRadius)
			{
				num5 = num6 - UnitConst.GetInstance().soldierConst[this.index].maxRadius;
				num6 = UnitConst.GetInstance().soldierConst[this.index].maxRadius;
			}
			float num7 = tarPos.x - this.tr.position.x;
			float num8 = tarPos.z - this.tr.position.z;
			float num9 = tarPos.x - this.tr.position.x;
			float num10 = tarPos.z - this.tr.position.z;
			float num11 = Mathf.Atan(num8 / num7);
			float num12 = (Mathf.Sqrt(Mathf.Pow(num7, 2f) + Mathf.Pow(num8, 2f)) + num5) / Mathf.Sqrt(1f + Mathf.Pow(Mathf.Tan(num11 - 0.0174532924f * num3), 2f));
			float num13 = num12 * Mathf.Tan(num11 - 0.0174532924f * num3);
			if (num9 > 0f && num10 > 0f)
			{
				num12 *= 1f;
				num13 *= 1f;
			}
			if (num9 < 0f && num10 > 0f)
			{
				num12 *= -1f;
				num13 *= -1f;
			}
			if (num9 > 0f && num10 < 0f)
			{
				num12 *= 1f;
				num13 *= 1f;
			}
			if (num9 < 0f && num10 < 0f)
			{
				num12 *= -1f;
				num13 *= -1f;
			}
			if ((num8 > 0f && num13 < 0f) || (num8 < 0f && num13 > 0f))
			{
				num13 *= -1f;
			}
			bullet.targetP = this.tr.position + new Vector3(num12, 0f, num13);
			bullet.transform.rotation = Quaternion.Euler(57.29578f * Mathf.Atan(transform.position.y / num6), transform.eulerAngles.y + num3, transform.eulerAngles.z);
			this.lastAngle = num3;
		}
		if (this.index == 3)
		{
			bullet.isSStateY = 1;
			bullet.isSstate = true;
			bullet.SetInfo(this, shootIndex);
			if (shootIndex == 2)
			{
				bullet = PoolManage.Ins.GetBullet(this.shootPList[shootIndex + 1].position, this.shootPList[shootIndex + 1].rotation, null);
				PoolManage.Ins.CreatEffect(UnitConst.GetInstance().soldierConst[this.index].fightEffect, this.shootPList[shootIndex + 1].position, this.shootPList[shootIndex + 1].rotation, null);
				bullet.target = ((!(tarTr == null) && !tarTr.IsDie) ? tarTr : null);
				bullet.targetP = tarPos;
				bullet.isSstate = true;
				bullet.isSStateY = -1;
				bullet.SetInfo(this, shootIndex);
			}
		}
		else
		{
			bullet.SetInfo(this, 0);
		}
	}

	[DebuggerHidden]
	private IEnumerator DianCiLine(Transform ShootP, Vector3 tarPos)
	{
		T_TankAbstract.<DianCiLine>c__Iterator2F <DianCiLine>c__Iterator2F = new T_TankAbstract.<DianCiLine>c__Iterator2F();
		<DianCiLine>c__Iterator2F.tarPos = tarPos;
		<DianCiLine>c__Iterator2F.<$>tarPos = tarPos;
		<DianCiLine>c__Iterator2F.<>f__this = this;
		return <DianCiLine>c__Iterator2F;
	}

	public void SetStar(int starLevel, Transform effectParent, int supplytankstar = 0)
	{
		this.TankStar = starLevel;
		if (supplytankstar > 0)
		{
			this.SupplyTankStar = supplytankstar;
		}
		float y = 0.6f;
		switch (this.index)
		{
		case 1:
			y = 0.6f;
			break;
		case 2:
			y = 1f;
			break;
		case 3:
			y = 1.5f;
			break;
		case 4:
			y = 1.5f;
			break;
		case 5:
			y = 1.5f;
			break;
		case 6:
			y = 1.5f;
			break;
		case 7:
			y = 0.6f;
			break;
		}
		if (this.tankType == T_TankAbstract.TankType.特种兵)
		{
			y = 2f;
		}
		switch (starLevel)
		{
		case 1:
			if (supplytankstar == 0)
			{
				PoolManage.Ins.GetEffectByName("1J", this.tr).DesInsInPool(3f);
			}
			if (this.starLevelGa != null)
			{
				UnityEngine.Object.Destroy(this.starLevelGa);
			}
			this.starLevelGa = PoolManage.Ins.GetModelByResourceByName("1JModel", effectParent);
			this.starLevelGa.transform.position = this.tr.position + new Vector3(0f, y, 0f);
			this.starLevelGa.transform.rotation = Quaternion.identity;
			break;
		case 2:
			if (supplytankstar == 0)
			{
				PoolManage.Ins.GetEffectByName("2J", this.tr).DesInsInPool(3f);
			}
			if (this.starLevelGa != null)
			{
				UnityEngine.Object.Destroy(this.starLevelGa);
			}
			this.starLevelGa = PoolManage.Ins.GetModelByResourceByName("2JModel", effectParent);
			this.starLevelGa.transform.position = this.tr.position + new Vector3(0f, y, 0f);
			this.starLevelGa.transform.rotation = Quaternion.identity;
			break;
		case 3:
			if (supplytankstar == 0)
			{
				PoolManage.Ins.GetEffectByName("3J", this.tr).DesInsInPool(3f);
			}
			if (this.starLevelGa != null)
			{
				UnityEngine.Object.Destroy(this.starLevelGa);
			}
			this.starLevelGa = PoolManage.Ins.GetModelByResourceByName("3JModel", effectParent);
			this.starLevelGa.transform.position = this.tr.position + new Vector3(0f, y, 0f);
			this.starLevelGa.transform.rotation = Quaternion.identity;
			break;
		}
		this.starLevelGa.transform.localEulerAngles = new Vector3(-50f, 150f, 3f);
		Transform transform = this.starLevelGa.GetComponentInChildren<MeshRenderer>().transform;
		transform.transform.localPosition = new Vector3(0f, 1f, 0f);
		transform.transform.localEulerAngles = new Vector3(-90f, 90f, 0f);
		if (starLevel == 1)
		{
			transform.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
		}
	}

	public virtual void Update()
	{
		if (this.line_Tank != null && this.line_Tank.activeInHierarchy)
		{
			this.lineTime += Time.deltaTime;
			if (this.lineTime >= 2f)
			{
				this.line_Tank.SetActive(false);
				this.lineTime = 0f;
			}
			this.line_Tank.GetComponent<LineRenderer>().SetPosition(0, new Vector3(base.transform.position.x, 0.5f, base.transform.position.z));
		}
		if (this.updateTime < Time.time)
		{
			this.UpdateBySecond();
			this.updateTime = Time.time + 0.2f;
		}
	}

	public virtual void UpdateBySecond()
	{
	}

	public abstract void SetMove(bool isMove);

	public virtual void ForceAttack(Transform tar, Vector3 _pos)
	{
		this.T_TankFightState.forcsMoveTargets_Line = Vector3.zero;
		if (this.ForAttack(tar, _pos))
		{
			this.T_TankFightState.ClearForcsTarget_PathFailedList();
		}
	}

	public void ForceAttack_FindPathFailed(Transform tar, Vector3 _pos)
	{
		this.ForAttack(tar, _pos);
	}

	protected bool ForAttack(Transform tar, Vector3 _pos)
	{
		if (this.forcsTarget != null && this.Targetes.Contains(this.forcsTarget) && tar.GetComponent<Character>().Equals(this.forcsTarget))
		{
			return false;
		}
		if (!base.IsCanShootByCharType(tar.GetComponent<Character>()))
		{
			return false;
		}
		if (this.charaType == Enum_CharaType.defender)
		{
			this.TankAIPath.target = tar;
		}
		this.continueAttackPos = _pos;
		this.continueAttackTr = tar;
		this.forcsTarget = tar.GetComponent<Character>();
		this.continueType = 1;
		if (this.IsPause)
		{
			return false;
		}
		if (this.Targetes.Contains(this.forcsTarget))
		{
			this.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
			return true;
		}
		Vector3 a = new Vector3(tar.position.x, this.tr.position.y, tar.position.z);
		this.TargetP = a - (a - this.tr.position).normalized * (this.CharacterBaseFightInfo.ShootMaxRadius * 0.5f);
		this.targetPosToMove = this.TargetP;
		this.targetPos = this.TargetP;
		this.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Moving);
		return true;
	}

	public virtual void ForceMoving(Vector3 _pos, Vector3 tarPos, float speedTimes = 1f)
	{
		this.TargetP = new Vector3(_pos.x, this.tr.position.y, _pos.z);
		this.targetPos = tarPos;
		this.targetPosToMove = _pos;
		this.continueType = 0;
		if (this.IsPause)
		{
			LogManage.LogError("Pause CanNot Move");
			return;
		}
		if (this.isDowning)
		{
			LogManage.LogError("isDowning CanNot Move");
			this.downToDoType = DownToDoType.forceMoving;
			return;
		}
		this.forcsTarget = null;
		this.T_TankFightState.forcsTarget_Line = null;
		this.T_TankFightState.forcsMoveTargets_Line = Vector3.zero;
		if (this.State != T_TankFightState.TankFightState.Moving)
		{
			this.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Moving, StateTransition.Overwrite);
		}
		this.SetMove(true);
	}

	public void SetAIPathSucceddCallBack()
	{
		if (this.charaType == Enum_CharaType.attacker)
		{
			this.TankAIPath.target = this.TargetPTransform;
			this.TankAIPath.OnTargetReachedCallBack = new Action(this.TankAStarPathSucceedCallBack);
		}
		else if (this.charaType == Enum_CharaType.defender)
		{
			this.TankAIPath.OnTargetReachedCallBack = new Action(this.TankAStarPathSucceedCallBack);
		}
	}

	private void TankAStarPathSucceedCallBack()
	{
		if (this.forcsTarget && !this.Targetes.Contains(this.forcsTarget))
		{
			this.T_TankFightState.ReAttackForTarget();
		}
		else
		{
			if (this.targetPos != this.targetPosToMove && this.State == T_TankFightState.TankFightState.Moving)
			{
				this.ForceMoving(this.targetPosToMove, this.targetPosToMove, 1f);
				return;
			}
			LogManage.LogError("TankAStarPathSucceedCallBack~~~~~~~~");
			if (this.Targetes.Count == 0)
			{
				if (this.State != T_TankFightState.TankFightState.Idle)
				{
					this.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
				}
			}
			else if (this.State != T_TankFightState.TankFightState.Searching)
			{
				this.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
			}
			if (this.index != 7 && UIManager.curState == SenceState.Home)
			{
				this.tr.DORotate(Vector3.zero, 0.2f, RotateMode.Fast);
				this.head.DORotate(Vector3.zero, 0.3f, RotateMode.Fast);
			}
			if (this.line_Tank != null)
			{
				this.line_Tank.SetActive(false);
			}
		}
	}

	public void ClearAIPathCallBack()
	{
		this.TankAIPath.OnTargetReachedCallBack = null;
	}

	public GameObject GetLineTank()
	{
		if (this.line_Tank == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(SenceManager.inst.lineObj, this.tr.position, Quaternion.identity) as GameObject;
			gameObject.transform.parent = this.tr;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			this.line_Tank = gameObject;
		}
		this.line_Tank.SetActive(true);
		this.lineTime = 0f;
		return this.line_Tank;
	}

	public virtual void HurtByBaseFightInfo(BaseFightInfo attackFightInfo)
	{
		bool towerToTank = true;
		bool flag = false;
		int num = NumericalMgr.S_AttackDamage(attackFightInfo, this.CharacterBaseFightInfo, towerToTank, out flag);
		if (flag)
		{
			HUDTextTool.inst.SetBaoJiText("暴击" + num, this.tr);
		}
		this.DoHurt(num);
	}

	public virtual void SuSkillHurt(int idx)
	{
		int basePower = UnitConst.GetInstance().skillList[idx].basePower;
		FightPanelManager.inst.CreatFightMessage("-" + basePower, Color.red, this.tr);
		this.DoHurt(basePower);
	}

	public virtual void DoHurt(int damage)
	{
		if (FightHundler.FightEnd)
		{
			return;
		}
		if (this.CharacterBaseFightInfo.Shield > 0 && damage > 0)
		{
			this.CharacterBaseFightInfo.Shield = this.CharacterBaseFightInfo.Shield - damage;
			AudioManage.inst.PlayAuido("ironshoot", false);
			if (this.CharacterBaseFightInfo.Shield > 0)
			{
				if (this.m_shieldInfo == null)
				{
					this.m_shieldInfo = InfoPanel.inst.CreatShieldInfo();
					InfoPanel.inst.lifeInfos.Add(this.m_shieldInfo);
				}
				this.m_shieldInfo.ShowShieldLife(this, null);
				UnityEngine.Debug.Log("CharacterBaseFightInfo.Shield:" + this.CharacterBaseFightInfo.Shield);
			}
			if (this.CharacterBaseFightInfo.Shield > 0 || !(this.MyBuffRuntime != null))
			{
				return;
			}
			this.MyBuffRuntime.RemoveBuff(Buff.BuffType.Shield);
		}
		base.CurLife -= (float)damage;
		if (base.CurLife >= (float)base.MaxLife)
		{
			base.CurLife = (float)base.MaxLife;
			if (this.m_lifeInfo != null)
			{
				this.m_lifeInfo.ShowTankLife(this, null);
				if (this.m_lifeInfo != null)
				{
					base.StartCoroutine(this.m_lifeInfo.AreadyMaxLife());
				}
			}
		}
		if (base.CurLife < (float)base.MaxLife && base.CurLife > 0f)
		{
			if (this.m_lifeInfo == null)
			{
				this.m_lifeInfo = InfoPanel.inst.CreatInfo();
				InfoPanel.inst.lifeInfos.Add(this.m_lifeInfo);
			}
			this.m_lifeInfo.ShowTankLife(this, null);
		}
		if (base.CurLife < 1f && !this.IsDie)
		{
			if (UIManager.curState == SenceState.WatchVideo)
			{
				return;
			}
			this.Die();
		}
	}

	protected void SynchronousDead()
	{
		SettleUniteData settleUniteData = new SettleUniteData();
		settleUniteData.deadType = 1;
		settleUniteData.deadSenceId = this.sceneId;
		settleUniteData.deadIdx = this.index;
		if (this.towerID > 0L)
		{
			settleUniteData.deadBuildingID = this.towerID;
			if (this.towerID == HeroInfo.GetInstance().PlayerCommandoBuildingID)
			{
				settleUniteData.deadType = 7;
				settleUniteData.deadSenceId = HeroInfo.GetInstance().Commando_Fight.id;
				settleUniteData.deadBuildingID = HeroInfo.GetInstance().PlayerCommandoBuildingID;
				settleUniteData.deadIdx = this.index;
			}
		}
		else
		{
			if (this.ExtraArmyId == 0)
			{
				return;
			}
			settleUniteData.deadType = 9;
			settleUniteData.deadSenceId = (long)SenceManager.inst.ExtraArmyDataList[this.ExtraArmyId].ID;
			settleUniteData.num = 1;
			bool flag = false;
			foreach (KeyValuePair<int, SCExtraArmy> current in SenceManager.inst.ExtraArmyList)
			{
				for (int i = 0; i < current.Value.itemId2Level.Count; i++)
				{
					if (flag)
					{
						break;
					}
					int num = (int)current.Value.itemId2Level[i].key;
					if (num == this.index && current.Value.itemId2Num[i].value > 0L)
					{
						current.Value.itemId2Num[i].value -= 1L;
						flag = true;
						break;
					}
				}
			}
		}
		LogManage.Log("data.deadId------------" + settleUniteData.deadSenceId);
		settleUniteData.posX = this.tr.position.x;
		settleUniteData.posZ = this.tr.position.z;
		if (!ActivityManager.GetIns().IsFromAct || (ActivityManager.GetIns().IsFromAct && ActivityManager.GetIns().curActItem.soliders.Count <= 0))
		{
			FightHundler.AddDeadSettleUnites(settleUniteData);
		}
		if (this.towerID > 0L)
		{
			if (this.tankType == T_TankAbstract.TankType.特种兵)
			{
				int index = this.index;
				if (index != 1)
				{
					if (index == 2)
					{
						SettlementManager.AddNewDead(1002);
					}
				}
				else
				{
					SettlementManager.AddNewDead(1001);
				}
			}
			else
			{
				SettlementManager.AddNewDead(this.index);
			}
		}
	}

	public virtual void Die()
	{
	}

	public void SelfDestruct()
	{
		if (this.tr == null)
		{
			return;
		}
		this.CreatDeathEffect();
		if (this.m_lifeInfo != null)
		{
			InfoPanel.inst.RemoveInfo(this.m_lifeInfo);
		}
		if (this.charaType == Enum_CharaType.attacker)
		{
			SenceManager.inst.RemoveAttackTank(this);
		}
		else if (this.charaType == Enum_CharaType.defender)
		{
			SenceManager.inst.RemoveDefendTank(this);
		}
		base.Destory();
	}

	protected void CreatDeathEffect()
	{
		PoolManage.Ins.CreatEffect("A_4_baozha", this.tr.position, this.tr.rotation, SenceManager.inst.bulletPool);
		Body_Model deadTower = PoolManage.Ins.GetModelByBundleByName(UnitConst.GetInstance().soldierConst[this.index].bodyId + "_die", SenceManager.inst.towerPool);
		if (deadTower)
		{
			deadTower.tr.position = new Vector3(this.tr.position.x, 0.01f, this.tr.position.z);
			deadTower.tr.rotation = this.tr.rotation;
			Animation componentInChildren = deadTower.GetComponentInChildren<Animation>();
			componentInChildren.CrossFade("Die");
			componentInChildren.CrossFadeQueued("Diestop");
			PoolManage.Ins.CreatEffect("ranshao_bingzhong_half", deadTower.tr.position, Quaternion.identity, deadTower.transform);
			deadTower.tr.DOMoveY(-6f, 3f, false).SetDelay(3f).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(deadTower.ga);
			});
		}
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

	protected void CreatBody()
	{
		if (this.ModelBody && this.ModelBody.tr.FindChild("dirtyback"))
		{
			Body_Model effectByName = PoolManage.Ins.GetEffectByName(UnitConst.GetInstance().soldierConst[this.index].dirtyBack, this.ModelBody.tr.FindChild("dirtyback"));
			if (!(effectByName == null))
			{
				this.DirtyBack = effectByName;
				effectByName.SetActive(false);
			}
		}
	}

	public Transform GetWorldNearestTower()
	{
		Character character = null;
		this.tmpGetNearestChar = 0f;
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i] != null && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].secType != 9 && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].resType != 5 && (this.tmpGetNearestChar == 0f || Vector3.Distance(this.tr.position, SenceManager.inst.towers[i].tr.position) < this.tmpGetNearestChar))
			{
				this.tmpGetNearestChar = Vector3.Distance(this.tr.position, SenceManager.inst.towers[i].tr.position);
				character = SenceManager.inst.towers[i];
			}
		}
		if (character)
		{
			return character.tr;
		}
		return null;
	}

	public void NewSearching()
	{
		if ((FightPanelManager.IsRetreat || FightHundler.FightEnd) && UIManager.curState != SenceState.WatchVideo)
		{
			return;
		}
		if (!GameSetting.autoFight)
		{
			if (this.Targetes.Count == 0)
			{
				this.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
				return;
			}
			this.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
		}
		else
		{
			this.FindEnemies();
		}
	}

	public void FindEnemies()
	{
		float num = 3.40282347E+38f;
		this.enemy = null;
		if (this.charaType == Enum_CharaType.attacker)
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (SenceManager.inst.towers[i] && UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[i].index].resType < 3)
				{
					float num2 = Vector3.Distance(this.tr.position, SenceManager.inst.towers[i].tr.position);
					if (num2 < num)
					{
						num = num2;
						this.enemy = SenceManager.inst.towers[i];
					}
				}
			}
			for (int j = 0; j < SenceManager.inst.Tanks_Defend.Count; j++)
			{
				if (SenceManager.inst.Tanks_Defend[j])
				{
					float num2 = Vector3.Distance(this.tr.position, SenceManager.inst.Tanks_Defend[j].tr.position);
					if (num2 < num)
					{
						num = num2;
						this.enemy = SenceManager.inst.Tanks_Defend[j];
					}
				}
			}
			if (this.enemy && base.Tank)
			{
				this.ForceAttack(this.enemy.tr, this.enemy.tr.position + this.enemy.GetVPos(base.Tank));
			}
		}
		else if (this.charaType == Enum_CharaType.defender)
		{
			for (int k = 0; k < SenceManager.inst.Tanks_Attack.Count; k++)
			{
				if (SenceManager.inst.Tanks_Attack[k])
				{
					float num2 = Vector3.Distance(this.tr.position, SenceManager.inst.Tanks_Attack[k].tr.position);
					if (num2 < num)
					{
						num = num2;
						this.enemy = SenceManager.inst.Tanks_Attack[k];
					}
				}
			}
			if (this.enemy)
			{
				this.ForceAttack(this.enemy.tr, this.enemy.tr.position + this.enemy.GetVPos(base.Tank));
			}
		}
	}
}
