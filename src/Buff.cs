using System;

public class Buff
{
	public enum BuffType
	{
		None = 1,
		MoveSpeedAdd,
		Shield = 16,
		AttackAdd = 8,
		DefAdd = 4,
		SmokeBomb = 32,
		Halo = 64,
		ContinueHurt = 128,
		TeamStarUP = 256,
		FireSpeedAdd = 512,
		Life = 1024
	}

	public enum TargetType
	{
		Other,
		Self,
		None
	}

	public enum PowerType
	{
		None = -1,
		Percent,
		Absolute
	}

	public int id;

	public string name;

	public string desc;

	public Buff.BuffType buffType;

	public int buffLevel;

	public Buff.TargetType target;

	public Buff.PowerType powerType;

	public int power;

	public float lifeTime;

	public string effect;
}
