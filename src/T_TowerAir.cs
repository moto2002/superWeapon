using System;
using UnityEngine;

public class T_TowerAir : MonoBehaviour
{
	private enum TowerAirState
	{
		Stay,
		Return,
		Out,
		Up,
		Down
	}

	public Body_Model Air_Model;

	public int airModelIndex;

	public Transform Real_Model;

	private T_TowerAir.TowerAirState towerAirState;

	private float speed;

	private float jd;

	private Animation animation1;

	private bool IsKirov;

	private float jd1;

	private float jd2;

	private bool copter;

	private float copter_time;

	private int copter_no;

	private float y;

	private float y0;

	private float y1;

	private float y2;

	private float stay_time;

	private void Start()
	{
	}

	public void Air_Return()
	{
		this.towerAirState = T_TowerAir.TowerAirState.Return;
		this.copter = false;
		this.IsKirov = false;
		if (this.Air_Model.name == "ari_apache" || this.Air_Model.name == "ari_icehel")
		{
			this.copter = true;
		}
		else if (this.Air_Model.name == "kirov ")
		{
			this.IsKirov = true;
			return;
		}
		if (this.Air_Model.BlueModel)
		{
			if (!this.Air_Model.BlueModel.gameObject.activeSelf)
			{
				return;
			}
			this.Real_Model = this.Air_Model.BlueModel.gameObject.transform;
			if (this.Air_Model.BlueModel.gameObject.GetComponent<Animation>())
			{
				this.animation1 = this.Air_Model.BlueModel.gameObject.GetComponent<Animation>();
				if (this.animation1)
				{
					this.animation1.Play();
					this.animation1.wrapMode = WrapMode.Loop;
				}
			}
		}
		if (this.copter)
		{
			this.Air_Model.tr.localEulerAngles = new Vector3(0f, (float)UnityEngine.Random.Range(0, 360), 0f);
			this.Real_Model.transform.localPosition = new Vector3(0f, 20f, -50f);
			this.y = 5f;
			this.y0 = 1.3f;
			if (this.Air_Model.name == "ari_apache")
			{
				this.y0 = 0f;
			}
			else if (this.Air_Model.name == "ari_icehel")
			{
				this.y0 = 0.5f;
			}
			this.y1 = this.y;
			this.y2 = this.Real_Model.transform.localPosition.y * 0.5f;
			this.jd = 15f;
			this.speed = 1f;
		}
		else
		{
			this.Real_Model.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			this.Real_Model.transform.localPosition = new Vector3(0f, 30f, -50f);
			this.y = 0f;
			this.y0 = 0f;
			this.y1 = 0f;
			this.y2 = this.Real_Model.transform.localPosition.y * 1f;
			this.jd = -25f;
			this.speed = 2f;
			this.jd2 = 1f;
			if (this.Air_Model.name == "ari_appollo")
			{
				this.jd1 = 0f;
				this.jd2 = -1f;
				this.Real_Model.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.speed = 2.5f;
			}
			else if (this.Air_Model.name == "bomber")
			{
				this.jd1 = 0f;
				this.jd2 = -1f;
				this.Real_Model.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			}
			else if (this.Air_Model.name == "ari_mige")
			{
				this.jd1 = 0f;
				this.Real_Model.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.jd2 = -1f;
				this.speed = 3f;
			}
		}
	}

	public void Air_Out()
	{
	}

