using System;
using UnityEngine;

public class OnLineAward : MonoBehaviour
{
	public static OnLineAward _inst;

	private float timeRemaind;

	public UILabel Ttime;

	public static LoadReward laod = new LoadReward();

	public void OnDestroy()
	{
		OnLineAward._inst = null;
	}

	private void Start()
	{
		OnLineAward._inst = this;
	}

	private void Update()
	{
		if (OnLineAward.laod.step != 0)
		{
			if (TimeTools.GetNowTimeSyncServerToDateTime() >= OnLineAward.laod.time)
			{
				UnitConst.GetInstance().loadReward[OnLineAward.laod.step].isCanGetOnLine = true;
				this.Ttime.text = LanguageManage.GetTextByKey("可领取", "others");
				ButtonClick component = base.transform.parent.GetComponent<ButtonClick>();
				component.eventType = EventManager.EventType.MainPanel_OnLine;
			}
			else
			{
				ActivityPanelManager.isCanGetOnLine = false;
				TimeSpan timeSpan = OnLineAward.laod.time - TimeTools.GetNowTimeSyncServerToDateTime();
				if (timeSpan.Hours > 0)
				{
					this.Ttime.text = string.Format("{0}时{1}分{2}秒", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				}
				else if (timeSpan.Minutes > 0)
				{
					this.Ttime.text = string.Format("{0}分{1}秒", timeSpan.Minutes, timeSpan.Seconds);
				}
				else
				{
					this.Ttime.text = string.Format("{0}秒", timeSpan.Seconds);
				}
				ButtonClick component2 = base.transform.parent.GetComponent<ButtonClick>();
				component2.eventType = EventManager.EventType.none;
			}
		}
		else
		{
			base.transform.parent.gameObject.SetActive(false);
		}
	}
}
