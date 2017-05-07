using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class BuffEffect : MonoBehaviour
{
	private float buffTime;

	public T_TankAbstract myTank;

	public T_Tower myTower;

	public BuffRuntime buffRuntime;

	public int buffIndex;

	public int buffSkillID;

	public Character BuffSender;

	private float buffSpeedAddition;

	private void Start()
	{
		base.Invoke("RemoveBuff", UnitConst.GetInstance().BuffConst[this.buffIndex].lifeTime);
	}

	public void ApplyBuff()
	{
		Buff.BuffType buffType = UnitConst.GetInstance().BuffConst[this.buffIndex].buffType;
		Buff.BuffType buffType2 = buffType;
		switch (buffType2)
		{
		case Buff.BuffType.MoveSpeedAdd:
			if (this.myTank)
			{
				this.myTank.MyBuffRuntime.RemoveBuff(UnitConst.GetInstance().BuffConst[this.buffIndex].buffType);
				Buff.PowerType powerType = UnitConst.GetInstance().BuffConst[this.buffIndex].powerType;
				if (powerType != Buff.PowerType.Percent)
				{
					if (powerType == Buff.PowerType.Absolute)
					{
						T_TankAbstract expr_F3_cp_0 = this.myTank;
						expr_F3_cp_0.CharacterBaseFightInfo.moveSpeed = expr_F3_cp_0.CharacterBaseFightInfo.moveSpeed + (float)UnitConst.GetInstance().BuffConst[this.buffIndex].power;
					}
				}
				else
				{
					T_TankAbstract expr_12A_cp_0 = this.myTank;
					expr_12A_cp_0.CharacterBaseFightInfo.moveSpeed = expr_12A_cp_0.CharacterBaseFightInfo.moveSpeed + this.myTank.Movespeed * (float)UnitConst.GetInstance().BuffConst[this.buffIndex].power * 0.01f;
				}
				this.myTank.Movespeed = this.myTank.CharacterBaseFightInfo.moveSpeed;
			}
			goto IL_6A5;
		case (Buff.BuffType)3:
			IL_31:
			if (buffType2 == Buff.BuffType.AttackAdd)
			{
				if (this.myTank)
				{
					this.myTank.MyBuffRuntime.RemoveBuff(UnitConst.GetInstance().BuffConst[this.buffIndex].buffType);
					Buff.PowerType powerType = UnitConst.GetInstance().BuffConst[this.buffIndex].powerType;
					if (powerType != Buff.PowerType.Percent)
					{
						if (powerType == Buff.PowerType.Absolute)
						{
							T_TankAbstract expr_358_cp_0 = this.myTank;
							expr_358_cp_0.CharacterBaseFightInfo.breakArmor = expr_358_cp_0.CharacterBaseFightInfo.breakArmor + UnitConst.GetInstance().BuffConst[this.buffIndex].power;
						}
					}
					else
					{
						T_TankAbstract expr_38E_cp_0 = this.myTank;
						expr_38E_cp_0.CharacterBaseFightInfo.breakArmor = expr_38E_cp_0.CharacterBaseFightInfo.breakArmor + (int)((float)(this.myTank.CharacterBaseFightInfo.breakArmor * UnitConst.GetInstance().BuffConst[this.buffIndex].power) * 0.01f);
					}
				}
				goto IL_6A5;
			}
			if (buffType2 == Buff.BuffType.Shield)
			{
				if (this.myTank)
				{
					Buff.PowerType powerType = UnitConst.GetInstance().BuffConst[this.buffIndex].powerType;
					if (powerType == Buff.PowerType.Absolute)
					{
						this.myTank.MyBuffRuntime.RemoveBuff(UnitConst.GetInstance().BuffConst[this.buffIndex].buffType);
						T_TankAbstract expr_1F9_cp_0 = this.myTank;
						expr_1F9_cp_0.CharacterBaseFightInfo.Shield = expr_1F9_cp_0.CharacterBaseFightInfo.Shield + UnitConst.GetInstance().BuffConst[this.buffIndex].power;
						if (this.myTank.m_shieldInfo == null)
						{
							this.myTank.m_shieldInfo = InfoPanel.inst.CreatShieldInfo();
							InfoPanel.inst.lifeInfos.Add(this.myTank.m_shieldInfo);
						}
						this.myTank.m_shieldInfo.ShowShieldLife(this.myTank, null);
						if (this.myTank.m_lifeInfo == null)
						{
							this.myTank.m_lifeInfo = InfoPanel.inst.CreatInfo();
							InfoPanel.inst.lifeInfos.Add(this.myTank.m_lifeInfo);
						}
						this.myTank.m_lifeInfo.ShowTankLife(this.myTank, null);
					}
				}
				goto IL_6A5;
			}
			if (buffType2 == Buff.BuffType.SmokeBomb)
			{
				goto IL_6A5;
			}
			if (buffType2 == Buff.BuffType.Halo)
			{
				if (this.myTank)
				{
					this.myTank.IsPause = true;
				}
				goto IL_6A5;
			}
			if (buffType2 == Buff.BuffType.ContinueHurt)
			{
				base.StartCoroutine(this.ContinueHurt());
				goto IL_6A5;
			}
			if (buffType2 == Buff.BuffType.TeamStarUP)
			{
				UnitConst.GetInstance().BuffConst[this.buffIndex].power *= 1;
				if (this.myTank)
				{
					this.myTank.SetStar(UnitConst.GetInstance().BuffConst[this.buffIndex].buffLevel, base.transform, 0);
					Buff.PowerType powerType = UnitConst.GetInstance().BuffConst[this.buffIndex].powerType;
					if (powerType == Buff.PowerType.Percent)
					{
						T_TankAbstract expr_599_cp_0 = this.myTank;
						expr_599_cp_0.CharacterBaseFightInfo.life = expr_599_cp_0.CharacterBaseFightInfo.life + (int)((float)(this.myTank.CharacterBaseFightInfo.life * UnitConst.GetInstance().BuffConst[this.buffIndex].power) * 0.01f);
						T_TankAbstract expr_5E3_cp_0 = this.myTank;
						expr_5E3_cp_0.CharacterBaseFightInfo.breakArmor = expr_5E3_cp_0.CharacterBaseFightInfo.breakArmor + (int)((float)(this.myTank.CharacterBaseFightInfo.breakArmor * UnitConst.GetInstance().BuffConst[this.buffIndex].power) * 0.01f);
						T_TankAbstract expr_62D_cp_0 = this.myTank;
						expr_62D_cp_0.CharacterBaseFightInfo.defBreak = expr_62D_cp_0.CharacterBaseFightInfo.defBreak + (int)((float)(this.myTank.CharacterBaseFightInfo.defBreak * UnitConst.GetInstance().BuffConst[this.buffIndex].power) * 0.01f);
						this.myTank.MaxLifeChange(this.myTank.CharacterBaseFightInfo.life);
					}
				}
				goto IL_6A5;
			}
			if (buffType2 == Buff.BuffType.FireSpeedAdd)
			{
				goto IL_6A5;
			}
			if (buffType2 != Buff.BuffType.Life)
			{
				goto IL_6A5;
			}
			goto IL_6A5;
		case Buff.BuffType.DefAdd:
			if (this.myTank)
			{
				this.myTank.MyBuffRuntime.RemoveBuff(UnitConst.GetInstance().BuffConst[this.buffIndex].buffType);
				Buff.PowerType powerType = UnitConst.GetInstance().BuffConst[this.buffIndex].powerType;
				if (powerType != Buff.PowerType.Percent)
				{
					if (powerType == Buff.PowerType.Absolute)
					{
						T_TankAbstract expr_44E_cp_0 = this.myTank;
						expr_44E_cp_0.CharacterBaseFightInfo.defBreak = expr_44E_cp_0.CharacterBaseFightInfo.defBreak + UnitConst.GetInstance().BuffConst[this.buffIndex].power;
					}
				}
				else
				{
					T_TankAbstract expr_484_cp_0 = this.myTank;
					expr_484_cp_0.CharacterBaseFightInfo.defBreak = expr_484_cp_0.CharacterBaseFightInfo.defBreak + (int)((float)(this.myTank.CharacterBaseFightInfo.defBreak * UnitConst.GetInstance().BuffConst[this.buffIndex].power) * 0.01f);
				}
			}
			goto IL_6A5;
		}
		goto IL_31;
		IL_6A5:
		base.StartCoroutine(this.Buff_TimeOver(this.myTank, null, this.buffIndex));
	}

	[DebuggerHidden]
	public IEnumerator ContinueHurt()
	{
		BuffEffect.<ContinueHurt>c__Iterator29 <ContinueHurt>c__Iterator = new BuffEffect.<ContinueHurt>c__Iterator29();
		<ContinueHurt>c__Iterator.<>f__this = this;
		return <ContinueHurt>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator Buff_TimeOver(T_TankAbstract tank, T_Tower tower, int buffIndex)
	{
		BuffEffect.<Buff_TimeOver>c__Iterator2A <Buff_TimeOver>c__Iterator2A = new BuffEffect.<Buff_TimeOver>c__Iterator2A();
		<Buff_TimeOver>c__Iterator2A.buffIndex = buffIndex;
		<Buff_TimeOver>c__Iterator2A.tank = tank;
		<Buff_TimeOver>c__Iterator2A.<$>buffIndex = buffIndex;
		<Buff_TimeOver>c__Iterator2A.<$>tank = tank;
		<Buff_TimeOver>c__Iterator2A.<>f__this = this;
		return <Buff_TimeOver>c__Iterator2A;
	}

	public void OnDisable()
	{
		Buff.BuffType buffType = UnitConst.GetInstance().BuffConst[this.buffIndex].buffType;
		Buff.BuffType buffType2 = buffType;
		if (buffType2 != Buff.BuffType.SmokeBomb)
		{
			if (buffType2 != Buff.BuffType.Halo)
			{
				if (buffType2 != Buff.BuffType.ContinueHurt)
				{
					if (buffType2 != Buff.BuffType.TeamStarUP)
					{
						if (buffType2 != Buff.BuffType.Life)
						{
						}
					}
				}
			}
			else if (this.myTank)
			{
				this.myTank.IsPause = false;
			}
		}
		else if (this.myTank)
		{
			this.myTank.MyBuffRuntime.Stealth(null, false);
		}
		else if (this.myTower)
		{
		}
	}

	private void RemoveBuff()
	{
	}

	public void Destory()
	{
		if (base.gameObject != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
