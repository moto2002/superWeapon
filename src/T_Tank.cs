using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class T_Tank : T_TankAbstract
{
	public Vector3 landingPos;

	public int parkingId;

	public Transform lookTransform;

	public bool resetLookTransform;

	public bool attack;

	public float HeadRotationSpeed;

	public float accelerationSpeed = 25f;

	private float lastShootForIndex3;

	private float shootCDForIndex3;

	public float shootCD = 1f;

	public bool IsAuto;

	public bool isPatrol;

	public List<Transform> PatrolTranses = new List<Transform>();

	public bool isFollow;

	protected Vector3 headStartPos;

	protected float a = 20f;

	protected float downSpeed;

	protected Vector3 vt;

	protected bool isHeadRotainon;

	public T_Commander this_commander;

	public T_TankAI this_TankAI;

	public Dictionary<Vector3, T_TankAbstract> posUsedList = new Dictionary<Vector3, T_TankAbstract>();

	protected bool downToDo;

	protected Vector3 downPos;

	protected float offset = 0.5f;

	protected int curDir = 1;

	protected float curOffset;

	public bool canFire;

	private int moveID;

	public override T_TankAbstract.TankType tankType
	{
		get
		{
			return T_TankAbstract.TankType.坦克;
		}
	}

	public int MoveID
	{
		get
		{
			return this.moveID;
		}
		set
		{
			this.moveID = value;
		}
	}

	public override void UpdateBySecond()
	{
		base.UpdateBySecond();
		if (UIManager.curState == SenceState.Attacking || UIManager.curState == SenceState.WatchVideo)
		{
			if (this.charaType == Enum_CharaType.defender)
			{
				for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
				{
					if (SenceManager.inst.Tanks_Attack[i] != null)
					{
						if (base.IsCanShootByCharFlak(SenceManager.inst.Tanks_Attack[i]))
						{
							float num = GameTools.GetDistance(this.tr.position, SenceManager.inst.Tanks_Attack[i].tr.position, GameTools.Distance_No_X_Y_Z.NoY) - UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Attack[i].index].size * 0.5f;
							if (num < 0f)
							{
								num = 0f;
							}
							if (num <= UnitConst.GetInstance().soldierConst[this.index].maxRadius && num >= UnitConst.GetInstance().soldierConst[this.index].minRadius)
							{
								if (!this.Targetes.Contains(SenceManager.inst.Tanks_Attack[i]))
								{
									this.Targetes.Add(SenceManager.inst.Tanks_Attack[i]);
								}
								if (base.State == T_TankFightState.TankFightState.Idle && this.Targetes.Count > 0)
								{
									base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
								}
							}
							else if (this.Targetes.Contains(SenceManager.inst.Tanks_Attack[i]))
							{
								this.Targetes.Remove(SenceManager.inst.Tanks_Attack[i]);
							}
							if (num <= UnitConst.GetInstance().soldierConst[this.index].eyeRadius && num >= UnitConst.GetInstance().soldierConst[this.index].minRadius)
							{
								if (!this.Targets_InEye.Contains(SenceManager.inst.Tanks_Attack[i]))
								{
									this.Targets_InEye.Add(SenceManager.inst.Tanks_Attack[i]);
								}
							}
							else if (this.Targets_InEye.Contains(SenceManager.inst.Tanks_Attack[i]))
							{
								this.Targets_InEye.Remove(SenceManager.inst.Tanks_Attack[i]);
							}
						}
					}
				}
				if (this.forcsTarget == null)
				{
					if (base.Target == null)
					{
						Character nearest_InEye = base.GetNearest_InEye();
						if (nearest_InEye != null)
						{
							this.ForceAttack(nearest_InEye.tr, nearest_InEye.tr.position);
						}
						else
						{
							this.TankAIPath.enabled = false;
						}
					}
					else
					{
						this.ForceAttack(base.Target.tr, base.Target.tr.position);
					}
				}
				else if (this.Targets_InEye.Contains(this.forcsTarget))
				{
					if (!this.TankAIPath.enabled)
					{
						this.TankAIPath.enabled = true;
						this.ForceAttack(this.forcsTarget.tr, this.forcsTarget.tr.position);
					}
				}
				else if (base.Target == null)
				{
					Character nearest_InEye2 = base.GetNearest_InEye();
					if (nearest_InEye2 != null)
					{
						this.ForceAttack(nearest_InEye2.tr, nearest_InEye2.tr.position);
					}
					else
					{
						this.TankAIPath.enabled = false;
					}
				}
				else
				{
					this.ForceAttack(base.Target.tr, base.Target.tr.position);
				}
				if (!this.TankAIPath.enabled && this.Targetes.Count == 0 && this.forcsTarget != null)
				{
					this.ForceAttack(this.forcsTarget.tr, this.forcsTarget.tr.position);
				}
			}
			else if (this.charaType == Enum_CharaType.attacker)
			{
				for (int j = 0; j < SenceManager.inst.Tanks_Defend.Count; j++)
				{
					if (SenceManager.inst.Tanks_Defend[j] != null)
					{
						if (base.IsCanShootByCharFlak(SenceManager.inst.Tanks_Defend[j]))
						{
							float num2 = Vector3.Distance(this.tr.position, SenceManager.inst.Tanks_Defend[j].tr.position) - UnitConst.GetInstance().soldierConst[SenceManager.inst.Tanks_Defend[j].index].size * 0.5f;
							if (num2 < 0f)
							{
								num2 = 0f;
							}
							if (num2 <= UnitConst.GetInstance().soldierConst[this.index].maxRadius && num2 >= UnitConst.GetInstance().soldierConst[this.index].minRadius)
							{
								if (!this.Targetes.Contains(SenceManager.inst.Tanks_Defend[j]))
								{
									this.Targetes.Add(SenceManager.inst.Tanks_Defend[j]);
								}
								if (base.State == T_TankFightState.TankFightState.Idle && this.Targetes.Count > 0)
								{
									base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
								}
							}
							else if (this.Targetes.Contains(SenceManager.inst.Tanks_Defend[j]))
							{
								this.Targetes.Remove(SenceManager.inst.Tanks_Defend[j]);
							}
						}
					}
				}
				for (int k = 0; k < SenceManager.inst.towers.Count; k++)
				{
					if (SenceManager.inst.towers[k] != null)
					{
						if (UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[k].index].resType < 3 || UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[k].index].secType == 20)
						{
							float num3 = GameTools.GetDistance(this.tr.position, SenceManager.inst.towers[k].tr.position, GameTools.Distance_No_X_Y_Z.NoY) - (float)UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[k].index].size * 0.5f;
							float num4 = UnitConst.GetInstance().soldierConst[this.index].maxRadius;
							if (this.this_commander && this.forcsTarget != null && SenceManager.inst.towers[k] == this.forcsTarget)
							{
								if (this.forcsTarget is T_Tower)
								{
									num3 = Vector3.Distance(this.tr.position, this.forcsTarget.tr.position) - (float)UnitConst.GetInstance().buildingConst[(this.forcsTarget as T_Tower).index].size * 0.5f;
								}
								else if (this.forcsTarget is T_Tank)
								{
									num3 = Vector3.Distance(this.tr.position, this.forcsTarget.tr.position) - UnitConst.GetInstance().soldierConst[(this.forcsTarget as T_Tank).index].size * 0.5f;
								}
								if (this.this_commander.tanya_skillType != TanyaSkillType.None)
								{
									num4 = 0f;
									if (num3 > 5f && this.this_commander.tanya_skillType == TanyaSkillType.FindTowerSuc)
									{
										this.this_commander.tanya_Return_Pos = this.tr.position;
									}
									if (num3 < 1.5f && this.this_commander.tanya_skillType == TanyaSkillType.FindTowerSuc)
									{
										if (this.this_commander.tanya_Return_Pos == Vector3.zero)
										{
											this.this_commander.tanya_Return_Pos = this.tr.position;
										}
										this.this_commander.tanya_skillType = TanyaSkillType.SetBomb;
										if (this.AnimationControler && !this.IsDie)
										{
											this.AnimationControler.AnimPlay("Attack2");
										}
										if (this.forcsTarget != null)
										{
										}
										this.this_commander.tanya_SetBomb_Pos = this.tr.position;
									}
								}
							}
							if (num3 <= num4 && num3 >= UnitConst.GetInstance().soldierConst[this.index].minRadius)
							{
								if (this.this_commander && this.this_commander.tanya_skillType != TanyaSkillType.None)
								{
									this.Targetes.Clear();
								}
								else if (!this.Targetes.Contains(SenceManager.inst.towers[k]))
								{
									this.Targetes.Add(SenceManager.inst.towers[k]);
								}
								if (base.State == T_TankFightState.TankFightState.Idle && this.Targetes.Count > 0)
								{
									base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
								}
							}
							else if (this.Targetes.Contains(SenceManager.inst.towers[k]))
							{
								this.Targetes.Remove(SenceManager.inst.towers[k]);
							}
						}
						else if (this.Targetes.Contains(SenceManager.inst.towers[k]))
						{
							this.Targetes.Remove(SenceManager.inst.towers[k]);
						}
					}
				}
			}
		}
		for (int l = this.Targetes.Count - 1; l >= 0; l--)
		{
			if (this.Targetes[l] == null || this.Targetes[l].IsDie)
			{
				this.Targetes.Remove(this.Targetes[l]);
			}
		}
	}

	public override void SetMove(bool isCanMove)
	{
		if (this.TankAIPath)
		{
			this.TankAIPath.enabled = isCanMove;
			this.TankAIPath.canMove = isCanMove;
		}
	}

	[DebuggerHidden]
	private IEnumerator JLS()
	{
		T_Tank.<JLS>c__Iterator33 <JLS>c__Iterator = new T_Tank.<JLS>c__Iterator33();
		<JLS>c__Iterator.<>f__this = this;
		return <JLS>c__Iterator;
	}

	public override void Update()
	{
		base.Update();
		if (base.State == T_TankFightState.TankFightState.Attacking && this.index == 3 && base.IsCanShootByBuff() && Time.time > this.lastShootForIndex3 + this.shootCDForIndex3)
		{
			this.TianQiShoot(2, base.T_TankFightState.target, base.T_TankFightState.targetPos);
			this.lastShootForIndex3 = Time.time;
		}
		if (FightPanelManager.IsRetreat)
		{
			return;
		}
		if (this.isDowning)
		{
			this.downSpeed += this.a * Time.deltaTime;
			this.tr.position += Vector3.down * this.downSpeed * Time.deltaTime;
			if (this.tr.position.y <= 0.28f)
			{
				if (!SenceManager.inst.Tanks_Attack.Contains(this))
				{
					SenceManager.inst.Tanks_Attack.Add(this);
				}
				this.tr.position = new Vector3(this.tr.position.x, 0.28f, this.tr.position.z);
				this.isDowning = false;
				if (GameSetting.autoFight && !FightPanelManager.IsRetreat)
				{
					base.NewSearching();
				}
				else
				{
					this.t_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
					if (base.TargetP != Vector3.zero)
					{
						this.ForceMoving(base.TargetP, base.TargetP, 1f);
					}
				}
			}
		}
	}

	protected void OnDisable()
	{
		if (this.t_TankSelectState)
		{
			UnityEngine.Object.Destroy(this.t_TankSelectState.gameObject);
		}
		if (this.t_TankFightState)
		{
			UnityEngine.Object.Destroy(this.t_TankFightState.gameObject);
		}
		if (this.TankAIPath)
		{
			this.TankAIPath.target = null;
			this.SetMove(false);
		}
	}

	public override void InitInfo()
	{
		GameObject gameObject = this.tr.Find("JLS").gameObject;
		gameObject.SetActive(false);
		this.IsDie = false;
		this.size = (int)UnitConst.GetInstance().soldierConst[this.index].size;
		this.height = UnitConst.GetInstance().soldierConst[this.index].hight;
		this.bodyForAttack.size = new Vector3(UnitConst.GetInstance().soldierConst[this.index].size * 0.25f, this.height, UnitConst.GetInstance().soldierConst[this.index].size * 0.25f);
		this.bodyForAttack.center = new Vector3(0f, this.height * 0.5f, 0f);
		this.CharacterControl.radius = 0.5f;
		this.CharacterControl.height = this.height;
		this.CharacterControl.center = new Vector3(0f, ((this.CharacterControl.radius <= this.CharacterControl.height) ? this.CharacterControl.height : this.CharacterControl.radius) * 0.5f, 0f);
		base.Movespeed = UnitConst.GetInstance().soldierConst[this.index].speed;
		base.RoatSpeed = UnitConst.GetInstance().soldierConst[this.index].roatSpeed;
		this.HeadRotationSpeed = UnitConst.GetInstance().soldierConst[this.index].headSpeed;
		this.accelerationSpeed = UnitConst.GetInstance().soldierConst[this.index].accelerationSpeed;
		this.shootCDForIndex3 = UnitConst.GetInstance().soldierConst[this.index].frequency1;
		this.bulletType = UnitConst.GetInstance().soldierConst[this.index].bulletType;
		if (this.this_commander != null)
		{
			this.index = 8;
			this.lv = 1;
		}
		else if (UnitConst.GetInstance().soldierConst[this.index].isCanFly)
		{
			this.tr.position += new Vector3(0f, 5f, 0f);
			this.CharacterControl.enabled = false;
			this.TankAIPath.enabled = false;
		}
		if (UIManager.curState != SenceState.WatchVideo)
		{
			if (this.charaType == Enum_CharaType.attacker)
			{
				this.CharacterBaseFightInfo = InfoMgr.GetTankFightData(this.index, this.lv, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
			}
			else if (this.charaType == Enum_CharaType.defender)
			{
				if (!SenceInfo.curMap.IsMyMap)
				{
					this.CharacterBaseFightInfo = InfoMgr.GetTankFightData(this.index, this.lv, SenceInfo.SpyPlayerInfo.vip, SenceInfo.curMap.EnemyTech);
				}
				else
				{
					this.CharacterBaseFightInfo = InfoMgr.GetTankFightData(this.index, this.lv, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
				}
			}
		}
		base.MaxLife = this.CharacterBaseFightInfo.life;
		base.CurLife = (float)base.MaxLife;
		string bodyId = UnitConst.GetInstance().soldierConst[this.index].bodyId;
		if (this.this_commander != null)
		{
			if (this.this_commander.EnemyCommanderIndex == 0)
			{
				bodyId = UnitConst.GetInstance().soldierList[HeroInfo.GetInstance().Commando_Fight.index].bodyId;
			}
			else
			{
				bodyId = UnitConst.GetInstance().soldierList[this.this_commander.EnemyCommanderIndex].bodyId;
			}
		}
		base.CreateBody(bodyId);
		if (SenceInfo.curMap.IsMyMap)
		{
			if (this.charaType == Enum_CharaType.defender)
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
			else if (this.charaType == Enum_CharaType.attacker)
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
		}
		else if (this.charaType == Enum_CharaType.defender)
		{
			if (this.ModelBody && this.ModelBody.BlueModel)
			{
				this.ModelBody.BlueModel.gameObject.SetActive(false);
			}
			if (this.ModelBody && this.ModelBody.RedModel)
			{
				this.ModelBody.RedModel.gameObject.SetActive(true);
				this.modelClore = Enum_ModelColor.Red;
			}
		}
		else if (this.charaType == Enum_CharaType.attacker)
		{
			if (this.ModelBody && this.ModelBody.BlueModel)
			{
				this.ModelBody.BlueModel.gameObject.SetActive(true);
				this.modelClore = Enum_ModelColor.Blue;
			}
			if (this.ModelBody && this.ModelBody.RedModel)
			{
				this.ModelBody.RedModel.gameObject.SetActive(false);
			}
		}
		ShootP[] componentsInChildren = this.AnimationControler.GetComponentsInChildren<ShootP>();
		this.shootPList.Clear();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.activeInHierarchy)
			{
				this.shootPList.Add(componentsInChildren[i].transform);
			}
		}
		base.GetHeadOrMuzzle();
		base.CreatBody();
		if (UnitConst.GetInstance().soldierConst[this.index].headSpeed > 0f && this.head != null)
		{
			this.headStartPos = this.head.position;
			this.AnimationControler.AllAnimation.AddRange(this.head.GetComponentsInChildren<Animation>());
		}
		if (this.head)
		{
			this.head.rotation = Quaternion.identity;
		}
		if (this.muzzle)
		{
			this.muzzle.rotation = Quaternion.identity;
		}
		this.Targetes.Clear();
		if (!base.T_TankFightState)
		{
		}
		if (!base.T_TankSelectState)
		{
		}
		if (this.ModelBody)
		{
			Transform[] componentsInChildren2 = this.ModelBody.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].gameObject.layer = 0;
			}
		}
		this.SetMove(false);
		base.SetAIPathSucceddCallBack();
		if (this.ExtraArmyId != 0)
		{
			GameObject gameObject2 = FuncUIManager.inst.ResourcePanelManage.AddExtraArmyDes(this.tr, string.Format("特殊兵种\nID:{0}", this.ExtraArmyId));
			if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
			{
				SenceManager.inst.ExtraAymyTankList.Add(this.ExtraArmyId, base.GetComponent<T_Tank>());
			}
		}
	}

	public void SetInfo_Commander()
	{
		base.MaxLife = this.this_commander.basic_data.maxlife;
		base.CurLife = (float)base.MaxLife;
		base.Movespeed = this.this_commander.basic_data.speed;
		this.shootCD = this.this_commander.basic_data.shoot_reload_time;
		this.CharacterBaseFightInfo.life = base.MaxLife;
		this.CharacterBaseFightInfo.breakArmor = this.this_commander.basic_data.power;
		this.CharacterBaseFightInfo.defBreak = this.this_commander.basic_data.defense;
		if (this.ModelBody)
		{
			this.ModelBody.tr.localScale = Vector3.one * 3f;
		}
	}

	public override Vector3 GetVPos(T_TankAbstract thisTank)
	{
		int num = SenceManager.inst.Radiuses.IndexOf(thisTank.CharacterBaseFightInfo.ShootMaxRadius - SenceManager.GetVPosDistance);
		Vector3 vector = Vector3.zero;
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
		return vector;
	}

	public override void MouseUp()
	{
		if (UIManager.curState == SenceState.Home)
		{
			return;
		}
		if (SenceManager.inst.fightType == FightingType.Guard)
		{
			return;
		}
		if ((FightPanelManager.inst && FightPanelManager.supperSkillReady) || (FightPanelManager.inst && FightPanelManager.inst.isSpColor))
		{
			DragMgr.inst.MouseUp(MouseCommonType.supperSkill, this.tr.position, null);
		}
		else if (this.charaType == Enum_CharaType.defender)
		{
			EventNoteMgr.NoticeFoceAttack(this.sceneId);
			SenceManager.inst.ForceAttack(this);
		}
	}

	public override void Die()
	{
		LogManage.Log(" 坦克干掉了-----------------  ");
		this.IsDie = true;
		base.CreatDeathEffect();
		if (this.m_lifeInfo != null)
		{
			UnityEngine.Object.Destroy(this.m_lifeInfo.gameObject);
			this.m_lifeInfo.Remove();
		}
		if (this.charaType == Enum_CharaType.attacker)
		{
			SenceManager.inst.RemoveAttackTank(this);
		}
		else if (this.charaType == Enum_CharaType.defender)
		{
			SenceManager.inst.RemoveDefendTank(this);
		}
		if (!NewbieGuidePanel.isEnemyAttck && NewbieGuidePanel.curGuideIndex != -1)
		{
			if (SenceManager.inst.Tanks_Attack.Count > 0)
			{
				HUDTextTool.inst.NextLuaCall("坦克在NPC进攻的时候调用", new object[0]);
			}
			else
			{
				SenceManager.inst.settType = SettlementType.success;
				CoroutineInstance.DoJob(FightHundler.OpenSettlementPanle());
				CSAddResore cSAddResore = new CSAddResore();
				cSAddResore.id = 1;
				ClientMgr.GetNet().SendHttp(1016, cSAddResore, null, null);
			}
		}
		if (NewbieGuidePanel.isEnemyAttck)
		{
			if (this.charaType == Enum_CharaType.defender)
			{
				UnityEngine.Debug.LogError(string.Format("防御坦克 有ID 为 {0} 的坦克", this.sceneId));
			}
			EventNoteMgr.NoticeDie(1, this.sceneId);
			if (this.charaType != Enum_CharaType.defender && this.towerID >= 0L)
			{
				base.SynchronousDead();
				SenceManager.inst.UnitOver(1);
			}
		}
		base.Destory();
	}

	public void DianCi_Hit(int time, int basic_power, string effectname = null)
	{
		LogManage.Log("附加电磁");
		base.Movespeed = 0f;
		base.RoatSpeed = 0f;
		this.MyBuffRuntime.AddBuff(Buff.BuffType.Halo);
		for (int i = 0; i <= time; i++)
		{
			base.StartCoroutine(this.DianCi_Once(i, time, basic_power, effectname));
		}
	}

	[DebuggerHidden]
	private IEnumerator DianCi_Once(int no, int no_max, int power, string effectname = null)
	{
		T_Tank.<DianCi_Once>c__Iterator34 <DianCi_Once>c__Iterator = new T_Tank.<DianCi_Once>c__Iterator34();
		<DianCi_Once>c__Iterator.no = no;
		<DianCi_Once>c__Iterator.no_max = no_max;
		<DianCi_Once>c__Iterator.power = power;
		<DianCi_Once>c__Iterator.effectname = effectname;
		<DianCi_Once>c__Iterator.<$>no = no;
		<DianCi_Once>c__Iterator.<$>no_max = no_max;
		<DianCi_Once>c__Iterator.<$>power = power;
		<DianCi_Once>c__Iterator.<$>effectname = effectname;
		<DianCi_Once>c__Iterator.<>f__this = this;
		return <DianCi_Once>c__Iterator;
	}

	private void TianQiShoot(int shootIndex, Character tarTr, Vector3 tarPos)
	{
		AudioManage.inst.PlayAuidoBySelf_3D(UnitConst.GetInstance().soldierConst[this.index].fightSound, this.ga, false, 0uL);
		if (this.shootPList.Count < 3)
		{
			LogManage.Log("天启 小炮火点 未挂开火点");
			return;
		}
		PoolManage.Ins.CreatEffect(UnitConst.GetInstance().soldierConst[this.index].fightEffect, this.shootPList[2].position, this.shootPList[2].rotation, this.shootPList[2]);
		PoolManage.Ins.CreatEffect(UnitConst.GetInstance().soldierConst[this.index].fightEffect, this.shootPList[3].position, this.shootPList[3].rotation, this.shootPList[3]);
		T_BulletNew bullet = PoolManage.Ins.GetBullet(this.shootPList[2].position, this.shootPList[2].rotation, null);
		T_BulletNew bullet2 = PoolManage.Ins.GetBullet(this.shootPList[3].position, this.shootPList[3].rotation, null);
		bullet.target = ((!(tarTr == null) && !tarTr.IsDie) ? tarTr : null);
		bullet.targetP = tarPos;
		bullet2.target = ((!(tarTr == null) && !tarTr.IsDie) ? tarTr : null);
		bullet2.targetP = tarPos;
		bullet2.SetInfo(this, 2);
		bullet.SetInfo(this, 2);
	}
}
