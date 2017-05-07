using System;
using UnityEngine;

public class ArmyTitleShow : Tittle_3D
{
	private Camera cam;

	public UISprite speendProcess;

	public UILabel timetext;

	public UISprite black;

	public DateTime beginTime;

	public DateTime endTime;

	public Action CallBack;

	public float distance;

	public UISprite updatingEnum;

	private bool isEnd;

	public void SetUpdatingEum(int index, int level)
	{
		this.updatingEnum.spriteName = UnitConst.GetInstance().soldierConst[UnitConst.GetInstance().GetArmyId(index, level)].icon;
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
			if (this.black)
			{
				this.black.fillAmount = (float)(1.0 - TimeTools.DateDiffToDouble(this.beginTime, TimeTools.GetNowTimeSyncServerToDateTime()) / TimeTools.DateDiffToDouble(this.beginTime, this.endTime));
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
		if (this.black)
		{
			this.black.fillAmount = 1f;
		}
		this.tar = null;
		this.timetext.text = string.Empty;
	}
}
