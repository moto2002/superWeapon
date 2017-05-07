using msg;
using System;
using UnityEngine;

public class BuildingDes : MonoBehaviour
{
	private Camera cam;

	public Transform tar;

	public string Des;

	public UILabel Label;

	public float yOffect;

	public float distance;

	private bool IsSend;

	private void Start()
	{
		this.cam = UIManager.inst.uiCamera;
		this.distance = CameraControl.inst.minfar - CameraControl.inst.bigfar;
		this.Label = base.GetComponentInChildren<UILabel>();
		this.Label.text = this.Des;
	}

	public virtual void YoofectUp(float i)
	{
		this.yOffect = 2f + i * 3f;
	}

	private void Update()
	{
		this.yOffect = 4f;
		if (this.tar == null || this.cam == null || Camera.main == null || !this.tar.gameObject.activeInHierarchy)
		{
			NGUITools.Destroy(base.gameObject);
			return;
		}
		float num = this.yOffect - 1f / this.distance * (CameraControl.inst.MainCamera.transform.localPosition.z + Mathf.Abs(CameraControl.inst.bigfar));
		Vector3 position = new Vector3(this.tar.position.x, this.tar.position.y + num, this.tar.position.z);
		Vector3 vector = CameraControl.inst.MainCamera.WorldToScreenPoint(position);
		Vector3 vector2 = UIManager.inst.uiCamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, UIManager.inst.uiCamera.transform.position.z));
		base.transform.position = new Vector3(vector2.x, vector2.y, 0f);
		base.transform.localScale = Vector3.one * 0.6f;
		if (this.tar.GetComponent<T_Tank>())
		{
			if (this.tar.GetComponent<T_Tank>().ExtraArmyId != 0)
			{
				if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
				{
					if (SenceManager.inst.ExtraArmyDataList.ContainsKey(this.tar.GetComponent<T_Tank>().ExtraArmyId))
					{
						TimeSpan timeSpan = TimeTools.ConvertLongDateTime(SenceManager.inst.ExtraArmyDataList[this.tar.GetComponent<T_Tank>().ExtraArmyId].life_time) - TimeTools.GetNowTimeSyncServerToDateTime();
						string arg = string.Empty;
						if (timeSpan.TotalSeconds > 1.0 && OnLineAward.laod.step != 0)
						{
							if (timeSpan.Hours > 0)
							{
								arg = string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
							}
							else if (timeSpan.Minutes > 0)
							{
								arg = string.Format("{0}:{1}", timeSpan.Minutes, timeSpan.Seconds);
							}
							else if (timeSpan.Seconds > 1)
							{
								arg = string.Format("{0}", timeSpan.Seconds);
							}
							else
							{
								this.CSClearExtarArmy();
								UnityEngine.Object.Destroy(this.tar.gameObject);
								UnityEngine.Object.Destroy(base.gameObject);
							}
						}
						this.Label.fontSize = 28;
						this.Label.text = string.Format("[FFFF4B]特殊兵种[-]\n[FF00FF]{0}[-]", arg);
					}
				}
				else
				{
					this.Label.fontSize = 28;
					this.Label.text = string.Format("[DC143C]特殊兵种[-]", new object[0]);
				}
			}
		}
		else if (this.tar.GetComponent<T_AirShip>() && this.tar.GetComponent<T_AirShip>().ExtraArmyId != 0)
		{
			if (SenceInfo.curMap.ID == HeroInfo.GetInstance().homeMapID)
			{
				if (SenceManager.inst.ExtraArmyDataList.ContainsKey(this.tar.GetComponent<T_AirShip>().ExtraArmyId))
				{
					TimeSpan timeSpan2 = TimeTools.ConvertLongDateTime(SenceManager.inst.ExtraArmyDataList[this.tar.GetComponent<T_AirShip>().ExtraArmyId].life_time) - TimeTools.GetNowTimeSyncServerToDateTime();
					string arg2 = string.Empty;
					if (timeSpan2.TotalSeconds > 1.0 && OnLineAward.laod.step != 0)
					{
						if (timeSpan2.Hours > 0)
						{
							arg2 = string.Format("{0}:{1}:{2}", timeSpan2.Hours, timeSpan2.Minutes, timeSpan2.Seconds);
						}
						else if (timeSpan2.Minutes > 0)
						{
							arg2 = string.Format("{0}:{1}", timeSpan2.Minutes, timeSpan2.Seconds);
						}
						else if (timeSpan2.Seconds > 1)
						{
							arg2 = string.Format("{0}", timeSpan2.Seconds);
						}
						else
						{
							this.CSClearExtarArmy();
							UnityEngine.Object.Destroy(this.tar.gameObject);
							UnityEngine.Object.Destroy(base.gameObject);
						}
					}
					this.Label.fontSize = 28;
					this.Label.text = string.Format("[FFFF4B]特殊兵种[-]\n[FF00FF]{0}[-]", arg2);
				}
			}
			else
			{
				this.Label.fontSize = 28;
				this.Label.text = string.Format("[DC143C]特殊兵种[-]", new object[0]);
			}
		}
	}

	public void CSClearExtarArmy()
	{
		if (this.IsSend)
		{
			return;
		}
		this.IsSend = true;
		CSClearExtarArmy cSClearExtarArmy = new CSClearExtarArmy();
		cSClearExtarArmy.id = (long)SenceManager.inst.ExtraArmyDataList[this.tar.GetComponent<T_Tank>().ExtraArmyId].ID;
		if (!SenceManager.inst.Des_ExtraArmyDataList.ContainsKey((int)cSClearExtarArmy.id))
		{
			SenceManager.inst.Des_ExtraArmyDataList.Add((int)cSClearExtarArmy.id, 1);
			ClientMgr.GetNet().SendHttp(3024, cSClearExtarArmy, new DataHandler.OpcodeHandler(this.SCClearExtarArmyCallBack), null);
		}
	}

	public void SCClearExtarArmyCallBack(bool Error, Opcode func)
	{
		if (!Error)
		{
			UnityEngine.Object.Destroy(this.tar.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
