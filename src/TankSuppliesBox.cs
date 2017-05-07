using System;
using UnityEngine;

public class TankSuppliesBox : MonoBehaviour
{
	public SkillManage.TankSuppliesBox_Type tankbox_type;

	public int tankbox_id;

	public int tankbox_lv;

	public float size_X;

	public float size_Z;

	private BoxCollider this_boxcollider;

	private Rigidbody this_rigidbody;

	public Transform San;

	public float down_speed;

	private bool down_end;

	private bool jihuo;

	public void Init()
	{
		this.down_end = false;
		base.gameObject.AddComponent<BoxCollider>();
		base.gameObject.AddComponent<Rigidbody>();
		this.this_boxcollider = base.gameObject.GetComponent<BoxCollider>();
		this.this_rigidbody = base.gameObject.GetComponent<Rigidbody>();
		this.this_boxcollider.size = new Vector3(this.size_X, 2f, this.size_Z);
		this.this_boxcollider.center = new Vector3(0f, 1f, 0f);
		this.this_boxcollider.isTrigger = true;
		this.this_rigidbody.useGravity = false;
		base.gameObject.layer = 19;
	}

	private void Update()
	{
		if (!this.down_end)
		{
			base.transform.position = new Vector3(base.transform.position.x, Mathf.Max(0f, base.transform.position.y - this.down_speed * Time.deltaTime), base.transform.position.z);
		}
		else
		{
			this.San.transform.localPosition = new Vector3(0f, Mathf.Max(-10f, this.San.transform.localPosition.y - this.down_speed * Time.deltaTime), 0f);
		}
		if (base.transform.position.y <= 0f)
		{
			this.down_end = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 18 && this.down_end)
		{
			if (!this.jihuo)
			{
				this.jihuo = true;
				SkillManage.inst.TankSuppliesBox_AddTank(this.tankbox_type, base.transform.position, this.tankbox_id, 1, 1, this.tankbox_lv);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
