using System;
using UnityEngine;

public class NumericalMgr
{
	public static int c1 = 117;

	public static int c2 = 20;

	public static float c3 = 1.5f;

	public static int commandHp;

	public static float sumHp;

	public static int S_AttackDamage(BaseFightInfo attacker, BaseFightInfo defender, bool towerToTank, out bool isCrit)
	{
		int num = attacker.breakArmor;
		int num2 = defender.defBreak;
		if (num == 0)
		{
			num = attacker.hitArmor;
			num2 = defender.defHit;
		}
		num2 -= attacker.avoiddef;
		if (num2 < 0)
		{
			num2 = 0;
		}
		int num3 = NumericalMgr.c1;
		if (towerToTank)
		{
			num3 = NumericalMgr.c2;
		}
		int num4;
		if (num2 == 0)
		{
			num4 = num;
		}
		else
		{
			num4 = Mathf.CeilToInt((float)num * (1f - (float)num2 * 1f / (float)(num2 + num3 * defender.lv)));
		}
		int num5 = UnityEngine.Random.Range(1, 10000);
		if (num5 < attacker.crit - defender.resist)
		{
			num4 = Mathf.FloorToInt((float)num4 * NumericalMgr.c3 + (float)attacker.critHR);
			isCrit = true;
		}
		else
		{
			isCrit = false;
		}
		if (num5 < attacker.trueChancePer)
		{
			num4 += attacker.trueDamage;
		}
		if (num5 < defender.reChancePer)
		{
			num4 -= defender.reDamage;
		}
		if (num4 < 0)
		{
			num4 = 1;
		}
		return num4;
	}

	public static int MainBuildingDamage(int hp)
	{
		int result = Mathf.RoundToInt(0.7f * (float)NumericalMgr.commandHp * ((float)hp / NumericalMgr.sumHp));
		if (SenceManager.inst.MainBuilding.CurLife > (float)NumericalMgr.commandHp * 0.3f)
		{
			return result;
		}
		return 0;
	}
}
