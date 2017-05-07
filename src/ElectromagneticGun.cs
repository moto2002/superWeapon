using System;
using UnityEngine;

public class ElectromagneticGun : MonoBehaviour
{
	private Vector3 MB_pos;

	private Vector3 Pos_Speed;

	private float Speed;

	private int SkillID;

	private bool boom;

	public void SetElectromagneticGun(Vector3 pos, int skillID, Vector3 gun_pos, float speed)
	{
		this.boom = false;
		pos += new Vector3(0f, 0f, 0f);
		this.MB_pos = pos;
		base.transform.position = pos + gun_pos;
		this.Pos_Speed = new Vector3(Mathf.Abs(gun_pos.x), Mathf.Abs(gun_pos.y), Mathf.Abs(gun_pos.z));
		this.Speed = speed;
		this.SkillID = skillID;
	}

	private void Update()
	{
		base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, this.MB_pos.x, this.Pos_Speed.x * this.Speed * Time.deltaTime), Mathf.MoveTowards(base.transform.position.y, this.MB_pos.y, this.Pos_Speed.y * this.Speed * Time.deltaTime), Mathf.MoveTowards(base.transform.position.z, this.MB_pos.z, this.Pos_Speed.z * this.Speed * Time.deltaTime));
		if (Vector3.Distance(base.transform.position, this.MB_pos) < 12f && !this.boom)
		{
			this.boom = true;
			SkillManage.inst.ElectromagneticGun_Boom(this.MB_pos, this.SkillID);
		}
		if (Vector3.Distance(base.transform.position, this.MB_pos) < 0.2f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
