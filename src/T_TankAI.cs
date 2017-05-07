using System;
using UnityEngine;

public class T_TankAI : MonoBehaviour
{
	public T_Tank This_Tank;

	public int AI_TeamIndex;

	public int AI_Teamindex;

	public bool Move;

	public bool Fire;

	public bool AdvanceFire;

	public Vector3 End_pos;

	public Transform Att_MB;

	public Vector3 Att_pos;

	public GameObject HeaderFlag;

	private float Flag_Height;

	public CharacterController CC;

	public string Log;

	public int CannotFireToMB;

	private bool Init_already;

	private AIPath aipath;

	public int tank_index;

	public bool IsCreateByBox;

	public float dis_basic;

	public void Init()
	{
		if (this.Init_already)
		{
			return;
		}
		this.This_Tank = base.GetComponent<T_Tank>();
		this.aipath = base.GetComponent<AIPath>();
		this.CC = base.GetComponent<CharacterController>();
		if (this.CC)
		{
			this.CC.radius = 0f;
		}
		if (this.AI_Teamindex == 1)
		{
			if (!NewbieGuidePanel.isEnemyAttck)
			{
			}
			if (this.This_Tank.this_commander != null)
			{
				this.Flag_Height = 2.2f;
			}
			else
			{
				switch (this.This_Tank.index)
				{
				case 1:
					this.Flag_Height = 1f;
					break;
				case 2:
					this.Flag_Height = 1f;
					break;
				case 3:
					this.Flag_Height = 1.3f;
					break;
				case 4:
					this.Flag_Height = 2f;
					break;
				case 5:
					this.Flag_Height = 1.8f;
					break;
				case 6:
					this.Flag_Height = 1.2f;
					break;
				case 7:
					this.Flag_Height = 1f;
					break;
				}
			}
			this.Init_already = true;
		}
	}

	private void Start()
	{
	}
}