	private void Update()
	{
		if (this.IsKirov)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
		}
		if (this.Air_Model == null || this.Real_Model == null)
		{
			return;
		}
		if (this.towerAirState == T_TowerAir.TowerAirState.Return)
		{
			if (this.copter)
			{
				this.copter_time += Time.deltaTime;
				if (this.copter_time >= 1.5f)
				{
					this.copter_time = 0f;
					this.copter_no++;
				}
				if (this.copter_no % 2 == 0)
				{
					this.y += 1f * Time.deltaTime;
				}
				else
				{
					this.y -= 1f * Time.deltaTime;
				}
			}
			this.Real_Model.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Real_Model.transform.localPosition.y, this.y, Mathf.Max(1f, this.Real_Model.transform.localPosition.y - this.y) * this.speed * Time.deltaTime), Mathf.MoveTowards(this.Real_Model.transform.localPosition.z, 0f, Mathf.Min(10f, Mathf.Max(1f, Mathf.Abs(this.Real_Model.transform.localPosition.z))) * this.speed * Time.deltaTime));
			if (Mathf.Abs(this.Real_Model.transform.localPosition.z) >= 30f)
			{
				this.Real_Model.transform.localEulerAngles = new Vector3(this.jd * this.jd2, this.Real_Model.transform.localEulerAngles.y, 0f + this.jd1);
			}
			else
			{
				this.Real_Model.transform.localEulerAngles = new Vector3(Mathf.Abs(this.Real_Model.transform.localPosition.z) / 30f * this.jd * this.jd2, this.Real_Model.transform.localEulerAngles.y, 0f + this.jd1);
			}
			if (this.Real_Model.transform.localPosition.z >= 0f)
			{
				this.towerAirState = T_TowerAir.TowerAirState.Down;
			}
		}
		else if (this.towerAirState == T_TowerAir.TowerAirState.Down)
		{
			if (this.copter)
			{
				this.copter_time += Time.deltaTime;
				if (this.copter_time >= 1.5f)
				{
					this.copter_time = 0f;
					this.copter_no++;
				}
				if (this.copter_no % 2 == 0)
				{
					this.y += 1f * Time.deltaTime;
				}
				else
				{
					this.y -= 1f * Time.deltaTime;
				}
			}
			this.y = Mathf.Max(this.y0, this.y - 2f * this.speed * Time.deltaTime);
			this.Real_Model.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Real_Model.transform.localPosition.y, this.y, Mathf.Max(1f, this.Real_Model.transform.localPosition.y) * this.speed * Time.deltaTime), 0f);
			if (this.Air_Model.tr.localEulerAngles.y <= 180f)
			{
				this.Air_Model.tr.localEulerAngles = new Vector3(0f, Mathf.Max(this.Air_Model.tr.localEulerAngles.y - 80f * Time.deltaTime, 0f), 0f);
			}
			else if (this.Air_Model.tr.localEulerAngles.y > 180f)
			{
				this.Air_Model.tr.localEulerAngles = new Vector3(0f, Mathf.Min(this.Air_Model.tr.localEulerAngles.y + 80f * Time.deltaTime, 360f), 0f);
			}
			if (this.Real_Model.transform.localPosition.y <= this.y0 && this.Air_Model.tr.localEulerAngles.y == 0f)
			{
				this.towerAirState = T_TowerAir.TowerAirState.Stay;
				this.copter_time = 0f;
				this.stay_time = (float)UnityEngine.Random.Range(10, 30);
			}
		}
		else if (this.towerAirState == T_TowerAir.TowerAirState.Stay)
		{
			if (this.copter && this.animation1)
			{
				this.animation1.Stop();
			}
			this.copter_time += Time.deltaTime;
			if (this.copter_time >= this.stay_time)
			{
				this.copter_time = 0f;
				this.towerAirState = T_TowerAir.TowerAirState.Up;
				if (this.copter && this.animation1)
				{
					this.animation1.Play();
				}
			}
		}
		else if (this.towerAirState == T_TowerAir.TowerAirState.Up)
		{
			if (this.copter)
			{
				this.copter_time += Time.deltaTime;
				if (this.copter_time >= 1.5f)
				{
					this.copter_time = 0f;
					this.copter_no++;
				}
				if (this.copter_no % 2 == 0)
				{
					this.y += 1f * Time.deltaTime;
				}
				else
				{
					this.y -= 1f * Time.deltaTime;
				}
			}
			this.y = Mathf.Min(this.y1, this.y + 2f * this.speed * Time.deltaTime);
			this.Real_Model.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Real_Model.transform.localPosition.y, this.y1, Mathf.Max(1f, this.Real_Model.transform.localPosition.y) * this.speed * Time.deltaTime), 0f);
			if (this.Real_Model.transform.localPosition.y >= this.y1)
			{
				this.towerAirState = T_TowerAir.TowerAirState.Out;
				this.copter_time = 0f;
				this.stay_time = (float)UnityEngine.Random.Range(2, 10);
			}
		}
		if (this.towerAirState == T_TowerAir.TowerAirState.Out)
		{
			if (this.Real_Model.transform.localPosition.z >= 100f)
			{
				this.copter_time += Time.deltaTime;
				if (this.copter_time >= this.stay_time)
				{
					this.copter_time = 0f;
					this.Air_Return();
				}
			}
			else if (this.copter)
			{
				this.copter_time += Time.deltaTime;
				if (this.copter_time >= 1.5f)
				{
					this.copter_time = 0f;
					this.copter_no++;
				}
				if (this.copter_no % 2 == 0)
				{
					this.y += 1f * Time.deltaTime;
				}
				else
				{
					this.y -= 1f * Time.deltaTime;
				}
			}
			if (this.copter)
			{
				if (Mathf.Abs(this.Real_Model.transform.localPosition.z) >= 4f)
				{
					this.Real_Model.transform.localEulerAngles = new Vector3(this.jd, this.Real_Model.transform.localEulerAngles.y, 0f + this.jd1);
				}
				else
				{
					this.Real_Model.transform.localEulerAngles = new Vector3(Mathf.Abs(this.Real_Model.transform.localPosition.z) / 4f * this.jd, this.Real_Model.transform.localEulerAngles.y, 0f + this.jd1);
				}
				this.Real_Model.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Real_Model.transform.localPosition.y, this.y2, Mathf.Max(1f, this.Real_Model.transform.localPosition.y - this.y) * this.speed * Time.deltaTime), Mathf.MoveTowards(this.Real_Model.transform.localPosition.z, 100f, Mathf.Min(10f, Mathf.Max(1f, Mathf.Abs(this.Real_Model.transform.localPosition.z))) * this.speed * Time.deltaTime));
			}
			else
			{
				if (Mathf.Abs(this.Real_Model.transform.localPosition.z) >= 40f)
				{
					this.Real_Model.transform.localEulerAngles = new Vector3(-this.jd * this.jd2, this.Real_Model.transform.localEulerAngles.y, 0f + this.jd1);
				}
				else
				{
					this.Real_Model.transform.localEulerAngles = new Vector3(Mathf.Abs(this.Real_Model.transform.localPosition.z) / -40f * this.jd * this.jd2, this.Real_Model.transform.localEulerAngles.y, 0f + this.jd1);
				}
				this.Real_Model.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Real_Model.transform.localPosition.y, this.y2, Mathf.Max(1f, this.Real_Model.transform.localPosition.y - this.y) * 0.5f * this.speed * Time.deltaTime), Mathf.MoveTowards(this.Real_Model.transform.localPosition.z, 100f, Mathf.Min(10f, Mathf.Max(1f, Mathf.Abs(this.Real_Model.transform.localPosition.z))) * this.speed * Time.deltaTime));
			}
		}
	}
}
