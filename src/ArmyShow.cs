using System;
using UnityEngine;

public class ArmyShow : MonoBehaviour
{
	public int Index;

	public bool NowChoose;

	public Body_Model effect_model;

	public Transform Real_model;

	public Transform Head_model;

	public Transform DaoDan_model;

	public Transform TaiZi_model;

	private Vector3 Basic_pos;

	private Vector3 Basic_rot;

	private Vector3 TaiZi_rot;

	private Vector3 Basic_sca;

	public bool IsPlane;

	public bool IsCopter;

	private Animation animation1;

	private float height_wc;

	private float negativedirection;

	private float negativedirection2;

	public float rotate_speed;

	private float shake_time;

	private float shake_time0;

	private int shake_no;

	private float shake_degree;

	private float head_time;

	private int head_no;

	private float plane_time;

	private int plane_no;

	private float plane_speed;

	private float plane_speed1;

	private bool CannotRotate;

	public void Init()
	{
		this.CannotRotate = false;
		this.IsPlane = true;
		if (!this.effect_model)
		{
			return;
		}
		this.Real_model = this.effect_model.BlueModel.transform;
		this.Real_model.transform.localPosition = Vector3.zero;
		this.negativedirection = 1f;
		switch (this.Index)
		{
		case 1:
			this.Real_model.transform.localPosition = new Vector3(0f, 0f, -0.2f);
			break;
		case 3:
			this.Real_model.transform.localPosition = new Vector3(0f, 0f, -0.2f);
			break;
		case 5:
			this.Real_model.transform.localPosition = new Vector3(0f, 0f, -0.15f);
			break;
		case 6:
			this.Real_model.transform.localPosition = new Vector3(0f, 0f, -0.5f);
			break;
		case 15:
			this.negativedirection = -1f;
			this.Real_model.transform.localScale *= 0.6f;
			break;
		case 16:
			this.negativedirection = 1f;
			break;
		case 17:
			this.negativedirection = -1f;
			this.negativedirection2 = -1f;
			break;
		case 18:
			this.Real_model.transform.localPosition = new Vector3(0f, 0.3f, 0f);
			break;
		case 19:
			this.Real_model.transform.localPosition = new Vector3(0f, 0.3f, 0f);
			this.height_wc = -0.8f;
			this.IsCopter = true;
			break;
		case 20:
			this.Real_model.transform.localPosition = new Vector3(0f, 1.32f, 0f);
			this.height_wc = 0.22f;
			this.IsCopter = true;
			break;
		}
		this.Basic_pos = this.Real_model.transform.localPosition;
		this.Basic_rot = this.Real_model.transform.localEulerAngles;
		this.Basic_sca = this.Real_model.transform.localScale;
		Transform[] componentsInChildren = this.effect_model.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.name == "head")
			{
				this.Head_model = transform;
				this.IsPlane = false;
				break;
			}
		}
		if (this.Head_model == null)
		{
			Transform[] componentsInChildren2 = this.effect_model.GetComponentsInChildren<Transform>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Transform transform2 = componentsInChildren2[j];
				if (transform2.name == "daodan")
				{
					this.DaoDan_model = transform2;
					this.IsPlane = false;
					break;
				}
			}
		}
		this.shake_degree = 0.0025f;
		if (this.IsPlane && this.effect_model.BlueModel.gameObject.GetComponent<Animation>())
		{
			this.animation1 = this.effect_model.BlueModel.gameObject.GetComponent<Animation>();
			this.animation1.Play();
			this.animation1.wrapMode = WrapMode.Loop;
		}
		Transform[] componentsInChildren3 = this.Real_model.GetComponentsInChildren<Transform>();
		for (int k = 0; k < componentsInChildren3.Length; k++)
		{
			Transform transform3 = componentsInChildren3[k];
			transform3.gameObject.layer = 18;
		}
		this.effect_model.tr.parent = this.TaiZi_model;
		this.effect_model.tr.localPosition = Vector3.zero;
		this.effect_model.tr.localEulerAngles = new Vector3(0f, 0f, 0f);
		this.TaiZi_rot = this.TaiZi_model.transform.localEulerAngles;
		Transform[] componentsInChildren4 = this.TaiZi_model.GetComponentsInChildren<Transform>();
		for (int l = 0; l < componentsInChildren4.Length; l++)
		{
			Transform transform4 = componentsInChildren4[l];
			if (this.IsPlane)
			{
				transform4.gameObject.layer = 18;
			}
			else
			{
				transform4.gameObject.layer = 18;
			}
		}
		MeshRenderer[] componentsInChildren5 = this.effect_model.GetComponentsInChildren<MeshRenderer>();
		for (int m = 0; m < componentsInChildren5.Length; m++)
		{
			MeshRenderer meshRenderer = componentsInChildren5[m];
			if (meshRenderer.name == "tank_yingzi" || meshRenderer.name == "tank3_1_yingzi")
			{
				meshRenderer.transform.localPosition = new Vector3(meshRenderer.transform.localPosition.x, 0.05f, meshRenderer.transform.localPosition.z);
				meshRenderer.gameObject.layer = 18;
			}
		}
		if (this.IsPlane)
		{
			ArmyControlAndUpdatePanel.Inst.SetRightPanelTo3D(true);
		}
		else
		{
			ArmyControlAndUpdatePanel.Inst.SetRightPanelTo3D(true);
		}
	}

	public void SetNowChoose(bool True)
	{
		if (True)
		{
			this.NowChoose = true;
			this.Real_model.transform.localPosition = this.Basic_pos;
			this.Real_model.transform.localEulerAngles = this.Basic_rot;
			this.TaiZi_model.localEulerAngles = this.TaiZi_rot;
		}
		else
		{
			this.NowChoose = false;
			this.Real_model.transform.localPosition = this.Basic_pos;
			this.Real_model.transform.localEulerAngles = this.Basic_rot;
			this.TaiZi_model.localEulerAngles = this.TaiZi_rot;
			this.head_no = 0;
			this.rotate_speed = 0f;
			this.plane_time = 0f;
			this.plane_no = 0;
			this.plane_speed = 0f;
			this.plane_speed1 = 0f;
		}
	}

	public void SetCarRotate(float speed)
	{
	}

	private void Update()
	{
		if (!this.Real_model)
		{
			return;
		}
		if (!this.NowChoose)
		{
			this.Real_model.transform.localScale = this.Basic_sca;
			if (this.Head_model)
			{
				this.Head_model.transform.localEulerAngles = new Vector3(this.Head_model.transform.localEulerAngles.x, Mathf.MoveTowardsAngle(this.Head_model.transform.localEulerAngles.y, 0f, 60f * Time.deltaTime), this.Head_model.transform.localEulerAngles.z);
			}
			if (this.DaoDan_model)
			{
				this.DaoDan_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.DaoDan_model.transform.localEulerAngles.x, 0f, 60f * Time.deltaTime), this.DaoDan_model.transform.localEulerAngles.y, this.DaoDan_model.transform.localEulerAngles.z);
			}
			return;
		}
		this.Real_model.transform.localScale = 1.2f * this.Basic_sca;
		this.rotate_speed = Mathf.Max(-500f, Mathf.Min(500f, this.rotate_speed));
		this.rotate_speed = Mathf.MoveTowards(this.rotate_speed, 0f, 300f * Time.deltaTime);
		this.TaiZi_model.transform.Rotate(0f, this.rotate_speed * Time.deltaTime, 0f);
		if (!this.IsPlane)
		{
			this.shake_time += Time.deltaTime;
			if (this.shake_time >= 0.1f)
			{
				this.shake_time = 0f;
				this.shake_no++;
				if (this.shake_no % 2 == 0)
				{
					this.Real_model.transform.localPosition = this.Basic_pos + new Vector3(0.2f * this.shake_degree, 0.5f * this.shake_degree, 0.2f * this.shake_degree);
				}
				else
				{
					this.Real_model.transform.localPosition = this.Basic_pos - new Vector3(0.2f * this.shake_degree, 0.5f * this.shake_degree, 0.2f * this.shake_degree);
				}
			}
			if (this.Head_model)
			{
				if (Mathf.Abs(this.rotate_speed) <= 500f)
				{
					this.head_time += Time.deltaTime;
					if (this.head_time > 2f)
					{
						this.head_time = 0f;
						this.head_no++;
					}
					if (this.head_no > 0)
					{
						if (this.head_no % 2 == 0)
						{
							this.Head_model.transform.localEulerAngles = new Vector3(this.Head_model.transform.localEulerAngles.x, Mathf.MoveTowardsAngle(this.Head_model.transform.localEulerAngles.y, 10f, 60f * Time.deltaTime), this.Head_model.transform.localEulerAngles.z);
						}
						else
						{
							this.Head_model.transform.localEulerAngles = new Vector3(this.Head_model.transform.localEulerAngles.x, Mathf.MoveTowardsAngle(this.Head_model.transform.localEulerAngles.y, -30f, 60f * Time.deltaTime), this.Head_model.transform.localEulerAngles.z);
						}
					}
				}
				else
				{
					this.head_time = 0f;
					this.Head_model.transform.localEulerAngles = new Vector3(this.Head_model.transform.localEulerAngles.x, Mathf.MoveTowardsAngle(this.Head_model.transform.localEulerAngles.y, 0f, 60f * Time.deltaTime), this.Head_model.transform.localEulerAngles.z);
				}
			}
			else if (this.DaoDan_model)
			{
				this.head_time += Time.deltaTime;
				if (this.head_time > 2f)
				{
					this.head_time = 0f;
					this.head_no++;
				}
				if (Mathf.Abs(this.rotate_speed) <= 500f)
				{
					if (this.head_no > 0)
					{
						if (this.head_no % 2 == 0)
						{
							this.DaoDan_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.DaoDan_model.transform.localEulerAngles.x, 5f, 60f * Time.deltaTime), this.DaoDan_model.transform.localEulerAngles.y, this.DaoDan_model.transform.localEulerAngles.z);
						}
						else
						{
							this.DaoDan_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.DaoDan_model.transform.localEulerAngles.x, -20f, 60f * Time.deltaTime), this.DaoDan_model.transform.localEulerAngles.y, this.DaoDan_model.transform.localEulerAngles.z);
						}
					}
				}
				else
				{
					this.DaoDan_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.DaoDan_model.transform.localEulerAngles.x, 0f, 60f * Time.deltaTime), this.DaoDan_model.transform.localEulerAngles.y, this.DaoDan_model.transform.localEulerAngles.z);
				}
			}
		}
		else if (this.IsPlane)
		{
			if (this.IsCopter)
			{
				this.plane_time += Time.deltaTime;
				if (this.plane_no == 0)
				{
					if (this.plane_time > 1.5f)
					{
						this.plane_time = 0f;
						this.plane_no++;
						this.CannotRotate = true;
					}
				}
				else if (this.plane_no == 1)
				{
					if (this.Real_model.transform.localPosition.y <= 2f + this.height_wc)
					{
						this.plane_speed += 0.5f * Time.deltaTime;
					}
					else if (this.Real_model.transform.localPosition.y <= 3f + this.height_wc)
					{
						this.plane_speed -= 0.5f * Time.deltaTime;
					}
					if (this.plane_time < 5f)
					{
						this.Real_model.transform.localPosition = new Vector3(this.Real_model.transform.localPosition.x, this.Real_model.transform.localPosition.y + this.plane_speed * Time.deltaTime, this.Real_model.transform.localPosition.z);
					}
					else
					{
						this.Real_model.transform.localPosition = new Vector3(this.Real_model.transform.localPosition.x, this.Real_model.transform.localPosition.y + this.plane_speed * Time.deltaTime, this.Real_model.transform.localPosition.z);
						this.Real_model.transform.localPosition = new Vector3(this.Real_model.transform.localPosition.x, Mathf.MoveTowards(this.Real_model.transform.localPosition.y, 4.5f + this.height_wc, 20f * this.plane_speed1 * Time.deltaTime), this.Real_model.transform.localPosition.z);
					}
					if (this.plane_time > 5f)
					{
						this.plane_speed1 += 0.1f * Time.deltaTime;
						this.Real_model.transform.Translate(0f, 0f, 10f * this.plane_speed1 * Time.deltaTime);
						this.Real_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.x, 10f, 100f * this.plane_speed1 * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.y, 60f, 250f * this.plane_speed1 * Time.deltaTime), this.Real_model.transform.localEulerAngles.z);
					}
					if (this.Real_model.transform.localPosition.z > 20f)
					{
						this.plane_no++;
						this.Real_model.transform.localPosition = new Vector3(13.75f, 4f + this.height_wc, -6f);
						this.Real_model.transform.localEulerAngles = new Vector3(0f, -65f, 0f);
					}
				}
				else if (this.plane_no == 2)
				{
					this.plane_speed1 += 1f * Time.deltaTime;
					this.Real_model.transform.localPosition = new Vector3(Mathf.MoveTowards(this.Real_model.transform.localPosition.x, 0f, (this.Real_model.transform.localPosition.x + 0.1f) * Time.deltaTime), Mathf.MoveTowards(this.Real_model.transform.localPosition.y, 3.5f + this.height_wc, this.plane_speed1 * Time.deltaTime), Mathf.MoveTowards(this.Real_model.transform.localPosition.z, 0f, Mathf.Abs(this.Real_model.transform.localPosition.z - 0.1f) * Time.deltaTime));
					this.Real_model.transform.localEulerAngles = new Vector3((1f - (12.5f - this.Real_model.transform.localPosition.x) / 12.5f) * 20f + 10f, this.Real_model.transform.localEulerAngles.y, this.Real_model.transform.localEulerAngles.z);
					if (this.Real_model.transform.localPosition.x <= 0.5f)
					{
						this.plane_no++;
					}
				}
				else if (this.plane_no == 3)
				{
					this.plane_speed1 += 1f * Time.deltaTime;
					this.Real_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.x, 0f, 5f * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.y, 0f, 70f * Time.deltaTime), this.Real_model.transform.localEulerAngles.z);
					if (Mathf.Abs(this.Real_model.transform.localEulerAngles.y) <= 20f)
					{
						this.Real_model.transform.localPosition = new Vector3(Mathf.MoveTowards(this.Real_model.transform.localPosition.x, 0f, (this.Real_model.transform.localPosition.x + 0.1f) * Time.deltaTime), Mathf.MoveTowards(this.Real_model.transform.localPosition.y, this.Basic_pos.y, (this.Real_model.transform.localPosition.y - this.Basic_pos.y + 0.1f) * Time.deltaTime), Mathf.MoveTowards(this.Real_model.transform.localPosition.z, 0f, Mathf.Abs(this.Real_model.transform.localPosition.z - 0.1f) * Time.deltaTime));
					}
					if (this.Real_model.transform.localPosition.y <= this.Basic_pos.y)
					{
						DieBall dieBall = PoolManage.Ins.CreatEffect("jiangluosan_yanwu", this.TaiZi_model.position, this.TaiZi_model.rotation, this.Real_model);
						Transform[] componentsInChildren = dieBall.GetComponentsInChildren<Transform>();
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							Transform transform = componentsInChildren[i];
							transform.gameObject.layer = 18;
							if (transform.GetComponent<ParticleSystem>())
							{
								transform.GetComponent<ParticleSystem>().startColor -= new Color(0f, 0f, 0f, 0.5f);
							}
						}
						this.plane_time = 0f;
						this.plane_no++;
						this.CannotRotate = false;
					}
				}
				else if (this.plane_no == 4 && this.plane_time >= 3f)
				{
					this.plane_time = 0f;
					this.plane_no = 0;
					this.head_no = 0;
					this.rotate_speed = 0f;
					this.plane_time = 0f;
					this.plane_no = 0;
					this.plane_speed = 0f;
					this.plane_speed1 = 0f;
				}
			}
			else
			{
				if (this.Index == 15)
				{
					return;
				}
				this.plane_time += Time.deltaTime;
				if (this.plane_no == 0)
				{
					if (this.plane_time > 1.5f)
					{
						this.plane_time = 0f;
						this.plane_no++;
						this.CannotRotate = true;
					}
				}
				else if (this.plane_no == 1)
				{
					this.plane_speed1 = Mathf.Min(0.5f, this.plane_speed1 + Time.deltaTime);
					this.Real_model.transform.Translate(0f, 0f, 10f * this.negativedirection * this.plane_speed1 * Time.deltaTime);
					if (this.negativedirection == -1f)
					{
						if (this.negativedirection2 != -1f)
						{
							this.Real_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.x, 30f, 150f * this.plane_speed1 * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.y, -165f, 150f * this.plane_speed1 * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.z, 30f, 150f * this.plane_speed1 * Time.deltaTime));
						}
						else
						{
							this.Real_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.x, 30f, 120f * this.plane_speed1 * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.y, -165f, 150f * this.plane_speed1 * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.z, -150f, 150f * this.plane_speed1 * Time.deltaTime));
						}
					}
					else
					{
						this.Real_model.transform.localEulerAngles = new Vector3(Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.x, -30f, 120f * this.plane_speed1 * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.y, 45f, 150f * this.plane_speed1 * Time.deltaTime), Mathf.MoveTowardsAngle(this.Real_model.transform.localEulerAngles.z, -30f, 150f * this.plane_speed1 * Time.deltaTime));
					}
					if (this.Real_model.transform.localPosition.z >= 30f)
					{
						this.Real_model.transform.localPosition = new Vector3(-6.1f, 1.29f, 15.11f);
						if (this.negativedirection == 1f)
						{
							this.Real_model.transform.localEulerAngles = new Vector3(-10.5f, 120f, 20f);
						}
						else if (this.negativedirection2 == -1f)
						{
							this.Real_model.transform.localEulerAngles = new Vector3(10.5f, -60f, -200f);
						}
						else
						{
							this.Real_model.transform.localPosition = new Vector3(-4.5f, 0.95f, 11.11f);
							this.Real_model.transform.localEulerAngles = new Vector3(10.5f, -60f, -20f);
						}
						this.plane_time = 0f;
						this.plane_no++;
					}
				}
				else if (this.plane_no == 2)
				{
					if (this.plane_time > 0f)
					{
						this.Real_model.transform.Translate(0f, 0f, 12f * this.negativedirection * this.plane_speed1 * Time.deltaTime);
						if (this.Real_model.transform.localPosition.z <= -10f)
						{
							this.Real_model.transform.localPosition = new Vector3(20f, 3.5f, -50f);
							if (this.negativedirection == 1f)
							{
								this.Real_model.transform.localEulerAngles = new Vector3(20f, -20f, -15f);
							}
							else if (this.negativedirection2 == -1f)
							{
								this.Real_model.transform.localEulerAngles = new Vector3(-20f, 160f, -195f);
							}
							else
							{
								this.Real_model.transform.localEulerAngles = new Vector3(20f, 160f, -15f);
							}
							this.plane_no++;
							this.plane_time = 0f;
						}
					}
				}
				else if (this.plane_no == 3)
				{
					if (this.plane_time > 0f)
					{
						this.Real_model.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Real_model.transform.localPosition.y, this.Basic_pos.y, 2f * (this.Real_model.transform.localPosition.y - this.Basic_pos.y + 0.2f + 3f) * Time.deltaTime), Mathf.MoveTowards(this.Real_model.transform.localPosition.z, 0f, 1.8f * Mathf.Abs(this.Real_model.transform.localPosition.z - 2f) * Time.deltaTime));
						if (this.negativedirection == 1f)
						{
							this.Real_model.transform.localEulerAngles = new Vector3((1f - (this.Real_model.transform.localPosition.z + 50f) / 50f) * 20f, (1f - (this.Real_model.transform.localPosition.z + 50f) / 50f) * -20f, (1f - (this.Real_model.transform.localPosition.z + 30f) / 30f) * -15f);
						}
						else if (this.negativedirection2 == -1f)
						{
							this.Real_model.transform.localEulerAngles = new Vector3((1f - (this.Real_model.transform.localPosition.z + 50f) / 50f) * -20f, (1f - (this.Real_model.transform.localPosition.z + 50f) / 50f) * -20f + 180f, (1f - (this.Real_model.transform.localPosition.z + 30f) / 30f) * 15f + 180f);
						}
						else
						{
							this.Real_model.transform.localEulerAngles = new Vector3((1f - (this.Real_model.transform.localPosition.z + 50f) / 50f) * 20f, (1f - (this.Real_model.transform.localPosition.z + 50f) / 50f) * -20f + 180f, (1f - (this.Real_model.transform.localPosition.z + 30f) / 30f) * 15f);
						}
						if (this.Real_model.transform.localPosition.z >= 0f)
						{
							DieBall dieBall2 = PoolManage.Ins.CreatEffect("jiangluosan_yanwu", this.TaiZi_model.position, this.TaiZi_model.rotation, this.Real_model);
							Transform[] componentsInChildren2 = dieBall2.GetComponentsInChildren<Transform>();
							for (int j = 0; j < componentsInChildren2.Length; j++)
							{
								Transform transform2 = componentsInChildren2[j];
								transform2.gameObject.layer = 18;
								if (transform2.GetComponent<ParticleSystem>())
								{
									transform2.GetComponent<ParticleSystem>().startColor -= new Color(0f, 0f, 0f, 0.5f);
								}
							}
							this.plane_time = 0f;
							this.plane_no = 4;
						}
					}
				}
				else if (this.plane_no == 4 && this.plane_time > 3f)
				{
					this.plane_time = 0f;
					this.plane_no = 0;
					this.head_no = 0;
					this.rotate_speed = 0f;
					this.plane_time = 0f;
					this.plane_no = 0;
					this.plane_speed = 0f;
					this.plane_speed1 = 0f;
				}
			}
		}
	}
}
