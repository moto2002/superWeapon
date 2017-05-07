using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SkillBuffManage : MonoBehaviour
{
	public static SkillBuffManage inst;

	public int smoke_no;

	private int cs_a;

	private float cs_time;

	public void OnDestroy()
	{
		SkillBuffManage.inst = null;
	}

	private void Awake()
	{
		SkillBuffManage.inst = this;
	}

	public void Repair_Place(int skillID, Vector3 pos)
	{
		Body_Model body_Model = new Body_Model();
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		if (skill.renju > 1)
		{
			body_Model = PoolManage.Ins.GetEffectByName("zhandiweixiu", null);
			body_Model.tr.position = pos;
			AudioManage.inst.PlayAuido("relifezone", false);
		}
		else
		{
			AudioManage.inst.PlayAuido("relifezoneB", false);
		}
		for (int i = 0; i < skill.renju; i++)
		{
			if (!SkillManage.inst.EnemySkillOpen)
			{
				base.StartCoroutine(this.Repair_Once(body_Model.ga, i, skill.renju - 1, float.Parse(skill.renjuCd), pos, (float)skill.attarkRadius, (int)((float)UnitConst.GetInstance().skillList[skillID].basePower * (1f + (float)HeroInfo.GetInstance().SkillInfo[UnitConst.GetInstance().skillList[skillID].skillType] * 0.1f)), 0));
			}
			else
			{
				base.StartCoroutine(this.Repair_Once(body_Model.ga, i, skill.renju - 1, float.Parse(skill.renjuCd), pos, (float)skill.attarkRadius, (int)((float)UnitConst.GetInstance().skillList[skillID].basePower * (1f + (float)SkillManage.inst.EnemySkillList[skillID] * 0.1f)), 0));
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator Repair_Once(GameObject place_model, int No, int No_Max, float renju_cd, Vector3 pos, float radius, int power, int no)
	{
		SkillBuffManage.<Repair_Once>c__Iterator4E <Repair_Once>c__Iterator4E = new SkillBuffManage.<Repair_Once>c__Iterator4E();
		<Repair_Once>c__Iterator4E.renju_cd = renju_cd;
		<Repair_Once>c__Iterator4E.No = No;
		<Repair_Once>c__Iterator4E.pos = pos;
		<Repair_Once>c__Iterator4E.radius = radius;
		<Repair_Once>c__Iterator4E.no = no;
		<Repair_Once>c__Iterator4E.No_Max = No_Max;
		<Repair_Once>c__Iterator4E.power = power;
		<Repair_Once>c__Iterator4E.place_model = place_model;
		<Repair_Once>c__Iterator4E.<$>renju_cd = renju_cd;
		<Repair_Once>c__Iterator4E.<$>No = No;
		<Repair_Once>c__Iterator4E.<$>pos = pos;
		<Repair_Once>c__Iterator4E.<$>radius = radius;
		<Repair_Once>c__Iterator4E.<$>no = no;
		<Repair_Once>c__Iterator4E.<$>No_Max = No_Max;
		<Repair_Once>c__Iterator4E.<$>power = power;
		<Repair_Once>c__Iterator4E.<$>place_model = place_model;
		return <Repair_Once>c__Iterator4E;
	}

	public void CreateBuffPlace(int skillID, Vector3 pos)
	{
		Body_Model body_Model = new Body_Model();
		string buffName = string.Empty;
		NewSkillType skillType = (NewSkillType)UnitConst.GetInstance().skillList[skillID].skillType;
		switch (skillType)
		{
		case NewSkillType.Charge:
			buffName = "攻速UP";
			AudioManage.inst.PlayAuido("speedup", false);
			goto IL_149;
		case NewSkillType.Blitzkrieg:
			buffName = "攻击UP\n速度UP";
			AudioManage.inst.PlayAuido("speedup", false);
			goto IL_149;
		case NewSkillType.BlackholeArmor:
			buffName = "护盾UP";
			AudioManage.inst.PlayAuido("armorget", false);
			goto IL_149;
		case NewSkillType.SmokeShell:
			buffName = string.Empty;
			this.smoke_no++;
			body_Model = PoolManage.Ins.GetModelByBundleByName("yanwudan", null);
			body_Model.tr.position = pos;
			AudioManage.inst.PlayAuido("Smokebomb", false);
			goto IL_149;
		case NewSkillType.DirectionalBlasting:
		case NewSkillType.ElectromagneticGun:
		case NewSkillType.RemoteNavalGun:
			IL_4D:
			if (skillType != NewSkillType.Tiemu)
			{
				goto IL_149;
			}
			buffName = "防御UP";
			body_Model = PoolManage.Ins.GetModelByBundleByName("tiemu", null);
			body_Model.tr.position = pos;
			AudioManage.inst.PlayAuido("ironcurtain", false);
			goto IL_149;
		case NewSkillType.Samson:
			AudioManage.inst.PlayAuido("speeddown", false);
			buffName = "防御Down\n攻速Down";
			goto IL_149;
		}
		goto IL_4D;
		IL_149:
		Skill skill = UnitConst.GetInstance().skillList[skillID];
		int[] array = new int[skill.buffId.Length];
		for (int i = 0; i < skill.buffId.Length; i++)
		{
			array[i] = skill.buffId[i];
		}
		if (skill.renju > 0)
		{
			if (UnitConst.GetInstance().skillList[skillID].skillType == 16)
			{
				base.StartCoroutine(this.SetBuildBuff_Once(skillID, buffName, body_Model.ga, 0, 0, float.Parse(skill.renjuCd), pos, (float)skill.attarkRadius, array, 0, body_Model.ga));
				int num;
				if (!SkillManage.inst.EnemySkillOpen)
				{
					num = (int)((float)skill.renju * (1f + (float)HeroInfo.GetInstance().SkillInfo[UnitConst.GetInstance().skillList[skillID].skillType] * 0.1f));
				}
				else
				{
					num = (int)((float)skill.renju * (1f + (float)SkillManage.inst.EnemySkillList[skillID] * 0.1f));
				}
				for (int j = 0; j < num; j++)
				{
					base.StartCoroutine(this.SetBuff_Once(skillID, buffName, body_Model.ga, j, num - 1, float.Parse(skill.renjuCd), pos, (float)skill.attarkRadius, array, 0));
				}
			}
			else
			{
				for (int k = 0; k < skill.renju; k++)
				{
					base.StartCoroutine(this.SetBuff_Once(skillID, buffName, body_Model.ga, k, skill.renju - 1, float.Parse(skill.renjuCd), pos, (float)skill.attarkRadius, array, 0));
				}
			}
		}
		else if (UnitConst.GetInstance().skillList[skillID].skillType == 20)
		{
			base.StartCoroutine(this.SetBuildBuff_Once(skillID, buffName, body_Model.ga, 0, 0, float.Parse(skill.renjuCd), pos, (float)skill.attarkRadius, array, 0, body_Model.ga));
		}
		else
		{
			base.StartCoroutine(this.SetBuff_Once(skillID, buffName, body_Model.ga, 0, 0, float.Parse(skill.renjuCd), pos, (float)skill.attarkRadius, array, 0));
		}
	}

	[DebuggerHidden]
	private IEnumerator SetBuff_Once(int skillID, string BuffName, GameObject buffplace_model, int No, int No_Max, float renju_cd, Vector3 pos, float radius, int[] BuffID, int no)
	{
		SkillBuffManage.<SetBuff_Once>c__Iterator4F <SetBuff_Once>c__Iterator4F = new SkillBuffManage.<SetBuff_Once>c__Iterator4F();
		<SetBuff_Once>c__Iterator4F.renju_cd = renju_cd;
		<SetBuff_Once>c__Iterator4F.No = No;
		<SetBuff_Once>c__Iterator4F.pos = pos;
		<SetBuff_Once>c__Iterator4F.radius = radius;
		<SetBuff_Once>c__Iterator4F.no = no;
		<SetBuff_Once>c__Iterator4F.skillID = skillID;
		<SetBuff_Once>c__Iterator4F.buffplace_model = buffplace_model;
		<SetBuff_Once>c__Iterator4F.BuffID = BuffID;
		<SetBuff_Once>c__Iterator4F.BuffName = BuffName;
		<SetBuff_Once>c__Iterator4F.No_Max = No_Max;
		<SetBuff_Once>c__Iterator4F.<$>renju_cd = renju_cd;
		<SetBuff_Once>c__Iterator4F.<$>No = No;
		<SetBuff_Once>c__Iterator4F.<$>pos = pos;
		<SetBuff_Once>c__Iterator4F.<$>radius = radius;
		<SetBuff_Once>c__Iterator4F.<$>no = no;
		<SetBuff_Once>c__Iterator4F.<$>skillID = skillID;
		<SetBuff_Once>c__Iterator4F.<$>buffplace_model = buffplace_model;
		<SetBuff_Once>c__Iterator4F.<$>BuffID = BuffID;
		<SetBuff_Once>c__Iterator4F.<$>BuffName = BuffName;
		<SetBuff_Once>c__Iterator4F.<$>No_Max = No_Max;
		<SetBuff_Once>c__Iterator4F.<>f__this = this;
		return <SetBuff_Once>c__Iterator4F;
	}

	[DebuggerHidden]
	private IEnumerator TieMu_Add(T_TankAbstract t_tank, int skillID)
	{
		SkillBuffManage.<TieMu_Add>c__Iterator50 <TieMu_Add>c__Iterator = new SkillBuffManage.<TieMu_Add>c__Iterator50();
		<TieMu_Add>c__Iterator.skillID = skillID;
		<TieMu_Add>c__Iterator.<$>skillID = skillID;
		return <TieMu_Add>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator SmokeShell_Add(int No, T_TankAbstract t_tank, int skillID, GameObject Yan)
	{
		return new SkillBuffManage.<SmokeShell_Add>c__Iterator51();
	}

	[DebuggerHidden]
	private IEnumerator SetBuildBuff_Once(int skillID, string BuffName, GameObject buffplace_model, int No, int No_Max, float renju_cd, Vector3 pos, float radius, int[] BuffID, int no, GameObject Yan)
	{
		SkillBuffManage.<SetBuildBuff_Once>c__Iterator52 <SetBuildBuff_Once>c__Iterator = new SkillBuffManage.<SetBuildBuff_Once>c__Iterator52();
		<SetBuildBuff_Once>c__Iterator.renju_cd = renju_cd;
		<SetBuildBuff_Once>c__Iterator.No = No;
		<SetBuildBuff_Once>c__Iterator.pos = pos;
		<SetBuildBuff_Once>c__Iterator.radius = radius;
		<SetBuildBuff_Once>c__Iterator.no = no;
		<SetBuildBuff_Once>c__Iterator.<$>renju_cd = renju_cd;
		<SetBuildBuff_Once>c__Iterator.<$>No = No;
		<SetBuildBuff_Once>c__Iterator.<$>pos = pos;
		<SetBuildBuff_Once>c__Iterator.<$>radius = radius;
		<SetBuildBuff_Once>c__Iterator.<$>no = no;
		return <SetBuildBuff_Once>c__Iterator;
	}

	[DebuggerHidden]
	public IEnumerator DianCi_Over(float time, T_Tank tank)
	{
		SkillBuffManage.<DianCi_Over>c__Iterator53 <DianCi_Over>c__Iterator = new SkillBuffManage.<DianCi_Over>c__Iterator53();
		<DianCi_Over>c__Iterator.time = time;
		<DianCi_Over>c__Iterator.<$>time = time;
		return <DianCi_Over>c__Iterator;
	}

	private void AUpdate()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			this.cs_a = 1;
		}
		if (Input.GetKeyDown(KeyCode.F10) && this.cs_a == 1)
		{
			this.cs_a = 0;
			base.StartCoroutine(this.GetAllSkill(1));
		}
		if (Input.GetKeyDown(KeyCode.F11) && this.cs_a == 1)
		{
			this.cs_a = 0;
			base.StartCoroutine(this.GetAllSkill(2));
		}
		if (Input.GetKeyDown(KeyCode.F12) && this.cs_a == 1)
		{
			this.cs_a = 0;
			base.StartCoroutine(this.GetAllSkill(3));
		}
		if (Input.GetKeyDown(KeyCode.F8))
		{
			ChatHandler.CG_Chat("/cmd addmoney 1000000", 0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			for (int i = 0; i < SenceManager.inst.towers.Count; i++)
			{
				if (SenceManager.inst.towers[i].index == 23)
				{
					HUDTextTool.inst.SetText("-" + 99999999, SenceManager.inst.towers[i].tr, Color.red, 0.8f, 40);
					SenceManager.inst.towers[i].DoHurt(99999999, -10L, true);
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			for (int j = 0; j < SenceManager.inst.towers.Count; j++)
			{
				if (SenceManager.inst.towers[j].index == 1)
				{
					HUDTextTool.inst.SetText("-" + 99999999, SenceManager.inst.towers[j].tr, Color.red, 0.8f, 40);
					SenceManager.inst.towers[j].DoHurt(99999999, -10L, true);
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			for (int k = 0; k < SenceManager.inst.towers.Count; k++)
			{
				if (SenceManager.inst.towers[k].index == 16 || SenceManager.inst.towers[k].index == 17 || SenceManager.inst.towers[k].index == 18 || SenceManager.inst.towers[k].index == 19 || SenceManager.inst.towers[k].index == 22)
				{
					HUDTextTool.inst.SetText("-" + 99999999, SenceManager.inst.towers[k].tr, Color.red, 0.8f, 40);
					SenceManager.inst.towers[k].DoHurt(99999999, -10L, true);
				}
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator GetAllSkill(int a)
	{
		SkillBuffManage.<GetAllSkill>c__Iterator54 <GetAllSkill>c__Iterator = new SkillBuffManage.<GetAllSkill>c__Iterator54();
		<GetAllSkill>c__Iterator.a = a;
		<GetAllSkill>c__Iterator.<$>a = a;
		return <GetAllSkill>c__Iterator;
	}
}
