using msg;
using System;
using UnityEngine;

public class T_AirShip : T_TankAbstract
{
	private bool isFly;

	public override T_TankAbstract.TankType tankType
	{
		get
		{
			return T_TankAbstract.TankType.飞机;
		}
	}

	public override Vector3 GetVPos(T_TankAbstract tank)
	{
		throw new NotImplementedException();
	}

	public override void InitInfo()
	{
		GameObject gameObject = this.tr.Find("JLS").gameObject;
		gameObject.SetActive(false);
		this.IsDie = false;
		float hight = UnitConst.GetInstance().soldierConst[this.index].hight;
		this.bodyForAttack.size = new Vector3(UnitConst.GetInstance().soldierConst[this.index].size * 0.25f, hight, UnitConst.GetInstance().soldierConst[this.index].size * 0.25f);
		this.bodyForAttack.center = new Vector3(0f, hight * 0.5f, 0f);
		this.CharacterControl.radius = 0.5f;
		this.CharacterControl.height = hight;
		this.CharacterControl.center = new Vector3(0f, ((this.CharacterControl.radius <= this.CharacterControl.height) ? this.CharacterControl.height : this.CharacterControl.radius) * 0.5f, 0f);
		this.bulletType = UnitConst.GetInstance().soldierConst[this.index].bulletType;
		this.size = (int)UnitConst.GetInstance().soldierConst[this.index].size;
		this.tr.position += new Vector3(0f, 5f, 0f);
		this.CharacterControl.enabled = false;
		this.TankAIPath.enabled = false;
		if (UIManager.curState != SenceState.WatchVideo)
		{
			if (!SenceInfo.curMap.IsMyMap)
			{
				if (this.charaType == Enum_CharaType.attacker)
				{
					this.CharacterBaseFightInfo = InfoMgr.GetTankFightData(this.index, this.lv, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
				}
				else if (this.charaType == Enum_CharaType.defender)
				{
					this.CharacterBaseFightInfo = InfoMgr.GetTankFightData(this.index, this.lv, SenceInfo.SpyPlayerInfo.vip, SenceInfo.curMap.EnemyTech);
				}
			}
			else if (this.charaType == Enum_CharaType.defender)
			{
				this.CharacterBaseFightInfo = InfoMgr.GetTankFightData(this.index, this.lv, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
			}
		}
		base.MaxLife = this.CharacterBaseFightInfo.life;
		base.CurLife = (float)base.MaxLife;
		string bodyId = UnitConst.GetInstance().soldierConst[this.index].bodyId;
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

	public override void SetMove(bool isMove)
	{
		this.isFly = isMove;
	}

	public override void Update()
	{
		base.Update();
		if (FightPanelManager.IsRetreat)
		{
			return;
		}
		if (this.isFly)
		{
			if (this.forcsTarget)
			{
				base.State = T_TankFightState.TankFightState.Moving;
				Vector3 vector = new Vector3(this.forcsTarget.tr.position.x, this.tr.position.y, this.forcsTarget.tr.position.z);
				Vector3 forward = vector - this.tr.position;
				forward.y = 0f;
				Quaternion to = Quaternion.LookRotation(forward);
				this.tr.rotation = Quaternion.Slerp(this.tr.rotation, to, this.TankAIPath.turningSpeed * Time.deltaTime);
				Vector3 vector2 = vector - this.tr.position;
				vector2.y = 0f;
				this.tr.Translate(vector2.normalized * this.TankAIPath.speed * Time.deltaTime, Space.World);
				if (Vector3.Distance(this.tr.position, vector) < 1f)
				{
					this.SetMove(false);
					base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
				}
			}
			else
			{
				base.State = T_TankFightState.TankFightState.Moving;
				Vector3 forward2 = base.TargetP - this.tr.position;
				forward2.y = 0f;
				Quaternion to2 = Quaternion.LookRotation(forward2);
				this.tr.rotation = Quaternion.Slerp(this.tr.rotation, to2, this.TankAIPath.turningSpeed * Time.deltaTime);
				Vector3 vector3 = base.TargetP - this.tr.position;
				vector3.y = 0f;
				this.tr.Translate(vector3.normalized * this.TankAIPath.speed * Time.deltaTime, Space.World);
				if (Vector3.Distance(this.tr.position, base.TargetP) < 1f)
				{
					this.SetMove(false);
					base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Searching);
				}
			}
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
				Debug.LogError(string.Format("防御坦克 有ID 为 {0} 的坦克", this.sceneId));
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

	public override void UpdateBySecond()
	{
		if (this.IsDie)
		{
			return;
		}
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
							float num = GameTools.GetDistance(this.tr.position, SenceManager.inst.Tanks_Attack[i].tr.position, GameTools.Distance_No_X_Y_Z.NoY) - (float)SenceManager.inst.Tanks_Attack[i].size * 0.5f;
							if (num < 0f)
							{
								num = 0f;
							}
							if (num <= this.CharacterBaseFightInfo.ShootMaxRadius && num >= this.CharacterBaseFightInfo.ShootMinRadius)
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
							if (num <= this.CharacterBaseFightInfo.EyeMaxRadius && num >= this.CharacterBaseFightInfo.EyeMinRadius)
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
							float num2 = Vector3.Distance(this.tr.position, SenceManager.inst.Tanks_Defend[j].tr.position) - (float)SenceManager.inst.Tanks_Defend[j].size * 0.5f;
							if (num2 < 0f)
							{
								num2 = 0f;
							}
							if (num2 <= this.CharacterBaseFightInfo.ShootMaxRadius && num2 >= this.CharacterBaseFightInfo.ShootMinRadius)
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
							float num3 = GameTools.GetDistance(this.tr.position, SenceManager.inst.towers[k].tr.position, GameTools.Distance_No_X_Y_Z.NoY) - (float)SenceManager.inst.towers[k].size * 0.5f;
							if (num3 <= this.CharacterBaseFightInfo.ShootMaxRadius && num3 >= this.CharacterBaseFightInfo.ShootMinRadius)
							{
								if (!this.Targetes.Contains(SenceManager.inst.towers[k]))
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
}
