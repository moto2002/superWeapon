using System;
using UnityEngine;

[RequireComponent(typeof(UILabel))]
public class UITimer : MonoBehaviour
{
	public enum TimeShowType
	{
		DD_HH_MM_SS,
		HH_MM_SS
	}

	public delegate string TimeFormater(string day, string hour, string min, string sec);

	public delegate void OnTimeOver();

	private float startTime;

	private float timeLength;

	private bool isStart;

	private float periodTime;

	private UILabel timeLabel;

	private float leftTime;

	public UITimer.TimeShowType showType;

	public UITimer.TimeFormater timeFormater;

	public UITimer.OnTimeOver onTimeOver;

	private void Start()
	{
		this.timeLabel = base.GetComponent<UILabel>();
	}

	private void FixedUpdate()
	{
		if (this.isStart)
		{
			this.periodTime += Time.deltaTime;
			if (this.periodTime >= 1f)
			{
				this.periodTime = 0f;
				this.leftTime = this.timeLength - (Time.realtimeSinceStartup - this.startTime);
				if (this.leftTime > 0f)
				{
					this.DrawText(this.leftTime);
				}
				else
				{
					this.isStart = false;
					this.DrawText(0f);
					if (this.onTimeOver != null)
					{
						this.onTimeOver();
					}
				}
			}
		}
	}

	public void StartCountDown(float time)
	{
		this.startTime = Time.realtimeSinceStartup;
		this.timeLength = time;
		this.isStart = true;
		this.periodTime = 0f;
	}

	public void Stop()
	{
		this.startTime = 0f;
		this.timeLength = 0f;
		this.isStart = false;
		this.periodTime = 0f;
	}

	private void DrawText(float time)
	{
		int num = (int)time;
		if (this.showType == UITimer.TimeShowType.DD_HH_MM_SS)
		{
			int num2 = num / 86400;
			int num3 = num % 86400 / 3600;
			int num4 = num % 3600 / 60;
			int num5 = num % 3600 % 60;
			string text = num2.ToString();
			string text2 = num3.ToString().PadLeft(2, '0');
			string text3 = num4.ToString().PadLeft(2, '0');
			string text4 = num5.ToString().PadLeft(2, '0');
			if (this.timeFormater != null)
			{
				string text5 = this.timeFormater(text, text2, text3, text4);
				this.timeLabel.text = text5;
			}
			else
			{
				this.timeLabel.text = string.Format("{0}天{1}时{2}分{3}秒", new object[]
				{
					text,
					text2,
					text3,
					text4
				});
			}
		}
		else
		{
			int num2 = 0;
			int num3 = num / 3600;
			int num4 = num % 3600 / 60;
			int num5 = num % 3600 % 60;
			string day = num2.ToString();
			string text6 = num3.ToString().PadLeft(2, '0');
			string text7 = num4.ToString().PadLeft(2, '0');
			string text8 = num5.ToString().PadLeft(2, '0');
			if (this.timeFormater != null)
			{
				string text9 = this.timeFormater(day, text6, text7, text8);
				this.timeLabel.text = text9;
			}
			else
			{
				this.timeLabel.text = string.Format("{0}时{1}分{2}秒", text6, text7, text8);
			}
		}
	}
}
