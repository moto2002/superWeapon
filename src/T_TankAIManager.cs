using System;
using System.Collections.Generic;
using UnityEngine;

public class T_TankAIManager : MonoBehaviour
{
	public static T_TankAIManager inst;

	public bool On_Off;

	private float flag_time;

	private float flag_no;

	private float flag_speed;

	public Vector3 Flag_Pos;

	public float jd;

	public Dictionary<int, int> TankTeamTeam = new Dictionary<int, int>();

	public Dictionary<int, T_Tank> TankHeaderList = new Dictionary<int, T_Tank>();

	public List<Vector3> AttPointUsedList = new List<Vector3>();

	public void OnDestroy()
	{
		T_TankAIManager.inst = null;
	}

	private void Awake()
	{
		this.On_Off = false;
		T_TankAIManager.inst = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
		this.jd += 100f * Time.deltaTime;
		if (this.jd >= 360f)
		{
			this.jd = 0f;
		}
		this.flag_time += Time.deltaTime;
		if (this.flag_time >= 0.5f)
		{
			this.flag_time = 0f;
			this.flag_no += 1f;
		}
		this.flag_speed = 3f;
		if (this.flag_no % 2f == 0f)
		{
			this.Flag_Pos = new Vector3(0f, Mathf.Max(0f, this.Flag_Pos.y - this.Flag_Pos.y * this.flag_speed * Time.deltaTime), 0f);
		}
		else if (this.flag_no % 1f == 0f)
		{
			this.Flag_Pos = new Vector3(0f, Mathf.Min(0.8f, this.Flag_Pos.y + (0.8f - this.Flag_Pos.y) * this.flag_speed * Time.deltaTime), 0f);
		}
	}

	public void AddScript(T_Tank tank)
	{
		tank.ga.AddComponent<T_TankAI>();
		tank.this_TankAI = tank.GetComponent<T_TankAI>();
		tank.this_TankAI.tank_index = tank.index;
		if (tank.this_commander != null)
		{
			tank.this_TankAI.tank_index = 1000 + tank.this_commander.index;
		}
		this.ReSetHeader();
		tank.this_TankAI.Init();
	}

	public void ReSetHeader()
	{
	}

	public void SetAIOrder(Vector3 _pos, Vector3 own_pos, bool IsMoveNoFire, Transform MB_tr = null, int TeamID = 0)
	{
		if (GameSetting.autoFight)
		{
			AutoFightBtn._inst.SetAutoFight();
			return;
		}
	}

