using System;
using UnityEngine;

public class fpscounter : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		base.useGUILayout = false;
	}

	private void Start()
	{
		if (!base.guiText)
		{
			LogManage.Log("UtilityFramesPerSecond needs a GUIText component!");
			base.enabled = false;
			return;
		}
		this.timeleft = this.updateInterval;
	}

	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			string text = string.Format("{0:F2} FPS", num);
			base.guiText.text = text;
			if (num < 30f)
			{
				base.guiText.material.color = Color.red;
			}
			else if (num < 10f)
			{
				base.guiText.material.color = Color.green;
			}
			else
			{
				base.guiText.material.color = Color.green;
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}
}
