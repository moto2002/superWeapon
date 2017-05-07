using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class T_Commander : T_TankAbstract
{
	public enum SkillState
	{
		初始,
		可释放,
		释放,
		冷却中
	}

	public CommanderType commanderType;

	public Soldier basic_data = new Soldier();

	public SoldierData soldierdata = new SoldierData();

	private int SkillID;

	public int EnemyCommanderIndex;

	public int skill_Level;

	public bool MasterIsEnemy;

	public float skill_time;

	public float skill_cd_time;

	private float talk_time;

	private float talk_cd_time;

	public TanyaSkillType tanya_skillType;

	public Vector3 tanya_SetBomb_Pos;

	public Vector3 tanya_Return_Pos = Vector3.zero;

	public T_Commander.SkillState NowSkillState;

	public float SkillCD;

	public override T_TankAbstract.TankType tankType
	{
		get
		{
			return T_TankAbstract.TankType.特种兵;
		}
	}

	private void CS_Set(bool enemy = false, int _index = 0, int _level = 0, int _star = 0, int _skill_level = 0)
	{
		this.index = _index;
		this.lv = _level;
		this.star = _star;
		this.skill_Level = _skill_level;
		this.soldierdata = InfoMgr.GetSoldierData(_index, _star, _level);
		this.basic_data.maxlife = this.soldierdata.soldierLevelSet.life;
		this.basic_data.power = this.soldierdata.soldierLevelSet.breakArmor;
		this.basic_data.defense = this.soldierdata.soldierLevelSet.defBreak;
		this.basic_data.speed = this.soldierdata.soldier.speed;
		this.basic_data.talk_time = 14f;
		this.basic_data.shoot_cd_time = this.soldierdata.soldier.shoot_cd_time;
		this.basic_data.shoot_index = this.soldierdata.soldier.shoot_index;
		this.basic_data.shoot_reload_time = this.soldierdata.soldier.shoot_reload_time;
		this.basic_data.first_aureole_tanktype = this.soldierdata.soldierUpSet.armyId;
		this.SkillID = this.soldierdata.soldier.skillID;
		float num;
		if (!enemy)
		{
			int id = UnitConst.GetInstance().skillUpdateConst.Values.SingleOrDefault((SkillUpdate a) => a.itemId == UnitConst.GetInstance().skillList[this.SkillID].skillType && a.level == HeroInfo.GetInstance().Commando_Fight.skillLevel).id;
			num = (float)UnitConst.GetInstance().skillUpdateConst[id].level * 0.1f + 1f;
		}
		else
		{
			int id = UnitConst.GetInstance().skillUpdateConst.Values.SingleOrDefault((SkillUpdate a) => a.itemId == UnitConst.GetInstance().skillList[this.SkillID].skillType && a.level == _skill_level).id;
			num = (float)_skill_level * 0.1f + 1f;
		}
		this.basic_data.tanya_bomb_cd_time = (float)UnitConst.GetInstance().skillList[this.soldierdata.soldier.skillID].coldDown;
		this.basic_data.tanya_bomb_runspeed = 1.5f;
		this.basic_data.tanya_bomb_power = (int)((float)UnitConst.GetInstance().skillList[this.soldierdata.soldier.skillID].basePower * num);
		this.basic_data.tanya_bomb_hurtradius = UnitConst.GetInstance().skillList[this.soldierdata.soldier.skillID].hurtRadius;
		this.basic_data.boris_cure_cd_time = (float)UnitConst.GetInstance().skillList[this.soldierdata.soldier.skillID].coldDown;
		float num2 = (float)UnitConst.GetInstance().skillList[this.soldierdata.soldier.skillID].basePower * num;
		this.basic_data.bores_cure_power = (int)num2;
	}

	public void Init(CommanderType com, bool enemy = false, int index = 0, int level = 0, int star = 0, int skill_level = 0, bool masterIsEnemy = false)
	{
		this.MasterIsEnemy = masterIsEnemy;
		this.CS_Set(enemy, index, level, star, skill_level);
		this.commanderType = com;
		switch (this.commanderType)
		{
		case CommanderType.Tanya:
			this.skill_cd_time = this.basic_data.tanya_bomb_cd_time;
			break;
		case CommanderType.Boris:
			this.skill_cd_time = this.basic_data.boris_cure_cd_time;
			break;
		}
		HUDTextTool.inst.SetText(LanguageManage.GetTextByKey(this.soldierdata.soldier.name, "soldier") + LanguageManage.GetTextByKey("进入战场", "soldier"), this.tr, Color.yellow, 1.5f, 40);
		this.talk_cd_time = this.basic_data.talk_time;
		this.ReSetAureole(true);
		if (FightPanelManager.inst)
		{
			foreach (KeyValuePair<int, SoldierUIITem> current in FightPanelManager.inst.solider_UIDIC)
			{
				if (current.Value.soliderType == SoliderType.Commander)
				{
					current.Value.This_Tank = this;
				}
			}
		}
	}

	private void Update_Tanya()
	{
		if (this.tanya_skillType == TanyaSkillType.None)
		{
			this.SkillCD = this.skill_time / this.skill_cd_time;
			if (this.NowSkillState == T_Commander.SkillState.初始 || this.NowSkillState == T_Commander.SkillState.冷却中)
			{
				this.skill_time += Time.deltaTime;
			}
			if (this.skill_time >= this.skill_cd_time)
			{
				if (this.NowSkillState == T_Commander.SkillState.初始 || this.NowSkillState == T_Commander.SkillState.冷却中)
				{
					this.NowSkillState = T_Commander.SkillState.可释放;
					return;
				}
				if (this.NowSkillState != T_Commander.SkillState.释放)
				{
					return;
				}
				if (this.NowSkillState == T_Commander.SkillState.释放)
				{
					this.skill_time = 0f;
					this.tanya_skillType = TanyaSkillType.WaitToFindTower;
					if (GameSetting.autoFight)
					{
						FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("算了，还是我自己选吧", "soldier"), Color.gray, this.tr);
					}
					else
					{
						FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("也许你要帮我选个目标", "soldier"), Color.gray, this.tr);
					}
					AudioManage.inst.PlayAuidoBySelf_2D("jiguang", base.gameObject, false, 0uL);
					this.forcsTarget = null;
				}
			}
		}
		else if (this.tanya_skillType == TanyaSkillType.WaitToFindTower)
		{
			this.SkillCD = 1f;
			this.skill_time += Time.deltaTime;
			if (this.skill_time >= 3f)
			{
				this.skill_time = 0f;
				if (!GameSetting.autoFight)
				{
					if (this.forcsTarget != null)
					{
						FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("我明白了，向目标前进", "soldier"), Color.gray, this.tr);
					}
					else
					{
						FightPanelManager.inst.CreatFightMessage(LanguageManage.GetTextByKey("算了，还是我自己选吧", "soldier"), Color.gray, this.tr);
						this.forcsTarget = SkillManage.inst.FindTower(this.tr.position, 50f, 1, true, false)[0];
					}
				}
				this.Commander_SillRun(true);
				this.ForceAttack(this.forcsTarget.tr, this.forcsTarget.tr.position);
				AudioManage.inst.PlayAuidoBySelf_2D("tanya", base.gameObject, false, 0uL);
				this.tanya_skillType = TanyaSkillType.FindTowerSuc;
			}
			else
			{
				base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
				if (this.forcsTarget != null)
				{
					this.skill_time = 3f;
				}
			}
		}
		else if (this.tanya_skillType == TanyaSkillType.FindTowerSuc)
		{
			this.SkillCD = 1f;
			if (this.forcsTarget == null)
			{
				this.tanya_skillType = TanyaSkillType.None;
				this.Commander_SillRun(false);
				base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
			}
			this.skill_time -= Time.deltaTime;
			if (this.skill_time <= -15f)
			{
				this.skill_time = 0f;
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("我无法攻击到目标建筑", "soldier"), this.tr, Color.gray, 1.5f, 40);
				this.Commander_SillRun(false);
				this.tanya_skillType = TanyaSkillType.None;
				base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
			}
		}
		else if (this.tanya_skillType == TanyaSkillType.SetBomb)
		{
			this.SkillCD = 1f;
			this.tr.position = this.tanya_SetBomb_Pos;
			if (this.skill_time < 0f)
			{
				this.skill_time = 0f;
				this.Commander_SillRun(false);
			}
			if (this.forcsTarget == null)
			{
				this.SetBomb_Back();
				this.tanya_skillType = TanyaSkillType.None;
				base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
			}
			else
			{
				this.skill_time += Time.deltaTime;
				if (this.skill_time >= 1f)
				{
					this.skill_time = 0f;
					this.SetBomb_Back();
				}
			}
		}
		else if (this.tanya_skillType == TanyaSkillType.OutFromTower)
		{
			this.SkillCD = 1f;
			this.skill_time += Time.deltaTime;
			float num = Vector3.Distance(base.transform.position, this.tanya_Return_Pos);
			if (this.skill_time > 0.5f)
			{
				this.skill_time = 0f;
				this.tanya_skillType = TanyaSkillType.None;
				this.NowSkillState = T_Commander.SkillState.冷却中;
				base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
			}
			this.ForceMoving(this.tanya_Return_Pos, this.tanya_Return_Pos, 1f);
		}
		this.talk_time += Time.deltaTime;
		if (this.talk_time >= this.talk_cd_time)
		{
			this.talk_time = 0f;
		}
	}

	public void SetBomb_Back()
	{
		this.tr.position = this.tanya_SetBomb_Pos;
		this.tanya_skillType = TanyaSkillType.OutFromTower;
		base.T_TankFightState.ChangeState(T_TankFightState.TankFightState.Idle);
	}

	public void Commander_SillRun(bool run)
	{
		if (this.TankAIPath)
		{
			this.TankAIPath.speed = base.Movespeed;
		}
	}

	public void UseSkillByPlayer()
	{
		this.NowSkillState = T_Commander.SkillState.释放;
	}

	private void Update_Boris()
	{
		this.SkillCD = this.skill_time / this.skill_cd_time;
		if (this.NowSkillState == T_Commander.SkillState.初始 || this.NowSkillState == T_Commander.SkillState.冷却中)
		{
			this.skill_time += Time.deltaTime;
		}
		if (this.skill_time >= this.skill_cd_time)
		{
			if (this.NowSkillState == T_Commander.SkillState.初始 || this.NowSkillState == T_Commander.SkillState.冷却中)
			{
				this.NowSkillState = T_Commander.SkillState.可释放;
				return;
			}
			if (this.NowSkillState != T_Commander.SkillState.释放)
			{
				return;
			}
			if (this.NowSkillState == T_Commander.SkillState.释放)
			{
				this.skill_time = 0f;
				this.talk_time = 0f;
				this.DoHurt(0 - this.basic_data.bores_cure_power);
				HUDTextTool.inst.SetText("+" + this.basic_data.bores_cure_power, this.tr, Color.green, 0.8f, 40);
				PoolManage.Ins.GetEffectByName("jinjiweixiu", this.tr).DesInsInPool(1f);
				AudioManage.inst.PlayAuidoBySelf_2D("relifeB", null, false, 0uL);
				this.NowSkillState = T_Commander.SkillState.冷却中;
			}
		}
	}

	public void ReSetAureole(bool on)
	{
		this.SetAureole(on);
	}

	private void SetAureole(bool on)
	{
		LogManage.Log("生成特种兵光环！");
		bool percentage = true;
		if (on)
		{
			for (int i = 0; i < SenceManager.inst.Tanks_Attack.Count; i++)
			{
				if (SenceManager.inst.Tanks_Attack[i].tankType != T_TankAbstract.TankType.特种兵 && SenceManager.inst.Tanks_Attack[i].index == this.soldierdata.soldierUpSet.armyId && !SenceManager.inst.Tanks_Attack[i].CommanderAureole_Action)
				{
					SenceManager.inst.Tanks_Attack[i].CommanderAureole_Action = true;
					foreach (KeyValuePair<SpecialPro, int> current in this.soldierdata.soldierUpSet.armyAffixes)
					{
						this.SetAureoleBuff(percentage, SenceManager.inst.Tanks_Attack[i], current.Key, current.Value, on);
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < SenceManager.inst.Tanks_Attack.Count; j++)
			{
				if (SenceManager.inst.Tanks_Attack[j].tankType != T_TankAbstract.TankType.特种兵 && SenceManager.inst.Tanks_Attack[j].index == this.soldierdata.soldierUpSet.armyId && SenceManager.inst.Tanks_Attack[j].CommanderAureole_Action)
				{
					foreach (KeyValuePair<SpecialPro, int> current2 in this.soldierdata.soldierUpSet.armyAffixes)
					{
						this.SetAureoleBuff(percentage, SenceManager.inst.Tanks_Attack[j], current2.Key, current2.Value, on);
					}
				}
			}
		}
	}

	public void SetAureoleBuff(bool percentage, T_TankAbstract tank, SpecialPro bufftype, int buffpower, bool on)
	{
		if (on)
		{
			PoolManage.Ins.GetEffectByName("heidongzhuangjia", tank.tr).DesInsInPool(1f);
		}
		else
		{
			PoolManage.Ins.GetEffectByName("zhanzhengkongjv", tank.tr).DesInsInPool(1.5f);
		}
	}

	public override Vector3 GetVPos(T_TankAbstract tank)
	{
		return Vector3.zero;
	}

	public override void ForceMoving(Vector3 _pos, Vector3 tarPos, float speedTimes = 1f)
	{
		if (this.tanya_skillType == TanyaSkillType.WaitToFindTower || this.tanya_skillType == TanyaSkillType.SetBomb)
		{
			LogManage.LogError("this_commander");
			return;
		}
		if (this.tanya_skillType == TanyaSkillType.OutFromTower)
		{
			_pos = this.tanya_Return_Pos;
			tarPos = this.tanya_Return_Pos;
		}
		base.ForceMoving(_pos, tarPos, speedTimes);
	}

	public override void InitInfo()
	{
		GameObject gameObject = this.tr.Find("JLS").gameObject;
		gameObject.SetActive(false);
		this.IsDie = false;
		this.size = 2;
		this.bulletType = 0;
		this.height = 4f;
		this.bodyForAttack.size = new Vector3((float)this.size * 0.25f, this.height, (float)this.size * 0.25f);
		this.bodyForAttack.center = new Vector3(0f, this.height * 0.5f, 0f);
		this.CharacterControl.radius = 0.5f;
		this.CharacterControl.height = this.height;
		this.CharacterControl.center = new Vector3(0f, ((this.CharacterControl.radius <= this.CharacterControl.height) ? this.CharacterControl.height : this.CharacterControl.radius) * 0.5f, 0f);
		if (UIManager.curState != SenceState.WatchVideo)
		{
			if (this.charaType == Enum_CharaType.attacker)
			{
				this.CharacterBaseFightInfo = InfoMgr.GetSoliderFightData(this.index, this.lv, this.star, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
			}
			else if (this.charaType == Enum_CharaType.defender)
			{
				if (!SenceInfo.curMap.IsMyMap)
				{
					this.CharacterBaseFightInfo = InfoMgr.GetSoliderFightData(this.index, this.lv, this.star, SenceInfo.SpyPlayerInfo.vip, SenceInfo.curMap.EnemyTech);
				}
				else
				{
					this.CharacterBaseFightInfo = InfoMgr.GetSoliderFightData(this.index, this.lv, this.star, HeroInfo.GetInstance().vipData.VipLevel, HeroInfo.GetInstance().PlayerTechnologyInfo);
				}
			}
		}
		base.Movespeed = this.CharacterBaseFightInfo.moveSpeed;
		base.RoatSpeed = this.CharacterBaseFightInfo.rotaSpeed;
		base.MaxLife = this.CharacterBaseFightInfo.life;
		base.CurLife = (float)base.MaxLife;
		string bodyId;
		if (this.EnemyCommanderIndex == 0)
		{
			bodyId = UnitConst.GetInstance().soldierList[HeroInfo.GetInstance().Commando_Fight.index].bodyId;
		}
		else
		{
			bodyId = UnitConst.GetInstance().soldierList[this.EnemyCommanderIndex].bodyId;
		}
		base.CreateBody(bodyId);
		if (this.ModelBody)
		{
			this.ModelBody.tr.localScale = Vector3.one * 3f;
		}
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
		if (this.TankAIPath)
		{
			this.TankAIPath.enabled = isMove;
			this.TankAIPath.canMove = isMove;
		}
	}

	public override void Die()
	{
		LogManage.Log(" 坦克干掉了-----------------  ");
		this.IsDie = true;
		SettlementManager.isDead = true;
		this.ReSetAureole(false);
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
		if (this.AnimationControler)
		{
			this.AnimationControler.AnimPlay("Die");
		}
		base.GetComponent<AIPath>().enabled = false;
		if (FightPanelManager.inst != null)
		{
			FightPanelManager.inst.CommandSoliderIsDead();
		}
		base.Destory();
	}

	public override void Update()
	{
		base.Update();
		if (FightHundler.FightEnd)
		{
			return;
		}
		if (this.IsDie)
		{
			return;
		}
		int skillID = this.SkillID;
		if (skillID != 1001)
		{
			if (skillID == 1002)
			{
				this.Update_Boris();
			}
		}
		else
		{
			this.Update_Tanya();
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
							float num3 = GameTools.GetDistance(this.tr.position, SenceManager.inst.towers[k].tr.position, GameTools.Distance_No_X_Y_Z.NoY) - (float)UnitConst.GetInstance().buildingConst[SenceManager.inst.towers[k].index].size * 0.5f;
							if (this.forcsTarget != null && SenceManager.inst.towers[k] == this.forcsTarget)
							{
								if (this.forcsTarget is T_Tower)
								{
									num3 = Vector3.Distance(this.tr.position, this.forcsTarget.tr.position) - (float)this.forcsTarget.size * 0.5f;
								}
								else if (this.forcsTarget is T_Tank)
								{
									num3 = Vector3.Distance(this.tr.position, this.forcsTarget.tr.position) - (float)this.forcsTarget.size * 0.5f;
								}
								if (this.tanya_skillType != TanyaSkillType.None)
								{
									if (num3 > 5f && this.tanya_skillType == TanyaSkillType.FindTowerSuc)
									{
										this.tanya_Return_Pos = this.tr.position;
									}
									if (num3 < 1.5f && this.tanya_skillType == TanyaSkillType.FindTowerSuc)
									{
										if (this.tanya_Return_Pos == Vector3.zero)
										{
											this.tanya_Return_Pos = this.tr.position;
										}
										this.tanya_skillType = TanyaSkillType.SetBomb;
										if (this.AnimationControler && !this.IsDie)
										{
											this.AnimationControler.AnimPlay("Attack2");
										}
										if (this.forcsTarget != null)
										{
											SkillManage.inst.SetDirectionalBlasting(this.forcsTarget.tr.position, 50, this);
										}
										this.tanya_SetBomb_Pos = this.tr.position;
									}
								}
							}
							if (num3 <= this.CharacterBaseFightInfo.ShootMaxRadius && num3 >= this.CharacterBaseFightInfo.ShootMinRadius)
							{
								if (this.tanya_skillType != TanyaSkillType.None)
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
}
