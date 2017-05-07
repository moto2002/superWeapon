using System;
using UnityEngine;

public class T_Missile : MonoBehaviour
{
	public float speed;

	public Vector3 MB_pos;

	private Vector3 MB_speed;

	public Transform camera_Client;

	public int Missile_Power;

	private bool boom;

	public void Init()
	{
		this.boom = false;
		this.MB_speed = new Vector3(Mathf.Abs(base.transform.position.x - this.MB_pos.x), Mathf.Abs(base.transform.position.y - this.MB_pos.y), Mathf.Abs(base.transform.position.z - this.MB_pos.z));
	}

	private void Update()
	{
		base.transform.LookAt(this.MB_pos);
		base.transform.Translate(0f, 0f, this.speed * Time.deltaTime);
		if (base.transform.position.y <= 0f && !this.boom)
		{
			this.boom = true;
			DieBall dieBall = PoolManage.Ins.CreatEffect(UnitConst.GetInstance().skillList[5].damageEffect, base.transform.position, new Quaternion(0f, 0f, 0f, 0f), SenceManager.inst.bulletPool);
			if (dieBall.ga)
			{
				AudioManage.inst.PlayAuidoBySelf_3D("bomberexplosion", dieBall.ga, false, 0uL);
				if (dieBall.ga.GetComponent<AudioSource>())
				{
					dieBall.ga.GetComponent<AudioSource>().volume = UnityEngine.Random.Range(0.3f, 1f);
				}
			}
			SkillManage.inst.Missile_Boom(0f, base.transform.position, 0, this.Missile_Power);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
