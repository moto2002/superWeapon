using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SkillManage : MonoBehaviour
{
	public enum TankSuppliesBox_Type
	{
		HuiXiong = 2,
		DuoGongNeng = 1,
		TianQi = 3,
		HuoJian,
		GuangLing,
		DianCi
	}

	public static SkillManage inst;

	private int renju;

	private Vector3 shootPos;

	private int skillId;

	private GameObject S_BulletRes;

	public bool IsSkiillShow;

	public List<T_TankAbstract> skill_tanks = new List<T_TankAbstract>();

	public List<T_TankAbstract> supplies_tanks = new List<T_TankAbstract>();

	private bool Air_Boom;

	public float skill_excursion;

	public Dictionary<int, int> EnemySkillList = new Dictionary<int, int>();

	public bool EnemySkillOpen;

	private ScreenOverlay MainCamera_ScreenOverlay;

	private float HeDan_Light_speed;

	private bool HeDan_Light0;

	private ScreenOverlay MainCamera_ScreenOverlay1;

	private float SanDian_Light_speed;

	private List<float> SanDian_Light_MidTime = new List<float>();

	private int SanDian_Light_Num;

	private bool SanDian_Light0;

	private float SanDian_Light_time;

	private bool TeamStarUP_Used;

	private int lv = 1;

	private Vector3 point;

	public GameObject ReadyUseSkill_circleEffect;

	public FightPanel_SkillAndSoliderUIItem useskillCard;

	private float use_time;

	public bool ReadyUseSkill;

	public bool ReadyUseSkill_Next;

	public void OnDestroy()
	{
		SkillManage.inst = null;
	}

	private void Awake()
	{
		SkillManage.inst = this;
		this.S_BulletRes = (GameObject)Resources.Load(ResManager.BulletRes_Path + "SupperBullet");
		base.gameObject.AddComponent<SkillBuffManage>();
		base.gameObject.AddComponent<DamageMonitor>();
		base.gameObject.AddComponent<T_TankAIManager>();
		new GameObject("LegionMapManager")
		{
			transform = 
			{
				parent = base.transform,
				localPosition = Vector3.zero + new Vector3(0f, -1000f, 0f)
			}
		}.AddComponent<LegionMapManager>();
	}

	public void AcquittalSkill(int skillID, Vector3 pos, int skillLV = -1)
	{
		if (skillLV == -1)
		{
			this.EnemySkillOpen = false;
			this.EnemySkillList.Clear();
		}
		else
		{
			this.EnemySkillOpen = true;
			if (!this.EnemySkillList.ContainsKey(skillID))
			{
				this.EnemySkillList.Add(skillID, skillLV);
			}
			else
			{
				this.EnemySkillList[skillID] = skillLV;
			}
		}
		FightPanelManager.supperSkillReady = false;
		LogManage.Log("释放技能：skillID=" + skillID);
		UnityEngine.Debug.Log("释放技能：skillID=" + skillID);
		if (skillID >= 100000)
		{
			skillID = 7;
			if (FightPanelManager.inst)
			{
				FightPanelManager.inst.SetAirCD();
			}
		}
		else if (FightPanelManager.inst)
		{
			FightPanelManager.inst.SetSkillCD();
		}
		if (UnitConst.GetInstance().skillList[skillID].skillType != 3)
		{
			pos = new Vector3(pos.x - this.skill_excursion, 0f, pos.z - this.skill_excursion);
		}
		NewSkillType skillType = (NewSkillType)UnitConst.GetInstance().skillList[skillID].skillType;
		switch (skillType)
		{
		case NewSkillType.Sanbing:
			if (UIManager.curState != SenceState.WatchVideo)
			{
				SkillManage.inst.CreateParatrooper(pos, skillID);
			}
			goto IL_3D1;
		case NewSkillType.Jingzhunpaoji:
			PoolManage.Ins.CreatEffect(UnitConst.GetInstance().skillList[skillID].fightEffect, pos, Quaternion.identity, null);
			SkillManage.inst.CreatePrecisionArtillery(pos, skillID);
			goto IL_3D1;
		case (NewSkillType)3:
		case (NewSkillType)8:
			IL_17D:
			if (skillType != NewSkillType.None)
			{
				goto IL_3D1;
			}
			goto IL_3D1;
		case NewSkillType.Hedan:
		{
			Vector3 vector = pos;
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("S_3_guiji", null);
			if (modelByBundleByName)
			{
				modelByBundleByName.tr.position = new Vector3(vector.x, 15f, vector.z);
				modelByBundleByName.tr.rotation = Quaternion.Euler(90f, 0f, 0f);
				BoxCollider compentIfNoAddOne = GameTools.GetCompentIfNoAddOne<BoxCollider>(modelByBundleByName.ga);
				compentIfNoAddOne.isTrigger = true;
				compentIfNoAddOne.size = new Vector3(1f, 1f, 2f);
				GameTools.GetCompentIfNoAddOne<Rigidbody>(modelByBundleByName.ga);
				T_NuclearBomb compentIfNoAddOne2 = GameTools.GetCompentIfNoAddOne<T_NuclearBomb>(modelByBundleByName.ga);
				compentIfNoAddOne2.skillID = skillID;
				compentIfNoAddOne2.body = modelByBundleByName;
				compentIfNoAddOne2.Init(T_NuclearBomb.Boomer_Kind.Hedan, 0f);
			}
			goto IL_3D1;
		}
		case NewSkillType.Tiemu:
		case NewSkillType.Charge:
		case NewSkillType.Blitzkrieg:
		case NewSkillType.BlackholeArmor:
		case NewSkillType.SmokeShell:
		case NewSkillType.Samson:
			SkillBuffManage.inst.CreateBuffPlace(skillID, pos);
			goto IL_3D1;
		case NewSkillType.Shandianfengbao:
			SkillManage.inst.CreateLightningStorm(pos, skillID);
			goto IL_3D1;
		case NewSkillType.TeamStarUP:
			SkillManage.inst.CreatTeamStarUP(pos, skillID);
			goto IL_3D1;
		case NewSkillType.MechanizedSupplyBox:
			switch (skillID)
			{
			case 25:
				SkillManage.inst.Set_TankSuppliesBox(SkillManage.TankSuppliesBox_Type.DuoGongNeng, pos, 1, 1);
				break;
			case 26:
				SkillManage.inst.Set_TankSuppliesBox(SkillManage.TankSuppliesBox_Type.DuoGongNeng, pos, 1, 2);
				break;
			case 27:
				SkillManage.inst.Set_TankSuppliesBox(SkillManage.TankSuppliesBox_Type.DuoGongNeng, pos, 1, 3);
				break;
			}
			goto IL_3D1;
		case NewSkillType.TankSupplyBox:
			switch (skillID)
			{
			case 28:
				SkillManage.inst.Set_TankSuppliesBox(SkillManage.TankSuppliesBox_Type.HuiXiong, pos, 1, 1);
				break;
			case 29:
				SkillManage.inst.Set_TankSuppliesBox(SkillManage.TankSuppliesBox_Type.HuiXiong, pos, 1, 2);
				break;
			case 30:
				SkillManage.inst.Set_TankSuppliesBox(SkillManage.TankSuppliesBox_Type.HuiXiong, pos, 1, 3);
				break;
			}
			goto IL_3D1;
		case NewSkillType.FieldMaintenance:
		case NewSkillType.EmergencyMaintenance:
			SkillBuffManage.inst.Repair_Place(skillID, pos);
			goto IL_3D1;
		case NewSkillType.DirectionalBlasting:
			this.SetDirectionalBlasting(pos, skillID, null);
			goto IL_3D1;
		case NewSkillType.ElectromagneticGun:
			this.SetElectromagneticGun(pos, skillID);
			goto IL_3D1;
		case NewSkillType.RemoteNavalGun:
			this.SetRemoteNavalGun(pos, skillID);
			goto IL_3D1;
		}
		goto IL_17D;
		IL_3D1:
		if (FightPanelManager.inst)
		{
		}
		if (UIManager.curState != SenceState.WatchVideo)
		{
			EventNoteMgr.SaveSupperSkill(skillID, pos);
			FightHundler.AddDeadSettleUnites(new SettleUniteData
			{
				deadType = 4,
				deadSenceId = (long)skillID,
				posX = pos.x,
				posZ = pos.z,
				RandomSeed = GameSetting.RandomSeed
			});
		}
	}

	public void CreateParatrooper(Vector3 pos, int skillID)
	{
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		PoolManage.Ins.CreatEffect("xuanzhong_green", pos, Quaternion.identity, null).LifeTime = 3f;
		for (int i = 0; i < skill.renju; i++)
		{
			int num;
			if (!this.EnemySkillOpen)
			{
				num = HeroInfo.GetInstance().playerlevel;
			}
			else
			{
				num = this.EnemySkillList[skillID];
			}
			base.StartCoroutine(this.CreateSanBing(i, pos, SenceManager.inst.SoldierId, skill.basePower, 1, (int)SenceManager.inst.SoldierId, num, true, true, 0));
		}
	}

	[DebuggerHidden]
	private IEnumerator CreateSanBing(int No, Vector3 pos, long carryId, int soldierIdx, int soldierNum, int team, int lv, bool isSanbing = false, bool isPingBiSendMessage = false, int tanksuppliesboxlv = 0)
	{
		SkillManage.<CreateSanBing>c__Iterator55 <CreateSanBing>c__Iterator = new SkillManage.<CreateSanBing>c__Iterator55();
		<CreateSanBing>c__Iterator.No = No;
		<CreateSanBing>c__Iterator.pos = pos;
		<CreateSanBing>c__Iterator.soldierNum = soldierNum;
		<CreateSanBing>c__Iterator.carryId = carryId;
		<CreateSanBing>c__Iterator.soldierIdx = soldierIdx;
		<CreateSanBing>c__Iterator.<$>No = No;
		<CreateSanBing>c__Iterator.<$>pos = pos;
		<CreateSanBing>c__Iterator.<$>soldierNum = soldierNum;
		<CreateSanBing>c__Iterator.<$>carryId = carryId;
		<CreateSanBing>c__Iterator.<$>soldierIdx = soldierIdx;
		<CreateSanBing>c__Iterator.<>f__this = this;
		return <CreateSanBing>c__Iterator;
	}

	public void CreatePrecisionArtillery(Vector3 pos, int skillID)
	{
		GameObject gameObject = Resources.Load(string.Empty) as GameObject;
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		this.shootPos = pos;
		this.skillId = skillID;
		this.renju = skill.renju;
		base.StartCoroutine(this.StartShoot(pos, skillID));
	}

	[DebuggerHidden]
	private IEnumerator StartShoot(Vector3 pos, int skillID)
	{
		SkillManage.<StartShoot>c__Iterator56 <StartShoot>c__Iterator = new SkillManage.<StartShoot>c__Iterator56();
		<StartShoot>c__Iterator.skillID = skillID;
		<StartShoot>c__Iterator.<$>skillID = skillID;
		<StartShoot>c__Iterator.<>f__this = this;
		return <StartShoot>c__Iterator;
	}

	private void Shoot(Vector3 pos, int skillID, int index)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.S_BulletRes, new Vector3(T_MotherShip.inst.tr.position.x / 2f, 10f, T_MotherShip.inst.tr.position.z), T_MotherShip.inst.tr.rotation) as GameObject;
		gameObject.transform.parent = SenceManager.inst.bulletPool;
		T_SupperBullet component = gameObject.GetComponent<T_SupperBullet>();
		component.idx = skillID;
		component.skillID = skillID;
		component.hitCount = UnitConst.GetInstance().skillList[skillID].hitCount;
		component.shootPos = pos;
		component.sc.radius = (float)UnitConst.GetInstance().skillList[skillID].hurtRadius;
		component.index = index;
		component.renju = this.renju;
		if (!this.EnemySkillOpen)
		{
			DamageMonitor.inst.AddDamageRecord(skillID, 0, (int)((float)UnitConst.GetInstance().skillList[skillID].basePower * (1f + (float)HeroInfo.GetInstance().SkillInfo[UnitConst.GetInstance().skillList[skillID].skillType] * 0.1f)));
		}
	}

	private void SetRemoteNavalGun(Vector3 pos, int skillID)
	{
		SkillManage.inst.CreateRemoteNavalGun(pos, skillID);
	}

	public void CreateRemoteNavalGun(Vector3 pos, int skillID)
	{
		GameObject gameObject = Resources.Load(string.Empty) as GameObject;
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		List<T_Tower> list = new List<T_Tower>();
		list = this.FindTower(pos, (float)skill.attarkRadius, skill.hurtRadius, true, false);
		Vector3 vector = default(Vector3);
		int num = 0;
		for (int i = 0; i < skill.renju; i++)
		{
			if (num % 2 == 0 && i / 2 < list.Count && list.Count > 0 && list[i / 2].ga != null)
			{
				vector = list[i / 2].tr.position;
				num++;
			}
			else
			{
				num++;
				vector = pos + new Vector3((float)UnityEngine.Random.Range(-skill.attarkRadius, skill.attarkRadius), 0f, (float)UnityEngine.Random.Range(-skill.attarkRadius, skill.attarkRadius));
			}
			PoolManage.Ins.CreatEffect(UnitConst.GetInstance().skillList[5].fightEffect, vector, Quaternion.identity, null);
			base.StartCoroutine(this.StartNavalShoot(0.3f + float.Parse(skill.renjuCd) * (float)i, vector, skillID, i));
		}
	}

	[DebuggerHidden]
	private IEnumerator StartNavalShoot(float time, Vector3 pos, int skillID, int i)
	{
		SkillManage.<StartNavalShoot>c__Iterator57 <StartNavalShoot>c__Iterator = new SkillManage.<StartNavalShoot>c__Iterator57();
		<StartNavalShoot>c__Iterator.time = time;
		<StartNavalShoot>c__Iterator.pos = pos;
		<StartNavalShoot>c__Iterator.skillID = skillID;
		<StartNavalShoot>c__Iterator.i = i;
		<StartNavalShoot>c__Iterator.<$>time = time;
		<StartNavalShoot>c__Iterator.<$>pos = pos;
		<StartNavalShoot>c__Iterator.<$>skillID = skillID;
		<StartNavalShoot>c__Iterator.<$>i = i;
		<StartNavalShoot>c__Iterator.<>f__this = this;
		return <StartNavalShoot>c__Iterator;
	}

	public void CreateNuclearBomb(Transform pos, int skillID)
	{
		LogManage.Log("核弹ID:" + skillID);
		base.StartCoroutine(this.StarNuclearBomb(pos, skillID));
	}

	[DebuggerHidden]
	private IEnumerator StarNuclearBomb(Transform pos, int skillID)
	{
		SkillManage.<StarNuclearBomb>c__Iterator58 <StarNuclearBomb>c__Iterator = new SkillManage.<StarNuclearBomb>c__Iterator58();
		<StarNuclearBomb>c__Iterator.pos = pos;
		<StarNuclearBomb>c__Iterator.skillID = skillID;
		<StarNuclearBomb>c__Iterator.<$>pos = pos;
		<StarNuclearBomb>c__Iterator.<$>skillID = skillID;
		<StarNuclearBomb>c__Iterator.<>f__this = this;
		return <StarNuclearBomb>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator EndNuclearBomb(T_Tower tower, int power, int no, int max_no)
	{
		SkillManage.<EndNuclearBomb>c__Iterator59 <EndNuclearBomb>c__Iterator = new SkillManage.<EndNuclearBomb>c__Iterator59();
		<EndNuclearBomb>c__Iterator.no = no;
		<EndNuclearBomb>c__Iterator.max_no = max_no;
		<EndNuclearBomb>c__Iterator.power = power;
		<EndNuclearBomb>c__Iterator.tower = tower;
		<EndNuclearBomb>c__Iterator.<$>no = no;
		<EndNuclearBomb>c__Iterator.<$>max_no = max_no;
		<EndNuclearBomb>c__Iterator.<$>power = power;
		<EndNuclearBomb>c__Iterator.<$>tower = tower;
		return <EndNuclearBomb>c__Iterator;
	}

	public void CreateLightningStorm(Vector3 pos, int skillID)
	{
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		PoolManage.Ins.CreatEffect(skill.bodyEffect, new Vector3(pos.x, 15f, pos.z), Quaternion.identity, null);
		this.StartLightingStorm(pos, skillID);
	}

	private void StartLightingStorm(Vector3 pos, int skillID)
	{
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		for (int i = 0; i < skill.renju; i++)
		{
			List<T_TankAbstract> hurt_tank_list = new List<T_TankAbstract>();
			List<T_Tower> hurt_tower_list = new List<T_Tower>();
			hurt_tank_list = this.FindTank(pos, (float)skill.hurtRadius, 0, false);
			hurt_tower_list = this.FindTower(pos, (float)skill.hurtRadius, 0, false, false);
			int damage;
			if (!this.EnemySkillOpen)
			{
				damage = (int)((float)UnitConst.GetInstance().skillList[skillID].basePower * (1f + (float)HeroInfo.GetInstance().SkillInfo[UnitConst.GetInstance().skillList[skillID].skillType] * 0.1f));
			}
			else
			{
				damage = (int)((float)UnitConst.GetInstance().skillList[skillID].basePower * (1f + (float)this.EnemySkillList[skillID] * 0.1f));
			}
			PoolManage.Ins.CreatEffect(skill.damageEffect, new Vector3(pos.x, 0f, pos.z), Quaternion.identity, null);
			base.StartCoroutine(this.LightingStormHurtToTower(i, damage, hurt_tower_list, hurt_tank_list));
		}
	}

	[DebuggerHidden]
	private IEnumerator LightingStormHurtToTower(int no_now, int damage, List<T_Tower> hurt_tower_list, List<T_TankAbstract> hurt_tank_list)
	{
		SkillManage.<LightingStormHurtToTower>c__Iterator5A <LightingStormHurtToTower>c__Iterator5A = new SkillManage.<LightingStormHurtToTower>c__Iterator5A();
		<LightingStormHurtToTower>c__Iterator5A.no_now = no_now;
		<LightingStormHurtToTower>c__Iterator5A.hurt_tower_list = hurt_tower_list;
		<LightingStormHurtToTower>c__Iterator5A.damage = damage;
		<LightingStormHurtToTower>c__Iterator5A.hurt_tank_list = hurt_tank_list;
		<LightingStormHurtToTower>c__Iterator5A.<$>no_now = no_now;
		<LightingStormHurtToTower>c__Iterator5A.<$>hurt_tower_list = hurt_tower_list;
		<LightingStormHurtToTower>c__Iterator5A.<$>damage = damage;
		<LightingStormHurtToTower>c__Iterator5A.<$>hurt_tank_list = hurt_tank_list;
		return <LightingStormHurtToTower>c__Iterator5A;
	}

	private void SetCamera_HeDan_Light()
	{
		this.HeDan_Light0 = true;
		if (CameraControl.inst.MainCamera.GetComponent<ScreenOverlay>() != null)
		{
			CameraControl.inst.MainCamera.GetComponent<ScreenOverlay>().enabled = true;
			this.MainCamera_ScreenOverlay = CameraControl.inst.MainCamera.GetComponent<ScreenOverlay>();
			this.MainCamera_ScreenOverlay.blendMode = ScreenOverlay.OverlayBlendMode.Multiply;
			this.MainCamera_ScreenOverlay.intensity = 2f;
			this.HeDan_Light_speed = 0.25f;
		}
	}

	private void SetCamera_SanDian_Light()
	{
		this.SanDian_Light0 = true;
		if (CameraControl.inst.MainCamera.GetComponent<ScreenOverlay>() != null)
		{
			CameraControl.inst.MainCamera.GetComponent<ScreenOverlay>().enabled = true;
			this.MainCamera_ScreenOverlay1 = CameraControl.inst.MainCamera.GetComponent<ScreenOverlay>();
			this.MainCamera_ScreenOverlay1.blendMode = ScreenOverlay.OverlayBlendMode.Multiply;
			this.MainCamera_ScreenOverlay1.intensity = 2f;
			this.SanDian_Light_speed = 15f;
			this.SanDian_Light_Num = 33;
			for (int i = 0; i < this.SanDian_Light_Num; i++)
			{
				if (i < 10)
				{
					this.SanDian_Light_MidTime.Add(0.09f);
				}
				else
				{
					this.SanDian_Light_MidTime.Add(UnityEngine.Random.Range(0.1f, 0.3f));
				}
			}
		}
	}

	private void Update()
	{
		this.Hit_Use_Skill_Updata();
		if (FightPanelManager.inst != null)
		{
			for (int i = 0; i < FightPanelManager.inst.skillUIList.Count; i++)
			{
				if (FightPanelManager.inst.skillUIList[i] != null)
				{
					FightPanelManager.inst.skillUIList[i].SkillUIITem_Update();
				}
			}
		}
		if (this.TeamStarUP_Used && this.skill_tanks.Count > 0)
		{
			for (int j = 0; j < this.skill_tanks.Count; j++)
			{
				if (this.skill_tanks[j] != null)
				{
				}
			}
		}
	}

	public void CreatTeamStarUP(Vector3 pos, int skillID)
	{
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		List<T_Tank> list = new List<T_Tank>();
		List<T_Tower> list2 = new List<T_Tower>();
		skill.hurtRadius = 5;
		LogManage.Log("释放星级强化！作用范围：" + skill.hurtRadius);
		switch (skillID)
		{
		case 25:
			skillID = 40;
			break;
		case 26:
			skillID = 41;
			break;
		case 27:
			skillID = 42;
			break;
		}
		AudioManage.inst.PlayAuido("armystarup", false);
		int[] buffId = UnitConst.GetInstance().skillList[skillID].buffId;
		if (buffId != null)
		{
			for (int i = 0; i < buffId.Length; i++)
			{
				this.lv = UnitConst.GetInstance().BuffConst[buffId[i]].buffLevel;
				for (int j = 0; j < this.skill_tanks.Count; j++)
				{
					if (this.skill_tanks[j] && Vector3.Distance(pos, this.skill_tanks[j].tr.position) < (float)skill.hurtRadius && this.skill_tanks[j].star < this.lv)
					{
						this.skill_tanks[j].MyBuffRuntime.AddBuffIndex(skillID, null, UnitConst.GetInstance().skillList[skillID].buffId);
					}
				}
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator StarUP_BuffEnd(T_TankAbstract tank, int skillID, float time)
	{
		SkillManage.<StarUP_BuffEnd>c__Iterator5B <StarUP_BuffEnd>c__Iterator5B = new SkillManage.<StarUP_BuffEnd>c__Iterator5B();
		<StarUP_BuffEnd>c__Iterator5B.time = time;
		<StarUP_BuffEnd>c__Iterator5B.tank = tank;
		<StarUP_BuffEnd>c__Iterator5B.<$>time = time;
		<StarUP_BuffEnd>c__Iterator5B.<$>tank = tank;
		return <StarUP_BuffEnd>c__Iterator5B;
	}

	public void CreatNAIRON(Vector3 pos, int skillID)
	{
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		List<T_Tank> list = new List<T_Tank>();
		List<T_Tower> list2 = new List<T_Tower>();
		skill.hurtRadius = 5;
		List<T_TankAbstract> list3 = this.FindTank(pos, 20f, 0, true);
		for (int i = 0; i < list3.Count; i++)
		{
		}
		for (int j = 0; j < list3.Count; j++)
		{
			HUDTextTool.inst.SetText("技能作用", list3[j].tr, Color.red, true);
		}
	}

	public void KillMJ(int id, Vector3 pos)
	{
		AudioManage.inst.audioPlay.Stop();
		DieBall dieBall = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().skillList[id].damageEffect, HUDTextTool.inst.hedanBoom, Quaternion.identity, null);
		AudioManage.inst.PlayAuidoBySelf_3D("skill4_hit", dieBall.ga, false, 0uL);
	}

	public void Set_TankSuppliesBox(SkillManage.TankSuppliesBox_Type tank_type, Vector3 pos, int box_id, int box_lv)
	{
		Body_Model body_Model = new Body_Model();
		body_Model = PoolManage.Ins.GetModelByBundleByName("case", null);
		AudioManage.inst.PlayAuidoBySelf_3D("Packagedrop", body_Model.ga, false, 0uL);
		Transform transform = body_Model.tr.FindChild("case_w");
		Transform transform2 = body_Model.tr.FindChild("case_y");
		Transform transform3 = body_Model.tr.FindChild("case_g");
		transform3.gameObject.SetActive(box_lv == 1);
		transform2.gameObject.SetActive(box_lv == 3);
		transform.gameObject.SetActive(box_lv == 2);
		body_Model.tr.position = pos + new Vector3(0f, 15f, 0f);
		TankSuppliesBox tankSuppliesBox = body_Model.gameObject.AddComponent<TankSuppliesBox>();
		tankSuppliesBox.size_X = 2f;
		tankSuppliesBox.size_Z = 2f;
		tankSuppliesBox.tankbox_type = tank_type;
		tankSuppliesBox.tankbox_id = box_id;
		tankSuppliesBox.tankbox_lv = box_lv;
		tankSuppliesBox.down_speed = 10f;
		tankSuppliesBox.Init();
		PoolManage.Ins.CreatEffect("xuanzhong_green", pos, Quaternion.identity, null).LifeTime = 3f;
		GameObject otherModelByName = PoolManage.Ins.GetOtherModelByName("SanBing", null);
		AudioManage.inst.PlayAuidoBySelf_3D("skill2_open", tankSuppliesBox.gameObject, false, 0uL);
		otherModelByName.transform.parent = tankSuppliesBox.transform;
		otherModelByName.transform.localPosition = new Vector3(0f, 0.7f, 0f);
		tankSuppliesBox.San = otherModelByName.transform;
	}

	public void TankSuppliesBox_AddTank(SkillManage.TankSuppliesBox_Type tank_type, Vector3 pos, int box_id, int tank_id, int num, int starlv)
	{
		AudioManage.inst.PlayAuidoBySelf_3D("Packageopen", null, false, 0uL);
		int build_id = 0;
		int tank_index = 0;
		int team_id = 0;
		switch (tank_type)
		{
		case SkillManage.TankSuppliesBox_Type.DuoGongNeng:
			tank_index = 1;
			num = 6;
			break;
		case SkillManage.TankSuppliesBox_Type.HuiXiong:
			tank_index = 2;
			num = 4;
			break;
		case SkillManage.TankSuppliesBox_Type.TianQi:
			tank_index = 3;
			break;
		case SkillManage.TankSuppliesBox_Type.HuoJian:
			tank_index = 4;
			break;
		case SkillManage.TankSuppliesBox_Type.GuangLing:
			tank_index = 5;
			break;
		case SkillManage.TankSuppliesBox_Type.DianCi:
			tank_index = 6;
			break;
		}
		SoldierUIITem soldierUIITem = new SoldierUIITem();
		FightPanelManager.inst.AddNewSupplyTankUIItem(tank_index, num);
		for (int i = 0; i < num; i++)
		{
			int level;
			if (!this.EnemySkillOpen)
			{
				level = HeroInfo.GetInstance().playerlevel;
			}
			else
			{
				level = 1;
			}
			base.StartCoroutine(this.CreateBoxTank(pos, build_id, tank_index, 1, team_id, level, i, starlv));
		}
	}

	[DebuggerHidden]
	private IEnumerator CreateBoxTank(Vector3 pos, int build_id, int tank_index, int num, int team_id, int level, int No, int box_lv)
	{
		SkillManage.<CreateBoxTank>c__Iterator5C <CreateBoxTank>c__Iterator5C = new SkillManage.<CreateBoxTank>c__Iterator5C();
		<CreateBoxTank>c__Iterator5C.No = No;
		<CreateBoxTank>c__Iterator5C.pos = pos;
		<CreateBoxTank>c__Iterator5C.box_lv = box_lv;
		<CreateBoxTank>c__Iterator5C.tank_index = tank_index;
		<CreateBoxTank>c__Iterator5C.<$>No = No;
		<CreateBoxTank>c__Iterator5C.<$>pos = pos;
		<CreateBoxTank>c__Iterator5C.<$>box_lv = box_lv;
		<CreateBoxTank>c__Iterator5C.<$>tank_index = tank_index;
		<CreateBoxTank>c__Iterator5C.<>f__this = this;
		return <CreateBoxTank>c__Iterator5C;
	}

	private void SetElectromagneticGun(Vector3 pos, int skillID)
	{
		AudioManage.inst.PlayAuidoBySelf_3D("Emshoot", null, false, 0uL);
		Body_Model body_Model = new Body_Model();
		body_Model = PoolManage.Ins.GetEffectByName("diancipao_guiji", null);
		body_Model.ga.AddComponent<ElectromagneticGun>();
		body_Model.GetComponent<ElectromagneticGun>().SetElectromagneticGun(pos, skillID, new Vector3(20f, 30f, 50f), 1.5f);
	}

	public void ElectromagneticGun_Boom(Vector3 pos, int skillID)
	{
		Body_Model body_Model = new Body_Model();
		body_Model = PoolManage.Ins.GetEffectByName("diancipao_mingzhong", null);
		body_Model.DesInsInPool(2f);
		body_Model.tr.position = pos;
		Transform[] componentsInChildren = body_Model.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			transform.gameObject.layer = 7;
		}
		base.StartCoroutine(this.ElectromagneticGun_Over(pos, skillID));
	}

	[DebuggerHidden]
	private IEnumerator ElectromagneticGun_Over(Vector3 pos, int skillID)
	{
		SkillManage.<ElectromagneticGun_Over>c__Iterator5D <ElectromagneticGun_Over>c__Iterator5D = new SkillManage.<ElectromagneticGun_Over>c__Iterator5D();
		<ElectromagneticGun_Over>c__Iterator5D.skillID = skillID;
		<ElectromagneticGun_Over>c__Iterator5D.pos = pos;
		<ElectromagneticGun_Over>c__Iterator5D.<$>skillID = skillID;
		<ElectromagneticGun_Over>c__Iterator5D.<$>pos = pos;
		<ElectromagneticGun_Over>c__Iterator5D.<>f__this = this;
		return <ElectromagneticGun_Over>c__Iterator5D;
	}

	[DebuggerHidden]
	private IEnumerator ElectromagneticGun_End(T_Tower t_tower, Body_Model effect1, Skill sk)
	{
		SkillManage.<ElectromagneticGun_End>c__Iterator5E <ElectromagneticGun_End>c__Iterator5E = new SkillManage.<ElectromagneticGun_End>c__Iterator5E();
		<ElectromagneticGun_End>c__Iterator5E.sk = sk;
		<ElectromagneticGun_End>c__Iterator5E.t_tower = t_tower;
		<ElectromagneticGun_End>c__Iterator5E.effect1 = effect1;
		<ElectromagneticGun_End>c__Iterator5E.<$>sk = sk;
		<ElectromagneticGun_End>c__Iterator5E.<$>t_tower = t_tower;
		<ElectromagneticGun_End>c__Iterator5E.<$>effect1 = effect1;
		return <ElectromagneticGun_End>c__Iterator5E;
	}

	public void SetDirectionalBlasting(Vector3 pos, int skillID, T_Commander FormCommander = null)
	{
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		List<T_Tower> list = new List<T_Tower>();
		Body_Model body_Model = new Body_Model();
		list = this.FindTower(pos, (float)skill.hurtRadius, 1, true, false);
		if (list.Count > 0)
		{
			body_Model = PoolManage.Ins.GetEffectByName("dingxiangbaopo_zhadan", list[0].tr);
			body_Model.tr.position = list[0].tr.position + new Vector3(0f, 4.5f, 0f);
			pos = list[0].tr.position + new Vector3(0f, 1.5f, 0f);
		}
		else
		{
			body_Model = PoolManage.Ins.GetEffectByName("dingxiangbaopo_zhadan", null);
			pos += new Vector3(0f, 1.5f, 0f);
			body_Model.tr.position = pos;
		}
		for (int i = 0; i < 5; i++)
		{
			base.StartCoroutine(this.DirectionalBlasting_Text((float)i, body_Model.ga));
		}
		AudioManage.inst.PlayAuidoBySelf_3D("Countdown", null, false, 0uL);
		base.StartCoroutine(this.DirectionalBlasting_Boom(pos, skill, 5f, body_Model.ga, FormCommander));
	}

	[DebuggerHidden]
	private IEnumerator DirectionalBlasting_Text(float time, GameObject effect)
	{
		SkillManage.<DirectionalBlasting_Text>c__Iterator5F <DirectionalBlasting_Text>c__Iterator5F = new SkillManage.<DirectionalBlasting_Text>c__Iterator5F();
		<DirectionalBlasting_Text>c__Iterator5F.time = time;
		<DirectionalBlasting_Text>c__Iterator5F.effect = effect;
		<DirectionalBlasting_Text>c__Iterator5F.<$>time = time;
		<DirectionalBlasting_Text>c__Iterator5F.<$>effect = effect;
		return <DirectionalBlasting_Text>c__Iterator5F;
	}

	[DebuggerHidden]
	private IEnumerator DirectionalBlasting_Boom(Vector3 pos, Skill sk, float time, GameObject effect, T_Commander FormCommander = null)
	{
		SkillManage.<DirectionalBlasting_Boom>c__Iterator60 <DirectionalBlasting_Boom>c__Iterator = new SkillManage.<DirectionalBlasting_Boom>c__Iterator60();
		<DirectionalBlasting_Boom>c__Iterator.time = time;
		<DirectionalBlasting_Boom>c__Iterator.pos = pos;
		<DirectionalBlasting_Boom>c__Iterator.sk = sk;
		<DirectionalBlasting_Boom>c__Iterator.FormCommander = FormCommander;
		<DirectionalBlasting_Boom>c__Iterator.effect = effect;
		<DirectionalBlasting_Boom>c__Iterator.<$>time = time;
		<DirectionalBlasting_Boom>c__Iterator.<$>pos = pos;
		<DirectionalBlasting_Boom>c__Iterator.<$>sk = sk;
		<DirectionalBlasting_Boom>c__Iterator.<$>FormCommander = FormCommander;
		<DirectionalBlasting_Boom>c__Iterator.<$>effect = effect;
		<DirectionalBlasting_Boom>c__Iterator.<>f__this = this;
		return <DirectionalBlasting_Boom>c__Iterator;
	}

	public List<T_Tower> FindTower(Vector3 pos, float radius, int num, bool buff, bool wall = false)
	{
		List<T_Tower> list = new List<T_Tower>();
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i] && Vector3.Distance(pos, SenceManager.inst.towers[i].tr.position) < radius)
			{
				if (SenceManager.inst.towers[i].secType == 20)
				{
					if (!buff)
					{
					}
					if (wall)
					{
						list.Add(SenceManager.inst.towers[i]);
					}
				}
				else
				{
					list.Add(SenceManager.inst.towers[i]);
				}
			}
		}
		if (num == 0)
		{
			return list;
		}
		List<T_Tower> list2 = new List<T_Tower>();
		for (int j = 0; j < num; j++)
		{
			float num2 = radius;
			int index = 0;
			for (int k = 0; k < list.Count; k++)
			{
				if (list[k] != null && Vector3.Distance(pos, list[k].tr.position) <= num2)
				{
					num2 = Vector3.Distance(pos, list[k].tr.position);
					index = k;
				}
			}
			if (list.Count > 0)
			{
				list2.Add(list[index]);
				list.Remove(list[index]);
			}
		}
		return list2;
	}

	public void Missile_Boom(float time, Vector3 pos, int radius, int power)
	{
		radius = 5;
		List<T_Tower> list = this.FindTower(pos, (float)radius, 1, true, false);
		this.HitToDefTank(pos, (float)radius, power);
		if (list.Count > 0)
		{
			list[0].DoHurt(power, -10L, true);
		}
	}

	public List<T_TankAbstract> FindTank(Vector3 pos, float radius, int num, bool buff)
	{
		List<T_TankAbstract> list = new List<T_TankAbstract>();
		List<T_TankAbstract> list2 = new List<T_TankAbstract>();
		if (buff)
		{
			list2 = SenceManager.inst.Tanks_Attack;
		}
		else
		{
			list2 = SenceManager.inst.Tanks_Defend;
		}
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i] && Vector3.Distance(pos, list2[i].tr.position) < radius)
			{
				list.Add(list2[i]);
			}
		}
		if (num == 0)
		{
			return list;
		}
		List<T_TankAbstract> list3 = new List<T_TankAbstract>();
		for (int j = 0; j < num; j++)
		{
			float num2 = radius;
			int index = 0;
			for (int k = 0; k < list.Count; k++)
			{
				if (list[k] != null && Vector3.Distance(pos, list[k].tr.position) <= num2)
				{
					num2 = Vector3.Distance(pos, list[k].tr.position);
					index = k;
				}
			}
			if (list.Count > 0)
			{
				list3.Add(list[index]);
				list.Remove(list[index]);
			}
		}
		return list3;
	}

	public void HitToDefTank(Vector3 pos, float radius, int power)
	{
		List<T_TankAbstract> list = this.FindTank(pos, radius, 20, false);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i])
			{
				FightPanelManager.inst.CreatFightMessage("-" + power, Color.red, list[i].tr);
				list[i].DoHurt(power);
			}
		}
	}

	public void AddToReadyUseSkill(FightPanel_SkillAndSoliderUIItem card, DieBall circleEffect)
	{
		if (this.useskillCard != null)
		{
			if (this.useskillCard.GetComponent<SkillUIITem>())
			{
				this.useskillCard.GetComponent<SkillUIITem>().ReadyToUse = false;
				this.useskillCard.tr.localPosition = new Vector3(this.useskillCard.tr.localPosition.x, 0f, this.useskillCard.tr.localPosition.z);
			}
			if (this.ReadyUseSkill_circleEffect)
			{
				UnityEngine.Object.Destroy(this.ReadyUseSkill_circleEffect.gameObject);
			}
		}
		this.useskillCard = card;
		this.ReadyUseSkill = true;
		this.ReadyUseSkill_circleEffect = circleEffect.ga;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f * (float)Screen.width, 0.5f * (float)Screen.height, 0f));
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 100f, LayerMask.GetMask(new string[]
		{
			"Ground"
		})))
		{
			this.ReadyUseSkill_circleEffect.transform.position = raycastHit.point;
		}
		this.use_time = 0.4f;
	}

	private void Hit_Use_Skill_Updata()
	{
		this.use_time -= Time.deltaTime;
		if (Camera_FingerManager.newbiLock)
		{
			return;
		}
		this.Use_Skill();
	}

	public void Use_Skill()
	{
		if (!this.ReadyUseSkill)
		{
			return;
		}
		CameraControl.inst.enabled = false;
		if (this.use_time > 0f)
		{
			return;
		}
		if (Input.GetMouseButton(0) || Camera_FingerManager.newbiLock)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 100f, LayerMask.GetMask(new string[]
			{
				"UI"
			})))
			{
				return;
			}
			if (Physics.Raycast(ray, out raycastHit, 100f, LayerMask.GetMask(new string[]
			{
				"Ground"
			})))
			{
				this.point = raycastHit.point;
			}
			if (this.ReadyUseSkill_circleEffect)
			{
				if (Camera_FingerManager.newbiLock)
				{
					this.ReadyUseSkill_circleEffect.transform.position = SenceManager.inst.MainBuilding.tr.position;
					this.ReadyUseSkill_Next = true;
				}
				else if (Vector2.Distance(new Vector2(this.ReadyUseSkill_circleEffect.transform.position.x, this.ReadyUseSkill_circleEffect.transform.position.z), new Vector2(this.point.x, this.point.z)) < 20f)
				{
					this.ReadyUseSkill_circleEffect.transform.position = this.point;
					this.ReadyUseSkill_Next = true;
				}
			}
		}
		if (this.ReadyUseSkill_Next && (!Input.GetMouseButton(0) || Camera_FingerManager.newbiLock) && this.useskillCard != null)
		{
			if (this.useskillCard.GetComponent<SkillUIITem>())
			{
				this.useskillCard.GetComponent<SkillUIITem>().True_UseCard(this.point);
				HUDTextTool.inst.NextLuaCall("放技能· · · ", new object[0]);
			}
			this.ReadyUseSkill = false;
			this.ReadyUseSkill_Next = false;
			this.useskillCard = null;
			UnityEngine.Object.Destroy(this.ReadyUseSkill_circleEffect.gameObject);
			CameraControl.inst.enabled = true;
			FightPanelManager.inst.CurSelectUIItem = null;
		}
		if (!this.ReadyUseSkill_circleEffect)
		{
			CameraControl.inst.enabled = true;
		}
	}
}