	public Vector3 GetTeamerPos(Vector3 HeaderPos, T_Tank tank)
	{
		Vector3 vector = HeaderPos;
		float num = 1.8f;
		switch (this.TankTeamTeam[tank.this_TankAI.AI_TeamIndex])
		{
		case 1:
			vector = HeaderPos;
			break;
		case 2:
		{
			int num2 = tank.this_TankAI.AI_Teamindex - 1;
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					vector += new Vector3(num, 0f, 0f);
				}
			}
			else
			{
				vector = HeaderPos;
			}
			break;
		}
		case 3:
			switch (tank.this_TankAI.AI_Teamindex)
			{
			case 1:
				vector = HeaderPos;
				break;
			case 2:
				vector += new Vector3(0.8f * num, 0f, 0.8f * num);
				break;
			case 3:
				vector += new Vector3(0.8f * num, 0f, -0.8f * num);
				break;
			}
			break;
		case 4:
			switch (tank.this_TankAI.AI_Teamindex)
			{
			case 1:
				vector = HeaderPos;
				break;
			case 2:
				vector += new Vector3(-1f * num, 0f, 0f);
				break;
			case 3:
				vector += new Vector3(0.5f * num, 0f, 0.8f * num);
				break;
			case 4:
				vector += new Vector3(0.5f * num, 0f, -0.8f * num);
				break;
			}
			break;
		case 5:
			switch (tank.this_TankAI.AI_Teamindex)
			{
			case 1:
				vector = HeaderPos;
				break;
			case 2:
				vector += new Vector3(-0.5f * num, 0f, 0.62f * num);
				break;
			case 3:
				vector += new Vector3(-0.5f * num, 0f, -0.62f * num);
				break;
			case 4:
				vector += new Vector3(0.5f * num, 0f, -0.62f * num);
				break;
			case 5:
				vector += new Vector3(0.5f * num, 0f, 0.62f * num);
				break;
			}
			break;
		case 6:
			switch (tank.this_TankAI.AI_Teamindex)
			{
			case 1:
				vector = HeaderPos;
				break;
			case 2:
				vector += new Vector3(-0.4f * num, 0f, -0.8f * num);
				break;
			case 3:
				vector += new Vector3(-num, 0f, 0f);
				break;
			case 4:
				vector += new Vector3(-0.4f * num, 0f, 0.8f * num);
				break;
			case 5:
				vector += new Vector3(0.7f * num, 0f, 0.5f * num);
				break;
			case 6:
				vector += new Vector3(0.7f * num, 0f, -0.5f * num);
				break;
			}
			break;
		}
		return vector;
	}

	public Vector3 CheckAttPoint(Vector3 apply_pos, T_TankAbstract apply_tank, Transform MB_tr)
	{
		Vector3 zero = Vector3.zero;
		bool flag = false;
		if (!this.VerifyMapGrid(apply_pos) && !UnitConst.GetInstance().soldierConst[apply_tank.index].isCanFly)
		{
			flag = true;
		}
		for (int i = 0; i < this.AttPointUsedList.Count; i++)
		{
			float num = Vector2.Distance(new Vector2(this.AttPointUsedList[i].x, this.AttPointUsedList[i].z), new Vector2(apply_pos.x, apply_pos.z));
			float num2 = 3f;
			if (UnitConst.GetInstance().soldierConst[apply_tank.index].isCanFly)
			{
				num2 = 6f;
			}
			if (num < num2)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			this.AttPointUsedList.Add(apply_pos);
			return apply_pos;
		}
		int j = (int)apply_tank.CharacterBaseFightInfo.ShootMaxRadius;
		GameObject gameObject = new GameObject(" AttPoint_Parent");
		gameObject.transform.parent = MB_tr;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.transform.LookAt(apply_pos);
		GameObject gameObject2 = new GameObject(" AttPoint");
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localPosition = new Vector3(0f, 0f, (float)j);
		if (UnitConst.GetInstance().soldierConst[apply_tank.index].isCanFly)
		{
			gameObject2.transform.localPosition = new Vector3(0f, 0f, (float)(j - 2));
		}
		int num3 = (int)gameObject.transform.eulerAngles.y;
		float num4 = 1f;
		for (int k = 3; k > 0; k--)
		{
			for (j = (int)apply_tank.CharacterBaseFightInfo.ShootMaxRadius; j > 0; j--)
			{
				gameObject2.transform.localPosition = new Vector3(0f, 0f, (float)j);
				if (UnitConst.GetInstance().soldierConst[apply_tank.index].isCanFly)
				{
					gameObject2.transform.localPosition = new Vector3(0f, 0f, (float)Mathf.Max(0, j - 2));
				}
				for (int l = 0; l < 360; l++)
				{
					gameObject.transform.eulerAngles = new Vector3(0f, (float)num3 + ((l % 2 != 0) ? (-num4 * ((float)l / 2f + 1f)) : (num4 * ((float)l / 2f + 1f))), 0f);
					Vector3 position = gameObject2.transform.position;
					bool flag2 = false;
					if (!this.VerifyMapGrid(position))
					{
						flag2 = true;
					}
					else
					{
						for (int m = 0; m < this.AttPointUsedList.Count; m++)
						{
							float num5 = Vector2.Distance(new Vector2(this.AttPointUsedList[m].x, this.AttPointUsedList[m].z), new Vector2(position.x, position.z));
							if (num5 < (float)k)
							{
								flag2 = true;
							}
						}
					}
					if (!flag2)
					{
						this.AttPointUsedList.Add(position);
						return position;
					}
				}
			}
		}
		if (this.AttPointUsedList.Count <= 0)
		{
			return apply_tank.tr.position;
		}
		int num6 = 0;
		if (num6 >= this.AttPointUsedList.Count)
		{
			return zero;
		}
		return this.AttPointUsedList[num6];
	}

	public bool VerifyMapGrid(Vector3 pos)
	{
		int num = Mathf.RoundToInt(pos.x);
		if (num < 0 || num >= SenceManager.inst.arrayX)
		{
			return false;
		}
		int num2 = Mathf.RoundToInt(pos.z);
		if (num2 < 0 || num2 >= SenceManager.inst.arrayY)
		{
			return false;
		}
		List<T_Tower> list = new List<T_Tower>();
		for (int i = 0; i < SenceManager.inst.towers.Count; i++)
		{
			if (SenceManager.inst.towers[i])
			{
				float num3 = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(SenceManager.inst.towers[i].tr.position.x, SenceManager.inst.towers[i].tr.position.z));
				if (num3 <= (float)SenceManager.inst.towers[i].size * 1.2f)
				{
					list.Add(SenceManager.inst.towers[i]);
				}
				if (num3 <= (float)SenceManager.inst.towers[i].size * 0.6f)
				{
					return false;
				}
			}
		}
		int num4 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j])
			{
				float num5 = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(list[j].tr.position.x, list[j].tr.position.z));
				if (num5 <= (float)list[j].size * 0.5f)
				{
					num4++;
					if (num4 >= 2)
					{
						return false;
					}
				}
			}
		}
		return MapGridManager.VerifyMapGrid(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)) && MapGridManager.VerifyMapGrid(Mathf.RoundToInt(pos.x), (int)pos.z) && MapGridManager.VerifyMapGrid((int)pos.x, Mathf.RoundToInt(pos.z)) && MapGridManager.VerifyMapGrid((int)pos.x, (int)pos.z);
	}
}
