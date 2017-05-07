using System;
using UnityEngine;

public class TimeTittleBtn : Tittle_3D
{
	private Camera cam;

	public GameObject skillPri_Bac;

	public GameObject skillInter_Bac;

	public GameObject skillSenior_Bac;

	public UISprite speendProcess;

	public UILabel timetext;

	public DateTime beginTime;

	public DateTime endTime;

	public Action CallBack;

	public float distance;

	public UISprite updatingEnum;

	private bool isEnd;

	public void SetUpdatingEum(int updateEnum)
	{
		switch (updateEnum)
		{
		case 1:
			this.updatingEnum.spriteName = "建筑升级";
			break;
		case 2:
			this.updatingEnum.spriteName = "兵种升级";
			break;
		case 3:
			this.updatingEnum.spriteName = "科技升级";
			break;
		case 4:
			this.skillPri_Bac.SetActive(true);
			break;
		case 5:
			this.skillInter_Bac.SetActive(true);
			break;
		case 6:
			this.skillSenior_Bac.SetActive(true);
			break;
		default:
			this.updatingEnum.spriteName = string.Empty;
			break;
		}
	}

	private void Start()
	{
		this.tr = base.transform;
		this.cam = UIManager.inst.uiCamera;
		this.distance = CameraControl.inst.minfar - CameraControl.inst.bigfar;
	}

	private void OnEnable()
	{
		this.beginTime = TimeTools.GetNowTimeSyncServerToDateTime();
	}

	private void Update()
	{
		if (this.tar == null || this.cam == null || Camera.main == null || !this.tar.gameObject.activeInHierarchy)
		{
			NGUITools.Destroy(this.ga);
			return;
		}
		float num = this.yOffect - 1f / this.distance * (CameraControl.inst.MainCamera.transform.localPosition.z + Mathf.Abs(CameraControl.inst.bigfar));
		Vector3 position = new Vector3(this.tar.position.x, this.tar.position.y + num, this.tar.position.z);
		Vector3 vector = CameraControl.inst.MainCamera.WorldToScreenPoint(position);
		Vector3 vector2 = UIManager.inst.uiCamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, UIManager.inst.uiCamera.transform.position.z));
		this.tr.position = new Vector3(vector2.x, vector2.y, 0f);
		this.tr.localScale = Vector3.one * 0.6f;
		if (TimeTools.DateDiffToDouble(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime) > 0.0)
		{
			if (this.speendProcess)
			{
				this.speendProcess.fillAmount = (float)(TimeTools.DateDiffToDouble(this.beginTime, TimeTools.GetNowTimeSyncServerToDateTime()) / TimeTools.DateDiffToDouble(this.beginTime, this.endTime));
			}
			this.timetext.text = TimeTools.DateDiffToString(TimeTools.GetNowTimeSyncServerToDateTime(), this.endTime);
		}
		else
		{
			this.TimeEnd();
		}
	}

	public void TimeEnd()
	{
		if (this.isEnd)
		{
			NGUITools.Destroy(this.ga);
			return;
		}
		this.isEnd = true;
		if (this.CallBack != null)
		{
			this.CallBack();
			this.CallBack = null;
			NGUITools.Destroy(this.ga);
		}
		if (this.speendProcess)
		{
			this.speendProcess.fillAmount = 0f;
		}
		this.tar = null;
		this.timetext.text = string.Empty;
	}
}
