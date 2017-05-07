using System;
using UnityEngine;

public class T_NuclearBomb : IMonoBehaviour
{
	public enum Boomer_Kind
	{
		Hedan,
		Boomer
	}

	public bool Boomer;

	private bool isFirst = true;

	public Vector3 MB_Pos;

	public float Toward_y;

	private float speed_X;

	private float speed_Z;

	private float DownSpeed = -5f;

	public int skillID;

	public Body_Model body;

	private Body_Model effect;

	public void Start()
	{
	}

	public void Update()
	{
		this.tr.eulerAngles = new Vector3(0f, this.Toward_y, 0f);
		this.DownSpeed -= 20f * Time.deltaTime;
		if (this.Boomer)
		{
			this.tr.Translate(0f, this.DownSpeed * Time.deltaTime, 0f);
		}
		if (this.tr.position.y < -200f)
		{
			GameTools.RemoveComponent<Rigidbody>(this.ga);
			if (this.effect)
			{
				this.effect.DesInsInPool();
			}
			if (this.body)
			{
				this.body.DesInsInPool();
			}
		}
		if (this.tr.position.y < 0.3f && this.isFirst)
		{
			this.isFirst = false;
			this.OOnTrigger_Enter(null);
		}
	}

	public void Init(T_NuclearBomb.Boomer_Kind boomer_kind, float toward_y)
	{
		this.isFirst = true;
		this.DownSpeed = -0f - (float)UnityEngine.Random.Range(0, 15);
		this.Toward_y = toward_y;
		this.speed_X = Mathf.Abs(this.tr.position.x - this.MB_Pos.x) * 1f;
		this.speed_Z = Mathf.Abs(this.tr.position.z - this.MB_Pos.z) * 1f;
	}

	private void OOnTrigger_Enter(Collider other)
	{
		NewSkillType skillType = (NewSkillType)UnitConst.GetInstance().skillList[this.skillID].skillType;
		switch (skillType)
		{
		case NewSkillType.Sanbing:
			return;
		case NewSkillType.Jingzhunpaoji:
			return;
		case (NewSkillType)3:
			IL_3B:
			if (skillType != NewSkillType.None)
			{
				return;
			}
			return;
		case NewSkillType.Hedan:
			AudioManage.inst.audioPlay.Stop();
			AudioManage.inst.PlayAuido("skill4_warning", false);
			PoolManage.Ins.CreatEffect(UnitConst.GetInstance().skillList[this.skillID].bodyEffect, new Vector3(this.tr.position.x, 0f, this.tr.position.z), Quaternion.identity, null);
			HUDTextTool.inst.hedanBoom = this.tr.position;
			SkillManage.inst.CreateNuclearBomb(this.tr, this.skillID);
			GameTools.RemoveComponent<Rigidbody>(this.ga);
			this.body.DesInsInPool(0f);
			return;
		case NewSkillType.Tiemu:
			return;
		case NewSkillType.Shandianfengbao:
			return;
		}
		goto IL_3B;
	}
}
