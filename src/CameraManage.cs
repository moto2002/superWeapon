using System;
using UnityEngine;

public class CameraManage : IMonoBehaviour
{
	public SenceType senceCameraType;

	private Camera camer;

	public override void Awake()
	{
		Loading.changeSence.Add(this);
		this.camer = base.GetComponent<Camera>();
		base.Awake();
	}

	private void FixedUpdate()
	{
		if (this.senceCameraType != Loading.senceType)
		{
			if (this.camer && this.camer.enabled)
			{
				this.camer.enabled = false;
			}
			if (this.ga && this.ga.activeSelf)
			{
				this.ga.SetActive(false);
			}
		}
		else
		{
			if (this.camer && !this.camer.enabled)
			{
				this.camer.enabled = true;
			}
			if (this.ga && !this.ga.activeSelf)
			{
				this.ga.SetActive(true);
			}
		}
	}

	public void ChangeSence(SenceType sence)
	{
		if (sence == this.senceCameraType)
		{
			if (this.ga)
			{
				this.ga.SetActive(true);
			}
			if (this.camer && !this.camer.enabled)
			{
				this.camer.enabled = true;
			}
		}
		else
		{
			if (this.ga)
			{
				this.ga.SetActive(false);
			}
			if (this.camer && this.camer.enabled)
			{
				this.camer.enabled = false;
			}
		}
	}
}
